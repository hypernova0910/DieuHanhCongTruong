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
    public partial class FormBCTH : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        public FormBCTH()
        {
            cn = frmLoggin.sqlCon;
            InitializeComponent();
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
                    "from cecm_BaocaoTonghop as DA " +
                    "left join cecm_provinces as Tinh on Tinh.id = DA.Tinh " +
                    "left join cecm_provinces as Huyen on Huyen.id = DA.Huyen " +
                    "left join cecm_provinces as Xa on Xa.id = DA.Xa " +
                    "left join cecm_programData on cecm_programData.id = DA.Duan " +
                    "WHERE 1 = 1 ";
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

        private void FormBCTH_Load(object sender, EventArgs e)
        {
            LoadData();
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
                FrmThemmoiBCTH frm = new FrmThemmoiBCTH(id_kqks);
                frm.Text = "CHỈNH SỬA BÁO CÁO TỔNG HỢP KẾT QUẢ ĐIỀU TRA KHẢO SÁT";
                frm.ShowDialog();
                LoadData();
            }
            //delete column
            if (e.ColumnIndex == Export.Index)
            {
                string json = "", Phieudieutra = "", Baocao = "", Bando = "";
                SqlCommandBuilder sqlCommand = null;

                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(
                    "SELECT " +
                    "cecm_BaocaoTonghop.* ," +
                    "giamsat.nameId as TenGiamSat, " +
                    "d.code as DVKScode," +
                    "cecm_programData.id as 'Duanid', " +
                    "cecm_programData.name as 'DuanName'," +
                    "cecm_programData.code as 'Duancode'," +
                    "Tinh.Ten as 'TinhST'," +
                    "Tinh.code as 'Tinhcode'," +
                    "Huyen.Ten as 'HuyenST'," +
                    "Huyen.code as 'Huyencode'," +
                    "Xa.Ten as 'XaST'," +
                    "Xa.code as 'Xacode' " +
                    "FROM cecm_BaocaoTonghop " +
                    "left join cecm_programData on cecm_programData.id = cecm_BaocaoTonghop.Duan " +
                    "left join cecm_provinces as Tinh on cecm_BaocaoTonghop.Tinh = Tinh.id " +
                    "left join cecm_provinces as Huyen on cecm_BaocaoTonghop.Huyen = Huyen.id " +
                    "left join cecm_provinces as Xa on cecm_BaocaoTonghop.Xa = Xa.id " +
                    "left join cert_department d on d.id = cecm_BaocaoTonghop.DVKS_id " +
                    "left join Cecm_ProgramStaff giamsat on cecm_BaocaoTonghop.Giamsat_id = giamsat.id " +
                    "where cecm_BaocaoTonghop.id = " + id_kqks, cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);

                foreach (DataRow dr in datatableProvince.Rows)
                {
                    idDuan = int.Parse(dr["Duanid"].ToString());
                    Phieudieutra = dr["PhieudieutraLink"].ToString();
                    Baocao = dr["BaocaoLink"].ToString();
                    Bando = dr["BandoLink"].ToString();
                }
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    lstHientrang lstSD = new lstHientrang();
                    lstSD.area1 = double.Parse(dr["Datthocu"].ToString().Split('-')[0]);
                    lstSD.area2 = double.Parse(dr["Dattrongtrot"].ToString().Split('-')[0]);
                    lstSD.area3 = double.Parse(dr["Dattronglaunam"].ToString().Split('-')[0]);
                    lstSD.area4 = double.Parse(dr["Matnuoc"].ToString().Split('-')[0]);
                    lstSD.area5 = double.Parse(dr["Datrungtunhien"].ToString().Split('-')[0]);
                    lstSD.area6 = double.Parse(dr["Datxd"].ToString().Split('-')[0]);
                    lstSD.area7 = double.Parse(dr["Datgiaothong"].ToString().Split('-')[0]);
                    lstSD.area8 = double.Parse(dr["Datthuyloi"].ToString().Split('-')[0]);
                    lstSD.area9 = double.Parse(dr["Datnghiadia"].ToString().Split('-')[0]);
                    lstSD.area10 = double.Parse(dr["Cacloaidatkhac"].ToString().Split('-')[0]);

                    lstHientrang lstNN = new lstHientrang();
                    lstNN.area1 = double.Parse(dr["Datthocu"].ToString().Split('-')[1]);
                    lstNN.area2 = double.Parse(dr["Dattrongtrot"].ToString().Split('-')[1]);
                    lstNN.area3 = double.Parse(dr["Dattronglaunam"].ToString().Split('-')[1]);
                    lstNN.area4 = double.Parse(dr["Matnuoc"].ToString().Split('-')[1]);
                    lstNN.area5 = double.Parse(dr["Datrungtunhien"].ToString().Split('-')[1]);
                    lstNN.area6 = double.Parse(dr["Datxd"].ToString().Split('-')[1]);
                    lstNN.area7 = double.Parse(dr["Datgiaothong"].ToString().Split('-')[1]);
                    lstNN.area8 = double.Parse(dr["Datthuyloi"].ToString().Split('-')[1]);
                    lstNN.area9 = double.Parse(dr["Datnghiadia"].ToString().Split('-')[1]);
                    lstNN.area10 = double.Parse(dr["Cacloaidatkhac"].ToString().Split('-')[1]);

                    lstHientrang lstKON = new lstHientrang();
                    lstKON.area1 = double.Parse(dr["Datthocu2"].ToString());
                    lstKON.area2 = double.Parse(dr["Dattrongtrot2"].ToString());
                    lstKON.area3 = double.Parse(dr["Dattronglaunam2"].ToString());
                    lstKON.area4 = double.Parse(dr["Matnuoc2"].ToString());
                    lstKON.area5 = double.Parse(dr["Datrungtunhien2"].ToString());
                    lstKON.area6 = double.Parse(dr["Datxd2"].ToString());
                    lstKON.area7 = double.Parse(dr["Datgiaothong2"].ToString());
                    lstKON.area8 = double.Parse(dr["Datthuyloi2"].ToString());
                    lstKON.area9 = double.Parse(dr["Datnghiadia2"].ToString());
                    lstKON.area10 = double.Parse(dr["Cacloaidatkhac2"].ToString());

                    List<lstBMVN> lstMine = new List<lstBMVN>();
                    for (int i = 0; i < 5; i++)
                    {
                        lstMine.Add(new lstBMVN()
                        {
                            mine1 = double.Parse(dr["Bompha"].ToString().Split('-')[i]),
                            mine2 = double.Parse(dr["Danphao"].ToString().Split('-')[i]),
                            mine3 = double.Parse(dr["Tenlua"].ToString().Split('-')[i]),
                            mine4 = double.Parse(dr["Luudan"].ToString().Split('-')[i]),
                            mine5 = double.Parse(dr["Bombi"].ToString().Split('-')[i]),
                            mine6 = double.Parse(dr["Mintrongtang"].ToString().Split('-')[i]),
                            mine7 = double.Parse(dr["Mintrongnguoi"].ToString().Split('-')[i]),
                            mine8 = double.Parse(dr["Cacloaivatno"].ToString().Split('-')[i]),
                            mine9 = double.Parse(dr["Satthep"].ToString().Split('-')[i]),

                        });
                    }

                    lstBMVN_note lstMineNote = new lstBMVN_note()
                    {
                        mine1_note = dr["Bompha"].ToString().Split('-')[5],
                        mine2_note = dr["Danphao"].ToString().Split('-')[5],
                        mine3_note = dr["Tenlua"].ToString().Split('-')[5],
                        mine4_note = dr["Luudan"].ToString().Split('-')[5],
                        mine5_note = dr["Bombi"].ToString().Split('-')[5],
                        mine6_note = dr["Mintrongtang"].ToString().Split('-')[5],
                        mine7_note = dr["Mintrongnguoi"].ToString().Split('-')[5],
                        mine8_note = dr["Cacloaivatno"].ToString().Split('-')[5],
                        mine9_note = dr["Satthep"].ToString().Split('-')[5],
                    };

                    lstDT DTRP = new lstDT()
                    {
                        DT1 = double.Parse(dr["DTrapha"].ToString().Split('-')[1]),
                        DT2 = double.Parse(dr["DTrapha"].ToString().Split('-')[2]),
                        DT3 = double.Parse(dr["DTrapha"].ToString().Split('-')[3]),
                        DT4 = double.Parse(dr["DTrapha"].ToString().Split('-')[4]),
                        DT5 = double.Parse(dr["DTrapha"].ToString().Split('-')[5]),
                        DT6 = double.Parse(dr["DTrapha"].ToString().Split('-')[6]),
                        DT7 = double.Parse(dr["DTrapha"].ToString().Split('-')[7]),
                        DT8 = double.Parse(dr["DTrapha"].ToString().Split('-')[8]),
                        DT9 = double.Parse(dr["DTrapha"].ToString().Split('-')[9]),
                        DT10 = double.Parse(dr["DTrapha"].ToString().Split('-')[10]),
                    };

                    List<ClassBCTHKQDTO> Lst = new List<ClassBCTHKQDTO>();
                    Lst.Add(new ClassBCTHKQDTO()
                    {
                        Mabaocao = dr["Mabaocao"].ToString(),
                        DVKScode = dr["DVKScode"].ToString(),
                        commune_id = long.TryParse(dr["Xa"].ToString(), out long Xa) ? Xa : - 1,
                        district_id = long.TryParse(dr["Huyen"].ToString(), out long Huyen) ? Huyen : -1,
                        province_id = long.TryParse(dr["Tinh"].ToString(), out long Tinh) ? Tinh : -1,
                        communeidST = dr["XaST"].ToString(),
                        districtidST = dr["HuyenST"].ToString(),
                        provinceidST = dr["TinhST"].ToString(),
                        cecm_program_idST = dr["DuanName"].ToString(),
                        commune_code = dr["Xacode"].ToString(),
                        district_code = dr["Huyencode"].ToString(),
                        province_code = dr["Tinhcode"].ToString(),
                        Thon = dr["Thon"].ToString(),
                        cecm_program_code = dr["Duancode"].ToString(),
                        component_id = 1,

                        topoST = dr["Diahinh"].ToString(),
                        soilST = dr["Loaidat"].ToString(),
                        weatherST = dr["Thoitiet"].ToString(),
                        pollutionST = dr["Hientrangonhiem"].ToString(),
                        accidentST = dr["Lichsutainan"].ToString(),
                        historyST = dr["Lichsuhoatdong"].ToString(),

                        totalArea1 = double.Parse(dr["DTsd"].ToString()),
                        totalArea2 = double.Parse(dr["DTnghingo"].ToString()),
                        lstSD = lstSD,
                        lstNN = lstNN,
                        lstKON = lstKON,

                        lstMine = lstMine,
                        lstMineNote = lstMineNote,

                        DTKS = double.Parse(dr["DTKS"].ToString()),
                        TH_tong = double.Parse(dr["Tongsotinhieu"].ToString()),
                        TH_1 = double.Parse(dr["Tinhieu"].ToString().Split('-')[0]),
                        TH_2 = double.Parse(dr["Tinhieu"].ToString().Split('-')[1]),
                        MD_tong = double.Parse(dr["Tongmatdo"].ToString()),
                        MD_1 = double.Parse(dr["Matdo"].ToString().Split('-')[0]),
                        MD_2 = double.Parse(dr["Matdo"].ToString().Split('-')[1]),

                        TH_chuaXL = double.Parse(dr["SLtinhieu5m"].ToString()),
                        Mucdo = dr["Mucdoonhiem"].ToString().Split('.')[0],
                        Phanloai = dr["Phanloai"].ToString(),
                        Capdat = dr["Capdat"].ToString(),

                        Chihuy_id = long.TryParse(dr["Chihuy_id"].ToString(), out long Chihuy_id) ? Chihuy_id : -1,
                        Chihuy = dr["Chihuy"].ToString(),
                        Chihuy_other = dr["Chihuy_other"].ToString(),
                        Doitruong_id = long.TryParse(dr["Doitruong_id"].ToString(), out long Doitruong_id) ? Doitruong_id : -1,
                        Doitruong = dr["Doitruong"].ToString(),
                        Doitruong_other = dr["Doitruong_other"].ToString(),

                        Giamsat_id = long.TryParse(dr["Giamsat_id"].ToString(), out long Giamsat_id) ? Giamsat_id : -1,
                        Giamsat = dr["TenGiamSat"].ToString(),
                        Giamsat_other = dr["Giamsat_other"].ToString(),


                        DT_tong = double.Parse(dr["DTrapha"].ToString().Split('-')[0]),
                        DTRP = DTRP

                    });
                    ClassBCTHKQ LstDaily = new ClassBCTHKQ
                    {
                        ClassBCTHKQDTO = Lst,
                    };
                    //string json = JsonSerializer.Serialize(Lst);
                    //File.WriteAllText(@"E:\LstResult.json", json);

                    json = JsonConvert.SerializeObject(LstDaily, Formatting.Indented);
                    List<string> A = new List<string>();
                    A.Add(Phieudieutra);
                    A.Add(Baocao);
                    A.Add(Bando);
                    CompressedFile frm = new CompressedFile(json, A);
                    frm.ShowDialog();

                    //CompressedFile(json, string Phieudieutra, string Baocao, string Bando)
                    //var saveLocation = AppUtils.SaveFileTxt();
                    //if (string.IsNullOrEmpty(saveLocation) == false)
                    //    System.IO.File.WriteAllText(saveLocation, json);

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
            if (e.ColumnIndex == DoiRPBMXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_BaocaoTonghop WHERE id = " + id_kqks, cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                }
                LoadData();
            }
            if (e.ColumnIndex == cotExcel.Index)
            {
                string pathFile = "";
                saveFileDialog1.FileName = "THKS_" + Guid.NewGuid().ToString();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pathFile = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_THKS.xls";
                if (pathFile != null)
                {
                    try
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
                                "cecm_BaocaoTonghop.*, " +
                                "case when doitrg.nameId is null then cecm_BaocaoTonghop.Doitruong_other else doitrg.nameId end as TenDoiTruong, " +
                                "case when chihuy.nameId is null then cecm_BaocaoTonghop.Chihuy_other else chihuy.nameId end as TenChiHuy, " +
                                "case when giamsat.nameId is null then cecm_BaocaoTonghop.Giamsat_other else giamsat.nameId end as TenGiamSat, " +
                                "d.name as TenDVGS, " +
                                "cecm_program1.name as 'TenDuan'," +
                                "cecm_program1.id as 'Duanid'," +
                                //"[Hopphan]," +
                                "Tinh.Ten as 'TenTinh'," +
                                "Tinh.id as 'Tinhid'," +
                                "Huyen.Ten as 'TenHuyen'," +
                                "Huyen.id as 'Huyenid'," +
                                "Xa.Ten as 'TenXa'," +
                                "Xa.id as 'Xaid' " +
                                "FROM cecm_BaocaoTonghop " +
                                "left join Cecm_ProgramStaff doitrg on cecm_BaocaoTonghop.Doitruong_id = doitrg.id " +
                                "left join Cecm_ProgramStaff chihuy on cecm_BaocaoTonghop.Chihuy_id = chihuy.id " +
                                "left join Cecm_ProgramStaff giamsat on cecm_BaocaoTonghop.Giamsat_id = giamsat.id " +
                                "left join cert_department d on cecm_BaocaoTonghop.DVKS_id = d.id " +
                                "left join cecm_programData as cecm_program1 on cecm_program1.id = cecm_BaocaoTonghop.Duan " +
                                "left join cecm_provinces as Tinh on cecm_BaocaoTonghop.Tinh = Tinh.id " +
                                "left join cecm_provinces as Huyen on cecm_BaocaoTonghop.Huyen = Huyen.id " +
                                "left join cecm_provinces as Xa on cecm_BaocaoTonghop.Xa = Xa.id " +
                                "where cecm_BaocaoTonghop.id = {0}", id_kqks), cn);
                            sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                            sqlAdapter.Fill(datatable);
                            if (datatable.Rows.Count != 0)
                            {
                                marker.AddVariable("obj", datatable);
                                DataRow dr = datatable.Rows[0];
                                marker.AddVariable("MaBaocao_long", "Số: " + dr["Mabaocao"].ToString() + "/BC-THKQKS");
                                DateTime now = DateTime.Now;
                                marker.AddVariable("Ngaybaocao", dr["TenTinh"].ToString() + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                                marker.AddVariable("TenDuan", "Dự án: " + dr["TenDuan"].ToString());
                                marker.AddVariable("Diahinh", "Địa hình: " + dr["Diahinh"].ToString());
                                marker.AddVariable("Loaidat", "Loại đất, cấp đất phổ biến: " + dr["Loaidat"].ToString());
                                marker.AddVariable("Thoitiet", "Thời tiết, khí hậu: " + dr["Thoitiet"].ToString());
                                marker.AddVariable("Hientrangonhiem", "Hiện trạng ô nhiễm bom mìn: " + dr["Hientrangonhiem"].ToString());
                                marker.AddVariable("Lichsutainan", "Lịch sử tai nạn bom mìn: " + dr["Lichsutainan"].ToString());
                                marker.AddVariable("Lichsuhoatdong", "Lịch sử hoạt động khắc phục bom mìn: " + dr["Lichsuhoatdong"].ToString());
                                marker.AddVariable("DTsd", "Tổng diện tích đã khảo sát: " + dr["DTsd"].ToString() + " ha");
                                marker.AddVariable("DTnghingo", "Tổng diện tích ô nhiễm sau khảo sát: " + dr["DTnghingo"].ToString() + " ha");
                                marker.AddVariable("DT_KhongON", "Tổng diện tích không ô nhiễm sau khảo sát: " + dr["DT_KhongON"].ToString() + " ha");

                                marker.AddVariable("Datthocu0", "1. Đất thổ cư: " + dr["Datthocu"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Dattrongtrot0", "2. Đất trồng trọt: " + dr["Dattrongtrot"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Dattronglaunam0", "3. Đất trồng cây lâu năm: " + dr["Dattronglaunam"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Matnuoc0", "4. Mặt nước " + dr["Matnuoc"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Datrungtunhien0", "5. Đất rừng tự nhiên: " + dr["Datrungtunhien"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Datxd0", "6. Đất xây dựng: " + dr["Datxd"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Datgiaothong0", "7. Đất giao thông: " + dr["Datgiaothong"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Datthuyloi0", "8. Đất thuỷ lợi: " + dr["Datthuyloi"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Datnghiadia0", "9. Đất nghĩa địa: " + dr["Datnghiadia"].ToString().Split('-')[0] + " ha");
                                marker.AddVariable("Cacloaidatkhac0", "10. Các loại đất khác: " + dr["Cacloaidatkhac"].ToString().Split('-')[0] + " ha");

                                marker.AddVariable("Datthocu1", "1. Đất thổ cư: " + dr["Datthocu"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Dattrongtrot1", "2. Đất trồng trọt: " + dr["Dattrongtrot"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Dattronglaunam1", "3. Đất trồng cây lâu năm: " + dr["Dattronglaunam"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Matnuoc1", "4. Mặt nước " + dr["Matnuoc"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Datrungtunhien1", "5. Đất rừng tự nhiên: " + dr["Datrungtunhien"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Datxd1", "6. Đất xây dựng: " + dr["Datxd"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Datgiaothong1", "7. Đất giao thông: " + dr["Datgiaothong"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Datthuyloi1", "8. Đất thuỷ lợi: " + dr["Datthuyloi"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Datnghiadia1", "9. Đất nghĩa địa: " + dr["Datnghiadia"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("Cacloaidatkhac1", "10. Các loại đất khác: " + dr["Cacloaidatkhac"].ToString().Split('-')[1] + " ha");

                                marker.AddVariable("Datthocu2", "1. Đất thổ cư: " + dr["Datthocu2"].ToString() + " ha");
                                marker.AddVariable("Dattrongtrot2", "2. Đất trồng trọt: " + dr["Dattrongtrot2"].ToString() + " ha");
                                marker.AddVariable("Dattronglaunam2", "3. Đất trồng cây lâu năm: " + dr["Dattronglaunam2"].ToString() + " ha");
                                marker.AddVariable("Matnuoc2", "4. Mặt nước " + dr["Matnuoc2"].ToString() + " ha");
                                marker.AddVariable("Datrungtunhien2", "5. Đất rừng tự nhiên: " + dr["Datrungtunhien2"].ToString() + " ha");
                                marker.AddVariable("Datxd2", "6. Đất xây dựng: " + dr["Datxd2"].ToString() + " ha");
                                marker.AddVariable("Datgiaothong2", "7. Đất giao thông: " + dr["Datgiaothong2"].ToString() + " ha");
                                marker.AddVariable("Datthuyloi2", "8. Đất thuỷ lợi: " + dr["Datthuyloi2"].ToString() + " ha");
                                marker.AddVariable("Datnghiadia2", "9. Đất nghĩa địa: " + dr["Datnghiadia2"].ToString() + " ha");
                                marker.AddVariable("Cacloaidatkhac2", "10. Các loại đất khác: " + dr["Cacloaidatkhac2"].ToString() + " ha");

                                marker.AddVariable("Tongsotinhieu", "Tổng số tín hiệu (m2): " + dr["Tongsotinhieu"].ToString() + ", trong đó");
                                marker.AddVariable("Tinhieu", "Là bom mìn, vật nổ: " + dr["Tinhieu"].ToString().Split('-')[0] + "; khác: " + dr["Tinhieu"].ToString().Split('-')[1]);

                                marker.AddVariable("Tongmatdo", "Mật độ (tín hiệu/10.000 m2): " + dr["Tongmatdo"].ToString() + ", trong đó");
                                marker.AddVariable("Matdo", "Là bom mìn, vật nổ: " + dr["Matdo"].ToString().Split('-')[0] + "; khác: " + dr["Matdo"].ToString().Split('-')[1]);

                                marker.AddVariable("Bompha0", dr["Bompha"].ToString().Split('-')[0]);
                                marker.AddVariable("Bompha1", dr["Bompha"].ToString().Split('-')[1]);
                                marker.AddVariable("Bompha2", dr["Bompha"].ToString().Split('-')[2]);
                                marker.AddVariable("Bompha3", dr["Bompha"].ToString().Split('-')[3]);
                                marker.AddVariable("Bompha4", dr["Bompha"].ToString().Split('-')[4]);
                                marker.AddVariable("Bompha5", dr["Bompha"].ToString().Split('-')[5]);

                                marker.AddVariable("Danphao0", dr["Danphao"].ToString().Split('-')[0]);
                                marker.AddVariable("Danphao1", dr["Danphao"].ToString().Split('-')[1]);
                                marker.AddVariable("Danphao2", dr["Danphao"].ToString().Split('-')[2]);
                                marker.AddVariable("Danphao3", dr["Danphao"].ToString().Split('-')[3]);
                                marker.AddVariable("Danphao4", dr["Danphao"].ToString().Split('-')[4]);
                                marker.AddVariable("Danphao5", dr["Danphao"].ToString().Split('-')[5]);

                                marker.AddVariable("Tenlua0", dr["Tenlua"].ToString().Split('-')[0]);
                                marker.AddVariable("Tenlua1", dr["Tenlua"].ToString().Split('-')[1]);
                                marker.AddVariable("Tenlua2", dr["Tenlua"].ToString().Split('-')[2]);
                                marker.AddVariable("Tenlua3", dr["Tenlua"].ToString().Split('-')[3]);
                                marker.AddVariable("Tenlua4", dr["Tenlua"].ToString().Split('-')[4]);
                                marker.AddVariable("Tenlua5", dr["Tenlua"].ToString().Split('-')[5]);

                                marker.AddVariable("Luudan0", dr["Luudan"].ToString().Split('-')[0]);
                                marker.AddVariable("Luudan1", dr["Luudan"].ToString().Split('-')[1]);
                                marker.AddVariable("Luudan2", dr["Luudan"].ToString().Split('-')[2]);
                                marker.AddVariable("Luudan3", dr["Luudan"].ToString().Split('-')[3]);
                                marker.AddVariable("Luudan4", dr["Luudan"].ToString().Split('-')[4]);
                                marker.AddVariable("Luudan5", dr["Luudan"].ToString().Split('-')[5]);

                                marker.AddVariable("Bombi0", dr["Bombi"].ToString().Split('-')[0]);
                                marker.AddVariable("Bombi1", dr["Bombi"].ToString().Split('-')[1]);
                                marker.AddVariable("Bombi2", dr["Bombi"].ToString().Split('-')[2]);
                                marker.AddVariable("Bombi3", dr["Bombi"].ToString().Split('-')[3]);
                                marker.AddVariable("Bombi4", dr["Bombi"].ToString().Split('-')[4]);
                                marker.AddVariable("Bombi5", dr["Bombi"].ToString().Split('-')[5]);

                                marker.AddVariable("Mintrongtang0", dr["Mintrongtang"].ToString().Split('-')[0]);
                                marker.AddVariable("Mintrongtang1", dr["Mintrongtang"].ToString().Split('-')[1]);
                                marker.AddVariable("Mintrongtang2", dr["Mintrongtang"].ToString().Split('-')[2]);
                                marker.AddVariable("Mintrongtang3", dr["Mintrongtang"].ToString().Split('-')[3]);
                                marker.AddVariable("Mintrongtang4", dr["Mintrongtang"].ToString().Split('-')[4]);
                                marker.AddVariable("Mintrongtang5", dr["Mintrongtang"].ToString().Split('-')[5]);

                                marker.AddVariable("Mintrongnguoi0", dr["Mintrongnguoi"].ToString().Split('-')[0]);
                                marker.AddVariable("Mintrongnguoi1", dr["Mintrongnguoi"].ToString().Split('-')[1]);
                                marker.AddVariable("Mintrongnguoi2", dr["Mintrongnguoi"].ToString().Split('-')[2]);
                                marker.AddVariable("Mintrongnguoi3", dr["Mintrongnguoi"].ToString().Split('-')[3]);
                                marker.AddVariable("Mintrongnguoi4", dr["Mintrongnguoi"].ToString().Split('-')[4]);
                                marker.AddVariable("Mintrongnguoi5", dr["Mintrongnguoi"].ToString().Split('-')[5]);

                                marker.AddVariable("Cacloaivatno0", dr["Cacloaivatno"].ToString().Split('-')[0]);
                                marker.AddVariable("Cacloaivatno1", dr["Cacloaivatno"].ToString().Split('-')[1]);
                                marker.AddVariable("Cacloaivatno2", dr["Cacloaivatno"].ToString().Split('-')[2]);
                                marker.AddVariable("Cacloaivatno3", dr["Cacloaivatno"].ToString().Split('-')[3]);
                                marker.AddVariable("Cacloaivatno4", dr["Cacloaivatno"].ToString().Split('-')[4]);
                                marker.AddVariable("Cacloaivatno5", dr["Cacloaivatno"].ToString().Split('-')[5]);

                                marker.AddVariable("Satthep0", dr["Satthep"].ToString().Split('-')[0]);
                                marker.AddVariable("Satthep1", dr["Satthep"].ToString().Split('-')[1]);
                                marker.AddVariable("Satthep2", dr["Satthep"].ToString().Split('-')[2]);
                                marker.AddVariable("Satthep3", dr["Satthep"].ToString().Split('-')[3]);
                                marker.AddVariable("Satthep4", dr["Satthep"].ToString().Split('-')[4]);
                                marker.AddVariable("Satthep5", dr["Satthep"].ToString().Split('-')[5]);

                                marker.AddVariable("DTrapha0", "Diện tích cần rà phá bom mìn: " + dr["DTrapha"].ToString().Split('-')[0] + " ha; Trong đó:");
                                marker.AddVariable("DTrapha1", "Đến độ sâu 0,3 m: " + dr["DTrapha"].ToString().Split('-')[1] + " ha");
                                marker.AddVariable("DTrapha2", "Đến độ sâu 5m: " + dr["DTrapha"].ToString().Split('-')[2] + " ha");
                                marker.AddVariable("DTrapha3", "Đến độ sâu 3m: " + dr["DTrapha"].ToString().Split('-')[3] + " ha");
                                marker.AddVariable("DTrapha4", "Đến độ sâu 1,5 m: " + dr["DTrapha"].ToString().Split('-')[4] + " ha");
                                marker.AddVariable("DTrapha5", "Diện tích mặt nước: " + dr["DTrapha"].ToString().Split('-')[5] + " ha");
                                marker.AddVariable("DTbien", "Diện tích biển: " + dr["DTrapha"].ToString().Split('-')[6] + " ha (trong đó độ sâu ≤ 15 m: " + dr["DTrapha"].ToString().Split('-')[7] + " ha; từ 15-30 m: " + dr["DTrapha"].ToString().Split('-')[8] + " ha; trên 30 m: " + dr["DTrapha"].ToString().Split('-')[9] + " ha)");
                                //marker.AddVariable("DTrapha6", dr["DTrapha"].ToString().Split('-')[6]);
                                //marker.AddVariable("DTrapha7", dr["DTrapha"].ToString().Split('-')[7]);
                                //marker.AddVariable("DTrapha8", dr["DTrapha"].ToString().Split('-')[8]);
                                marker.AddVariable("DTrapha10", "Diện tích phát quang phục vụ RPBM: " + dr["DTrapha"].ToString().Split('-')[10] + " ha");
                                
                            }

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
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace);
                    }
                }
            }
        }

        private void BtnThemmoi_Click(object sender, EventArgs e)
        {
            FrmThemmoiBCTH frm = new FrmThemmoiBCTH(0);
            frm.Text = "THÊM MỚI BÁO CÁO TỔNG HỢP KẾT QUẢ ĐIỀU TRA KHẢO SÁT";
            frm.ShowDialog();
            LoadData();
        }
    }
}
