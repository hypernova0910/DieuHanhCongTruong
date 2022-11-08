using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using System.Globalization;
using DieuHanhCongTruong.Common;

namespace VNRaPaBomMin
{
    public partial class FrmBaocaoKQ : Form
    {
        private SqlConnection cn = null;
        public string ipAddr = "", databaseName = "", userName = "";
        public int idDuan;
        private ConnectionWithExtraInfo connectionWithExtraInfo;

        public FrmBaocaoKQ()
        {
            connectionWithExtraInfo = UtilsDatabase._ExtraInfoConnettion;
            cn = connectionWithExtraInfo.Connection as SqlConnection;
            InitializeComponent();
        }

        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0 || e.ColumnIndex < 0)
        //        return;

        //    var dgvRow = dataGridView1.Rows[e.RowIndex];
        //    if (dgvRow.Tag == null)
        //        return;
        //    string str = dgvRow.Tag as string;
        //    int id_kqks = int.Parse(str);

        //    if (e.ColumnIndex == 5)
        //    {
        //        FrmThemmoiBaocaoKQ frm = new FrmThemmoiBaocaoKQ(id_kqks);
        //        frm.Text = "CHỈNH SỬA BÁO CÁO KẾT QUẢ KHẢO SÁT HÀNG NGÀY";
        //        frm.ShowDialog();
        //        LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        //    }
        //    //delete column
        //    if (e.ColumnIndex == 4)
        //    {
        //        List<lst_resultSurvey> lst_resultSurveyA = new List<lst_resultSurvey>();
        //        List<lst_bomb> lst_bombA = new List<lst_bomb>();
        //        SqlCommandBuilder sqlCommand = null;
        //        SqlCommandBuilder sqlCommand1 = null;
        //        SqlCommandBuilder sqlCommand2 = null;

        //        SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT cecm_reportdaily.id,cecm_programData.id as 'Duanid', cecm_programData.name as 'Duan',cecm_programData.code as 'Duancode',[Hopphan],Tinh.Ten as 'Tinh',Tinh.code as 'Tinhcode',Huyen.Ten as 'Huyen',Huyen.code as 'Huyencode',Xa.Ten as 'Xa',Xa.code as 'Xacode',[Ngaytao],[MaNV],cert_command_person.name as 'Doiso',[Doiso] as 'Doisoid',cert_command_person.master as 'Doitruong',[Quanso],[timeBatdau],[timeKetthuc],[timeDV],[LidoDV],[MaBaocaongay],[Oxanhla],[Odo],[Oxam],[TongoKs],[TongoDs],[DientichKS],[Doingoai],[Doitruongngoai],[Thon] FROM [dbo].[cecm_reportdaily] left join cecm_programData on cecm_programData.id = cecm_reportdaily.Duan left join cecm_provinces as Tinh on cecm_reportdaily.Tinh = Tinh.id left join cecm_provinces as Huyen on cecm_reportdaily.Huyen = Huyen.id left join cecm_provinces as Xa on cecm_reportdaily.Xa = Xa.id left join cert_command_person on cecm_reportdaily.Doiso = cert_command_person.id where cecm_reportdaily.id = " + id_kqks, cn);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
        //        System.Data.DataTable datatableProvince = new System.Data.DataTable();
        //        sqlAdapterProvince.Fill(datatableProvince);

        //        foreach (DataRow dr in datatableProvince.Rows)
        //        {
        //            idDuan = int.Parse(dr["Duanid"].ToString());
        //        }
        //        SqlDataAdapter sqlAdapterKQKS = new SqlDataAdapter("select Cecm_TerrainRectangle.* from Cecm_TerrainRectangle left join cecm_program_area_map on cecm_program_area_map.id = Cecm_TerrainRectangle.programId where cecm_program_area_map.cecm_program_id = " + idDuan + "or cecm_program_area_map.cecm_program_id = " + idDuan, cn);
        //        sqlCommand1 = new SqlCommandBuilder(sqlAdapterKQKS);
        //        System.Data.DataTable datatableKQKS = new System.Data.DataTable();
        //        sqlAdapterKQKS.Fill(datatableKQKS);
        //        if (datatableKQKS.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr in datatableKQKS.Rows)
        //            {
        //                lst_resultSurvey A = new lst_resultSurvey
        //                {
        //                    st_ref1 = dr["code"].ToString(),
        //                    id_ref1 = long.Parse(dr["Ketqua"].ToString().Split('.')[0]),
        //                    id_ref2 = long.Parse(dr["Matdo"].ToString().Split('.')[0]),
        //                    double_column1 = double.Parse(dr["DaxulyM2"].ToString()),
        //                    double_column2 = double.Parse(dr["DientichchuaKS"].ToString()),
        //                    st_ref2 = dr["Ghichu"].ToString(),
        //                    double_column3 = double.Parse(dr["Dientichcaybui"].ToString()),
        //                    double_column4 = double.Parse(dr["Dientichtretruc"].ToString()),
        //                    double_column5 = double.Parse(dr["Dientichcayto"].ToString()),
        //                    double_column6 = double.Parse(dr["Matdothua"].ToString()),
        //                    double_column7 = double.Parse(dr["MatdoTB"].ToString()),
        //                    double_column8 = double.Parse(dr["Matdoday"].ToString()),
        //                    double_column9 = double.Parse(dr["Sotinhieu"].ToString()),
        //                };

        //                lst_resultSurveyA.Add(A);
        //            };

        //        }

