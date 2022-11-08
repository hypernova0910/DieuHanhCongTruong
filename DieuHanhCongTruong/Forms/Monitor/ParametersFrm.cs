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
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            AppUtils.SaveRecentInput(nudDilution);
            AppUtils.SaveRecentInput(nudMinPoint);
            AppUtils.SaveRecentInput(nudMinTime);
            MessageBox.Show("Cập nhật tham số thành công");
            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            nudDilution.Value = (decimal)DILUTION_DEFAULT;
            nudMinPoint.Value = MIN_POINT_DEFAULT;
            nudMinTime.Value = MIN_TIME_DEFAULT;
        }
    }
}
