using AxMapWinGIS;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;
using Image = MapWinGIS.Image;
using Point = MapWinGIS.Point;

namespace DieuHanhCongTruong.Command
{
    class MapMenuCommand
    {
        public static AxMap axMap1 = MyMainMenu2.Instance.axMap1;

        public static List<int> machinePointBombLayers = new List<int>();
        public static List<int> machinePointMineLayers = new List<int>();
        private static List<int> machinePointBombModelLayers = new List<int>();
        private static List<int> machinePointMineModelLayers = new List<int>();
        private static List<int> machinePointModelHistoryLayers = new List<int>();
        private static List<int> machinePointRealTimeLayers = new List<int>();
        private static List<int> machinePointRealTimeModelLayers = new List<int>();

        public static List<int> machineLineBombLayers = new List<int>();
        public static List<int> machineLineMineLayers = new List<int>();
        private static List<int> machineLineBombModelLayers = new List<int>();
        private static List<int> machineLineMineModelLayers = new List<int>();
        private static List<int> machineLineModelHistoryLayers = new List<int>();
        private static List<int> machineLineRealTimeLayers = new List<int>();
        private static List<int> machineLineRealTimeModelLayers = new List<int>();

        public static int polygonAreaLayer = -1;
        private static int polygonLayerTest = -1;
        public static List<int> polygonLayers = new List<int>();
        public static List<int> polygonLayersMine = new List<int>();
        public static int oluoiLayer = -1;
        public static int suspectPointLayerBomb = -1;
        public static int suspectPointLayerMine = -1;
        public static int userSuspectPointLayerBomb = -1;
        public static int userSuspectPointLayerMine = -1;
        private static int flagLayer = -1;
        private static int flagRealTimeLayer = -1;
        public static List<int> imageLayers = new List<int>();
        private static int deepLayer = -1;
        private static int pointLayer = -1;
        private static int greenFlagLayer = -1;
        private static int machineLayer = -1;
        private static int bgLayer = -1;
        private static int lineLayer = -1;
        public static int labelLayer = -1;
        private static int distanceLayer = -1;
        private static int markerDistanceLayer = -1;
        private static int highlightLayer = -1;
        private static int highlightCurrentPointLayer = -1;
        private static int highlightCurrentPointModelLayer = -1;
        public static int ranhDoLayer = -1;
        //private static List<int> polygonLayers = new List<int>();

        //private static Dictionary<string, Color> machineActive__color = new Dictionary<string, Color>();
        private static Dictionary<string, int> machineActive__pointLayer = new Dictionary<string, int>();
        private static Dictionary<string, int> machineActive__pointModelLayer = new Dictionary<string, int>();
        private static Dictionary<string, Point> machineActive__lastPoint = new Dictionary<string, Point>();
        private static Dictionary<string, int> machineActive__lineLayer = new Dictionary<string, int>();
        private static Dictionary<string, int> machineActive__lineModelLayer = new Dictionary<string, int>();
        private static Dictionary<string, DateTime> machineActive__lastTime = new Dictionary<string, DateTime>();
        public static Dictionary<long, List<Shape>> idKV__shapeOLuoi = new Dictionary<long, List<Shape>>();

        public static Dictionary<long, int> idKV__shapeIndex = new Dictionary<long, int>();

        private static int MIN_TIME_NEW_LINE = 90;
        private static double MIN_DISTANCE_NEW_LINE = 7;

        private static Color[] activeMachineColor;

        //private static string[] machine_colors = {
        //    "#A2231D",
        //    "#2271B3",
        //    "#D0D0D0",
        //    "#2C5545",
        //    "#A03472",
        //    "#A18594",
        //    "#898176",
        //    "#F3DA0B",
        //    "#955F20",
        //    "#A98307",
        //    "#49678D",
        //    "#FFA420",
        //    "#F5D033",
        //    "#89AC76",
        //    "#212121",
        //    "#4C9141",
        //    "#8A6642",
        //    "#6C7059",
        //};

        public static void LoadMap()
        {
            activeMachineColor = Constants.MACHINE_COLORS.Select(color => ColorTranslator.FromHtml(color)).ToArray();
            initPolygonAreaLayer();
            initPolygonLayer(true, false);
            InitPointImageLayer(true, false);
            InitPointImageLayer(false, false);
            InitPointImageLayer(true, true);
            InitPointImageLayer(false, true);
            InitLineLayer(true, false);
            InitLineLayer(false, false);
            InitLineLayer(true, true);
            InitLineLayer(false, true);
            InitSuspectPointLayer();
            InitSuspectPointMineLayer();
            InitUserSuspectPointLayer();
            InitUserSuspectPointMineLayer();
            //InitFlagLayer();
            //InitFlagRealTimeLayer();
            //InitDeepLayer();
            //InitGreenFlagLayer();
            //InitMachineLayer();
            //InitDistancePointLayer();
            InitHighlightLayer();
            //InitHighlightCurrentPointLayer();
            //InitHighlightCurrentPointModelLayer();
            lineLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            labelLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //tooltipLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //markerDistanceLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            distanceLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            InitRanhDoLayer();
            InitOLuoiLayer();
            initPolygonTestLayer();

        }

        public static void LoadImage(string path, double xmax, double xmin, double ymax, double ymin)
        {
            MapWinGIS.Image img = AppUtils.OpenImage(path);
            //img.Open(path, ImageType.USE_FILE_EXTENSION);
            //Image img = OpenImage(path);
            img.OriginalDX = (xmax - xmin) / img.OriginalWidth;
            img.OriginalDY = (ymax - ymin) / img.OriginalHeight;
            //MessageBox.Show("width = " + img.OriginalDX + " / height = " + img.OriginalDY);
            //img.ProjectionToImage(xmin, ymin, out int x, out int y);
            //img.SetVisibleExtents(xmin, ymin, xmax, ymax, 500, 10);
            img.OriginalXllCenter = xmin;
            img.OriginalYllCenter = ymin;
            int imageLayer = axMap1.AddLayer(img, true);
            axMap1.MoveLayerBottom(imageLayer);
            imageLayers.Add(imageLayer);
        }

        //private static void InitImageLayer()
        //{

        //    double xmin = 106.828998;
        //    double xmax = 106.844626;
        //    double ymin = 17.261828;
        //    double ymax = 17.27483;

        //    //double xminUTM = 1909441.791;
        //    //double xmaxUTM = 1910895.457;
        //    //double yminUTM = 696088.270;
        //    //double ymaxUTM = 694439.028;

        //    var pathpng = AppUtils.GetAppDataPath();
        //    pathpng = System.IO.Path.Combine(pathpng, "11111111.png");

        //    MapWinGIS.Image img = AppUtils.OpenImage(pathpng);
        //    img.OriginalDX = (xmax - xmin) / img.OriginalWidth;
        //    img.OriginalDY = (ymax - ymin) / img.OriginalHeight;
        //    //MessageBox.Show("width = " + img.OriginalDX + " / height = " + img.OriginalDY);
        //    //img.ProjectionToImage(xmin, ymin, out int x, out int y);
        //    //img.SetVisibleExtents(xmin, ymin, xmax, ymax, 500, 10);
        //    img.OriginalXllCenter = xmin;
        //    img.OriginalYllCenter = ymin;

        //    imageLayer = axMap1.AddLayer(img, true);

        //    axMap1.Redraw();
        //}

        //public static void drawPolygon(double[] xPoints, double[] yPoints, Color color, bool fill)
        //{
        //    if (xPoints.Length != yPoints.Length)
        //    {
        //        MessageBox.Show("Invalid Polygon Coordinates");
        //        return;
        //    }
        //    //axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        //    object X = xPoints;
        //    object Y = yPoints;
        //    int size = xPoints.Length;
        //    axMap1.DrawPolygonEx(polygonLayer, ref X, ref Y, size, AppUtils.ColorToUint(color), fill);
        //    //axMap1.Redraw();
        //}

