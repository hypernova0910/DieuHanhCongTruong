namespace VNRaPaBomMin
{
    partial class QuanLyDonViForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btThem = new System.Windows.Forms.Button();
            this.cotXoa = new System.Windows.Forms.DataGridViewImageColumn();
            this.cotSua = new System.Windows.Forms.DataGridViewImageColumn();
            this.cotHeadPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotFax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGVDonVi = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDonVi)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DGVDonVi);
            this.groupBox1.Location = new System.Drawing.Point(2, 86);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1526, 869);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Các đơn vị";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 65;
            this.label1.Text = "Nhập tên đơn vị";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(143, 18);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(263, 22);
            this.tbSearch.TabIndex = 66;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(435, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnSearch.Size = new System.Drawing.Size(98, 39);
            this.btnSearch.TabIndex = 67;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btThem
            // 
            this.btThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btThem.AutoSize = true;
            this.btThem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btThem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btThem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btThem.Location = new System.Drawing.Point(1420, 18);
            this.btThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btThem.Name = "btThem";
            this.btThem.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btThem.Size = new System.Drawing.Size(104, 39);
            this.btThem.TabIndex = 262;
            this.btThem.Text = "Thêm mới";
            this.btThem.UseVisualStyleBackColor = false;
            this.btThem.Click += new System.EventHandler(this.btThem_Click);
            // 
            // cotXoa
            // 
            this.cotXoa.FillWeight = 30F;
            this.cotXoa.HeaderText = "Xóa";
            this.cotXoa.Image = global::DieuHanhCongTruong.Properties.Resources.DeleteRed;
            this.cotXoa.MinimumWidth = 6;
            this.cotXoa.Name = "cotXoa";
            // 
            // cotSua
            // 
            this.cotSua.FillWeight = 30F;
            this.cotSua.HeaderText = "Sửa";
            this.cotSua.Image = global::DieuHanhCongTruong.Properties.Resources.Modify;
            this.cotSua.MinimumWidth = 6;
            this.cotSua.Name = "cotSua";
            // 
            // cotHeadPos
            // 
            this.cotHeadPos.HeaderText = "Chức vụ người đứng đầu";
            this.cotHeadPos.MinimumWidth = 6;
            this.cotHeadPos.Name = "cotHeadPos";
            // 
            // cotHead
            // 
            this.cotHead.HeaderText = "Người đứng đầu";
            this.cotHead.MinimumWidth = 6;
            this.cotHead.Name = "cotHead";
            // 
            // cotEmail
            // 
            this.cotEmail.HeaderText = "Email";
            this.cotEmail.MinimumWidth = 6;
            this.cotEmail.Name = "cotEmail";
            // 
            // cotFax
            // 
            this.cotFax.HeaderText = "Fax";
            this.cotFax.MinimumWidth = 6;
            this.cotFax.Name = "cotFax";
            // 
            // cotPhone
            // 
            this.cotPhone.HeaderText = "Điện thoại";
            this.cotPhone.MinimumWidth = 6;
            this.cotPhone.Name = "cotPhone";
            // 
            // cotAddress
            // 
            this.cotAddress.HeaderText = "Địa chỉ";
            this.cotAddress.MinimumWidth = 6;
            this.cotAddress.Name = "cotAddress";
            // 
            // cotName
            // 
            this.cotName.FillWeight = 150F;
            this.cotName.HeaderText = "Tên cơ quan, đơn vị, tổ chức";
            this.cotName.MinimumWidth = 6;
            this.cotName.Name = "cotName";
            // 
            // cotID
            // 
            this.cotID.FillWeight = 20F;
            this.cotID.HeaderText = "ID";
            this.cotID.MinimumWidth = 6;
            this.cotID.Name = "cotID";
            this.cotID.Visible = false;
            // 
            // DGVDonVi
            // 
            this.DGVDonVi.AllowUserToAddRows = false;
            this.DGVDonVi.AllowUserToDeleteRows = false;
            this.DGVDonVi.AllowUserToResizeColumns = false;
            this.DGVDonVi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVDonVi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVDonVi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVDonVi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotID,
            this.cotName,
            this.cotAddress,
            this.cotPhone,
            this.cotFax,
            this.cotEmail,
            this.cotHead,
            this.cotHeadPos,
            this.cotSua,
            this.cotXoa});
            this.DGVDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVDonVi.EnableHeadersVisualStyles = false;
            this.DGVDonVi.Location = new System.Drawing.Point(4, 19);
            this.DGVDonVi.Margin = new System.Windows.Forms.Padding(4);
            this.DGVDonVi.Name = "DGVDonVi";
            this.DGVDonVi.RowHeadersVisible = false;
            this.DGVDonVi.RowHeadersWidth = 51;
            this.DGVDonVi.Size = new System.Drawing.Size(1518, 846);
            this.DGVDonVi.TabIndex = 0;
            this.DGVDonVi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDonVi_CellClick);
            // 
            // QuanLyDonViForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 986);
            this.Controls.Add(this.btThem);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuanLyDonViForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ ĐƠN VỊ";
            this.Load += new System.EventHandler(this.QuanLyDonViForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVDonVi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btThem;
        private System.Windows.Forms.DataGridView DGVDonVi;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotHeadPos;
        private System.Windows.Forms.DataGridViewImageColumn cotSua;
        private System.Windows.Forms.DataGridViewImageColumn cotXoa;
    }
}