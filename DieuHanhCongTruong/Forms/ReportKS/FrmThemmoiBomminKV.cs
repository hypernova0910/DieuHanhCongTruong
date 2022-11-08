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
    public partial class FrmThemmoiBomminKV : Form
    {
        public SqlConnection _Cn = null;
        public int id_mine = 0;
        public long idDA = 0;
        public int idLoai = 0;
        public int idO = 0;
        private DataGridViewRow dataGridViewRow;

        public FrmThemmoiBomminKV(DataGridViewRow dataGridViewRow, long idDA)
        {
            //id_mine = id;
            this.idDA = idDA;
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
            UpdateInfomation();
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.Close();
        }
        private void loadDuLieu()
        {
            LoadCBOL();

            CecmReportSurveyAreaBMVN bmvn = dataGridViewRow.Tag as CecmReportSurveyAreaBMVN;
            txtKihieu.Text = bmvn.Kyhieu;
            comboBox_Loai.Text = bmvn.Loai;
            comboBox_O.SelectedValue = bmvn.idRectangle;
            nudSL.Value = bmvn.SL;
            nudKichthuoc.Value = (decimal)bmvn.Kichthuoc;

            nudKinhdo.Value = (decimal)Math.Round(bmvn.Kinhdo, 6);
            nudVido.Value = (decimal)Math.Round(bmvn.Vido, 6);

            nudDosau.Value = (decimal)bmvn.Deep;
            cbTinhtrang.Text = bmvn.Tinhtrang;
            cbPPXL.Text = bmvn.PPXuLy;
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

        private void FrmThemmoiBommin_Load(object sender, EventArgs e)
        {
            loadDuLieu();
            if (string.IsNullOrEmpty(cbTinhtrang.Text) == true)
            {
                cbTinhtrang.SelectedIndex = 0;
            }
            if (string.IsNullOrEmpty(cbPPXL.Text) == true)
            {
                cbPPXL.SelectedIndex = 0;
            }
        }
        private void UpdateInfomation()
        {
            try
            {
                //if (id_mine != 0)
                //{
                //    // Chua co tao moi
                //    SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[Cecm_VNTerrainMinePoint] SET [Kyhieu] = @Kyhieu,[Loai] = @Loai,[SL] = @SL,[Area] = @Area,[XPoint] = @Kinhdo,[YPoint] = @Vido ,[Deep] = @Dosau,[Tinhtrang] = @Tinhtrang,[PPXuLy] = @PPXuLy WHERE id = " + id_mine, _Cn);

                //    SqlParameter Kyhieu = new SqlParameter("@Kyhieu", SqlDbType.NVarChar, 255);
                //    Kyhieu.Value = txtKihieu.Text;
                //    cmd2.Parameters.Add(Kyhieu);

                //    SqlParameter Loai = new SqlParameter("@Loai", SqlDbType.NVarChar, 50);
                //    //Ketqua.Value = comboBox_KQ.SelectedItem.ToString();
                //    Loai.Value = comboBox_Loai.Text;
                //    cmd2.Parameters.Add(Loai);

                //    /*SqlParameter O = new SqlParameter("@O", SqlDbType.Int);
                //    O.Value = idO;
                //    cmd2.Parameters.Add(O);*/

                //    SqlParameter SL = new SqlParameter("@SL", SqlDbType.Int);
                //    if (string.IsNullOrEmpty(txtSL.Text) == false)
                //    {
                //        SL.Value = int.Parse(txtSL.Text);
                //        cmd2.Parameters.Add(SL);
                //    }
                //    else
                //    {
                //        SL.Value = 0;
                //        cmd2.Parameters.Add(SL);
                //    }

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

                //    SqlParameter PPXuLy = new SqlParameter("@PPXuLy", SqlDbType.NVarChar, 225);
                //    PPXuLy.Value = cbPPXL.Text;
                //    cmd2.Parameters.Add(PPXuLy);

                //    int temp = 0;
                //    temp = cmd2.ExecuteNonQuery();

                //    if (temp > 0)
                //    {
                //        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                //        this.DialogResult = DialogResult.OK;
                //        this.Close();
                //        return true;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                //        this.DialogResult = DialogResult.None;
                //        this.Close();
                //        return false;
                //    }
                //}
                //else
                //{
                //    return false;
                //}
                CecmReportSurveyAreaBMVN bmvn = new CecmReportSurveyAreaBMVN();
                bmvn.Kyhieu = txtKihieu.Text;
                bmvn.Loai = comboBox_Loai.Text;
                bmvn.idRectangle = (long)comboBox_O.SelectedValue;
                bmvn.SL = (int)nudSL.Value;
                bmvn.Tinhtrang = cbTinhtrang.SelectedIndex > 0 ? cbTinhtrang.Text : "";
                bmvn.Vido = (double)nudVido.Value;
                bmvn.Kinhdo = (double)nudKinhdo.Value;
                bmvn.Kichthuoc = (double)nudKichthuoc.Value;
                bmvn.Deep = (double)nudDosau.Value;
                bmvn.PPXuLy = cbPPXL.SelectedIndex > 0 ? cbPPXL.Text : "";
                dataGridViewRow.Tag = bmvn;
                dataGridViewRow.Cells[1].Value = bmvn.Loai;
                dataGridViewRow.Cells[2].Value = bmvn.Kyhieu;
                dataGridViewRow.Cells[3].Value = comboBox_O.Text;
                dataGridViewRow.Cells[4].Value = bmvn.Kichthuoc;
                dataGridViewRow.Cells[5].Value = bmvn.Deep;
                dataGridViewRow.Cells[6].Value = bmvn.Kinhdo;
                dataGridViewRow.Cells[7].Value = bmvn.Vido;
                dataGridViewRow.Cells[8].Value = bmvn.Tinhtrang;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void comboBox_O_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_O.SelectedItem != null)
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT * FROM Cecm_TerrainRectangle where programId = " + idDA, _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                System.Data.DataTable datatableProvince = new System.Data.DataTable();
                sqlAdapterProvince.Fill(datatableProvince);
                foreach (DataRow dr in datatableProvince.Rows)
                {
                    if (string.IsNullOrEmpty(dr["id"].ToString()))
                        continue;
                    idO = int.Parse(dr["id"].ToString());
                    //txtDosau.Text = dr["Deep"].ToString();
                    //txtKichthuoc.Text = dr["Area"].ToString();
                }
            }
        }

        private void FrmThemmoiBomminKV_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FrmThemmoiBomminKV_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
