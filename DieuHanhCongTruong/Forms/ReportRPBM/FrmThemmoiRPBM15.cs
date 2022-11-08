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
using DieuHanhCongTruong.ReportRPBM;

namespace DieuHanhCongTruong
{
    public partial class FrmThemmoiRPBM15 : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        public int DuanId = 0;
        public int Deptid_survey = 0;
        public int Captain_id = 0;
        public int Boss_id = 0;
        public int Captain_sign_id = 0;
        public int Cadres_id = 0;
        public string addressDuan = "";
        public int TinhId = 0;
        public int HuyenId = 0;
        public int XaId = 0;
        public string symbol = "0";
        public string table_name = "PlanDestroyBomb";
        public string field_name1 = "PlanDestroyBomb_PlanDestroyBomb_Type";
        public string field_name2 = "PlanDestroyBomb_PlanDestroyBomb_Vehicle";
        //public int command_shipId = 0;
        //public int bossId = 0;
        //public int command_destroyId = 0;
        //public int dept_id = 0;
        public int category = 0;
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM15(int i)
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
        private void LoadCBStaff(ComboBox cb)
        {
            cb.DataSource = null;
            //if (comboBox_TenDA.SelectedValue is long)
            //{
            UtilsDatabase.buildCombobox(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + DuanId, "id", "nameId");
            //}

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
        private void comboBox_Xa_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;

            if (comboBox_Xa.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Xa.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
                System.Data.DataTable datatableCounty = new System.Data.DataTable();
                sqlAdapterCounty.Fill(datatableCounty);

                foreach (DataRow dr in datatableCounty.Rows)
                {
                    XaId = int.Parse(dr["id"].ToString());
                }
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
                    LoadCBStaff(txt_boss_idST);
                    LoadCBStaff(txt_command_ship_idST);
                    LoadCBStaff(txt_command_destroy_idST);
                    UtilsDatabase.LoadCBDept(txt_deptidST, DuanId);
                }
                //SqlDataAdapter sqlAdapterCounty1 = new SqlDataAdapter(string.Format("select CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web as 'gid',dtg.cecm_program_id,dtg.table_name from dept_tham_gia dtg left join cert_department d on CASE WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1 WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1 ELSE 0 END = 1 WHERE dtg.cecm_program_id = {0} and dtg.dept_id_web IS NOT NULL", DuanId), _Cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterCounty1);
                //System.Data.DataTable datatableCounty1 = new System.Data.DataTable();
                //sqlAdapterCounty1.Fill(datatableCounty1);
                //txt_deptidST.Items.Clear();
                //txt_deptidST.Items.Add("Chọn");
                //foreach (DataRow dr in datatableCounty1.Rows)
                //{
                //    if (string.IsNullOrEmpty(dr["dept_idST"].ToString()))
                //        continue;
                //    var a = dr["gid"].ToString() + "-" + dr["dept_idST"].ToString();
                //    txt_deptidST.Items.Add(a);
                //}
            }
        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM15 where gid = {0}", id_BSKQ), _Cn);
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
                        time_dates_notifiedST.Value = DateTime.ParseExact(dr["dates_notifiedST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_time_signedST.Value = DateTime.ParseExact(dr["time_signedST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_time_deli_fromST.Value = DateTime.ParseExact(dr["time_deli_fromST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_time_deli_toST.Value = DateTime.ParseExact(dr["time_deli_toST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        txt_base_on_tech.Text = dr["base_on_tech"].ToString();
                        txt_technical_regulations.Text = dr["technical_regulations"].ToString();
                        txt_num_all_bomb.Text = dr["num_all_bomb"].ToString();
                        txt_force_used.Text = dr["force_used"].ToString();
                        txt_address_destroy.Text = dr["address_destroy"].ToString();
                        txt_num_ship.Text = dr["num_ship"].ToString();
                        txt_organ_approve.Text = dr["organ_approve"].ToString();
                        txt_plan_approve.Text = dr["plan_approve"].ToString();
                        txt_time_destroy_from.Text = dr["time_destroy_from"].ToString();
                        txt_time_destroy_to.Text = dr["time_destroy_to"].ToString();

                        txt_deptidST.SelectedValue = long.TryParse(dr["deptid"].ToString(), out long deptid) ? deptid : -1;
                        //txt_deptidST.Text = dr["deptidST"].ToString();

                        //bossId = int.Parse(dr["boss_id"].ToString());
                        //txt_boss_idST.Text = dr["boss_idST"].ToString();
                        if(long.TryParse(dr["boss_id"].ToString(), out long boss_id))
                        {
                            txt_boss_idST.SelectedValue = boss_id;
                        }
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();

                        //command_destroyId = int.Parse(dr["command_destroy_id"].ToString());
                        //txt_command_destroy_idST.Text = dr["command_destroy_idST"].ToString();
                        if (long.TryParse(dr["command_destroy_id"].ToString(), out long command_destroy_id))
                        {
                            txt_command_destroy_idST.SelectedValue = command_destroy_id;
                        }
                        txt_command_destroy_id_other.Text = dr["command_destroy_id_other"].ToString();

                        
                        //command_shipId = int.Parse(dr["command_ship_id"].ToString());
                        //txt_command_ship_idST.Text = dr["command_ship_idST"].ToString();
                        if (long.TryParse(dr["command_ship_id"].ToString(), out long command_ship_id))
                        {
                            txt_command_ship_idST.SelectedValue = command_ship_id;
                        }
                        txt_command_ship_id_other.Text = dr["command_ship_id_other"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();
                    }
                }
            }
        }
        private void FrmThemmoiRPBM15_Load(object sender, EventArgs e)
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
            LoadData1();
            LoadData2();

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
                        "UPDATE [dbo].[RPBM15] SET " +
                        "[symbol] = @symbol," +
                        "[address] = @address," +
                        "[datesST] = @datesST," +
                        "[deptid] = @deptid," +
                        "[deptidST] = @deptidST," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[address_cecm] = @address_cecm," +
                        "[technical_regulations] = @technical_regulations," +
                        "[base_on_tech] = @base_on_tech," +
                        "[time_signedST] = @time_signedST," +
                        "[num_all_bomb] = @num_all_bomb," +
                        "[force_used] = @force_used," +
                        "[command_destroy_id] = @command_destroy_id," +
                        "[command_destroy_idST] = @command_destroy_idST," +
                        "[command_destroy_id_other] = @command_destroy_id_other," +
                        "[dates_notifiedST] = @dates_notifiedST," +
                        "[time_deli_fromST] = @time_deli_fromST," +
                        "[time_deli_toST] = @time_deli_toST," +
                        "[time_destroy_from] = @time_destroy_from," +
                        "[time_destroy_to] = @time_destroy_to," +
                        "[address_destroy] = @address_destroy," +
                        "[num_ship] = @num_ship," +
                        "[command_ship_id] = @command_ship_id," +
                        "[command_ship_idST] = @command_ship_idST," +
                        "[command_ship_id_other] = @command_ship_id_other," +
                        "[organ_approve] = @organ_approve," +
                        "[plan_approve] = @plan_approve," +
                        "[boss_id] = @boss_id," +
                        "[boss_idST] = @boss_idST," +
                        "[boss_id_other] = @boss_id_other," +
                        "[files]=@files, " +
                        "[province_id]=@province_id," +
                        "[district_id]=@district_id," +
                        "[commune_id]=@commune_id " +
                        "WHERE gid = " + dem, _Cn);

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

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter deptid = new SqlParameter("@deptid", SqlDbType.NVarChar, 200);
                    deptid.Value = txt_deptidST.SelectedValue;
                    cmd2.Parameters.Add(deptid);

