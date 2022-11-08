
namespace DieuHanhCongTruong
{
    partial class FrmThemmoiRPBM14
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
            this.label10 = new System.Windows.Forms.Label();
            this.txt_deep = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_detail = new System.Windows.Forms.TextBox();
            this.time_dates_rpbmST = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_area_all_rpbm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ground_done = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_cdt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_Xa = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_Tinh = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_TenDA = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.time_datesST = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_Huyen = new System.Windows.Forms.ComboBox();
            this.comboBox_Xa1 = new System.Windows.Forms.ComboBox();
            this.comboBox_Tinh1 = new System.Windows.Forms.ComboBox();
            this.comboBox_Huyen1 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_deptid_rpbmST = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_symbol = new System.Windows.Forms.TextBox();
            this.txt_boss_idST = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt = new System.Windows.Forms.Label();
            this.txt_boss_id_other = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.tbCategory = new System.Windows.Forms.TextBox();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.tbDoc_file = new System.Windows.Forms.LinkLabel();
            this.btOpentbDoc_file = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(759, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 18);
            this.label10.TabIndex = 582;
            this.label10.Text = "Độ sâu RPBM (m)";
            // 
            // txt_deep
            // 
            this.txt_deep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_deep.Location = new System.Drawing.Point(908, 170);
            this.txt_deep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_deep.Name = "txt_deep";
            this.txt_deep.Size = new System.Drawing.Size(229, 24);
            this.txt_deep.TabIndex = 581;
            this.txt_deep.Text = "0";
            this.txt_deep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_deep.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.txt_deep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCheck_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(350, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 18);
            this.label5.TabIndex = 576;
            this.label5.Text = "Cụ thể";
            // 
            // txt_detail
            // 
            this.txt_detail.AcceptsReturn = true;
            this.txt_detail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_detail.Location = new System.Drawing.Point(466, 168);
            this.txt_detail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_detail.Name = "txt_detail";
            this.txt_detail.Size = new System.Drawing.Size(290, 24);
            this.txt_detail.TabIndex = 575;
            // 
            // time_dates_rpbmST
            // 
            this.time_dates_rpbmST.CustomFormat = "dd/MM/yyyy";
            this.time_dates_rpbmST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_dates_rpbmST.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.time_dates_rpbmST.Location = new System.Drawing.Point(119, 168);
            this.time_dates_rpbmST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.time_dates_rpbmST.Name = "time_dates_rpbmST";
            this.time_dates_rpbmST.Size = new System.Drawing.Size(225, 24);
            this.time_dates_rpbmST.TabIndex = 574;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 18);
            this.label4.TabIndex = 573;
            this.label4.Text = "Ngày bắt đầu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(757, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 572;
            this.label2.Text = "Diện tích RPBM (m2)";
            // 
            // txt_area_all_rpbm
            // 
            this.txt_area_all_rpbm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_area_all_rpbm.Location = new System.Drawing.Point(910, 122);
            this.txt_area_all_rpbm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_area_all_rpbm.Name = "txt_area_all_rpbm";
            this.txt_area_all_rpbm.Size = new System.Drawing.Size(229, 24);
            this.txt_area_all_rpbm.TabIndex = 571;
            this.txt_area_all_rpbm.Text = "0";
            this.txt_area_all_rpbm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_area_all_rpbm.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.txt_area_all_rpbm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCheck_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(350, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 18);
            this.label1.TabIndex = 570;
            this.label1.Text = "Mặt bằng KVTC";
            // 
            // txt_ground_done
            // 
            this.txt_ground_done.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ground_done.Location = new System.Drawing.Point(466, 122);
            this.txt_ground_done.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ground_done.Name = "txt_ground_done";
            this.txt_ground_done.Size = new System.Drawing.Size(290, 24);
            this.txt_ground_done.TabIndex = 569;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(5, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 18);
            this.label15.TabIndex = 568;
            this.label15.Text = "Chủ đầu tư";
            // 
            // txt_cdt
            // 
            this.txt_cdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cdt.Location = new System.Drawing.Point(119, 122);
            this.txt_cdt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_cdt.Name = "txt_cdt";
            this.txt_cdt.Size = new System.Drawing.Size(225, 24);
            this.txt_cdt.TabIndex = 567;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(760, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 565;
            this.label12.Text = "Hạng mục:";
            // 
            // comboBox_Xa
            // 
            this.comboBox_Xa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Xa.FormattingEnabled = true;
            this.comboBox_Xa.Location = new System.Drawing.Point(909, 30);
            this.comboBox_Xa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Xa.Name = "comboBox_Xa";
            this.comboBox_Xa.Size = new System.Drawing.Size(230, 26);
            this.comboBox_Xa.TabIndex = 564;
            this.comboBox_Xa.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Xa_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 18);
            this.label8.TabIndex = 559;
            this.label8.Text = "Tỉnh ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(761, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 18);
            this.label9.TabIndex = 563;
            this.label9.Text = "Xã";
            // 
            // comboBox_Tinh
            // 
            this.comboBox_Tinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Tinh.FormattingEnabled = true;
            this.comboBox_Tinh.Location = new System.Drawing.Point(118, 30);
            this.comboBox_Tinh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Tinh.Name = "comboBox_Tinh";
            this.comboBox_Tinh.Size = new System.Drawing.Size(226, 26);
            this.comboBox_Tinh.TabIndex = 560;
            this.comboBox_Tinh.SelectedValueChanged += new System.EventHandler(this.comboBox_Tinh_SelectedIndexChanged);
            this.comboBox_Tinh.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Tinh_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(349, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 18);
            this.label11.TabIndex = 561;
            this.label11.Text = "Huyện";
            // 
            // comboBox_TenDA
            // 
            this.comboBox_TenDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TenDA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_TenDA.FormattingEnabled = true;
            this.comboBox_TenDA.Location = new System.Drawing.Point(466, 78);
            this.comboBox_TenDA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_TenDA.Name = "comboBox_TenDA";
            this.comboBox_TenDA.Size = new System.Drawing.Size(290, 26);
            this.comboBox_TenDA.TabIndex = 558;
            this.comboBox_TenDA.SelectedIndexChanged += new System.EventHandler(this.comboBox_TenDA_SelectedIndexChanged);
            this.comboBox_TenDA.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_TenDA_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(351, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 18);
            this.label7.TabIndex = 557;
            this.label7.Text = "Dự án";
            // 
            // time_datesST
            // 
            this.time_datesST.CustomFormat = "dd/MM/yyyy";
            this.time_datesST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_datesST.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.time_datesST.Location = new System.Drawing.Point(119, 80);
            this.time_datesST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.time_datesST.Name = "time_datesST";
            this.time_datesST.Size = new System.Drawing.Size(225, 24);
            this.time_datesST.TabIndex = 556;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 18);
            this.label3.TabIndex = 555;
            this.label3.Text = "Ngày";
            // 
            // comboBox_Huyen
            // 
            this.comboBox_Huyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Huyen.FormattingEnabled = true;
            this.comboBox_Huyen.Location = new System.Drawing.Point(466, 30);
            this.comboBox_Huyen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Huyen.Name = "comboBox_Huyen";
            this.comboBox_Huyen.Size = new System.Drawing.Size(290, 26);
            this.comboBox_Huyen.TabIndex = 562;
            this.comboBox_Huyen.SelectedValueChanged += new System.EventHandler(this.comboBox_Huyen_SelectedIndexChanged);
            this.comboBox_Huyen.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Huyen_Validating);
            // 
            // comboBox_Xa1
            // 
            this.comboBox_Xa1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Xa1.FormattingEnabled = true;
            this.comboBox_Xa1.Location = new System.Drawing.Point(907, 258);
            this.comboBox_Xa1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Xa1.Name = "comboBox_Xa1";
            this.comboBox_Xa1.Size = new System.Drawing.Size(230, 26);
            this.comboBox_Xa1.TabIndex = 613;
            this.comboBox_Xa1.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Xa1_Validating);
            // 
            // comboBox_Tinh1
            // 
            this.comboBox_Tinh1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Tinh1.FormattingEnabled = true;
            this.comboBox_Tinh1.Location = new System.Drawing.Point(117, 258);
            this.comboBox_Tinh1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Tinh1.Name = "comboBox_Tinh1";
            this.comboBox_Tinh1.Size = new System.Drawing.Size(226, 26);
            this.comboBox_Tinh1.TabIndex = 609;
            this.comboBox_Tinh1.SelectedValueChanged += new System.EventHandler(this.comboBox_Tinh1_SelectedIndexChanged);
            this.comboBox_Tinh1.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Tinh1_Validating);
            // 
            // comboBox_Huyen1
            // 
            this.comboBox_Huyen1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Huyen1.FormattingEnabled = true;
            this.comboBox_Huyen1.Location = new System.Drawing.Point(466, 258);
            this.comboBox_Huyen1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Huyen1.Name = "comboBox_Huyen1";
            this.comboBox_Huyen1.Size = new System.Drawing.Size(290, 26);
            this.comboBox_Huyen1.TabIndex = 611;
            this.comboBox_Huyen1.SelectedValueChanged += new System.EventHandler(this.comboBox_Huyen1_SelectedIndexChanged);
            this.comboBox_Huyen1.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Huyen1_Validating);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(5, 261);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 18);
            this.label17.TabIndex = 614;
            this.label17.Text = "Địa điểm Tỉnh";
            // 
            // txt_deptid_rpbmST
            // 
            this.txt_deptid_rpbmST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txt_deptid_rpbmST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_deptid_rpbmST.FormattingEnabled = true;
            this.txt_deptid_rpbmST.Location = new System.Drawing.Point(119, 215);
            this.txt_deptid_rpbmST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_deptid_rpbmST.Name = "txt_deptid_rpbmST";
            this.txt_deptid_rpbmST.Size = new System.Drawing.Size(226, 26);
            this.txt_deptid_rpbmST.TabIndex = 616;
            this.txt_deptid_rpbmST.Validating += new System.ComponentModel.CancelEventHandler(this.txt_deptid_rpbmST_Validating);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(4, 218);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(98, 18);
            this.label24.TabIndex = 615;
            this.label24.Text = "Đơn vị RPBM";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(353, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.TabIndex = 618;
            this.label6.Text = "Ký hiệu số ";
            // 
            // txt_symbol
            // 
            this.txt_symbol.AcceptsReturn = true;
            this.txt_symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_symbol.Location = new System.Drawing.Point(466, 215);
            this.txt_symbol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_symbol.Name = "txt_symbol";
            this.txt_symbol.Size = new System.Drawing.Size(290, 24);
            this.txt_symbol.TabIndex = 617;
            this.txt_symbol.Text = "0";
            this.txt_symbol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_symbol.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.txt_symbol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCheck_KeyPress);
            // 
            // txt_boss_idST
            // 
            this.txt_boss_idST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_boss_idST.FormattingEnabled = true;
            this.txt_boss_idST.Location = new System.Drawing.Point(117, 302);
            this.txt_boss_idST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_boss_idST.Name = "txt_boss_idST";
            this.txt_boss_idST.Size = new System.Drawing.Size(226, 26);
            this.txt_boss_idST.TabIndex = 620;
            this.txt_boss_idST.SelectedIndexChanged += new System.EventHandler(this.txt_boss_idST_SelectedIndexChanged);
            this.txt_boss_idST.SelectedValueChanged += new System.EventHandler(this.txt_boss_idST_SelectedValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 305);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 18);
            this.label13.TabIndex = 619;
            this.label13.Text = "ĐD ĐV RPBM";
            // 
            // txt
            // 
            this.txt.AutoSize = true;
            this.txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt.Location = new System.Drawing.Point(351, 307);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(90, 18);
            this.txt.TabIndex = 622;
            this.txt.Text = "ĐD ĐV khác";
            // 
            // txt_boss_id_other
            // 
            this.txt_boss_id_other.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_boss_id_other.Location = new System.Drawing.Point(466, 304);
            this.txt_boss_id_other.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_boss_id_other.Name = "txt_boss_id_other";
            this.txt_boss_id_other.Size = new System.Drawing.Size(290, 24);
            this.txt_boss_id_other.TabIndex = 621;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(351, 262);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 18);
            this.label14.TabIndex = 623;
            this.label14.Text = "Huyện";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(763, 260);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(26, 18);
            this.label16.TabIndex = 624;
            this.label16.Text = "Xã";
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(1071, 384);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonSave.Size = new System.Drawing.Size(66, 40);
            this.buttonSave.TabIndex = 657;
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
            this.buttonClose.Location = new System.Drawing.Point(961, 384);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.buttonClose.Size = new System.Drawing.Size(78, 40);
            this.buttonClose.TabIndex = 656;
            this.buttonClose.Text = "Đóng";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(399, 81);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 17);
            this.label18.TabIndex = 659;
            this.label18.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(100, 215);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 17);
            this.label19.TabIndex = 660;
            this.label19.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(97, 168);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(13, 17);
            this.label23.TabIndex = 664;
            this.label23.Text = "*";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(49, 81);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(13, 17);
            this.label25.TabIndex = 665;
            this.label25.Text = "*";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(3, 354);
            this.label65.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(95, 18);
            this.label65.TabIndex = 734;
            this.label65.Text = "File đính kèm";
            // 
            // tbCategory
            // 
            this.tbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCategory.Location = new System.Drawing.Point(910, 80);
            this.tbCategory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.ReadOnly = true;
            this.tbCategory.Size = new System.Drawing.Size(232, 24);
            this.tbCategory.TabIndex = 736;
            this.tbCategory.Text = "Rà phá";
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Image = global::DieuHanhCongTruong.Properties.Resources.Delete;
            this.btnDeleteFile.Location = new System.Drawing.Point(165, 347);
            this.btnDeleteFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(40, 25);
            this.btnDeleteFile.TabIndex = 767;
            this.btnDeleteFile.UseVisualStyleBackColor = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // tbDoc_file
            // 
            this.tbDoc_file.AutoSize = true;
            this.tbDoc_file.Location = new System.Drawing.Point(213, 351);
            this.tbDoc_file.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tbDoc_file.Name = "tbDoc_file";
            this.tbDoc_file.Size = new System.Drawing.Size(20, 17);
            this.tbDoc_file.TabIndex = 766;
            this.tbDoc_file.TabStop = true;
            this.tbDoc_file.Text = "...";
            this.tbDoc_file.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tbDoc_file_LinkClicked);
            // 
            // btOpentbDoc_file
            // 
            this.btOpentbDoc_file.Image = global::DieuHanhCongTruong.Properties.Resources.tag_16;
            this.btOpentbDoc_file.Location = new System.Drawing.Point(117, 347);
            this.btOpentbDoc_file.Margin = new System.Windows.Forms.Padding(4);
            this.btOpentbDoc_file.Name = "btOpentbDoc_file";
            this.btOpentbDoc_file.Size = new System.Drawing.Size(40, 25);
            this.btOpentbDoc_file.TabIndex = 765;
            this.btOpentbDoc_file.UseVisualStyleBackColor = true;
            this.btOpentbDoc_file.Click += new System.EventHandler(this.btOpentbDoc_file_Click);
            // 
            // FrmThemmoiRPBM14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 439);
            this.Controls.Add(this.btnDeleteFile);
            this.Controls.Add(this.tbDoc_file);
            this.Controls.Add(this.btOpentbDoc_file);
            this.Controls.Add(this.tbCategory);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.txt_boss_id_other);
            this.Controls.Add(this.txt_boss_idST);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_symbol);
            this.Controls.Add(this.txt_deptid_rpbmST);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.comboBox_Xa1);
            this.Controls.Add(this.comboBox_Tinh1);
            this.Controls.Add(this.comboBox_Huyen1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_deep);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_detail);
            this.Controls.Add(this.time_dates_rpbmST);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_area_all_rpbm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_ground_done);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_cdt);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBox_Xa);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox_Tinh);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboBox_TenDA);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.time_datesST);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_Huyen);
            this.Name = "FrmThemmoiRPBM14";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmThemmoiRPBM14";
            this.Load += new System.EventHandler(this.FrmThemmoiRPBM14_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_deep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_detail;
        private System.Windows.Forms.DateTimePicker time_dates_rpbmST;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_area_all_rpbm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ground_done;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_cdt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox_Xa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_Tinh;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_TenDA;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker time_datesST;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_Huyen;
        private System.Windows.Forms.ComboBox comboBox_Xa1;
        private System.Windows.Forms.ComboBox comboBox_Tinh1;
        private System.Windows.Forms.ComboBox comboBox_Huyen1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox txt_deptid_rpbmST;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_symbol;
        private System.Windows.Forms.ComboBox txt_boss_idST;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label txt;
        private System.Windows.Forms.TextBox txt_boss_id_other;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox tbCategory;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.LinkLabel tbDoc_file;
        private System.Windows.Forms.Button btOpentbDoc_file;
    }
}