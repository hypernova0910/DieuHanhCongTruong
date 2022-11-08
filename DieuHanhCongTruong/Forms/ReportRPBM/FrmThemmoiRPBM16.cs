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
    public partial class FrmThemmoiRPBM16 : Form
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
        public string table_name = "ReportConfDestroy";
        public string field_name1 = "ReportConfDestroy_ReportConfDestroy_CDTMember";
        public string field_name2 = "ReportConfDestroy_ReportConfDestroy_SurMember";
        public string field_name3 = "ReportConfDestroy_ReportConfDestroy_LocalMember";
        public string field_name4 = "ReportConfDestroy_ReportConfDestroy_ConsMember";
        public string field_name5 = "ReportConfDestroy_ReportConfDestroy_Bomb";
        public int bossId = 0;
        public int dept_id = 0;
        public int surveyId = 0;
        public int localId = 0;
        public int constuctId = 0;
        private bool isLuuClicked = false;
        public FrmThemmoiRPBM16(int i)
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
                    GetAllStaffWithIdProgram(txt_survey_idST);
                    GetAllStaffWithIdProgram(txt_local_idST);
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
                UtilsDatabase.LoadCBDept(txt_deptidST, DuanId);
            }
        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from RPBM16 where gid = {0}", id_BSKQ), _Cn);
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
                        time_dates_nowST.Value = DateTime.ParseExact(dr["dates_nowST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_date_destroy_fromST.Value = DateTime.ParseExact(dr["date_destroy_fromST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        time_date_destroy_toST.Value = DateTime.ParseExact(dr["date_destroy_toST"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        txt_base_on_tech.Text = dr["base_on_tech"].ToString();
                        txt_technical_regulations.Text = dr["technical_regulations"].ToString();
                        txt_base_on.Text = dr["base_on"].ToString();
                        txt_ground_destroy.Text = dr["ground_destroy"].ToString();
                        txt_method_handl.Text = dr["method_handl"].ToString();
                        txt_address_handl.Text = dr["address_handl"].ToString();
                        txt_result_handl.Text = dr["result_handl"].ToString();
                        txt_amount.Text = dr["amount"].ToString();

                        dept_id = int.Parse(dr["deptid"].ToString());
                        txt_deptidST.Text = dr["deptidST"].ToString();

                        bossId = int.Parse(dr["boss_id"].ToString());
                        txt_boss_idST.Text = dr["boss_idST"].ToString();
                        txt_boss_id_other.Text = dr["boss_id_other"].ToString();
                        surveyId = int.Parse(dr["survey_id"].ToString());
                        txt_survey_idST.Text = dr["survey_idST"].ToString();
                        txt_survey_id_other.Text = dr["survey_id_other"].ToString();
                        constuctId = int.Parse(dr["construct_id"].ToString());
                        txt_constuct_idST.Text = dr["construct_idST"].ToString();
                        txt_constuct_id_other.Text = dr["construct_id_other"].ToString();
                        localId = int.Parse(dr["local_id"].ToString());
                        txt_local_idST.Text = dr["local_idST"].ToString();
                        txt_local_id_other.Text = dr["local_id_other"].ToString();
                        tbDoc_file.Text = dr["files"].ToString();
                    }
                }
            }
        }
        private void FrmThemmoiRPBM16_Load(object sender, EventArgs e)
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
            LoadData1(0, id_BSKQ, field_name1);
            LoadData2(0, id_BSKQ, field_name2);
            LoadData3(0, id_BSKQ, field_name3);
            LoadData4(0, id_BSKQ, field_name4);
            LoadData5();

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
                FormThemmoiMember frm = new FormThemmoiMember(id_kqks, id_BSKQ, field_name1, table_name, DuanId);
                frm.Text = "CHỈNH SỬA ĐƠN VỊ CHỦ ĐẦU TƯ";
                frm.ShowDialog();
                LoadData1(0, id_BSKQ, field_name1);
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
                LoadData1(0, id_BSKQ, field_name1);
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
                frm.Text = "CHỈNH SỬA ĐƠN VỊ GIÁM SÁT";
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
                frm.Text = "CHỈNH SỬA ĐƠN VỊ ĐỊA PHƯƠNG";
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
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dataGridView5.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id_kqks = int.Parse(str);

            if (e.ColumnIndex == 6)
            {
                FormThemmoiKetQuaBMVN frm = new FormThemmoiKetQuaBMVN(id_kqks, id_BSKQ, field_name5, table_name);
                frm.Text = "CHỈNH SỬA BOM MÌN, VẬT NỔ";
                frm.ShowDialog();
                LoadData5();
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
                LoadData5();
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
        private void LoadData5()
        {
            //try
            //{
            //    if (AppUtils.CheckLoggin() == false)
            //        return;
            //    dataGridView5.Rows.Clear();

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
            //            var string5 = dr["string5"].ToString();
            //            dataGridView5.Rows.Add(indexRow, string1, string2, string3, string4, string5, Resources.Modify, Resources.DeleteRed);
            //            dataGridView5.Rows[indexRow - 1].Tag = gid;

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
                dataGridView5.Rows.Clear();

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
                    "(main_id = {0} and field_name = N'{1}' and table_name = N'{2}')", id_BSKQ, field_name5, table_name);
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
                        var string2 = dr["string2"].ToString();
                        var string3 = dr["string3"].ToString();
                        var string4 = dr["string4"].ToString();
                        long.TryParse(dr["long2"].ToString(), out long long2);
                        dataGridView5.Rows.Add(indexRow, string1, string2, string3, long2, string4, Resources.Modify, Resources.DeleteRed);
                        dataGridView5.Rows[indexRow - 1].Tag = gid;

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
                        "UPDATE [dbo].[RPBM16] SET " +
                        "[symbol] = @symbol," +
                        "[address] = @address," +
                        "[datesST] = @datesST," +
                        "[cecm_program_id] = @cecm_program_id," +
                        "[cecm_program_idST] = @cecm_program_idST," +
                        "[address_cecm] = @address_cecm," +
                        "[base_on_tech] = @base_on_tech," +
                        "[technical_regulations] = @technical_regulations," +
                        "[base_on] = @base_on," +
                        "[dates_nowST] = @dates_nowST," +
                        "[ground_destroy] = @ground_destroy," +
                        "[deptidST] = @deptidST," +
                        "[deptid] = @deptid," +
                        "[date_destroy_fromST] = @date_destroy_fromST," +
                        "[date_destroy_toST] = @date_destroy_toST," +
                        "[method_handl] = @method_handl," +
                        "[address_handl] = @address_handl," +
                        "[result_handl] = @result_handl," +
                        "[amount] = @amount," +
                        "[survey_id] = @survey_id," +
                        "[survey_idST] = @survey_idST," +
                        "[survey_id_other] = @survey_id_other," +
                        "[local_id] = @local_id," +
                        "[local_idST] = @local_idST," +
                        "[local_id_other] = @local_id_other," +
                        "[boss_id] = @boss_id," +
                        "[boss_idST] = @boss_idST," +
                        "[boss_id_other] = @boss_id_other," +
                        "[construct_id] = @construct_id," +
                        "[construct_idST] = @construct_idST," +
                        "[construct_id_other] = @construct_id_other, " +
                        "[files] = @files, " +
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

                    SqlParameter dates_nowST = new SqlParameter("@dates_nowST", SqlDbType.NVarChar, 200);
                    dates_nowST.Value = time_dates_nowST.Text;
                    cmd2.Parameters.Add(dates_nowST);

                    SqlParameter date_destroy_fromST = new SqlParameter("@date_destroy_fromST", SqlDbType.NVarChar, 200);
                    date_destroy_fromST.Value = time_date_destroy_fromST.Text;
                    cmd2.Parameters.Add(date_destroy_fromST);

                    SqlParameter date_destroy_toST = new SqlParameter("@date_destroy_toST", SqlDbType.NVarChar, 200);
                    date_destroy_toST.Value = time_date_destroy_toST.Text;
                    cmd2.Parameters.Add(date_destroy_toST);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

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

                    SqlParameter base_on = new SqlParameter("@base_on", SqlDbType.NVarChar, 200);
                    base_on.Value = txt_base_on.Text;
                    cmd2.Parameters.Add(base_on);

                    SqlParameter ground_destroy = new SqlParameter("@ground_destroy", SqlDbType.NVarChar, 200);
                    ground_destroy.Value = txt_ground_destroy.Text;
                    cmd2.Parameters.Add(ground_destroy);

                    SqlParameter method_handl = new SqlParameter("@method_handl", SqlDbType.NVarChar, 200);
                    method_handl.Value = txt_method_handl.Text;
                    cmd2.Parameters.Add(method_handl);

                    SqlParameter address_handl = new SqlParameter("@address_handl", SqlDbType.NVarChar, 200);
                    address_handl.Value = txt_address_handl.Text;
                    cmd2.Parameters.Add(address_handl);

                    SqlParameter result_handl = new SqlParameter("@result_handl", SqlDbType.NVarChar, 200);
                    result_handl.Value = txt_result_handl.Text;
                    cmd2.Parameters.Add(result_handl);

                    SqlParameter amount = new SqlParameter("@amount", SqlDbType.NVarChar, 200);
                    amount.Value = txt_amount.Text;
                    cmd2.Parameters.Add(amount);

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

                    SqlParameter constuct_id = new SqlParameter("@construct_id", SqlDbType.NVarChar, 200);
                    constuct_id.Value = constuctId;
                    cmd2.Parameters.Add(constuct_id);

                    SqlParameter constuct_idST = new SqlParameter("@construct_idST", SqlDbType.NVarChar, 200);
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

                    SqlParameter constuct_id_other = new SqlParameter("@construct_id_other", SqlDbType.NVarChar, 200);
                    constuct_id_other.Value = txt_constuct_id_other.Text;
                    cmd2.Parameters.Add(constuct_id_other);

                    SqlParameter local_id = new SqlParameter("@local_id", SqlDbType.NVarChar, 200);
                    local_id.Value = localId;
                    cmd2.Parameters.Add(local_id);

                    SqlParameter local_idST = new SqlParameter("@local_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        local_idST.Value = txt_local_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(local_idST);
                    }
                    catch
                    {
                        local_idST.Value = txt_local_idST.Text;
                        cmd2.Parameters.Add(local_idST);
                    }

                    SqlParameter local_id_other = new SqlParameter("@local_id_other", SqlDbType.NVarChar, 200);
                    local_id_other.Value = txt_local_id_other.Text;
                    cmd2.Parameters.Add(local_id_other);

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
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[RPBM16]([symbol],[address],[datesST],[cecm_program_id],[cecm_program_idST],[address_cecm],[base_on_tech],[technical_regulations],[base_on],[dates_nowST],[ground_destroy],[deptidST],[deptid],[date_destroy_fromST],[date_destroy_toST],[method_handl],[address_handl],[result_handl],[amount],[survey_id],[survey_idST],[survey_id_other],[local_id],[local_idST],[local_id_other],[boss_id],[boss_idST],[boss_id_other],[construct_id],[construct_idST],[construct_id_other],[files],province_id,district_id,commune_id)" +
                        "VALUES(@symbol,@address,@datesST,@cecm_program_id,@cecm_program_idST,@address_cecm,@base_on_tech,@technical_regulations,@base_on,@dates_nowST,@ground_destroy,@deptidST,@deptid,@date_destroy_fromST,@date_destroy_toST,@method_handl,@address_handl,@result_handl,@amount,@survey_id,@survey_idST,@survey_id_other,@local_id,@local_idST,@local_id_other,@boss_id,@boss_idST,@boss_id_other,@construct_id,@construct_idST,@construct_id_other,@files,@province_id,@district_id,@commune_id)", _Cn);

                    SqlParameter symbol = new SqlParameter("@symbol", SqlDbType.NVarChar, 200);
                    symbol.Value = txt_symbol.Text;
                    cmd2.Parameters.Add(symbol);

                    SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 200);
                    address.Value = (comboBox_Xa.SelectedIndex > 0 ? comboBox_Xa.Text + "," : "") + (comboBox_Huyen.SelectedIndex > 0 ? comboBox_Huyen.Text + "," : "") + (comboBox_Tinh.SelectedIndex > 0 ? comboBox_Tinh.Text : "");
                    cmd2.Parameters.Add(address);

                    SqlParameter datesST = new SqlParameter("@datesST", SqlDbType.NVarChar, 200);
                    datesST.Value = time_datesST.Text;
                    cmd2.Parameters.Add(datesST);

                    SqlParameter dates_nowST = new SqlParameter("@dates_nowST", SqlDbType.NVarChar, 200);
                    dates_nowST.Value = time_dates_nowST.Text;
                    cmd2.Parameters.Add(dates_nowST);

                    SqlParameter date_destroy_fromST = new SqlParameter("@date_destroy_fromST", SqlDbType.NVarChar, 200);
                    date_destroy_fromST.Value = time_date_destroy_fromST.Text;
                    cmd2.Parameters.Add(date_destroy_fromST);

                    SqlParameter date_destroy_toST = new SqlParameter("@date_destroy_toST", SqlDbType.NVarChar, 200);
                    date_destroy_toST.Value = time_date_destroy_toST.Text;
                    cmd2.Parameters.Add(date_destroy_toST);

                    SqlParameter Duan = new SqlParameter("@cecm_program_id", SqlDbType.Int);
                    Duan.Value = DuanId;
                    cmd2.Parameters.Add(Duan);

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

                    SqlParameter base_on = new SqlParameter("@base_on", SqlDbType.NVarChar, 200);
                    base_on.Value = txt_base_on.Text;
                    cmd2.Parameters.Add(base_on);

                    SqlParameter ground_destroy = new SqlParameter("@ground_destroy", SqlDbType.NVarChar, 200);
                    ground_destroy.Value = txt_ground_destroy.Text;
                    cmd2.Parameters.Add(ground_destroy);

                    SqlParameter method_handl = new SqlParameter("@method_handl", SqlDbType.NVarChar, 200);
                    method_handl.Value = txt_method_handl.Text;
                    cmd2.Parameters.Add(method_handl);

                    SqlParameter address_handl = new SqlParameter("@address_handl", SqlDbType.NVarChar, 200);
                    address_handl.Value = txt_address_handl.Text;
                    cmd2.Parameters.Add(address_handl);

                    SqlParameter result_handl = new SqlParameter("@result_handl", SqlDbType.NVarChar, 200);
                    result_handl.Value = txt_result_handl.Text;
                    cmd2.Parameters.Add(result_handl);

                    SqlParameter amount = new SqlParameter("@amount", SqlDbType.NVarChar, 200);
                    amount.Value = txt_amount.Text;
                    cmd2.Parameters.Add(amount);

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

                    SqlParameter constuct_id = new SqlParameter("@construct_id", SqlDbType.NVarChar, 200);
                    constuct_id.Value = constuctId;
                    cmd2.Parameters.Add(constuct_id);

                    SqlParameter constuct_idST = new SqlParameter("@construct_idST", SqlDbType.NVarChar, 200);
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

                    SqlParameter constuct_id_other = new SqlParameter("@construct_id_other", SqlDbType.NVarChar, 200);
                    constuct_id_other.Value = txt_constuct_id_other.Text;
                    cmd2.Parameters.Add(constuct_id_other);

                    SqlParameter local_id = new SqlParameter("@local_id", SqlDbType.NVarChar, 200);
                    local_id.Value = localId;
                    cmd2.Parameters.Add(local_id);

                    SqlParameter local_idST = new SqlParameter("@local_idST", SqlDbType.NVarChar, 200);
                    try
                    {
                        local_idST.Value = txt_local_idST.Text.Split('-')[1];
                        cmd2.Parameters.Add(local_idST);
                    }
                    catch
                    {
                        local_idST.Value = txt_local_idST.Text;
                        cmd2.Parameters.Add(local_idST);
                    }

                    SqlParameter local_id_other = new SqlParameter("@local_id_other", SqlDbType.NVarChar, 200);
                    local_id_other.Value = txt_local_id_other.Text;
                    cmd2.Parameters.Add(local_id_other);

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
                        SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT max([gid]) as gid FROM RPBM16"), _Cn);
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
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, field_name1, table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ CHỦ ĐẦU TƯ";
            frm.ShowDialog();

            LoadData1(id_BSKQ, 0, field_name1);
        }
        private void buttonSave2_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, field_name2, table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ GIÁM SÁT";
            frm.ShowDialog();

            LoadData2(id_BSKQ, 0, field_name2);
        }
        private void buttonSave3_Click(object sender, EventArgs e)
        {
            FormThemmoiMember frm = new FormThemmoiMember(0, id_BSKQ, field_name3, table_name, DuanId);
            frm.Text = "THÊM MỚI ĐƠN VỊ ĐỊA PHƯƠNG";
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
        private void buttonSave5_Click(object sender, EventArgs e)
        {
            FormThemmoiKetQuaBMVN frm = new FormThemmoiKetQuaBMVN(0, id_BSKQ, field_name5, table_name);
            frm.Text = "THÊM MỚI BOM MÌN, VẬT NỔ";
            frm.ShowDialog();

            LoadData5();
        }
        private void txt_monitor_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            surveyId = ChooseCBB(txt_survey_idST);
            txt_survey_id_other.ReadOnly = true;
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

        private void txt_local_idST_SelectedIndexChanged(object sender, EventArgs e)
        {
            localId = ChooseCBB(txt_local_idST);
            txt_local_id_other.ReadOnly = true;
        }

        private void txt_deptidST_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            //TimeBD.Text = null;
            //TimeKT.Text = null;
            if (txt_deptidST.SelectedItem != null && txt_deptidST.Text != "Chọn")
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM dept_tham_gia where dept_id_web = {0}", txt_deptidST.SelectedItem.ToString().Split('-')[0]), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    dept_id = int.Parse(dr["dept_id_web"].ToString());
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
