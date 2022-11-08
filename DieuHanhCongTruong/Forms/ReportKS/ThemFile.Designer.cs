
namespace VNRaPaBomMin
{
    partial class ThemFile
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
            this.label27 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFileType = new System.Windows.Forms.ComboBox();
            this.btThoat = new System.Windows.Forms.Button();
            this.btLuu = new System.Windows.Forms.Button();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.tbDoc_file = new System.Windows.Forms.LinkLabel();
            this.btOpentbDoc_file = new System.Windows.Forms.Button();
            this.label88 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(23, 95);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(91, 17);
            this.label27.TabIndex = 6;
            this.label27.Text = "File đính kèm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Loại file";
            // 
            // cbFileType
            // 
            this.cbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileType.FormattingEnabled = true;
            this.cbFileType.Items.AddRange(new object[] {
            "Bản đồ xác định vị trí",
            "Sơ đồ/bản đồ tình trạng ô nhiễm BMVN",
            "Bảng tổng hợp khối lượng ĐT",
            "Bảng dự toán KPĐT",
            "Các phụ lục, hướng dẫn thực hiện"});
            this.cbFileType.Location = new System.Drawing.Point(126, 27);
            this.cbFileType.Margin = new System.Windows.Forms.Padding(4);
            this.cbFileType.Name = "cbFileType";
            this.cbFileType.Size = new System.Drawing.Size(470, 24);
            this.cbFileType.TabIndex = 10;
            this.cbFileType.Validating += new System.ComponentModel.CancelEventHandler(this.cbFileType_Validating);
            // 
            // btThoat
            // 
            this.btThoat.AutoSize = true;
            this.btThoat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btThoat.Location = new System.Drawing.Point(409, 145);
            this.btThoat.Margin = new System.Windows.Forms.Padding(4);
            this.btThoat.Name = "btThoat";
            this.btThoat.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btThoat.Size = new System.Drawing.Size(76, 39);
            this.btThoat.TabIndex = 11;
            this.btThoat.Text = "Đóng";
            this.btThoat.UseVisualStyleBackColor = true;
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // btLuu
            // 
            this.btLuu.AutoSize = true;
            this.btLuu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btLuu.ForeColor = System.Drawing.Color.White;
            this.btLuu.Location = new System.Drawing.Point(530, 145);
            this.btLuu.Margin = new System.Windows.Forms.Padding(4);
            this.btLuu.Name = "btLuu";
            this.btLuu.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btLuu.Size = new System.Drawing.Size(66, 39);
            this.btLuu.TabIndex = 12;
            this.btLuu.Text = "Lưu";
            this.btLuu.UseVisualStyleBackColor = false;
            this.btLuu.Click += new System.EventHandler(this.btLuu_Click);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Image = global::DieuHanhCongTruong.Properties.Resources.Delete;
            this.btnDeleteFile.Location = new System.Drawing.Point(185, 94);
            this.btnDeleteFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(40, 25);
            this.btnDeleteFile.TabIndex = 98;
            this.btnDeleteFile.UseVisualStyleBackColor = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // tbDoc_file
            // 
            this.tbDoc_file.AutoSize = true;
            this.tbDoc_file.Location = new System.Drawing.Point(244, 100);
            this.tbDoc_file.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tbDoc_file.Name = "tbDoc_file";
            this.tbDoc_file.Size = new System.Drawing.Size(20, 17);
            this.tbDoc_file.TabIndex = 97;
            this.tbDoc_file.TabStop = true;
            this.tbDoc_file.Text = "...";
            this.tbDoc_file.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tbDoc_file_LinkClicked);
            // 
            // btOpentbDoc_file
            // 
            this.btOpentbDoc_file.Image = global::DieuHanhCongTruong.Properties.Resources.tag_16;
            this.btOpentbDoc_file.Location = new System.Drawing.Point(126, 95);
            this.btOpentbDoc_file.Margin = new System.Windows.Forms.Padding(4);
            this.btOpentbDoc_file.Name = "btOpentbDoc_file";
            this.btOpentbDoc_file.Size = new System.Drawing.Size(40, 25);
            this.btOpentbDoc_file.TabIndex = 96;
            this.btOpentbDoc_file.UseVisualStyleBackColor = true;
            this.btOpentbDoc_file.Click += new System.EventHandler(this.btOpentbDoc_file_Click);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.ForeColor = System.Drawing.Color.Red;
            this.label88.Location = new System.Drawing.Point(87, 27);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(13, 17);
            this.label88.TabIndex = 478;
            this.label88.Text = "*";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ThemFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 217);
            this.Controls.Add(this.label88);
            this.Controls.Add(this.btnDeleteFile);
            this.Controls.Add(this.tbDoc_file);
            this.Controls.Add(this.btOpentbDoc_file);
            this.Controls.Add(this.btThoat);
            this.Controls.Add(this.btLuu);
            this.Controls.Add(this.cbFileType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label27);
            this.Name = "ThemFile";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThemFile";
            this.Load += new System.EventHandler(this.ThemFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFileType;
        private System.Windows.Forms.Button btThoat;
        private System.Windows.Forms.Button btLuu;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.LinkLabel tbDoc_file;
        private System.Windows.Forms.Button btOpentbDoc_file;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}