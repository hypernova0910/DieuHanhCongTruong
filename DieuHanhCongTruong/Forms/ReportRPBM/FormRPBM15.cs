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
    public partial class FormRPBM15 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "PlanDestroyBomb";
        public string field_name1 = "PlanDestroyBomb_PlanDestroyBomb_Type";
        public string field_name2 = "PlanDestroyBomb_PlanDestroyBomb_Vehicle";

        public FormRPBM15()
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM15] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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

        private void FormRPBM15_Load(object sender, EventArgs e)
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

                //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM15 left join cecm_programData on cecm_programData.id = RPBM15.cecm_program_id where gid = " + id_kqks, cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                //sqlAdapterProvince.Fill(datatableProvince);
                //foreach (DataRow dr in datatableProvince.Rows)
                //{
                //    string pathFile = AppUtils.SaveExcel("BB_RPBM15");

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
                //            xlWorkSheet.Cells[2, 2] = dr["datesST"].ToString();
                //            xlWorkSheet.Cells[3, 2] ="ĐƠN VỊ"+ dr["deptidST"].ToString();
                //            xlWorkSheet.Cells[5, 3] = dr["symbol"].ToString();

                //            xlWorkSheet.Cells[5, 16] = dr["address"].ToString() + ", " + dr["datesST"].ToString();
                //            xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            xlWorkSheet.Cells[9, 2] = "Căn cứ Quy chuẩn kỹ thuật Quốc gia " + dr["technical_regulations"].ToString();
                //            xlWorkSheet.Cells[11, 2] = "Căn cứ  " + dr["base_on_tech"].ToString();
                //            xlWorkSheet.Cells[14, 2] = "Căn cứ biên bản xác nhận số lượng, chủng loại bom mìn vật nổ thu được trong thi công RPBM dự án " + dr["cecm_program_idST"].ToString();
                //            xlWorkSheet.Cells[15, 2] = "Đơn vị "+ dr["deptidST"].ToString() +" lập kế hoạch hủy bom mìn vật nổ thu gom trong quá trình thi công rà phá bom mìn vật nổ như sau: ";
                //            xlWorkSheet.Cells[18, 2] = "Nhằm xử lý toàn bộ số bom mìn vật nổ thu hồi được trong quá trình thi công rà phá bom mìn vật nổ tại dự án "+ dr["cecm_program_idST"].ToString() +" đảm bảo an toàn. ";
                //            xlWorkSheet.Cells[24, 2] = "1. Vận chuyển, hủy nổ bom mìn vật nổ, tổng số lượng: " + dr["num_all_bomb"].ToString();

                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            int indexRow = 26, stt = 1;
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
                //                    xlWorkSheet.Cells[indexRow, 2] = "- "+ dr1["gid"].ToString()+" , "+ dr1["string1"].ToString() + " , " + dr1["string2"].ToString();
                //                    indexRow++;
                //                }
                //            }
                //            if (A1 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            indexRow = indexRow + 2;
                //            xlWorkSheet.Cells[indexRow, 2] = "- Sử dụng lực lượng " + dr["force_used"].ToString();
                //            indexRow += 1;
                //            if (dr["command_destroy_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[indexRow, 2] = "- Chỉ huy hủy nổ: "+ dr["command_destroy_idST"].ToString() +" - Chỉ huy trưởng công trường.";
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[indexRow, 2] = "- Chỉ huy hủy nổ: " + dr["command_destroy_id_other"].ToString() + " - Chỉ huy trưởng công trường.";
                //            }
                //            indexRow = indexRow + 2;
                //            int A2 = indexRow;
                //            foreach (DataRow dr2 in datatableProvince1.Rows)
                //            {
                //                if (dr2["field_name"].ToString().Contains(field_name2))
                //                {
                //                    xlWorkSheet.Cells[indexRow, 2] = "- " + dr2["gid"].ToString() + " , " + dr2["string1"].ToString() + " , " + dr2["string2"].ToString();
                //                    indexRow++;
                //                }
                //            }
                //            if (A2 == indexRow)
                //            {
                //                indexRow++;
                //            }
                //            indexRow += 3;
                //            xlWorkSheet.Cells[indexRow, 2] = "- Thời gian hiệp đồng và ra thông báo hủy: " + dr["dates_notifiedST"].ToString();
                //            indexRow += 2;
                //            xlWorkSheet.Cells[indexRow, 2] = "+ Từ 00 giờ 00, " + dr["time_deli_fromST"].ToString();
                //            indexRow += 1;
                //            xlWorkSheet.Cells[indexRow, 2] = "+ Từ 00 giờ 00, " + dr["time_deli_toST"].ToString();
                //            indexRow += 1;
                //            xlWorkSheet.Cells[indexRow, 2] = "- Thời gian hủy trong ngày: Từ "+ dr["time_destroy_from"].ToString() +" giờ 00 đến " + dr["time_destroy_to"].ToString() +" giờ 00.";
                //            indexRow += 2;
                //            xlWorkSheet.Cells[indexRow, 2] = "Bãi hủy: " + dr["address_destroy"].ToString();
                //            indexRow += 3;
                //            xlWorkSheet.Cells[indexRow, 2] = "- Vận chuyển "+ dr["num_ship"].ToString() +" chuyến (thực hiện nghiêm việc sắp xếp BMVN).";
                //            indexRow += 1;
                //            if (dr["command_destroy_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[indexRow, 2] = "- Chỉ huy vận chuyển: Đồng chí " + dr["command_ship_idST"].ToString() + " - Chỉ huy trưởng công trường.";
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[indexRow, 2] = "- Chỉ huy vận chuyển: Đồng chí " + dr["command_ship_id_other"].ToString() + " - Chỉ huy trưởng công trường.";
                //            }

                //            indexRow += 33;
                //            xlWorkSheet.Cells[indexRow, 2] = "Kính đề nghị "+ dr["organ_approve"].ToString() +" phê duyệt kế hoạch "+ dr["plan_approve"].ToString() +" để các cơ quan, đơn vị liên quan tổ chức thực hiện./.";

                //            indexRow += 5;

                //            if (dr["boss_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[indexRow, 14] = dr["boss_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[indexRow, 14] = dr["boss_id_other"].ToString();
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
                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM15.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM15_" + Guid.NewGuid().ToString();
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
                                "SELECT " +
                                "tbl.*," +
                                "Tinh.Ten as province_idST, " +
                                "case when command_destroy.nameId is null then tbl.command_destroy_id_other else command_destroy.nameId end as Tencommand_destroy, " +
                                "case when boss.nameId is null then tbl.boss_id_other else boss.nameId end as Tenboss, " +
                                "case when command_ship.nameId is null then tbl.command_ship_id_other else command_ship.nameId end as Tencommand_ship, " +
                                //"CONCAT(command_ship_idST, tbl.command_ship_id_other) as Tencommand, " +
                                "cecm_programData.code as 'cecm_program_code' " +
                                "FROM RPBM15 tbl " +
                                "left join cecm_provinces Tinh on tbl.province_id = Tinh.id " +
                                "left join cecm_programData on cecm_programData.id = tbl.cecm_program_id " +
                                "left join Cecm_ProgramStaff command_destroy on tbl.command_destroy_id = command_destroy.id " +
                                "left join Cecm_ProgramStaff boss on tbl.boss_id = boss.id " +
                                "left join Cecm_ProgramStaff command_ship on tbl.command_ship_id = command_ship.id " +
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
                                //marker.AddVariable("Cancu", string.Format("Căn cứ phương án kỹ thuật thi công đã được các cấp có thẩm quyền phê duyệt, Quy tắc an toàn về hủy BMVN theo quy định tại Quy chuẩn {0}, Tiêu chuẩn {1}, điều tra, khảo sát, rà phá bom mìn vật nổ ……………",
                                //    dr["technical_regulations"].ToString(),
                                //    dr["base_on_tech"].ToString()));
                                //marker.AddVariable("Cancu2", string.Format("Các bên thống nhất lập biên bản xác nhận số lượng và chủng loại bom, đạn, vật nổ do đơn vị thi công RPBM {0} thu được trong quá trình thi công RPBM của dự án nói trên như sau:",
                                //    dr["deptidST"].ToString()));
                                //marker.AddVariable("amount", string.Format("'- Biên bản được các bên nhất trí thông qua và lập thành {0} bản có nội dung như nhau….../.",
                                //    dr["amount"].ToString()));
                                marker.AddVariable("symbol", "Số: " + dr["symbol"].ToString());
                                marker.AddVariable("Duan", "Dự án: " + dr["cecm_program_idST"].ToString());
                                marker.AddVariable("NhamXuLy", string.Format("Nhằm xử lý toàn bộ số bom mìn vật nổ thu hồi được trong quá trình thi công rà phá bom mìn vật nổ tại dự án {0} đảm bảo an toàn.",
                                    dr["cecm_program_idST"].ToString()));
                                marker.AddVariable("DonVi", string.Format("Đơn vị {0} lập kế hoạch hủy bom mìn vật nổ thu gom trong quá trình thi công rà phá bom mìn vật nổ như sau:", dr["deptidST"].ToString()));
                                marker.AddVariable("TGHuy", string.Format("'- Thời gian hủy trong ngày: Từ {0} giờ 00 đến {1} giờ 00.",
                                    dr["time_destroy_from"].ToString(),
                                    dr["time_destroy_to"].ToString()));
                                marker.AddVariable("num_ship", string.Format("'- Vận chuyển {0} chuyến (thực hiện nghiêm việc sắp xếp BMVN).",
                                    dr["num_ship"].ToString()));
                                marker.AddVariable("Tencommand_destroy", string.Format("'- Chỉ huy hủy nổ: {0} - Chỉ huy trưởng công trường.",
                                    dr["Tencommand_destroy"].ToString()));
                                marker.AddVariable("Tencommand_ship", string.Format("'- Chỉ huy vận chuyển: Đồng chí {0} - Chỉ huy trưởng công trường.",
                                    dr["Tencommand_ship"].ToString()));
                                marker.AddVariable("DeNghi", string.Format("Kính đề nghị {0} phê duyệt kế hoạch {1} để các cơ quan, đơn vị liên quan tổ chức thực hiện./.",
                                    dr["organ_approve"].ToString(),
                                    dr["plan_approve"].ToString()));

                                string sql = 
                                    "SELECT " +
                                    "tbl.*, " +
                                    "CASE WHEN mst.dvs_name is null or tbl.long1 = 14 then tbl.string1 else mst.dvs_name end as LoaiBMVN " +
                                    "FROM groundDeliveryRecordsMember tbl " +
                                    "left join mst_division mst on mst.dvs_value = tbl.long1 and mst.dvs_group_cd = '001' " +
                                    "where " +
                                    "main_id = " + id_kqks + 
                                    " and table_name = N'" + table_name + "' " +
                                    "and field_name = '" + field_name1 + "'";
                                
                                SqlDataAdapter sqlAdapterBM = new SqlDataAdapter(sql, cn);
                                SqlCommandBuilder sqlCommandBM = new SqlCommandBuilder(sqlAdapterBM);
                                System.Data.DataTable datatableBM = new System.Data.DataTable();
                                sqlAdapterBM.Fill(datatableBM);
                                marker.AddVariable("BM", datatableBM);

                                SqlDataAdapter sqlAdapterPT = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name2 + "'", cn);
                                SqlCommandBuilder sqlCommandPT = new SqlCommandBuilder(sqlAdapterPT);
                                System.Data.DataTable datatablePT = new System.Data.DataTable();
                                sqlAdapterPT.Fill(datatablePT);
                                marker.AddVariable("PT", datatablePT);

                                //    SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (ORDER BY gid) as STT, * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name5 + "'", cn);
                                //    SqlCommandBuilder sqlCommand = new SqlCommandBuilder(sqlAdapterProvince1);
                                //    System.Data.DataTable datatableBMVN = new System.Data.DataTable();
                                //    sqlAdapterProvince1.Fill(datatableBMVN);
                                //    marker.AddVariable("BMVN", datatableBMVN);

                                //    SqlDataAdapter sqlAdapterCDT = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name1 + "'", cn);
                                //    SqlCommandBuilder sqlCommandCDT = new SqlCommandBuilder(sqlAdapterProvince1);
                                //    System.Data.DataTable datatableCDT = new System.Data.DataTable();
                                //    sqlAdapterCDT.Fill(datatableCDT);
                                //    marker.AddVariable("CDT", datatableCDT);

                                //    bool coNguoiKyCDT = false;
                                //    foreach (DataRow drCDT in datatableCDT.Rows)
                                //    {
                                //        if (drCDT["long5"].ToString() == "1")
                                //        {
                                //            marker.AddVariable("daidienCDT", drCDT["string1"].ToString());
                                //            coNguoiKyCDT = true;
                                //            break;
                                //        }
                                //    }
                                //    if (!coNguoiKyCDT)
                                //    {
                                //        marker.AddVariable("daidienCDT", "");
                                //    }

                                //    SqlDataAdapter sqlAdapterDVGS = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name2 + "'", cn);
                                //    SqlCommandBuilder sqlCommandDVGS = new SqlCommandBuilder(sqlAdapterProvince1);
                                //    System.Data.DataTable datatableDVGS = new System.Data.DataTable();
                                //    sqlAdapterDVGS.Fill(datatableDVGS);
                                //    marker.AddVariable("DVGS", datatableDVGS);

                                //    bool coNguoiKyDVGS = false;
                                //    foreach (DataRow drDVGS in datatableDVGS.Rows)
                                //    {
                                //        if (drDVGS["long5"].ToString() == "1")
                                //        {
                                //            marker.AddVariable("daidienDVGS", drDVGS["string1"].ToString());
                                //            coNguoiKyDVGS = true;
                                //            break;
                                //        }
                                //    }
                                //    if (!coNguoiKyDVGS)
                                //    {
                                //        marker.AddVariable("daidienDVGS", "");
                                //    }

                                //    SqlDataAdapter sqlAdapterDP = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name3 + "'", cn);
                                //    SqlCommandBuilder sqlCommandDP = new SqlCommandBuilder(sqlAdapterProvince1);
                                //    System.Data.DataTable datatableDP = new System.Data.DataTable();
                                //    sqlAdapterDP.Fill(datatableDP);
                                //    marker.AddVariable("DP", datatableDP);

                                //    bool coNguoiKyDP = false;
                                //    foreach (DataRow drDP in datatableDP.Rows)
                                //    {
                                //        if (drDP["long5"].ToString() == "1")
                                //        {
                                //            marker.AddVariable("daidienDP", drDP["string1"].ToString());
                                //            coNguoiKyDP = true;
                                //            break;
                                //        }
                                //    }
                                //    if (!coNguoiKyDP)
                                //    {
                                //        marker.AddVariable("daidienDP", "");
                                //    }

                                //    SqlDataAdapter sqlAdapterDVTC = new SqlDataAdapter("SELECT long5, N'Ông' as Ong, N'Chức vụ' as ChucVu, CONCAT(long1ST, string1) as string1, CONCAT(long2ST, string2) as string2 FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name4 + "'", cn);
                                //    SqlCommandBuilder sqlCommandDVTC = new SqlCommandBuilder(sqlAdapterProvince1);
                                //    System.Data.DataTable datatableDVTC = new System.Data.DataTable();
                                //    sqlAdapterDVTC.Fill(datatableDVTC);
                                //    marker.AddVariable("DVTC", datatableDVTC);

                                //    bool coNguoiKyDVTC = false;
                                //    foreach (DataRow drDVTC in datatableDVTC.Rows)
                                //    {
                                //        if (drDVTC["long5"].ToString() == "1")
                                //        {
                                //            marker.AddVariable("daidienDVTC", drDVTC["string1"].ToString());
                                //            coNguoiKyDVTC = true;
                                //            break;
                                //        }
                                //    }
                                //    if (!coNguoiKyDVTC)
                                //    {
                                //        marker.AddVariable("daidienDVTC", "");
                                //    }

                            }

                            marker.ApplyMarkers(Syncfusion.XlsIO.UnknownVariableAction.Skip);

                            //Saving and closing the workbook
                            workbook.SaveAs(pathFile);
                            //workbook.ActiveSheetIndex = 0;
                            //if(workbook.Worksheets.Count > 1)
                            //{
                            //    workbook.Worksheets.Remove(workbook.Worksheets.Count - 1);
                            //}
                        }
                        catch (Exception ex)
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

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM15 left join cecm_programData on cecm_programData.id = RPBM15.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM15 lst = new classRPBM15();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.dates_notifiedST = dr["dates_notifiedST"].ToString();
                    lst.time_signedST = dr["time_signedST"].ToString();
                    lst.time_deli_fromST = dr["time_deli_fromST"].ToString();
                    lst.time_deli_toST = dr["time_deli_toST"].ToString();
                    lst.address_destroy = dr["address_destroy"].ToString();
                    
                    lst.base_on_tech = dr["base_on_tech"].ToString();
                    lst.technical_regulations = dr["technical_regulations"].ToString();
                    lst.force_used = dr["force_used"].ToString();
                    lst.num_ship = dr["num_ship"].ToString();
                    lst.organ_approve = dr["organ_approve"].ToString();
                    lst.plan_approve = dr["plan_approve"].ToString();

                    lst.time_destroy_from = long.Parse(dr["time_destroy_from"].ToString());
                    lst.time_destroy_to = long.Parse(dr["time_destroy_to"].ToString());
    
                    lst.boss_id = long.TryParse(dr["boss_id"].ToString(), out long boss_id) ? boss_id : -1;
                    try
                    {
                        lst.boss_idST = dr["boss_idST"].ToString();
                    }
                    catch
                    {
                        lst.boss_idST = "";
                    }
                    lst.boss_id_other = dr["boss_id_other"].ToString();
                    lst.command_ship_id = long.TryParse(dr["command_ship_id"].ToString(), out long command_ship_id) ? command_ship_id : -1;
                    try
                    {
                        lst.command_ship_idST = dr["command_ship_idST"].ToString();
                    }
                    catch
                    {
                        lst.command_ship_idST = "";
                    }
                    lst.command_ship_id_other = dr["command_ship_id_other"].ToString();
                    lst.command_destroy_id = long.TryParse(dr["command_destroy_id"].ToString(), out long command_destroy_id) ? command_destroy_id : -1;
                    try
                    {
                        lst.command_destroy_idST = dr["command_destroy_idST"].ToString();
                    }
                    catch
                    {
                        lst.command_destroy_idST = "";
                    }
                    lst.command_destroy_id_other = dr["command_destroy_id_other"].ToString();
                    lst.deptid = int.Parse(dr["deptid"].ToString());
                    try
                    {
                        lst.deptidST = dr["deptidST"].ToString();
                    }
                    catch
                    {
                        lst.deptidST = "";
                    }
                    lst.files = dr["files"].ToString();
                    lst.num_all_bomb = double.TryParse(dr["num_all_bomb"].ToString(), out double num_all_bomb) ? num_all_bomb : 0;

                    List <groundDeliveryRecords_Member> Lst1 = new List<groundDeliveryRecords_Member>();
                    List<groundDeliveryRecords_Member> Lst2 = new List<groundDeliveryRecords_Member>();

                    SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("select ROW_NUMBER() OVER (ORDER BY gid) as 'STT', * from groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "'", cn);
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
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            A.long1 = long.TryParse(dr1["long1"].ToString(), out long long1) ? long1 : -1;
                            A.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;
                            Lst1.Add(A);
                        }
                        if (dr1["field_name"].ToString().Contains(field_name2))
                        {
                            groundDeliveryRecords_Member A = new groundDeliveryRecords_Member();
                            A.gid = int.Parse(dr1["gid"].ToString());
                            A.table_name = dr1["table_name"].ToString();
                            A.field_name = dr1["field_name"].ToString();
                            A.main_id = int.Parse(dr1["main_id"].ToString());
                            A.string1 = dr1["string1"].ToString();
                            A.string2 = dr1["string2"].ToString();
                            A.long2 = long.TryParse(dr1["long2"].ToString(), out long long2) ? long2 : -1;
                            Lst2.Add(A);
                        }
                    }
                    lst.planDestroyBombType_lstSubTable = Lst1;
                    lst.planDestroyBombVehicle_lstSubTable = Lst2;

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM15");
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
                FrmThemmoiRPBM15 frm = new FrmThemmoiRPBM15(id_kqks);
                frm.Text = "CHỈNH SỬA BIÊN BẢN BÀN GIAO MẶT BẰNG ĐÃ THI CÔNG RPBM";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM15] WHERE gid = " + id_kqks, cn);
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
            FrmThemmoiRPBM15 frm = new FrmThemmoiRPBM15(0);
            frm.Text = "THÊM MỚI BIÊN BẢN BÀN GIAO MẶT BẰNG ĐÃ THI CÔNG RPBM";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}


