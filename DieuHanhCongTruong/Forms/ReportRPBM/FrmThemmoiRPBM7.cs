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
    public partial class FrmThemmoiRPBM7 : Form
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
        public string symbol = "0";
        public string table_name = "CONSTRUCTIONDIARYGENERAL";

        public static string field_name_cv = "ConstructDiaryGeneral_ConstructDiaryGeneralInforBomb";
        public static string field_name_bmvn = "ConstructionDiaryGeneral_ConstructionDiaryGeneralInfor_BMVN";
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM7(int i)
        {
            id_BSKQ = i;
            _Cn = _Cn = frmLoggin.sqlCon;
            InitializeComponent();
        }
        private void tbCheck_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {

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
            //txt_deptid_surveyST.Text = null;
            //TimeBD.Text = null;
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

                    UtilsDatabase.LoadCBStaff(txt_boss_idST, DuanId);
                    UtilsDatabase.LoadCBStaff(txt_captain_idST, DuanId);
                    UtilsDatabase.LoadCBStaff(txt_captain_sign_idST, DuanId);
                    UtilsDatabase.LoadCBStaff(txt_cadres_idST, DuanId);

                    UtilsDatabase.LoadCBDept(txt_deptid_surveyST, DuanId);
                }
            }
        }
        private void txt_deptid_surveyST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            ////TimeBD.Text = null;
            ////TimeKT.Text = null;
            //if (txt_deptid_surveyST.SelectedItem != null && txt_deptid_surveyST.Text != "Chọn")
            //{
            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptid_surveyST.SelectedItem.ToString().Split('-')[0]), _Cn);
            //    sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);

            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        Deptid_survey = int.Parse(dr["dept_id_web"].ToString());
            //        //txt_masterIdST.Text = dr["head"].ToString();
            //    }
            //}
        }
        private bool UpdateInfomation(int dem)
        {
            try
            {
                //txt_boss_idST.Text = CheckChoose(txt_boss_idST);
                //txt_captain_idST.Text = CheckChoose(txt_captain_idST);
                //txt_captain_sign_idST.Text = CheckChoose(txt_captain_sign_idST);
                //txt_cadres_idST.Text = CheckChoose(txt_cadres_idST);
                if (dem != 0)
                {
                    SqlCommand cmd2 = new SqlCommand(
                        "UPDATE [dbo].[RPBM7] SET " +
                        "[address_cecm] = @address_cecm, " +
                        "[address] = @address," +
                        "[deptid_survey] = @deptid_survey," +
                        "[deptid_surveyST] = @deptid_surveyST," +
                        "[symbol] = @symbol," +
                        "[datesST] = @datesST," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[captain_id] = @captain_id," +
                        "[captain_idST] = @captain_idST," +
                        "[captain_id_other] = @captain_id_other," +
                        "[cadres_id] = @cadres_id," +
                        "[cadres_idST] = @cadres_idST," +
                        "[cadres_id_other] = @cadres_id_other," +
                        "[address_tc] = @address_tc," +
                        "[dates_tcST] = @dates_tcST," +
                        "[weather] = @weather," +
                        "[comment] = @comment," +
                        "[boss_id] = @boss_id," +
                        "[boss_idST] = @boss_idST," +
                        "[boss_id_other] = @boss_id_other," +
                        "[captain_sign_id] = @captain_sign_id," +
                        "[captain_sign_idST] = @captain_sign_idST," +
                        "[captain_sign_id_other] = @captain_sign_id_other," +
                        "[files]=@files, " +
                        "[province_id]=@province_id," +
                        "[district_id]=@district_id," +
                        "[commune_id]=@commune_id " +
                        "WHERE gid = " + dem, _Cn);

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

                    SqlParameter deptid_survey = new SqlParameter("@deptid_survey", SqlDbType.NVarChar, 200);
                    deptid_survey.Value = txt_deptid_surveyST.SelectedValue;
                    cmd2.Parameters.Add(deptid_survey);

                    SqlParameter deptid_surveyST = new SqlParameter("@deptid_surveyST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_surveyST.Value = txt_deptid_surveyST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_surveyST);
                    }
                    catch
                    {
                        deptid_surveyST.Value = txt_deptid_surveyST.Text;
                        cmd2.Parameters.Add(deptid_surveyST);
                    }

                    SqlParameter address_tc = new SqlParameter("@address_tc", SqlDbType.NVarChar, 200);
                    address_tc.Value = txt_address_tc.Text;
                    cmd2.Parameters.Add(address_tc);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter dates_tcST = new SqlParameter("@dates_tcST", SqlDbType.NVarChar, 200);
                    dates_tcST.Value = time_dates_tcST.Text;
                    cmd2.Parameters.Add(dates_tcST);

                    SqlParameter weather = new SqlParameter("@weather", SqlDbType.NVarChar, 200);
                    weather.Value = txt_weather.Text;
                    cmd2.Parameters.Add(weather);

                    SqlParameter comment = new SqlParameter("@comment", SqlDbType.NVarChar, 200);
                    comment.Value = txt_comment.Text;
                    cmd2.Parameters.Add(comment);

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

                    SqlParameter cadres_id = new SqlParameter("@cadres_id", SqlDbType.NVarChar, 200);
                    cadres_id.Value = txt_cadres_idST.SelectedValue;
                    cmd2.Parameters.Add(cadres_id);

                    SqlParameter cadres_idST = new SqlParameter("@cadres_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        cadres_idST.Value = txt_cadres_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(cadres_idST);
                    }
                    catch
                    {
                        cadres_idST.Value = txt_cadres_idST.Text;
                        cmd2.Parameters.Add(cadres_idST);
                    }

                    SqlParameter cadres_id_other = new SqlParameter("@cadres_id_other", SqlDbType.NVarChar, 200);
                    cadres_id_other.Value = txt_cadres_id_other.Text;
                    cmd2.Parameters.Add(cadres_id_other);

                    SqlParameter captain_id = new SqlParameter("@captain_id", SqlDbType.NVarChar, 200);
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

                    SqlParameter captain_sign_id = new SqlParameter("@captain_sign_id", SqlDbType.NVarChar, 200);
                    captain_sign_id.Value = txt_captain_sign_idST.SelectedValue;
                    cmd2.Parameters.Add(captain_sign_id);

                    SqlParameter captain_sign_idST = new SqlParameter("@captain_sign_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        captain_sign_idST.Value = txt_captain_sign_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(captain_sign_idST);
                    }
                    catch
                    {
                        captain_sign_idST.Value = txt_captain_sign_idST.Text;
                        cmd2.Parameters.Add(captain_sign_idST);
                    }

                    SqlParameter captain_sign_id_other = new SqlParameter("@captain_sign_id_other", SqlDbType.NVarChar, 200);
                    captain_sign_id_other.Value = txt_captain_sign_id_other.Text;
                    cmd2.Parameters.Add(captain_sign_id_other);

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
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[RPBM7]([address_cecm],[address],[deptid_survey],[deptid_surveyST],[symbol],[datesST],[cecm_program_id],[cecm_program_idST],[captain_id],[captain_idST],[captain_id_other],[cadres_id],[cadres_idST],[cadres_id_other],[address_tc],[dates_tcST],[weather],[comment],[boss_id],[boss_idST],[boss_id_other],[captain_sign_id],[captain_sign_idST],[captain_sign_id_other],[files],province_id, district_id,commune_id)" +
                        "VALUES(@address_cecm,@address,@deptid_survey,@deptid_surveyST,@symbol,@datesST,@cecm_program_id,@cecm_program_idST,@captain_id,@captain_idST,@captain_id_other,@cadres_id,@cadres_idST,@cadres_id_other,@address_tc,@dates_tcST,@weather,@comment,@boss_id,@boss_idST,@boss_id_other,@captain_sign_id,@captain_sign_idST,@captain_sign_id_other,@files,@province_id, @district_id,@commune_id)", _Cn);

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

                    SqlParameter deptid_survey = new SqlParameter("@deptid_survey", SqlDbType.NVarChar, 200);
                    deptid_survey.Value = txt_deptid_surveyST.SelectedValue;
                    cmd2.Parameters.Add(deptid_survey);

                    SqlParameter deptid_surveyST = new SqlParameter("@deptid_surveyST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_surveyST.Value = txt_deptid_surveyST.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_surveyST);
                    }
                    catch
                    {
                        deptid_surveyST.Value = txt_deptid_surveyST.Text;
                        cmd2.Parameters.Add(deptid_surveyST);
                    }

                    SqlParameter address_tc = new SqlParameter("@address_tc", SqlDbType.NVarChar, 200);
                    address_tc.Value = txt_address_tc.Text;
                    cmd2.Parameters.Add(address_tc);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter dates_tcST = new SqlParameter("@dates_tcST", SqlDbType.NVarChar, 200);
                    dates_tcST.Value = time_dates_tcST.Text;
                    cmd2.Parameters.Add(dates_tcST);

                    SqlParameter weather = new SqlParameter("@weather", SqlDbType.NVarChar, 200);
                    weather.Value = txt_weather.Text;
                    cmd2.Parameters.Add(weather);

                    SqlParameter comment = new SqlParameter("@comment", SqlDbType.NVarChar, 200);
                    comment.Value = txt_comment.Text;
                    cmd2.Parameters.Add(comment);

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

                    SqlParameter cadres_id = new SqlParameter("@cadres_id", SqlDbType.NVarChar, 200);
                    cadres_id.Value = txt_cadres_idST.SelectedValue;
                    cmd2.Parameters.Add(cadres_id);

                    SqlParameter cadres_idST = new SqlParameter("@cadres_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        cadres_idST.Value = txt_cadres_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(cadres_idST);
                    }
                    catch
                    {
                        cadres_idST.Value = txt_cadres_idST.Text;
                        cmd2.Parameters.Add(cadres_idST);
                    }

                    SqlParameter cadres_id_other = new SqlParameter("@cadres_id_other", SqlDbType.NVarChar, 200);
                    cadres_id_other.Value = txt_cadres_id_other.Text;
                    cmd2.Parameters.Add(cadres_id_other);

                    SqlParameter captain_id = new SqlParameter("@captain_id", SqlDbType.NVarChar, 200);
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

                    SqlParameter captain_sign_id = new SqlParameter("@captain_sign_id", SqlDbType.NVarChar, 200);
                    captain_sign_id.Value = txt_captain_sign_idST.SelectedValue;
                    cmd2.Parameters.Add(captain_sign_id);

                    SqlParameter captain_sign_idST = new SqlParameter("@captain_sign_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        captain_sign_idST.Value = txt_captain_sign_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(captain_sign_idST);
                    }
                    catch
                    {
                        captain_sign_idST.Value = txt_captain_sign_idST.Text;
                        cmd2.Parameters.Add(captain_sign_idST);
                    }

                    SqlParameter captain_sign_id_other = new SqlParameter("@captain_sign_id_other", SqlDbType.NVarChar, 200);
                    captain_sign_id_other.Value = txt_captain_sign_id_other.Text;
                    cmd2.Parameters.Add(captain_sign_id_other);

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
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM RPBM7"), _Cn);
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
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM7 where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        DuanId = int.Parse(dr["cecm_program_id"].ToString());
                        comboBox_TenDA.Text = dr["cecm_program_idST"].ToString();
                        txt_symbol.Text = dr["symbol"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();

                        //Deptid_survey = int.Parse(dr["deptid_survey"].ToString());
                        if (long.TryParse(dr["deptid_survey"].ToString(), out long deptid_survey))
                        {
                            txt_deptid_surveyST.SelectedValue = deptid_survey;
                        }
                        txt_deptid_surveyST.Text = dr["deptid_surveyST"].ToString();

                        //Boss_id = int.Parse(dr["boss_id"].ToString());
                        if (long.TryParse(dr["boss_id"].ToString(), out long boss_id))
                        {
                            txt_boss_idST.SelectedValue = boss_id;
                        }
                        txt_boss_idST.Text = dr["boss_idST"].ToString();
                        if (txt_boss_idST.Text != "")
                        {
                            txt_boss_id_other.ReadOnly = true;
                        }
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();

                        //Cadres_id = int.Parse(dr["cadres_id"].ToString());
                        if (long.TryParse(dr["cadres_id"].ToString(), out long cadres_id))
                        {
                            txt_cadres_idST.SelectedValue = cadres_id;
                        }
                        txt_cadres_idST.Text = dr["cadres_idST"].ToString();
                        if (cadres_id > 0)
                        {
                            txt_cadres_id_other.ReadOnly = true;
                        }
                        txt_cadres_id_other.Text = dr["cadres_id_other"].ToString();

                        //Captain_id = int.Parse(dr["captain_id"].ToString());
                        if (long.TryParse(dr["captain_id"].ToString(), out long captain_id))
                        {
                            txt_captain_idST.SelectedValue = captain_id;
                        }
                        txt_captain_idST.Text = dr["captain_idST"].ToString();
                        if (captain_id > 0)
                        {
                            txt_captain_id_other.ReadOnly = true;
                        }
                        txt_captain_id_other.Text = dr["captain_id_other"].ToString();

                        //Captain_sign_id = int.Parse(dr["captain_sign_id"].ToString());
                        if (long.TryParse(dr["captain_sign_id"].ToString(), out long captain_sign_id))
                        {
                            txt_captain_sign_idST.SelectedValue = captain_sign_id;
                        }
                        txt_captain_sign_idST.Text = dr["captain_sign_idST"].ToString();
                        if (captain_sign_id > 0)
                        {
                            txt_captain_sign_id_other.ReadOnly = true;
                        }
                        txt_captain_sign_id_other.Text = dr["captain_sign_id_other"].ToString();

                        symbol = dr["symbol"].ToString();

                        //var adress = dr["address"].ToString().Split(',');
                        //comboBox_Tinh.Text = adress[2];
                        //comboBox_Huyen.Text = adress[1];
                        //comboBox_Xa.Text = adress[0];
                        comboBox_Tinh.SelectedValue = long.TryParse(dr["province_id"].ToString(), out long province_id) ? province_id : -1;
                        comboBox_Huyen.SelectedValue = long.TryParse(dr["district_id"].ToString(), out long district_id) ? district_id : -1;
                        comboBox_Xa.SelectedValue = long.TryParse(dr["commune_id"].ToString(), out long commune_id) ? commune_id : -1;
                        addressDuan = dr["address_cecm"].ToString();

                        time_datesST.Value = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_dates_tcST.Value = DateTime.ParseExact(dr["dates_tcST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        txt_address_tc.Text = dr["address_tc"].ToString();
                        txt_weather.Text = dr["weather"].ToString();
                        txt_comment.Text = dr["comment"].ToString();
                    }
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
            LoadData1();
            LoadDataBMVN();
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
                    id_BSKQ, field_name_cv, table_name);
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
                        var string3 = dr["string3"].ToString();
                        var string4 = dr["string4"].ToString();
                        dgvCongViec.Rows.Add(indexRow, long1ST, string1, string2, string3, string4, Resources.Modify, Resources.DeleteRed);
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

        private void buttonSave1_Click(object sender, EventArgs e)
        {
            FormThemmoiInforBomb frm = new FormThemmoiInforBomb(0, id_BSKQ, "ConstructDiaryGeneral_ConstructDiaryGeneralInforBomb", table_name);
            frm.Text = "THÊM MỚI CÔNG VIỆC";
            frm.ShowDialog();

            LoadData1();
        }

        private void txt_captain_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Captain_id = ChooseCBB(txt_captain_idST);
            //if (txt_captain_idST.Text == "Chọn" || txt_captain_idST.Text == "")
            //{
            //    txt_captain_id_other.ReadOnly = false;
            //}
            //else
            //{
            //    txt_captain_id_other.ReadOnly = true;
            //    txt_captain_id_other.Text = "";
            //}
        }

        private void txt_boss_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Boss_id = ChooseCBB(txt_boss_idST);
            //txt_boss_id_other.ReadOnly = true;
            //if (txt_boss_idST.Text == "Chọn" || txt_boss_idST.Text == "")
            //{
            //    txt_boss_id_other.ReadOnly = false;
            //}
            //else
            //{
            //    txt_boss_id_other.ReadOnly = true;
            //    txt_boss_id_other.Text = "";

            //}
        }

        private void txt_captain_sign_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Captain_sign_id = ChooseCBB(txt_captain_sign_idST);
            //txt_captain_sign_id_other.ReadOnly = true;
            //if (txt_captain_sign_idST.Text == "Chọn" || txt_captain_sign_idST.Text == "")
            //{
            //    txt_captain_sign_id_other.ReadOnly = false;
            //}
            //else
            //{
            //    txt_captain_sign_id_other.ReadOnly = true;
            //    txt_captain_sign_id_other.Text = "";

            //}
        }

        private void txt_cadres_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cadres_id = ChooseCBB(txt_cadres_idST);
            //txt_cadres_id_other.ReadOnly = true;
            //if (txt_cadres_idST.Text == "Chọn" || txt_cadres_idST.Text == "")
            //{
            //    txt_cadres_id_other.ReadOnly = false;
            //}
            //else
            //{
            //    txt_cadres_id_other.ReadOnly = true;
            //    txt_cadres_id_other.Text = "";

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

        private void txt_deptid_surveyST_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_deptid_surveyST, "");
                return;
            }
            if (txt_deptid_surveyST.Text == "" || txt_deptid_surveyST.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_surveyST, "Chưa chọn đơn vị");
                return;
            }

            else
            {
                e.Cancel = false;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_surveyST, "");
            }
        }

        private void comboBox_Tinh_Validating(object sender, CancelEventArgs e)
        {
            //if (comboBox_Tinh.Text == "" || comboBox_Tinh.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    comboBox_Tinh.Focus();
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
            //    comboBox_Tinh.Focus();
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
            //    comboBox_Tinh.Focus();
            //    errorProvider1.SetError(comboBox_Xa, "Chưa chọn xã");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(comboBox_Xa, "");
            //}
        }

        private void txt_cadres_idST_Validating(object sender, CancelEventArgs e)
        {
            //ComboBox A = txt_cadres_idST;
            //string strError = "Chưa chọn cán bộ";
            //if (A.Text == "" || A.Text == "Chọn")
            //{
            //    e.Cancel = true;
            //    //comboBox_TenDA.Focus();
            //    errorProvider1.SetError(A, strError);
            //    return;
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(A, "");
            //}
        }

        private void txt_boss_idST_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txt_captain_sign_idST_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txt_deptid_surveyST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_deptid_surveyST.SelectedValue is long))
            {
                return;
            }
        }

        private void txt_cadres_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            //Captain_sign_id = ChooseCBB(txt_captain_sign_idST);
            txt_cadres_id_other.ReadOnly = true;
            if (txt_cadres_idST.Text == "Chọn" || txt_cadres_idST.Text == "")
            {
                txt_cadres_id_other.ReadOnly = false;
            }
            else
            {
                txt_cadres_id_other.ReadOnly = true;
                txt_cadres_id_other.Text = "";

            }
        }

        private void txt_captain_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            txt_captain_id_other.ReadOnly = true;
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

        private void txt_boss_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            txt_boss_id_other.ReadOnly = true;
            if (txt_boss_idST.Text == "Chọn" || txt_boss_idST.Text == "")
            {
                txt_boss_id_other.ReadOnly = false;
            }
            else
            {
                txt_boss_id_other.ReadOnly = true;
                txt_boss_id_other.Text = "";

            }
        }

        private void txt_captain_sign_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            txt_captain_sign_id_other.ReadOnly = true;
            if (txt_captain_sign_idST.Text == "Chọn" || txt_captain_sign_idST.Text == "")
            {
                txt_captain_sign_id_other.ReadOnly = false;
            }
            else
            {
                txt_captain_sign_id_other.ReadOnly = true;
                txt_captain_sign_id_other.Text = "";

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

        private void buttonSaveCongViec_Click(object sender, EventArgs e)
        {
            FormThemmoiInforBomb frm = new FormThemmoiInforBomb(0, id_BSKQ, field_name_cv, table_name);
            frm.Text = "THÊM MỚI CHI TIẾT CÔNG VIỆC";
            frm.ShowDialog();

            LoadData1();
        }

        private void dgvCongViec_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                FormThemmoiInforBomb frm = new FormThemmoiInforBomb(id_kqks, id_BSKQ, "ConstructDiaryGeneral_ConstructDiaryGeneralInforBomb", table_name);
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

        private void btnSaveBMVN_Click(object sender, EventArgs e)
        {
            FormThemmoiKetQuaBMVN frm = new FormThemmoiKetQuaBMVN(0, id_BSKQ, field_name_bmvn, table_name);
            frm.Text = "THÊM MỚI SỐ LƯỢNG, CHỦNG LOẠI BOM ĐẠN, VẬT NỔ ĐÃ HỦY (XỬ LÝ)";
            frm.ShowDialog();

            LoadDataBMVN();
        }

        private void dgVBMVN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgVBMVN.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == dgvBMVN_cotSua.Index)
            {
                FormThemmoiKetQuaBMVN frm = new FormThemmoiKetQuaBMVN(id_kqks, id_BSKQ, field_name_bmvn, table_name);
                frm.Text = "CHỈNH SỬA SỐ LƯỢNG, CHỦNG LOẠI BOM ĐẠN, VẬT NỔ ĐÃ HỦY (XỬ LÝ)";
                frm.ShowDialog();
                //LoadData1(id_BSKQ, "ConstructionDiaryBomb_ConstructionDiaryInforBomb_BMVN");
                LoadDataBMVN();
            }
            //delete column
            if (e.ColumnIndex == dgvBMVN_cotXoa.Index)
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
                LoadDataBMVN();
            }
        }

        private void LoadDataBMVN()
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dgVBMVN.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                string sql = string.Format(
                    "select " +
                    "tbl.gid, " +
                    "tbl.string1, " +
                    "tbl.string2, " +
                    "tbl.string3, " +
                    "tbl.string4, " +
                    "mst.dvs_name as LoaiBMVN, " +
                    "tbl.long2 " +
                    "from groundDeliveryRecordsMember tbl " +
                    "left join mst_division mst on tbl.long1 = mst.dvs_value and mst.dvs_group_cd = '001' " +
                    "where " +
                    "(main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", id_BSKQ, field_name_bmvn, table_name);
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
                        if (string.IsNullOrEmpty(LoaiBMVN))
                        {
                            string1 = dr["string1"].ToString();
                        }
                        else
                        {
                            string1 = LoaiBMVN;
                        }
                        var string2 = dr["string2"].ToString();
                        var string3 = dr["string3"].ToString();
                        var string4 = dr["string4"].ToString();
                        long.TryParse(dr["long2"].ToString(), out long long2);
                        dgVBMVN.Rows.Add(indexRow, string1, string2, string3, long2, string4, Resources.Modify, Resources.DeleteRed);
                        dgVBMVN.Rows[indexRow - 1].Tag = gid;

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
