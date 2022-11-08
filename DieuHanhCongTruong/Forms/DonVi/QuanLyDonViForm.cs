using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class QuanLyDonViForm : Form
    {
        private SqlConnection _Cn = null;

        public QuanLyDonViForm()
        {
            _Cn = frmLoggin.sqlCon;

            InitializeComponent();
        }

        public string FindLinhVuc(int number)
        {
            string name = "031", retVal = string.Empty;
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                // Set value Xa
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT dvs_name FROM cecm_department WHERE dvs_group_cd = '{0}' AND dvs_value = {1}", name, number), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                foreach (DataRow dr in datatable.Rows)
                {
                    retVal = dr["dvs_name"].ToString();
                }
                return retVal;
            }
            catch
            {
                return string.Empty;
            }
        }

        private void LoadData()
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            if (DGVDonVi.Rows.Count != 0)
                DGVDonVi.Rows.Clear();

            //DGVDonVi.ColumnCount = 10;
            //DGVDonVi.Columns[0].HeaderText = "ID";
            //DGVDonVi.Columns[1].HeaderText = "Tên cơ quan, đơn vị, tổ chức";
            //DGVDonVi.Columns[2].HeaderText = "Địa chỉ";
            //DGVDonVi.Columns[3].HeaderText = "Giấy phép";
            //DGVDonVi.Columns[4].HeaderText = "Điện thoại";
            //DGVDonVi.Columns[5].HeaderText = "Fax";
            //DGVDonVi.Columns[6].HeaderText = "Email";
            //DGVDonVi.Columns[7].HeaderText = "Người đứng đầu";
            //DGVDonVi.Columns[8].HeaderText = "Chức vụ người đứng đầu";
            //DGVDonVi.Columns[9].HeaderText = "Lĩnh vực hoạt động";
            //DGVDonVi.Columns[0].Visible = false;
            //for (int i = 0; i < DGVDonVi.Columns.Count; i++)
            //{
            //    DGVDonVi.Columns[i].ReadOnly = true;
            //}

            //for (int i = 0; i < DGVDonVi.ColumnCount; i++)
            //{
            //    DGVDonVi.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    DGVDonVi.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //if (DGVDonVi.Rows != null && DGVDonVi.Rows.Count != 0)
            //    DGVDonVi.Rows[0].Selected = true;
            //DGVDonVi.AllowUserToAddRows = false;
            //DGVDonVi.BackgroundColor = Color.White;
            //DGVDonVi.RowHeadersVisible = false;
            //DGVDonVi.AllowUserToResizeColumns = false;
            //DGVDonVi.AllowUserToResizeRows = false;
            //DGVDonVi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            System.Data.DataTable datatable = new System.Data.DataTable();
            datatable.Clear();
            string sql = "SELECT * FROM cert_department " +
            "where LOWER(name) like @search " +
            "or LOWER(name_english) like @search " +
            "or LOWER(code) like @search";
            sqlAdapter = new SqlDataAdapter(sql, _Cn);
            sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "search",
                Value = "%" + tbSearch.Text.ToLower() + "%", 
                SqlDbType = SqlDbType.NVarChar,
                Size = 100
            });
            sqlAdapter.Fill(datatable);

            if (datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in datatable.Rows)
                {
                    string linhVucHoatDong = string.Empty;
                    if (AppUtils.IsNumber(dr["action_type"].ToString()))
                    {
                        int action_type = int.Parse(dr["action_type"].ToString());
                        linhVucHoatDong = FindLinhVuc(action_type);
                    }
                    int i = DGVDonVi.Rows.Add(
                        dr["id"].ToString(),
                        dr["name"].ToString(),
                        dr["address"].ToString(),
                        dr["phone"].ToString(),
                        dr["fax"].ToString(),
                        dr["email"].ToString(),
                        dr["master"].ToString(),
                        dr["pos_master"].ToString()
                        );
                    DGVDonVi.Rows[i].Tag = dr["id"].ToString();
                }
            }
        }

        private void QuanLyDonViForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DeleteData()
        {
            if (DGVDonVi.RowCount == 0)
                return;

            if (MessageBox.Show("Xác nhận xóa dữ liệu", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                for (int i = 0; i < DGVDonVi.SelectedRows.Count; i++)
                {
                    try
                    {
                        int idVal = -1;
                        int rowindex = DGVDonVi.SelectedRows[i].Index;
                        if (DGVDonVi.Rows[rowindex].Cells[0].Value != null && AppUtils.IsNumber(DGVDonVi.Rows[rowindex].Cells[0].Value.ToString()))
                            idVal = int.Parse(DGVDonVi.Rows[rowindex].Cells[0].Value.ToString());

                        SqlTransaction transaction = _Cn.BeginTransaction();
                        try
                        {
                            SqlCommand cmd = new SqlCommand(string.Format("USE [{0}] DELETE FROM cert_department WHERE cert_department.id = {1};", frmLoggin.databaseName, idVal), _Cn, transaction);
                            int susscec1 = cmd.ExecuteNonQuery();
                            SqlCommand cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM object_relation WHERE id_first = {1} AND (code_relation = '{2}' OR code_relation = '{3}');", frmLoggin.databaseName, idVal, ThemMoiDonViThucHienFormNew.FIELD, ThemMoiDonViThucHienFormNew.MONEY_SOURCE), _Cn, transaction);
                            cmd2.ExecuteNonQuery();
                            if (susscec1 < 0)
                            {
                                MessageBox.Show(string.Format("Không thể xóa"));
                                return;
                            }
                        }catch(Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show(ex.Message);
                            return;
                        }
                        
                        transaction.Commit();

                        DGVDonVi.Rows.RemoveAt(DGVDonVi.SelectedRows[i].Index);
                    }
                    catch (System.Exception ex)
                    {
                        
                    }
                }
            }
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            Color.FromArgb(40, 167, 69);
            ThemMoiDonViThucHienFormNew frm = new ThemMoiDonViThucHienFormNew(int.MinValue);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            EditDonVi();
        }

        private void EditDonVi()
        {
            int idVal = int.MinValue;
            for (int i = 0; i < DGVDonVi.SelectedRows.Count; i++)
            {
                try
                {
                    int rowindex = DGVDonVi.SelectedRows[i].Index;
                    if (DGVDonVi.Rows[rowindex].Cells[0].Value != null && AppUtils.IsNumber(DGVDonVi.Rows[rowindex].Cells[0].Value.ToString()))
                        idVal = int.Parse(DGVDonVi.Rows[rowindex].Cells[0].Value.ToString());
                }
                catch
                {
                }
            }

            ThemMoiDonViThucHienFormNew frm = new ThemMoiDonViThucHienFormNew(idVal);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void DGVDonVi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditDonVi();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DGVDonVi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = DGVDonVi.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == cotSua.Index && e.RowIndex >= 0)
            {
                ThemMoiDonViThucHienFormNew frm = new ThemMoiDonViThucHienFormNew(id);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
            else if (e.ColumnIndex == cotXoa.Index && e.RowIndex >= 0)
            {
                if (DGVDonVi.RowCount == 0)
                    return;

                if (MessageBox.Show("Xác nhận xóa dữ liệu", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //for (int i = 0; i < DGVDonVi.SelectedRows.Count; i++)
                    //{
                        try
                        {
                            //int idVal = -1;
                            //int rowindex = DGVDonVi.SelectedRows[i].Index;
                            //if (DGVDonVi.Rows[rowindex].Cells[0].Value != null && AppUtils.IsNumber(DGVDonVi.Rows[rowindex].Cells[0].Value.ToString()))
                            //    idVal = int.Parse(DGVDonVi.Rows[rowindex].Cells[0].Value.ToString());

                            SqlTransaction transaction = _Cn.BeginTransaction();
                            try
                            {
                                SqlCommand cmd = new SqlCommand(string.Format("USE [{0}] DELETE FROM cert_department WHERE cert_department.id = {1};", frmLoggin.databaseName, id), _Cn, transaction);
                                int susscec1 = cmd.ExecuteNonQuery();
                                SqlCommand cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM object_relation WHERE id_first = {1} AND (code_relation = '{2}' OR code_relation = '{3}');", frmLoggin.databaseName, id, ThemMoiDonViThucHienFormNew.FIELD, ThemMoiDonViThucHienFormNew.MONEY_SOURCE), _Cn, transaction);
                                cmd2.ExecuteNonQuery();
                                if (susscec1 < 0)
                                {
                                    MessageBox.Show(string.Format("Không thể xóa"));
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show(ex.Message);
                                return;
                            }

                            transaction.Commit();

                            DGVDonVi.Rows.RemoveAt(e.RowIndex);
                        }
                        catch (System.Exception ex)
                        {
                            
                        }
                    //}
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}