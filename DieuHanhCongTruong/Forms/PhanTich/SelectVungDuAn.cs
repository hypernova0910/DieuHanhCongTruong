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

namespace DieuHanhCongTruong.Forms.PhanTich
{
    public partial class SelectVungDuAn : Form
    {
        private long idDA;

        public long idKV;

        public SelectVungDuAn(long idDA)
        {
            InitializeComponent();
            this.idDA = idDA;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SelectVungDuAn_Load(object sender, EventArgs e)
        {
            tbDuAn.Text = MyMainMenu2.tenDADH;
            try
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name, polygongeomst FROM cecm_program_area_map where cecm_program_id = " + idDA), frmLoggin.sqlCon);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                DataRow dr2 = datatable.NewRow();
                dr2["id"] = -1;
                dr2["name"] = "Chọn vùng dự án";
                datatable.Rows.InsertAt(dr2, 0);
                cbKhuVuc.DataSource = datatable;
                cbKhuVuc.DisplayMember = "name";
                cbKhuVuc.ValueMember = "id";

            }
            catch (Exception ex)
            {
            }
        }

        private void cbKhuVuc_Validating(object sender, CancelEventArgs e)
        {
            if((long)cbKhuVuc.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(cbKhuVuc, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(cbKhuVuc, "Chưa chọn vùng dự án");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            idKV = (long)cbKhuVuc.SelectedValue;
            this.DialogResult = DialogResult.OK;
        }
    }
}
