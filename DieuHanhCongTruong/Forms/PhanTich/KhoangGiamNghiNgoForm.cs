using DieuHanhCongTruong.Common;
using System;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class KhoangGiamNghiNgoForm : Form
    {
        public KhoangGiamNghiNgoForm()
        {
            InitializeComponent();
        }

        public double GetKhoangNghiNgo
        {
            get
            {
                return double.Parse(tbKhoangNghiNgo.Text);
            }
        }

        private void btchapnhan_Click(object sender, EventArgs e)
        {
            if (AppUtils.ValidateNumber(tbKhoangNghiNgo) == false)
                return;

            AppUtils.SaveRecentInput(tbKhoangNghiNgo);

            this.DialogResult = DialogResult.OK;
        }

        private void KhoangGiamNghiNgoForm_Load(object sender, EventArgs e)
        {
            AppUtils.LoadRecentInput(tbKhoangNghiNgo, "50");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}