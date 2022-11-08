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
    public partial class FrmThemmoiRPBM17 : Form
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
        //public int TinhId = 0;
        //public int HuyenId = 0;
        //public int XaId = 0;
        public string symbol = "0";
        public string table_name = "ReportResConsRPBM";
        public string field_name1 = "ReportResConsRPBM_ReportResConsRPBM_Work";
        //public int commandId = 0;
        public int deptidHrpbm = 0;
        //public int constructId = 0;
        public int category = 0;
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM17(int i)
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

        public void LoadCBStaff(ComboBox cb)
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
        private void comboBox_TenDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
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
                    UtilsDatabase.LoadCBStaff(txt_command_idST,DuanId);
                    UtilsDatabase.LoadCBStaff(txt_construct_idST,DuanId);
                }
                //SqlDataAdapter sqlAdapterCounty1 = new SqlDataAdapter(string.Format("select CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web as 'gid',dtg.cecm_program_id,dtg.table_name from dept_tham_gia dtg left join cert_department d on CASE WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1 WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1 ELSE 0 END = 1 WHERE dtg.cecm_program_id = {0} and dtg.dept_id_web IS NOT NULL", DuanId), _Cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapterCounty1);
                //System.Data.DataTable datatableCounty1 = new System.Data.DataTable();
                //sqlAdapterCounty1.Fill(datatableCounty1);
                //txt_deptid_hrpbm.Items.Clear();
                //txt_deptid_hrpbm.Items.Add("Chọn");
                //foreach (DataRow dr in datatableCounty1.Rows)
                //{
                //    if (string.IsNullOrEmpty(dr["dept_idST"].ToString()))
                //        continue;
                //    var a = dr["gid"].ToString() + "-" + dr["dept_idST"].ToString();
                //    txt_deptid_hrpbm.Items.Add(a);
                //}
                UtilsDatabase.LoadCBDept(txt_deptid_hrpbm, DuanId);
            }
        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM17 where gid = {0}", id_BSKQ), _Cn);
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
                        time_date_economic_contractsST.Value = DateTime.ParseExact(dr["date_economic_contractsST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_date_startST.Value = DateTime.ParseExact(dr["date_startST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_dates_endST.Value = DateTime.ParseExact(dr["dates_endST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        txt_organ_receive.Text = dr["organ_receive"].ToString();
                        txt_technical_regulations.Text = dr["technical_regulations"].ToString();
                        txt_base_on_thongtu.Text = dr["base_on_thongtu"].ToString();
                        txt_num_economic_contracts.Text = dr["num_economic_contracts"].ToString();
                        txt_organization_signed.Text = dr["organization_signed"].ToString();
                        txt_mission.Text = dr["mission"].ToString();
                        txt_deep_rpbm.Text = dr["deep_rpbm"].ToString();
                        txt_num_team.Text = dr["num_team"].ToString();
                        txt_number_driver.Text = dr["number_driver"].ToString();
                        txt_number_machine_min.Text = dr["number_machine_min"].ToString();
                        txt_number_machine_bomb.Text = dr["number_machine_bomb"].ToString();
                        txt_area_rapha.Text = dr["area_rapha"].ToString();
                        txt_deep_rapha.Text = dr["deep_rapha"].ToString();
                        txt_result.Text = dr["result"].ToString();
                        txt_comment_tc.Text = dr["comment_tc"].ToString();
                        txt_conclusion.Text = dr["conclusion"].ToString();

                        deptidHrpbm = int.Parse(dr["deptid_hrpbm"].ToString());
                        txt_deptid_hrpbm.Text = dr["deptid_hrpbmST"].ToString();

                        //commandId = int.Parse(dr["command_id"].ToString());
                        //txt_command_idST.Text = dr["command_idST"].ToString();
                        if(long.TryParse(dr["command_id"].ToString(), out long command_id))
                        {
                            txt_command_idST.SelectedValue = command_id;
                        }
                        txt_command_id_other.Text = dr["command_id_other"].ToString();
                        //constructId = int.Parse(dr["construct_id"].ToString());
                        //txt_construct_idST.Text = dr["construct_idST"].ToString();
                        if (long.TryParse(dr["construct_id"].ToString(), out long construct_id))
                        {
                            txt_construct_idST.SelectedValue = construct_id;
                        }
                        txt_construct_id_other.Text = dr["construct_id_other"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();
                        lbFile_1.Text = dr["files_1"].ToString();
                        lbFile_2.Text = dr["files_2"].ToString();
                    }
                }
            }
        }
        private void FrmThemmoiRPBM17_Load(object sender, EventArgs e)
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

            if (e.ColumnIndex == 6)
            {
                FormThemmoiKLTCNew2 frm = new FormThemmoiKLTCNew2(id_kqks, id_BSKQ, field_name1, table_name);
                frm.Text = "CHỈNH SỬA KẾT QUẢ THI CÔNG";
                //frm.label5.Visible = false;
                //frm.txtString5.Visible = false;
                frm.ShowDialog();
                LoadData1();
            }
            //delete column
            if (e.ColumnIndex == 7)
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

        private void LoadData1()
        {
            //try
            //{
            //    if (AppUtils.CheckLoggin() == false)
            //        return;
            //    dataGridView1.Rows.Clear();

            //    System.Data.DataTable datatable = new System.Data.DataTable();
            //    SqlCommandBuilder sqlCommand = null;
            //    SqlDataAdapter sqlAdapter = null;
            //    sqlAdapter = new SqlDataAdapter(string.Format("select * from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}') or (main_id = {3} and field_name = N'{4}' and table_name = N'{5}')", dem, dem1, table_name, dem0, dem1, table_name), _Cn);
            //    sqlCommand = new SqlCommandBuilder(sqlAdapter);
            //    sqlAdapter.Fill(datatable);

            //    if (datatable.Rows.Count != 0)
            //    {
            //        int indexRow = 1;
            //        foreach (DataRow dr in datatable.Rows)
            //        {
            //            var gid = dr["gid"].ToString();
            //            var string1 = dr["string1"].ToString();
            //            var string2 = dr["string2"].ToString();
            //            var string3 = dr["string3"].ToString();
            //            var string4 = dr["string4"].ToString();
            //            //var string5 = dr["string5"].ToString();
            //            var string6 = dr["string6"].ToString();

            //            dataGridView1.Rows.Add(indexRow, string1, string2, string3, string4, string6, Resources.Modify, Resources.DeleteRed);
            //            dataGridView1.Rows[indexRow - 1].Tag = gid;

            //            indexRow++;
            //        }
            //    }

            //}
            //catch (System.Exception ex)
            //{
            //    //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
            //    return;
            //}
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dataGridView1.Rows.Clear();

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
                        var string3 = dr["string3"].ToString();
                        dataGridView1.Rows.Add(indexRow, string1, string2, double1, double2, string3);
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
                        "UPDATE [dbo].[RPBM17] SET " +
                        "[symbol] = @symbol," +
                        "[address] = @address," +
                        "[datesST] = @datesST," +
                        "[organ_receive] = @organ_receive," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[deptid_hrpbm] = @deptid_hrpbm," +
                        "[deptid_hrpbmST] = @deptid_hrpbmST," +
                        "[address_cecm] = @address_cecm," +
                        "[base_on_thongtu] = @base_on_thongtu," +
                        "[technical_regulations] = @technical_regulations," +
                        "[num_economic_contracts] = @num_economic_contracts," +
                        "[date_economic_contractsST] = @date_economic_contractsST," +
                        "[organization_signed] = @organization_signed," +
                        "[mission] = @mission," +
                        "[deep_rpbm] = @deep_rpbm," +
                        "[date_startST] = @date_startST," +
                        "[dates_endST] = @dates_endST," +
                        "[command_id] = @command_id," +
                        "[command_idST] = @command_idST," +
                        "[command_id_other] = @command_id_other," +
                        "[num_team] = @num_team," +
                        "[number_driver] = @number_driver," +
                        "[number_machine_min] = @number_machine_min," +
                        "[number_machine_bomb] = @number_machine_bomb," +
                        "[area_rapha] = @area_rapha," +
                        "[deep_rapha] = @deep_rapha," +
                        "[result] = @result," +
                        "[comment_tc] = @comment_tc," +
                        "[conclusion] = @conclusion," +
                        "[construct_id] = @construct_id," +
                        "[construct_idST] = @construct_idST," +
                        "[construct_id_other] = @construct_id_other, " +
                        "[files] = @files," +
                        "[files_1] = @files_1," +
                        "[files_2] = @files_2,  " +
                        "[province_id]=@province_id," +
                        "[district_id]=@district_id," +
                        "[commune_id]=@commune_id " +
                        "WHERE gid = " + dem, _Cn);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                    cmd2.Parameters.Add(address);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter date_economic_contractsST = new SqlParameter("@date_economic_contractsST", SqlDbType.NVarChar, 200);
                    date_economic_contractsST.Value = time_date_economic_contractsST.Text;
                    cmd2.Parameters.Add(date_economic_contractsST);

                    SqlParameter date_startST = new SqlParameter("@date_startST", SqlDbType.NVarChar, 200);
                    date_startST.Value = time_date_startST.Text;
                    cmd2.Parameters.Add(date_startST);

                    SqlParameter dates_endST = new SqlParameter("@dates_endST", SqlDbType.NVarChar, 200);
                    dates_endST.Value = time_dates_endST.Text;
                    cmd2.Parameters.Add(dates_endST);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter cecm_program_idST = new SqlParameter("cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter organ_receive = new SqlParameter("@organ_receive", SqlDbType.NVarChar, 200);
                    organ_receive.Value = txt_organ_receive.Text;
                    cmd2.Parameters.Add(organ_receive);

                    SqlParameter base_on_thongtu = new SqlParameter("@base_on_thongtu", SqlDbType.NVarChar, 200);
                    base_on_thongtu.Value = txt_base_on_thongtu.Text;
                    cmd2.Parameters.Add(base_on_thongtu);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter num_economic_contracts = new SqlParameter("@num_economic_contracts", SqlDbType.NVarChar, 200);
                    num_economic_contracts.Value = txt_num_economic_contracts.Text;
                    cmd2.Parameters.Add(num_economic_contracts);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter mission = new SqlParameter("@mission", SqlDbType.NVarChar, 200);
                    mission.Value = txt_mission.Text;
                    cmd2.Parameters.Add(mission);

                    SqlParameter deep_rpbm = new SqlParameter("@deep_rpbm", SqlDbType.NVarChar, 200);
                    deep_rpbm.Value = txt_deep_rpbm.Text;
                    cmd2.Parameters.Add(deep_rpbm);

                    SqlParameter num_team = new SqlParameter("@num_team", SqlDbType.NVarChar, 200);
                    num_team.Value = txt_num_team.Text;
                    cmd2.Parameters.Add(num_team);

                    SqlParameter number_driver = new SqlParameter("@number_driver", SqlDbType.NVarChar, 200);
                    number_driver.Value = txt_number_driver.Text;
                    cmd2.Parameters.Add(number_driver);

                    SqlParameter number_machine_min = new SqlParameter("@number_machine_min", SqlDbType.NVarChar, 200);
                    number_machine_min.Value = txt_number_machine_min.Text;
                    cmd2.Parameters.Add(number_machine_min);

                    SqlParameter number_machine_bomb = new SqlParameter("@number_machine_bomb", SqlDbType.NVarChar, 200);
                    number_machine_bomb.Value = txt_number_machine_bomb.Text;
                    cmd2.Parameters.Add(number_machine_bomb);

                    SqlParameter area_rapha = new SqlParameter("@area_rapha", SqlDbType.NVarChar, 200);
                    area_rapha.Value = txt_area_rapha.Text;
                    cmd2.Parameters.Add(area_rapha);

                    SqlParameter deep_rapha = new SqlParameter("@deep_rapha", SqlDbType.NVarChar, 200);
                    deep_rapha.Value = txt_deep_rapha.Text;
                    cmd2.Parameters.Add(deep_rapha);

                    SqlParameter result = new SqlParameter("@result", SqlDbType.NVarChar, 200);
                    result.Value = txt_result.Text;
                    cmd2.Parameters.Add(result);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter files_1 = new SqlParameter("@files_1", SqlDbType.NVarChar, 200);
                    files_1.Value = lbFile_1.Text;
                    cmd2.Parameters.Add(files_1);

                    SqlParameter files_2 = new SqlParameter("@files_2", SqlDbType.NVarChar, 200);
                    files_2.Value = lbFile_2.Text;
                    cmd2.Parameters.Add(files_2);

                    SqlParameter comment_tc = new SqlParameter("@comment_tc", SqlDbType.NVarChar, 200);
                    comment_tc.Value = txt_comment_tc.Text;
                    cmd2.Parameters.Add(comment_tc);

                    SqlParameter conclusion = new SqlParameter("@conclusion", SqlDbType.NVarChar, 200);
                    conclusion.Value = txt_conclusion.Text;
                    cmd2.Parameters.Add(conclusion);

                    SqlParameter deptid_hrpbm = new SqlParameter("@deptid_hrpbm", SqlDbType.NVarChar, 200);
                    deptid_hrpbm.Value = txt_deptid_hrpbm.SelectedValue;
                    cmd2.Parameters.Add(deptid_hrpbm);

                    SqlParameter deptid_hrpbmST = new SqlParameter("@deptid_hrpbmST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_hrpbmST.Value = txt_deptid_hrpbm.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_hrpbmST);
                    }
                    catch
                    {
                        deptid_hrpbmST.Value = txt_deptid_hrpbm.Text;
                        cmd2.Parameters.Add(deptid_hrpbmST);
                    }

                    SqlParameter command_id = new SqlParameter("@command_id", SqlDbType.BigInt);
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

                    SqlParameter construct_id = new SqlParameter("@construct_id", SqlDbType.BigInt);
                    construct_id.Value = txt_construct_idST.SelectedValue;
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
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[RPBM17]([symbol],[address],[datesST],[organ_receive],[cecm_program_id],[cecm_program_idST],[deptid_hrpbm],[deptid_hrpbmST],[address_cecm],[base_on_thongtu],[technical_regulations],[num_economic_contracts],[date_economic_contractsST],[organization_signed],[mission],[deep_rpbm],[date_startST],[dates_endST],[command_id],[command_idST],[command_id_other],[num_team],[number_driver],[number_machine_min],[number_machine_bomb],[area_rapha],[deep_rapha],[result],[comment_tc],[conclusion],[construct_id],[construct_idST],[construct_id_other],[files],[files_1],[files_2],province_id,district_id,commune_id)" +
                        "VALUES(@symbol,@address,@datesST,@organ_receive,@cecm_program_id,@cecm_program_idST,@deptid_hrpbm,@deptid_hrpbmST,@address_cecm,@base_on_thongtu,@technical_regulations,@num_economic_contracts,@date_economic_contractsST,@organization_signed,@mission,@deep_rpbm,@date_startST,@dates_endST,@command_id,@command_idST,@command_id_other,@num_team,@number_driver,@number_machine_min,@number_machine_bomb,@area_rapha,@deep_rapha,@result,@comment_tc,@conclusion,@construct_id,@construct_idST,@construct_id_other,@files,@files_1,@files_2,@province_id,@district_id,@commune_id)", _Cn);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                    cmd2.Parameters.Add(address);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter date_economic_contractsST = new SqlParameter("@date_economic_contractsST", SqlDbType.NVarChar, 200);
                    date_economic_contractsST.Value = time_date_economic_contractsST.Text;
                    cmd2.Parameters.Add(date_economic_contractsST);

                    SqlParameter date_startST = new SqlParameter("@date_startST", SqlDbType.NVarChar, 200);
                    date_startST.Value = time_date_startST.Text;
                    cmd2.Parameters.Add(date_startST);

                    SqlParameter dates_endST = new SqlParameter("@dates_endST", SqlDbType.NVarChar, 200);
                    dates_endST.Value = time_dates_endST.Text;
                    cmd2.Parameters.Add(dates_endST);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter cecm_program_idST = new SqlParameter("cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter organ_receive = new SqlParameter("@organ_receive", SqlDbType.NVarChar, 200);
                    organ_receive.Value = txt_organ_receive.Text;
                    cmd2.Parameters.Add(organ_receive);

                    SqlParameter base_on_thongtu = new SqlParameter("@base_on_thongtu", SqlDbType.NVarChar, 200);
                    base_on_thongtu.Value = txt_base_on_thongtu.Text;
                    cmd2.Parameters.Add(base_on_thongtu);

                    SqlParameter technical_regulations = new SqlParameter("@technical_regulations", SqlDbType.NVarChar, 200);
                    technical_regulations.Value = txt_technical_regulations.Text;
                    cmd2.Parameters.Add(technical_regulations);

                    SqlParameter num_economic_contracts = new SqlParameter("@num_economic_contracts", SqlDbType.NVarChar, 200);
                    num_economic_contracts.Value = txt_num_economic_contracts.Text;
                    cmd2.Parameters.Add(num_economic_contracts);

                    SqlParameter organization_signed = new SqlParameter("@organization_signed", SqlDbType.NVarChar, 200);
                    organization_signed.Value = txt_organization_signed.Text;
                    cmd2.Parameters.Add(organization_signed);

                    SqlParameter mission = new SqlParameter("@mission", SqlDbType.NVarChar, 200);
                    mission.Value = txt_mission.Text;
                    cmd2.Parameters.Add(mission);

                    SqlParameter deep_rpbm = new SqlParameter("@deep_rpbm", SqlDbType.NVarChar, 200);
                    deep_rpbm.Value = txt_deep_rpbm.Text;
                    cmd2.Parameters.Add(deep_rpbm);

                    SqlParameter num_team = new SqlParameter("@num_team", SqlDbType.NVarChar, 200);
                    num_team.Value = txt_num_team.Text;
                    cmd2.Parameters.Add(num_team);

                    SqlParameter number_driver = new SqlParameter("@number_driver", SqlDbType.NVarChar, 200);
                    number_driver.Value = txt_number_driver.Text;
                    cmd2.Parameters.Add(number_driver);

                    SqlParameter number_machine_min = new SqlParameter("@number_machine_min", SqlDbType.NVarChar, 200);
                    number_machine_min.Value = txt_number_machine_min.Text;
                    cmd2.Parameters.Add(number_machine_min);

                    SqlParameter number_machine_bomb = new SqlParameter("@number_machine_bomb", SqlDbType.NVarChar, 200);
                    number_machine_bomb.Value = txt_number_machine_bomb.Text;
                    cmd2.Parameters.Add(number_machine_bomb);

                    SqlParameter area_rapha = new SqlParameter("@area_rapha", SqlDbType.NVarChar, 200);
                    area_rapha.Value = txt_area_rapha.Text;
                    cmd2.Parameters.Add(area_rapha);

                    SqlParameter deep_rapha = new SqlParameter("@deep_rapha", SqlDbType.NVarChar, 200);
                    deep_rapha.Value = txt_deep_rapha.Text;
                    cmd2.Parameters.Add(deep_rapha);

                    SqlParameter result = new SqlParameter("@result", SqlDbType.NVarChar, 200);
                    result.Value = txt_result.Text;
                    cmd2.Parameters.Add(result);

                    SqlParameter comment_tc = new SqlParameter("@comment_tc", SqlDbType.NVarChar, 200);
                    comment_tc.Value = txt_comment_tc.Text;
                    cmd2.Parameters.Add(comment_tc);

                    SqlParameter conclusion = new SqlParameter("@conclusion", SqlDbType.NVarChar, 200);
                    conclusion.Value = txt_conclusion.Text;
                    cmd2.Parameters.Add(conclusion);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter files_1 = new SqlParameter("@files_1", SqlDbType.NVarChar, 200);
                    files_1.Value = lbFile_1.Text;
                    cmd2.Parameters.Add(files_1);

                    SqlParameter files_2 = new SqlParameter("@files_2", SqlDbType.NVarChar, 200);
                    files_2.Value = lbFile_2.Text;
                    cmd2.Parameters.Add(files_2);

                    SqlParameter deptid_hrpbm = new SqlParameter("@deptid_hrpbm", SqlDbType.NVarChar, 200);
                    deptid_hrpbm.Value = txt_deptid_hrpbm.SelectedValue;
                    cmd2.Parameters.Add(deptid_hrpbm);

                    SqlParameter deptid_hrpbmST = new SqlParameter("@deptid_hrpbmST", SqlDbType.NVarChar, 200);
                    try
                    {
                        deptid_hrpbmST.Value = txt_deptid_hrpbm.Text.Split('-')[1];
                        cmd2.Parameters.Add(deptid_hrpbmST);
                    }
                    catch
                    {
                        deptid_hrpbmST.Value = txt_deptid_hrpbm.Text;
                        cmd2.Parameters.Add(deptid_hrpbmST);
                    }

                    SqlParameter command_id = new SqlParameter("@command_id", SqlDbType.BigInt);
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

                    SqlParameter construct_id = new SqlParameter("@construct_id", SqlDbType.BigInt);
                    construct_id.Value = txt_construct_idST.SelectedValue;
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
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM RPBM17"), _Cn);
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
        private void buttonSave1_Click(object sender, EventArgs e)
        {
            FormThemmoiKLTCNew2 frm = new FormThemmoiKLTCNew2(0, id_BSKQ, field_name1, table_name);
            frm.Text = "THÊM MỚI KẾT QUẢ THI CÔNG";
            //frm.label5.Visible = false;
            //frm.txtString5.Visible = false;
            frm.ShowDialog();

            LoadData1();
        }
        private void txt_deptid_hrpbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            ////TimeBD.Text = null;
            ////TimeKT.Text = null;
            //if (txt_deptid_hrpbm.SelectedItem != null && txt_deptid_hrpbm.Text != "Chọn")
            //{
            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptid_hrpbm.SelectedItem.ToString().Split('-')[0]), _Cn);
            //    sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);

            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        deptidHrpbm = int.Parse(dr["dept_id_web"].ToString());
            //        //txt_masterIdST.Text = dr["head"].ToString();
            //    }
            //}
        }

        private void txt_command_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //commandId = ChooseCBB(txt_command_idST);
            //txt_command_id_other.ReadOnly = true;
        }

        private void txt_construct_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //constructId = ChooseCBB(txt_construct_idST);
            //txt_construct_id_other.ReadOnly = true;
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

        private void txt_deptid_hrpbm_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox_TenDA.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txt_deptid_hrpbm, "");
                return;
            }
            if (txt_deptid_hrpbm.Text == "" || txt_deptid_hrpbm.Text == "Chọn")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_hrpbm, "Chưa chọn đơn vị");
                return;
            }

            else
            {
                e.Cancel = false;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txt_deptid_hrpbm, "");
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
            if (!(txt_command_idST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_command_idST.SelectedValue > 0)
            {
                txt_command_id_other.ReadOnly = true;
                txt_command_id_other.Text = "";
            }
            else
            {
                txt_command_id_other.ReadOnly = false;
            }
        }

        private void txt_construct_idST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_construct_idST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_construct_idST.SelectedValue > 0)
            {
                txt_construct_id_other.ReadOnly = true;
                txt_construct_id_other.Text = "";
            }
            else
            {
                txt_construct_id_other.ReadOnly = false;
            }
        }
        public string openTextLb = "All files (*.*)|*.*";
        private void btnFile_1_Click(object sender, EventArgs e)
        {
            lbFile_1.Text = AppUtils.SaveFileRPBM(openTextLb);
        }

        private void btnFile_2_Click(object sender, EventArgs e)
        {
            lbFile_2.Text = AppUtils.SaveFileRPBM(openTextLb);
        }

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
