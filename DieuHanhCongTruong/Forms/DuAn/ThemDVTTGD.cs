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

namespace VNRaPaBomMin
{
    public partial class ThemDVTTGD : Form
    {
        private long gid;
        private long cecm_program_id;
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;
        private bool btnLuuClicked = false;

        public ThemDVTTGD(long cecm_program_id, long gid = -1)
        {
            InitializeComponent();
            this.cecm_program_id = cecm_program_id;
            this.gid = gid;
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            if (_ExtraInfoConnettion == null)
                return;
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThemDVTTGD_Load(object sender, EventArgs e)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            DataTable datatable = new DataTable();
            string sql_oluoi =
                "select id, name from cert_department";
            //frmLoggin.sqlCon.Open();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql_oluoi, cn);
            sqlAdapter.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            SqlCommandBuilder sqlCommand = new SqlCommandBuilder(sqlAdapter);
            //frmLoggin.sqlCon.BeginTransaction();
            sqlAdapter.Fill(datatable);
            DataRow dr = datatable.NewRow();
            dr["id"] = -1;
            dr["name"] = "- Chưa chọn đơn vị -";
            datatable.Rows.InsertAt(dr, 0);
            cbDept.DataSource = datatable;
            cbDept.DisplayMember = "name";
            cbDept.ValueMember = "id";
            if (gid > 0)
            {
                this.Text = "Cập nhật đơn vị";
                List<DataRow> drs = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "dept_tham_gia", "gid", gid.ToString());
                if (drs.Count > 0)
                {
                    DataRow dr_ = drs[0];
                    if(long.TryParse(dr_["dept_id"].ToString(), out long dept_id))
                    {
                        cbDept.SelectedValue = dept_id;
                    }
                    //cbDept.SelectedValue = long.Parse(dr_["dept_id"].ToString());
                    tbDept_other.Text = dr_["dept_other"].ToString();
                    tbEmail.Text = dr_["email"].ToString();
                    tbAddress.Text = dr_["address"].ToString();
                    tbPhone.Text = dr_["phone"].ToString();
                    tbFax.Text = dr_["fax"].ToString();
                    tbDeptHead.Text = dr_["head"].ToString();
                    tbDeptHeadPos.Text = dr_["head_pos"].ToString();
                    tbHeadPhone.Text = dr_["head_phone"].ToString();
                    tbHeadEmail.Text = dr_["head_email"].ToString();
                    tbDocNumber.Text = dr_["doc_number"].ToString();
                    if (DateTime.TryParse(dr_["doc_date"].ToString(), out DateTime date))
                    {
                        dtpDocDate.Value = date;
                    }
                    tbDocPerson.Text = dr_["doc_person"].ToString();
                    tbDocFiles.Text = dr_["doc_file"].ToString();
                }
            }
            else
            {
                this.Text = "Thêm mới đơn vị";
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            btnLuuClicked = true;
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                btnLuuClicked = false;
                return;
            }
            if (gid > 0)
            {
                string sql =
                    "UPDATE dept_tham_gia SET " +
                    "dept_id = @dept_id, " +
                    "dept_other = @dept_other, " +
                    "phone = @phone, " +
                    "email = @email, " +
                    "address = @address, " +
                    "fax = @fax, " +
                    "head = @head, " +
                    "head_pos = @head_pos, " +
                    "head_phone = @head_phone, " +
                    "head_email = @head_email, " +
                    "doc_number = @doc_number, " +
                    "doc_date = @doc_date, " +
                    "doc_person = @doc_person, " +
                    "doc_file = @doc_file, " +
                    "cecm_program_id = @cecm_program_id " +
                    "WHERE gid = @gid";
                SqlCommand cmd = new SqlCommand(sql, frmLoggin.sqlCon);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "gid", gid.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", cbDept.SelectedValue.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_other", tbDept_other.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", tbPhone.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", tbEmail.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", tbAddress.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "fax", tbFax.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head", tbDeptHead.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_pos", tbDeptHeadPos.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_phone", tbHeadPhone.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_email", tbHeadEmail.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", tbDocNumber.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", dtpDocDate);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person", tbDocPerson.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_file", tbDocFiles.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", cecm_program_id + "", false);
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                //cmd.Transaction.Commit();
                int temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                }
            }
            else
            {
                string sql =
                    "INSERT INTO dept_tham_gia(" +
                    "dept_id, dept_other, phone, email, address, fax, head, head_pos, head_phone, head_email, doc_number, doc_date, doc_person, doc_file, cecm_program_id, table_name) " +
                    "VALUES(" +
                    "@dept_id, @dept_other, @phone, @email, @address, @fax, @head, @head_pos, @head_phone, @head_email, @doc_number, @doc_date, @doc_person, @doc_file, @cecm_program_id, @table_name) ";
                SqlCommand cmd = new SqlCommand(sql, frmLoggin.sqlCon);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", cbDept.SelectedValue.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_other", tbDept_other.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", tbPhone.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", tbEmail.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", tbAddress.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "fax", tbFax.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head", tbDeptHead.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_pos", tbDeptHeadPos.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_phone", tbHeadPhone.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_email", tbHeadEmail.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", tbDocNumber.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", dtpDocDate);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person", tbDocPerson.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_file", tbDocFiles.Text, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", cecm_program_id + "", false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "table_name", TableName.TUYEN_TRUYEN_GIAO_DUC, false);
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                //cmd.Transaction.Commit();
                int temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    MessageBox.Show("Thêm mới dữ liệu thành công... ", "Thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm mới dữ liệu không thành công ", "Lỗi");
                }
            }
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy((int)cecm_program_id);
            if (string.IsNullOrEmpty(filePath) == false)
                tbDocFiles.Text = filePath;
        }

        private void tbDocFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp((int)cecm_program_id), tbDocFiles.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void cbDept_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cbDept.SelectedValue is long))
            {
                return;
            }
            if ((long)cbDept.SelectedValue > 0)
            {
                tbDept_other.Enabled = false;
                tbDept_other.Text = "";
                List<DataRow> drs = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "cert_department", "id", ((long)cbDept.SelectedValue).ToString());
                if (drs.Count > 0)
                {
                    DataRow dr_ = drs[0];
                    //cbDept.SelectedValue = long.Parse(dr_["dept_id"].ToString());
                    //tbDept_other.Text = dr_["dept_other"].ToString();
                    tbEmail.Text = dr_["email"].ToString();
                    tbEmail.Enabled = false;
                    tbAddress.Text = dr_["address"].ToString();
                    tbAddress.Enabled = false;
                    tbPhone.Text = dr_["phone"].ToString();
                    tbPhone.Enabled = false;
                    tbFax.Text = dr_["fax"].ToString();
                    tbFax.Enabled = false;
                    tbDeptHead.Text = dr_["master"].ToString();
                    tbDeptHead.Enabled = false;
                    tbDeptHeadPos.Text = dr_["pos_master"].ToString();
                    tbDeptHeadPos.Enabled = false;
                    tbHeadPhone.Text = dr_["head_phone"].ToString();
                    tbHeadPhone.Enabled = false;
                    tbHeadEmail.Text = dr_["head_email"].ToString();
                    tbHeadEmail.Enabled = false;
                    //tbHeadPhone.Text = "";
                    //tbHeadEmail.Text = "";
                    //tbDocNumber.Text = "";
                    //dtpDocDate.Value = DateTime.Now;
                    //tbDocPerson.Text = "";
                    //tbDocFiles.Text = "";
                }
            }
            else
            {
                tbDept_other.Enabled = true;
                tbDept_other.Text = "";
                tbEmail.Text = "";
                tbEmail.Enabled = true;
                tbAddress.Text = "";
                tbAddress.Enabled = true;
                tbPhone.Text = "";
                tbPhone.Enabled = true;
                tbFax.Text = "";
                tbFax.Enabled = true;
                tbDeptHead.Text = "";
                tbDeptHead.Enabled = true;
                tbDeptHeadPos.Text = "";
                tbDeptHeadPos.Enabled = true;
                tbHeadEmail.Text = "";
                tbHeadEmail.Enabled = true;
                tbHeadPhone.Text = "";
                tbHeadPhone.Enabled = true;
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.EMAIL_REGEX.IsMatch(tbEmail.Text) || tbEmail.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbEmail, "");
            }
            else
            {
                e.Cancel = true;
                tbEmail.Focus();
                errorProvider.SetError(tbEmail, "Email chưa đúng định dạng");
            }
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.PHONE_REGEX.IsMatch(tbPhone.Text) || tbPhone.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbPhone, "");
            }
            else
            {
                e.Cancel = true;
                tbPhone.Focus();
                errorProvider.SetError(tbPhone, "SĐT chưa đúng định dạng");
            }
        }

        private void tbHeadPhone_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.PHONE_REGEX.IsMatch(tbHeadPhone.Text) || tbHeadPhone.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbHeadPhone, "");
            }
            else
            {
                e.Cancel = true;
                tbHeadPhone.Focus();
                errorProvider.SetError(tbHeadPhone, "SĐT chưa đúng định dạng");
            }
        }

        private void tbHeadEmail_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.EMAIL_REGEX.IsMatch(tbHeadEmail.Text) || tbHeadEmail.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbHeadEmail, "");
            }
            else
            {
                e.Cancel = true;
                tbHeadEmail.Focus();
                errorProvider.SetError(tbHeadEmail, "Email chưa đúng định dạng");
            }
        }

        private void cbDept_Validating(object sender, CancelEventArgs e)
        {
            if (!btnLuuClicked)
            {
                return;
            }
            if ((long)cbDept.SelectedValue > 0 || tbDept_other.Text.Trim() != "")
            {
                e.Cancel = false;
                errorProvider.SetError(cbDept, "");
            }
            else
            {
                e.Cancel = true;
                cbDept.Focus();
                errorProvider.SetError(cbDept, "Chưa chọn đơn vị");
            }
        }
    }
}