        //        SqlDataAdapter sqlAdapterBom = new SqlDataAdapter("SELECT Cecm_VNTerrainMinePoint.*,Cecm_TerrainRectangle.code FROM Cecm_VNTerrainMinePoint left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint.programId left join Cecm_TerrainRectangle on Cecm_TerrainRectangle.id = Cecm_VNTerrainMinePoint.idRectangle where idRectangle != -1 and (cecm_program_area_map.cecm_program_id = " + idDuan + "or cecm_program_area_map.cecm_program_id = " + idDuan + ")", cn);
        //        sqlCommand2 = new SqlCommandBuilder(sqlAdapterBom);
        //        System.Data.DataTable datatableBom = new System.Data.DataTable();
        //        sqlAdapterBom.Fill(datatableBom);
        //        if (datatableBom.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr in datatableBom.Rows)
        //            {
        //                if (dr["Loai"].ToString() != null)
        //                {
        //                    lst_bomb B = new lst_bomb
        //                    {
        //                        st_ref1 = dr["Kyhieu"].ToString(),
        //                        id_ref1 = long.Parse(dr["Loai"].ToString().Split('.')[0]),
        //                        st_ref2 = dr["code"].ToString(),
        //                        id_ref2 = long.Parse(dr["SL"].ToString()),
        //                        double_column1 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
        //                        double_column5 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
        //                        double_column2 = Math.Round(double.Parse(dr["XPoint"].ToString()), 6),
        //                        double_column3 = Math.Round(double.Parse(dr["YPoint"].ToString()), 6),
        //                        double_column4 = double.Parse(dr["Deep"].ToString()),
        //                        st_ref3 = dr["Tinhtrang"].ToString(),
        //                    };

        //                    lst_bombA.Add(B);
        //                }
        //                else
        //                {
        //                    lst_bomb B = new lst_bomb
        //                    {
        //                        st_ref1 = dr["Kyhieu"].ToString(),
        //                        id_ref1 = 0,
        //                        st_ref2 = dr["code"].ToString(),
        //                        id_ref2 = long.Parse(dr["SL"].ToString()),
        //                        double_column1 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
        //                        double_column5 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
        //                        double_column2 = Math.Round(double.Parse(dr["XPoint"].ToString()), 6),
        //                        double_column3 = Math.Round(double.Parse(dr["YPoint"].ToString()), 6),
        //                        double_column4 = double.Parse(dr["Deep"].ToString()),
        //                        st_ref3 = dr["Tinhtrang"].ToString(),
        //                    };

        //                    lst_bombA.Add(B);
        //                }
        //            };

        //        }

        //        foreach (DataRow dr in datatableProvince.Rows)
        //        {
        //            List<ReportSurveyDailyDTO> Lst = new List<ReportSurveyDailyDTO>();
        //            Lst.Add(new ReportSurveyDailyDTO()
        //            {
        //                communeidST = dr["Xa"].ToString(),
        //                districtidST = dr["Huyen"].ToString(),
        //                provinceidST = dr["Tinh"].ToString(),
        //                cecm_program_idST = dr["Duan"].ToString(),
        //                commune_code = dr["Xacode"].ToString(),
        //                district_code = dr["Huyencode"].ToString(),
        //                province_code = dr["Tinhcode"].ToString(),
        //                cecm_program_code = dr["Duancode"].ToString(),
        //                datesST = Convert.ToDateTime(dr["Ngaytao"]).ToString("dd/MM/yyyy"),
        //                taskcode = dr["MaNV"].ToString(),
        //                component_id = 1,
        //                dept_idST = dr["Doiso"].ToString(),
        //                team_number = dr["Doingoai"].ToString(),
        //                dailycode = dr["MaBaocaongay"].ToString(),
        //                date_startST = Convert.ToDateTime(dr["timeBatdau"]).ToString("dd/MM/yyyy"),
        //                date_endST = Convert.ToDateTime(dr["timeKetthuc"]).ToString("dd/MM/yyyy"),
        //                date_endtmpST = Convert.ToDateTime(dr["timeDV"]).ToString("dd/MM/yyyy"),
        //                master_idST = dr["Doitruong"].ToString(),
        //                master_name = dr["Doitruongngoai"].ToString(),
        //                total_team = long.Parse(dr["Quanso"].ToString()),
        //                reason = dr["LidoDV"].ToString(),
        //                lst_resultSurvey = lst_resultSurveyA,
        //                lst_bomb = lst_bombA
        //            });
        //            ReportSurveyDaily LstDaily = new ReportSurveyDaily
        //            {
        //                ReportSurveyDailyDTO = Lst,
        //            };
        //            //string json = JsonSerializer.Serialize(Lst);
        //            //File.WriteAllText(@"E:\LstResult.json", json);

        //            string json = JsonConvert.SerializeObject(LstDaily, Formatting.Indented);

        //            var saveLocation = AppUtils.SaveFileTxt();
        //            if (string.IsNullOrEmpty(saveLocation) == false)
        //            {
        //                System.IO.File.WriteAllText(saveLocation, json);
        //                MessageBox.Show("Xuất file dữ liệu thành công... ", "Thành công");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Xuất file dữ liệu thất bại... ", "Thất bại");
        //            }

        //            //string JSONresult = JsonConvert.SerializeObject(Lst);
        //            //string path = @"E:\LstResult.json";
        //            //using (var tw = new StreamWriter(path, true))
        //            //{
        //            //    tw.WriteLine(JSONresult.ToString());
        //            //    tw.Close();
        //            //}
        //        }

