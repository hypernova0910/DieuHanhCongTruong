
namespace VNRaPaBomMin
{
    partial class FrmKHKSKT
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
            this.BtnThemmoi = new System.Windows.Forms.Button();
            this.dgvKHDTBMVN = new System.Windows.Forms.DataGridView();
            this.cotSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotDuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotDVKS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotExcel = new System.Windows.Forms.DataGridViewImageColumn();
            this.cotJSON = new System.Windows.Forms.DataGridViewImageColumn();
            this.cotSua = new System.Windows.Forms.DataGridViewImageColumn();
            this.cotXoa = new System.Windows.Forms.DataGridViewImageColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.TxtTImkiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeNgayketthuc = new System.Windows.Forms.DateTimePicker();
            this.timeNgaybatdau = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnTimkiem = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKHDTBMVN)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnThemmoi
            // 
            this.BtnThemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnThemmoi.AutoSize = true;
            this.BtnThemmoi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnThemmoi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnThemmoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnThemmoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnThemmoi.Location = new System.Drawing.Point(1423, 66);
            this.BtnThemmoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnThemmoi.Name = "BtnThemmoi";
            this.BtnThemmoi.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnThemmoi.Size = new System.Drawing.Size(104, 39);
            this.BtnThemmoi.TabIndex = 9;
            this.BtnThemmoi.Text = "Thêm mới";
            this.BtnThemmoi.UseVisualStyleBackColor = false;
            this.BtnThemmoi.Click += new System.EventHandler(this.BtnThemmoi_Click);
            // 
            // dgvKHDTBMVN
            // 
            this.dgvKHDTBMVN.AllowUserToAddRows = false;
            this.dgvKHDTBMVN.AllowUserToDeleteRows = false;
            this.dgvKHDTBMVN.AllowUserToResizeColumns = false;
            this.dgvKHDTBMVN.AllowUserToResizeRows = false;
            this.dgvKHDTBMVN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKHDTBMVN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKHDTBMVN.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKHDTBMVN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKHDTBMVN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotSTT,
            this.cotDuan,
            this.cotDVKS,
            this.cotExcel,
            this.cotJSON,
            this.cotSua,
            this.cotXoa});
            this.dgvKHDTBMVN.EnableHeadersVisualStyles = false;
            this.dgvKHDTBMVN.Location = new System.Drawing.Point(12, 135);
            this.dgvKHDTBMVN.Name = "dgvKHDTBMVN";
            this.dgvKHDTBMVN.RowHeadersVisible = false;
            this.dgvKHDTBMVN.RowHeadersWidth = 51;
            this.dgvKHDTBMVN.RowTemplate.Height = 24;
            this.dgvKHDTBMVN.Size = new System.Drawing.Size(1515, 818);
            this.dgvKHDTBMVN.TabIndex = 10;
            this.dgvKHDTBMVN.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKHDTBMVN_CellContentClick);
            // 
            // cotSTT
            // 
            this.cotSTT.FillWeight = 20F;
            this.cotSTT.HeaderText = "STT";
            this.cotSTT.MinimumWidth = 6;
            this.cotSTT.Name = "cotSTT";
            // 
            // cotDuan
            // 
            this.cotDuan.HeaderText = "Dự án";
            this.cotDuan.MinimumWidth = 6;
            this.cotDuan.Name = "cotDuan";
            // 
            // cotDVKS
            // 
            this.cotDVKS.HeaderText = "Đơn vị khảo sát";
            this.cotDVKS.MinimumWidth = 6;
            this.cotDVKS.Name = "cotDVKS";
            // 
            // cotExcel
            // 
            this.cotExcel.FillWeight = 20F;
            this.cotExcel.HeaderText = "Excel";
            this.cotExcel.Image = global::DieuHanhCongTruong.Properties.Resources.excel;
            this.cotExcel.MinimumWidth = 6;
            this.cotExcel.Name = "cotExcel";
            this.cotExcel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cotExcel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cotJSON
            // 
            this.cotJSON.FillWeight = 20F;
            this.cotJSON.HeaderText = "JSON";
            this.cotJSON.Image = global::DieuHanhCongTruong.Properties.Resources.create;
            this.cotJSON.MinimumWidth = 6;
            this.cotJSON.Name = "cotJSON";
            this.cotJSON.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cotJSON.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cotSua
            // 
            this.cotSua.FillWeight = 20F;
            this.cotSua.HeaderText = "Sửa";
            this.cotSua.Image = global::DieuHanhCongTruong.Properties.Resources.Modify;
            this.cotSua.MinimumWidth = 6;
            this.cotSua.Name = "cotSua";
            this.cotSua.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cotSua.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cotXoa
            // 
            this.cotXoa.FillWeight = 20F;
            this.cotXoa.HeaderText = "Xóa";
            this.cotXoa.Image = global::DieuHanhCongTruong.Properties.Resources.DeleteRed;
            this.cotXoa.MinimumWidth = 6;
            this.cotXoa.Name = "cotXoa";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "(*.xls)|*.xls";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // TxtTImkiem
            // 
            this.TxtTImkiem.Location = new System.Drawing.Point(72, 71);
            this.TxtTImkiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtTImkiem.Name = "TxtTImkiem";
            this.TxtTImkiem.Size = new System.Drawing.Size(332, 22);
            this.TxtTImkiem.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Dự án ";
            // 
            // timeNgayketthuc
            // 
            this.timeNgayketthuc.Checked = false;
            this.timeNgayketthuc.CustomFormat = "dd/MM/yyyy";
            this.timeNgayketthuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeNgayketthuc.Location = new System.Drawing.Point(758, 72);
            this.timeNgayketthuc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timeNgayketthuc.Name = "timeNgayketthuc";
            this.timeNgayketthuc.ShowCheckBox = true;
            this.timeNgayketthuc.Size = new System.Drawing.Size(145, 22);
            this.timeNgayketthuc.TabIndex = 38;
            this.timeNgayketthuc.Value = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.timeNgayketthuc.ValueChanged += new System.EventHandler(this.timeNgayketthuc_ValueChanged);
            // 
            // timeNgaybatdau
            // 
            this.timeNgaybatdau.Checked = false;
            this.timeNgaybatdau.CustomFormat = "dd/MM/yyyy";
            this.timeNgaybatdau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeNgaybatdau.Location = new System.Drawing.Point(509, 71);
            this.timeNgaybatdau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timeNgaybatdau.Name = "timeNgaybatdau";
            this.timeNgaybatdau.ShowCheckBox = true;
            this.timeNgaybatdau.Size = new System.Drawing.Size(151, 22);
            this.timeNgaybatdau.TabIndex = 37;
            this.timeNgaybatdau.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.timeNgaybatdau.ValueChanged += new System.EventHandler(this.timeNgaybatdau_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(682, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 36;
            this.label4.Text = "Đến ngày";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 35;
            this.label3.Text = "Từ ngày ";
            // 
            // BtnTimkiem
            // 
            this.BtnTimkiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTimkiem.AutoSize = true;
            this.BtnTimkiem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnTimkiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.BtnTimkiem.ForeColor = System.Drawing.Color.White;
            this.BtnTimkiem.Location = new System.Drawing.Point(1301, 66);
            this.BtnTimkiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTimkiem.Name = "BtnTimkiem";
            this.BtnTimkiem.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnTimkiem.Size = new System.Drawing.Size(98, 39);
            this.BtnTimkiem.TabIndex = 39;
            this.BtnTimkiem.Text = "Tìm kiếm";
            this.BtnTimkiem.UseVisualStyleBackColor = false;
            this.BtnTimkiem.Click += new System.EventHandler(this.BtnTimkiem_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReset.AutoSize = true;
            this.BtnReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnReset.ForeColor = System.Drawing.Color.White;
            this.BtnReset.Location = new System.Drawing.Point(1173, 66);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.BtnReset.Size = new System.Drawing.Size(109, 39);
            this.BtnReset.TabIndex = 189;
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
            this.label2.Location = new System.Drawing.Point(466, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(664, 20);
            this.label2.TabIndex = 190;
            this.label2.Text = "KẾ HOẠCH KHẢO SÁT KỸ THUẬT XÁC ĐỊNH KHU VỰC Ô NHIỄM BOM MÌN VẬT NỔ";
            // 
            // FrmKHKSKT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 986);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.BtnTimkiem);
            this.Controls.Add(this.timeNgayketthuc);
            this.Controls.Add(this.timeNgaybatdau);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtTImkiem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvKHDTBMVN);
            this.Controls.Add(this.BtnThemmoi);
            this.Name = "FrmKHKSKT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KẾ HOẠCH KHẢO SÁT KỸ THUẬT XÁC ĐỊNH KHU VỰC Ô NHIỄM BOM MÌN VẬT NỔ";
            this.Load += new System.EventHandler(this.FrmKHDTBMVN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKHDTBMVN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnThemmoi;
        private System.Windows.Forms.DataGridView dgvKHDTBMVN;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotDuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotDVKS;
        private System.Windows.Forms.DataGridViewImageColumn cotExcel;
        private System.Windows.Forms.DataGridViewImageColumn cotJSON;
        private System.Windows.Forms.DataGridViewImageColumn cotSua;
        private System.Windows.Forms.DataGridViewImageColumn cotXoa;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox TxtTImkiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker timeNgayketthuc;
        private System.Windows.Forms.DateTimePicker timeNgaybatdau;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnTimkiem;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label2;
    }
}