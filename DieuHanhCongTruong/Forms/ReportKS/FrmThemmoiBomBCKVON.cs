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
    public partial class FrmThemmoiBomBCKVON : Form
    {
        public SqlConnection _Cn = null;
        public int id_mine = 0;
        public long idVung = 0;
        public int idO = 0;
        public int idLoai = 0;
        public string Ma = "";
        private DataGridViewRow dataGridViewRow;

        public FrmThemmoiBomBCKVON(DataGridViewRow dataGridViewRow, long idVung)
        {
            //id_mine = id;
            this.idVung = idVung;
            //Ma = MaCHA;
            this.dataGridViewRow = dataGridViewRow;
            _Cn = frmLoggin.sqlCon;
            InitializeComponent();
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

        private void FrmThemmoiBomBCKVON_Load(object sender, EventArgs e)
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

        private void loadDuLieu()
        {
            LoadCBOL();

            CecmReportPollutionAreaBMVN bmvn = dataGridViewRow.Tag as CecmReportPollutionAreaBMVN;
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
            txtGhichu.Text = bmvn.Ghichu;
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
                    "FROM OLuoi where cecm_program_areamap_id = " + idVung;
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
                CecmReportPollutionAreaBMVN bmvn = new CecmReportPollutionAreaBMVN();
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
                bmvn.Ghichu = txtGhichu.Text;
                dataGridViewRow.Tag = bmvn;
                dataGridViewRow.Cells[1].Value = bmvn.Loai;
                dataGridViewRow.Cells[2].Value = bmvn.Kyhieu;
                dataGridViewRow.Cells[3].Value = comboBox_O.Text;
                dataGridViewRow.Cells[4].Value = bmvn.Kichthuoc;
                dataGridViewRow.Cells[5].Value = bmvn.Kinhdo;
                dataGridViewRow.Cells[6].Value = bmvn.Vido;
                dataGridViewRow.Cells[7].Value = bmvn.Deep;
                dataGridViewRow.Cells[8].Value = bmvn.Tinhtrang;
                dataGridViewRow.Cells[9].Value = bmvn.PPXuLy;
                dataGridViewRow.Cells[10].Value = bmvn.Ghichu;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Yêu cầu nhập đúng định dạng của dữ liệu", "Lỗi");
            }
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

        private void FrmThemmoiBomBCKVON_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FrmThemmoiBomBCKVON_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

