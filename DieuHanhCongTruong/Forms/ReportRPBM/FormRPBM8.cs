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
    public partial class FormRPBM8 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "REPORTPROBSCENE"; 
        public string field_name1 = "ReportProbScene_ReportProbScene_CDTMember";
        public string field_name2 = "ReportProbScene_ReportProbScene_MonitorMem";
        public string field_name3 = "ReportProbScene_ReportProbScene_ConstuctMem";
        public FormRPBM8()
        {
            cn = frmLoggin.sqlCon;
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM8] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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

        private void FormRPBM8_Load(object sender, EventArgs e)
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


                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM8.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM8_" + Guid.NewGuid().ToString();
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
                            "SELECT RPBM8.* , " +
                            "Tinh.Ten as province_idST, " +
                            "cecm_programData.code as 'cecm_program_code'  " +
                            "FROM RPBM8 " +
                            "left join cecm_programData on cecm_programData.id = RPBM8.cecm_program_id " +
                            "left join cecm_provinces Tinh on RPBM8.province_id = Tinh.id " +
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
                            marker.AddVariable("num_point_test", "Tiến hành kiểm tra " + dr["num_point_test"].ToString() + " điểm tại khu vực thi công gói thầu RPBM thuộc dự án: " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("cancu", "Căn cứ Quy trình kỹ thuật ĐT, KS, RPBM "+ dr["qtkt"].ToString() + ", Quy chuẩn kỹ thuật Quốc gia " + dr["qckt"].ToString());

                            marker.AddVariable("Doichieu", "Đối chiếu khối lượng đã thi công rà phá bom mìn vật nổ theo Quy định của Bộ Quốc phòng. Hôm nay " + dr["dates_nowST"].ToString() + " chúng tôi tiến hành kiểm tra xác suất thi công RPBM trên mặt bằng dự án " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("num_machine_mine", "- Máy dò mìn: " + dr["num_machine_mine"].ToString() + "  : Sử dụng kiểm tra tín hiệu dò tìm trên cạn ở độ sâu 0,3m hoặc 0,5m");
                            marker.AddVariable("num_machine_bomb", "- Máy dò bom: " + dr["num_machine_bomb"].ToString() + " : Sử dụng kiểm tra tín hiệu dò tìm trên cạn đến độ sâu 5m …..");
                            marker.AddVariable("area_test", "- b) Phạm vi diện tích kiểm tra: " + dr["area_test"].ToString() + " m2");
                            marker.AddVariable("num_o_test", "- Kiểm tra " + dr["num_o_test"].ToString() + " ô thi công. Kích thước 01 ô: 20 m x 20 m = 400 m2.");
                            marker.AddVariable("area_o_test", "Diện tích kiểm tra: ô x 400 m2 = " + dr["area_o_test"].ToString() + " m2");
                            marker.AddVariable("deep_o_test", "Kiểm tra dò tìm trên cạn ở các độ sâu 0,3 m đến  " + dr["deep_o_test"].ToString() + " m. Độ sâu dò tìm tính từ mặt đất tự nhiên hiện tại trở xuống.");
                            marker.AddVariable("result_test", "Kết quả: Toàn bộ diện tích các ô kiểm tra ngẫu nhiên theo xác suất không phát hiện tín hiệu bom mìn vật nổ ở các độ sâu được kiểm tra, phát hiện " + dr["result_test"].ToString());
                            marker.AddVariable("qtkt", "Thi công đảm bảo chất lượng đúng yêu cầu của hồ sơ Phương án KTTC đã được duyệt, tuân thủ Quy trình kỹ thuật " + dr["qtkt"].ToString() + " Quy chuẩn kỹ thuật " + dr["qckt"].ToString());
                            marker.AddVariable("Mbtc", "Mặt bằng khu vực thi công gói thầu rà phá bom mìn vật nổ dự án: " + dr["cecm_program_idST"].ToString() + " đảm bảo an toàn, đạt chất lượng yêu cầu cầu kỹ thuật. Đồng ý triển khai các công việc tiếp theo.");
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

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM8 left join cecm_programData on cecm_programData.id = RPBM8.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM8 lst = new classRPBM8();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();

                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.dates_nowST = dr["dates_nowST"].ToString();
                    lst.base_on_tech = dr["base_on_tech"].ToString();
                    lst.num_point_test = dr["num_point_test"].ToString();
                    lst.result_rpbm = dr["result_rpbm"].ToString();
                    lst.num_machine_mine = double.Parse(dr["num_machine_mine"].ToString());
                    lst.num_machine_bomb = double.Parse(dr["num_machine_bomb"].ToString());
                    lst.area_test = double.Parse(dr["area_test"].ToString());
                    lst.num_o_test = double.Parse(dr["num_o_test"].ToString());
                    lst.area_o_test = double.Parse(dr["area_o_test"].ToString());
                    lst.deep_o_test = double.Parse(dr["deep_o_test"].ToString());

                    lst.result_test = dr["result_test"].ToString();
                    lst.qtkt = dr["qtkt"].ToString();
                    lst.qckt = dr["qckt"].ToString();

                    lst.boss_id = int.Parse(dr["boss_id"].ToString());
                    lst.boss_idST = dr["boss_idST"].ToString();
                    lst.boss_id_other = dr["boss_id_other"].ToString();
                    lst.monitor_id = int.Parse(dr["monitor_id"].ToString());
                    lst.monitor_idST = dr["monitor_idST"].ToString();
                    lst.monitor_id_other = dr["monitor_id_other"].ToString();
                    lst.constuct_id = int.Parse(dr["constuct_id"].ToString());
                    lst.constuct_idST = dr["constuct_idST"].ToString();
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
                    lst.reportProbScene_CDTMember_lstSubTable = Lst1;
                    lst.reportProbScene_MonitoMem_lstSubTable = Lst3;
                    lst.reportProbScene_ConsMem_lstSubTable = Lst2;

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM8");
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
                FrmThemmoiRPBM8 frm = new FrmThemmoiRPBM8(id_kqks);
                frm.Text = "CHỈNH SỬA BIÊN BẢN KIỂM TRA XÁC SUẤT HIỆN TRƯỜNG";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM8] WHERE gid = " + id_kqks, cn);
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
            FrmThemmoiRPBM8 frm = new FrmThemmoiRPBM8(0);
            frm.Text = "THÊM MỚI BIÊN BẢN KIỂM TRA XÁC SUẤT HIỆN TRƯỜNG";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}


