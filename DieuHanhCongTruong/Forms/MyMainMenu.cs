using DieuHanhCongTruong.Command;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.Forms
{
    public partial class MyMainMenu : Form
    {
        private Button lastSelectedMenu;
        private Button lastSelectedSubMenu;
        private Form activeForm;
        public static MyMainMenu Instance;

        public MyMainMenu()
        {
            InitializeComponent();
            CustomizeDesign();
            Instance = this;
        }

        private void CustomizeDesign()
        {
            HideAllSubMenu();
            SetTagForSubMenu();
        }

        private void HideAllSubMenu()
        {
            pnlDieuHanhGiamSatSub.Visible = false;
            pnlDuLieuHoTroSub.Visible = false;
            pnlInAnBanDoSub.Visible = false;
            pnlPhanTichDuLieuSub.Visible = false;
            pnlQuanLyDuAnSub.Visible = false;
            pnlTienIchSub.Visible = false;
            pnlKetQuaKSSub.Visible = false;
            pnlKQRPSub.Visible = false;
            pnlCaiDatSub.Visible = false;
            pnlTaiKhoanSub.Visible = false;
        }

        private delegate void MenuCommandDelegate();

        private void SetTagForSubMenu()
        {
            //Quản lý đơn vị
            btnQuanLyDonVi_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.QuanLyDonVi;
            //Danh sách dự án
            btnDanhSachCacDuAn_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.DanhSachDuAn;
            //Điều hành, giám sát
            btnNhanDuLieuTuMayDo_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.NhanDuLieuMayDo;
            //Tiện ích
            btnDanhSachBMVN_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.DanhSachBMVN;
            //Menu báo cáo KS
            btnKS01_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.KS01;
            btnKS02_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.KS02;
            btnKS03_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.KS03;
            btnKS04_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.KS04;
            btnKS05_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.KS05;
            //Menu báo cáo RP
            btnRP01_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM01;
            btnRP02_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM02;
            btnRP03_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM03;
            btnRP04_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM04;
            btnRP05_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM05;
            btnRP06_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM06;
            btnRP07_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM07;
            btnRP08_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM08;
            btnRP09_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM09;
            btnRP10_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM10;
            btnRP11_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM11;
            btnRP12_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM12;
            btnRP13_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM13;
            btnRP14_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM14;
            btnRP15_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM15;
            btnRP16_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM16;
            btnRP17_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.RPBM17;
            //Cài đặt
            btnCaiDatChung_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.CaiDatChung;
            btnThongTinPhanMem_SubMenu.Tag = (MenuCommandDelegate)MenuCommand.ThongTinPhanMem;
        }

        private void ToggleSubMenu(Panel subMenu)
        {
            bool visible = subMenu.Visible;
            HideAllSubMenu();
            subMenu.Visible = !visible;
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Panel subMenu = (Panel)this.GetNextControl(btn, true);
            ToggleSubMenu(subMenu);
            SelectMenu(btn);
        }

        private void SelectMenu(Button btn)
        {
            if(lastSelectedMenu != null)
            {
                lastSelectedMenu.BackColor = Constants.MENU_BACKGROUND_COLOR;
                lastSelectedMenu.ForeColor = SystemColors.ControlText;
                lastSelectedMenu.Font = new Font(lastSelectedMenu.Font, FontStyle.Regular);
            }
            if(this.GetNextControl(btn, true).Visible)
            {
                btn.BackColor = Constants.SELECTED_COLOR;
                btn.ForeColor = Constants.MENU_BACKGROUND_COLOR;
                btn.Font = new Font(btn.Font, FontStyle.Bold);
            }
            lastSelectedMenu = btn;
        }

        private void SubMenu_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.ForeColor == Constants.SELECTED_COLOR)
            {
                return;
            }
            if (lastSelectedSubMenu != null)
            {
                lastSelectedSubMenu.ForeColor = SystemColors.ControlText;
            }
            btn.ForeColor = Constants.SELECTED_COLOR;
            lastSelectedSubMenu = btn;
            if(btn.Tag != null)
            {
                MenuCommandDelegate d = btn.Tag as MenuCommandDelegate;
                d();
            }
        }

        private void MyMainMenu_Load(object sender, EventArgs e)
        {
            if(frmLoggin.sqlCon == null)
            {
                this.Hide();
                frmLoggin frm = new frmLoggin();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    this.Close();
                }
            }
        }

        public void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.AutoScaleMode = AutoScaleMode.Dpi;
            pnlMain.Controls.Add(childForm);
            //pnlMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnCaiDat_Menu_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Panel subMenu = (Panel)this.GetNextControl(btn, true);
            ToggleSubMenu(subMenu);
        }

        private void btnCaiDatChung_SubMenu_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag != null)
            {
                MenuCommandDelegate d = btn.Tag as MenuCommandDelegate;
                d();
            }
        }

        private void btnThonhTinPhanMem_SubMenu_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag != null)
            {
                MenuCommandDelegate d = btn.Tag as MenuCommandDelegate;
                d();
            }
        }

        private void MyMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
        }
    }
}
