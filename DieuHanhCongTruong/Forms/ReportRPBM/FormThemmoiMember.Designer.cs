
namespace DieuHanhCongTruong
{
    partial class FormThemmoiMember
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
            this.components = new System.ComponentModel.Container();
            this.txtString1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtString2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_string1ST = new System.Windows.Forms.ComboBox();
            this.txt_string2ST = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtString1
            // 
            this.txtString1.Location = new System.Drawing.Point(152, 87);
            this.txtString1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtString1.Name = "txtString1";
            this.txtString1.Size = new System.Drawing.Size(310, 22);
            this.txtString1.TabIndex = 39;
            this.txtString1.Validating += new System.ComponentModel.CancelEventHandler(this.txt_string1ST_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 38;
            this.label1.Text = "Họ và tên: ";
            // 
            // txtString2
            // 
            this.txtString2.Location = new System.Drawing.Point(152, 127);
            this.txtString2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtString2.Name = "txtString2";
            this.txtString2.Size = new System.Drawing.Size(310, 22);
            this.txtString2.TabIndex = 41;
            this.txtString2.Validating += new System.ComponentModel.CancelEventHandler(this.txtString2_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 40;
            this.label2.Text = "Chức vụ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 266;
            this.label3.Text = "Chức vụ khác:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 17);
            this.label4.TabIndex = 264;
            this.label4.Text = "Họ và tên khác: ";
            // 
            // txt_string1ST
            // 
            this.txt_string1ST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_string1ST.FormattingEnabled = true;
            this.txt_string1ST.Location = new System.Drawing.Point(152, 12);
            this.txt_string1ST.Name = "txt_string1ST";
            this.txt_string1ST.Size = new System.Drawing.Size(310, 26);
            this.txt_string1ST.TabIndex = 268;
            this.txt_string1ST.SelectedIndexChanged += new System.EventHandler(this.txt_string1ST_SelectedIndexChanged);
            this.txt_string1ST.SelectedValueChanged += new System.EventHandler(this.txt_string1ST_SelectedValueChanged);
            this.txt_string1ST.Validating += new System.ComponentModel.CancelEventHandler(this.txt_string1ST_Validating);
            // 
            // txt_string2ST
            // 
            this.txt_string2ST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_string2ST.FormattingEnabled = true;
            this.txt_string2ST.Location = new System.Drawing.Point(152, 49);
            this.txt_string2ST.Name = "txt_string2ST";
            this.txt_string2ST.Size = new System.Drawing.Size(310, 26);
            this.txt_string2ST.TabIndex = 269;
            this.txt_string2ST.SelectedIndexChanged += new System.EventHandler(this.txt_string2ST_SelectedIndexChanged);
            this.txt_string2ST.SelectedValueChanged += new System.EventHandler(this.txt_string2ST_SelectedValueChanged);
            this.txt_string2ST.Validating += new System.ComponentModel.CancelEventHandler(this.txt_string2ST_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 270;
            this.label5.Text = "Người kí ";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(152, 166);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(101, 22);
            this.checkBox1.TabIndex = 271;
            this.checkBox1.Text = "Là người kí";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(396, 206);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonSave.Size = new System.Drawing.Size(66, 40);
            this.buttonSave.TabIndex = 659;
            this.buttonSave.Text = "Lưu";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClose.CausesValidation = false;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(286, 206);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(78, 40);
            this.buttonClose.TabIndex = 658;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormThemmoiMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 273);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_string2ST);
            this.Controls.Add(this.txt_string1ST);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtString2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtString1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormThemmoiMember";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm mới Đơn vị khảo sát";
            this.Load += new System.EventHandler(this.FormThemmoiMember_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtString1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtString2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox txt_string1ST;
        private System.Windows.Forms.ComboBox txt_string2ST;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}