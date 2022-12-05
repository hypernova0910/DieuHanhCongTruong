namespace VNRaPaBomMin
{
    partial class PhanTichDaiMauTuTruong
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
            this.lbError = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tabControlDaiMau = new System.Windows.Forms.TabControl();
            this.tabBomb = new System.Windows.Forms.TabPage();
            this.btnDefault_Bomb = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numDaiMau_Bomb = new System.Windows.Forms.NumericUpDown();
            this.DGVThongTinBomb = new System.Windows.Forms.DataGridView();
            this.cotMin_Bomb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotMax_Bomb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotColor_Bomb = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabMine = new System.Windows.Forms.TabPage();
            this.btnDefault_Mine = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numDaiMau_Mine = new System.Windows.Forms.NumericUpDown();
            this.DGVThongTinMine = new System.Windows.Forms.DataGridView();
            this.cotMin_Mine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotMax_Mine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotColor_Mine = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabControlDaiMau.SuspendLayout();
            this.tabBomb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDaiMau_Bomb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTinBomb)).BeginInit();
            this.tabMine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDaiMau_Mine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTinMine)).BeginInit();
            this.SuspendLayout();
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.ForeColor = System.Drawing.Color.DarkRed;
            this.lbError.Location = new System.Drawing.Point(9, 511);
            this.lbError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(20, 17);
            this.lbError.TabIndex = 8;
            this.lbError.Text = "...";
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(585, 614);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonSave.Size = new System.Drawing.Size(66, 40);
            this.buttonSave.TabIndex = 370;
            this.buttonSave.Text = "Lưu";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.btOk_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(477, 614);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(78, 40);
            this.buttonClose.TabIndex = 369;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tabControlDaiMau
            // 
            this.tabControlDaiMau.Controls.Add(this.tabBomb);
            this.tabControlDaiMau.Controls.Add(this.tabMine);
            this.tabControlDaiMau.Location = new System.Drawing.Point(13, 12);
            this.tabControlDaiMau.Name = "tabControlDaiMau";
            this.tabControlDaiMau.SelectedIndex = 0;
            this.tabControlDaiMau.Size = new System.Drawing.Size(638, 581);
            this.tabControlDaiMau.TabIndex = 372;
            // 
            // tabBomb
            // 
            this.tabBomb.Controls.Add(this.btnDefault_Bomb);
            this.tabBomb.Controls.Add(this.label1);
            this.tabBomb.Controls.Add(this.numDaiMau_Bomb);
            this.tabBomb.Controls.Add(this.DGVThongTinBomb);
            this.tabBomb.Location = new System.Drawing.Point(4, 25);
            this.tabBomb.Name = "tabBomb";
            this.tabBomb.Padding = new System.Windows.Forms.Padding(3);
            this.tabBomb.Size = new System.Drawing.Size(630, 552);
            this.tabBomb.TabIndex = 0;
            this.tabBomb.Text = "Dải màu từ trường bom";
            this.tabBomb.UseVisualStyleBackColor = true;
            // 
            // btnDefault_Bomb
            // 
            this.btnDefault_Bomb.AutoSize = true;
            this.btnDefault_Bomb.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDefault_Bomb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDefault_Bomb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault_Bomb.ForeColor = System.Drawing.Color.White;
            this.btnDefault_Bomb.Location = new System.Drawing.Point(511, 20);
            this.btnDefault_Bomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDefault_Bomb.Name = "btnDefault_Bomb";
            this.btnDefault_Bomb.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnDefault_Bomb.Size = new System.Drawing.Size(102, 40);
            this.btnDefault_Bomb.TabIndex = 374;
            this.btnDefault_Bomb.Text = "Mặc định";
            this.btnDefault_Bomb.UseVisualStyleBackColor = false;
            this.btnDefault_Bomb.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 373;
            this.label1.Text = "Số dải màu";
            // 
            // numDaiMau_Bomb
            // 
            this.numDaiMau_Bomb.Location = new System.Drawing.Point(109, 20);
            this.numDaiMau_Bomb.Margin = new System.Windows.Forms.Padding(4);
            this.numDaiMau_Bomb.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numDaiMau_Bomb.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDaiMau_Bomb.Name = "numDaiMau_Bomb";
            this.numDaiMau_Bomb.Size = new System.Drawing.Size(65, 22);
            this.numDaiMau_Bomb.TabIndex = 372;
            this.numDaiMau_Bomb.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDaiMau_Bomb.ValueChanged += new System.EventHandler(this.numDaiMau_Bomb_ValueChanged);
            // 
            // DGVThongTinBomb
            // 
            this.DGVThongTinBomb.AllowUserToAddRows = false;
            this.DGVThongTinBomb.AllowUserToResizeRows = false;
            this.DGVThongTinBomb.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVThongTinBomb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVThongTinBomb.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotMin_Bomb,
            this.cotMax_Bomb,
            this.cotColor_Bomb});
            this.DGVThongTinBomb.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DGVThongTinBomb.Location = new System.Drawing.Point(3, 82);
            this.DGVThongTinBomb.Margin = new System.Windows.Forms.Padding(4);
            this.DGVThongTinBomb.Name = "DGVThongTinBomb";
            this.DGVThongTinBomb.RowHeadersVisible = false;
            this.DGVThongTinBomb.RowHeadersWidth = 51;
            this.DGVThongTinBomb.Size = new System.Drawing.Size(624, 467);
            this.DGVThongTinBomb.TabIndex = 2;
            this.DGVThongTinBomb.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVThongTin_CellClick);
            // 
            // cotMin_Bomb
            // 
            this.cotMin_Bomb.HeaderText = "Min";
            this.cotMin_Bomb.MinimumWidth = 6;
            this.cotMin_Bomb.Name = "cotMin_Bomb";
            // 
            // cotMax_Bomb
            // 
            this.cotMax_Bomb.HeaderText = "Max";
            this.cotMax_Bomb.MinimumWidth = 6;
            this.cotMax_Bomb.Name = "cotMax_Bomb";
            // 
            // cotColor_Bomb
            // 
            this.cotColor_Bomb.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cotColor_Bomb.HeaderText = "Màu";
            this.cotColor_Bomb.MinimumWidth = 6;
            this.cotColor_Bomb.Name = "cotColor_Bomb";
            this.cotColor_Bomb.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cotColor_Bomb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tabMine
            // 
            this.tabMine.Controls.Add(this.btnDefault_Mine);
            this.tabMine.Controls.Add(this.label2);
            this.tabMine.Controls.Add(this.numDaiMau_Mine);
            this.tabMine.Controls.Add(this.DGVThongTinMine);
            this.tabMine.Location = new System.Drawing.Point(4, 25);
            this.tabMine.Name = "tabMine";
            this.tabMine.Padding = new System.Windows.Forms.Padding(3);
            this.tabMine.Size = new System.Drawing.Size(630, 552);
            this.tabMine.TabIndex = 1;
            this.tabMine.Text = "Dải màu từ trường mìn";
            this.tabMine.UseVisualStyleBackColor = true;
            // 
            // btnDefault_Mine
            // 
            this.btnDefault_Mine.AutoSize = true;
            this.btnDefault_Mine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDefault_Mine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDefault_Mine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault_Mine.ForeColor = System.Drawing.Color.White;
            this.btnDefault_Mine.Location = new System.Drawing.Point(508, 21);
            this.btnDefault_Mine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDefault_Mine.Name = "btnDefault_Mine";
            this.btnDefault_Mine.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnDefault_Mine.Size = new System.Drawing.Size(102, 40);
            this.btnDefault_Mine.TabIndex = 377;
            this.btnDefault_Mine.Text = "Mặc định";
            this.btnDefault_Mine.UseVisualStyleBackColor = false;
            this.btnDefault_Mine.Click += new System.EventHandler(this.btnDefault_Mine_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 376;
            this.label2.Text = "Số dải màu";
            // 
            // numDaiMau_Mine
            // 
            this.numDaiMau_Mine.Location = new System.Drawing.Point(106, 21);
            this.numDaiMau_Mine.Margin = new System.Windows.Forms.Padding(4);
            this.numDaiMau_Mine.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numDaiMau_Mine.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDaiMau_Mine.Name = "numDaiMau_Mine";
            this.numDaiMau_Mine.Size = new System.Drawing.Size(65, 22);
            this.numDaiMau_Mine.TabIndex = 375;
            this.numDaiMau_Mine.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDaiMau_Mine.ValueChanged += new System.EventHandler(this.numDaiMau_Mine_ValueChanged);
            // 
            // DGVThongTinMine
            // 
            this.DGVThongTinMine.AllowUserToAddRows = false;
            this.DGVThongTinMine.AllowUserToResizeRows = false;
            this.DGVThongTinMine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVThongTinMine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVThongTinMine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotMin_Mine,
            this.cotMax_Mine,
            this.cotColor_Mine});
            this.DGVThongTinMine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DGVThongTinMine.Location = new System.Drawing.Point(3, 81);
            this.DGVThongTinMine.Margin = new System.Windows.Forms.Padding(4);
            this.DGVThongTinMine.Name = "DGVThongTinMine";
            this.DGVThongTinMine.RowHeadersVisible = false;
            this.DGVThongTinMine.RowHeadersWidth = 51;
            this.DGVThongTinMine.Size = new System.Drawing.Size(624, 468);
            this.DGVThongTinMine.TabIndex = 3;
            this.DGVThongTinMine.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVThongTinMine_CellClick);
            // 
            // cotMin_Mine
            // 
            this.cotMin_Mine.HeaderText = "Min";
            this.cotMin_Mine.MinimumWidth = 6;
            this.cotMin_Mine.Name = "cotMin_Mine";
            // 
            // cotMax_Mine
            // 
            this.cotMax_Mine.HeaderText = "Max";
            this.cotMax_Mine.MinimumWidth = 6;
            this.cotMax_Mine.Name = "cotMax_Mine";
            // 
            // cotColor_Mine
            // 
            this.cotColor_Mine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cotColor_Mine.HeaderText = "Màu";
            this.cotColor_Mine.MinimumWidth = 6;
            this.cotColor_Mine.Name = "cotColor_Mine";
            this.cotColor_Mine.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cotColor_Mine.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PhanTichDaiMauTuTruong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 665);
            this.Controls.Add(this.tabControlDaiMau);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.lbError);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PhanTichDaiMauTuTruong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân tích dải màu từ trường";
            this.Load += new System.EventHandler(this.PhanTichDaiMauTuTruong_Load);
            this.tabControlDaiMau.ResumeLayout(false);
            this.tabBomb.ResumeLayout(false);
            this.tabBomb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDaiMau_Bomb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTinBomb)).EndInit();
            this.tabMine.ResumeLayout(false);
            this.tabMine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDaiMau_Mine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTinMine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TabControl tabControlDaiMau;
        private System.Windows.Forms.TabPage tabBomb;
        private System.Windows.Forms.DataGridView DGVThongTinBomb;
        private System.Windows.Forms.TabPage tabMine;
        private System.Windows.Forms.DataGridView DGVThongTinMine;
        private System.Windows.Forms.Button btnDefault_Bomb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numDaiMau_Bomb;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotMin_Bomb;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotMax_Bomb;
        private System.Windows.Forms.DataGridViewButtonColumn cotColor_Bomb;
        private System.Windows.Forms.Button btnDefault_Mine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numDaiMau_Mine;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotMin_Mine;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotMax_Mine;
        private System.Windows.Forms.DataGridViewButtonColumn cotColor_Mine;
    }
}