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
    public partial class FormThemmoiKetQuaBMVN : Form
    {
        public SqlConnection _Cn = null;
        public int id_BSKQ = 0;
        public string field_name = "";
        public string table_name = "";
        public int main_id = 0;
        private bool isLuuClicked = false;

        public FormThemmoiKetQuaBMVN(int i, int main_id, string field_name, string table_name)
        {
            id_BSKQ = i;
            this.field_name = field_name;
            this.table_name = table_name;
            this.main_id = main_id;
            _Cn = _Cn = frmLoggin.sqlCon;
            InitializeComponent();
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
                        "([table_name],[field_name],[main_id],long1,long2,[string1],[string2],[string3],[string4]) " +
                        "Values " +
                        "(@table_name,@field_name,@main_id,@long1,@long2,@string1,@string2,@string3,@string4)", _Cn);


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
                        "string3=@string3," +
                        "string4=@string4 " +
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
                long1.Value = cbLoaiBMVN.SelectedValue;
                cmd2.Parameters.Add(long1);

                SqlParameter long2 = new SqlParameter("@long2", SqlDbType.BigInt);
                long2.Value = nudSoLuong.Value;
                cmd2.Parameters.Add(long2);

                SqlParameter string1 = new SqlParameter("@string1", SqlDbType.NVarChar, 100);
                string1.Value = tbBMVNKhac.Text;
                cmd2.Parameters.Add(string1);

                SqlParameter string2 = new SqlParameter("@string2", SqlDbType.NVarChar, 100);
                string2.Value = tbKyHieu.Text;
                cmd2.Parameters.Add(string2);

                SqlParameter string3 = new SqlParameter("@string3", SqlDbType.NVarChar, 100);
                string3.Value = tbDonVi.Text;
                cmd2.Parameters.Add(string3);

                SqlParameter string4 = new SqlParameter("@string4", SqlDbType.NVarChar, 100);
                string4.Value = tbGhiChu.Text;
                cmd2.Parameters.Add(string4);

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
                MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                //MyLogger.Log("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                return false;
            }
        }

        private void FormThemmoiKetQuaBMVN_Load(object sender, EventArgs e)
        {
            UtilsDatabase.buildComboboxMST(cbLoaiBMVN, "001");
            //UtilsDatabase.buildCombobox(cbLoaiBMVN, "SELECT dvs_name, dvs_value FROM mst_division WHERE dvs_group_cd = '001'", "dvs_value", "dvs_name");
            LoadData(id_BSKQ);
        }

        private void LoadData(int i)
        {
            if (i != 0)
            {
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("Select * from groundDeliveryRecordsMember where gid = {0}", id_BSKQ), _Cn);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        cbLoaiBMVN.SelectedValue = long.TryParse(dr["long1"].ToString(), out long long1) ? long1 : -1;
                        tbBMVNKhac.Text = dr["string1"].ToString();
                        tbKyHieu.Text = dr["string2"].ToString();
                        tbDonVi.Text = dr["string3"].ToString();
                        nudSoLuong.Value = decimal.TryParse(dr["long2"].ToString(), out decimal long2) ? long2 : 0;
                        tbGhiChu.Text = dr["string4"].ToString();
                    }
                }
            }
        }

        private void cbLoaiBMVN_Validating(object sender, CancelEventArgs e)
        {
            if (!isLuuClicked)
            {
                return;
            }
            if(cbLoaiBMVN.SelectedValue is long)
            {
                if ((long)cbLoaiBMVN.SelectedValue > 0 && (long)cbLoaiBMVN.SelectedValue != 14)
                {
                    e.Cancel = false;
                    errorProvider1.SetError(cbLoaiBMVN, "");
                }
                else
                {
                    if(tbBMVNKhac.Text.Trim() == "")
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(cbLoaiBMVN, "Chưa chọn loại BMVN");
                    }
                    else
                    {
                        e.Cancel = false;
                        errorProvider1.SetError(cbLoaiBMVN, "");
                    }
                }
            }
        }

        private void cbLoaiBMVN_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbLoaiBMVN.SelectedValue is long)
            {
                if((long)cbLoaiBMVN.SelectedValue > 0 && (long)cbLoaiBMVN.SelectedValue != 14)
                {
                    tbBMVNKhac.ReadOnly = true;
                    tbBMVNKhac.Text = "";
                }
                else
                {
                    tbBMVNKhac.ReadOnly = false;
                }
            }
        }
    }
}