        public static void drawPolygon(int colorIndex, double[] xPoints, double[] yPoints, bool isBomb)
        {
            //if (xPoints.Length != yPoints.Length)
            //{
            //    MessageBox.Show("Invalid Polygon Coordinates");
            //    return;
            //}
            ////axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //object X = xPoints;
            //object Y = yPoints;
            //int size = xPoints.Length;
            //if (isBomb)
            //{
            //    axMap1.DrawPolygonEx(polygonLayer, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
            //}
            //else
            //{
            //    axMap1.DrawPolygonEx(polygonLayerMine, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
            //}
            Shapefile sf;
            if (isBomb)
            {
                //sf = axMap1.get_Shapefile(polygonLayer);
                sf = axMap1.get_Shapefile(polygonLayers[colorIndex]);
            }
            else
            {
                //sf = axMap1.get_Shapefile(polygonLayerMine);
                sf = axMap1.get_Shapefile(polygonLayersMine[colorIndex]);
            }
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POLYGON);
            for (int j = 0; j < xPoints.Length; j++)
            {
                Point pnt = new Point();
                pnt.x = xPoints[j];
                pnt.y = yPoints[j];
                shp.InsertPoint(pnt, ref j);
            }
            int index = sf.NumShapes;
            if (!sf.EditInsertShape(shp, ref index))
            {
                MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("drawPolygon()");
                return;
            }
        }

        public static void drawPolygonArea(PointF[] polygon, long idKV)
        {
            //if (xPoints.Length != yPoints.Length)
            //{
            //    MessageBox.Show("Invalid Polygon Coordinates");
            //    return;
            //}
            ////axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //object X = xPoints;
            //object Y = yPoints;
            //int size = xPoints.Length;
            //if (isBomb)
            //{
            //    axMap1.DrawPolygonEx(polygonLayer, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
            //}
            //else
            //{
            //    axMap1.DrawPolygonEx(polygonLayerMine, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
            //}
            Shapefile sf = axMap1.get_Shapefile(polygonAreaLayer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POLYGON);
            for (int j = 0; j < polygon.Length; j++)
            {
                Point pnt = new Point();
                pnt.x = polygon[j].X;
                pnt.y = polygon[j].Y;
                shp.InsertPoint(pnt, ref j);
            }
            shp.Key = idKV.ToString();
            int index = sf.NumShapes;
            if (!sf.EditInsertShape(shp, ref index))
            {
                MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("drawPolygon()");
                return;
            }
            if (idKV__shapeIndex.ContainsKey(idKV))
            {
                idKV__shapeIndex[idKV] = index;
            }
            else
            {
                idKV__shapeIndex.Add(idKV, index);
            }
        }

        //public static void AddLabelInfo(string text, double x, double y)
        //{
        //    //if (xPoints.Length != yPoints.Length)
        //    //{
        //    //    MessageBox.Show("Invalid Polygon Coordinates");
        //    //    return;
        //    //}
        //    ////axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        //    //object X = xPoints;
        //    //object Y = yPoints;
        //    //int size = xPoints.Length;
        //    //if (isBomb)
        //    //{
        //    //    axMap1.DrawPolygonEx(polygonLayer, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
        //    //}
        //    //else
        //    //{
        //    //    axMap1.DrawPolygonEx(polygonLayerMine, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
        //    //}
        //    Shapefile sf = axMap1.get_Shapefile(polygonAreaLayer);
        //    Labels labels = sf.Labels;
        //    LabelCategory labelCategory = labels.AddCategory("Transparent");
        //    labelCategory.FrameVisible = false;
        //    labelCategory.FontColor = AppUtils.ColorToUint(Color.Red);
        //    labelCategory.FontSize = 12;
        //    labels.ApplyCategories();
        //    labels.AddLabel(text, bottom + margin / 5 * 2, left);
        //    MapMenuCommand.axMap1.set_DrawingLabels(MapMenuCommand.labelLayer, labels);
        //}

