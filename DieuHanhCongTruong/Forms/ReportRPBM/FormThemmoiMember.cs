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
    public partial class FormThemmoiMember : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string field_name = "";
        public string table_name = "";
        public string long_1 = "-1";
        public string long_2 = "-1";
        public string long_5 = "";
        private bool btnLuuClicked = false;

        public int main_id = 0;
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;
        public int idProgram = 0;
        public FormThemmoiMember(int i, int main_id, string field_name, string table_name, int idProgram)
        {
            id_BSKQ = i;
            this.field_name = field_name;
            this.table_name = table_name;
            this.main_id = main_id;
            this.idProgram = idProgram;
            _Cn = _Cn = frmLoggin.sqlCon;
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            if (_ExtraInfoConnettion == null)
                return;
            InitializeComponent();
        }
        private string CheckChoose(ComboBox A)
        {
            if (A.Text == "Chọn" || A.Text == "")
            {
                A.Text = "";
            }
            return A.Text;
        }
        private bool UpdateInfomation()
        {
            try
            {
                //SqlCommandBuilder sqlCommand = null;
                //SqlDataAdapter sqlAdapter = null;
                //DataTable datatable = new DataTable();
                //sqlAdapter = new SqlDataAdapter(String.Format("USE [{0}] SELECT cecm_user.user_name FROM cecm_user where user_name = '{1}'", databaseName, tbTenDangNhap.Text), cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapter);
                //sqlAdapter.Fill(datatable);
                if (id_BSKQ == 0)
                {
                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[groundDeliveryRecordsMember] ([table_name],[field_name],[main_id],[string1],[string2],[long1ST],[long2ST],[long1],[long2],[long5]) Values (@table_name,@field_name,@main_id,@string1,@string2,@long1ST,@long2ST,@long1,@long2,@long5)", _Cn);
                    
                    SqlParameter table_name = new SqlParameter("@table_name", SqlDbType.NVarChar, 255);
                    table_name.Value = this.table_name;
                    cmd2.Parameters.Add(table_name);

                    SqlParameter field_name = new SqlParameter("@field_name", SqlDbType.NVarChar, 100);
                    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                    field_name.Value = this.field_name;
                    cmd2.Parameters.Add(field_name);

                    SqlParameter main_id = new SqlParameter("@main_id", SqlDbType.NVarChar, 100);
                    main_id.Value = this.main_id;
                    cmd2.Parameters.Add(main_id);

                    SqlParameter string1 = new SqlParameter("@string1", SqlDbType.NVarChar, 100);
                    string1.Value = txtString1.Text;
                    cmd2.Parameters.Add(string1);

                    SqlParameter string2 = new SqlParameter("@string2", SqlDbType.NVarChar, 100);
                    string2.Value = txtString2.Text;
                    cmd2.Parameters.Add(string2);

                    SqlParameter long1 = new SqlParameter("@long1", SqlDbType.NVarChar, 100);
                    long1.Value = txt_string1ST.SelectedValue != null ? txt_string1ST.SelectedValue : -1;
                    cmd2.Parameters.Add(long1);

                    SqlParameter long2 = new SqlParameter("@long2", SqlDbType.NVarChar, 100);
                    long2.Value = txt_string2ST.SelectedValue != null ? txt_string2ST.SelectedValue : -1;
                    cmd2.Parameters.Add(long2);

                    SqlParameter long5 = new SqlParameter("@long5", SqlDbType.NVarChar, 100);
                    long5.Value = long_5;
                    cmd2.Parameters.Add(long5);

                    SqlParameter string1ST = new SqlParameter("@long1ST", SqlDbType.NVarChar, 100);
                    try
                    {
                        string1ST.Value = txt_string1ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(string1ST);
                    }
                    catch
                    {
                        if (txt_string1ST.Text == "Chọn")
                        {
                            string1ST.Value = "";
                            cmd2.Parameters.Add(string1ST);
                        }
                        else
                        {
                            string1ST.Value = txt_string1ST.Text;
                            cmd2.Parameters.Add(string1ST);
                        }
                    }

                    SqlParameter string2ST = new SqlParameter("@long2ST", SqlDbType.NVarChar, 100);
                    try
                    {
                        string2ST.Value = txt_string2ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(string2ST);
                    }
                    catch
                    {
                        if(txt_string2ST.Text == "Chọn")
                        {
                            string2ST.Value = "";
                            cmd2.Parameters.Add(string2ST);
                        }
                        else
                        {
                            string2ST.Value = txt_string2ST.Text;
                            cmd2.Parameters.Add(string2ST);
                        }
                    }

                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();

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
                else
                {
                    // Chua co tao moi
                    SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[groundDeliveryRecordsMember] SET table_name = @table_name, field_name=@field_name, main_id=@main_id,string1=@string1,string2=@string2,long1=@long1,long2=@long2,long1ST=@long1ST,long2ST=@long2ST,long5=@long5 WHERE gid =  " + id_BSKQ, _Cn);

                    SqlParameter table_name = new SqlParameter("@table_name", SqlDbType.NVarChar, 255);
                    table_name.Value = this.table_name;
                    cmd2.Parameters.Add(table_name);

                    SqlParameter field_name = new SqlParameter("@field_name", SqlDbType.NVarChar, 100);
                    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                    field_name.Value = this.field_name;
                    cmd2.Parameters.Add(field_name);

                    SqlParameter main_id = new SqlParameter("@main_id", SqlDbType.NVarChar, 100);
                    main_id.Value = this.main_id;
                    cmd2.Parameters.Add(main_id);

                    SqlParameter string1 = new SqlParameter("@string1", SqlDbType.NVarChar, 100);
                    string1.Value = txtString1.Text;
                    cmd2.Parameters.Add(string1);

                    SqlParameter string2 = new SqlParameter("@string2", SqlDbType.NVarChar, 100);
                    string2.Value = txtString2.Text;
                    cmd2.Parameters.Add(string2);

                    SqlParameter long2 = new SqlParameter("@long2", SqlDbType.NVarChar, 100);
                    long2.Value = txt_string2ST.SelectedValue != null ? txt_string2ST.SelectedValue : -1;
                    cmd2.Parameters.Add(long2);

                    SqlParameter long1 = new SqlParameter("@long1", SqlDbType.NVarChar, 100);
                    long1.Value = txt_string1ST.SelectedValue != null ? txt_string1ST.SelectedValue : -1;
                    cmd2.Parameters.Add(long1);

                    SqlParameter string1ST = new SqlParameter("@long1ST", SqlDbType.NVarChar, 100);
                    try
                    {
                        string1ST.Value = txt_string1ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(string1ST);
                    }
                    catch
                    {
                        if (txt_string1ST.Text == "Chọn")
                        {
                            string1ST.Value = "";
                            cmd2.Parameters.Add(string1ST);
                        }
                        else
                        {
                            string1ST.Value = txt_string1ST.Text;
                            cmd2.Parameters.Add(string1ST);
                        }
                    }

                    SqlParameter string2ST = new SqlParameter("@long2ST", SqlDbType.NVarChar, 100);
                    try
                    {
                        string2ST.Value = txt_string2ST.Text.Split('-')[1];
                        cmd2.Parameters.Add(string2ST);
                    }
                    catch
                    {
                        if (txt_string2ST.Text == "Chọn")
                        {
                            string2ST.Value = "";
                            cmd2.Parameters.Add(string2ST);
                        }
                        else
                        {
                            string2ST.Value = txt_string2ST.Text;
                            cmd2.Parameters.Add(string2ST);
                        }
                    }


                    SqlParameter long5 = new SqlParameter("@long5", SqlDbType.NVarChar, 100);
                    long5.Value = long_5;
                    cmd2.Parameters.Add(long5);

                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();

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
            if(checkBox1.Checked == true)
            {
                long_5 = "1";
            }
            else
            {
                long_5 = "0";
            }
            btnLuuClicked = true;
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                btnLuuClicked = false;
                return;
            }
            //var checkLong5 = CheckLong5(table_name, idProgram);
            //if(checkLong5 == 1)
            //{
            //    long5 = "1";
            //}
            //else
            //{
            //    long5 = "0";
            //}
            var check = UpdateInfomation();
        }
        private int CheckLong5(string table_name, int idProgram)
        {
            int temp = 0;
            SqlCommandBuilder sqlCommand1 = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("Select * from groundDeliveryRecordsMember where table_name = N'{0}' and main_id = '{0}'", table_name, idProgram), _Cn);
            sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            if (datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in datatable.Rows)
                {
                    if (dr["long5"].ToString() == "1")
                    {
                        temp = 1;
                    }
                }
            }
            return temp;
        }
        private void FormThemmoiMember_Load(object sender, EventArgs e)
        {
            UtilsDatabase.LoadCBStaff(txt_string1ST, idProgram);
            UtilsDatabase.LoadCBGroup(txt_string2ST, "049");
            LoadData(id_BSKQ);
        }
        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlCommandBuilder sqlCommand1 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from groundDeliveryRecordsMember where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        txtString1.Text = dr["string1"].ToString();
                        txtString2.Text = dr["string2"].ToString();
                        //txt_string1ST.SelectedValue = (long.TryParse(dr["long1"].ToString(), out long long1) ? long1 : -1);
                        //txt_string2ST.SelectedValue = (long.TryParse(dr["long2ST"].ToString(), out long long2) ? long2 : -1);

                        if (long.TryParse(dr["long1"].ToString(), out long long1))
                        {
                            txt_string1ST.SelectedValue = long1;
                        }
                        else
                        {
                            txt_string1ST.SelectedValue = -1;
                        }
                        if (long.TryParse(dr["long2"].ToString(), out long long2))
                        {
                            txt_string2ST.SelectedValue = long2;
                        }
                        else
                        {
                            txt_string2ST.SelectedValue = -1;
                        }
                        if (dr["long5"].ToString() == "1")
                        {
                            checkBox1.Checked = true;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                        }
                        if(txtString1.Text == "")
                        {
                            txtString1.ReadOnly = true;
                        }
                        if (txtString2.Text == "")
                        {
                            txtString2.ReadOnly = true;
                        }
                    }
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void txt_string1ST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;

            //if (txt_string1ST.SelectedItem != null && txt_string1ST.Text != "Chọn")
            //{
            //    txtString1.ReadOnly = true;
            //    txtString2.ReadOnly = true;
            //    txtString1.Text = "";
            //    txtString2.Text = "";
            //    SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM Cecm_ProgramStaff where id = {0} and cecmProgramId = {1}", txt_string1ST.SelectedItem.ToString().Split('-')[0], idProgram), _Cn);
            //    sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            //    System.Data.DataTable datatableWard = new System.Data.DataTable();
            //    sqlAdapterWard.Fill(datatableWard);

            //    foreach (DataRow dr in datatableWard.Rows)
            //    {
            //        long_1 = txt_string1ST.SelectedItem.ToString().Split('-')[0];
            //        txt_string1ST.Text = dr["nameId"].ToString();
            //    }
            //}
            //else
            //{
            //    if (txt_string2ST.SelectedItem != null && txt_string2ST.Text != "Chọn")
            //    {
            //        txtString1.ReadOnly = true;
            //        txtString2.ReadOnly = true;
            //        txtString1.Text = "";
            //        txtString2.Text = "";
            //    }
            //    else
            //    {
            //        txtString1.ReadOnly = false;
            //        txtString2.ReadOnly = false;
            //    }
                
            //}
        }
        public string GetIdGroup(string indexSelect, string code)
        {
            if (string.IsNullOrEmpty(code))
                return string.Empty;

            SqlCommandBuilder sqlCommand = null;
            try
            {
                SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT * FROM cecm_department WHERE dvs_group_cd = '{0}'", code), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
                System.Data.DataTable datatableWard = new System.Data.DataTable();
                sqlAdapterWard.Fill(datatableWard);

                foreach (DataRow dr in datatableWard.Rows)
                {
                    if (indexSelect == dr["dvs_name"].ToString())
                        return dr["dvs_cd"].ToString();
                }
                return string.Empty;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return string.Empty;
            }
        }
        private void txt_string2ST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (txt_string2ST.SelectedItem != null && txt_string2ST.Text != "Chọn")
            //{
            //    txtString1.ReadOnly = true;
            //    txtString2.ReadOnly = true;
            //    txtString1.Text = "";
            //    txtString2.Text = "";
            //    if (txt_string2ST.SelectedItem.ToString() != "Chọn")
            //    {
            //        long_2 = GetIdGroup(txt_string2ST.SelectedItem.ToString(), "049");
            //    }
            //}
            //else
            //{
            //    if (txt_string1ST.SelectedItem != null && txt_string1ST.Text != "Chọn")
            //    {
            //        txtString1.ReadOnly = true;
            //        txtString2.ReadOnly = true;
            //        txtString1.Text = "";
            //        txtString2.Text = "";
            //    }
            //    else
            //    {
            //        txtString1.ReadOnly = false;
            //        txtString2.ReadOnly = false;
            //    }

            //}
        }

        private void txt_string1ST_Validating(object sender, CancelEventArgs e)
        {
            if (!btnLuuClicked)
            {
                return;
            }
            ComboBox A = txt_string1ST;
            TextBox B = txtString1;
            string strError = "Chưa nhập họ tên";

            if (A.Text == "" && B.Text =="" || A.Text == "Chọn" && B.Text == "")
            {
                if (B.Text == "")
                {
                    e.Cancel = true;
                    //comboBox_TenDA.Focus();
                    errorProvider1.SetError(A, strError);
                    return;
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(A, "");
            }
        }

        private void txt_string2ST_Validating(object sender, CancelEventArgs e)
        {
            if (!btnLuuClicked)
            {
                return;
            }
            ComboBox A = txt_string2ST;
            TextBox B = txtString2;
            string strError = "Chưa nhập chức vụ";

            if (A.Text == "" && B.Text == "" || A.Text == "Chọn" && B.Text == "")
            {
                if (B.Text == "")
                {
                    e.Cancel = true;
                    //comboBox_TenDA.Focus();
                    errorProvider1.SetError(A, strError);
                    return;
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(A, "");
            }
        }

        private void txtString2_Validating(object sender, CancelEventArgs e)
        {
            if (!btnLuuClicked)
            {
                return;
            }
            ComboBox A = txt_string2ST;
            TextBox B = txtString2;
            string strError = "Chưa nhập chức vụ";

            if (A.Text == "" && B.Text == "" || A.Text == "Chọn" && B.Text == "")
            {
                if (B.Text == "")
                {
                    e.Cancel = true;
                    //comboBox_TenDA.Focus();
                    errorProvider1.SetError(A, strError);
                    return;
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(A, "");
            }
        }

        private void txt_string1ST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(txt_string1ST.SelectedValue is long))
            {
                return;
            }
            if ((long)txt_string1ST.SelectedValue > 0)
            {
                txtString1.ReadOnly = true;
                txtString2.ReadOnly = true;
                txtString1.Text = "";
                txtString2.Text = "";
            }
            else
            {
                if (txt_string2ST.SelectedItem != null && txt_string2ST.Text != "Chọn")
                {
                    txtString1.ReadOnly = true;
                    txtString2.ReadOnly = true;
                    txtString1.Text = "";
                    txtString2.Text = "";
                }
                else
                {
                    txtString1.ReadOnly = false;
                    txtString2.ReadOnly = false;
                }

            }
        }

        private void txt_string2ST_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txt_string2ST.Text != "" && txt_string2ST.Text != "Chọn")
            {
                txtString1.ReadOnly = true;
                txtString2.ReadOnly = true;
                txtString1.Text = "";
                txtString2.Text = "";
            }
            else
            {
                if (txt_string1ST.Text != "" && txt_string1ST.Text != "Chọn")
                {
                    txtString1.ReadOnly = true;
                    txtString2.ReadOnly = true;
                    txtString1.Text = "";
                    txtString2.Text = "";
                }
                else
                {
                    txtString1.ReadOnly = false;
                    txtString2.ReadOnly = false;
                }

            }
        }
    }
}
