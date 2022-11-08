using DieuHanhCongTruong.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNRaPaBomMin;

namespace DieuHanhCongTruong.Command
{
    class MenuCommand
    {
        public static void RPBM01()
        {
            FormRPBM1 form = new FormRPBM1();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM02()
        {
            FormRPBM2 form = new FormRPBM2();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM03()
        {
            FormRPBM3 form = new FormRPBM3();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM04()
        {
            FormRPBM4 form = new FormRPBM4();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM05()
        {
            FormRPBM5 form = new FormRPBM5();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM06()
        {
            FormRPBM6 form = new FormRPBM6();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM07()
        {
            FormRPBM7 form = new FormRPBM7();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM08()
        {
            FormRPBM8 form = new FormRPBM8();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM09()
        {
            FormRPBM9 form = new FormRPBM9();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM10()
        {
            FormRPBM10 form = new FormRPBM10();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM11()
        {
            FormRPBM11 form = new FormRPBM11();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM12()
        {
            FormRPBM12 form = new FormRPBM12();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM13()
        {
            FormRPBM13 form = new FormRPBM13();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM14()
        {
            FormRPBM14 form = new FormRPBM14();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM15()
        {
            FormRPBM15 form = new FormRPBM15();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM16()
        {
            FormRPBM16 form = new FormRPBM16();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void RPBM17()
        {
            FormRPBM17 form = new FormRPBM17();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void KS01()
        {
            FrmKHKSKT form = new FrmKHKSKT();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void KS02()
        {
            FrmBaocaoKQ form = new FrmBaocaoKQ();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void KS03()
        {
            FrmBCKVON form = new FrmBCKVON();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void KS04()
        {
            FrmBaocaoKV form = new FrmBaocaoKV();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void KS05()
        {
            FormBCTH form = new FormBCTH();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void QuanLyDonVi()
        {
            QuanLyDonViForm form = new QuanLyDonViForm();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void DanhSachDuAn()
        {
            DanhSachDuAnFrm form = new DanhSachDuAnFrm();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void CaiDatChung()
        {
            MenuLoaderManagerFrm form = new MenuLoaderManagerFrm();
            form.ShowDialog();
        }

        public static void ThongTinPhanMem()
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog();
        }

        public static void NhanDuLieuMayDo()
        {
            MonitorFormNew form = new MonitorFormNew();
            MyMainMenu.Instance.OpenChildForm(form);
        }

        public static void DanhSachBMVN()
        {
            DanhSachBMVN form = new DanhSachBMVN();
            MyMainMenu.Instance.OpenChildForm(form);
        }
    }
}
