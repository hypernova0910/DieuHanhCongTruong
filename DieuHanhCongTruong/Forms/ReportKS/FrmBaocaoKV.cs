using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Common;

namespace VNRaPaBomMin
{
    public partial class FrmBaocaoKV : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan = 0;
        public FrmBaocaoKV()
        {
            cn = frmLoggin.sqlCon;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = DGV_BaoCaoKV.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == DoiRPBMSua.Index)
            {
                FrmThemmoiBaoCaoKQKV frm = new FrmThemmoiBaoCaoKQKV(id_kqks);
                frm.Text = "CHỈNH SỬA BÁO CÁO KẾT QUẢ KHẢO SÁT KHU VỰC";
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    FrmBaocaoKQ_Load(sender, e);
                }
            }
            //export
            if (e.ColumnIndex == Export.Index)
            {
                List<result_report_lstSubTable> result_report_lstSubTablex = new List<result_report_lstSubTable>();

                SqlCommandBuilder sqlCommand = null;
                SqlCommandBuilder sqlCommand1 = null;
                SqlCommandBuilder sqlCommand2 = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(
                    "SELECT " +
                    //"cecm_report_survey_area.id," +
                    //"cecm_report_survey_area.ChiHuyCT," +
                    "cecm_report_survey_area.timeTongSo,cecm_programData.id as 'Duanid', " +
                    "cecm_programData.name as 'Duan'," +
                    "cecm_programData.code as 'Duancode'," +
                    "[Hopphan],Tinh.code as 'Tinh'," +
                    "Tinh.Ten as 'Tinhcode'," +
                    "Huyen.code as 'Huyen'," +
                    "Huyen.Ten as 'Huyencode'," +
                    "Xa.code as 'Xa'," +
                    "Xa.Ten as 'Xacode'," +
                    //"cecm_report_survey_area.Hopphan," +
                    //"MaNV," +
                    //"timeBatdau," +
                    //"timeKetthuc," +
                    //"DientichKS," +
                    //"NguonBangChung," +
                    //"SLBangChungYeuCau," +
                    //"cecm_report_survey_area.isactive," +
                    //"SLDaKS," +
                    //"DTDaKS," +
                    //"DTKSKhacYeuCau," +
                    //"DTDaKTCL," +
                    //"DTPhaiLamLai," +
                    //"Odo," +
                    //"TinHieu," +
                    //"Ovang," +
                    //"Oxanhdatroi," +
                    //"Otrang," +
                    //"Oxam," +
                    //"Oxanhla," +
                    //"TongSoOKS," +
                    //"SoOONhiem," +
                    //"SoKVONhiem," +
                    //"DTKhuONhiem," +
                    //"MucDoONhiem," +
                    //"TienDoTH," +
                    "d.code as DVKScode," +
                    @"cecm_report_survey_area.[id]
                      ,[Duan]
                      ,[MaBaocaongay]
                      ,[Hopphan]
                      ,[Tinh]
                      ,[Huyen]
                      ,[Xa]
                      ,[Thon]
                      ,[MaNV]
                      ,[timeBatdau]
                      ,[timeKetthuc]
                      ,[timeTongSo]
                      ,[Doiso]
                      ,[Doitruong]
                      ,[Ngaytao]
                      ,[DientichKS]
                      ,[NguonBangChung]
                      ,[SLBangChungYeuCau]
                      ,cecm_report_survey_area.[isactive]
                      ,[SLDaKS]
                      ,[DTDaKS]
                      ,[DTKSKhacYeuCau]
                      ,[DTDaKTCL]
                      ,[DTPhaiLamLai]
                      ,[TinHieu]
                      ,[Oxanhla]
                      ,[Odo]
                      ,[Oxam]
                      ,[Ovang]
                      ,[Otrang]
                      ,[Oxanhdatroi]
                      ,[TongSoOKS]
                      ,[SoOONhiem]
                      ,[SoKVONhiem]
                      ,[DTKhongONhiem]
                      ,[DTKhuONhiem]
                      ,[MucDoONhiem]
                      ,[TienDoTH]
                      ,GiamSat
                      ,[ChiHuyCT]
                      ,[KVKhaoSat]
                      ,[PhotoFile]
                      ,[DrawFile]
                      ,[NguoiVe]
                      ,[TGVe]
                      ,[DVKS_id]
                      ,[Tinh_id]
                      ,[Huyen_id]
                      ,[Xa_id]" +
                    ",cert_command_person.name as 'Doiso'," +
                    "giamsat.nameId as GiamSat_idST, " +
                    "chihuy.nameId as ChiHuyCT_idST, " +
                    "cert_command_person.id as 'Doisoid'," +
                    "cert_command_person.master as 'Doitruong', " +
                    "area_map.code as cecm_program_area_code " +
                    "FROM [dbo].cecm_report_survey_area " +
                    "left join cecm_programData on cecm_programData.name = cecm_report_survey_area.Duan " +
                    "left join cecm_provinces as Tinh on cecm_report_survey_area.[Tinh_id] = Tinh.id " +
                    "left join cecm_provinces as Huyen on cecm_report_survey_area.Huyen_id = Huyen.id " +
                    "left join cecm_provinces as Xa on cecm_report_survey_area.Xa_id = Xa.id " +
                    "left join cert_command_person on cecm_report_survey_area.Doiso = cert_command_person.name " +
                    "left join cert_department d on d.id = cecm_report_survey_area.DVKS_id " +
                    "left join Cecm_ProgramStaff giamsat on cecm_report_survey_area.GiamSat_id = giamsat.id " +
                    "left join Cecm_ProgramStaff chihuy on cecm_report_survey_area.ChiHuyCT_id = chihuy.id " +
                    "left join cecm_program_area_map area_map on cecm_report_survey_area.cecm_program_area_map_id = area_map.id " +
                    "where cecm_report_survey_area.id = " + id_kqks, cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);

                foreach (DataRow dr in datatableProvince.Rows)
                {
                    int.TryParse(dr["Duanid"].ToString(), out idDuan);
                    //idDuan = int.Parse(dr["Duanid"].ToString());
                }
                //SqlDataAdapter sqlAdapterBom = new SqlDataAdapter("SELECT Cecm_VNTerrainMinePoint.*,Cecm_TerrainRectangle.code FROM Cecm_VNTerrainMinePoint left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint.programId left join Cecm_TerrainRectangle on Cecm_TerrainRectangle.id = Cecm_VNTerrainMinePoint.idRectangle where idRectangle != -1 and (cecm_program_area_map.cecm_program_id = " + idDuan + "or cecm_program_area_map.cecm_program_id = " + idDuan + ")", cn);
                //sqlCommand2 = new SqlCommandBuilder(sqlAdapterBom);
                //System.Data.DataTable datatableBom = new System.Data.DataTable();
                //sqlAdapterBom.Fill(datatableBom);
                SqlCommandBuilder sqlCommand3 = null;
                SqlDataAdapter sqlAdapter3 = null;
                System.Data.DataTable tableBMVN = new System.Data.DataTable();
                string sql = "Select tbl.*, ol.o_id as ol_idST " +
                    "FROM cecm_report_survey_area_BMVN tbl " +
                    "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                    "WHERE tbl.cecm_report_survey_area_id = " + id_kqks;
                sqlAdapter3 = new SqlDataAdapter(sql, cn);
                sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                sqlAdapter3.Fill(tableBMVN);
                if (tableBMVN.Rows.Count != 0)
                {
                    foreach (DataRow dr in tableBMVN.Rows)
                    {
                        double x = 0, y = 0, z = 0, t = 0;
                        //if (string.IsNullOrEmpty(dr["Area"].ToString()) == false)
                        //{
                        //    x = Math.Pow(double.Parse(dr["Area"].ToString()), 2);
                        //}
                        if (string.IsNullOrEmpty(dr["Deep"].ToString()) == false)
                        {
                            y = double.Parse(dr["Deep"].ToString());
                        }
                        //if (string.IsNullOrEmpty(dr["XPoint"].ToString()) == false)
                        //{
                        //    z = Math.Round(double.Parse(dr["XPoint"].ToString()), 6);
                        //}
                        //if (string.IsNullOrEmpty(dr["YPoint"].ToString()) == false)
                        //{
                        //    t = Math.Round(double.Parse(dr["YPoint"].ToString()), 6);
                        //}
                        if (string.IsNullOrEmpty(dr["Kinhdo"].ToString()) == false)
                        {
                            z = Math.Round(double.Parse(dr["Kinhdo"].ToString()), 6);
                        }
                        if (string.IsNullOrEmpty(dr["Vido"].ToString()) == false)
                        {
                            t = Math.Round(double.Parse(dr["Vido"].ToString()), 6);
                        }
                        int m = 0, n = 0;
                        //Hủy tại chỗThu gom

                        //Đã xử lýChưa xử lý
                        if (dr["Tinhtrang"].ToString() == Constants.TINHTRANG_DAXULY)
                        {
                            m = 1;
                        }
                        if (dr["Tinhtrang"].ToString() == Constants.TINHTRANG_CHUAXULY)
                        {
                            m = 2;
                        }
                        if (dr["PPXuLy"].ToString() == Constants.PPXULY_HUYTAICHO)
                        {
                            n = 1;
                        }
                        if (dr["PPXuLy"].ToString() == Constants.PPXULY_THUGOM)
                        {
                            n = 2;
                        }
                        if (long.TryParse(dr["Loai"].ToString().Split('.')[0], out long Loai))
                        {
                            result_report_lstSubTable B = new result_report_lstSubTable
                            {
                                long1 = Loai,
                                string1 = dr["Kyhieu"].ToString(),
                                string2 = dr["ol_idST"].ToString(),
                                double1 = x,
                                double2 = y,
                                double3 = z,
                                double4 = t,
                                long3 = m,
                                long4 = n,
                            };

                            result_report_lstSubTablex.Add(B);
                        }
                        else
                        {
                            result_report_lstSubTable B = new result_report_lstSubTable
                            {
                                long1 = 0,
                                string1 = dr["Kyhieu"].ToString(),
                                string2 = dr["ol_idST"].ToString(),
                                double1 = x,
                                double2 = y,
                                double3 = z,
                                double4 = t,
                                long3 = m,
                                long4 = n,
                            };

                            result_report_lstSubTablex.Add(B);
                        }
                    };

                }

                foreach (DataRow dr in datatableProvince.Rows)
                {
                    //Chọn
                    //Thông tin tin cậy
                    //Kết quả ĐTPKT
                    //Các tổ chức khác
                    int a = 0, b = 0, c = 0;
                    if (dr["NguonBangChung"].ToString() == "Thông tin tin cậy")
                    {
                        a = 1;//Nguồn bằng chứng khảo sát
                    }
                    if (dr["NguonBangChung"].ToString() == "Kết quả ĐTPKT")
                    {
                        a = 2;//Nguồn bằng chứng khảo sát
                    }
                    if (dr["NguonBangChung"].ToString() == "Các tổ chức khác")
                    {
                        a = 3;//Nguồn bằng chứng khảo sát
                    }
                    if (dr["isactive"].ToString() == "Hoàn thành")
                    {
                        b = 1;
                    }
                    if (dr["isactive"].ToString() == "Tạm dừng")
                    {
                        b = 2;
                    }
                    // CaoTrung bìnhThấp
                    if (dr["MucDoONhiem"].ToString() == "Cao")
                    {
                        c = 1;
                    }
                    if (dr["MucDoONhiem"].ToString() == "Trung bình")
                    {
                        c = 2;
                    }
                    if (dr["MucDoONhiem"].ToString() == "Thấp")
                    {
                        c = 3;
                    }
                    string hst = Convert.ToDateTime(dr["timeBatdau"]).ToString("dd/MM/yyyy");
                    string het = Convert.ToDateTime(dr["timeKetthuc"]).ToString("dd/MM/yyyy");
                    List<ResultReportAreaDTO> Lst = new List<ResultReportAreaDTO>();
                    Lst.Add(new ResultReportAreaDTO()
                    {
                        cecm_program_area_code = dr["cecm_program_area_code"].ToString(),
                        Thon = dr["Thon"].ToString(),
                        commune_id = long.TryParse(dr["Xa_id"].ToString(), out long Xa_id) ? Xa_id : -1,
                        district_id = long.TryParse(dr["Huyen_id"].ToString(), out long Huyen_id) ? Huyen_id : -1,
                        province_id = long.TryParse(dr["Tinh_id"].ToString(), out long Tinh_id) ? Tinh_id : -1,
                        communeidST = dr["Xacode"].ToString(),
                        districtidST = dr["Huyencode"].ToString(),
                        provinceidST = dr["Tinhcode"].ToString(),
                        cecm_program_idST = dr["Duan"].ToString(),
                        commune_code = dr["Xa"].ToString(),
                        district_code = dr["Huyen"].ToString(),
                        province_code = dr["Tinh"].ToString(),
                        cecm_program_code = dr["Duancode"].ToString(),
                        package_id = 1,
                        misson_code = dr["MaNV"].ToString(), //mã nhiệm vụ
                        start_date = hst,

                        end_date = het,
                        area_square = long.Parse(dr["DientichKS"].ToString()), //Diện tích kv 
                        evidence_source = a,  //Nguồn bằng chứng khảo sát
                        proof_count = int.Parse(dr["SLBangChungYeuCau"].ToString()),  //so bang chung
                        mission_status = b, //ttrang
                        result_doubt_survey = int.Parse(dr["SLDaKS"].ToString()),//so nghi ngo
                        result_survey_square = long.Parse(dr["DTDaKS"].ToString()),//Diện tích đã thực hiện khảo sát
                        result_survey_diff = long.Parse(dr["DTKSKhacYeuCau"].ToString()),//Diện tích khảo sát khác với yêu cầu
                        result_square_tested = long.Parse(dr["DTDaKTCL"].ToString()), //Diện tích đã kiểm tra chất lượng
                        result_square_remake = long.Parse(dr["DTPhaiLamLai"].ToString()), //DT làm lại
                        result_red = int.Parse(dr["Odo"].ToString()), //So o do
                        result_signal_survey = int.Parse(dr["TinHieu"].ToString()),//tin hieu
                        result_yellow = int.Parse(dr["Ovang"].ToString()), //So o vang
                        //result_blue = int.Parse(dr["Oxanhdatroi"].ToString()),//xanh troi
                        //result_white = int.Parse(dr["Otrang"].ToString()), //So o trang 
                        result_gray = int.Parse(dr["Oxam"].ToString()),//xam
                        result_not_polluted = int.Parse(dr["Oxanhla"].ToString()), //số ô không ô nhiễm
                        result_color = int.Parse(dr["TongSoOKS"].ToString()),//tong o

                        result_count_color_cbma = int.Parse(dr["SoOONhiem"].ToString()), //KD o nhiem

                        result_count_cbma = int.Parse(dr["SoKVONhiem"].ToString()),//so o trong kv nhiem
                        result_square_cha = long.Parse(dr["DTKhuONhiem"].ToString()),//dt nhiem
                        polluted_level = c, // mucdo
                        progress = int.Parse(dr["TienDoTH"].ToString()),//tien do
                        dept_idST = dr["Doiso"].ToString(),
                        captain_idST = dr["Doitruong"].ToString(),
                        site_commander = dr["ChiHuyCT"].ToString(),
                        countDate = int.Parse(dr["TimeTongSo"].ToString()),
                        area_map_id = 1,
                        result_report_lstSubTable = result_report_lstSubTablex,
                        //Mới
                        MaBaocaongay = dr["MaBaocaongay"].ToString(),
                        Ngaytao = Convert.ToDateTime(dr["Ngaytao"]).ToString("dd/MM/yyyy"),
                        DVKScode = dr["DVKScode"].ToString(),
                        result_brown = int.TryParse(dr["Oxanhdatroi"].ToString(), out int Onau) ? Onau : 0,
                        DTKhongONhiem = double.TryParse(dr["DTKhongONhiem"].ToString(), out double DTKhongONhiem) ? DTKhongONhiem : 0,
                        NguoiVe = dr["NguoiVe"].ToString(),
                        TGVe = Convert.ToDateTime(dr["TGVe"]).ToString("dd/MM/yyyy"),
                        GiamSat = dr["GiamSat"].ToString(),
                        GiamSat_idST = dr["GiamSat_idST"].ToString(),
                        ChiHuyCT_idST = dr["ChiHuyCT_idST"].ToString(),
                    });;
                    ResultReportArea LstDaily = new ResultReportArea
                    {
                        ResultReportAreaDTO = Lst,
                    };
                    //string json = JsonSerializer.Serialize(Lst);
                    //File.WriteAllText(@"E:\LstResult.json", json);

                    string json = JsonConvert.SerializeObject(LstDaily, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileTxt();
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
                    //MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
                }

            }
            //xoa
            if (e.ColumnIndex == DoiRPBMXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_report_survey_area WHERE cecm_report_survey_area.id = " + id_kqks, cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                }
                LoadData(TxtTImkiem.Text);
            }
            if(e.ColumnIndex == cotXuatExcel.Index)
            {
                try
                {


                    string pathFile = "";
                    saveFileDialog1.FileName = "KSKV_" + Guid.NewGuid().ToString();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        pathFile = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                    string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_KS1KV.xls";
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
                            //FileStream cfFileStream = new FileStream(sourceFile, FileMode.Open);
                            FileStream cfFileStream = File.OpenRead(sourceFile);
                            workbook = excelEngine.Excel.Workbooks.Open(cfFileStream);

                            //The first worksheet in the workbook is accessed.
                            IWorksheet worksheet = workbook.Worksheets[0];

                            //Create Template Marker processor.
                            //Apply the marker to export data from datatable to worksheet.
                            ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();
                            //marker.AddVariable("SalesList", table);

                            SqlCommandBuilder sqlCommand1 = null;
                            SqlDataAdapter sqlAdapter = null;
                            DataTable datatable = new DataTable();
                            sqlAdapter = new SqlDataAdapter(string.Format(
                                "SELECT " +
                                "tbl.*, " +
                                "case when giamsat.nameId is null then tbl.GiamSat else giamsat.nameId end as TenGiamSat, " +
                                "case when chihuy.nameId is null then tbl.ChiHuyCT else chihuy.nameId end as TenChiHuy, " +
                                "TblTinh.Ten as 'TenTinh', " +
                                "TblHuyen.Ten as 'TenHuyen', " +
                                "TblXa.Ten as 'TenXa', " +
                                "d.name as tenDVKS " +
                                "from cecm_report_survey_area tbl " +
                                "left join cert_department d on d.id = tbl.DVKS_id " +
                                "left join cecm_provinces as TblTinh on tbl.Tinh_id = TblTinh.id " +
                                "left join cecm_provinces as TblHuyen on tbl.Huyen_id = TblHuyen.id " +
                                "left join cecm_provinces as TblXa on tbl.Xa_id = TblXa.id " +
                                "left join Cecm_ProgramStaff giamsat on tbl.GiamSat_id = giamsat.id " +
                                "left join Cecm_ProgramStaff chihuy on tbl.ChiHuyCT_id = chihuy.id " +
                                "where tbl.id = {0}", id_kqks), cn);
                            sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                            sqlAdapter.Fill(datatable);
                            if (datatable.Rows.Count != 0)
                            {
                                marker.AddVariable("obj", datatable);
                                DataRow dr = datatable.Rows[0];
                                marker.AddVariable("MaBaocaongay_long", "Số: " + dr["MaBaocaongay"].ToString() + "/BC-HTKS");
                                DateTime now = DateTime.Now;
                                marker.AddVariable("Ngaybaocao", dr["TenTinh"].ToString() + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                                marker.AddVariable("DiadiemSodo", dr["TenTinh"].ToString() + "/" + dr["TenHuyen"].ToString() + "/" + dr["TenXa"].ToString());
                                if (DateTime.TryParse(dr["Ngaytao"].ToString(), out DateTime Ngaytao))
                                {
                                    marker.AddVariable("Ngaytao", Ngaytao.Day + "/" + Ngaytao.Month + "/" + Ngaytao.Year);
                                }
                                else
                                {
                                    marker.AddVariable("Ngaytao", "");
                                }
                                if (DateTime.TryParse(dr["TGVe"].ToString(), out DateTime TGVe))
                                {
                                    marker.AddVariable("TGVe", TGVe.Day + "/" + TGVe.Month + "/" + TGVe.Year);
                                }
                                else
                                {
                                    marker.AddVariable("TGVe", "");
                                }
                                marker.AddVariable("Duan", "Dự án: " + dr["Duan"].ToString());
                                //marker.AddVariable("Doiso", string.IsNullOrEmpty(dr["Doiso"].ToString()) ? dr["Doingoai"].ToString() : dr["Doiso"].ToString());
                                //marker.AddVariable("Doitruong", string.IsNullOrEmpty(dr["Doitruong"].ToString()) ? dr["Doitruongngoai"].ToString() : dr["Doitruong"].ToString());
                                marker.AddVariable("Doiso", dr["Doiso"].ToString());
                                marker.AddVariable("Doitruong", dr["Doitruong"].ToString());
                                if (DateTime.TryParse(dr["timeBatdau"].ToString(), out DateTime timeBatdau))
                                {
                                    marker.AddVariable("timeBatdau", timeBatdau.Day + "/" + timeBatdau.Month + "/" + timeBatdau.Year);
                                }
                                else
                                {
                                    marker.AddVariable("timeBatdau", "");
                                }
                                if (DateTime.TryParse(dr["timeKetthuc"].ToString(), out DateTime timeKetthuc))
                                {
                                    marker.AddVariable("timeKetthuc", timeKetthuc.Day + "/" + timeKetthuc.Month + "/" + timeKetthuc.Year);
                                }
                                else
                                {
                                    marker.AddVariable("timeKetthuc", "");
                                }
                                bool error = false;
                                byte[] image = new byte[0];
                                try
                                {
                                    image = File.ReadAllBytes(dr["PhotoFile"].ToString());
                                }
                                catch (Exception)
                                {
                                    error = true;
                                }
                                if (error)
                                {
                                    marker.AddVariable("image", "");
                                }
                                else
                                {
                                    marker.AddVariable("image", image);
                                }

                                //if (DateTime.TryParse(dr["timeDV"].ToString(), out DateTime timeDV))
                                //{
                                //    marker.AddVariable("timeDV", timeDV.Day + "/" + timeDV.Month + "/" + timeDV.Year);
                                //}
                                //else
                                //{
                                //    marker.AddVariable("timeDV", "");
                                //}
                                //marker.AddVariable("LidoDV", dr["LidoDV"].ToString());
                                //marker.AddVariable("Thon", dr["Thon"].ToString());
                                //marker.AddVariable("Xa", dr["Xa"].ToString());
                                //marker.AddVariable("Huyen", dr["Huyen"].ToString());
                                //marker.AddVariable("Tinh", dr["Tinh"].ToString());

                                //marker.AddVariable("Oxanhla", dr["Oxanhla"].ToString());
                                //marker.AddVariable("Odo", dr["Odo"].ToString());
                                //marker.AddVariable("Oxam", dr["Oxam"].ToString());
                                //marker.AddVariable("TongoDs", dr["TongoDs"].ToString());
                                //marker.AddVariable("TongoKs", dr["TongoKs"].ToString());
                                //marker.AddVariable("DientichKS", dr["DientichKS"].ToString());

                                //System.Data.DataTable tableKQKS = new System.Data.DataTable();
                                //SqlCommandBuilder sqlCommand2 = null;
                                //SqlDataAdapter sqlAdapter2 = null;
                                //sqlAdapter2 = new SqlDataAdapter(string.Format(
                                //    "Select tbl.*, ol.o_id as ol_idST " +
                                //    "FROM cecm_reportdaily_KQKS tbl " +
                                //    "left join OLuoi ol on tbl.ol_id = ol.gid " +
                                //    "WHERE tbl.cecm_reportdaily_id = {0}"
                                //    , id_kqks), cn);
                                //sqlCommand2 = new SqlCommandBuilder(sqlAdapter2);
                                //sqlAdapter2.Fill(tableKQKS);
                                //marker.AddVariable("KQKS", tableKQKS);

                                SqlCommandBuilder sqlCommand3 = null;
                                SqlDataAdapter sqlAdapter3 = null;
                                System.Data.DataTable tableBMVN = new System.Data.DataTable();
                                string sql = "Select tbl.*, ol.o_id as ol_idST " +
                                    "FROM cecm_report_survey_area_BMVN tbl " +
                                    "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                                    "WHERE tbl.cecm_report_survey_area_id = " + id_kqks;
                                sqlAdapter3 = new SqlDataAdapter(sql, cn);
                                sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                                sqlAdapter3.Fill(tableBMVN);
                                marker.AddVariable("BMVN", tableBMVN);

                                marker.AddVariable("BMVNCount", "Tổng số vật nổ tìm thấy: " + tableBMVN.Rows.Count);
                                int daXuLy = 0;
                                int huyTaiCho = 0;
                                int thuGom = 0;
                                foreach (DataRow drBMVN in tableBMVN.Rows)
                                {
                                    if (drBMVN["Tinhtrang"].ToString() == "Đã xử lý")
                                    {
                                        daXuLy++;
                                        if (drBMVN["PPXuLy"].ToString() == "Hủy tại chỗ")
                                        {
                                            huyTaiCho++;
                                        }
                                        if (drBMVN["PPXuLy"].ToString() == "Thu gom")
                                        {
                                            thuGom++;
                                        }
                                    }
                                    
                                }
                                marker.AddVariable("thongKeXuLy", string.Format("Đã xử lý {0}. Trong đó: Hủy tại chỗ {1}. Thu gom {2}", daXuLy, huyTaiCho, thuGom));
                                //marker.AddVariable("daXuLy", daXuLy);
                                //marker.AddVariable("huyTaiCho", huyTaiCho);
                                //marker.AddVariable("thuGom", thuGom);
                            }

                            //marker.AddVariable("str", "Nguyễn Minh Hiếu");
                            marker.ApplyMarkers(Syncfusion.XlsIO.UnknownVariableAction.Skip);

                            //Saving and closing the workbook
                            //workbook.ActiveSheetIndex = 0;
                            


                            workbook.SaveAs(pathFile);
                            //workbook.Worksheets.Remove(workbook.Worksheets.Count - 1);
                            //workbook.Worksheets[0].Activate();
                            //workbook.Save();

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
        }
        private void BtnThemmoi_Click(object sender, EventArgs e)
        {
            FrmThemmoiBaoCaoKQKV frm = new FrmThemmoiBaoCaoKQKV();
            frm.Text = "THÊM MỚI BÁO CÁO KẾT QUẢ KHẢO SÁT KHU VỰC";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                FrmBaocaoKQ_Load(sender, e);
            }
        }
        private void FrmBaocaoKQ_Load(object sender, EventArgs e)
        {
            LoadData(TxtTImkiem.Text);
        }

        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            LoadData(TxtTImkiem.Text);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtTImkiem.Text = "";
            timeNgaybatdau.Checked = false;
            timeNgayketthuc.Checked = false;
            LoadData(TxtTImkiem.Text);
        }

        private void timeNgaybatdau_ValueChanged(object sender, EventArgs e)
        {
            if (timeNgaybatdau.Value > timeNgayketthuc.Value)
            {
                timeNgaybatdau.Value = timeNgayketthuc.Value;
            }
        }

        private void timeNgayketthuc_ValueChanged(object sender, EventArgs e)
        {
            if (timeNgaybatdau.Value > timeNgayketthuc.Value)
            {
                timeNgayketthuc.Value = timeNgaybatdau.Value;
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
            //    return;
            this.Close();
        }
        private void LoadData(string name)
        {
            try
            {
                //date1 = date1.Split('/')[1] + "-" + date1.Split('/')[0] + "-" + date1.Split('/')[2];
                //date2 = date2.Split('/')[1] + "-" + date2.Split('/')[0] + "-" + date2.Split('/')[2];

                DGV_BaoCaoKV.Rows.Clear();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                DataSet dataset = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql = "Select " +
                    "cecm_report_survey_area.id, " +
                    "cecm_report_survey_area.Duan, " +
                    "cecm_report_survey_area.Hopphan as 'Hợp phần', " +
                    "Tinh.Ten as 'Tỉnh', " +
                    "Huyen.Ten as 'Huyện', " +
                    "Xa.Ten as 'Xã', " +
                    "cecm_report_survey_area.Thon as 'Thôn', " +
                    "CONCAT(FORMAT(timeBatdau, 'dd/MM/yyyy'), ' - ', FORMAT(timeKetthuc, 'dd/MM/yyyy')) as 'Thời gian' " +
                    "from cecm_report_survey_area " +
                    "left join cecm_provinces as Tinh on cecm_report_survey_area.Tinh_id = Tinh.id " +
                    "left join cecm_provinces as Huyen on cecm_report_survey_area.Huyen_id = Huyen.id " +
                    "left join cecm_provinces as Xa on cecm_report_survey_area.Xa_id = Xa.id " +
                    "where 1 = 1 ";
                    //"and timeBatdau >'" + date1 + "' and timeKetthuc <'" + date2 + "' ";
                if (!string.IsNullOrEmpty(name))
                {
                    sql += " AND LOWER(Duan) LIKE @programName ";
                }
                if (timeNgaybatdau.Checked)
                {
                    sql += " AND timeBatdau > @timeBatdau ";
                }
                if (timeNgayketthuc.Checked)
                {
                    sql += " AND timeKetthuc < @timeKetthuc ";
                }
                sql += "order by cecm_report_survey_area.id ";
                sqlAdapter = new SqlDataAdapter(sql, cn);
                if (!string.IsNullOrEmpty(name))
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "programName",
                        Value = "%" + name.ToLower() + "%",
                        SqlDbType = SqlDbType.NVarChar
                    });
                }
                if (timeNgaybatdau.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "timeBatdau",
                        Value = timeNgaybatdau.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }
                if (timeNgayketthuc.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "timeKetthuc",
                        Value = timeNgayketthuc.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                dataset = new DataSet();
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idDuAn = dr["id"].ToString();
                        var row1 = dr["Duan"].ToString();
                        var row2 = dr["Hợp phần"].ToString();
                        var row3 = dr["Tỉnh"].ToString();
                        var row4 = dr["Huyện"].ToString();
                        var row5 = dr["Xã"].ToString();
                        var row6 = dr["Thời gian"].ToString();


                        DGV_BaoCaoKV.Rows.Add(indexRow, row1, row2, row3, row4, row5, row6);
                        DGV_BaoCaoKV.Rows[indexRow - 1].Tag = idDuAn;

                        indexRow++;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }
    }
}
