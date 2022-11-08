using DieuHanhCongTruong.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class ThemFile : Form
    {
        private DataGridViewRow dr;
        private DataGridView dgv;
        private string tempFolder = Guid.NewGuid().ToString();
        public static string[] file_types = {
            "Bản đồ xác định vị trí",
            "Sơ đồ/bản đồ tình trạng ô nhiễm BMVN",
            "Bảng tổng hợp khối lượng ĐT",
            "Bảng dự toán KPĐT",
            "Các phụ lục, hướng dẫn thực hiện"};

    //thêm mới
        public ThemFile(DataGridView dgv)
        {
            InitializeComponent();
            this.dgv = dgv;
        }

        //chỉnh sửa
        public ThemFile(DataGridViewRow dr)
        {
            InitializeComponent();
            this.dr = dr;
        }

        private void btOpentbDoc_file_Click(object sender, EventArgs e)
        {
            if (dr != null)
            {
                string filePath = AppUtils.OpenFileDialogCopy(AppUtils.ReportFolder);
                if (string.IsNullOrEmpty(filePath) == false)
                    tbDoc_file.Text = filePath;
            }
            else
            {
                //AppUtils.GetFolderTemp("DeptTemp");
                string filePath = AppUtils.OpenFileDialogCopy(tempFolder);
                if (string.IsNullOrEmpty(filePath) == false)
                    tbDoc_file.Text = filePath;
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            tbDoc_file.Text = "";
        }

        private void tbDoc_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile;
            if (dr != null)
            {
                pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(AppUtils.ReportFolder), tbDoc_file.Text);
            }
            else
            {
                pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(tempFolder), tbDoc_file.Text);
            }
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void CopyFolder()
        {
            string destPath = AppUtils.GetFolderTemp(AppUtils.ReportFolder);
            //string strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
            string sourcePath = AppUtils.GetFolderTemp(tempFolder);
            AppUtils.Copy(sourcePath, destPath);
            sourcePath = Directory.GetParent(sourcePath).FullName;
            sourcePath = Directory.GetParent(sourcePath).FullName;
            AppUtils.ClearFolder(sourcePath);
        }

        private void ClearFolder()
        {
            string sourcePath = AppUtils.GetFolderTemp(tempFolder);
            sourcePath = Directory.GetParent(sourcePath).FullName;
            AppUtils.ClearFolder(sourcePath);
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            CopyFolder();
            KHKSKT_File file = new KHKSKT_File();
            file.file_name = tbDoc_file.Text;
            file.file_type = cbFileType.SelectedIndex + 1;
            if(dr == null)
            {
                //dr = new DataGridViewRow();
                int index = dgv.Rows.Add();
                dr = dgv.Rows[index];
                dr.Cells[0].Value = dr.Cells.Count;
            }
            dr.Tag = file;
            dr.Cells[1].Value = cbFileType.Text;
            dr.Cells[2].Value = file.file_name;
            this.Close();
        }

        private void ThemFile_Load(object sender, EventArgs e)
        {
            if(dr != null)
            {
                KHKSKT_File file = dr.Tag as KHKSKT_File;
                cbFileType.SelectedIndex = (int)file.file_type - 1;
                tbDoc_file.Text = file.file_name;
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            ClearFolder();
            this.Close();
        }

        private void cbFileType_Validating(object sender, CancelEventArgs e)
        {
            if(cbFileType.SelectedIndex > 0)
            {
                e.Cancel = false;
                errorProvider1.SetError(cbFileType, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(cbFileType, "Chưa chọn loại file");
                cbFileType.Focus();
            }
        }
    }
}
