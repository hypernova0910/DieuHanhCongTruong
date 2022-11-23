using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using MIConvexHull;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin;
using VNRaPaBomMin.Models;

namespace DieuHanhCongTruong.Command
{
    class PhanTichKhoangGiamNghiNgoCommand
    {
        public static void Execute()
        {
            //MapMenuCommand.axMap1.SendMouseDown = true;
            MapMenuCommand.axMap1.ChooseLayer += AxMap1_ChooseLayer;
            MapMenuCommand.axMap1.Identifier.IdentifierMode = tkIdentifierMode.imSingleLayer;
            MapMenuCommand.axMap1.Identifier.HotTracking = true;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmIdentify;
            MapMenuCommand.axMap1.ShowToolTip("Chon đường bao dự án", 3000);

            MapMenuCommand.axMap1.ShapeIdentified += AxMap1_ShapeIdentified;
            //MapMenuCommand.axMap1.ShapeHighlighted += AxMap1_ShapeHighlighted;

            //MyMainMenu2.Instance.KeyPreview = true;
            MyMainMenu2.Instance.KeyDown += Instance_KeyDown;
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
            MapMenuCommand.axMap1.IdentifiedShapes.Clear();
            //MapMenuCommand.axMap1.ShowToolTip("", 0);
            //MyMainMenu2.Instance.KeyPreview = false;
        }

        private static void AxMap1_ChooseLayer(object sender, AxMapWinGIS._DMapEvents_ChooseLayerEvent e)
        {
            e.layerHandle = MapMenuCommand.polygonLayer;
        }

        private static void Instance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        private static void AxMap1_ShapeIdentified(object sender, AxMapWinGIS._DMapEvents_ShapeIdentifiedEvent e)
        {
            if (e.layerHandle == MapMenuCommand.polygonLayer)
            {
                Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(e.layerHandle);
                Shape shp = sf.Shape[e.shapeIndex];
                Shapefile sf_bomb = MapMenuCommand.axMap1.get_Shapefile(MapMenuCommand.suspectPointLayer);
                long.TryParse(shp.Key, out long idKV);
                if (TINCommand.triangulations.ContainsKey(idKV))
                {
                    List<CustomFace> triangle = TINCommand.triangulations[idKV];
                    List<CecmReportPollutionAreaBMVN> lstBMVN = new List<CecmReportPollutionAreaBMVN>();
                    for (int i = 0; i < sf_bomb.NumShapes; i++)
                    {
                        Shape shapeBomb = sf_bomb.Shape[i];
                        Point pnt = shapeBomb.Point[0];
                        if (shp.PointInThisPoly(pnt))
                        {
                            CecmReportPollutionAreaBMVN bmvn = JsonConvert.DeserializeObject<CecmReportPollutionAreaBMVN>(shapeBomb.Key);
                            bmvn.idArea = idKV;
                            bmvn.programId = MyMainMenu2.idDADH;
                            List<InfoConnect> contour = new List<InfoConnect>();
                            double area = FindArea(bmvn, 50, triangle, ref contour);
                            bmvn.Area = area;
                            bmvn.contour = contour;
                            if(contour.Count > 2)
                            {
                                FindDeep(bmvn);
                            }
                            else
                            {
                                bmvn.Deep = 0;
                            }
                            lstBMVN.Add(bmvn);
                        }
                    }
                    KhoangNghiNgoForm form = new KhoangNghiNgoForm(lstBMVN);
                    form.Show();
                }

                //CecmProgramAreaLineDTO line = JsonConvert.DeserializeObject<CecmProgramAreaLineDTO>(shp.Key);
                //Point pnt_start = shp.Point[0];
                //Point pnt_end = shp.Point[1];
                //Draw(pnt_start.x, pnt_start.y, pnt_end.x, pnt_end.y, line);

                Exit();
            }
        }

