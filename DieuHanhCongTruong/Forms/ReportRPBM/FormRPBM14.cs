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
    public partial class FormRPBM14 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        //public string table_name = "ReportHandoverResult";
        //public string field_name1 = "ReportHandoverResult_ReportHandoverResult_CDTMember";
        //public string field_name2 = "ReportHandoverResult_ReportHandoverResult_ConsMem";
        //public string field_name3 = "ReportHandoverResult_ReportHandoverResult_SurveyMem";
        //public string field_name4 = "ReportHandoverResult_ReportHandoverResult_LocalMem";

        public FormRPBM14()
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM14] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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

        private void FormRPBM14_Load(object sender, EventArgs e)
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

                //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM14 left join cecm_programData on cecm_programData.id = RPBM14.cecm_program_id where gid = " + id_kqks, cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                //sqlAdapterProvince.Fill(datatableProvince);
                //foreach (DataRow dr in datatableProvince.Rows)
                //{
                //    string pathFile = AppUtils.SaveExcel("BB_RPBM14");

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
                //            xlWorkSheet.Cells[2, 2] = dr["deptid_rpbmST"].ToString();
                //            xlWorkSheet.Cells[5, 2] = dr["address"].ToString() + ", " + dr["datesST"].ToString();
                //            xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            xlWorkSheet.Cells[9, 2] = "Hạng mục: " + "Rà phá bom mìn vật nổ";

                //            xlWorkSheet.Cells[10, 2] = "Địa điểm: " + dr["address_cecm"].ToString();
                //            xlWorkSheet.Cells[11, 2] = "Chủ đầu tư: " + dr["cdt"].ToString();
                //            xlWorkSheet.Cells[12, 2] = "ĐƠN VỊ "+ dr["deptid_rpbmST"].ToString() +" THI CÔNG RPBM CAM KẾT";
                //            xlWorkSheet.Cells[14, 2] = dr["ground_done"].ToString();
                //            xlWorkSheet.Cells[17, 2] = "Tổng diện tích rà phá bom mìn, vật nổ cho dự án: "+ dr["area_all_rpbm"].ToString() +" ha";
                //            xlWorkSheet.Cells[18, 2] = "Địa điểm: " + dr["address_rpbm"].ToString();
                //            xlWorkSheet.Cells[19, 2] = "Cụ thể: " + dr["detail"].ToString();
                //            xlWorkSheet.Cells[21, 2] = "Đơn vị "+ dr["deptid_rpbmST"].ToString()+" cam kết chịu trách nhiệm trước Chủ đầu tư và Pháp luật nếu còn sót lại bom đạn, vật nổ trong phạm vi đã rà phá kể từ "+ dr["dates_rpbmST"].ToString() +" Tuy nhiên trách nhiệm này không được tính nếu xuất hiện bom đạn vật nổ từ nơi khác chuyển đến./.";
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            //xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();


                //            if (dr["boss_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[26, 14] = dr["boss_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[26, 14] = dr["boss_id_other"].ToString();
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
                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM14.xls";
                string pathFile = "";
                saveFileDialog1.FileName = "RPBM14_" + Guid.NewGuid().ToString();
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
                                "SELECT *, " +
                                "Tinh.Ten as province_idST, " +
                                "cecm_programData.code as 'cecm_program_code'  " +
                                "FROM RPBM14 " +
                                "left join cecm_provinces Tinh on RPBM14.province_id = Tinh.id " +
                                "left join cecm_programData on cecm_programData.id = RPBM14.cecm_program_id " +
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
                                marker.AddVariable("deep", string.Format("b) Độ sâu rà phá bom mìn vật nổ đến {0} m, tính từ mặt đất tự nhiên hiện tại trở xuống.", dr["deep"].ToString()));
                                marker.AddVariable("CamKetTo", string.Format("ĐƠN VỊ {0} THI CÔNG RPBM CAM KẾT",
                                    dr["deptid_rpbmST"].ToString()));
                                marker.AddVariable("CamKet", string.Format("Đơn vị {0} cam kết chịu trách nhiệm trước Chủ đầu tư và Pháp luật nếu còn sót lại bom đạn, vật nổ trong phạm vi đã rà phá kể từ {1} Tuy nhiên trách nhiệm này không được tính nếu xuất hiện bom đạn vật nổ từ nơi khác chuyển đến./.", 
                                    dr["deptid_rpbmST"].ToString(),
                                    dr["dates_rpbmST"]));
                                marker.AddVariable("Tenboss", string.IsNullOrEmpty(dr["boss_idST"].ToString()) ? dr["boss_id_other"].ToString() : dr["boss_idST"].ToString());
                                marker.AddVariable("TongDT", string.Format("Tổng diện tích rà phá bom mìn, vật nổ cho dự án: {0} ha", dr["area_all_rpbm"].ToString()));
                                //marker.AddVariable("Cancu", string.Format("Căn cứ phương án kỹ thuật thi công đã được các cấp có thẩm quyền phê duyệt, Quy tắc an toàn về hủy BMVN theo quy định tại Quy chuẩn {0}, Tiêu chuẩn {1}, điều tra, khảo sát, rà phá bom mìn vật nổ ……………",
                                //    dr["technical_regulations"].ToString(),
                                //    dr["base_on_tech"].ToString()));
                                //marker.AddVariable("Cancu2", string.Format("Các bên thống nhất lập biên bản xác nhận số lượng và chủng loại bom, đạn, vật nổ do đơn vị thi công RPBM {0} thu được trong quá trình thi công RPBM của dự án nói trên như sau:",
                                //    dr["deptidST"].ToString()));
                                //marker.AddVariable("amount", string.Format("'- Biên bản được các bên nhất trí thông qua và lập thành {0} bản có nội dung như nhau….../.",
                                //    dr["amount"].ToString()));
                                //marker.AddVariable("symbol", "Số: " + dr["symbol"].ToString());
                                //marker.AddVariable("Duan", "Dự án: " + dr["cecm_program_idST"].ToString());
                                //marker.AddVariable("NhamXuLy", string.Format("Nhằm xử lý toàn bộ số bom mìn vật nổ thu hồi được trong quá trình thi công rà phá bom mìn vật nổ tại dự án {0} đảm bảo an toàn.",
                                //    dr["cecm_program_idST"].ToString()));
                                //marker.AddVariable("DonVi", string.Format("Đơn vị {0} lập kế hoạch hủy bom mìn vật nổ thu gom trong quá trình thi công rà phá bom mìn vật nổ như sau:", dr["deptidST"].ToString()));
                                //marker.AddVariable("TGHuy", string.Format("'- Thời gian hủy trong ngày: Từ {0} giờ 00 đến {1} giờ 00.",
                                //    dr["time_destroy_from"].ToString(),
                                //    dr["time_destroy_to"].ToString()));
                                //marker.AddVariable("num_ship", string.Format("'- Vận chuyển {0} chuyến (thực hiện nghiêm việc sắp xếp BMVN).",
                                //    dr["num_ship"].ToString()));
                                //marker.AddVariable("Tencommand_destroy", string.Format("'- Chỉ huy hủy nổ: {0} - Chỉ huy trưởng công trường.",
                                //    dr["Tencommand_destroy"].ToString()));
                                //marker.AddVariable("Tencommand_ship", string.Format("'- Chỉ huy vận chuyển: Đồng chí {0} - Chỉ huy trưởng công trường.",
                                //    dr["Tencommand_ship"].ToString()));
                                //marker.AddVariable("DeNghi", string.Format("Kính đề nghị {0} phê duyệt kế hoạch {1} để các cơ quan, đơn vị liên quan tổ chức thực hiện./.",
                                //    dr["organ_approve"].ToString(),
                                //    dr["plan_approve"].ToString()));

                                //SqlDataAdapter sqlAdapterBM = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name1 + "'", cn);
                                //SqlCommandBuilder sqlCommandBM = new SqlCommandBuilder(sqlAdapterBM);
                                //System.Data.DataTable datatableBM = new System.Data.DataTable();
                                //sqlAdapterBM.Fill(datatableBM);
                                //marker.AddVariable("BM", datatableBM);

                                //SqlDataAdapter sqlAdapterPT = new SqlDataAdapter("SELECT * FROM groundDeliveryRecordsMember where main_id = " + id_kqks + "and table_name = N'" + table_name + "' and field_name = '" + field_name2 + "'", cn);
                                //SqlCommandBuilder sqlCommandPT = new SqlCommandBuilder(sqlAdapterPT);
                                //System.Data.DataTable datatablePT = new System.Data.DataTable();
                                //sqlAdapterPT.Fill(datatablePT);
                                //marker.AddVariable("PT", datatablePT);

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

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM14 left join cecm_programData on cecm_programData.id = RPBM14.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM14 lst = new classRPBM14();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.dates_rpbmST = dr["dates_rpbmST"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    try
                    {
                        lst.deptid_rpbmST = dr["deptid_rpbmST"].ToString();
                    }
                    catch
                    {
                        lst.deptid_rpbmST = "";
                    }
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.cdt = dr["cdt"].ToString();
                    lst.ground_done = dr["ground_done"].ToString();
                    lst.detail = dr["detail"].ToString();
                    lst.address_rpbm = dr["address_rpbm"].ToString();

                    lst.deep = double.Parse(dr["deep"].ToString());
                    lst.area_all_rpbm = double.Parse(dr["area_all_rpbm"].ToString());
                    lst.deptid_rpbm = long.Parse(dr["deptid_rpbm"].ToString());
                    lst.captain_sign_id = int.Parse(dr["boss_id"].ToString());
                    try
                    {
                        lst.captain_sign_idST = dr["boss_idST"].ToString();
                    }
                    catch
                    {
                        lst.captain_sign_idST = "";
                    }
                    lst.captain_sign_id_other = dr["boss_id_other"].ToString();
                    lst.files = dr["files"].ToString();

                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM14");
                    if (string.IsNullOrEmpty(saveLocation) == false)
                    {
                        System.IO.File.WriteAllText(saveLocation, json);
                        MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xuất file dữ liệu thất bại... ", "Thất bại");
                    }

                    //MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                    //CompressedFile frm = new CompressedFile(LstDaily, Phieudieutra, Baocao, Bando);
                    //frm.ShowDialog();
                }
            }
            if (e.ColumnIndex == 6)
            {
                //UtilsDatabase.DeleteMemberByTablename(table_name);
                FrmThemmoiRPBM14 frm = new FrmThemmoiRPBM14(id_kqks);
                frm.Text = "CHỈNH SỬA CAM KẾT BẢO ĐẢM AN TOÀN MẶT BẰNG ĐÃ RÀ PHÁ BOM MÌN VẬT NỔ";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM14] WHERE gid = " + id_kqks, cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
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
            //UtilsDatabase.DeleteMemberByTablename(table_name);
            FrmThemmoiRPBM14 frm = new FrmThemmoiRPBM14(0);
            frm.Text = "THÊM MỚI CAM KẾT BẢO ĐẢM AN TOÀN MẶT BẰNG ĐÃ RÀ PHÁ BOM MÌN VẬT NỔ";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}


