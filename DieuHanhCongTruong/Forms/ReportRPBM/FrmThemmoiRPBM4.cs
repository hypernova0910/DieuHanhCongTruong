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
    public partial class FrmThemmoiRPBM4 : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        public int DuanId = 0;
        public string addressDuan = "";
        //public int TinhId = 0;
        //public int HuyenId = 0;
        //public int XaId = 0;
        public int SurveyId = 0;
        public int captainId = 0;
        public int deptidTcks = 0;
        public string table_name = "GROUNDDELIVERYRECORDS";
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM4(int i)
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
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM4 where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        DuanId = int.Parse(dr["cecm_program_id"].ToString());
                        //deptidTcks = int.Parse(dr["deptid_tcks"].ToString());
                        if (long.TryParse(dr["deptid_tcks"].ToString(), out long deptid_tcks))
                        {
                            txt_deptid_tcksST.SelectedValue = deptid_tcks;
                        }
                        if (long.TryParse(dr["survey_id"].ToString(), out long survey_id))
                        {
                            txt_survey_idST.SelectedValue = survey_id;
                        }
                        if (long.TryParse(dr["captain_id"].ToString(), out long captain_id))
                        {
                            txt_captain_idST.SelectedValue = deptid_tcks;
                        }
                        //SurveyId = int.Parse(dr["survey_id"].ToString());
                        //captainId = int.Parse(dr["captain_id"].ToString());
                        tbDoc_file.Text = dr["files"].ToString();

                        comboBox_TenDA.Text = dr["cecm_program_idST"].ToString();
                        //var adress = dr["address"].ToString().Split(',');
                        //comboBox_Tinh.Text = adress[2];
                        //comboBox_Huyen.Text = adress[1];
                        //comboBox_Xa.Text = adress[0];
                        comboBox_Tinh.SelectedValue = long.TryParse(dr["province_id"].ToString(), out long province_id) ? province_id : -1;
                        comboBox_Huyen.SelectedValue = long.TryParse(dr["district_id"].ToString(), out long district_id) ? district_id : -1;
                        comboBox_Xa.SelectedValue = long.TryParse(dr["commune_id"].ToString(), out long commune_id) ? commune_id : -1;
                        txt_symbol.Text = dr["symbol"].ToString();
                        addressDuan = dr["address_cecm"].ToString();
                        time_datesST.Value = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        date_dates_hdktST.Value = DateTime.ParseExact(dr["dates_hdktST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        date_time_nt_fromST.Value = DateTime.ParseExact(dr["time_nt_fromST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        date_time_nt_toST.Value = DateTime.ParseExact(dr["time_nt_toST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_thong_tu.Text = dr["thong_tu"].ToString();
                        txt_base_qckt.Text = dr["base_qckt"].ToString();
                        txt_base_hdkt_number.Text = dr["base_hdkt_number"].ToString();
                        //date_dates_hdktST.Text = dr["dates_hdktST"].ToString();
                        txt_organization_signed.Text = dr["organization_signed"].ToString();
                        txt_deep_signal.Text = dr["deep_signal"].ToString();
                        txt_num_team_construct.Text = dr["num_team_construct"].ToString();
                        txt_num_mem_all.Text = dr["num_mem_all"].ToString();
                        txt_num_mem_1.Text = dr["num_mem_1"].ToString();
                        txt_num_mem_2.Text = dr["num_mem_2"].ToString();
                        txt_num_mem_3.Text = dr["num_mem_3"].ToString();
                        txt_captain_idST.Text = dr["captain_idST"].ToString();
                        //if (txt_captain_idST.Text != "")
                        //{
                        //    txt_captain_id_other.ReadOnly = true;
                        //}
                        txt_captain_id_other.Text = dr["captain_id_other"].ToString();
                        txt_area_rpbm.Text = dr["area_rpbm"].ToString();
                        txt_area_tcks.Text = dr["area_tcks"].ToString();
                        txt_num_tcks.Text = dr["num_tcks"].ToString();
                        txt_area_ks.Text = dr["area_ks"].ToString();
                        txt_type_forest_1.Text = dr["type_forest_1"].ToString();
                        txt_area_phatdon.Text = dr["area_phatdon"].ToString();
                        txt_area_ks_1.Text = dr["area_ks_1"].ToString();
                        txt_signal_process.Text = dr["signal_process"].ToString();
                        txt_area_ks_2.Text = dr["area_ks_2"].ToString();
                        txt_dig_lane_signal_1.Text = dr["dig_lane_signal_1"].ToString();
                        txt_dig_lane_signal_2.Text = dr["dig_lane_signal_2"].ToString();
                        txt_result.Text = dr["result"].ToString();
                        txt_ratio_clean_ground.Text = dr["ratio_clean_ground"].ToString();
                        txt_type_forest_2.Text = dr["type_forest_2"].ToString();
                        txt_type_signal_density_1.Text = dr["type_signal_density_1"].ToString();
                        txt_avg_signal_density_1.Text = dr["avg_signal_density_1"].ToString();
                        txt_avg_signal_density_2.Text = dr["avg_signal_density_2"].ToString();
                        txt_avg_signal_density_3.Text = dr["avg_signal_density_3"].ToString();
                        txt_type_land_1.Text = dr["type_land_1"].ToString();
                        txt_type_land_2.Text = dr["type_land_2"].ToString();
                        txt_type_land_3.Text = dr["type_land_3"].ToString();
                        txt_type_land_4.Text = dr["type_land_4"].ToString();
                        txt_topo.Text = dr["topo"].ToString();
                        txt_climate.Text = dr["climate"].ToString();
                        txt_situation_bomb.Text = dr["situation_bomb"].ToString();
                        txt_infor_other.Text = dr["infor_other"].ToString();
                        txt_area_affect.Text = dr["area_affect"].ToString();
                        txt_conclusion.Text = dr["conclusion"].ToString();
                        txt_deptid_tcksST.Text = dr["deptid_tcksST"].ToString();
                        txt_survey_idST.Text = dr["survey_idST"].ToString();
                        txt_survey_id_other.Text = dr["survey_id_other"].ToString();
                        //if (txt_survey_idST.Text != "")
                        //{
                        //    txt_survey_id_other.ReadOnly = true;
                        //}
                        txt_population.Text = dr["population"].ToString();
                    }
                }
            }
        }
        private void comboBox_Tinh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_Tinh.SelectedValue is long)
            {
                UtilsDatabase.LoadCBHuyen(comboBox_Huyen, (long)comboBox_Tinh.SelectedValue);
            }
        }

        private void comboBox_Huyen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_Huyen.SelectedValue is long)
            {
                UtilsDatabase.LoadCBXa(comboBox_Xa, (long)comboBox_Huyen.SelectedValue);
            }
        }

        private void comboBox_TenDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //txt_survey_idST.Items.Clear();
            SurveyId = 0;
            deptidTcks = 0;
            DuanId = 0;
            addressDuan = "";
            //TimeKT.Text = null;
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
                    //GetAllStaffWithIdProgram(txt_survey_idST);
                    //GetAllStaffWithIdProgram(txt_captain_idST);

                    UtilsDatabase.LoadCBStaff(txt_survey_idST, DuanId);
                    UtilsDatabase.LoadCBStaff(txt_captain_idST, DuanId);

                    UtilsDatabase.LoadCBDept(txt_deptid_tcksST, DuanId);
                }
            }
         
        }


        private void FrmThemmoiRPBM1_Load(object sender, EventArgs e)
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
            //GroundDeliveryRecords_GroundDeliveryRecords_CDTMember
            //GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem
            //LoadData1(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_CDTMember");
            //LoadData2(0, id_BSKQ, "GroundDeliveryRecords_GroundDeliveryRecords_SurveyMem");
        }

        private void TXT_deptid_tcksST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            ////TimeBD.Text = null;
            ////TimeKT.Text = null;
            //if (txt_deptid_tcksST.SelectedItem != null && txt_deptid_tcksST.Text != "Chọn" )
            //{
            //    try
            //    {
            //        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptid_tcksST.SelectedItem.ToString().Split('-')[0]), _Cn);
            //        sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            //        System.Data.DataTable datatableWard = new System.Data.DataTable();
            //        sqlAdapterWard.Fill(datatableWard);

            //        foreach (DataRow dr in datatableWard.Rows)
            //        {
            //            deptidTcks = int.Parse(dr["dept_id_web"].ToString());
            //        }
            //    }
            //    catch
            //    {
            //        deptidTcks = 0;
            //    }
                
            //}
        }
        private bool UpdateInfomation(int dem)
        {
            try
            {
                //txt_survey_idST.Text = CheckChoose(txt_survey_idST);
                //txt_captain_idST.Text = CheckChoose(txt_captain_idST);

                //SqlCommandBuilder sqlCommand = null;
                //SqlDataAdapter sqlAdapter = null;
                //DataTable datatable = new DataTable();
                //sqlAdapter = new SqlDataAdapter(String.Format("USE [{0}] SELECT cecm_user.user_name FROM cecm_user where user_name = '{1}'", databaseName, tbTenDangNhap.Text), cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapter);
                //sqlAdapter.Fill(datatable);
                SqlCommand cmd2;
                if (dem != 0)
                {
                    cmd2 = new SqlCommand("UPDATE [dbo].[RPBM4] SET [symbol] = @symbol,[population] = @population,[address] = @address,[datesST] = @datesST,[cecm_program_id] = @cecm_program_id,[cecm_program_idST] = @cecm_program_idST,[address_cecm] = @address_cecm,[thong_tu] = @thong_tu,[base_qckt] = @base_qckt,[base_hdkt_number] = @base_hdkt_number,[dates_hdktST] = @dates_hdktST,[organization_signed] = @organization_signed,[deep_signal] = @deep_signal,[num_team_construct] = @num_team_construct,[num_mem_all] = @num_mem_all,[num_mem_1] = @num_mem_1,[num_mem_2] = @num_mem_2,[num_mem_3] = @num_mem_3,[captain_id] = @captain_id,[captain_idST] = @captain_idST,[captain_id_other] = @captain_id_other,[time_nt_fromST] = @time_nt_fromST,[time_nt_toST] = @time_nt_toST,[area_rpbm] = @area_rpbm,[area_tcks] = @area_tcks,[num_tcks] = @num_tcks,[area_ks] = @area_ks,[type_forest_1] = @type_forest_1,[area_phatdon] = @area_phatdon,[area_ks_1] = @area_ks_1,[signal_process] = @signal_process,[area_ks_2] = @area_ks_2,[dig_lane_signal_1] = @dig_lane_signal_1,[dig_lane_signal_2] = @dig_lane_signal_2,[result] = @result,[ratio_clean_ground] = @ratio_clean_ground,[type_forest_2] = @type_forest_2,[type_signal_density_1] = @type_signal_density_1,[avg_signal_density_1] = @avg_signal_density_1,[avg_signal_density_2] = @avg_signal_density_2,[avg_signal_density_3] = @avg_signal_density_3,[type_land_1] = @type_land_1,[type_land_2] = @type_land_2,[topo] = @topo,[type_land_3] = @type_land_3,[type_land_4] = @type_land_4,[climate] = @climate,[situation_bomb] = @situation_bomb,[infor_other] = @infor_other,[area_affect] = @area_affect,[deptid_tcks] = @deptid_tcks,[deptid_tcksST] = @deptid_tcksST,[conclusion] = @conclusion,[survey_id] = @survey_id,[survey_idST] = @survey_idST,[survey_id_other] = @survey_id_other,[files]=@files,province_id=@province_id,district_id=@district_id,commune_id=@commune_id  WHERE gid = " + dem, _Cn);

                    

                    

                }
                else
                {
                    // Chua co tao moi
                    cmd2 = new SqlCommand("INSERT INTO [dbo].[RPBM4]([symbol],[population],[address],[datesST],[cecm_program_id],[cecm_program_idST],[address_cecm],[thong_tu],[base_qckt],[base_hdkt_number],[dates_hdktST],[organization_signed],[deep_signal],[num_team_construct],[num_mem_all],[num_mem_1],[num_mem_2],[num_mem_3],[captain_id],[captain_idST],[captain_id_other],[time_nt_fromST],[time_nt_toST],[area_rpbm],[area_tcks],[num_tcks],[area_ks],[type_forest_1],[area_phatdon],[area_ks_1],[signal_process],[area_ks_2],[dig_lane_signal_1],[dig_lane_signal_2],[result],[ratio_clean_ground],[type_forest_2],[type_signal_density_1],[avg_signal_density_1],[avg_signal_density_2],[avg_signal_density_3],[type_land_1],[type_land_2],[topo],[type_land_3],[type_land_4],[climate],[situation_bomb],[infor_other],[area_affect],[deptid_tcks],[deptid_tcksST],[conclusion],[survey_id],[survey_idST],[survey_id_other],[files],province_id,district_id,commune_id)" +
                        "VALUES (@symbol,@population,@address,@datesST,@cecm_program_id,@cecm_program_idST,@address_cecm,@thong_tu,@base_qckt,@base_hdkt_number,@dates_hdktST,@organization_signed,@deep_signal,@num_team_construct,@num_mem_all,@num_mem_1,@num_mem_2,@num_mem_3,@captain_id,@captain_idST,@captain_id_other,@time_nt_fromST,@time_nt_toST,@area_rpbm,@area_tcks,@num_tcks,@area_ks,@type_forest_1,@area_phatdon,@area_ks_1,@signal_process,@area_ks_2,@dig_lane_signal_1,@dig_lane_signal_2,@result,@ratio_clean_ground,@type_forest_2,@type_signal_density_1,@avg_signal_density_1,@avg_signal_density_2,@avg_signal_density_3,@type_land_1,@type_land_2,@topo,@type_land_3,@type_land_4,@climate,@situation_bomb,@infor_other,@area_affect,@deptid_tcks,@deptid_tcksST,@conclusion,@survey_id,@survey_idST,@survey_id_other,@files,@province_id,@district_id,@commune_id)", _Cn);

                }
                SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                symbol.Value = txt_symbol.Text;
                cmd2.Parameters.Add(symbol);

                SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                files.Value = tbDoc_file.Text;
                cmd2.Parameters.Add(files);

                SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.BigInt);
                Duan.Value = DuanId;
                cmd2.Parameters.Add(Duan);

                SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                cmd2.Parameters.Add(address);

                SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                datesST.Value = time_datesST.Text;
                cmd2.Parameters.Add(datesST);

                SqlParameter population = new SqlParameter("@population", SqlDbType.NVarChar, 200);
                population.Value = txt_population.Text;
                cmd2.Parameters.Add(population);

                SqlParameter cecm_program_idST = new SqlParameter("cecm_program_idST", SqlDbType.NVarChar, 200);
                cecm_program_idST.Value = comboBox_TenDA.Text;
                cmd2.Parameters.Add(cecm_program_idST);

                SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                address_cecm.Value = addressDuan;
                cmd2.Parameters.Add(address_cecm);

                SqlParameter thong_tu = new SqlParameter("@thong_tu", SqlDbType.NVarChar, 200);
                thong_tu.Value = txt_thong_tu.Text;
                cmd2.Parameters.Add(thong_tu);


                SqlParameter base_qckt = new SqlParameter("@base_qckt", SqlDbType.NVarChar, 200);
                base_qckt.Value = txt_base_qckt.Text;
                cmd2.Parameters.Add(base_qckt);


                SqlParameter base_hdkt_number = new SqlParameter("@base_hdkt_number", SqlDbType.NVarChar, 200);
                base_hdkt_number.Value = txt_base_hdkt_number.Text;
                cmd2.Parameters.Add(base_hdkt_number);


                SqlParameter dates_hdktST = new SqlParameter("@dates_hdktST", SqlDbType.NVarChar, 200);
                dates_hdktST.Value = date_dates_hdktST.Text;
                cmd2.Parameters.Add(dates_hdktST);


                SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                organization_signed.Value = txt_organization_signed.Text;
                cmd2.Parameters.Add(organization_signed);


                SqlParameter deep_signal = new SqlParameter("@deep_signal", SqlDbType.NVarChar, 200);
                deep_signal.Value = txt_deep_signal.Text;
                cmd2.Parameters.Add(deep_signal);


                SqlParameter num_team_construct = new SqlParameter("@num_team_construct", SqlDbType.NVarChar, 200);
                num_team_construct.Value = txt_num_mem_all.Text;
                cmd2.Parameters.Add(num_team_construct);


                SqlParameter num_mem_all = new SqlParameter("@num_mem_all", SqlDbType.NVarChar, 200);
                num_mem_all.Value = txt_num_mem_all.Text;
                cmd2.Parameters.Add(num_mem_all);


                SqlParameter num_mem_1 = new SqlParameter("@num_mem_1", SqlDbType.NVarChar, 200);
                num_mem_1.Value = txt_num_mem_1.Text;
                cmd2.Parameters.Add(num_mem_1);


                SqlParameter num_mem_2 = new SqlParameter("@num_mem_2", SqlDbType.NVarChar, 200);
                num_mem_2.Value = txt_num_mem_2.Text;
                cmd2.Parameters.Add(num_mem_2);

                SqlParameter num_mem_3 = new SqlParameter("@num_mem_3", SqlDbType.NVarChar, 200);
                num_mem_3.Value = txt_num_mem_3.Text;
                cmd2.Parameters.Add(num_mem_3);

                SqlParameter captain_id = new SqlParameter("@captain_id", SqlDbType.BigInt);
                captain_id.Value = txt_captain_idST.SelectedValue;
                cmd2.Parameters.Add(captain_id);

                SqlParameter captain_idST = new SqlParameter("@captain_idST", SqlDbType.NVarChar, 200);
                try
                {
                    captain_idST.Value = txt_captain_idST.Text.Split('-')[1];
                    cmd2.Parameters.Add(captain_idST);
                }
                catch
                {
                    captain_idST.Value = txt_captain_idST.Text;
                    cmd2.Parameters.Add(captain_idST);
                }

                SqlParameter captain_id_other = new SqlParameter("@captain_id_other", SqlDbType.NVarChar, 200);
                captain_id_other.Value = txt_captain_id_other.Text;
                cmd2.Parameters.Add(captain_id_other);

                SqlParameter time_nt_fromST = new SqlParameter("@time_nt_fromST", SqlDbType.NVarChar, 200);
                time_nt_fromST.Value = date_time_nt_fromST.Text;
                cmd2.Parameters.Add(time_nt_fromST);

                SqlParameter time_nt_toST = new SqlParameter("@time_nt_toST", SqlDbType.NVarChar, 200);
                time_nt_toST.Value = date_time_nt_toST.Text;
                cmd2.Parameters.Add(time_nt_toST);

                SqlParameter area_rpbm = new SqlParameter("@area_rpbm", SqlDbType.NVarChar, 200);
                area_rpbm.Value = txt_area_rpbm.Text;
                cmd2.Parameters.Add(area_rpbm);

                SqlParameter area_tcks = new SqlParameter("@area_tcks", SqlDbType.NVarChar, 200);
                area_tcks.Value = txt_area_tcks.Text;
                cmd2.Parameters.Add(area_tcks);

                SqlParameter num_tcks = new SqlParameter("@num_tcks", SqlDbType.NVarChar, 200);
                num_tcks.Value = txt_num_tcks.Text;
                cmd2.Parameters.Add(num_tcks);

                SqlParameter area_ks = new SqlParameter("@area_ks", SqlDbType.NVarChar, 200);
                area_ks.Value = txt_area_ks.Text;
                cmd2.Parameters.Add(area_ks);

                SqlParameter type_forest_1 = new SqlParameter("@type_forest_1", SqlDbType.NVarChar, 200);
                type_forest_1.Value = txt_type_forest_1.Text;
                cmd2.Parameters.Add(type_forest_1);

                SqlParameter area_phatdon = new SqlParameter("@area_phatdon", SqlDbType.NVarChar, 200);
                area_phatdon.Value = txt_area_phatdon.Text;
                cmd2.Parameters.Add(area_phatdon);

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

                SqlParameter type_forest_2 = new SqlParameter("@type_forest_2", SqlDbType.NVarChar, 200);
                type_forest_2.Value = txt_type_forest_2.Text;
                cmd2.Parameters.Add(type_forest_2);

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

                SqlParameter type_land_3 = new SqlParameter("@type_land_3", SqlDbType.NVarChar, 200);
                type_land_3.Value = txt_type_land_3.Text;
                cmd2.Parameters.Add(type_land_3);

                SqlParameter type_land_4 = new SqlParameter("@type_land_4", SqlDbType.NVarChar, 200);
                type_land_4.Value = txt_type_land_4.Text;
                cmd2.Parameters.Add(type_land_4);

                SqlParameter topo = new SqlParameter("@topo", SqlDbType.NVarChar, 200);
                topo.Value = txt_topo.Text;
                cmd2.Parameters.Add(topo);

                SqlParameter climate = new SqlParameter("@climate", SqlDbType.NVarChar, 200);
                climate.Value = txt_climate.Text;
                cmd2.Parameters.Add(climate);

                SqlParameter situation_bomb = new SqlParameter("@situation_bomb", SqlDbType.NVarChar, 200);
                situation_bomb.Value = txt_situation_bomb.Text;
                cmd2.Parameters.Add(situation_bomb);

                SqlParameter infor_other = new SqlParameter("@infor_other", SqlDbType.NVarChar, 200);
                infor_other.Value = txt_infor_other.Text;
                cmd2.Parameters.Add(infor_other);

                SqlParameter area_affect = new SqlParameter("@area_affect", SqlDbType.NVarChar, 200);
                area_affect.Value = txt_area_affect.Text;
                cmd2.Parameters.Add(area_affect);

                SqlParameter conclusion = new SqlParameter("@conclusion", SqlDbType.NVarChar, 200);
                conclusion.Value = txt_conclusion.Text;
                cmd2.Parameters.Add(conclusion);

                SqlParameter deptid_tcks = new SqlParameter("@deptid_tcks", SqlDbType.BigInt);
                deptid_tcks.Value = txt_deptid_tcksST.SelectedValue;
                cmd2.Parameters.Add(deptid_tcks);

                SqlParameter deptid_tcksST = new SqlParameter("@deptid_tcksST", SqlDbType.NVarChar, 200);
                try
                {
                    deptid_tcksST.Value = txt_deptid_tcksST.Text.Split('-')[1];
                    cmd2.Parameters.Add(deptid_tcksST);
                }
                catch
                {
                    deptid_tcksST.Value = txt_deptid_tcksST.Text;
                    cmd2.Parameters.Add(deptid_tcksST);
                }

                SqlParameter survey_id = new SqlParameter("@survey_id", SqlDbType.BigInt);
                survey_id.Value = txt_survey_idST.SelectedValue;
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
                    

                    if (dem != 0)
                    {
                        SqlCommand cmd3 = new SqlCommand("UPDATE groundDeliveryRecordsMember SET [main_id] = @main_id WHERE main_id = 0 and table_name = N'" + table_name + "'", _Cn);
                        SqlParameter main_id = new SqlParameter("@main_id", SqlDbType.Int);
                        main_id.Value = dem;
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
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);

                return false;
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

        private void txt_survey_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            SurveyId =  ChooseCBB(txt_survey_idST);
            txt_survey_id_other.ReadOnly = true;
            if (txt_survey_idST.Text == "Chọn" || txt_survey_idST.Text == "")
            {
                txt_survey_id_other.ReadOnly = false;
            }
            else
            {
                txt_survey_id_other.ReadOnly = true;
                txt_survey_id_other.Text = "";
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txt_thong_tu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_captain_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            captainId = ChooseCBB(txt_captain_idST);
            if (txt_captain_idST.Text == "Chọn" || txt_captain_idST.Text == "")
            {
                txt_captain_id_other.ReadOnly = false;
            }
            else
            {
                txt_captain_id_other.ReadOnly = true;
                txt_captain_id_other.Text = "";
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

        private void txt_deptid_tcksST_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_deptid_tcksST, "");
                return;
            }
            if (txt_deptid_tcksST.Text == "" || txt_deptid_tcksST.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_tcksST, "Chưa chọn đơn vị");
                return;
            }

            else
            {
                e.Cancel = false;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_tcksST, "");
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
            //if (comboBox_Xa.Text == "" || comboBox_Xa.Text == "Chọn" )
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

        private void txt_deptid_tcksST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_deptid_tcksST.SelectedValue is long))
            {
                return;
            }
        }

        private void txt_survey_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_survey_idST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_survey_idST.SelectedValue > 0)
            {
                txt_survey_id_other.ReadOnly = true;
                txt_survey_id_other.Text = "";
            }
            else
            {
                txt_survey_id_other.ReadOnly = false;
            }
        }

        private void txt_captain_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_captain_idST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_captain_idST.SelectedValue > 0)
            {
                txt_captain_id_other.ReadOnly = true;
                txt_captain_id_other.Text = "";
            }
            else
            {
                txt_captain_id_other.ReadOnly = false;
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