                    SqlParameter deptidST = new SqlParameter("@deptidST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptidST.Value = txt_deptidST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptidST);
                    }
                    catch
                    {
                        deptidST.Value = txt_deptidST.Text;
                        cmd2.Parameters.Add(deptidST);
                    }

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter time_signedST = new SqlParameter("@time_signedST", SqlDbType.NVarChar, 200);
                    time_signedST.Value = time_time_signedST.Text;
                    cmd2.Parameters.Add(time_signedST);

                    SqlParameter dates_notifiedST = new SqlParameter("@dates_notifiedST", SqlDbType.NVarChar, 200);
                    dates_notifiedST.Value = time_dates_notifiedST.Text;
                    cmd2.Parameters.Add(dates_notifiedST);

                    SqlParameter time_deli_fromST = new SqlParameter("@time_deli_fromST", SqlDbType.NVarChar, 200);
                    time_deli_fromST.Value = time_time_deli_fromST.Text;
                    cmd2.Parameters.Add(time_deli_fromST);

                    SqlParameter time_deli_toST = new SqlParameter("@time_deli_toST", SqlDbType.NVarChar, 200);
                    time_deli_toST.Value = time_time_deli_toST.Text;
                    cmd2.Parameters.Add(time_deli_toST);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    base_on_tech.Value = txt_base_on_tech.Text;
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter num_all_bomb = new SqlParameter("@num_all_bomb", SqlDbType.NVarChar, 200);
                    num_all_bomb.Value = txt_num_all_bomb.Text;
                    cmd2.Parameters.Add(num_all_bomb);

