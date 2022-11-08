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
    public partial class FormRPBM16 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "ReportConfDestroy";
        public string field_name1 = "ReportConfDestroy_ReportConfDestroy_CDTMember";
        public string field_name2 = "ReportConfDestroy_ReportConfDestroy_SurMember";
        public string field_name3 = "ReportConfDestroy_ReportConfDestroy_LocalMember";
        public string field_name4 = "ReportConfDestroy_ReportConfDestroy_ConsMember";
        public string field_name5 = "ReportConfDestroy_ReportConfDestroy_Bomb";

        public FormRPBM16()
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM16] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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

        private void FormRPBM16_Load(object sender, EventArgs e)
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
            if (e.ColumnIndex == 4)
            {
                //SqlCommandBuilder sqlCommand = null;

                //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM16 left join cecm_programData on cecm_programData.id = RPBM16.cecm_program_id where gid = " + id_kqks, cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                //sqlAdapterProvince.Fill(datatableProvince);
                //foreach (DataRow dr in datatableProvince.Rows)
                //{
                //    string pathFile = AppUtils.SaveExcel("BB_RPBM16");

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

                //            xlWorkSheet.Cells[5, 2] = dr["address"].ToString() + ", " + dr["datesST"].ToString();
                //            xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            xlWorkSheet.Cells[9, 2] = "Hạng mục: " + dr["categoryST"].ToString();
                //            xlWorkSheet.Cells[10, 2] = "Địa điểm: " + dr["address_cecm"].ToString();
                //            //xlWorkSheet.Cells[11, 2] = "Hôm nay: " + dr["date_nowST"].ToString() + " , tại công trường thi công RPBM dự án: " + dr["cecm_program_idST"].ToString();
                //            xlWorkSheet.Cells[11, 2] = "Căn cứ phương án kỹ thuật thi công đã được các cấp có thẩm quyền phê duyệt, Quy tắc an toàn về hủy BMVN theo quy định tại Quy chuẩn " + dr["technical_regulations"].ToString() + " ,Tiêu chuẩn " + dr["base_on_tech"].ToString()+ " ,điều tra, khảo sát, rà phá bom mìn vật nổ ………….";
                //            xlWorkSheet.Cells[14, 2] = dr["base_on"].ToString();
                //            xlWorkSheet.Cells[15, 2] = "Hôm nay, "+ dr["dates_nowST"].ToString() +", tại bãi hủy thuộc "+ dr["ground_destroy"].ToString();

                //            int indexRow = 20, stt = 1;
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
                //                    if (dr1["string1"].ToString() != "")
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr1["string1"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr1["string2"].ToString();
                //                    }
                //                    else
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr1["string3"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr1["string4"].ToString();
                //                    }
                //                    indexRow++;
                //                }
                //            }
                //            if (A1 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            indexRow = indexRow + 4;
                //            int A2 = indexRow;
                //            foreach (DataRow dr2 in datatableProvince1.Rows)
                //            {
                //                if (dr2["field_name"].ToString().Contains(field_name2))
                //                {
                //                    xlWorkSheet.Cells[indexRow, 2] = dr2["gid"].ToString();
                //                    if (dr2["string1"].ToString() != "")
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr2["string1"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr2["string2"].ToString();
                //                    }
                //                    else
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr2["string3"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr2["string4"].ToString();
                //                    }
                //                    indexRow++;
                //                }
                //            }
                //            if (A2 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            indexRow = indexRow + 5;
                //            int A3 = indexRow;
                //            foreach (DataRow dr3 in datatableProvince1.Rows)
                //            {
                //                if (dr3["field_name"].ToString().Contains(field_name3))
                //                {
                //                    xlWorkSheet.Cells[indexRow, 2] = dr3["gid"].ToString();
                //                    if (dr3["string1"].ToString() != "")
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr3["string1"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr3["string2"].ToString();
                //                    }
                //                    else
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr3["string3"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr3["string4"].ToString();
                //                    }
                //                    indexRow++;
                //                }
                //            }
                //            if (A3 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            indexRow = indexRow + 4;
                //            int A4 = indexRow;
                //            foreach (DataRow dr4 in datatableProvince1.Rows)
                //            {
                //                if (dr4["field_name"].ToString().Contains(field_name4))
                //                {
                //                    xlWorkSheet.Cells[indexRow, 2] = dr4["gid"].ToString();
                //                    if (dr4["string1"].ToString() != "")
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr4["string1"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr4["string2"].ToString();
                //                    }
                //                    else
                //                    {
                //                        xlWorkSheet.Cells[indexRow, 5] = dr4["string3"].ToString();
                //                        xlWorkSheet.Cells[indexRow, 14] = dr4["string4"].ToString();
                //                    }
                //                    indexRow++;
                //                }
                //            }
                //            if (A4 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            indexRow += 2;
                //            xlWorkSheet.Cells[indexRow, 2] = "Căn cứ Biên bản xác nhận số lượng bom mìn vật nổ thu hồi dự án " + dr["cecm_program_idST"].ToString();

                //            indexRow = indexRow + 5;
                //            int A5 = indexRow;
                //            foreach (DataRow dr3 in datatableProvince1.Rows)
                //            {
                //                if (dr3["field_name"].ToString().Contains(field_name5))
                //                {
                //                    xlWorkSheet.Cells[indexRow, 2] = dr3["gid"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 5] = dr3["string1"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 13] = dr3["string2"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 15] = dr3["string3"].ToString();
                //                    xlWorkSheet.Cells[indexRow, 17] = dr3["string4"].ToString();
                //                    indexRow++;
                //                }
                //            }
                //            if (A5 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            xlWorkSheet.Cells[indexRow, 2] = "2. Thời gian hủy nổ: Bắt đầu: " + dr["date_destroy_fromST"].ToString();
                //            indexRow = indexRow + 1;
                //            xlWorkSheet.Cells[indexRow, 2] = "Hoàn thành: " + dr["date_destroy_toST"].ToString();
                //            indexRow = indexRow + 2;
                //            xlWorkSheet.Cells[indexRow, 2] = dr["method_handl"].ToString();
                //            indexRow = indexRow + 2;
                //            xlWorkSheet.Cells[indexRow, 2] = dr["address_handl"].ToString();
                //            indexRow = indexRow + 2;
                //            xlWorkSheet.Cells[indexRow, 2] = "Các vật nổ nói trên đã được phá hủy, " + dr["result_handl"].ToString();

                //            indexRow += 7;
                //            if (dr["survey_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[indexRow, 4] = dr["survey_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[indexRow, 4] = dr["survey_id_other"].ToString();
                //            }
                //            if (dr["local_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[indexRow, 14] = dr["local_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[indexRow, 14] = dr["local_id_other"].ToString();
                //            }
                //            indexRow += 6;
                //            if (dr["boss_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[indexRow, 4] = dr["boss_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[indexRow, 4] = dr["boss_id_other"].ToString();
                //            }
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
                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM16.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM16_" + Guid.NewGuid().ToString();
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

                        try
                        {
                            //The first worksheet in the workbook is accessed.
                            IWorksheet worksheet = workbook.Worksheets[0];

                            //Create Template Marker processor.
                            //Apply the marker to export data from datatable to worksheet.
                            ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();
                            //marker.AddVariable("SalesList", table);

                            System.Data.DataTable datatable = new System.Data.DataTable();
                            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(
                                "SELECT RPBM16.* , " +
                                "Tinh.Ten as province_idST, " +
                                "cecm_programData.code as 'cecm_program_code'  " +
                                "FROM RPBM16 " +
                                "left join cecm_provinces Tinh on RPBM16.province_id = Tinh.id " +
                                "left join cecm_programData on cecm_programData.id = RPBM16.cecm_program_id " +
                                "where gid = " + id_kqks,
                                cn);
                            //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                            //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                            sqlAdapterProvince.Fill(datatable);
                            marker.AddVariable("obj", datatable);
                            if (datatable.Rows.Count != 0)
                            {
                                DataRow dr = datatable.Rows[0];
                                DateTime now = DateTime.TryParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime datesST) ? datesST : DateTime.Now;
                                marker.AddVariable("Ngaybaocao", dr["province_idST"].ToString().Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);

                                marker.AddVariable("Cancu", string.Format("Căn cứ phương án kỹ thuật thi công đã được các cấp có thẩm quyền phê duyệt, Quy tắc an toàn về hủy BMVN theo quy định tại Quy chuẩn {0}, Tiêu chuẩn {1}, điều tra, khảo sát, rà phá bom mìn vật nổ ……………",
                                    dr["technical_regulations"].ToString(),
                                    dr["base_on_tech"].ToString()));
                                marker.AddVariable("Cancu1", string.Format("Căn cứ Biên bản xác nhận số lượng bom mìn vật nổ thu hồi {0}. Dự án {1}",
                                    dr["datesST"].ToString(),
                                    dr["cecm_program_idST"].ToString()));
                                marker.AddVariable("Cancu2", string.Format("Các bên thống nhất lập biên bản xác nhận số lượng và chủng loại bom, đạn, vật nổ do đơn vị thi công RPBM {0} thu được trong quá trình thi công RPBM của dự án nói trên như sau:",
                                    dr["deptidST"].ToString()));
                                marker.AddVariable("amount", string.Format("'- Biên bản được các bên nhất trí thông qua và lập thành {0} bản có nội dung như nhau….../.",
                                    dr["amount"].ToString()));

                                //List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                                //SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (ORDER BY gid) as STT, * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name5 + "'", cn);
                                //SqlCommandBuilder sqlCommand = new SqlCommandBuilder(sqlAdapterProvince1);
                                System.Data.DataTable datatableBMVN = new System.Data.DataTable();
                                //sqlAdapterProvince1.Fill(datatableBMVN);
                                //marker.AddVariable("BMVN", datatableBMVN);
                                string sql4 =
                                    "select " +
                                    "ROW_NUMBER() OVER (ORDER BY tbl.gid) as 'STT', " +
                                    "tbl.string2, " +
                                    "tbl.string3, " +
                                    "tbl.string4, " +
                                    //"tbl.string5, " +
                                    "tbl.long2, " +
                                    "CASE WHEN mst.dvs_name is null or tbl.long1 = 14 then tbl.string1 else mst.dvs_name end as LoaiBMVN " +
                                    "from groundDeliveryRecordsMember tbl " +
                                    "left join mst_division mst on tbl.long1 = mst.dvs_value and mst.dvs_group_cd = '001' " +
                                    "where tbl.main_id = " + id_kqks + " " +
                                    "and tbl.table_name = N'" + table_name + "' " +
                                    "and field_name = '" + field_name5 + "' ";
                                SqlDataAdapter sqlAdapterProvince1 = sqlAdapterProvince1 = new SqlDataAdapter(sql4, cn);
                                sqlAdapterProvince1.Fill(datatableBMVN);
                                marker.AddVariable("BMVN", datatableBMVN);

                                SqlDataAdapter sqlAdapterCDT = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name1 + "'", cn);
                                //SqlCommandBuilder sqlCommandCDT = new SqlCommandBuilder(sqlAdapterProvince1);
                                System.Data.DataTable datatableCDT = new System.Data.DataTable();
                                sqlAdapterCDT.Fill(datatableCDT);
                                marker.AddVariable("CDT", datatableCDT);

                                bool coNguoiKyCDT = false;
                                foreach (DataRow drCDT in datatableCDT.Rows)
                                {
                                    if (drCDT["long5"].ToString() == "1")
                                    {
                                        marker.AddVariable("daidienCDT", drCDT["string1"].ToString());
                                        coNguoiKyCDT = true;
                                        break;
                                    }
                                }
                                if (!coNguoiKyCDT)
                                {
                                    marker.AddVariable("daidienCDT", "");
                                }

                                SqlDataAdapter sqlAdapterDVGS = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name2 + "'", cn);
                                //SqlCommandBuilder sqlCommandDVGS = new SqlCommandBuilder(sqlAdapterProvince1);
                                System.Data.DataTable datatableDVGS = new System.Data.DataTable();
                                sqlAdapterDVGS.Fill(datatableDVGS);
                                marker.AddVariable("DVGS", datatableDVGS);

                                bool coNguoiKyDVGS = false;
                                foreach (DataRow drDVGS in datatableDVGS.Rows)
                                {
                                    if (drDVGS["long5"].ToString() == "1")
                                    {
                                        marker.AddVariable("daidienDVGS", drDVGS["string1"].ToString());
                                        coNguoiKyDVGS = true;
                                        break;
                                    }
                                }
                                if (!coNguoiKyDVGS)
                                {
                                    marker.AddVariable("daidienDVGS", "");
                                }

                                SqlDataAdapter sqlAdapterDP = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name3 + "'", cn);
                                //SqlCommandBuilder sqlCommandDP = new SqlCommandBuilder(sqlAdapterProvince1);
                                System.Data.DataTable datatableDP = new System.Data.DataTable();
                                sqlAdapterDP.Fill(datatableDP);
                                marker.AddVariable("DP", datatableDP);

                                bool coNguoiKyDP = false;
                                foreach (DataRow drDP in datatableDP.Rows)
                                {
                                    if (drDP["long5"].ToString() == "1")
                                    {
                                        marker.AddVariable("daidienDP", drDP["string1"].ToString());
                                        coNguoiKyDP = true;
                                        break;
                                    }
                                }
                                if (!coNguoiKyDP)
                                {
                                    marker.AddVariable("daidienDP", "");
                                }

                                SqlDataAdapter sqlAdapterDVTC = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name4 + "'", cn);
                                //SqlCommandBuilder sqlCommandDVTC = new SqlCommandBuilder(sqlAdapterProvince1);
                                System.Data.DataTable datatableDVTC = new System.Data.DataTable();
                                sqlAdapterDVTC.Fill(datatableDVTC);
                                marker.AddVariable("DVTC", datatableDVTC);

                                bool coNguoiKyDVTC = false;
                                foreach (DataRow drDVTC in datatableDVTC.Rows)
                                {
                                    if (drDVTC["long5"].ToString() == "1")
                                    {
                                        marker.AddVariable("daidienDVTC", drDVTC["string1"].ToString());
                                        coNguoiKyDVTC = true;
                                        break;
                                    }
                                }
                                if (!coNguoiKyDVTC)
                                {
                                    marker.AddVariable("daidienDVTC", "");
                                }

                            }

                            marker.ApplyMarkers(Syncfusion.XlsIO.UnknownVariableAction.Skip);

                            //Saving and closing the workbook
                            workbook.SaveAs(pathFile);
                            //workbook.ActiveSheetIndex = 0;
                            //if(workbook.Worksheets.Count > 1)
                            //{
                            //    workbook.Worksheets.Remove(workbook.Worksheets.Count - 1);
                            //}
                        }catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }




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

            if (e.ColumnIndex == 5)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM16 left join cecm_programData on cecm_programData.id = RPBM16.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM16 lst = new classRPBM16();
                    lst.gid = long.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.cecm_program_id = long.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.dates_nowST = dr["dates_nowST"].ToString();
                    lst.date_destroy_fromST = dr["date_destroy_fromST"].ToString();
                    lst.date_destroy_toST = dr["date_destroy_toST"].ToString();

                    lst.base_on_tech = dr["base_on_tech"].ToString();
                    lst.technical_regulations = dr["technical_regulations"].ToString();
                    lst.base_on = dr["base_on"].ToString();
                    lst.ground_destroy = dr["ground_destroy"].ToString();
                    lst.method_handl = dr["method_handl"].ToString();
                    lst.address_handl = dr["address_handl"].ToString();
                    lst.result_handl = dr["result_handl"].ToString();

                    lst.amount = double.Parse(dr["amount"].ToString());

                    lst.boss_id = long.Parse(dr["boss_id"].ToString());
                    try
                    {
                        lst.boss_idST = dr["boss_idST"].ToString();
                    }
                    catch
                    {
                        lst.boss_idST = "";
                    }
                    lst.boss_id_other = dr["boss_id_other"].ToString();
                    lst.survey_id = long.Parse(dr["survey_id"].ToString());
                    try
                    {
                        lst.survey_idST = dr["survey_idST"].ToString();
                    }
                    catch
                    {
                        lst.survey_idST = "";
                    }
                    lst.survey_id_other = dr["survey_id_other"].ToString();
                    lst.local_id = long.Parse(dr["local_id"].ToString());
                    try
                    {
                        lst.local_idST = dr["local_idST"].ToString();
                    }
                    catch
                    {
                        lst.local_idST = "";
                    }
                    lst.local_id_other = dr["local_id_other"].ToString();

                    lst.construct_id = long.Parse(dr["construct_id"].ToString());
                    try
                    {
                        lst.construct_idST = dr["construct_idST"].ToString();
                    }
                    catch
                    {
                        lst.construct_idST = "";
                    }
                    lst.construct_id_other = dr["construct_id_other"].ToString();

                    lst.deptid = long.Parse(dr["deptid"].ToString());
                    try
                    {
                        lst.deptidST = dr["deptidST"].ToString();
                    }
                    catch
                    {
                        lst.deptidST = "";
                    }
                    lst.files = dr["files"].ToString();

                    List<groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst2 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst3 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst4 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst5 = new List<groundDeliveryRecords_Member>();

                    SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("select ROW_NUMBER() OVER (ORDER BY gid) as 'STT', * from groundDeliveryRecordsMember where main_id = " + id_kqks + " and table_name = N'" + table_name + "'", cn);
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
                            A.long1 = long.TryParse(dr1["long1"].ToString(), out long long1) ? long1 : -1;
                            A.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;
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
                            A.long1 = int.Parse(dr1["long1"].ToString());
                            A.long2 = int.Parse(dr1["long2"].ToString());
                            A.long1ST = dr1["long1ST"].ToString();
                            A.long2ST = dr1["long2ST"].ToString();
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            //A.long2 = dr1["long2"].ToString();
                            A.long5 = dr1["long5"].ToString();
                            Lst3.Add(A);
                        }
                        if (dr1["field_name"].ToString().Contains(field_name4))
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
                            Lst4.Add(A);
                        }
                        if (dr1["field_name"].ToString().Contains(field_name5))
                        {
                            groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                            A.gid = int.Parse(dr1["gid"].ToString());
                            A.table_name = dr1["table_name"].ToString();
                            A.field_name = dr1["field_name"].ToString();
                            A.main_id = int.Parse(dr1["main_id"].ToString());
                            A.long1 = long.Parse(dr1["long1"].ToString());
                            A.long2 = long.Parse(dr1["long2"].ToString());
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            A.string3 = dr1["string3"].ToString();
                            A.string4 = dr1["string4"].ToString();
                            A.string5 = dr1["string5"].ToString();
                            A.string6 = dr1["string6"].ToString();
                            Lst5.Add(A);
                        }
                    }
                    lst.reportConfDestroy_CDTMember_lstSubTable = Lst1;
                    lst.reportConfDestroy_SurMember_lstSubTable = Lst2;
                    lst.reportConfDestroy_LocalMember_lstSubTable = Lst3;
                    lst.reportConfDestroy_ConsMember_lstSubTable = Lst4;
                    lst.reportConfDestroy_Bomb_lstSubTable = Lst5;

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM16");
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
                FrmThemmoiRPBM16 frm = new FrmThemmoiRPBM16(id_kqks);
                frm.Text = "CHỈNH SỬA BIÊN BẢN XÁC NHẬN SỐ LƯỢNG BOM MÌN VẬT NỔ ĐÃ HỦY (XỬ LÝ)";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM16] WHERE gid = " + id_kqks, cn);
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
            FrmThemmoiRPBM16 frm = new FrmThemmoiRPBM16(0);
            frm.Text = "THÊM MỚI BIÊN BẢN XÁC NHẬN SỐ LƯỢNG BOM MÌN VẬT NỔ ĐÃ HỦY (XỬ LÝ)";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}