        private static double FindArea(CecmReportPollutionAreaBMVN bmvn, double KGNN, List<CustomFace> triangles, ref List<InfoConnect> contourHasMine)
        {
            //if (double.IsNaN(item.Position.Z))
            //    continue;

            //if (isOverwrite == false)
            //{
            //    if (item.PolygonLinkId.IsValid || item.Deep > 0 || item.Area > 0)
            //        continue;
            //}

            //ObjectIdCollection oidCollContourLine = surfaceTerran.ExtractContoursAt(item.Position.Z - (item.Position.Z * dVal) / 100);
            //List<ObjectId> lOiPlineHasMine = new List<ObjectId>();
            //double areaPline = double.NaN;
            double x = bmvn.XPoint;
            double y = bmvn.YPoint;
            double z = bmvn.ZPoint;
            if (triangles == null)
            {
                return 0;
            }
            AdjacentTriangles cmd = new AdjacentTriangles(triangles);
            cmd.GetSuspectPoints(z - z * KGNN / 100);
            double areaPline = 0;

            foreach (List<InfoConnect> contour in cmd.lstContour)
            {
                //if (oid.IsValid == false)
                //    continue;

                //Polyline pline = oid.GetObject(OpenMode.ForWrite) as Polyline;
                //if (pline == null)
                //    continue;

                //if (oidLayer.IsValid)
                //    pline.LayerId = oidLayer;

                //if (pline.NumberOfVertices <= 2)
                //{
                //    pline.Erase();
                //    continue;
                //}
                //isClose = false;

                //if (pline.Closed == false)
                //{
                //    pline.Closed = true;
                //    isClose = true;
                //}

                //if (pPolygonArx.Create(oid, ">@]x3X~9msjW.)g") == false)
                //    continue;

                //InPolyTypeMgd typeMgd = InPolyTypeMgd.INPOLY_MGD_ERROR;
                //typeMgd = pPolygonArx.PointInPolygon(new Point2d(item.Position.X, item.Position.Y));

                //if (typeMgd == InPolyTypeMgd.INPOLY_MGD_INSIDE || typeMgd == InPolyTypeMgd.INPOLY_MGD_VERTEX || typeMgd == InPolyTypeMgd.INPOLY_MGD_EDGE)
                //{
                //    lOiPlineHasMine.Add(oid);

                //    // Chi lay cai dau tien
                //    if (areaPline != double.NaN)
                //        areaPline = pline.Area;
                //}
                //else
                //{
                //    pline.Erase();
                //}

                //if (isClose)
                //    pline.Closed = false;

                //pPolygonArx.ClearAll();


                //double longMin = double.MaxValue;
                //double latMin = double.MaxValue;
                //double longMax = double.MinValue;
                //double latMax = double.MinValue;
                //foreach(InfoConnect infoConnect in contour)
                //{
                //    if(infoConnect.long_value < longMin)
                //    {
                //        longMin = infoConnect.long_value;
                //    }
                //    if (infoConnect.lat_value < latMin)
                //    {
                //        latMin = infoConnect.lat_value;
                //    }
                //    if (infoConnect.long_value > longMax)
                //    {
                //        longMax = infoConnect.long_value;
                //    }
                //    if (infoConnect.lat_value > latMax)
                //    {
                //        latMax = infoConnect.lat_value;
                //    }
                //}
                //if(x < latMax && x > latMin && y < longMax && y > longMin)
                //{
                //var convexHull = ConvexHull.Create2D(contour);
                //if (convexHull.Outcome == ConvexHullCreationResultOutcome.Success)
                //{
                //    var result = convexHull.Result
                //    Shape shp = new Shape();
                //    shp.Create(ShpfileType.SHP_POLYGON);

                //}
                //MapMenuCommand.drawPolygonTest(contour);
                Shape shpPolygon = new Shape();
                shpPolygon.Create(ShpfileType.SHP_POLYGON);
                for (int i = 0; i < contour.Count; i++)
                {
                    InfoConnect infoConnect = contour[i];
                    Point vertexPolygon = new Point();
                    Point2d point2D = AppUtils.ConverUTMToLatLong(infoConnect.lat_value, infoConnect.long_value);
                    vertexPolygon.x = point2D.X;
                    vertexPolygon.y = point2D.Y;
                    //pnt.x = infoConnect.lat_value;
                    //pnt.y = infoConnect.long_value;
                    shpPolygon.InsertPoint(vertexPolygon, ref i);
                }
                Point2d point_temp = AppUtils.ConverUTMToLatLong(x, y);
                Point point = new Point();
                point.x = point_temp.X;
                point.y = point_temp.Y;
                if (shpPolygon.PointInThisPoly(point))
                {
                    contourHasMine = contour;
                    return AreaPolygon(contour);
                }
                
                //}
            }
            return 0;
            //if (item.IsWriteEnabled == false)
            //    item.UpgradeOpen();

            //if (lOiPlineHasMine.Count != 0)
            //{
            //    item.PolygonLinkId = lOiPlineHasMine.FirstOrDefault();
            //    item.Area = areaPline;
            //}
        }

