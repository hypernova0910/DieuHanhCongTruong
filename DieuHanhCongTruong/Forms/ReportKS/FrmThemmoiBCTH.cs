using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
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

namespace VNRaPaBomMin
{
    public partial class FrmThemmoiBCTH : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        //public int DuanId = 0;
        //public int TinhId = 0;
        //public int HuyenId = 0;
        //public int XaId = 0;
        //public int DoitruongId = 0;
        //public int ChihuyId = 0;
        public FrmThemmoiBCTH(int i)
        {
            id_BSKQ = i;
            _Cn = frmLoggin.sqlCon;
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

        private void LoadDVKS()
        {
            cbDVKS.DataSource = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter(
                    string.Format(@"SELECT id, name 
                    FROM cert_department 
                    WHERE id_web in (SELECT dept_id_web FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0}) 
                    or id in (SELECT dept_id FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0})", comboBox_TenDA.SelectedValue, TableName.KHAO_SAT), _Cn);
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

        //Hiếu làm thêm phần này
        private void LoadCBStaff(ComboBox cb)
        {
            cb.DataSource = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                //SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + comboBox_TenDA.SelectedValue, _Cn);
                ////sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
                //System.Data.DataTable datatableStaff = new System.Data.DataTable();
                //sqlAdapterProgram.Fill(datatableStaff);
                //DataRow drStaff = datatableStaff.NewRow();
                //drStaff["id"] = -1;
                //drStaff["nameId"] = "Chưa chọn";
                //datatableStaff.Rows.InsertAt(drStaff, 0);
                //cb.DataSource = datatableStaff;
                //cb.ValueMember = "id";
                //cb.DisplayMember = "nameId";
                UtilsDatabase.buildCombobox(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + comboBox_TenDA.SelectedValue, "id", "nameId");
            }
            
        }

        //A Tân làm phần này
        private void GetAllStaffWithIdProgram(System.Windows.Forms.ComboBox cb)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterCounty1 = new SqlDataAdapter(string.Format("SELECT * FROM Cecm_ProgramStaff where cecmProgramId = {0}", comboBox_TenDA.SelectedValue), _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterCounty1);
            System.Data.DataTable datatableCounty1 = new System.Data.DataTable();
            sqlAdapterCounty1.Fill(datatableCounty1);
            cb.Items.Clear();
            cb.Items.Add("Chọn");
            foreach (DataRow dr in datatableCounty1.Rows)
            {
                if (string.IsNullOrEmpty(dr["nameId"].ToString()))
                    continue;
                var a = dr["id"].ToString() + "-" + dr["nameId"].ToString();
                cb.Items.Add(a);
            }
        }
        private int ChooseCBB(System.Windows.Forms.ComboBox cb)
        {
            SqlCommandBuilder sqlCommand = null;
            var id = 0;
            if (cb.SelectedItem != null)
            {
                try
                {
                    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM Cecm_ProgramStaff where id = {0} and cecmProgramId = {1}", cb.SelectedItem.ToString().Split('-')[0], comboBox_TenDA.SelectedValue), _Cn);
                    sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                    System.Data.DataTable datatableWard = new System.Data.DataTable();
                    sqlAdapterWard.Fill(datatableWard);

                    foreach (DataRow dr in datatableWard.Rows)
                    {
                        id = int.Parse(dr["id"].ToString());
                        cb.Text = dr["nameId"].ToString();
                    }
                }
                catch
                {

                }
            }
            return id;
        }
        private void UpdateCheckboxList(string A, CheckedListBox B)
        {
            if (A != "")
            {
                for (int i = 0; i < A.Split('-').Length; i++)
                {
                    for (int k = 0; k < B.Items.Count; k++)
                    {
                        if (k == int.Parse(A.Split('-')[i]) - 1)
                        {
                            B.SetItemCheckState(k, CheckState.Checked);
                        }

                    }

                }
            }
        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT cecm_BaocaoTonghop.*, cecm_program1.name as 'TenDuan',cecm_program1.id as 'Duanid',[Hopphan],Tinh.Ten as 'TenTinh',Tinh.id as 'Tinhid',Huyen.Ten as 'TenHuyen',Huyen.id as 'Huyenid',Xa.Ten as 'TenXa',Xa.id as 'Xaid' FROM cecm_BaocaoTonghop left join cecm_programData as cecm_program1 on cecm_program1.id = cecm_BaocaoTonghop.Duan left join cecm_provinces as Tinh on cecm_BaocaoTonghop.Tinh = Tinh.id left join cecm_provinces as Huyen on cecm_BaocaoTonghop.Huyen = Huyen.id left join cecm_provinces as Xa on cecm_BaocaoTonghop.Xa = Xa.id where cecm_BaocaoTonghop.id = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        tbMabaocao.Text = dr["Mabaocao"].ToString();
                        txtHopphan.Text = dr["Hopphan"].ToString();
                        if(long.TryParse(dr["Duan"].ToString(), out long Duan))
                        {
                            comboBox_TenDA.SelectedValue = Duan;
                        }
                        if (long.TryParse(dr["DVKS_id"].ToString(), out long DVKS_id))
                        {
                            cbDVKS.SelectedValue = DVKS_id;
                        }
                        comboBox_Tinh.SelectedValue = long.TryParse(dr["Tinhid"].ToString(), out long Tinh) ? Tinh : -1;
                        comboBox_Huyen.SelectedValue = long.TryParse(dr["Huyenid"].ToString(), out long Huyen) ? Huyen : -1;
                        comboBox_Xa.SelectedValue = long.TryParse(dr["Xaid"].ToString(), out long Xa) ? Xa : -1;
                        txtThon.Text = dr["Thon"].ToString();
                        comboBox_TenDA.Text = dr["TenDuan"].ToString();
                        textBox_Diahinh.Text = dr["Diahinh"].ToString();
                        textBox_Loaidat.Text = dr["Loaidat"].ToString();
                        textBox_Thoitiet.Text = dr["Thoitiet"].ToString();
                        textBox_HTON.Text = dr["Hientrangonhiem"].ToString();
                        richTextBox_LSTN.Text = dr["Lichsutainan"].ToString();
                        richTextBox_LSHD.Text = dr["Lichsuhoatdong"].ToString();
                        textBoxSD_Tong.Text = dr["DTsd"].ToString();
                        textBoxNN_Tong.Text = dr["DTnghingo"].ToString();
                        tbKhongON.Text = dr["DT_KhongON"].ToString();

                        textBoxSD_1.Text = dr["Datthocu"].ToString().Split('-')[0];
                        textBoxNN_1.Text = dr["Datthocu"].ToString().Split('-')[1];
                        textBoxSD_2.Text = dr["Dattrongtrot"].ToString().Split('-')[0];
                        textBoxNN_2.Text = dr["Dattrongtrot"].ToString().Split('-')[1];
                        textBoxSD_3.Text = dr["Dattronglaunam"].ToString().Split('-')[0];
                        textBoxNN_3.Text = dr["Dattronglaunam"].ToString().Split('-')[1];
                        textBoxSD_4.Text = dr["Matnuoc"].ToString().Split('-')[0];
                        textBoxNN_4.Text = dr["Matnuoc"].ToString().Split('-')[1];
                        textBoxSD_5.Text = dr["Datrungtunhien"].ToString().Split('-')[0];
                        textBoxNN_5.Text = dr["Datrungtunhien"].ToString().Split('-')[1];
                        textBoxSD_6.Text = dr["Datxd"].ToString().Split('-')[0];
                        textBoxNN_6.Text = dr["Datxd"].ToString().Split('-')[1];
                        textBoxSD_7.Text = dr["Datgiaothong"].ToString().Split('-')[0];
                        textBoxNN_7.Text = dr["Datgiaothong"].ToString().Split('-')[1];
                        textBoxSD_8.Text = dr["Datthuyloi"].ToString().Split('-')[0];
                        textBoxNN_8.Text = dr["Datthuyloi"].ToString().Split('-')[1];
                        textBoxSD_9.Text = dr["Datnghiadia"].ToString().Split('-')[0];
                        textBoxNN_9.Text = dr["Datnghiadia"].ToString().Split('-')[1];
                        textBoxSD_10.Text = dr["Cacloaidatkhac"].ToString().Split('-')[0];
                        textBoxNN_10.Text = dr["Cacloaidatkhac"].ToString().Split('-')[1];

                        tbKhongON_1.Text = dr["Datthocu2"].ToString();
                        tbKhongON_2.Text = dr["Dattrongtrot2"].ToString();
                        tbKhongON_3.Text = dr["Dattronglaunam2"].ToString();
                        tbKhongON_4.Text = dr["Matnuoc2"].ToString();
                        tbKhongON_5.Text = dr["Datrungtunhien2"].ToString();
                        tbKhongON_6.Text = dr["Datxd2"].ToString();
                        tbKhongON_7.Text = dr["Datgiaothong2"].ToString();
                        tbKhongON_8.Text = dr["Datthuyloi2"].ToString();
                        tbKhongON_9.Text = dr["Datnghiadia2"].ToString();
                        tbKhongON_10.Text = dr["Cacloaidatkhac2"].ToString();

                        textBox_DTKS.Text = dr["DTKS"].ToString();
                        textBoxTH_Tong.Text = dr["Tongsotinhieu"].ToString();
                        textBoxTH_1.Text = dr["Tinhieu"].ToString().Split('-')[0];
                        textBoxTH_2.Text = dr["Tinhieu"].ToString().Split('-')[1];
                        textBoxMD_Tong.Text = dr["Tongmatdo"].ToString();
                        textBoxMD_1.Text = dr["Matdo"].ToString().Split('-')[0];
                        textBoxMD_2.Text = dr["Matdo"].ToString().Split('-')[1];

                        textBoxVN1_1.Text = dr["Bompha"].ToString().Split('-')[0];
                        textBoxVN1_2.Text = dr["Bompha"].ToString().Split('-')[1];
                        textBoxVN1_3.Text = dr["Bompha"].ToString().Split('-')[2];
                        textBoxVN1_4.Text = dr["Bompha"].ToString().Split('-')[3];
                        textBoxVN1_5.Text = dr["Bompha"].ToString().Split('-')[4];
                        textBoxVN1_6.Text = dr["Bompha"].ToString().Split('-')[5];

                        textBoxVN2_1.Text = dr["Danphao"].ToString().Split('-')[0];
                        textBoxVN2_2.Text = dr["Danphao"].ToString().Split('-')[1];
                        textBoxVN2_3.Text = dr["Danphao"].ToString().Split('-')[2];
                        textBoxVN2_4.Text = dr["Danphao"].ToString().Split('-')[3];
                        textBoxVN2_5.Text = dr["Danphao"].ToString().Split('-')[4];
                        textBoxVN2_6.Text = dr["Danphao"].ToString().Split('-')[5];

                        textBoxVN3_1.Text = dr["Tenlua"].ToString().Split('-')[0];
                        textBoxVN3_2.Text = dr["Tenlua"].ToString().Split('-')[1];
                        textBoxVN3_3.Text = dr["Tenlua"].ToString().Split('-')[2];
                        textBoxVN3_4.Text = dr["Tenlua"].ToString().Split('-')[3];
                        textBoxVN3_5.Text = dr["Tenlua"].ToString().Split('-')[4];
                        textBoxVN3_6.Text = dr["Tenlua"].ToString().Split('-')[5];

                        textBoxVN4_1.Text = dr["Luudan"].ToString().Split('-')[0];
                        textBoxVN4_2.Text = dr["Luudan"].ToString().Split('-')[1];
                        textBoxVN4_3.Text = dr["Luudan"].ToString().Split('-')[2];
                        textBoxVN4_4.Text = dr["Luudan"].ToString().Split('-')[3];
                        textBoxVN4_5.Text = dr["Luudan"].ToString().Split('-')[4];
                        textBoxVN4_6.Text = dr["Luudan"].ToString().Split('-')[5];

                        textBoxVN5_1.Text = dr["Bombi"].ToString().Split('-')[0];
                        textBoxVN5_2.Text = dr["Bombi"].ToString().Split('-')[1];
                        textBoxVN5_3.Text = dr["Bombi"].ToString().Split('-')[2];
                        textBoxVN5_4.Text = dr["Bombi"].ToString().Split('-')[3];
                        textBoxVN5_5.Text = dr["Bombi"].ToString().Split('-')[4];
                        textBoxVN5_6.Text = dr["Bombi"].ToString().Split('-')[5];

                        textBoxVN6_1.Text = dr["Mintrongtang"].ToString().Split('-')[0];
                        textBoxVN6_2.Text = dr["Mintrongtang"].ToString().Split('-')[1];
                        textBoxVN6_3.Text = dr["Mintrongtang"].ToString().Split('-')[2];
                        textBoxVN6_4.Text = dr["Mintrongtang"].ToString().Split('-')[3];
                        textBoxVN6_5.Text = dr["Mintrongtang"].ToString().Split('-')[4];
                        textBoxVN6_6.Text = dr["Mintrongtang"].ToString().Split('-')[5];

                        textBoxVN7_1.Text = dr["Mintrongnguoi"].ToString().Split('-')[0];
                        textBoxVN7_2.Text = dr["Mintrongnguoi"].ToString().Split('-')[1];
                        textBoxVN7_3.Text = dr["Mintrongnguoi"].ToString().Split('-')[2];
                        textBoxVN7_4.Text = dr["Mintrongnguoi"].ToString().Split('-')[3];
                        textBoxVN7_5.Text = dr["Mintrongnguoi"].ToString().Split('-')[4];
                        textBoxVN7_6.Text = dr["Mintrongnguoi"].ToString().Split('-')[5];

                        textBoxVN8_1.Text = dr["Cacloaivatno"].ToString().Split('-')[0];
                        textBoxVN8_2.Text = dr["Cacloaivatno"].ToString().Split('-')[1];
                        textBoxVN8_3.Text = dr["Cacloaivatno"].ToString().Split('-')[2];
                        textBoxVN8_4.Text = dr["Cacloaivatno"].ToString().Split('-')[3];
                        textBoxVN8_5.Text = dr["Cacloaivatno"].ToString().Split('-')[4];
                        textBoxVN8_6.Text = dr["Cacloaivatno"].ToString().Split('-')[5];

                        textBoxVN9_1.Text = dr["Satthep"].ToString().Split('-')[0];
                        textBoxVN9_2.Text = dr["Satthep"].ToString().Split('-')[1];
                        textBoxVN9_3.Text = dr["Satthep"].ToString().Split('-')[2];
                        textBoxVN9_4.Text = dr["Satthep"].ToString().Split('-')[3];
                        textBoxVN9_5.Text = dr["Satthep"].ToString().Split('-')[4];
                        textBoxVN9_6.Text = dr["Satthep"].ToString().Split('-')[5];

                        textBoxDT_Tong.Text = dr["DTrapha"].ToString().Split('-')[0];
                        textBoxDT_1.Text = dr["DTrapha"].ToString().Split('-')[1];
                        textBoxDT_2.Text = dr["DTrapha"].ToString().Split('-')[2];
                        textBoxDT_3.Text = dr["DTrapha"].ToString().Split('-')[3];
                        textBoxDT_4.Text = dr["DTrapha"].ToString().Split('-')[4];
                        textBoxDT_5.Text = dr["DTrapha"].ToString().Split('-')[5];
                        textBoxDT_6.Text = dr["DTrapha"].ToString().Split('-')[6];
                        textBoxDT_7.Text = dr["DTrapha"].ToString().Split('-')[7];
                        textBoxDT_8.Text = dr["DTrapha"].ToString().Split('-')[8];
                        textBoxDT_9.Text = dr["DTrapha"].ToString().Split('-')[9];
                        textBoxDT_10.Text = dr["DTrapha"].ToString().Split('-')[10];

                        textBox_SLTH5m.Text = dr["SLtinhieu5m"].ToString();
                        //UpdateCheckboxList(dr["Mucdoonhiem"].ToString(), checkedListBox_Mucdo);
                        comboBox_Mucdo.Text = dr["Mucdoonhiem"].ToString();
                        textBox_Phanloai.Text = dr["Phanloai"].ToString();
                        textBox_Capdat.Text = dr["Capdat"].ToString();
                        if (long.TryParse(dr["Chihuy_id"].ToString(), out long Chihuy_id))
                        {
                            cbChihuy.SelectedValue = Chihuy_id;
                        }
                        //ChihuyId = int.Parse( dr["Chihuy_id"].ToString());
                        //cbChihuy.Text = dr["Chihuy"].ToString();
                        textBox_Chihuy_other.Text = dr["Chihuy_other"].ToString();
                        if (long.TryParse(dr["Doitruong_id"].ToString(), out long Doitruong_id))
                        {
                            cbDoitruong.SelectedValue = Doitruong_id;
                        }
                        //DoitruongId = int.Parse(dr["Doitruong_id"].ToString());
                        //cbDoitruong.Text = dr["Doitruong"].ToString();
                        textBox_Doitruong_other.Text = dr["Doitruong_other"].ToString();

                        if (long.TryParse(dr["Giamsat_id"].ToString(), out long Giamsat_id))
                        {
                            cbGiamSat.SelectedValue = Giamsat_id;
                        }
                        tbGiamSatOther.Text = dr["Giamsat_other"].ToString();

                        lbPhieudieutra.Text = dr["PhieudieutraLink"].ToString();
                        lbBCKS.Text = dr["BaocaoLink"].ToString();
                        lbBando.Text = dr["BandoLink"].ToString();

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
                        "UPDATE [dbo].[cecm_BaocaoTonghop] SET " +
                        "[Duan] = @Duan ," +
                        "[DVKS_id] = @DVKS_id ," +
                        "[Hopphan] =  @Hopphan ," +
                        "[Mabaocao] =  @Mabaocao ," +
                        "[Tinh] =  @Tinh ," +
                        "[Huyen] = @Huyen ," +
                        "[Xa] =  @Xa ," +
                        "[Thon] =  @Thon ," +
                        "[Diahinh] =  @Diahinh ," +
                        "[Loaidat] =  @Loaidat ," +
                        "[Thoitiet] =  @Thoitiet ," +
                        "[Hientrangonhiem] =  @Hientrangonhiem ," +
                        "[Lichsutainan] = @Lichsutainan ," +
                        "[Lichsuhoatdong] = @Lichsuhoatdong ," +
                        "[DTsd] = @DTsd ," +
                        "[DTnghingo] = @DTnghingo ," +
                        "[DT_KhongON] = @DT_KhongON ," +
                        "[Datthocu] = @Datthocu ," +
                        "[Dattrongtrot] =  @Dattrongtrot ," +
                        "[Dattronglaunam] =  @Dattronglaunam ," +
                        "[Matnuoc] =  @Matnuoc ," +
                        "[Datrungtunhien] =  @Datrungtunhien ," +
                        "[Datxd] =  @Datxd ," +
                        "[Datgiaothong] =  @Datgiaothong ," +
                        "[Datthuyloi] =  @Datthuyloi ," +
                        "[Datnghiadia] =  @Datnghiadia ," +
                        "[Cacloaidatkhac] =  @Cacloaidatkhac ," +
                        "[Datthocu2] = @Datthocu2 ," +
                        "[Dattrongtrot2] =  @Dattrongtrot2 ," +
                        "[Dattronglaunam2] =  @Dattronglaunam2 ," +
                        "[Matnuoc2] =  @Matnuoc2 ," +
                        "[Datrungtunhien2] =  @Datrungtunhien2 ," +
                        "[Datxd2] =  @Datxd2 ," +
                        "[Datgiaothong2] =  @Datgiaothong2 ," +
                        "[Datthuyloi2] =  @Datthuyloi2 ," +
                        "[Datnghiadia2] =  @Datnghiadia2 ," +
                        "[Cacloaidatkhac2] =  @Cacloaidatkhac2 ," +
                        "[DTKS] =  @DTKS ," +
                        "[Tongsotinhieu] =  @Tongsotinhieu ," +
                        "[Tinhieu] =  @Tinhieu ," +
                        "[Tongmatdo] =  @Tongmatdo ," +
                        "[Matdo] =  @Matdo ," +
                        "[Bompha] =  @Bompha ," +
                        "[Danphao] =  @Danphao ," +
                        "[Tenlua] =  @Tenlua ," +
                        "[Luudan] =  @Luudan ," +
                        "[Bombi] =  @Bombi ," +
                        "[Mintrongtang] =  @Mintrongtang ," +
                        "[Mintrongnguoi] =  @Mintrongnguoi ," +
                        "[Cacloaivatno] =  @Cacloaivatno ," +
                        "[Satthep] = @Satthep ," +
                        "[SLtinhieu5m] = @SLtinhieu5m ," +
                        "[Mucdoonhiem] =  @Mucdoonhiem ," +
                        "[Phanloai] =  @Phanloai ," +
                        "[Capdat] = @Capdat ," +
                        "[DTrapha] =  @DTrapha ," +
                        "[Chihuy_id] =  @Chihuy_id," +
                        "[Chihuy] =  @Chihuy," +
                        "[Chihuy_other] =  @Chihuy_other ," +
                        "[Doitruong_id] =  @Doitruong_id," +
                        "[Doitruong] =  @Doitruong," +
                        "[Doitruong_other] =  @Doitruong_other," +
                        "[Giamsat_id] =  @Giamsat_id," +
                        "[Giamsat_other] =  @Giamsat_other," +
                        "[PhieudieutraLink] = @PhieudieutraLink," +
                        "[BaocaoLink] = @BaocaoLink," +
                        "[BandoLink] = @BandoLink " +
                        "WHERE id = " + dem, _Cn);
                    SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.BigInt);
                    Duan.Value = comboBox_TenDA.SelectedValue;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                    DVKS_id.Value = cbDVKS.SelectedValue;
                    cmd2.Parameters.Add(DVKS_id);

                    SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 200);
                    Hopphan.Value = txtHopphan.Text;
                    cmd2.Parameters.Add(Hopphan);

                    SqlParameter Mabaocao = new SqlParameter("@Mabaocao", SqlDbType.NVarChar, 50);
                    Mabaocao.Value = tbMabaocao.Text;
                    cmd2.Parameters.Add(Mabaocao);

                    SqlParameter Tinh = new SqlParameter("@Tinh", SqlDbType.Int);
                    Tinh.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(Tinh);

                    SqlParameter Huyen = new SqlParameter("@Huyen", SqlDbType.Int);
                    Huyen.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(Huyen);

                    SqlParameter Xa = new SqlParameter("@Xa", SqlDbType.Int);
                    Xa.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(Xa);

                    SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 200);
                    Thon.Value = txtThon.Text;
                    cmd2.Parameters.Add(Thon);

                    SqlParameter Diahinh = new SqlParameter("@Diahinh", SqlDbType.NVarChar, 200);
                    Diahinh.Value = textBox_Diahinh.Text;
                    cmd2.Parameters.Add(Diahinh);

                    SqlParameter Loaidat = new SqlParameter("@Loaidat", SqlDbType.NVarChar, 200);
                    Loaidat.Value = textBox_Loaidat.Text;
                    cmd2.Parameters.Add(Loaidat);

                    SqlParameter Thoitiet = new SqlParameter("@Thoitiet", SqlDbType.NVarChar, 200);
                    Thoitiet.Value = textBox_Thoitiet.Text;
                    cmd2.Parameters.Add(Thoitiet);

                    SqlParameter Hientrangonhiem = new SqlParameter("@Hientrangonhiem", SqlDbType.NVarChar, 200);
                    Hientrangonhiem.Value = textBox_HTON.Text;
                    cmd2.Parameters.Add(Hientrangonhiem);

                    SqlParameter Lichsutainan = new SqlParameter("@Lichsutainan", SqlDbType.NVarChar, 2200);
                    Lichsutainan.Value = richTextBox_LSTN.Text;
                    cmd2.Parameters.Add(Lichsutainan);

                    SqlParameter Lichsuhoatdong = new SqlParameter("@Lichsuhoatdong", SqlDbType.NVarChar, 2200);
                    Lichsuhoatdong.Value = richTextBox_LSHD.Text;
                    cmd2.Parameters.Add(Lichsuhoatdong);

                    SqlParameter DTsd = new SqlParameter("@DTsd", SqlDbType.NVarChar, 200);
                    DTsd.Value = textBoxSD_Tong.Text;
                    cmd2.Parameters.Add(DTsd);

                    SqlParameter DTnghingo = new SqlParameter("@DTnghingo", SqlDbType.NVarChar, 200);
                    DTnghingo.Value = textBoxNN_Tong.Text;
                    cmd2.Parameters.Add(DTnghingo);

                    SqlParameter Datthocu = new SqlParameter("@Datthocu", SqlDbType.NVarChar, 200);
                    Datthocu.Value = textBoxSD_1.Text + "-" + textBoxNN_1.Text;
                    cmd2.Parameters.Add(Datthocu);

                    SqlParameter Dattrongtrot = new SqlParameter("@Dattrongtrot", SqlDbType.NVarChar, 200);
                    Dattrongtrot.Value = textBoxSD_2.Text + "-" + textBoxNN_2.Text;
                    cmd2.Parameters.Add(Dattrongtrot);

                    SqlParameter Dattronglaunam = new SqlParameter("@Dattronglaunam", SqlDbType.NVarChar, 200);
                    Dattronglaunam.Value = textBoxSD_3.Text + "-" + textBoxNN_3.Text;
                    cmd2.Parameters.Add(Dattronglaunam);

                    SqlParameter Matnuoc = new SqlParameter("@Matnuoc", SqlDbType.NVarChar, 200);
                    Matnuoc.Value = textBoxSD_4.Text + "-" + textBoxNN_4.Text;
                    cmd2.Parameters.Add(Matnuoc);

                    SqlParameter Datrungtunhien = new SqlParameter("@Datrungtunhien", SqlDbType.NVarChar, 200);
                    Datrungtunhien.Value = textBoxSD_5.Text + "-" + textBoxNN_5.Text;
                    cmd2.Parameters.Add(Datrungtunhien);

                    SqlParameter Datxd = new SqlParameter("@Datxd", SqlDbType.NVarChar, 200);
                    Datxd.Value = textBoxSD_6.Text + "-" + textBoxNN_6.Text;
                    cmd2.Parameters.Add(Datxd);

                    SqlParameter Datgiaothong = new SqlParameter("@Datgiaothong", SqlDbType.NVarChar, 200);
                    Datgiaothong.Value = textBoxSD_7.Text + "-" + textBoxNN_7.Text;
                    cmd2.Parameters.Add(Datgiaothong);

                    SqlParameter Datthuyloi = new SqlParameter("@Datthuyloi", SqlDbType.NVarChar, 200);
                    Datthuyloi.Value = textBoxSD_8.Text + "-" + textBoxNN_8.Text;
                    cmd2.Parameters.Add(Datthuyloi);

                    SqlParameter Datnghiadia = new SqlParameter("@Datnghiadia", SqlDbType.NVarChar, 200);
                    Datnghiadia.Value = textBoxSD_9.Text + "-" + textBoxNN_9.Text;
                    cmd2.Parameters.Add(Datnghiadia);

                    SqlParameter Cacloaidatkhac = new SqlParameter("@Cacloaidatkhac", SqlDbType.NVarChar, 200);
                    Cacloaidatkhac.Value = textBoxSD_10.Text + "-" + textBoxNN_10.Text;
                    cmd2.Parameters.Add(Cacloaidatkhac);

                    SqlParameter DT_KhongON = new SqlParameter("@DT_KhongON", SqlDbType.Float);
                    DT_KhongON.Value = tbKhongON.Text;
                    cmd2.Parameters.Add(DT_KhongON);

                    SqlParameter Datthocu2 = new SqlParameter("@Datthocu2", SqlDbType.Float);
                    Datthocu2.Value = tbKhongON_1.Text;
                    cmd2.Parameters.Add(Datthocu2);

                    SqlParameter Dattrongtrot2 = new SqlParameter("@Dattrongtrot2", SqlDbType.Float);
                    Dattrongtrot2.Value = tbKhongON_2.Text;
                    cmd2.Parameters.Add(Dattrongtrot2);

                    SqlParameter Dattronglaunam2 = new SqlParameter("@Dattronglaunam2", SqlDbType.Float);
                    Dattronglaunam2.Value = tbKhongON_3.Text;
                    cmd2.Parameters.Add(Dattronglaunam2);

                    SqlParameter Matnuoc2 = new SqlParameter("@Matnuoc2", SqlDbType.Float);
                    Matnuoc2.Value = tbKhongON_4.Text;
                    cmd2.Parameters.Add(Matnuoc2);

                    SqlParameter Datrungtunhien2 = new SqlParameter("@Datrungtunhien2", SqlDbType.Float);
                    Datrungtunhien2.Value = tbKhongON_5.Text;
                    cmd2.Parameters.Add(Datrungtunhien2);

                    SqlParameter Datxd2 = new SqlParameter("@Datxd2", SqlDbType.Float);
                    Datxd2.Value = tbKhongON_6.Text;
                    cmd2.Parameters.Add(Datxd2);

                    SqlParameter Datgiaothong2 = new SqlParameter("@Datgiaothong2", SqlDbType.Float);
                    Datgiaothong2.Value = tbKhongON_7.Text;
                    cmd2.Parameters.Add(Datgiaothong2);

                    SqlParameter Datthuyloi2 = new SqlParameter("@Datthuyloi2", SqlDbType.Float);
                    Datthuyloi2.Value = tbKhongON_8.Text;
                    cmd2.Parameters.Add(Datthuyloi2);

                    SqlParameter Datnghiadia2 = new SqlParameter("@Datnghiadia2", SqlDbType.Float);
                    Datnghiadia2.Value = tbKhongON_9.Text;
                    cmd2.Parameters.Add(Datnghiadia2);

                    SqlParameter Cacloaidatkhac2 = new SqlParameter("@Cacloaidatkhac2", SqlDbType.Float);
                    Cacloaidatkhac2.Value = tbKhongON_10.Text;
                    cmd2.Parameters.Add(Cacloaidatkhac2);

                    SqlParameter DTKS = new SqlParameter("@DTKS", SqlDbType.NVarChar, 200);
                    DTKS.Value = textBox_DTKS.Text;
                    cmd2.Parameters.Add(DTKS);

                    SqlParameter Tongsotinhieu = new SqlParameter("@Tongsotinhieu", SqlDbType.NVarChar, 200);
                    Tongsotinhieu.Value = textBoxTH_Tong.Text;
                    cmd2.Parameters.Add(Tongsotinhieu);

                    SqlParameter Tinhieu = new SqlParameter("@Tinhieu", SqlDbType.NVarChar, 200);
                    Tinhieu.Value = textBoxTH_1.Text + "-" + textBoxTH_2.Text;
                    cmd2.Parameters.Add(Tinhieu);

                    SqlParameter Tongmatdo = new SqlParameter("@Tongmatdo", SqlDbType.NVarChar, 200);
                    Tongmatdo.Value = textBoxMD_Tong.Text;
                    cmd2.Parameters.Add(Tongmatdo);

                    SqlParameter Matdo = new SqlParameter("@Matdo", SqlDbType.NVarChar, 200);
                    Matdo.Value = textBoxMD_1.Text + "-" + textBoxMD_2.Text;
                    cmd2.Parameters.Add(Matdo);

                    SqlParameter Bompha = new SqlParameter("@Bompha", SqlDbType.NVarChar, 200);
                    Bompha.Value = textBoxVN1_1.Text + "-" + textBoxVN1_2.Text + "-" + textBoxVN1_3.Text + "-" + textBoxVN1_4.Text + "-" + textBoxVN1_5.Text + "-" + textBoxVN1_6.Text;
                    cmd2.Parameters.Add(Bompha);

                    SqlParameter Danphao = new SqlParameter("@Danphao", SqlDbType.NVarChar, 200);
                    Danphao.Value = textBoxVN2_1.Text + "-" + textBoxVN2_2.Text + "-" + textBoxVN2_3.Text + "-" + textBoxVN2_4.Text + "-" + textBoxVN2_5.Text + "-" + textBoxVN2_6.Text;
                    cmd2.Parameters.Add(Danphao);

                    SqlParameter Tenlua = new SqlParameter("@Tenlua", SqlDbType.NVarChar, 200);
                    Tenlua.Value = textBoxVN3_1.Text + "-" + textBoxVN3_2.Text + "-" + textBoxVN3_3.Text + "-" + textBoxVN3_4.Text + "-" + textBoxVN3_5.Text + "-" + textBoxVN3_6.Text;
                    cmd2.Parameters.Add(Tenlua);

                    SqlParameter Luudan = new SqlParameter("@Luudan", SqlDbType.NVarChar, 200);
                    Luudan.Value = textBoxVN4_1.Text + "-" + textBoxVN4_2.Text + "-" + textBoxVN4_3.Text + "-" + textBoxVN4_4.Text + "-" + textBoxVN4_5.Text + "-" + textBoxVN4_6.Text;
                    cmd2.Parameters.Add(Luudan);

                    SqlParameter Bombi = new SqlParameter("@Bombi", SqlDbType.NVarChar, 200);
                    Bombi.Value = textBoxVN5_1.Text + "-" + textBoxVN5_2.Text + "-" + textBoxVN5_3.Text + "-" + textBoxVN5_4.Text + "-" + textBoxVN5_5.Text + "-" + textBoxVN5_6.Text;
                    cmd2.Parameters.Add(Bombi);

                    SqlParameter Mintrongtang = new SqlParameter("@Mintrongtang", SqlDbType.NVarChar, 200);
                    Mintrongtang.Value = textBoxVN6_1.Text + "-" + textBoxVN6_2.Text + "-" + textBoxVN6_3.Text + "-" + textBoxVN6_4.Text + "-" + textBoxVN6_5.Text + "-" + textBoxVN6_6.Text;
                    cmd2.Parameters.Add(Mintrongtang);

                    SqlParameter Mintrongnguoi = new SqlParameter("@Mintrongnguoi", SqlDbType.NVarChar, 200);
                    Mintrongnguoi.Value = textBoxVN7_1.Text + "-" + textBoxVN7_2.Text + "-" + textBoxVN7_3.Text + "-" + textBoxVN7_4.Text + "-" + textBoxVN7_5.Text + "-" + textBoxVN7_6.Text;
                    cmd2.Parameters.Add(Mintrongnguoi);

                    SqlParameter Cacloaivatno = new SqlParameter("@Cacloaivatno", SqlDbType.NVarChar, 200);
                    Cacloaivatno.Value = textBoxVN8_1.Text + "-" + textBoxVN8_2.Text + "-" + textBoxVN8_3.Text + "-" + textBoxVN8_4.Text + "-" + textBoxVN8_5.Text + "-" + textBoxVN8_6.Text;
                    cmd2.Parameters.Add(Cacloaivatno);

                    SqlParameter Satthep = new SqlParameter("@Satthep", SqlDbType.NVarChar, 200);
                    Satthep.Value = textBoxVN9_1.Text + "-" + textBoxVN9_2.Text + "-" + textBoxVN9_3.Text + "-" + textBoxVN9_4.Text + "-" + textBoxVN9_5.Text + "-" + textBoxVN9_6.Text;
                    cmd2.Parameters.Add(Satthep);

                    SqlParameter SLtinhieu5m = new SqlParameter("@SLtinhieu5m", SqlDbType.NVarChar, 200);
                    SLtinhieu5m.Value = textBox_SLTH5m.Text;
                    cmd2.Parameters.Add(SLtinhieu5m);

                    SqlParameter Mucdoonhiem = new SqlParameter("@Mucdoonhiem", SqlDbType.NVarChar, 200);
                    Mucdoonhiem.Value = comboBox_Mucdo.Text;
                    cmd2.Parameters.Add(Mucdoonhiem);

                    SqlParameter Phanloai = new SqlParameter("@Phanloai", SqlDbType.NVarChar, 200);
                    Phanloai.Value = textBox_Phanloai.Text;
                    cmd2.Parameters.Add(Phanloai);

                    SqlParameter Capdat = new SqlParameter("@Capdat", SqlDbType.NVarChar, 200);
                    Capdat.Value = textBox_Capdat.Text;
                    cmd2.Parameters.Add(Capdat);

                    SqlParameter DTrapha = new SqlParameter("@DTrapha", SqlDbType.NVarChar, 200);
                    DTrapha.Value = textBoxDT_Tong.Text + "-" + textBoxDT_1.Text + "-" + textBoxDT_2.Text + "-" + textBoxDT_3.Text + "-" + textBoxDT_4.Text + "-" + textBoxDT_5.Text + "-" + textBoxDT_6.Text + "-" + textBoxDT_7.Text + "-" + textBoxDT_8.Text + "-" + textBoxDT_9.Text + "-" + textBoxDT_10.Text;
                    cmd2.Parameters.Add(DTrapha);

                    SqlParameter Chihuy_id = new SqlParameter("@Chihuy_id", SqlDbType.NVarChar, 200);
                    Chihuy_id.Value = cbChihuy.SelectedValue;
                    cmd2.Parameters.Add(Chihuy_id);

                    SqlParameter Chihuy = new SqlParameter("@Chihuy", SqlDbType.NVarChar, 200);
                    try
                    {
                        Chihuy.Value = cbChihuy.Text.Split('-')[1];
                        cmd2.Parameters.Add(Chihuy);
                    }
                    catch
                    {
                        Chihuy.Value = cbChihuy.Text;
                        cmd2.Parameters.Add(Chihuy);
                    }

                    SqlParameter Chihuy_other = new SqlParameter("@Chihuy_other", SqlDbType.NVarChar, 200);
                    Chihuy_other.Value = textBox_Chihuy_other.Text;
                    cmd2.Parameters.Add(Chihuy_other);

                    SqlParameter Doitruong_id = new SqlParameter("@Doitruong_id", SqlDbType.NVarChar, 200);
                    Doitruong_id.Value = cbDoitruong.SelectedValue;
                    cmd2.Parameters.Add(Doitruong_id);

                    SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 200);
                    try
                    {
                        Doitruong.Value = cbDoitruong.Text.Split('-')[1];
                        cmd2.Parameters.Add(Doitruong);
                    }
                    catch
                    {
                        Doitruong.Value = cbDoitruong.Text;
                        cmd2.Parameters.Add(Doitruong);
                    }

                    SqlParameter Doitruong_other = new SqlParameter("@Doitruong_other", SqlDbType.NVarChar, 200);
                    Doitruong_other.Value = textBox_Doitruong_other.Text;
                    cmd2.Parameters.Add(Doitruong_other);

                    SqlParameter Giamsat_id = new SqlParameter("@Giamsat_id", SqlDbType.BigInt);
                    Giamsat_id.Value = cbGiamSat.SelectedValue;
                    cmd2.Parameters.Add(Giamsat_id);

                    SqlParameter Giamsat_other = new SqlParameter("@Giamsat_other", SqlDbType.NVarChar, 200);
                    Giamsat_other.Value = tbGiamSatOther.Text;
                    cmd2.Parameters.Add(Giamsat_other);

                    SqlParameter PhieudieutraLink = new SqlParameter("@PhieudieutraLink", SqlDbType.NVarChar, 2200);
                    PhieudieutraLink.Value = lbPhieudieutra.Text;
                    cmd2.Parameters.Add(PhieudieutraLink);

                    SqlParameter BaocaoLink = new SqlParameter("@BaocaoLink", SqlDbType.NVarChar, 2200);
                    BaocaoLink.Value = lbBCKS.Text;
                    cmd2.Parameters.Add(BaocaoLink);

                    SqlParameter BandoLink = new SqlParameter("@BandoLink", SqlDbType.NVarChar, 2200);
                    BandoLink.Value = lbBando.Text;
                    cmd2.Parameters.Add(BandoLink);

                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();

                        if (temp > 0)
                        {
                            MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                            this.Close();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                            this.Close();
                            return false;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        this.Close();
                        return false;
                    }

                }
                else
                {

                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO [dbo].[cecm_BaocaoTonghop]" +
                        "([Duan],DVKS_id,[Hopphan],Mabaocao,[Tinh],[Huyen],[Xa],[Thon],[Diahinh],[Loaidat],[Thoitiet],[Hientrangonhiem],[Lichsutainan],[Lichsuhoatdong],[DTsd],[DTnghingo],DT_KhongON,[Datthocu],[Dattrongtrot],[Dattronglaunam],[Matnuoc],[Datrungtunhien],[Datxd],[Datgiaothong],[Datthuyloi],[Datnghiadia],[Cacloaidatkhac],[Datthocu2],[Dattrongtrot2],[Dattronglaunam2],[Matnuoc2],[Datrungtunhien2],[Datxd2],[Datgiaothong2],[Datthuyloi2],[Datnghiadia2],[Cacloaidatkhac2],[DTKS],[Tongsotinhieu],[Tinhieu],[Tongmatdo],[Matdo],[Bompha],[Danphao],[Tenlua],[Luudan],[Bombi],[Mintrongtang],[Mintrongnguoi],[Cacloaivatno],[Satthep],[SLtinhieu5m],[Mucdoonhiem],[Phanloai],[Capdat],[DTrapha],[Chihuy_id],[Chihuy],[Chihuy_other],[Doitruong_id],[Doitruong],[Doitruong_other],Giamsat_id,Giamsat_other,[PhieudieutraLink],[BaocaoLink],[BandoLink])" +
                        "VALUES" +
                        "(@Duan,@DVKS_id,@Hopphan,@Mabaocao,@Tinh,@Huyen,@Xa,@Thon,@Diahinh,@Loaidat,@Thoitiet,@Hientrangonhiem,@Lichsutainan,@Lichsuhoatdong,@DTsd,@DTnghingo,@DT_KhongON,@Datthocu,@Dattrongtrot,@Dattronglaunam,@Matnuoc,@Datrungtunhien,@Datxd,@Datgiaothong,@Datthuyloi,@Datnghiadia,@Cacloaidatkhac,@Datthocu2,@Dattrongtrot2,@Dattronglaunam2,@Matnuoc2,@Datrungtunhien2,@Datxd2,@Datgiaothong2,@Datthuyloi2,@Datnghiadia2,@Cacloaidatkhac2,@DTKS,@Tongsotinhieu,@Tinhieu,@Tongmatdo,@Matdo,@Bompha,@Danphao,@Tenlua,@Luudan,@Bombi,@Mintrongtang,@Mintrongnguoi,@Cacloaivatno,@Satthep,@SLtinhieu5m,@Mucdoonhiem,@Phanloai,@Capdat,@DTrapha,@Chihuy_id,@Chihuy,@Chihuy_other,@Doitruong_id,@Doitruong,@Doitruong_other,@Giamsat_id,@Giamsat_other,@PhieudieutraLink,@BaocaoLink,@BandoLink)", _Cn);

                    SqlParameter Duan = new SqlParameter("@Duan", SqlDbType.BigInt);
                    Duan.Value = comboBox_TenDA.SelectedValue;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter DVKS_id = new SqlParameter("@DVKS_id", SqlDbType.BigInt);
                    DVKS_id.Value = cbDVKS.SelectedValue;
                    cmd2.Parameters.Add(DVKS_id);

                    SqlParameter Hopphan = new SqlParameter("@Hopphan", SqlDbType.NVarChar, 200);
                    Hopphan.Value = txtHopphan.Text;
                    cmd2.Parameters.Add(Hopphan);

                    SqlParameter Mabaocao = new SqlParameter("@Mabaocao", SqlDbType.NVarChar, 50);
                    Mabaocao.Value = tbMabaocao.Text;
                    cmd2.Parameters.Add(Mabaocao);

                    SqlParameter Tinh = new SqlParameter("@Tinh", SqlDbType.Int);
                    Tinh.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(Tinh);

                    SqlParameter Huyen = new SqlParameter("@Huyen", SqlDbType.Int);
                    Huyen.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(Huyen);

                    SqlParameter Xa = new SqlParameter("@Xa", SqlDbType.Int);
                    Xa.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(Xa);

                    SqlParameter Thon = new SqlParameter("@Thon", SqlDbType.NVarChar, 200);
                    Thon.Value = txtThon.Text;
                    cmd2.Parameters.Add(Thon);

                    SqlParameter Diahinh = new SqlParameter("@Diahinh", SqlDbType.NVarChar, 200);
                    Diahinh.Value = textBox_Diahinh.Text;
                    cmd2.Parameters.Add(Diahinh);

                    SqlParameter Loaidat = new SqlParameter("@Loaidat", SqlDbType.NVarChar, 200);
                    Loaidat.Value = textBox_Loaidat.Text;
                    cmd2.Parameters.Add(Loaidat);

                    SqlParameter Thoitiet = new SqlParameter("@Thoitiet", SqlDbType.NVarChar, 200);
                    Thoitiet.Value = textBox_Thoitiet.Text;
                    cmd2.Parameters.Add(Thoitiet);

                    SqlParameter Hientrangonhiem = new SqlParameter("@Hientrangonhiem", SqlDbType.NVarChar, 200);
                    Hientrangonhiem.Value = textBox_HTON.Text;
                    cmd2.Parameters.Add(Hientrangonhiem);

                    SqlParameter Lichsutainan = new SqlParameter("@Lichsutainan", SqlDbType.NVarChar, 2200);
                    Lichsutainan.Value = richTextBox_LSTN.Text;
                    cmd2.Parameters.Add(Lichsutainan);

                    SqlParameter Lichsuhoatdong = new SqlParameter("@Lichsuhoatdong", SqlDbType.NVarChar, 2200);
                    Lichsuhoatdong.Value = richTextBox_LSHD.Text;
                    cmd2.Parameters.Add(Lichsuhoatdong);

                    SqlParameter DTsd = new SqlParameter("@DTsd", SqlDbType.NVarChar, 200);
                    DTsd.Value = textBoxSD_Tong.Text;
                    cmd2.Parameters.Add(DTsd);

                    SqlParameter DTnghingo = new SqlParameter("@DTnghingo", SqlDbType.NVarChar, 200);
                    DTnghingo.Value = textBoxNN_Tong.Text;
                    cmd2.Parameters.Add(DTnghingo);

                    SqlParameter Datthocu = new SqlParameter("@Datthocu", SqlDbType.NVarChar, 200);
                    Datthocu.Value = textBoxSD_1.Text + "-" + textBoxNN_1.Text;
                    cmd2.Parameters.Add(Datthocu);

                    SqlParameter Dattrongtrot = new SqlParameter("@Dattrongtrot", SqlDbType.NVarChar, 200);
                    Dattrongtrot.Value = textBoxSD_2.Text + "-" + textBoxNN_2.Text;
                    cmd2.Parameters.Add(Dattrongtrot);

                    SqlParameter Dattronglaunam = new SqlParameter("@Dattronglaunam", SqlDbType.NVarChar, 200);
                    Dattronglaunam.Value = textBoxSD_3.Text + "-" + textBoxNN_3.Text;
                    cmd2.Parameters.Add(Dattronglaunam);

                    SqlParameter Matnuoc = new SqlParameter("@Matnuoc", SqlDbType.NVarChar, 200);
                    Matnuoc.Value = textBoxSD_4.Text + "-" + textBoxNN_4.Text;
                    cmd2.Parameters.Add(Matnuoc);

                    SqlParameter Datrungtunhien = new SqlParameter("@Datrungtunhien", SqlDbType.NVarChar, 200);
                    Datrungtunhien.Value = textBoxSD_5.Text + "-" + textBoxNN_5.Text;
                    cmd2.Parameters.Add(Datrungtunhien);

                    SqlParameter Datxd = new SqlParameter("@Datxd", SqlDbType.NVarChar, 200);
                    Datxd.Value = textBoxSD_6.Text + "-" + textBoxNN_6.Text;
                    cmd2.Parameters.Add(Datxd);

                    SqlParameter Datgiaothong = new SqlParameter("@Datgiaothong", SqlDbType.NVarChar, 200);
                    Datgiaothong.Value = textBoxSD_7.Text + "-" + textBoxNN_7.Text;
                    cmd2.Parameters.Add(Datgiaothong);

                    SqlParameter Datthuyloi = new SqlParameter("@Datthuyloi", SqlDbType.NVarChar, 200);
                    Datthuyloi.Value = textBoxSD_8.Text + "-" + textBoxNN_8.Text;
                    cmd2.Parameters.Add(Datthuyloi);

                    SqlParameter Datnghiadia = new SqlParameter("@Datnghiadia", SqlDbType.NVarChar, 200);
                    Datnghiadia.Value = textBoxSD_9.Text + "-" + textBoxNN_9.Text;
                    cmd2.Parameters.Add(Datnghiadia);

                    SqlParameter Cacloaidatkhac = new SqlParameter("@Cacloaidatkhac", SqlDbType.NVarChar, 200);
                    Cacloaidatkhac.Value = textBoxSD_10.Text + "-" + textBoxNN_10.Text;
                    cmd2.Parameters.Add(Cacloaidatkhac);

                    SqlParameter DT_KhongON = new SqlParameter("@DT_KhongON", SqlDbType.Float);
                    DT_KhongON.Value = tbKhongON.Text;
                    cmd2.Parameters.Add(DT_KhongON);

                    SqlParameter Datthocu2 = new SqlParameter("@Datthocu2", SqlDbType.Float);
                    Datthocu2.Value = tbKhongON_1.Text;
                    cmd2.Parameters.Add(Datthocu2);

                    SqlParameter Dattrongtrot2 = new SqlParameter("@Dattrongtrot2", SqlDbType.Float);
                    Dattrongtrot2.Value = tbKhongON_2.Text;
                    cmd2.Parameters.Add(Dattrongtrot2);

                    SqlParameter Dattronglaunam2 = new SqlParameter("@Dattronglaunam2", SqlDbType.Float);
                    Dattronglaunam2.Value = tbKhongON_3.Text;
                    cmd2.Parameters.Add(Dattronglaunam2);

                    SqlParameter Matnuoc2 = new SqlParameter("@Matnuoc2", SqlDbType.Float);
                    Matnuoc2.Value = tbKhongON_4.Text;
                    cmd2.Parameters.Add(Matnuoc2);

                    SqlParameter Datrungtunhien2 = new SqlParameter("@Datrungtunhien2", SqlDbType.Float);
                    Datrungtunhien2.Value = tbKhongON_5.Text;
                    cmd2.Parameters.Add(Datrungtunhien2);

                    SqlParameter Datxd2 = new SqlParameter("@Datxd2", SqlDbType.Float);
                    Datxd2.Value = tbKhongON_6.Text;
                    cmd2.Parameters.Add(Datxd2);

                    SqlParameter Datgiaothong2 = new SqlParameter("@Datgiaothong2", SqlDbType.Float);
                    Datgiaothong2.Value = tbKhongON_7.Text;
                    cmd2.Parameters.Add(Datgiaothong2);

                    SqlParameter Datthuyloi2 = new SqlParameter("@Datthuyloi2", SqlDbType.Float);
                    Datthuyloi2.Value = tbKhongON_8.Text;
                    cmd2.Parameters.Add(Datthuyloi2);

                    SqlParameter Datnghiadia2 = new SqlParameter("@Datnghiadia2", SqlDbType.Float);
                    Datnghiadia2.Value = tbKhongON_9.Text;
                    cmd2.Parameters.Add(Datnghiadia2);

                    SqlParameter Cacloaidatkhac2 = new SqlParameter("@Cacloaidatkhac2", SqlDbType.Float);
                    Cacloaidatkhac2.Value = tbKhongON_10.Text;
                    cmd2.Parameters.Add(Cacloaidatkhac2);

                    SqlParameter DTKS = new SqlParameter("@DTKS", SqlDbType.NVarChar, 200);
                    DTKS.Value = textBox_DTKS.Text;
                    cmd2.Parameters.Add(DTKS);

                    SqlParameter Tongsotinhieu = new SqlParameter("@Tongsotinhieu", SqlDbType.NVarChar, 200);
                    Tongsotinhieu.Value = textBoxTH_Tong.Text;
                    cmd2.Parameters.Add(Tongsotinhieu);

                    SqlParameter Tinhieu = new SqlParameter("@Tinhieu", SqlDbType.NVarChar, 200);
                    Tinhieu.Value = textBoxTH_1.Text + "-" + textBoxTH_2.Text;
                    cmd2.Parameters.Add(Tinhieu);

                    SqlParameter Tongmatdo = new SqlParameter("@Tongmatdo", SqlDbType.NVarChar, 200);
                    Tongmatdo.Value = textBoxMD_Tong.Text;
                    cmd2.Parameters.Add(Tongmatdo);

                    SqlParameter Matdo = new SqlParameter("@Matdo", SqlDbType.NVarChar, 200);
                    Matdo.Value = textBoxMD_1.Text + "-" + textBoxMD_2.Text;
                    cmd2.Parameters.Add(Matdo);

                    SqlParameter Bompha = new SqlParameter("@Bompha", SqlDbType.NVarChar, 200);
                    Bompha.Value = textBoxVN1_1.Text + "-" + textBoxVN1_2.Text + "-" + textBoxVN1_3.Text + "-" + textBoxVN1_4.Text + "-" + textBoxVN1_5.Text + "-" + textBoxVN1_6.Text;
                    cmd2.Parameters.Add(Bompha);

                    SqlParameter Danphao = new SqlParameter("@Danphao", SqlDbType.NVarChar, 200);
                    Danphao.Value = textBoxVN2_1.Text + "-" + textBoxVN2_2.Text + "-" + textBoxVN2_3.Text + "-" + textBoxVN2_4.Text + "-" + textBoxVN2_5.Text + "-" + textBoxVN2_6.Text;
                    cmd2.Parameters.Add(Danphao);

                    SqlParameter Tenlua = new SqlParameter("@Tenlua", SqlDbType.NVarChar, 200);
                    Tenlua.Value = textBoxVN3_1.Text + "-" + textBoxVN3_2.Text + "-" + textBoxVN3_3.Text + "-" + textBoxVN3_4.Text + "-" + textBoxVN3_5.Text + "-" + textBoxVN3_6.Text;
                    cmd2.Parameters.Add(Tenlua);

                    SqlParameter Luudan = new SqlParameter("@Luudan", SqlDbType.NVarChar, 200);
                    Luudan.Value = textBoxVN4_1.Text + "-" + textBoxVN4_2.Text + "-" + textBoxVN4_3.Text + "-" + textBoxVN4_4.Text + "-" + textBoxVN4_5.Text + "-" + textBoxVN4_6.Text;
                    cmd2.Parameters.Add(Luudan);

                    SqlParameter Bombi = new SqlParameter("@Bombi", SqlDbType.NVarChar, 200);
                    Bombi.Value = textBoxVN5_1.Text + "-" + textBoxVN5_2.Text + "-" + textBoxVN5_3.Text + "-" + textBoxVN5_4.Text + "-" + textBoxVN5_5.Text + "-" + textBoxVN5_6.Text;
                    cmd2.Parameters.Add(Bombi);

                    SqlParameter Mintrongtang = new SqlParameter("@Mintrongtang", SqlDbType.NVarChar, 200);
                    Mintrongtang.Value = textBoxVN6_1.Text + "-" + textBoxVN6_2.Text + "-" + textBoxVN6_3.Text + "-" + textBoxVN6_4.Text + "-" + textBoxVN6_5.Text + "-" + textBoxVN6_6.Text;
                    cmd2.Parameters.Add(Mintrongtang);

                    SqlParameter Mintrongnguoi = new SqlParameter("@Mintrongnguoi", SqlDbType.NVarChar, 200);
                    Mintrongnguoi.Value = textBoxVN7_1.Text + "-" + textBoxVN7_2.Text + "-" + textBoxVN7_3.Text + "-" + textBoxVN7_4.Text + "-" + textBoxVN7_5.Text + "-" + textBoxVN7_6.Text;
                    cmd2.Parameters.Add(Mintrongnguoi);

                    SqlParameter Cacloaivatno = new SqlParameter("@Cacloaivatno", SqlDbType.NVarChar, 200);
                    Cacloaivatno.Value = textBoxVN8_1.Text + "-" + textBoxVN8_2.Text + "-" + textBoxVN8_3.Text + "-" + textBoxVN8_4.Text + "-" + textBoxVN8_5.Text + "-" + textBoxVN8_6.Text;
                    cmd2.Parameters.Add(Cacloaivatno);

                    SqlParameter Satthep = new SqlParameter("@Satthep", SqlDbType.NVarChar, 200);
                    Satthep.Value = textBoxVN9_1.Text + "-" + textBoxVN9_2.Text + "-" + textBoxVN9_3.Text + "-" + textBoxVN9_4.Text + "-" + textBoxVN9_5.Text + "-" + textBoxVN9_6.Text;
                    cmd2.Parameters.Add(Satthep);

                    SqlParameter SLtinhieu5m = new SqlParameter("@SLtinhieu5m", SqlDbType.NVarChar, 200);
                    SLtinhieu5m.Value = textBox_SLTH5m.Text;
                    cmd2.Parameters.Add(SLtinhieu5m);

                    SqlParameter Mucdoonhiem = new SqlParameter("@Mucdoonhiem", SqlDbType.NVarChar, 200);
                    Mucdoonhiem.Value = comboBox_Mucdo.Text;
                    cmd2.Parameters.Add(Mucdoonhiem);

                    SqlParameter Phanloai = new SqlParameter("@Phanloai", SqlDbType.NVarChar, 200);
                    Phanloai.Value = textBox_Phanloai.Text;
                    cmd2.Parameters.Add(Phanloai);

                    SqlParameter Capdat = new SqlParameter("@Capdat", SqlDbType.NVarChar, 200);
                    Capdat.Value = textBox_Capdat.Text;
                    cmd2.Parameters.Add(Capdat);

                    SqlParameter DTrapha = new SqlParameter("@DTrapha", SqlDbType.NVarChar, 200);
                    DTrapha.Value = textBoxDT_Tong.Text + "-" + textBoxDT_1.Text + "-" + textBoxDT_2.Text + "-" + textBoxDT_3.Text + "-" + textBoxDT_4.Text + "-" + textBoxDT_5.Text + "-" + textBoxDT_6.Text + "-" + textBoxDT_7.Text + "-" + textBoxDT_8.Text + "-" + textBoxDT_9.Text + "-" + textBoxDT_10.Text;
                    cmd2.Parameters.Add(DTrapha);

                    SqlParameter Chihuy_id = new SqlParameter("@Chihuy_id", SqlDbType.NVarChar, 200);
                    Chihuy_id.Value = cbChihuy.SelectedValue;
                    cmd2.Parameters.Add(Chihuy_id);

                    SqlParameter Chihuy = new SqlParameter("@Chihuy", SqlDbType.NVarChar, 200);
                    try
                    {
                        Chihuy.Value = cbChihuy.Text.Split('-')[1];
                        cmd2.Parameters.Add(Chihuy);
                    }
                    catch
                    {
                        Chihuy.Value = cbChihuy.Text;
                        cmd2.Parameters.Add(Chihuy);
                    }

                    SqlParameter Chihuy_other = new SqlParameter("@Chihuy_other", SqlDbType.NVarChar, 200);
                    Chihuy_other.Value = textBox_Chihuy_other.Text;
                    cmd2.Parameters.Add(Chihuy_other);

                    SqlParameter Doitruong_id = new SqlParameter("@Doitruong_id", SqlDbType.NVarChar, 200);
                    Doitruong_id.Value = cbDoitruong.SelectedValue;
                    cmd2.Parameters.Add(Doitruong_id);

                    SqlParameter Doitruong = new SqlParameter("@Doitruong", SqlDbType.NVarChar, 200);
                    try
                    {
                        Doitruong.Value = cbDoitruong.Text.Split('-')[1];
                        cmd2.Parameters.Add(Doitruong);
                    }
                    catch
                    {
                        Doitruong.Value = cbDoitruong.Text;
                        cmd2.Parameters.Add(Doitruong);
                    }

                    SqlParameter Doitruong_other = new SqlParameter("@Doitruong_other", SqlDbType.NVarChar, 200);
                    Doitruong_other.Value = textBox_Doitruong_other.Text;
                    cmd2.Parameters.Add(Doitruong_other);

                    SqlParameter Giamsat_id = new SqlParameter("@Giamsat_id", SqlDbType.BigInt);
                    Giamsat_id.Value = cbGiamSat.SelectedValue;
                    cmd2.Parameters.Add(Giamsat_id);

                    SqlParameter Giamsat_other = new SqlParameter("@Giamsat_other", SqlDbType.NVarChar, 200);
                    Giamsat_other.Value = tbGiamSatOther.Text;
                    cmd2.Parameters.Add(Giamsat_other);

                    SqlParameter PhieudieutraLink = new SqlParameter("@PhieudieutraLink", SqlDbType.NVarChar, 2200);
                    PhieudieutraLink.Value = lbPhieudieutra.Text;
                    cmd2.Parameters.Add(PhieudieutraLink);

                    SqlParameter BaocaoLink = new SqlParameter("@BaocaoLink", SqlDbType.NVarChar, 2200);
                    BaocaoLink.Value = lbBCKS.Text;
                    cmd2.Parameters.Add(BaocaoLink);

                    SqlParameter BandoLink = new SqlParameter("@BandoLink", SqlDbType.NVarChar, 2200);
                    BandoLink.Value = lbBando.Text;
                    cmd2.Parameters.Add(BandoLink);

                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();

                        if (temp > 0)
                        {
                            MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                            this.Close();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                            this.Close();
                            return false;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                        this.Close();
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

        private void FrmThemmoiBCTH_Load(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT Ten FROM cecm_provinces where level = 1", _Cn); sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            //System.Data.DataTable datatableProvince = new System.Data.DataTable();
            //sqlAdapterProvince.Fill(datatableProvince);
            //comboBox_Tinh.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProvince.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
            //        continue;

            //    comboBox_Tinh.Items.Add(dr["Ten"].ToString());
            //}
            UtilsDatabase.LoadCBTinh(comboBox_Tinh);

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

            LoadData(id_BSKQ);

        }
        private void comboBox_Tinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Tinh.SelectedValue is long)
            {
                UtilsDatabase.LoadCBHuyen(comboBox_Huyen, (long)comboBox_Tinh.SelectedValue);
            }
        }

        private void comboBox_Huyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Huyen.SelectedValue is long)
            {
                UtilsDatabase.LoadCBXa(comboBox_Xa, (long)comboBox_Huyen.SelectedValue);
            }
        }

        private void comboBox_TenDA_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        private void textBoxSD_TextChanged(object sender, EventArgs e)
        {
            if(((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "0";
            }
            textBoxSD_Tong.Text = (double.Parse(textBoxSD_1.Text) + double.Parse(textBoxSD_2.Text) + double.Parse(textBoxSD_3.Text) + double.Parse(textBoxSD_4.Text) + double.Parse(textBoxSD_5.Text) + double.Parse(textBoxSD_6.Text) + double.Parse(textBoxSD_7.Text) + double.Parse(textBoxSD_8.Text) + double.Parse(textBoxSD_9.Text) + double.Parse(textBoxSD_10.Text)).ToString();
        }

        private void tbKhongON_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "0";
            }
            tbKhongON.Text = (double.Parse(tbKhongON_1.Text) + double.Parse(tbKhongON_2.Text) + double.Parse(tbKhongON_3.Text) + double.Parse(tbKhongON_4.Text) + double.Parse(tbKhongON_5.Text) + double.Parse(tbKhongON_6.Text) + double.Parse(tbKhongON_7.Text) + double.Parse(tbKhongON_8.Text) + double.Parse(tbKhongON_9.Text) + double.Parse(tbKhongON_10.Text)).ToString();
        }

        private void textBoxNN_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "0";
            }
            textBoxNN_Tong.Text = (double.Parse(textBoxNN_1.Text) + double.Parse(textBoxNN_2.Text) + double.Parse(textBoxNN_3.Text) + double.Parse(textBoxNN_4.Text) + double.Parse(textBoxNN_5.Text) + double.Parse(textBoxNN_6.Text) + double.Parse(textBoxNN_7.Text) + double.Parse(textBoxNN_8.Text) + double.Parse(textBoxNN_9.Text) + double.Parse(textBoxNN_10.Text)).ToString();
        }
        private void textBoxTH_2_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "0";
            }
            textBoxTH_Tong.Text = (double.Parse(textBoxTH_2.Text) + double.Parse(textBoxTH_1.Text)).ToString();
        }

        private void textBoxMD_2_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "0";
            }
            textBoxMD_Tong.Text = (double.Parse(textBoxMD_2.Text) + double.Parse(textBoxMD_1.Text)).ToString();
        }
        private void textBoxBom_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "0";
            }
            textBoxVN1_5.Text = (double.Parse(textBoxVN1_1.Text) + double.Parse(textBoxVN1_2.Text) + double.Parse(textBoxVN1_3.Text) + double.Parse(textBoxVN1_4.Text)).ToString();
            textBoxVN2_5.Text = (double.Parse(textBoxVN2_1.Text) + double.Parse(textBoxVN2_2.Text) + double.Parse(textBoxVN2_3.Text) + double.Parse(textBoxVN2_4.Text)).ToString();
            textBoxVN3_5.Text = (double.Parse(textBoxVN3_1.Text) + double.Parse(textBoxVN3_2.Text) + double.Parse(textBoxVN3_3.Text) + double.Parse(textBoxVN3_4.Text)).ToString();
            textBoxVN4_5.Text = (double.Parse(textBoxVN4_1.Text) + double.Parse(textBoxVN4_2.Text) + double.Parse(textBoxVN4_3.Text) + double.Parse(textBoxVN4_4.Text)).ToString();
            textBoxVN5_5.Text = (double.Parse(textBoxVN5_1.Text) + double.Parse(textBoxVN5_2.Text) + double.Parse(textBoxVN5_3.Text) + double.Parse(textBoxVN5_4.Text)).ToString();
            textBoxVN6_5.Text = (double.Parse(textBoxVN6_1.Text) + double.Parse(textBoxVN6_2.Text) + double.Parse(textBoxVN6_3.Text) + double.Parse(textBoxVN6_4.Text)).ToString();
            textBoxVN7_5.Text = (double.Parse(textBoxVN7_1.Text) + double.Parse(textBoxVN7_2.Text) + double.Parse(textBoxVN7_3.Text) + double.Parse(textBoxVN7_4.Text)).ToString();
            textBoxVN8_5.Text = (double.Parse(textBoxVN8_1.Text) + double.Parse(textBoxVN8_2.Text) + double.Parse(textBoxVN8_3.Text) + double.Parse(textBoxVN8_4.Text)).ToString();
            textBoxVN9_5.Text = (double.Parse(textBoxVN9_1.Text) + double.Parse(textBoxVN9_2.Text) + double.Parse(textBoxVN9_3.Text) + double.Parse(textBoxVN9_4.Text)).ToString();
        }

        private void textBoxDT_9_TextChanged(object sender, EventArgs e)
        {
            textBoxDT_6.Text = (double.Parse(textBoxDT_9.Text) + double.Parse(textBoxDT_7.Text) + double.Parse(textBoxDT_8.Text)).ToString();
        }

        private void textBoxDT_10_TextChanged(object sender, EventArgs e)
        {
            textBoxDT_Tong.Text = (double.Parse(textBoxDT_10.Text) + double.Parse(textBoxDT_1.Text) + double.Parse(textBoxDT_2.Text) + double.Parse(textBoxDT_3.Text) + double.Parse(textBoxDT_4.Text) + double.Parse(textBoxDT_5.Text) + double.Parse(textBoxDT_6.Text)).ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if (checkedListBox_Mucdo.CheckedItems.Count > 0)
            //{
            //    for (int i = 0; i < checkedListBox_Mucdo.CheckedItems.Count; i++)
            //    {
            //        if (idMucdo == "")
            //        {

            //            idMucdo += checkedListBox_Mucdo.CheckedItems[i].ToString().Split('.')[0];
            //        }
            //        else
            //        {
            //            idMucdo += "-" + checkedListBox_Mucdo.CheckedItems[i].ToString().Split('.')[0];
            //        }
            //    }
            //}
            //if (lbPhieudieutra.Text == "..." || lbBando.Text == "..." || lbBCKS.Text == "...")
            //{
            //    MessageBox.Show("Vui lòng nhập lại địa chỉ file");
            //}
            //if(comboBox_Huyen.Text == "" || comboBox_Mucdo.Text ==""|| comboBox_Tinh.Text == ""|| comboBox_Xa.Text == "")
            //{
            //    MessageBox.Show("Vui lòng kiểm tra lại combobox");
            //}
            //else
            //{
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            bool success = UpdateInfomation(id_BSKQ);
                //}
        }

        private void LoadCBDA()
        {
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT id, name FROM cecm_programData", _Cn);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            //comboBox_TenDA.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProgram.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    comboBox_TenDA.Items.Add(dr["name"].ToString());
            //}
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["name"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            comboBox_TenDA.DataSource = datatableProgram;
            comboBox_TenDA.ValueMember = "id";
            comboBox_TenDA.DisplayMember = "name";
        }

        private void btnPhieudieutra_Click(object sender, EventArgs e)
        {
            string locationstring = string.Empty;
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "Word File (*.docx)|*.docx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            locationstring = openFileDialog1.FileName;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể đọc file từ ổ đĩa: " + ex.Message);
                }
            }

            if (string.IsNullOrEmpty(locationstring) == false)
                lbPhieudieutra.Text = locationstring;
        }

        private void btnBCKS_Click(object sender, EventArgs e)
        {
            string locationstring = string.Empty;
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "Word File (*.docx)|*.docx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            locationstring = openFileDialog1.FileName;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể đọc file từ ổ đĩa: " + ex.Message);
                }
            }

            if (string.IsNullOrEmpty(locationstring) == false)
                lbBCKS.Text = locationstring;
        }

        private void btnBando_Click(object sender, EventArgs e)
        {
            string locationstring = string.Empty;
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "DWG File (*.dwg)|*.dwg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            locationstring = openFileDialog1.FileName;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể đọc file từ ổ đĩa: " + ex.Message);
                }
            }

            if (string.IsNullOrEmpty(locationstring) == false)
                lbBando.Text = locationstring;
        }

        private void textBoxDT_1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = "0";
            }
        }

        private void FrmThemmoiBCTH_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtThon_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void textBox_Chihuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ChihuyId = ChooseCBB(cbChihuy);
            //if(ChihuyId > 0)
            //{
            //    textBox_Chihuy_other.ReadOnly = true;
            //}
            //else
            //{
            //    textBox_Chihuy_other.ReadOnly = false;
            //}
        }

        private void textBox_Doitruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DoitruongId = ChooseCBB(cbDoitruong);
            //if (DoitruongId > 0)
            //{
            //    textBox_Doitruong_other.ReadOnly = true;
            //}
            //else
            //{
            //    textBox_Doitruong_other.ReadOnly = false;
            //}
            
        }

        private void lbPhieudieutra_Validating(object sender, CancelEventArgs e)
        {
            if(lbPhieudieutra.Text == "...")
            {
                e.Cancel = true;
                //lbPhieudieutra.Focus();
                errorProvider1.SetError(lbPhieudieutra, "Chưa chọn phiếu điều tra");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(lbPhieudieutra, "");
            }
        }

        private void lbBCKS_Validating(object sender, CancelEventArgs e)
        {
            if (lbBCKS.Text == "...")
            {
                e.Cancel = true;
                //lbBCKS.Focus();
                errorProvider1.SetError(lbBCKS, "Chưa chọn báo cáo KS");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(lbBCKS, "");
            }
        }

        private void lbBando_Validating(object sender, CancelEventArgs e)
        {
            if (lbBando.Text == "...")
            {
                e.Cancel = true;
                //lbBando.Focus();
                errorProvider1.SetError(lbBando, "Chưa chọn bản đồ nhiệm vụ KS/RPBM");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(lbBando, "");
            }
        }

        private void comboBox_TenDA_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (comboBox_TenDA.SelectedValue is long)
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT startTime,endTime,id FROM cecm_programData where id = {0}", comboBox_TenDA.SelectedValue), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    //DuanId = int.Parse(dr["id"].ToString());
                    //GetAllStaffWithIdProgram(textBox_Chihuy);
                    //GetAllStaffWithIdProgram(textBox_Doitruong);
                    //GetAllStaffWithIdProgram(cbGiamSat);
                    LoadCBStaff(cbChihuy);
                    LoadCBStaff(cbDoitruong);
                    LoadCBStaff(cbGiamSat);
                    LoadDVKS();
                }
            }
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

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            if(comboBox_Tinh.Text == "")
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

        private void comboBox_Mucdo_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_Mucdo.Text == "")
            {
                e.Cancel = true;
                //comboBox_Mucdo.Focus();
                tabControl1.SelectedTab = tabDanhGiaChung;
                errorProvider1.SetError(comboBox_Mucdo, "Chưa chọn mức độ");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Mucdo, "");
            }
        }

        private void cbGiamSat_SelectedValueChanged(object sender, EventArgs e)
        {
            if(!(cbGiamSat.SelectedValue is long))
            {
                return;
            }
            if((long)cbGiamSat.SelectedValue > 0)
            {
                tbGiamSatOther.ReadOnly = true;
                tbGiamSatOther.Text = "";
            }
            else
            {
                tbGiamSatOther.ReadOnly = false;
            }
        }

        private void cbDVKS_Validating(object sender, CancelEventArgs e)
        {

        }

        private void textBox_Chihuy_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cbChihuy.SelectedValue is long))
            {
                return;
            }
            if ((long)cbChihuy.SelectedValue > 0)
            {
                textBox_Chihuy_other.ReadOnly = true;
                textBox_Chihuy_other.Text = "";
            }
            else
            {
                textBox_Chihuy_other.ReadOnly = false;
            }
        }

        private void textBox_Doitruong_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cbDoitruong.SelectedValue is long))
            {
                return;
            }
            if ((long)cbDoitruong.SelectedValue > 0)
            {
                textBox_Doitruong_other.ReadOnly = true;
                textBox_Doitruong_other.Text = "";
            }
            else
            {
                textBox_Doitruong_other.ReadOnly = false;
            }
        }

        private void textBox_DTKS_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(textBox_DTKS.Text, out double DTKS);
            int.TryParse(textBoxTH_1.Text, out int TH_1);
            int.TryParse(textBoxTH_2.Text, out int TH_2);
            textBoxTH_Tong.Text = (TH_1 + TH_2).ToString();
            if(DTKS == 0)
            {
                textBoxMD_Tong.Text = "0";
                textBoxMD_1.Text = "0";
                textBoxMD_2.Text = "0";
            }
            else
            {
                textBoxMD_Tong.Text = ((TH_1 + TH_2) / DTKS * 10000).ToString();
                textBoxMD_1.Text = Math.Round(TH_1 / DTKS * 10000, 3).ToString();
                textBoxMD_2.Text = Math.Round(TH_2 / DTKS * 10000, 3).ToString();
            }
        }
    }
}
