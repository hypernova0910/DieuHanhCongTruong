using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
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

namespace DieuHanhCongTruong.ReportRPBM
{
    public partial class FormThemmoiKLTCNew : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string field_name = "";
        public string table_name = "";
        public int main_id = 0;
        private bool isLuuClicked = false;

        public FormThemmoiKLTCNew(int i, int main_id, string field_name, string table_name)
        {
            id_BSKQ = i;
            this.field_name = field_name;
            this.table_name = table_name;
            this.main_id = main_id;
            _Cn = _Cn = frmLoggin.sqlCon;
            InitializeComponent();
        }

        private void FormThemmoiKLTCNew_Load(object sender, EventArgs e)
        {
            UtilsDatabase.buildCombobox(cbDauViec, "SELECT id, the_content FROM NoiDungCongViec WHERE the_level = 1 AND type = 1", "id", "the_content");
            LoadData(id_BSKQ);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            isLuuClicked = true;
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                UpdateInfomation();
            }
            isLuuClicked = false;
        }

        private void cbDauViec_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbDauViec.SelectedValue is long)
            {
                UtilsDatabase.buildCombobox(cbCongViec, "SELECT id, the_content FROM NoiDungCongViec WHERE the_level = 2 AND type = 1 and parent_id = " + cbDauViec.SelectedValue, "id", "the_content");
            }
        }

        private void cbCongViec_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCongViec.SelectedValue is long)
            {
                if ((long)cbCongViec.SelectedValue > 0)
                {
                    tbCongViecKhac.Text = "";
                    tbCongViecKhac.ReadOnly = true;
                }
                else
                {
                    tbCongViecKhac.ReadOnly = false;
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
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from groundDeliveryRecordsMember where gid = {0}", id_BSKQ), _Cn);
                sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        cbDauViec.SelectedValue = long.TryParse(dr["long1"].ToString(), out long long1) ? long1 : -1;
                        cbCongViec.SelectedValue = long.TryParse(dr["long2"].ToString(), out long long2) ? long2 : -1;
                        tbCongViecKhac.Text = dr["string1"].ToString();
                        txtString2.Text = dr["string2"].ToString();
                        nudKLHD.Value = decimal.TryParse(dr["double1"].ToString(), out decimal double1) ? double1 : 0;
                        nudKLTC.Value = decimal.TryParse(dr["double2"].ToString(), out decimal double2) ? double2 : 0;
                        nudKLNT.Value = decimal.TryParse(dr["double3"].ToString(), out decimal double3) ? double3 : 0;
                    }
                }
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
                SqlCommand cmd2;
                if (id_BSKQ == 0)
                {
                    // Chua co tao moi
                    cmd2 = new SqlCommand(
                        "INSERT INTO [dbo].[groundDeliveryRecordsMember] " +
                        "([table_name],[field_name],[main_id],long1,long2,[string1],[string2],[double1],[double2],double3) " +
                        "Values " +
                        "(@table_name,@field_name,@main_id,@long1,@long2,@string1,@string2,@double1,@double2,@double3)", _Cn);


                }
                else
                {
                    // Chua co tao moi
                    cmd2 = new SqlCommand("UPDATE [dbo].[groundDeliveryRecordsMember] SET " +
                        "table_name = @table_name, " +
                        "field_name=@field_name, " +
                        "main_id=@main_id," +
                        "long1=@long1," +
                        "long2=@long2," +
                        "string1=@string1," +
                        "string2=@string2," +
                        "double1=@double1," +
                        "double2=@double2, " +
                        "double3=@double3 " +
                        "WHERE gid =  " + id_BSKQ, _Cn);
                }

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

                SqlParameter long1 = new SqlParameter("@long1", SqlDbType.BigInt);
                long1.Value = cbDauViec.SelectedValue;
                cmd2.Parameters.Add(long1);

                SqlParameter long2 = new SqlParameter("@long2", SqlDbType.BigInt);
                long2.Value = cbCongViec.SelectedValue;
                cmd2.Parameters.Add(long2);

                SqlParameter string1 = new SqlParameter("@string1", SqlDbType.NVarChar, 200);
                string1.Value = tbCongViecKhac.Text;
                cmd2.Parameters.Add(string1);

                SqlParameter string2 = new SqlParameter("@string2", SqlDbType.NVarChar, 200);
                string2.Value = txtString2.Text;
                cmd2.Parameters.Add(string2);

                SqlParameter double1 = new SqlParameter("@double1", SqlDbType.Float);
                double1.Value = nudKLHD.Value;
                cmd2.Parameters.Add(double1);

                SqlParameter double2 = new SqlParameter("@double2", SqlDbType.Float);
                double2.Value = nudKLTC.Value;
                cmd2.Parameters.Add(double2);

                SqlParameter double3 = new SqlParameter("@double3", SqlDbType.Float);
                double3.Value = nudKLNT.Value;
                cmd2.Parameters.Add(double3);

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
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return false;
            }
        }

        private void cbDauViec_Validating(object sender, CancelEventArgs e)
        {
            if(cbDauViec.SelectedValue is long)
            {
                if((long)cbDauViec.SelectedValue > 0)
                {
                    e.Cancel = false;
                    errorProvider1.SetError(cbDauViec, "");
                }
                else
                {
                    e.Cancel = true;
                    errorProvider1.SetError(cbDauViec, "Chưa chọn đầu việc");
                }
            }
        }

        private void cbCongViec_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if (cbCongViec.SelectedValue is long)
            {
                if ((long)cbCongViec.SelectedValue > 0 && (long)cbCongViec.SelectedValue != 14)
                {
                    e.Cancel = false;
                    errorProvider1.SetError(cbCongViec, "");
                }
                else
                {
                    if (tbCongViecKhac.Text.Trim() == "")
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(cbCongViec, "Chưa chọn tên công việc");
                    }
                    else
                    {
                        e.Cancel = false;
                        errorProvider1.SetError(cbCongViec, "");
                    }
                }
            }
        }
    }
}
