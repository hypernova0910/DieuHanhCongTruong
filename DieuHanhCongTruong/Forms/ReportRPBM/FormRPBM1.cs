using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Syncfusion.XlsIO;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;

namespace DieuHanhCongTruong
{
    public partial class FormRPBM1 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "GROUNDDELIVERYRECORDS";
        public string fieldName2 = "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem";
        public string fieldName1 = "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember";
        public FormRPBM1()
        {
            cn = frmLoggin.sqlCon;
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from dbo.groundDeliveryRecords where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "' order by gid"), cn);
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
                ////MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return;
            }
        }

        private void FormRPBM1_Load(object sender, EventArgs e)
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

        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtTImkiem.Text = "";
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
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
            if (e.ColumnIndex == cotXuatExcel.Index)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                
                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM1.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM1_" + Guid.NewGuid().ToString();
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
                        SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM groundDeliveryRecords left join cecm_programData on cecm_programData.id = groundDeliveryRecords.cecm_program_id where gid = " + id_kqks, cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                        //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                        sqlAdapterProvince.Fill(datatable);
                        marker.AddVariable("obj", datatable);
                        if (datatable.Rows.Count != 0)
                        {
                            DataRow dr = datatable.Rows[0];
                            DateTime now = DateTime.Now;
                            marker.AddVariable("Ngaybaocao", dr["address"].ToString().Split(',')[2].Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                            marker.AddVariable("Cancu", " - Căn cứ Quy trình kỹ thuật " + dr["base_on_tech"].ToString() + ", Quy chuẩn kỹ thuật Quốc gia " + dr["technical_regulations"].ToString());
                            marker.AddVariable("HDKT", " - Hợp đồng kinh tế số " + dr["num_economic_contracts"].ToString() + "/HĐKT-RPBM ngày " + dr["date_economic_contractsST"].ToString() + " được ký kết giữa " + dr["organization_signed"].ToString());
                            marker.AddVariable("time_signedST", "Hôm nay, ngày " + dr["time_signedST"].ToString() + "tại hiện trường mặt bằng dự án: " + dr["cecm_program_idST"].ToString());
                            marker.AddVariable("area_ground", "2. Diện tích mặt bằng của dự án: " + dr["area_ground"].ToString() + "(chủ đầu tư cung cấp hồ sơ, bản vẽ của dự án). ");
                            marker.AddVariable("KL1", string.Format("1. Các bên nhất trí bàn giao mặt bằng tại thực địa làm cơ sở cho đơn vị: {0} triển khai thi công khảo sát thu thập số liệu phục vụ cho lập phương án kỹ thuật thi công và dự toán rà phá bom mìn vật nổ dự án", dr["deptid_handoverST"].ToString()));
                            marker.AddVariable("amount", string.Format("Biên bản được lập thành {0} bản lưu giữ tại hồ sơ hoàn công công trình.", dr["amount"].ToString()));
                            marker.AddVariable("KL2", "2. " + dr["conclusion"].ToString());
                        }

                        System.Data.DataTable datatableSub1 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub1 = null;
                        SqlDataAdapter sqlAdapterSub1 = null;
                        sqlAdapterSub1 = new SqlDataAdapter(string.Format("select long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}') or (main_id = {3} and field_name = N'{4}' and table_name = N'{5}')", id_kqks, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem", "GROUNDDELIVERYRECORDS", id_kqks, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem", "GROUNDDELIVERYRECORDS"), cn);
                        sqlCommandSub1 = new SqlCommandBuilder(sqlAdapterSub1);
                        sqlAdapterSub1.Fill(datatableSub1);
                        marker.AddVariable("datatableSub1", datatableSub1);
                        bool coNguoiKy1 = false;
                        foreach (DataRow drDVKS in datatableSub1.Rows)
                        {
                            if (drDVKS["long5"].ToString() == "1")
                            {
                                marker.AddVariable("daidienDVKS", drDVKS["string1"].ToString());
                                coNguoiKy1 = true;
                                break;
                            }
                        }
                        if (!coNguoiKy1)
                        {
                            marker.AddVariable("daidienDVKS", "");
                        }

                        System.Data.DataTable datatableSub2 = new System.Data.DataTable();
                        SqlCommandBuilder sqlCommandSub2 = null;
                        SqlDataAdapter sqlAdapterSub2 = null;
                        sqlAdapterSub2 = new SqlDataAdapter(string.Format("select long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}') or (main_id = {3} and field_name = N'{4}' and table_name = N'{5}')", id_kqks, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember", "GROUNDDELIVERYRECORDS", id_kqks, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember", "GROUNDDELIVERYRECORDS"), cn);
                        sqlCommandSub2 = new SqlCommandBuilder(sqlAdapterSub2);
                        sqlAdapterSub2.Fill(datatableSub2);
                        marker.AddVariable("datatableSub2", datatableSub2);
                        bool coNguoiKy2 = false;
                        foreach(DataRow drCDT in datatableSub2.Rows)
                        {
                            if(drCDT["long5"].ToString() == "1")
                            {
                                marker.AddVariable("daidienCDT", drCDT["string1"].ToString());
                                coNguoiKy2 = true;
                                break;
                            }
                        }
                        if (!coNguoiKy2)
                        {
                            marker.AddVariable("daidienCDT", "");
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
                //foreach (DataRow dr in datatableProvince.Rows)
                //{
                //    string pathFile = AppUtils.SaveExcel("BB_RPBM1");

                //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                //    //if (pathFile != null)
                //    //{
                //    //    //open the excel
                //    //    Workbook xlWorkbooK = xlApp.Workbooks.Open(pathFile);
                //    //    //get the first sheet of the excel
                //    //    Worksheet xlWorkSheet = (Worksheet)xlWorkbooK.Worksheets.get_Item(1);
                //    //    try
                //    //    {
                //    //        Range range = xlWorkSheet.UsedRange;
                //    //        int rowCount = range.Rows.Count;
                //    //        int columnCount = range.Columns.Count;

                //    //        xlWorkSheet.Cells[5, 2] = dr["address"].ToString() + ", " + dr["datesST"].ToString();
                //    //        xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        xlWorkSheet.Cells[9, 2] = "Địa điểm: " + dr["address_cecm"].ToString();
                //    //        xlWorkSheet.Cells[11, 2] = "- Căn cứ Quy trình kỹ thuật " + dr["base_on_tech"].ToString() + " , Quy chuẩn kỹ thuật Quốc gia " + dr["technical_regulations"].ToString();
                //    //        xlWorkSheet.Cells[15, 2] = "- Hợp đồng kinh tế số: " + dr["num_economic_contracts"].ToString() + "/HĐKT-RPBM ngày " + dr["date_economic_contractsST"].ToString() + " được ký kết giữa " + dr["organization_signed"].ToString();
                //    //        xlWorkSheet.Cells[17, 2] = "Hôm nay: " + dr["time_signedST"].ToString() + "} tại hiện trường mặt bằng dự án: " + dr["cecm_program_idST"].ToString();
                //    //        //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //    //        int indexRow = 23;
                //    //        int A1 = indexRow;
                //    //        List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                //    //        SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "'", cn);
                //    //        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince1);
                //    //        System.Data.DataTable datatableProvince1 = new System.Data.DataTable();
                //    //        sqlAdapterProvince1.Fill(datatableProvince1);
                //    //        foreach (DataRow dr1 in datatableProvince1.Rows)
                //    //        {
                //    //            if (dr1["field_name"].ToString().Contains(fieldName1))
                //    //            {
                //    //                xlWorkSheet.Cells[indexRow, 2] = dr1["gid"].ToString();
                //    //                if (dr1["string1"].ToString() != "")
                //    //                {
                //    //                    xlWorkSheet.Cells[indexRow, 5] = dr1["string1"].ToString();
                //    //                    xlWorkSheet.Cells[indexRow, 14] = dr1["string2"].ToString();
                //    //                }
                //    //                else
                //    //                {
                //    //                    xlWorkSheet.Cells[indexRow, 5] = dr1["string3"].ToString();
                //    //                    xlWorkSheet.Cells[indexRow, 14] = dr1["string4"].ToString();
                //    //                }

                //    //                indexRow++;
                //    //            }

                //    //        }
                //    //        if (A1 == indexRow)
                //    //        {
                //    //            indexRow++;
                //    //        }
                //    //        indexRow = indexRow + 4;
                //    //        int A2 = indexRow;
                //    //        foreach (DataRow dr2 in datatableProvince1.Rows)
                //    //        {
                //    //            if (dr2["field_name"].ToString().Contains(fieldName2))
                //    //            {
                //    //                xlWorkSheet.Cells[indexRow, 2] = dr2["gid"].ToString();
                //    //                if (dr2["string1"].ToString() != "")
                //    //                {
                //    //                    xlWorkSheet.Cells[indexRow, 5] = dr2["string1"].ToString();
                //    //                    xlWorkSheet.Cells[indexRow, 14] = dr2["string2"].ToString();
                //    //                }
                //    //                else
                //    //                {
                //    //                    xlWorkSheet.Cells[indexRow, 5] = dr2["string3"].ToString();
                //    //                    xlWorkSheet.Cells[indexRow, 14] = dr2["string4"].ToString();
                //    //                }
                //    //                indexRow++;

                //    //            }

                //    //        }
                //    //        if (A2 == indexRow)
                //    //        {
                //    //            indexRow++;
                //    //        }
                //    //        indexRow += 3;
                //    //        xlWorkSheet.Cells[indexRow, 2] = "2. Diện tích mặt bằng của dự án: " + dr["area_ground"].ToString() + " (chủ đầu tư cung cấp hồ sơ, bản vẽ của dự án).";
                //    //        indexRow++;
                //    //        xlWorkSheet.Cells[indexRow, 2] = "3. Diện tích thi công khảo sát phục vụ cho lập phương án KTTC và dự toán rà phá bom mìn vật nổ: " + dr["area_construction"].ToString() + " (không ít hơn 1% diện tích cần RPBM).";
                //    //        indexRow++;
                //    //        xlWorkSheet.Cells[indexRow, 2] = "3. Diện tích thi công khảo sát phục vụ cho lập phương án KTTC và dự toán rà phá bom mìn vật nổ: " + dr["area_construction"].ToString() + " (không ít hơn 1% diện tích cần RPBM).";
                //    //        indexRow++;
                //    //        xlWorkSheet.Cells[indexRow, 2] = "5. Các yêu cầu khác của chủ đầu tư: " + dr["request_other"].ToString();
                //    //        indexRow += 4;
                //    //        xlWorkSheet.Cells[indexRow, 2] = "1. Các bên nhất trí bàn giao mặt bằng tại thực địa làm cơ sở cho đơn vị: " + dr["deptid_handoverST"].ToString() + " triển khai thi công khảo sát thu thập số liệu";
                //    //        indexRow++;
                //    //        xlWorkSheet.Cells[indexRow, 2] = "2. " + dr["conclusion"].ToString();
                //    //        indexRow += 3;
                //    //        xlWorkSheet.Cells[indexRow, 2] = "Biên bản được lập thành " + dr["conclusion"].ToString() + " bản lưu giữ tại hồ sơ hoàn công công trình.";
                //    //        indexRow += 4;
                //    //        if (dr["boss_idST"].ToString() != null)
                //    //        {
                //    //            xlWorkSheet.Cells[indexRow, 4] = dr["boss_idST"].ToString();
                //    //        }
                //    //        else
                //    //        {
                //    //            xlWorkSheet.Cells[indexRow, 4] = dr["boss_id_other"].ToString();
                //    //        }
                //    //        if (dr["survey_idST"].ToString() != null)
                //    //        {
                //    //            xlWorkSheet.Cells[indexRow, 14] = dr["survey_idST"].ToString();
                //    //        }
                //    //        else
                //    //        {
                //    //            xlWorkSheet.Cells[indexRow, 14] = dr["survey_id_other"].ToString();
                //    //        }


                //    //        xlWorkbooK.Save();
                //    //        xlWorkbooK.Close(false);
                //    //        xlApp.IgnoreRemoteRequests = false;
                //    //    }
                //    //    catch
                //    //    {
                //    //    }
                //    //    finally
                //    //    {
                //    //        xlApp.Quit();

                //    //        if (xlWorkSheet != null)
                //    //            Marshal.ReleaseComObject(xlWorkSheet);
                //    //        if (xlWorkbooK != null)
                //    //            Marshal.ReleaseComObject(xlWorkbooK);
                //    //        if (xlApp != null)
                //    //            Marshal.ReleaseComObject(xlApp);
                //    //    }
                //    //    System.IO.FileInfo fi = new System.IO.FileInfo(pathFile);
                //    //    if (fi.Exists)
                //    //    {
                //    //        System.Diagnostics.Process.Start(pathFile);
                //    //    }
                //    //}

                    
                //}
            }
            if (e.ColumnIndex == Export.Index)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM groundDeliveryRecords left join cecm_programData on cecm_programData.id = groundDeliveryRecords.cecm_program_id where gid = " + id_kqks, cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    groundDeliveryRecordsDTO lst = new groundDeliveryRecordsDTO();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.base_on_tech = dr["base_on_tech"].ToString();
                    lst.technical_regulations = dr["technical_regulations"].ToString();
                    lst.technical_regulationsST = dr["technical_regulationsST"].ToString();
                    lst.num_economic_contracts = dr["num_economic_contracts"].ToString();
                    lst.date_economic_contractsST = dr["date_economic_contractsST"].ToString();
                    lst.organization_signed = dr["organization_signed"].ToString();
                    lst.time_signedST = dr["time_signedST"].ToString();
                    lst.detail_giaonhan = dr["detail_giaonhan"].ToString();
                    lst.area_ground = float.Parse(dr["area_ground"].ToString());
                    lst.area_construction = float.Parse(dr["area_construction"].ToString());
                    lst.deep = float.Parse(dr["deep"].ToString());
                    lst.request_other = dr["request_other"].ToString();
                    lst.deptid_handover = int.Parse(dr["deptid_handover"].ToString());
                    lst.deptid_handoverST = dr["deptid_handoverST"].ToString();
                    lst.conclusion = dr["conclusion"].ToString();
                    lst.amount = float.Parse(dr["amount"].ToString());
                    lst.boss_idST = dr["boss_idST"].ToString();
                    lst.survey_idST = dr["survey_idST"].ToString();
                    lst.boss_id = int.Parse(dr["boss_id"].ToString());
                    lst.survey_id = int.Parse(dr["survey_id"].ToString());
                    lst.boss_id_other = dr["boss_id_other"].ToString();
                    lst.survey_id_other = dr["survey_id_other"].ToString();
                    lst.files = dr["files"].ToString();

                    List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst2 = new List<groundDeliveryRecords_Member>();
                    SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + " and table_name = N'"+ table_name +"'", cn);
                    sqlCommand = new SqlCommandBuilder(sqlAdapterProvince1);
                    System.Data.DataTable datatableProvince1 = new System.Data.DataTable();
                    sqlAdapterProvince1.Fill(datatableProvince1);
                    foreach (DataRow dr1 in datatableProvince1.Rows)
                    {
                        if (dr1["field_name"].ToString().Contains(fieldName1))
                        {
                            groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                            A.gid = int.Parse(dr1["gid"].ToString());
                            A.table_name = dr1["table_name"].ToString();
                            A.field_name = dr1["field_name"].ToString();
                            A.main_id = int.Parse(dr1["main_id"].ToString());
                            A.long1 = long.TryParse(dr1["long1"].ToString(), out long long1) ? long1 : -1;
                            A.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;

                            A.long1ST = dr1["long1ST"].ToString();
                            A.long2ST = dr1["long2ST"].ToString();
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            A.long5 = dr1["long5"].ToString();
                            Lst1.Add(A);
                        }
                        if (dr1["field_name"].ToString().Contains(fieldName2))
                        {
                            groundDeliveryRecords_Member B = new groundDeliveryRecords_Member();
                            B.gid = int.Parse(dr1["gid"].ToString());
                            B.table_name = dr1["table_name"].ToString();
                            B.field_name = dr1["field_name"].ToString();
                            B.main_id = int.Parse(dr1["main_id"].ToString());
                            B.long1 = long.TryParse(dr1["long1"].ToString(), out long long1) ? long1 : -1;
                            B.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;

                            B.long1ST = dr1["long1ST"].ToString();
                            B.long2ST = dr1["long2ST"].ToString();
                            B.string1 = dr1["string1"].ToString();
                            B.string2 = dr1["string2"].ToString();
                            B.long5 = dr1["long5"].ToString();
                            Lst2.Add(B);
                        }
                    }
                    //string json = JsonSerializer.Serialize(Lst);
                    //File.WriteAllText(@"E:\LstResult.json", json);
                    lst.groundDeliveryRecords_CDTMember_lstSubTable = Lst1;
                    lst.groundDeliveryRecords_SurveyMem_lstSubTable = Lst2;

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM1");
                    if (string.IsNullOrEmpty(saveLocation) == false)
                    {
                        System.IO.File.WriteAllText(saveLocation, json);
                        MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xuất file dữ liệu thất bại... ", "Thất bại");
                    }
                    //string JSONresult = JsonConvert.SerializeObject(Lst);
                    //string path = @"E:\LstResult.json";
                    //using (var tw = new StreamWriter(path, true))
                    //{
                    //    tw.WriteLine(JSONresult.ToString());
                    //    tw.Close();
                    //}

                    //CompressedFile frm = new CompressedFile(LstDaily, Phieudieutra, Baocao, Bando);
                    //frm.ShowDialog();
                }
            }
            if (e.ColumnIndex == DoiRPBMSua.Index)
            {
                UtilsDatabase.DeleteMemberByTablename(table_name);
                FrmThemmoiRPBM1 frm = new FrmThemmoiRPBM1(id_kqks);
                frm.Text = "CHỈNH SỬA BIÊN BẢN BÀN GIAO MẶT BẰNG";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == DoiRPBMXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM groundDeliveryRecords WHERE gid = " + id_kqks, cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                    UtilsDatabase.DeleteMemberByMainId(table_name,id_kqks);
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
            FrmThemmoiRPBM1 frm = new FrmThemmoiRPBM1(0);
            frm.Text = "THÊM MỚI BIÊN BẢN BÀN GIAO MẶT BẰNG";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}