        //    }
        //    if (e.ColumnIndex == 6)
        //    {
        //        if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
        //            return;
        //        SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_reportdaily WHERE cecm_reportdaily.id = " + id_kqks, cn);
        //        int temp = 0;
        //        temp = cmd2.ExecuteNonQuery();
        //        if (temp > 0)
        //        {
        //            MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
        //        }
        //        LoadData(TxtTImkiem.Text, timeNgaybatdau.Text, timeNgayketthuc.Text);
        //    }
        //}
        private void BtnThemmoi_Click(object sender, EventArgs e)
        {
            FrmThemmoiBaocaoKQ frm = new FrmThemmoiBaocaoKQ(0);
            frm.Text = "THÊM MỚI BÁO CÁO KẾT QUẢ KHẢO SÁT HÀNG NGÀY";
            frm.ShowDialog();
            LoadData();
        }
        private void FrmBaocaoKQ_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
                FrmThemmoiBaocaoKQ frm = new FrmThemmoiBaocaoKQ(id_kqks);
                frm.Text = "CHỈNH SỬA BÁO CÁO KẾT QUẢ KHẢO SÁT HÀNG NGÀY";
                frm.ShowDialog();
                LoadData();
            }
            //delete column
            if (e.ColumnIndex == Export.Index)
            {
                List<lst_resultSurvey> lst_resultSurveyA = new List<lst_resultSurvey>();
                List<lst_bomb> lst_bombA = new List<lst_bomb>();
                SqlCommandBuilder sqlCommand = null;
                //SqlCommandBuilder sqlCommand1 = null;
                //SqlCommandBuilder sqlCommand2 = null;

                string sql_main = "SELECT " +
                    "cecm_reportdaily.id," +
                    "cecm_programData.id as 'Duanid', " +
                    "cecm_programData.name as 'Duan'," +
                    "cecm_programData.code as 'Duancode'," +
                    "[Hopphan]," +
                    "d.code as DVKScode," +
                    "cecm_reportdaily.Tinh as Tinh_id," +
                    "cecm_reportdaily.Huyen as Huyen_id," +
                    "cecm_reportdaily.Xa as Xa_id," +
                    "Tinh.Ten as 'Tinh'," +
                    "Tinh.code as 'Tinhcode'," +
                    "Huyen.Ten as 'Huyen'," +
                    "Huyen.code as 'Huyencode'," +
                    "Xa.Ten as 'Xa'," +
                    "Xa.code as 'Xacode'," +
                    "[Ngaytao]," +
                    "[MaNV]," +
                    "cert_command_person.name as 'Doiso'," +
                    "[Doiso] as 'Doisoid'," +
                    "cert_command_person.master as 'Doitruong'," +
                    "[Quanso]," +
                    "[timeBatdau]," +
                    "[timeKetthuc]," +
                    "[timeDV]," +
                    "[LidoDV]," +
                    "[MaBaocaongay]," +
                    "[Oxanhla]," +
                    "[Odo]," +
                    "[Ovang]," +
                    "[Onau]," +
                    "[Oxam]," +
                    "TongO," +
                    "[TongoKs]," +
                    "[TongoDs]," +
                    "[DientichKS]," +
                    "DientichKTLai," +
                    "[Doingoai]," +
                    "[Doitruongngoai]," +
                    @"[GiamSat_id]
                      ,[ChiHuyCT_id]
                      ,[GiamSat_other]
                      ,[ChiHuyCT_other]
                       ,[DTNNON]
                      ,[DTON]
                      ,[TinHieuTong]
                      ,[TinHieuBMVN]
                      ,[TinHieuKhac]
                      ,[MatDoTHTong]
                      ,[MatDoTHBMVN]
                      ,[MatDoTHKhac]," +
                      "giamsat.nameId as GiamSat_idST," +
                      "chihuy.nameId as ChiHuyCT_idST," +
                    "[Thon] " +
                    "FROM " +
                    "[dbo].[cecm_reportdaily] " +
                    "left join cecm_programData on cecm_programData.id = cecm_reportdaily.Duan " +
                    "left join cecm_provinces as Tinh on cecm_reportdaily.Tinh = Tinh.id " +
                    "left join cecm_provinces as Huyen on cecm_reportdaily.Huyen = Huyen.id " +
                    "left join cecm_provinces as Xa on cecm_reportdaily.Xa = Xa.id " +
                    "left join cert_command_person on cecm_reportdaily.Doiso = cert_command_person.id " +
                    "left join cert_department d on d.id = cecm_reportdaily.DVKS_id " +
                    "left join Cecm_ProgramStaff giamsat on cecm_reportdaily.GiamSat_id = giamsat.id " +
                    "left join Cecm_ProgramStaff chihuy on cecm_reportdaily.ChiHuyCT_id = chihuy.id " +
                    "where cecm_reportdaily.id = " + id_kqks;
                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(sql_main, cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);

                foreach (DataRow dr in datatableProvince.Rows)
                {
                    idDuan = int.Parse(dr["Duanid"].ToString());
                }
                //SqlDataAdapter sqlAdapterKQKS = new SqlDataAdapter("select Cecm_TerrainRectangle.* from Cecm_TerrainRectangle left join cecm_program_area_map on cecm_program_area_map.id = Cecm_TerrainRectangle.programId where cecm_program_area_map.cecm_program_id = " + idDuan + "or cecm_program_area_map.cecm_program_id = " + idDuan, cn);
                //sqlCommand1 = new SqlCommandBuilder(sqlAdapterKQKS);
                //System.Data.DataTable datatableKQKS = new System.Data.DataTable();
                //sqlAdapterKQKS.Fill(datatableKQKS);
                System.Data.DataTable tableKQKS = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter1 = null;
                sqlAdapter1 = new SqlDataAdapter(string.Format(
                    "Select tbl.*, ol.o_id as ol_idST, area.code as area_code " +
                    "FROM cecm_reportdaily_KQKS tbl " +
                    "left join OLuoi ol on tbl.ol_id = ol.gid " +
                    "left join cecm_program_area_map area on tbl.area_id = area.id " +
                    "WHERE tbl.cecm_reportdaily_id = {0}"
                    , id_kqks), cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter1);
                sqlAdapter1.Fill(tableKQKS);
                if (tableKQKS.Rows.Count != 0)
                {
                    foreach (DataRow dr in tableKQKS.Rows)
                    {
                        lst_resultSurvey A = new lst_resultSurvey
                        {
                            st_ref1 = dr["ol_idST"].ToString(),
                            id_ref1 = long.TryParse(dr["Ketqua"].ToString(), out long Ketqua) ? Ketqua : 0,
                            id_ref2 = long.TryParse(dr["Matdo"].ToString().Split('.')[0], out long Matdo) ? Matdo : 0,
                            double_column1 = double.TryParse(dr["DaxulyM2"].ToString(), out double DaxulyM2) ? DaxulyM2 : 0,
                            double_column2 = double.TryParse(dr["DientichchuaKS"].ToString(), out double DientichchuaKS) ? DientichchuaKS: 0,
                            st_ref2 = dr["Ghichu"].ToString(),
                            double_column3 = double.TryParse(dr["Dientichcaybui"].ToString(), out double Dientichcaybui) ? Dientichcaybui : 0,
                            double_column4 = double.TryParse(dr["Dientichtretruc"].ToString(), out double Dientichtretruc) ? Dientichtretruc : 0,
                            double_column5 = double.TryParse(dr["Dientichcayto"].ToString(), out double Dientichcayto) ? Dientichcayto : 0,
                            double_column6 = double.TryParse(dr["Matdothua"].ToString(), out double Matdothua) ? Matdothua : 0,
                            double_column7 = double.TryParse(dr["MatdoTB"].ToString(), out double MatdoTB) ? MatdoTB : 0,
                            double_column8 = double.TryParse(dr["Matdoday"].ToString(), out double Matdoday) ? Matdoday : 0,
                            double_column9 = double.TryParse(dr["Sotinhieu"].ToString(), out double Sotinhieu) ? Sotinhieu : 0,
                            st_ref3 = dr["area_code"].ToString()
                        };

                        lst_resultSurveyA.Add(A);
                    };

                }

                //SqlDataAdapter sqlAdapterBom = new SqlDataAdapter("SELECT Cecm_VNTerrainMinePoint.*,Cecm_TerrainRectangle.code FROM Cecm_VNTerrainMinePoint left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint.programId left join Cecm_TerrainRectangle on Cecm_TerrainRectangle.id = Cecm_VNTerrainMinePoint.idRectangle where idRectangle != -1 and (cecm_program_area_map.cecm_program_id = " + idDuan + "or cecm_program_area_map.cecm_program_id = " + idDuan + ")", cn);
                //sqlCommand2 = new SqlCommandBuilder(sqlAdapterBom);
                //System.Data.DataTable datatableBom = new System.Data.DataTable();
                //sqlAdapterBom.Fill(datatableBom);
                SqlCommandBuilder sqlCommand2 = null;
                SqlDataAdapter sqlAdapter2 = null;
                System.Data.DataTable tableBMVN = new System.Data.DataTable();
                string sql = "Select tbl.*, ol.o_id as ol_idST " +
                    "FROM cecm_reportdaily_BMVN tbl " +
                    "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                    "WHERE tbl.cecm_reportdaily_id = " + id_kqks;
                sqlAdapter2 = new SqlDataAdapter(sql, cn);
                sqlCommand2 = new SqlCommandBuilder(sqlAdapter2);
                sqlAdapter2.Fill(tableBMVN);
                if (tableBMVN.Rows.Count != 0)
                {
                    foreach (DataRow dr in tableBMVN.Rows)
                    {
                        //long Tinhtrang = 0;
                        //if (dr["Tinhtrang"].ToString() == Constants.TINHTRANG_DAXULY)
                        //{
                        //    Tinhtrang = 1;
                        //}
                        //if (dr["Tinhtrang"].ToString() == Constants.TINHTRANG_CHUAXULY)
                        //{
                        //    Tinhtrang = 2;
                        //}
                        //long PPXuLy = 0;
                        //if (dr["PPXuLy"].ToString() == Constants.PPXULY_HUYTAICHO)
                        //{
                        //    PPXuLy = 1;
                        //}
                        //if (dr["PPXuLy"].ToString() == Constants.PPXULY_THUGOM)
                        //{
                        //    PPXuLy = 2;
                        //}
                        if (long.TryParse(dr["Loai"].ToString().Split('.')[0], out long Loai))
                        {
                            lst_bomb B = new lst_bomb
                            {
                                st_ref1 = dr["Kyhieu"].ToString(),
                                id_ref1 = Loai,
                                st_ref2 = dr["ol_idST"].ToString(),
                                id_ref2 = long.Parse(dr["SL"].ToString()),
                                double_column1 = double.Parse(dr["Kichthuoc"].ToString()),
                                //double_column1 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
                                //double_column5 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
                                double_column2 = Math.Round(double.Parse(dr["Kinhdo"].ToString()), 6),
                                double_column3 = Math.Round(double.Parse(dr["Vido"].ToString()), 6),

                                double_column4 = double.Parse(dr["Deep"].ToString()),
                                //st_ref3 = dr["Tinhtrang"].ToString(),
                                //st_ref4 = dr["PPXuLy"].ToString(),
                                //long_column1 = Tinhtrang,
                                //long_column2 = PPXuLy
                                id_ref8 = long.TryParse(dr["Tinhtrang"].ToString(), out long Tinhtrang) ? Tinhtrang : -1,
                                id_ref9 = long.TryParse(dr["PPXuLy"].ToString(), out long PPXuLy) ? PPXuLy : -1,
                            };

                            lst_bombA.Add(B);
                        }
                        else
                        {
                            lst_bomb B = new lst_bomb
                            {
                                st_ref1 = dr["Kyhieu"].ToString(),
                                id_ref1 = 0,
                                st_ref2 = dr["ol_idST"].ToString(),
                                id_ref2 = long.Parse(dr["SL"].ToString()),
                                double_column1 = double.Parse(dr["Kichthuoc"].ToString()),
                                //double_column1 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
                                //double_column5 = Math.Pow(double.Parse(dr["Area"].ToString()), 2),
                                double_column2 = Math.Round(double.Parse(dr["Kinhdo"].ToString()), 6),
                                double_column3 = Math.Round(double.Parse(dr["Vido"].ToString()), 6),
                                double_column4 = double.Parse(dr["Deep"].ToString()),
                                st_ref3 = dr["Tinhtrang"].ToString(),
                                st_ref4 = dr["PPXuLy"].ToString(),
                                //long_column1 = Tinhtrang,
                                //long_column2 = PPXuLy
                                id_ref8 = long.TryParse(dr["Tinhtrang"].ToString(), out long Tinhtrang) ? Tinhtrang : -1,
                                id_ref9 = long.TryParse(dr["PPXuLy"].ToString(), out long PPXuLy) ? PPXuLy : -1,
                            };

                            lst_bombA.Add(B);
                        }
                    };

                }

                foreach (DataRow dr in datatableProvince.Rows)
                {
                    List<ReportSurveyDailyDTO> Lst = new List<ReportSurveyDailyDTO>();
                    Lst.Add(new ReportSurveyDailyDTO()
                    {
                        GiamSat_id = long.TryParse(dr["GiamSat_id"].ToString(), out long GiamSat_id) ? GiamSat_id : -1,
                        ChiHuyCT_id = long.TryParse(dr["ChiHuyCT_id"].ToString(), out long ChiHuyCT_id) ? ChiHuyCT_id : -1,
                        GiamSat_idST = dr["GiamSat_idST"].ToString(),
                        ChiHuyCT_idST = dr["ChiHuyCT_idST"].ToString(),
                        GiamSat_other = dr["GiamSat_other"].ToString(),
                        ChiHuyCT_other = dr["ChiHuyCT_other"].ToString(),
                        communeid = long.Parse(dr["Xa_id"].ToString()),
                        districtid = long.Parse(dr["Huyen_id"].ToString()),
                        provinceid = long.Parse(dr["Tinh_id"].ToString()),
                        communeidST = dr["Xa"].ToString(),
                        districtidST = dr["Huyen"].ToString(),
                        provinceidST = dr["Tinh"].ToString(),
                        cecm_program_idST = dr["Duan"].ToString(),
                        commune_code = dr["Xacode"].ToString(),
                        district_code = dr["Huyencode"].ToString(),
                        province_code = dr["Tinhcode"].ToString(),
                        Thon = dr["Thon"].ToString(),
                        cecm_program_code = dr["Duancode"].ToString(),
                        datesST = Convert.ToDateTime(dr["Ngaytao"]).ToString("dd/MM/yyyy"),
                        taskcode = dr["MaNV"].ToString(),
                        component_id = 1,
                        dept_idST = dr["Doiso"].ToString(),
                        team_number = dr["Doingoai"].ToString(),
                        dailycode = dr["MaBaocaongay"].ToString(),
                        date_startST = Convert.ToDateTime(dr["timeBatdau"]).ToString("dd/MM/yyyy"),
                        date_endST = Convert.ToDateTime(dr["timeKetthuc"]).ToString("dd/MM/yyyy"),
                        date_endtmpST = Convert.ToDateTime(dr["timeDV"]).ToString("dd/MM/yyyy"),
                        master_idST = dr["Doitruong"].ToString(),
                        master_name = dr["Doitruongngoai"].ToString(),
                        total_team = long.Parse(dr["Quanso"].ToString()),
                        reason = dr["LidoDV"].ToString(),
                        DVKScode = dr["DVKScode"].ToString(),
                        lst_resultSurvey = lst_resultSurveyA,
                        lst_bomb = lst_bombA,
                        DTNNON = double.TryParse(dr["DTNNON"].ToString(), out double DTNNON) ? DTNNON : 0,
                        DTON = double.TryParse(dr["DTON"].ToString(), out double DTON) ? DTON : 0,
                        TinHieuTong = int.TryParse(dr["TinHieuTong"].ToString(), out int TinHieuTong) ? TinHieuTong : 0,
                        TinHieuBMVN = int.TryParse(dr["TinHieuBMVN"].ToString(), out int TinHieuBMVN) ? TinHieuBMVN : 0,
                        TinHieuKhac = int.TryParse(dr["TinHieuKhac"].ToString(), out int TinHieuKhac) ? TinHieuKhac : 0,
                        MatDoTHTong = double.TryParse(dr["MatDoTHTong"].ToString(), out double MatDoTHTong) ? MatDoTHTong : 0,
                        MatDoTHBMVN = double.TryParse(dr["MatDoTHBMVN"].ToString(), out double MatDoTHBMVN) ? MatDoTHBMVN : 0,
                        MatDoTHKhac = double.TryParse(dr["MatDoTHKhac"].ToString(), out double MatDoTHKhac) ? MatDoTHKhac : 0,
                        Oxanhla = int.TryParse(dr["Oxanhla"].ToString(), out int Oxanhla) ? Oxanhla : 0,
                        Odo = int.TryParse(dr["Odo"].ToString(), out int Odo) ? Odo : 0,
                        Ovang = int.TryParse(dr["Ovang"].ToString(), out int Ovang) ? Ovang : 0,
                        Onau = int.TryParse(dr["Onau"].ToString(), out int Onau) ? Onau : 0,
                        Oxam = int.TryParse(dr["Oxam"].ToString(), out int Oxam) ? Oxam : 0,
                        TongO = int.TryParse(dr["TongO"].ToString(), out int TongO) ? TongO : 0,
                        TongoDs = int.TryParse(dr["TongoDs"].ToString(), out int TongoDs) ? TongoDs : 0,
                        TongoKs = int.TryParse(dr["TongoKs"].ToString(), out int TongoKs) ? TongoKs : 0,
                        DientichKS = double.TryParse(dr["DientichKS"].ToString(), out double DientichKS) ? DientichKS : 0,
                        DientichKTLai = double.TryParse(dr["DientichKTLai"].ToString(), out double DientichKTLai) ? DientichKTLai : 0,
                    });
                    ReportSurveyDaily LstDaily = new ReportSurveyDaily
                    {
                        ReportSurveyDailyDTO = Lst,
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
                }

            }
            if (e.ColumnIndex == DoiRPBMXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                try
                {
                    connectionWithExtraInfo.BeginTransaction();
                    SqlCommand cmd = new SqlCommand("DELETE FROM cecm_reportdaily WHERE cecm_reportdaily.id = " + id_kqks, cn);
                    int temp = 0;
                    cmd.Transaction = connectionWithExtraInfo.Transaction as SqlTransaction;
                    temp = cmd.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM cecm_reportdaily_KQKS WHERE cecm_reportdaily_id = " + id_kqks, cn);
                    cmd1.Transaction = connectionWithExtraInfo.Transaction as SqlTransaction;
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_reportdaily_BMVN WHERE cecm_reportdaily_id = " + id_kqks, cn);
                    cmd2.Transaction = connectionWithExtraInfo.Transaction as SqlTransaction;
                    cmd2.ExecuteNonQuery();
                    if (temp > 0)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                        connectionWithExtraInfo.Transaction.Commit();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                        connectionWithExtraInfo.Transaction.Rollback();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                    connectionWithExtraInfo.Transaction.Rollback();
                }
                
                LoadData();
            }
            if(e.ColumnIndex == cotExcel.Index)
            {
                try
                {


                    string pathFile = "";
                    saveFileDialog1.FileName = "KSN_" + Guid.NewGuid().ToString();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        pathFile = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                    string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\BB_KQKSHN.xls";
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

                            DataTable datatable = new DataTable();
                            SqlDataAdapter sqlAdapter = new SqlDataAdapter(string.Format(
                                "SELECT " +
                                "cecm_reportdaily.id, " +
                                "cecm_reportdaily.Duan as 'Duan'," +
                                "cecm_programData.name as 'TenDA'," +
                                "[Hopphan]," +
                                "Tinh.Ten as 'Tinh'," +
                                "Tinh.id as 'Tinhid'," +
                                "Huyen.Ten as 'Huyen'," +
                                "Huyen.id as 'Huyenid'," +
                                "Xa.Ten as 'Xa'," +
                                "Xa.id as 'Xaid'," +
                                "d.name as TenDVKS, " +
                                "[Ngaytao]," +
                                "[MaNV]," +
                                "cert_command_person.name as 'Doiso'," +
                                "[Doiso] as 'Doisoid'," +
                                "cert_command_person.master as 'Doitruong'," +
                                "[Quanso]," +
                                "[timeBatdau]," +
                                "[timeKetthuc]," +
                                "[timeDV]," +
                                "[LidoDV]," +
                                "[MaBaocaongay]," +
                                "[Oxanhla]," +
                                "[Odo]," +
                                "[Oxam]," +
                                "Ovang," +
                                "Onau," +
                                "TongO," +
                                "[TongoKs]," +
                                "[TongoDs]," +
                                "[DientichKS]," +
                                "DientichKTLai," +
                                "DTNNON," +
                                "DTON," +
                                "TinHieuTong," +
                                "TinHieuBMVN," +
                                "TinHieuKhac," +
                                "MatDoTHTong," +
                                "MatDoTHBMVN," +
                                "MatDoTHKhac," +
                                "[Doingoai]," +
                                "[Doitruongngoai]," +
                                "case when giamsat.nameId is null then cecm_reportdaily.GiamSat_other else giamsat.nameId end as TenGiamSat, " +
                                "case when chihuy.nameId is null then cecm_reportdaily.ChiHuyCT_other else chihuy.nameId end as TenChiHuy, " +
                                "[Thon] " +
                                "FROM [dbo].[cecm_reportdaily] " +
                                "left join cert_department d on d.id = cecm_reportdaily.DVKS_id " +
                                "left join cecm_programData on cecm_programData.id = cecm_reportdaily.Duan " +
                                "left join cecm_provinces as Tinh on cecm_reportdaily.Tinh = Tinh.id " +
                                "left join cecm_provinces as Huyen on cecm_reportdaily.Huyen = Huyen.id " +
                                "left join cecm_provinces as Xa on cecm_reportdaily.Xa = Xa.id " +
                                "left join cert_command_person on cecm_reportdaily.Doiso = cert_command_person.id " +
                                "left join Cecm_ProgramStaff giamsat on cecm_reportdaily.GiamSat_id = giamsat.id " +
                                "left join Cecm_ProgramStaff chihuy on cecm_reportdaily.ChiHuyCT_id = chihuy.id " +
                                "where cecm_reportdaily.id = {0}", id_kqks), cn);
                            //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                            SqlCommandBuilder sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                            sqlAdapter.Fill(datatable);
                            marker.AddVariable("obj", datatable);
                            if (datatable.Rows.Count != 0)
                            {
                                DataRow dr = datatable.Rows[0];
                                marker.AddVariable("MaBaocaongay", "Số: " + dr["MaBaocaongay"].ToString() + "/BC-KSN");
                                marker.AddVariable("MaBaocaongay_short", dr["MaBaocaongay"].ToString());
                                DateTime now = DateTime.Now;
                                marker.AddVariable("Ngaybaocao", dr["Tinh"].ToString() + ", ngày " + now.Day + " tháng " + now.Month + " năm " + now.Year);
                                if (DateTime.TryParse(dr["Ngaytao"].ToString(), out DateTime Ngaytao))
                                {
                                    marker.AddVariable("Ngaytao", Ngaytao.Day + "/" + Ngaytao.Month + "/" + Ngaytao.Year);
                                }
                                else
                                {
                                    marker.AddVariable("Ngaytao", "");
                                }
                                marker.AddVariable("TenDA", "Dự án: " + dr["TenDA"].ToString());
                                marker.AddVariable("MaNV", dr["MaNV"].ToString());
                                marker.AddVariable("Doiso", string.IsNullOrEmpty(dr["Doiso"].ToString()) ? dr["Doingoai"].ToString() : dr["Doiso"].ToString());
                                marker.AddVariable("Doitruong", string.IsNullOrEmpty(dr["Doitruong"].ToString()) ? dr["Doitruongngoai"].ToString() : dr["Doitruong"].ToString());
                                marker.AddVariable("Quanso", dr["Quanso"].ToString());
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
                                if (DateTime.TryParse(dr["timeDV"].ToString(), out DateTime timeDV))
                                {
                                    marker.AddVariable("timeDV", timeDV.Day + "/" + timeDV.Month + "/" + timeDV.Year);
                                }
                                else
                                {
                                    marker.AddVariable("timeDV", "");
                                }
                                marker.AddVariable("LidoDV", dr["LidoDV"].ToString());
                                marker.AddVariable("Thon", dr["Thon"].ToString());
                                marker.AddVariable("Xa", dr["Xa"].ToString());
                                marker.AddVariable("Huyen", dr["Huyen"].ToString());
                                marker.AddVariable("Tinh", dr["Tinh"].ToString());

                                marker.AddVariable("Oxanhla", dr["Oxanhla"].ToString());
                                marker.AddVariable("Odo", dr["Odo"].ToString());
                                marker.AddVariable("Oxam", dr["Oxam"].ToString());
                                marker.AddVariable("Ovang", dr["Ovang"].ToString());
                                marker.AddVariable("Onau", dr["Onau"].ToString());
                                marker.AddVariable("TongoDs", dr["TongoDs"].ToString());
                                marker.AddVariable("TongoKs", dr["TongoKs"].ToString());
                                marker.AddVariable("DientichKS", dr["DientichKS"].ToString());
                                marker.AddVariable("DientichKTLai", dr["DientichKTLai"].ToString());

                                System.Data.DataTable tableKQKS = new System.Data.DataTable();
                                SqlCommandBuilder sqlCommand2 = null;
                                SqlDataAdapter sqlAdapter2 = null;
                                sqlAdapter2 = new SqlDataAdapter(string.Format(
                                    "Select " +
                                    "tbl.*, " +
                                    "mst.dvs_name as KetquaST, " +
                                    "ol.o_id as ol_idST " +
                                    "FROM cecm_reportdaily_KQKS tbl " +
                                    "left join mst_division mst on tbl.Ketqua = mst.dvs_value and mst.dvs_group_cd = '002' " +
                                    "left join OLuoi ol on tbl.ol_id = ol.gid " +
                                    "WHERE tbl.cecm_reportdaily_id = {0}"
                                    , id_kqks), cn);
                                sqlCommand2 = new SqlCommandBuilder(sqlAdapter2);
                                sqlAdapter2.Fill(tableKQKS);
                                marker.AddVariable("KQKS", tableKQKS);
                                marker.AddVariable("KQKSCount", "*Tổng số ô: " + tableKQKS.Rows.Count);

                                SqlCommandBuilder sqlCommand3 = null;
                                SqlDataAdapter sqlAdapter3= null;
                                System.Data.DataTable tableBMVN = new System.Data.DataTable();
                                string sql =
                                   "Select " +
                                   "tbl.*, " +
                                   "mst.dvs_name as TinhtrangST, " +
                                   "ol.o_id as ol_idST " +
                                   "FROM cecm_reportdaily_BMVN tbl " +
                                   "left join mst_division mst on tbl.Tinhtrang = mst.dvs_value and mst.dvs_group_cd = '003' " +
                                   "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                                   "WHERE tbl.cecm_reportdaily_id = " + id_kqks;
                                sqlAdapter3 = new SqlDataAdapter(sql, cn);
                                sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                                sqlAdapter3.Fill(tableBMVN);
                                marker.AddVariable("BMVN", tableBMVN);

                                marker.AddVariable("BMVNCount", tableBMVN.Rows.Count);
                                marker.AddVariable("BMVNCount_long", "Tổng số vật nổ tìm thấy: " + tableBMVN.Rows.Count);
                                int daXuLy = 0;
                                int huyTaiCho = 0;
                                int thuGom = 0;
                                foreach(DataRow drBMVN in tableBMVN.Rows)
                                {
                                    if(drBMVN["Tinhtrang"].ToString() == "1")
                                    {
                                        daXuLy++;
                                        if (drBMVN["PPXuLy"].ToString() == "1")
                                        {
                                            huyTaiCho++;
                                        }
                                        if (drBMVN["PPXuLy"].ToString() == "2")
                                        {
                                            thuGom++;
                                        }
                                    }
                                    
                                }
                                marker.AddVariable("daXuLy", daXuLy);
                                marker.AddVariable("huyTaiCho", huyTaiCho);
                                marker.AddVariable("thuGom", thuGom);

                                //marker.AddVariable("Thon", dr["Thon"].ToString());
                                //marker.AddVariable("Xa", dr["Xa"].ToString());
                                //marker.AddVariable("Huyen", dr["Huyen"].ToString());
                                //marker.AddVariable("Tinh", dr["Tinh"].ToString());

                                //foreach (DataRow dr in datatable.Rows)
                                //{
                                //    txtHopphan.Text = dr["Hopphan"].ToString();
                                //    timeNgaytao.Text = dr["Ngaytao"].ToString();
                                //    txtMaNV.Text = dr["MaNV"].ToString();
                                //    cbb_Doiso.Text = dr["Doiso"].ToString();
                                //    txtDoitruong.Text = dr["Doitruong"].ToString();
                                //    txtQuanso.Text = dr["Quanso"].ToString();
                                //    TimeBD.Text = dr["timeBatdau"].ToString();
                                //    TimeKT.Text = dr["timeKetthuc"].ToString();
                                //    TimeDV.Text = dr["timeDV"].ToString();
                                //    txtLydoDV.Text = dr["LidoDV"].ToString();
                                //    txtMaBCngay.Text = dr["MaBaocaongay"].ToString();
                                //    nudXanhla.Value = int.Parse(dr["Oxanhla"].ToString());
                                //    nudDo.Value = int.Parse(dr["Odo"].ToString());
                                //    nudXam.Value = int.Parse(dr["Oxam"].ToString());
                                //    nudTongKS.Value = int.Parse(dr["TongoKs"].ToString());
                                //    nudDS.Value = int.Parse(dr["TongoDs"].ToString());
                                //    nudDientichKS.Value = int.Parse(dr["DientichKS"].ToString());
                                //    if (long.TryParse(dr["Duan"].ToString(), out long Duan))
                                //    {
                                //        comboBox_TenDA.SelectedValue = Duan;
                                //    }

                                //    comboBox_Tinh.Text = dr["Tinh"].ToString();
                                //    comboBox_Huyen.Text = dr["Huyen"].ToString();
                                //    comboBox_Xa.Text = dr["Xa"].ToString();
                                //    TinhId = int.Parse(dr["Tinhid"].ToString());
                                //    //DuanId = int.Parse(dr["Duanid"].ToString());
                                //    HuyenId = int.Parse(dr["Huyenid"].ToString());
                                //    XaId = int.Parse(dr["Xaid"].ToString());
                                //    PersonId = dr["Doisoid"].ToString();
                                //    txtDoingoai.Text = dr["Doingoai"].ToString();
                                //    TxtDoitruongngoai.Text = dr["Doitruongngoai"].ToString();
                                //    txtThon.Text = dr["Thon"].ToString();

                                //}
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
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
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

        private void BtnDong_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
            //    return;
            this.Close();
        }
        private void LoadData()
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                //var date1 = date11.Split('/')[1] + "-" + date11.Split('/')[0] + "-" + date11.Split('/')[2];
                //var date2 = date22.Split('/')[1] + "-" + date22.Split('/')[0] + "-" + date22.Split('/')[2];
                dataGridView1.Rows.Clear();

                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql = 
                    @"select DA.id as 'ID', cecm_programData.name as 'Dự án', CONCAT(FORMAT(cecm_programData.startTime, 'dd/MM/yyyy') ,' - ',FORMAT(cecm_programData.endTime, 'dd/MM/yyyy')) as 'Thời gian', CONCAT(Xa.Ten, ',', Huyen.Ten,',',Tinh.Ten) as 'Địa điểm' 
                    from cecm_reportdaily as DA
                    left join cecm_provinces Tinh on Tinh.id = DA.Tinh 
                    left join cecm_provinces Huyen on Huyen.id = DA.Huyen
                    left join cecm_provinces Xa on Xa.id = DA.Xa
                    left join cecm_programData on cecm_programData.id = DA.Duan 
                    WHERE 1 = 1 ";
                if (!string.IsNullOrEmpty(TxtTImkiem.Text))
                {
                    sql += " AND LOWER(cecm_programData.name) LIKE @programName ";
                }
                if (timeNgaybatdau.Checked)
                {
                    sql += " AND cecm_programData.startTime >=  @date1 ";
                }
                if (timeNgayketthuc.Checked)
                {
                    sql += " AND cecm_programData.endTime <= @date2 ";
                }
                sql += " ORDER BY DA.id ";
                sqlAdapter = new SqlDataAdapter(sql, cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapter);
                if (!string.IsNullOrEmpty(TxtTImkiem.Text))
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "programName",
                        Value = "%" + TxtTImkiem.Text.ToLower() + "%",
                        SqlDbType = SqlDbType.NVarChar
                    });
                }
                if (timeNgaybatdau.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "date1",
                        Value = timeNgaybatdau.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }
                if (timeNgayketthuc.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "date2",
                        Value = timeNgayketthuc.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }
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
    }
}
