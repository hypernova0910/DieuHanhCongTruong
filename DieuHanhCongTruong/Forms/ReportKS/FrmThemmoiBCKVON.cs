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
    public partial class FrmThemmoiBCKVON : Form
    {
        public SqlConnection _Cn = null;
        private ConnectionWithExtraInfo _cn;
        public int id_BSKQ = 0;
        public int DuanId = 0;
        public int VungId = 0;
        public string TenVung = "0";
        //public int TinhId =0;
        //public int HuyenId = 0;
        //public int XaId = 0;
        public string PersonId = "0";
        public string Polygon = "";
        public string id_LoaiTV = "";
        public string id_DophuTV = "";
        public string id_PT = "";
        public string id_LoaiKV = "";
        public string id_LoaiDC = "";
        public string id_Loaixe = "";
        public string id_Diahinh = "";
        public string id_Loaidat = "";
        public string id_Thang = "";
        public string id_Mucdich = "";
        private bool isLuuClicked = false;

        public FrmThemmoiBCKVON(int i)
        {
            id_BSKQ = i;
            _cn = UtilsDatabase._ExtraInfoConnettion;
            _Cn = _cn.Connection as SqlConnection;
            InitializeComponent();
        }

        private void LoadCBStaff(ComboBox cb)
        {
            cb.DataSource = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                UtilsDatabase.buildCombobox(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + comboBox_TenDA.SelectedValue, "id", "nameId");
            }

        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
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
        private void button1_Click(object sender, EventArgs e)
        {
            string A = "";
            if (checkedListBox_LoaiTV.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_LoaiTV.CheckedItems.Count; i++)
                {
                    if (A == "")
                    {

                        A += checkedListBox_LoaiTV.CheckedItems[i].ToString();
                    }
                    else
                    {
                        A += " + " + checkedListBox_LoaiTV.CheckedItems[i].ToString();
                    }
                }
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
                        "UPDATE [dbo].[cecm_ReportPollutionArea] SET " +
                        "[Duan] = @Duan," +
                        "[VungDuan] = @VungDuan," +
                        "[DVKS_id] = @DVKS_id," +
                        "code = @code," +
                        "[Hopphan] = @Hopphan," +
                        "[Tinh] = @Tinh," +
                        "[Huyen] = @Huyen," +
                        "[Xa] = @Xa," +
                        "[Thon] = @Thon," +
                        "[MaKV] = @MaKV," +
                        "[Tendoi] = @Tendoi," +
                        "[Doitruong] = @Doitruong," +
                        "[TinhtrangKV] = @TinhtrangKV," +
                        "[NgayBD] = @NgayBD," +
                        "[NgayKT] = @NgayKT," +
                        "[MaNV] = @MaNV," +
                        "[UutienRP] = @UutienRP," +
                        "[MucdichSD] = @MucdichSD," +
                        "[Nguoihuong] = @Nguoihuong," +
                        "[LoaiThucvat] = @LoaiThucvat," +
                        "[DophuTV] = @DophuTV," +
                        "[PTphatquang] = @PTphatquang," +
                        "[Loaihinhdiachat] = @Loaihinhdiachat," +
                        "[Loaidat] = @Loaidat," +
                        "[LoaihinhKV] = @LoaihinhKV," +
                        "[Loaixe] = @Loaixe," +
                        "[Diahinh] = @Diahinh," +
                        "[DientichKV] = @DientichKV," +
                        "[Thangkotiepcan] = @Thangkotiepcan," +
                        "[Lido] = @Lido," +
                        "[Ngaytao] = @Ngaytao," +
                        "[Phantram] = @Phantram," +
                        "[Doingoai] = @Doingoai," +
                        "[Doitruongngoai] = @Doitruongngoai, " +
                        "Dai = @Dai, " +
                        "Rong = @Rong, " +
                        "Ghichu = @Ghichu, " +
                        "[GiamSat_id] = @GiamSat_id, " +
                        "[ChiHuyCT_id] = @ChiHuyCT_id, " +
                        "[GiamSat_other] = @GiamSat_other, " +
                        "[ChiHuyCT_other] = @ChiHuyCT_other, " +
                        "MucdichSDkhac = @MucdichSDkhac, " +
                        "Phantram_id = @Phantram_id " + 
                        "WHERE id = " + dem, _Cn);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;
                    SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.BigInt);
                    Duan.Value = comboBox_TenDA.SelectedValue;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter VungDuan = new SqlParameter("@VungDuan", SqlDbType.BigInt);
                    VungDuan.Value = comboBox_Vung.SelectedValue != null ? comboBox_Vung.SelectedValue : -1;
                    cmd2.Parameters.Add(VungDuan);

                    SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                    DVKS_id.Value = cbDVKS.SelectedValue;
                    cmd2.Parameters.Add(DVKS_id);

                    SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 50);
                    Hopphan.Value = txtHopphan.Text;
                    cmd2.Parameters.Add(Hopphan);

                    SqlParameter code = new SqlParameter("@code", SqlDbType.NVarChar, 50);
                    code.Value = tbCode.Text;
                    cmd2.Parameters.Add(code);

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

                    SqlParameter MaKV = new SqlParameter("@MaKV", SqlDbType.NVarChar, 50);
                    MaKV.Value = txtMaKV.Text;
                    cmd2.Parameters.Add(MaKV);


                    if (cbb_Doiso.Text == "Khác ...")
                    {
                        SqlParameter Tendoi = new SqlParameter("@Tendoi", SqlDbType.NVarChar, 50);
                        Tendoi.Value = 0;
                        cmd2.Parameters.Add(Tendoi);
                    }
                    else
                    {
                        SqlParameter Tendoi = new SqlParameter("@Tendoi", SqlDbType.NVarChar, 50);
                        Tendoi.Value = PersonId;
                        cmd2.Parameters.Add(Tendoi);
                    }

                    SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 50);
                    Doitruong.Value = txtDoitruong.Text;
                    cmd2.Parameters.Add(Doitruong);

                    SqlParameter TinhtrangKV = new SqlParameter("@TinhtrangKV", SqlDbType.NVarChar, 50);
                    TinhtrangKV.Value = comboBox_TinhtrangKV.Text;
                    cmd2.Parameters.Add(TinhtrangKV);

                    SqlParameter timeBatdau = new SqlParameter("@NgayBD", SqlDbType.DateTime);
                    timeBatdau.Value = TimeBD.Value;
                    cmd2.Parameters.Add(timeBatdau);

                    SqlParameter timeKetthuc = new SqlParameter("@NgayKT", SqlDbType.DateTime);
                    timeKetthuc.Value = TimeKT.Value;
                    cmd2.Parameters.Add(timeKetthuc);

                    SqlParameter MucdichSD = new SqlParameter("@MucdichSD", SqlDbType.NVarChar, 50);
                    MucdichSD.Value = comboBox_Mucdichsd.Text;
                    cmd2.Parameters.Add(MucdichSD);

                    SqlParameter MucdichSDkhac = new SqlParameter("@MucdichSDkhac", SqlDbType.NVarChar, 50);
                    MucdichSDkhac.Value = txtMucdich.Text;
                    cmd2.Parameters.Add(MucdichSDkhac);

                    SqlParameter UutienRP = new SqlParameter("@UutienRP", SqlDbType.NVarChar, 50);
                    UutienRP.Value = comboBox_Uutien.Text;
                    cmd2.Parameters.Add(UutienRP);

                    SqlParameter Nguoihuong = new SqlParameter("@Nguoihuong", SqlDbType.NVarChar, 50);
                    Nguoihuong.Value = comboBox_Nguoihuongloi.Text;
                    cmd2.Parameters.Add(Nguoihuong);

                    SqlParameter LoaiThucvat = new SqlParameter("@LoaiThucvat", SqlDbType.NVarChar, 50);
                    LoaiThucvat.Value = id_LoaiTV;
                    cmd2.Parameters.Add(LoaiThucvat);

                    SqlParameter DophuTV = new SqlParameter("@DophuTV", SqlDbType.NVarChar, 50);
                    DophuTV.Value = id_DophuTV;
                    cmd2.Parameters.Add(DophuTV);

                    SqlParameter PTphatquang = new SqlParameter("@PTphatquang", SqlDbType.NVarChar, 50);
                    PTphatquang.Value = id_PT;
                    cmd2.Parameters.Add(PTphatquang);

                    SqlParameter Loaihinhdiachat = new SqlParameter("@Loaihinhdiachat", SqlDbType.NVarChar, 50);
                    Loaihinhdiachat.Value = id_LoaiDC;
                    cmd2.Parameters.Add(Loaihinhdiachat);

                    SqlParameter Loaidat = new SqlParameter("@Loaidat", SqlDbType.NVarChar, 50);
                    Loaidat.Value = id_Loaidat;
                    cmd2.Parameters.Add(Loaidat);

                    SqlParameter LoaihinhKV = new SqlParameter("@LoaihinhKV", SqlDbType.NVarChar, 50);
                    LoaihinhKV.Value = id_LoaiKV;
                    cmd2.Parameters.Add(LoaihinhKV);

                    SqlParameter Loaixe = new SqlParameter("@Loaixe", SqlDbType.NVarChar, 50);
                    Loaixe.Value = id_Loaixe;
                    cmd2.Parameters.Add(Loaixe);

                    SqlParameter Diahinh = new SqlParameter("@Diahinh", SqlDbType.NVarChar, 50);
                    Diahinh.Value = id_Diahinh;
                    cmd2.Parameters.Add(Diahinh);

                    SqlParameter Thangkotiepcan = new SqlParameter("@Thangkotiepcan", SqlDbType.NVarChar, 50);
                    Thangkotiepcan.Value = id_Thang;
                    cmd2.Parameters.Add(Thangkotiepcan);

                    SqlParameter Phantram = new SqlParameter("@Phantram", SqlDbType.NVarChar, 50);
                    Phantram.Value = comboBox_Phantram.Text;
                    cmd2.Parameters.Add(Phantram);

                    SqlParameter Phantram_id = new SqlParameter("@Phantram_id", SqlDbType.BigInt);
                    Phantram_id.Value = comboBox_Phantram.SelectedIndex + 1;
                    cmd2.Parameters.Add(Phantram_id);

                    SqlParameter DientichKV = new SqlParameter("@DientichKV", SqlDbType.NVarChar, 50);
                    DientichKV.Value = txt_Dientich.Text;
                    cmd2.Parameters.Add(DientichKV);

                    SqlParameter Lido = new SqlParameter("@Lido", SqlDbType.NVarChar, 50);
                    Lido.Value = richTextBox_Lydo.Text;
                    cmd2.Parameters.Add(Lido);

                    SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 255);
                    Thon.Value = txtThon.Text;
                    cmd2.Parameters.Add(Thon);

                    SqlParameter Doingoai = new SqlParameter("@Doingoai", SqlDbType.NVarChar, 255);
                    Doingoai.Value = txtDoingoai.Text;
                    cmd2.Parameters.Add(Doingoai);

                    SqlParameter Dai = new SqlParameter("@Dai", SqlDbType.Float);
                    Dai.Value = txtDai.Text;
                    cmd2.Parameters.Add(Dai);

                    SqlParameter Rong = new SqlParameter("@Rong", SqlDbType.Float);
                    Rong.Value = txtRong.Text;
                    cmd2.Parameters.Add(Rong);

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

                    SqlParameter Ghichu = new SqlParameter("@Ghichu", SqlDbType.NVarChar, 1000);
                    Ghichu.Value = tbGhichu.Text;
                    cmd2.Parameters.Add(Ghichu);

                    try
                    {
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
                    catch (System.Exception ex)
                    {
                        //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                        //this.Close();
                        return false;
                    }

                }
                else
                {

                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[cecm_ReportPollutionArea]([Duan],[VungDuan],DVKS_id,[Hopphan],code,[Tinh],[Huyen],[Xa],[Thon],[MaKV],[Tendoi],[Doitruong],[TinhtrangKV],[NgayBD],[NgayKT],[MaNV],[UutienRP],[MucdichSD],[Nguoihuong],[LoaiThucvat],[DophuTV],[PTphatquang],[Loaihinhdiachat],[Loaidat],[LoaihinhKV],[Loaixe],[Diahinh],[DientichKV],[Thangkotiepcan],[Lido],[Ngaytao],[Phantram],[Doingoai],[Doitruongngoai],[Dai],[Rong],Ghichu,GiamSat_id,ChiHuyCT_id,GiamSat_other,ChiHuyCT_other,[MucdichSDkhac],Phantram_id) " +
                        "VALUES(@Duan,@VungDuan,@DVKS_id,@Hopphan,@code,@Tinh,@Huyen,@Xa,@Thon,@MaKV,@Tendoi,@Doitruong,@TinhtrangKV,@NgayBD,@NgayKT,@MaNV,@UutienRP,@MucdichSD,@Nguoihuong,@LoaiThucvat,@DophuTV,@PTphatquang,@Loaihinhdiachat,@Loaidat,@LoaihinhKV,@Loaixe,@Diahinh,@DientichKV,@Thangkotiepcan,@Lido,@Ngaytao,@Phantram,@Doingoai,@Doitruongngoai,@Dai,@Rong,@Ghichu,@GiamSat_id,@ChiHuyCT_id,@GiamSat_other,@ChiHuyCT_other,@MucdichSDkhac,@Phantram_id)", _Cn);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;
                    SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.BigInt);
                    Duan.Value = comboBox_TenDA.SelectedValue;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter VungDuan = new SqlParameter("@VungDuan", SqlDbType.BigInt);
                    VungDuan.Value = comboBox_Vung.SelectedValue != null ? comboBox_Vung.SelectedValue : -1;
                    cmd2.Parameters.Add(VungDuan);

                    SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                    DVKS_id.Value = cbDVKS.SelectedValue;
                    cmd2.Parameters.Add(DVKS_id);

                    SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 50);
                    Hopphan.Value = txtHopphan.Text;
                    cmd2.Parameters.Add(Hopphan);

                    SqlParameter code = new SqlParameter("@code", SqlDbType.NVarChar, 50);
                    code.Value = tbCode.Text;
                    cmd2.Parameters.Add(code);

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

                    SqlParameter MaKV = new SqlParameter("@MaKV", SqlDbType.NVarChar, 50);
                    MaKV.Value = txtMaKV.Text;
                    cmd2.Parameters.Add(MaKV);
                    if (cbb_Doiso.Text == "Khác ...")
                    {
                        SqlParameter Tendoi = new SqlParameter("@Tendoi", SqlDbType.NVarChar, 50);
                        Tendoi.Value = 0;
                        cmd2.Parameters.Add(Tendoi);
                    }
                    else
                    {
                        SqlParameter Tendoi = new SqlParameter("@Tendoi", SqlDbType.NVarChar, 50);
                        Tendoi.Value = PersonId;
                        cmd2.Parameters.Add(Tendoi);
                    }

                    SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 50);
                    Doitruong.Value = txtDoitruong.Text;
                    cmd2.Parameters.Add(Doitruong);

                    SqlParameter TinhtrangKV = new SqlParameter("@TinhtrangKV", SqlDbType.NVarChar, 50);
                    TinhtrangKV.Value = comboBox_TinhtrangKV.Text;
                    cmd2.Parameters.Add(TinhtrangKV);

                    SqlParameter timeBatdau = new SqlParameter("@NgayBD", SqlDbType.DateTime);
                    timeBatdau.Value = TimeBD.Value;
                    cmd2.Parameters.Add(timeBatdau);

                    SqlParameter timeKetthuc = new SqlParameter("@NgayKT", SqlDbType.DateTime);
                    timeKetthuc.Value = TimeKT.Value;
                    cmd2.Parameters.Add(timeKetthuc);

                    SqlParameter MucdichSD = new SqlParameter("@MucdichSD", SqlDbType.NVarChar, 50);
                    MucdichSD.Value = comboBox_Mucdichsd.Text;
                    cmd2.Parameters.Add(MucdichSD);

                    SqlParameter MucdichSDkhac = new SqlParameter("@MucdichSDkhac", SqlDbType.NVarChar, 50);
                    MucdichSDkhac.Value = txtMucdich.Text;
                    cmd2.Parameters.Add(MucdichSDkhac);

                    SqlParameter UutienRP = new SqlParameter("@UutienRP", SqlDbType.NVarChar, 50);
                    UutienRP.Value = comboBox_Uutien.Text;
                    cmd2.Parameters.Add(UutienRP);

                    SqlParameter Nguoihuong = new SqlParameter("@Nguoihuong", SqlDbType.NVarChar, 50);
                    Nguoihuong.Value = comboBox_Nguoihuongloi.Text;
                    cmd2.Parameters.Add(Nguoihuong);

                    SqlParameter LoaiThucvat = new SqlParameter("@LoaiThucvat", SqlDbType.NVarChar, 50);
                    LoaiThucvat.Value = id_LoaiTV;
                    cmd2.Parameters.Add(LoaiThucvat);

                    SqlParameter DophuTV = new SqlParameter("@DophuTV", SqlDbType.NVarChar, 50);
                    DophuTV.Value = id_DophuTV;
                    cmd2.Parameters.Add(DophuTV);

                    SqlParameter PTphatquang = new SqlParameter("@PTphatquang", SqlDbType.NVarChar, 50);
                    PTphatquang.Value = id_PT;
                    cmd2.Parameters.Add(PTphatquang);

                    SqlParameter Loaihinhdiachat = new SqlParameter("@Loaihinhdiachat", SqlDbType.NVarChar, 50);
                    Loaihinhdiachat.Value = id_LoaiDC;
                    cmd2.Parameters.Add(Loaihinhdiachat);

                    SqlParameter Loaidat = new SqlParameter("@Loaidat", SqlDbType.NVarChar, 50);
                    Loaidat.Value = id_Loaidat;
                    cmd2.Parameters.Add(Loaidat);

                    SqlParameter LoaihinhKV = new SqlParameter("@LoaihinhKV", SqlDbType.NVarChar, 50);
                    LoaihinhKV.Value = id_LoaiKV;
                    cmd2.Parameters.Add(LoaihinhKV);

                    SqlParameter Loaixe = new SqlParameter("@Loaixe", SqlDbType.NVarChar, 50);
                    Loaixe.Value = id_Loaixe;
                    cmd2.Parameters.Add(Loaixe);

                    SqlParameter Diahinh = new SqlParameter("@Diahinh", SqlDbType.NVarChar, 50);
                    Diahinh.Value = id_Diahinh;
                    cmd2.Parameters.Add(Diahinh);

                    SqlParameter Thangkotiepcan = new SqlParameter("@Thangkotiepcan", SqlDbType.NVarChar, 50);
                    Thangkotiepcan.Value = id_Thang;
                    cmd2.Parameters.Add(Thangkotiepcan);

                    SqlParameter Phantram = new SqlParameter("@Phantram", SqlDbType.NVarChar, 50);
                    Phantram.Value = comboBox_Phantram.Text;
                    cmd2.Parameters.Add(Phantram);

                    SqlParameter Phantram_id = new SqlParameter("@Phantram_id", SqlDbType.BigInt);
                    Phantram_id.Value = comboBox_Phantram.SelectedIndex + 1;
                    cmd2.Parameters.Add(Phantram_id);

                    SqlParameter DientichKV = new SqlParameter("@DientichKV", SqlDbType.NVarChar, 50);
                    DientichKV.Value = txt_Dientich.Text;
                    cmd2.Parameters.Add(DientichKV);

                    SqlParameter Lido = new SqlParameter("@Lido", SqlDbType.NVarChar, 50);
                    Lido.Value = richTextBox_Lydo.Text;
                    cmd2.Parameters.Add(Lido);

                    SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 255);
                    Thon.Value = txtThon.Text;
                    cmd2.Parameters.Add(Thon);

                    SqlParameter Doingoai = new SqlParameter("@Doingoai", SqlDbType.NVarChar, 255);
                    Doingoai.Value = txtDoingoai.Text;
                    cmd2.Parameters.Add(Doingoai);

                    SqlParameter Doitruongngoai = new SqlParameter("@Doitruongngoai", SqlDbType.NVarChar, 255);
                    Doitruongngoai.Value = TxtDoitruongngoai.Text;
                    cmd2.Parameters.Add(Doitruongngoai);

                    SqlParameter Dai = new SqlParameter("@Dai", SqlDbType.Float);
                    Dai.Value = txtDai.Text;
                    cmd2.Parameters.Add(Dai);

                    SqlParameter Rong = new SqlParameter("@Rong", SqlDbType.Float);
                    Rong.Value = txtRong.Text;
                    cmd2.Parameters.Add(Rong);

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

                    SqlParameter Ghichu = new SqlParameter("@Ghichu", SqlDbType.NVarChar, 1000);
                    Ghichu.Value = tbGhichu.Text;
                    cmd2.Parameters.Add(Ghichu);
                    try
                    {
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
                    catch (System.Exception ex)
                    {
                        //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                        //this.Close();
                        return false;
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");

                return false;
            }
        }

        private void UpdateCheckboxList(string A, CheckedListBox B)
        {
            if (A != "")
            {
                for (int i = 0; i < A.Split('+').Length; i++)
                {
                    for (int k = 0; k < B.Items.Count; k++)
                    {
                        if (k == int.Parse(A.Split('+')[i]) - 1)
                        {
                            B.SetItemCheckState(k, CheckState.Checked);
                        }

                    }

                }
            }
        }
        private void Datagridview2_Load(int DAid)
        {
            if(!(comboBox_Vung.SelectedValue is long))
            {
                return;
            }
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM cecm_program_area_map where id = {0}", comboBox_Vung.SelectedValue), _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            System.Data.DataTable datatableWard = new System.Data.DataTable();
            sqlAdapterWard.Fill(datatableWard);
            DataTable table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Điểm", typeof(string));
            table.Columns.Add("Kinh độ", typeof(string));
            table.Columns.Add("Vĩ độ", typeof(string));
            table.Columns.Add("Mã KV", typeof(string));
            foreach (DataRow dr in datatableWard.Rows)
            {
                string Str = dr["Polygongeomst"].ToString();
                //Str = Str.Replace("(((", "+");
                //Str = Str.Replace(")))", "+");
                //var LstStr = Str.Split('+')[1];
                //var LstStr1 = LstStr.Split(',');
                List<PointF[]> lst = AppUtils.convertMultiPolygon(Str);
                int i = 0;
                foreach (PointF[] points in lst)
                {
                    foreach(PointF point in points)
                    {
                        i++;
                        table.Rows.Add(i, "Điểm " + i.ToString(), point.X, point.Y, dr["code"].ToString());
                    }
                }
            }
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = table;
            dataGridView2.ReadOnly = true;

            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dataGridView2.Rows != null && dataGridView2.Rows.Count != 0)
            {
                dataGridView2.Rows[0].Selected = true;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.BackgroundColor = Color.White;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.AllowUserToResizeRows = false;
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
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT Cecm_VNTerrainMinePoint_CHA.id as 'STT',Cecm_VNTerrainMinePoint_CHA.XPoint as 'Kinh độ',Cecm_VNTerrainMinePoint_CHA.YPoint as 'Vĩ độ',Cecm_VNTerrainMinePoint_CHA.[Loai] as 'Loại',Cecm_VNTerrainMinePoint_CHA.[SL] as 'Lượng', Cecm_VNTerrainMinePoint_CHA.[Tinhtrang] as 'Tình trạng', Cecm_VNTerrainMinePoint_CHA.Ghichu as 'Ghi chú' FROM Cecm_VNTerrainMinePoint_CHA where idRectangle != -1 and Cecm_VNTerrainMinePoint_CHA.programId = {0}", dem), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idDuAn = dr["STT"].ToString();
                        var row1 = dr["Kinh độ"].ToString();
                        var row2 = dr["Vĩ độ"].ToString();
                        var row3 = dr["Loại"].ToString();
                        var row4 = dr["Lượng"].ToString();
                        var row5 = dr["Tình trạng"].ToString();
                        var row6 = dr["Ghi chú"].ToString();

                        dgvBMVN.Rows.Add(indexRow, row1, row2, row3, row4, row5, row6, Resources.Modify, Resources.DeleteRed);
                        dgvBMVN.Rows[indexRow - 1].Tag = idDuAn;

                        indexRow++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void LoadDataMine_onChange_cbVungDA()
        {
            try
            {
                dgvBMVN.Rows.Clear();
                if(!(comboBox_Vung.SelectedValue is long))
                {
                    return;
                }
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
                    "Cecm_VNTerrainMinePoint.XPoint as 'Kinh độ'," +
                    "Cecm_VNTerrainMinePoint.YPoint as 'Vĩ độ'," +
                    "Deep as 'Độ sâu'," +
                    "Cecm_VNTerrainMinePoint.[Tinhtrang] as 'Tình trạng' " +
                    "FROM Cecm_VNTerrainMinePoint " +
                    //"left join cecm_program_area_map on cecm_program_area_map.id = Cecm_VNTerrainMinePoint.programId " +
                    "left join OLuoi on OLuoi.gid = Cecm_VNTerrainMinePoint.idRectangle " +
                    "where 1 = 1 " +
                    "and Cecm_VNTerrainMinePoint.idArea = @idArea ";
                sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
                sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "idArea",
                    Value = comboBox_Vung.SelectedValue,
                    SqlDbType = SqlDbType.BigInt,
                });
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
                        var Kyhieu = dr["Kí hiệu"].ToString();
                        //var row2 = dr["Loại"].ToString();
                        var Loai = "";
                        var Ma_o = dr["Mã ô"].ToString();
                        var SL = dr["SL"].ToString();
                        var Kinhdo = dr["Kinh độ"].ToString();
                        var Vido = dr["Vĩ độ"].ToString();
                        var Deep = dr["Độ sâu"].ToString();
                        //var row8 = dr["Tình trạng"].ToString();
                        var Tinhtrang = "";

                        CecmReportPollutionAreaBMVN bmvn = new CecmReportPollutionAreaBMVN();
                        bmvn.Kyhieu = dr["Kí hiệu"].ToString();
                        //bmvn.Loai = dr["Loại"].ToString(); 
                        bmvn.idRectangle = long.Parse(dr["idRectangle"].ToString());
                        bmvn.Kinhdo = double.Parse(dr["Kinh độ"].ToString());
                        bmvn.Vido = double.Parse(dr["Vĩ độ"].ToString());
                        bmvn.Deep = double.Parse(dr["Độ sâu"].ToString());

                        dgvBMVN.Rows.Add(indexRow, Kyhieu, Loai,  Ma_o, "", Kinhdo, Vido, Deep, Tinhtrang, "", "");
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

        private void LoadDataMine()
        {
            try
            {
                dgvBMVN.Rows.Clear();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql = "Select tbl.*, ol.o_id as ol_idST " +
                    "FROM cecm_ReportPollutionArea_BMVN tbl " +
                    "left join OLuoi ol on tbl.idRectangle = ol.gid " +
                    "WHERE tbl.cecm_ReportPollutionArea_id = " + id_BSKQ;
                sqlAdapter = new SqlDataAdapter(sql, _cn.Connection as SqlConnection);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        //var idDuAn = dr["STT"].ToString();
                        var Kyhieu = dr["Kyhieu"].ToString();
                        var Loai = dr["Loai"].ToString();
                        var Ma_o = dr["ol_idST"].ToString();
                        var kinhdo = dr["Kinhdo"].ToString();
                        var vido = dr["Vido"].ToString();
                        //var Kichthuoc_str = dr["Kichthuoc"].ToString();
                        var loai = dr["Loai"].ToString();
                        var Deep = dr["Deep"].ToString();
                        var luong = dr["SL"].ToString();
                        var tinhtrang = dr["Tinhtrang"].ToString();
                        var PPXuLy = dr["PPXuLy"].ToString();
                        var ghichu = dr["Ghichu"].ToString();

                        CecmReportPollutionAreaBMVN bmvn = new CecmReportPollutionAreaBMVN();
                        bmvn.Kyhieu = dr["Kyhieu"].ToString();
                        bmvn.Loai = dr["Loai"].ToString();
                        bmvn.idRectangle = long.Parse(dr["idRectangle"].ToString());
                        bmvn.SL = int.Parse(dr["SL"].ToString());
                        bmvn.Kinhdo = double.Parse(dr["Kinhdo"].ToString());
                        bmvn.Vido = double.Parse(dr["Vido"].ToString());
                        bmvn.Deep = double.Parse(dr["Deep"].ToString());
                        bmvn.Tinhtrang = dr["Tinhtrang"].ToString();
                        if (double.TryParse(dr["Kichthuoc"].ToString(), out double Kichthuoc))
                        {
                            bmvn.Kichthuoc = Kichthuoc;
                        }
                        bmvn.PPXuLy = dr["PPXuLy"].ToString();
                        bmvn.Ghichu = dr["Ghichu"].ToString();

                        dgvBMVN.Rows.Add(indexRow, Loai, Kyhieu, Ma_o, Kichthuoc, kinhdo, vido, Deep, tinhtrang, PPXuLy, ghichu);
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

        private void LoadCBDA()
        {
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT id, name FROM cecm_programData", _Cn);
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

        private void FrmThemmoiBCKVON_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT Ten FROM cecm_provinces where level = 1", _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);
            //comboBox_Tinh.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProvince.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
            //        continue;

            //    comboBox_Tinh.Items.Add(dr["Ten"].ToString());
            //}
            UtilsDatabase.buildCombobox(comboBox_Tinh, "SELECT id, Ten FROM cecm_provinces WHERE level = 1", "id", "Ten");

            //SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", _Cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            //System.Data.DataTable datatableProgram = new System.Data.DataTable();
            //sqlAdapterProgram.Fill(datatableProgram);
            ////comboBox_TenDA.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProgram.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    comboBox_TenDA.Items.Add(dr["name"].ToString());
            //}
            LoadCBDA();

            SqlDataAdapter sqlAdapterPerson1 = new SqlDataAdapter("SELECT [cert_command_person].* FROM [cert_command_person]", _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterPerson1);
            System.Data.DataTable datatablePerson1 = new System.Data.DataTable();
            sqlAdapterPerson1.Fill(datatablePerson1);
            foreach (DataRow dr in datatablePerson1.Rows)
            {
                if (string.IsNullOrEmpty(dr["name"].ToString()))
                    continue;

                cbb_Doiso.Items.Add(dr["name"].ToString());
            }
            cbb_Doiso.Items.Add("Khác ...");
            if (id_BSKQ != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT cecm_ReportPollutionArea.*,cecm_program2.code as 'TenVungDuan',cecm_program2.id as 'VungDuanid', cecm_program1.name as 'TenDuan',cecm_program1.id as 'Duanid',[Hopphan],Tinh.Ten as 'TenTinh',Tinh.id as 'Tinhid',Huyen.Ten as 'TenHuyen',Huyen.id as 'Huyenid',Xa.Ten as 'TenXa',Xa.id as 'Xaid',[MaNV],cert_command_person.name as 'TenDoiso',cert_command_person.master as 'TenDoitruong' FROM cecm_ReportPollutionArea left join cecm_programData as cecm_program1  on cecm_program1.id = cecm_ReportPollutionArea.Duan left join cecm_program_area_map as cecm_program2 on cecm_program2.id = cecm_ReportPollutionArea.VungDuan left join cecm_provinces as Tinh on cecm_ReportPollutionArea.Tinh = Tinh.id left join cecm_provinces as Huyen on cecm_ReportPollutionArea.Huyen = Huyen.id left join cecm_provinces as Xa on cecm_ReportPollutionArea.Xa = Xa.id left join cert_command_person on cecm_ReportPollutionArea.Tendoi = cert_command_person.id where cecm_ReportPollutionArea.id = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        TenVung = dr["TenVungDuan"].ToString();
                        //VungId = int.Parse(dr["VungDuanid"].ToString());
                        tbCode.Text = dr["code"].ToString();
                        txtHopphan.Text = dr["Hopphan"].ToString();
                        timeNgaytao.Text = dr["Ngaytao"].ToString();
                        txtMaNV.Text = dr["MaNV"].ToString();
                        cbb_Doiso.Text = string.IsNullOrEmpty(dr["TenDoiso"].ToString()) ? "Khác ..." : dr["TenDoiso"].ToString();
                        txtDoitruong.Text = dr["Doitruong"].ToString();
                        txtMaKV.Text = dr["MaKV"].ToString();
                        TimeBD.Value = DateTime.TryParse(dr["NgayBD"].ToString(), out DateTime NgayBD) ? NgayBD : DateTime.Now;
                        TimeKT.Value = DateTime.TryParse(dr["NgayKT"].ToString(), out DateTime NgayKT) ? NgayKT : DateTime.Now;
                        comboBox_TinhtrangKV.Text = dr["TinhtrangKV"].ToString();
                        id_Mucdich = dr["MucdichSD"].ToString();
                        if (id_Mucdich != "0.0" )
                        {
                            comboBox_Mucdichsd.Text = id_Mucdich;
                        }
                        txtMucdich.Text = dr["MucdichSDkhac"].ToString();
                        comboBox_Uutien.Text = dr["UutienRP"].ToString();
                        comboBox_Nguoihuongloi.Text = dr["Nguoihuong"].ToString();

                        id_LoaiTV = dr["LoaiThucvat"].ToString();
                        UpdateCheckboxList(id_LoaiTV, checkedListBox_LoaiTV);
                        id_DophuTV = dr["DophuTV"].ToString();
                        UpdateCheckboxList(id_DophuTV, checkedListBox_Dophu);
                        id_PT = dr["PTphatquang"].ToString();
                        UpdateCheckboxList(id_PT, checkedListBox_PTphatquang);
                        id_LoaiDC = dr["Loaihinhdiachat"].ToString();
                        UpdateCheckboxList(id_LoaiDC, checkedListBox_LoaiDiachat);
                        id_Loaidat = dr["Loaidat"].ToString();
                        UpdateCheckboxList(id_Loaidat, checkedListBox_Loaidat);
                        id_LoaiKV = dr["LoaihinhKV"].ToString();
                        UpdateCheckboxList(id_LoaiKV, checkedListBox_LoaiKV);
                        id_Loaixe = dr["Loaixe"].ToString();
                        UpdateCheckboxList(id_Loaixe, checkedListBox_Loaixe);
                        id_Diahinh = dr["Diahinh"].ToString();
                        UpdateCheckboxList(id_Diahinh, checkedListBox_Diahinh);
                        id_Thang = dr["Thangkotiepcan"].ToString();
                        UpdateCheckboxList(id_Thang, checkedListBox_Thang);

                        richTextBox_Lydo.Text = dr["Lido"].ToString();
                        txt_Dientich.Text = dr["DientichKV"].ToString();
                        comboBox_Phantram.Text = dr["Phantram"].ToString();
                        comboBox_TenDA.Text = dr["TenDuan"].ToString();
                        if(long.TryParse(dr["Duan"].ToString(), out long Duan))
                        {
                            comboBox_TenDA.SelectedValue = Duan;
                        }
                        if (long.TryParse(dr["VungDuan"].ToString(), out long VungDuan))
                        {
                            comboBox_Vung.SelectedValue = VungDuan;
                        }
                        if (long.TryParse(dr["DVKS_id"].ToString(), out long DVKS_id))
                        {
                            cbDVKS.SelectedValue = DVKS_id;
                        }
                        //comboBox_Tinh.Text = dr["TenTinh"].ToString();
                        //comboBox_Huyen.Text = dr["TenHuyen"].ToString();
                        //comboBox_Xa.Text = dr["TenXa"].ToString();
                        //int.TryParse(dr["Tinhid"].ToString(), out TinhId);
                        int.TryParse(dr["Duanid"].ToString(), out DuanId);
                        //int.TryParse(dr["Huyenid"].ToString(), out HuyenId);
                        //int.TryParse(dr["Xaid"].ToString(), out XaId);
                        if(long.TryParse(dr["Tinhid"].ToString(), out long TinhId))
                        {
                            comboBox_Tinh.SelectedValue = TinhId;
                        }
                        if (long.TryParse(dr["Huyenid"].ToString(), out long Huyenid))
                        {
                            comboBox_Huyen.SelectedValue = Huyenid;
                        }
                        if (long.TryParse(dr["Xaid"].ToString(), out long Xaid))
                        {
                            comboBox_Xa.SelectedValue = Xaid;
                        }
                        PersonId = dr["Tendoi"].ToString();
                        txtDoingoai.Text = dr["Doingoai"].ToString();
                        TxtDoitruongngoai.Text = dr["Doitruongngoai"].ToString();
                        txtThon.Text = dr["Thon"].ToString();
                        txtDai.Text = dr["Dai"].ToString();
                        txtRong.Text = dr["Rong"].ToString();
                        tbGhichu.Text = dr["Ghichu"].ToString();
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
                    }
                }
            }

            //LoadDataMine(VungId, VungId);
            LoadDataMine();
        }

        //private void comboBox_Tinh_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SqlCommandBuilder sqlCommand = null;
        //    comboBox_Huyen.Text = null;
        //    comboBox_Xa.Text = null;
        //    if (comboBox_Tinh.SelectedItem != null)
        //    {
        //        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Tinh.SelectedItem.ToString()), _Cn);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
        //        System.Data.DataTable datatableWard = new System.Data.DataTable();
        //        sqlAdapterWard.Fill(datatableWard);

        //        foreach (DataRow dr in datatableWard.Rows)
        //        {
        //            TinhId = int.Parse(dr["id"].ToString());
        //        }

        //        SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 2 and parent_id = {0}", TinhId), _Cn);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
        //        System.Data.DataTable datatableCounty = new System.Data.DataTable();
        //        sqlAdapterCounty.Fill(datatableCounty);
        //        comboBox_Huyen.Items.Clear();
        //        comboBox_Huyen.Items.Add("Chọn");
        //        foreach (DataRow dr in datatableCounty.Rows)
        //        {
        //            if (string.IsNullOrEmpty(dr["Ten"].ToString()))
        //                continue;

        //            comboBox_Huyen.Items.Add(dr["Ten"].ToString());
        //        }


        //    }
        //}

        //private void comboBox_Huyen_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SqlCommandBuilder sqlCommand = null;
        //    comboBox_Xa.Text = null;
        //    if (comboBox_Huyen.SelectedItem != null)
        //    {
        //        SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Huyen.SelectedItem.ToString()), _Cn);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
        //        System.Data.DataTable datatableCounty = new System.Data.DataTable();
        //        sqlAdapterCounty.Fill(datatableCounty);

        //        foreach (DataRow dr in datatableCounty.Rows)
        //        {
        //            HuyenId = int.Parse(dr["id"].ToString());
        //        }

        //        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 3 and parentiddistrict = {0}", HuyenId), _Cn);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
        //        System.Data.DataTable datatableWard = new System.Data.DataTable();
        //        sqlAdapterWard.Fill(datatableWard);
        //        comboBox_Xa.Items.Clear();
        //        comboBox_Xa.Items.Add("Chọn");
        //        foreach (DataRow dr in datatableWard.Rows)
        //        {
        //            if (string.IsNullOrEmpty(dr["Ten"].ToString()))
        //                continue;

        //            comboBox_Xa.Items.Add(dr["Ten"].ToString());
        //        }
        //    }
        //}

        private void LoadCBVungDA(long idDA)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name FROM cecm_program_area_map where cecm_program_id = " + idDA), _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            DataRow dr = datatable.NewRow();
            dr["id"] = -1;
            dr["name"] = "Chưa chọn";
            datatable.Rows.InsertAt(dr, 0);
            comboBox_Vung.DataSource = datatable;
            comboBox_Vung.DisplayMember = "name";
            comboBox_Vung.ValueMember = "id";
        }

        private void comboBox_TenDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (comboBox_TenDA.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT startTime,endTime,id FROM cecm_programData where name = N'{0}'", comboBox_TenDA.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    //TimeBD.Text = dr["starttime"].ToString();
                    //TimeKT.Text = dr["endtime"].ToString();
                    DuanId = int.Parse(dr["id"].ToString());
                }

                comboBox_Vung.Items.Clear();
                comboBox_Vung.Text = "Chọn";
                SqlDataAdapter sqlAdapterPerson1 = new SqlDataAdapter("select * from cecm_program_area_map where cecm_program_id = " + DuanId, _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterPerson1);
                System.Data.DataTable datatablePerson1 = new System.Data.DataTable();
                sqlAdapterPerson1.Fill(datatablePerson1);
                foreach (DataRow dr in datatablePerson1.Rows)
                {
                    if (string.IsNullOrEmpty(dr["code"].ToString()))
                        continue;

                    comboBox_Vung.Items.Add(dr["code"].ToString());
                }
                comboBox_Vung.Items.Add("Khác ...");
            }
        }

        //private void comboBox_Xa_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SqlCommandBuilder sqlCommand = null;
        //    if (comboBox_Xa.SelectedItem != null)
        //    {
        //        SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Xa.SelectedItem.ToString()), _Cn);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
        //        System.Data.DataTable datatableCounty = new System.Data.DataTable();
        //        sqlAdapterCounty.Fill(datatableCounty);

        //        foreach (DataRow dr in datatableCounty.Rows)
        //        {
        //            XaId = int.Parse(dr["id"].ToString());
        //        }
        //    }
        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvBMVN.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            //string str = dgvRow.Tag as string;
            //int id_kqks = int.Parse(str);
            if (e.ColumnIndex == DoiRPBMSua.Index)
            {
                FrmThemmoiBomBCKVON frm = new FrmThemmoiBomBCKVON(dgvBMVN.Rows[e.RowIndex], (long)comboBox_Vung.SelectedValue);

                frm.ShowDialog();
                //LoadDataMine(VungId, VungId);
            }
            //delete column
            if (e.ColumnIndex == DoiRPBMXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                //SqlCommand cmd2 = new SqlCommand("DELETE FROM Cecm_VNTerrainMinePoint_CHA WHERE id = " + id_kqks, _Cn);
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
                //LoadDataMine(VungId, VungId);
                dgvBMVN.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            id_LoaiTV = "";
            id_DophuTV = "";
            id_PT = "";
            id_LoaiKV = "";
            id_LoaiDC = "";
            id_Loaixe = "";
            id_Diahinh = "";
            id_Loaidat = "";
            id_Thang = "";
            id_Mucdich = "";
            if (checkedListBox_LoaiTV.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_LoaiTV.CheckedItems.Count; i++)
                {
                    if (id_LoaiTV == "")
                    {

                        id_LoaiTV += checkedListBox_LoaiTV.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_LoaiTV += "+" + checkedListBox_LoaiTV.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_LoaiKV.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_LoaiKV.CheckedItems.Count; i++)
                {
                    if (id_LoaiKV == "")
                    {

                        id_LoaiKV += checkedListBox_LoaiKV.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_LoaiKV += "+" + checkedListBox_LoaiKV.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_Dophu.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_Dophu.CheckedItems.Count; i++)
                {
                    if (id_DophuTV == "")
                    {

                        id_DophuTV += checkedListBox_Dophu.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_DophuTV += "+" + checkedListBox_Dophu.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_LoaiDiachat.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_LoaiDiachat.CheckedItems.Count; i++)
                {
                    if (id_LoaiDC == "")
                    {

                        id_LoaiDC += checkedListBox_LoaiDiachat.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_LoaiDC += "+" + checkedListBox_LoaiDiachat.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_Loaixe.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_Loaixe.CheckedItems.Count; i++)
                {
                    if (id_Loaixe == "")
                    {

                        id_Loaixe += checkedListBox_Loaixe.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_Loaixe += "+" + checkedListBox_Loaixe.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_PTphatquang.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_PTphatquang.CheckedItems.Count; i++)
                {
                    if (id_PT == "")
                    {

                        id_PT += checkedListBox_PTphatquang.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_PT += "+" + checkedListBox_PTphatquang.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_Diahinh.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_Diahinh.CheckedItems.Count; i++)
                {
                    if (id_Diahinh == "")
                    {

                        id_Diahinh += checkedListBox_Diahinh.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_Diahinh += "+" + checkedListBox_Diahinh.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_Loaidat.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_Loaidat.CheckedItems.Count; i++)
                {
                    if (id_Loaidat == "")
                    {

                        id_Loaidat += checkedListBox_Loaidat.CheckedItems[i].ToString().Split('.')[0];
                    }
                    else
                    {
                        id_Loaidat += "+" + checkedListBox_Loaidat.CheckedItems[i].ToString().Split('.')[0];
                    }
                }
            }
            if (checkedListBox_Thang.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_Thang.CheckedItems.Count; i++)
                {
                    if (id_Thang == "")
                    {

                        id_Thang += checkedListBox_Thang.CheckedItems[i].ToString().Split(' ')[1];
                    }
                    else
                    {
                        id_Thang += "+" + checkedListBox_Thang.CheckedItems[i].ToString().Split(' ')[1];
                    }
                }
            }
            //if (comboBox_TenDA.Text == "" || txtMaKV.Text == "" || comboBox_Uutien.Text == "" || comboBox_TinhtrangKV.Text == "" || comboBox_Mucdichsd.Text == ""|| comboBox_Nguoihuongloi.Text == "" || VungId == -1 || DuanId == -1) 
            //{
            //    MessageBox.Show("Vui lòng kiểm tra lại thông tin ở combobox", "Lỗi");
            //}
            //else
            //{
            //    if(txtDai.Text == "")
            //    {
            //        txtDai.Text = "0";
            //    }
            //    if (txtRong.Text == "")
            //    {
            //        txtRong.Text = "0";
            //    }
            //    if (txt_Dientich.Text == "")
            //    {
            //        txt_Dientich.Text = "0";
            //    }
            //    if (comboBox_Huyen.Text == "" || comboBox_Tinh.Text == "" || comboBox_Xa.Text == "" || comboBox_Vung.Text == "")
            //    {
            //        MessageBox.Show("Vui lòng kiểm tra lại tỉnh huyện xã và khu vực khảo sát");
            //    }
            //    else
            //    {
            //        bool success = UpdateInfomation(id_BSKQ);
            //    }
            //}
            isLuuClicked = true;
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                isLuuClicked = false;
                return;
            }
            isLuuClicked = false;
            _cn.BeginTransaction();
            bool success = UpdateInfomation(id_BSKQ);
            if(id_BSKQ <= 0)
            {
                id_BSKQ = UtilsDatabase.GetLastIdIndentifyTable(_cn, "cecm_ReportPollutionArea");
            }
            bool successDeleteSub = DeleteSubTable();
            bool successBMVN = UpdateBMVN();
            if (success && successDeleteSub && successBMVN)
            {
                _cn.Transaction.Commit();
                MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                this.DialogResult = DialogResult.OK;

            }
            else
            {
                _cn.Transaction.Rollback();
                MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                this.DialogResult = DialogResult.None;
            }
            this.Close();
        }

        private bool DeleteSubTable()
        {
            try
            {
                SqlCommand cmd2 = new SqlCommand("DELETE FROM cecm_ReportPollutionArea_BMVN WHERE cecm_ReportPollutionArea_id = " + id_BSKQ, _cn.Connection as SqlConnection);
                cmd2.Transaction = _cn.Transaction as SqlTransaction;
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
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
                    CecmReportPollutionAreaBMVN bmvn = dataGridViewRow.Tag as CecmReportPollutionAreaBMVN;
                    //if (id_kqks != 0),Dientichcaybui,Dientichcayto,MatdoTB,Sotinhieu=,Ghichu,Dientichtretruc,Matdothua,Matdoday,Matdo, DaxulyM2
                    //{
                    string sql =
                        "INSERT INTO cecm_ReportPollutionArea_BMVN" +
                        "(Kyhieu, Loai, idRectangle,SL,Tinhtrang,Vido,Kinhdo,Deep,Kichthuoc, PPXuLy, cecm_ReportPollutionArea_id, Ghichu) " +
                        "VALUES(@Kyhieu, @Loai, @idRectangle, @SL,@Tinhtrang,@Vido,@Kinhdo,@Deep,@Kichthuoc, @PPXuLy, @cecm_ReportPollutionArea_id, @Ghichu)";
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
                    Tinhtrang.Value = bmvn.Tinhtrang != null ? bmvn.Tinhtrang : "";
                    cmd2.Parameters.Add(Tinhtrang);

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

                    SqlParameter PPXuLy = new SqlParameter("@PPXuLy", SqlDbType.NVarChar, 50);
                    PPXuLy.Value = bmvn.PPXuLy != null ? bmvn.PPXuLy : "";
                    cmd2.Parameters.Add(PPXuLy);

                    SqlParameter cecm_ReportPollutionArea_id = new SqlParameter("@cecm_ReportPollutionArea_id", SqlDbType.BigInt);
                    cecm_ReportPollutionArea_id.Value = id_BSKQ;
                    cmd2.Parameters.Add(cecm_ReportPollutionArea_id);

                    SqlParameter Ghichu = new SqlParameter("@Ghichu", SqlDbType.NVarChar, 1000);
                    Ghichu.Value = bmvn.Ghichu != null ? bmvn.Ghichu : "";
                    cmd2.Parameters.Add(Ghichu);

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

        private void comboBox_Mucdichsd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Mucdichsd.SelectedItem.ToString() == "5. Khác")
            {
                txtMucdich.Enabled = true;
            }
            else
            {
                txtMucdich.Text = "";
                txtMucdich.Enabled = false;
            }
        }

        private void cbb_Doiso_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            if (cbb_Doiso.SelectedItem.ToString() != "Khác ...")
            {
                if (cbb_Doiso.SelectedItem != null)
                {
                    SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter(string.Format("SELECT id, master FROM [cert_command_person] where name = N'{0}'", cbb_Doiso.SelectedItem.ToString()), _Cn);
                    sqlCommand = new SqlCommandBuilder(sqlAdapterPerson);
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
                }
            }
            else
            {
                PersonId = "0";
                txtDoingoai.Enabled = true;
                TxtDoitruongngoai.Enabled = true;
                txtDoitruong.Text = "";
            }
        }

        private void txtDoitruong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaKV_TextChanged(object sender, EventArgs e)
        {
            Datagridview2_Load(VungId);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        //private void txtMucdich_TextChanged(object sender, EventArgs e)
        //{
        //    if(txtMucdich.Text != "")
        //    {
        //        comboBox_Mucdichsd.Items.Clear();
        //    }
        //    else
        //    {
        //        if(comboBox_Mucdichsd.Items.Count != 0)
        //        {
        //            comboBox_Mucdichsd.Items.Add("1.Nông nghiệp");
        //            comboBox_Mucdichsd.Items.Add("2.Lâm nghiệp");
        //            comboBox_Mucdichsd.Items.Add("3.Xây dựng hạ tầng");
        //            comboBox_Mucdichsd.Items.Add("4.Khu dân cư");
        //            comboBox_Mucdichsd.Items.Add("5.Khác");
        //        }
        //    }
        //}

        private void txtDai_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtValidateS.Visible = false;
                txt_Dientich.Text = (double.Parse(txtDai.Text) * double.Parse(txtRong.Text)).ToString();
            }
            catch
            {
                txtValidateS.Visible = true;
            }
        }

        private void txtRong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtValidateS.Visible = false;
                txt_Dientich.Text = (double.Parse(txtDai.Text) * double.Parse(txtRong.Text)).ToString();
            }
            catch
            {
                txtValidateS.Visible = true;
            }
        }

        private void FrmThemmoiBCKVON_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FrmThemmoiBCKVON_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void comboBox_TenDA_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                LoadDVKS();
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT startTime,endTime,id FROM cecm_programData where id = {0}", comboBox_TenDA.SelectedValue), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    //TimeBD.Text = dr["starttime"].ToString();
                    //TimeKT.Text = dr["endtime"].ToString();
                    DuanId = int.Parse(dr["id"].ToString());
                }

                //comboBox_Vung.Items.Clear();
                //comboBox_Vung.Text = "Chọn";

                //SqlDataAdapter sqlAdapterPerson1 = new SqlDataAdapter("select * from cecm_program_area_map where cecm_program_id = " + DuanId, _Cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterPerson1);
                //System.Data.DataTable datatablePerson1 = new System.Data.DataTable();
                //sqlAdapterPerson1.Fill(datatablePerson1);
                //foreach (DataRow dr in datatablePerson1.Rows)
                //{
                //    if (string.IsNullOrEmpty(dr["code"].ToString()))
                //        continue;

                //    comboBox_Vung.Items.Add(dr["code"].ToString());
                //}
                //comboBox_Vung.Items.Add("Khác ...");
                
                LoadCBVungDA((long)comboBox_TenDA.SelectedValue);
                LoadCBStaff(cbGiamSat);
                LoadCBStaff(cbChiHuyCT);
            }
        }

        private void comboBox_Vung_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //if (comboBox_Vung.SelectedItem.ToString() != "Khác ...")
            //{
                if (comboBox_Vung.SelectedItem != null)
                {
                    SqlDataAdapter sqlAdapterPerson = new SqlDataAdapter(string.Format("SELECT id FROM cecm_program_area_map where code = N'{0}'", comboBox_Vung.SelectedItem.ToString()), _Cn);
                    sqlCommand = new SqlCommandBuilder(sqlAdapterPerson);
                    System.Data.DataTable datatablePerson = new System.Data.DataTable();
                    sqlAdapterPerson.Fill(datatablePerson);
                    foreach (DataRow dr in datatablePerson.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["id"].ToString()))
                            continue;

                        VungId = int.Parse(dr["id"].ToString());
                    }
                    Datagridview2_Load(VungId);
                    //LoadDataMine(VungId, VungId);
                    LoadDataMine_onChange_cbVungDA();
                }
            //}
        }

        private void comboBox_Vung_Validating(object sender, CancelEventArgs e)
        {

        }

        private void comboBox_TenDA_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_TenDA.SelectedValue == null)
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(comboBox_TenDA, "Chưa chọn dự án");
                return;
            }
            if ((long)comboBox_TenDA.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_TenDA, "");
            }
            else
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(comboBox_TenDA, "Chưa chọn dự án");
            }
        }

        private void cbDVKS_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if(cbDVKS.SelectedValue == null)
            {
                e.Cancel = true;
                //cbDVKS.Focus();
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
                //cbDVKS.Focus();
                errorProvider1.SetError(cbDVKS, "Chưa chọn đơn vị khảo sát");
            }
        }

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_Tinh.Text == "")
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
            if (string.IsNullOrEmpty(comboBox_Tinh.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Huyen, "");
                return;
            }
            if (comboBox_Huyen.Text == "")
            {
                e.Cancel = true;
                //comboBox_Huyen.Focus();
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
            if (string.IsNullOrEmpty(comboBox_Huyen.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Xa, "");
                return;
            }
            if (comboBox_Xa.Text == "")
            {
                e.Cancel = true;
                //comboBox_Xa.Focus();
                errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Xa, "");
            }
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

        private void tbCode_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCode.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbCode, "Chưa nhập số hiệu");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbCode, "");
            }
        }

        private void comboBox_Tinh_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBox_Tinh.SelectedValue is long)
            {
                UtilsDatabase.buildCombobox(comboBox_Huyen, "SELECT id, Ten FROM cecm_provinces WHERE parent_id = " + comboBox_Tinh.SelectedValue, "id", "Ten");
            }
            
        }

        private void comboBox_Huyen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_Huyen.SelectedValue is long)
            {
                UtilsDatabase.buildCombobox(comboBox_Xa, "SELECT id, Ten FROM cecm_provinces WHERE parentiddistrict = " + comboBox_Huyen.SelectedValue, "id", "Ten");
            }
        }
    }
}