        private static double AreaPolygon(List<InfoConnect> polygon)
        {
            if(polygon.Count < 3)
            {
                return 0;
            }
            polygon.Add(polygon[0]);
            var area = Math.Abs(polygon.Take(polygon.Count - 1)
               .Select((p, i) => (polygon[i + 1].lat_value - p.lat_value) * (polygon[i + 1].long_value + p.long_value))
               .Sum() / 2);
            return area;
        }

        private static InfoLine FindDeep(CecmReportPollutionAreaBMVN bmvn)
        {
            List<InfoConnect> contour = bmvn.contour;
            List<InfoConnect> intersections = new List<InfoConnect>();
            for (int i = 0; i < contour.Count - 1; i++)
            {
                InfoConnect intersection = Giao2DuongThang2D(bmvn.XPoint, bmvn.YPoint, contour[i].lat_value, contour[i].long_value, contour[i + 1].lat_value, contour[i + 1].long_value);
                if(intersection != null)
                {
                    intersections.Add(intersection);
                }
            }
            InfoConnect intersectionLast = Giao2DuongThang2D(bmvn.XPoint, bmvn.YPoint, contour[contour.Count - 1].lat_value, contour[contour.Count - 1].long_value, contour[0].lat_value, contour[0].long_value);
            if (intersectionLast != null)
            {
                intersections.Add(intersectionLast);
            }
            if (intersections.Count < 2)
            {
                return null;
            }
            intersections = intersections.OrderBy(point => point.long_value).ToList();
            InfoLine line = new InfoLine();
            line.start = intersections.FirstOrDefault();
            line.end = intersections.LastOrDefault();
            bmvn.Deep = AppUtils.DistanceUTM(line.start.lat_value, line.start.long_value, line.end.lat_value, line.end.long_value);
            return line;
        }

        public static InfoConnect Giao2DuongThang2D(double Ax, double Ay, double Cx, double Cy, double Dx, double Dy)
        {
            if((Ax - Cx) * (Ax - Dx) >= 0)
            {
                return null;
            }
            //double vtcp1_x = 0;
            //double vtcp1_y = 1;
            //double vtpt1_x = 1;
            //double vtpt1_y = 0;
            double vtcp2_x = Dx - Cx;
            double vtcp2_y = Dy - Cy;
            double vtpt2_x = -vtcp2_y;
            double vtpt2_y = vtcp2_x;
            //Phương trình đường thẳng 1(song song Oy)
            //x = Ax
            //Phương trình đường thẳng 2
            //vtpt2_x * (x - Cx) + vtpt2_y * (y - Cy) = 0
            //=> vtpt2_x * x + vtpt2_y * y = vtpt2_x * Cx + vtpt2_y * Cy
            double heSoTuDo = vtpt2_x * Cx + vtpt2_y * Cy;
            InfoConnect intersect = GeometryUtils.Giao2DuongThang2D(1, 0, Ax, vtpt2_x, vtpt2_y, heSoTuDo);
            return intersect;
        }
    }
}
