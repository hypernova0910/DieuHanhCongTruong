
namespace VNRaPaBomMin
{
    partial class MonitorFormNew
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorFormNew));
            this.metroTabControl2 = new System.Windows.Forms.TabControl();
            this.machineBombTab = new System.Windows.Forms.TabPage();
            this.chart_bomb = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.machineMineTab = new System.Windows.Forms.TabPage();
            this.chart_mine = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvMessage = new System.Windows.Forms.DataGridView();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.magnetic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GPS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeGPS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotSoVeTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotSaiSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metroTabControl1 = new System.Windows.Forms.TabControl();
            this.chartTab = new System.Windows.Forms.TabPage();
            this.tableTab = new System.Windows.Forms.TabPage();
            this.mapTab = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDistance = new System.Windows.Forms.ToolStripButton();
            this.btnPoint = new System.Windows.Forms.ToolStripButton();
            this.btnShowPoint = new System.Windows.Forms.ToolStripButton();
            this.btnModel = new System.Windows.Forms.ToolStripButton();
            this.axMap1 = new AxMapWinGIS.AxMap();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPara = new System.Windows.Forms.Button();
            this.cb50x50 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbKhuVuc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btConnect = new System.Windows.Forms.Button();
            this.cbTenDuAn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.pnlMachineBomb = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCapNhatDuLieu = new System.Windows.Forms.Button();
            this.pnlKQPTContainer = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.machineTab = new System.Windows.Forms.TabPage();
            this.resultTab = new System.Windows.Forms.TabPage();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnRealTime = new System.Windows.Forms.Button();
            this.btnModelHistory = new System.Windows.Forms.Button();
            this.btnModel_ = new System.Windows.Forms.Button();
            this.btnModelRealTime = new System.Windows.Forms.Button();
            this.metroTabControl2.SuspendLayout();
            this.machineBombTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_bomb)).BeginInit();
            this.machineMineTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_mine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessage)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.chartTab.SuspendLayout();
            this.tableTab.SuspendLayout();
            this.mapTab.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.machineTab.SuspendLayout();
            this.resultTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl2
            // 
            this.metroTabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTabControl2.Controls.Add(this.machineBombTab);
            this.metroTabControl2.Controls.Add(this.machineMineTab);
            this.metroTabControl2.Location = new System.Drawing.Point(12, 18);
            this.metroTabControl2.Margin = new System.Windows.Forms.Padding(2);
            this.metroTabControl2.Name = "metroTabControl2";
            this.metroTabControl2.SelectedIndex = 1;
            this.metroTabControl2.Size = new System.Drawing.Size(979, 656);
            this.metroTabControl2.TabIndex = 2;
            // 
            // machineBombTab
            // 
            this.machineBombTab.Controls.Add(this.chart_bomb);
            this.machineBombTab.Location = new System.Drawing.Point(4, 26);
            this.machineBombTab.Margin = new System.Windows.Forms.Padding(2);
            this.machineBombTab.Name = "machineBombTab";
            this.machineBombTab.Size = new System.Drawing.Size(971, 626);
            this.machineBombTab.TabIndex = 0;
            this.machineBombTab.Text = "Dò bom   ";
            // 
            // chart_bomb
            // 
            chartArea1.AxisX.LineWidth = 3;
            chartArea1.AxisX.Maximum = 60D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea1.AxisX2.Minimum = -100000D;
            chartArea1.AxisY.LineWidth = 3;
            chartArea1.Name = "ChartArea1";
            this.chart_bomb.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_bomb.Legends.Add(legend1);
            this.chart_bomb.Location = new System.Drawing.Point(8, 8);
            this.chart_bomb.Margin = new System.Windows.Forms.Padding(2);
            this.chart_bomb.Name = "chart_bomb";
            this.chart_bomb.Size = new System.Drawing.Size(952, 602);
            this.chart_bomb.TabIndex = 2;
            this.chart_bomb.Text = "Gía trị từ trường máy dò bom";
            // 
            // machineMineTab
            // 
            this.machineMineTab.Controls.Add(this.chart_mine);
            this.machineMineTab.Location = new System.Drawing.Point(4, 26);
            this.machineMineTab.Margin = new System.Windows.Forms.Padding(2);
            this.machineMineTab.Name = "machineMineTab";
            this.machineMineTab.Size = new System.Drawing.Size(971, 626);
            this.machineMineTab.TabIndex = 1;
            this.machineMineTab.Text = "Dò mìn   ";
            // 
            // chart_mine
            // 
            chartArea2.AxisX.LineWidth = 3;
            chartArea2.AxisX.Maximum = 60D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea2.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea2.AxisX2.Minimum = -100000D;
            chartArea2.AxisY.LineWidth = 3;
            chartArea2.Name = "ChartArea1";
            this.chart_mine.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_mine.Legends.Add(legend2);
            this.chart_mine.Location = new System.Drawing.Point(8, 8);
            this.chart_mine.Margin = new System.Windows.Forms.Padding(2);
            this.chart_mine.Name = "chart_mine";
            this.chart_mine.Size = new System.Drawing.Size(952, 602);
            this.chart_mine.TabIndex = 2;
            this.chart_mine.Text = "Gía trị từ trường máy dò bom";
            // 
            // dgvMessage
            // 
            this.dgvMessage.AllowUserToDeleteRows = false;
            this.dgvMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMessage.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMessage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.count,
            this.command,
            this.machineId,
            this.time,
            this.magnetic,
            this.GPS,
            this.corner,
            this.timeGPS,
            this.status,
            this.cotSoVeTinh,
            this.cotSaiSo});
            this.dgvMessage.EnableHeadersVisualStyles = false;
            this.dgvMessage.Location = new System.Drawing.Point(10, 20);
            this.dgvMessage.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMessage.Name = "dgvMessage";
            this.dgvMessage.ReadOnly = true;
            this.dgvMessage.RowHeadersVisible = false;
            this.dgvMessage.RowHeadersWidth = 51;
            this.dgvMessage.RowTemplate.Height = 24;
            this.dgvMessage.RowTemplate.ReadOnly = true;
            this.dgvMessage.Size = new System.Drawing.Size(980, 650);
            this.dgvMessage.TabIndex = 2;
            // 
            // count
            // 
            this.count.FillWeight = 35.70382F;
            this.count.HeaderText = "STT";
            this.count.MinimumWidth = 6;
            this.count.Name = "count";
            this.count.ReadOnly = true;
            // 
            // command
            // 
            this.command.FillWeight = 80F;
            this.command.HeaderText = "Loại máy dò";
            this.command.MinimumWidth = 20;
            this.command.Name = "command";
            this.command.ReadOnly = true;
            // 
            // machineId
            // 
            this.machineId.FillWeight = 114.8156F;
            this.machineId.HeaderText = "SH máy";
            this.machineId.MinimumWidth = 6;
            this.machineId.Name = "machineId";
            this.machineId.ReadOnly = true;
            // 
            // time
            // 
            this.time.FillWeight = 101.417F;
            this.time.HeaderText = "Thời gian gửi";
            this.time.MinimumWidth = 6;
            this.time.Name = "time";
            this.time.ReadOnly = true;
            // 
            // magnetic
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.magnetic.DefaultCellStyle = dataGridViewCellStyle2;
            this.magnetic.FillWeight = 170.5584F;
            this.magnetic.HeaderText = "Gía trị từ trường";
            this.magnetic.MinimumWidth = 6;
            this.magnetic.Name = "magnetic";
            this.magnetic.ReadOnly = true;
            // 
            // GPS
            // 
            this.GPS.FillWeight = 126.7711F;
            this.GPS.HeaderText = "Tọa độ";
            this.GPS.MinimumWidth = 6;
            this.GPS.Name = "GPS";
            this.GPS.ReadOnly = true;
            // 
            // corner
            // 
            this.corner.FillWeight = 42.25705F;
            this.corner.HeaderText = "Góc";
            this.corner.MinimumWidth = 6;
            this.corner.Name = "corner";
            this.corner.ReadOnly = true;
            // 
            // timeGPS
            // 
            this.timeGPS.FillWeight = 101.417F;
            this.timeGPS.HeaderText = "Thời gian";
            this.timeGPS.MinimumWidth = 6;
            this.timeGPS.Name = "timeGPS";
            this.timeGPS.ReadOnly = true;
            // 
            // status
            // 
            this.status.FillWeight = 73.59273F;
            this.status.HeaderText = "Trạng thái";
            this.status.MinimumWidth = 6;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // cotSoVeTinh
            // 
            this.cotSoVeTinh.FillWeight = 50F;
            this.cotSoVeTinh.HeaderText = "Số vệ tinh";
            this.cotSoVeTinh.MinimumWidth = 6;
            this.cotSoVeTinh.Name = "cotSoVeTinh";
            this.cotSoVeTinh.ReadOnly = true;
            // 
            // cotSaiSo
            // 
            this.cotSaiSo.FillWeight = 50F;
            this.cotSaiSo.HeaderText = "Sai số";
            this.cotSaiSo.MinimumWidth = 6;
            this.cotSaiSo.Name = "cotSaiSo";
            this.cotSaiSo.ReadOnly = true;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.chartTab);
            this.metroTabControl1.Controls.Add(this.tableTab);
            this.metroTabControl1.Controls.Add(this.mapTab);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(1010, 732);
            this.metroTabControl1.TabIndex = 1;
            // 
            // chartTab
            // 
            this.chartTab.Controls.Add(this.metroTabControl2);
            this.chartTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartTab.Location = new System.Drawing.Point(4, 26);
            this.chartTab.Margin = new System.Windows.Forms.Padding(2);
            this.chartTab.Name = "chartTab";
            this.chartTab.Size = new System.Drawing.Size(1002, 702);
            this.chartTab.TabIndex = 0;
            this.chartTab.Text = "Biểu đồ   ";
            // 
            // tableTab
            // 
            this.tableTab.Controls.Add(this.dgvMessage);
            this.tableTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableTab.Location = new System.Drawing.Point(4, 26);
            this.tableTab.Margin = new System.Windows.Forms.Padding(2);
            this.tableTab.Name = "tableTab";
            this.tableTab.Size = new System.Drawing.Size(1002, 702);
            this.tableTab.TabIndex = 2;
            this.tableTab.Text = "Bảng   ";
            // 
            // mapTab
            // 
            this.mapTab.Controls.Add(this.toolStrip1);
            this.mapTab.Controls.Add(this.axMap1);
            this.mapTab.Location = new System.Drawing.Point(4, 26);
            this.mapTab.Margin = new System.Windows.Forms.Padding(2);
            this.mapTab.Name = "mapTab";
            this.mapTab.Padding = new System.Windows.Forms.Padding(2);
            this.mapTab.Size = new System.Drawing.Size(1002, 702);
            this.mapTab.TabIndex = 3;
            this.mapTab.Text = "Bản đồ";
            this.mapTab.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDistance,
            this.btnPoint,
            this.btnShowPoint,
            this.btnModel});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(998, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDistance
            // 
            this.btnDistance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDistance.Image = global::DieuHanhCongTruong.Properties.Resources.distance;
            this.btnDistance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDistance.Name = "btnDistance";
            this.btnDistance.Size = new System.Drawing.Size(29, 24);
            this.btnDistance.Text = "Đo khoảng cách";
            this.btnDistance.Click += new System.EventHandler(this.btnDistance_Click);
            // 
            // btnPoint
            // 
            this.btnPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPoint.Image = global::DieuHanhCongTruong.Properties.Resources.marker;
            this.btnPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(29, 24);
            this.btnPoint.Text = "Lấy tọa độ điểm";
            this.btnPoint.Click += new System.EventHandler(this.btnPoint_Click);
            // 
            // btnShowPoint
            // 
            this.btnShowPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowPoint.Image = global::DieuHanhCongTruong.Properties.Resources.show;
            this.btnShowPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowPoint.Name = "btnShowPoint";
            this.btnShowPoint.Size = new System.Drawing.Size(29, 24);
            this.btnShowPoint.Tag = "";
            this.btnShowPoint.Text = "Hiện điểm dò được";
            this.btnShowPoint.Click += new System.EventHandler(this.btnShowPoint_Click);
            // 
            // btnModel
            // 
            this.btnModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnModel.Enabled = false;
            this.btnModel.Image = global::DieuHanhCongTruong.Properties.Resources.model_on;
            this.btnModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModel.Name = "btnModel";
            this.btnModel.Size = new System.Drawing.Size(29, 24);
            this.btnModel.Text = "Bật nắn điểm";
            this.btnModel.ToolTipText = "Bật nắn điểm";
            this.btnModel.Click += new System.EventHandler(this.btnModel_Click);
            // 
            // axMap1
            // 
            this.axMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMap1.Enabled = true;
            this.axMap1.Location = new System.Drawing.Point(2, 2);
            this.axMap1.Margin = new System.Windows.Forms.Padding(2);
            this.axMap1.Name = "axMap1";
            this.axMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMap1.OcxState")));
            this.axMap1.Size = new System.Drawing.Size(998, 698);
            this.axMap1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.Controls.Add(this.btnPara);
            this.panel2.Controls.Add(this.cb50x50);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbKhuVuc);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btConnect);
            this.panel2.Controls.Add(this.cbTenDuAn);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbIP);
            this.panel2.Location = new System.Drawing.Point(1016, 26);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 201);
            this.panel2.TabIndex = 2;
            // 
            // btnPara
            // 
            this.btnPara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPara.AutoSize = true;
            this.btnPara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnPara.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPara.ForeColor = System.Drawing.Color.White;
            this.btnPara.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPara.Location = new System.Drawing.Point(584, 125);
            this.btnPara.Margin = new System.Windows.Forms.Padding(4);
            this.btnPara.Name = "btnPara";
            this.btnPara.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.btnPara.Size = new System.Drawing.Size(119, 52);
            this.btnPara.TabIndex = 20;
            this.btnPara.Text = "Tham số";
            this.btnPara.UseVisualStyleBackColor = false;
            this.btnPara.Click += new System.EventHandler(this.btnPara_Click);
            // 
            // cb50x50
            // 
            this.cb50x50.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb50x50.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb50x50.FormattingEnabled = true;
            this.cb50x50.Location = new System.Drawing.Point(584, 86);
            this.cb50x50.Margin = new System.Windows.Forms.Padding(4);
            this.cb50x50.Name = "cb50x50";
            this.cb50x50.Size = new System.Drawing.Size(279, 28);
            this.cb50x50.TabIndex = 19;
            this.cb50x50.SelectedValueChanged += new System.EventHandler(this.cb50x50_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(490, 89);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Ô 50x50";
            // 
            // cbKhuVuc
            // 
            this.cbKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhuVuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKhuVuc.FormattingEnabled = true;
            this.cbKhuVuc.Location = new System.Drawing.Point(122, 86);
            this.cbKhuVuc.Margin = new System.Windows.Forms.Padding(4);
            this.cbKhuVuc.Name = "cbKhuVuc";
            this.cbKhuVuc.Size = new System.Drawing.Size(359, 28);
            this.cbKhuVuc.TabIndex = 17;
            this.cbKhuVuc.SelectedValueChanged += new System.EventHandler(this.cbKhuVuc_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Khu vực";
            // 
            // btConnect
            // 
            this.btConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btConnect.AutoSize = true;
            this.btConnect.BackColor = System.Drawing.Color.ForestGreen;
            this.btConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConnect.ForeColor = System.Drawing.Color.White;
            this.btConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btConnect.Location = new System.Drawing.Point(745, 125);
            this.btConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btConnect.Name = "btConnect";
            this.btConnect.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.btConnect.Size = new System.Drawing.Size(119, 52);
            this.btConnect.TabIndex = 15;
            this.btConnect.Text = "Kết nối";
            this.btConnect.UseVisualStyleBackColor = false;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // cbTenDuAn
            // 
            this.cbTenDuAn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTenDuAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTenDuAn.FormattingEnabled = true;
            this.cbTenDuAn.Location = new System.Drawing.Point(122, 26);
            this.cbTenDuAn.Margin = new System.Windows.Forms.Padding(4);
            this.cbTenDuAn.Name = "cbTenDuAn";
            this.cbTenDuAn.Size = new System.Drawing.Size(740, 28);
            this.cbTenDuAn.TabIndex = 14;
            this.cbTenDuAn.SelectedValueChanged += new System.EventHandler(this.cbTenDuAn_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Dự án";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Địa chỉ IP";
            // 
            // tbIP
            // 
            this.tbIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIP.Location = new System.Drawing.Point(122, 148);
            this.tbIP.Margin = new System.Windows.Forms.Padding(4);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(359, 26);
            this.tbIP.TabIndex = 11;
            this.tbIP.Text = "localhost";
            // 
            // pnlMachineBomb
            // 
            this.pnlMachineBomb.AutoScroll = true;
            this.pnlMachineBomb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMachineBomb.Location = new System.Drawing.Point(2, 2);
            this.pnlMachineBomb.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMachineBomb.Name = "pnlMachineBomb";
            this.pnlMachineBomb.Size = new System.Drawing.Size(876, 464);
            this.pnlMachineBomb.TabIndex = 12;
            // 
            // btnCapNhatDuLieu
            // 
            this.btnCapNhatDuLieu.Location = new System.Drawing.Point(1970, 50);
            this.btnCapNhatDuLieu.Margin = new System.Windows.Forms.Padding(4);
            this.btnCapNhatDuLieu.Name = "btnCapNhatDuLieu";
            this.btnCapNhatDuLieu.Size = new System.Drawing.Size(94, 29);
            this.btnCapNhatDuLieu.TabIndex = 15;
            this.btnCapNhatDuLieu.Text = "Cập nhật";
            this.btnCapNhatDuLieu.UseVisualStyleBackColor = true;
            this.btnCapNhatDuLieu.Click += new System.EventHandler(this.btnCapNhatDuLieu_Click);
            // 
            // pnlKQPTContainer
            // 
            this.pnlKQPTContainer.AutoScroll = true;
            this.pnlKQPTContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKQPTContainer.Location = new System.Drawing.Point(2, 2);
            this.pnlKQPTContainer.Margin = new System.Windows.Forms.Padding(2);
            this.pnlKQPTContainer.Name = "pnlKQPTContainer";
            this.pnlKQPTContainer.Size = new System.Drawing.Size(876, 377);
            this.pnlKQPTContainer.TabIndex = 17;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.machineTab);
            this.tabControl1.Controls.Add(this.resultTab);
            this.tabControl1.Location = new System.Drawing.Point(1016, 231);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 497);
            this.tabControl1.TabIndex = 18;
            // 
            // machineTab
            // 
            this.machineTab.Controls.Add(this.pnlMachineBomb);
            this.machineTab.Location = new System.Drawing.Point(4, 25);
            this.machineTab.Margin = new System.Windows.Forms.Padding(2);
            this.machineTab.Name = "machineTab";
            this.machineTab.Padding = new System.Windows.Forms.Padding(2);
            this.machineTab.Size = new System.Drawing.Size(880, 468);
            this.machineTab.TabIndex = 0;
            this.machineTab.Text = "Máy dò";
            this.machineTab.UseVisualStyleBackColor = true;
            // 
            // resultTab
            // 
            this.resultTab.Controls.Add(this.pnlKQPTContainer);
            this.resultTab.Location = new System.Drawing.Point(4, 25);
            this.resultTab.Margin = new System.Windows.Forms.Padding(2);
            this.resultTab.Name = "resultTab";
            this.resultTab.Padding = new System.Windows.Forms.Padding(2);
            this.resultTab.Size = new System.Drawing.Size(880, 381);
            this.resultTab.TabIndex = 1;
            this.resultTab.Text = "Kết quả phân tích";
            this.resultTab.UseVisualStyleBackColor = true;
            // 
            // btnHistory
            // 
            this.btnHistory.AutoSize = true;
            this.btnHistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHistory.Location = new System.Drawing.Point(1035, 260);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(123, 27);
            this.btnHistory.TabIndex = 19;
            this.btnHistory.Text = "Chưa nắn lịch sử";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnRealTime
            // 
            this.btnRealTime.AutoSize = true;
            this.btnRealTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRealTime.Location = new System.Drawing.Point(1182, 260);
            this.btnRealTime.Name = "btnRealTime";
            this.btnRealTime.Size = new System.Drawing.Size(137, 27);
            this.btnRealTime.TabIndex = 20;
            this.btnRealTime.Text = "Chưa nắn real time";
            this.btnRealTime.UseVisualStyleBackColor = true;
            this.btnRealTime.Click += new System.EventHandler(this.btnRealTime_Click);
            // 
            // btnModelHistory
            // 
            this.btnModelHistory.AutoSize = true;
            this.btnModelHistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnModelHistory.Location = new System.Drawing.Point(1334, 260);
            this.btnModelHistory.Name = "btnModelHistory";
            this.btnModelHistory.Size = new System.Drawing.Size(88, 27);
            this.btnModelHistory.TabIndex = 21;
            this.btnModelHistory.Text = "Nắn lịch sử";
            this.btnModelHistory.UseVisualStyleBackColor = true;
            this.btnModelHistory.Click += new System.EventHandler(this.btnModelHistory_Click);
            // 
            // btnModel_
            // 
            this.btnModel_.AutoSize = true;
            this.btnModel_.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnModel_.Location = new System.Drawing.Point(1438, 260);
            this.btnModel_.Name = "btnModel_";
            this.btnModel_.Size = new System.Drawing.Size(102, 27);
            this.btnModel_.TabIndex = 22;
            this.btnModel_.Text = "Nắn real time";
            this.btnModel_.UseVisualStyleBackColor = true;
            this.btnModel_.Click += new System.EventHandler(this.btnModel__Click);
            // 
            // btnModelRealTime
            // 
            this.btnModelRealTime.AutoSize = true;
            this.btnModelRealTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnModelRealTime.Location = new System.Drawing.Point(1558, 260);
            this.btnModelRealTime.Name = "btnModelRealTime";
            this.btnModelRealTime.Size = new System.Drawing.Size(225, 27);
            this.btnModelRealTime.TabIndex = 23;
            this.btnModelRealTime.Text = "Chưa nắn real time ở bản đồ nắn";
            this.btnModelRealTime.UseVisualStyleBackColor = true;
            this.btnModelRealTime.Click += new System.EventHandler(this.btnModelRealTime_Click);
            // 
            // MonitorFormNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1918, 732);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCapNhatDuLieu);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.btnModelRealTime);
            this.Controls.Add(this.btnModel_);
            this.Controls.Add(this.btnModelHistory);
            this.Controls.Add(this.btnRealTime);
            this.Controls.Add(this.btnHistory);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MonitorFormNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhận dữ liệu máy dò";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonitorForm2_FormClosing);
            this.Load += new System.EventHandler(this.MonitorForm2_Load);
            this.metroTabControl2.ResumeLayout(false);
            this.machineBombTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_bomb)).EndInit();
            this.machineMineTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_mine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessage)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.chartTab.ResumeLayout(false);
            this.tableTab.ResumeLayout(false);
            this.mapTab.ResumeLayout(false);
            this.mapTab.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.machineTab.ResumeLayout(false);
            this.resultTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl metroTabControl1;
        private System.Windows.Forms.TabPage chartTab;
        private System.Windows.Forms.TabPage tableTab;
        private System.Windows.Forms.TabControl metroTabControl2;
        private System.Windows.Forms.TabPage machineBombTab;
        private System.Windows.Forms.TabPage machineMineTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_bomb;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_mine;
        private System.Windows.Forms.DataGridView dgvMessage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.ComboBox cbTenDuAn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.FlowLayoutPanel pnlMachineBomb;
        private System.Windows.Forms.Button btnCapNhatDuLieu;
        private System.Windows.Forms.TabPage mapTab;
        private AxMapWinGIS.AxMap axMap1;
        private System.Windows.Forms.ComboBox cb50x50;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbKhuVuc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlKQPTContainer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage machineTab;
        private System.Windows.Forms.TabPage resultTab;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnDistance;
        private System.Windows.Forms.ToolStripButton btnPoint;
        private System.Windows.Forms.ToolStripButton btnShowPoint;
        private System.Windows.Forms.ToolStripButton btnModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn command;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn magnetic;
        private System.Windows.Forms.DataGridViewTextBoxColumn GPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn corner;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeGPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotSoVeTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotSaiSo;
        private System.Windows.Forms.Button btnPara;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnRealTime;
        private System.Windows.Forms.Button btnModelHistory;
        private System.Windows.Forms.Button btnModel_;
        private System.Windows.Forms.Button btnModelRealTime;
        //public GsPreviewCtrl mPreviewCtrl;
    }
}