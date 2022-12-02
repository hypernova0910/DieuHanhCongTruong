namespace Charts
{
    partial class TuTruongForm
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
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.SBControlDraw = new System.Windows.Forms.HScrollBar();
            this.labelGraph = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // SBControlDraw
            // 
            this.SBControlDraw.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SBControlDraw.Location = new System.Drawing.Point(0, 735);
            this.SBControlDraw.Name = "SBControlDraw";
            this.SBControlDraw.Size = new System.Drawing.Size(1312, 14);
            this.SBControlDraw.TabIndex = 0;
            this.SBControlDraw.ValueChanged += new System.EventHandler(this.SelectDraw);
            // 
            // labelGraph
            // 
            this.labelGraph.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGraph.Location = new System.Drawing.Point(0, 0);
            this.labelGraph.Name = "labelGraph";
            this.labelGraph.Size = new System.Drawing.Size(1312, 29);
            this.labelGraph.TabIndex = 1;
            this.labelGraph.Text = "Biểu đồ từ trường";
            this.labelGraph.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TuTruongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 749);
            this.Controls.Add(this.labelGraph);
            this.Controls.Add(this.SBControlDraw);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TuTruongForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biểu đồ từ trường";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.HScrollBar SBControlDraw;
        private System.Windows.Forms.Label labelGraph;

        

    }
}

