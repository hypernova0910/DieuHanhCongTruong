using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.CustomControl
{
    public partial class MachineControl : Panel
    {
        private Label lblMachineCode;
        private Label lblMagnetic;
        private Label lblLatLong;
        private Label lblStaff;
        private PictureBox pboxIconMachine;

        public static int BOM = 1;
        public static int MIN = 2;

        public MachineControl()
        {
            InitializeComponent();
            lblMachineCode = new Label();
            lblMagnetic = new Label();
            lblLatLong = new Label();
            lblStaff = new Label();
            pboxIconMachine = new PictureBox();
            //
            // machineControl
            //
            this.Controls.Add(this.lblMachineCode);
            this.Controls.Add(this.lblMagnetic);
            this.Controls.Add(this.lblLatLong);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.pboxIconMachine);
            //this.Location = new System.Drawing.Point(3, 3);
            this.Size = new System.Drawing.Size(572, 68);
            //this.TabIndex = 0;
            //
            // lblMachineCode
            //
            this.lblMachineCode.AutoSize = true;
            this.lblMachineCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineCode.Location = new System.Drawing.Point(88, 11);
            this.lblMachineCode.Name = "lblMachineCode";
            this.lblMachineCode.Size = new System.Drawing.Size(145, 20);
            this.lblMachineCode.TabIndex = 1;
            this.lblMachineCode.Text = "B1 - 12464929a34";
            this.lblMachineCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMagnetic
            // 
            this.lblMagnetic.AutoSize = true;
            this.lblMagnetic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMagnetic.Location = new System.Drawing.Point(535, 36);
            this.lblMagnetic.Name = "lblMagnetic";
            this.lblMagnetic.Size = new System.Drawing.Size(31, 20);
            this.lblMagnetic.TabIndex = 3;
            this.lblMagnetic.Text = "7.5";
            this.lblMagnetic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLatLong
            // 
            this.lblLatLong.AutoSize = true;
            this.lblLatLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatLong.Location = new System.Drawing.Point(396, 11);
            this.lblLatLong.Name = "lblLatLong";
            this.lblLatLong.Size = new System.Drawing.Size(170, 20);
            this.lblLatLong.TabIndex = 2;
            this.lblLatLong.Text = "17.086545, 20.075544";
            this.lblLatLong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStaff
            // 
            this.lblStaff.AutoSize = true;
            this.lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(88, 36);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(134, 20);
            this.lblStaff.TabIndex = 2;
            this.lblStaff.Text = "Nguyễn Tuấn Hà";
            this.lblStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pboxIconMachine.Dock = System.Windows.Forms.DockStyle.Left;
            this.pboxIconMachine.Image = global::DieuHanhCongTruong.Properties.Resources.bom;
            this.pboxIconMachine.Location = new System.Drawing.Point(0, 0);
            this.pboxIconMachine.Name = "pictureBox1";
            this.pboxIconMachine.Size = new System.Drawing.Size(73, 68);
            this.pboxIconMachine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxIconMachine.TabIndex = 0;
            this.pboxIconMachine.TabStop = false;
        }

        public MachineControl(int type, string codeMachine, string staff)
        {
            InitializeComponent();
            lblMachineCode = new Label();
            lblMagnetic = new Label();
            lblLatLong = new Label();
            lblStaff = new Label();
            pboxIconMachine = new PictureBox();
            //
            // machineControl
            //
            this.Controls.Add(this.lblMachineCode);
            this.Controls.Add(this.lblMagnetic);
            this.Controls.Add(this.lblLatLong);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.pboxIconMachine);
            this.Size = new System.Drawing.Size(572, 68);
            //this.TabIndex = 0;
            //
            // lblMachineCode
            //
            this.lblMachineCode.AutoSize = true;
            this.lblMachineCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineCode.Location = new System.Drawing.Point(88, 11);
            this.lblMachineCode.Size = new System.Drawing.Size(145, 20);
            this.lblMachineCode.TabIndex = 1;
            this.lblMachineCode.Text = codeMachine;
            this.lblMachineCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMachineCode.Click += ChildControlClick;
            // 
            // lblMagnetic
            // 
            this.lblMagnetic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMagnetic.Location = new System.Drawing.Point(320, 36);
            this.lblMagnetic.Size = new System.Drawing.Size(200, 20);
            this.lblMagnetic.TabIndex = 3;
            this.lblMagnetic.Text = "0";
            this.lblMagnetic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMagnetic.Click += ChildControlClick;
            // 
            // lblLatLong
            // 
            this.lblLatLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatLong.Location = new System.Drawing.Point(324, 11);
            this.lblLatLong.Name = "lblLatLong";
            this.lblLatLong.Size = new System.Drawing.Size(196, 20);
            this.lblLatLong.TabIndex = 2;
            this.lblLatLong.Text = "17.086545, 20.075544";
            this.lblLatLong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLatLong.Click += ChildControlClick;
            // 
            // lblStaff
            // 
            this.lblStaff.AutoSize = true;
            this.lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(88, 36);
            this.lblStaff.Size = new System.Drawing.Size(134, 20);
            this.lblStaff.TabIndex = 2;
            this.lblStaff.Text = staff;
            this.lblStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStaff.Click += ChildControlClick;
            // 
            // pboxIconMachine
            // 
            this.pboxIconMachine.Dock = System.Windows.Forms.DockStyle.Left;
            if(type == BOM)
            {
                this.pboxIconMachine.Image = global::DieuHanhCongTruong.Properties.Resources.bom_original;
            }
            else if(type == MIN)
            {
                this.pboxIconMachine.Image = global::DieuHanhCongTruong.Properties.Resources.min_original;
            }
            this.pboxIconMachine.Location = new System.Drawing.Point(0, 0);
            this.pboxIconMachine.Size = new System.Drawing.Size(73, 68);
            this.pboxIconMachine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxIconMachine.TabIndex = 0;
            this.pboxIconMachine.TabStop = false;
            this.pboxIconMachine.Click += ChildControlClick;
        }

        public MachineControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void ChildControlClick(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        public void setGPS(double latt, double longt)
        {
            lblLatLong.Text = Math.Round(latt, 6).ToString() + ", " + Math.Round(longt, 6).ToString();
        }

        public void setForeColor(Color color)
        {
            lblMachineCode.ForeColor = color;
            lblMagnetic.ForeColor = color;
            lblLatLong.ForeColor = color;
            lblStaff.ForeColor = color;
        }

        public void setActive(bool isActive)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    if (isActive)
                    {
                        lblMachineCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblMagnetic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblLatLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblMachineCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblMagnetic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblLatLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                }));
            }
            else
            {
                if (isActive)
                {
                    lblMachineCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblMagnetic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblLatLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblMachineCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblMagnetic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblLatLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        public void setMagnetic(double magnetic)
        {
            lblMagnetic.Text = magnetic.ToString();
        }

        public Label getLblMachineCode()
        {
            return lblMachineCode;
        }

        public Label getLblStaff()
        {
            return lblStaff;
        }
    }
}
