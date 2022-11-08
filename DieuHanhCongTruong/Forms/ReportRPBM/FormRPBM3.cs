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
    public partial class FormRPBM3 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "TESTRECORDRESULT";
        public string field_name1 = "TestRecordResult_TestRecordResult_CdtMember";
        public string field_name2 = "TestRecordResult_TestRecordResult_SurMember";
        public FormRPBM3()
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from RPBM3 where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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

        private void FormRPBM3_Load(object sender, EventArgs e)
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

        private void FormRPBM3_Load_1(object sender, EventArgs e)
        {

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
                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM3.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM3_" + Guid.NewGuid().ToString();
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
                        SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM3 left join cecm_programData on cecm_programData.id = RPBM3.cecm_program_id where gid = " + id_kqks, cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                        //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                        sqlAdapterProvince.Fill(datatable);
                        marker.AddVariable("obj", datatable);
                        if (datatable.Rows.Count != 0)
                        {
                            DataRow dr = datatable.Rows[0];
                            DateTime now = DateTime.Now;
                            marker.AddVariable("Ngaybaocao", dr["address"].ToString().Split(',')[2].Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                            marker.AddVariable("HDKT", " - Hợp đồng kinh tế số " + dr["base_hdkt_number"].ToString() + "/HĐKT-RPBM ngày " + dr["dates_hdktST"].ToString() + " được ký kết giữa " + dr["organization_signed"].ToString());
                            marker.AddVariable("Homnay", "Hôm nay, ngày " + dr["dates_nowST"].ToString() + " tại hiện trường mặt bằng dự án: " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("DVKS", "Đơn vị khảo sát đã tiến hành thi công khảo sát đánh giá, thu thập số liệu mật độ tín hiệu, bom đạn, phạm vi rà phá bom mìn vật nổ mặt bằng dự án:" + dr["cecm_program_idST"].ToString() + " theo đúng kế hoạch, Quy trình kỹ thuật, Quy chuẩn quốc gia: " + dr["base_qcqg"].ToString());
                            marker.AddVariable("CTKS", "Thực hiện công tác khảo sát thu thập số liệu phục vụ lập phương án kỹ thuật thi công và dự toán dự án: " + dr["cecm_program_idST"].ToString() + ", đơn vị đã tiến hành thi công các điểm khảo sát...(căn cứ vào diện tích cần RPBM để xác định số lượng điểm khảo sát và diện tích cần khảo sát theo quy định). Mỗi điểm khảo sát có kích thước ô là 25 m x 25 m = 500 m2 (tùy theo địa hình và tính chất của dự án để xác định kích thước).");
                            marker.AddVariable("area_rpbm", "a) Diện tích RPBM của dự án là: " + dr["area_rpbm"].ToString() + "ha");
                            marker.AddVariable("area_tcks", "b) Diện tích thi công khảo sát: " + dr["area_tcks"].ToString() + " ha. Cụ thể như sau:");
                            marker.AddVariable("area_phatdon", "- Diện tích phát dọn tương đương rừng loại: " + dr["area_phatdon"].ToString() + " ha");
                            marker.AddVariable("matdo_phatdon", "- Diện tích khảo sát đến độ sâu 0,3 m (hoặc 0,5 m) mật độ loại " + dr["matdo_phatdon"].ToString() + " : " + dr["area_ks_1"].ToString() + " ha");
                            marker.AddVariable("signal_process", "- Xử lý tín hiệu đến độ sâu 0,3 m (hoặc 0,5 m, 1 m): " + dr["signal_process"].ToString() + " tín hiệu");
                            marker.AddVariable("area_ks_2", "- Diện tích khảo sát rà phá bom mìn vật nổ đến độ sâu 5 m: " + dr["area_ks_2"].ToString() + " ha");
                            marker.AddVariable("dig_lane_signal_1", "- Đào, xử lý tín hiệu độ sâu 0,3 ¸ 3 m: " + dr["dig_lane_signal_1"].ToString() + " tín hiệu");
                            marker.AddVariable("dig_lane_signal_2", "- Đào, xử lý tín hiệu độ sâu 5 m: " + dr["dig_lane_signal_2"].ToString() + " tín hiệu");
                            marker.AddVariable("result", "* Kết quả thu được: " + dr["result"].ToString());
                            marker.AddVariable("type_forest", "- Tỷ lệ phát dọn mặt bằng thi công tương đương rừng loại " + dr["type_forest_water"].ToString() + " chiếm " + dr["ratio_clean_water"].ToString() + " %");
                            marker.AddVariable("type_signal_density_1", "- Mật độ tín hiệu trên cạn đến độ sâu 0,3 m (hoặc 0,5 m, 1 m), mật độ loại " + dr["type_signal_density_1"].ToString() + " trung bình: " + dr["avg_signal_density_1"].ToString() + " tín hiệu/ha");
                            marker.AddVariable("avg_signal_density_2", "- Mật độ tín hiệu trên cạn đến độ sâu 3 m trung bình: " + dr["avg_signal_density_2"].ToString() + " tín hiệu/ha");
                            marker.AddVariable("avg_signal_density_3", "- Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình: " + dr["avg_signal_density_3"].ToString() + " tín hiệu/ha");
                            marker.AddVariable("type_land_1", "- Cấp đất tại khu vực thi công ở độ sâu 0,3 m là đất cấp " + dr["type_land_1"].ToString() + " ; lớn hơn 0,3m đến 5 m là đất cấp " + dr["type_land_2"].ToString());
                            marker.AddVariable("type_forest_water", "- Tỷ lệ phát dọn mặt bằng thi công tương đương rừng loại " + dr["type_forest_water"].ToString() + " chiếm " + dr["ratio_clean_water"].ToString() + " %");
                            marker.AddVariable("type_signal_density_2", "- Mật độ tín hiệu đến độ sâu 0,5 m, mật độ loại " + dr["type_signal_density_2"].ToString() + " trung bình: " + dr["avg_signal_density_4"].ToString() + " tín hiệu/ha");
                            marker.AddVariable("avg_signal_density_5", "- Mật độ tín hiệu trên cạn đến độ sâu 3 m trung bình: " + dr["avg_signal_density_5"].ToString() + " tín hiệu/ha");
                            marker.AddVariable("avg_signal_density_6", "- Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình: " + dr["avg_signal_density_6"].ToString() + " tín hiệu/ha");

                            marker.AddVariable("eval_water_flow", "- Đánh giá về lưu tốc nước chảy: " + dr["eval_water_flow"].ToString() + " m/s");

                            marker.AddVariable("deep_water", "- Độ sâu nước: " + dr["deep_water"].ToString() + " ; Phạm vi khu vực cần RPBM: " + dr["limit_area_rpbm"].ToString());
                            marker.AddVariable("signal_sea_1", "- Mật độ tín hiệu trên bề mặt đáy biển, qua xử lý dữ liệu từ hệ thống sona và từ kế thu được " + dr["signal_sea_1"].ToString() + " tín hiệu");
                            marker.AddVariable("signal_sea_2", "- Mật độ tín hiệu từ bề mặt đáy biển đến độ sâu 1 m, qua xử lý dữ liệu từ hệ thống sona và từ kế thu được " + dr["signal_sea_2"].ToString() + " tín hiệu");
                            marker.AddVariable("deep_sea", "- Độ sâu nước biển: " + dr["deep_sea"].ToString());
                            marker.AddVariable("conclusion", "5. Kết luận: " + dr["conclusion"].ToString());
                            marker.AddVariable("cecm_program_idST", "Đồng ý nghiệm thu kết quả thi công khảo sát tại các khu vực thuộc phạm vi dự án làm cơ sở lập phương án kỹ thuật thi công và dự toán rà phá bom mìn vật nổ dự án: " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("num_all_report", "Biên bản này được lập thành " + dr["num_all_report"].ToString() + " bản có giá trị pháp lý như nhau, chủ đầu tư giữ " + dr["num_cdt_report"].ToString() + "  bản, đơn vị khảo sát giữ " + dr["num_ks_report"].ToString());
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
                                marker.AddVariable("boss_id", drDVKS["string1"].ToString());
                                coNguoiKy1 = true;
                                break;
                            }
                        }
                        if (!coNguoiKy1)
                        {
                            marker.AddVariable("boss_id", "");
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
                                marker.AddVariable("survey_id", drCDT["string1"].ToString());
                                coNguoiKy2 = true;
                                break;
                            }
                        }
                        if (!coNguoiKy2)
                        {
                            marker.AddVariable("survey_id", "");
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

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM3 left join cecm_programData on cecm_programData.id = RPBM3.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM3 lst = new classRPBM3();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.base_qtkt = dr["base_qtkt"].ToString();
                    lst.dates_qtktST = dr["dates_qtktST"].ToString();
                    lst.base_qcqg = dr["base_qcqg"].ToString();
                    lst.dates_hdktST = dr["dates_hdktST"].ToString();
                    lst.base_hdkt_number = dr["base_hdkt_number"].ToString();
                    lst.dates_hdktST = dr["dates_hdktST"].ToString();
                    lst.organization_signed = dr["organization_signed"].ToString();
                    lst.dates_nowST = dr["dates_nowST"].ToString();
                    lst.time_nt_fromST = dr["time_nt_fromST"].ToString();
                    lst.time_nt_toST = dr["time_nt_toST"].ToString();
                    lst.quality_ks = dr["quality_ks"].ToString();
                    lst.quymo_ks = dr["quymo_ks"].ToString();
                    lst.phamvi_dtich_ks = dr["phamvi_dtich_ks"].ToString();
                    lst.area_rpbm = double.Parse(dr["area_rpbm"].ToString());
                    lst.area_tcks = dr["area_tcks"].ToString();
                    lst.area_phatdon = dr["area_phatdon"].ToString();
                    lst.matdo_phatdon = dr["matdo_phatdon"].ToString();
                    lst.area_ks_1 = dr["area_ks_1"].ToString();
                    lst.signal_process = dr["signal_process"].ToString();
                    lst.area_ks_2 = dr["area_ks_2"].ToString();
                    lst.dig_lane_signal_1 = dr["dig_lane_signal_1"].ToString();
                    lst.dig_lane_signal_2 = dr["dig_lane_signal_2"].ToString();
                    lst.result = dr["result"].ToString();
                    lst.type_forest  = dr["type_forest"].ToString();
                    lst.ratio_clean_ground = dr["ratio_clean_ground"].ToString();
                    lst.type_signal_density_1 = dr["type_signal_density_1"].ToString();
                    lst.avg_signal_density_1 = dr["avg_signal_density_1"].ToString();
                    lst.avg_signal_density_2 = dr["avg_signal_density_2"].ToString();
                    lst.avg_signal_density_3 = dr["avg_signal_density_3"].ToString();
                    lst.type_land_1 = dr["type_land_1"].ToString();
                    lst.type_land_2 = dr["type_land_2"].ToString();
                    lst.ratio_clean_water = dr["ratio_clean_water"].ToString();
                    lst.type_forest_water = dr["type_forest_water"].ToString();
                    lst.type_signal_density_2 = dr["type_signal_density_2"].ToString();
                    lst.avg_signal_density_4 = dr["avg_signal_density_4"].ToString();
                    lst.avg_signal_density_5 = dr["avg_signal_density_5"].ToString();
                    lst.avg_signal_density_6 = dr["avg_signal_density_6"].ToString();
                    lst.eval_water_flow = dr["eval_water_flow"].ToString();
                    lst.deep_water = dr["deep_water"].ToString();
                    lst.limit_area_rpbm = dr["limit_area_rpbm"].ToString();
                    lst.signal_sea_1 = dr["signal_sea_1"].ToString();
                    lst.signal_sea_2 = dr["signal_sea_2"].ToString();
                    lst.deep_sea = dr["deep_sea"].ToString();
                    lst.conclusion = dr["conclusion"].ToString();
                    lst.num_all_report = double.Parse(dr["num_all_report"].ToString());
                    lst.num_cdt_report = double.Parse(dr["num_cdt_report"].ToString());
                    lst.num_ks_report = double.Parse(dr["num_ks_report"].ToString());
                    try
                    {
                        lst.boss_id = int.Parse(dr["boss_id"].ToString());
                        lst.boss_idST = dr["boss_idST"].ToString();
                        lst.boss_id_other = dr["boss_id_other"].ToString();
                    }
                    catch
                    {
                        lst.boss_id = 0;
                        lst.boss_idST = "";
                        lst.boss_id_other = dr["boss_id_other"].ToString();
                    }
                    try
                    {
                        lst.survey_id = int.Parse(dr["survey_id"].ToString());
                        lst.survey_idST = dr["survey_idST"].ToString();
                        lst.survey_id_other = dr["survey_id_other"].ToString();
                    }
                    catch
                    {
                        lst.survey_id = 0;
                        lst.survey_idST = "";
                        lst.survey_id_other = dr["survey_id_other"].ToString();
                    }

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
                            A.long1 = int.Parse(dr1["long1"].ToString());
                            A.long2 = int.Parse(dr1["long2"].ToString());
                            A.long1ST = dr1["long1ST"].ToString();
                            A.long2ST = dr1["long2ST"].ToString();
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            //A.long2 = dr1["long2"].ToString();
                            A.long5 = dr1["long5"].ToString();

                            Lst1.Add(A);
                        }
                        else
                        {
                            groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                            A.gid = int.Parse(dr1["gid"].ToString());
                            A.table_name = dr1["table_name"].ToString();
                            A.field_name = dr1["field_name"].ToString();
                            A.main_id = int.Parse(dr1["main_id"].ToString());
                            A.long1 = int.Parse(dr1["long1"].ToString());
                            A.long2 = int.Parse(dr1["long2"].ToString());
                            A.long1ST = dr1["long1ST"].ToString();
                            A.long2ST = dr1["long2ST"].ToString();
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            //A.long2 = dr1["long2"].ToString();
                            A.long5 = dr1["long5"].ToString();

                            //A.long2 = dr1["long2"].ToString();
                            Lst2.Add(A);
                        }
                        
                    }
                    //string json = JsonSerializer.Serialize(Lst);
                    //File.WriteAllText(@"E:\LstResult.json", json);
                    lst.testRecordResult_CdtMember_lstSubTable = Lst1;
                    lst.testRecordResult_SurMember_lstSubTable = Lst2;
                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM3");
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
                FrmThemmoiRPBM3 frm = new FrmThemmoiRPBM3(id_kqks);
                frm.Text = "CHỈNH SỬA BIÊN BẢN NGHIỆM THU KẾT QUẢ KHẢO SÁT";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM RPBM3 WHERE gid = " + id_kqks, cn);
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
            FrmThemmoiRPBM3 frm = new FrmThemmoiRPBM3(0);
            frm.Text = "THÊM MỚI BIÊN BẢN NGHIỆM THU KẾT QUẢ KHẢO SÁT";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}

