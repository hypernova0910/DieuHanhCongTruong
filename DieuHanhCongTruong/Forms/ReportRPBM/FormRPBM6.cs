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
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Common;

namespace DieuHanhCongTruong
{
    public partial class FormRPBM6 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "REPORTTESTEQUIPMENT";
        public string field_name1 = "ReportTestEquipment_ReportTestEquipment_CDTMember";
        public string field_name2 = "ReportTestEquipment_ReportTestEquipment_MonitorMem";
        public string field_name3 = "ReportTestEquipment_ReportTestEquipment_ConstuctMem";
        public FormRPBM6()
        {
            cn = cn = frmLoggin.sqlCon;
            InitializeComponent();
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtTImkiem.Text = "";
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM6] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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

        private void FormRPBM2_Load(object sender, EventArgs e)
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
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;


                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM6.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM6_" + Guid.NewGuid().ToString();
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
                            "RPBM6.*, " +
                            "Tinh.Ten as province_idST, " +
                            "cecm_programData.code as 'cecm_program_code'  " +
                            "FROM RPBM6  " +
                            "left join cecm_provinces Tinh on RPBM6.province_id = Tinh.id " +
                            "left join cecm_programData on cecm_programData.id = RPBM6.cecm_program_id " +
                            "where gid = " + id_kqks, cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                        //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                        sqlAdapterProvince.Fill(datatable);
                        marker.AddVariable("obj", datatable);
                        if (datatable.Rows.Count != 0)
                        {
                            DataRow dr = datatable.Rows[0];
                            DateTime now = DateTime.Now;
                            marker.AddVariable("Ngaybaocao", dr["province_idST"].ToString().Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                            //marker.AddVariable("hdkt", " - Hợp đồng kinh tế số " + dr["num_economic_contracts"].ToString() + "/HĐKT-RPBM ngày " + dr["date_economic_contractsST"].ToString() + " được ký kết giữa " + dr["organization_signed"].ToString());
                            marker.AddVariable("homnay", "Hôm nay: " + dr["date_nowST"].ToString() + " , tại công trường thi công RPBM dự án: " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("datesST", "Các bên tiến hành kiểm tra quân số, trang bị kỹ thuật trước khi thi công rà phá bom mìn dự án: " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("amount", "Biên bản được lập thành " + dr["amount"].ToString() + "  bản có giá trị như nhau, để các bên có cơ sở thực hiện và lưu vào hồ sơ dự án.");
                        }

                        System.Data.DataTable datatableSub1 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub1 = null;
                        SqlDataAdapter sqlAdapterSub1 = null;
                        sqlAdapterSub1 = new SqlDataAdapter(string.Format("select long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", id_kqks, field_name1, table_name), cn);
                        sqlCommandSub1 = new SqlCommandBuilder(sqlAdapterSub1);
                        sqlAdapterSub1.Fill(datatableSub1);
                        marker.AddVariable("datatableSub1", datatableSub1);
                        bool coNguoiKy1 = false;
                        foreach (DataRow drDVKS in datatableSub1.Rows)
                        {
                            if (drDVKS["long5"].ToString() == "1")
                            {
                                marker.AddVariable("CDT", drDVKS["string1"].ToString());
                                coNguoiKy1 = true;
                                break;
                            }
                        }
                        if (!coNguoiKy1)
                        {
                            marker.AddVariable("CDT", "");
                        }

                        System.Data.DataTable datatableSub2 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub2 = null;
                        SqlDataAdapter sqlAdapterSub2 = null;
                        sqlAdapterSub2 = new SqlDataAdapter(string.Format("select long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", id_kqks, field_name2, table_name), cn);
                        sqlCommandSub2 = new SqlCommandBuilder(sqlAdapterSub2);
                        sqlAdapterSub2.Fill(datatableSub2);
                        marker.AddVariable("datatableSub2", datatableSub2);
                        bool coNguoiKy2 = false;
                        foreach (DataRow drCDT in datatableSub2.Rows)
                        {
                            if (drCDT["long5"].ToString() == "1")
                            {
                                marker.AddVariable("DVGS", drCDT["string1"].ToString());
                                coNguoiKy2 = true;
                                break;
                            }
                        }
                        if (!coNguoiKy2)
                        {
                            marker.AddVariable("DVGS", "");
                        }
                        System.Data.DataTable datatableSub3 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub3 = null;
                        SqlDataAdapter sqlAdapterSub3 = null;
                        sqlAdapterSub3 = new SqlDataAdapter(string.Format("select long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", id_kqks, field_name3, table_name), cn);
                        sqlCommandSub3 = new SqlCommandBuilder(sqlAdapterSub3);
                        sqlAdapterSub3.Fill(datatableSub3);
                        marker.AddVariable("datatableSub3", datatableSub3);
                        bool coNguoiKy3 = false;
                        foreach (DataRow drCDT in datatableSub3.Rows)
                        {
                            if (drCDT["long5"].ToString() == "1")
                            {
                                marker.AddVariable("DVTC", drCDT["string1"].ToString());
                                coNguoiKy3 = true;
                                break;
                            }
                        }
                        if (!coNguoiKy3)
                        {
                            marker.AddVariable("DVTC", "");
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

            if (e.ColumnIndex == 5)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM6 left join cecm_programData on cecm_programData.id = RPBM6.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM6 lst = new classRPBM6();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.category = int.Parse(dr["category"].ToString().Split('.')[0]);
                    lst.categoryST = dr["category"].ToString().Split('.')[1];
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.date_nowST = dr["date_nowST"].ToString();
                    lst.num_team_bctc = int.Parse(dr["num_team_bctc"].ToString());
                    lst.num_min = double.Parse(dr["num_min"].ToString());
                    lst.num_bomb = double.Parse(dr["num_bomb"].ToString());
                    lst.num_elect = double.Parse(dr["num_elect"].ToString());
                    lst.num_talkie = double.Parse(dr["num_talkie"].ToString());
                    lst.num_gps = double.Parse(dr["num_gps"].ToString());
                    lst.num_car = double.Parse(dr["num_car"].ToString());
                    lst.num_carST = dr["num_carST"].ToString();
                    lst.conclusion = dr["conclusion"].ToString();
                    lst.amount = float.Parse(dr["amount"].ToString());
                    lst.boss_id = int.Parse(dr["boss_id"].ToString());
                    try
                    {
                        lst.boss_idST = dr["boss_idST"].ToString();
                    }
                    catch
                    {
                        lst.boss_idST = "";
                    }
                    lst.boss_id_other = dr["boss_id_other"].ToString();
                    lst.monitor_id = int.Parse(dr["monitor_id"].ToString());
                    try
                    {
                        lst.monitor_idST = dr["monitor_idST"].ToString();
                    }
                    catch
                    {
                        lst.monitor_idST = "";
                    }

                    lst.monitor_id_other = dr["monitor_id_other"].ToString();
                    lst.constuct_id = int.Parse(dr["constuct_id"].ToString());
                    try
                    {
                        lst.constuct_idST = dr["constuct_idST"].ToString();
                    }
                    catch
                    {
                        lst.constuct_idST = "";
                    }

                    lst.constuct_id_other = dr["constuct_id_other"].ToString();
                    try
                    {
                        lst.deptid_load = int.Parse(dr["deptid_load"].ToString());
                        lst.deptid_loadST = dr["deptid_loadST"].ToString();
                    }
                    catch
                    {
                        lst.deptid_load = 0;
                        lst.deptid_loadST = "";
                    }
                    lst.files = dr["files"].ToString();

                    List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst2 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst3 = new List<groundDeliveryRecords_Member>();

                    SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "'", cn);
                    sqlCommand = new SqlCommandBuilder(sqlAdapterProvince1);
                    System.Data.DataTable datatableProvince1 = new System.Data.DataTable();
                    sqlAdapterProvince1.Fill(datatableProvince1);
                    foreach (DataRow dr1 in datatableProvince1.Rows)
                    {
                        if (dr1["field_name"].ToString().Contains(field_name1))
                        {
                            groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                            A.gid = int.Parse(dr1["gid"].ToString());
                            A.table_name = dr1["table_name"].ToString();
                            A.field_name = dr1["field_name"].ToString();
                            A.main_id = int.Parse(dr1["main_id"].ToString());
                            A.long1 = int.TryParse(dr1["long1"].ToString(), out int long1) ? long1 : -1;
                            A.long2 = int.TryParse(dr1["long2"].ToString(), out int long2) ? long2 : -1;
                            A.long1ST = dr1["long1ST"].ToString();
                            A.long2ST = dr1["long2ST"].ToString();
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            //A.long2 = dr1["long2"].ToString();
                            A.long5 = dr1["long5"].ToString();

                            Lst1.Add(A);
                        }
                        if (dr1["field_name"].ToString().Contains(field_name2))
                        {
                            groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                            A.gid = int.Parse(dr1["gid"].ToString());
                            A.table_name = dr1["table_name"].ToString();
                            A.field_name = dr1["field_name"].ToString();
                            A.main_id = int.Parse(dr1["main_id"].ToString());
                            A.long1 = int.TryParse(dr1["long1"].ToString(), out int long1) ? long1 : -1;
                            A.long2 = int.TryParse(dr1["long2"].ToString(), out int long2) ? long2 : -1;
                            A.long1ST = dr1["long1ST"].ToString();
                            A.long2ST = dr1["long2ST"].ToString();
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            //A.long2 = dr1["long2"].ToString();
                            A.long5 = dr1["long5"].ToString();

                            Lst2.Add(A);
                        }
                        if (dr1["field_name"].ToString().Contains(field_name3))
                        {
                            groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                            A.gid = int.Parse(dr1["gid"].ToString());
                            A.table_name = dr1["table_name"].ToString();
                            A.field_name = dr1["field_name"].ToString();
                            A.main_id = int.Parse(dr1["main_id"].ToString());
                            A.long1 = int.TryParse(dr1["long1"].ToString(), out int long1) ? long1 : -1;
                            A.long2 = int.TryParse(dr1["long2"].ToString(), out int long2) ? long2 : -1;
                            A.long1ST = dr1["long1ST"].ToString();
                            A.long2ST = dr1["long2ST"].ToString();
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            //A.long2 = dr1["long2"].ToString();
                            A.long5 = dr1["long5"].ToString();

                            Lst3.Add(A);
                        }

                    }
                    lst.reportTestEquipment_CDTMember_lstSubTable = Lst1;
                    lst.reportTestEquipment_MonitoMem_lstSubTable = Lst2;
                    lst.reportTestEquipment_ConsMem_lstSubTable = Lst3;
                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);
                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM6");
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
                UtilsDatabase.DeleteMemberByTablename(table_name);
                FrmThemmoiRPBM6 frm = new FrmThemmoiRPBM6(id_kqks);
                frm.Text = "CHỈNH SỬA BIÊN BẢN KIỂM TRA QUÂN SỐ, TRANG BỊ KĨ THUẬT";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM6] WHERE gid = " + id_kqks, cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                    UtilsDatabase.DeleteMemberByMainId(table_name, id_kqks);

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
            UtilsDatabase.DeleteMemberByTablename(table_name);
            FrmThemmoiRPBM6 frm = new FrmThemmoiRPBM6(0);
            frm.Text = "THÊM MỚI BIÊN BẢN KIỂM TRA QUÂN SỐ, TRANG BỊ KĨ THUẬT";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}

