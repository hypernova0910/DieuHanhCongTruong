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
    public partial class FrmThemmoiRPBM1 : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        public int DuanId = 0;
        public string addressDuan = "";
        public int TinhId = 0;
        public int HuyenId = 0;
        public int XaId = 0;
        public string table_name = "GROUNDDELIVERYRECORDS";
        public int bossId = 0;
        public int surveyId = 0;
        public int deptidHandover = 0;
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM1(int i)
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
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox A = (TextBox)sender;

            if (!double.TryParse(A.Text, out double a))
            {
                A.Text = "0";
            }
        }
        //private void LoadCBStaff(ComboBox cb)
        //{
        //    cb.DataSource = null;
        //    //if (comboBox_TenDA.SelectedValue is long)
        //    //{
        //    UtilsDatabase.buildCombobox(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + DuanId, "id", "nameId");
        //    //}

        //}
        public void LoadCBDept(ComboBox cb)
        {
            cb.DataSource = null;
            //if (comboBox_TenDA.SelectedValue is long)
            //{
            UtilsDatabase.buildCombobox(cb, "select CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web as 'gid',dtg.cecm_program_id,dtg.table_name from dept_tham_gia dtg left join cert_department d on CASE WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1 WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1 ELSE 0 END = 1 WHERE dtg.cecm_program_id = {0} and dtg.dept_id_web IS NOT NULL"+DuanId, "gid", "dept_idST");
            //}

        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from groundDeliveryRecords where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        txt_symbol.Text = dr["symbol"].ToString();
                        DuanId = int.Parse(dr["cecm_program_id"].ToString());
                        tbDoc_file.Text = dr["files"].ToString();
                        //bossId = int.Parse(dr["boss_id"].ToString());
                        if (long.TryParse(dr["boss_id"].ToString(), out long boss_id))
                        {
                            txt_boss_idST.SelectedValue = boss_id;
                        }
                        //surveyId = int.Parse(dr["survey_id"].ToString()) ;
                        if (long.TryParse(dr["survey_id"].ToString(), out long survey_id))
                        {
                            txt_survey_idST.SelectedValue = survey_id;
                        }
                        //deptidHandover = int.Parse(dr["deptid_handover"].ToString());
                        if (long.TryParse(dr["deptid_handover"].ToString(), out long deptid_handover))
                        {
                            txt_deptid_handoverST.SelectedValue = deptid_handover;
                        }
                        comboBox_TenDA.Text = dr["cecm_program_idST"].ToString();
                        var adress = dr["address"].ToString().Split(',');
                        comboBox_Tinh.Text = adress[2];
                        comboBox_Huyen.Text = adress[1];
                        comboBox_Xa.Text = adress[0];
                        addressDuan = dr["address_cecm"].ToString();
                        time_datesST.Value = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_base_on_tech.Text = dr["base_on_tech"].ToString();
                        txt_technical_regulations.Text = dr["technical_regulations"].ToString();
                        txt_num_economic_contracts.Text = dr["num_economic_contracts"].ToString();
                        time_date_economic_contractsST.Value = DateTime.ParseExact(dr["date_economic_contractsST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_organization_signed.Text = dr["organization_signed"].ToString();
                        time_time_signedST.Value = DateTime.ParseExact(dr["time_signedST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_detail_giaonhan.Text = dr["detail_giaonhan"].ToString();
                        richTextBox_technical_regulationsST.Text = dr["technical_regulationsST"].ToString();
                        txt_area_ground.Text = dr["area_ground"].ToString();
                        txt_area_construction.Text = dr["area_construction"].ToString();
                        txt_deep.Text = dr["deep"].ToString();
                        txt_request_other.Text = dr["request_other"].ToString();
                        txt_deptid_handoverST.Text = dr["deptid_handoverST"].ToString();
                        txt_conclusion.Text = dr["conclusion"].ToString();
                        txt_amount.Text = dr["amount"].ToString();
                        txt_boss_idST.Text = dr["boss_idST"].ToString();
                        txt_survey_idST.Text = dr["survey_idST"].ToString();
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();
                        txt_survey_id_other.Text = dr["survey_id_other"].ToString();
                    }
                }
            }
        }
        private void FrmThemmoiRPBM1_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT Ten FROM cecm_provinces where level = 1", _Cn); sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);
            comboBox_Tinh.Items.Add("Chọn");
            foreach (DataRow dr in datatableProvince.Rows)
            {
                if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                    continue;

                comboBox_Tinh.Items.Add(dr["Ten"].ToString());
            }

            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
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
            //GroundDeliveryRecords_GroundDeliveryRecords_CDTMember
            //GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem
            LoadData1(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember");
            LoadData2(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem");
        }
        private void GetAllStaffWithIdProgram(System.Windows.Forms.ComboBox cb) {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterCounty1 = new SqlDataAdapter(string.Format("SELECT * FROM Cecm_ProgramStaff where cecmProgramId = {0}",DuanId), _Cn);
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
        private void txt_Staff1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void txt_Staff2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void comboBox_Tinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            comboBox_Huyen.Text = null;
            comboBox_Xa.Text = null;
            if (comboBox_Tinh.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Tinh.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    TinhId = int.Parse(dr["id"].ToString());
                }

                SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 2 and parent_id = {0}", TinhId), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
                System.Data.DataTable datatableCounty = new System.Data.DataTable();
                sqlAdapterCounty.Fill(datatableCounty);
                comboBox_Huyen.Items.Clear();
                comboBox_Huyen.Items.Add("Chọn");
                foreach (DataRow dr in datatableCounty.Rows)
                {
                    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                        continue;

                    comboBox_Huyen.Items.Add(dr["Ten"].ToString());
                }


            }
        }

        private void comboBox_Huyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            comboBox_Xa.Text = null;
            if (comboBox_Huyen.SelectedItem != null)
            {
                SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", comboBox_Huyen.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
                System.Data.DataTable datatableCounty = new System.Data.DataTable();
                sqlAdapterCounty.Fill(datatableCounty);

                foreach (DataRow dr in datatableCounty.Rows)
                {
                    HuyenId = int.Parse(dr["id"].ToString());
                }

                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT Ten FROM cecm_provinces where level = 3 and parentiddistrict = {0}", HuyenId), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);
                comboBox_Xa.Items.Clear();
                comboBox_Xa.Items.Add("Chọn");
                foreach (DataRow dr in datatableWard.Rows)
                {
                    if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                        continue;

                    comboBox_Xa.Items.Add(dr["Ten"].ToString());
                }
            }
        }

        private void comboBox_TenDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            txt_boss_idST.Text = "Chọn";
            txt_survey_idST.Text = "Chọn";
            txt_deptid_handoverST.Text = "Chọn";
            addressDuan = "";
            if (comboBox_TenDA.SelectedItem != null && comboBox_TenDA.Text != "Chọn")
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM cecm_programData where name = N'{0}'", comboBox_TenDA.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    DuanId = int.Parse(dr["id"].ToString());
                    addressDuan = dr["address"].ToString();
                    UtilsDatabase.LoadCBStaff(txt_boss_idST,DuanId);
                    UtilsDatabase.LoadCBStaff(txt_survey_idST, DuanId);
                    UtilsDatabase.LoadCBDept(txt_deptid_handoverST, DuanId);
                }
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
                    SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[groundDeliveryRecords] SET [symbol] = @symbol, [address] = @address,[datesST] = @datesST,[cecm_program_id] = @cecm_program_id,[cecm_program_idST] = @cecm_program_idST,[address_cecm] = @address_cecm,[base_on_tech] = @base_on_tech,[technical_regulations] = @technical_regulations,[technical_regulationsST] = @technical_regulationsST,[num_economic_contracts] = @num_economic_contracts,[date_economic_contractsST] = @date_economic_contractsST,[organization_signed] = @organization_signed,[time_signedST] = @time_signedST,[detail_giaonhan] = @detail_giaonhan,[area_ground] = @area_ground,[area_construction] = @area_construction,[deep] = @deep,[request_other] = @request_other,[deptid_handover] = @deptid_handover,[deptid_handoverST] = @deptid_handoverST,[conclusion] = @conclusion,[amount] = @amount,[boss_id] = @boss_id,[survey_id] = @survey_id,[boss_idST] = @boss_idST,[survey_idST] = @survey_idST,[files]=@files WHERE gid = " + dem, _Cn);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = comboBox_Xa.Text + "," + comboBox_Huyen.Text + "," + comboBox_Tinh.Text;
                    cmd2.Parameters.Add(address);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter cecm_program_idST = new SqlParameter("cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    base_on_tech.Value = txt_base_on_tech.Text;
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter technical_regulationsST = new SqlParameter("@technical_regulationsST", SqlDbType.NVarChar, 200);
                    technical_regulationsST.Value = richTextBox_technical_regulationsST.Text;
                    cmd2.Parameters.Add(technical_regulationsST);

                    SqlParameter num_economic_contracts = new SqlParameter("@num_economic_contracts", SqlDbType.NVarChar, 200);
                    num_economic_contracts.Value = txt_num_economic_contracts.Text;
                    cmd2.Parameters.Add(num_economic_contracts);

                    SqlParameter date_economic_contractsST = new SqlParameter("@date_economic_contractsST", SqlDbType.NVarChar, 200);
                    date_economic_contractsST.Value = time_date_economic_contractsST.Text;
                    cmd2.Parameters.Add(date_economic_contractsST);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter time_signedST = new SqlParameter("@time_signedST", SqlDbType.NVarChar, 200);
                    time_signedST.Value = time_time_signedST.Text;
                    cmd2.Parameters.Add(time_signedST);

                    SqlParameter detail_giaonhan = new SqlParameter("@detail_giaonhan", SqlDbType.NVarChar, 200);
                    detail_giaonhan.Value = txt_detail_giaonhan.Text;
                    cmd2.Parameters.Add(detail_giaonhan);

                    SqlParameter area_ground = new SqlParameter("@area_ground", SqlDbType.Float);
                    area_ground.Value = txt_area_ground.Text;
                    cmd2.Parameters.Add(area_ground);

                    SqlParameter area_construction = new SqlParameter("@area_construction", SqlDbType.Float);
                    area_construction.Value = txt_area_construction.Text;
                    cmd2.Parameters.Add(area_construction);

                    SqlParameter deep = new SqlParameter("@deep", SqlDbType.Float);
                    deep.Value = txt_deep.Text;
                    cmd2.Parameters.Add(deep);

                    SqlParameter request_other = new SqlParameter("@request_other", SqlDbType.NVarChar, 200);
                    request_other.Value = txt_request_other.Text;
                    cmd2.Parameters.Add(request_other);

                    SqlParameter amount = new SqlParameter("@amount", SqlDbType.Int);
                    amount.Value = txt_amount.Text;
                    cmd2.Parameters.Add(amount);

                    SqlParameter deptid_handover = new SqlParameter("@deptid_handover", SqlDbType.NVarChar, 200);
                    deptid_handover.Value = txt_deptid_handoverST.SelectedValue;
                    cmd2.Parameters.Add(deptid_handover);

                    SqlParameter deptid_handoverST = new SqlParameter("@deptid_handoverST", SqlDbType.NVarChar, 200);        
                    try
                    {
                        deptid_handoverST.Value = txt_deptid_handoverST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_handoverST);
                    }
                    catch
                    {
                        deptid_handoverST.Value = txt_deptid_handoverST.Text;
                        cmd2.Parameters.Add(deptid_handoverST);
                    }

                    SqlParameter conclusion = new SqlParameter("@conclusion", SqlDbType.NVarChar, 200);
                    conclusion.Value = txt_conclusion.Text;
                    cmd2.Parameters.Add(conclusion);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.NVarChar, 200);
                    boss_id.Value = txt_boss_idST.SelectedValue;
                    cmd2.Parameters.Add(boss_id);

                    SqlParameter survey_id = new SqlParameter("@survey_id", SqlDbType.NVarChar, 200);
                    survey_id.Value = txt_survey_idST.SelectedValue;
                    cmd2.Parameters.Add(survey_id);

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

                    SqlParameter survey_idST = new SqlParameter("@survey_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        survey_idST.Value = txt_survey_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(survey_idST);
                    }
                    catch
                    {
                        survey_idST.Value = txt_survey_idST.Text;
                        cmd2.Parameters.Add(survey_idST);
                    }

                    SqlParameter boss_id_other = new SqlParameter("@boss_id_other", SqlDbType.NVarChar, 200);
                    boss_id_other.Value = txt_boss_id_other.Text;
                    cmd2.Parameters.Add(boss_id_other);

                    SqlParameter survey_id_other = new SqlParameter("@survey_id_other", SqlDbType.NVarChar, 200);
                    survey_id_other.Value = txt_survey_id_other.Text;
                    cmd2.Parameters.Add(survey_id_other);

                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();
                        SqlCommand cmd3 = new SqlCommand("UPDATE groundDeliveryRecordsMember SET [main_id] = @main_id WHERE main_id = 0 and table_name = N'"+table_name + "'", _Cn);

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
                        ////MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                        this.Close();
                        return false;
                    }

                }
                else
                {
                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[groundDeliveryRecords]([symbol],[address],[datesST],[cecm_program_id],[cecm_program_idST],[address_cecm],[base_on_tech],[technical_regulations],[technical_regulationsST],[num_economic_contracts],[date_economic_contractsST],[organization_signed],[time_signedST],[detail_giaonhan],[area_ground],[area_construction],[deep],[request_other],[deptid_handover],[deptid_handoverST],[conclusion],[amount],[boss_id],[survey_id],[boss_idST],[survey_idST],[boss_id_other],[survey_id_other],[files])" +
                        "VALUES(@symbol,@address,@datesST,@cecm_program_id,@cecm_program_idST,@address_cecm,@base_on_tech,@technical_regulations,@technical_regulationsST,@num_economic_contracts,@date_economic_contractsST,@organization_signed,@time_signedST,@detail_giaonhan,@area_ground,@area_construction,@deep,@request_other,@deptid_handover,@deptid_handoverST,@conclusion,@amount,@boss_id,@survey_id,@boss_idST,@survey_idST,@boss_id_other,@survey_id_other,@files)", _Cn);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = comboBox_Xa.Text + "," + comboBox_Huyen.Text + "," + comboBox_Tinh.Text;
                    cmd2.Parameters.Add(address);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter cecm_program_idST = new SqlParameter("cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    base_on_tech.Value = txt_base_on_tech.Text;
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter technical_regulationsST = new SqlParameter("@technical_regulationsST", SqlDbType.NVarChar, 200);
                    technical_regulationsST.Value = richTextBox_technical_regulationsST.Text;
                    cmd2.Parameters.Add(technical_regulationsST);

                    SqlParameter num_economic_contracts = new SqlParameter("@num_economic_contracts", SqlDbType.NVarChar, 200);
                    num_economic_contracts.Value = txt_num_economic_contracts.Text;
                    cmd2.Parameters.Add(num_economic_contracts);

                    SqlParameter date_economic_contractsST = new SqlParameter("@date_economic_contractsST", SqlDbType.NVarChar, 200);
                    date_economic_contractsST.Value = time_date_economic_contractsST.Text;
                    cmd2.Parameters.Add(date_economic_contractsST);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter time_signedST = new SqlParameter("@time_signedST", SqlDbType.NVarChar, 200);
                    time_signedST.Value = time_time_signedST.Text;
                    cmd2.Parameters.Add(time_signedST);

                    SqlParameter detail_giaonhan = new SqlParameter("@detail_giaonhan", SqlDbType.NVarChar, 200);
                    detail_giaonhan.Value = txt_detail_giaonhan.Text;
                    cmd2.Parameters.Add(detail_giaonhan);

                    SqlParameter area_ground = new SqlParameter("@area_ground", SqlDbType.BigInt);
                    area_ground.Value = txt_area_ground.Text;
                    cmd2.Parameters.Add(area_ground);

                    SqlParameter area_construction = new SqlParameter("@area_construction", SqlDbType.BigInt);
                    area_construction.Value = txt_area_construction.Text;
                    cmd2.Parameters.Add(area_construction);

                    SqlParameter deep = new SqlParameter("@deep", SqlDbType.BigInt);
                    deep.Value = txt_deep.Text;
                    cmd2.Parameters.Add(deep);

                    SqlParameter request_other = new SqlParameter("@request_other", SqlDbType.NVarChar, 200);
                    request_other.Value = txt_request_other.Text;
                    cmd2.Parameters.Add(request_other);

                    SqlParameter amount = new SqlParameter("@amount", SqlDbType.Int);
                    amount.Value = txt_amount.Text;
                    cmd2.Parameters.Add(amount);

                    SqlParameter deptid_handover = new SqlParameter("@deptid_handover", SqlDbType.NVarChar, 200);
                    deptid_handover.Value = txt_deptid_handoverST.SelectedValue;
                    cmd2.Parameters.Add(deptid_handover);

                    SqlParameter deptid_handoverST = new SqlParameter("@deptid_handoverST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_handoverST.Value = txt_deptid_handoverST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_handoverST);
                    }
                    catch
                    {
                        deptid_handoverST.Value = txt_deptid_handoverST.Text;
                        cmd2.Parameters.Add(deptid_handoverST);
                    }

                    SqlParameter conclusion = new SqlParameter("@conclusion", SqlDbType.NVarChar, 200);
                    conclusion.Value = txt_conclusion.Text;
                    cmd2.Parameters.Add(conclusion);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.NVarChar, 200);
                    boss_id.Value = txt_boss_idST.SelectedValue;
                    cmd2.Parameters.Add(boss_id);

                    SqlParameter survey_id = new SqlParameter("@survey_id", SqlDbType.NVarChar, 200);
                    survey_id.Value = txt_survey_idST.SelectedValue;
                    cmd2.Parameters.Add(survey_id);

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

                    SqlParameter survey_idST = new SqlParameter("@survey_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        survey_idST.Value = txt_survey_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(survey_idST);
                    }
                    catch
                    {
                        survey_idST.Value = txt_survey_idST.Text;
                        cmd2.Parameters.Add(survey_idST);
                    }

                    SqlParameter boss_id_other = new SqlParameter("@boss_id_other", SqlDbType.NVarChar, 200);
                    boss_id_other.Value = txt_boss_id_other.Text;
                    cmd2.Parameters.Add(boss_id_other);

                    SqlParameter survey_id_other = new SqlParameter("@survey_id_other", SqlDbType.NVarChar, 200);
                    survey_id_other.Value = txt_survey_id_other.Text;
                    cmd2.Parameters.Add(survey_id_other);

                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();
                        SqlCommandBuilder sqlCommand = null;
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM groundDeliveryRecords"), _Cn);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                        System.Data.DataTable datatableWard = new System.Data.DataTable();
                        sqlAdapterWard.Fill(datatableWard);

                        foreach (DataRow dr in datatableWard.Rows)
                        {
                            var gid1 = int.Parse(dr["gid"].ToString());
                            SqlCommand cmd3 = new SqlCommand("UPDATE groundDeliveryRecordsMember SET [main_id] = @main_id WHERE main_id = 0 and table_name = N'"+table_name+"'", _Cn);

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
                        ////MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                        this.Close();
                        return false;
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
                ////MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);

                return false;
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

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember", table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ CHỦ ĐẦU TƯ";
                frm.ShowDialog();
                LoadData1(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember");
            }
            //delete column
            if (e.ColumnIndex == 5)
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
                LoadData1(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dataGridView2.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem", table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ KHẢO SÁT";
                frm.ShowDialog();
                LoadData2(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem");
            }
            //delete column
            if (e.ColumnIndex == 5)
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
                LoadData2(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem");
            }
        }
        private void LoadData1(int dem0,int dem, string dem1)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dataGridView1.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                sqlAdapter = new SqlDataAdapter(string.Format("select * from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}') or (main_id = {3} and field_name = N'{4}' and table_name = N'{5}')", dem, dem1, table_name, dem0,dem1, table_name), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        string gid = dr["gid"].ToString();
                        string string1 = "";
                        string string2 = "";
                        if (dr["long1ST"].ToString()!= "")
                        {
                            string1 = dr["long1ST"].ToString();
                            string2 = dr["long2ST"].ToString();
                        }
                        else
                        {
                            string1 = dr["string1"].ToString();
                            string2 = dr["string2"].ToString();
                        }
                        
                        if(dr["long5"].ToString() == "1")
                        {
                            var long5 = Resources.OK_16;
                            dataGridView1.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        else
                        {
                            var long5 = Resources.cancel_16;
                            dataGridView1.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        dataGridView1.Rows[indexRow - 1].Tag = gid;

                        indexRow++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                ////MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return;
            }
        }
        private void LoadData2(int dem0, int dem, string dem1)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dataGridView2.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                sqlAdapter = new SqlDataAdapter(string.Format("select * from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}') or (main_id = {3} and field_name = N'{4}' and table_name = N'{5}')", dem, dem1, table_name, dem0, dem1, table_name), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var gid = dr["gid"].ToString();
                        string string1 = "";
                        string string2 = "";
                        if (dr["long1ST"].ToString() != "")
                        {
                            string1 = dr["long1ST"].ToString();
                            string2 = dr["long2ST"].ToString();
                        }
                        else
                        {
                            string1 = dr["string1"].ToString();
                            string2 = dr["string2"].ToString();
                        }

                        if (dr["long5"].ToString() == "1")
                        {
                            var long5 = Resources.OK_16;
                            dataGridView2.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        else
                        {
                            var long5 = Resources.cancel_16;
                            dataGridView2.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        dataGridView2.Rows[indexRow - 1].Tag = gid;

                        indexRow++;
                    }
                }

            }
            catch (System.Exception ex)
            {
                ////MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return;
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
            if (txt_area_ground.Text != null && txt_area_construction.Text != null && txt_amount.Text != null && txt_deep.Text != null)
            {
                bool success = UpdateInfomation(id_BSKQ);
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin đã nhập?", "Cảnh báo");
                this.Close();
            }
            
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void buttonSave1_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember", table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ CHỦ ĐẦU TƯ";
            frm.ShowDialog();

            LoadData1(id_BSKQ,0, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember");
        }
        private void buttonSave2_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem", table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ KHẢO SÁT";
            frm.ShowDialog();

            LoadData2(id_BSKQ, 0, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem");
        }

        private void txt_deptid_handoverST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (txt_deptid_handoverST.SelectedItem != null && txt_deptid_handoverST.Text != "Chọn")
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptid_handoverST.SelectedItem.ToString().Split('-')[0]), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    deptidHandover = int.Parse(dr["dept_id_web"].ToString());
                    //txt_masterIdST.Text = dr["head"].ToString();
                }
            }
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

        private void txt_deptid_handoverST_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_deptid_handoverST, "");
                return;
            }

            if (txt_deptid_handoverST.Text == "" || txt_deptid_handoverST.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_handoverST, "Chưa chọn đơn vị");
                return;
            }

            else
            {
                e.Cancel = false;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_handoverST, "");
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
            //if (comboBox_Huyen.Text == "" || comboBox_Huyen.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    //comboBox_Huyen.Focus();
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
            //if (comboBox_Xa.Text == "" || comboBox_Xa.Text == "Chọn" )
            //{
            //    e.Cancel = true;
            //    //comboBox_Xa.Focus();
            //    errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Xa, "");
            //}
        }

        private void txt_deptid_handoverST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_deptid_handoverST.SelectedValue is long))
            {
                return;
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

        private void txt_symbol_Validating(object sender, CancelEventArgs e)
        {
            if(txt_symbol.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txt_symbol, "Chưa nhập số biên bản");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_symbol, "");
            }
        }
    }
}
