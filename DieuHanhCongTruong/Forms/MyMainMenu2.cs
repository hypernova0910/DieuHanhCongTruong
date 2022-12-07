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
    public partial class MyMainMenu2 : Form
    {
        public static MyMainMenu2 Instance;
        public static long idDADH = -1;
        public static string tenDADH = "";
        private bool init = false;

        public MyMainMenu2()
        {
            Instance = this;
            InitializeComponent();
            CustomizeDesign();
            
            init = true;
        }

        private void CustomizeDesign()
        {
            SetTagForSubMenu();
            SetEventForToolStrip();
            tabCtrlLineChart.Visible = false;
            pnlChonDiemMatCat.Visible = false;
            tsProgressSurface.Visible = false;
            tsProgressHistory.Visible = false;
            tsProgressPhanTich.Visible = false;
            tabControlBottom.Visible = false;
            pnlToolTip.Visible = false;
            
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
            //In ấn bản đồ
            chỉnhSửaÔLướiToolStripMenuItem.Click += new EventHandler(MenuCommand2.ChinhSuaOLuoi);
            inMảnhBảnĐồTheoÔLướiToolStripMenuItem.Click += new EventHandler(MenuCommand2.InOLuoi);
            //Điều hành, giám sát
            điềuHànhDựÁnToolStripMenuItem.Click += new EventHandler(MenuCommand2.DieuHanhDuAn);
            nhậnDữLiệuTừMáyDòToolStripMenuItem.Click += new EventHandler(MenuCommand2.NhanDuLieuMayDo);
            //Phân tích dữ liệu
            tựĐộngPhânTíchDữLiệuToolStripMenuItem.Click += new EventHandler(MenuCommand2.TuDongPhanTich);
            cậpNhậtDữLiệuTừMáyDòToolStripMenuItem.Click += new EventHandler(MenuCommand2.CapNhatDuLieuTuMayDo);
            vẽMặtCắtTừTrườngToolStripMenuItem.Click += new EventHandler(MenuCommand2.VeMatCatTuTruong);
            tìmĐiểmTừTrườngDựaVàoMặtCắtToolStripMenuItem.Click += new EventHandler(MenuCommand2.TimDiemTuTruongMatCat);
            phânTíchĐộSâuDựaVàoKhoảngGiảmNghiNgờToolStripMenuItem.Click += new EventHandler(MenuCommand2.PhanTichDoSauKhoangGiamNghiNgo);
            phânTíchKhoảngGiảmNghiNgờToolStripMenuItem.Click += new EventHandler(MenuCommand2.PhanTichKhoangGiamNghiNgo);
            phânTíchDảiMàuToolStripMenuItem.Click += new EventHandler(MenuCommand2.PhanTichDaiMau);
            biểuĐồTừTrườngToolStripMenuItem.Click += new EventHandler(MenuCommand2.BieuDoTuTruong);
            bậtTắtĐốiTượngToolStripMenuItem.Click += new EventHandler(MenuCommand2.BatTatDoiTuong);
            danhSáchBMVNToolStripMenuItem.Click += new EventHandler(MenuCommand2.DanhSachBMVN);
            //Tiện ích
            khoảngCáchToolStripMenuItem.Click += new EventHandler(MenuCommand2.KhoangCach);
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

        private void SetEventForToolStrip()
        {
            //In ấn bản đồ
            btnInManhBanDoOLuoi.Click += new EventHandler(MenuCommand2.InOLuoi);
            //Điều hành, giám sát
            btnNhanDuLieuMayDo.Click += new EventHandler(MenuCommand2.NhanDuLieuMayDo);
            //Phân tích dữ liệu
            btnTuDongPhanTich.Click += new EventHandler(MenuCommand2.TuDongPhanTich);
            btnVeMatCatTuTruong.Click += new EventHandler(MenuCommand2.VeMatCatTuTruong);
            btnTimDiemTuTruongDuaVaoMatCat.Click += new EventHandler(MenuCommand2.TimDiemTuTruongMatCat);
            btnTuTruongBeMat.Click += new EventHandler(MenuCommand2.TimDiemTuTruongBeMat);
            btnPhanTichDoSau.Click += new EventHandler(MenuCommand2.PhanTichDoSauKhoangGiamNghiNgo);
            btnChiTietBMVNPhanTich.Click += new EventHandler(MenuCommand2.PhanTichKhoangGiamNghiNgo);
            btnPhanTichDaiMau.Click += new EventHandler(MenuCommand2.PhanTichDaiMau);
            btnBieuDoTuTruong.Click += new EventHandler(MenuCommand2.BieuDoTuTruong);
            btnBatTatDoiTuong.Click += new EventHandler(MenuCommand2.BatTatDoiTuong);
            btnDanhSachBMVN.Click += new EventHandler(MenuCommand2.DanhSachBMVN);
            //Tiện ích
            btnKhoangCach.Click += new EventHandler(MenuCommand2.KhoangCach);
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
                    MapMenuCommand.LoadMap();
                    managerCECMUserControl1.LoadTreeView(true);
                }
                else
                {
                    this.Close();
                }
            }
            
        }

        private void btnShowPolygon_Click(object sender, EventArgs e)
        {
            MapMenuCommand.togglePolygon(true);
        }

        private void btnHidePolygon_Click(object sender, EventArgs e)
        {
            MapMenuCommand.togglePolygon(false);
        }

        //private void btnShowPolygonBombMine_Click(object sender, EventArgs e)
        //{
        //    if(btnShowPolygonBombMine.Text == "Từ trường máy mìn")
        //    {
        //        btnShowPolygonBombMine.Text = "Từ trường máy bom";
        //        MapMenuCommand.showPolygonMine();
        //    }
        //    else
        //    {
        //        btnShowPolygonBombMine.Text = "Từ trường máy mìn";
        //        MapMenuCommand.showPolygonBomb();
        //    }
        //}

        private void MyMainMenu2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        //Khi phân tích sẽ disable một số menu
        public void TogglePhanTichMenu(bool enable)
        {
            ToggleMagneticMenu(enable);
            TogglePointMenu(enable);
        }

        //Menu ẩn
        public void ToggleMagneticMenu(bool enable)
        {
            tựĐộngPhânTíchDữLiệuToolStripMenuItem.Enabled = enable;
            vẽMặtCắtTừTrườngToolStripMenuItem.Enabled = enable;
            phânTíchDảiMàuToolStripMenuItem.Enabled = enable;
            tìmĐiểmTừTrườngDựaVàoMặtCắtToolStripMenuItem.Enabled = enable;
            phânTíchĐộSâuDựaVàoKhoảngGiảmNghiNgờToolStripMenuItem.Enabled = enable;
            phânTíchKhoảngGiảmNghiNgờToolStripMenuItem.Enabled = enable;

            btnTuDongPhanTich.Visible = enable;
            btnVeMatCatTuTruong.Visible = enable;
            btnPhanTichDaiMau.Visible = enable;
            btnTimDiemTuTruongDuaVaoMatCat.Visible = enable;
            btnChiTietBMVNPhanTich.Visible = enable;
            btnPhanTichDoSau.Visible = enable;
            btnTuTruongBeMat.Visible = enable;
        }

        public void TogglePointMenu(bool enable)
        {
            cậpNhậtDữLiệuTừMáyDòToolStripMenuItem.Enabled = enable;
        }

        public void ToggleMapMenu(bool enable)
        {
            chỉnhSửaÔLướiToolStripMenuItem.Enabled = enable;
        }

        private void tabCtrlLineChart_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            //e.Graphics.DrawString(this.tabCtrlLineChart.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            //e.DrawFocusRectangle();
            var tabPage = this.tabCtrlLineChart.TabPages[e.Index];
            var tabRect = this.tabCtrlLineChart.GetTabRect(e.Index);
            var closeImage = Properties.Resources.Delete;
            
            //if(e.Index == tabCtrlLineChart.SelectedIndex)
            //{
            //    using (SolidBrush brush =
            //       new SolidBrush(Constants.MENU_BACKGROUND_COLOR))
            //    using (SolidBrush textBrush =
            //           new SolidBrush(Constants.SELECTED_COLOR))
            //    {
            //        e.Graphics.FillRectangle(brush, e.Bounds);
            //        e.Graphics.DrawString(tabPage.Text, e.Font, textBrush, e.Bounds.X + 3, e.Bounds.Y + 4);
            //        e.Graphics.DrawImage(closeImage,
            //        (tabRect.Right - closeImage.Width),
            //        tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
            //    }
            //}
            //else
            //{
                e.Graphics.DrawImage(closeImage,
                (tabRect.Right - closeImage.Width),
                tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                    tabRect, e.Index == tabCtrlLineChart.SelectedIndex ? Constants.SELECTED_COLOR : tabPage.ForeColor, TextFormatFlags.Left);
            //}
            
        }

        private void tabCtrlLineChart_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            //for (int i = 0; i < this.tabCtrlLineChart.TabPages.Count; i++)
            //{
            //    Rectangle r = tabCtrlLineChart.GetTabRect(i);
            //    //Getting the position of the "x" mark.
            //    Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
            //    if (closeButton.Contains(e.Location))
            //    {
            //        this.tabCtrlLineChart.TabPages.RemoveAt(i);
            //        break;
            //    }
            //}
            var closeImage = Properties.Resources.Delete;
            for (var i = 0; i < this.tabCtrlLineChart.TabPages.Count; i++)
            {
                var tabRect = this.tabCtrlLineChart.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                
                var imageRect = new Rectangle(
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                    closeImage.Width,
                    closeImage.Height);
                if (imageRect.Contains(e.Location))
                {
                    this.tabCtrlLineChart.TabPages.RemoveAt(i);
                    break;
                }
            }
            if(tabCtrlLineChart.TabPages.Count == 0)
            {
                tabCtrlLineChart.Visible = false;
                pnlChonDiemMatCat.Visible = false;
                if(tabPageBMVN.Controls.Count == 0)
                {
                    tabControlBottom.Visible = false;
                }
            }
        }

        private void rbBomb_CheckedChanged(object sender, EventArgs e)
        {
            MapMenuCommand.toggleLayer(rbBomb.Checked, rbModel.Checked);
        }

        private void rbMine_CheckedChanged(object sender, EventArgs e)
        {
            //MapMenuCommand.togglePolygonMine(rbMine.Checked, rbModel.Checked);
        }

        private void rbUnmodel_CheckedChanged(object sender, EventArgs e)
        {
            MapMenuCommand.toggleLayer(rbBomb.Checked, rbModel.Checked);
        }
    }
}
