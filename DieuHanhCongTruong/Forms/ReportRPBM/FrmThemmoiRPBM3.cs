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
    public partial class FrmThemmoiRPBM3 : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        public int DuanId = 0;
        public string addressDuan = "";
        public int TinhId = 0;
        public int HuyenId = 0;
        public int XaId = 0;
        public string table_name = "TESTRECORDRESULT";
        public int deptidLoad = 0;
        public int bossId = 0;
        public int surveyId = 0;
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM3(int i)
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
            int id = 0;
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
                    id = 0;
                }
            }
            return id;
        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM3 where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        DuanId = int.Parse(dr["cecm_program_id"].ToString());
                        comboBox_TenDA.Text = dr["cecm_program_idST"].ToString();
                        addressDuan = dr["address_cecm"].ToString();
                        var adress = dr["address"].ToString().Split(',');
                        comboBox_Tinh.Text = adress[2];
                        comboBox_Huyen.Text = adress[1];
                        comboBox_Xa.Text = adress[0];

                        txt_symbol.Text = dr["symbol"].ToString();
                        bossId = int.Parse(dr["boss_id"].ToString());
                        txt_boss_idST.Text = dr["boss_idST"].ToString();
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();
                        surveyId = int.Parse(dr["survey_id"].ToString());
                        txt_survey_idST.Text = dr["survey_idST"].ToString();
                        txt_survey_id_other.Text = dr["survey_id_other"].ToString();
                        txt_deptid_loadST.Text = dr["deptid_loadST"].ToString();
                        //deptidLoad = int.Parse(dr["deptid_load"].ToString());
                        if (long.TryParse(dr["deptid_load"].ToString(), out long deptid_load))
                        {
                            txt_deptid_loadST.SelectedValue = deptid_load;
                        }

                        txt_datesST.Value = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_dates_nowST.Value = DateTime.ParseExact(dr["dates_nowST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_time_nt_fromST.Value = DateTime.ParseExact(dr["time_nt_fromST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_time_nt_toST.Value = DateTime.ParseExact(dr["time_nt_toST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_dates_qtktST.Value = DateTime.ParseExact(dr["dates_qtktST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_dates_hdktST.Value = DateTime.ParseExact(dr["dates_hdktST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        txt_base_qtkt.Text = dr["base_qtkt"].ToString();
                        txt_base_qcqg.Text = dr["base_qcqg"].ToString();
                        txt_base_hdkt_number.Text = dr["base_hdkt_number"].ToString();
                        txt_organization_signed.Text = dr["organization_signed"].ToString();
                        txt_quality_ks.Text = dr["quality_ks"].ToString();
                        txt_quymo_ks.Text = dr["quymo_ks"].ToString();
                        txt_phamvi_dtich_ks.Text = dr["phamvi_dtich_ks"].ToString();
                        txt_area_rpbm.Text = dr["area_rpbm"].ToString();
                        txt_area_tcks.Text = dr["area_tcks"].ToString();
                        txt_area_phatdon.Text = dr["area_phatdon"].ToString();
                        txt_matdo_phatdon.Text = dr["matdo_phatdon"].ToString();
                        txt_area_ks_1.Text = dr["area_ks_1"].ToString();
                        txt_signal_process.Text = dr["signal_process"].ToString();
                        txt_area_ks_2.Text = dr["area_ks_2"].ToString();
                        txt_dig_lane_signal_1.Text = dr["dig_lane_signal_1"].ToString();
                        txt_dig_lane_signal_2.Text = dr["dig_lane_signal_2"].ToString();
                        txt_result.Text = dr["result"].ToString();
                        txt_ratio_clean_ground.Text = dr["ratio_clean_ground"].ToString();
                        txt_type_forest.Text = dr["type_forest"].ToString();
                        txt_type_signal_density_1.Text = dr["type_signal_density_1"].ToString();
                        txt_avg_signal_density_1.Text = dr["avg_signal_density_1"].ToString();
                        txt_avg_signal_density_2.Text = dr["avg_signal_density_2"].ToString();
                        txt_avg_signal_density_3.Text = dr["avg_signal_density_3"].ToString();
                        txt_type_land_1.Text = dr["type_land_1"].ToString();
                        txt_type_land_2.Text = dr["type_land_2"].ToString();
                        txt_ratio_clean_water.Text = dr["ratio_clean_water"].ToString();
                        txt_type_forest_water.Text = dr["type_forest_water"].ToString();
                        txt_type_signal_density_2.Text = dr["type_signal_density_2"].ToString();
                        txt_avg_signal_density_4.Text = dr["avg_signal_density_4"].ToString();
                        txt_avg_signal_density_5.Text = dr["avg_signal_density_5"].ToString();
                        txt_avg_signal_density_6.Text = dr["avg_signal_density_6"].ToString();
                        txt_eval_water_flow.Text = dr["eval_water_flow"].ToString();
                        txt_deep_water.Text = dr["deep_water"].ToString();
                        txt_limit_area_rpbm.Text = dr["limit_area_rpbm"].ToString();
                        txt_signal_sea_1.Text = dr["signal_sea_1"].ToString();
                        txt_signal_sea_2.Text = dr["signal_sea_2"].ToString();
                        txt_deep_sea.Text = dr["deep_sea"].ToString();
                        txt_conclusion.Text = dr["conclusion"].ToString();
                        txt_num_all_report.Text = dr["num_all_report"].ToString();
                        txt_num_cdt_report.Text = dr["num_cdt_report"].ToString();
                        txt_num_ks_report.Text = dr["num_ks_report"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();
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
            LoadData1(0, id_BSKQ, "TestRecordResult_TestRecordResult_CdtMember");
            LoadData2(0, id_BSKQ, "TestRecordResult_TestRecordResult_SurMember");
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
            txt_boss_idST.Items.Clear();
            txt_survey_idST.Items.Clear();
            bossId = 0;
            surveyId = 0;
            DuanId = 0;
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
                    GetAllStaffWithIdProgram(txt_boss_idST);
                    GetAllStaffWithIdProgram(txt_survey_idST);
                    UtilsDatabase.LoadCBDept(txt_deptid_loadST, DuanId);
                    
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
                    SqlCommand cmd2 = new SqlCommand(
                        "UPDATE [dbo].[RPBM3] SET " +
                        "[deptid_load]=@deptid_load," +
                        "[deptid_loadST]=@deptid_loadST, " +
                        "[symbol] = @symbol," +
                        "[address] = @address," +
                        "[datesST] = @datesST," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[address_cecm] = @address_cecm," +
                        "[base_qtkt] = @base_qtkt," +
                        "[dates_qtktST] = @dates_qtktST," +
                        "[base_qcqg] = @base_qcqg," +
                        "[base_hdkt_number] = @base_hdkt_number," +
                        "[dates_hdktST] = @dates_hdktST," +
                        "[organization_signed] = @organization_signed," +
                        "[dates_nowST] = @dates_nowST," +
                        "[time_nt_fromST] = @time_nt_fromST," +
                        "[time_nt_toST] = @time_nt_toST," +
                        "[quality_ks] = @quality_ks," +
                        "[quymo_ks] = @quymo_ks," +
                        "[phamvi_dtich_ks] = @phamvi_dtich_ks," +
                        "[area_rpbm] = @area_rpbm," +
                        "[area_tcks] = @area_tcks," +
                        "[area_phatdon] = @area_phatdon," +
                        "[matdo_phatdon] = @matdo_phatdon," +
                        "[area_ks_1] = @area_ks_1," +
                        "[signal_process] = @signal_process," +
                        "[area_ks_2] = @area_ks_2," +
                        "[dig_lane_signal_1] = @dig_lane_signal_1," +
                        "[dig_lane_signal_2] = @dig_lane_signal_2," +
                        "[result] = @result," +
                        "[ratio_clean_ground] = @ratio_clean_ground," +
                        "[type_forest] = @type_forest," +
                        "[type_signal_density_1] = @type_signal_density_1," +
                        "[avg_signal_density_1] = @avg_signal_density_1," +
                        "[avg_signal_density_2] = @avg_signal_density_2," +
                        "[avg_signal_density_3] = @avg_signal_density_3," +
                        "[type_land_1] = @type_land_1," +
                        "[type_land_2] = @type_land_2," +
                        "[ratio_clean_water] = @ratio_clean_water," +
                        "[type_forest_water] = @type_forest_water," +
                        "[type_signal_density_2] = @type_signal_density_2," +
                        "[avg_signal_density_4] = @avg_signal_density_4," +
                        "[avg_signal_density_5] = @avg_signal_density_5," +
                        "[avg_signal_density_6] = @avg_signal_density_6," +
                        "[eval_water_flow] = @eval_water_flow," +
                        "[deep_water] = @deep_water," +
                        "[limit_area_rpbm] = @limit_area_rpbm," +
                        "[signal_sea_1] = @signal_sea_1," +
                        "[signal_sea_2] = @signal_sea_2," +
                        "[deep_sea] = @deep_sea," +
                        "[conclusion] = @conclusion," +
                        "[num_all_report] = @num_all_report," +
                        "[num_cdt_report] = @num_cdt_report," +
                        "[num_ks_report] = @num_ks_report," +
                        "[boss_id] = @boss_id," +
                        "[boss_idST] = @boss_idST," +
                        "[boss_id_other] = @boss_id_other," +
                        "[survey_id] = @survey_id," +
                        "[survey_idST] = @survey_idST," +
                        "[survey_id_other] = @survey_id_other," +
                        "files = @files " + 
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
                    address.Value = comboBox_Xa.Text + "," + comboBox_Huyen.Text + "," + comboBox_Tinh.Text;
                    cmd2.Parameters.Add(address);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = txt_dates_nowST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter base_qtkt = new SqlParameter("@base_qtkt", SqlDbType.NVarChar, 200);
                    base_qtkt.Value = txt_base_qtkt.Text;
                    cmd2.Parameters.Add(base_qtkt);

                    SqlParameter dates_qtktST = new SqlParameter("@dates_qtktST", SqlDbType.NVarChar, 200);
                    dates_qtktST.Value = txt_dates_qtktST.Text;
                    cmd2.Parameters.Add(dates_qtktST);

                    SqlParameter base_qcqg = new SqlParameter("@base_qcqg", SqlDbType.NVarChar, 200);
                    base_qcqg.Value = txt_base_qcqg.Text;
                    cmd2.Parameters.Add(base_qcqg);

                    SqlParameter base_hdkt_number = new SqlParameter("@base_hdkt_number", SqlDbType.NVarChar, 200);
                    base_hdkt_number.Value = txt_base_hdkt_number.Text;
                    cmd2.Parameters.Add(base_hdkt_number);

                    SqlParameter dates_hdktST = new SqlParameter("@dates_hdktST", SqlDbType.NVarChar, 200);
                    dates_hdktST.Value = txt_dates_hdktST.Text;
                    cmd2.Parameters.Add(dates_hdktST);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter dates_nowST = new SqlParameter("@dates_nowST", SqlDbType.NVarChar, 200);
                    dates_nowST.Value = txt_dates_nowST.Text;
                    cmd2.Parameters.Add(dates_nowST);

                    SqlParameter time_nt_fromST = new SqlParameter("@time_nt_fromST", SqlDbType.NVarChar, 200);
                    time_nt_fromST.Value = txt_time_nt_fromST.Text;
                    cmd2.Parameters.Add(time_nt_fromST);

                    SqlParameter time_nt_toST = new SqlParameter("@time_nt_toST", SqlDbType.NVarChar, 200);
                    time_nt_toST.Value = txt_time_nt_toST.Text;
                    cmd2.Parameters.Add(time_nt_toST);

                    SqlParameter quality_ks = new SqlParameter("@quality_ks", SqlDbType.NVarChar, 200);
                    quality_ks.Value = txt_quality_ks.Text;
                    cmd2.Parameters.Add(quality_ks);

                    SqlParameter quymo_ks = new SqlParameter("@quymo_ks", SqlDbType.NVarChar, 200);
                    quymo_ks.Value = txt_quymo_ks.Text;
                    cmd2.Parameters.Add(quymo_ks);

                    SqlParameter phamvi_dtich_ks = new SqlParameter("@phamvi_dtich_ks", SqlDbType.NVarChar, 200);
                    phamvi_dtich_ks.Value = txt_phamvi_dtich_ks.Text;
                    cmd2.Parameters.Add(phamvi_dtich_ks);

                    SqlParameter area_rpbm = new SqlParameter("@area_rpbm", SqlDbType.NVarChar, 200);
                    area_rpbm.Value = txt_area_rpbm.Text;
                    cmd2.Parameters.Add(area_rpbm);

                    SqlParameter area_tcks = new SqlParameter("@area_tcks", SqlDbType.NVarChar, 200);
                    area_tcks.Value = txt_area_tcks.Text;
                    cmd2.Parameters.Add(area_tcks);

                    SqlParameter area_phatdon = new SqlParameter("@area_phatdon", SqlDbType.NVarChar, 200);
                    area_phatdon.Value = txt_area_phatdon.Text;
                    cmd2.Parameters.Add(area_phatdon);

                    SqlParameter matdo_phatdon = new SqlParameter("@matdo_phatdon", SqlDbType.NVarChar, 200);
                    matdo_phatdon.Value = txt_matdo_phatdon.Text;
                    cmd2.Parameters.Add(matdo_phatdon);

                    SqlParameter area_ks_1 = new SqlParameter("@area_ks_1", SqlDbType.NVarChar, 200);
                    area_ks_1.Value = txt_area_ks_1.Text;
                    cmd2.Parameters.Add(area_ks_1);

                    SqlParameter signal_process = new SqlParameter("@signal_process", SqlDbType.NVarChar, 200);
                    signal_process.Value = txt_signal_process.Text;
                    cmd2.Parameters.Add(signal_process);

                    SqlParameter area_ks_2 = new SqlParameter("@area_ks_2", SqlDbType.NVarChar, 200);
                    area_ks_2.Value = txt_area_ks_2.Text;
                    cmd2.Parameters.Add(area_ks_2);

                    SqlParameter dig_lane_signal_1 = new SqlParameter("@dig_lane_signal_1", SqlDbType.NVarChar, 200);
                    dig_lane_signal_1.Value = txt_dig_lane_signal_1.Text;
                    cmd2.Parameters.Add(dig_lane_signal_1);

                    SqlParameter dig_lane_signal_2 = new SqlParameter("@dig_lane_signal_2", SqlDbType.NVarChar, 200);
                    dig_lane_signal_2.Value = txt_dig_lane_signal_2.Text;
                    cmd2.Parameters.Add(dig_lane_signal_2);

                    SqlParameter result = new SqlParameter("@result", SqlDbType.NVarChar, 200);
                    result.Value = txt_result.Text;
                    cmd2.Parameters.Add(result);

                    SqlParameter ratio_clean_ground = new SqlParameter("@ratio_clean_ground", SqlDbType.NVarChar, 200);
                    ratio_clean_ground.Value = txt_ratio_clean_ground.Text;
                    cmd2.Parameters.Add(ratio_clean_ground);

                    SqlParameter type_forest = new SqlParameter("@type_forest", SqlDbType.NVarChar, 200);
                    type_forest.Value = txt_type_forest.Text;
                    cmd2.Parameters.Add(type_forest);

                    SqlParameter type_signal_density_1 = new SqlParameter("@type_signal_density_1", SqlDbType.NVarChar, 200);
                    type_signal_density_1.Value = txt_type_signal_density_1.Text;
                    cmd2.Parameters.Add(type_signal_density_1);

                    SqlParameter avg_signal_density_1 = new SqlParameter("@avg_signal_density_1", SqlDbType.NVarChar, 200);
                    avg_signal_density_1.Value = txt_avg_signal_density_1.Text;
                    cmd2.Parameters.Add(avg_signal_density_1);

                    SqlParameter avg_signal_density_2 = new SqlParameter("@avg_signal_density_2", SqlDbType.NVarChar, 200);
                    avg_signal_density_2.Value = txt_avg_signal_density_2.Text;
                    cmd2.Parameters.Add(avg_signal_density_2);

                    SqlParameter avg_signal_density_3 = new SqlParameter("@avg_signal_density_3", SqlDbType.NVarChar, 200);
                    avg_signal_density_3.Value = txt_avg_signal_density_3.Text;
                    cmd2.Parameters.Add(avg_signal_density_3);

                    SqlParameter type_land_1 = new SqlParameter("@type_land_1", SqlDbType.NVarChar, 200);
                    type_land_1.Value = txt_type_land_1.Text;
                    cmd2.Parameters.Add(type_land_1);

                    SqlParameter type_land_2 = new SqlParameter("@type_land_2", SqlDbType.NVarChar, 200);
                    type_land_2.Value = txt_type_land_2.Text;
                    cmd2.Parameters.Add(type_land_2);

                    SqlParameter ratio_clean_water = new SqlParameter("@ratio_clean_water", SqlDbType.NVarChar, 200);
                    ratio_clean_water.Value = txt_ratio_clean_water.Text;
                    cmd2.Parameters.Add(ratio_clean_water);

                    SqlParameter type_forest_water = new SqlParameter("@type_forest_water", SqlDbType.NVarChar, 200);
                    type_forest_water.Value = txt_type_forest_water.Text;
                    cmd2.Parameters.Add(type_forest_water);

                    SqlParameter type_signal_density_2 = new SqlParameter("@type_signal_density_2", SqlDbType.NVarChar, 200);
                    type_signal_density_2.Value = txt_type_signal_density_2.Text;
                    cmd2.Parameters.Add(type_signal_density_2);

                    SqlParameter avg_signal_density_4 = new SqlParameter("@avg_signal_density_4", SqlDbType.NVarChar, 200);
                    avg_signal_density_4.Value = txt_avg_signal_density_4.Text;
                    cmd2.Parameters.Add(avg_signal_density_4);

                    SqlParameter avg_signal_density_5 = new SqlParameter("@avg_signal_density_5", SqlDbType.NVarChar, 200);
                    avg_signal_density_5.Value = txt_avg_signal_density_5.Text;
                    cmd2.Parameters.Add(avg_signal_density_5);

                    SqlParameter avg_signal_density_6 = new SqlParameter("@avg_signal_density_6", SqlDbType.NVarChar, 200);
                    avg_signal_density_6.Value = txt_avg_signal_density_6.Text;
                    cmd2.Parameters.Add(avg_signal_density_6);

                    SqlParameter eval_water_flow = new SqlParameter("@eval_water_flow", SqlDbType.NVarChar, 200);
                    eval_water_flow.Value = txt_eval_water_flow.Text;
                    cmd2.Parameters.Add(eval_water_flow);

                    SqlParameter deep_water = new SqlParameter("@deep_water", SqlDbType.NVarChar, 200);
                    deep_water.Value = txt_deep_water.Text;
                    cmd2.Parameters.Add(deep_water);

                    SqlParameter limit_area_rpbm = new SqlParameter("@limit_area_rpbm", SqlDbType.NVarChar, 200);
                    limit_area_rpbm.Value = txt_limit_area_rpbm.Text;
                    cmd2.Parameters.Add(limit_area_rpbm);

                    SqlParameter signal_sea_1 = new SqlParameter("@signal_sea_1", SqlDbType.NVarChar, 200);
                    signal_sea_1.Value = txt_signal_sea_1.Text;
                    cmd2.Parameters.Add(signal_sea_1);

                    SqlParameter signal_sea_2 = new SqlParameter("@signal_sea_2", SqlDbType.NVarChar, 200);
                    signal_sea_2.Value = txt_signal_sea_2.Text;
                    cmd2.Parameters.Add(signal_sea_2);

                    SqlParameter deep_sea = new SqlParameter("@deep_sea", SqlDbType.NVarChar, 200);
                    deep_sea.Value = txt_deep_sea.Text;
                    cmd2.Parameters.Add(deep_sea);

                    SqlParameter conclusion = new SqlParameter("@conclusion", SqlDbType.NVarChar, 200);
                    conclusion.Value = txt_conclusion.Text;
                    cmd2.Parameters.Add(conclusion);

                    SqlParameter num_all_report = new SqlParameter("@num_all_report", SqlDbType.NVarChar, 200);
                    num_all_report.Value = txt_num_all_report.Text;
                    cmd2.Parameters.Add(num_all_report);

                    SqlParameter num_cdt_report = new SqlParameter("@num_cdt_report", SqlDbType.NVarChar, 200);
                    num_cdt_report.Value = txt_num_cdt_report.Text;
                    cmd2.Parameters.Add(num_cdt_report);

                    SqlParameter num_ks_report = new SqlParameter("@num_ks_report", SqlDbType.NVarChar, 200);
                    num_ks_report.Value = txt_num_ks_report.Text;
                    cmd2.Parameters.Add(num_ks_report);

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

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

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
                        //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                        this.Close();
                        return false;
                    }

                }
                else
                {
                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[RPBM3]([deptid_load],[deptid_loadST],[symbol],[address],[datesST],[cecm_program_id],[cecm_program_idST],[address_cecm],[base_qtkt],[dates_qtktST],[base_qcqg],[base_hdkt_number],[dates_hdktST],[organization_signed],[dates_nowST],[time_nt_fromST],[time_nt_toST],[quality_ks],[quymo_ks],[phamvi_dtich_ks],[area_rpbm],[area_tcks],[area_phatdon],[matdo_phatdon],[area_ks_1],[signal_process],[area_ks_2],[dig_lane_signal_1],[dig_lane_signal_2],[result],[ratio_clean_ground],[type_forest],[type_signal_density_1],[avg_signal_density_1],[avg_signal_density_2],[avg_signal_density_3],[type_land_1],[type_land_2],[ratio_clean_water],[type_forest_water],[type_signal_density_2],[avg_signal_density_4],[avg_signal_density_5],[avg_signal_density_6],[eval_water_flow],[deep_water],[limit_area_rpbm],[signal_sea_1],[signal_sea_2],[deep_sea],[conclusion],[num_all_report],[num_cdt_report],[num_ks_report],[boss_id],[boss_idST],[boss_id_other],[survey_id],[survey_idST],[survey_id_other],files)" +
                        "VALUES(@deptid_load,@deptid_loadST,@symbol,@address,@datesST,@cecm_program_id,@cecm_program_idST,@address_cecm,@base_qtkt,@dates_qtktST,@base_qcqg,@base_hdkt_number,@dates_hdktST,@organization_signed,@dates_nowST,@time_nt_fromST,@time_nt_toST,@quality_ks,@quymo_ks,@phamvi_dtich_ks,@area_rpbm,@area_tcks,@area_phatdon,@matdo_phatdon,@area_ks_1,@signal_process,@area_ks_2,@dig_lane_signal_1,@dig_lane_signal_2,@result,@ratio_clean_ground,@type_forest,@type_signal_density_1,@avg_signal_density_1,@avg_signal_density_2,@avg_signal_density_3,@type_land_1,@type_land_2,@ratio_clean_water,@type_forest_water,@type_signal_density_2,@avg_signal_density_4,@avg_signal_density_5,@avg_signal_density_6,@eval_water_flow,@deep_water,@limit_area_rpbm,@signal_sea_1,@signal_sea_2,@deep_sea,@conclusion,@num_all_report,@num_cdt_report,@num_ks_report,@boss_id,@boss_idST,@boss_id_other,@survey_id,@survey_idST,@survey_id_other,@files)", _Cn);

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
                    address.Value = comboBox_Xa.Text + "," + comboBox_Huyen.Text + "," + comboBox_Tinh.Text;
                    cmd2.Parameters.Add(address);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = txt_dates_nowST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter base_qtkt = new SqlParameter("@base_qtkt", SqlDbType.NVarChar, 200);
                    base_qtkt.Value = txt_base_qtkt.Text;
                    cmd2.Parameters.Add(base_qtkt);

                    SqlParameter dates_qtktST = new SqlParameter("@dates_qtktST", SqlDbType.NVarChar, 200);
                    dates_qtktST.Value = txt_dates_qtktST.Text;
                    cmd2.Parameters.Add(dates_qtktST);

                    SqlParameter base_qcqg = new SqlParameter("@base_qcqg", SqlDbType.NVarChar, 200);
                    base_qcqg.Value = txt_base_qcqg.Text;
                    cmd2.Parameters.Add(base_qcqg);

                    SqlParameter base_hdkt_number = new SqlParameter("@base_hdkt_number", SqlDbType.NVarChar, 200);
                    base_hdkt_number.Value = txt_base_hdkt_number.Text;
                    cmd2.Parameters.Add(base_hdkt_number);

                    SqlParameter dates_hdktST = new SqlParameter("@dates_hdktST", SqlDbType.NVarChar, 200);
                    dates_hdktST.Value = txt_dates_hdktST.Text;
                    cmd2.Parameters.Add(dates_hdktST);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter dates_nowST = new SqlParameter("@dates_nowST", SqlDbType.NVarChar, 200);
                    dates_nowST.Value = txt_dates_nowST.Text;
                    cmd2.Parameters.Add(dates_nowST);

                    SqlParameter time_nt_fromST = new SqlParameter("@time_nt_fromST", SqlDbType.NVarChar, 200);
                    time_nt_fromST.Value = txt_time_nt_fromST.Text;
                    cmd2.Parameters.Add(time_nt_fromST);

                    SqlParameter time_nt_toST = new SqlParameter("@time_nt_toST", SqlDbType.NVarChar, 200);
                    time_nt_toST.Value = txt_time_nt_toST.Text;
                    cmd2.Parameters.Add(time_nt_toST);

                    SqlParameter quality_ks = new SqlParameter("@quality_ks", SqlDbType.NVarChar, 200);
                    quality_ks.Value = txt_quality_ks.Text;
                    cmd2.Parameters.Add(quality_ks);

                    SqlParameter quymo_ks = new SqlParameter("@quymo_ks", SqlDbType.NVarChar, 200);
                    quymo_ks.Value = txt_quymo_ks.Text;
                    cmd2.Parameters.Add(quymo_ks);

                    SqlParameter phamvi_dtich_ks = new SqlParameter("@phamvi_dtich_ks", SqlDbType.NVarChar, 200);
                    phamvi_dtich_ks.Value = txt_phamvi_dtich_ks.Text;
                    cmd2.Parameters.Add(phamvi_dtich_ks);

                    SqlParameter area_rpbm = new SqlParameter("@area_rpbm", SqlDbType.NVarChar, 200);
                    area_rpbm.Value = txt_area_rpbm.Text;
                    cmd2.Parameters.Add(area_rpbm);

                    SqlParameter area_tcks = new SqlParameter("@area_tcks", SqlDbType.NVarChar, 200);
                    area_tcks.Value = txt_area_tcks.Text;
                    cmd2.Parameters.Add(area_tcks);

                    SqlParameter area_phatdon = new SqlParameter("@area_phatdon", SqlDbType.NVarChar, 200);
                    area_phatdon.Value = txt_area_phatdon.Text;
                    cmd2.Parameters.Add(area_phatdon);

                    SqlParameter matdo_phatdon = new SqlParameter("@matdo_phatdon", SqlDbType.NVarChar, 200);
                    matdo_phatdon.Value = txt_matdo_phatdon.Text;
                    cmd2.Parameters.Add(matdo_phatdon);

                    SqlParameter area_ks_1 = new SqlParameter("@area_ks_1", SqlDbType.NVarChar, 200);
                    area_ks_1.Value = txt_area_ks_1.Text;
                    cmd2.Parameters.Add(area_ks_1);

                    SqlParameter signal_process = new SqlParameter("@signal_process", SqlDbType.NVarChar, 200);
                    signal_process.Value = txt_signal_process.Text;
                    cmd2.Parameters.Add(signal_process);

                    SqlParameter area_ks_2 = new SqlParameter("@area_ks_2", SqlDbType.NVarChar, 200);
                    area_ks_2.Value = txt_area_ks_2.Text;
                    cmd2.Parameters.Add(area_ks_2);

                    SqlParameter dig_lane_signal_1 = new SqlParameter("@dig_lane_signal_1", SqlDbType.NVarChar, 200);
                    dig_lane_signal_1.Value = txt_dig_lane_signal_1.Text;
                    cmd2.Parameters.Add(dig_lane_signal_1);

                    SqlParameter dig_lane_signal_2 = new SqlParameter("@dig_lane_signal_2", SqlDbType.NVarChar, 200);
                    dig_lane_signal_2.Value = txt_dig_lane_signal_2.Text;
                    cmd2.Parameters.Add(dig_lane_signal_2);

                    SqlParameter result = new SqlParameter("@result", SqlDbType.NVarChar, 200);
                    result.Value = txt_result.Text;
                    cmd2.Parameters.Add(result);

                    SqlParameter ratio_clean_ground = new SqlParameter("@ratio_clean_ground", SqlDbType.NVarChar, 200);
                    ratio_clean_ground.Value = txt_ratio_clean_ground.Text;
                    cmd2.Parameters.Add(ratio_clean_ground);

                    SqlParameter type_forest = new SqlParameter("@type_forest", SqlDbType.NVarChar, 200);
                    type_forest.Value = txt_type_forest.Text;
                    cmd2.Parameters.Add(type_forest);

                    SqlParameter type_signal_density_1 = new SqlParameter("@type_signal_density_1", SqlDbType.NVarChar, 200);
                    type_signal_density_1.Value = txt_type_signal_density_1.Text;
                    cmd2.Parameters.Add(type_signal_density_1);

                    SqlParameter avg_signal_density_1 = new SqlParameter("@avg_signal_density_1", SqlDbType.NVarChar, 200);
                    avg_signal_density_1.Value = txt_avg_signal_density_1.Text;
                    cmd2.Parameters.Add(avg_signal_density_1);

                    SqlParameter avg_signal_density_2 = new SqlParameter("@avg_signal_density_2", SqlDbType.NVarChar, 200);
                    avg_signal_density_2.Value = txt_avg_signal_density_2.Text;
                    cmd2.Parameters.Add(avg_signal_density_2);

                    SqlParameter avg_signal_density_3 = new SqlParameter("@avg_signal_density_3", SqlDbType.NVarChar, 200);
                    avg_signal_density_3.Value = txt_avg_signal_density_3.Text;
                    cmd2.Parameters.Add(avg_signal_density_3);

                    SqlParameter type_land_1 = new SqlParameter("@type_land_1", SqlDbType.NVarChar, 200);
                    type_land_1.Value = txt_type_land_1.Text;
                    cmd2.Parameters.Add(type_land_1);

                    SqlParameter type_land_2 = new SqlParameter("@type_land_2", SqlDbType.NVarChar, 200);
                    type_land_2.Value = txt_type_land_2.Text;
                    cmd2.Parameters.Add(type_land_2);

                    SqlParameter ratio_clean_water = new SqlParameter("@ratio_clean_water", SqlDbType.NVarChar, 200);
                    ratio_clean_water.Value = txt_ratio_clean_water.Text;
                    cmd2.Parameters.Add(ratio_clean_water);

                    SqlParameter type_forest_water = new SqlParameter("@type_forest_water", SqlDbType.NVarChar, 200);
                    type_forest_water.Value = txt_type_forest_water.Text;
                    cmd2.Parameters.Add(type_forest_water);

                    SqlParameter type_signal_density_2 = new SqlParameter("@type_signal_density_2", SqlDbType.NVarChar, 200);
                    type_signal_density_2.Value = txt_type_signal_density_2.Text;
                    cmd2.Parameters.Add(type_signal_density_2);

                    SqlParameter avg_signal_density_4 = new SqlParameter("@avg_signal_density_4", SqlDbType.NVarChar, 200);
                    avg_signal_density_4.Value = txt_avg_signal_density_4.Text;
                    cmd2.Parameters.Add(avg_signal_density_4);

                    SqlParameter avg_signal_density_5 = new SqlParameter("@avg_signal_density_5", SqlDbType.NVarChar, 200);
                    avg_signal_density_5.Value = txt_avg_signal_density_5.Text;
                    cmd2.Parameters.Add(avg_signal_density_5);

                    SqlParameter avg_signal_density_6 = new SqlParameter("@avg_signal_density_6", SqlDbType.NVarChar, 200);
                    avg_signal_density_6.Value = txt_avg_signal_density_6.Text;
                    cmd2.Parameters.Add(avg_signal_density_6);

                    SqlParameter eval_water_flow = new SqlParameter("@eval_water_flow", SqlDbType.NVarChar, 200);
                    eval_water_flow.Value = txt_eval_water_flow.Text;
                    cmd2.Parameters.Add(eval_water_flow);

                    SqlParameter deep_water = new SqlParameter("@deep_water", SqlDbType.NVarChar, 200);
                    deep_water.Value = txt_deep_water.Text;
                    cmd2.Parameters.Add(deep_water);

                    SqlParameter limit_area_rpbm = new SqlParameter("@limit_area_rpbm", SqlDbType.NVarChar, 200);
                    limit_area_rpbm.Value = txt_limit_area_rpbm.Text;
                    cmd2.Parameters.Add(limit_area_rpbm);

                    SqlParameter signal_sea_1 = new SqlParameter("@signal_sea_1", SqlDbType.NVarChar, 200);
                    signal_sea_1.Value = txt_signal_sea_1.Text;
                    cmd2.Parameters.Add(signal_sea_1);

                    SqlParameter signal_sea_2 = new SqlParameter("@signal_sea_2", SqlDbType.NVarChar, 200);
                    signal_sea_2.Value = txt_signal_sea_2.Text;
                    cmd2.Parameters.Add(signal_sea_2);

                    SqlParameter deep_sea = new SqlParameter("@deep_sea", SqlDbType.NVarChar, 200);
                    deep_sea.Value = txt_deep_sea.Text;
                    cmd2.Parameters.Add(deep_sea);

                    SqlParameter conclusion = new SqlParameter("@conclusion", SqlDbType.NVarChar, 200);
                    conclusion.Value = txt_conclusion.Text;
                    cmd2.Parameters.Add(conclusion);

                    SqlParameter num_all_report = new SqlParameter("@num_all_report", SqlDbType.NVarChar, 200);
                    num_all_report.Value = txt_num_all_report.Text;
                    cmd2.Parameters.Add(num_all_report);

                    SqlParameter num_cdt_report = new SqlParameter("@num_cdt_report", SqlDbType.NVarChar, 200);
                    num_cdt_report.Value = txt_num_cdt_report.Text;
                    cmd2.Parameters.Add(num_cdt_report);

                    SqlParameter num_ks_report = new SqlParameter("@num_ks_report", SqlDbType.NVarChar, 200);
                    num_ks_report.Value = txt_num_ks_report.Text;
                    cmd2.Parameters.Add(num_ks_report);

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

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();
                        SqlCommandBuilder sqlCommand = null;
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM RPBM3"), _Cn);
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
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, "TestRecordResult_TestRecordResult_CdtMember", table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ CHỦ ĐẦU TƯ";
                frm.ShowDialog();
                LoadData1(0, id_BSKQ, "TestRecordResult_TestRecordResult_CdtMember");
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
                LoadData1(0, id_BSKQ, "TestRecordResult_TestRecordResult_CdtMember");
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
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, "TestRecordResult_TestRecordResult_SurMember", table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ KHẢO SÁT";
                frm.ShowDialog();
                LoadData2(0, id_BSKQ, "TestRecordResult_TestRecordResult_SurMember");
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
                LoadData2(0, id_BSKQ, "TestRecordResult_TestRecordResult_SurMember");
            }
        }
        private void LoadData1(int dem0, int dem, string dem1)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dataGridView1.Rows.Clear();

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
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if (true)
            //{
            //    bool success = UpdateInfomation(id_BSKQ);
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng kiểm tra lại thông tin đã nhập?", "Cảnh báo");
            //    this.Close();
            //}
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

        private void buttonSave1_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, "TestRecordResult_TestRecordResult_CdtMember", table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ CHỦ ĐẦU TƯ";
            frm.ShowDialog();

            LoadData1(id_BSKQ, 0, "TestRecordResult_TestRecordResult_CdtMember");
        }
        private void buttonSave2_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, "TestRecordResult_TestRecordResult_SurMember", table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ KHẢO SÁT";
            frm.ShowDialog();

            LoadData2(id_BSKQ, 0, "TestRecordResult_TestRecordResult_SurMember");
        }

        private void txt_people1ST_SelectedIndexChanged(object sender, EventArgs e)
        {
            bossId = ChooseCBB(txt_boss_idST);
            txt_boss_id_other.ReadOnly = true;
        }

        private void txt_people2ST_SelectedIndexChanged(object sender, EventArgs e)
        {
            surveyId = ChooseCBB(txt_survey_idST);
            txt_survey_id_other.ReadOnly = true;
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

        private void txt_base_hdkt_number_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

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

        private void txt_deptid_loadST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_deptid_loadST.SelectedValue is long))
            {
                return;
            }
        }
        private void txt_symbol_Validating(object sender, CancelEventArgs e)
        {
            if (txt_symbol.Text == "")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_symbol, "Chưa nhập số báo cáo");
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
