namespace VNRaPaBomMin
{
    partial class SelectDieuHanhRPBMForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbkCapNhatDuLieu = new System.Windows.Forms.CheckBox();
            this.DGVVungDaLuu = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbDieuHanhMoi = new System.Windows.Forms.RadioButton();
            this.rbDieuHanhDaCo = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVVungDaLuu)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbkCapNhatDuLieu);
            this.groupBox1.Controls.Add(this.DGVVungDaLuu);
            this.groupBox1.Location = new System.Drawing.Point(16, 71);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(644, 282);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cbkCapNhatDuLieu
            // 
            this.cbkCapNhatDuLieu.AutoSize = true;
            this.cbkCapNhatDuLieu.Location = new System.Drawing.Point(8, 23);
            this.cbkCapNhatDuLieu.Margin = new System.Windows.Forms.Padding(4);
            this.cbkCapNhatDuLieu.Name = "cbkCapNhatDuLieu";
            this.cbkCapNhatDuLieu.Size = new System.Drawing.Size(133, 21);
            this.cbkCapNhatDuLieu.TabIndex = 1;
            this.cbkCapNhatDuLieu.Text = "Cập nhật dữ liệu";
            this.cbkCapNhatDuLieu.UseVisualStyleBackColor = true;
            // 
            // DGVVungDaLuu
            // 
            this.DGVVungDaLuu.AllowUserToAddRows = false;
            this.DGVVungDaLuu.AllowUserToDeleteRows = false;
            this.DGVVungDaLuu.AllowUserToOrderColumns = true;
            this.DGVVungDaLuu.AllowUserToResizeColumns = false;
            this.DGVVungDaLuu.AllowUserToResizeRows = false;
            this.DGVVungDaLuu.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DGVVungDaLuu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVVungDaLuu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName});
            this.DGVVungDaLuu.Location = new System.Drawing.Point(8, 52);
            this.DGVVungDaLuu.Margin = new System.Windows.Forms.Padding(4);
            this.DGVVungDaLuu.MultiSelect = false;
            this.DGVVungDaLuu.Name = "DGVVungDaLuu";
            this.DGVVungDaLuu.ReadOnly = true;
            this.DGVVungDaLuu.RowHeadersVisible = false;
            this.DGVVungDaLuu.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DGVVungDaLuu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVVungDaLuu.Size = new System.Drawing.Size(628, 223);
            this.DGVVungDaLuu.TabIndex = 0;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "Tên tệp";
            this.FileName.MinimumWidth = 6;
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 455;
            // 
            // rbDieuHanhMoi
            // 
            this.rbDieuHanhMoi.AutoSize = true;
            this.rbDieuHanhMoi.Checked = true;
            this.rbDieuHanhMoi.Location = new System.Drawing.Point(16, 15);
            this.rbDieuHanhMoi.Margin = new System.Windows.Forms.Padding(4);
            this.rbDieuHanhMoi.Name = "rbDieuHanhMoi";
            this.rbDieuHanhMoi.Size = new System.Drawing.Size(226, 21);
            this.rbDieuHanhMoi.TabIndex = 0;
            this.rbDieuHanhMoi.TabStop = true;
            this.rbDieuHanhMoi.Text = "Điều hành với file phân tích mới";
            this.rbDieuHanhMoi.UseVisualStyleBackColor = true;
            this.rbDieuHanhMoi.CheckedChanged += new System.EventHandler(this.rbDieuHanhMoi_CheckedChanged);
            // 
            // rbDieuHanhDaCo
            // 
            this.rbDieuHanhDaCo.AutoSize = true;
            this.rbDieuHanhDaCo.Location = new System.Drawing.Point(16, 43);
            this.rbDieuHanhDaCo.Margin = new System.Windows.Forms.Padding(4);
            this.rbDieuHanhDaCo.Name = "rbDieuHanhDaCo";
            this.rbDieuHanhDaCo.Size = new System.Drawing.Size(151, 21);
            this.rbDieuHanhDaCo.TabIndex = 1;
            this.rbDieuHanhDaCo.Text = "Mở file phân tích cũ";
            this.rbDieuHanhDaCo.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(447, 361);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnCancel.Size = new System.Drawing.Size(76, 39);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(541, 362);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnOk.Size = new System.Drawing.Size(111, 39);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Chấp nhận";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // SelectDieuHanhRPBMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 414);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rbDieuHanhDaCo);
            this.Controls.Add(this.rbDieuHanhMoi);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDieuHanhRPBMForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHỌN CÁCH ĐIỀU HÀNH";
            this.Load += new System.EventHandler(this.SelectDieuHanhRPBMForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVVungDaLuu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGVVungDaLuu;
        private System.Windows.Forms.RadioButton rbDieuHanhMoi;
        private System.Windows.Forms.RadioButton rbDieuHanhDaCo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.CheckBox cbkCapNhatDuLieu;
    }
}