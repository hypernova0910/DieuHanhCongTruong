
namespace DieuHanhCongTruong
{
    partial class MenuLoaderManagerFrm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btchapnhan = new System.Windows.Forms.Button();
            this.nudMinMine = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudMinBomb = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.nudMinClusterSize = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudNguongMin = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudNguongBom = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudRanhDoPT = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudKhoangPT = new System.Windows.Forms.NumericUpDown();
            this.lbTepDuongDan = new System.Windows.Forms.Label();
            this.btDuongDan = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPhanTichOnline = new System.Windows.Forms.TextBox();
            this.tbKhoangThoiGian = new System.Windows.Forms.TextBox();
            this.tbHeSoMayDoMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHeSoMayDoBom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinMine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinBomb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinClusterSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNguongMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNguongBom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRanhDoPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKhoangPT)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(404, 778);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnCancel.Size = new System.Drawing.Size(76, 39);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btchapnhan
            // 
            this.btchapnhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btchapnhan.AutoSize = true;
            this.btchapnhan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btchapnhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btchapnhan.ForeColor = System.Drawing.Color.White;
            this.btchapnhan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btchapnhan.Location = new System.Drawing.Point(515, 778);
            this.btchapnhan.Margin = new System.Windows.Forms.Padding(4);
            this.btchapnhan.Name = "btchapnhan";
            this.btchapnhan.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btchapnhan.Size = new System.Drawing.Size(66, 39);
            this.btchapnhan.TabIndex = 10;
            this.btchapnhan.Text = "Lưu";
            this.btchapnhan.UseVisualStyleBackColor = false;
            this.btchapnhan.Click += new System.EventHandler(this.btchapnhan_Click);
            // 
            // nudMinMine
            // 
            this.nudMinMine.DecimalPlaces = 2;
            this.nudMinMine.Location = new System.Drawing.Point(303, 480);
            this.nudMinMine.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMinMine.Name = "nudMinMine";
            this.nudMinMine.Size = new System.Drawing.Size(276, 22);
            this.nudMinMine.TabIndex = 56;
            this.nudMinMine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudMinMine.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(29, 480);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 17);
            this.label11.TabIndex = 55;
            this.label11.Text = "Ngưỡng tối thiểu có mìn";
            // 
            // nudMinBomb
            // 
            this.nudMinBomb.DecimalPlaces = 2;
            this.nudMinBomb.Location = new System.Drawing.Point(303, 432);
            this.nudMinBomb.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMinBomb.Name = "nudMinBomb";
            this.nudMinBomb.Size = new System.Drawing.Size(276, 22);
            this.nudMinBomb.TabIndex = 54;
            this.nudMinBomb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudMinBomb.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(29, 432);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(162, 17);
            this.label12.TabIndex = 53;
            this.label12.Text = "Ngưỡng tối thiểu có bom";
            // 
            // nudMinClusterSize
            // 
            this.nudMinClusterSize.Location = new System.Drawing.Point(303, 380);
            this.nudMinClusterSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMinClusterSize.Name = "nudMinClusterSize";
            this.nudMinClusterSize.Size = new System.Drawing.Size(276, 22);
            this.nudMinClusterSize.TabIndex = 52;
            this.nudMinClusterSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudMinClusterSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 380);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(177, 17);
            this.label10.TabIndex = 51;
            this.label10.Text = "Số bom tối thiểu trong cụm";
            // 
            // nudNguongMin
            // 
            this.nudNguongMin.DecimalPlaces = 2;
            this.nudNguongMin.Location = new System.Drawing.Point(303, 330);
            this.nudNguongMin.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudNguongMin.Name = "nudNguongMin";
            this.nudNguongMin.Size = new System.Drawing.Size(276, 22);
            this.nudNguongMin.TabIndex = 50;
            this.nudNguongMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNguongMin.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 330);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 17);
            this.label9.TabIndex = 49;
            this.label9.Text = "Ngưỡng mìn";
            // 
            // nudNguongBom
            // 
            this.nudNguongBom.DecimalPlaces = 2;
            this.nudNguongBom.Location = new System.Drawing.Point(303, 282);
            this.nudNguongBom.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudNguongBom.Name = "nudNguongBom";
            this.nudNguongBom.Size = new System.Drawing.Size(276, 22);
            this.nudNguongBom.TabIndex = 48;
            this.nudNguongBom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNguongBom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 282);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 17);
            this.label8.TabIndex = 47;
            this.label8.Text = "Ngưỡng bom";
            // 
            // nudRanhDoPT
            // 
            this.nudRanhDoPT.DecimalPlaces = 2;
            this.nudRanhDoPT.Location = new System.Drawing.Point(303, 234);
            this.nudRanhDoPT.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudRanhDoPT.Name = "nudRanhDoPT";
            this.nudRanhDoPT.Size = new System.Drawing.Size(276, 22);
            this.nudRanhDoPT.TabIndex = 46;
            this.nudRanhDoPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudRanhDoPT.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 234);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 17);
            this.label6.TabIndex = 45;
            this.label6.Text = "Rãnh dò phân tích (m)";
            // 
            // nudKhoangPT
            // 
            this.nudKhoangPT.Location = new System.Drawing.Point(303, 187);
            this.nudKhoangPT.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudKhoangPT.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudKhoangPT.Name = "nudKhoangPT";
            this.nudKhoangPT.Size = new System.Drawing.Size(276, 22);
            this.nudKhoangPT.TabIndex = 44;
            this.nudKhoangPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudKhoangPT.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lbTepDuongDan
            // 
            this.lbTepDuongDan.AutoSize = true;
            this.lbTepDuongDan.Location = new System.Drawing.Point(375, 537);
            this.lbTepDuongDan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTepDuongDan.Name = "lbTepDuongDan";
            this.lbTepDuongDan.Size = new System.Drawing.Size(20, 17);
            this.lbTepDuongDan.TabIndex = 43;
            this.lbTepDuongDan.Text = "...";
            // 
            // btDuongDan
            // 
            this.btDuongDan.Location = new System.Drawing.Point(303, 531);
            this.btDuongDan.Margin = new System.Windows.Forms.Padding(4);
            this.btDuongDan.Name = "btDuongDan";
            this.btDuongDan.Size = new System.Drawing.Size(64, 28);
            this.btDuongDan.TabIndex = 41;
            this.btDuongDan.Text = "Chọn";
            this.btDuongDan.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(29, 537);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 28);
            this.label7.TabIndex = 42;
            this.label7.Text = "Đường dẫn lưu tệp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 187);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 17);
            this.label5.TabIndex = 40;
            this.label5.Text = "Khoảng phân tích";
            // 
            // tbPhanTichOnline
            // 
            this.tbPhanTichOnline.Location = new System.Drawing.Point(303, 145);
            this.tbPhanTichOnline.Margin = new System.Windows.Forms.Padding(4);
            this.tbPhanTichOnline.Name = "tbPhanTichOnline";
            this.tbPhanTichOnline.Size = new System.Drawing.Size(276, 22);
            this.tbPhanTichOnline.TabIndex = 39;
            // 
            // tbKhoangThoiGian
            // 
            this.tbKhoangThoiGian.Location = new System.Drawing.Point(303, 106);
            this.tbKhoangThoiGian.Margin = new System.Windows.Forms.Padding(4);
            this.tbKhoangThoiGian.Name = "tbKhoangThoiGian";
            this.tbKhoangThoiGian.Size = new System.Drawing.Size(276, 22);
            this.tbKhoangThoiGian.TabIndex = 38;
            // 
            // tbHeSoMayDoMin
            // 
            this.tbHeSoMayDoMin.Location = new System.Drawing.Point(303, 66);
            this.tbHeSoMayDoMin.Margin = new System.Windows.Forms.Padding(4);
            this.tbHeSoMayDoMin.Name = "tbHeSoMayDoMin";
            this.tbHeSoMayDoMin.Size = new System.Drawing.Size(276, 22);
            this.tbHeSoMayDoMin.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 145);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 17);
            this.label4.TabIndex = 34;
            this.label4.Text = "Khoảng cập nhật phân tích Online";
            // 
            // tbHeSoMayDoBom
            // 
            this.tbHeSoMayDoBom.Location = new System.Drawing.Point(303, 27);
            this.tbHeSoMayDoBom.Margin = new System.Windows.Forms.Padding(4);
            this.tbHeSoMayDoBom.Name = "tbHeSoMayDoBom";
            this.tbHeSoMayDoBom.Size = new System.Drawing.Size(276, 22);
            this.tbHeSoMayDoBom.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "Khoảng thời gian hiển thị điểm(Giây)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Hệ số từ trường máy dò mìn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 17);
            this.label1.TabIndex = 32;
            this.label1.Text = "Hệ số từ trường máy dò bom";
            // 
            // MenuLoaderManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 830);
            this.Controls.Add(this.nudMinMine);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nudMinBomb);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.nudMinClusterSize);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nudNguongMin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nudNguongBom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudRanhDoPT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudKhoangPT);
            this.Controls.Add(this.lbTepDuongDan);
            this.Controls.Add(this.btDuongDan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPhanTichOnline);
            this.Controls.Add(this.tbKhoangThoiGian);
            this.Controls.Add(this.tbHeSoMayDoMin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbHeSoMayDoBom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btchapnhan);
            this.Controls.Add(this.btnCancel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(625, 877);
            this.Name = "MenuLoaderManagerFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tùy chỉnh Ribbon";
            this.Load += new System.EventHandler(this.MenuLoaderManagerFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinMine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinBomb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinClusterSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNguongMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNguongBom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRanhDoPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKhoangPT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btchapnhan;
        private System.Windows.Forms.NumericUpDown nudMinMine;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudMinBomb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudMinClusterSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudNguongMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudNguongBom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudRanhDoPT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudKhoangPT;
        private System.Windows.Forms.Label lbTepDuongDan;
        private System.Windows.Forms.Button btDuongDan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox tbPhanTichOnline;
        public System.Windows.Forms.TextBox tbKhoangThoiGian;
        public System.Windows.Forms.TextBox tbHeSoMayDoMin;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tbHeSoMayDoBom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}