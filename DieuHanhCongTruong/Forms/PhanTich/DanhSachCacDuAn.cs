using Charts;
using DieuHanhCongTruong.Forms.Account;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class DanhSachCacDuAn : Form
    {
        private SqlConnection cn = null;
        public string _MaMay = "";
        private long _idDuAn = 0;

        private void DGVThongTin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            _MaMay = DGVThongTin.Rows[e.RowIndex].Cells[cotMACid.Index].Value.ToString();
            if (!string.IsNullOrEmpty(_MaMay))
            {
                //this.DialogResult = DialogResult.OK;
                TuTruongForm frm = new TuTruongForm(_MaMay);
                frm.ShowDialog();
            }
        }

        public DanhSachCacDuAn(long idDuAn)
        {
            InitializeComponent();

            _idDuAn = idDuAn;
        }

        private void DanhSachCacDuAn_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            DataSet dataset = new DataSet();
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("Select mac_id, code FROM Cecm_ProgramMachineBomb WHERE Cecm_ProgramMachineBomb.cecm_program_id = {0}", _idDuAn), frmLoggin.sqlCon);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);

            //DGVThongTin.DataSource = datatable;
            foreach(DataRow dr in datatable.Rows)
            {
                DGVThongTin.Rows.Add(dr["mac_id"].ToString(), dr["code"].ToString());
            }

            //SqlCommandBuilder sqlCommand = null;
            //SqlDataAdapter sqlAdapter = null;
            //DataTable datatable = new DataTable();
            //sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM cecm_program"), cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapter);
            //sqlAdapter.Fill(datatable);
            //DGVThongTin.DataSource = datatable;

            //for (int i = 0; i < DGVThongTin.ColumnCount; i++)
            //{
            //    DGVThongTin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    DGVThongTin.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //if (DGVThongTin.Rows != null && DGVThongTin.Rows.Count != 0)
            //    DGVThongTin.Rows[0].Selected = true;
            //DGVThongTin.AllowUserToAddRows = false;
            //DGVThongTin.BackgroundColor = Color.White;
            //DGVThongTin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DGVThongTin.RowHeadersVisible = false;
            //DGVThongTin.AllowUserToResizeRows = false;
        }
    }
}