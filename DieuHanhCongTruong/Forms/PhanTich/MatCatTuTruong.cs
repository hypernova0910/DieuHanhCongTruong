using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DieuHanhCongTruong.Forms.PhanTich
{
    public partial class MatCatTuTruong : UserControl
    {
        public static bool isPointing = false;
        public CecmProgramAreaLineDTO line;

        public MatCatTuTruong(CecmProgramAreaLineDTO line)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.line = line;
        }

        public void DrawLine(List<Point2d> lst)
        {
            foreach (Point2d point in lst)
            {
                chart1.Series[0].Points.AddXY(point.X, point.Y);
            }
        }

        public void DrawLineTest()
        {
            chart1.Series[0].Points.AddXY(0, 0);
            chart1.Series[0].Points.AddXY(25, 25);
            chart1.Series[0].Points.AddXY(30, 17);
            chart1.Series[0].Points.AddXY(50, 123);
            chart1.Series[0].Points.AddXY(60, -77);
            chart1.Series[0].Points.AddXY(75.7, 100);
            chart1.Series[0].Points.AddXY(90, -123);
            chart1.Series[0].Points.AddXY(100, 1123);
        }
    }
}
