using DieuHanhCongTruong.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class ParametersFrm : Form
    {
        //Sai số GPS mặc định
        public static double DILUTION_DEFAULT = 3.2;

        //TG quy định điểm liền kề mặc định
        public static int MIN_TIME_DEFAULT = 20;

        //Số điểm tối thiểu mặc định
        public static int MIN_POINT_DEFAULT = 10;

        public static double KHOANG_CAP_NHAT_PT_ONLINE_DEFAULT = 1000;

        public static int KHOANG_PT_DEFAULT = 3;

        public static double RANH_DO_PT_DEFAULT = 0.2;

        public static double NGUONG_BOM_DEFAULT = 1;

        public static double NGUONG_MIN_DEFAULT = 8;

        public static int MIN_CLUSTER_SIZE_DEFAULT = 1;

        public static double MIN_BOMB_DEFAULT = 0.2;

        public static double MIN_MINE_DEFAULT = 2;


        public ParametersFrm()
        {
            InitializeComponent();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Parameters_Load(object sender, EventArgs e)
        {
            AppUtils.LoadRecentInput(nudDilution, DILUTION_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudMinPoint, MIN_POINT_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudMinTime, MIN_TIME_DEFAULT.ToString());

            AppUtils.LoadRecentInput(tbPhanTichOnline, KHOANG_CAP_NHAT_PT_ONLINE_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudKhoangPT, KHOANG_PT_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudRanhDoPT, RANH_DO_PT_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudNguongBom, NGUONG_BOM_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudNguongMin, NGUONG_MIN_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudMinClusterSize, MIN_CLUSTER_SIZE_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudMinBomb, MIN_BOMB_DEFAULT.ToString());
            AppUtils.LoadRecentInput(nudMinMine, MIN_MINE_DEFAULT.ToString());
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            AppUtils.SaveRecentInput(nudDilution);
            AppUtils.SaveRecentInput(nudMinPoint);
            AppUtils.SaveRecentInput(nudMinTime);

            AppUtils.SaveRecentInput(tbPhanTichOnline);
            AppUtils.SaveRecentInput(nudKhoangPT);
            AppUtils.SaveRecentInput(nudRanhDoPT);
            AppUtils.SaveRecentInput(nudNguongBom);
            AppUtils.SaveRecentInput(nudNguongMin);
            AppUtils.SaveRecentInput(nudMinClusterSize);
            AppUtils.SaveRecentInput(nudMinBomb);
            AppUtils.SaveRecentInput(nudMinMine);
            MessageBox.Show("Cập nhật tham số thành công");
            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            nudDilution.Value = (decimal)DILUTION_DEFAULT;
            nudMinPoint.Value = MIN_POINT_DEFAULT;
            nudMinTime.Value = MIN_TIME_DEFAULT;
        }

        private void btnDefaultPTOnline_Click(object sender, EventArgs e)
        {
            tbPhanTichOnline.Text = KHOANG_CAP_NHAT_PT_ONLINE_DEFAULT.ToString();
            nudKhoangPT.Value = KHOANG_PT_DEFAULT;
            nudRanhDoPT.Value = (decimal)RANH_DO_PT_DEFAULT;
            nudNguongBom.Value = (decimal)NGUONG_BOM_DEFAULT;
            nudNguongMin.Value = (decimal)NGUONG_MIN_DEFAULT;
            nudMinClusterSize.Value = MIN_CLUSTER_SIZE_DEFAULT;
            nudMinBomb.Value = (decimal)MIN_BOMB_DEFAULT;
            nudMinMine.Value = (decimal)MIN_MINE_DEFAULT;
        }
    }
}