                    SqlParameter force_used = new SqlParameter("@force_used", SqlDbType.NVarChar, 200);
                    force_used.Value = txt_force_used.Text;
                    cmd2.Parameters.Add(force_used);

                    SqlParameter time_destroy_from = new SqlParameter("@time_destroy_from", SqlDbType.NVarChar, 200);
                    time_destroy_from.Value = txt_time_destroy_from.Text;
                    cmd2.Parameters.Add(time_destroy_from);

                    SqlParameter time_destroy_to = new SqlParameter("@time_destroy_to", SqlDbType.NVarChar, 200);
                    time_destroy_to.Value = txt_time_destroy_to.Text;
                    cmd2.Parameters.Add(time_destroy_to);

                    SqlParameter address_destroy = new SqlParameter("@address_destroy", SqlDbType.NVarChar, 200);
                    address_destroy.Value = txt_address_destroy.Text;
                    cmd2.Parameters.Add(address_destroy);

                    SqlParameter num_ship = new SqlParameter("@num_ship", SqlDbType.NVarChar, 200);
                    num_ship.Value = txt_num_ship.Text;
                    cmd2.Parameters.Add(num_ship);

                    SqlParameter organ_approve = new SqlParameter("@organ_approve", SqlDbType.NVarChar, 200);
                    organ_approve.Value = txt_organ_approve.Text;
                    cmd2.Parameters.Add(organ_approve);

