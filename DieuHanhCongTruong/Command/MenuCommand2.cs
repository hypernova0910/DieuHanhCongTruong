using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MIConvexHull;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin;

namespace DieuHanhCongTruong.Command
{
    class MenuCommand2
    {
        public static void RPBM01(object sender, EventArgs e)
        {
            FormRPBM1 form = new FormRPBM1();
            form.ShowDialog();
        }

        public static void RPBM02(object sender, EventArgs e)
        {
            FormRPBM2 form = new FormRPBM2();
            form.ShowDialog();
        }

        public static void RPBM03(object sender, EventArgs e)
        {
            FormRPBM3 form = new FormRPBM3();
            form.ShowDialog();
        }

        public static void RPBM04(object sender, EventArgs e)
        {
            FormRPBM4 form = new FormRPBM4();
            form.ShowDialog();
        }

        public static void RPBM05(object sender, EventArgs e)
        {
            FormRPBM5 form = new FormRPBM5();
            form.ShowDialog();
        }

        public static void RPBM06(object sender, EventArgs e)
        {
            FormRPBM6 form = new FormRPBM6();
            form.ShowDialog();
        }

        public static void RPBM07(object sender, EventArgs e)
        {
            FormRPBM7 form = new FormRPBM7();
            form.ShowDialog();
        }

        public static void RPBM08(object sender, EventArgs e)
        {
            FormRPBM8 form = new FormRPBM8();
            form.ShowDialog();
        }

        public static void RPBM09(object sender, EventArgs e)
        {
            FormRPBM9 form = new FormRPBM9();
            form.ShowDialog();
        }

        public static void RPBM10(object sender, EventArgs e)
        {
            FormRPBM10 form = new FormRPBM10();
            form.ShowDialog();
        }

        public static void RPBM11(object sender, EventArgs e)
        {
            FormRPBM11 form = new FormRPBM11();
            form.ShowDialog();
        }

        public static void RPBM12(object sender, EventArgs e)
        {
            FormRPBM12 form = new FormRPBM12();
            form.ShowDialog();
        }

        public static void RPBM13(object sender, EventArgs e)
        {
            FormRPBM13 form = new FormRPBM13();
            form.ShowDialog();
        }

        public static void RPBM14(object sender, EventArgs e)
        {
            FormRPBM14 form = new FormRPBM14();
            form.ShowDialog();
        }

        public static void RPBM15(object sender, EventArgs e)
        {
            FormRPBM15 form = new FormRPBM15();
            form.ShowDialog();
        }

        public static void RPBM16(object sender, EventArgs e)
        {
            FormRPBM16 form = new FormRPBM16();
            form.ShowDialog();
        }

        public static void RPBM17(object sender, EventArgs e)
        {
            FormRPBM17 form = new FormRPBM17();
            form.ShowDialog();
        }

        public static void KS01(object sender, EventArgs e)
        {
            FrmKHKSKT form = new FrmKHKSKT();
            form.ShowDialog();
        }

        public static void KS02(object sender, EventArgs e)
        {
            FrmBaocaoKQ form = new FrmBaocaoKQ();
            form.ShowDialog();
        }

        public static void KS03(object sender, EventArgs e)
        {
            FrmBCKVON form = new FrmBCKVON();
            form.ShowDialog();
        }

        public static void KS04(object sender, EventArgs e)
        {
            FrmBaocaoKV form = new FrmBaocaoKV();
            form.ShowDialog();
        }

        public static void KS05(object sender, EventArgs e)
        {
            FormBCTH form = new FormBCTH();
            form.ShowDialog();
        }

        public static void QuanLyDonVi(object sender, EventArgs e)
        {
            QuanLyDonViForm form = new QuanLyDonViForm();
            form.ShowDialog();
        }

        public static void DanhSachDuAn(object sender, EventArgs e)
        {
            DanhSachDuAnFrm form = new DanhSachDuAnFrm();
            form.ShowDialog();
        }

        public static void CaiDatChung(object sender, EventArgs e)
        {
            MenuLoaderManagerFrm form = new MenuLoaderManagerFrm();
            form.ShowDialog();
        }

        public static void ThongTinPhanMem(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog();
        }

