﻿using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.PhanTich;
using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DieuHanhCongTruong.Command
{
    class TimDiemMatCatCommand
    {
        private TabPage activeTab;

        public void Execute()
        {
            MatCatTuTruong.isPointing = true;
            activeTab = MyMainMenu2.Instance.tabCtrlLineChart.SelectedTab;
            MatCatTuTruong chartForm = activeTab.Controls[0] as MatCatTuTruong;
            //chartForm.toolTip1.SetToolTip(chartForm.chart1, "Chọn điểm nghi ngờ. Enter để hoàn tất. Esc để hủy");
            chartForm.chart1.MouseClick += chart1_MouseClick;
            //chartForm.KeyDown += chart1_KeyDown;
            //chartForm.PreviewKeyDown += ChartForm_PreviewKeyDown;
            MyMainMenu2.Instance.buttonCancelPoints.Click += ButtonCancelPoints_Click;
            MyMainMenu2.Instance.buttonSavePoints.Click += ButtonSavePoints_Click;
            MyMainMenu2.Instance.pnlChonDiemMatCat.Height = 54;
        }

        private void ButtonSavePoints_Click(object sender, EventArgs e)
        {
            MatCatTuTruong chartForm = activeTab.Controls[0] as MatCatTuTruong;
            CecmProgramAreaLineDTO line = chartForm.line;
            foreach (DataPoint point in chartForm.chart1.Series[1].Points)
            {
                double distance_ratio = point.XValue / chartForm.chart1.ChartAreas[0].AxisX.Maximum;
                //Phương trình đường thẳng
                //x = x0 + at
                //y = y0 + bt
                double[] utm0 = AppUtils.ConverLatLongToUTM(line.start_y, line.start_x);
                double x0 = utm0[0];
                double y0 = utm0[1];
                double[] utm1 = AppUtils.ConverLatLongToUTM(line.end_y, line.end_x);
                double x1 = utm1[0];
                double y1 = utm1[1];
                double a = x1 - x0;
                double b = y1 - y0;
                double t;
                if (a != 0)
                {
                    t = (x1 - x0) / a;
                }
                else
                {
                    t = (y1 - y0) / b;
                }
                double t_point = t * distance_ratio;
                double x_point = x0 + a * t_point;
                double y_point = y0 + b * t_point;
                double latt = 0, longt = 0;
                AppUtils.ToLatLon(x_point, y_point, ref latt, ref longt, "48N");
                MapMenuCommand.addSuspectPoint(longt, latt);
            }
            Exit();
        }

        private void ButtonCancelPoints_Click(object sender, EventArgs e)
        {
            Exit();
        }

        //private void ChartForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    e.
        //}

        private void Exit()
        {
            MatCatTuTruong chartForm = activeTab.Controls[0] as MatCatTuTruong;
            //chartForm.toolTip1.RemoveAll();
            chartForm.chart1.MouseClick -= chart1_MouseClick;
            MyMainMenu2.Instance.buttonSavePoints.Click -= ButtonSavePoints_Click;
            MyMainMenu2.Instance.buttonCancelPoints.Click -= ButtonCancelPoints_Click;
            MyMainMenu2.Instance.pnlChonDiemMatCat.Height = 0;
            chartForm.chart1.Series[1].Points.Clear();
            //chartForm.chart1.KeyDown -= chart1_KeyDown;
        }

        //private void chart1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Escape)
        //    {
        //        Exit();
        //    }
        //    else if(e.KeyCode == Keys.Enter)
        //    {
        //        MessageBox.Show("ok");
        //    }
        //}

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            Chart chart1 = (Chart)sender;
            var pos = e.Location;
            var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                    var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);

                    chart1.Series[1].Points.AddXY(xVal, 0);
                }
            }
        }
    }
}