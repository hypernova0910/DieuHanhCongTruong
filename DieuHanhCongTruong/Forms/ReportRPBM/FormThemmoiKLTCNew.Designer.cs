
namespace DieuHanhCongTruong.ReportRPBM
{
    partial class FormThemmoiKLTCNew
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
            this.components = new System.ComponentModel.Container();
            this.nudKLHD = new System.Windows.Forms.NumericUpDown();
            this.tbCongViecKhac = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbCongViec = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDauViec = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtString2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudKLTC = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudKLNT = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudKLHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKLTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKLNT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // nudKLHD
            // 
            this.nudKLHD.DecimalPlaces = 3;
            this.nudKLHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudKLHD.Location = new System.Drawing.Point(217, 142);
            this.nudKLHD.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudKLHD.Name = "nudKLHD";
            this.nudKLHD.Size = new System.Drawing.Size(294, 26);
            this.nudKLHD.TabIndex = 683;
            this.nudKLHD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbCongViecKhac
            // 
            this.tbCongViecKhac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCongViecKhac.Location = new System.Drawing.Point(217, 87);
            this.tbCongViecKhac.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCongViecKhac.Name = "tbCongViecKhac";
            this.tbCongViecKhac.Size = new System.Drawing.Size(294, 26);
            this.tbCongViecKhac.TabIndex = 682;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 20);
            this.label7.TabIndex = 681;
            this.label7.Text = "Công việc khác";
            // 
            // cbCongViec
            // 
            this.cbCongViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCongViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCongViec.FormattingEnabled = true;
            this.cbCongViec.Location = new System.Drawing.Point(757, 30);
            this.cbCongViec.Name = "cbCongViec";
            this.cbCongViec.Size = new System.Drawing.Size(294, 28);
            this.cbCongViec.TabIndex = 680;
            this.cbCongViec.SelectedValueChanged += new System.EventHandler(this.cbCongViec_SelectedValueChanged);
            this.cbCongViec.Validating += new System.ComponentModel.CancelEventHandler(this.cbCongViec_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(678, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 20);
            this.label5.TabIndex = 679;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(558, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 20);
            this.label6.TabIndex = 678;
            this.label6.Text = "Tên công việc: ";
            // 
            // cbDauViec
            // 
            this.cbDauViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDauViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDauViec.FormattingEnabled = true;
            this.cbDauViec.Location = new System.Drawing.Point(217, 30);
            this.cbDauViec.Name = "cbDauViec";
            this.cbDauViec.Size = new System.Drawing.Size(294, 28);
            this.cbDauViec.TabIndex = 677;
            this.cbDauViec.SelectedValueChanged += new System.EventHandler(this.cbDauViec_SelectedValueChanged);
            this.cbDauViec.Validating += new System.ComponentModel.CancelEventHandler(this.cbDauViec_Validating);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.Red;
            this.label55.Location = new System.Drawing.Point(138, 27);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(15, 20);
            this.label55.TabIndex = 676;
            this.label55.Text = "*";
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(985, 263);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonSave.Size = new System.Drawing.Size(66, 40);
            this.buttonSave.TabIndex = 675;
            this.buttonSave.Text = "Lưu";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClose.CausesValidation = false;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(875, 263);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(78, 40);
            this.buttonClose.TabIndex = 674;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 20);
            this.label4.TabIndex = 673;
            this.label4.Text = "Tên đầu việc: ";
            // 
            // txtString2
            // 
            this.txtString2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtString2.Location = new System.Drawing.Point(757, 84);
            this.txtString2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtString2.Name = "txtString2";
            this.txtString2.Size = new System.Drawing.Size(294, 26);
            this.txtString2.TabIndex = 670;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 20);
            this.label2.TabIndex = 669;
            this.label2.Text = "Khối lượng hợp đồng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(563, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 668;
            this.label1.Text = "Đơn vị tính:";
            // 
            // nudKLTC
            // 
            this.nudKLTC.DecimalPlaces = 3;
            this.nudKLTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudKLTC.Location = new System.Drawing.Point(757, 141);
            this.nudKLTC.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudKLTC.Name = "nudKLTC";
            this.nudKLTC.Size = new System.Drawing.Size(294, 26);
            this.nudKLTC.TabIndex = 685;
            this.nudKLTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(562, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 20);
            this.label8.TabIndex = 684;
            this.label8.Text = "Khối lượng thi công";
            // 
            // nudKLNT
            // 
            this.nudKLNT.DecimalPlaces = 3;
            this.nudKLNT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudKLNT.Location = new System.Drawing.Point(217, 199);
            this.nudKLNT.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudKLNT.Name = "nudKLNT";
            this.nudKLNT.Size = new System.Drawing.Size(294, 26);
            this.nudKLNT.TabIndex = 687;
            this.nudKLNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 198);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 20);
            this.label9.TabIndex = 686;
            this.label9.Text = "Khối lượng nghiệm thu";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormThemmoiKLTCNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 354);
            this.Controls.Add(this.nudKLNT);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nudKLTC);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudKLHD);
            this.Controls.Add(this.tbCongViecKhac);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbCongViec);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbDauViec);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtString2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormThemmoiKLTCNew";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormThemmoiKLTCNew";
            this.Load += new System.EventHandler(this.FormThemmoiKLTCNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudKLHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKLTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKLNT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudKLHD;
        private System.Windows.Forms.TextBox tbCongViecKhac;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbCongViec;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDauViec;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtString2;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudKLTC;
        public System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudKLNT;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}