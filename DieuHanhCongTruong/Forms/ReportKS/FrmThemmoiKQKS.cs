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
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class FrmThemmoiKQKS : Form
    {
        //public SqlConnection _cn.Connection as SqlConnection = null;
        public int id_kqks = 0;
        public long idDA = 0;
        public int idVungCS = 0;
        private long idVung = 0;
        private DataGridViewRow dataGridViewRow;
        private ConnectionWithExtraInfo _cn;

        public FrmThemmoiKQKS(DataGridViewRow dataGridViewRow, long idDa)
        {
            //id_kqks = id;
            this.dataGridViewRow = dataGridViewRow;
            idDA = idDa;
            //_cn.Connection as SqlConnection = _cn.Connection as SqlConnection = frmLoggin.sqlCon;
            _cn = UtilsDatabase._ExtraInfoConnettion;
            InitializeComponent();
        }
        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
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
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            //bool success = UpdateInfomation();
            CecmReportDailyKQKS kqks = dataGridViewRow.Tag as CecmReportDailyKQKS;
            //idVung = kqks.area_id;
            kqks.area_id = (long)comboBox_Vungcoso.SelectedValue;
            kqks.Ketqua = (long)comboBox_KQ.SelectedValue;
            kqks.DientichchuaKS = (double)nudDTChuaKS.Value;
            kqks.ol_id = (long)cbOLuoi.SelectedValue;
            kqks.Dientichcaybui = (double)nudDTCaybui.Value;
            kqks.Dientichcayto = (double)nudDTCayto.Value;
            kqks.MatdoTB = (double)nudMatDoTB.Value;
            kqks.Sotinhieu = (int)nudSotinhieu.Value;
            kqks.Ghichu = txtGhichuchuaKS.Text;
            kqks.Dientichtretruc = (double)nudDTTretruc.Value;
            kqks.Matdothua = (double)nudMatdothua.Value;
            kqks.Matdoday = (double)nudMatdoday.Value;
            kqks.DaxulyM2 = (double)nudDTDaxuly.Value;
            kqks.Matdo = cbMatDo.Text;
            dataGridViewRow.Tag = kqks;
            dataGridViewRow.Cells[1].Value = cbOLuoi.Text;
            dataGridViewRow.Cells[2].Value = nudSotinhieu.Value;
            dataGridViewRow.Cells[3].Value = comboBox_KQ.Text;
            //UpdateInfomation();
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void FrmThemmoiKQKS_Load(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * FROM cecm_program_area_map where cecm_program_id = " + idDA, _cn.Connection as SqlConnection);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            //System.Data.DataTable datatableProvince = new System.Data.DataTable();
            //sqlAdapterProvince.Fill(datatableProvince);
            //foreach (DataRow dr in datatableProvince.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["code"].ToString()))
            //        continue;

            //    comboBox_Vungcoso.Items.Add(dr["code"].ToString());
            //}
            //if (id_kqks != 0)
            //{
            //    SqlCommandBuilder sqlCommand1 = null;
            //    SqlDataAdapter sqlAdapter = null;
            //    System.Data.DataTable datatable = new System.Data.DataTable();
            //    sqlAdapter = new SqlDataAdapter(string.Format("select Cecm_TerrainRectangle.*, cecm_program_area_map.code as 'Vungcoso' from Cecm_TerrainRectangle left join cecm_program_area_map on cecm_program_area_map.id = Cecm_TerrainRectangle.programId where Cecm_TerrainRectangle.id = {0}", id_kqks), _cn.Connection as SqlConnection);
            //    sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
            //    sqlAdapter.Fill(datatable);
            //    if (datatable.Rows.Count != 0)
            //    {
            //        foreach (DataRow dr in datatable.Rows)
            //        {
            //            idVung = int.Parse(dr["programId"].ToString());
            //            comboBox_Vungcoso.Text = dr["Vungcoso"].ToString();
            //            comboBox_KQ.Text = dr["Ketqua"].ToString();
            //            txtDTChuaKS.Text = dr["DientichchuaKS"].ToString();
            //            comboBox_MaKQKS.Text = dr["code"].ToString();
            //            txtCaybui.Text = dr["Dientichcaybui"].ToString();
            //            txtCayto.Text = dr["Dientichcayto"].ToString();
            //            txtTB.Text = dr["MatdoTB"].ToString();
            //            txtSotinhieu.Text = dr["Sotinhieu"].ToString();
            //            txtGhichuchuaKS.Text = dr["Ghichu"].ToString();
            //            txtTretruc.Text = dr["Dientichtretruc"].ToString();
            //            txtMatdothua.Text = dr["Matdothua"].ToString();
            //            txtMatdoday.Text = dr["Matdoday"].ToString();
            //            txtDaxuly.Text = dr["DaxulyM2"].ToString();
            //            cbMatDo.Text = dr["Matdo"].ToString();
            //        }
            //    }
            //}

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name FROM cecm_program_area_map where cecm_program_id = " + idDA), frmLoggin.sqlCon);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            //DataRow dr = datatable.NewRow();
            //dr["id"] = -1;
            //dr["name"] = "Chưa chọn";
            //datatable.Rows.InsertAt(dr, 0);
            comboBox_Vungcoso.DataSource = datatable;
            comboBox_Vungcoso.DisplayMember = "name";
            comboBox_Vungcoso.ValueMember = "id";

            UtilsDatabase.buildComboboxMST(comboBox_KQ, "002");

            //string sql_oluoi =
            //        "SELECT " +
            //        "gid, o_id " +
            //        "FROM OLuoi where cecm_program_areamap_id = " + comboBox_Vungcoso.SelectedValue;
            //SqlDataAdapter sqlAdapter3 = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
            //SqlCommandBuilder sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
            //DataTable datatable3 = new DataTable();
            //sqlAdapter3.Fill(datatable3);

            //DataRow dr2 = datatable3.NewRow();
            //dr2["gid"] = -1;
            //dr2["o_id"] = "Chưa chọn";
            //datatable3.Rows.InsertAt(dr2, 0);
            //cbOLuoi.DataSource = datatable3;
            //cbOLuoi.DisplayMember = "o_id";
            //cbOLuoi.ValueMember = "gid";

            CecmReportDailyKQKS kqks = dataGridViewRow.Tag as CecmReportDailyKQKS;
            idVung = kqks.area_id;
            comboBox_Vungcoso.SelectedValue = kqks.area_id;
            comboBox_KQ.SelectedValue = kqks.Ketqua;
            nudDTChuaKS.Value = (decimal)kqks.DientichchuaKS;
            cbOLuoi.SelectedValue = kqks.ol_id;
            nudDTCaybui.Value = (decimal)kqks.Dientichcaybui;
            nudDTCayto.Value = (decimal)kqks.Dientichcayto;
            nudMatDoTB.Value = (decimal)kqks.MatdoTB;
            nudSotinhieu.Value = kqks.Sotinhieu;
            txtGhichuchuaKS.Text = kqks.Ghichu;
            nudDTTretruc.Value = (decimal)kqks.Dientichtretruc;
            nudMatdothua.Value = (decimal)kqks.Matdothua;
            nudMatdoday.Value = (decimal)kqks.Matdoday;
            nudDTDaxuly.Value = (decimal)kqks.DaxulyM2;
            cbMatDo.Text = kqks.Matdo;
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
                CecmReportDailyKQKS kqks = dataGridViewRow.Tag as CecmReportDailyKQKS;
                //if (id_kqks != 0)
                //{
                    string sql =
                        "UPDATE [dbo].[cecm_reportdaily_KQKS] set programId = @programId,Ketqua=@Ketqua, DientichchuaKS = @DientichchuaKS,Dientichcaybui = @Dientichcaybui,Dientichcayto= @Dientichcayto,MatdoTB=@MatdoTB,Sotinhieu=@Sotinhieu,Ghichu=@Ghichu,Dientichtretruc=@Dientichtretruc,Matdothua=@Matdothua,Matdoday=@Matdoday,Matdo=@Matdo, DaxulyM2 = @DaxulyM2 where id = " + kqks.id;
                    SqlCommand cmd2 = new SqlCommand(sql, _cn.Connection as SqlConnection);
                    cmd2.Transaction = _cn.Transaction as SqlTransaction;

                    SqlParameter programId = new SqlParameter("@programId", SqlDbType.Int);
                    //Vungcoso.Value = comboBox_Vungcoso.SelectedItem.ToString();
                    programId.Value = idVung;
                    cmd2.Parameters.Add(programId);

                    SqlParameter Ketqua = new SqlParameter("@Ketqua", SqlDbType.BigInt);
                    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                    Ketqua.Value = comboBox_KQ.SelectedValue;
                    cmd2.Parameters.Add(Ketqua);

                    SqlParameter DientichchuaKS = new SqlParameter("@DientichchuaKS", SqlDbType.Float);
                    DientichchuaKS.Value = nudDTChuaKS.Value;
                    cmd2.Parameters.Add(DientichchuaKS);

                    SqlParameter Dientichcaybui = new SqlParameter("@Dientichcaybui", SqlDbType.Float);
                    Dientichcaybui.Value = nudDTCaybui.Value;
                    cmd2.Parameters.Add(Dientichcaybui);

                    SqlParameter Dientichcayto = new SqlParameter("@Dientichcayto", SqlDbType.Float);
                    Dientichcayto.Value = nudDTCaybui.Text;
                    cmd2.Parameters.Add(Dientichcayto);

                    SqlParameter MatdoTB = new SqlParameter("@MatdoTB", SqlDbType.Float);
                    MatdoTB.Value = nudMatDoTB.Value;
                    cmd2.Parameters.Add(MatdoTB);

                    SqlParameter Sotinhieu = new SqlParameter("@Sotinhieu", SqlDbType.Int);
                    Sotinhieu.Value = nudSotinhieu.Value;
                    cmd2.Parameters.Add(Sotinhieu);

                    SqlParameter Ghichu = new SqlParameter("@Ghichu", SqlDbType.NVarChar, 100);
                    Ghichu.Value = txtGhichuchuaKS.Text;
                    cmd2.Parameters.Add(Ghichu);

                    SqlParameter Dientichtretruc = new SqlParameter("@Dientichtretruc", SqlDbType.Float);
                    Dientichtretruc.Value = nudDTTretruc.Value;
                    cmd2.Parameters.Add(Dientichtretruc);

                    SqlParameter Matdothua = new SqlParameter("@Matdothua", SqlDbType.Float);
                    Matdothua.Value = nudMatdothua.Value;
                    cmd2.Parameters.Add(Matdothua);

                    SqlParameter Matdoday = new SqlParameter("@Matdoday", SqlDbType.Float);
                    Matdoday.Value = nudMatdoday.Value;
                    cmd2.Parameters.Add(Matdoday);

                    SqlParameter Daxuly = new SqlParameter("@DaxulyM2", SqlDbType.Float);
                    Daxuly.Value = nudDTDaxuly.Value;
                    cmd2.Parameters.Add(Daxuly);

                    SqlParameter MaO = new SqlParameter("@MaO", SqlDbType.NVarChar, 50);
                    MaO.Value = cbOLuoi.Text;
                    cmd2.Parameters.Add(MaO);

                    SqlParameter Matdo = new SqlParameter("@Matdo", SqlDbType.NVarChar, 50);
                    Matdo.Value = cbMatDo.Text;
                    cmd2.Parameters.Add(Matdo);

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
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");

                return false;
            }
        }

        private void FrmThemmoiKQKS_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FrmThemmoiKQKS_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void comboBox_Vungcoso_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_Vungcoso.SelectedValue is long)
            {
                string sql_oluoi =
                    "SELECT " +
                    "gid, o_id " +
                    //"long_corner1, lat_corner1, " +
                    //"long_corner2, lat_corner2, " +
                    //"long_corner3, lat_corner3, " +
                    //"long_corner4, lat_corner4 " +
                    "FROM OLuoi where cecm_program_areamap_id = " + comboBox_Vungcoso.SelectedValue;
                SqlDataAdapter sqlAdapter3 = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
                SqlCommandBuilder sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                DataTable datatable3 = new DataTable();
                sqlAdapter3.Fill(datatable3);
                DataRow dr2 = datatable3.NewRow();
                dr2["gid"] = -1;
                dr2["o_id"] = "Chưa chọn";
                datatable3.Rows.InsertAt(dr2, 0);
                cbOLuoi.DataSource = datatable3;
                cbOLuoi.DisplayMember = "o_id";
                cbOLuoi.ValueMember = "gid";
            }
        }

        private void comboBox_Vungcoso_Validating(object sender, CancelEventArgs e)
        {
            if(!(comboBox_Vungcoso.SelectedValue is long))
            {
                return;
            }
            if((long)comboBox_Vungcoso.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBox_Vungcoso, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(comboBox_Vungcoso, "Chưa chọn vùng cơ sở");
            }
        }

        private void FrmThemmoiKQKS_Validating(object sender, CancelEventArgs e)
        {
            if (!(cbOLuoi.SelectedValue is long))
            {
                return;
            }
            if ((long)cbOLuoi.SelectedValue > 0 || cbOLuoi.Items.Count <= 1)
            {
                e.Cancel = false;
                errorProvider1.SetError(cbOLuoi, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(cbOLuoi, "Chưa chọn ô lưới");
            }
        }
    }
}
