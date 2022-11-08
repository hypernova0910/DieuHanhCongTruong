using DieuHanhCongTruong.Common;
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

namespace VNRaPaBomMin
{
    public partial class DeptTree : Form
    {
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;
        private ThemMoiDonViThucHienFormNew parent_form;

        public DeptTree(ThemMoiDonViThucHienFormNew parent_form)
        {
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            this.parent_form = parent_form;
            InitializeComponent();
        }

        private void DeptTree_Load(object sender, EventArgs e)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            DataTable datatable = new DataTable();
            datatable.Clear();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("SELECT id, name FROM cert_department WHERE parent_id is NULL or parent_id < 0", cn);
            sqlAdapter.Fill(datatable);

            foreach (DataRow dr in datatable.Rows)
            {
                TreeNode node = treeView1.Nodes.Add(dr["name"].ToString());
                node.Tag = long.Parse(dr["id"].ToString());
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.Checked)
            {
                long id = (long)e.Node.Tag;
                SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
                DataTable datatable = new DataTable();
                datatable.Clear();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter("SELECT id, name FROM cert_department WHERE parent_id = " + id, cn);
                sqlAdapter.Fill(datatable);

                foreach (DataRow dr in datatable.Rows)
                {
                    TreeNode node = e.Node.Nodes.Add(dr["name"].ToString());
                    node.Tag = long.Parse(dr["id"].ToString());
                }
                e.Node.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            parent_form.ParentID = (long)treeView1.SelectedNode.Tag;
            parent_form.tbDeptParent.Text = treeView1.SelectedNode.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
