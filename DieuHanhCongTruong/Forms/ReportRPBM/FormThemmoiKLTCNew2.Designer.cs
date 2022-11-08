
namespace DieuHanhCongTruong.ReportRPBM
{
    partial class FormThemmoiKLTCNew2
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
            this.nudKLTC = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
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
            this.tbGhiChu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudKLTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKLHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // nudKLTC
            // 
            this.nudKLTC.DecimalPlaces = 3;
            this.nudKLTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudKLTC.Location = new System.Drawing.Point(762, 138);
            this.nudKLTC.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudKLTC.Name = "nudKLTC";
            this.nudKLTC.Size = new System.Drawing.Size(294, 26);
            this.nudKLTC.TabIndex = 703;
            this.nudKLTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(567, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 20);
            this.label8.TabIndex = 702;
            this.label8.Text = "Khối lượng thi công";
            // 
            // nudKLHD
            // 
            this.nudKLHD.DecimalPlaces = 3;
            this.nudKLHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudKLHD.Location = new System.Drawing.Point(222, 139);
            this.nudKLHD.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudKLHD.Name = "nudKLHD";
            this.nudKLHD.Size = new System.Drawing.Size(294, 26);
            this.nudKLHD.TabIndex = 701;
            this.nudKLHD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbCongViecKhac
            // 
            this.tbCongViecKhac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCongViecKhac.Location = new System.Drawing.Point(222, 84);
            this.tbCongViecKhac.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCongViecKhac.Name = "tbCongViecKhac";
            this.tbCongViecKhac.Size = new System.Drawing.Size(294, 26);
            this.tbCongViecKhac.TabIndex = 700;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(28, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 20);
            this.label7.TabIndex = 699;
            this.label7.Text = "Công việc khác";
            // 
            // cbCongViec
            // 
            this.cbCongViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCongViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCongViec.FormattingEnabled = true;
            this.cbCongViec.Location = new System.Drawing.Point(762, 27);
            this.cbCongViec.Name = "cbCongViec";
            this.cbCongViec.Size = new System.Drawing.Size(294, 28);
            this.cbCongViec.TabIndex = 698;
            this.cbCongViec.SelectedValueChanged += new System.EventHandler(this.cbCongViec_SelectedValueChanged);
            this.cbCongViec.Validating += new System.ComponentModel.CancelEventHandler(this.cbCongViec_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(683, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 20);
            this.label5.TabIndex = 697;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(563, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 20);
            this.label6.TabIndex = 696;
            this.label6.Text = "Tên công việc: ";
            // 
            // cbDauViec
            // 
            this.cbDauViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDauViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDauViec.FormattingEnabled = true;
            this.cbDauViec.Location = new System.Drawing.Point(222, 27);
            this.cbDauViec.Name = "cbDauViec";
            this.cbDauViec.Size = new System.Drawing.Size(294, 28);
            this.cbDauViec.TabIndex = 695;
            this.cbDauViec.SelectedValueChanged += new System.EventHandler(this.cbDauViec_SelectedValueChanged);
            this.cbDauViec.Validating += new System.ComponentModel.CancelEventHandler(this.cbDauViec_Validating);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.Red;
            this.label55.Location = new System.Drawing.Point(143, 24);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(15, 20);
            this.label55.TabIndex = 694;
            this.label55.Text = "*";
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(990, 260);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonSave.Size = new System.Drawing.Size(66, 40);
            this.buttonSave.TabIndex = 693;
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
            this.buttonClose.Location = new System.Drawing.Point(880, 260);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(78, 40);
            this.buttonClose.TabIndex = 692;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 20);
            this.label4.TabIndex = 691;
            this.label4.Text = "Tên đầu việc: ";
            // 
            // txtString2
            // 
            this.txtString2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtString2.Location = new System.Drawing.Point(762, 81);
            this.txtString2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtString2.Name = "txtString2";
            this.txtString2.Size = new System.Drawing.Size(294, 26);
            this.txtString2.TabIndex = 690;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 20);
            this.label2.TabIndex = 689;
            this.label2.Text = "Khối lượng hợp đồng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(568, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 688;
            this.label1.Text = "Đơn vị tính:";
            // 
            // tbGhiChu
            // 
            this.tbGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGhiChu.Location = new System.Drawing.Point(222, 199);
            this.tbGhiChu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbGhiChu.Name = "tbGhiChu";
            this.tbGhiChu.Size = new System.Drawing.Size(294, 26);
            this.tbGhiChu.TabIndex = 705;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 704;
            this.label3.Text = "Ghi chú";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormThemmoiKLTCNew2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 371);
            this.Controls.Add(this.tbGhiChu);
            this.Controls.Add(this.label3);
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
            this.Name = "FormThemmoiKLTCNew2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormThemmoiKLTCNew2";
            this.Load += new System.EventHandler(this.FormThemmoiKLTCNew2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudKLTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKLHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nudKLTC;
        public System.Windows.Forms.Label label8;
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
        private System.Windows.Forms.TextBox tbGhiChu;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}