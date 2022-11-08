using System;
using System.Windows.Forms;

namespace DieuHanhCongTruong
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        //private void btDuongDan_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog folderBrowser = new OpenFileDialog();
        //    string folderPath = string.Empty;
        //    // Set validate names and check file exists to false otherwise windows will
        //    // not let you select "Folder Selection."
        //    folderBrowser.ValidateNames = false;
        //    folderBrowser.CheckFileExists = false;
        //    folderBrowser.CheckPathExists = true;
        //    // Always default to Folder Selection.
        //    folderBrowser.FileName = "Chọn.";
        //    if (folderBrowser.ShowDialog() == DialogResult.OK)
        //    {
        //        folderPath = System.IO.Path.GetDirectoryName(folderBrowser.FileName);

        //        lbTepDuongDan.Text = folderPath;
        //    }
        //}

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            //AppUtils.LoadRecentInput(tbHoTen, string.Empty);
            //AppUtils.LoadRecentInput(tbSoDienThoai, string.Empty);
            //AppUtils.LoadRecentInput(tbDiaChi, string.Empty);
            //AppUtils.LoadRecentInput(tbEmail, string.Empty);
            //AppUtils.LoadRecentInput(lbTepDuongDan, "...");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //AppUtils.SaveRecentInput(tbHoTen);
            //AppUtils.SaveRecentInput(tbSoDienThoai);
            //AppUtils.SaveRecentInput(tbDiaChi);
            //AppUtils.SaveRecentInput(tbEmail);
            //AppUtils.SaveRecentInput(lbTepDuongDan);
        }
    }
}