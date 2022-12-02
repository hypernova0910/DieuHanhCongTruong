namespace VNRaPaBomMin
{
    partial class KhoangNghiNgoForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.DGVData = new System.Windows.Forms.DataGridView();
            this.Stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TuTruong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoSau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DienTich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhanTich = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.QuyetDinh = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cotXoa = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(1356, 286);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnCancel.Size = new System.Drawing.Size(76, 39);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.AutoSize = true;
            this.btnOk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(1465, 286);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnOk.Size = new System.Drawing.Size(66, 39);
            this.btnOk.TabIndex = 31;
            this.btnOk.Text = "Lưu";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // DGVData
            // 
            this.DGVData.AllowUserToAddRows = false;
            this.DGVData.AllowUserToDeleteRows = false;
            this.DGVData.AllowUserToResizeRows = false;
            this.DGVData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Stt,
            this.X,
            this.Y,
            this.TuTruong,
            this.DoSau,
            this.DienTich,
            this.cotLoai,
            this.PhanTich,
            this.QuyetDinh,
            this.cotXoa});
            this.DGVData.Location = new System.Drawing.Point(13, 13);
            this.DGVData.Margin = new System.Windows.Forms.Padding(4);
            this.DGVData.MultiSelect = false;
            this.DGVData.Name = "DGVData";
            this.DGVData.RowHeadersVisible = false;
            this.DGVData.RowHeadersWidth = 51;
            this.DGVData.Size = new System.Drawing.Size(1518, 250);
            this.DGVData.TabIndex = 32;
            this.DGVData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVData_CellClick);
            // 
            // Stt
            // 
            this.Stt.FillWeight = 20F;
            this.Stt.HeaderText = "STT";
            this.Stt.MinimumWidth = 6;
            this.Stt.Name = "Stt";
            this.Stt.ReadOnly = true;
            // 
            // X
            // 
            this.X.HeaderText = "Vĩ độ";
            this.X.MinimumWidth = 6;
            this.X.Name = "X";
            this.X.ReadOnly = true;
            // 
            // Y
            // 
            this.Y.HeaderText = "Kinh độ";
            this.Y.MinimumWidth = 6;
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            // 
            // TuTruong
            // 
            this.TuTruong.HeaderText = "Từ trường";
            this.TuTruong.MinimumWidth = 6;
            this.TuTruong.Name = "TuTruong";
            this.TuTruong.ReadOnly = true;
            // 
            // DoSau
            // 
            this.DoSau.HeaderText = "Độ sâu";
            this.DoSau.MinimumWidth = 6;
            this.DoSau.Name = "DoSau";
            this.DoSau.ReadOnly = true;
            // 
            // DienTich
            // 
            this.DienTich.HeaderText = "Diện tích";
            this.DienTich.MinimumWidth = 6;
            this.DienTich.Name = "DienTich";
            this.DienTich.ReadOnly = true;
            // 
            // cotLoai
            // 
            this.cotLoai.FillWeight = 30F;
            this.cotLoai.HeaderText = "Loại";
            this.cotLoai.MinimumWidth = 6;
            this.cotLoai.Name = "cotLoai";
            // 
            // PhanTich
            // 
            this.PhanTich.FillWeight = 50F;
            this.PhanTich.HeaderText = "Phân tích / Cắm cờ";
            this.PhanTich.MinimumWidth = 6;
            this.PhanTich.Name = "PhanTich";
            this.PhanTich.ReadOnly = true;
            // 
            // QuyetDinh
            // 
            this.QuyetDinh.FillWeight = 50F;
            this.QuyetDinh.HeaderText = "Khẳng định có BMVN";
            this.QuyetDinh.MinimumWidth = 6;
            this.QuyetDinh.Name = "QuyetDinh";
            // 
            // cotXoa
            // 
            this.cotXoa.FillWeight = 30F;
            this.cotXoa.HeaderText = "Xóa";
            this.cotXoa.Image = global::DieuHanhCongTruong.Properties.Resources.DeleteRed;
            this.cotXoa.MinimumWidth = 6;
            this.cotXoa.Name = "cotXoa";
            this.cotXoa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cotXoa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // KhoangNghiNgoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1547, 349);
            this.Controls.Add(this.DGVData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KhoangNghiNgoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khoảng nghi ngờ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KhoangNghiNgoForm_FormClosed);
            this.Load += new System.EventHandler(this.KhoangNghiNgoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView DGVData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stt;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn TuTruong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoSau;
        private System.Windows.Forms.DataGridViewTextBoxColumn DienTich;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotLoai;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PhanTich;
        private System.Windows.Forms.DataGridViewCheckBoxColumn QuyetDinh;
        private System.Windows.Forms.DataGridViewImageColumn cotXoa;
    }
}