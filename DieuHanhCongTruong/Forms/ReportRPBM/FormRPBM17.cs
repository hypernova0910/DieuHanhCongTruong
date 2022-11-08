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
    public partial class FormRPBM17 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "ReportResConsRPBM";
        public string field_name1 = "ReportResConsRPBM_ReportResConsRPBM_Work";

        public FormRPBM17()
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM17] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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

        private void FormRPBM17_Load(object sender, EventArgs e)
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
            //if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
            //    return;
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
            if (e.ColumnIndex == cotExcel.Index)
            {
                //SqlCommandBuilder sqlCommand = null;

                //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM17 left join cecm_programData on cecm_programData.id = RPBM17.cecm_program_id where gid = " + id_kqks, cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                //sqlAdapterProvince.Fill(datatableProvince);
                //foreach (DataRow dr in datatableProvince.Rows)
                //{
                //    string pathFile = AppUtils.SaveExcel("BB_RPBM17");

                //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                //    if (pathFile != null)
                //    {
                //        //open the excel
                //        Workbook xlWorkbooK = xlApp.Workbooks.Open(pathFile);
                //        //get the first sheet of the excel
                //        Worksheet xlWorkSheet = (Worksheet)xlWorkbooK.Worksheets.get_Item(1);
                //        try
                //        {
                //            Range range = xlWorkSheet.UsedRange;
                //            int rowCount = range.Rows.Count;
                //            int columnCount = range.Columns.Count;
                //            xlWorkSheet.Cells[2, 2] = dr["deptid_hrpbmST"].ToString();
                //            xlWorkSheet.Cells[5, 2] = dr["address"].ToString() + ", " + dr["datesST"].ToString();
                //            xlWorkSheet.Cells[8, 2] = "Kính gửi: " + dr["organ_receive"].ToString();
                //            xlWorkSheet.Cells[9, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            xlWorkSheet.Cells[10, 2] = "Hạng mục: " + dr["categoryST"].ToString();
                //            xlWorkSheet.Cells[11, 2] = "Địa điểm: " + dr["address_cecm"].ToString();

                //            xlWorkSheet.Cells[14, 2] = "- Thông tư số " + dr["base_on_thongtu"].ToString();
                //            xlWorkSheet.Cells[15, 2] = "- Quy chuẩn kỹ thuật " + dr["technical_regulations"].ToString();
                //            xlWorkSheet.Cells[16, 2] = "- Hợp đồng kinh tế số "+ dr["num_economic_contracts"].ToString() +" " + dr["date_economic_contractsST"].ToString() +" được ký kết giữa "+ dr["organization_signed"].ToString() +" về việc thi công gói thầu rà phá bom mìn vật nổ dự án: " + dr["cecm_program_idST"].ToString() +" địa điểm "+ dr["address_cecm"].ToString();
                //            xlWorkSheet.Cells[19, 2] = "1. Nhiệm vụ: " + dr["mission"].ToString();
                //            xlWorkSheet.Cells[20, 2] = "Rà phá tất cả các loại bom mìn vật nổ trên mặt bằng dự án: " + dr["cecm_program_idST"].ToString() + " địa điểm " + dr["address_cecm"].ToString() + " để đảm bảo an toàn cho thi công xây dựng các hạng mục công trình tiếp theo …………….";
                //            xlWorkSheet.Cells[21, 2] = "Độ sâu RPBM: đến "+ dr["deep_rpbm"].ToString() +" m, tính từ mặt đất tự nhiên hiện tại trở xuống.";
                //            xlWorkSheet.Cells[30, 2] = "- Bắt đầu từ ngày: " + dr["date_startST"].ToString();
                //            xlWorkSheet.Cells[31, 2] = "- Thời gian hoàn thành ngày: " + dr["dates_endST"].ToString();
                //            xlWorkSheet.Cells[34, 2] = "- Thời gian hoàn thành ngày: " + dr["dates_endST"].ToString();
                //            if (dr["command_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[34, 2] = "- Điều động lực lượng thi công rà phá bom mìn vật nổ do đồng chí" + dr["command_idST"].ToString() + " chỉ huy chung.";
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[34, 2] = "- Điều động lực lượng thi công rà phá bom mìn vật nổ do đồng chí" + dr["command_id_other"].ToString() + " chỉ huy chung.";
                //            }
                //            xlWorkSheet.Cells[35, 2] = "- Lực lượng thi công gồm: "+ dr["num_team"].ToString() +" Đội RPBM, quân số theo biên chế (Có danh sách và quyết định điều động lực lượng kèm theo).";
                //            xlWorkSheet.Cells[36, 2] = "- Trang bị mỗi đội: "+ dr["number_driver"].ToString() +" xe ô tô, "+ dr["number_machine_min"].ToString() +" máy dò mìn, "+ dr["number_machine_bomb"].ToString() +" máy dò bom và thiết bị cầm tay đồng bộ, đầy đủ theo biên chế quy định…";
                //            xlWorkSheet.Cells[38, 2] = "- Diện tích thi công RPBM cho dự án là: "+ dr["area_rapha"].ToString()+" ha (đã được thể hiện tại bản vẽ thiết kế mặt bằng, tọa độ...)";
                //            xlWorkSheet.Cells[39, 2] = "- Độ sâu RPBM: đến "+ dr["deep_rapha"].ToString()+" m, tính từ mặt đất tự nhiên hiện tại trở xuống.";

                //            int indexRow = 42, stt = 1;
                //            int A1 = indexRow;
                //            List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                //            SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "'", cn);
                //            sqlCommand = new SqlCommandBuilder(sqlAdapterProvince1);
                //            System.Data.DataTable datatableProvince1 = new System.Data.DataTable();
                //            sqlAdapterProvince1.Fill(datatableProvince1);
                //            foreach (DataRow dr1 in datatableProvince1.Rows)
                //            {
                //                if (dr1["field_name"].ToString().Contains(field_name1))
                //                {
                //                    xlWorkSheet.Cells[indexRow, 2] = dr1["gid"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 5] = dr1["string1"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 13] = dr1["string2"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 15] = dr1["string3"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 17] = dr1["string4"].ToString();
                //                    indexRow++;
                //                }
                //            }
                //            if (A1 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            xlWorkSheet.Cells[indexRow, 2] = "* Kết quả thu được: " + dr["result"].ToString();
                //            indexRow = indexRow + 2;
                //            xlWorkSheet.Cells[indexRow, 2] = dr["comment_tc"].ToString();
                //            indexRow = indexRow + 1;
                //            xlWorkSheet.Cells[indexRow, 2] = "7. Kết luận: " + dr["conclusion"].ToString();
                //            indexRow = indexRow + 1;
                //            xlWorkSheet.Cells[indexRow, 2] = "Thực hiện Hợp đồng thi công gói thầu rà phá bom mìn vật nổ dự án: "+ dr["cecm_program_idST"].ToString() +" Đơn vị đã chấp hành nghiêm Quy chuẩn, Quy trình kỹ thuật.....và phương án KTTC và kế hoạch đã được phê duyệt................";

                //            indexRow += 5;

                //            if (dr["construct_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[indexRow, 14] = dr["construct_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[indexRow, 14] = dr["construct_id_other"].ToString();
                //            }
                //            xlWorkbooK.Save();
                //            xlWorkbooK.Close(false);
                //            xlApp.IgnoreRemoteRequests = false;
                //        }
                //        catch (System.Exception ex)
                //        {
                //        }
                //        finally
                //        {
                //            xlApp.Quit();

                //            if (xlWorkSheet != null)
                //                Marshal.ReleaseComObject(xlWorkSheet);
                //            if (xlWorkbooK != null)
                //                Marshal.ReleaseComObject(xlWorkbooK);
                //            if (xlApp != null)
                //                Marshal.ReleaseComObject(xlApp);
                //        }
                //        System.IO.FileInfo fi = new System.IO.FileInfo(pathFile);
                //        if (fi.Exists)
                //        {
                //            System.Diagnostics.Process.Start(pathFile);
                //        }
                //    }

                //}
                SqlCommandBuilder sqlCommand = null;


                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM17.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM17_" + Guid.NewGuid().ToString();
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
                            "Select tbl.*, " +
                            "Tinh.Ten as province_idST, " +
                            "case when command.nameId is null then tbl.command_id_other else command.nameId end as Tencommand, " +
                            "case when construct.nameId is null then tbl.construct_id_other else construct.nameId end as Tenconstruct " +
                            "from RPBM17 tbl " +
                            "left join cecm_provinces Tinh on tbl.province_id = Tinh.id " +
                            "left join Cecm_ProgramStaff command on tbl.command_id = command.id " +
                            "left join Cecm_ProgramStaff construct on tbl.construct_id = construct.id " +
                            "where gid = " + id_kqks,
                            cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                        //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                        sqlAdapterProvince.Fill(datatable);
                        marker.AddVariable("obj", datatable);
                        if (datatable.Rows.Count != 0)
                        {
                            DataRow dr = datatable.Rows[0];
                            DateTime now = DateTime.TryParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime datesST) ? datesST : DateTime.Now;
                            marker.AddVariable("Ngaybaocao", dr["province_idST"].ToString().Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                            //marker.AddVariable("Cancu", " - Căn cứ Quy trình kỹ thuật " + dr["base_on_tech"].ToString() + ", Quy chuẩn kỹ thuật Quốc gia " + dr["technical_regulations"].ToString());
                            //marker.AddVariable("HDKT", " - Hợp đồng kinh tế số " + dr["num_economic_contracts"].ToString() + "/HĐKT-RPBM ngày " + dr["date_economic_contractsST"].ToString() + " được ký kết giữa " + dr["organization_signed"].ToString());
                            //marker.AddVariable("time_signedST", "Hôm nay, ngày " + dr["time_signedST"].ToString() + "tại hiện trường mặt bằng dự án: " + dr["cecm_program_idST"].ToString());
                            //marker.AddVariable("area_ground", "2. Diện tích mặt bằng của dự án: " + dr["area_ground"].ToString() + "(chủ đầu tư cung cấp hồ sơ, bản vẽ của dự án). ");
                            //marker.AddVariable("KL1", string.Format("1. Các bên nhất trí bàn giao mặt bằng tại thực địa làm cơ sở cho đơn vị: {0} triển khai thi công khảo sát thu thập số liệu phục vụ cho lập phương án kỹ thuật thi công và dự toán rà phá bom mìn vật nổ dự án", dr["deptid_handoverST"].ToString()));
                            //marker.AddVariable("amount", string.Format("Biên bản được lập thành {0} bản lưu giữ tại hồ sơ hoàn công công trình.", dr["amount"].ToString()));
                            //marker.AddVariable("KL2", "2. " + dr["conclusion"].ToString());

                            marker.AddVariable("organ_receive", "Kính gửi: " + dr["organ_receive"].ToString());
                            marker.AddVariable("HDKT", string.Format(" - Hợp đồng kinh tế số {0}/HĐKT-RPBM {1} được ký kết giữa {2} về việc thi công gói thầu rà phá bom mìn vật nổ dự án: {3} địa điểm {4}",
                                dr["num_economic_contracts"].ToString(),
                                dr["date_economic_contractsST"].ToString(),
                                dr["organization_signed"].ToString(),
                                dr["cecm_program_idST"].ToString(),
                                dr["address"].ToString()));
                            marker.AddVariable("RPAll", string.Format("Rà phá tất cả các loại bom mìn vật nổ trên mặt bằng dự án: {0} địa điểm {1} để đảm bảo an toàn cho thi công xây dựng các hạng mục công trình tiếp theo",
                                dr["cecm_program_idST"].ToString(),
                                dr["address"].ToString()));
                            string[] TinhHuyenXa = dr["address"].ToString().Split(',');
                            string Tinh = TinhHuyenXa[TinhHuyenXa.Length - 1];
                            marker.AddVariable("HiepDong", string.Format(" - Hiệp đồng chặt chẽ với Bộ CHQS tỉnh {0}; chủ đầu tư, địa phương trên địa bàn để thi công đảm bảo chất lượng, hiệu quả.",
                                Tinh));
                            marker.AddVariable("DieuDong", string.Format(" - Điều động lực lượng thi công rà phá bom mìn vật nổ do đồng chí {0} chỉ huy chung.",
                                dr["Tencommand"].ToString()));
                            marker.AddVariable("LucLuongTC", string.Format(" - Lực lượng thi công gồm: {0} Đội RPBM, quân số theo biên chế (Có danh sách và quyết định điều động lực lượng kèm theo).",
                                dr["num_team"].ToString()));
                            marker.AddVariable("TrangBi", string.Format(" - Trang bị mỗi đội: {0} xe ô tô, {1} máy dò mìn, {2} máy dò bom và thiết bị cầm tay đồng bộ, đầy đủ theo biên chế quy định…",
                                dr["number_driver"].ToString(),
                                dr["number_machine_min"].ToString(),
                                dr["number_machine_bomb"].ToString()));
                            marker.AddVariable("DTTC", string.Format(" - Diện tích thi công RPBM cho dự án là: {0} ha (đã được thể hiện tại bản vẽ thiết kế mặt bằng, tọa độ...)",
                                dr["area_rapha"].ToString()));
                            marker.AddVariable("DoSau", string.Format(" - Độ sâu RPBM: đến {0} m, tính từ mặt đất tự nhiên hiện tại trở xuống.",
                                dr["deep_rapha"].ToString()));
                            marker.AddVariable("ThucHienHopDong", string.Format("Thực hiện Hợp đồng thi công gói thầu rà phá bom mìn vật nổ dự án: {0} Đơn vị đã chấp hành nghiêm Quy chuẩn, Quy trình kỹ thuật.....và phương án KTTC và kế hoạch đã được phê duyệt................",
                                dr["cecm_program_idST"].ToString()));
                            //marker.AddVariable("construct_idST", string.IsNullOrEmpty(dr["construct_idST"].ToString()) ? dr["construct_id_other"].ToString() : dr["construct_idST"].ToString());

                            //System.Data.DataTable datatableKQTC = new System.Data.DataTable();
                            //SqlCommandBuilder sqlCommandKQTC = null;
                            //SqlDataAdapter sqlAdapterKQTC = null;
                            //sqlAdapterKQTC = new SqlDataAdapter(string.Format("select * from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}') or (main_id = {3} and field_name = N'{4}' and table_name = N'{5}')", dem, dem1, table_name, dem0, dem1, table_name), cn);
                            //sqlCommandKQTC = new SqlCommandBuilder(sqlAdapterKQTC);
                            //sqlAdapterKQTC.Fill(datatableKQTC);
                            System.Data.DataTable datatableSub1 = new System.Data.DataTable();
                            SqlCommandBuilder sqlCommandSub1 = null;
                            SqlDataAdapter sqlAdapterSub1 = null;
                            sqlAdapterSub1 = new SqlDataAdapter(
                                "select " +
                            "ROW_NUMBER() OVER (ORDER BY tbl.gid) as 'STT', " +
                            "tbl.*, " +
                            "CASE WHEN ndcv.the_content is null then tbl.string1 else ndcv.the_content end as CongViec " +
                            "from groundDeliveryRecordsMember tbl " +
                            "left join NoiDungCongViec ndcv on tbl.long2 = ndcv.id " +
                            "where main_id = " + id_kqks + "" +
                            "and table_name = N'" + table_name + "' " +
                            "and field_name = N'" + field_name1 + "'", cn);
                            sqlCommandSub1 = new SqlCommandBuilder(sqlAdapterSub1);
                            sqlAdapterSub1.Fill(datatableSub1);
                            marker.AddVariable("KQTC", datatableSub1);
                        }

                        marker.ApplyMarkers(Syncfusion.XlsIO.UnknownVariableAction.Skip);

                        //Saving and closing the workbook
                        workbook.SaveAs(pathFile);
                        //workbook.ActiveSheetIndex = 0;
                        //if(workbook.Worksheets.Count > 1)
                        //{
                        //    workbook.Worksheets.Remove(workbook.Worksheets.Count - 1);
                        //}
                        

                        //Close the workbook
                        workbook.Close();
                        cfFileStream.Close();

                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook workbook1 = excel.Workbooks.Open(pathFile);
                        workbook1.Worksheets.get_Item(1).Activate();
                        workbook1.Save();
                        workbook1.Close();

                        //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                        //Workbook xlWorkbooK = xlApp.Workbooks.Open(pathFile);
                        ////get the first sheet of the excel
                        //Worksheet xlWorkSheet = (Worksheet)xlWorkbooK.Worksheets.get_Item(1);
                        FileInfo fi = new FileInfo(pathFile);
                        if (fi.Exists)
                        {
                            System.Diagnostics.Process.Start(pathFile);
                        }
                    }
                }
            }

            if (e.ColumnIndex == Export.Index)
            {
                string files = "", files_1 = "", files_2 = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM17 left join cecm_programData on cecm_programData.id = RPBM17.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM17 lst = new classRPBM17();
                    lst.gid = long.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.cecm_program_id = long.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.date_economic_contractsST = dr["date_economic_contractsST"].ToString();
                    lst.date_startST = dr["date_startST"].ToString();
                    lst.dates_endST = dr["dates_endST"].ToString();

                    lst.organ_receive = dr["organ_receive"].ToString();
                    lst.base_on_thongtu = dr["base_on_thongtu"].ToString();
                    lst.technical_regulations = dr["technical_regulations"].ToString();
                    lst.num_economic_contracts = dr["num_economic_contracts"].ToString();
                    lst.organization_signed = dr["organization_signed"].ToString();
                    lst.mission = dr["mission"].ToString();
                    lst.result = dr["result"].ToString();
                    lst.comment_tc = dr["comment_tc"].ToString();
                    lst.conclusion = dr["conclusion"].ToString();

                    lst.deep_rpbm = double.Parse(dr["deep_rpbm"].ToString());
                    lst.num_team = double.Parse(dr["num_team"].ToString());
                    lst.number_driver = double.Parse(dr["number_driver"].ToString());
                    lst.number_machine_min = double.Parse(dr["number_machine_min"].ToString());
                    lst.number_machine_bomb = double.Parse(dr["number_machine_bomb"].ToString());
                    lst.area_rapha = double.Parse(dr["area_rapha"].ToString());
                    lst.deep_rapha = double.Parse(dr["deep_rapha"].ToString());

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
                    lst.command_id = int.Parse(dr["command_id"].ToString());
                    try
                    {
                        lst.command_idST = dr["command_idST"].ToString();
                    }
                    catch
                    {
                        lst.command_idST = "";
                    }
                    lst.command_id_other = dr["command_id_other"].ToString();
                    lst.deptid_hrpbm = int.Parse(dr["deptid_hrpbm"].ToString());
                    try
                    {
                        lst.deptid_hrpbmST = dr["deptid_hrpbmST"].ToString();
                    }
                    catch
                    {
                        lst.deptid_hrpbmST = "";
                    }
                    lst.files = dr["files"].ToString();
                    List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();

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
                            A.long1 = long.TryParse(dr1["long1"].ToString(), out long long1) ? long1 : -1;
                            A.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            A.string3 = dr1["string3"].ToString();
                            A.double1 = double.TryParse(dr1["double1"].ToString(), out double double1) ? double1 : 0;
                            A.double2 = double.TryParse(dr1["double2"].ToString(), out double double2) ? double2 : 0;
                            Lst1.Add(A);
                        }
                    }
                    lst.reportResConsRPBM_Work_lstSubTable = Lst1;

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM17");
                    if (string.IsNullOrEmpty(saveLocation) == false)
                    {
                        System.IO.File.WriteAllText(saveLocation, json);
                        MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xuất file dữ liệu thất bại... ", "Thất bại");
                    }

                    //json = JsonConvert.SerializeObject(lst, Formatting.Indented);
                    //List<string> lstFiles = new List<string>();
                    //lstFiles.Add(files);
                    //lstFiles.Add(files_1);
                    //lstFiles.Add(files_2);
                    //CompressedFile frm = new CompressedFile(json, lstFiles);
                    //frm.ShowDialog();

                    //CompressedFile frm = new CompressedFile(LstDaily, Phieudieutra, Baocao, Bando);
                    //frm.ShowDialog();
                }
            }
            if (e.ColumnIndex == DoiRPBMSua.Index)
            {
                UtilsDatabase.DeleteMemberByTablename(table_name);
                FrmThemmoiRPBM17 frm = new FrmThemmoiRPBM17(id_kqks);
                frm.Text = "CHỈNH SỬA BÁO CÁO KẾT QUẢ THI CÔNG GÓI THẦU RÀ PHÁ BOM MÌN VẬT NỔ";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == DoiRPBMXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM17] WHERE gid = " + id_kqks, cn);
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
            FrmThemmoiRPBM17 frm = new FrmThemmoiRPBM17(0);
            frm.Text = "THÊM MỚI BÁO CÁO KẾT QUẢ THI CÔNG GÓI THẦU RÀ PHÁ BOM MÌN VẬT NỔ";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}