        public static void DieuHanhDuAn(object sender, EventArgs e)
        {
            MyMainMenu2.Instance.managerCECMUserControl1.LoadTreeView();
        }

        public static void NhanDuLieuMayDo(object sender, EventArgs e)
        {
            MonitorFormNew form = new MonitorFormNew();
            form.ShowDialog();
        }

        public static void TuDongPhanTich(object sender, EventArgs e)
        {
            if (MyMainMenu2.idDADH <= 0)
            {
                MessageBox.Show("Chưa điều hành dự án");
                MyMainMenu2.Instance.managerCECMUserControl1.LoadTreeView();
                return;
            }
            MapMenuCommand.ClearSuspectPoints();
            foreach (List<CustomFace> triangulation in TINCommand.triangulations.Values)
            {
                PhanTichCommand cmd = new PhanTichCommand(triangulation);
                cmd.Execute();
            }
        }

        public static void CapNhatDuLieuTuMayDo(object sender, EventArgs e)
        {
            if(MyMainMenu2.idDADH <= 0)
            {
                MessageBox.Show("Chưa điều hành dự án");
                MyMainMenu2.Instance.managerCECMUserControl1.LoadTreeView();
                return;
            }
            MagneticCommand.Execute(MyMainMenu2.idDADH);
        }

        public static void VeMatCatTuTruong(object sender, EventArgs e)
        {
            if (MyMainMenu2.idDADH <= 0)
            {
                MessageBox.Show("Chưa điều hành dự án");
                MyMainMenu2.Instance.managerCECMUserControl1.LoadTreeView();
                return;
            }
            VeMatCatTuTruongCommand.Execute();
        }

        public static void TimDiemTuTruongMatCat(object sender, EventArgs e)
        {
            if (MyMainMenu2.Instance.tabCtrlLineChart.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa vẽ mặt cắt từ trường");
                return;
            }
            TimDiemMatCatCommand cmd = new TimDiemMatCatCommand();
            cmd.Execute();
        }

        public static void PhanTichKhoangGiamNghiNgo(object sender, EventArgs e)
        {
            //if (MyMainMenu2.Instance.tabCtrlLineChart.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Chưa vẽ mặt cắt từ trường");
            //    return;
            //}
            PhanTichKhoangGiamNghiNgoCommand.Execute();
        }

        public static void DanhSachBMVN(object sender, EventArgs e)
        {
            DanhSachBMVN form = new DanhSachBMVN();
            form.ShowDialog();
        }

        public static void KhoangCach(object sender, EventArgs e)
        {
            KhoangCachCommand.Execute();
        }

        public static void DoiMK(object sender, EventArgs e)
        {
            frmChangePass form = new frmChangePass();
            form.ShowDialog();
            if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    SqlCommand cmd2 = new SqlCommand(string.Format("ALTER LOGIN {0} WITH PASSWORD = '{1}' OLD_PASSWORD = '{2}'", frmLoggin.userName, form.MatKhauMoi, form.matKhauCu), frmLoggin.sqlCon);
                    int temp = 0;
                    temp = cmd2.ExecuteNonQuery();
                    if (temp == -1)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công, Bạn hãy đăng nhập lại hệ thống");
                    }
                    OpenLoginForm();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Đã có lỗi xảy ra khi cập nhật dữ liệu vào Database! {0}", ex.Message);
                    return;
                }
            }
        }

        public static void DangXuat(object sender, EventArgs e)
        {
            try
            {
                frmLoggin.userPasswords = string.Empty;
                if (frmLoggin.sqlCon != null)
                {
                    frmLoggin.sqlCon.Close();
                    frmLoggin.sqlCon.Dispose();
                    frmLoggin.sqlCon = null;

                    MessageBox.Show("Đăng xuất thành công", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Chưa kết nối vào hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            OpenLoginForm();
        }

        private static void OpenLoginForm()
        {
            MyMainMenu2.Instance.Hide();
            frmLoggin frm = new frmLoggin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MyMainMenu2.Instance.Show();
            }
            else
            {
                MyMainMenu2.Instance.Close();
            }
        }

        public static void PhanTichDaiMau(object sender, EventArgs e)
        {
            PhanTichDaiMauTuTruong form = new PhanTichDaiMauTuTruong();
            form.ShowDialog();
        }
    }
}
