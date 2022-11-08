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
    public partial class FormRPBM4 : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public string table_name = "CONSTRUCTIONDIARYBOMB";
        public FormRPBM4()
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

                sqlAdapter = new SqlDataAdapter(string.Format("select gid as 'ID', cecm_program_idST as 'Dự án', datesST as 'Thời gian', address as 'Địa điểm' from [dbo].[RPBM4] where cecm_program_idST LIKE N'%" + name + "%' and datesST >'" + date1 + "' and datesST <'" + date2 + "'"), cn);
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
            if (e.ColumnIndex == cotExcel.Index)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                //SqlCommandBuilder sqlCommand = null;

                //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM4 left join cecm_programData on cecm_programData.id = RPBM4.cecm_program_id where gid = " + id_kqks, cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                //System.Data.DataTable datatableProvince = new System.Data.DataTable();
                //sqlAdapterProvince.Fill(datatableProvince);
                //foreach (DataRow dr in datatableProvince.Rows)
                //{
                //    string pathFile = AppUtils.SaveExcel("BB_RPBM4");

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
                //            xlWorkSheet.Cells[2, 2] = dr["deptid_tcksST"].ToString();

                //            xlWorkSheet.Cells[5, 2] = dr["address"].ToString() + ", " + dr["datesST"].ToString();
                //            xlWorkSheet.Cells[9, 2] = "Hạng mục: " + dr["category"].ToString();
                //            xlWorkSheet.Cells[8, 2] = "Dự án: " + dr["cecm_program_idST"].ToString();
                //            xlWorkSheet.Cells[10, 2] = "Địa điểm: " + dr["address_cecm"].ToString();
                //            xlWorkSheet.Cells[13, 2] = "- Thông tư số: " + dr["thong_tu"].ToString();
                //            xlWorkSheet.Cells[14, 2] = "- Quy chuẩn kỹ thuật: " + dr["base_qckt"].ToString() ;
                //            xlWorkSheet.Cells[16, 2] = "- Hợp đồng kinh tế số: " + dr["base_hdkt_number"].ToString() + "/HĐKT-RPBM ngày " + dr["dates_hdktST"].ToString() + " được ký kết giữa " + dr["organization_signed"].ToString();
                //            xlWorkSheet.Cells[25, 2] = "- Xác định mật độ tín hiệu độ sâu đến " + dr["deep_signal"].ToString() + " m, tính từ mặt đất tự nhiên hiện tại trở xuống. Cấp đất khi phải đào xử lý tín hiệu.";
                //            xlWorkSheet.Cells[30, 2] = "Sử dụng " + dr["num_team_construct"].ToString() + "  đội thi công khảo sát hỗn hợp trên cạn và dưới nước, quân số "+ dr["num_mem_all"].ToString() + " đ/c; thành phần gồm: "+ dr["num_mem_all"].ToString() + " đ/c;  phụ trách phân đội: "+dr["num_mem_1"].ToString() + " đ/c; kỹ thuật viên: "+ dr["num_mem_2"].ToString() + " đ/c; nhân viên y tế  "+ dr["num_mem_3"].ToString() + " đ/c";

                //            if (dr["captain_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[31, 2] = "Chỉ huy: " + dr["captain_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[31, 2] = "Chỉ huy: " + dr["captain_id_other"].ToString();
                //            }
                //            xlWorkSheet.Cells[34, 2] = "- Bắt đầu từ: " + dr["time_nt_fromST"].ToString();
                //            xlWorkSheet.Cells[35, 2] = "- Hoàn thành: " + dr["time_nt_toST"].ToString();

                //            xlWorkSheet.Cells[39, 2] = "- Diện tích RPBM của dự án: " + dr["area_rpbm"].ToString() + " ha";
                //            xlWorkSheet.Cells[40, 2] = "- Diện tích thi công khảo sát: " + dr["area_tcks"].ToString()+ " ha";
                //            xlWorkSheet.Cells[41, 2] = "- Tiến hành thi công " + dr["num_tcks"].ToString() + " điểm. Mỗi điểm khảo sát có kích thước ô là 25 m x 25 m = 500 m2. Với diện tích khảo sát: "+ dr["area_ks"].ToString()+ " ha ≥ 1% diện tích thi công RPBM của dự án.";
                //            xlWorkSheet.Cells[43, 2] = "- Diện tích phát dọn tương đương rừng loại " + dr["type_forest_1"].ToString() + " : " + dr["area_phatdon"].ToString() + " ha";
                //            xlWorkSheet.Cells[44, 2] = "- Diện tích khảo sát RPBM đến độ sâu 0,3 m: " + dr["area_ks_1"].ToString() + " ha";
                //            xlWorkSheet.Cells[45, 2] = "- Xử lý tín hiệu đến độ sâu 0,3 m: " + dr["signal_process"].ToString() + " tín hiệu";
                //            xlWorkSheet.Cells[46, 2] = "- Diện tích khảo sát rà phá bom mìn, vật nổ đến độ sâu 5 m: " + dr["area_ks_2"].ToString() + " ha";

                //            xlWorkSheet.Cells[47, 2] = "- Đào, xử lý tín hiệu độ sâu 0,3 m đến 3 m: " + dr["dig_lane_signal_1"].ToString() + " tín hiệu";
                //            xlWorkSheet.Cells[48, 2] = "- Đào, xử lý tín hiệu độ sâu 5 m: " + dr["dig_lane_signal_2"].ToString() + " tín hiệu";
                //            xlWorkSheet.Cells[49, 2] = "* Kết quả thu được: " + dr["result"].ToString();

                //            xlWorkSheet.Cells[51, 2] = "- Tỷ lệ phát dọn mặt bằng thi công tương đương rừng loại " + dr["type_forest_2"].ToString() + " chiếm " + dr["ratio_clean_ground"].ToString() + " % tổng diện tích thi công khảo sát trong khu vực.";
                //            xlWorkSheet.Cells[52, 2] = "- Mật độ tín hiệu trên cạn đến độ sâu 0,3 m, mật độ loại " + dr["type_signal_density_1"].ToString() + " trung bình: " + dr["avg_signal_density_1"].ToString() + " tín hiệu/ha.";
                //            xlWorkSheet.Cells[53, 2] = "- Mật độ tín hiệu trên cạn đến độ sâu 3 m trung bình: " + dr["avg_signal_density_2"].ToString() + " tín hiệu/ha";
                //            xlWorkSheet.Cells[54, 2] = "- Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình: " + dr["avg_signal_density_3"].ToString() + " tín hiệu/ha";
                //            xlWorkSheet.Cells[55, 2] = "- Cấp đất tại khu vực thi công ở độ sâu 0,3 m là đất cấp " + dr["type_land_1"].ToString() + " ; lớn hơn 0,3 m đến 5 m là đất cấp  " + dr["type_land_2"].ToString();

                //            xlWorkSheet.Cells[57, 2] = "- Địa hình: " + dr["topo"].ToString();
                //            xlWorkSheet.Cells[58, 2] = "- Địa chất: Qua khảo sát đánh giá địa chất đất tại khu vực thi công dự án, với độ sâu từ mặt đất tự nhiên đến 0,3 m được xác định là loại đất cấp " + dr["type_land_3"].ToString() + " , lớn hơn 0,3 m đến 5 m: được xác định là đất cấp " + dr["type_land_4"].ToString();
                //            xlWorkSheet.Cells[59, 2] = "- Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình: " + dr["avg_signal_density_3"].ToString()+" tín hiệu/ha";
                //            xlWorkSheet.Cells[60, 2] = "- Khí hậu, thủy văn: " + dr["climate"].ToString();
                //            xlWorkSheet.Cells[61, 2] = "- Tỷ lệ phát dọn mặt bằng thi công tương đương rừng loại " + dr["type_forest_2"].ToString() + " chiếm "+ dr["ratio_clean_ground"].ToString() + " % tổng diện tích thi công khảo sát trong khu vực.";
                //            xlWorkSheet.Cells[63, 2] = "- Tình hình bom mìn vật nổ: " + dr["situation_bomb"].ToString();
                //            xlWorkSheet.Cells[64, 2] = "- Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình: " + dr["avg_signal_density_3"].ToString() + " tín hiệu/ha";
                //            xlWorkSheet.Cells[65, 2] = "Qua các thông tin và tài liệu lưu trữ về lịch sử chiến tranh, về các trận oanh tạc bằng không quân " + dr["infor_other"].ToString();
                //            xlWorkSheet.Cells[66, 2] = "Khu vực có bị ảnh hưởng bom mìn vật nổ trong chiến tranh " + dr["area_affect"].ToString();

                //            xlWorkSheet.Cells[69, 2] = "Đơn vị thi công khảo sát " + dr["deptid_tcksST"].ToString()+ " đã thực hiện xong công tác khảo sát  Việc chấp hành các quy định  , tiến độ ……….. số liệu trung thực không.... tiết kiệm ….. công tác an toàn …..";
                //            xlWorkSheet.Cells[70, 2] = dr["conclusion"].ToString();
                //            if (dr["survey_idST"].ToString() != null)
                //            {
                //                xlWorkSheet.Cells[74, 14] =dr["survey_idST"].ToString();
                //            }
                //            else
                //            {
                //                xlWorkSheet.Cells[71, 14] =dr["survey_id_other"].ToString();
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
                try
                {


                    string pathFile = "";
                    saveFileDialog1.FileName = "RPBM4_" + Guid.NewGuid().ToString();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        pathFile = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                    string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_RPBM4.xls";
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
                            SqlDataAdapter sqlAdapter = new SqlDataAdapter(string.Format(
                                "Select " +
                                "tbl.*, " +
                                "Tinh.Ten as province_idST " +
                                "from RPBM4 tbl " +
                                "left join cecm_provinces Tinh on tbl.province_id = Tinh.id " +
                                //"left join cecm_provinces Huyen on tbl.district_id = Huyen.id " +
                                //"left join cecm_provinces Xa on tbl.commune_id = Xa.id " +
                                "where gid = {0}", id_kqks), cn);
                            //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                            SqlCommandBuilder sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                            sqlAdapter.Fill(datatable);
                            marker.AddVariable("obj", datatable);
                            if (datatable.Rows.Count != 0)
                            {
                                DataRow dr = datatable.Rows[0];
                                //marker.AddVariable("MaBaocaongay", "Số: " + dr["MaBaocaongay"].ToString() + "/BC-KSN");
                                //marker.AddVariable("MaBaocaongay_short", dr["MaBaocaongay"].ToString());
                                DateTime date = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                marker.AddVariable("Ngaybaocao", dr["province_idST"].ToString().Replace("Tỉnh", "").Replace("Thành phố", "") + ", ngày " + date.Day + " tháng " + date.Month + " năm " + date.Year);
                                DateTime dates_hdkt = DateTime.ParseExact(dr["dates_hdktST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                marker.AddVariable("hdkt", 
                                    string.Format(
                                        " - Hợp đồng kinh tế số {0}/HĐKT-KSRPBM ngày {1} tháng {2} năm {3}.được ký kết giữa {4} về việc thi công gói thầu rà phá bom mìn vật nổ dự án: {5} địa điểm {6}", 
                                        dr["base_hdkt_number"].ToString(), 
                                        dates_hdkt.Day, 
                                        dates_hdkt.Month, 
                                        dates_hdkt.Year, 
                                        dr["organization_signed"].ToString(),
                                        dr["cecm_program_idST"].ToString(),
                                        dr["address"].ToString()));
                                marker.AddVariable("deep_signal", string.Format(" - Xác định mật độ tín hiệu độ sâu đến {0} m, tính từ mặt đất tự nhiên hiện tại trở xuống. Cấp đất khi phải đào xử lý tín hiệu.", dr["deep_signal"].ToString()));
                                marker.AddVariable("LucLuong", string.Format("Sử dụng {0} đội thi công khảo sát hỗn hợp trên cạn và dưới nước, quân số {1} đ/c; thành phần gồm: {2} đ/c; phụ trách phân đội {3} đ/c; kỹ thuật viên: {4} đ/c; nhân viên y tế {5} đ/c.", 
                                    dr["num_team_construct"].ToString(), 
                                    dr["num_mem_all"].ToString(),
                                    dr["num_mem_all"].ToString(),
                                    dr["num_mem_1"].ToString(),
                                    dr["num_mem_2"].ToString(),
                                    dr["num_mem_3"].ToString()
                                ));
                                marker.AddVariable("ChiHuy", "Chỉ huy: " + (!string.IsNullOrEmpty(dr["captain_idST"].ToString()) && dr["captain_idST"].ToString() != "Chọn" ? dr["captain_idST"].ToString() : dr["captain_id_other"].ToString()));
                                marker.AddVariable("TienHanhTC", string.Format(" - Tiến hành thi công {0} điểm. Mỗi điểm khảo sát có kích thước ô là 25 m x 25 m = 500 m2. Với diện tích khảo sát: {1} ha ≥ 1% diện tích thi công RPBM của dự án.",
                                    dr["num_tcks"].ToString(),
                                    dr["area_ks"].ToString()
                                ));
                                marker.AddVariable("DTPhatDon", string.Format(" - Diện tích phát dọn tương đương rừng loại {0}: {1} ha.",
                                    dr["type_forest_1"].ToString(),
                                    dr["area_phatdon"].ToString()
                                ));
                                marker.AddVariable("TLPhatDon", string.Format(" - Tỷ lệ phát dọn mặt bằng thi công tương đương rừng loại {0} chiếm {1} % tổng diện tích thi công khảo sát trong khu vực.",
                                    dr["type_forest_2"].ToString(),
                                    dr["ratio_clean_ground"].ToString()
                                ));
                                marker.AddVariable("MDTH1", string.Format(" - Mật độ tín hiệu trên cạn đến độ sâu 0,3 m, mật độ loại {0} trung bình: {1} tín hiệu/ha.",
                                    dr["type_signal_density_1"].ToString(),
                                    dr["avg_signal_density_1"].ToString()
                                ));
                                marker.AddVariable("CapDat", string.Format(" - Cấp đất tại khu vực thi công ở độ sâu 0,3 m là đất cấp {0} ; lớn hơn 0,3 m đến 5 m là đất cấp {1}",
                                    dr["type_land_1"].ToString(),
                                    dr["type_land_2"].ToString()
                                ));
                                marker.AddVariable("DiaChat", string.Format(" - Địa chất: Qua khảo sát đánh giá địa chất đất tại khu vực thi công dự án, với độ sâu từ mặt đất tự nhiên đến 0,3 m được xác định là loại đất cấp {0} , lớn hơn 0,3 m đến 5 m: được xác định là đất cấp {1}",
                                    dr["type_land_3"].ToString(),
                                    dr["type_land_4"].ToString()
                                ));
                                marker.AddVariable("KL", string.Format("Đơn vị thi công khảo sát {0} đã thực hiện xong công tác khảo sát  Việc chấp hành các quy định  , tiến độ ……….. số liệu trung thực không.... tiết kiệm ….. công tác an toàn …..\n{1}",
                                    dr["deptid_tcksST"].ToString(),
                                    dr["conclusion"].ToString()
                                ));
                                marker.AddVariable("DaiDienDVKS", (!string.IsNullOrEmpty(dr["survey_idST"].ToString()) && dr["survey_idST"].ToString() != "Chọn" ? dr["survey_idST"].ToString() : dr["survey_id_other"].ToString()));
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }

            if (e.ColumnIndex == 5)
            {
                //string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * , cecm_programData.code as 'cecm_program_code'  FROM RPBM4 left join cecm_programData on cecm_programData.id = RPBM4.cecm_program_id where gid = " + id_kqks, cn);

                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    classRPBM4 lst = new classRPBM4();
                    lst.gid = int.Parse(dr["gid"].ToString());
                    lst.deptid_tcks = int.Parse(dr["deptid_tcks"].ToString());
                    lst.address = dr["address"].ToString();
                    lst.datesST = dr["datesST"].ToString();
                    lst.cecm_program_id = int.Parse(dr["cecm_program_id"].ToString());
                    lst.cecm_program_idST = dr["cecm_program_idST"].ToString();
                    lst.cecm_program_code = dr["cecm_program_code"].ToString();
                    lst.symbol = dr["symbol"].ToString();
                    lst.address_cecm = dr["address_cecm"].ToString();
                    lst.thong_tu = dr["thong_tu"].ToString();
                    lst.base_qckt = dr["base_qckt"].ToString();
                    lst.base_hdkt_number = dr["base_hdkt_number"].ToString();
                    lst.dates_hdktST = dr["dates_hdktST"].ToString();
                    lst.organization_signed = dr["organization_signed"].ToString();
                    lst.deep_signal = double.Parse(dr["deep_signal"].ToString());
                    lst.num_team_construct = dr["num_team_construct"].ToString();
                    lst.num_mem_all = dr["num_mem_all"].ToString();
                    lst.num_mem_1 = dr["num_mem_1"].ToString();
                    lst.num_mem_2 = dr["num_mem_2"].ToString();
                    lst.num_mem_3 = dr["num_mem_3"].ToString();
                    lst.captain_id = int.TryParse(dr["captain_id"].ToString(), out int captain_id) ? captain_id : -1;
                    lst.captain_idST = dr["captain_idST"].ToString();
                    lst.captain_id_other = dr["captain_id_other"].ToString();
                    lst.time_nt_fromST = dr["time_nt_fromST"].ToString();
                    lst.time_nt_toST = dr["time_nt_toST"].ToString();
                    lst.area_rpbm = double.Parse(dr["area_rpbm"].ToString());
                    lst.area_tcks = dr["area_tcks"].ToString();
                    lst.num_tcks = dr["num_tcks"].ToString();
                    lst.area_ks = dr["area_ks"].ToString();
                    lst.type_forest_1 = dr["type_forest_1"].ToString();
                    lst.area_phatdon = dr["area_phatdon"].ToString();
                    lst.area_ks_1 = dr["area_ks_1"].ToString();
                    lst.signal_process = dr["signal_process"].ToString();
                    lst.area_ks_2 = dr["area_ks_2"].ToString();
                    lst.dig_lane_signal_1 = dr["dig_lane_signal_1"].ToString();
                    lst.dig_lane_signal_2 = dr["dig_lane_signal_2"].ToString();
                    lst.result = dr["result"].ToString();
                    lst.ratio_clean_ground = dr["ratio_clean_ground"].ToString();
                    lst.type_forest_2 = dr["type_forest_2"].ToString();
                    lst.type_signal_density_1 = dr["type_signal_density_1"].ToString();
                    lst.avg_signal_density_1 = dr["avg_signal_density_1"].ToString();
                    lst.avg_signal_density_2 = dr["avg_signal_density_2"].ToString();
                    lst.avg_signal_density_3 = dr["avg_signal_density_3"].ToString();
                    lst.type_land_1 = dr["type_land_1"].ToString();
                    lst.type_land_2 = dr["type_land_2"].ToString();
                    lst.type_land_3 = dr["type_land_3"].ToString();
                    lst.type_land_4 = dr["type_land_4"].ToString();
                    lst.topo = dr["topo"].ToString();
                    lst.climate = dr["climate"].ToString();
                    lst.situation_bomb = dr["situation_bomb"].ToString();
                    lst.infor_other = dr["infor_other"].ToString();
                    lst.area_affect = dr["area_affect"].ToString();
                    lst.situation_dancu = dr["population"].ToString();
                    try
                    {
                        lst.deptid_tcksST = dr["deptid_tcksST"].ToString();
                    }
                    catch
                    {
                        lst.deptid_tcksST = "";
                    }

                    
                    lst.conclusion = dr["conclusion"].ToString();
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
                    lst.files = dr["files"].ToString();


                    string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileJson("JsonRPBM4");
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
                FrmThemmoiRPBM4 frm = new FrmThemmoiRPBM4(id_kqks);
                frm.Text = "CHỈNH SỬA BÁO CÁO KẾT QUẢ KHẢO SÁT RÀ PHÁ BOM MÌN";
                frm.ShowDialog();
                LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
            }
            //delete column
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM [dbo].[RPBM4] WHERE gid = " + id_kqks, cn);
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
            FrmThemmoiRPBM4 frm = new FrmThemmoiRPBM4(0);
            frm.Text = "THÊM MỚI BÁO CÁO KẾT QUẢ KHẢO SÁT RÀ PHÁ BOM MÌN";
            frm.ShowDialog();
            LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        }
    }

}

