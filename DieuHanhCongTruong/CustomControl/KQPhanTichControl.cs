using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;

namespace DieuHanhCongTruong.CustomControl
{
    public partial class KQPhanTichControl : Panel
    {
        private Panel pnlStatus;
        private Label lblMachineCode;
        private Label lblStaff;
        private Label lblLatLong;
        private PictureBox pboxIconAlert;
        private Label lblDepth;
        private Label lblIsBomb;
        private Button btnSave;

        private const string COBOM = "Có bom";
        private const string COMIN = "Có mìn";
        private const string COBOMMIN = "Có bom, mìn";

        public MapWinGIS.Labels label_info { get; set; }
        //public Vertex vertex { get; set; }
        public long program_id { get; set; }
        public long area_id { get; set; }
        public long o_id { get; set; }

        public static int CAMCO = 1;
        public static int BOM = 2;

        public KQPhanTichControl()
        {
            InitializeComponent();
            pnlStatus = new Panel();
            lblMachineCode = new Label();
            lblStaff = new Label();
            lblLatLong = new Label();
            pboxIconAlert = new PictureBox();
            lblDepth = new Label();
            lblIsBomb = new Label();
            // 
            // pnlKQPhanTich
            // 
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.lblMachineCode);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.lblLatLong);
            this.Controls.Add(this.pboxIconAlert);
            this.Location = new System.Drawing.Point(1022, 540);
            this.Size = new System.Drawing.Size(698, 68);
            this.Dock = DockStyle.Top;
            //this.TabIndex = 1;
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
            this.lblStaff.Click += (sender, e) =>
            {
                this.OnClick(e);
            };
            // 
            // pboxIconAlert
            // 
            this.pboxIconAlert.Dock = System.Windows.Forms.DockStyle.Left;
            this.pboxIconAlert.Image = global::DieuHanhCongTruong.Properties.Resources.camco;
            this.pboxIconAlert.Location = new System.Drawing.Point(0, 0);
            this.pboxIconAlert.Name = "pboxIconAlert";
            this.pboxIconAlert.Size = new System.Drawing.Size(73, 68);
            this.pboxIconAlert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxIconAlert.TabIndex = 0;
            this.pboxIconAlert.TabStop = false;
            this.pboxIconAlert.Margin = new Padding(10);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblDepth);
            this.pnlStatus.Controls.Add(this.lblIsBomb);
            this.pnlStatus.Location = new System.Drawing.Point(357, 34);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(166, 22);
            this.pnlStatus.TabIndex = 0;
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepth.Location = new System.Drawing.Point(67, 0);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(32, 20);
            this.lblDepth.TabIndex = 4;
            this.lblDepth.Text = "2m";
            this.lblDepth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIsBomb
            // 
            this.lblIsBomb.AutoSize = true;
            this.lblIsBomb.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblIsBomb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsBomb.ForeColor = System.Drawing.Color.Red;
            this.lblIsBomb.Location = new System.Drawing.Point(99, 0);
            this.lblIsBomb.Name = "lblIsBomb";
            this.lblIsBomb.Size = new System.Drawing.Size(67, 20);
            this.lblIsBomb.TabIndex = 4;
            this.lblIsBomb.Text = COBOM;
            this.lblIsBomb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //foreach (Control control in Controls)
            //{
            //    control.Click += (sender, e) =>
            //    {
            //        this.OnClick(new EventArgs());
            //    };
            //}
        }

        public KQPhanTichControl(int type, string codeMachine, string staff, double latt, double longt, double magnetic, double depth = double.NaN, bool existBomb = false, bool existMine = false)
        {
            InitializeComponent();
            pnlStatus = new Panel();
            lblMachineCode = new Label();
            lblStaff = new Label();
            lblLatLong = new Label();
            pboxIconAlert = new PictureBox();
            lblDepth = new Label();
            lblIsBomb = new Label();
            btnSave = new Button();
            // 
            // pnlKQPhanTich
            // 
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblMachineCode);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.lblLatLong);
            this.Controls.Add(this.pboxIconAlert);
            this.Location = new System.Drawing.Point(1022, 540);
            this.Size = new System.Drawing.Size(572, 68);
            //this.TabIndex = 1;
            // 
            // lblMachineCode
            // 
            this.lblMachineCode.AutoSize = true;
            this.lblMachineCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineCode.Location = new System.Drawing.Point(88, 11);
            //this.lblMachineCode.Name = "lblMachineCode";
            this.lblMachineCode.Size = new System.Drawing.Size(145, 20);
            this.lblMachineCode.TabIndex = 1;
            this.lblMachineCode.Text = codeMachine;
            this.lblMachineCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMachineCode.Click += ChildControlClick;
            // 
            // lblStaff
            // 
            this.lblStaff.AutoSize = true;
            this.lblStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff.Location = new System.Drawing.Point(88, 36);
            //this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(134, 20);
            this.lblStaff.TabIndex = 2;
            this.lblStaff.Text = staff;
            this.lblStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStaff.Click += ChildControlClick;
            // 
            // pboxIconAlert
            // 
            this.pboxIconAlert.Dock = System.Windows.Forms.DockStyle.Left;
            if (type == CAMCO)
            {
                this.pboxIconAlert.Image = global::DieuHanhCongTruong.Properties.Resources.camco;
            }
            else if(type == BOM)
            {
                this.pboxIconAlert.Image = global::DieuHanhCongTruong.Properties.Resources.circle_bomb;
            }
            this.pboxIconAlert.Location = new System.Drawing.Point(0, 0);
            //this.pboxIconAlert.Name = "pboxIconAlert";
            this.pboxIconAlert.Size = new System.Drawing.Size(73, 68);
            this.pboxIconAlert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxIconAlert.TabIndex = 0;
            this.pboxIconAlert.TabStop = false;
            this.pboxIconAlert.Margin = new Padding(10);
            this.pboxIconAlert.Padding = new Padding(10);
            this.pboxIconAlert.Click += ChildControlClick;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(270, 34);
            //this.btnSave.Name = "btnSave";
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.AutoSize = true;
            this.btnSave.Text = "Lưu";
            //this.pnlStatus.Size = new System.Drawing.Size(166, 22);
            //this.pnlStatus.TabIndex = 0;
            if (existBomb || existMine)
            {
                this.btnSave.Visible = true;
            }
            else
            {
                this.btnSave.Visible = false;
            }
            this.btnSave.Click += (sender, e) =>
            {
                //if(program_id <= 0)
                //{
                //    MessageBox.Show("Thiếu id dự án", "Thất bại");
                //}
                //double latt = 0, longt = 0;
                SqlCommand cmd1 = new SqlCommand(
                    "INSERT INTO Cecm_VNTerrainMinePoint " +
                    "(Kinhdo, Vido, ZPoint, Deep, programId, idArea, idRectangle, TimeExecute, IsBomb, XPoint, YPoint) " +
                    "VALUES(@Kinhdo, @Vido, @ZPoint, @Deep, @programId, @idArea, @idRectangle, @TimeExecute, @IsBomb, @XPoint, @YPoint)", 
                    frmLoggin.sqlCon);
                //Kinhdo
                SqlParameter Kinhdo = new SqlParameter("@Kinhdo", SqlDbType.Float);
                Kinhdo.Value = longt;
                cmd1.Parameters.Add(Kinhdo);

                //Vido                  
                SqlParameter Vido = new SqlParameter("@Vido", SqlDbType.Float);
                Vido.Value = latt;
                cmd1.Parameters.Add(Vido);

                double[] utm = AppUtils.ConverLatLongToUTM(latt, longt);

                //XPoint
                SqlParameter XPoint = new SqlParameter("@XPoint", SqlDbType.Float);
                XPoint.Value = utm[0];
                cmd1.Parameters.Add(XPoint);

                //YPoint
                SqlParameter YPoint = new SqlParameter("@YPoint", SqlDbType.Float);
                YPoint.Value = utm[1];
                cmd1.Parameters.Add(YPoint);

                //ZPoint                  
                SqlParameter ZPoint = new SqlParameter("@ZPoint", SqlDbType.Float);
                ZPoint.Value = magnetic;
                cmd1.Parameters.Add(ZPoint);

                //Deep                  
                SqlParameter Deep = new SqlParameter("@Deep", SqlDbType.Float);
                Deep.Value = depth;
                cmd1.Parameters.Add(Deep);

                //program_id                  
                SqlParameter programId = new SqlParameter("@programId", SqlDbType.BigInt);
                programId.Value = program_id;
                cmd1.Parameters.Add(programId);

                //idArea                  
                SqlParameter idArea = new SqlParameter("@idArea", SqlDbType.BigInt);
                idArea.Value = area_id;
                cmd1.Parameters.Add(idArea);

                //idRectangle                  
                SqlParameter idRectangle = new SqlParameter("@idRectangle", SqlDbType.BigInt);
                idRectangle.Value = o_id;
                cmd1.Parameters.Add(idRectangle);

                //TimeExecute                  
                SqlParameter TimeExecute = new SqlParameter("@TimeExecute", SqlDbType.DateTime);
                TimeExecute.Value = DateTime.Now;
                cmd1.Parameters.Add(TimeExecute);

                //IsBomb
                int isBomb;
                if(lblIsBomb.Text == COMIN)
                {
                    isBomb = 2;
                }
                else
                {
                    isBomb = 1;
                }
                SqlParameter IsBomb = new SqlParameter("@IsBomb", SqlDbType.Int);
                IsBomb.Value = isBomb;
                cmd1.Parameters.Add(IsBomb);

                int temp1 = 0;
                temp1 = cmd1.ExecuteNonQuery();
                if (temp1 > 0)
                {
                    //status = 1;
                    MessageBox.Show("Thêm dữ liệu thành công... ", "Thành công");
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu không thành công... ", "Thất bại");
                }
            };
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblDepth);
            this.pnlStatus.Controls.Add(this.lblIsBomb);
            this.pnlStatus.Location = new System.Drawing.Point(357, 34);
            //this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(166, 22);
            this.pnlStatus.TabIndex = 0;
            this.pnlStatus.Click += ChildControlClick;
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepth.Location = new System.Drawing.Point(67, 0);
            //this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(32, 20);
            this.lblDepth.TabIndex = 4;
            if (double.IsNaN(depth))
            {
                this.lblDepth.Text = "";
            }
            else
            {
                this.lblDepth.Text = depth.ToString() + "m";
            }
            this.lblDepth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDepth.Click += ChildControlClick;
            // 
            // lblIsBomb
            // 
            this.lblIsBomb.AutoSize = true;
            this.lblIsBomb.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblIsBomb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsBomb.ForeColor = System.Drawing.Color.Red;
            this.lblIsBomb.Location = new System.Drawing.Point(99, 0);
            //this.lblIsBomb.Name = "lblIsBomb";
            this.lblIsBomb.Size = new System.Drawing.Size(67, 20);
            this.lblIsBomb.TabIndex = 4;
            if(existBomb && existMine)
            {
                this.lblIsBomb.Text = COBOMMIN;
            }
            else
            {
                if (existBomb)
                {
                    this.lblIsBomb.Text = COBOM;
                }
                else if (existMine)
                {
                    this.lblIsBomb.Text = COMIN;
                }
                else
                {
                    this.lblIsBomb.Text = "";
                }
            }
            
            this.lblIsBomb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblIsBomb.Click += ChildControlClick;
            // 
            // lblLatLong
            // 
            this.lblLatLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatLong.Location = new System.Drawing.Point(280, 11);
            //this.lblLatLong.Name = "lblLatLong";
            this.lblLatLong.Size = new System.Drawing.Size(243, 20);
            this.lblLatLong.TabIndex = 2;
            this.lblLatLong.Text = Math.Round(latt, 6).ToString() + ", " + Math.Round(longt, 6).ToString() + " - " + magnetic.ToString();
            this.lblLatLong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLatLong.Click += ChildControlClick;
            this.lblLatLong.Click += ChildControlClick;
        }

        public KQPhanTichControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void ChildControlClick(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        public void setDepth(double depth)
        {
            lblDepth.Text = depth.ToString() + "m";
        }

        public void setExistBomb(bool existBomb)
        {
            if (existBomb)
            {
                this.btnSave.Visible = true;
                if (this.lblIsBomb.Text == COMIN || this.lblIsBomb.Text == COBOMMIN)
                {
                    
                    this.lblIsBomb.Text = COBOMMIN;
                }
                else
                {
                    this.lblIsBomb.Text = COBOM;
                }
            }
            else
            {
                
                if (this.lblIsBomb.Text == COMIN || this.lblIsBomb.Text == COBOMMIN)
                {
                    this.btnSave.Visible = true;
                    this.lblIsBomb.Text = COMIN;
                }
                else
                {
                    this.btnSave.Visible = false;
                    this.lblIsBomb.Text = "";
                }
                
            }
        }

        public void setExistMine(bool existMine)
        {
            if (existMine)
            {
                this.btnSave.Visible = true;
                if (this.lblIsBomb.Text == COBOM || this.lblIsBomb.Text == COBOMMIN)
                {

                    this.lblIsBomb.Text = COBOMMIN;
                }
                else
                {
                    this.lblIsBomb.Text = COMIN;
                }
            }
            else
            {
                
                if (this.lblIsBomb.Text == COBOM || this.lblIsBomb.Text == COBOMMIN)
                {
                    this.btnSave.Visible = true;
                    this.lblIsBomb.Text = COBOM;
                }
                else
                {
                    this.btnSave.Visible = false;
                    this.lblIsBomb.Text = "";
                }

            }
        }

        public void setType(int type)
        {
            if (type == BOM)
            {
                this.pboxIconAlert.Image = global::DieuHanhCongTruong.Properties.Resources.circle_bomb;
            }
            else if(type == CAMCO)
            {
                this.pboxIconAlert.Image = global::DieuHanhCongTruong.Properties.Resources.camco;
            }
        }
    }
}
