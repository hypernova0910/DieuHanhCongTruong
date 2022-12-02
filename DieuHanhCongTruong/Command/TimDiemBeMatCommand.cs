using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;

namespace DieuHanhCongTruong.Command
{
    class TimDiemBeMatCommand
    {
        public static void Execute()
        {
            //MapMenuCommand.axMap1.ChooseLayer += AxMap1_ChooseLayer;
            //MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmAddShape;
            //ShapeEditor se = MapMenuCommand.axMap1.ShapeEditor;
            //se.
            MapMenuCommand.axMap1.SendMouseDown = true;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmNone;
            MapMenuCommand.axMap1.MouseDownEvent += AxMap1_MouseDownEvent;
            MyMainMenu2.Instance.KeyDown += Instance_KeyDown;
            MyMainMenu2.Instance.menuStrip1.Enabled = false;
            MyMainMenu2.Instance.pnlToolBar.Enabled = false;
        }

        private static void Instance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        private static void AxMap1_MouseDownEvent(object sender, AxMapWinGIS._DMapEvents_MouseDownEvent e)
        {
            if (e.button == 1)          // left button
            {
                bool isBomb = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayers[0]);
                CecmReportPollutionAreaBMVN point = new CecmReportPollutionAreaBMVN();
                double latt = 0, longt = 0;
                MapMenuCommand.axMap1.PixelToProj(e.x, e.y, ref longt, ref latt);
                point.Kinhdo = longt;
                point.Vido = latt;
                point.TimeExecute = DateTime.Now;
                double[] utm = AppUtils.ConverLatLongToUTM(latt, longt);
                point.XPoint = utm[0];
                point.YPoint = utm[1];
                point.UserAdd = true;
                Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(MapMenuCommand.polygonAreaLayer);

                Point pntMap = new Point();
                pntMap.x = longt;
                pntMap.y = latt;
                long idKV = -1;
                for(int i = 0; i < sf.NumShapes; i++)
                {
                    Shape shape = sf.Shape[i];
                    
                    if (shape.PointInThisPoly(pntMap))
                    {
                        idKV = long.Parse(shape.Key);
                        break;
                    }
                }
                if(idKV > 0)
                {
                    List<CustomFace> triangles;
                    if (isBomb)
                    {
                        triangles = TINCommand.triangulations_bomb[idKV];
                    }
                    else
                    {
                        triangles = TINCommand.triangulations_mine[idKV];
                    }
                    point.idArea = idKV;
                    point.ZPoint = GeometryUtils.GetMagneticAtPoint(point.XPoint, point.YPoint, triangles);
                    List<InfoConnect> contourGiamNghiNgo = new List<InfoConnect>();
                    double area = PhanTichKhoangGiamNghiNgoCommand.FindArea(point, 50, triangles, ref contourGiamNghiNgo, true, false);
                    point.Area = area;
                    point.contour = contourGiamNghiNgo;
                    if (contourGiamNghiNgo.Count > 2)
                    {
                        PhanTichKhoangGiamNghiNgoCommand.FindDeep(point);
                    }
                    else
                    {
                        point.Deep = 0;
                    }
                }
                else
                {
                    point.idArea = -1;
                    point.ZPoint = 0;
                    point.Area = 0;
                    point.contour = new List<InfoConnect>();
                    point.Deep = 0;
                }
                MapMenuCommand.addSuspectPoint(longt, latt, point, isBomb, true);
            }
        }

        private static void Exit()
        {
            MapMenuCommand.axMap1.SendMouseDown = false;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmPan;
            MapMenuCommand.axMap1.MouseDownEvent -= AxMap1_MouseDownEvent;
            MyMainMenu2.Instance.KeyDown -= Instance_KeyDown;
            MyMainMenu2.Instance.menuStrip1.Enabled = true;
            MyMainMenu2.Instance.pnlToolBar.Enabled = true;
        }
    }
}
