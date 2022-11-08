using DieuHanhCongTruong;
using DieuHanhCongTruong.Common;
using System;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class SelectDieuHanhRPBMForm : Form
    {
        public string _DwgPath = string.Empty;
        public string _DWGCopy = string.Empty;
        public bool _IsNew = false;
        public bool _IsUpdate = false;
        public int _IdDuAn = -1;

        public SelectDieuHanhRPBMForm(int idDuAn, string dwgPath)
        {
            _DwgPath = dwgPath;
            _IdDuAn = idDuAn;
            InitializeComponent();
        }

        private void rbDieuHanhMoi_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDieuHanhMoi.Checked)
            {
                cbkCapNhatDuLieu.Visible = false;
                DGVVungDaLuu.Visible = false;
            }
            else
            {
                cbkCapNhatDuLieu.Visible = true;
                DGVVungDaLuu.Visible = true;
            }
        }

        private void btCauHinh_Click(object sender, EventArgs e)
        {
            ConfigForm frm = new ConfigForm();
            frm.ShowDialog();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
            if (string.IsNullOrEmpty(strPath))
            {
                MessageBox.Show("Không tìm thấy đường dẫn lưu tệp");
                ConfigForm frm = new ConfigForm();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
                else
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }

            if (rbDieuHanhDaCo.Checked)
            {
                _IsNew = false;
                _IsUpdate = cbkCapNhatDuLieu.Checked;

                if (DGVVungDaLuu.SelectedRows.Count != 0)
                {
                    _DWGCopy = (string)DGVVungDaLuu.SelectedRows[0].Tag;
                }
            }
            else
            {
                _IsNew = true;
                // Copy file
                string newFileName = System.IO.Path.GetFileName(_DwgPath);
                string originFileName = System.IO.Path.GetFileName(_DwgPath);
                DateTime dateNow = DateTime.Now;
                newFileName = dateNow.ToString("dd-MM-yyyy_HH-mm-ss") + "_" + newFileName;

                if (System.IO.Directory.Exists(strPath + "\\" + _IdDuAn + "\\Data") == false)
                {
                    System.IO.Directory.CreateDirectory(strPath + "\\" + _IdDuAn + "\\Data");
                }

                if (System.IO.Directory.Exists(strPath + "\\" + _IdDuAn + "\\Temp") == false)
                {
                    System.IO.Directory.CreateDirectory(strPath + "\\" + _IdDuAn + "\\Temp");
                }

                if (AppUtils.CopyFile(_DwgPath, strPath + "\\" + _IdDuAn + "\\Data" + "\\" + newFileName))
                    _DWGCopy = strPath + "\\" + _IdDuAn + "\\Data" + "\\" + newFileName;

                AppUtils.CopyFile(_DwgPath, strPath + "\\" + _IdDuAn + "\\Temp" + "\\" + originFileName, false);
            }
        }

        private void SelectDieuHanhRPBMForm_Load(object sender, EventArgs e)
        {
            cbkCapNhatDuLieu.Visible = false;
            DGVVungDaLuu.Visible = false;

            var strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
            if (string.IsNullOrEmpty(strPath))
            {
                MessageBox.Show("Không tìm thấy đường dẫn lưu tệp");
                ConfigForm frm = new ConfigForm();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
                else
                    return;
            }

            if (System.IO.Directory.Exists(strPath + "\\" + _IdDuAn + "\\Data") == false)
            {
                System.IO.Directory.CreateDirectory(strPath + "\\" + _IdDuAn + "\\Data");
            }

            string[] fileArray = System.IO.Directory.GetFiles(strPath + "\\" + _IdDuAn + "\\Data");

            int index = 0;
            for (int i = 0; i < fileArray.Length; i++)
            {
                string fileName = System.IO.Path.GetFileName(fileArray[i]);
                if (fileName.ToUpper().Contains(".DWG"))
                {
                    DGVVungDaLuu.Rows.Add(fileName);
                    DGVVungDaLuu.Rows[index].Tag = fileArray[i];
                    index++;
                }
            }
        }
    }
}