using System;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class frmChangePass : Form
    {
        public frmChangePass()
        {
            InitializeComponent();
        }

        public string matKhauCu = "", MatKhauMoi = "";

        private void frmChangePass_Load(object sender, EventArgs e)
        {
        }

        private void ViewPasscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(ViewPasscheckBox.Checked == false)
            {
                tbMatKhauCu.PasswordChar = '*';
                tbMatKhauMoi.PasswordChar = '*';
                tbMatKhauMoiRp.PasswordChar = '*';
            }
            else
            {
                tbMatKhauCu.PasswordChar = '\0';
                tbMatKhauMoi.PasswordChar = '\0';
                tbMatKhauMoiRp.PasswordChar = '\0';
            }
        }

        private void btchapnhan_Click(object sender, EventArgs e)
        {
            matKhauCu = ""; MatKhauMoi = "";
            if (tbMatKhauMoi.Text != tbMatKhauMoiRp.Text)
            {
                MessageBox.Show("Mật khẩu mới không khớp !");
                this.DialogResult = DialogResult.None;
                return;
            }
            if (tbMatKhauMoi.Text == "" || tbMatKhauCu.Text == "" || tbMatKhauMoiRp.Text == "")
            {
                MessageBox.Show("Dữ liệu không được để trống");
                this.DialogResult = DialogResult.None;
                return;
            }
            matKhauCu = tbMatKhauCu.Text;
            MatKhauMoi = tbMatKhauMoi.Text;
        }
    }
}