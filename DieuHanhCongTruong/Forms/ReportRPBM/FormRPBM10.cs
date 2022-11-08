using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DieuHanhCongTruong.Properties;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Syncfusion.XlsIO;
using System.Globalization;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Common;

namespace DieuHanhCongTruong
{
    public partial class FormRPBM10 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;

        public FormRPBM10()
        {
            cn = cn = frmLoggin.sqlCon;
            InitializeComponent();
        }
        private void LoadData(string name, string date1, string date2)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;

                dataGridView1.Rows.Clear();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM10] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "' order by gid"), cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idDuAn = dr["ID"].ToString();
                        var tenDuAn = dr["Dự án"].ToString();
                        var startTime = dr["Thời gian"].ToString();
                        var diaDiem = dr["Địa điểm"].ToString();

                        dataGridView1.Rows.Add(indexRow, tenDuAn, startTime, diaDiem);
                        dataGridView1.Rows[indexRow - 1].Tag = idDuAn;

                        indexRow++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return;
            }
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtTImkiem.Text = "";
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
        private void FormRPBM10_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            //comboBox_TenDA.Items.Add("Chọn");
            foreach (DataRow dr in datatableProgram.Rows)
            {
                if (string.IsNullOrEmpty(dr["name"].ToString()))
                    continue;

                TxtTImkiem.Items.Add(dr["name"].ToString());
            }
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }

        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dataGridView1.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);
            if (e.ColumnIndex == 4)
            {
                SqlCommandBuilder sqlCommand = null;


                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM10.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM10_" + Guid.NewGuid().ToString();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pathFile = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                if (pathFile != null)
                {
                    using (ExcelEngine excelEngine = new ExcelEngine())
                    {
                        IApplication application = excelEngine.Excel;
                        application.DefaultVersion = ExcelVersion.Excel97to2003;

                        //Open an existing spreadsheet, which will be used as a template for generating the new spreadsheet.
                        //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
                        IWorkbook workbook;

                        //Open existing Excel template
                        //Stream cfFileStream = assembly.GetManifestResourceStream("TemplateMarker.Data.TemplateMarker.xlsx");
                        FileStream cfFileStream = new FileStream(sourceFile, FileMode.Open);
                        workbook = excelEngine.Excel.Workbooks.Open(cfFileStream);

                        //The first worksheet in the workbook is accessed.
                        IWorksheet worksheet = workbook.Worksheets[0];

                        //Create Template Marker processor.
                        //Apply the marker to export data from datatable to worksheet.
                        ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();
                        //marker.AddVariable("SalesList", table);

                        System.Data.DataTable datatable = new System.Data.DataTable();
                        SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(
                            "SELECT " +
                            "case when construct.nameId is null then tbl.construct_id_other else construct.nameId end as DVRP, " +
                            "tbl.* , " +
                            "Tinh.Ten as province_idST, " +
                            "cecm_programData.code as 'cecm_program_code' " +
                            "FROM RPBM10 tbl " +
                            "left join cecm_provinces Tinh on tbl.province_id = Tinh.id " +
                            "left join Cecm_ProgramStaff construct on tbl.construct_id = construct.id " +
                            "left join cecm_programData on cecm_programData.id = tbl.cecm_program_id " +
                            "where gid = " + id_kqks, cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                        //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                        sqlAdapterProvince.Fill(datatable);
                        marker.AddVariable("obj", datatable);
                        if (datatable.Rows.Count != 0)
                        {
                            DataRow dr = datatable.Rows[0];
                            DateTime now = DateTime.TryParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateST) ? dateST : DateTime.Now;
                            marker.AddVariable("Ngaybaocao", dr["province_idST"].ToString().Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);                          
                            marker.AddVariable("organ_receive", "Kính gửi: " + dr["organ_receive"].ToString());
                            marker.AddVariable("base_hdkt_number", "Thực hiện Hợp đồng kinh tế số " + dr["base_hdkt_number"].ToString() + " /HĐKT-RPBM ngày " + dr["dates_hdktST"].ToString() + "  được ký kết giữa " + dr["organization_signed"].ToString() + " về việc thi công gói thầu rà phá bom mìn, vật nổ dự án " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("deptid_rpbmST", "Hiện tại đơn vị " + dr["deptid_rpbmST"].ToString() + " đã tổ chức thi công gói thầu rà phá bom mìn vật nổ trên mặt bằng được giao theo hợp đồng đã ký kết.");
                            marker.AddVariable("organ_ngthu", "Kính đề nghị tổ chức " + dr["organ_ngthu"].ToString() + " nghiệm thu hoàn thành công tác rà phá bom mìn vật nổ. Cụ thể như sau: ");
                            marker.AddVariable("address_request", "Địa điểm: " + dr["address_request"].ToString() + " thi công rà phá bom mìn vật nổ dự án: " + dr["cecm_program_idST"].ToString());
                        }
                        
                        //marker.AddVariable("str", "Nguyễn Minh Hiếu");
                        marker.ApplyMarkers(Syncfusion.XlsIO.UnknownVariableAction.Skip);

                        //Saving and closing the workbook
                        workbook.SaveAs(pathFile);
                        workbook.ActiveSheetIndex = 0;

                        //Close the workbook
                        workbook.Close();
                        cfFileStream.Close();

                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook workbook1 = excel.Workbooks.Open(pathFile);
                        workbook1.Worksheets.get_Item(1).Activate();
                        workbook1.Save();
                        workbook1.Close();

                        FileInfo fi = new FileInfo(pathFile);
                        if (fi.Exists)
                        {
                            System.Diagnostics.Process.Start(pathFile);
                        }
                    }
                }
            }

            if (e.ColumnIndex ==5)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM10 left join cecm_programData on cecm_programData.id = RPBM10.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM10 lst = new classRPBM10();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.symbol = dr["symbol"].ToString();
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code= dr["cecm_program_code"].ToString();

                    lst.address = dr["address"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.organ_receive = dr["organ_receive"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.base_hdkt_number = dr["base_hdkt_number"].ToString();
                    lst.dates_hdktST = dr["dates_hdktST"].ToString();
                    lst.organization_signed = dr["organization_signed"].ToString();
                    lst.organ_ngthu = dr["organ_ngthu"].ToString();
                    lst.dates_requestST = dr["dates_requestST"].ToString();
                    lst.address_request = dr["address_request"].ToString();

                    lst.deptid_rpbm = int.Parse(dr["deptid_rpbm"].ToString());
                    try
                    {
                        lst.deptid_rpbmST = dr["deptid_rpbmST"].ToString();
                    }
                    catch
                    {
                        lst.deptid_rpbmST = "";
                    }
                    lst.construct_id = int.Parse(dr["construct_id"].ToString());
                    try
                    {
                        lst.construct_idST = dr["construct_idST"].ToString();
                    }
                    catch
                    {
                        lst.construct_idST = "";
                    }
                    lst.construct_id_other = dr["construct_id_other"].ToString();
                    lst.files = dr["files"].ToString();

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM10");
                    if (string.IsNullOrEmpty(saveLocation) == false)
                    {
                        System.IO.File.WriteAllText(saveLocation, json);
                        MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xuất file dữ liệu thất bại... ", "Thất bại");
                    }
                    //CompressedFile frm = new CompressedFile(LstDaily, Phieudieutra, Baocao, Bando);
                    //frm.ShowDialog();
                }
            }
            if (e.ColumnIndex == 6)
            {
                FrmThemmoiRPBM10 frm = new FrmThemmoiRPBM10(id_kqks);
                frm.Text = "CHỈNH SỬA PHIẾU YÊU CẦU NGHIỆM THU";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM10] WHERE gid = " + id_kqks, cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                    //UtilsDatabase.DeleteMemberByMainId(table_name, id_kqks);

                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                }
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
        }

        private void BtnThemmoi_Click(object sender, EventArgs e)
        {
            FrmThemmoiRPBM10 frm = new FrmThemmoiRPBM10(0);
            frm.Text = "THÊM MỚI PHIẾU YÊU CẦU NGHIỆM THU";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}


