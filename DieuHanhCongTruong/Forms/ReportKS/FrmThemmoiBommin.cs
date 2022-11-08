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
    public partial class FrmThemmoiBommin : Form
    {
        public SqlConnection _Cn = null;
        public int id_mine = 0;
        public long idDA = 0;
        public int idO = 0;
        public int idLoai = 0;
        private DataGridViewRow dataGridViewRow;

        public FrmThemmoiBommin(DataGridViewRow dataGridViewRow, long idDa)
        {
            //id_mine = id;
            idDA = idDa;
            this.dataGridViewRow = dataGridViewRow;
            _Cn = frmLoggin.sqlCon;
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
            //if(txtKihieu.Text == "" || comboBox_Loai.Text == "" || comboBox_O.Text == "" || txtSL.Text == "" || txtKinhdo.Text == "" || txtVido.Text == "" || txtKichthuoc.Text == "" || txtDosau.Text == "")
            //{
            //    MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
            //}
            //else
            //{
            //    bool success = UpdateInfomation();
            //}
            UpdateInfomation();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }

        private void FrmThemmoiBommin_Load(object sender, EventArgs e)
        {
            //SqlCommandBuilder sqlCommand = null;
            //SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * FROM Cecm_TerrainRectangle left join cecm_program_area_map on cecm_program_area_map.cecm_program_id = programId where programId = " + idDA+ " or programId = " + idDA, _Cn);
            //sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            //System.Data.DataTable datatableProvince = new System.Data.DataTable();
            //sqlAdapterProvince.Fill(datatableProvince);
            //foreach (DataRow dr in datatableProvince.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["code"].ToString()))
            //        continue;

            //    comboBox_O.Items.Add(dr["code"].ToString());
            //}
            //if (id_mine != 0)
            //{
            //    SqlCommandBuilder sqlCommand1 = null;
            //    SqlDataAdapter sqlAdapter = null;
            //    System.Data.DataTable datatable = new System.Data.DataTable();
            //    sqlAdapter = new SqlDataAdapter(string.Format("select [Cecm_VNTerrainMinePoint].*, Cecm_TerrainRectangle.code as 'O' from [Cecm_VNTerrainMinePoint] left join Cecm_TerrainRectangle on  Cecm_TerrainRectangle.id = [Cecm_VNTerrainMinePoint].idRectangle where [Cecm_VNTerrainMinePoint].id = {0}", id_mine), _Cn);
            //    sqlCommand1 = new SqlCommandBuilder(sqlAdapter);
            //    sqlAdapter.Fill(datatable);
            //    if (datatable.Rows.Count != 0)
            //    {
            //        foreach (DataRow dr in datatable.Rows)
            //        {
            //            txtKihieu.Text = dr["Kyhieu"].ToString();
            //            comboBox_Loai.Text = dr["Loai"].ToString();
            //            comboBox_O.Text = dr["O"].ToString();
            //            txtSL.Text = dr["SL"].ToString();
            //            txtKichthuoc.Text = Math.Sqrt(double.Parse(dr["Area"].ToString())).ToString();

            //            txtKinhdo.Text = Math.Round(double.Parse(dr["XPoint"].ToString()), 6).ToString();
            //            txtVido.Text = Math.Round(double.Parse(dr["YPoint"].ToString()), 6).ToString();

            //            txtDosau.Text = dr["Deep"].ToString();
            //            txtTinhtrang.Text = dr["Tinhtrang"].ToString();
            //            idO = int.Parse(dr["idRectangle"].ToString());

            //        }
            //    }
            //}

            UtilsDatabase.buildComboboxMST(cbTinhtrang, "003");
            UtilsDatabase.buildComboboxMST(cbPPXL, "004");
            LoadCBOL();

            CecmReportDailyBMVN bmvn = dataGridViewRow.Tag as CecmReportDailyBMVN;
            txtKihieu.Text = bmvn.Kyhieu;
            comboBox_Loai.Text = bmvn.Loai;
            comboBox_O.SelectedValue = bmvn.idRectangle;
            nudSL.Value = bmvn.SL;
            nudKichthuoc.Value = (decimal)bmvn.Kichthuoc;
            nudDai.Value = (decimal)bmvn.Dai;
            nudRong.Value = (decimal)bmvn.Rong;

            nudKinhdo.Value = (decimal)Math.Round(bmvn.Kinhdo, 6);
            nudVido.Value = (decimal)Math.Round(bmvn.Vido, 6);

            nudDosau.Value = (decimal)bmvn.Deep;
            cbTinhtrang.SelectedValue = bmvn.Tinhtrang;
            cbPPXL.SelectedValue = bmvn.PPXuLy;
            //idO = int.Parse(dr["idRectangle"].ToString());
        }

        private void LoadCBOL()
        {
            string sql_oluoi =
                    "SELECT " +
                    "gid, o_id " +
                    //"long_corner1, lat_corner1, " +
                    //"long_corner2, lat_corner2, " +
                    //"long_corner3, lat_corner3, " +
                    //"long_corner4, lat_corner4 " +
                    "FROM OLuoi where cecm_program_id = " + idDA;
            SqlDataAdapter sqlAdapter3 = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
            SqlCommandBuilder sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
            DataTable datatable3 = new DataTable();
            sqlAdapter3.Fill(datatable3);
            DataRow dr2 = datatable3.NewRow();
            dr2["gid"] = -1;
            dr2["o_id"] = "Chưa chọn";
            datatable3.Rows.InsertAt(dr2, 0);
            comboBox_O.DataSource = datatable3;
            comboBox_O.DisplayMember = "o_id";
            comboBox_O.ValueMember = "gid";
        }

        private void UpdateInfomation()
        {
            try
            {
                //SqlCommandBuilder sqlCommand = null;
                //SqlDataAdapter sqlAdapter = null;
                //DataTable datatable = new DataTable();
                //sqlAdapter = new SqlDataAdapter(String.Format("USE [{0}] SELECT cecm_user.user_name FROM cecm_user where user_name = '{1}'", databaseName, tbTenDangNhap.Text), cn);
                //sqlCommand = new SqlCommandBuilder(sqlAdapter);
                //sqlAdapter.Fill(datatable);
                //if (id_mine != 0)
                //{
                //    // Chua co tao moi
                //    SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[Cecm_VNTerrainMinePoint] SET [Kyhieu] = @Kyhieu,[Loai] = @Loai,[idRectangle] = @O,[SL] = @SL,[Area] = @Area,[XPoint] = @Kinhdo,[YPoint] = @Vido ,[Deep] = @Dosau,[Tinhtrang] = @Tinhtrang WHERE id = " + id_mine, _Cn);
                //    SqlParameter Kyhieu = new SqlParameter("@Kyhieu", SqlDbType.NVarChar, 255);
                //    Kyhieu.Value = txtKihieu.Text;
                //    cmd2.Parameters.Add(Kyhieu);

                //    SqlParameter Loai = new SqlParameter("@Loai", SqlDbType.NVarChar, 50);
                //    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                //    Loai.Value = comboBox_Loai.Text;
                //    cmd2.Parameters.Add(Loai);

                //    SqlParameter O = new SqlParameter("@O", SqlDbType.Int);
                //    O.Value = idO.ToString();
                //    cmd2.Parameters.Add(O);


                //    SqlParameter SL = new SqlParameter("@SL", SqlDbType.Int);
                //    SL.Value = txtSL.Text;
                //    cmd2.Parameters.Add(SL);

                //    SqlParameter Kichthuoc = new SqlParameter("@Area", SqlDbType.NVarChar, 225);
                //    Kichthuoc.Value = Math.Pow(double.Parse(txtKichthuoc.Text), 2).ToString();
                //    cmd2.Parameters.Add(Kichthuoc);

                //    SqlParameter Kinhdo = new SqlParameter("@Kinhdo", SqlDbType.NVarChar, 225);
                //    Kinhdo.Value = txtKinhdo.Text;
                //    cmd2.Parameters.Add(Kinhdo);

                //    SqlParameter Vido = new SqlParameter("@Vido", SqlDbType.NVarChar, 225);
                //    Vido.Value = txtVido.Text;
                //    cmd2.Parameters.Add(Vido);

                //    SqlParameter Dosau = new SqlParameter("@Dosau", SqlDbType.NVarChar, 225);
                //    Dosau.Value = txtDosau.Text;
                //    cmd2.Parameters.Add(Dosau);

                //    SqlParameter Tinhtrang = new SqlParameter("@Tinhtrang", SqlDbType.NVarChar, 225);
                //    Tinhtrang.Value = txtTinhtrang.Text;
                //    cmd2.Parameters.Add(Tinhtrang);


                //    int temp = 0;
                //    temp = cmd2.ExecuteNonQuery();

                //    if (temp > 0)
                //    {
                //        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                //        this.Close();
                //        return true;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                //        this.Close();
                //        return false;
                //    }
                //}
                //else
                //{
                //    return false;
                //}
                CecmReportDailyBMVN bmvn = new CecmReportDailyBMVN();
                bmvn.Kyhieu = txtKihieu.Text;
                bmvn.Loai = comboBox_Loai.Text;
                bmvn.idRectangle = (long)comboBox_O.SelectedValue;
                bmvn.SL = (int)nudSL.Value;
                bmvn.Tinhtrang = (long)cbTinhtrang.SelectedValue;
                bmvn.PPXuLy = (long)cbPPXL.SelectedValue;
                bmvn.Vido = (double)nudVido.Value;
                bmvn.Kinhdo = (double)nudKinhdo.Value;
                bmvn.Kichthuoc = (double)nudKichthuoc.Value;
                bmvn.Dai = (double)nudDai.Value;
                bmvn.Rong = (double)nudRong.Value;
                bmvn.Deep = (double)nudDosau.Value;
                dataGridViewRow.Tag = bmvn;
                dataGridViewRow.Cells[1].Value = bmvn.Kyhieu;
                dataGridViewRow.Cells[2].Value = bmvn.Loai;
                dataGridViewRow.Cells[3].Value = comboBox_O.Text;
                dataGridViewRow.Cells[4].Value = bmvn.SL;
                dataGridViewRow.Cells[5].Value = bmvn.Kinhdo;
                dataGridViewRow.Cells[6].Value = bmvn.Vido;
                dataGridViewRow.Cells[7].Value = bmvn.Deep;
                dataGridViewRow.Cells[8].Value = cbTinhtrang.Text;
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
                //return false;
            }
        }

        private void comboBox_O_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox_O.SelectedItem != null)
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * FROM Cecm_TerrainRectangle left join cecm_program_area_map on cecm_program_area_map.id = Cecm_TerrainRectangle.programId where cecm_program_area_map.cecm_program_id = " + idDA, _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    if (string.IsNullOrEmpty(dr["id"].ToString()))
                        continue;
                    //txtKinhdo.Text = dr["Xpoint"].ToString();
                    //txtVido.Text = dr["YPoint"].ToString();
                    idO = int.Parse(dr["id"].ToString());
                    //txtKichthuoc.Text = dr["Area"].ToString();

                }
            }
        }

        private void nudDaiRong_ValueChanged(object sender, EventArgs e)
        {
            nudKichthuoc.Value = nudDai.Value * nudRong.Value;
        }

        private void txtKihieu_Validating(object sender, CancelEventArgs e)
        {
            if(txtKihieu.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtKihieu, "Chưa nhập ký hiệu");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtKihieu, "");
            }
        }
    }
}
