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
    public partial class FrmThemmoiRPBM12 : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        public int DuanId = 0;
        public string addressDuan = "";
        public int TinhId = 0;
        public int HuyenId = 0;
        public int XaId = 0;
        public string symbol = "0";

        public int bossId = 0;
        public int surveyId = 0;
        public int constructId = 0;
        public int commandId = 0;
        public int deptidLoad = 0;
        private bool isLuuClicked = false;

        public string table_name = "ReportConsDone";
        public string field_name1 = "ReportConsDone_ReportConsDone_Info";
        public string field_name2 = "ReportConsDone_ReportConsDone_CDTMem";
        public string field_name3 = "ReportConsDone_ReportConsDone_SurMem";
        public string field_name4 = "ReportConsDone_ReportConsDone_ConstuctMem";
        public FrmThemmoiRPBM12(int i)
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
        private string CheckChoose(ComboBox A)
        {
            if (A.Text == "Chọn" || A.Text == "")
            {
                A.Text = "";
            }
            return A.Text;
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
            if (cb.SelectedItem != null && cb.Text != "Chọn")
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
                    GetAllStaffWithIdProgram(txt_boss_idST);
                    GetAllStaffWithIdProgram(txt_survey_idST);
                    GetAllStaffWithIdProgram(txt_construct_idST);
                    //GetAllStaffWithIdProgram(txt_command_idST);
                    UtilsDatabase.LoadCBStaff(txt_command_idST, DuanId);

                    UtilsDatabase.LoadCBDept(txt_deptid_loadST, DuanId);

                    
                }
            }
        }
        private bool UpdateInfomation(int dem)
        {
            try
            {
                //txt_command_idST.Text = CheckChoose(txt_command_idST);

                if (dem != 0)
                {
                    SqlCommand cmd2 = new SqlCommand(
                        "UPDATE [dbo].[RPBM12] SET " +
                        "[deptid_load]=@deptid_load," +
                        "[deptid_loadST]=@deptid_loadST, " +
                        "[symbol] = @symbol," +
                        "[address] = @address," +
                        "[datesST] = @datesST," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[address_cecm] = @address_cecm," +
                        "[base_on_tech] = @base_on_tech," +
                        "[technical_regulations] = @technical_regulations," +
                        "[num_economic_contracts] = @num_economic_contracts," +
                        "[date_economic_contractsST] = @date_economic_contractsST," +
                        "[organization_signed] = @organization_signed," +
                        "[report_scene] = @report_scene," +
                        "[time_acceptance_startST] = @time_acceptance_startST," +
                        "[time_acceptance_endST] = @time_acceptance_endST," +
                        "[field_site] = @field_site," +
                        "[resutl_acceptance] = @resutl_acceptance," +
                        "[evaluate_opinion] = @evaluate_opinion," +
                        "[evaluate_opinion_other] = @evaluate_opinion_other," +
                        "[conclude_acceptance] = @conclude_acceptance," +
                        "[amount] = @amount," +
                        "[survey_id] = @survey_id," +
                        "[survey_idST] = @survey_idST," +
                        "[survey_id_other] = @survey_id_other," +
                        "[command_id] = @command_id," +
                        "[command_idST] = @command_idST," +
                        "[command_id_other] = @command_id_other," +
                        "[boss_id] = @boss_id,[boss_idST] = @boss_idST," +
                        "[boss_id_other] = @boss_id_other," +
                        "[construct_id] = @construct_id," +
                        "[construct_idST] = @construct_idST," +
                        "[construct_id_other] = @construct_id_other," +
                        "[files] = @files, " +
                        "[province_id]=@province_id," +
                        "[district_id]=@district_id," +
                        "[commune_id]=@commune_id " +
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

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = txt_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter date_economic_contractsST = new SqlParameter("@date_economic_contractsST", SqlDbType.NVarChar, 200);
                    date_economic_contractsST.Value = txt_date_economic_contractsST.Text;
                    cmd2.Parameters.Add(date_economic_contractsST);

                    SqlParameter time_acceptance_startST = new SqlParameter("@time_acceptance_startST", SqlDbType.NVarChar, 200);
                    time_acceptance_startST.Value = txt_time_acceptance_startST.Text;
                    cmd2.Parameters.Add(time_acceptance_startST);

                    SqlParameter time_acceptance_endST = new SqlParameter("@time_acceptance_endST", SqlDbType.NVarChar, 200);
                    time_acceptance_endST.Value = txt_time_acceptance_endST.Text;
                    cmd2.Parameters.Add(time_acceptance_endST);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    base_on_tech.Value = txt_base_on_tech.Text;
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter num_economic_contracts = new SqlParameter("@num_economic_contracts", SqlDbType.NVarChar, 200);
                    num_economic_contracts.Value = txt_num_economic_contracts.Text;
                    cmd2.Parameters.Add(num_economic_contracts);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter report_scene = new SqlParameter("@report_scene", SqlDbType.NVarChar, 200);
                    report_scene.Value = txt_report_scene.Text;
                    cmd2.Parameters.Add(report_scene);

                    SqlParameter field_site = new SqlParameter("@field_site", SqlDbType.NVarChar, 200);
                    field_site.Value = txt_field_site.Text;
                    cmd2.Parameters.Add(field_site);

                    SqlParameter resutl_acceptance = new SqlParameter("@resutl_acceptance", SqlDbType.NVarChar, 200);
                    resutl_acceptance.Value = txt_resutl_acceptance.Text;
                    cmd2.Parameters.Add(resutl_acceptance);

                    SqlParameter evaluate_opinion = new SqlParameter("@evaluate_opinion", SqlDbType.NVarChar, 200);
                    evaluate_opinion.Value = txt_evaluate_opinion.Text;
                    cmd2.Parameters.Add(evaluate_opinion);

                    SqlParameter evaluate_opinion_other = new SqlParameter("@evaluate_opinion_other", SqlDbType.NVarChar, 200);
                    evaluate_opinion_other.Value = txt_evaluate_opinion_other.Text;
                    cmd2.Parameters.Add(evaluate_opinion_other);

                    SqlParameter conclude_acceptance = new SqlParameter("@conclude_acceptance", SqlDbType.NVarChar, 200);
                    conclude_acceptance.Value = txt_conclude_acceptance.Text;
                    cmd2.Parameters.Add(conclude_acceptance);

                    SqlParameter amount = new SqlParameter("@amount", SqlDbType.NVarChar, 200);
                    amount.Value = txt_amount.Text;
                    cmd2.Parameters.Add(amount);

                    SqlParameter construct_id = new SqlParameter("@construct_id", SqlDbType.NVarChar, 200);
                    construct_id.Value = constructId;
                    cmd2.Parameters.Add(construct_id);

                    SqlParameter construct_idST = new SqlParameter("@construct_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        construct_idST.Value = txt_construct_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(construct_idST);
                    }
                    catch
                    {
                        construct_idST.Value = txt_construct_idST.Text;
                        cmd2.Parameters.Add(construct_idST);
                    }

                    SqlParameter construct_id_other = new SqlParameter("@construct_id_other", SqlDbType.NVarChar, 200);
                    construct_id_other.Value = txt_construct_id_other.Text;
                    cmd2.Parameters.Add(construct_id_other);

                    SqlParameter survey_id = new SqlParameter("@survey_id", SqlDbType.NVarChar, 200);
                    survey_id.Value = surveyId;
                    cmd2.Parameters.Add(survey_id);

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

                    SqlParameter survey_id_other = new SqlParameter("@survey_id_other", SqlDbType.NVarChar, 200);
                    survey_id_other.Value = txt_survey_id_other.Text;
                    cmd2.Parameters.Add(survey_id_other);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.NVarChar, 200);
                    boss_id.Value = bossId;
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

                    SqlParameter command_id = new SqlParameter("@command_id", SqlDbType.NVarChar, 200);
                    command_id.Value = txt_command_idST.SelectedValue;
                    cmd2.Parameters.Add(command_id);

                    SqlParameter command_idST = new SqlParameter("@command_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        command_idST.Value = txt_command_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(command_idST);
                    }
                    catch
                    {
                        command_idST.Value = txt_command_idST.Text;
                        cmd2.Parameters.Add(command_idST);
                    }

                    SqlParameter command_id_other = new SqlParameter("@command_id_other", SqlDbType.NVarChar, 200);
                    command_id_other.Value = txt_command_id_other.Text;
                    cmd2.Parameters.Add(command_id_other);

                    SqlParameter deptid_load = new SqlParameter("@deptid_load", SqlDbType.NVarChar, 200);
                    deptid_load.Value = txt_deptid_loadST.SelectedValue;
                    cmd2.Parameters.Add(deptid_load);

                    SqlParameter deptid_loadST = new SqlParameter("@deptid_loadST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_loadST.Value = txt_deptid_loadST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_loadST);
                    }
                    catch
                    {
                        deptid_loadST.Value = txt_deptid_loadST.Text;
                        cmd2.Parameters.Add(deptid_loadST);
                    }

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
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO [dbo].[RPBM12]([deptid_load],[deptid_loadST],[symbol],[address],[datesST],[cecm_program_id],[cecm_program_idST],[address_cecm],[base_on_tech],[technical_regulations],[num_economic_contracts],[date_economic_contractsST],[organization_signed],[report_scene],[time_acceptance_startST],[time_acceptance_endST],[field_site],[resutl_acceptance],[evaluate_opinion],[evaluate_opinion_other],[conclude_acceptance],[amount],[survey_id],[survey_idST],[survey_id_other],[command_id],[command_idST],[command_id_other],[boss_id],[boss_idST],[boss_id_other],[construct_id],[construct_idST],[construct_id_other],[files],province_id,district_id,commune_id)" +
                        "VALUES(@deptid_load,@deptid_loadST,@symbol,@address,@datesST,@cecm_program_id,@cecm_program_idST,@address_cecm,@base_on_tech,@technical_regulations,@num_economic_contracts,@date_economic_contractsST,@organization_signed,@report_scene,@time_acceptance_startST,@time_acceptance_endST,@field_site,@resutl_acceptance,@evaluate_opinion,@evaluate_opinion_other,@conclude_acceptance,@amount,@survey_id,@survey_idST,@survey_id_other,@command_id,@command_idST,@command_id_other,@boss_id,@boss_idST,@boss_id_other,@construct_id,@construct_idST,@construct_id_other,@files,@province_id,@district_id,@commune_id)", _Cn);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

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

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = txt_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter date_economic_contractsST = new SqlParameter("@date_economic_contractsST", SqlDbType.NVarChar, 200);
                    date_economic_contractsST.Value = txt_date_economic_contractsST.Text;
                    cmd2.Parameters.Add(date_economic_contractsST);

                    SqlParameter time_acceptance_startST = new SqlParameter("@time_acceptance_startST", SqlDbType.NVarChar, 200);
                    time_acceptance_startST.Value = txt_time_acceptance_startST.Text;
                    cmd2.Parameters.Add(time_acceptance_startST);

                    SqlParameter time_acceptance_endST = new SqlParameter("@time_acceptance_endST", SqlDbType.NVarChar, 200);
                    time_acceptance_endST.Value = txt_time_acceptance_endST.Text;
                    cmd2.Parameters.Add(time_acceptance_endST);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    base_on_tech.Value = txt_base_on_tech.Text;
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter num_economic_contracts = new SqlParameter("@num_economic_contracts", SqlDbType.NVarChar, 200);
                    num_economic_contracts.Value = txt_num_economic_contracts.Text;
                    cmd2.Parameters.Add(num_economic_contracts);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter report_scene = new SqlParameter("@report_scene", SqlDbType.NVarChar, 200);
                    report_scene.Value = txt_report_scene.Text;
                    cmd2.Parameters.Add(report_scene);

                    SqlParameter field_site = new SqlParameter("@field_site", SqlDbType.NVarChar, 200);
                    field_site.Value = txt_field_site.Text;
                    cmd2.Parameters.Add(field_site);

                    SqlParameter resutl_acceptance = new SqlParameter("@resutl_acceptance", SqlDbType.NVarChar, 200);
                    resutl_acceptance.Value = txt_resutl_acceptance.Text;
                    cmd2.Parameters.Add(resutl_acceptance);

                    SqlParameter evaluate_opinion = new SqlParameter("@evaluate_opinion", SqlDbType.NVarChar, 200);
                    evaluate_opinion.Value = txt_evaluate_opinion.Text;
                    cmd2.Parameters.Add(evaluate_opinion);

                    SqlParameter evaluate_opinion_other = new SqlParameter("@evaluate_opinion_other", SqlDbType.NVarChar, 200);
                    evaluate_opinion_other.Value = txt_evaluate_opinion_other.Text;
                    cmd2.Parameters.Add(evaluate_opinion_other);

                    SqlParameter conclude_acceptance = new SqlParameter("@conclude_acceptance", SqlDbType.NVarChar, 200);
                    conclude_acceptance.Value = txt_conclude_acceptance.Text;
                    cmd2.Parameters.Add(conclude_acceptance);

                    SqlParameter amount = new SqlParameter("@amount", SqlDbType.NVarChar, 200);
                    amount.Value = txt_amount.Text;
                    cmd2.Parameters.Add(amount);

                    SqlParameter construct_id = new SqlParameter("@construct_id", SqlDbType.NVarChar, 200);
                    construct_id.Value = constructId;
                    cmd2.Parameters.Add(construct_id);

                    SqlParameter construct_idST = new SqlParameter("@construct_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        construct_idST.Value = txt_construct_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(construct_idST);
                    }
                    catch
                    {
                        construct_idST.Value = txt_construct_idST.Text;
                        cmd2.Parameters.Add(construct_idST);
                    }

                    SqlParameter construct_id_other = new SqlParameter("@construct_id_other", SqlDbType.NVarChar, 200);
                    construct_id_other.Value = txt_construct_id_other.Text;
                    cmd2.Parameters.Add(construct_id_other);

                    SqlParameter survey_id = new SqlParameter("@survey_id", SqlDbType.NVarChar, 200);
                    survey_id.Value = surveyId;
                    cmd2.Parameters.Add(survey_id);

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

                    SqlParameter survey_id_other = new SqlParameter("@survey_id_other", SqlDbType.NVarChar, 200);
                    survey_id_other.Value = txt_survey_id_other.Text;
                    cmd2.Parameters.Add(survey_id_other);

                    SqlParameter boss_id = new SqlParameter("@boss_id", SqlDbType.NVarChar, 200);
                    boss_id.Value = bossId;
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

                    SqlParameter command_id = new SqlParameter("@command_id", SqlDbType.NVarChar, 200);
                    command_id.Value = txt_command_idST.SelectedValue;
                    cmd2.Parameters.Add(command_id);

                    SqlParameter command_idST = new SqlParameter("@command_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        command_idST.Value = txt_command_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(command_idST);
                    }
                    catch
                    {
                        command_idST.Value = txt_command_idST.Text;
                        cmd2.Parameters.Add(command_idST);
                    }

                    SqlParameter command_id_other = new SqlParameter("@command_id_other", SqlDbType.NVarChar, 200);
                    command_id_other.Value = txt_command_id_other.Text;
                    cmd2.Parameters.Add(command_id_other);

                    SqlParameter deptid_load = new SqlParameter("@deptid_load", SqlDbType.NVarChar, 200);
                    deptid_load.Value = txt_deptid_loadST.SelectedValue;
                    cmd2.Parameters.Add(deptid_load);

                    SqlParameter deptid_loadST = new SqlParameter("@deptid_loadST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_loadST.Value = txt_deptid_loadST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_loadST);
                    }
                    catch
                    {
                        deptid_loadST.Value = txt_deptid_loadST.Text;
                        cmd2.Parameters.Add(deptid_loadST);
                    }

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
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM RPBM12"), _Cn);
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
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM12 where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        txt_symbol.Text = dr["symbol"].ToString();
                        DuanId = int.Parse(dr["cecm_program_id"].ToString());
                        tbDoc_file.Text = dr["files"].ToString();

                        comboBox_TenDA.Text = dr["cecm_program_idST"].ToString();
                        addressDuan = dr["address_cecm"].ToString();
                        //var adress = dr["address"].ToString().Split(',');
                        //comboBox_Tinh.Text = adress[2];
                        //comboBox_Huyen.Text = adress[1];
                        //comboBox_Xa.Text = adress[0];
                        comboBox_Tinh.SelectedValue = long.TryParse(dr["province_id"].ToString(), out long province_id) ? province_id : -1;
                        comboBox_Huyen.SelectedValue = long.TryParse(dr["district_id"].ToString(), out long district_id) ? district_id : -1;
                        comboBox_Xa.SelectedValue = long.TryParse(dr["commune_id"].ToString(), out long commune_id) ? commune_id : -1;
                        txt_datesST.Value = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_date_economic_contractsST.Value = DateTime.ParseExact(dr["date_economic_contractsST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_time_acceptance_endST.Value = DateTime.ParseExact(dr["time_acceptance_endST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_time_acceptance_startST.Value = DateTime.ParseExact(dr["time_acceptance_startST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        //commandId = int.Parse(dr["command_id"].ToString());
                        if (long.TryParse(dr["command_id"].ToString(), out long command_id))
                        {
                            txt_command_idST.SelectedValue = command_id;
                        }
                        txt_command_idST.Text = dr["command_idST"].ToString();
                        if (txt_command_idST.Text != "")
                        {
                            txt_command_id_other.ReadOnly = true;
                        }
                        txt_command_id_other.Text = dr["command_id_other"].ToString();
                        bossId = int.Parse(dr["boss_id"].ToString());
                        txt_boss_idST.Text = dr["boss_idST"].ToString();
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();
                        constructId = int.Parse(dr["construct_id"].ToString());
                        txt_construct_idST.Text = dr["construct_idST"].ToString();
                        txt_construct_id_other.Text = dr["construct_id_other"].ToString();
                        surveyId = int.Parse(dr["survey_id"].ToString());
                        txt_survey_idST.Text = dr["survey_idST"].ToString();
                        txt_survey_id_other.Text = dr["survey_id_other"].ToString();

                        txt_base_on_tech.Text = dr["base_on_tech"].ToString();
                        txt_technical_regulations.Text = dr["technical_regulations"].ToString();
                        txt_num_economic_contracts.Text = dr["num_economic_contracts"].ToString();
                        txt_organization_signed.Text = dr["organization_signed"].ToString();
                        txt_report_scene.Text = dr["report_scene"].ToString();
                        txt_field_site.Text = dr["field_site"].ToString();
                        txt_resutl_acceptance.Text = dr["resutl_acceptance"].ToString();
                        txt_evaluate_opinion.Text = dr["evaluate_opinion"].ToString();
                        txt_evaluate_opinion_other.Text = dr["evaluate_opinion_other"].ToString();
                        txt_conclude_acceptance.Text = dr["conclude_acceptance"].ToString();
                        txt_amount.Text = dr["amount"].ToString();

                        txt_deptid_loadST.Text = dr["deptid_loadST"].ToString();
                        //deptidLoad = int.Parse(dr["deptid_load"].ToString());
                        if (long.TryParse(dr["deptid_load"].ToString(), out long deptid_load))
                        {
                            txt_deptid_loadST.SelectedValue = deptid_load;
                        }
                    }
                }
            }
        }
        private void FrmThemmoiRPBM12_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;

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
            LoadData1();
            LoadData2(0, id_BSKQ, field_name2);
            LoadData3(0, id_BSKQ, field_name3);
            LoadData4(0, id_BSKQ, field_name4);

            //GroundDeliveryRecords_GroundDeliveryRecords_CDTMember
            //GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem
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
            //if (true)
            //{
            //    bool success = UpdateInfomation(id_BSKQ);
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng kiểm tra lại thông tin đã nhập?", "Cảnh báo");
            //    this.Close();
            //}
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
                dgvCongViec.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                string sql = string.Format(
                    "select tbl.*, " +
                    "ndcv1.the_content as nd1, " +
                    "ndcv2.the_content as nd2 " +
                    "from groundDeliveryRecordsMember tbl " +
                    "left join NoiDungCongViec ndcv1 on tbl.long1 = ndcv1.id " +
                    "left join NoiDungCongViec ndcv2 on tbl.long2 = ndcv2.id " +
                    "where main_id = {0} and field_name = N'{1}' and table_name = N'{2}' ",
                    id_BSKQ, field_name1, table_name);
                sqlAdapter = new SqlDataAdapter(sql, _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var gid = dr["gid"].ToString();
                        var long1ST = dr["nd1"].ToString();
                        var long2ST = dr["nd2"].ToString();
                        var string1 = "";
                        if (string.IsNullOrEmpty(long2ST))
                        {
                            string1 = dr["string1"].ToString();
                        }
                        else
                        {
                            string1 = long2ST;
                        }
                        var string2 = dr["string2"].ToString();
                        var double1 = dr["double1"].ToString();
                        var double2 = dr["double2"].ToString();
                        var double3 = dr["double3"].ToString();
                        dgvCongViec.Rows.Add(indexRow, string1, string2, double1, double2, double3);
                        dgvCongViec.Rows[indexRow - 1].Tag = gid;

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
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return;
            }
        }
        private void LoadData3(int dem0, int dem, string dem1)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dataGridView3.Rows.Clear();

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
                            dataGridView3.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        else
                        {
                            var long5 = Resources.cancel_16;
                            dataGridView3.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        dataGridView3.Rows[indexRow - 1].Tag = gid;

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
        private void LoadData4(int dem0, int dem, string dem1)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dataGridView4.Rows.Clear();

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
                            dataGridView4.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        else
                        {
                            var long5 = Resources.cancel_16;
                            dataGridView4.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        dataGridView4.Rows[indexRow - 1].Tag = gid;

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

            var dgvRow = dgvCongViec.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == DoiRPBMSua.Index)
            {
                FormThemmoiKLTCNew frm = new FormThemmoiKLTCNew(id_kqks, id_BSKQ, field_name1, table_name);
                frm.Text = "CHỈNH SỬA CÔNG VIỆC";
                frm.ShowDialog();
                LoadData1();
            }
            //delete column
            if (e.ColumnIndex == DoiRPBMXoa.Index)
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

            var dgvRow = dataGridView2.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, field_name2, table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠM VỊ CHỦ ĐẦU TƯ";
                frm.ShowDialog();
                LoadData2(0, id_BSKQ, field_name2);
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
                LoadData2(0, id_BSKQ, field_name2);
            }
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dataGridView3.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, field_name3, table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ GIÁM SÁT";
                frm.ShowDialog();
                LoadData3(0, id_BSKQ, field_name3);
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
                LoadData3(0, id_BSKQ, field_name3);
            }
        }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dataGridView4.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, field_name4, table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ THI CÔNG";
                frm.ShowDialog();
                LoadData4(0, id_BSKQ, field_name4);
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
                LoadData4(0, id_BSKQ, field_name4);
            }
        }

        private void buttonSave1_Click(object sender, EventArgs e)
        {
            FormThemmoiKLTCNew frm = new FormThemmoiKLTCNew(0, id_BSKQ, field_name1, table_name);
            frm.Text = "THÊM MỚI CÔNG VIỆC";
            frm.ShowDialog();

            LoadData1();
        }
        private void buttonSave2_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, field_name2, table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ CHỦ ĐẦU TƯ";
            frm.ShowDialog();

            LoadData2(id_BSKQ, 0, field_name2);
        }
        private void buttonSave3_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, field_name3, table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ GIÁM SÁT";

            frm.ShowDialog();

            LoadData3(id_BSKQ, 0, field_name3);
        }
        private void buttonSave4_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, field_name4, table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ THI CÔNG";

            frm.ShowDialog();

            LoadData4(id_BSKQ, 0, field_name4);
        }

        private void txt_boss_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            bossId = ChooseCBB(txt_boss_idST);
            txt_boss_id_other.ReadOnly = true;
        }

        private void txt_survey_idST_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            surveyId = ChooseCBB(txt_survey_idST);
            txt_survey_id_other.ReadOnly = true;
        }

        private void txt_construct_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            constructId = ChooseCBB(txt_construct_idST);
            txt_construct_id_other.ReadOnly = true;
        }

        private void txt_command_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            commandId = ChooseCBB(txt_command_idST);
            if (txt_command_idST.Text == "Chọn" || txt_command_idST.Text == "")
            {
                txt_command_id_other.ReadOnly = false;
            }
            else
            {
                txt_command_id_other.ReadOnly = true;
                txt_command_id_other.Text = "";
            }
        }

        private void txt_deptid_loadST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            ////TimeBD.Text = null;
            ////TimeKT.Text = null;
            //if (txt_deptid_loadST.SelectedItem != null && txt_deptid_loadST.Text != "Chọn")
            //{
            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptid_loadST.SelectedItem.ToString().Split('-')[0]), _Cn);
            //    sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);

            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        deptidLoad = int.Parse(dr["dept_id_web"].ToString());
            //        //txt_masterIdST.Text = dr["head"].ToString();
            //    }
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

        private void txt_deptid_loadST_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_deptid_loadST, "");
                return;
            }
            if (txt_deptid_loadST.Text == "" || txt_deptid_loadST.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_loadST, "Chưa chọn đơn vị");
                return;
            }

            else
            {
                e.Cancel = false;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_loadST, "");
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
            //if (comboBox_Huyen.Text == "" || comboBox_Huyen.Text == "Chọn" )
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

        private void txt_command_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txt_command_idST.Text == "Chọn" || txt_command_idST.Text == "")
            {
                txt_command_id_other.ReadOnly = false;
            }
            else
            {
                txt_command_id_other.ReadOnly = true;
                txt_command_id_other.Text = "";
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
