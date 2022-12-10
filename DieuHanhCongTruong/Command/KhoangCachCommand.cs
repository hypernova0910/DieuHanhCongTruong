using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.Command
{
    class KhoangCachCommand
    {
        public static void Execute()
        {
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmMeasure;
            Measuring measuring = MapMenuCommand.axMap1.Measuring;
            measuring.MeasuringType = tkMeasuringType.MeasureDistance;
            measuring.LengthPrecision = 3;
            measuring.LengthUnits = tkLengthDisplayMode.ldmMetric;
            MapMenuCommand.axMap1.MeasuringChanged += AxMap1_MeasuringChanged;
            measuring.ShowLength = false;
            MyMainMenu2.Instance.KeyDown += Instance_KeyDown;
            MyMainMenu2.Instance.pnlToolTip.Visible = true;
            MyMainMenu2.Instance.lblToolTip.Visible = true;
            MyMainMenu2.Instance.lblToolTip.Text = "Chọn điểm 1. Nhấn ESC để hủy";
            MyMainMenu2.Instance.menuStrip1.Enabled = false;
            MyMainMenu2.Instance.pnlToolBar.Enabled = false;
        }

        private static void Instance_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Exit();
                //e.SuppressKeyPress = true;
            }
        }

        private static void Exit()
        {
            MyMainMenu2.Instance.pnlToolTip.Visible = false;
            MyMainMenu2.Instance.lblToolTip.Visible = false;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmPan;
            MapMenuCommand.axMap1.MeasuringChanged -= AxMap1_MeasuringChanged;
            MyMainMenu2.Instance.KeyDown -= Instance_KeyDown;
            MyMainMenu2.Instance.menuStrip1.Enabled = true;
            MyMainMenu2.Instance.pnlToolBar.Enabled = true;
        }

        private static void AxMap1_MeasuringChanged(object sender, AxMapWinGIS._DMapEvents_MeasuringChangedEvent e)
        {
            Measuring measuring = MapMenuCommand.axMap1.Measuring;
            if(measuring.PointCount == 1)
            {
                MyMainMenu2.Instance.lblToolTip.Text = "Chọn điểm 2. Nhấn ESC để hủy";
            }
            if (measuring.PointCount == 2)
            {
                measuring.get_PointXY(0, out double x1, out double y1);
                measuring.get_PointXY(1, out double x2, out double y2);
                double distance = AppUtils.DistanceLatLong(y1, x1, y2, x2);
                MapMenuCommand.axMap1.Measuring.FinishMeasuring();
                MapMenuCommand.axMap1.Redraw();
                MessageBox.Show(
                    "Kinh độ điểm 1: " + x1 + "\n" +
                    "Vĩ độ điểm 1: " + y1 + "\n" +
                    "Kinh độ điểm 2: " + x2 + "\n" +
                    "Vĩ độ điểm 2: " + y2 + "\n" +
                    "Khoảng cách: " + Math.Round(distance, 3) + "m"
                );
                MyMainMenu2.Instance.lblToolTip.Text = "Chọn điểm 1. Nhấn ESC để hủy";
                //MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmPan;
            }
        }
    }
}
