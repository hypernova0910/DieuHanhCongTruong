namespace VNRaPaBomMin
{
    partial class frmChangePass
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
            this.tbMatKhauCu = new System.Windows.Forms.TextBox();
            this.tbMatKhauMoi = new System.Windows.Forms.TextBox();
            this.tbMatKhauMoiRp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ViewPasscheckBox = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMatKhauCu
            // 
            this.tbMatKhauCu.Location = new System.Drawing.Point(148, 12);
            this.tbMatKhauCu.Margin = new System.Windows.Forms.Padding(4);
            this.tbMatKhauCu.Name = "tbMatKhauCu";
            this.tbMatKhauCu.PasswordChar = '*';
            this.tbMatKhauCu.Size = new System.Drawing.Size(256, 22);
            this.tbMatKhauCu.TabIndex = 1;
            // 
            // tbMatKhauMoi
            // 
            this.tbMatKhauMoi.Location = new System.Drawing.Point(148, 44);
            this.tbMatKhauMoi.Margin = new System.Windows.Forms.Padding(4);
            this.tbMatKhauMoi.Name = "tbMatKhauMoi";
            this.tbMatKhauMoi.PasswordChar = '*';
            this.tbMatKhauMoi.Size = new System.Drawing.Size(256, 22);
            this.tbMatKhauMoi.TabIndex = 2;
            // 
            // tbMatKhauMoiRp
            // 
            this.tbMatKhauMoiRp.Location = new System.Drawing.Point(148, 76);
            this.tbMatKhauMoiRp.Margin = new System.Windows.Forms.Padding(4);
            this.tbMatKhauMoiRp.Name = "tbMatKhauMoiRp";
            this.tbMatKhauMoiRp.PasswordChar = '*';
            this.tbMatKhauMoiRp.Size = new System.Drawing.Size(256, 22);
            this.tbMatKhauMoiRp.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mật khẩu cũ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mật khẩu mới";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nhập lại mật khẩu";
            // 
            // ViewPasscheckBox
            // 
            this.ViewPasscheckBox.AutoSize = true;
            this.ViewPasscheckBox.Location = new System.Drawing.Point(411, 13);
            this.ViewPasscheckBox.Name = "ViewPasscheckBox";
            this.ViewPasscheckBox.Size = new System.Drawing.Size(140, 21);
            this.ViewPasscheckBox.TabIndex = 6;
            this.ViewPasscheckBox.Text = "Hiện thị mật khẩu";
            this.ViewPasscheckBox.UseVisualStyleBackColor = true;
            this.ViewPasscheckBox.CheckedChanged += new System.EventHandler(this.ViewPasscheckBox_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(485, 123);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonSave.Size = new System.Drawing.Size(66, 40);
            this.buttonSave.TabIndex = 659;
            this.buttonSave.Text = "Lưu";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.btchapnhan_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(375, 123);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(78, 40);
            this.buttonClose.TabIndex = 658;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // frmChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 191);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.ViewPasscheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMatKhauMoiRp);
            this.Controls.Add(this.tbMatKhauMoi);
            this.Controls.Add(this.tbMatKhauCu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi mật khẩu";
            this.Load += new System.EventHandler(this.frmChangePass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbMatKhauCu;
        private System.Windows.Forms.TextBox tbMatKhauMoi;
        private System.Windows.Forms.TextBox tbMatKhauMoiRp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ViewPasscheckBox;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
    }
}