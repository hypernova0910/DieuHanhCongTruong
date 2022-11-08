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
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Properties;

namespace DieuHanhCongTruong
{
    public partial class FrmThemmoiRPBM14 : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        public int DuanId = 0;
        //public int deptidRpbm = 0;
        public int Captain_id = 0;
        public int Boss_id = 0;
        public int Captain_sign_id = 0;
        public int Cadres_id = 0;
        public string addressDuan = "";
        public int TinhId = 0;
        public int HuyenId = 0;
        public int XaId = 0;
        public int TinhId1 = 0;
        public int HuyenId1 = 0;
        public int XaId1 = 0;
        public string symbol = "0";
        public int category = 0;
        public int bossId = 0;
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM14(int i)
        {
            id_BSKQ = i;
            _Cn = _Cn = frmLoggin.sqlCon;
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
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox A = (TextBox)sender;

            if (!double.TryParse(A.Text, out double a))
            {
                A.Text = "0";
            }
        }
        private void GetAllStaffWithIdProgram(System.Windows.Forms.ComboBox cb)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterCounty1 = new SqlDataAdapter(string.Format("SELECT * FROM Cecm_ProgramStaff where cecmProgramId = {0}", DuanId), _Cn);
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
                    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM Cecm_ProgramStaff where id = {0} and cecmProgramId = {1}", cb.SelectedItem.ToString().Split('-')[0], DuanId), _Cn);
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
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (comboBox_TenDA.SelectedItem != null && comboBox_TenDA.Text != "Chọn" )
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM cecm_programData where name = N'{0}'", comboBox_TenDA.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    DuanId = int.Parse(dr["id"].ToString());
                    addressDuan = dr["address"].ToString();
                    //GetAllStaffWithIdProgram(txt_boss_idST);
                    UtilsDatabase.LoadCBStaff(txt_boss_idST, DuanId);
                    UtilsDatabase.LoadCBDept(txt_deptid_rpbmST, DuanId);

                }
                SqlDataAdapter sqlAdapterCounty1 = new SqlDataAdapter(string.Format("select CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web as 'gid',dtg.cecm_program_id,dtg.table_name from dept_tham_gia dtg left join cert_department d on CASE WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1 WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1 ELSE 0 END = 1 WHERE dtg.cecm_program_id = {0} and dtg.dept_id_web IS NOT NULL", DuanId), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty1);
                System.Data.DataTable datatableCounty1 = new System.Data.DataTable();
                sqlAdapterCounty1.Fill(datatableCounty1);
                //txt_deptid_rpbmST.Items.Clear();
                //txt_deptid_rpbmST.Items.Add("Chọn");
                //foreach (DataRow dr in datatableCounty1.Rows)
                //{
                //    if (string.IsNullOrEmpty(dr["dept_idST"].ToString()))
                //        continue;
                //    var a = dr["gid"].ToString() + "-" + dr["dept_idST"].ToString();
                //    txt_deptid_rpbmST.Items.Add(a);
                //}
                //UtilsDatabase.LoadCBDept(txt_deptid_rpbmST, DuanId);
            }
        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM14 where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        txt_symbol.Text = dr["symbol"].ToString();
                        DuanId = int.Parse(dr["cecm_program_id"].ToString());
                        comboBox_TenDA.Text = dr["cecm_program_idST"].ToString();
                        //var adress = dr["address"].ToString().Split(',');
                        //comboBox_Tinh.Text = adress[2];
                        //comboBox_Huyen.Text = adress[1];
                        //comboBox_Xa.Text = adress[0];
                        comboBox_Tinh.SelectedValue = long.TryParse(dr["province_id"].ToString(), out long province_id) ? province_id : -1;
                        comboBox_Huyen.SelectedValue = long.TryParse(dr["district_id"].ToString(), out long district_id) ? district_id : -1;
                        comboBox_Xa.SelectedValue = long.TryParse(dr["commune_id"].ToString(), out long commune_id) ? commune_id : -1;
                        addressDuan = dr["address_cecm"].ToString();
                        time_datesST.Value = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_dates_rpbmST.Value = DateTime.ParseExact(dr["dates_rpbmST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        //var address_rpbm = dr["address_rpbm"].ToString().Split(',');
                        //comboBox_Tinh1.Text = address_rpbm[2];
                        //comboBox_Huyen1.Text = address_rpbm[1];
                        //comboBox_Xa1.Text = address_rpbm[0];
                        comboBox_Tinh1.SelectedValue = long.TryParse(dr["province_id_rpbm"].ToString(), out long province_id_rpbm) ? province_id_rpbm : -1;
                        comboBox_Huyen1.SelectedValue = long.TryParse(dr["district_id_rpbm"].ToString(), out long district_id_rpbm) ? district_id_rpbm : -1;
                        comboBox_Xa1.SelectedValue = long.TryParse(dr["commune_id_rpbm"].ToString(), out long commune_id_rpbm) ? commune_id_rpbm : -1;
                        //deptidRpbm = int.Parse(dr["deptid_rpbm"].ToString());
                        txt_deptid_rpbmST.Text = dr["deptid_rpbmST"].ToString();
                        if (long.TryParse(dr["deptid_rpbmST"].ToString(), out long deptid_rpbmST))
                        {
                            txt_deptid_rpbmST.SelectedValue = deptid_rpbmST;
                        }
                        txt_cdt.Text = dr["cdt"].ToString();
                        txt_ground_done.Text = dr["ground_done"].ToString();
                        txt_area_all_rpbm.Text = dr["area_all_rpbm"].ToString();
                        txt_detail.Text = dr["detail"].ToString();
                        txt_deep.Text = dr["deep"].ToString();
                        //bossId = int.Parse(dr["boss_id"].ToString());
                        txt_boss_idST.Text = dr["boss_idST"].ToString();
                        if (long.TryParse(dr["boss_idST"].ToString(), out long boss_idST))
                        {
                            txt_boss_idST.SelectedValue = boss_idST;
                        }
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();
                    }
                }
            }
        }
        private void FrmThemmoiRPBM14_Load(object sender, EventArgs e)
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
            //SqlDataAdapter sqlAdapterProvince1 = new SqlDataAdapter("SELECT Ten FROM cecm_provinces where level = 1", _Cn); sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            //System.Data.DataTable datatableProvince1 = new System.Data.DataTable();
            //sqlAdapterProvince1.Fill(datatableProvince1);
            //comboBox_Tinh1.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProvince1.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
            //        continue;

            //    comboBox_Tinh1.Items.Add(dr["Ten"].ToString());
            //}
            UtilsDatabase.LoadCBTinh(comboBox_Tinh);
            UtilsDatabase.LoadCBTinh(comboBox_Tinh1);

            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", _Cn);
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            comboBox_TenDA.Items.Add("Chọn");
            foreach (DataRow dr in datatableProgram.Rows)
            {
                if (string.IsNullOrEmpty(dr["name"].ToString()))
                    continue;

                comboBox_TenDA.Items.Add(dr["name"].ToString());
            }
            LoadData(id_BSKQ);
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
                        "UPDATE [dbo].[RPBM14] SET " +
                        "[symbol] = @symbol," +
                        "[address] = @address," +
                        "[datesST] = @datesST," +
                        "[deptid_rpbm] = @deptid_rpbm," +
                        "[deptid_rpbmST] = @deptid_rpbmST," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[address_cecm] = @address_cecm," +
                        "[ground_done] = @ground_done," +
                        "[area_all_rpbm] = @area_all_rpbm," +
                        "[address_rpbm] = @address_rpbm," +
                        "[detail] = @detail," +
                        "[deep] = @deep," +
                        "[dates_rpbmST] = @dates_rpbmST," +
                        "[cdt] = @cdt," +
                        "[boss_id] = @boss_id," +
                        "[boss_idST] = @boss_idST," +
                        "[boss_id_other] = @boss_id_other," +
                        "[files]=@files, " +
                        "[province_id]=@province_id," +
                        "[district_id]=@district_id," +
                        "[commune_id]=@commune_id, " +
                        "[province_id_rpbm]=@province_id_rpbm," +
                        "[district_id_rpbm]=@district_id_rpbm," +
                        "[commune_id_rpbm]=@commune_id_rpbm " +
                        "WHERE gid = " + dem, _Cn);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter cecm_program_idST = new SqlParameter("@cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                    cmd2.Parameters.Add(address);

                    SqlParameter address_rpbm = new SqlParameter("@address_rpbm", SqlDbType.NVarChar, 200);
                    address_rpbm.Value = (comboBox_Xa1.SelectedIndex > 0 ? comboBox_Xa1.Text + "," : "") + (comboBox_Huyen1.SelectedIndex > 0 ? comboBox_Huyen1.Text + "," : "") + (comboBox_Tinh1.SelectedIndex > 0 ? comboBox_Tinh1.Text : "");
                    cmd2.Parameters.Add(address_rpbm);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter deptid_rpbm = new SqlParameter("@deptid_rpbm", SqlDbType.NVarChar, 200);
                    deptid_rpbm.Value = txt_deptid_rpbmST.SelectedValue;
                    cmd2.Parameters.Add(deptid_rpbm);

                    SqlParameter deptid_rpbmST = new SqlParameter("@deptid_rpbmST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_rpbmST.Value = txt_deptid_rpbmST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_rpbmST);
                    }
                    catch
                    {
                        deptid_rpbmST.Value = txt_deptid_rpbmST.Text;
                        cmd2.Parameters.Add(deptid_rpbmST);
                    }

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter dates_rpbmST = new SqlParameter("@dates_rpbmST", SqlDbType.NVarChar, 200);
                    dates_rpbmST.Value = time_dates_rpbmST.Text;
                    cmd2.Parameters.Add(dates_rpbmST);

                    SqlParameter cdt = new SqlParameter("@cdt", SqlDbType.NVarChar, 200);
                    cdt.Value = txt_cdt.Text;
                    cmd2.Parameters.Add(cdt);

                    SqlParameter ground_done = new SqlParameter("@ground_done", SqlDbType.NVarChar, 200);
                    ground_done.Value = txt_ground_done.Text;
                    cmd2.Parameters.Add(ground_done);

                    SqlParameter area_all_rpbm = new SqlParameter("@area_all_rpbm", SqlDbType.NVarChar, 200);
                    area_all_rpbm.Value = txt_area_all_rpbm.Text;
                    cmd2.Parameters.Add(area_all_rpbm);

                    SqlParameter detail = new SqlParameter("@detail", SqlDbType.NVarChar, 200);
                    detail.Value = txt_detail.Text;
                    cmd2.Parameters.Add(detail);

                    SqlParameter deep = new SqlParameter("@deep", SqlDbType.NVarChar, 200);
                    deep.Value = txt_deep.Text;
                    cmd2.Parameters.Add(deep);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.NVarChar, 200);
                    boss_id.Value = txt_boss_idST.SelectedValue;
                    cmd2.Parameters.Add(boss_id);

                    SqlParameter boss_idST = new SqlParameter("@boss_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        boss_idST.Value = txt_boss_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(boss_idST);
                    }
                    catch
                    {
                        boss_idST.Value = txt_boss_idST.Text == "Chọn" ? "" : txt_boss_idST.Text;
                        cmd2.Parameters.Add(boss_idST);
                    }

                    SqlParameter boss_id_other = new SqlParameter("@boss_id_other", SqlDbType.NVarChar, 200);
                    boss_id_other.Value = txt_boss_id_other.Text;
                    cmd2.Parameters.Add(boss_id_other);

                    SqlParameter province_id = new SqlParameter("@province_id", SqlDbType.BigInt);
                    province_id.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(province_id);

                    SqlParameter district_id = new SqlParameter("@district_id", SqlDbType.BigInt);
                    district_id.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(district_id);

                    SqlParameter commune_id = new SqlParameter("@commune_id", SqlDbType.BigInt);
                    commune_id.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(commune_id);

                    SqlParameter province_id_rpbm = new SqlParameter("@province_id_rpbm", SqlDbType.BigInt);
                    province_id_rpbm.Value = comboBox_Tinh1.SelectedValue;
                    cmd2.Parameters.Add(province_id_rpbm);

                    SqlParameter district_id_rpbm = new SqlParameter("@district_id_rpbm", SqlDbType.BigInt);
                    district_id_rpbm.Value = comboBox_Huyen1.SelectedValue;
                    cmd2.Parameters.Add(district_id_rpbm);

                    SqlParameter commune_id_rpbm = new SqlParameter("@commune_id_rpbm", SqlDbType.BigInt);
                    commune_id_rpbm.Value = comboBox_Xa1.SelectedValue;
                    cmd2.Parameters.Add(commune_id_rpbm);

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
                        //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                        this.Close();
                        return false;
                    }

                }
                else
                {
                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO [dbo].[RPBM14]([symbol],[address],[datesST],[deptid_rpbm],[deptid_rpbmST],[cecm_program_id],[cecm_program_idST],[address_cecm],[ground_done],[area_all_rpbm],[address_rpbm],[detail],[deep],[dates_rpbmST],[cdt],[boss_id],[boss_idST],[boss_id_other],[files],province_id,district_id,commune_id,province_id_rpbm,district_id_rpbm,commune_id_rpbm)" +
                        "VALUES(@symbol,@address,@datesST,@deptid_rpbm,@deptid_rpbmST,@cecm_program_id,@cecm_program_idST,@address_cecm,@ground_done,@area_all_rpbm,@address_rpbm,@detail,@deep,@dates_rpbmST,@cdt,@boss_id,@boss_idST,@boss_id_other,@files,@province_id,@district_id,@commune_id,@province_id_rpbm,@district_id_rpbm,@commune_id_rpbm)", _Cn);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter cecm_program_idST = new SqlParameter("@cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                    cmd2.Parameters.Add(address);

                    SqlParameter address_rpbm = new SqlParameter("@address_rpbm", SqlDbType.NVarChar, 200);
                    address_rpbm.Value = (comboBox_Xa1.SelectedIndex > 0 ? comboBox_Xa1.Text + "," : "") + (comboBox_Huyen1.SelectedIndex > 0 ? comboBox_Huyen1.Text + "," : "") + (comboBox_Tinh1.SelectedIndex > 0 ? comboBox_Tinh1.Text : "");
                    cmd2.Parameters.Add(address_rpbm);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter deptid_rpbm = new SqlParameter("@deptid_rpbm", SqlDbType.NVarChar, 200);
                    deptid_rpbm.Value = txt_deptid_rpbmST.SelectedValue;
                    cmd2.Parameters.Add(deptid_rpbm);

                    SqlParameter deptid_rpbmST = new SqlParameter("@deptid_rpbmST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_rpbmST.Value = txt_deptid_rpbmST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_rpbmST);
                    }
                    catch
                    {
                        deptid_rpbmST.Value = txt_deptid_rpbmST.Text;
                        cmd2.Parameters.Add(deptid_rpbmST);
                    }

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter dates_rpbmST = new SqlParameter("@dates_rpbmST", SqlDbType.NVarChar, 200);
                    dates_rpbmST.Value = time_dates_rpbmST.Text;
                    cmd2.Parameters.Add(dates_rpbmST);

                    SqlParameter cdt = new SqlParameter("@cdt", SqlDbType.NVarChar, 200);
                    cdt.Value = txt_cdt.Text;
                    cmd2.Parameters.Add(cdt);

                    SqlParameter ground_done = new SqlParameter("@ground_done", SqlDbType.NVarChar, 200);
                    ground_done.Value = txt_ground_done.Text;
                    cmd2.Parameters.Add(ground_done);

                    SqlParameter area_all_rpbm = new SqlParameter("@area_all_rpbm", SqlDbType.NVarChar, 200);
                    area_all_rpbm.Value = txt_area_all_rpbm.Text;
                    cmd2.Parameters.Add(area_all_rpbm);

                    SqlParameter detail = new SqlParameter("@detail", SqlDbType.NVarChar, 200);
                    detail.Value = txt_detail.Text;
                    cmd2.Parameters.Add(detail);

                    SqlParameter deep = new SqlParameter("@deep", SqlDbType.NVarChar, 200);
                    deep.Value = txt_deep.Text;
                    cmd2.Parameters.Add(deep);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.NVarChar, 200);
                    boss_id.Value = txt_boss_idST.SelectedValue;
                    cmd2.Parameters.Add(boss_id);

                    SqlParameter boss_idST = new SqlParameter("@boss_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        boss_idST.Value = txt_boss_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(boss_idST);
                    }
                    catch
                    {
                        boss_idST.Value = txt_boss_idST.Text;
                        cmd2.Parameters.Add(boss_idST);
                    }

                    SqlParameter boss_id_other = new SqlParameter("@boss_id_other", SqlDbType.NVarChar, 200);
                    boss_id_other.Value = txt_boss_id_other.Text;
                    cmd2.Parameters.Add(boss_id_other);

                    SqlParameter province_id = new SqlParameter("@province_id", SqlDbType.BigInt);
                    province_id.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(province_id);

                    SqlParameter district_id = new SqlParameter("@district_id", SqlDbType.BigInt);
                    district_id.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(district_id);

                    SqlParameter commune_id = new SqlParameter("@commune_id", SqlDbType.BigInt);
                    commune_id.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(commune_id);

                    SqlParameter province_id_rpbm = new SqlParameter("@province_id_rpbm", SqlDbType.BigInt);
                    province_id_rpbm.Value = comboBox_Tinh1.SelectedValue;
                    cmd2.Parameters.Add(province_id_rpbm);

                    SqlParameter district_id_rpbm = new SqlParameter("@district_id_rpbm", SqlDbType.BigInt);
                    district_id_rpbm.Value = comboBox_Huyen1.SelectedValue;
                    cmd2.Parameters.Add(district_id_rpbm);

                    SqlParameter commune_id_rpbm = new SqlParameter("@commune_id_rpbm", SqlDbType.BigInt);
                    commune_id_rpbm.Value = comboBox_Xa1.SelectedValue;
                    cmd2.Parameters.Add(commune_id_rpbm);

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
                        //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                        this.Close();
                        return false;
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);

                return false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            isLuuClicked = true;
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                isLuuClicked = false;
                return;
            }
            isLuuClicked = false;
            bool success = UpdateInfomation(id_BSKQ);

        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }
        private void comboBox_Tinh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Tinh1.SelectedValue is long)
            {
                UtilsDatabase.LoadCBHuyen(comboBox_Huyen1, (long)comboBox_Tinh1.SelectedValue);
            }
        }
        private void comboBox_Huyen1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Huyen1.SelectedValue is long)
            {
                UtilsDatabase.LoadCBXa(comboBox_Xa1, (long)comboBox_Huyen1.SelectedValue);
            }
        }

        private void txt_deptid_rpbmST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            ////TimeBD.Text = null;
            ////TimeKT.Text = null;
            //if (txt_deptid_rpbmST.SelectedItem != null && txt_deptid_rpbmST.Text != "Chọn")
            //{
            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptid_rpbmST.SelectedItem.ToString().Split('-')[0]), _Cn);
            //    sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);

            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        deptidRpbm = int.Parse(dr["dept_id_web"].ToString());
            //        //txt_masterIdST.Text = dr["head"].ToString();
            //    }
            //}
        }

        private void txt_boss_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bossId = ChooseCBB(txt_boss_idST);
            //if(txt_boss_idST.Text == "Chọn")
            //{
            //    txt_boss_id_other.ReadOnly = false;
            //}
            //else
            //{
            //    txt_boss_id_other.ReadOnly = true;
            //}
        }
        private void comboBox_TenDA_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox_TenDA.Text == "" || comboBox_TenDA.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(comboBox_TenDA, "Chưa chọn dự án");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_TenDA, "");
            }
        }

        private void txt_deptid_rpbmST_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_deptid_rpbmST, "");
                return;
            }
            if (txt_deptid_rpbmST.Text == "" || txt_deptid_rpbmST.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_rpbmST, "Chưa chọn đơn vị");
                return;
            }

            else
            {
                e.Cancel = false;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_rpbmST, "");
            }
        }

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            //if (comboBox_Tinh.Text == "" || comboBox_Tinh.Text == "Chọn" )
            //{
            //    e.Cancel = true;
            //    //comboBox_Tinh.Focus();
            //    errorProvider1.SetError(comboBox_Tinh, "Chưa chọn tỉnh");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Tinh, "");
            //}
        }

        private void comboBox_Huyen_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(comboBox_Tinh.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Huyen, "");
            //    return;
            //}
            //if (comboBox_Huyen.Text == "" || comboBox_Huyen.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    //comboBox_Tinh.Focus();
            //    errorProvider1.SetError(comboBox_Huyen, "Chưa chọn huyện");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Huyen, "");
            //}
        }

        private void comboBox_Xa_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(comboBox_Huyen.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Xa, "");
            //    return;
            //}
            //if (comboBox_Xa.Text == "" || comboBox_Xa.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    //comboBox_Tinh.Focus();
            //    errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Xa, "");
            //}
        }
        private void comboBox_Tinh1_Validating(object sender, CancelEventArgs e)
        {
            //if (comboBox_Tinh1.Text == "" || comboBox_Tinh1.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    //comboBox_Tinh.Focus();
            //    errorProvider1.SetError(comboBox_Tinh1, "Chưa chọn tỉnh");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Tinh1, "");
            //}
        }

        private void comboBox_Huyen1_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(comboBox_Tinh1.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Huyen1, "");
            //    return;
            //}
            //if (comboBox_Huyen1.Text == "" || comboBox_Huyen1.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    //comboBox_Tinh.Focus();
            //    errorProvider1.SetError(comboBox_Huyen1, "Chưa chọn huyện");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Huyen1, "");
            //}
        }

        private void comboBox_Xa1_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(comboBox_Huyen1.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Xa1, "");
            //    return;
            //}
            //if (comboBox_Xa1.Text == "" || comboBox_Xa1.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    //comboBox_Tinh.Focus();
            //    errorProvider1.SetError(comboBox_Xa1, "Chưa chọn xã");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Xa1, "");
            //}
        }

        private void txt_boss_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_boss_idST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_boss_idST.SelectedValue > 0)
            {
                txt_boss_id_other.ReadOnly = true;
                txt_boss_id_other.Text = "";
            }
            else
            {
                txt_boss_id_other.ReadOnly = false;
            }
        }
        public string openTextLb = "All files (*.*)|*.*";

        private void btOpentbDoc_file_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(AppUtils.ReportFolder);
            if (string.IsNullOrEmpty(filePath) == false)
                tbDoc_file.Text = filePath;
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            tbDoc_file.Text = "";
        }

        private void tbDoc_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(AppUtils.ReportFolder), tbDoc_file.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }
    }
}
