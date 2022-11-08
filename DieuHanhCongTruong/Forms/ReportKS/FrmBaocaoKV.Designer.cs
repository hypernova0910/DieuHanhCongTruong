namespace VNRaPaBomMin
{
    partial class FrmBaocaoKV
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtTImkiem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timeNgaybatdau = new System.Windows.Forms.DateTimePicker();
            this.timeNgayketthuc = new System.Windows.Forms.DateTimePicker();
            this.BtnTimkiem = new System.Windows.Forms.Button();
            this.BtnThemmoi = new System.Windows.Forms.Button();
            this.DGV_BaoCaoKV = new System.Windows.Forms.DataGridView();
            this.dgvStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotXuatExcel = new System.Windows.Forms.DataGridViewImageColumn();
            this.Export = new System.Windows.Forms.DataGridViewImageColumn();
            this.DoiRPBMSua = new System.Windows.Forms.DataGridViewImageColumn();
            this.DoiRPBMXoa = new System.Windows.Forms.DataGridViewImageColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.BtnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_BaoCaoKV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dự án ";
            // 
            // TxtTImkiem
            // 
            this.TxtTImkiem.Location = new System.Drawing.Point(78, 76);
            this.TxtTImkiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtTImkiem.Name = "TxtTImkiem";
            this.TxtTImkiem.Size = new System.Drawing.Size(304, 22);
            this.TxtTImkiem.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(403, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Từ ngày ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(659, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Đến ngày";
            // 
            // timeNgaybatdau
            // 
            this.timeNgaybatdau.Checked = false;
            this.timeNgaybatdau.CustomFormat = "dd/MM/yyyy";
            this.timeNgaybatdau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeNgaybatdau.Location = new System.Drawing.Point(473, 75);
            this.timeNgaybatdau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timeNgaybatdau.Name = "timeNgaybatdau";
            this.timeNgaybatdau.ShowCheckBox = true;
            this.timeNgaybatdau.Size = new System.Drawing.Size(136, 22);
            this.timeNgaybatdau.TabIndex = 5;
            this.timeNgaybatdau.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.timeNgaybatdau.ValueChanged += new System.EventHandler(this.timeNgaybatdau_ValueChanged);
            // 
            // timeNgayketthuc
            // 
            this.timeNgayketthuc.Checked = false;
            this.timeNgayketthuc.CustomFormat = "dd/MM/yyyy";
            this.timeNgayketthuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeNgayketthuc.Location = new System.Drawing.Point(753, 74);
            this.timeNgayketthuc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timeNgayketthuc.Name = "timeNgayketthuc";
            this.timeNgayketthuc.ShowCheckBox = true;
            this.timeNgayketthuc.Size = new System.Drawing.Size(143, 22);
            this.timeNgayketthuc.TabIndex = 6;
            this.timeNgayketthuc.Value = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.timeNgayketthuc.ValueChanged += new System.EventHandler(this.timeNgayketthuc_ValueChanged);
            // 
            // BtnTimkiem
            // 
            this.BtnTimkiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTimkiem.AutoSize = true;
            this.BtnTimkiem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnTimkiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.BtnTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTimkiem.ForeColor = System.Drawing.Color.White;
            this.BtnTimkiem.Location = new System.Drawing.Point(1287, 66);
            this.BtnTimkiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTimkiem.Name = "BtnTimkiem";
            this.BtnTimkiem.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnTimkiem.Size = new System.Drawing.Size(103, 40);
            this.BtnTimkiem.TabIndex = 7;
            this.BtnTimkiem.Text = "Tìm kiếm";
            this.BtnTimkiem.UseVisualStyleBackColor = false;
            this.BtnTimkiem.Click += new System.EventHandler(this.BtnTimkiem_Click);
            // 
            // BtnThemmoi
            // 
            this.BtnThemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnThemmoi.AutoSize = true;
            this.BtnThemmoi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnThemmoi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnThemmoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnThemmoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnThemmoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnThemmoi.Location = new System.Drawing.Point(1419, 67);
            this.BtnThemmoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnThemmoi.Name = "BtnThemmoi";
            this.BtnThemmoi.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnThemmoi.Size = new System.Drawing.Size(109, 40);
            this.BtnThemmoi.TabIndex = 8;
            this.BtnThemmoi.Text = "Thêm mới";
            this.BtnThemmoi.UseVisualStyleBackColor = false;
            this.BtnThemmoi.Click += new System.EventHandler(this.BtnThemmoi_Click);
            // 
            // DGV_BaoCaoKV
            // 
            this.DGV_BaoCaoKV.AllowUserToAddRows = false;
            this.DGV_BaoCaoKV.AllowUserToDeleteRows = false;
            this.DGV_BaoCaoKV.AllowUserToOrderColumns = true;
            this.DGV_BaoCaoKV.AllowUserToResizeRows = false;
            this.DGV_BaoCaoKV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_BaoCaoKV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGV_BaoCaoKV.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_BaoCaoKV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_BaoCaoKV.ColumnHeadersHeight = 30;
            this.DGV_BaoCaoKV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_BaoCaoKV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvStt,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.cotXuatExcel,
            this.Export,
            this.DoiRPBMSua,
            this.DoiRPBMXoa});
            this.DGV_BaoCaoKV.EnableHeadersVisualStyles = false;
            this.DGV_BaoCaoKV.Location = new System.Drawing.Point(27, 132);
            this.DGV_BaoCaoKV.Margin = new System.Windows.Forms.Padding(4);
            this.DGV_BaoCaoKV.MultiSelect = false;
            this.DGV_BaoCaoKV.Name = "DGV_BaoCaoKV";
            this.DGV_BaoCaoKV.ReadOnly = true;
            this.DGV_BaoCaoKV.RowHeadersVisible = false;
            this.DGV_BaoCaoKV.RowHeadersWidth = 51;
            this.DGV_BaoCaoKV.Size = new System.Drawing.Size(1501, 825);
            this.DGV_BaoCaoKV.TabIndex = 24;
            this.DGV_BaoCaoKV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            // Column1
            // 
            this.Column1.HeaderText = "Tên Dự án";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Hợp phần";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Tỉnh";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Huyện";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Xã";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Thời gian";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // cotXuatExcel
            // 
            this.cotXuatExcel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.cotXuatExcel.HeaderText = "Excel";
            this.cotXuatExcel.Image = global::DieuHanhCongTruong.Properties.Resources.excel;
            this.cotXuatExcel.MinimumWidth = 6;
            this.cotXuatExcel.Name = "cotXuatExcel";
            this.cotXuatExcel.ReadOnly = true;
            this.cotXuatExcel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cotXuatExcel.Width = 47;
            // 
            // Export
            // 
            this.Export.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Export.HeaderText = "JSON";
            this.Export.Image = global::DieuHanhCongTruong.Properties.Resources.create;
            this.Export.MinimumWidth = 6;
            this.Export.Name = "Export";
            this.Export.ReadOnly = true;
            this.Export.Width = 51;
            // 
            // DoiRPBMSua
            // 
            this.DoiRPBMSua.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DoiRPBMSua.FillWeight = 106.599F;
            this.DoiRPBMSua.HeaderText = "Sửa";
            this.DoiRPBMSua.Image = global::DieuHanhCongTruong.Properties.Resources.Modify;
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
            this.DoiRPBMXoa.Image = global::DieuHanhCongTruong.Properties.Resources.DeleteRed;
            this.DoiRPBMXoa.MinimumWidth = 6;
            this.DoiRPBMXoa.Name = "DoiRPBMXoa";
            this.DoiRPBMXoa.ReadOnly = true;
            this.DoiRPBMXoa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DoiRPBMXoa.Width = 39;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "(*.xls)|*.xls";
            // 
            // BtnReset
            // 
            this.BtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReset.AutoSize = true;
            this.BtnReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnReset.ForeColor = System.Drawing.Color.White;
            this.BtnReset.Location = new System.Drawing.Point(1148, 67);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnReset.Size = new System.Drawing.Size(109, 39);
            this.BtnReset.TabIndex = 191;
            this.BtnReset.Text = "Xóa bộ lọc";
            this.BtnReset.UseVisualStyleBackColor = false;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(622, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(417, 20);
            this.label2.TabIndex = 192;
            this.label2.Text = "BÁO CÁO HOÀN THÀNH KHẢO SÁT MỘT KHU VỰC";
            // 
            // FrmBaocaoKV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1555, 986);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.DGV_BaoCaoKV);
            this.Controls.Add(this.BtnThemmoi);
            this.Controls.Add(this.BtnTimkiem);
            this.Controls.Add(this.timeNgayketthuc);
            this.Controls.Add(this.timeNgaybatdau);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtTImkiem);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmBaocaoKV";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO HOÀN THÀNH KHẢO SÁT MỘT KHU VỰC";
            this.Load += new System.EventHandler(this.FrmBaocaoKQ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_BaoCaoKV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtTImkiem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker timeNgaybatdau;
        private System.Windows.Forms.DateTimePicker timeNgayketthuc;
        private System.Windows.Forms.Button BtnTimkiem;
        private System.Windows.Forms.Button BtnThemmoi;
        private System.Windows.Forms.DataGridView DGV_BaoCaoKV;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewImageColumn cotXuatExcel;
        private System.Windows.Forms.DataGridViewImageColumn Export;
        private System.Windows.Forms.DataGridViewImageColumn DoiRPBMSua;
        private System.Windows.Forms.DataGridViewImageColumn DoiRPBMXoa;
        private System.Windows.Forms.Label label2;
    }
}