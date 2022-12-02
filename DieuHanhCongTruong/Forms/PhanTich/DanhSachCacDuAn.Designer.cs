namespace VNRaPaBomMin
{
    partial class DanhSachCacDuAn
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
            this.DGVThongTin = new System.Windows.Forms.DataGridView();
            this.cotMACid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTin)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGVThongTin);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1117, 364);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách các máy dò";
            // 
            // DGVThongTin
            // 
            this.DGVThongTin.AllowUserToAddRows = false;
            this.DGVThongTin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVThongTin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVThongTin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotMACid,
            this.cotCode});
            this.DGVThongTin.Location = new System.Drawing.Point(8, 23);
            this.DGVThongTin.Margin = new System.Windows.Forms.Padding(4);
            this.DGVThongTin.Name = "DGVThongTin";
            this.DGVThongTin.RowHeadersVisible = false;
            this.DGVThongTin.RowHeadersWidth = 51;
            this.DGVThongTin.Size = new System.Drawing.Size(1101, 334);
            this.DGVThongTin.TabIndex = 0;
            this.DGVThongTin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVThongTin_CellClick);
            // 
            // cotMACid
            // 
            this.cotMACid.FillWeight = 70F;
            this.cotMACid.HeaderText = "MAC id";
            this.cotMACid.MinimumWidth = 6;
            this.cotMACid.Name = "cotMACid";
            // 
            // cotCode
            // 
            this.cotCode.FillWeight = 30F;
            this.cotCode.HeaderText = "Mã máy";
            this.cotCode.MinimumWidth = 6;
            this.cotCode.Name = "cotCode";
            // 
            // DanhSachCacDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 394);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DanhSachCacDuAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách các máy dò";
            this.Load += new System.EventHandler(this.DanhSachCacDuAn_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGVThongTin;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotMACid;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotCode;
    }
}