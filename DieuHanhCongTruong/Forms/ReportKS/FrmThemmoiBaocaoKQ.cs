using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class FrmThemmoiBaocaoKQ : Form
    {
        //public SqlConnection _cn.Connection as SqlConnection = null;
        private ConnectionWithExtraInfo _cn = null;
        public int id_BSKQ = 0;
        public int DuanId = -1;
        //public int TinhId = 0;
        //public int HuyenId=0;
        //public int XaId = 0;
        public string PersonId = "0";
        private bool loading = false;

        public FrmThemmoiBaocaoKQ(int i)
        {
            id_BSKQ = i;
            //_cn.Connection as SqlConnection = _cn.Connection as SqlConnection = frmLoggin.sqlCon;
            _cn = UtilsDatabase._ExtraInfoConnettion;
            InitializeComponent();
        }
        private void tbCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private bool UpdateInfomation(int dem)
        {
            try
            {
                //SqlCommandBuilder sqlCommand = null;
                //SqlDataAdapter sqlAdapter = null;
                //DataTable datatable = new DataTable();
                //sqlAdapter = new SqlDataAdapter(String.Format("USE [{0}] SELECT cecm_user.user_name FROM cecm_user where user_name = '{1}'", databaseName, tbTenDangNhap.Text), cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapter);
                //sqlAdapter.Fill(datatable);
                
                if (dem != 0)
                {
                    SqlCommand cmd2 = new SqlCommand(
                        "UPDATE [dbo].[cecm_reportdaily] SET " +
                        "[Duan] = @Duan," +
                        "[DVKS_id] = @DVKS_id," +
                        "[Hopphan] = @Hopphan," +
                        "[Tinh] = @Tinh," +
                        "[Huyen] = @Huyen," +
                        "[Xa] = @Xa," +
                        "[Ngaytao] = @Ngaytao," +
                        "[MaNV] = @MaNV," +
                        "[Doiso] =@Doiso," +
                        "[Doitruong] = @Doitruong," +
                        "[Quanso] = @Quanso," +
                        "[timeBatdau] = @timeBatdau," +
                        "[timeKetthuc] = @timeKetthuc," +
                        "[timeDV] = @timeDV," +
                        "[LidoDV] = @LidoDV," +
                        "[MaBaocaongay] =@MaBaocaongay," +
                        "[Oxanhla] = @Oxanhla," +
                        "[Odo] = @Odo," +
                        "[Oxam] = @Oxam," +
                        "[Ovang] = @Ovang," +
                        "[Onau] = @Onau," +
                        "[TongO] = @TongO," +
                        "[TongoKs] = @TongoKs," +
                        "[TongoDs] = @TongoDs," +
                        "[DientichKS] = @DientichKS, " +
                        "[DientichKTLai] = @DientichKTLai, " +
                        "[DTNNON] = @DTNNON, " +
                        "[DTON] = @DTON, " +
                        "[TinHieuTong] = @TinHieuTong, " +
                        "[TinHieuBMVN] = @TinHieuBMVN, " +
                        "[TinHieuKhac] = @TinHieuKhac, " +
                        "[MatDoTHTong] = @MatDoTHTong, " +
                        "[MatDoTHBMVN] = @MatDoTHBMVN, " +
                        "[MatDoTHKhac] = @MatDoTHKhac, " +
                        "[Doingoai] = @Doingoai, " +
                        "[GiamSat_id] = @GiamSat_id, " +
                        "[ChiHuyCT_id] = @ChiHuyCT_id, " +
                        "[GiamSat_other] = @GiamSat_other, " +
                        "[ChiHuyCT_other] = @ChiHuyCT_other, " +
                        "[Doitruongngoai]=@Doitruongngoai, " +
                        "[Thon]=@Thon " +
                        "WHERE cecm_reportdaily.id = " + dem, _cn.Connection as SqlConnection);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;

                    SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.BigInt);
                    Duan.Value = comboBox_TenDA.SelectedValue;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                    DVKS_id.Value = cbDVKS.SelectedValue;
                    cmd2.Parameters.Add(DVKS_id);

                    SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 255);
                    Hopphan.Value = txtHopphan.Text;
                    cmd2.Parameters.Add(Hopphan);

                    SqlParameter Tinh = new SqlParameter("@Tinh", SqlDbType.Int);
                    Tinh.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(Tinh);

                    SqlParameter Huyen = new SqlParameter("@Huyen", SqlDbType.Int);
                    Huyen.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(Huyen);

                    SqlParameter Xa = new SqlParameter("@Xa", SqlDbType.Int);
                    Xa.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(Xa);

                    SqlParameter Ngaytao = new SqlParameter("@Ngaytao", SqlDbType.DateTime);
                    Ngaytao.Value = timeNgaytao.Value;
                    cmd2.Parameters.Add(Ngaytao);

                    // Ngay Bat dau
                    SqlParameter MaNV = new SqlParameter("@MaNV", SqlDbType.NVarChar, 50);
                    MaNV.Value = txtMaNV.Text;
                    cmd2.Parameters.Add(MaNV);

                    SqlParameter Doiso = new SqlParameter("@Doiso", SqlDbType.NVarChar, 50);
                    Doiso.Value = cbb_Doiso.SelectedValue;
                    cmd2.Parameters.Add(Doiso);

                    SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 50);
                    Doitruong.Value = txtDoitruong.Text;
                    cmd2.Parameters.Add(Doitruong);

                    SqlParameter Quanso = new SqlParameter("@Quanso", SqlDbType.Int);
                    Quanso.Value = nudQuanSo.Value;
                    cmd2.Parameters.Add(Quanso);

                    SqlParameter timeBatdau = new SqlParameter("@timeBatdau", SqlDbType.DateTime);
                    timeBatdau.Value = TimeBD.Value;
                    cmd2.Parameters.Add(timeBatdau);

                    SqlParameter timeKetthuc = new SqlParameter("@timeKetthuc", SqlDbType.DateTime);
                    timeKetthuc.Value = TimeKT.Value;
                    cmd2.Parameters.Add(timeKetthuc);

                    SqlParameter timeDV = new SqlParameter("@timeDV", SqlDbType.DateTime);
                    timeDV.Value = TimeDV.Value;
                    cmd2.Parameters.Add(timeDV);

                    SqlParameter LidoDV = new SqlParameter("@LidoDV", SqlDbType.NVarChar, 255);
                    LidoDV.Value = txtLydoDV.Text;
                    cmd2.Parameters.Add(LidoDV);

                    SqlParameter MaBaocaongay = new SqlParameter("@MaBaocaongay", SqlDbType.NVarChar, 255);
                    MaBaocaongay.Value = txtMaBCngay.Text;
                    cmd2.Parameters.Add(MaBaocaongay);

                    SqlParameter Oxanhla = new SqlParameter("@Oxanhla", SqlDbType.Int);
                    Oxanhla.Value = nudXanhla.Value;
                    cmd2.Parameters.Add(Oxanhla);

                    SqlParameter Odo = new SqlParameter("@Odo", SqlDbType.Int);
                    Odo.Value = nudDo.Value;
                    cmd2.Parameters.Add(Odo);

                    SqlParameter Oxam = new SqlParameter("@Oxam", SqlDbType.Int);
                    Oxam.Value = nudXam.Value;
                    cmd2.Parameters.Add(Oxam);

                    SqlParameter Ovang = new SqlParameter("@Ovang", SqlDbType.Int);
                    Ovang.Value = nudVang.Value;
                    cmd2.Parameters.Add(Ovang);

                    SqlParameter Onau = new SqlParameter("@Onau", SqlDbType.Int);
                    Onau.Value = nudNau.Value;
                    cmd2.Parameters.Add(Onau);

                    SqlParameter TongO = new SqlParameter("@TongO", SqlDbType.Int);
                    TongO.Value = nudTongO.Value;
                    cmd2.Parameters.Add(TongO);

                    SqlParameter TongoKs = new SqlParameter("@TongoKs", SqlDbType.Int);
                    TongoKs.Value = nudTongKS.Value;
                    cmd2.Parameters.Add(TongoKs);

                    SqlParameter TongoDs = new SqlParameter("@TongoDs", SqlDbType.Int);
                    TongoDs.Value = nudDS.Value;
                    cmd2.Parameters.Add(TongoDs);

                    SqlParameter DientichKS = new SqlParameter("@DientichKS", SqlDbType.Float);
                    DientichKS.Value = nudDientichKS.Value;
                    cmd2.Parameters.Add(DientichKS);

                    SqlParameter DientichKTLai = new SqlParameter("@DientichKTLai", SqlDbType.Float);
                    DientichKTLai.Value = nudDientichKTLai.Value;
                    cmd2.Parameters.Add(DientichKTLai);

                    SqlParameter DTNNON = new SqlParameter("@DTNNON", SqlDbType.Float);
                    DTNNON.Value = nudDTNNON.Value;
                    cmd2.Parameters.Add(DTNNON);

                    SqlParameter DTON = new SqlParameter("@DTON", SqlDbType.Float);
                    DTON.Value = nudDTON.Value;
                    cmd2.Parameters.Add(DTON);

                    SqlParameter TinHieuTong = new SqlParameter("@TinHieuTong", SqlDbType.Int);
                    TinHieuTong.Value = nudTongTH.Value;
                    cmd2.Parameters.Add(TinHieuTong);

                    SqlParameter TinHieuBMVN = new SqlParameter("@TinHieuBMVN", SqlDbType.Int);
                    TinHieuBMVN.Value = nudTHBMVN.Value;
                    cmd2.Parameters.Add(TinHieuBMVN);

                    SqlParameter TinHieuKhac = new SqlParameter("@TinHieuKhac", SqlDbType.Int);
                    TinHieuKhac.Value = nudTHKhac.Value;
                    cmd2.Parameters.Add(TinHieuKhac);

                    SqlParameter MatDoTHTong = new SqlParameter("@MatDoTHTong", SqlDbType.Float);
                    MatDoTHTong.Value = nudMatDoTHTong.Value;
                    cmd2.Parameters.Add(MatDoTHTong);

                    SqlParameter MatDoTHBMVN = new SqlParameter("@MatDoTHBMVN", SqlDbType.Float);
                    MatDoTHBMVN.Value = nudMatDoTHBMVN.Value;
                    cmd2.Parameters.Add(MatDoTHBMVN);

                    SqlParameter MatDoTHKhac = new SqlParameter("@MatDoTHKhac", SqlDbType.Float);
                    MatDoTHKhac.Value = nudMatDoTHKhac.Value;
                    cmd2.Parameters.Add(MatDoTHKhac);

                    SqlParameter Doingoai = new SqlParameter("@Doingoai", SqlDbType.NVarChar, 255);
                    Doingoai.Value = txtDoingoai.Text;
                    cmd2.Parameters.Add(Doingoai);

                    SqlParameter Doitruongngoai = new SqlParameter("@Doitruongngoai", SqlDbType.NVarChar, 255);
                    Doitruongngoai.Value = TxtDoitruongngoai.Text;
                    cmd2.Parameters.Add(Doitruongngoai);

                    SqlParameter GiamSat_id = new SqlParameter("@GiamSat_id", SqlDbType.BigInt);
                    GiamSat_id.Value = cbGiamSat.SelectedValue;
                    cmd2.Parameters.Add(GiamSat_id);

                    SqlParameter ChiHuyCT_id = new SqlParameter("@ChiHuyCT_id", SqlDbType.BigInt);
                    ChiHuyCT_id.Value = cbChiHuyCT.SelectedValue;
                    cmd2.Parameters.Add(ChiHuyCT_id);

                    SqlParameter GiamSat_other = new SqlParameter("@GiamSat_other", SqlDbType.NVarChar, 255);
                    GiamSat_other.Value = tbGiamSatOther.Text;
                    cmd2.Parameters.Add(GiamSat_other);

                    SqlParameter ChiHuyCT_other = new SqlParameter("@ChiHuyCT_other", SqlDbType.NVarChar, 255);
                    ChiHuyCT_other.Value = tbChiHuyCTOther.Text;
                    cmd2.Parameters.Add(ChiHuyCT_other);

                    SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 255);
                    Thon.Value = txtThon.Text;
                    cmd2.Parameters.Add(Thon);

                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();

                    if (temp > 0)
                    {
                        //MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                        //this.Close();
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                        //this.Close();
                        return false;
                    }
                }
                else
                {
                    
                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO cecm_reportdaily (Duan,DVKS_id,Hopphan,Tinh,Huyen,Xa,Ngaytao,MaNV,Doiso,Doitruong,Quanso,timeBatdau,timeKetthuc,timeDV,LidoDV,MaBaocaongay,Oxanhla,Odo,Oxam,Ovang,Onau,TongO,TongoKs,TongoDs,DientichKS,DientichKTLai,DTNNON,DTON,TinHieuTong,TinHieuBMVN,TinHieuKhac,MatDoTHTong,MatDoTHBMVN,MatDoTHKhac,GiamSat_id,ChiHuyCT_id,GiamSat_other,ChiHuyCT_other, Doingoai, Doitruongngoai, Thon) " +
                        "VALUES(@Duan, @DVKS_id, @Hopphan, @Tinh, @Huyen, @Xa, @Ngaytao, @MaNV, @Doiso, @Doitruong, @Quanso, @timeBatdau, @timeKetthuc, @timeDV, @LidoDV, @MaBaocaongay, @Oxanhla, @Odo, @Oxam,@Ovang,@Onau,@TongO, @TongoKs, @TongoDs, @DientichKS,@DientichKTLai,@DTNNON,@DTON,@TinHieuTong,@TinHieuBMVN,@TinHieuKhac,@MatDoTHTong,@MatDoTHBMVN,@MatDoTHKhac,@GiamSat_id,@ChiHuyCT_id,@GiamSat_other,@ChiHuyCT_other, @Doingoai, @Doitruongngoai, @Thon)", _cn.Connection as SqlConnection);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;

                    SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.BigInt);
                    Duan.Value = comboBox_TenDA.SelectedValue;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                    DVKS_id.Value = cbDVKS.SelectedValue;
                    cmd2.Parameters.Add(DVKS_id);

                    SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 255);
                    Hopphan.Value = txtHopphan.Text;
                    cmd2.Parameters.Add(Hopphan);

                    SqlParameter Tinh = new SqlParameter("@Tinh", SqlDbType.Int);
                    Tinh.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(Tinh);

                    SqlParameter Huyen = new SqlParameter("@Huyen", SqlDbType.Int);
                    Huyen.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(Huyen);

                    SqlParameter Xa = new SqlParameter("@Xa", SqlDbType.Int);
                    Xa.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(Xa);

                    SqlParameter Ngaytao = new SqlParameter("@Ngaytao", SqlDbType.DateTime);
                    Ngaytao.Value = timeNgaytao.Value;
                    cmd2.Parameters.Add(Ngaytao);

                    // Ngay Bat dau
                    SqlParameter MaNV = new SqlParameter("@MaNV", SqlDbType.NVarChar, 50);
                    MaNV.Value = txtMaNV.Text;
                    cmd2.Parameters.Add(MaNV);

                    SqlParameter Doiso = new SqlParameter("@Doiso", SqlDbType.NVarChar, 50);
                    Doiso.Value = cbb_Doiso.SelectedValue;
                    cmd2.Parameters.Add(Doiso);

                    SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 50);
                    Doitruong.Value = txtDoitruong.Text;
                    cmd2.Parameters.Add(Doitruong);

                    SqlParameter Quanso = new SqlParameter("@Quanso", SqlDbType.Int);
                    Quanso.Value = nudQuanSo.Value;
                    cmd2.Parameters.Add(Quanso);

                    SqlParameter timeBatdau = new SqlParameter("@timeBatdau", SqlDbType.DateTime);
                    timeBatdau.Value = TimeBD.Value;
                    cmd2.Parameters.Add(timeBatdau);

                    SqlParameter timeKetthuc = new SqlParameter("@timeKetthuc", SqlDbType.DateTime);
                    timeKetthuc.Value = TimeKT.Value;
                    cmd2.Parameters.Add(timeKetthuc);

                    SqlParameter timeDV = new SqlParameter("@timeDV", SqlDbType.DateTime);
                    timeDV.Value = TimeDV.Value;
                    cmd2.Parameters.Add(timeDV);

                    SqlParameter LidoDV = new SqlParameter("@LidoDV", SqlDbType.NVarChar, 255);
                    LidoDV.Value = txtLydoDV.Text;
                    cmd2.Parameters.Add(LidoDV);

                    SqlParameter MaBaocaongay = new SqlParameter("@MaBaocaongay", SqlDbType.NVarChar, 255);
                    MaBaocaongay.Value = txtMaBCngay.Text;
                    cmd2.Parameters.Add(MaBaocaongay);

                    SqlParameter Oxanhla = new SqlParameter("@Oxanhla", SqlDbType.Int);
                    Oxanhla.Value = nudXanhla.Value;
                    cmd2.Parameters.Add(Oxanhla);

                    SqlParameter Odo = new SqlParameter("@Odo", SqlDbType.Int);
                    Odo.Value = nudDo.Value;
                    cmd2.Parameters.Add(Odo);

                    SqlParameter Oxam = new SqlParameter("@Oxam", SqlDbType.Int);
                    Oxam.Value = nudXam.Value;
                    cmd2.Parameters.Add(Oxam);

                    SqlParameter Ovang = new SqlParameter("@Ovang", SqlDbType.Int);
                    Ovang.Value = nudVang.Value;
                    cmd2.Parameters.Add(Ovang);

                    SqlParameter Onau = new SqlParameter("@Onau", SqlDbType.Int);
                    Onau.Value = nudNau.Value;
                    cmd2.Parameters.Add(Onau);

                    SqlParameter TongO = new SqlParameter("@TongO", SqlDbType.Int);
                    TongO.Value = nudTongO.Value;
                    cmd2.Parameters.Add(TongO);

                    SqlParameter TongoKs = new SqlParameter("@TongoKs", SqlDbType.Int);
                    TongoKs.Value = nudTongKS.Value;
                    cmd2.Parameters.Add(TongoKs);

                    SqlParameter TongoDs = new SqlParameter("@TongoDs", SqlDbType.Int);
                    TongoDs.Value = nudDS.Value;
                    cmd2.Parameters.Add(TongoDs);

                    SqlParameter DientichKS = new SqlParameter("@DientichKS", SqlDbType.Float);
                    DientichKS.Value = nudDientichKS.Value;
                    cmd2.Parameters.Add(DientichKS);

                    SqlParameter DientichKTLai = new SqlParameter("@DientichKTLai", SqlDbType.Float);
                    DientichKTLai.Value = nudDientichKTLai.Value;
                    cmd2.Parameters.Add(DientichKTLai);

                    SqlParameter DTNNON = new SqlParameter("@DTNNON", SqlDbType.Float);
                    DTNNON.Value = nudDTNNON.Value;
                    cmd2.Parameters.Add(DTNNON);

                    SqlParameter DTON = new SqlParameter("@DTON", SqlDbType.Float);
                    DTON.Value = nudDTON.Value;
                    cmd2.Parameters.Add(DTON);

                    SqlParameter TinHieuTong = new SqlParameter("@TinHieuTong", SqlDbType.Int);
                    TinHieuTong.Value = nudTongTH.Value;
                    cmd2.Parameters.Add(TinHieuTong);

                    SqlParameter TinHieuBMVN = new SqlParameter("@TinHieuBMVN", SqlDbType.Int);
                    TinHieuBMVN.Value = nudTHBMVN.Value;
                    cmd2.Parameters.Add(TinHieuBMVN);

                    SqlParameter TinHieuKhac = new SqlParameter("@TinHieuKhac", SqlDbType.Int);
                    TinHieuKhac.Value = nudTHKhac.Value;
                    cmd2.Parameters.Add(TinHieuKhac);

                    SqlParameter MatDoTHTong = new SqlParameter("@MatDoTHTong", SqlDbType.Float);
                    MatDoTHTong.Value = nudMatDoTHTong.Value;
                    cmd2.Parameters.Add(MatDoTHTong);

                    SqlParameter MatDoTHBMVN = new SqlParameter("@MatDoTHBMVN", SqlDbType.Float);
                    MatDoTHBMVN.Value = nudMatDoTHBMVN.Value;
                    cmd2.Parameters.Add(MatDoTHBMVN);

                    SqlParameter MatDoTHKhac = new SqlParameter("@MatDoTHKhac", SqlDbType.Float);
                    MatDoTHKhac.Value = nudMatDoTHKhac.Value;
                    cmd2.Parameters.Add(MatDoTHKhac);

                    SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 255);
                    Thon.Value = txtThon.Text;
                    cmd2.Parameters.Add(Thon);

                    SqlParameter GiamSat_id = new SqlParameter("@GiamSat_id", SqlDbType.BigInt);
                    GiamSat_id.Value = cbGiamSat.SelectedValue;
                    cmd2.Parameters.Add(GiamSat_id);

                    SqlParameter ChiHuyCT_id = new SqlParameter("@ChiHuyCT_id", SqlDbType.BigInt);
                    ChiHuyCT_id.Value = cbChiHuyCT.SelectedValue;
                    cmd2.Parameters.Add(ChiHuyCT_id);

                    SqlParameter GiamSat_other = new SqlParameter("@GiamSat_other", SqlDbType.NVarChar, 255);
                    GiamSat_other.Value = tbGiamSatOther.Text;
                    cmd2.Parameters.Add(GiamSat_other);

                    SqlParameter ChiHuyCT_other = new SqlParameter("@ChiHuyCT_other", SqlDbType.NVarChar, 255);
                    ChiHuyCT_other.Value = tbChiHuyCTOther.Text;
                    cmd2.Parameters.Add(ChiHuyCT_other);

                    SqlParameter Doingoai = new SqlParameter("@Doingoai", SqlDbType.NVarChar, 255);
                    Doingoai.Value = txtDoingoai.Text;
                    cmd2.Parameters.Add(Doingoai);

                    SqlParameter Doitruongngoai = new SqlParameter("@Doitruongngoai", SqlDbType.NVarChar, 255);
                    Doitruongngoai.Value = TxtDoitruongngoai.Text;
                    cmd2.Parameters.Add(Doitruongngoai);

                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();



                    if (temp > 0)
                    {
                        //_cn.Transaction.Commit();
                        //MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                        //this.Close();
                        return true;
                    }
                    else
                    {
                        //_cn.Transaction.Rollback();
                        //MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                        //this.Close();
                        return false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");

                return false;
            }
        }
        //private void LoadDataKQKS(int dem, int dem1)
        //{
        //    try
        //    {
        //        if (AppUtils.CheckLoggin() == false)
        //            return;
        //        dgvKQKS.Rows.Clear();

        //        System.Data.DataTable datatable = new System.Data.DataTable();
        //        SqlCommandBuilder sqlCommand = null;
        //        SqlDataAdapter sqlAdapter = null;
        //        sqlAdapter = new SqlDataAdapter(string.Format("select Cecm_TerrainRectangle.id as 'Id',Cecm_TerrainRectangle.code as 'Mã ô', Sotinhieu as 'Số tín hiệu', Ketqua as 'Kết quả' from Cecm_TerrainRectangle left join cecm_program_area_map on cecm_program_area_map.id = Cecm_TerrainRectangle.programId where cecm_program_area_map.cecm_program_id = {0} or cecm_program_area_map.cecm_program_id = {0}", dem, dem1), _cn.Connection as SqlConnection); 
        //        //sqlAdapter = new SqlDataAdapter(string.Format("SELECT gid, "), _cn.Connection as SqlConnection);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapter);
        //        sqlAdapter.Fill(datatable);                

        //        if (datatable.Rows.Count != 0)
        //        {
        //            int indexRow = 1;
        //            foreach (DataRow dr in datatable.Rows)
        //            {
        //                var idKqks = dr["Id"].ToString();
        //                var tenDuAn = dr["Mã ô"].ToString();
        //                var startTime = dr["Số tín hiệu"].ToString();
        //                var diaDiem = dr["Kết quả"].ToString();

        //                dgvKQKS.Rows.Add(indexRow, tenDuAn, startTime, diaDiem, Resources.Modify, Resources.DeleteRed);
        //                dgvKQKS.Rows[indexRow - 1].Tag = idKqks;

        //                indexRow++;
        //            }
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
        //        return;
        //    }
        //}


        private bool UpdateKQKS()
        {
            try
            {
                foreach (DataGridViewRow dataGridViewRow in dgvKQKS.Rows)
                {
                    CecmReportDailyKQKS kqks = dataGridViewRow.Tag as CecmReportDailyKQKS;
                    //if (id_kqks != 0),Dientichcaybui,Dientichcayto,MatdoTB,Sotinhieu=,Ghichu,Dientichtretruc,Matdothua,Matdoday,Matdo, DaxulyM2
                    //{
                    string sql =
                        "INSERT INTO cecm_reportdaily_KQKS" +
                        "(area_id, Ketqua, DientichchuaKS,Dientichcaybui,Dientichcayto,MatdoTB,Sotinhieu,Ghichu,Dientichtretruc,Matdothua,Matdoday,Matdo, DaxulyM2, ol_id, cecm_reportdaily_id) " +
                        "VALUES(@area_id, @Ketqua, @DientichchuaKS, @Dientichcaybui,@Dientichcayto,@MatdoTB,@Sotinhieu,@Ghichu,@Dientichtretruc,@Matdothua,@Matdoday,@Matdo,@DaxulyM2, @ol_id, @cecm_reportdaily_id)";
                    SqlCommand cmd2 = new SqlCommand(sql, _cn.Connection as SqlConnection);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;

                    SqlParameter area_id = new SqlParameter("@area_id", SqlDbType.BigInt);
                    //Vungcoso.Value = comboBox_Vungcoso.SelectedItem.ToString();
                    area_id.Value = kqks.area_id;
                    cmd2.Parameters.Add(area_id);

                    SqlParameter Ketqua = new SqlParameter("@Ketqua", SqlDbType.BigInt);
                    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                    Ketqua.Value = kqks.Ketqua;
                    cmd2.Parameters.Add(Ketqua);

                    SqlParameter DientichchuaKS = new SqlParameter("@DientichchuaKS", SqlDbType.Float);
                    DientichchuaKS.Value = kqks.DientichchuaKS;
                    cmd2.Parameters.Add(DientichchuaKS);

                    SqlParameter Dientichcaybui = new SqlParameter("@Dientichcaybui", SqlDbType.Float);
                    Dientichcaybui.Value = kqks.Dientichcaybui;
                    cmd2.Parameters.Add(Dientichcaybui);

                    SqlParameter Dientichcayto = new SqlParameter("@Dientichcayto", SqlDbType.Float);
                    Dientichcayto.Value = kqks.Dientichcayto;
                    cmd2.Parameters.Add(Dientichcayto);

                    SqlParameter MatdoTB = new SqlParameter("@MatdoTB", SqlDbType.Float);
                    MatdoTB.Value = kqks.MatdoTB;
                    cmd2.Parameters.Add(MatdoTB);

                    SqlParameter Sotinhieu = new SqlParameter("@Sotinhieu", SqlDbType.Int);
                    Sotinhieu.Value = kqks.Sotinhieu;
                    cmd2.Parameters.Add(Sotinhieu);

                    SqlParameter Ghichu = new SqlParameter("@Ghichu", SqlDbType.NVarChar, 100);
                    Ghichu.Value = kqks.Ghichu != null ? kqks.Ghichu : "";
                    cmd2.Parameters.Add(Ghichu);

                    SqlParameter Dientichtretruc = new SqlParameter("@Dientichtretruc", SqlDbType.Float);
                    Dientichtretruc.Value = kqks.Dientichtretruc;
                    cmd2.Parameters.Add(Dientichtretruc);

                    SqlParameter Matdothua = new SqlParameter("@Matdothua", SqlDbType.Float);
                    Matdothua.Value = kqks.Matdothua;
                    cmd2.Parameters.Add(Matdothua);

                    SqlParameter Matdoday = new SqlParameter("@Matdoday", SqlDbType.Float);
                    Matdoday.Value = kqks.Matdoday;
                    cmd2.Parameters.Add(Matdoday);

                    SqlParameter DaxulyM2 = new SqlParameter("@DaxulyM2", SqlDbType.Float);
                    DaxulyM2.Value = kqks.DaxulyM2;
                    cmd2.Parameters.Add(DaxulyM2);

                    SqlParameter ol_id = new SqlParameter("@ol_id", SqlDbType.BigInt);
                    ol_id.Value = kqks.ol_id;
                    cmd2.Parameters.Add(ol_id);

                    SqlParameter cecm_reportdaily_id = new SqlParameter("@cecm_reportdaily_id", SqlDbType.BigInt);
                    cecm_reportdaily_id.Value = id_BSKQ;
                    cmd2.Parameters.Add(cecm_reportdaily_id);

                    SqlParameter Matdo = new SqlParameter("@Matdo", SqlDbType.NVarChar, 50);
                    Matdo.Value = kqks.Matdo != null ? kqks.Matdo : "";
                    cmd2.Parameters.Add(Matdo);

                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
            
        }

        private bool UpdateBMVN()
        {
            try
            {
                foreach (DataGridViewRow dataGridViewRow in dgvBMVN.Rows)
                {
                    CecmReportDailyBMVN bmvn = dataGridViewRow.Tag as CecmReportDailyBMVN;
                    //if (id_kqks != 0),Dientichcaybui,Dientichcayto,MatdoTB,Sotinhieu=,Ghichu,Dientichtretruc,Matdothua,Matdoday,Matdo, DaxulyM2
                    //{
                    string sql =
                        "INSERT INTO cecm_reportdaily_BMVN" +
                        "(Kyhieu, Loai, idRectangle,SL,Tinhtrang, PPXuLy, Vido,Kinhdo,Deep,Kichthuoc,Dai,Rong, cecm_reportdaily_id) " +
                        "VALUES(@Kyhieu, @Loai, @idRectangle, @SL,@Tinhtrang, @PPXuLy, @Vido,@Kinhdo,@Deep,@Kichthuoc,@Dai,@Rong, @cecm_reportdaily_id)";
                    SqlCommand cmd2 = new SqlCommand(sql, _cn.Connection as SqlConnection);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;

                    SqlParameter Kyhieu = new SqlParameter("@Kyhieu", SqlDbType.NVarChar, 50);
                    //Vungcoso.Value = comboBox_Vungcoso.SelectedItem.ToString();
                    Kyhieu.Value = bmvn.Kyhieu != null ? bmvn.Kyhieu : "";
                    cmd2.Parameters.Add(Kyhieu);

                    SqlParameter Loai = new SqlParameter("@Loai", SqlDbType.NVarChar, 50);
                    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                    Loai.Value = bmvn.Loai != null ? bmvn.Loai : "";
                    cmd2.Parameters.Add(Loai);

                    SqlParameter idRectangle = new SqlParameter("@idRectangle", SqlDbType.BigInt);
                    idRectangle.Value = bmvn.idRectangle;
                    cmd2.Parameters.Add(idRectangle);

                    SqlParameter SL = new SqlParameter("@SL", SqlDbType.Int);
                    SL.Value = bmvn.SL;
                    cmd2.Parameters.Add(SL);

                    SqlParameter Tinhtrang = new SqlParameter("@Tinhtrang", SqlDbType.NVarChar, 50);
                    Tinhtrang.Value = bmvn.Tinhtrang;
                    cmd2.Parameters.Add(Tinhtrang);

                    SqlParameter PPXuLy = new SqlParameter("@PPXuLy", SqlDbType.BigInt);
                    PPXuLy.Value = bmvn.PPXuLy;
                    cmd2.Parameters.Add(PPXuLy);

                    SqlParameter Vido = new SqlParameter("@Vido", SqlDbType.Float);
                    Vido.Value = bmvn.Vido;
                    cmd2.Parameters.Add(Vido);

                    SqlParameter Kinhdo = new SqlParameter("@Kinhdo", SqlDbType.Float);
                    Kinhdo.Value = bmvn.Kinhdo;
                    cmd2.Parameters.Add(Kinhdo);

                    SqlParameter Deep = new SqlParameter("@Deep", SqlDbType.Float);
                    Deep.Value = bmvn.Deep;
                    cmd2.Parameters.Add(Deep);

                    SqlParameter Kichthuoc = new SqlParameter("@Kichthuoc", SqlDbType.Float);
                    Kichthuoc.Value = bmvn.Kichthuoc;
                    cmd2.Parameters.Add(Kichthuoc);

                    SqlParameter Dai = new SqlParameter("@Dai", SqlDbType.Float);
                    Dai.Value = bmvn.Dai;
                    cmd2.Parameters.Add(Dai);

                    SqlParameter Rong = new SqlParameter("@Rong", SqlDbType.Float);
                    Rong.Value = bmvn.Rong;
                    cmd2.Parameters.Add(Rong);

                    SqlParameter cecm_reportdaily_id = new SqlParameter("@cecm_reportdaily_id", SqlDbType.BigInt);
                    cecm_reportdaily_id.Value = id_BSKQ;
                    cmd2.Parameters.Add(cecm_reportdaily_id);

                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }

        private void LoadDataKQKS()
        {
            try
            {
                dgvKQKS.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                sqlAdapter = new SqlDataAdapter(string.Format(
                    "Select " +
                    "tbl.*, " +
                    "mst.dvs_name as KetquaST, " +
                    "ol.o_id as ol_idST " +
                    "FROM cecm_reportdaily_KQKS tbl " +
                    "left join mst_division mst on tbl.Ketqua = mst.dvs_value and mst.dvs_group_cd = '002' " +
                    "left join OLuoi ol on tbl.ol_id = ol.gid " +
                    "WHERE tbl.cecm_reportdaily_id = {0}"
                    , id_BSKQ), _cn.Connection as SqlConnection);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 0;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        //var idKqks = dr["Id"].ToString();
                        //var tenDuAn = dr["Mã ô"].ToString();
                        //var startTime = dr["Số tín hiệu"].ToString();
                        //var diaDiem = dr["Kết quả"].ToString();

                        CecmReportDailyKQKS kqks = new CecmReportDailyKQKS();
                        kqks.id = long.Parse(dr["id"].ToString());
                        kqks.area_id = long.Parse(dr["area_id"].ToString());
                        kqks.Ketqua = long.TryParse(dr["Ketqua"].ToString(), out long Ketqua) ? Ketqua : -1;
                        string KetquaST = dr["KetquaST"].ToString();
                        kqks.DientichchuaKS = double.Parse(dr["DientichchuaKS"].ToString());
                        kqks.ol_id = long.Parse(dr["ol_id"].ToString());
                        kqks.Dientichcaybui = double.Parse(dr["Dientichcaybui"].ToString());
                        kqks.Dientichcayto = double.Parse(dr["Dientichcayto"].ToString());
                        kqks.MatdoTB = double.Parse(dr["MatdoTB"].ToString());
                        kqks.Sotinhieu = int.Parse(dr["Sotinhieu"].ToString());
                        kqks.Ghichu = dr["Ghichu"].ToString();
                        kqks.Dientichtretruc = double.Parse(dr["Dientichtretruc"].ToString());
                        kqks.Matdothua = double.Parse(dr["Matdothua"].ToString());
                        kqks.Matdoday = double.Parse(dr["Matdoday"].ToString());
                        kqks.DaxulyM2 = double.Parse(dr["DaxulyM2"].ToString());
                        kqks.Matdo = dr["Matdo"].ToString();

                        dgvKQKS.Rows.Add(indexRow + 1, dr["ol_idST"].ToString(), kqks.Sotinhieu, KetquaST, Resources.Modify, Resources.DeleteRed);
                        dgvKQKS.Rows[indexRow].Tag = kqks;

                        indexRow++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void LoadDataKQKS_onChange_cbDA()
        {
            try
            {
                dgvKQKS.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                //sqlAdapter = new SqlDataAdapter(string.Format("select Cecm_TerrainRectangle.id as 'Id',Cecm_TerrainRectangle.code as 'Mã ô', Sotinhieu as 'Số tín hiệu', Ketqua as 'Kết quả' from Cecm_TerrainRectangle left join cecm_program_area_map on cecm_program_area_map.id = Cecm_TerrainRectangle.programId where cecm_program_area_map.cecm_program_id = {0} or cecm_program_area_map.cecm_program_id = {0}", dem, dem1), _cn.Connection as SqlConnection);
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT gid, o_id, cecm_program_areamap_id FROM OLuoi WHERE cecm_program_id = {0}", comboBox_TenDA.SelectedValue), _cn.Connection as SqlConnection);
                //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idOL = dr["gid"].ToString();
                        var code = dr["o_id"].ToString();
                        //var startTime = dr["Số tín hiệu"].ToString();
                        //var diaDiem = dr["Kết quả"].ToString();

                        //string sql =
                        //"INSERT INTO cecm_reportdaily_KQKS(ol_id) VALUES(" + idOL + ")";
                        
                        //SqlCommand cmd = new SqlCommand(sql, _cn.Connection as SqlConnection);
                        //cmd.Transaction = _cn.Transaction as SqlTransaction;
                        //cmd.ExecuteNonQuery();
                        //int gid = UtilsDatabase.GetLastIdIndentifyTable(_cn, "cecm_reportdaily_KQKS");

                        dgvKQKS.Rows.Add(indexRow, code);
                        CecmReportDailyKQKS kqks = new CecmReportDailyKQKS();
                        //kqks.id = gid;
                        kqks.code = code;
                        kqks.ol_id = long.Parse(idOL);
                        kqks.area_id = long.Parse(dr["cecm_program_areamap_id"].ToString());
                        dgvKQKS.Rows[indexRow - 1].Tag = kqks;


                        indexRow++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void LoadDataMine(int dem, int dem1)
        {
            try
            {
                dgvBMVN.Rows.Clear();

                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT Cecm_VNTerrainMinePoint.id as 'STT',Cecm_VNTerrainMinePoint.[Kyhieu] as 'Kí hiệu',Cecm_VNTerrainMinePoint.[Loai] as 'Loại',Cecm_TerrainRectangle.code as 'Mã ô',Cecm_VNTerrainMinePoint.[SL] as'SL',Cecm_VNTerrainMinePoint.Kinhdo as 'Kinh độ',Cecm_VNTerrainMinePoint.Vido as 'Vĩ độ',Deep as 'Độ sâu',Cecm_VNTerrainMinePoint.[Tinhtrang] as 'Tình trạng' FROM Cecm_VNTerrainMinePoint left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint.programId left join Cecm_TerrainRectangle on Cecm_TerrainRectangle.id = Cecm_VNTerrainMinePoint.idRectangle where idRectangle != -1 and (cecm_program_area_map.cecm_program_id = {0} or cecm_program_area_map.cecm_program_id = {1})", dem, dem1), _cn.Connection as SqlConnection);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                dgvBMVN.DataSource = null;

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idKqks = dr["STT"].ToString();
                        var row1 = dr["Kí hiệu"].ToString();
                        var row2 = dr["Loại"].ToString(); ;
                        var row3 = dr["Mã ô"].ToString();
                        var row4 = dr["SL"].ToString();
                        var row5 = dr["Kinh độ"].ToString();
                        var row6 = dr["Vĩ độ"].ToString();
                        var row7 = dr["Độ sâu"].ToString();
                        var row8 = dr["Tình trạng"].ToString();

                        dgvBMVN.Rows.Add(indexRow, row1, row2, row3, row4, row5, row6, row7, row8, Resources.Modify, Resources.DeleteRed);
                        dgvBMVN.Rows[indexRow - 1].Tag = idKqks;

                        indexRow++;
                    }

                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void LoadDataMine()
        {
            try
            {
                dgvBMVN.Rows.Clear();

                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql = 
                    "Select " +
                    "tbl.*, " +
                    "mst.dvs_name as TinhtrangST, " +
                    "ol.o_id as ol_idST " +
                    "FROM cecm_reportdaily_BMVN tbl " +
                    "left join mst_division mst on tbl.Tinhtrang = mst.dvs_value and mst.dvs_group_cd = '003' " +
                    "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                    "WHERE tbl.cecm_reportdaily_id = " + id_BSKQ;
                sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                dgvBMVN.DataSource = null;

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idKqks = indexRow;
                        var row1 = dr["Kyhieu"].ToString();
                        var row2 = dr["Loai"].ToString();
                        var row3 = dr["ol_idST"].ToString();
                        var row4 = dr["SL"].ToString();
                        var row5 = dr["Kinhdo"].ToString();
                        var row6 = dr["Vido"].ToString();
                        var row7 = dr["Deep"].ToString();
                        var row8 = dr["TinhtrangST"].ToString();

                        CecmReportDailyBMVN bmvn = new CecmReportDailyBMVN();
                        bmvn.Kyhieu = dr["Kyhieu"].ToString();
                        bmvn.Loai = dr["Loai"].ToString();
                        bmvn.idRectangle = long.Parse(dr["idRectangle"].ToString());
                        bmvn.SL = int.Parse(dr["SL"].ToString());
                        bmvn.Kinhdo = double.Parse(dr["Kinhdo"].ToString());
                        bmvn.Vido = double.Parse(dr["Vido"].ToString());
                        bmvn.Deep = double.Parse(dr["Deep"].ToString());
                        bmvn.Tinhtrang = long.TryParse(dr["Tinhtrang"].ToString(), out long Tinhtrang) ? Tinhtrang : -1;
                        bmvn.PPXuLy = long.TryParse(dr["PPXuLy"].ToString(), out long PPXuLy) ? PPXuLy : -1;
                        if (double.TryParse(dr["Kichthuoc"].ToString(), out double Kichthuoc))
                        {
                            bmvn.Kichthuoc = Kichthuoc;
                        }
                        if (double.TryParse(dr["Dai"].ToString(), out double Dai))
                        {
                            bmvn.Dai = Dai;
                        }
                        if (double.TryParse(dr["Rong"].ToString(), out double Rong))
                        {
                            bmvn.Rong = Rong;
                        }

                        dgvBMVN.Rows.Add(indexRow, row1, row2, row3, row4, row5, row6, row7, row8, Resources.Modify, Resources.DeleteRed);
                        dgvBMVN.Rows[indexRow - 1].Tag = bmvn;

                        indexRow++;
                    }

                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void LoadDataMine_onChange_cbDA()
        {
            try
            {
                dgvBMVN.Rows.Clear();

                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql =
                    "SELECT " +
                    "Cecm_VNTerrainMinePoint.id as 'STT'," +
                    "Cecm_VNTerrainMinePoint.[Kyhieu] as 'Kí hiệu'," +
                    "Cecm_VNTerrainMinePoint.[Loai] as 'Loại'," +
                    "Cecm_VNTerrainMinePoint.idRectangle as idRectangle," + 
                    "OLuoi.o_id as 'Mã ô'," +
                    "Cecm_VNTerrainMinePoint.[SL] as'SL'," +
                    "Cecm_VNTerrainMinePoint.Kinhdo as 'Kinh độ'," +
                    "Cecm_VNTerrainMinePoint.Vido as 'Vĩ độ'," +
                    "Deep as 'Độ sâu'," +
                    "Cecm_VNTerrainMinePoint.[Tinhtrang] as 'Tình trạng' " +
                    "FROM Cecm_VNTerrainMinePoint " +
                    //"left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint.programId " +
                    "left join OLuoi on OLuoi.gid = Cecm_VNTerrainMinePoint.idRectangle " +
                    "where 1 = 1 " +
                    "and Cecm_VNTerrainMinePoint.programId = @cecm_program_id ";
                    //"and idRectangle != -1 " +
                    //"and (cecm_program_area_map.cecm_program_id = @cecm_program_id " +
                    //"or cecm_program_area_map.cecm_program_id = @cecm_program_id) ";
                if (dtpStart.Checked)
                {
                    sql += "and Cecm_VNTerrainMinePoint.TimeExecute > @start_date ";
                }
                if (dtpEnd.Checked)
                {
                    sql += "and Cecm_VNTerrainMinePoint.TimeExecute < @end_date ";
                }
                sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
                sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "cecm_program_id",
                    Value = comboBox_TenDA.SelectedValue,
                    SqlDbType = SqlDbType.BigInt,
                });
                if (dtpStart.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "start_date",
                        Value = dtpStart.Value,
                        SqlDbType = SqlDbType.DateTime,
                    });
                }
                if (dtpEnd.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "end_date",
                        Value = dtpEnd.Value,
                        SqlDbType = SqlDbType.DateTime,
                    });
                }
                //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                dgvBMVN.DataSource = null;

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        
                        var idKqks = dr["STT"].ToString();
                        var row1 = dr["Kí hiệu"].ToString();
                        //var row2 = dr["Loại"].ToString();
                        var row2 = "";
                        var row3 = dr["Mã ô"].ToString();
                        var row4 = dr["SL"].ToString();
                        var row5 = dr["Kinh độ"].ToString();
                        var row6 = dr["Vĩ độ"].ToString();
                        var row7 = dr["Độ sâu"].ToString();
                        //var row8 = dr["Tình trạng"].ToString();
                        var row8 = "";

                        CecmReportDailyBMVN bmvn = new CecmReportDailyBMVN();
                        bmvn.Kyhieu = dr["Kí hiệu"].ToString();
                        //bmvn.Loai = dr["Loại"].ToString(); 
                        bmvn.idRectangle = long.Parse(dr["idRectangle"].ToString());
                        bmvn.Kinhdo = double.Parse(dr["Kinh độ"].ToString());
                        bmvn.Vido = double.Parse(dr["Vĩ độ"].ToString());
                        bmvn.Deep = double.Parse(dr["Độ sâu"].ToString());

                        dgvBMVN.Rows.Add(indexRow, row1, row2, row3, row4, row5, row6, row7, row8, Resources.Modify, Resources.DeleteRed);
                        dgvBMVN.Rows[indexRow - 1].Tag = bmvn;

                        indexRow++;
                    }

                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            //_cn.Transaction.Rollback();
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //if(txtXanhla.Text == "")
            //{
            //    i = 1;
            //}
            //if (txtDo.Text == "")
            //{
            //    i = 1;
            //}
            //if (txtXam.Text == "")
            //{
            //    i = 1;
            //}
            //if (txtKS.Text == "")
            //{
            //    i = 1;
            //}
            //if (txtDS.Text == "")
            //{
            //    i = 1;
            //}
            //if (txtDientichKS.Text == "")
            //{
            //    i = 1;
            //}
            //if(txtQuanso.Text == "")
            //{
            //    i = 1;
            //}
            //if(DuanId == -1)
            //{
            //    i = 1;
            //}
            //if(i==1)
            //{
            //    MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
            //}
            //else
            //{
            //if (comboBox_Huyen.Text == "" || comboBox_Tinh.Text == "" || comboBox_Xa.Text == "")
            //{
            //    MessageBox.Show("Vui lòng kiểm tra lại tỉnh huyện xã");
            //}
            //else
            //{
            //    bool success = UpdateInfomation(id_BSKQ);
            //}
            //}
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            _cn.BeginTransaction();
            bool success = UpdateInfomation(id_BSKQ);
            if(id_BSKQ <= 0)
            {
                id_BSKQ = UtilsDatabase.GetLastIdIndentifyTable(_cn, "cecm_reportdaily");
            }
            bool successDeleteSub = DeleteSubTable();
            bool successKQKS = UpdateKQKS();
            bool successBMVN = UpdateBMVN();
            if (success && successDeleteSub && successKQKS && successBMVN)
            {
                _cn.Transaction.Commit();
                MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                
            }
            else
            {
                _cn.Transaction.Rollback();
                MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
            }
            this.Close();
        }

        private void LoadCBDA()
        {
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT id, name FROM cecm_programData", _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["name"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_TenDA.DataSource = datatableProgram;
            comboBox_TenDA.ValueMember = "id";
            comboBox_TenDA.DisplayMember = "name";
        }

        private void LoadCBStaff()
        {
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT id, name FROM cert_command_person", _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["name"] = "Khác ...";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            cbb_Doiso.DataSource = datatableProgram;
            cbb_Doiso.ValueMember = "id";
            cbb_Doiso.DisplayMember = "name";
        }

        private void FrmThemmoiBaocaoKQ_Load(object sender, EventArgs e)
        {
            loading = true;
            //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT Ten FROM cecm_provinces where level = 1", _cn.Connection as SqlConnection);
            ////sqlAdapterProvince.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            //System.Data.DataTable datatableProvince = new System.Data.DataTable();
            //sqlAdapterProvince.Fill(datatableProvince);
            //comboBox_Tinh.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProvince.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
            //        continue;

            //    comboBox_Tinh.Items.Add(dr["Ten"].ToString());
            //}

            //SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter("SELECT [cert_command_person].* FROM [cert_command_person]", _cn.Connection as SqlConnection);
            ////sqlAdapterPerson.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            //System.Data.DataTable datatablePerson = new System.Data.DataTable();
            //sqlAdapterPerson.Fill(datatablePerson);
            //foreach (DataRow dr in datatablePerson.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    cbb_Doiso.Items.Add(dr["name"].ToString());
            //}
            //cbb_Doiso.Items.Add("Khác ...");
            LoadCBStaff();

            LoadCBDA();
            LoadCBTinh();

            if (id_BSKQ >= 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format(
                    "SELECT " +
                    "cecm_reportdaily.id, " +
                    "cecm_reportdaily.Duan as 'Duan'," +
                    "cecm_programData.id as 'Duanid'," +
                    "[Hopphan]," +
                    "Tinh.Ten as 'Tinh'," +
                    "Tinh.id as 'Tinhid'," +
                    "Huyen.Ten as 'Huyen'," +
                    "Huyen.id as 'Huyenid'," +
                    "Xa.Ten as 'Xa'," +
                    "Xa.id as 'Xaid'," +
                    "DVKS_id, " +
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
                    "GiamSat_id," +
                    "ChiHuyCT_id," +
                    "GiamSat_other," +
                    "ChiHuyCT_other," +
                    "[Thon] " +
                    "FROM [dbo].[cecm_reportdaily] " +
                    "left join cecm_programData on cecm_programData.id = cecm_reportdaily.Duan " +
                    "left join cecm_provinces as Tinh on cecm_reportdaily.Tinh = Tinh.id " +
                    "left join cecm_provinces as Huyen on cecm_reportdaily.Huyen = Huyen.id " +
                    "left join cecm_provinces as Xa on cecm_reportdaily.Xa = Xa.id " +
                    "left join cert_command_person on cecm_reportdaily.Doiso = cert_command_person.id " +
                    "where cecm_reportdaily.id = {0}", id_BSKQ), _cn.Connection as SqlConnection);
                //sqlAdapter.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        txtHopphan.Text = dr["Hopphan"].ToString();
                        timeNgaytao.Text = dr["Ngaytao"].ToString();
                        txtMaNV.Text = dr["MaNV"].ToString();
                        //cbb_Doiso.Text = dr["Doiso"].ToString();
                        
                        txtDoitruong.Text = dr["Doitruong"].ToString();
                        nudQuanSo.Value = decimal.Parse(dr["Quanso"].ToString());
                        TimeBD.Text = dr["timeBatdau"].ToString();
                        TimeKT.Text = dr["timeKetthuc"].ToString();
                        TimeDV.Text = dr["timeDV"].ToString();
                        txtLydoDV.Text = dr["LidoDV"].ToString();
                        txtMaBCngay.Text = dr["MaBaocaongay"].ToString();
                        nudXanhla.Value = int.Parse(dr["Oxanhla"].ToString());
                        nudDo.Value = int.Parse(dr["Odo"].ToString());
                        nudXam.Value = int.Parse(dr["Oxam"].ToString());
                        if(int.TryParse(dr["Ovang"].ToString(), out int Ovang))
                        {
                            nudVang.Value = Ovang;
                        }
                        if (int.TryParse(dr["Onau"].ToString(), out int Onau))
                        {
                            nudNau.Value = Onau;
                        }
                        if (decimal.TryParse(dr["DTNNON"].ToString(), out decimal DTNNON))
                        {
                            nudDTNNON.Value = DTNNON;
                        }
                        if (decimal.TryParse(dr["DTON"].ToString(), out decimal DTON))
                        {
                            nudDTON.Value = DTON;
                        }
                        if (int.TryParse(dr["TinHieuTong"].ToString(), out int TinHieuTong))
                        {
                            nudTongTH.Value = TinHieuTong;
                        }
                        if (int.TryParse(dr["TinHieuBMVN"].ToString(), out int TinHieuBMVN))
                        {
                            nudTHBMVN.Value = TinHieuBMVN;
                        }
                        if (int.TryParse(dr["TinHieuKhac"].ToString(), out int TinHieuKhac))
                        {
                            nudTHKhac.Value = TinHieuKhac;
                        }
                        if (decimal.TryParse(dr["MatDoTHTong"].ToString(), out decimal MatDoTHTong))
                        {
                            nudMatDoTHTong.Value = MatDoTHTong;
                        }
                        if (decimal.TryParse(dr["MatDoTHBMVN"].ToString(), out decimal MatDoTHBMVN))
                        {
                            nudMatDoTHBMVN.Value = MatDoTHBMVN;
                        }
                        if (decimal.TryParse(dr["MatDoTHKhac"].ToString(), out decimal MatDoTHKhac))
                        {
                            nudMatDoTHKhac.Value = MatDoTHKhac;
                        }
                        if (int.TryParse(dr["TongO"].ToString(), out int TongO))
                        {
                            nudTongO.Value = TongO;
                        }
                        nudTongKS.Value = int.Parse(dr["TongoKs"].ToString());
                        nudDS.Value = int.Parse(dr["TongoDs"].ToString());
                        if (double.TryParse(dr["DientichKS"].ToString(), out double DientichKS))
                        {
                            nudDientichKS.Value = (decimal)DientichKS;
                        }
                        if (double.TryParse(dr["DientichKTLai"].ToString(), out double DientichKTLai))
                        {
                            nudDientichKTLai.Value = (decimal)DientichKTLai;
                        }
                        if (long.TryParse(dr["Duan"].ToString(), out long Duan))
                        {
                            comboBox_TenDA.SelectedValue = Duan;
                        }
                        if (long.TryParse(dr["DVKS_id"].ToString(), out long DVKS_id))
                        {
                            cbDVKS.SelectedValue = DVKS_id;
                        }

                        //comboBox_Tinh.Text = dr["Tinh"].ToString();
                        //comboBox_Huyen.Text = dr["Huyen"].ToString();
                        //comboBox_Xa.Text = dr["Xa"].ToString();
                        //TinhId = int.Parse(dr["Tinhid"].ToString());
                        //DuanId = int.Parse(dr["Duanid"].ToString());
                        //HuyenId = int.Parse(dr["Huyenid"].ToString());
                        //XaId = int.Parse(dr["Xaid"].ToString());
                        if (long.TryParse(dr["Tinhid"].ToString(), out long Tinhid))
                        {
                            comboBox_Tinh.SelectedValue = Tinhid;
                        }
                        if (long.TryParse(dr["Huyenid"].ToString(), out long Huyenid))
                        {
                            comboBox_Huyen.SelectedValue = Huyenid;
                        }
                        if (long.TryParse(dr["Xaid"].ToString(), out long Xaid))
                        {
                            comboBox_Xa.SelectedValue = Xaid;
                        }
                        PersonId = dr["Doisoid"].ToString();
                        if (long.TryParse(dr["Doisoid"].ToString(), out long Doisoid))
                        {
                            cbb_Doiso.SelectedValue = Doisoid > 0 ? Doisoid : -1;
                        }
                        if (long.TryParse(dr["GiamSat_id"].ToString(), out long GiamSat_id))
                        {
                            cbGiamSat.SelectedValue = GiamSat_id > 0 ? GiamSat_id : -1;
                        }
                        if (long.TryParse(dr["ChiHuyCT_id"].ToString(), out long ChiHuyCT_id))
                        {
                            cbChiHuyCT.SelectedValue = ChiHuyCT_id > 0 ? ChiHuyCT_id : -1;
                        }
                        tbGiamSatOther.Text = dr["GiamSat_other"].ToString();
                        tbChiHuyCTOther.Text = dr["ChiHuyCT_other"].ToString();
                        txtDoingoai.Text = dr["Doingoai"].ToString();
                        TxtDoitruongngoai.Text = dr["Doitruongngoai"].ToString();
                        txtThon.Text = dr["Thon"].ToString();

                    }
                }
            }

            LoadDataMine();
            LoadDataKQKS();
            loading = false;
        }

        private void LoadCBTinh()
        {
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 1", _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_Tinh.DataSource = datatableProgram;
            comboBox_Tinh.ValueMember = "id";
            comboBox_Tinh.DisplayMember = "Ten";
        }

        private void LoadCBHuyen()
        {
            if(!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 2 AND parent_id = " + comboBox_Tinh.SelectedValue, _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_Huyen.DataSource = datatableProgram;
            comboBox_Huyen.ValueMember = "id";
            comboBox_Huyen.DisplayMember = "Ten";
        }

        private void LoadCBXa()
        {
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 3 AND parentiddistrict = " + comboBox_Huyen.SelectedValue, _cn.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_Xa.DataSource = datatableProgram;
            comboBox_Xa.ValueMember = "id";
            comboBox_Xa.DisplayMember = "Ten";
        }

        private void comboBox_Tinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            //comboBox_Huyen.Text = null;
            //comboBox_Xa.Text = null;

            //if (comboBox_Tinh.SelectedItem != null)
            //{
            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Tinh.SelectedItem.ToString()), _cn.Connection as SqlConnection);
            //    //sqlAdapterWard.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);

            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        TinhId = int.Parse(dr["id"].ToString());
            //    }

            //    SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 2 and parent_id = {0}", TinhId), _cn.Connection as SqlConnection);
            //    //sqlAdapterCounty.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            //    System.Data.DataTable datatableCounty = new System.Data.DataTable();
            //    sqlAdapterCounty.Fill(datatableCounty);
            //    comboBox_Huyen.Items.Clear();
            //    comboBox_Huyen.Items.Add("Chọn");
            //    foreach (DataRow dr in datatableCounty.Rows)
            //    {
            //        if (string.IsNullOrEmpty(dr["Ten"].ToString()))
            //            continue;

            //        comboBox_Huyen.Items.Add(dr["Ten"].ToString());
            //    }


            //}
            if (!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            LoadCBHuyen();
        }

        private void comboBox_Huyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            //comboBox_Xa.Text = null;
            //if (comboBox_Huyen.SelectedItem != null)
            //{
            //    SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Huyen.SelectedItem.ToString()), _cn.Connection as SqlConnection);
            //    //sqlAdapterCounty.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            //    System.Data.DataTable datatableCounty = new System.Data.DataTable();
            //    sqlAdapterCounty.Fill(datatableCounty);

            //    foreach (DataRow dr in datatableCounty.Rows)
            //    {
            //        HuyenId = int.Parse(dr["id"].ToString());
            //    }

            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 3 and parentiddistrict = {0}", HuyenId), _cn.Connection as SqlConnection);
            //    //sqlAdapterWard.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);
            //    comboBox_Xa.Items.Clear();
            //    comboBox_Xa.Items.Add("Chọn");
            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        if (string.IsNullOrEmpty(dr["Ten"].ToString()))
            //            continue;

            //        comboBox_Xa.Items.Add(dr["Ten"].ToString());
            //    }
            //}
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            LoadCBXa();
        }

        private bool DeleteSubTable()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM cecm_reportdaily_KQKS WHERE cecm_reportdaily_id = " + id_BSKQ, _cn.Connection as SqlConnection);
                cmd.Transaction = _cn.Transaction as SqlTransaction;
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_reportdaily_BMVN WHERE cecm_reportdaily_id = " + id_BSKQ, _cn.Connection as SqlConnection);
                cmd2.Transaction = _cn.Transaction as SqlTransaction;
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void comboBox_Xa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;

            //if (comboBox_Xa.SelectedItem != null)
            //{
            //    SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Xa.SelectedItem.ToString()), _cn.Connection as SqlConnection);
            //    //sqlAdapterCounty.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            //    System.Data.DataTable datatableCounty = new System.Data.DataTable();
            //    sqlAdapterCounty.Fill(datatableCounty);

            //    foreach (DataRow dr in datatableCounty.Rows)
            //    {
            //        XaId = int.Parse(dr["id"].ToString());
            //    }
            //}
        }

        private void TimeKT_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvKQKS.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            CecmReportDailyKQKS kqks = dgvRow.Tag as CecmReportDailyKQKS;
            //int id_kqks = int.Parse(str);

            if (e.ColumnIndex == dgvKQKS_cotSua.Index)
            {
                FrmThemmoiKQKS frm = new FrmThemmoiKQKS(dgvRow, (long)comboBox_TenDA.SelectedValue);
                frm.ShowDialog();

                //LoadDataKQKS(DuanId, DuanId);
            }
            //delete column
            if (e.ColumnIndex == dgvKQKS_cotXoa.Index)
            {
                //if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                //    return;
                //SqlCommand cmd2 = new SqlCommand("DELETE FROM Cecm_TerrainRectangle WHERE Cecm_TerrainRectangle.id = " + id_kqks, _cn.Connection as SqlConnection); int temp = 0;
                //temp = cmd2.ExecuteNonQuery();
                //if (temp > 0)
                //{
                //    MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                //}
                //else
                //{
                //    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                //}
                //LoadDataKQKS(DuanId, DuanId);
                dgvKQKS.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void dgvBMVN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvBMVN.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            //string str = dgvRow.Tag as string;
            //int id_kqks = int.Parse(str);

            if (e.ColumnIndex == dgvBMVN_cotSua.Index)
            {
                FrmThemmoiBommin frm = new FrmThemmoiBommin(dgvRow, (long)comboBox_TenDA.SelectedValue);
                frm.ShowDialog();
                //LoadDataMine(DuanId, DuanId);
            }
            //delete column
            if (e.ColumnIndex == dgvBMVN_cotXoa.Index)
            {
                //if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                //    return;
                //SqlCommand cmd2 = new SqlCommand("DELETE FROM Cecm_VNTerrainMinePoint WHERE id = " + id_kqks, _cn.Connection as SqlConnection);
                //int temp = 0;
                //temp = cmd2.ExecuteNonQuery();
                //if (temp > 0)
                //{
                //    MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                //}
                //else
                //{
                //    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                //}
                //LoadDataMine(DuanId, DuanId);
                dgvBMVN.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void cbb_Doiso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!(cbb_Doiso.SelectedValue is long))
            {
                return;
            }
            if ((long)cbb_Doiso.SelectedValue > 0)
            {
                //if (cbb_Doiso.SelectedItem != null)
                //{
                    SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter(string.Format("SELECT id, master FROM [cert_command_person] where id = {0}", cbb_Doiso.SelectedValue), _cn.Connection as SqlConnection);
                    //sqlAdapterPerson.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                    System.Data.DataTable datatablePerson = new System.Data.DataTable();
                    sqlAdapterPerson.Fill(datatablePerson);
                    foreach (DataRow dr in datatablePerson.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["id"].ToString()))
                            continue;
                        PersonId = dr["id"].ToString();
                        txtDoitruong.Text = dr["master"].ToString();
                    }
                    txtDoingoai.Enabled = false;
                    TxtDoitruongngoai.Enabled = false;
                    txtDoingoai.Text = "";
                    TxtDoitruongngoai.Text = "";
                //}
                //else
                //{
                //    txtDoingoai.Enabled = true;
                //    TxtDoitruongngoai.Enabled = true;
                //    txtDoitruong.Text = "";
                //}
            }
            else
            {
                PersonId = "0";
                txtDoingoai.Enabled = true;
                TxtDoitruongngoai.Enabled = true;
                txtDoitruong.Text = "";
                txtDoitruong.Enabled = false;
            }
        }

        private void LoadDVKS()
        {
            cbDVKS.DataSource = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter(
                    string.Format(@"SELECT id, name 
                    FROM cert_department 
                    WHERE id_web in (SELECT dept_id_web FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0}) 
                    or id in (SELECT dept_id FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0})", comboBox_TenDA.SelectedValue, TableName.KHAO_SAT), _cn.Connection as SqlConnection);
                //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                System.Data.DataTable datatableStaff = new System.Data.DataTable();
                sqlAdapterProgram.Fill(datatableStaff);
                DataRow drStaff = datatableStaff.NewRow();
                drStaff["id"] = -1;
                drStaff["name"] = "Chưa chọn";
                datatableStaff.Rows.InsertAt(drStaff, 0);
                cbDVKS.DataSource = datatableStaff;
                cbDVKS.ValueMember = "id";
                cbDVKS.DisplayMember = "name";
            }
        }

        private void txtDoitruong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDoingoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchBMVN_Click(object sender, EventArgs e)
        {
            LoadDataMine_onChange_cbDA();
        }

        private void comboBox_TenDA_Validating(object sender, CancelEventArgs e)
        {
            if(comboBox_TenDA.SelectedValue == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(comboBox_TenDA, "Chưa chọn dự án");
                return;
            }
            if((long)comboBox_TenDA.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_TenDA, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(comboBox_TenDA, "Chưa chọn dự án");
            }
        }

        private void LoadCBStaff(ComboBox cb)
        {
            cb.DataSource = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                UtilsDatabase.buildCombobox(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + comboBox_TenDA.SelectedValue, "id", "nameId");
            }

        }

        private void comboBox_TenDA_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT startTime,endTime,id FROM cecm_programData where id = {0}", comboBox_TenDA.SelectedValue), _cn.Connection as SqlConnection);
                //sqlAdapterWard.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    TimeBD.Text = dr["startTime"].ToString();
                    TimeKT.Text = dr["endTime"].ToString();
                    DuanId = int.Parse(dr["id"].ToString());
                }
                LoadDVKS();
                LoadCBStaff(cbGiamSat);
                LoadCBStaff(cbChiHuyCT);

                //DeleteSubTable();
                if (!loading)
                {
                    LoadDataKQKS_onChange_cbDA();
                    LoadDataMine_onChange_cbDA();
                }


                //cbb_Doiso.Items.Clear();
                //SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter("SELECT [cert_command_person].* FROM [cert_command_person]", _cn.Connection as SqlConnection);
                ////sqlAdapterPerson.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                //System.Data.DataTable datatablePerson = new System.Data.DataTable();
                //sqlAdapterPerson.Fill(datatablePerson);
                //foreach (DataRow dr in datatablePerson.Rows)
                //{
                //    if (string.IsNullOrEmpty(dr["name"].ToString()))
                //        continue;

                //    cbb_Doiso.Items.Add(dr["name"].ToString());
                //}
                //cbb_Doiso.Items.Add("Khác ...");
            }
        }

        private void comboBox_Tinh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            LoadCBHuyen();
        }

        private void comboBox_Huyen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            LoadCBXa();
        }

        private void cbDVKS_Validating(object sender, CancelEventArgs e)
        {

        }

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            if(!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Tinh.SelectedValue <= 0)
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Tinh, "Chưa chọn tỉnh");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Tinh, "");
            }
        }

        private void comboBox_Huyen_Validating(object sender, CancelEventArgs e)
        {
            if (!(comboBox_Tinh.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Tinh.SelectedValue <= 0)
            {
                return;
            }
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Huyen.SelectedValue <= 0)
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Huyen, "Chưa chọn huyện");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Huyen, "");
            }
        }

        private void comboBox_Xa_Validating(object sender, CancelEventArgs e)
        {
            if (!(comboBox_Huyen.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Huyen.SelectedValue <= 0)
            {
                return;
            }
            if (!(comboBox_Xa.SelectedValue is long))
            {
                return;
            }
            if ((long)comboBox_Xa.SelectedValue <= 0)
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Xa, "");
            }
        }

        private void nudMau_ValueChanged(object sender, EventArgs e)
        {
            nudTongO.Value = nudXanhla.Value + nudDo.Value + nudVang.Value + nudNau.Value + nudXam.Value;
        }

        private void nudTHBMVN_ValueChanged(object sender, EventArgs e)
        {
            nudTongTH.Value = nudTHBMVN.Value + nudTHKhac.Value;
        }

        private void nudMatDoTHBMVN_ValueChanged(object sender, EventArgs e)
        {
            nudMatDoTHTong.Value = nudMatDoTHBMVN.Value + nudMatDoTHKhac.Value;
        }

        private void cbGiamSat_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cbGiamSat.SelectedValue is long))
            {
                return;
            }
            if ((long)cbGiamSat.SelectedValue > 0)
            {
                tbGiamSatOther.ReadOnly = true;
                tbGiamSatOther.Text = "";
            }
            else
            {
                tbGiamSatOther.ReadOnly = false;
            }
        }

        private void cbChiHuyCT_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cbChiHuyCT.SelectedValue is long))
            {
                return;
            }
            if ((long)cbChiHuyCT.SelectedValue > 0)
            {
                tbChiHuyCTOther.ReadOnly = true;
                tbChiHuyCTOther.Text = "";
            }
            else
            {
                tbChiHuyCTOther.ReadOnly = false;
            }
        }

        private void cbDVKS_Validating_1(object sender, CancelEventArgs e)
        {
            if (cbDVKS.SelectedValue == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(cbDVKS, "Chưa chọn đơn vị khảo sát");
                return;
            }
            if ((long)cbDVKS.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(cbDVKS, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(cbDVKS, "Chưa chọn đơn vị khảo sát");
            }
        }
    }
}
