namespace VNRaPaBomMin
{
    partial class GroupLayerForm
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
            this.DGVData = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btApDung = new System.Windows.Forms.Button();
            this.cotLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVData)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVData
            // 
            this.DGVData.AllowUserToAddRows = false;
            this.DGVData.AllowUserToDeleteRows = false;
            this.DGVData.AllowUserToResizeRows = false;
            this.DGVData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DGVData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotLayer,
            this.cotVisible});
            this.DGVData.Location = new System.Drawing.Point(16, 15);
            this.DGVData.Margin = new System.Windows.Forms.Padding(4);
            this.DGVData.MultiSelect = false;
            this.DGVData.Name = "DGVData";
            this.DGVData.RowHeadersVisible = false;
            this.DGVData.RowHeadersWidth = 51;
            this.DGVData.Size = new System.Drawing.Size(632, 288);
            this.DGVData.TabIndex = 0;
            this.DGVData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVData_CellClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(343, 320);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnCancel.Size = new System.Drawing.Size(76, 39);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.AutoSize = true;
            this.btnOk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(582, 320);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnOk.Size = new System.Drawing.Size(66, 39);
            this.btnOk.TabIndex = 29;
            this.btnOk.Text = "Lưu";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btApDung
            // 
            this.btApDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApDung.AutoSize = true;
            this.btApDung.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btApDung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btApDung.ForeColor = System.Drawing.Color.White;
            this.btApDung.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btApDung.Location = new System.Drawing.Point(455, 318);
            this.btApDung.Margin = new System.Windows.Forms.Padding(4);
            this.btApDung.Name = "btApDung";
            this.btApDung.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btApDung.Size = new System.Drawing.Size(95, 39);
            this.btApDung.TabIndex = 31;
            this.btApDung.Text = "Áp dụng";
            this.btApDung.UseVisualStyleBackColor = false;
            this.btApDung.Click += new System.EventHandler(this.btApDung_Click);
            // 
            // cotLayer
            // 
            this.cotLayer.HeaderText = "Lớp";
            this.cotLayer.MinimumWidth = 6;
            this.cotLayer.Name = "cotLayer";
            this.cotLayer.ReadOnly = true;
            // 
            // cotVisible
            // 
            this.cotVisible.FillWeight = 20F;
            this.cotVisible.HeaderText = "Hiển thị";
            this.cotVisible.MinimumWidth = 6;
            this.cotVisible.Name = "cotVisible";
            // 
            // GroupLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 372);
            this.Controls.Add(this.btApDung);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.DGVData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupLayerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý lớp";
            this.Load += new System.EventHandler(this.GroupLayerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVData;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btApDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotLayer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cotVisible;
    }
}