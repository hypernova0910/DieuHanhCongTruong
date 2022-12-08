namespace VNRaPaBomMin
{
    partial class ChiaManhBanDoOLuoiFrm
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
            this.btchiamanh = new System.Windows.Forms.Button();
            this.buttonCacel = new System.Windows.Forms.Button();
            this.cbOLuoi = new System.Windows.Forms.ComboBox();
            this.cbKhuVuc = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tbDuAn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btchiamanh
            // 
            this.btchiamanh.AutoSize = true;
            this.btchiamanh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btchiamanh.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btchiamanh.ForeColor = System.Drawing.Color.White;
            this.btchiamanh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btchiamanh.Location = new System.Drawing.Point(683, 132);
            this.btchiamanh.Margin = new System.Windows.Forms.Padding(4);
            this.btchiamanh.Name = "btchiamanh";
            this.btchiamanh.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btchiamanh.Size = new System.Drawing.Size(76, 39);
            this.btchiamanh.TabIndex = 43;
            this.btchiamanh.Text = "In";
            this.btchiamanh.UseVisualStyleBackColor = false;
            this.btchiamanh.Click += new System.EventHandler(this.btchiamanh_Click);
            // 
            // buttonCacel
            // 
            this.buttonCacel.AutoSize = true;
            this.buttonCacel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCacel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCacel.Location = new System.Drawing.Point(584, 132);
            this.buttonCacel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCacel.Name = "buttonCacel";
            this.buttonCacel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonCacel.Size = new System.Drawing.Size(76, 39);
            this.buttonCacel.TabIndex = 40;
            this.buttonCacel.Text = "Đóng";
            this.buttonCacel.UseVisualStyleBackColor = true;
            // 
            // cbOLuoi
            // 
            this.cbOLuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOLuoi.FormattingEnabled = true;
            this.cbOLuoi.Location = new System.Drawing.Point(530, 74);
            this.cbOLuoi.Name = "cbOLuoi";
            this.cbOLuoi.Size = new System.Drawing.Size(229, 24);
            this.cbOLuoi.TabIndex = 60;
            this.cbOLuoi.SelectedValueChanged += new System.EventHandler(this.cbOLuoi_SelectedValueChanged);
            // 
            // cbKhuVuc
            // 
            this.cbKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhuVuc.FormattingEnabled = true;
            this.cbKhuVuc.Location = new System.Drawing.Point(117, 74);
            this.cbKhuVuc.Name = "cbKhuVuc";
            this.cbKhuVuc.Size = new System.Drawing.Size(244, 24);
            this.cbKhuVuc.TabIndex = 59;
            this.cbKhuVuc.SelectedValueChanged += new System.EventHandler(this.cbKhuVuc_SelectedValueChanged);
            this.cbKhuVuc.Validating += new System.ComponentModel.CancelEventHandler(this.cbKhuVuc_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(447, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 17);
            this.label13.TabIndex = 58;
            this.label13.Text = "Ô lưới";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 17);
            this.label14.TabIndex = 57;
            this.label14.Text = "Vùng dự án";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Image Files (*.png)|*.png";
            // 
            // tbDuAn
            // 
            this.tbDuAn.Location = new System.Drawing.Point(117, 20);
            this.tbDuAn.Name = "tbDuAn";
            this.tbDuAn.ReadOnly = true;
            this.tbDuAn.Size = new System.Drawing.Size(642, 22);
            this.tbDuAn.TabIndex = 62;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 61;
            this.label2.Text = "Dự án";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.ForeColor = System.Drawing.Color.Red;
            this.label49.Location = new System.Drawing.Point(89, 74);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(13, 17);
            this.label49.TabIndex = 315;
            this.label49.Text = "*";
            // 
            // ChiaManhBanDoOLuoiFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 209);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.tbDuAn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbOLuoi);
            this.Controls.Add(this.cbKhuVuc);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btchiamanh);
            this.Controls.Add(this.buttonCacel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChiaManhBanDoOLuoiFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IN MẢNH BẢN ĐỒ THEO Ô LƯỚI";
            this.Load += new System.EventHandler(this.frmChiaManhBanDo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btchiamanh;
        private System.Windows.Forms.Button buttonCacel;
        private System.Windows.Forms.ComboBox cbOLuoi;
        private System.Windows.Forms.ComboBox cbKhuVuc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox tbDuAn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label49;
    }
}