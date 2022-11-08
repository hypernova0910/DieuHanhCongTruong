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
    public partial class FrmThemmoiRPBM2 : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string idMucdo = "";
        public int DuanId = 0;
        public int OId = 0;
        public int SurveyId = 0;
        public string addressDuan = "";
        public int TinhId = 0;
        public int HuyenId = 0;
        public int XaId = 0;
        public string dateST_cecm = "";
        public string table_name = "CONSTRUCTIONDIARYBOMB";
        public int people1_id = 0;
        public int people2_id = 0;
        public int boss_id = 0;
        public int master_id = 0;
        private bool isLuuClicked = false;

        public static string field_name_cv = "ConstructionDiaryBomb_ConstructionDiaryInforBomb";
        public static string field_name_bmvn = "ConstructionDiaryBomb_ConstructionDiaryInforBomb_BMVN";
        public FrmThemmoiRPBM2(int i)
        {
            id_BSKQ = i;
            _Cn = _Cn = frmLoggin.sqlCon;
            InitializeComponent();
        }
        private string CheckChoose(ComboBox A)
        {
            if(A.Text == "Chọn" || A.Text == "")
            {
                A.Text = "";
            }
            return A.Text;
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

        private void comboBox_TenDA_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            txt_boxIdST.Text = null;
            txt_surveyIdST.Text = null;
            txt_people1ST.Text = null;
            txt_people2ST.Text = null;
            txt_masterIdST.Text = null;
            txt_bossIdST.Text = null;
            SurveyId = 0;
            boss_id = 0;
            people1_id = 0;
            people2_id = 0;
            DuanId = 0;
            addressDuan = "";
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if(!(comboBox_TenDA.SelectedValue is long))
            {
                return;
            }
            //if ((long)comboBox_TenDA.SelectedValue <= 0)
            //{
            //    return;
            //}
            //if (comboBox_TenDA.SelectedItem != null && comboBox_TenDA.Text != "Chọn")
            //{
                
            SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM cecm_programData where id = {0}", comboBox_TenDA.SelectedValue), _Cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            System.Data.DataTable datatableWard = new System.Data.DataTable();
            sqlAdapterWard.Fill(datatableWard);

            foreach (DataRow dr in datatableWard.Rows)
            {
                DuanId = int.Parse(dr["id"].ToString());
                addressDuan = dr["address"].ToString();

                    

            }
            UtilsDatabase.LoadCBStaff(txt_people1ST, DuanId);
            UtilsDatabase.LoadCBStaff(txt_people2ST, DuanId);
            UtilsDatabase.LoadCBStaff(txt_bossIdST, DuanId);
            UtilsDatabase.LoadCBStaff(txt_masterIdST, DuanId);

            UtilsDatabase.LoadCBDept(txt_surveyIdST, DuanId);

            SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("select o.gid,o.o_id from OLuoi as o left join cecm_program_area_map as program2 on program2.id = o.cecm_program_areamap_id left join cecm_programData as program1 on program2.cecm_program_id = program1.id where program1.id = {0}", DuanId), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
                System.Data.DataTable datatableCounty = new System.Data.DataTable();
                sqlAdapterCounty.Fill(datatableCounty);
                txt_boxIdST.Items.Clear();
                txt_boxIdST.Items.Add("Chọn");
                foreach (DataRow dr in datatableCounty.Rows)
                {
                    if (string.IsNullOrEmpty(dr["o_id"].ToString()))
                        continue;

                    txt_boxIdST.Items.Add(dr["o_id"].ToString());
                }
            //}
        }
        private void txt_boxIdST_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (txt_boxIdST.SelectedItem != null && txt_boxIdST.Text != "Chọn")
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM OLuoi where o_id = N'{0}'", txt_boxIdST.SelectedItem.ToString()), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    OId = int.Parse(dr["gid"].ToString());
                }
            }
        }
        private void txt_surveyIdST_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    id = 0;
                }
            }
            return id;
        }
        private bool UpdateInfomation(int dem)
        {
            try
            {
                //txt_boxIdST.Text = CheckChoose(txt_boxIdST);
                //txt_surveyIdST.Text = CheckChoose(txt_surveyIdST);
                //txt_people1ST.Text = CheckChoose(txt_people1ST);
                //txt_people2ST.Text = CheckChoose(txt_people2ST);
                //txt_masterIdST.Text = CheckChoose(txt_masterIdST);
                //txt_bossIdST.Text = CheckChoose(txt_bossIdST);
                if (dem != 0)
                {
                    SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[excutionSurveyLandmines] set [symbol]=@symbol, [cecm_program_id]=@cecm_program_id,[cecm_program_idST]=@cecm_program_idST,[address]=@address,[address_cecm]=@address_cecm,[surveyId]=@surveyId,[surveyIdST]=@surveyIdST,[masterId]=@masterId,[masterIdST]=@masterIdST,[master_other]=@master_other,[bossId]=@bossId,[bossIdST]=@bossIdST,[boss_other]=@boss_other,[boxId]=@boxId,[boxIdST]=@boxIdST,[dates_tcST]=@dates_tcST,[datesST]=@datesST,[weather]=@weather,[descript]=@descript,[people1ST]=@people1ST,[people2ST]=@people2ST,[people1Id]=@people1Id,[people2Id]=@people2Id,[people1_other]=@people1_other,[people2_other]=@people2_other,[files]=@files WHERE gid = " + dem, _Cn);

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

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = comboBox_Xa.Text + "," + comboBox_Huyen.Text + "," + comboBox_Tinh.Text;
                    cmd2.Parameters.Add(address);

                    SqlParameter surveyId = new SqlParameter("@surveyId", SqlDbType.BigInt);
                    surveyId.Value = txt_surveyIdST.SelectedValue;
                    cmd2.Parameters.Add(surveyId);

                    SqlParameter surveyIdST = new SqlParameter("@surveyIdST", SqlDbType.NVarChar, 200);
                    try
                    {
                        surveyIdST.Value = txt_surveyIdST.Text.Split('-')[1];
                        cmd2.Parameters.Add(surveyIdST);
                    }
                    catch
                    {
                        surveyIdST.Value = txt_surveyIdST.Text;
                        cmd2.Parameters.Add(surveyIdST);
                    }

                    SqlParameter masterId = new SqlParameter("@masterId", SqlDbType.BigInt);
                    masterId.Value = txt_masterIdST.SelectedValue != null ? txt_masterIdST.SelectedValue : -1;
                    cmd2.Parameters.Add(masterId);

                    SqlParameter masterIdST = new SqlParameter("@masterIdST", SqlDbType.NVarChar, 200);
                    try
                    {
                        masterIdST.Value = txt_masterIdST.Text.Split('-')[1];
                        cmd2.Parameters.Add(masterIdST);
                    }
                    catch
                    {
                        masterIdST.Value = txt_masterIdST.Text;
                        cmd2.Parameters.Add(masterIdST);
                    }

                    SqlParameter master_other = new SqlParameter("@master_other", SqlDbType.NVarChar, 200);
                    master_other.Value = txt_master_other.Text;
                    cmd2.Parameters.Add(master_other);

                    SqlParameter bossId = new SqlParameter("@bossId", SqlDbType.BigInt);
                    bossId.Value = txt_bossIdST.SelectedValue != null ? txt_bossIdST.SelectedValue : -1;
                    cmd2.Parameters.Add(bossId);

                    SqlParameter bossIdST = new SqlParameter("@bossIdST", SqlDbType.NVarChar, 200);
                    try
                    {
                        bossIdST.Value = txt_bossIdST.Text.Split('-')[1];
                        cmd2.Parameters.Add(bossIdST);
                    }
                    catch
                    {
                        bossIdST.Value = txt_bossIdST.Text;
                        cmd2.Parameters.Add(bossIdST);
                    }

                    SqlParameter boss_other = new SqlParameter("@boss_other", SqlDbType.NVarChar, 200);
                    boss_other.Value = txt_boss_other.Text;
                    cmd2.Parameters.Add(boss_other);

                    SqlParameter boxId = new SqlParameter("@boxId", SqlDbType.BigInt);
                    boxId.Value = OId;
                    cmd2.Parameters.Add(boxId);

                    SqlParameter boxIdST = new SqlParameter("@boxIdST", SqlDbType.NVarChar, 200);
                    boxIdST.Value = txt_boxIdST.Text;
                    cmd2.Parameters.Add(boxIdST);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);
                    
                    SqlParameter dates_tcST = new SqlParameter("@dates_tcST", SqlDbType.NVarChar, 200);
                    dates_tcST.Value = time_dates_tcST.Text;
                    cmd2.Parameters.Add(dates_tcST);

                    SqlParameter weather = new SqlParameter("@weather", SqlDbType.NVarChar, 200);
                    weather.Value = txt_weather.Text;
                    cmd2.Parameters.Add(weather);

                    SqlParameter descript = new SqlParameter("@descript", SqlDbType.NVarChar, 200);
                    descript.Value = txt_descript.Text;
                    cmd2.Parameters.Add(descript);

                    SqlParameter people1ST = new SqlParameter("@people1ST", SqlDbType.NVarChar, 200);
                    try
                    {
                        people1ST.Value = txt_people1ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(people1ST);
                    }
                    catch
                    {
                        people1ST.Value = txt_people1ST.Text;
                        cmd2.Parameters.Add(people1ST);
                    }

                    SqlParameter people2ST = new SqlParameter("@people2ST", SqlDbType.NVarChar, 200);
                    try
                    {
                        people2ST.Value = txt_people2ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(people2ST);
                    }
                    catch
                    {
                        people2ST.Value = txt_people2ST.Text;
                        cmd2.Parameters.Add(people2ST);
                    }

                    SqlParameter people1Id = new SqlParameter("@people1Id", SqlDbType.NVarChar, 200);
                    people1Id.Value = txt_people1ST.SelectedValue is long ? txt_people1ST.SelectedValue : -1;
                    cmd2.Parameters.Add(people1Id);

                    SqlParameter people2Id = new SqlParameter("@people2Id", SqlDbType.NVarChar, 200);
                    people2Id.Value = txt_people2ST.SelectedValue is long ? txt_people2ST.SelectedValue : -1;
                    cmd2.Parameters.Add(people2Id);

                    SqlParameter people1_other = new SqlParameter("@people1_other", SqlDbType.NVarChar, 200);
                    people1_other.Value = txt_people1_other.Text;
                    cmd2.Parameters.Add(people1_other);

                    SqlParameter people2_other = new SqlParameter("@people2_other", SqlDbType.NVarChar, 200);
                    people2_other.Value = txt_people2_other.Text;
                    cmd2.Parameters.Add(people2_other);

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
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[excutionSurveyLandmines]([symbol],[cecm_program_id],[cecm_program_idST],[address],[address_cecm],[surveyId],[surveyIdST],[masterId],[masterIdST],[master_other],[bossId],[bossIdST],[boss_other],[boxId],[boxIdST],[dates_tcST],[datesST],[weather],[descript],[people1ST],[people2ST],[people1Id],[people2Id],[people1_other],[people2_other],[files])" +
                        "VALUES(@symbol,@cecm_program_id,@cecm_program_idST,@address,@address_cecm,@surveyId,@surveyIdST,@masterId,@masterIdST,@master_other,@bossId,@bossIdST,@boss_other,@boxId,@boxIdST,@dates_tcST,@datesST,@weather,@descript,@people1ST,@people2ST,@people1Id,@people2Id,@people1_other,@people2_other,@files)", _Cn);
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

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = comboBox_Xa.Text + "," + comboBox_Huyen.Text + "," + comboBox_Tinh.Text;
                    cmd2.Parameters.Add(address);

                    SqlParameter surveyId = new SqlParameter("@surveyId", SqlDbType.NVarChar, 200);
                    surveyId.Value = txt_surveyIdST.SelectedValue;
                    cmd2.Parameters.Add(surveyId);

                    SqlParameter surveyIdST = new SqlParameter("@surveyIdST", SqlDbType.NVarChar, 200);
                    try
                    {
                        surveyIdST.Value = txt_surveyIdST.Text.Split('-')[1];
                        cmd2.Parameters.Add(surveyIdST);
                    }
                    catch
                    {
                        surveyIdST.Value = txt_surveyIdST.Text;
                        cmd2.Parameters.Add(surveyIdST);
                    }

                    SqlParameter masterId = new SqlParameter("@masterId", SqlDbType.BigInt);
                    masterId.Value = txt_masterIdST.SelectedValue != null ? txt_masterIdST.SelectedValue : -1;
                    cmd2.Parameters.Add(masterId);

                    SqlParameter masterIdST = new SqlParameter("@masterIdST", SqlDbType.NVarChar, 200);
                    try
                    {
                        masterIdST.Value = txt_masterIdST.Text.Split('-')[1];
                        cmd2.Parameters.Add(masterIdST);
                    }
                    catch
                    {
                        masterIdST.Value = txt_masterIdST.Text;
                        cmd2.Parameters.Add(masterIdST);
                    }

                    SqlParameter master_other = new SqlParameter("@master_other", SqlDbType.NVarChar, 200);
                    master_other.Value = txt_master_other.Text;
                    cmd2.Parameters.Add(master_other);

                    SqlParameter bossId = new SqlParameter("@bossId", SqlDbType.BigInt);
                    bossId.Value = txt_bossIdST.SelectedValue != null ? txt_bossIdST.SelectedValue : -1;
                    cmd2.Parameters.Add(bossId);

                    SqlParameter bossIdST = new SqlParameter("@bossIdST", SqlDbType.NVarChar, 200);
                    try
                    {
                        bossIdST.Value = txt_bossIdST.Text.Split('-')[1];
                        cmd2.Parameters.Add(bossIdST);
                    }
                    catch
                    {
                        bossIdST.Value = txt_bossIdST.Text;
                        cmd2.Parameters.Add(bossIdST);
                    }

                    SqlParameter boss_other = new SqlParameter("@boss_other", SqlDbType.NVarChar, 200);
                    boss_other.Value = txt_boss_other.Text;
                    cmd2.Parameters.Add(boss_other);

                    SqlParameter boxId = new SqlParameter("@boxId", SqlDbType.NVarChar, 200);
                    boxId.Value = OId;
                    cmd2.Parameters.Add(boxId);

                    SqlParameter boxIdST = new SqlParameter("@boxIdST", SqlDbType.NVarChar, 200);
                    boxIdST.Value = txt_boxIdST.Text;
                    cmd2.Parameters.Add(boxIdST);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter dates_tcST = new SqlParameter("@dates_tcST", SqlDbType.NVarChar, 200);
                    dates_tcST.Value = time_dates_tcST.Text;
                    cmd2.Parameters.Add(dates_tcST);

                    SqlParameter weather = new SqlParameter("@weather", SqlDbType.NVarChar, 200);
                    weather.Value = txt_weather.Text;
                    cmd2.Parameters.Add(weather);

                    SqlParameter descript = new SqlParameter("@descript", SqlDbType.NVarChar, 200);
                    descript.Value = txt_descript.Text;
                    cmd2.Parameters.Add(descript);

                    SqlParameter people1ST = new SqlParameter("@people1ST", SqlDbType.NVarChar, 200);
                    try
                    {
                        people1ST.Value = txt_people1ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(people1ST);
                    }
                    catch
                    {
                        people1ST.Value = txt_people1ST.Text;
                        cmd2.Parameters.Add(people1ST);
                    }

                    SqlParameter people2ST = new SqlParameter("@people2ST", SqlDbType.NVarChar, 200);
                    try
                    {
                        people2ST.Value = txt_people2ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(people2ST);
                    }
                    catch
                    {
                        people2ST.Value = txt_people2ST.Text;
                        cmd2.Parameters.Add(people2ST);
                    }

                    SqlParameter people1Id = new SqlParameter("@people1Id", SqlDbType.NVarChar, 200);
                    people1Id.Value = txt_people1ST.SelectedValue is long ? txt_people1ST.SelectedValue : -1;
                    cmd2.Parameters.Add(people1Id);

                    SqlParameter people2Id = new SqlParameter("@people2Id", SqlDbType.NVarChar, 200);
                    people2Id.Value = txt_people2ST.SelectedValue is long ? txt_people2ST.SelectedValue : -1;
                    cmd2.Parameters.Add(people2Id);

                    SqlParameter people1_other = new SqlParameter("@people1_other", SqlDbType.NVarChar, 200);
                    people1_other.Value = txt_people1_other.Text;
                    cmd2.Parameters.Add(people1_other);

                    SqlParameter people2_other = new SqlParameter("@people2_other", SqlDbType.NVarChar, 200);
                    people2_other.Value = txt_people2_other.Text;
                    cmd2.Parameters.Add(people2_other);
                    try
                    {
                        int temp = 0;
                        temp = cmd2.ExecuteNonQuery();
                        SqlCommandBuilder sqlCommand = null;
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM excutionSurveyLandmines"), _Cn);
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
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from excutionSurveyLandmines where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        DuanId = int.Parse(dr["cecm_program_id"].ToString());
                        //comboBox_TenDA.Text = dr["cecm_program_idST"].ToString();
                        comboBox_TenDA.SelectedValue = long.Parse(dr["cecm_program_id"].ToString());
                        addressDuan = dr["address_cecm"].ToString();
                        var adress = dr["address"].ToString().Split(',');
                        comboBox_Tinh.Text = adress[2];
                        comboBox_Huyen.Text = adress[1];
                        comboBox_Xa.Text = adress[0];
                        txt_symbol.Text = dr["symbol"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();

                        time_datesST.Value = DateTime.ParseExact(dr["datesST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_dates_tcST.Value = DateTime.ParseExact(dr["dates_tcST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        //SurveyId = int.Parse(dr["surveyId"].ToString());
                        if (long.TryParse(dr["surveyId"].ToString(), out long surveyId))
                        {
                            txt_surveyIdST.SelectedValue = surveyId > 0 ? surveyId : -1;
                        }
                        txt_surveyIdST.Text = dr["surveyIdST"].ToString();
                        //master_id = int.Parse(dr["masterId"].ToString());
                        if (long.TryParse(dr["masterId"].ToString(), out long masterId))
                        {
                            txt_masterIdST.SelectedValue = masterId > 0 ? masterId : -1;
                        }
                        //txt_masterIdST.Text = dr["masterIdST"].ToString();
                        if (txt_masterIdST.SelectedValue == null || (long)txt_masterIdST.SelectedValue > 0)
                        {
                            txt_master_other.ReadOnly = true;
                        }
                        txt_master_other.Text = dr["master_other"].ToString();
                        //boss_id = int.Parse(dr["bossId"].ToString());
                        if (long.TryParse(dr["bossId"].ToString(), out long bossId))
                        {
                            txt_bossIdST.SelectedValue = bossId > 0 ? bossId : -1;
                        }
                        //txt_bossIdST.Text = dr["bossIdST"].ToString();
                        if(txt_bossIdST.SelectedValue == null || (long)txt_bossIdST.SelectedValue > 0)
                        {
                            txt_boss_other.ReadOnly = true;
                        }
                        txt_boss_other.Text = dr["boss_other"].ToString();
                        OId = int.Parse(dr["boxId"].ToString());
                        txt_boxIdST.Text = dr["boxIdST"].ToString();
                        txt_weather.Text = dr["weather"].ToString();
                        txt_descript.Text = dr["descript"].ToString();
                        //txt_people1ST.Text = dr["people1ST"].ToString();
                        //txt_people2ST.Text = dr["people2ST"].ToString();

                        txt_people1_other.Text = dr["people1_other"].ToString();
                        txt_people2_other.Text = dr["people2_other"].ToString();
                        //people1_id = int.Parse( dr["people1Id"].ToString());
                        if (long.TryParse(dr["people1Id"].ToString(), out long people1Id))
                        {
                            txt_people1ST.SelectedValue = people1Id > 0 ? people1Id : -1;
                        }
                        if (txt_people1ST.SelectedValue == null || (long)txt_people1ST.SelectedValue > 0)
                        {
                            txt_people1_other.ReadOnly = true;
                        }
                        //people2_id = int.Parse( dr["people2Id"].ToString());
                        if (long.TryParse(dr["people2Id"].ToString(), out long people2Id))
                        {
                            txt_people2ST.SelectedValue = people2Id > 0 ? people2Id : -1;
                        }
                        if (txt_people2ST.SelectedValue == null || (long)txt_people2ST.SelectedValue > 0)
                        {
                            txt_people2_other.ReadOnly = true;
                        }
                    }
                }
            }
        }
        private void FrmThemmoiRPBM1_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;

            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT Ten FROM cecm_provinces where level = 1", _Cn); 
            sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);
            comboBox_Tinh.Items.Add("Chọn");
            foreach (DataRow dr in datatableProvince.Rows)
            {
                if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                    continue;

                comboBox_Tinh.Items.Add(dr["Ten"].ToString());
            }


            //SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT name FROM cecm_programData", _Cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            //System.Data.DataTable datatableProgram = new System.Data.DataTable();
            //sqlAdapterProgram.Fill(datatableProgram);
            //comboBox_TenDA.Items.Add("Chọn");
            //foreach (DataRow dr in datatableProgram.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["name"].ToString()))
            //        continue;

            //    comboBox_TenDA.Items.Add(dr["name"].ToString());
            //}
            UtilsDatabase.LoadCBDA(comboBox_TenDA);
            LoadData(id_BSKQ);
            LoadData1(id_BSKQ, field_name_cv);
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
        private void LoadData1(int main_id, string field_name)
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
                    "select " +
                    "tbl.gid, " +
                    "tbl.string1, " +
                    "tbl.string2, " +
                    "tbl.string3, " +
                    "tbl.string4, " +
                    "ndcv1.the_content as nd1, " +
                    "ndcv2.the_content as nd2 " +
                    "from groundDeliveryRecordsMember tbl " +
                    "left join NoiDungCongViec ndcv1 on tbl.long1 = ndcv1.id " +
                    "left join NoiDungCongViec ndcv2 on tbl.long2 = ndcv2.id " +
                    "where " +
                    "(main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", main_id, field_name, table_name);
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
                FormThemmoiInforBomb frm = new FormThemmoiInforBomb(id_kqks, id_BSKQ, field_name_cv, table_name);
                frm.Text = "CHỈNH SỬA CHI TIẾT CÔNG VIỆC";
                frm.ShowDialog();
                LoadData1(id_BSKQ, field_name_cv);
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
                LoadData1(id_BSKQ, field_name_cv);
            }
        }
        private void buttonSave1_Click(object sender, EventArgs e)
        {
            FormThemmoiInforBomb frm = new FormThemmoiInforBomb(0, id_BSKQ, field_name_cv, table_name);
            frm.Text = "THÊM MỚI CHI TIẾT CÔNG VIỆC";
            frm.ShowDialog();

            LoadData1(id_BSKQ, field_name_cv);
        }

        private void txt_people1ST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(txt_people1ST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_people1ST.SelectedValue > 0)
            {
                txt_people1_other.ReadOnly = true;
                txt_people1_other.Text = "";
            }
            else
            {
                txt_people1_other.ReadOnly = false;
            }
        }

        private void txt_people2ST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(txt_people2ST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_people2ST.SelectedValue > 0)
            {
                txt_people2_other.ReadOnly = true;
                txt_people2_other.Text = "";
            }
            else
            {
                txt_people2_other.ReadOnly = false;
            }
        }

        private void txt_bossIdST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(txt_bossIdST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_bossIdST.SelectedValue > 0)
            {
                txt_boss_other.ReadOnly = true;
                txt_boss_other.Text = "";
            }
            else
            {
                txt_boss_other.ReadOnly = false;
            }
        }

        private void txt_masterIdST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(txt_masterIdST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_masterIdST.SelectedValue > 0)
            {
                txt_master_other.ReadOnly = true;
                txt_master_other.Text = "";
            }
            else
            {
                txt_master_other.ReadOnly = false;
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

        private void txt_surveyIdST_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_surveyIdST, "");
                return;
            }
            if (txt_surveyIdST.Text == "" || txt_surveyIdST.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_surveyIdST, "Chưa chọn đơn vị");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_surveyIdST, "");
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


        private void txt_people1ST_Validating(object sender, CancelEventArgs e)
        {
            //ComboBox A = txt_people1ST;
            //TextBox B = txt_people1_other;
            //string strError = "Chưa chọn CB QLCL";

            //if (A.Text == "" || A.Text == "Chọn")
            //{
            //    if (B.Text == "")
            //    {
            //        e.Cancel = true;
            //        //comboBox_TenDA.Focus();
            //        errorProvider1.SetError(A, strError);
            //        return;
            //    }
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(A, "");
            //}
        } 

        private void txt_people2ST_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txt_masterIdST_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txt_bossIdST_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txt_surveyIdST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_surveyIdST.SelectedValue is long))
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

        //private void btnFile_Click(object sender, EventArgs e)
        //{
        //    lbFile.Text = AppUtils.SaveFileRPBM(openTextLb);
        //}
        //private void lbFile_Validating(object sender, CancelEventArgs e)
        //{
        //    if (lbFile.Text == "" || lbFile.Text == "...")
        //    {
        //        e.Cancel = true;
        //        lbFile.Focus();
        //        errorProvider1.SetError(lbFile, "Chưa chọn file");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(lbFile, "");
        //    }
        //}

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            tbDoc_file.Text = "";
        }

        private void btOpentbDoc_file_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(AppUtils.ReportFolder);
            if (string.IsNullOrEmpty(filePath) == false)
                tbDoc_file.Text = filePath;
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

        private void btnSaveBMVN_Click(object sender, EventArgs e)
        {
            FormThemmoiKetQuaBMVN frm = new FormThemmoiKetQuaBMVN(0, id_BSKQ, field_name_bmvn, table_name);
            frm.Text = "THÊM MỚI SỐ LƯỢNG, CHỦNG LOẠI BOM ĐẠN, VẬT NỔ ĐÃ HỦY (XỬ LÝ)";
            frm.ShowDialog();

            //LoadData1(id_BSKQ, "ConstructionDiaryBomb_ConstructionDiaryInforBomb_BMVN");
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
                //LoadData1(id_BSKQ, "ConstructionDiaryBomb_ConstructionDiaryInforBomb_BMVN");
                LoadDataBMVN();
            }
        }
    }
}
