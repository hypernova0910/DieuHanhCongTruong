using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class FrmBCKVON : Form
    {
        private SqlConnection cn = null;
        public int idVungDuan;
        public string MaKV;
        public string ipAddr = "", databaseName = "", userName = "";

        public FrmBCKVON()
        {
            cn = frmLoggin.sqlCon;
            InitializeComponent();
        }

        private void BtnThemmoi_Click(object sender, EventArgs e)
        {
            FrmThemmoiBCKVON frm = new FrmThemmoiBCKVON(0);
            frm.Text = "THÊM MỚI BÁO CÁO KHU VỰC KHẲNG ĐỊNH Ô NHIỄM";
            frm.ShowDialog();
            LoadData();
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

            if (e.ColumnIndex == DoiRPBMSua.Index)
            {
                FrmThemmoiBCKVON frm = new FrmThemmoiBCKVON(id_kqks);
                frm.Text = "CHỈNH SỬA BÁO CÁO KHU VỰC KHẲNG ĐỊNH Ô NHIỄM";
                frm.ShowDialog();
                LoadData();
            }
            //delete column
            if (e.ColumnIndex == Export.Index)
            {
                SqlCommandBuilder sqlCommand = null;
                SqlCommandBuilder sqlCommand2 = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(
                    "select " +
                    "cecm_programData.code as 'cecm_program_code', " +
                    "cecm_programData.name as 'TenDuan', " +
                    "VungDuan.position_lat, " +
                    "VungDuan.position_long," +
                    "VungDuan.polygongeomst as polygonGeomST, " +
                    "cecm_ReportPollutionArea.Tinh as Tinh_id, " +
                    "cecm_ReportPollutionArea.Huyen as Huyen_id, " +
                    "cecm_ReportPollutionArea.Xa as Xa_id, " +
                    "Tinh.Ten as 'TenTinh'," +
                    "Huyen.Ten as 'TenHuyen'," +
                    "Xa.Ten as 'TenXa'," +
                    "VungDuan.code as 'TenVung'," +
                    "VungDuan.code as 'CodeTenVung', " +
                    "giamsat.nameId as GiamSat_idST," +
                    "chihuy.nameId as ChiHuyCT_idST," +
                    "cert_command_person.name as 'TenDoiso'," +
                    "cert_command_person.master as 'TenDoitruong', " +
                    "d.code as DVKScode, " +
                    "d.id_web as dept_id_web, " +
                    "cecm_ReportPollutionArea.* " +
                    "from cecm_ReportPollutionArea " +
                    "left join cecm_programData on cecm_ReportPollutionArea.Duan = cecm_programData.id " +
                    "left join cecm_program_area_map as VungDuan on cecm_ReportPollutionArea.VungDuan = VungDuan.id " +
                    "left join cecm_provinces as Tinh on Tinh.id = cecm_ReportPollutionArea.Tinh " +
                    "left join cecm_provinces as Huyen on Huyen.id = cecm_ReportPollutionArea.Huyen " +
                    "left join cecm_provinces as Xa on Xa.id = cecm_ReportPollutionArea.Xa " +
                    "left join Cecm_ProgramStaff giamsat on cecm_ReportPollutionArea.GiamSat_id = giamsat.id " +
                    "left join Cecm_ProgramStaff chihuy on cecm_ReportPollutionArea.ChiHuyCT_id = chihuy.id " +
                    "left join cert_department d on d.id = cecm_ReportPollutionArea.DVKS_id " +
                    "left join cert_command_person on cecm_ReportPollutionArea.Tendoi = cert_command_person.id " +
                    "where cecm_ReportPollutionArea.id = " + id_kqks, cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    idVungDuan = int.Parse(dr["VungDuan"].ToString());
                    MaKV = dr["MaKV"].ToString();
                }

                foreach (DataRow dr in datatableProvince.Rows)
                {
                    idVungDuan = int.Parse(dr["VungDuan"].ToString());
                    List<lstChaBombDTO> lst_bombA = new List<lstChaBombDTO>();

                    //SqlDataAdapter sqlAdapterBom = new SqlDataAdapter("SELECT Cecm_VNTerrainMinePoint_CHA.*, Cecm_TerrainRectangle.code FROM Cecm_VNTerrainMinePoint_CHA left join Cecm_TerrainRectangle on Cecm_TerrainRectangle.id = Cecm_VNTerrainMinePoint_CHA.idRectangle left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint_CHA.programId where idRectangle != -1 and Cecm_VNTerrainMinePoint_CHA.programId = " + idVungDuan, cn);
                    //sqlCommand2 = new SqlCommandBuilder(sqlAdapterBom);
                    //System.Data.DataTable datatableBom = new System.Data.DataTable();
                    //sqlAdapterBom.Fill(datatableBom);
                    SqlCommandBuilder sqlCommand3 = null;
                    SqlDataAdapter sqlAdapter3 = null;
                    System.Data.DataTable tableBMVN = new System.Data.DataTable();
                    string sql = "Select tbl.*, ol.o_id as ol_idST " +
                        "FROM cecm_ReportPollutionArea_BMVN tbl " +
                        "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                        "WHERE tbl.cecm_ReportPollutionArea_id = " + id_kqks;
                    sqlAdapter3 = new SqlDataAdapter(sql, cn);
                    sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                    sqlAdapter3.Fill(tableBMVN);
                    if (tableBMVN.Rows.Count != 0)
                    {
                        foreach (DataRow dr1 in tableBMVN.Rows)
                        {
                            int PPXuLy = 0;
                            if (dr1["PPXuLy"].ToString() == Constants.PPXULY_HUYTAICHO)
                            {
                                PPXuLy = 1;
                            }
                            if (dr1["PPXuLy"].ToString() == Constants.PPXULY_THUGOM)
                            {
                                PPXuLy = 2;
                            }
                            int TinhTrang = 0;
                            if (dr1["Tinhtrang"].ToString() == Constants.TINHTRANG_DAXULY)
                            {
                                TinhTrang = 1;
                            }
                            if (dr1["Tinhtrang"].ToString() == Constants.TINHTRANG_CHUAXULY)
                            {
                                TinhTrang = 2;
                            }
                            lstChaBombDTO A1 = new lstChaBombDTO()
                            {
                                codeCha = MaKV,
                                bombType = long.TryParse(dr1["Loai"].ToString().Split('.')[0], out long Loai) ? Loai : 0,
                                codeO = dr1["ol_idST"].ToString(),
                                weight = long.TryParse(dr1["SL"].ToString(), out long SL) ? SL : 0,
                                statusBomb = TinhTrang,
                                theNote = dr1["Ghichu"].ToString(),
                                Kinhdo = double.TryParse(dr1["Kinhdo"].ToString(), out double Kinhdo) ? Kinhdo : 0,
                                Vido = double.TryParse(dr1["Vido"].ToString(), out double Vido) ? Vido : 0,
                                Kyhieu = dr1["Kyhieu"].ToString(),
                                Deep = double.TryParse(dr1["Deep"].ToString(), out double Deep) ? Deep : 0,
                                Kichthuoc = double.TryParse(dr1["Kichthuoc"].ToString(), out double Kichthuoc) ? Kichthuoc : 0,
                                PPXuLy = PPXuLy,
                                PPXuLyST = dr1["PPXuLy"].ToString()
                                //public String codeCha; //mã cha
                                //public long bombType; //id loại bom
                                //public String st_ref2; //Ô
                                //public long weight;//So luongư
                                //public long statusBomb;//id tình trạng
                                //public string theNote;//ghi chú
                            };
                            lst_bombA.Add(A1);
                        };
                    }

                    List<CbChaDTO> CbChaDTO = new List<CbChaDTO>();

                    string purposeTmp;
                    if (dr["MucdichSD"].ToString() == null || dr["MucdichSD"].ToString() == "")
                    {
                        purposeTmp = "0.0";
                    }
                    else
                    {
                        purposeTmp = dr["MucdichSD"].ToString();
                    }
                    string TinhtrangKV;
                    if (dr["TinhtrangKV"].ToString() == null || dr["TinhtrangKV"].ToString() == "")
                    {
                        TinhtrangKV = "0.0";
                    }
                    else
                    {
                        TinhtrangKV = dr["TinhtrangKV"].ToString();
                    }
                    string UutienRP;
                    if (dr["UutienRP"].ToString() == null || dr["UutienRP"].ToString() == "")
                    {
                        UutienRP = "0.0";
                    }
                    else
                    {
                        UutienRP = dr["UutienRP"].ToString();
                    }
                    string Nguoihuong;
                    if (dr["Nguoihuong"].ToString() == null || dr["Nguoihuong"].ToString() == "")
                    {
                        Nguoihuong = "0.0";
                    }
                    else
                    {
                        Nguoihuong = dr["Nguoihuong"].ToString();
                    }

                    CbChaDTO A = new CbChaDTO();
                    A.code = dr["code"].ToString();
                    A.note = dr["Ghichu"].ToString();
                    A.reason = dr["Lido"].ToString();
                    A.commune_id = long.Parse(dr["Xa_id"].ToString());
                    A.district_id = long.Parse(dr["Huyen_id"].ToString());
                    A.province_id = long.Parse(dr["Tinh_id"].ToString());
                    A.commune_idST = dr["TenXa"].ToString();
                    A.district_idST = dr["TenHuyen"].ToString();
                    A.province_idST = dr["TenTinh"].ToString();
                    A.cecm_program_idST = dr["TenDuan"].ToString();
                    A.commune_code = dr["Xa"].ToString();
                    A.district_code = dr["Huyen"].ToString();
                    A.province_code = dr["Tinh"].ToString();
                    A.village = dr["Thon"].ToString();
                    A.cecm_program_code = dr["cecm_program_code"].ToString();
                    A.date_createST = Convert.ToDateTime(dr["Ngaytao"]).ToString("dd/MM/yyyy");
                    A.component_id = 1;
                    A.cecm_program_packageid = 0;
                    A.cecm_plan_program_id = 0;
                    A.cecm_program_area_idST = dr["TenVung"].ToString();
                    A.cecm_program_area_code = dr["CodeTenVung"].ToString();
                    A.dept_code = dr["DVKScode"].ToString();
                    A.dept_id = long.TryParse(dr["dept_id_web"].ToString(), out long DVKScode) ? DVKScode : -1;
                    A.codeCHA = dr["MaKV"].ToString();
                    A.deptid = 0;
                    A.headerName = "";
                    A.mission_code = dr["MaNV"].ToString();
                    A.length = double.Parse(dr["Dai"].ToString().Replace('.', ','));
                    A.width = double.Parse(dr["Rong"].ToString().Replace('.', ','));
                    A.surface = double.Parse(dr["DientichKV"].ToString().Replace('.', ','));
                    A.status = long.Parse(TinhtrangKV.Split('.')[0]);
                    A.priority = long.Parse(UutienRP.Split('.')[0]);
                    A.date_startST = Convert.ToDateTime(dr["NgayBD"]).ToString("dd/MM/yyyy");
                    A.date_endST = Convert.ToDateTime(dr["NgayKT"]).ToString("dd/MM/yyyy");
                    A.purpose = long.Parse(purposeTmp.Split('.')[0]);
                    A.purposeOther = dr["MucdichSDkhac"].ToString();
                    A.person_benefit = long.Parse(Nguoihuong.Split('.')[0]);
                    A.lst_plantType = dr["LoaiThucvat"].ToString().Replace('+', ',');
                    A.lst_plantLevel = dr["DophuTV"].ToString().Replace('+', ',');
                    A.lst_plantDevice = dr["PTphatquang"].ToString().Replace('+', ',');
                    A.lst_soilType = dr["Loaidat"].ToString().Replace('+', ',');
                    A.lst_geologicalType = dr["Loaihinhdiachat"].ToString().Replace('+', ',');
                    A.lst_areaType = dr["LoaihinhKV"].ToString().Replace('+', ',');
                    A.lst_areaDevice = dr["Loaixe"].ToString().Replace('+', ',');
                    A.lst_topographic = dr["Diahinh"].ToString().Replace('+', ',');
                    A.lst_month = dr["Thangkotiepcan"].ToString().Replace('+', ',');
                    A.latValue = double.TryParse(dr["position_lat"].ToString(), out double position_lat) ? position_lat : 0;
                    A.longValue = double.TryParse(dr["position_long"].ToString(), out double position_long) ? position_long : 0;
                    A.supervisor_idST = dr["GiamSat_idST"].ToString();
                    A.supervisor_other = dr["GiamSat_other"].ToString();
                    A.head_idST = dr["ChiHuyCT_idST"].ToString();
                    A.head_other = dr["ChiHuyCT_other"].ToString();
                    A.group_idST = dr["TenDoiso"].ToString();
                    A.group_other = dr["Doingoai"].ToString();
                    A.captain_idST = dr["TenDoitruong"].ToString();
                    A.captain_other = dr["Doitruongngoai"].ToString();
                    A.polygonGeomST = "";
                    A.meridian = 0;
                    A.pZone = 0;
                    A.lstChaBombDTO = lst_bombA;
                    A.percentage = long.TryParse(dr["Phantram_id"].ToString(), out long Phantram_id) ? Phantram_id : -1;

                    CbChaDTO.Add(A);
                    ClassCbChaDTO LstDaily = new ClassCbChaDTO
                    {
                        CbChaDTO = CbChaDTO,
                    };
                    //string json = JsonSerializer.Serialize(Lst);
                    //File.WriteAllText(@"E:\LstResult.json", json);

                    string json = JsonConvert.SerializeObject(A, Formatting.Indented);

                    var saveLocation = AppUtils.SaveFileTxt();
                    //if (string.IsNullOrEmpty(saveLocation) == false)
                    //    System.IO.File.WriteAllText(saveLocation, json);
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
                    lst_bombA.Clear();
                    LstDaily = null;
                }
            }
            if (e.ColumnIndex == DoiRPBMXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_ReportPollutionArea WHERE cecm_ReportPollutionArea.id = " + id_kqks, cn);
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
                LoadData();
            }
            if (e.ColumnIndex == cotXuatExcel.Index)
            {
                try
                {


                    string pathFile = "";
                    saveFileDialog1.FileName = "KĐON_" + Guid.NewGuid().ToString();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        pathFile = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                    string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_KĐON.xls";
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
                                "SELECT cecm_ReportPollutionArea.*," +
                                "cecm_program2.code as 'TenVungDuan'," +
                                "cecm_program2.id as 'VungDuanid', " +
                                "cecm_program1.name as 'TenDuan'," +
                                "cecm_program1.id as 'Duanid'," +
                                "d.name as tenDVKS, " +
                                //"[Hopphan]," +
                                "Tinh.Ten as 'TenTinh'," +
                                "Tinh.id as 'Tinhid'," +
                                "Huyen.Ten as 'TenHuyen'," +
                                "Huyen.id as 'Huyenid'," +
                                "Xa.Ten as 'TenXa'," +
                                "Xa.id as 'Xaid'," +
                                "case when giamsat.nameId is null then cecm_ReportPollutionArea.GiamSat_other else giamsat.nameId end as TenGiamSat, " +
                                "case when chihuy.nameId is null then cecm_ReportPollutionArea.ChiHuyCT_other else chihuy.nameId end as TenChiHuy, " +
                                "cert_command_person.name as 'TenDoiso'," +
                                "cert_command_person.master as 'TenDoitruong' " +
                                "FROM cecm_ReportPollutionArea " +
                                "left join cert_department d on d.id = cecm_ReportPollutionArea.DVKS_id " +
                                "left join cecm_programData as cecm_program1  on cecm_program1.id = cecm_ReportPollutionArea.Duan " +
                                "left join cecm_program_area_map as cecm_program2 on cecm_program2.id = cecm_ReportPollutionArea.VungDuan " +
                                "left join cecm_provinces as Tinh on cecm_ReportPollutionArea.Tinh = Tinh.id " +
                                "left join cecm_provinces as Huyen on cecm_ReportPollutionArea.Huyen = Huyen.id " +
                                "left join cecm_provinces as Xa on cecm_ReportPollutionArea.Xa = Xa.id " +
                                "left join cert_command_person on cecm_ReportPollutionArea.Tendoi = cert_command_person.id " +
                                "left join Cecm_ProgramStaff giamsat on cecm_ReportPollutionArea.GiamSat_id = giamsat.id " +
                                "left join Cecm_ProgramStaff chihuy on cecm_ReportPollutionArea.ChiHuyCT_id = chihuy.id " +
                                "where cecm_ReportPollutionArea.id = {0}", id_kqks), cn);
                            sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                            sqlAdapter.Fill(datatable);
                            if (datatable.Rows.Count != 0)
                            {
                                marker.AddVariable("obj", datatable);
                                DataRow dr = datatable.Rows[0];
                                marker.AddVariable("MaBaocao_long", "Số: " + dr["code"].ToString());
                                DateTime.TryParse(dr["Ngaytao"].ToString(), out DateTime now);
                                marker.AddVariable("Ngaybaocao", dr["TenTinh"].ToString() + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                                marker.AddVariable("TenDuan", "Dự án: " + dr["TenDuan"].ToString());
                                marker.AddVariable("TenDoiso", string.IsNullOrEmpty(dr["TenDoiso"].ToString()) ? dr["Doingoai"].ToString() : dr["TenDoiso"].ToString());
                                marker.AddVariable("TenDoitruong", string.IsNullOrEmpty(dr["TenDoitruong"].ToString()) ? dr["Doitruongngoai"].ToString() : dr["TenDoitruong"].ToString());
                                if (DateTime.TryParse(dr["NgayBD"].ToString(), out DateTime timeBatdau))
                                {
                                    marker.AddVariable("NgayBD", timeBatdau.Day + "/" + timeBatdau.Month + "/" + timeBatdau.Year);
                                }
                                else
                                {
                                    marker.AddVariable("NgayBD", "");
                                }
                                if (DateTime.TryParse(dr["NgayKT"].ToString(), out DateTime timeKetthuc))
                                {
                                    marker.AddVariable("NgayKT", timeKetthuc.Day + "/" + timeKetthuc.Month + "/" + timeKetthuc.Year);
                                }
                                else
                                {
                                    marker.AddVariable("NgayKT", "");
                                }
                                string[] loaiThucVat = dr["LoaiThucvat"].ToString().Split('+');
                                string loaiThucVat_Co = "";
                                string loaiThucVat_BuiRam = "";
                                string loaiThucVat_TreTruc = "";
                                string loaiThucVat_RungCay = "";
                                string loaiThucVat_KhongCoThucVat = "";
                                string loaiThucVat_Khac = "";
                                foreach(string s in loaiThucVat)
                                {
                                    if(s == "1")
                                    {
                                        loaiThucVat_Co = "✓";
                                    }
                                    else if(s == "2")
                                    {
                                        loaiThucVat_BuiRam = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        loaiThucVat_TreTruc = "✓";
                                    }
                                    else if (s == "4")
                                    {
                                        loaiThucVat_RungCay = "✓";
                                    }
                                    else if (s == "5")
                                    {
                                        loaiThucVat_KhongCoThucVat = "✓";
                                    }
                                    else if (s == "6")
                                    {
                                        loaiThucVat_Khac = "✓";
                                    }
                                }
                                marker.AddVariable("loaiThucVat_Co", loaiThucVat_Co);
                                marker.AddVariable("loaiThucVat_BuiRam", loaiThucVat_BuiRam);
                                marker.AddVariable("loaiThucVat_TreTruc", loaiThucVat_TreTruc);
                                marker.AddVariable("loaiThucVat_RungCay", loaiThucVat_RungCay);
                                marker.AddVariable("loaiThucVat_KhongCoThucVat", loaiThucVat_KhongCoThucVat);
                                marker.AddVariable("loaiThucVat_Khac", loaiThucVat_Khac);

                                string[] doPhuThucVat = dr["DophuTV"].ToString().Split('+');
                                string doPhuThucVat_Day = "";
                                string doPhuThucVat_Mong = "";
                                string doPhuThucVat_Troc = "";
                                foreach (string s in doPhuThucVat)
                                {
                                    if (s == "1")
                                    {
                                        doPhuThucVat_Day = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        doPhuThucVat_Mong = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        doPhuThucVat_Troc = "✓";
                                    }
                                    
                                }
                                marker.AddVariable("doPhuThucVat_Day", doPhuThucVat_Day);
                                marker.AddVariable("doPhuThucVat_Mong", doPhuThucVat_Mong);
                                marker.AddVariable("doPhuThucVat_Troc", doPhuThucVat_Troc);

                                string[] PTphatquang = dr["PTphatquang"].ToString().Split('+');
                                string PTphatquang_ThuCong = "";
                                string PTphatquang_CoGioi = "";
                                string PTphatquang_MayCatCo = "";
                                string PTphatquang_KetHop = "";
                                foreach (string s in PTphatquang)
                                {
                                    if (s == "1")
                                    {
                                        PTphatquang_ThuCong = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        PTphatquang_CoGioi = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        PTphatquang_MayCatCo = "✓";
                                    }
                                    else if (s == "4")
                                    {
                                        PTphatquang_KetHop = "✓";
                                    }
                                    
                                }
                                marker.AddVariable("PTphatquang_ThuCong", PTphatquang_ThuCong);
                                marker.AddVariable("PTphatquang_CoGioi", PTphatquang_CoGioi);
                                marker.AddVariable("PTphatquang_MayCatCo", PTphatquang_MayCatCo);
                                marker.AddVariable("PTphatquang_KetHop", PTphatquang_KetHop);

                                string[] Loaihinhdiachat = dr["Loaihinhdiachat"].ToString().Split('+');
                                string Loaihinhdiachat_Mem = "";
                                string Loaihinhdiachat_TrungBinh = "";
                                string Loaihinhdiachat_Cung = "";
                                string Loaihinhdiachat_AmUout = "";
                                foreach (string s in Loaihinhdiachat)
                                {
                                    if (s == "1")
                                    {
                                        Loaihinhdiachat_Mem = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        Loaihinhdiachat_TrungBinh = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        Loaihinhdiachat_Cung = "✓";
                                    }
                                    else if (s == "4")
                                    {
                                        Loaihinhdiachat_AmUout = "✓";
                                    }

                                }
                                marker.AddVariable("Loaihinhdiachat_Mem", Loaihinhdiachat_Mem);
                                marker.AddVariable("Loaihinhdiachat_TrungBinh", Loaihinhdiachat_TrungBinh);
                                marker.AddVariable("Loaihinhdiachat_Cung", Loaihinhdiachat_Cung);
                                marker.AddVariable("Loaihinhdiachat_AmUout", Loaihinhdiachat_AmUout);

                                string[] Loaidat = dr["Loaidat"].ToString().Split('+');
                                string Loaidat_Cat = "";
                                string Loaidat_Do = "";
                                string Loaidat_GanGa = "";
                                string Loaidat_Thit = "";
                                string Loaidat_Set = "";
                                string Loaidat_Soi = "";
                                string Loaidat_Da = "";
                                string Loaidat_DamLay = "";
                                string Loaidat_Khac = "";
                                foreach (string s in Loaidat)
                                {
                                    if (s == "1")
                                    {
                                        Loaidat_Cat = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        Loaidat_Do = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        Loaidat_GanGa = "✓";
                                    }
                                    else if (s == "4")
                                    {
                                        Loaidat_Thit = "✓";
                                    }
                                    else if (s == "5")
                                    {
                                        Loaidat_Set = "✓";
                                    }
                                    else if (s == "6")
                                    {
                                        Loaidat_Soi = "✓";
                                    }
                                    else if (s == "7")
                                    {
                                        Loaidat_Da = "✓";
                                    }
                                    else if (s == "8")
                                    {
                                        Loaidat_DamLay = "✓";
                                    }
                                    else if (s == "9")
                                    {
                                        Loaidat_Khac = "✓";
                                    }
                                }
                                marker.AddVariable("Loaidat_Cat", Loaidat_Cat);
                                marker.AddVariable("Loaidat_Do", Loaidat_Do);
                                marker.AddVariable("Loaidat_GanGa", Loaidat_GanGa);
                                marker.AddVariable("Loaidat_Thit", Loaidat_Thit);
                                marker.AddVariable("Loaidat_Set", Loaidat_Set);
                                marker.AddVariable("Loaidat_Soi", Loaidat_Soi);
                                marker.AddVariable("Loaidat_Da", Loaidat_Da);
                                marker.AddVariable("Loaidat_DamLay", Loaidat_DamLay);
                                marker.AddVariable("Loaidat_Khac", Loaidat_Khac);

                                string[] LoaihinhKV = dr["LoaihinhKV"].ToString().Split('+');
                                string LoaihinhKV_DatHoang = "";
                                string LoaihinhKV_VenBien = "";
                                string LoaihinhKV_CanCuQSCu = "";
                                string LoaihinhKV_DamLay = "";
                                string LoaihinhKV_BoSong = "";
                                string LoaihinhKV_KhuDanCu = "";
                                string LoaihinhKV_DongRuong = "";
                                string LoaihinhKV_DuongLon = "";
                                string LoaihinhKV_DoThi = "";
                                string LoaihinhKV_DoiNui = "";
                                string LoaihinhKV_BenDuong = "";
                                string LoaihinhKV_TruSoHanhChinh = "";
                                string LoaihinhKV_Rung = "";
                                string LoaihinhKV_DuongMon = "";
                                string LoaihinhKV_Khac = "";
                                foreach (string s in LoaihinhKV)
                                {
                                    if (s == "1")
                                    {
                                        LoaihinhKV_DatHoang = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        LoaihinhKV_VenBien = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        LoaihinhKV_CanCuQSCu = "✓";
                                    }
                                    else if (s == "4")
                                    {
                                        LoaihinhKV_DamLay = "✓";
                                    }
                                    else if (s == "5")
                                    {
                                        LoaihinhKV_BoSong = "✓";
                                    }
                                    else if (s == "6")
                                    {
                                        LoaihinhKV_KhuDanCu = "✓";
                                    }
                                    else if (s == "7")
                                    {
                                        LoaihinhKV_DongRuong = "✓";
                                    }
                                    else if (s == "8")
                                    {
                                        LoaihinhKV_DuongLon = "✓";
                                    }
                                    else if (s == "9")
                                    {
                                        LoaihinhKV_DoThi = "✓";
                                    }
                                    else if (s == "10")
                                    {
                                        LoaihinhKV_DoiNui = "✓";
                                    }
                                    else if (s == "11")
                                    {
                                        LoaihinhKV_BenDuong = "✓";
                                    }
                                    else if (s == "12")
                                    {
                                        LoaihinhKV_TruSoHanhChinh = "✓";
                                    }
                                    else if (s == "13")
                                    {
                                        LoaihinhKV_Rung = "✓";
                                    }
                                    else if (s == "14")
                                    {
                                        LoaihinhKV_DuongMon = "✓";
                                    }
                                    else if (s == "15")
                                    {
                                        LoaihinhKV_Khac = "✓";
                                    }
                                }
                                marker.AddVariable("LoaihinhKV_DatHoang", LoaihinhKV_DatHoang);
                                marker.AddVariable("LoaihinhKV_VenBien", LoaihinhKV_VenBien);
                                marker.AddVariable("LoaihinhKV_CanCuQSCu", LoaihinhKV_CanCuQSCu);
                                marker.AddVariable("LoaihinhKV_DamLay", LoaihinhKV_DamLay);
                                marker.AddVariable("LoaihinhKV_BoSong", LoaihinhKV_BoSong);
                                marker.AddVariable("LoaihinhKV_KhuDanCu", LoaihinhKV_KhuDanCu);
                                marker.AddVariable("LoaihinhKV_DongRuong", LoaihinhKV_DongRuong);
                                marker.AddVariable("LoaihinhKV_DuongLon", LoaihinhKV_DuongLon);
                                marker.AddVariable("LoaihinhKV_DoThi", LoaihinhKV_DoThi);
                                marker.AddVariable("LoaihinhKV_DoiNui", LoaihinhKV_DoiNui);
                                marker.AddVariable("LoaihinhKV_BenDuong", LoaihinhKV_BenDuong);
                                marker.AddVariable("LoaihinhKV_TruSoHanhChinh", LoaihinhKV_TruSoHanhChinh);
                                marker.AddVariable("LoaihinhKV_Rung", LoaihinhKV_Rung);
                                marker.AddVariable("LoaihinhKV_DuongMon", LoaihinhKV_DuongMon);
                                marker.AddVariable("LoaihinhKV_Khac", LoaihinhKV_Khac);

                                string[] Loaixe = dr["Loaixe"].ToString().Split('+');
                                string Loaixe_MotCau = "";
                                string Loaixe_HaiCau = "";
                                string Loaixe_16Cho = "";
                                foreach (string s in Loaixe)
                                {
                                    if (s == "1")
                                    {
                                        Loaixe_MotCau = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        Loaixe_HaiCau = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        Loaixe_16Cho = "✓";
                                    }
                                }
                                marker.AddVariable("Loaixe_MotCau", Loaixe_MotCau);
                                marker.AddVariable("Loaixe_HaiCau", Loaixe_HaiCau);
                                marker.AddVariable("Loaixe_16Cho", Loaixe_16Cho);

                                string[] Diahinh = dr["Diahinh"].ToString().Split('+');
                                string Diahinh_Doc = "";
                                string Diahinh_HoiDoc = "";
                                string Diahinh_BangPhang = "";
                                foreach (string s in Diahinh)
                                {
                                    if (s == "1")
                                    {
                                        Diahinh_Doc = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        Diahinh_HoiDoc = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        Diahinh_BangPhang = "✓";
                                    }
                                }
                                marker.AddVariable("Diahinh_Doc", Diahinh_Doc);
                                marker.AddVariable("Diahinh_HoiDoc", Diahinh_HoiDoc);
                                marker.AddVariable("Diahinh_BangPhang", Diahinh_BangPhang);

                                string[] Thangkotiepcan = dr["Thangkotiepcan"].ToString().Split('+');
                                string Thangkotiepcan_1 = "";
                                string Thangkotiepcan_2 = "";
                                string Thangkotiepcan_3 = "";
                                string Thangkotiepcan_4 = "";
                                string Thangkotiepcan_5 = "";
                                string Thangkotiepcan_6 = "";
                                string Thangkotiepcan_7 = "";
                                string Thangkotiepcan_8 = "";
                                string Thangkotiepcan_9 = "";
                                string Thangkotiepcan_10 = "";
                                string Thangkotiepcan_11 = "";
                                string Thangkotiepcan_12 = "";
                                foreach (string s in Thangkotiepcan)
                                {
                                    if (s == "1")
                                    {
                                        Thangkotiepcan_1 = "✓";
                                    }
                                    else if (s == "2")
                                    {
                                        Thangkotiepcan_2 = "✓";
                                    }
                                    else if (s == "3")
                                    {
                                        Thangkotiepcan_3 = "✓";
                                    }
                                    else if (s == "4")
                                    {
                                        Thangkotiepcan_4 = "✓";
                                    }
                                    else if (s == "5")
                                    {
                                        Thangkotiepcan_5 = "✓";
                                    }
                                    else if (s == "6")
                                    {
                                        Thangkotiepcan_6 = "✓";
                                    }
                                    else if (s == "7")
                                    {
                                        Thangkotiepcan_7 = "✓";
                                    }
                                    else if (s == "8")
                                    {
                                        Thangkotiepcan_8 = "✓";
                                    }
                                    else if (s == "9")
                                    {
                                        Thangkotiepcan_9 = "✓";
                                    }
                                    else if (s == "10")
                                    {
                                        Thangkotiepcan_10 = "✓";
                                    }
                                    else if (s == "11")
                                    {
                                        Thangkotiepcan_11 = "✓";
                                    }
                                    else if (s == "12")
                                    {
                                        Thangkotiepcan_12 = "✓";
                                    }
                                }
                                marker.AddVariable("Thangkotiepcan_1", Thangkotiepcan_1);
                                marker.AddVariable("Thangkotiepcan_2", Thangkotiepcan_2);
                                marker.AddVariable("Thangkotiepcan_3", Thangkotiepcan_3);
                                marker.AddVariable("Thangkotiepcan_4", Thangkotiepcan_4);
                                marker.AddVariable("Thangkotiepcan_5", Thangkotiepcan_5);
                                marker.AddVariable("Thangkotiepcan_6", Thangkotiepcan_6);
                                marker.AddVariable("Thangkotiepcan_7", Thangkotiepcan_7);
                                marker.AddVariable("Thangkotiepcan_8", Thangkotiepcan_8);
                                marker.AddVariable("Thangkotiepcan_9", Thangkotiepcan_9);
                                marker.AddVariable("Thangkotiepcan_10", Thangkotiepcan_10);
                                marker.AddVariable("Thangkotiepcan_11", Thangkotiepcan_11);
                                marker.AddVariable("Thangkotiepcan_12", Thangkotiepcan_12);

                                //SqlCommandBuilder sqlCommand2 = null;
                                //SqlDataAdapter sqlAdapter2 = null;
                                //DataTable tableBMVNKS = new DataTable();
                                //sqlAdapter2 = new SqlDataAdapter(string.Format(
                                //    "SELECT " +
                                //    "Cecm_VNTerrainMinePoint_CHA.id as 'STT'," +
                                //    "Cecm_VNTerrainMinePoint_CHA.XPoint as 'Kinhdo'," +
                                //    "Cecm_VNTerrainMinePoint_CHA.YPoint as 'Vido'," +
                                //    "Cecm_VNTerrainMinePoint_CHA.[Loai] as 'Loai'," +
                                //    "Cecm_VNTerrainMinePoint_CHA.[Loai] as 'Kyhieu'," +
                                //    "Cecm_VNTerrainMinePoint_CHA.[SL] as 'Lượng', " +
                                //    "Cecm_VNTerrainMinePoint_CHA.[Tinhtrang] as 'Tinhtrang', " +
                                //    "Cecm_VNTerrainMinePoint_CHA.Ghichu as 'Ghichu' " +
                                //    "FROM Cecm_VNTerrainMinePoint_CHA " +
                                //    "where idRectangle != -1 " +
                                //    "and Cecm_VNTerrainMinePoint_CHA.programId = {0}", dr["Duanid"].ToString()), cn);
                                //sqlCommand2 = new SqlCommandBuilder(sqlAdapter2);
                                //sqlAdapter2.Fill(tableBMVNKS);
                                //tableBMVNKS.Columns.Add("STT");
                                //int index = 1;
                                //foreach(DataRow drBMVNKS in tableBMVNKS.Rows)
                                //{
                                //    drBMVNKS.BeginEdit();
                                //    drBMVNKS["STT"] = index;
                                //    index++;
                                //    drBMVNKS.EndEdit();
                                //}
                                //marker.AddVariable("BMVNKS", tableBMVNKS);

                                SqlCommandBuilder sqlCommand3 = null;
                                SqlDataAdapter sqlAdapter3 = null;
                                System.Data.DataTable tableBMVN = new System.Data.DataTable();
                                string sql = "Select tbl.*, ol.o_id as ol_idST " +
                                    "FROM cecm_ReportPollutionArea_BMVN tbl " +
                                    "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                                    "WHERE tbl.cecm_ReportPollutionArea_id = " + id_kqks;
                                sqlAdapter3 = new SqlDataAdapter(sql, cn);
                                sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                                sqlAdapter3.Fill(tableBMVN);
                                tableBMVN.Columns.Add("STT");
                                int index = 1;
                                foreach (DataRow drBMVNKS in tableBMVN.Rows)
                                {
                                    drBMVNKS.BeginEdit();
                                    drBMVNKS["STT"] = index;
                                    index++;
                                    drBMVNKS.EndEdit();
                                }
                                marker.AddVariable("BMVN", tableBMVN);
                                marker.AddVariable("BMVNCount", tableBMVN.Rows.Count);
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
                                marker.AddVariable("daXuLy", daXuLy);
                                marker.AddVariable("huyTaiCho", huyTaiCho);
                                marker.AddVariable("thuGom", thuGom);
                                marker.AddVariable("ghichu", "Ghi chú: " + dr["Ghichu"].ToString());

                                SqlCommandBuilder sqlCommand = null;
                                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format(
                                    @"SELECT code, Polygongeomst FROM cecm_program_area_map where id = {0}", dr["VungDuan"].ToString()), cn);
                                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                                System.Data.DataTable datatableWard = new System.Data.DataTable();
                                sqlAdapterWard.Fill(datatableWard);
                                DataTable tableCV = new DataTable();
                                tableCV.Columns.Add("STT", typeof(int));
                                tableCV.Columns.Add("Diem", typeof(string));
                                tableCV.Columns.Add("Kinhdo", typeof(string));
                                tableCV.Columns.Add("Vido", typeof(string));
                                tableCV.Columns.Add("MaKV", typeof(string));
                                foreach (DataRow drPolygongeomst in datatableWard.Rows)
                                {
                                    string Str = drPolygongeomst["Polygongeomst"].ToString();
                                    List<PointF[]> lst = AppUtils.convertMultiPolygon(Str);
                                    int i = 0;
                                    foreach (PointF[] points in lst)
                                    {
                                        foreach (PointF point in points)
                                        {
                                            i++;
                                            string indexStr = "";
                                            if (i == 0)
                                            {
                                                indexStr = "đầu";
                                            }
                                            else if (i == lst.Count * points.Length - 1)
                                            {
                                                indexStr = "cuối";
                                            }
                                            else
                                            {
                                                indexStr = (i - 1).ToString();
                                            }
                                            tableCV.Rows.Add(i, "Điểm " + i.ToString(), point.X, point.Y, dr["code"].ToString());
                                        }
                                    }
                                    //Str = Str.Replace("(((", "+");
                                    //Str = Str.Replace(")))", "+");
                                    //var LstStr = Str.Split('+')[1];
                                    //var LstStr1 = LstStr.Split(',');
                                    //int i = 0;
                                    //foreach (string A in LstStr1)
                                    //{
                                    //    i++;
                                    //    var B = A.Split(' ');
                                    //    string indexStr = "";
                                    //    if(i == 0)
                                    //    {
                                    //        indexStr = "đầu";
                                    //    }
                                    //    else if(i == LstStr1.Length)
                                    //    {
                                    //        indexStr = "cuối";
                                    //    }
                                    //    else
                                    //    {
                                    //        indexStr = (i - 1).ToString();
                                    //    }
                                    //    tableCV.Rows.Add(i, "Điểm " + indexStr, B[0], B[1], drPolygongeomst["code"].ToString());
                                    //}
                                }
                                marker.AddVariable("CV", tableCV);
                            }

                            //marker.AddVariable("str", "Nguyễn Minh Hiếu");
                            marker.ApplyMarkers(Syncfusion.XlsIO.UnknownVariableAction.Skip);

                            //Saving and closing the workbook
                            workbook.ActiveSheetIndex = 0;
                            //if (workbook.HasMacros)
                            //{
                            //    workbook.DisableMacrosStart = false;
                            //}
                            
                            workbook.SaveAs(pathFile);
                            //if (workbook.HasMacros)
                            //{
                            //    workbook.DisableMacrosStart = false;
                            //}

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

        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
            //    return;
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtTImkiem.Text = "";
            timeNgaybatdau.Checked = false;
            timeNgayketthuc.Checked = false;
            LoadData();
        }

        private void timeNgayketthuc_ValueChanged(object sender, EventArgs e)
        {
            if (timeNgaybatdau.Value > timeNgayketthuc.Value)
            {
                timeNgayketthuc.Value = timeNgaybatdau.Value;
            }
        }

        private void timeNgaybatdau_ValueChanged(object sender, EventArgs e)
        {
            if (timeNgaybatdau.Value > timeNgayketthuc.Value)
            {
                timeNgaybatdau.Value = timeNgayketthuc.Value;
            }
        }

        private void LoadData()
        {
            try
            {
                //date1 = date1.Split('/')[1] + "-" + date1.Split('/')[0] + "-" + date1.Split('/')[2];
                //date2 = date2.Split('/')[1] + "-" + date2.Split('/')[0] + "-" + date2.Split('/')[2];
                dataGridView1.Rows.Clear();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql =
                    "select " +
                    "DA.id as 'ID', " +
                    "cecm_programData.name as 'Dự án', " +
                    "CONCAT(FORMAT(cecm_programData.startTime, 'dd/MM/yyyy') ,' - ',FORMAT(cecm_programData.endTime, 'dd/MM/yyyy')) as 'Thời gian', " +
                    "CONCAT(Xa.Ten, ',', Huyen.Ten,',',Tinh.Ten) as 'Địa điểm' " +
                    "from cecm_ReportPollutionArea as DA " +
                    "left join cecm_provinces as Tinh on Tinh.id = DA.Tinh " +
                    "left join cecm_provinces as Huyen on Huyen.id = DA.Huyen " +
                    "left join cecm_provinces as Xa on Xa.id = DA.Xa " +
                    "left join cecm_programData on cecm_programData.id = DA.Duan " +
                    "where 1 = 1 ";
                if (!string.IsNullOrEmpty(TxtTImkiem.Text))
                {
                    sql += "and LOWER(cecm_programData.name) LIKE @name ";
                }
                if (timeNgaybatdau.Checked)
                {
                    sql += "and cecm_programData.startTime > @startTime ";
                }
                if (timeNgayketthuc.Checked)
                {
                    sql += "and cecm_programData.endTime < @endTime ";
                }
                sql += "order by DA.id ";
                sqlAdapter = new SqlDataAdapter(sql, cn);
                if (!string.IsNullOrEmpty(TxtTImkiem.Text))
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "name",
                        Value = "%" + TxtTImkiem.Text.ToLower() + "%",
                        SqlDbType = SqlDbType.NVarChar
                    });
                }
                if (timeNgaybatdau.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "startTime",
                        Value = timeNgaybatdau.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }
                if (timeNgayketthuc.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "endTime",
                        Value = timeNgayketthuc.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }
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
                return;
            }
        }

        private void FrmBCKVON_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}