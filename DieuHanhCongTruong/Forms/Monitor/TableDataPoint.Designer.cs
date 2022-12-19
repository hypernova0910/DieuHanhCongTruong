
namespace DieuHanhCongTruong.Forms.Monitor
{
    partial class TableDataPoint
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessage)).BeginInit();
            this.SuspendLayout();
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
            this.dgvMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMessage.EnableHeadersVisualStyles = false;
            this.dgvMessage.Location = new System.Drawing.Point(0, 0);
            this.dgvMessage.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMessage.Name = "dgvMessage";
            this.dgvMessage.ReadOnly = true;
            this.dgvMessage.RowHeadersVisible = false;
            this.dgvMessage.RowHeadersWidth = 51;
            this.dgvMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMessage.RowTemplate.Height = 24;
            this.dgvMessage.RowTemplate.ReadOnly = true;
            this.dgvMessage.Size = new System.Drawing.Size(994, 672);
            this.dgvMessage.TabIndex = 3;
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
            this.time.FillWeight = 150F;
            this.time.HeaderText = "Thời gian gửi";
            this.time.MinimumWidth = 6;
            this.time.Name = "time";
            this.time.ReadOnly = true;
            // 
            // magnetic
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.magnetic.DefaultCellStyle = dataGridViewCellStyle2;
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
            // TableDataPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvMessage);
            this.Name = "TableDataPoint";
            this.Size = new System.Drawing.Size(994, 672);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView dgvMessage;
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
    }
}
