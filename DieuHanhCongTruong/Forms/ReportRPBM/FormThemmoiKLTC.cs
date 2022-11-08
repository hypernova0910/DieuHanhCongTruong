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
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Properties;

namespace DieuHanhCongTruong
{
    public partial class FormThemmoiKLTC : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string field_name = "";
        public string table_name = "";
        public int main_id = 0;

        public FormThemmoiKLTC(int i, int main_id, string field_name, string table_name)
        {
            id_BSKQ = i;
            this.field_name = field_name;
            this.table_name = table_name;
            this.main_id = main_id;
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
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [dbo].[groundDeliveryRecordsMember] ([table_name],[field_name],[main_id],[string1],[string2],[string3],[string4],[string5],[string6]) Values (@table_name,@field_name,@main_id,@string1,@string2,@string3,@string4,@string5,@string6)", _Cn);

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

                    SqlParameter string3 = new SqlParameter("@string3", SqlDbType.NVarChar, 100);
                    string3.Value = txtString3.Text;
                    cmd2.Parameters.Add(string3);

                    SqlParameter string4 = new SqlParameter("@string4", SqlDbType.NVarChar, 100);
                    string4.Value = txtString4.Text;
                    cmd2.Parameters.Add(string4);

                    SqlParameter string5 = new SqlParameter("@string5", SqlDbType.NVarChar, 100);
                    string5.Value = txtString5.Text;
                    cmd2.Parameters.Add(string5);

                    SqlParameter string6 = new SqlParameter("@string6", SqlDbType.NVarChar, 100);
                    string6.Value = txtString6.Text;
                    cmd2.Parameters.Add(string6);

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
                    SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[groundDeliveryRecordsMember] SET table_name = @table_name, field_name=@field_name, main_id=@main_id,string1=@string1,string2=@string2,string3=@string3,string4=@string4,string5=@string5,string6=@string6 WHERE gid =  " + id_BSKQ, _Cn);

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

                    SqlParameter string3 = new SqlParameter("@string3", SqlDbType.NVarChar, 100);
                    string3.Value = txtString3.Text;
                    cmd2.Parameters.Add(string3);

                    SqlParameter string4 = new SqlParameter("@string4", SqlDbType.NVarChar, 100);
                    string4.Value = txtString4.Text;
                    cmd2.Parameters.Add(string4);

                    SqlParameter string5 = new SqlParameter("@string5", SqlDbType.NVarChar, 100);
                    string5.Value = txtString5.Text;
                    cmd2.Parameters.Add(string5);

                    SqlParameter string6 = new SqlParameter("@string6", SqlDbType.NVarChar, 100);
                    string6.Value = txtString6.Text;
                    cmd2.Parameters.Add(string6);

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
            var check = UpdateInfomation();
        }

        private void FormThemmoiMember_Load(object sender, EventArgs e)
        {
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
                        txtString3.Text = dr["string3"].ToString();
                        txtString4.Text = dr["string4"].ToString();
                        txtString5.Text = dr["string5"].ToString();
                        txtString6.Text = dr["string6"].ToString();

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

        private void txtString1_Validating(object sender, CancelEventArgs e)
        {
            if (txtString1.Text == "")
            {
                e.Cancel = true;
                //comboBox_TenDA.Focus();
                errorProvider1.SetError(txtString1, "Chưa nhập tên công việc");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtString1, "");
            }
        }
    }
}