                    SqlParameter plan_approve = new SqlParameter("@plan_approve", SqlDbType.NVarChar, 200);
                    plan_approve.Value = txt_plan_approve.Text;
                    cmd2.Parameters.Add(plan_approve);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.BigInt);
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

                    SqlParameter command_ship_id = new SqlParameter("@command_ship_id", SqlDbType.BigInt);
                    command_ship_id.Value = txt_command_ship_idST.SelectedValue;
                    cmd2.Parameters.Add(command_ship_id);

                    SqlParameter command_ship_idST = new SqlParameter("@command_ship_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        command_ship_idST.Value = txt_command_ship_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(command_ship_idST);
                    }
                    catch
                    {
                        command_ship_idST.Value = txt_command_ship_idST.Text;
                        cmd2.Parameters.Add(command_ship_idST);
                    }

                    SqlParameter command_ship_id_other = new SqlParameter("@command_ship_id_other", SqlDbType.NVarChar, 200);
                    command_ship_id_other.Value = txt_command_ship_id_other.Text;
                    cmd2.Parameters.Add(command_ship_id_other);

                    SqlParameter command_destroy_id = new SqlParameter("@command_destroy_id", SqlDbType.BigInt);
                    command_destroy_id.Value = txt_command_destroy_idST.SelectedValue;
                    cmd2.Parameters.Add(command_destroy_id);

                    SqlParameter command_destroy_idST = new SqlParameter("@command_destroy_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        command_destroy_idST.Value = txt_command_destroy_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(command_destroy_idST);
                    }
                    catch
                    {
                        command_destroy_idST.Value = txt_command_destroy_idST.Text;
                        cmd2.Parameters.Add(command_destroy_idST);
                    }

                    SqlParameter command_destroy_id_other = new SqlParameter("@command_destroy_id_other", SqlDbType.NVarChar, 200);
                    command_destroy_id_other.Value = txt_command_destroy_id_other.Text;
                    cmd2.Parameters.Add(command_destroy_id_other);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter province_id = new SqlParameter("@province_id", SqlDbType.BigInt);
                    province_id.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(province_id);

                    SqlParameter district_id = new SqlParameter("@district_id", SqlDbType.BigInt);
                    district_id.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(district_id);

                    SqlParameter commune_id = new SqlParameter("@commune_id", SqlDbType.BigInt);
                    commune_id.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(commune_id);

                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();
                        SqlCommand cmd3 = new SqlCommand("UPDATE groundDeliveryRecordsMember SET [main_id] = @main_id WHERE main_id = 0 and table_name = N'" + table_name + "'", _Cn);

                        SqlParameter main_id = new SqlParameter("@main_id", SqlDbType.Int);
                        main_id.Value = dem;
                        cmd3.Parameters.Add(main_id);

                        int temp12 = 0;
                        temp12 = cmd3.ExecuteNonQuery();
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
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[RPBM15]([symbol],[address],[datesST],[deptid],[deptidST],[cecm_program_id],[cecm_program_idST],[address_cecm],[technical_regulations],[base_on_tech],[time_signedST],[num_all_bomb],[force_used],[command_destroy_id],[command_destroy_idST],[command_destroy_id_other],[dates_notifiedST],[time_deli_fromST],[time_deli_toST],[time_destroy_from],[time_destroy_to],[address_destroy],[num_ship],[command_ship_id],[command_ship_idST],[command_ship_id_other],[organ_approve],[plan_approve],[boss_id],[boss_idST],[boss_id_other],[files],province_id,district_id,commune_id)" +
                        "VALUES(@symbol,@address,@datesST,@deptid,@deptidST,@cecm_program_id,@cecm_program_idST,@address_cecm,@technical_regulations,@base_on_tech,@time_signedST,@num_all_bomb,@force_used,@command_destroy_id,@command_destroy_idST,@command_destroy_id_other,@dates_notifiedST,@time_deli_fromST,@time_deli_toST,@time_destroy_from,@time_destroy_to,@address_destroy,@num_ship,@command_ship_id,@command_ship_idST,@command_ship_id_other,@organ_approve,@plan_approve,@boss_id,@boss_idST,@boss_id_other,@files,@province_id,@district_id,@commune_id)", _Cn);

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

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter deptid = new SqlParameter("@deptid", SqlDbType.NVarChar, 200);
                    deptid.Value = txt_deptidST.SelectedValue;
                    cmd2.Parameters.Add(deptid);

                    SqlParameter deptidST = new SqlParameter("@deptidST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptidST.Value = txt_deptidST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptidST);
                    }
                    catch
                    {
                        deptidST.Value = txt_deptidST.Text;
                        cmd2.Parameters.Add(deptidST);
                    }

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter time_signedST = new SqlParameter("@time_signedST", SqlDbType.NVarChar, 200);
                    time_signedST.Value = time_time_signedST.Text;
                    cmd2.Parameters.Add(time_signedST);

                    SqlParameter dates_notifiedST = new SqlParameter("@dates_notifiedST", SqlDbType.NVarChar, 200);
                    dates_notifiedST.Value = time_dates_notifiedST.Text;
                    cmd2.Parameters.Add(dates_notifiedST);

                    SqlParameter time_deli_fromST = new SqlParameter("@time_deli_fromST", SqlDbType.NVarChar, 200);
                    time_deli_fromST.Value = time_time_deli_fromST.Text;
                    cmd2.Parameters.Add(time_deli_fromST);

                    SqlParameter time_deli_toST = new SqlParameter("@time_deli_toST", SqlDbType.NVarChar, 200);
                    time_deli_toST.Value = time_time_deli_toST.Text;
                    cmd2.Parameters.Add(time_deli_toST);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    base_on_tech.Value = txt_base_on_tech.Text;
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter num_all_bomb = new SqlParameter("@num_all_bomb", SqlDbType.NVarChar, 200);
                    num_all_bomb.Value = txt_num_all_bomb.Text;
                    cmd2.Parameters.Add(num_all_bomb);

                    SqlParameter force_used = new SqlParameter("@force_used", SqlDbType.NVarChar, 200);
                    force_used.Value = txt_force_used.Text;
                    cmd2.Parameters.Add(force_used);

                    SqlParameter time_destroy_from = new SqlParameter("@time_destroy_from", SqlDbType.NVarChar, 200);
                    time_destroy_from.Value = txt_time_destroy_from.Text;
                    cmd2.Parameters.Add(time_destroy_from);

                    SqlParameter time_destroy_to = new SqlParameter("@time_destroy_to", SqlDbType.NVarChar, 200);
                    time_destroy_to.Value = txt_time_destroy_to.Text;
                    cmd2.Parameters.Add(time_destroy_to);

                    SqlParameter address_destroy = new SqlParameter("@address_destroy", SqlDbType.NVarChar, 200);
                    address_destroy.Value = txt_address_destroy.Text;
                    cmd2.Parameters.Add(address_destroy);

                    SqlParameter num_ship = new SqlParameter("@num_ship", SqlDbType.NVarChar, 200);
                    num_ship.Value = txt_num_ship.Text;
                    cmd2.Parameters.Add(num_ship);

                    SqlParameter organ_approve = new SqlParameter("@organ_approve", SqlDbType.NVarChar, 200);
                    organ_approve.Value = txt_organ_approve.Text;
                    cmd2.Parameters.Add(organ_approve);

                    SqlParameter plan_approve = new SqlParameter("@plan_approve", SqlDbType.NVarChar, 200);
                    plan_approve.Value = txt_plan_approve.Text;
                    cmd2.Parameters.Add(plan_approve);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.BigInt);
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

                    SqlParameter command_ship_id = new SqlParameter("@command_ship_id", SqlDbType.BigInt);
                    command_ship_id.Value = txt_command_ship_idST.SelectedValue;
                    cmd2.Parameters.Add(command_ship_id);

                    SqlParameter command_ship_idST = new SqlParameter("@command_ship_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        command_ship_idST.Value = txt_command_ship_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(command_ship_idST);
                    }
                    catch
                    {
                        command_ship_idST.Value = txt_command_ship_idST.Text;
                        cmd2.Parameters.Add(command_ship_idST);
                    }

                    SqlParameter command_ship_id_other = new SqlParameter("@command_ship_id_other", SqlDbType.NVarChar, 200);
                    command_ship_id_other.Value = txt_command_ship_id_other.Text;
                    cmd2.Parameters.Add(command_ship_id_other);

                    SqlParameter command_destroy_id = new SqlParameter("@command_destroy_id", SqlDbType.BigInt);
                    command_destroy_id.Value = txt_command_destroy_idST.SelectedValue;
                    cmd2.Parameters.Add(command_destroy_id);

                    SqlParameter command_destroy_idST = new SqlParameter("@command_destroy_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        command_destroy_idST.Value = txt_command_destroy_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(command_destroy_idST);
                    }
                    catch
                    {
                        command_destroy_idST.Value = txt_command_destroy_idST.Text;
                        cmd2.Parameters.Add(command_destroy_idST);
                    }

                    SqlParameter command_destroy_id_other = new SqlParameter("@command_destroy_id_other", SqlDbType.NVarChar, 200);
                    command_destroy_id_other.Value = txt_command_destroy_id_other.Text;
                    cmd2.Parameters.Add(command_destroy_id_other);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter province_id = new SqlParameter("@province_id", SqlDbType.BigInt);
                    province_id.Value = comboBox_Tinh.SelectedValue;
                    cmd2.Parameters.Add(province_id);

                    SqlParameter district_id = new SqlParameter("@district_id", SqlDbType.BigInt);
                    district_id.Value = comboBox_Huyen.SelectedValue;
                    cmd2.Parameters.Add(district_id);

                    SqlParameter commune_id = new SqlParameter("@commune_id", SqlDbType.BigInt);
                    commune_id.Value = comboBox_Xa.SelectedValue;
                    cmd2.Parameters.Add(commune_id);

                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();
                        SqlCommandBuilder sqlCommand = null;
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM RPBM15"), _Cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                        System.Data.DataTable datatableWard = new System.Data.DataTable();
                        sqlAdapterWard.Fill(datatableWard);

                        foreach (DataRow dr in datatableWard.Rows)
                        {
                            var gid1 = int.Parse(dr["gid"].ToString());
                            SqlCommand cmd3 = new SqlCommand("UPDATE groundDeliveryRecordsMember SET [main_id] = @main_id WHERE main_id = 0 and table_name = N'" + table_name + "'", _Cn);

                            SqlParameter main_id = new SqlParameter("@main_id", SqlDbType.Int);
                            main_id.Value = gid1;
                            cmd3.Parameters.Add(main_id);

                            int temp12 = 0;
                            temp12 = cmd3.ExecuteNonQuery();
                        }
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
        private void LoadData1()
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dgvBMVN.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                string sql = string.Format(
                    "select " +
                    "tbl.*, " +
                    "mst.dvs_name LoaiBMVN " +
                    "from groundDeliveryRecordsMember tbl " +
                    "left join mst_division mst on mst.dvs_value = tbl.long1 and mst.dvs_group_cd = '001' " +
                    "where main_id = {0} " +
                    "and field_name = N'{1}' " +
                    "and table_name = N'{2}'", id_BSKQ, field_name1, table_name);
                sqlAdapter = new SqlDataAdapter(sql, _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var gid = dr["gid"].ToString();
                        var LoaiBMVN = dr["LoaiBMVN"].ToString();
                        var string1 = "";
                        if (string.IsNullOrEmpty(LoaiBMVN) || LoaiBMVN == "Loại khác")
                        {
                            string1 = dr["string1"].ToString();
                        }
                        else
                        {
                            string1 = LoaiBMVN;
                        }
                        var long2 = dr["long2"].ToString();
                        var string2 = dr["string2"].ToString();
                        dgvBMVN.Rows.Add(indexRow, string1, long2, string2);
                        dgvBMVN.Rows[indexRow - 1].Tag = gid;

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
        private void LoadData2()
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dgvPhuongTien.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                string sql = string.Format("select * from groundDeliveryRecordsMember where main_id = {0} and field_name = N'{1}' and table_name = N'{2}'", id_BSKQ, field_name2, table_name);
                sqlAdapter = new SqlDataAdapter(sql, _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var gid = dr["gid"].ToString();
                        var string1 = dr["string1"].ToString();
                        var string2 = dr["string2"].ToString();
                        var long2 = dr["long2"].ToString();
                        dgvPhuongTien.Rows.Add(indexRow, string1, long2, string2);
                        dgvPhuongTien.Rows[indexRow - 1].Tag = gid;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvBMVN.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == dgvBMVNSua.Index)
            {
                FormThemmoiKetQuaBMVNMini frm = new FormThemmoiKetQuaBMVNMini(id_kqks, id_BSKQ, field_name1, table_name);
                frm.Text = "CHỈNH SỬA CHỦNG LOẠI BOM MÌN";
                frm.ShowDialog();
                LoadData1();
            }
            //delete column
            if (e.ColumnIndex == dgvBMVNXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM groundDeliveryRecordsMember WHERE gid = " + id_kqks, _Cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                }
                LoadData1();
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvPhuongTien.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == dgvPhuongTienSua.Index)
            {
                FormThemmoiPhuongTien frm = new FormThemmoiPhuongTien(id_kqks, id_BSKQ, field_name2, table_name);
                frm.Text = "CHỈNH SỬA PHƯƠNG TIỆN THAM GIA";
                frm.ShowDialog();
                LoadData2();
            }
            //delete column
            if (e.ColumnIndex == dgvPhuongTienXoa.Index)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                SqlCommand cmd2 = new SqlCommand("DELETE FROM groundDeliveryRecordsMember WHERE gid = " + id_kqks, _Cn);
                int temp = 0;
                temp = cmd2.ExecuteNonQuery();
                if (temp > 0)
                {
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu ko thành công... ", "Thất bại");
                }
                LoadData2();
            }
        }

        private void buttonSave1_Click(object sender, EventArgs e)
        {
            FormThemmoiKetQuaBMVNMini frm = new FormThemmoiKetQuaBMVNMini(0, id_BSKQ, field_name1, table_name);
            frm.Text = "THÊM MỚI CHỦNG LOẠI BOM MÌN";
            //frm.label4.Text = "Tên bom mìn";
            //frm.label2.Text = "Số lượng";
            frm.ShowDialog();

            LoadData1();
        }
        private void buttonSave2_Click(object sender, EventArgs e)
        {
            FormThemmoiPhuongTien frm = new FormThemmoiPhuongTien(0, id_BSKQ, field_name2, table_name);
            frm.Text = "THÊM MỚI PHƯƠNG TIỆN THAM GIA";
            //frm.label4.Text = "Tên phương tiện";
            //frm.label2.Text = "Số lượng";
            frm.ShowDialog();

            LoadData2();
        }

        private void txt_boss_idST_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //bossId = ChooseCBB(txt_boss_idST);
            //txt_boss_id_other.ReadOnly = true;
        }

        private void txt_deptidST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            ////TimeBD.Text = null;
            ////TimeKT.Text = null;
            //if (txt_deptidST.SelectedItem != null && txt_deptidST.Text != "Chọn")
            //{
            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptidST.SelectedItem.ToString().Split('-')[0]), _Cn);
            //    sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);

            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        dept_id = int.Parse(dr["dept_id_web"].ToString());
            //        //txt_masterIdST.Text = dr["head"].ToString();
            //    }
            //}
        }

        private void txt_command_ship_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //command_shipId = ChooseCBB(txt_command_ship_idST);
            //txt_command_ship_id_other.ReadOnly = true;
        }

        private void txt_command_destroy_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //command_destroyId = ChooseCBB(txt_command_destroy_idST);
            //txt_command_destroy_id_other.ReadOnly = true;
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

        private void txt_deptidST_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_deptidST, "");
                return;
            }
            if (txt_deptidST.Text == "" || txt_deptidST.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptidST, "Chưa chọn đơn vị");
                return;
            }

            else
            {
                e.Cancel = false;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptidST, "");
            }
        }

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            //if (comboBox_Tinh.Text == "" || comboBox_Tinh.Text == "Chọn")
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
            //if (comboBox_Huyen.Text == "" || comboBox_Huyen.Text=="Chọn")
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

        private void txt_command_destroy_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_command_destroy_idST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_command_destroy_idST.SelectedValue > 0)
            {
                txt_command_destroy_id_other.ReadOnly = true;
                txt_command_destroy_id_other.Text = "";
            }
            else
            {
                txt_command_destroy_id_other.ReadOnly = false;
            }
        }

        private void txt_command_ship_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_command_ship_idST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_command_ship_idST.SelectedValue > 0)
            {
                txt_command_ship_id_other.ReadOnly = true;
                txt_command_ship_id_other.Text = "";
            }
            else
            {
                txt_command_ship_id_other.ReadOnly = false;
            }
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

        private void txt_symbol_Validating(object sender, CancelEventArgs e)
        {
            if (txt_symbol.Text == "")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_symbol, "Chưa nhập số nhật kí");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_symbol, "");
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

