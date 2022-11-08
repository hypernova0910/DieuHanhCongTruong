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
    public partial class FrmThemmoiRPBM8 : Form
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
        public string table_name = "REPORTPROBSCENE"; 

        public int bossId = 0;
        public int monitorId = 0;
        public int constuctId = 0;
        public int deptidLoad = 0;
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM8(int i)
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
                    GetAllStaffWithIdProgram(txt_constuct_idST);
                    GetAllStaffWithIdProgram(txt_monitor_idST);

                    UtilsDatabase.LoadCBDept(txt_deptid_loadST, DuanId);

                    
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
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM8 where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        txt_symbol.Text = dr["symbol"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();

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
                        time_dates_nowST.Value = DateTime.ParseExact(dr["dates_nowST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        //txt_base_on_tech.Text = dr["base_on_tech"].ToString();
                        txt_result_rpbm.Text = dr["result_rpbm"].ToString();
                        txt_num_point_test.Text = dr["num_point_test"].ToString();
                        txt_num_machine_mine.Text = dr["num_machine_mine"].ToString();
                        txt_num_machine_bomb.Text = dr["num_machine_bomb"].ToString();
                        txt_area_test.Text = dr["area_test"].ToString();
                        txt_num_o_test.Text = dr["num_o_test"].ToString();
                        txt_area_o_test.Text = dr["area_o_test"].ToString();
                        txt_deep_o_test.Text = dr["deep_o_test"].ToString();
                        txt_result_test.Text = dr["result_test"].ToString();
                        txt_qtkt.Text = dr["qtkt"].ToString();
                        txt_qckt.Text = dr["qckt"].ToString();

                        bossId = int.Parse(dr["boss_id"].ToString());
                        txt_boss_idST.Text = dr["boss_idST"].ToString();
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();
                        monitorId = int.Parse(dr["monitor_id"].ToString());
                        txt_monitor_idST.Text = dr["monitor_idST"].ToString();
                        txt_monitor_id_other.Text = dr["monitor_id_other"].ToString();
                        constuctId = int.Parse(dr["constuct_id"].ToString());
                        txt_constuct_idST.Text = dr["constuct_idST"].ToString();
                        txt_constuct_id_other.Text = dr["constuct_id_other"].ToString();

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
        private void FrmThemmoiRPBM8_Load(object sender, EventArgs e)
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
            LoadData1(id_BSKQ, "ReportProbScene_ReportProbScene_CDTMember");
            LoadData2(id_BSKQ, "ReportProbScene_ReportProbScene_MonitorMem");
            LoadData3(id_BSKQ, "ReportProbScene_ReportProbScene_ConstuctMem");
        }
        private void dgvCDT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvCDT.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, "ReportProbScene_ReportProbScene_CDTMember", table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ CHỦ ĐẦU TƯ";
                frm.ShowDialog();
                LoadData1(id_BSKQ, "ReportProbScene_ReportProbScene_CDTMember");
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
                LoadData1(id_BSKQ, "ReportProbScene_ReportProbScene_CDTMember");
            }
        }
        private void dgvDVTC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvDVTC.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, "ReportProbScene_ReportProbScene_MonitorMem", table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ KHẢO SÁT";
                frm.ShowDialog();
                LoadData2(id_BSKQ, "ReportProbScene_ReportProbScene_MonitorMem");
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
                LoadData2(id_BSKQ, "ReportProbScene_ReportProbScene_MonitorMem");
            }
        }
        private void dgvDVGS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvDVGS.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 4)
            {
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, "ReportProbScene_ReportProbScene_ConstuctMem", table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ THI CÔNG";
                frm.ShowDialog();
                LoadData3(id_BSKQ, "ReportProbScene_ReportProbScene_ConstuctMem");
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
                LoadData3(id_BSKQ, "ReportProbScene_ReportProbScene_ConstuctMem");
            }
        }
        private void LoadData1(int main_id, string field_name)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dgvCDT.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                sqlAdapter = new SqlDataAdapter(string.Format("select * from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", main_id, field_name, table_name), _Cn);
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
                            dgvCDT.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        else
                        {
                            var long5 = Resources.cancel_16;
                            dgvCDT.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        dgvCDT.Rows[indexRow - 1].Tag = gid;

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
        private void LoadData2(int main_id, string field_name)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dgvDVTC.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                sqlAdapter = new SqlDataAdapter(string.Format("select * from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", main_id, field_name, table_name), _Cn);
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
                            dgvDVTC.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        else
                        {
                            var long5 = Resources.cancel_16;
                            dgvDVTC.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        dgvDVTC.Rows[indexRow - 1].Tag = gid;

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
        private void LoadData3(int main_id, string field_name)
        {
            try
            {
                //if (AppUtils.CheckLoggin() == false)
                //    return;
                dgvDVGS.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                sqlAdapter = new SqlDataAdapter(string.Format("select * from groundDeliveryRecordsMember where (main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", main_id, field_name, table_name), _Cn);
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
                            dgvDVGS.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        else
                        {
                            var long5 = Resources.cancel_16;
                            dgvDVGS.Rows.Add(indexRow, string1, string2, long5, Resources.Modify, Resources.DeleteRed);
                        }
                        dgvDVGS.Rows[indexRow - 1].Tag = gid;

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
                        "UPDATE [dbo].[RPBM8] SET " +
                        "[deptid_load]=@deptid_load," +
                        "[deptid_loadST]=@deptid_loadST, " +
                        "[symbol] = @symbol," +
                        "[address] = @address," +
                        "[datesST] = @datesST," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[address_cecm] = @address_cecm," +
                        "[base_on_tech] = @base_on_tech," +
                        "[result_rpbm] = @result_rpbm," +
                        "[num_point_test] = @num_point_test," +
                        "[dates_nowST] = @dates_nowST," +
                        "[num_machine_mine] = @num_machine_mine," +
                        "[num_machine_bomb] = @num_machine_bomb," +
                        "[area_test] = @area_test," +
                        "[num_o_test] = @num_o_test," +
                        "[area_o_test] = @area_o_test," +
                        "[deep_o_test] = @deep_o_test," +
                        "[result_test] = @result_test," +
                        "[qtkt] = @qtkt," +
                        "[qckt] = @qckt," +
                        "[boss_id] = @boss_id," +
                        "[boss_idST] = @boss_idST," +
                        "[boss_id_other] = @boss_id_other," +
                        "[monitor_id] = @monitor_id," +
                        "[monitor_idST] = @monitor_idST," +
                        "[monitor_id_other] = @monitor_id_other," +
                        "[constuct_id] = @constuct_id," +
                        "[constuct_idST] = @constuct_idST," +
                        "[constuct_id_other] = @constuct_id_other," +
                        "[files]=@files, " +
                        "[province_id]=@province_id," +
                        "[district_id]=@district_id," +
                        "[commune_id]=@commune_id " +
                        "WHERE gid = " + dem, _Cn);

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

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.NVarChar, 200);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                    cmd2.Parameters.Add(address);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter date_nowST = new SqlParameter("@dates_nowST", SqlDbType.NVarChar, 200);
                    date_nowST.Value = time_dates_nowST.Text;
                    cmd2.Parameters.Add(date_nowST);

                    SqlParameter cecm_program_idST = new SqlParameter("cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    //base_on_tech.Value = txt_base_on_tech.Text;
                    base_on_tech.Value = "";
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter result_rpbm = new SqlParameter("@result_rpbm", SqlDbType.NVarChar, 200);
                    result_rpbm.Value = txt_result_rpbm.Text;
                    cmd2.Parameters.Add(result_rpbm);

                    SqlParameter num_point_test = new SqlParameter("@num_point_test", SqlDbType.NVarChar, 200);
                    num_point_test.Value = txt_num_point_test.Text;
                    cmd2.Parameters.Add(num_point_test);

                    SqlParameter num_machine_mine = new SqlParameter("@num_machine_mine", SqlDbType.NVarChar, 200);
                    num_machine_mine.Value = txt_num_machine_mine.Text;
                    cmd2.Parameters.Add(num_machine_mine);

                    SqlParameter num_machine_bomb = new SqlParameter("@num_machine_bomb", SqlDbType.NVarChar, 200);
                    num_machine_bomb.Value = txt_num_machine_bomb.Text;
                    cmd2.Parameters.Add(num_machine_bomb);

                    SqlParameter area_test = new SqlParameter("@area_test", SqlDbType.NVarChar, 200);
                    area_test.Value = txt_area_test.Text;
                    cmd2.Parameters.Add(area_test);

                    SqlParameter num_o_test = new SqlParameter("@num_o_test", SqlDbType.NVarChar, 200);
                    num_o_test.Value = txt_num_o_test.Text;
                    cmd2.Parameters.Add(num_o_test);

                    SqlParameter area_o_test = new SqlParameter("@area_o_test", SqlDbType.NVarChar, 200);
                    area_o_test.Value = txt_area_o_test.Text;
                    cmd2.Parameters.Add(area_o_test);

                    SqlParameter deep_o_test = new SqlParameter("@deep_o_test", SqlDbType.NVarChar, 200);
                    deep_o_test.Value = txt_deep_o_test.Text;
                    cmd2.Parameters.Add(deep_o_test);

                    SqlParameter result_test = new SqlParameter("@result_test", SqlDbType.NVarChar, 200);
                    result_test.Value = txt_result_test.Text;
                    cmd2.Parameters.Add(result_test);

                    SqlParameter qtkt = new SqlParameter("@qtkt", SqlDbType.NVarChar, 200);
                    qtkt.Value = txt_qtkt.Text;
                    cmd2.Parameters.Add(qtkt);

                    SqlParameter qckt = new SqlParameter("@qckt", SqlDbType.NVarChar, 200);
                    qckt.Value = txt_qckt.Text;
                    cmd2.Parameters.Add(qckt);

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

                    SqlParameter monitor_id = new SqlParameter("@monitor_id", SqlDbType.NVarChar, 200);
                    monitor_id.Value = monitorId;
                    cmd2.Parameters.Add(monitor_id);

                    SqlParameter monitor_idST = new SqlParameter("@monitor_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        monitor_idST.Value = txt_monitor_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(monitor_idST);
                    }
                    catch
                    {
                        monitor_idST.Value = txt_monitor_idST.Text;
                        cmd2.Parameters.Add(monitor_idST);
                    }

                    SqlParameter monitor_id_other = new SqlParameter("@monitor_id_other", SqlDbType.NVarChar, 200);
                    monitor_id_other.Value = txt_monitor_id_other.Text;
                    cmd2.Parameters.Add(monitor_id_other);

                    SqlParameter constuct_id = new SqlParameter("@constuct_id", SqlDbType.NVarChar, 200);
                    constuct_id.Value = constuctId;
                    cmd2.Parameters.Add(constuct_id);

                    SqlParameter constuct_idST = new SqlParameter("@constuct_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        constuct_idST.Value = txt_constuct_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(constuct_idST);
                    }
                    catch
                    {
                        constuct_idST.Value = txt_constuct_idST.Text;
                        cmd2.Parameters.Add(constuct_idST);
                    }

                    SqlParameter constuct_id_other = new SqlParameter("@constuct_id_other", SqlDbType.NVarChar, 200);
                    constuct_id_other.Value = txt_constuct_id_other.Text;
                    cmd2.Parameters.Add(constuct_id_other);

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
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[RPBM8]([deptid_load],[deptid_loadST],[symbol],[address],[datesST],[cecm_program_id],[cecm_program_idST],[address_cecm],[base_on_tech],[result_rpbm],[num_point_test],[dates_nowST],[num_machine_mine],[num_machine_bomb],[area_test],[num_o_test],[area_o_test],[deep_o_test],[result_test],[qtkt],[qckt],[boss_id],[boss_idST],[boss_id_other],[monitor_id],[monitor_idST],[monitor_id_other],[constuct_id],[constuct_idST],[constuct_id_other],[files],province_id,district_id,commune_id)" +
                        "VALUES(@deptid_load,@deptid_loadST,@symbol,@address,@datesST,@cecm_program_id,@cecm_program_idST,@address_cecm,@base_on_tech,@result_rpbm,@num_point_test,@dates_nowST,@num_machine_mine,@num_machine_bomb,@area_test,@num_o_test,@area_o_test,@deep_o_test,@result_test,@qtkt,@qckt,@boss_id,@boss_idST,@boss_id_other,@monitor_id,@monitor_idST,@monitor_id_other,@constuct_id,@constuct_idST,@constuct_id_other,@files,@province_id,@district_id,@commune_id)", _Cn);

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

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter files = new SqlParameter("@files", SqlDbType.NVarChar, 200);
                    files.Value = tbDoc_file.Text;
                    cmd2.Parameters.Add(files);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.NVarChar, 200);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                    cmd2.Parameters.Add(address);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter date_nowST = new SqlParameter("@dates_nowST", SqlDbType.NVarChar, 200);
                    date_nowST.Value = time_dates_nowST.Text;
                    cmd2.Parameters.Add(date_nowST);

                    SqlParameter cecm_program_idST = new SqlParameter("cecm_program_idST", SqlDbType.NVarChar, 200);
                    cecm_program_idST.Value = comboBox_TenDA.Text;
                    cmd2.Parameters.Add(cecm_program_idST);

                    SqlParameter address_cecm = new SqlParameter("@address_cecm", SqlDbType.NVarChar, 200);
                    address_cecm.Value = addressDuan;
                    cmd2.Parameters.Add(address_cecm);

                    SqlParameter base_on_tech = new SqlParameter("@base_on_tech", SqlDbType.NVarChar, 200);
                    //base_on_tech.Value = txt_base_on_tech.Text;
                    base_on_tech.Value = "";
                    cmd2.Parameters.Add(base_on_tech);

                    SqlParameter result_rpbm = new SqlParameter("@result_rpbm", SqlDbType.NVarChar, 200);
                    result_rpbm.Value = txt_result_rpbm.Text;
                    cmd2.Parameters.Add(result_rpbm);

                    SqlParameter num_point_test = new SqlParameter("@num_point_test", SqlDbType.NVarChar, 200);
                    num_point_test.Value = txt_num_point_test.Text;
                    cmd2.Parameters.Add(num_point_test);

                    SqlParameter num_machine_mine = new SqlParameter("@num_machine_mine", SqlDbType.NVarChar, 200);
                    num_machine_mine.Value = txt_num_machine_mine.Text;
                    cmd2.Parameters.Add(num_machine_mine);

                    SqlParameter num_machine_bomb = new SqlParameter("@num_machine_bomb", SqlDbType.NVarChar, 200);
                    num_machine_bomb.Value = txt_num_machine_bomb.Text;
                    cmd2.Parameters.Add(num_machine_bomb);

                    SqlParameter area_test = new SqlParameter("@area_test", SqlDbType.NVarChar, 200);
                    area_test.Value = txt_area_test.Text;
                    cmd2.Parameters.Add(area_test);

                    SqlParameter num_o_test = new SqlParameter("@num_o_test", SqlDbType.NVarChar, 200);
                    num_o_test.Value = txt_num_o_test.Text;
                    cmd2.Parameters.Add(num_o_test);

                    SqlParameter area_o_test = new SqlParameter("@area_o_test", SqlDbType.NVarChar, 200);
                    area_o_test.Value = txt_area_o_test.Text;
                    cmd2.Parameters.Add(area_o_test);

                    SqlParameter deep_o_test = new SqlParameter("@deep_o_test", SqlDbType.NVarChar, 200);
                    deep_o_test.Value = txt_deep_o_test.Text;
                    cmd2.Parameters.Add(deep_o_test);

                    SqlParameter result_test = new SqlParameter("@result_test", SqlDbType.NVarChar, 200);
                    result_test.Value = txt_result_test.Text;
                    cmd2.Parameters.Add(result_test);

                    SqlParameter qtkt = new SqlParameter("@qtkt", SqlDbType.NVarChar, 200);
                    qtkt.Value = txt_qtkt.Text;
                    cmd2.Parameters.Add(qtkt);

                    SqlParameter qckt = new SqlParameter("@qckt", SqlDbType.NVarChar, 200);
                    qckt.Value = txt_qckt.Text;
                    cmd2.Parameters.Add(qckt);

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

                    SqlParameter monitor_id = new SqlParameter("@monitor_id", SqlDbType.NVarChar, 200);
                    monitor_id.Value = monitorId;
                    cmd2.Parameters.Add(monitor_id);

                    SqlParameter monitor_idST = new SqlParameter("@monitor_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        monitor_idST.Value = txt_monitor_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(monitor_idST);
                    }
                    catch
                    {
                        monitor_idST.Value = txt_monitor_idST.Text;
                        cmd2.Parameters.Add(monitor_idST);
                    }

                    SqlParameter monitor_id_other = new SqlParameter("@monitor_id_other", SqlDbType.NVarChar, 200);
                    monitor_id_other.Value = txt_monitor_id_other.Text;
                    cmd2.Parameters.Add(monitor_id_other);

                    SqlParameter constuct_id = new SqlParameter("@constuct_id", SqlDbType.NVarChar, 200);
                    constuct_id.Value = constuctId;
                    cmd2.Parameters.Add(constuct_id);

                    SqlParameter constuct_idST = new SqlParameter("@constuct_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        constuct_idST.Value = txt_constuct_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(constuct_idST);
                    }
                    catch
                    {
                        constuct_idST.Value = txt_constuct_idST.Text;
                        cmd2.Parameters.Add(constuct_idST);
                    }

                    SqlParameter constuct_id_other = new SqlParameter("@constuct_id_other", SqlDbType.NVarChar, 200);
                    constuct_id_other.Value = txt_constuct_id_other.Text;
                    cmd2.Parameters.Add(constuct_id_other);

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
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM RPBM8"), _Cn);
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
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, "ReportProbScene_ReportProbScene_CDTMember", table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ CHỦ ĐẦU TƯ";
            frm.ShowDialog();

            LoadData1(id_BSKQ, "ReportProbScene_ReportProbScene_CDTMember");
        }
        private void buttonSave2_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, "ReportProbScene_ReportProbScene_MonitorMem", table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ KHẢO SÁT";
            frm.ShowDialog();

            LoadData2(id_BSKQ, "ReportProbScene_ReportProbScene_MonitorMem");
        }
        private void buttonSave3_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, "ReportProbScene_ReportProbScene_ConstuctMem", table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ THI CÔNG";
            frm.ShowDialog();

            LoadData3(id_BSKQ, "ReportProbScene_ReportProbScene_ConstuctMem");
        }
        private void txt_monitor_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            monitorId = ChooseCBB(txt_monitor_idST);
            txt_monitor_id_other.ReadOnly = true;
        }

        private void txt_constuct_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            constuctId = ChooseCBB(txt_constuct_idST);
            txt_constuct_id_other.ReadOnly = true;
        }

        private void txt_boss_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            bossId = ChooseCBB(txt_boss_idST);
            txt_boss_id_other.ReadOnly = true;
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

        private void txt_deptid_loadST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_deptid_loadST.SelectedValue is long))
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
    }
}
