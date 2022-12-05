
namespace VNRaPaBomMin
{
    partial class DanhSachBMVN
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DanhSachBMVN));
            this.cbOLuoi = new System.Windows.Forms.ComboBox();
            this.cbKhuVuc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDA = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btThem = new System.Windows.Forms.Button();
            this.dgvBMVN = new System.Windows.Forms.DataGridView();
            this.buttonClose = new System.Windows.Forms.Button();
            this.axMap1 = new AxMapWinGIS.AxMap();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBMVNCount = new System.Windows.Forms.Label();
            this.lblDACount = new System.Windows.Forms.Label();
            this.lblKVCount = new System.Windows.Forms.Label();
            this.lblOLCount = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cotSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotIDHidden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotDuAn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotKhuVuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotOLuoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotViTri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotXoa = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBMVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbOLuoi
            // 
            this.cbOLuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOLuoi.FormattingEnabled = true;
            this.cbOLuoi.Location = new System.Drawing.Point(825, 20);
            this.cbOLuoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbOLuoi.Name = "cbOLuoi";
            this.cbOLuoi.Size = new System.Drawing.Size(217, 24);
            this.cbOLuoi.TabIndex = 13;
            this.cbOLuoi.SelectedValueChanged += new System.EventHandler(this.cbOLuoi_SelectedValueChanged);
            // 
            // cbKhuVuc
            // 
            this.cbKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhuVuc.FormattingEnabled = true;
            this.cbKhuVuc.Location = new System.Drawing.Point(523, 23);
            this.cbKhuVuc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbKhuVuc.Name = "cbKhuVuc";
            this.cbKhuVuc.Size = new System.Drawing.Size(187, 24);
            this.cbKhuVuc.TabIndex = 12;
            this.cbKhuVuc.SelectedValueChanged += new System.EventHandler(this.cbKhuVuc_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(751, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Ô lưới";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Vùng dự án";
            // 
            // cbDA
            // 
            this.cbDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDA.FormattingEnabled = true;
            this.cbDA.Location = new System.Drawing.Point(147, 20);
            this.cbDA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDA.Name = "cbDA";
            this.cbDA.Size = new System.Drawing.Size(187, 24);
            this.cbDA.TabIndex = 15;
            this.cbDA.SelectedValueChanged += new System.EventHandler(this.cbDA_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Dự án";
            // 
            // dtpTimeEnd
            // 
            this.dtpTimeEnd.Checked = false;
            this.dtpTimeEnd.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtpTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeEnd.Location = new System.Drawing.Point(523, 69);
            this.dtpTimeEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpTimeEnd.Name = "dtpTimeEnd";
            this.dtpTimeEnd.ShowCheckBox = true;
            this.dtpTimeEnd.Size = new System.Drawing.Size(187, 22);
            this.dtpTimeEnd.TabIndex = 19;
            this.dtpTimeEnd.ValueChanged += new System.EventHandler(this.dtpTimeEnd_ValueChanged);
            // 
            // dtpTimeStart
            // 
            this.dtpTimeStart.Checked = false;
            this.dtpTimeStart.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtpTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeStart.Location = new System.Drawing.Point(147, 69);
            this.dtpTimeStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpTimeStart.Name = "dtpTimeStart";
            this.dtpTimeStart.ShowCheckBox = true;
            this.dtpTimeStart.Size = new System.Drawing.Size(187, 22);
            this.dtpTimeStart.TabIndex = 18;
            this.dtpTimeStart.ValueChanged += new System.EventHandler(this.dtpTimeStart_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(371, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Thời gian kết thúc";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Thời gian bắt đầu";
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(825, 55);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnSearch.Size = new System.Drawing.Size(103, 41);
            this.btnSearch.TabIndex = 68;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btThem
            // 
            this.btThem.AutoSize = true;
            this.btThem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btThem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btThem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btThem.Location = new System.Drawing.Point(939, 55);
            this.btThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btThem.Name = "btThem";
            this.btThem.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btThem.Size = new System.Drawing.Size(108, 41);
            this.btThem.TabIndex = 263;
            this.btThem.Text = "Thêm mới";
            this.btThem.UseVisualStyleBackColor = false;
            this.btThem.Click += new System.EventHandler(this.btThem_Click);
            // 
            // dgvBMVN
            // 
            this.dgvBMVN.AllowUserToAddRows = false;
            this.dgvBMVN.AllowUserToDeleteRows = false;
            this.dgvBMVN.AllowUserToResizeColumns = false;
            this.dgvBMVN.AllowUserToResizeRows = false;
            this.dgvBMVN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBMVN.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBMVN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBMVN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotSTT,
            this.cotIDHidden,
            this.cotDuAn,
            this.cotKhuVuc,
            this.cotOLuoi,
            this.cotViTri,
            this.cotThoiGian,
            this.cotXoa});
            this.dgvBMVN.EnableHeadersVisualStyles = false;
            this.dgvBMVN.Location = new System.Drawing.Point(19, 128);
            this.dgvBMVN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvBMVN.MultiSelect = false;
            this.dgvBMVN.Name = "dgvBMVN";
            this.dgvBMVN.ReadOnly = true;
            this.dgvBMVN.RowHeadersVisible = false;
            this.dgvBMVN.RowHeadersWidth = 51;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            this.dgvBMVN.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvBMVN.RowTemplate.Height = 24;
            this.dgvBMVN.Size = new System.Drawing.Size(1025, 632);
            this.dgvBMVN.TabIndex = 264;
            this.dgvBMVN.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBMVN_CellClick);
            this.dgvBMVN.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvBMVN_RowStateChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(1979, 894);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(81, 41);
            this.buttonClose.TabIndex = 265;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // axMap1
            // 
            this.axMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMap1.Enabled = true;
            this.axMap1.Location = new System.Drawing.Point(0, 0);
            this.axMap1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.axMap1.Name = "axMap1";
            this.axMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMap1.OcxState")));
            this.axMap1.Size = new System.Drawing.Size(655, 657);
            this.axMap1.TabIndex = 266;
            this.axMap1.MouseDownEvent += new AxMapWinGIS._DMapEvents_MouseDownEventHandler(this.axMap1_MouseDownEvent);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1076, 704);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 267;
            this.label6.Text = "Số BMVN:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1076, 740);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 268;
            this.label7.Text = "Số dự án:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1463, 740);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 20);
            this.label8.TabIndex = 270;
            this.label8.Text = "Số ô 50x50:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1463, 704);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 20);
            this.label9.TabIndex = 269;
            this.label9.Text = "Số khu vực:";
            // 
            // lblBMVNCount
            // 
            this.lblBMVNCount.AutoSize = true;
            this.lblBMVNCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBMVNCount.Location = new System.Drawing.Point(1183, 704);
            this.lblBMVNCount.Name = "lblBMVNCount";
            this.lblBMVNCount.Size = new System.Drawing.Size(18, 20);
            this.lblBMVNCount.TabIndex = 271;
            this.lblBMVNCount.Text = "0";
            // 
            // lblDACount
            // 
            this.lblDACount.AutoSize = true;
            this.lblDACount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDACount.Location = new System.Drawing.Point(1183, 740);
            this.lblDACount.Name = "lblDACount";
            this.lblDACount.Size = new System.Drawing.Size(18, 20);
            this.lblDACount.TabIndex = 272;
            this.lblDACount.Text = "0";
            // 
            // lblKVCount
            // 
            this.lblKVCount.AutoSize = true;
            this.lblKVCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKVCount.Location = new System.Drawing.Point(1578, 704);
            this.lblKVCount.Name = "lblKVCount";
            this.lblKVCount.Size = new System.Drawing.Size(18, 20);
            this.lblKVCount.TabIndex = 273;
            this.lblKVCount.Text = "0";
            // 
            // lblOLCount
            // 
            this.lblOLCount.AutoSize = true;
            this.lblOLCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOLCount.Location = new System.Drawing.Point(1578, 740);
            this.lblOLCount.Name = "lblOLCount";
            this.lblOLCount.Size = new System.Drawing.Size(18, 20);
            this.lblOLCount.TabIndex = 274;
            this.lblOLCount.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axMap1);
            this.panel1.Location = new System.Drawing.Point(1080, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 657);
            this.panel1.TabIndex = 275;
            // 
            // cotSTT
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
            this.cotSTT.DefaultCellStyle = dataGridViewCellStyle2;
            this.cotSTT.FillWeight = 20F;
            this.cotSTT.HeaderText = "STT";
            this.cotSTT.MinimumWidth = 6;
            this.cotSTT.Name = "cotSTT";
            this.cotSTT.ReadOnly = true;
            // 
            // cotIDHidden
            // 
            this.cotIDHidden.HeaderText = "ID";
            this.cotIDHidden.MinimumWidth = 6;
            this.cotIDHidden.Name = "cotIDHidden";
            this.cotIDHidden.ReadOnly = true;
            this.cotIDHidden.Visible = false;
            // 
            // cotDuAn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
            this.cotDuAn.DefaultCellStyle = dataGridViewCellStyle3;
            this.cotDuAn.HeaderText = "Dự án";
            this.cotDuAn.MinimumWidth = 6;
            this.cotDuAn.Name = "cotDuAn";
            this.cotDuAn.ReadOnly = true;
            // 
            // cotKhuVuc
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            this.cotKhuVuc.DefaultCellStyle = dataGridViewCellStyle4;
            this.cotKhuVuc.HeaderText = "Khu vực";
            this.cotKhuVuc.MinimumWidth = 6;
            this.cotKhuVuc.Name = "cotKhuVuc";
            this.cotKhuVuc.ReadOnly = true;
            // 
            // cotOLuoi
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(3);
            this.cotOLuoi.DefaultCellStyle = dataGridViewCellStyle5;
            this.cotOLuoi.FillWeight = 50F;
            this.cotOLuoi.HeaderText = "Ô 50x50";
            this.cotOLuoi.MinimumWidth = 6;
            this.cotOLuoi.Name = "cotOLuoi";
            this.cotOLuoi.ReadOnly = true;
            // 
            // cotViTri
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(3);
            this.cotViTri.DefaultCellStyle = dataGridViewCellStyle6;
            this.cotViTri.HeaderText = "Vị trí";
            this.cotViTri.MinimumWidth = 6;
            this.cotViTri.Name = "cotViTri";
            this.cotViTri.ReadOnly = true;
            // 
            // cotThoiGian
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(3);
            this.cotThoiGian.DefaultCellStyle = dataGridViewCellStyle7;
            this.cotThoiGian.HeaderText = "Thời gian";
            this.cotThoiGian.MinimumWidth = 6;
            this.cotThoiGian.Name = "cotThoiGian";
            this.cotThoiGian.ReadOnly = true;
            // 
            // cotXoa
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle8.NullValue")));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(3);
            this.cotXoa.DefaultCellStyle = dataGridViewCellStyle8;
            this.cotXoa.FillWeight = 30F;
            this.cotXoa.HeaderText = "Xóa";
            this.cotXoa.Image = global::DieuHanhCongTruong.Properties.Resources.DeleteRed;
            this.cotXoa.MinimumWidth = 6;
            this.cotXoa.Name = "cotXoa";
            this.cotXoa.ReadOnly = true;
            // 
            // DanhSachBMVN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1765, 800);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblOLCount);
            this.Controls.Add(this.lblKVCount);
            this.Controls.Add(this.lblDACount);
            this.Controls.Add(this.lblBMVNCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.dgvBMVN);
            this.Controls.Add(this.btThem);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpTimeEnd);
            this.Controls.Add(this.dtpTimeStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbOLuoi);
            this.Controls.Add(this.cbKhuVuc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DanhSachBMVN";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DANH SÁCH BMVN";
            this.Load += new System.EventHandler(this.DanhSachBMVN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBMVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbOLuoi;
        private System.Windows.Forms.ComboBox cbKhuVuc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpTimeStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btThem;
        private System.Windows.Forms.DataGridView dgvBMVN;
        private System.Windows.Forms.Button buttonClose;
        private AxMapWinGIS.AxMap axMap1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBMVNCount;
        private System.Windows.Forms.Label lblDACount;
        private System.Windows.Forms.Label lblKVCount;
        private System.Windows.Forms.Label lblOLCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotIDHidden;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotDuAn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotKhuVuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotOLuoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotViTri;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotThoiGian;
        private System.Windows.Forms.DataGridViewImageColumn cotXoa;
    }
}