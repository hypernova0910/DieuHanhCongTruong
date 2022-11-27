using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.PhanTich;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using Newtonsoft.Json;

namespace DieuHanhCongTruong.Command
{
    class VeMatCatTuTruongCommand
    {
        public static void Execute()
        {
            //MapMenuCommand.axMap1.SendMouseDown = true;
            MapMenuCommand.axMap1.ChooseLayer += AxMap1_ChooseLayer;
            MapMenuCommand.axMap1.Identifier.IdentifierMode = tkIdentifierMode.imSingleLayer;
            MapMenuCommand.axMap1.Identifier.HotTracking = true;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmIdentify;
            MapMenuCommand.axMap1.ShowToolTip("Chon rãnh dò", Constants.TOOLTIP_MAP_TIME);

            MapMenuCommand.axMap1.ShapeIdentified += AxMap1_ShapeIdentified;
            //MapMenuCommand.axMap1.ShapeHighlighted += AxMap1_ShapeHighlighted;

            //MyMainMenu2.Instance.KeyPreview = true;
            MyMainMenu2.Instance.KeyDown += Instance_KeyDown;
            Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(MapMenuCommand.ranhDoLayer);
            sf.Identifiable = true;
            MyMainMenu2.Instance.menuStrip1.Enabled = false;
        }

        //private static void AxMap1_ShapeHighlighted(object sender, AxMapWinGIS._DMapEvents_ShapeHighlightedEvent e)
        //{
        //    e.
        //}

        private static void Exit()
        {
            MapMenuCommand.axMap1.ChooseLayer -= AxMap1_ChooseLayer;
            MapMenuCommand.axMap1.ShapeIdentified -= AxMap1_ShapeIdentified;
            MyMainMenu2.Instance.KeyDown -= Instance_KeyDown;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmPan;
            Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(MapMenuCommand.ranhDoLayer);
            sf.Identifiable = false;
            MyMainMenu2.Instance.menuStrip1.Enabled = true;
            //MapMenuCommand.axMap1.IdentifiedShapes.Clear();
        }

        private static void AxMap1_ChooseLayer(object sender, AxMapWinGIS._DMapEvents_ChooseLayerEvent e)
        {
            e.layerHandle = MapMenuCommand.ranhDoLayer;
        }

        private static void Instance_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        private static void AxMap1_ShapeIdentified(object sender, AxMapWinGIS._DMapEvents_ShapeIdentifiedEvent e)
        {
            if(e.layerHandle == MapMenuCommand.ranhDoLayer)
            {
                Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(e.layerHandle);
                Shape shp = sf.Shape[e.shapeIndex];
                CecmProgramAreaLineDTO line = JsonConvert.DeserializeObject<CecmProgramAreaLineDTO>(shp.Key);
                Point pnt_start = shp.Point[0];
                Point pnt_end = shp.Point[1];
                Draw(pnt_start.x, pnt_start.y, pnt_end.x, pnt_end.y, line);
                
                Exit();
            }
        }

        private static void Draw(double x1, double y1, double x2, double y2, CecmProgramAreaLineDTO line)
        {
            double[] utm1 = AppUtils.ConverLatLongToUTM(y1, x1);
            double[] utm2 = AppUtils.ConverLatLongToUTM(y2, x2);

            List<Point2d> points = new List<Point2d>();
            foreach(CustomFace face in TINCommand.triangulations[line.cecmprogramareamap_id.Value])
            {
                List<InfoConnect> giaoDiems = GetGiaoTamGiacMatPhang(face, utm1[0], utm1[1], utm2[0], utm2[1]);
                foreach(InfoConnect giaoDiem in giaoDiems)
                {
                    Point2d point = new Point2d();
                    point.Y = giaoDiem.the_value;
                    double distance_giao = AppUtils.DistanceUTM(giaoDiem.lat_value, giaoDiem.long_value, utm1[0], utm1[1]);
                    double distance_ranh = AppUtils.DistanceUTM(utm2[0], utm2[1], utm1[0], utm1[1]);
                    point.X = distance_giao / distance_ranh * 100;
                    points.Add(point);
                }
            }
            points = points.OrderBy(point => point.X).ToList();
            MatCatTuTruong form = new MatCatTuTruong(line);
            form.DrawLine(points);
            //form.DrawLineTest();
            TabPage tabPage = new TabPage();
            tabPage.Text = "Rãnh " + line.code + "    ";
            tabPage.Controls.Add(form);
            if(MyMainMenu2.Instance.tabCtrlLineChart.TabPages.Count == 0)
            {
                MyMainMenu2.Instance.tabCtrlLineChart.Height = 400;
            }
            MyMainMenu2.Instance.tabCtrlLineChart.TabPages.Add(tabPage);
            MyMainMenu2.Instance.tabCtrlLineChart.SelectedIndex = MyMainMenu2.Instance.tabCtrlLineChart.TabPages.IndexOf(tabPage);
        }

