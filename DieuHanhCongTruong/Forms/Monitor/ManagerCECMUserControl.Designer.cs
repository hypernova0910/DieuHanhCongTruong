namespace VNRaPaBomMin
{
    partial class ManagerCECMUserControl
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
            this.components = new System.ComponentModel.Container();
            this.menuKeHoachTrienKhai = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quảnLýDựÁnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thêmMớiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteProgramDataItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TVDanhSach = new System.Windows.Forms.TreeView();
            this.menuKeHoachTrienKhai.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuKeHoachTrienKhai
            // 
            this.menuKeHoachTrienKhai.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuKeHoachTrienKhai.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýDựÁnToolStripMenuItem,
            this.thêmMớiToolStripMenuItem,
            this.xóaToolStripMenuItem,
            this.deleteProgramDataItem});
            this.menuKeHoachTrienKhai.Name = "menuKeHoachTrienKhai";
            this.menuKeHoachTrienKhai.Size = new System.Drawing.Size(197, 100);
            this.menuKeHoachTrienKhai.Opening += new System.ComponentModel.CancelEventHandler(this.menuKeHoachTrienKhai_Opening);
            // 
            // quảnLýDựÁnToolStripMenuItem
            // 
            this.quảnLýDựÁnToolStripMenuItem.Name = "quảnLýDựÁnToolStripMenuItem";
            this.quảnLýDựÁnToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.quảnLýDựÁnToolStripMenuItem.Text = "Điều hành dự án";
            this.quảnLýDựÁnToolStripMenuItem.Click += new System.EventHandler(this.quảnLýDựÁnToolStripMenuItem_Click);
            // 
            // thêmMớiToolStripMenuItem
            // 
            this.thêmMớiToolStripMenuItem.Name = "thêmMớiToolStripMenuItem";
            this.thêmMớiToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.thêmMớiToolStripMenuItem.Text = "Thêm mới";
            this.thêmMớiToolStripMenuItem.Click += new System.EventHandler(this.thêmMớiToolStripMenuItem_Click);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.xóaToolStripMenuItem.Text = "Xóa";
            this.xóaToolStripMenuItem.DropDownOpening += new System.EventHandler(this.xóaToolStripMenuItem_DropDownOpening);
            this.xóaToolStripMenuItem.Click += new System.EventHandler(this.xóaToolStripMenuItem_Click);
            // 
            // deleteProgramDataItem
            // 
            this.deleteProgramDataItem.Name = "deleteProgramDataItem";
            this.deleteProgramDataItem.Size = new System.Drawing.Size(210, 24);
            this.deleteProgramDataItem.Text = "Xóa dữ liệu dự án";
            this.deleteProgramDataItem.Click += new System.EventHandler(this.deleteProgramDataItem_Click);
            // 
            // TVDanhSach
            // 
            this.TVDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TVDanhSach.Location = new System.Drawing.Point(0, 0);
            this.TVDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.TVDanhSach.Name = "TVDanhSach";
            this.TVDanhSach.Size = new System.Drawing.Size(347, 832);
            this.TVDanhSach.TabIndex = 1;
            this.TVDanhSach.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TVDanhSach_BeforeCollapse);
            this.TVDanhSach.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TVDanhSach_BeforeExpand);
            this.TVDanhSach.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TVDanhSach_NodeMouseClick);
            // 
            // ManagerCECMUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TVDanhSach);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ManagerCECMUserControl";
            this.Size = new System.Drawing.Size(347, 832);
            this.Load += new System.EventHandler(this.ManagerCECMUserControl_Load);
            this.menuKeHoachTrienKhai.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip menuKeHoachTrienKhai;
        private System.Windows.Forms.ToolStripMenuItem thêmMớiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteProgramDataItem;
        public System.Windows.Forms.ToolStripMenuItem quảnLýDựÁnToolStripMenuItem;
        private System.Windows.Forms.TreeView TVDanhSach;
    }
}
