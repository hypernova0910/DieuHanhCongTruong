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
            this.DGVThongTin = new System.Windows.Forms.DataGridView();
            this.numDaiMau = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            this.cbVungDuAn = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cotMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cotColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaiMau)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVThongTin
            // 
            this.DGVThongTin.AllowUserToAddRows = false;
            this.DGVThongTin.AllowUserToResizeRows = false;
            this.DGVThongTin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVThongTin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVThongTin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotMin,
            this.cotMax,
            this.cotColor});
            this.DGVThongTin.Location = new System.Drawing.Point(8, 114);
            this.DGVThongTin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGVThongTin.Name = "DGVThongTin";
            this.DGVThongTin.RowHeadersVisible = false;
            this.DGVThongTin.RowHeadersWidth = 51;
            this.DGVThongTin.Size = new System.Drawing.Size(643, 389);
            this.DGVThongTin.TabIndex = 1;
            this.DGVThongTin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVThongTin_CellClick);
            // 
            // numDaiMau
            // 
            this.numDaiMau.Location = new System.Drawing.Point(92, 54);
            this.numDaiMau.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numDaiMau.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numDaiMau.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDaiMau.Name = "numDaiMau";
            this.numDaiMau.Size = new System.Drawing.Size(65, 22);
            this.numDaiMau.TabIndex = 0;
            this.numDaiMau.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDaiMau.ValueChanged += new System.EventHandler(this.numDaiMau_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Số dải màu";
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
            // cbVungDuAn
            // 
            this.cbVungDuAn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVungDuAn.FormattingEnabled = true;
            this.cbVungDuAn.Location = new System.Drawing.Point(92, 15);
            this.cbVungDuAn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbVungDuAn.Name = "cbVungDuAn";
            this.cbVungDuAn.Size = new System.Drawing.Size(558, 24);
            this.cbVungDuAn.TabIndex = 9;
            this.cbVungDuAn.SelectedIndexChanged += new System.EventHandler(this.cbVungDuAn_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Vùng dự án";
            // 
            // cotMin
            // 
            this.cotMin.HeaderText = "Min";
            this.cotMin.MinimumWidth = 6;
            this.cotMin.Name = "cotMin";
            // 
            // cotMax
            // 
            this.cotMax.HeaderText = "Max";
            this.cotMax.MinimumWidth = 6;
            this.cotMax.Name = "cotMax";
            // 
            // cotColor
            // 
            this.cotColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cotColor.HeaderText = "Màu";
            this.cotColor.MinimumWidth = 6;
            this.cotColor.Name = "cotColor";
            this.cotColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cotColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(585, 525);
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
            this.buttonClose.Location = new System.Drawing.Point(477, 525);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(78, 40);
            this.buttonClose.TabIndex = 369;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.AutoSize = true;
            this.btnDefault.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault.ForeColor = System.Drawing.Color.White;
            this.btnDefault.Location = new System.Drawing.Point(548, 54);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnDefault.Size = new System.Drawing.Size(102, 40);
            this.btnDefault.TabIndex = 371;
            this.btnDefault.Text = "Mặc định";
            this.btnDefault.UseVisualStyleBackColor = false;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // PhanTichDaiMauTuTruong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 594);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.cbVungDuAn);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numDaiMau);
            this.Controls.Add(this.DGVThongTin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PhanTichDaiMauTuTruong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân tích dải màu từ trường";
            this.Load += new System.EventHandler(this.PhanTichDaiMauTuTruong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVThongTin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaiMau)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVThongTin;
        private System.Windows.Forms.NumericUpDown numDaiMau;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.ComboBox cbVungDuAn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotMax;
        private System.Windows.Forms.DataGridViewButtonColumn cotColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button btnDefault;
    }
}