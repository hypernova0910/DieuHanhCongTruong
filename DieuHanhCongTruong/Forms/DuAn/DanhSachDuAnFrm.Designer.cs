
namespace VNRaPaBomMin
{
    partial class DanhSachDuAnFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDuAn = new System.Windows.Forms.DataGridView();
            this.dgvStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvMaDuAn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTenDuAn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDonViThucHien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDiaDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoiRPBMSua = new System.Windows.Forms.DataGridViewImageColumn();
            this.DoiRPBMXoa = new System.Windows.Forms.DataGridViewImageColumn();
            this.btClose = new System.Windows.Forms.Button();
            this.btThemMoi = new System.Windows.Forms.Button();
            this.btImport = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuAn)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDuAn
            // 
            this.dgvDuAn.AllowUserToAddRows = false;
            this.dgvDuAn.AllowUserToDeleteRows = false;
            this.dgvDuAn.AllowUserToOrderColumns = true;
            this.dgvDuAn.AllowUserToResizeRows = false;
            this.dgvDuAn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDuAn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDuAn.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDuAn.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDuAn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDuAn.ColumnHeadersHeight = 30;
            this.dgvDuAn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDuAn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvStt,
            this.DgvMaDuAn,
            this.dgvTenDuAn,
            this.DgvDonViThucHien,
            this.DgvThoiGian,
            this.DgvDiaDiem,
            this.DoiRPBMSua,
            this.DoiRPBMXoa});
            this.dgvDuAn.EnableHeadersVisualStyles = false;
            this.dgvDuAn.Location = new System.Drawing.Point(26, 72);
            this.dgvDuAn.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDuAn.MultiSelect = false;
            this.dgvDuAn.Name = "dgvDuAn";
            this.dgvDuAn.ReadOnly = true;
            this.dgvDuAn.RowHeadersVisible = false;
            this.dgvDuAn.RowHeadersWidth = 51;
            this.dgvDuAn.Size = new System.Drawing.Size(1521, 841);
            this.dgvDuAn.TabIndex = 2;
            this.dgvDuAn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDuAn_CellClick);
            // 
            // dgvStt
            // 
            this.dgvStt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvStt.FillWeight = 184.4112F;
            this.dgvStt.HeaderText = "STT";
            this.dgvStt.MinimumWidth = 6;
            this.dgvStt.Name = "dgvStt";
            this.dgvStt.ReadOnly = true;
            this.dgvStt.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStt.Width = 50;
            // 
            // DgvMaDuAn
            // 
            this.DgvMaDuAn.FillWeight = 33.93257F;
            this.DgvMaDuAn.HeaderText = "Mã dự án";
            this.DgvMaDuAn.MinimumWidth = 6;
            this.DgvMaDuAn.Name = "DgvMaDuAn";
            this.DgvMaDuAn.ReadOnly = true;
            // 
            // dgvTenDuAn
            // 
            this.dgvTenDuAn.FillWeight = 89.40961F;
            this.dgvTenDuAn.HeaderText = "Tên dự án";
            this.dgvTenDuAn.MinimumWidth = 6;
            this.dgvTenDuAn.Name = "dgvTenDuAn";
            this.dgvTenDuAn.ReadOnly = true;
            // 
            // DgvDonViThucHien
            // 
            this.DgvDonViThucHien.FillWeight = 96.45034F;
            this.DgvDonViThucHien.HeaderText = "Đơn vị thực hiện";
            this.DgvDonViThucHien.MinimumWidth = 6;
            this.DgvDonViThucHien.Name = "DgvDonViThucHien";
            this.DgvDonViThucHien.ReadOnly = true;
            // 
            // DgvThoiGian
            // 
            this.DgvThoiGian.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DgvThoiGian.FillWeight = 39.52563F;
            this.DgvThoiGian.HeaderText = "Thời gian";
            this.DgvThoiGian.MinimumWidth = 6;
            this.DgvThoiGian.Name = "DgvThoiGian";
            this.DgvThoiGian.ReadOnly = true;
            // 
            // DgvDiaDiem
            // 
            this.DgvDiaDiem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DgvDiaDiem.FillWeight = 111.7317F;
            this.DgvDiaDiem.HeaderText = "Địa Điểm";
            this.DgvDiaDiem.MinimumWidth = 6;
            this.DgvDiaDiem.Name = "DgvDiaDiem";
            this.DgvDiaDiem.ReadOnly = true;
            // 
            // DoiRPBMSua
            // 
            this.DoiRPBMSua.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DoiRPBMSua.FillWeight = 106.599F;
            this.DoiRPBMSua.HeaderText = "Sửa";
            this.DoiRPBMSua.MinimumWidth = 6;
            this.DoiRPBMSua.Name = "DoiRPBMSua";
            this.DoiRPBMSua.ReadOnly = true;
            this.DoiRPBMSua.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DoiRPBMSua.Width = 39;
            // 
            // DoiRPBMXoa
            // 
            this.DoiRPBMXoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DoiRPBMXoa.FillWeight = 105.2337F;
            this.DoiRPBMXoa.HeaderText = "Xóa";
            this.DoiRPBMXoa.MinimumWidth = 6;
            this.DoiRPBMXoa.Name = "DoiRPBMXoa";
            this.DoiRPBMXoa.ReadOnly = true;
            this.DoiRPBMXoa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DoiRPBMXoa.Width = 39;
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.AutoSize = true;
            this.btClose.Location = new System.Drawing.Point(1457, 934);
            this.btClose.Margin = new System.Windows.Forms.Padding(4);
            this.btClose.Name = "btClose";
            this.btClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btClose.Size = new System.Drawing.Size(85, 39);
            this.btClose.TabIndex = 7;
            this.btClose.Text = "Đóng";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btThemMoi
            // 
            this.btThemMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btThemMoi.AutoSize = true;
            this.btThemMoi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btThemMoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btThemMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btThemMoi.Location = new System.Drawing.Point(1437, 10);
            this.btThemMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btThemMoi.Name = "btThemMoi";
            this.btThemMoi.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btThemMoi.Size = new System.Drawing.Size(110, 39);
            this.btThemMoi.TabIndex = 9;
            this.btThemMoi.Text = "Thêm mới";
            this.btThemMoi.UseVisualStyleBackColor = false;
            this.btThemMoi.Click += new System.EventHandler(this.btThemMoi_Click);
            // 
            // btImport
            // 
            this.btImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btImport.AutoSize = true;
            this.btImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btImport.ForeColor = System.Drawing.Color.White;
            this.btImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btImport.Location = new System.Drawing.Point(1330, 10);
            this.btImport.Margin = new System.Windows.Forms.Padding(4);
            this.btImport.Name = "btImport";
            this.btImport.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btImport.Size = new System.Drawing.Size(81, 39);
            this.btImport.TabIndex = 9;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = false;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "LOG file|*.log";
            // 
            // DanhSachDuAnFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 986);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.btThemMoi);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvDuAn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DanhSachDuAnFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách các dự án";
            this.Load += new System.EventHandler(this.DanhSachDuAnFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuAn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDuAn;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btThemMoi;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvMaDuAn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTenDuAn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDonViThucHien;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvThoiGian;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDiaDiem;
        private System.Windows.Forms.DataGridViewImageColumn DoiRPBMSua;
        private System.Windows.Forms.DataGridViewImageColumn DoiRPBMXoa;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}