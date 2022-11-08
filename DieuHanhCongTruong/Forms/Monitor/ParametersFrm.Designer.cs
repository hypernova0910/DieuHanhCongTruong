
namespace VNRaPaBomMin
{
    partial class ParametersFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudDilution = new System.Windows.Forms.NumericUpDown();
            this.nudMinTime = new System.Windows.Forms.NumericUpDown();
            this.nudMinPoint = new System.Windows.Forms.NumericUpDown();
            this.btThoat = new System.Windows.Forms.Button();
            this.btLuu = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudDilution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPoint)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sai số GPS (m)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 36);
            this.label2.TabIndex = 1;
            this.label2.Text = "TG quy định \r\nđiểm liền kề (s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số điểm tối thiểu";
            // 
            // nudDilution
            // 
            this.nudDilution.DecimalPlaces = 3;
            this.nudDilution.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDilution.Location = new System.Drawing.Point(237, 29);
            this.nudDilution.Name = "nudDilution";
            this.nudDilution.Size = new System.Drawing.Size(235, 24);
            this.nudDilution.TabIndex = 3;
            this.nudDilution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudMinTime
            // 
            this.nudMinTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMinTime.Location = new System.Drawing.Point(237, 87);
            this.nudMinTime.Name = "nudMinTime";
            this.nudMinTime.Size = new System.Drawing.Size(235, 24);
            this.nudMinTime.TabIndex = 4;
            this.nudMinTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudMinPoint
            // 
            this.nudMinPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMinPoint.Location = new System.Drawing.Point(237, 153);
            this.nudMinPoint.Name = "nudMinPoint";
            this.nudMinPoint.Size = new System.Drawing.Size(235, 24);
            this.nudMinPoint.TabIndex = 5;
            this.nudMinPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btThoat
            // 
            this.btThoat.AutoSize = true;
            this.btThoat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThoat.Location = new System.Drawing.Point(290, 433);
            this.btThoat.Margin = new System.Windows.Forms.Padding(4);
            this.btThoat.Name = "btThoat";
            this.btThoat.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btThoat.Size = new System.Drawing.Size(78, 40);
            this.btThoat.TabIndex = 30;
            this.btThoat.Text = "Đóng";
            this.btThoat.UseVisualStyleBackColor = true;
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // btLuu
            // 
            this.btLuu.AutoSize = true;
            this.btLuu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLuu.ForeColor = System.Drawing.Color.White;
            this.btLuu.Location = new System.Drawing.Point(406, 433);
            this.btLuu.Margin = new System.Windows.Forms.Padding(4);
            this.btLuu.Name = "btLuu";
            this.btLuu.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btLuu.Size = new System.Drawing.Size(66, 40);
            this.btLuu.TabIndex = 31;
            this.btLuu.Text = "Lưu";
            this.btLuu.UseVisualStyleBackColor = false;
            this.btLuu.Click += new System.EventHandler(this.btLuu_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.AutoSize = true;
            this.btnDefault.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault.ForeColor = System.Drawing.Color.White;
            this.btnDefault.Location = new System.Drawing.Point(370, 218);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnDefault.Size = new System.Drawing.Size(102, 40);
            this.btnDefault.TabIndex = 32;
            this.btnDefault.Text = "Mặc định";
            this.btnDefault.UseVisualStyleBackColor = false;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // ParametersFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 510);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btThoat);
            this.Controls.Add(this.btLuu);
            this.Controls.Add(this.nudMinPoint);
            this.Controls.Add(this.nudMinTime);
            this.Controls.Add(this.nudDilution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ParametersFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THAM SỐ NẮN ĐIỂM";
            this.Load += new System.EventHandler(this.Parameters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDilution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPoint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudDilution;
        private System.Windows.Forms.NumericUpDown nudMinTime;
        private System.Windows.Forms.NumericUpDown nudMinPoint;
        private System.Windows.Forms.Button btThoat;
        private System.Windows.Forms.Button btLuu;
        private System.Windows.Forms.Button btnDefault;
    }
}