        public static void drawPolygonTest(List<InfoConnect> polygon)
        {
            //if (xPoints.Length != yPoints.Length)
            //{
            //    MessageBox.Show("Invalid Polygon Coordinates");
            //    return;
            //}
            ////axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //object X = xPoints;
            //object Y = yPoints;
            //int size = xPoints.Length;
            //if (isBomb)
            //{
            //    axMap1.DrawPolygonEx(polygonLayer, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
            //}
            //else
            //{
            //    axMap1.DrawPolygonEx(polygonLayerMine, ref X, ref Y, size, AppUtils.ColorToUint(magnetic_colors[colorIndex]), true);
            //}
            Shapefile sf = axMap1.get_Shapefile(polygonLayerTest);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POLYGON);
            for (int j = 0; j < polygon.Count; j++)
            {
                Point pnt = new Point();
                Point2d point2D = AppUtils.ConverUTMToLatLong(polygon[j].lat_value, polygon[j].long_value);
                pnt.x = point2D.X;
                pnt.y = point2D.Y;
                shp.InsertPoint(pnt, ref j);
            }
            int index = sf.NumShapes;
            if (!sf.EditInsertShape(shp, ref index))
            {
                MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("drawPolygon()");
                return;
            }
        }

        private static void InitPointImageLayer(bool isBomb, bool model)
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                Shapefile sf = new Shapefile();
                //sf.Open(filename, null);
                int layer = axMap1.AddLayer(sf, isBomb);
                sf = axMap1.get_Shapefile(layer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
                sf = new Shapefile();
                if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                {
                    MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("InitPointImageLayer()");
                    return;
                }
                sf.Identifiable = false;
                layer = axMap1.AddLayer(sf, isBomb);
                if (isBomb)
                {
                    if (model)
                    {
                        machinePointBombModelLayers.Add(layer);
                    }
                    else
                    {
                        machinePointBombLayers.Add(layer);
                    }
                    
                }
                else
                {
                    if (model)
                    {
                        machinePointMineModelLayers.Add(layer);
                    }
                    else
                    {
                        machinePointMineLayers.Add(layer);
                    }
                    
                }
                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.PointType = tkPointSymbolType.ptSymbolPicture;
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), Constants.ICON_FOLDER, color + ".png");
                options.Picture = AppUtils.OpenImage(pathpng);
                options.AlignPictureByBottom = false;


                sf.CollisionMode = tkCollisionMode.AllowCollisions;
            }
        }

        private static void InitLineLayer(bool isBomb, bool model)
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                var sf = new Shapefile();
                sf.CreateNew("", ShpfileType.SHP_POLYLINE);
                //Shape shp = new Shape();
                //shp.Create(ShpfileType.SHP_POLYLINE);
                //int index = sf.NumShapes;
                //sf.EditInsertShape(shp, ref index);
                //var sf = CreateLines();
                sf.Identifiable = false;
                int layer = axMap1.AddLayer(sf, isBomb);
                if (isBomb)
                {
                    if (model)
                    {
                        machineLineBombModelLayers.Add(layer);
                    }
                    else
                    {
                        machineLineBombLayers.Add(layer);
                    }
                    
                }
                else
                {
                    if (model)
                    {
                        machineLineMineModelLayers.Add(layer);
                    }
                    else
                    {
                        machineLineMineLayers.Add(layer);
                    }
                    
                }

                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(AppUtils.ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = AppUtils.ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private static void InitRanhDoLayer()
        {
            var sf = new Shapefile();
            sf.CreateNew("", ShpfileType.SHP_POLYLINE);
            //Shape shp = new Shape();
            //shp.Create(ShpfileType.SHP_POLYLINE);
            //int index = sf.NumShapes;
            //sf.EditInsertShape(shp, ref index);
            //var sf = CreateLines();
            sf.Identifiable = false;
            ranhDoLayer = axMap1.AddLayer(sf, true);

            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.LineColor = AppUtils.ColorToUint(Color.White);
            options.LineWidth = 2;
            
            //labelCategory.FrameTransparency = 0;

            //MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
            //pattern.AddLine(AppUtils.ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
            //LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
            //segm.Color = AppUtils.ColorToUint(Color.Black);
            //segm.MarkerSize = 15;
            //segm.MarkerInterval = 32;
            //segm.MarkerIntervalIsRelative = false;
            //segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
            //options.LinePattern = pattern;
            //options.UseLinePattern = true;
        }

        private static void InitOLuoiLayer()
        {
            Shapefile sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitOLuoiLayer()");
                return;
            }
            sf.DefaultDrawingOptions.LineColor = AppUtils.ColorToUint(Color.White);
            sf.DefaultDrawingOptions.LineWidth = 5;
            sf.Identifiable = false;
            oluoiLayer = axMap1.AddLayer(sf, true);
            axMap1.set_ShapeLayerDrawFill(oluoiLayer, false);
        }

        private static void InitPointImageModelLayer()
        {

            foreach (string color in Constants.MACHINE_COLORS)
            {
                Shapefile sf = new Shapefile();
                //sf.Open(filename, null);
                int layer = axMap1.AddLayer(sf, true);
                sf = axMap1.get_Shapefile(layer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
                sf = new Shapefile();
                if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                {
                    MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("InitPointImageModelLayer()");
                    return;
                }
                layer = axMap1.AddLayer(sf, true);
                machinePointBombModelLayers.Add(layer);
                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.PointType = tkPointSymbolType.ptSymbolPicture;
                //options.PointSize = 10;
                //options.PointShape = tkPointShapeType.ptShapeCircle;
                //var pathpng = AppUtils.GetAppDataPath();
                //pathpng += "\\machine icons";
                //pathpng = System.IO.Path.Combine(pathpng, color + ".png");
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), Constants.ICON_FOLDER, color + ".png");
                options.Picture = AppUtils.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                //options.LineWidth = 2;

                //MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                //pattern.AddLine(utils.ColorByName(tkMapColor.Gray), 8.0f, tkDashStyle.dsSolid);
                //pattern.AddLine(utils.ColorByName(tkMapColor.Yellow), 7.0f, tkDashStyle.dsSolid);
                //LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowRight);
                //segm.Color = utils.ColorByName(tkMapColor.Orange);
                //segm.MarkerSize = 10;

                sf.CollisionMode = tkCollisionMode.AllowCollisions;
            }
        }

        private static void InitLineModelLayer()
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                var sf = new Shapefile();
                sf.CreateNew("", ShpfileType.SHP_POLYLINE);
                Shape shp = new Shape();
                shp.Create(ShpfileType.SHP_POLYLINE);
                int index = sf.NumShapes;
                sf.EditInsertShape(shp, ref index);
                //var sf = CreateLines();
                int layer = axMap1.AddLayer(sf, true);
                machineLineBombModelLayers.Add(layer);

                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(AppUtils.ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = AppUtils.ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private static void InitPointImageModelHistoryLayer()
        {

            foreach (string color in Constants.MACHINE_COLORS)
            {
                Shapefile sf = new Shapefile();
                //sf.Open(filename, null);
                int layer = axMap1.AddLayer(sf, true);
                sf = axMap1.get_Shapefile(layer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
                sf = new Shapefile();
                if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                {
                    MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("InitPointImageModelHistoryLayer()");
                    return;
                }
                layer = axMap1.AddLayer(sf, true);
                machinePointModelHistoryLayers.Add(layer);
                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.PointType = tkPointSymbolType.ptSymbolPicture;
                //options.PointSize = 10;
                //options.PointShape = tkPointShapeType.ptShapeCircle;
                //var pathpng = AppUtils.GetAppDataPath();
                //pathpng += "\\machine icons";
                //pathpng = System.IO.Path.Combine(pathpng, color + ".png");
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), Constants.ICON_FOLDER, color + ".png");
                options.Picture = AppUtils.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                //options.LineWidth = 2;

                //MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                //pattern.AddLine(utils.ColorByName(tkMapColor.Gray), 8.0f, tkDashStyle.dsSolid);
                //pattern.AddLine(utils.ColorByName(tkMapColor.Yellow), 7.0f, tkDashStyle.dsSolid);
                //LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowRight);
                //segm.Color = utils.ColorByName(tkMapColor.Orange);
                //segm.MarkerSize = 10;

                sf.CollisionMode = tkCollisionMode.AllowCollisions;
            }
        }

        private static void InitLineModelHistoryLayer()
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                var sf = new Shapefile();
                sf.CreateNew("", ShpfileType.SHP_POLYLINE);
                Shape shp = new Shape();
                shp.Create(ShpfileType.SHP_POLYLINE);
                int index = sf.NumShapes;
                sf.EditInsertShape(shp, ref index);
                //var sf = CreateLines();
                int layer = axMap1.AddLayer(sf, true);
                machineLineModelHistoryLayers.Add(layer);

                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(AppUtils.ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = AppUtils.ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private static void InitPointImageRealTimeLayer()
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                Shapefile sf = new Shapefile();
                //sf.Open(filename, null);
                int layer = axMap1.AddLayer(sf, true);
                sf = axMap1.get_Shapefile(layer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
                sf = new Shapefile();
                if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                {
                    MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("InitPointImageRealTimeLayer()");
                    return;
                }
                layer = axMap1.AddLayer(sf, true);
                machinePointRealTimeLayers.Add(layer);
                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.PointType = tkPointSymbolType.ptSymbolPicture;
                //options.PointSize = 10;
                //options.PointShape = tkPointShapeType.ptShapeCircle;
                //var pathpng = AppUtils.GetAppDataPath();
                //pathpng += "\\machine icons";
                //pathpng = System.IO.Path.Combine(pathpng, color + ".png");
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), Constants.ICON_FOLDER, color + ".png");
                options.Picture = AppUtils.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                //options.LineWidth = 2;

                //MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                //pattern.AddLine(utils.ColorByName(tkMapColor.Gray), 8.0f, tkDashStyle.dsSolid);
                //pattern.AddLine(utils.ColorByName(tkMapColor.Yellow), 7.0f, tkDashStyle.dsSolid);
                //LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowRight);
                //segm.Color = utils.ColorByName(tkMapColor.Orange);
                //segm.MarkerSize = 10;

                sf.CollisionMode = tkCollisionMode.AllowCollisions;
            }
        }

        private static void InitLineRealTimeLayer()
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                var sf = new Shapefile();
                sf.CreateNew("", ShpfileType.SHP_POLYLINE);
                //Shape shp = new Shape();
                //shp.Create(ShpfileType.SHP_POLYLINE);
                //int index = sf.NumShapes;
                //sf.EditInsertShape(shp, ref index);
                //var sf = CreateLines();
                int layer = axMap1.AddLayer(sf, true);
                machineLineRealTimeLayers.Add(layer);

                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(AppUtils.ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = AppUtils.ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private static void InitPointImageRealTimeModelLayer()
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                Shapefile sf = new Shapefile();
                //sf.Open(filename, null);
                int layer = axMap1.AddLayer(sf, true);
                sf = axMap1.get_Shapefile(layer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
                sf = new Shapefile();
                if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                {
                    MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("InitPointImageRealTimeModelLayer()");
                    return;
                }
                layer = axMap1.AddLayer(sf, true);
                machinePointRealTimeModelLayers.Add(layer);
                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.PointType = tkPointSymbolType.ptSymbolPicture;
                //options.PointSize = 10;
                //options.PointShape = tkPointShapeType.ptShapeCircle;
                //var pathpng = AppUtils.GetAppDataPath();
                //pathpng += "\\machine icons";
                //pathpng = System.IO.Path.Combine(pathpng, color + ".png");
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), Constants.ICON_FOLDER, color + ".png");
                options.Picture = AppUtils.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                //options.LineWidth = 2;

                //MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                //pattern.AddLine(utils.ColorByName(tkMapColor.Gray), 8.0f, tkDashStyle.dsSolid);
                //pattern.AddLine(utils.ColorByName(tkMapColor.Yellow), 7.0f, tkDashStyle.dsSolid);
                //LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowRight);
                //segm.Color = utils.ColorByName(tkMapColor.Orange);
                //segm.MarkerSize = 10;

                sf.CollisionMode = tkCollisionMode.AllowCollisions;
            }
        }

        private static void InitLineRealTimeModelLayer()
        {
            foreach (string color in Constants.MACHINE_COLORS)
            {
                var sf = new Shapefile();
                sf.CreateNew("", ShpfileType.SHP_POLYLINE);
                //Shape shp = new Shape();
                //shp.Create(ShpfileType.SHP_POLYLINE);
                //int index = sf.NumShapes;
                //sf.EditInsertShape(shp, ref index);
                //var sf = CreateLines();
                int layer = axMap1.AddLayer(sf, true);
                machineLineRealTimeModelLayers.Add(layer);

                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.LineColor = AppUtils.ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(AppUtils.ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = AppUtils.ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        public static void initPolygonLayer(bool bombVisible, bool mineVisible)
        {
            //Shapefile sf = new Shapefile();
            ////sf.Open(filename, null);
            //polygonLayer = axMap1.AddLayer(sf, true);
            //sf = axMap1.get_Shapefile(polygonLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding





            //polygonLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //polygonLayerMine = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //axMap1.SetDrawingLayerVisible(polygonLayer, false);
            //axMap1.SetDrawingLayerVisible(polygonLayerMine, false);

            initPolygonLayerBomb(bombVisible);
            initPolygonLayerMine(mineVisible);
            //foreach(int layer in polygonLayers)
            //{
            //    axMap1.MoveLayerBottom(layer);
            //}
            //foreach(int layer in imageLayers)
            //{
            //    axMap1.MoveLayerBottom(layer);
            //}
            foreach (int layer in machinePointBombLayers)
            {
                axMap1.MoveLayerTop(layer);
            }
            foreach (int layer in machineLineBombLayers)
            {
                axMap1.MoveLayerTop(layer);
            }
            if(suspectPointLayerBomb > 0)
            {
                axMap1.MoveLayerTop(suspectPointLayerBomb);
            }
            if (suspectPointLayerMine > 0)
            {
                axMap1.MoveLayerTop(suspectPointLayerMine);
            }
            if (polygonLayerTest > 0)
            {
                axMap1.MoveLayerTop(polygonLayerTest);
            }
        }

        private static void initPolygonAreaLayer()
        {
            Shapefile sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("initPolygonAreaLayer()");
                return;
            }
            sf.DefaultDrawingOptions.LineColor = AppUtils.ColorToUint(Color.Red);
            sf.Identifiable = false;
            polygonAreaLayer = axMap1.AddLayer(sf, true);
            axMap1.set_ShapeLayerFillColor(polygonAreaLayer, AppUtils.ColorToUint(Color.FromArgb(168, 50, 147)));
            axMap1.set_ShapeLayerFillTransparency(polygonAreaLayer, 0.3f);
            //axMap1.set_ShapeLayerDrawFill(polygonLayer, false);
            //axMap1.set_ShapeLayerDrawLine(layer, false);
        }

        private static void initPolygonTestLayer()
        {
            Shapefile sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("initPolygonAreaLayer()");
                return;
            }
            //sf.DefaultDrawingOptions.LineColor = AppUtils.ColorToUint(Color.Red);
            sf.Identifiable = false;
            polygonLayerTest = axMap1.AddLayer(sf, true);
            axMap1.set_ShapeLayerFillColor(polygonLayerTest, AppUtils.ColorToUint(Color.White));
            axMap1.set_ShapeLayerFillTransparency(polygonLayerTest, 0.5f);
            //axMap1.set_ShapeLayerDrawFill(polygonLayer, false);
            //axMap1.set_ShapeLayerDrawLine(layer, false);
        }

        private static void initPolygonLayerBomb(bool visible)
        {
            foreach(int layer in polygonLayers)
            {
                axMap1.RemoveLayer(layer);
            }
            polygonLayers.Clear();
            List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "DaiMauTuTruong", "IsBomb", "1" );

            if (lst.Count > 0)
            {
                foreach (DataRow dr in lst)
                {
                    int r = int.Parse(dr["red"].ToString());
                    int g = int.Parse(dr["green"].ToString());
                    int b = int.Parse(dr["blue"].ToString());
                    Color color = Color.FromArgb(r, g, b);
                    Shapefile sf = new Shapefile();
                    if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
                    {
                        MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                        MessageBox.Show("initPolygonLayer()");
                        return;
                    }
                    sf.DefaultDrawingOptions.LineColor = AppUtils.ColorToUint(color);
                    sf.Identifiable = false;
                    int layer = axMap1.AddLayer(sf, visible);
                    axMap1.set_ShapeLayerFillColor(layer, AppUtils.ColorToUint(color));
                    //axMap1.set_ShapeLayerDrawLine(layer, false);
                    polygonLayers.Add(layer);
                }
            }
            else
            {
                for (int i = 0; i < Constants.magnetic_colors.Length; i++)
                {
                    Shapefile sf_bomb = new Shapefile();
                    if (!sf_bomb.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
                    {
                        MessageBox.Show("Failed to create shapefile: " + sf_bomb.ErrorMsg[sf_bomb.LastErrorCode]);
                        MessageBox.Show("initPolygonLayer()");
                        return;
                    }
                    sf_bomb.DefaultDrawingOptions.LineColor = AppUtils.ColorToUint(Constants.magnetic_colors[i]);
                    int layer = axMap1.AddLayer(sf_bomb, visible);
                    axMap1.set_ShapeLayerFillColor(layer, AppUtils.ColorToUint(Constants.magnetic_colors[i]));
                    //axMap1.set_ShapeLayerDrawLine(layer, false);
                    polygonLayers.Add(layer);
                }
            }
        }

        private static void initPolygonLayerMine(bool visible)
        {
            foreach (int layer in polygonLayersMine)
            {
                axMap1.RemoveLayer(layer);
            }
            polygonLayersMine.Clear();
            List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "DaiMauTuTruong", "IsBomb", "2");

            if (lst.Count > 0)
            {
                foreach (DataRow dr in lst)
                {
                    int r = int.Parse(dr["red"].ToString());
                    int g = int.Parse(dr["green"].ToString());
                    int b = int.Parse(dr["blue"].ToString());
                    Color color = Color.FromArgb(r, g, b);
                    Shapefile sf = new Shapefile();
                    if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
                    {
                        MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                        MessageBox.Show("initPolygonLayer()");
                        return;
                    }
                    sf.DefaultDrawingOptions.LineColor = AppUtils.ColorToUint(color);
                    sf.Identifiable = false;
                    int layer = axMap1.AddLayer(sf, visible);
                    axMap1.set_ShapeLayerFillColor(layer, AppUtils.ColorToUint(color));
                    //axMap1.set_ShapeLayerDrawLine(layer, false);
                    polygonLayersMine.Add(layer);
                }
            }
            else
            {
                for (int i = 0; i < Constants.magnetic_colors.Length; i++)
                {
                    Shapefile sf_bomb = new Shapefile();
                    if (!sf_bomb.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
                    {
                        MessageBox.Show("Failed to create shapefile: " + sf_bomb.ErrorMsg[sf_bomb.LastErrorCode]);
                        MessageBox.Show("initPolygonLayer()");
                        return;
                    }
                    sf_bomb.DefaultDrawingOptions.LineColor = AppUtils.ColorToUint(Constants.magnetic_colors[i]);
                    int layer = axMap1.AddLayer(sf_bomb, visible);
                    axMap1.set_ShapeLayerFillColor(layer, AppUtils.ColorToUint(Constants.magnetic_colors[i]));
                    //axMap1.set_ShapeLayerDrawLine(layer, false);
                    polygonLayersMine.Add(layer);
                }
            }
        }

        private static void InitSuspectPointLayer()
        {
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            suspectPointLayerBomb = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(suspectPointLayerBomb);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitSuspectPointLayer()");
                return;
            }
            suspectPointLayerBomb = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            sf.Identifiable = false;

            Labels labels = sf.Labels;
            labels.Visible = true;
            labels.Alignment = tkLabelAlignment.laBottomRight;
            labels.OffsetX = 10;
            labels.OffsetY = 10;
            labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
            labels.AvoidCollisions = true;
        }

        private static void InitSuspectPointMineLayer()
        {
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            suspectPointLayerMine = axMap1.AddLayer(sf, false);
            sf = axMap1.get_Shapefile(suspectPointLayerMine);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitSuspectPointLayer()");
                return;
            }
            suspectPointLayerMine = axMap1.AddLayer(sf, false);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker min.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            sf.Identifiable = false;

            Labels labels = sf.Labels;
            labels.Visible = true;
            labels.Alignment = tkLabelAlignment.laBottomRight;
            labels.OffsetX = 10;
            labels.OffsetY = 10;
            labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
            labels.AvoidCollisions = true;
        }

        private static void InitUserSuspectPointLayer()
        {
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            userSuspectPointLayerBomb = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(userSuspectPointLayerBomb);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitSuspectPointLayer()");
                return;
            }
            userSuspectPointLayerBomb = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            sf.Identifiable = false;

            Labels labels = sf.Labels;
            labels.Visible = true;
            labels.Alignment = tkLabelAlignment.laBottomRight;
            labels.OffsetX = 10;
            labels.OffsetY = 10;
            labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
            labels.AvoidCollisions = true;
        }

        private static void InitUserSuspectPointMineLayer()
        {
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            userSuspectPointLayerMine = axMap1.AddLayer(sf, false);
            sf = axMap1.get_Shapefile(userSuspectPointLayerMine);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitSuspectPointLayer()");
                return;
            }
            userSuspectPointLayerMine = axMap1.AddLayer(sf, false);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker min.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            sf.Identifiable = false;

            Labels labels = sf.Labels;
            labels.Visible = true;
            labels.Alignment = tkLabelAlignment.laBottomRight;
            labels.OffsetX = 10;
            labels.OffsetY = 10;
            labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
            labels.AvoidCollisions = true;
        }

        private static void InitDistancePointLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            //markerDistanceLayer = axMap1.AddLayer(sf, true);
            //sf = axMap1.get_Shapefile(markerDistanceLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            //sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitDistancePointLayer()");
                return;
            }
            markerDistanceLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "markerDistance.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //MessageBox.Show("axMap1.MoveLayerTop(markerDistanceLayer): " + axMap1.MoveLayerTop(markerDistanceLayer));
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitHighlightLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            highlightLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(highlightLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitHighlightLayer()");
                return;
            }
            highlightLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "highlight.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitHighlightCurrentPointLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            highlightCurrentPointLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(highlightCurrentPointLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitHighlightCurrentPointLayer()");
                return;
            }
            highlightCurrentPointLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "highlight_current.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitHighlightCurrentPointModelLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            highlightCurrentPointModelLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(highlightCurrentPointModelLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitHighlightCurrentPointModelLayer()");
                return;
            }
            highlightCurrentPointModelLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "highlight_current.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitGreenFlagLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            greenFlagLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(greenFlagLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitGreenFlagLayer()");
                return;
            }
            greenFlagLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "green flag.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitMachineLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            machineLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(machineLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitMachineLayer()");
                return;
            }
            //sf.UseQTree = true;
            //sf.Labels.Generate("[Name]", tkLabelPositioning.lpCentroid, false);
            machineLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            //var pathpng = AppUtils.GetAppDataPath();
            //pathpng = System.IO.Path.Combine(pathpng, "green flag.png");
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitFlagLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            flagLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(flagLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitFlagLayer()");
                return;
            }
            flagLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "red flag.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitFlagRealTimeLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            flagRealTimeLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(flagRealTimeLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitFlagLayer()");
                return;
            }
            flagRealTimeLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "red flag real time.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitDeepLayer()
        {
            //axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "buildings.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("Couldn't file the file: " + filename);
            //    return;
            //}
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            deepLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(deepLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitDeepLayer()");
                return;
            }
            deepLayer = axMap1.AddLayer(sf, true);
            axMap1.MoveLayerTop(deepLayer);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;

            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "dosau.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public static void SetBound(double xmin, double ymin, double xmax, double ymax)
        {
            Extents extents = new Extents();
            extents.SetBounds(xmin, ymin, 0, xmax, ymax, 0);
            axMap1.Extents = extents;
        }

        public static void SetBoundKVDA(long idKV)
        {
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new DataTable();
            string sql = "SELECT " +
                "position_lat, position_long, " +
                "left_long, right_long, bottom_lat, top_lat " +
                "FROM cecm_program_area_map where id = " + idKV;
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            double xmax = 0, xmin = 0, ymax = 0, ymin = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                xmin = double.Parse(dr["left_long"].ToString());
                xmax = double.Parse(dr["right_long"].ToString());
                ymin = double.Parse(dr["bottom_lat"].ToString());
                ymax = double.Parse(dr["top_lat"].ToString());
            }
            SetBound(xmin, ymin, xmax, ymax);
        }

        public static void LoadKhuVuc(long idKV, long idDA, int indexKV)
        {
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new DataTable();
            string sql = "SELECT " +
                "position_lat, position_long, " +
                "photo_file, " +
                "left_long, right_long, bottom_lat, top_lat, " +
                "polygongeomst " +
                "FROM cecm_program_area_map where id = " + idKV;
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            string photoFileName = "unknown.png";
            double xmax = 0, xmin = 0, ymax = 0, ymin = 0;
            string polygongeomst = "";
            foreach (DataRow dr in datatable.Rows)
            {
                axMap1.SetLatitudeLongitude(double.Parse(dr["position_lat"].ToString()), double.Parse(dr["position_long"].ToString()));
                photoFileName = dr["photo_file"].ToString();
                xmin = double.Parse(dr["left_long"].ToString());
                xmax = double.Parse(dr["right_long"].ToString());
                ymin = double.Parse(dr["bottom_lat"].ToString());
                ymax = double.Parse(dr["top_lat"].ToString());
                polygongeomst = dr["polygongeomst"].ToString();
            }
            SetBound(xmin, ymin, xmax, ymax);
            List<PointF[]> lstPoints = AppUtils.convertMultiPolygon(polygongeomst);
            if(lstPoints.Count > 0)
            {
                PointF[] polygon = lstPoints[0];
                drawPolygonArea(polygon, idKV);
            }
            string pathTemp = AppUtils.GetFolderTemp((int)idDA);
            string fullPath = System.IO.Path.Combine(pathTemp, photoFileName);
            if (File.Exists(fullPath))
            {
                LoadImage(fullPath, xmax, xmin, ymax, ymin);
            }
            //else
            //{
            //    //axMap1.set_Image(imageLayer, null);
            //    Image img = axMap1.get_Image(imageLayer);
            //    img.Clear();
            //}
            //SqlCommandBuilder sqlCommand3 = null;
            //SqlDataAdapter sqlAdapter3 = null;
            //System.Data.DataTable datatable3 = new System.Data.DataTable();
            //string sql_oluoi =
            //    "SELECT " +
            //    "gid, o_id, " +
            //    "long_corner1, lat_corner1, " +
            //    "long_corner2, lat_corner2, " +
            //    "long_corner3, lat_corner3, " +
            //    "long_corner4, lat_corner4 " +
            //    "FROM OLuoi where cecm_program_areamap_id = " + idKV;
            //sqlAdapter3 = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
            //sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
            //sqlAdapter3.Fill(datatable3);

            idKV__shapeOLuoi.Remove(idKV);
            idKV__shapeOLuoi.Add(idKV, new List<Shape>());

            List<DataRow> drOL = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "OLuoi", "cecm_program_areamap_id", idKV.ToString());

            foreach (DataRow dr in drOL)
            {
                //double long_corner1 = double.Parse(dr["long_corner1"].ToString());
                //double long_corner2 = double.Parse(dr["long_corner2"].ToString());
                //double long_corner3 = double.Parse(dr["long_corner3"].ToString());
                //double long_corner4 = double.Parse(dr["long_corner4"].ToString());
                //double lat_corner1 = double.Parse(dr["lat_corner1"].ToString());
                //double lat_corner2 = double.Parse(dr["lat_corner2"].ToString());
                //double lat_corner3 = double.Parse(dr["lat_corner3"].ToString());
                //double lat_corner4 = double.Parse(dr["lat_corner4"].ToString());
                //long idOL = long.Parse(dr["gid"].ToString());
                OLuoi oluoi = new OLuoi();
                //gid
                string gid = dr["gid"].ToString();
                oluoi.gid = long.Parse(gid);
                //o_id
                string o_id = dr["o_id"].ToString();
                oluoi.o_id = o_id;
                //cecm_program_areamap_id
                string cecm_program_areamap_ID = dr["cecm_program_areamap_id"].ToString();
                oluoi.cecm_program_areamap_ID = idKV;
                //autoDivide
                oluoi.autoDivide = int.TryParse(dr["autoDivide"].ToString(), out int autoDivide) ? autoDivide : 0;
                //khaosat_deptid
                long khaosat_deptid = -1;
                long.TryParse(dr["khaosat_deptid"].ToString(), out khaosat_deptid);
                oluoi.khaosat_deptId = khaosat_deptid;
                //rapha_deptid
                long rapha_deptid = -1;
                long.TryParse(dr["rapha_deptid"].ToString(), out rapha_deptid);
                oluoi.raPha_deptId = rapha_deptid;
                //lat_center
                double lat_center = -1;
                double.TryParse(dr["lat_center"].ToString(), out lat_center);
                oluoi.lat_center = lat_center;
                //lat_corner1
                double lat_corner1 = -1;
                double.TryParse(dr["lat_corner1"].ToString(), out lat_corner1);
                oluoi.lat_corner1 = lat_corner1;
                //lat_corner2
                double lat_corner2 = -1;
                double.TryParse(dr["lat_corner2"].ToString(), out lat_corner2);
                oluoi.lat_corner2 = lat_corner2;
                //lat_corner3
                double lat_corner3 = -1;
                double.TryParse(dr["lat_corner3"].ToString(), out lat_corner3);
                oluoi.lat_corner3 = lat_corner3;
                //lat_corner4
                double lat_corner4 = -1;
                double.TryParse(dr["lat_corner4"].ToString(), out lat_corner4);
                oluoi.lat_corner4 = lat_corner4;
                //long_center
                double long_center = -1;
                double.TryParse(dr["long_center"].ToString(), out long_center);
                oluoi.long_center = long_center;
                //long_corner1
                double long_corner1 = -1;
                double.TryParse(dr["long_corner1"].ToString(), out long_corner1);
                oluoi.long_corner1 = long_corner1;
                //long_corner2
                double long_corner2 = -1;
                double.TryParse(dr["long_corner2"].ToString(), out long_corner2);
                oluoi.long_corner2 = long_corner2;
                //long_corner3
                double long_corner3 = -1;
                double.TryParse(dr["long_corner3"].ToString(), out long_corner3);
                oluoi.long_corner3 = long_corner3;
                //long_corner4
                double long_corner4 = -1;
                double.TryParse(dr["long_corner4"].ToString(), out long_corner4);
                oluoi.long_corner4 = long_corner4;
                //dividerAllGrid
                long dividerAllGrid = 0;
                long.TryParse(dr["dividerAllGrid"].ToString(), out dividerAllGrid);
                oluoi.dividerAllGrid = dividerAllGrid;
                //isCustomAllGrid
                long isCustomAllGrid = 0;
                long.TryParse(dr["isCustomAllGrid"].ToString(), out isCustomAllGrid);
                oluoi.isCustomAllGrid = isCustomAllGrid;
                //distanceAllGrid
                double distanceAllGrid = 0;
                double.TryParse(dr["distanceAllGrid"].ToString(), out distanceAllGrid);
                oluoi.distanceAllGrid = distanceAllGrid;
                //acutangeAllGrid
                double acutangeAllGrid = 0;
                double.TryParse(dr["acutangeAllGrid"].ToString(), out acutangeAllGrid);
                oluoi.acutangeAllGrid = acutangeAllGrid;
                //isCustom1
                long isCustom1 = 0;
                long.TryParse(dr["isCustom1"].ToString(), out isCustom1);
                oluoi.isCustom1 = isCustom1;
                //isCustom2
                long isCustom2 = 0;
                long.TryParse(dr["isCustom2"].ToString(), out isCustom2);
                oluoi.isCustom2 = isCustom2;
                //isCustom3
                long isCustom3 = 0;
                long.TryParse(dr["isCustom3"].ToString(), out isCustom3);
                oluoi.isCustom3 = isCustom3;
                //isCustom4
                long isCustom4 = 0;
                long.TryParse(dr["isCustom4"].ToString(), out isCustom4);
                oluoi.isCustom4 = isCustom4;
                //acuteAngle1
                double acuteAngle1 = 0;
                double.TryParse(dr["acuteAngle1"].ToString(), out acuteAngle1);
                oluoi.acuteAngle1 = acuteAngle1;
                //acuteAngle2
                double acuteAngle2 = 0;
                double.TryParse(dr["acuteAngle2"].ToString(), out acuteAngle2);
                oluoi.acuteAngle2 = acuteAngle2;
                //acuteAngle3
                double acuteAngle3 = 0;
                double.TryParse(dr["acuteAngle3"].ToString(), out acuteAngle3);
                oluoi.acuteAngle3 = acuteAngle3;
                //acuteAngle4
                double acuteAngle4 = 0;
                double.TryParse(dr["acuteAngle4"].ToString(), out acuteAngle4);
                oluoi.acuteAngle4 = acuteAngle4;
                //distance1
                double distance1 = 0;
                double.TryParse(dr["distance1"].ToString(), out distance1);
                oluoi.distance1 = distance1;
                //distance2
                double distance2 = 0;
                double.TryParse(dr["distance2"].ToString(), out distance2);
                oluoi.distance2 = distance2;
                //distance3
                double distance3 = 0;
                double.TryParse(dr["distance3"].ToString(), out distance3);
                oluoi.distance3 = distance3;
                //distance4
                double distance4 = 0;
                double.TryParse(dr["distance4"].ToString(), out distance4);
                oluoi.distance4 = distance4;
                
                List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "cecm_program_area_line", "cecmprogramareasub_id", dr["gid"].ToString());
                //List<CecmProgramAreaLineDTO> lstRanhDo = new List<CecmProgramAreaLineDTO>();
                int index = 0;
                List<CecmProgramAreaLineDTO> lstRanhDo = new List<CecmProgramAreaLineDTO>();
                foreach (DataRow dataRow in lst)
                {
                    bool parseSuccess =
                    double.TryParse(dataRow["start_x"].ToString(), out double lattStart) &
                    double.TryParse(dataRow["start_y"].ToString(), out double longtStart) &
                    double.TryParse(dataRow["end_x"].ToString(), out double lattEnd) &
                    double.TryParse(dataRow["end_y"].ToString(), out double longtEnd);
                    long.TryParse(dataRow["cecmprogramareasub_id"].ToString(), out long cecmprogramareasub_id);
                    long.TryParse(dataRow["cecmprogramareamap_id"].ToString(), out long cecmprogramareamap_id);
                    long.TryParse(dataRow["cecmprogram_id"].ToString(), out long cecmprogram_id);
                    if (parseSuccess)
                    {
                        CecmProgramAreaLineDTO line = new CecmProgramAreaLineDTO();
                        line.cecmprogram_id = cecmprogram_id;
                        line.cecmprogramareamap_id = cecmprogramareamap_id;
                        line.cecmprogramareasub_id = cecmprogramareasub_id;
                        line.start_y = longtStart;
                        line.start_x = lattStart;
                        line.end_y = longtEnd;
                        line.end_x = lattEnd;
                        line.code = (index + 1).ToString();
                        //lstRanhDo.Add(line);
                        index++;
                        //axMap1.DrawLineEx(lineLayer, line.start_y, line.start_x, line.end_y, line.end_x, 1, AppUtils.ColorToUint(Color.White));
                        string json = JsonConvert.SerializeObject(line);
                        addRanhDo(line.start_y, line.start_x, line.end_y, line.end_x, index % 2 == 1 ? index.ToString() : "", json, out int indexShape, out int indexLabel);
                        //if (index % 2 == 1)
                        //{
                        //    axMap1.DrawLabelEx(lineLayer, index.ToString(), line.start_y, line.start_x, 0);
                        //}
                        line.indexShape = indexShape;
                        line.indexLabel = indexLabel;
                        lstRanhDo.Add(line);
                    }

                }
                oluoi.lstRanhDo = lstRanhDo;
                drawOluoi(oluoi);
            }
            //DataRow dr2 = datatable3.NewRow();
            //dr2["gid"] = -1;
            //dr2["o_id"] = "Chưa chọn";
            //datatable3.Rows.InsertAt(dr2, 0);
            //cb50x50.DataSource = datatable3;
            //cb50x50.DisplayMember = "o_id";
            //cb50x50.ValueMember = "gid";
        }

        public static void LoadDuAn(long idDA)
        {
            foreach(int imageLayer in imageLayers)
            {
                axMap1.RemoveLayer(imageLayer);
            }
            imageLayers.Clear();
            //axMap1.ClearDrawing(oluoiLayer);
            //oluoiLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //axMap1.ClearDrawing(lineLayer);
            //lineLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            Shapefile sf = axMap1.get_Shapefile(ranhDoLayer);
            sf.EditClear();
            sf = axMap1.get_Shapefile(oluoiLayer);
            sf.EditClear();
            sf = axMap1.get_Shapefile(polygonAreaLayer);
            sf.EditClear();
            idKV__shapeIndex.Clear();
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new DataTable();
            string sql = "SELECT " +
                "id " +
                "FROM cecm_program_area_map where cecm_program_id = " + idDA;
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            int indexKV = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                long.TryParse(dr["id"].ToString(), out long idKV);
                LoadKhuVuc(idKV, idDA, indexKV);
                indexKV++;
            }
        }

        public static void drawOluoi(OLuoi oLuoi)
        {
            //axMap1.DrawLineEx(oluoiLayer, long_corner1, lat_corner1, long_corner2, lat_corner2, 5, AppUtils.ColorToUint(Color.White));
            //axMap1.DrawLineEx(oluoiLayer, long_corner2, lat_corner2, long_corner3, lat_corner3, 5, AppUtils.ColorToUint(Color.White));
            //axMap1.DrawLineEx(oluoiLayer, long_corner3, lat_corner3, long_corner4, lat_corner4, 5, AppUtils.ColorToUint(Color.White));
            //axMap1.DrawLineEx(oluoiLayer, long_corner4, lat_corner4, long_corner1, lat_corner1, 5, AppUtils.ColorToUint(Color.White));
            Shapefile sf = axMap1.get_Shapefile(oluoiLayer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POLYGON);
            shp.AddPoint(oLuoi.long_corner1, oLuoi.lat_corner1);
            shp.AddPoint(oLuoi.long_corner2, oLuoi.lat_corner2);
            shp.AddPoint(oLuoi.long_corner3, oLuoi.lat_corner3);
            shp.AddPoint(oLuoi.long_corner4, oLuoi.lat_corner4);
            shp.AddPoint(oLuoi.long_corner1, oLuoi.lat_corner1);
            shp.Key = JsonConvert.SerializeObject(oLuoi);
            idKV__shapeOLuoi[oLuoi.cecm_program_areamap_ID].Add(shp);
            int index = sf.NumShapes;
            if (!sf.EditInsertShape(shp, ref index))
            {
                MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("drawOluoi()");
                return;
            }
        }

        public static void RemoveHighlight()
        {
            Shapefile sf = axMap1.get_Shapefile(highlightLayer);
            sf.EditClear();
        }

        public static void addHighlight(double x, double y)
        {
            Shapefile sf = axMap1.get_Shapefile(highlightLayer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POINT);
            Point pnt = new Point();
            //axMap1.PixelToProj(x, y, ref x, ref y);
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            index = sf.NumShapes;
            if (!sf.EditInsertShape(shp, ref index))
            {
                MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("addHighlight()");
                return;
            }
            axMap1.Redraw();
        }

        //public static void LoadPoints(Dictionary<long, Dictionary<string, List<InfoConnect>>> idKV__Points)
        //{
        //    Dictionary<string, List<InfoConnect>> machine__points = new Dictionary<string, List<InfoConnect>>();
        //    foreach(var pair_idKV__Points in idKV__Points)
        //    {
        //        Dictionary<string, List<InfoConnect>> machine__points_temp = pair_idKV__Points.Value;
        //        foreach (var pair_machine__points_temp in machine__points_temp)
        //        {
        //            if (machine__points.ContainsKey(pair_machine__points_temp.Key))
        //            {
        //                machine__points[pair_machine__points_temp.Key].AddRange(pair_machine__points_temp.Value);
        //            }
        //            else
        //            {
        //                machine__points.Add(pair_machine__points_temp.Key, pair_machine__points_temp.Value);
        //            }
        //        }
        //    }
        //    foreach (var pair_machine__points in machine__points)
        //    {
        //        getColorForMachine(pair_machine__points.Key);
        //    }
        //    foreach (var pair_machine__points in machine__points)
        //    {
        //        Thread thread = new Thread(() => { 
        //            foreach(InfoConnect infoConnect in pair_machine__points.Value)
        //            {

        //                addMachinePoint(infoConnect.coordinate.Coordinates.X, infoConnect.coordinate.Coordinates.Y, pair_machine__points.Key, infoConnect.time_action, false);
        //            }
        //            axMap1.Redraw();
        //        });
        //        thread.IsBackground = true;
        //        thread.Start();
        //    }
        //}

        public static void togglePolygon(bool show)
        {
            //foreach(int layer in polygonLayers)
            //{
            //    axMap1.SetDrawingLayerVisible(layer, show);
            //}
            axMap1.SetDrawingLayerVisible(polygonAreaLayer, show);
        }

        public static void addMachinePoint(InfoConnect infoConnect, bool isRedraw = true)
        {
            try
            {
                //if (!machine__label.ContainsKey(codeMachine))
                //    return;
                string codeMachine = infoConnect.code;
                double x = infoConnect.coordinate.Coordinates.X;
                double y = infoConnect.coordinate.Coordinates.Y;
                DateTime timeAction = infoConnect.time_action.ToLocalTime();
                getColorForMachine(codeMachine, infoConnect.isMachineBom);
                Shapefile sf = axMap1.get_Shapefile(machineActive__pointLayer[codeMachine]);
                Shape shp = new Shape();
                shp.Create(ShpfileType.SHP_POINT);
                Point pnt = new Point();
                //axMap1.PixelToProj(x, y, ref x, ref y);
                pnt.x = x;
                pnt.y = y;
                int index = shp.numPoints;
                shp.InsertPoint(pnt, ref index);
                index = sf.NumShapes;
                if (!sf.EditInsertShape(shp, ref index))
                {
                    MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("addMachinePoint()");
                    return;
                }
                //if (!machine__shapeIndex_time.ContainsKey(codeMachine))
                //{
                //    machine__shapeIndex_time.Add(codeMachine, new Dictionary<int, DateTime>());
                //}
                //machine__shapeIndex_time[codeMachine].Add(index, time);
                if (!machineActive__lastTime.ContainsKey(codeMachine))
                {
                    machineActive__lastTime.Add(codeMachine, DateTime.MinValue);
                }

                //if (!machineActive__lastShape.ContainsKey(codeMachine))
                //{
                //    machineActive__lastShape.Add(codeMachine, shp);
                //}
                //else
                //{
                //    Shape recentShape = machineActive__lastShape[codeMachine];
                //    machineActive__lastShape[codeMachine] = shp;
                //    Point recentPoint = machineActive__lastPoint[codeMachine];
                //    double angle = AppUtils.CalculateAngle(recentPoint.x, recentPoint.y, x, y);
                //    recentShape.Rotate(recentPoint.x + 1, recentPoint.y + 1, angle);
                //}

                double recentDistance = 0;
                if (!machineActive__lastPoint.ContainsKey(codeMachine))
                {
                    machineActive__lastPoint.Add(codeMachine, pnt);
                    //machineActive__lastTime[codeMachine] = timeAction;
                }
                else
                {
                    Point recentPoint = machineActive__lastPoint[codeMachine];
                    machineActive__lastPoint[codeMachine] = pnt;
                    recentDistance = AppUtils.DistanceLatLong(recentPoint.y, recentPoint.x, y, x);


                    //if((timeAction - machineActive__lastTime[codeMachine]).TotalSeconds <= 20)
                    //{
                    //    if (!machineActive__lineLayer.ContainsKey(codeMachine))
                    //    {
                    //        int lineLayerNew = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                    //        if (btnShowPoint.Text == "Hiện điểm dò được" || btnModel.Text == "Tắt nắn điểm")
                    //        {
                    //            axMap1.SetDrawingLayerVisible(lineLayerNew, false);
                    //        }
                    //        machineActive__lineLayer.Add(codeMachine, lineLayerNew);
                    //    }
                    //    int lineLayer = machineActive__lineLayer[codeMachine];
                    //    axMap1.DrawLineEx(lineLayer, recentPoint.x, recentPoint.y, x, y, 2, ColorToUint(machineActive__color[codeMachine]));
                    //}
                    //machineActive__lastTime[codeMachine] = timeAction;
                }
                if ((timeAction - machineActive__lastTime[codeMachine]).TotalSeconds <= MIN_TIME_NEW_LINE && recentDistance < MIN_DISTANCE_NEW_LINE)
                {
                    addMachineLine(x, y, codeMachine, false);
                }
                else
                {
                    addMachineLine(x, y, codeMachine, true);
                }
                machineActive__lastTime[codeMachine] = timeAction;

                if (isRedraw)
                {
                    axMap1.Redraw();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private static void addMachineLine(double x, double y, string codeMachine, bool isNewLine)
        {
            //getLayerLineForMachines(codeMachine);
            Shapefile sf = axMap1.get_Shapefile(machineActive__lineLayer[codeMachine]);
            Shape shp;
            if (isNewLine)
            {
                shp = new Shape();
                shp.Create(ShpfileType.SHP_POLYLINE);
                int indexShape = sf.NumShapes;
                sf.EditInsertShape(shp, ref indexShape);
            }
            else
            {
                shp = sf.Shape[sf.NumShapes - 1];
            }

            MapWinGIS.Point pnt = new Point();
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            //axMap1.Redraw();
        }

        public static void addRanhDo(double x_start, double y_start, double x_end, double y_end, string label, string json, out int indexShape, out int indexLabel)
        {
            //getLayerLineForMachines(codeMachine);
            Shapefile sf = axMap1.get_Shapefile(ranhDoLayer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POLYLINE);
            shp.Key = json;
            //indexShape = sf.NumShapes;
            //sf.EditInsertShape(shp, ref indexShape);
            indexShape = sf.EditAddShape(shp);

            Point pnt_start = new Point();
            pnt_start.x = x_start;
            pnt_start.y = y_start;
            int index = shp.numPoints;
            shp.InsertPoint(pnt_start, ref index);

            Point pnt_end = new Point();
            pnt_end.x = x_end;
            pnt_end.y = y_end;
            index = shp.numPoints;
            shp.InsertPoint(pnt_end, ref index);

            if(label != "")
            {
                sf.Labels.AddLabel(label, x_start, y_start);
                indexLabel = sf.Labels.Count - 1;
            }
            else
            {
                indexLabel = -1;
            }
            //axMap1.Redraw();
        }

        private static void getColorForMachine(string codeMachine, bool isBomb)
        {
            //máy online hay offline
            //if (!machineActive__color.ContainsKey(codeMachine))
            //{
            //    machineActive__color.Add(codeMachine, activeMachineColor[machineActive__color.Count % activeMachineColor.Length]);
            //    //machine__label[codeMachine].setForeColor(machineActive__color[codeMachine]);
            //}
            getLayerPointForMachines(codeMachine, isBomb);
            getLayerLineForMachines(codeMachine, isBomb);
        }

        private static void getLayerPointForMachines(string codeMachine, bool isBomb)
        {
            //Nhằm đồng bộ màu điểm thì gán layer vào cả 2 collection điểm bình thường và điểm nắn
            if (!machineActive__pointLayer.ContainsKey(codeMachine))
            {
                int layer;
                if (isBomb)
                {
                    layer = machinePointBombLayers[machineActive__pointLayer.Count % machinePointBombLayers.Count];
                }
                else
                {
                    layer = machinePointMineLayers[machineActive__pointLayer.Count % machinePointMineLayers.Count];
                }
                machineActive__pointLayer.Add(codeMachine, layer);
                if (isBomb)
                {
                    layer = machinePointBombModelLayers[machineActive__pointLayer.Count % machinePointBombModelLayers.Count];
                }
                else
                {
                    layer = machinePointMineModelLayers[machineActive__pointLayer.Count % machinePointMineModelLayers.Count];
                }
                machineActive__pointModelLayer.Add(codeMachine, layer);
            }
        }

        private static void getLayerLineForMachines(string codeMachine, bool isBomb)
        {
            if (!machineActive__lineLayer.ContainsKey(codeMachine))
            {
                int lineLayer;
                if (isBomb)
                {
                    lineLayer = machineLineBombLayers[machineActive__lineLayer.Count % machineLineBombLayers.Count];
                    //lstPointBomb.Add(lineLayer);
                }
                else
                {
                    lineLayer = machineLineMineLayers[machineActive__lineLayer.Count % machineLineMineLayers.Count];
                    //lstPointMine.Add(lineLayer);
                }
                machineActive__lineLayer.Add(codeMachine, lineLayer);
                if (isBomb)
                {
                    lineLayer = machineLineBombModelLayers[machineActive__lineLayer.Count % machineLineBombLayers.Count];
                    //lstPointBomb.Add(lineLayer);
                }
                else
                {
                    lineLayer = machineLineMineModelLayers[machineActive__lineLayer.Count % machineLineMineLayers.Count];
                    //lstPointMine.Add(lineLayer);
                }
                machineActive__lineModelLayer.Add(codeMachine, lineLayer);
            }
        }

        //public static void LoadHistory(List<InfoConnect> lst)
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        LoadPointsHistory(lst);
        //    });
        //    thread.IsBackground = true;
        //    thread.Start();
        //}

        public static void ClearPointLayers()
        {
            //lstPointBomb.Clear();
            //lstPointMine.Clear();
        }

        public static void LoadPointsHistory(List<InfoConnect> lst)
        {
            foreach(InfoConnect doc in lst)
            {
                addMachinePoint(doc, false);
            }
        }

        public static void togglePolygonBomb(bool show)
        {
            //axMap1.set_LayerVisible(polygonLayer, show);
            foreach (int layer in polygonLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
            axMap1.set_LayerVisible(suspectPointLayerBomb, show);
            axMap1.set_LayerVisible(userSuspectPointLayerBomb, show);
            foreach (int layer in machinePointBombLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
            foreach (int layer in machineLineBombLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
        }

        public static void togglePolygonMine(bool show)
        {
            //axMap1.set_LayerVisible(polygonLayerMine, show);
            foreach (int layer in polygonLayersMine)
            {
                axMap1.set_LayerVisible(layer, show);
            }
            axMap1.set_LayerVisible(suspectPointLayerMine, show);
            axMap1.set_LayerVisible(userSuspectPointLayerMine, show);
            foreach (int layer in machinePointMineLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
            foreach (int layer in machineLineMineLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
        }

        public static void ClearPolygon()
        {
            //axMap1.RemoveLayer(polygonLayer);
            //axMap1.RemoveLayer(polygonLayerMine);
            //initPolygonLayer();
            foreach (int layer in polygonLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
                //axMap1.RemoveLayer(layer);
            }
            foreach (int layer in polygonLayersMine)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
                //axMap1.RemoveLayer(layer);
            }
            //initPolygonLayer();
        }

        public static void ClearHistoryPoints()
        {
            foreach (int layer in machinePointBombLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
                //axMap1.RemoveLayer(layer);
            }
            foreach (int layer in machineLineBombLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
            }
            foreach (int layer in machinePointMineLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
                //axMap1.RemoveLayer(layer);
            }
            foreach (int layer in machineLineMineLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
            }

            foreach (int layer in machinePointBombModelLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
                //axMap1.RemoveLayer(layer);
            }
            foreach (int layer in machineLineBombModelLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
            }
            foreach (int layer in machinePointMineModelLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
                //axMap1.RemoveLayer(layer);
            }
            foreach (int layer in machineLineMineModelLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
            }
        }

        public static void Redraw()
        {
            axMap1.Redraw();
        }

        public static void addSuspectPoint(double x, double y, CecmReportPollutionAreaBMVN point, bool isBomb, bool isUser)
        {
            int layer;
            if (isBomb)
            {
                if (isUser)
                {
                    layer = userSuspectPointLayerBomb;
                }
                else
                {
                    layer = suspectPointLayerBomb;
                }
            }
            else
            {
                if (isUser)
                {
                    layer = userSuspectPointLayerMine;
                }
                else
                {
                    layer = suspectPointLayerMine;
                }
            }
            Shapefile sf = axMap1.get_Shapefile(layer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POINT);
            Point pnt = new Point();
            pnt.x = x;
            pnt.y = y;
            //int index = shp.numPoints;
            //shp.InsertPoint(pnt, ref index);
            shp.AddPoint(x, y);
            point.indexLabel = sf.Labels.Count;
            if (idKV__shapeOLuoi.ContainsKey(point.idArea))
            {
                foreach (Shape shapeOLuoi in idKV__shapeOLuoi[point.idArea])
                {
                    if (shapeOLuoi.PointInThisPoly(pnt))
                    {
                        OLuoi oLuoi = JsonConvert.DeserializeObject<OLuoi>(shapeOLuoi.Key);
                        point.idRectangle = oLuoi.gid;
                        break;
                    }
                }
            }
            string json = JsonConvert.SerializeObject(point);
            shp.Key = json;
            //index = sf.NumShapes;
            //if (!sf.EditInsertShape(shp, ref index))
            //{
            //    MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
            //    MessageBox.Show("addSuspectPoint()");
            //    return;
            //}
            sf.EditAddShape(shp);
            sf.Labels.AddLabel(Math.Round(point.Deep, 3).ToString() + "m", x, y);
            axMap1.Redraw();
        }

        public static void ClearSuspectPoints()
        {
            Shapefile sf = axMap1.get_Shapefile(suspectPointLayerBomb);
            sf.EditClear();
            sf = axMap1.get_Shapefile(suspectPointLayerMine);
            sf.EditClear();
            //axMap1.RemoveLayer(suspectPointLayerBomb);
            //InitSuspectPointLayer();
            //axMap1.RemoveLayer(suspectPointLayerMine);
            //InitSuspectPointMineLayer();
        }

        public static void ClearUserSuspectPoints()
        {
            Shapefile sf = axMap1.get_Shapefile(userSuspectPointLayerBomb);
            sf.EditClear();
            sf = axMap1.get_Shapefile(userSuspectPointLayerMine);
            sf.EditClear();
            //axMap1.RemoveLayer(userSuspectPointLayerBomb);
            //InitUserSuspectPointLayer();
            //axMap1.RemoveLayer(userSuspectPointLayerMine);
            //InitUserSuspectPointMineLayer();
        }
    }
}