        //Tìm giao bề mặt từ trường qua mp qua 2 điểm và vuông góc với Oxy
        private static List<InfoConnect> GetGiaoTamGiacMatPhang(CustomFace cell, double x1, double y1, double x2, double y2)
        {
            //Tìm vector chỉ phương AB, AC
            double vtcp1_x = x1 - x2;
            double vtcp1_y = y1 - y2;
            double vtcp1_z = 0;
            double vtcp2_x = 0;
            double vtcp2_y = 0;
            double vtcp2_z = 1;
            //Tìm vector pháp tuyến
            double vtpt_x = vtcp1_y * vtcp2_z - vtcp2_y * vtcp1_z;  //a
            double vtpt_y = vtcp1_z * vtcp2_x - vtcp2_z * vtcp1_x;  //b
            double vtpt_z = vtcp1_x * vtcp2_y - vtcp2_x * vtcp1_y;  //c
            //Phương trình mặt phẳng a(x - x0) + b(y - y0) + c(z - z0) = 0
            //Chuyển về ax + by + cz = ax0 + by0 + cz0 = heSoTuDo
            double heSoTuDo = vtpt_x * x1 + vtpt_y * y1 + vtpt_z * 0;
            List<InfoConnect> polygon_temp = new List<InfoConnect>();
            InfoConnect A = GeometryUtils.GiaoDuongThangMatPhang(cell.Vertices[0], cell.Vertices[1], vtpt_x, vtpt_y, vtpt_z, heSoTuDo, true);
            if (A != null)
            {
                polygon_temp.Add(A);
            }
            InfoConnect B = GeometryUtils.GiaoDuongThangMatPhang(cell.Vertices[1], cell.Vertices[2], vtpt_x, vtpt_y, vtpt_z, heSoTuDo, true);
            if (B != null)
            {
                polygon_temp.Add(B);
            }
            InfoConnect C = GeometryUtils.GiaoDuongThangMatPhang(cell.Vertices[2], cell.Vertices[0], vtpt_x, vtpt_y, vtpt_z, heSoTuDo, true);
            if (C != null)
            {
                polygon_temp.Add(C);
            }
            return polygon_temp;
        }

        public static InfoConnect GiaoDoanThangMatPhang(InfoConnect M0, InfoConnect N0, double vtpt_x, double vtpt_y, double vtpt_z, double heSoTuDo)
        {
            //Check đoạn thẳng có cắt mặt phẳng không
            if ((vtpt_x * M0.lat_value + vtpt_y * M0.long_value + vtpt_z * M0.the_value - heSoTuDo) * (vtpt_x * N0.lat_value + vtpt_y * N0.long_value + vtpt_z * N0.the_value - heSoTuDo) > 0)
            {
                return null;
            }
            //Phương trình đường thẳng đi qua điểm M(x0, y0, z0) và nhận vectơ u(a,b,c) làm vector chỉ phương
            //x = x0 + at
            //y = y0 + bt
            //z = z0 + ct
            //Với x0 = M0.Position[0], y0 = M0.Position[1], z0 = M0.Position[2]
            double x0 = M0.lat_value;
            double y0 = M0.long_value;
            double z0 = M0.the_value;
            double a = N0.lat_value - M0.lat_value;
            double b = N0.long_value - M0.long_value;
            double c = N0.the_value - M0.the_value;
            //Mặt phẳng có phương trình vtpt_x * x + vtpt_y * y + vtpt_z * z = heSoTuDo
            //=> Tìm t từ phương trình vtpt_x * (x0 + at) + vtpt_y * (y0 + bt) + vtpt_z * (z0 + ct) = heSoTuDo
            //<=> vtpt_x * at + vtpt_y * bt + vtpt_z * ct = heSoTuDo - vtpt_x * x0 - vtpt_y * y0 - vtpt_z * z0
            //<=> t = (heSoTuDo - vtpt_x * x0 - vtpt_y * y0 - vtpt_z * z0) / (vtpt_x * a + vtpt_y * b + vtpt_z * c)
            if(vtpt_x * a + vtpt_y * b + vtpt_z * c == 0)
            {
                return null;
            }
            double t = (heSoTuDo - vtpt_x * x0 - vtpt_y * y0 - vtpt_z * z0) / (vtpt_x * a + vtpt_y * b + vtpt_z * c);
            double x = x0 + a * t;
            double y = y0 + b * t;
            double z = z0 + c * t;
            InfoConnect infoConnect = new InfoConnect();
            infoConnect.lat_value = x;
            infoConnect.long_value = y;
            infoConnect.the_value = z;
            return infoConnect;
        }
    }
}
