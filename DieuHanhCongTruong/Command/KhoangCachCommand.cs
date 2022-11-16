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
            //MyMainMenu2.Instance.KeyDown += Instance_KeyDown;
        }

        private static void Instance_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmPan;
                e.SuppressKeyPress = true;
            }
        }

        private static void AxMap1_MeasuringChanged(object sender, AxMapWinGIS._DMapEvents_MeasuringChangedEvent e)
        {
            Measuring measuring = MapMenuCommand.axMap1.Measuring;
            if (measuring.PointCount == 2)
            {
                measuring.get_PointXY(0, out double x1, out double y1);
                measuring.get_PointXY(1, out double x2, out double y2);
                double distance = AppUtils.DistanceLatLong(y1, x1, y2, x2);
                MessageBox.Show("Khoảng cách: " + distance + "m");
                MapMenuCommand.axMap1.Measuring.FinishMeasuring();
                MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmPan;
            }
        }
    }
}
