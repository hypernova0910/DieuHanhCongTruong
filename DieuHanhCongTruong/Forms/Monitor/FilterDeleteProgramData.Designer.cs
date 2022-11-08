
namespace VNRaPaBomMin
{
    partial class FilterDeleteProgramData
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbKhuVuc = new System.Windows.Forms.ComboBox();
            this.cbOLuoi = new System.Windows.Forms.ComboBox();
            this.dtpTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vùng dự án";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ô lưới";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Thời gian bắt đầu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Thời gian kết thúc";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(322, 226);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnCancel.Size = new System.Drawing.Size(76, 39);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(424, 226);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnOk.Size = new System.Drawing.Size(67, 39);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Xóa";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbKhuVuc
            // 
            this.cbKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhuVuc.FormattingEnabled = true;
            this.cbKhuVuc.Location = new System.Drawing.Point(179, 26);
            this.cbKhuVuc.Name = "cbKhuVuc";
            this.cbKhuVuc.Size = new System.Drawing.Size(312, 24);
            this.cbKhuVuc.TabIndex = 8;
            this.cbKhuVuc.SelectedValueChanged += new System.EventHandler(this.cbKhuVuc_SelectedValueChanged);
            // 
            // cbOLuoi
            // 
            this.cbOLuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOLuoi.FormattingEnabled = true;
            this.cbOLuoi.Location = new System.Drawing.Point(179, 72);
            this.cbOLuoi.Name = "cbOLuoi";
            this.cbOLuoi.Size = new System.Drawing.Size(312, 24);
            this.cbOLuoi.TabIndex = 9;
            // 
            // dtpTimeStart
            // 
            this.dtpTimeStart.Checked = false;
            this.dtpTimeStart.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtpTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeStart.Location = new System.Drawing.Point(179, 122);
            this.dtpTimeStart.Name = "dtpTimeStart";
            this.dtpTimeStart.ShowCheckBox = true;
            this.dtpTimeStart.Size = new System.Drawing.Size(312, 22);
            this.dtpTimeStart.TabIndex = 10;
            this.dtpTimeStart.ValueChanged += new System.EventHandler(this.dtpTimeStart_ValueChanged);
            // 
            // dtpTimeEnd
            // 
            this.dtpTimeEnd.Checked = false;
            this.dtpTimeEnd.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtpTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeEnd.Location = new System.Drawing.Point(179, 173);
            this.dtpTimeEnd.Name = "dtpTimeEnd";
            this.dtpTimeEnd.ShowCheckBox = true;
            this.dtpTimeEnd.Size = new System.Drawing.Size(312, 22);
            this.dtpTimeEnd.TabIndex = 11;
            this.dtpTimeEnd.ValueChanged += new System.EventHandler(this.dtpTimeEnd_ValueChanged);
            // 
            // FilterDeleteProgramData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 289);
            this.Controls.Add(this.dtpTimeEnd);
            this.Controls.Add(this.dtpTimeStart);
            this.Controls.Add(this.cbOLuoi);
            this.Controls.Add(this.cbKhuVuc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FilterDeleteProgramData";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LỌC DỮ LIỆU DỰ ÁN CẦN XÓA";
            this.Load += new System.EventHandler(this.FilterDeleteProgramData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cbKhuVuc;
        private System.Windows.Forms.ComboBox cbOLuoi;
        private System.Windows.Forms.DateTimePicker dtpTimeStart;
        private System.Windows.Forms.DateTimePicker dtpTimeEnd;
    }
}