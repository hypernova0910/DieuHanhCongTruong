using DieuHanhCongTruong.Command;
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
    public partial class MyMainMenu2 : Form
    {
        public static MyMainMenu2 Instance;
        public static long idDADH = -1;

        public MyMainMenu2()
        {
            InitializeComponent();
            CustomizeDesign();
            Instance = this;
        }

        private void CustomizeDesign()
        {
            SetTagForSubMenu();
        }

        private delegate void MenuCommandDelegate();

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if(!(sender is ToolStripMenuItem))
            //{
            //    return;
            //}
            //ToolStripMenuItem menuItem = (ToolStripMenuItem)e.ClickedItem;
            //if(menuItem.Tag != null)
            //{
            //    MenuCommandDelegate d = menuItem.Tag as MenuCommandDelegate;
            //    d();
            //}
        }

        private void SetTagForSubMenu()
        {
            //Quản lý đơn vị
            quảnLýĐơnVịToolStripMenuItem.Click += new EventHandler(MenuCommand2.QuanLyDonVi);
            //Danh sách dự án
            danhSáchCácDựÁnToolStripMenuItem.Click += new EventHandler(MenuCommand2.DanhSachDuAn);
            //Điều hành, giám sát
            điềuHànhDựÁnToolStripMenuItem.Click += new EventHandler(MenuCommand2.DieuHanhDuAn);
            nhậnDữLiệuTừMáyDòToolStripMenuItem.Click += new EventHandler(MenuCommand2.NhanDuLieuMayDo);
            //Tiện ích
            danhSáchBMVNToolStripMenuItem.Click += new EventHandler(MenuCommand2.DanhSachBMVN);
            //Menu báo cáo KS
            ks01ToolStripMenuItem.Click += new EventHandler(MenuCommand2.KS01);
            ks02ToolStripMenuItem.Click += new EventHandler(MenuCommand2.KS02);
            ks03ToolStripMenuItem.Click += new EventHandler(MenuCommand2.KS03);
            ks04ToolStripMenuItem.Click += new EventHandler(MenuCommand2.KS04);
            ks05ToolStripMenuItem.Click += new EventHandler(MenuCommand2.KS05);
            //Menu báo cáo RP
            rp01ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM01);
            rp02ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM02);
            rp03ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM03);
            rp04ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM04);
            rp05ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM05);
            rp06ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM06);
            rp07ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM07);
            rp08ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM08);
            rp09ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM09);
            rp10ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM10);
            rp11ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM11);
            rp12ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM12);
            rp13ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM13);
            rp14ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM14);
            rp15ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM15);
            rp16ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM16);
            rp17ToolStripMenuItem.Click += new EventHandler(MenuCommand2.RPBM17);
            //Cài đặt
            càiĐặtChungToolStripMenuItem.Click += new EventHandler(MenuCommand2.CaiDatChung);
            thôngTinPhầnMềmToolStripMenuItem.Click += new EventHandler(MenuCommand2.ThongTinPhanMem);
            //Tài khoản
            đổiMậtKhẩuToolStripMenuItem.Click += new EventHandler(MenuCommand2.DoiMK);
            đăngXuấtToolStripMenuItem.Click += new EventHandler(MenuCommand2.DangXuat);
        }

        private void MyMainMenu2_Load(object sender, EventArgs e)
        {
            if (frmLoggin.sqlCon == null)
            {
                this.Hide();
                frmLoggin frm = new frmLoggin();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                    //MapMenuCommand.LoadMap();
                }
                else
                {
                    this.Close();
                }
            }
            
        }
    }
}
