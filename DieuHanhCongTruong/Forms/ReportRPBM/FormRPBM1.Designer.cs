﻿
namespace DieuHanhCongTruong
{
    partial class FormRPBM1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotXuatExcel = new System.Windows.Forms.DataGridViewImageColumn();
            this.Export = new System.Windows.Forms.DataGridViewImageColumn();
            this.DoiRPBMSua = new System.Windows.Forms.DataGridViewImageColumn();
            this.DoiRPBMXoa = new System.Windows.Forms.DataGridViewImageColumn();
            this.timeNgayketthuc = new System.Windows.Forms.DateTimePicker();
            this.timeNgaybatdau = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnThemmoi = new System.Windows.Forms.Button();
            this.BtnTimkiem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtTImkiem = new System.Windows.Forms.ComboBox();
            this.BtnReset = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvStt,
            this.Column1,
            this.Column2,
            this.Column3,
            this.cotXuatExcel,
            this.Export,
            this.DoiRPBMSua,
            this.DoiRPBMXoa});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(14, 150);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1502, 783);
            this.dataGridView1.TabIndex = 46;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            this.Column1.HeaderText = "Dự án";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Thời gian";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Địa điểm";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // cotXuatExcel
            // 
            this.cotXuatExcel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.cotXuatExcel.HeaderText = "Excel";
            this.cotXuatExcel.Image = global::DieuHanhCongTruong.Properties.Resources.excel;
            this.cotXuatExcel.MinimumWidth = 6;
            this.cotXuatExcel.Name = "cotXuatExcel";
            this.cotXuatExcel.ReadOnly = true;
            this.cotXuatExcel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            // timeNgayketthuc
            // 
            this.timeNgayketthuc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeNgayketthuc.CustomFormat = "dd/MM/yyyy";
            this.timeNgayketthuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeNgayketthuc.Location = new System.Drawing.Point(733, 102);
            this.timeNgayketthuc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timeNgayketthuc.Name = "timeNgayketthuc";
            this.timeNgayketthuc.Size = new System.Drawing.Size(145, 22);
            this.timeNgayketthuc.TabIndex = 45;
            this.timeNgayketthuc.Value = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            // 
            // timeNgaybatdau
            // 
            this.timeNgaybatdau.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeNgaybatdau.CustomFormat = "dd/MM/yyyy";
            this.timeNgaybatdau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeNgaybatdau.Location = new System.Drawing.Point(484, 101);
            this.timeNgaybatdau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timeNgaybatdau.Name = "timeNgaybatdau";
            this.timeNgaybatdau.Size = new System.Drawing.Size(151, 22);
            this.timeNgaybatdau.TabIndex = 44;
            this.timeNgaybatdau.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(657, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 43;
            this.label4.Text = "Đến ngày";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 42;
            this.label3.Text = "Từ ngày ";
            // 
            // BtnThemmoi
            // 
            this.BtnThemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnThemmoi.AutoSize = true;
            this.BtnThemmoi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnThemmoi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnThemmoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnThemmoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnThemmoi.Location = new System.Drawing.Point(1412, 93);
            this.BtnThemmoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnThemmoi.Name = "BtnThemmoi";
            this.BtnThemmoi.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnThemmoi.Size = new System.Drawing.Size(104, 39);
            this.BtnThemmoi.TabIndex = 40;
            this.BtnThemmoi.Text = "Thêm mới";
            this.BtnThemmoi.UseVisualStyleBackColor = false;
            this.BtnThemmoi.Click += new System.EventHandler(this.BtnThemmoi_Click);
            // 
            // BtnTimkiem
            // 
            this.BtnTimkiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTimkiem.AutoSize = true;
            this.BtnTimkiem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnTimkiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.BtnTimkiem.ForeColor = System.Drawing.Color.White;
            this.BtnTimkiem.Location = new System.Drawing.Point(1277, 93);
            this.BtnTimkiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTimkiem.Name = "BtnTimkiem";
            this.BtnTimkiem.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnTimkiem.Size = new System.Drawing.Size(98, 39);
            this.BtnTimkiem.TabIndex = 39;
            this.BtnTimkiem.Text = "Tìm kiếm";
            this.BtnTimkiem.UseVisualStyleBackColor = false;
            this.BtnTimkiem.Click += new System.EventHandler(this.BtnTimkiem_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(644, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 24);
            this.label2.TabIndex = 38;
            this.label2.Text = "BIÊN BẢN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 36;
            this.label1.Text = "Dự án ";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(480, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(470, 24);
            this.label5.TabIndex = 47;
            this.label5.Text = "Bàn giao mặt bằng cho khảo sát rà phá bom mìn vật nổ";
            // 
            // TxtTImkiem
            // 
            this.TxtTImkiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTImkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTImkiem.FormattingEnabled = true;
            this.TxtTImkiem.Location = new System.Drawing.Point(69, 99);
            this.TxtTImkiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtTImkiem.Name = "TxtTImkiem";
            this.TxtTImkiem.Size = new System.Drawing.Size(332, 26);
            this.TxtTImkiem.TabIndex = 171;
            // 
            // BtnReset
            // 
            this.BtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReset.AutoSize = true;
            this.BtnReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnReset.ForeColor = System.Drawing.Color.White;
            this.BtnReset.Location = new System.Drawing.Point(1123, 93);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnReset.Size = new System.Drawing.Size(109, 39);
            this.BtnReset.TabIndex = 172;
            this.BtnReset.Text = "Xóa bộ lọc";
            this.BtnReset.UseVisualStyleBackColor = false;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "(*.xls)|*.xls";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // FormRPBM1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1555, 986);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.TxtTImkiem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.timeNgayketthuc);
            this.Controls.Add(this.timeNgaybatdau);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnThemmoi);
            this.Controls.Add(this.BtnTimkiem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormRPBM1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRPBM1";
            this.Load += new System.EventHandler(this.FormRPBM1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker timeNgayketthuc;
        private System.Windows.Forms.DateTimePicker timeNgaybatdau;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnThemmoi;
        private System.Windows.Forms.Button BtnTimkiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TxtTImkiem;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn cotXuatExcel;
        private System.Windows.Forms.DataGridViewImageColumn Export;
        private System.Windows.Forms.DataGridViewImageColumn DoiRPBMSua;
        private System.Windows.Forms.DataGridViewImageColumn DoiRPBMXoa;
    }
}