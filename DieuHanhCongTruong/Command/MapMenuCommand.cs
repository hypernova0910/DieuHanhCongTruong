using AxMapWinGIS;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = MapWinGIS.Image;

namespace DieuHanhCongTruong.Command
{
    class MapMenuCommand
    {
        private static AxMap axMap1 = MyMainMenu2.Instance.axMap1;

        private static List<int> machinePointLayers = new List<int>();
        private static List<int> machinePointModelLayers = new List<int>();
        private static List<int> machinePointModelHistoryLayers = new List<int>();
        private static List<int> machinePointRealTimeLayers = new List<int>();
        private static List<int> machinePointRealTimeModelLayers = new List<int>();

        private static List<int> machineLineLayers = new List<int>();
        private static List<int> machineLineModelLayers = new List<int>();
        private static List<int> machineLineModelHistoryLayers = new List<int>();
        private static List<int> machineLineRealTimeLayers = new List<int>();
        private static List<int> machineLineRealTimeModelLayers = new List<int>();

        private static int polygonLayer = -1;
        private static int oluoiLayer = -1;
        private static int suspectPointLayer = -1;
        private static int suspectPointLayerMine = -1;
        private static int flagLayer = -1;
        private static int flagRealTimeLayer = -1;
        private static List<int> imageLayers = new List<int>();
        private static int deepLayer = -1;
        private static int pointLayer = -1;
        private static int greenFlagLayer = -1;
        private static int machineLayer = -1;
        private static int bgLayer = -1;
        private static int lineLayer = -1;
        private static int labelLayer = -1;
        private static int distanceLayer = -1;
        private static int markerDistanceLayer = -1;
        private static int highlightLayer = -1;
        private static int highlightCurrentPointLayer = -1;
        private static int highlightCurrentPointModelLayer = -1;

        public static void LoadMap()
        {
            //InitImageLayer();
            initPolygonLayer();
            InitPointImageLayer();
            InitLineLayer();
            InitPointImageModelLayer();
            InitLineModelLayer();
            InitPointImageModelHistoryLayer();
            InitLineModelHistoryLayer();
            InitPointImageRealTimeLayer();
            InitLineRealTimeLayer();
            InitPointImageRealTimeModelLayer();
            InitLineRealTimeModelLayer();
            InitSuspectPointLayer();
            InitSuspectPointMineLayer();
            InitFlagLayer();
            InitFlagRealTimeLayer();
            InitDeepLayer();
            InitGreenFlagLayer();
            InitMachineLayer();
            InitDistancePointLayer();
            InitHighlightLayer();
            InitHighlightCurrentPointLayer();
            InitHighlightCurrentPointModelLayer();
            lineLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            labelLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //tooltipLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //markerDistanceLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            distanceLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
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

        public static void drawPolygon(double[] xPoints, double[] yPoints)
        {
            if (xPoints.Length != yPoints.Length)
            {
                MessageBox.Show("Invalid Polygon Coordinates");
                return;
            }
            axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            object X = xPoints;
            object Y = yPoints;
            int size = xPoints.Length;
            axMap1.DrawPolygon(ref X, ref Y, size, AppUtils.ColorToUint(Color.FromArgb(173, 38, 169)), true);
            axMap1.Redraw();
        }

        private static void InitPointImageLayer()
        {
            foreach (string color in Constants.colors)
            {
                Shapefile sf = new Shapefile();
                //sf.Open(filename, null);
                int layer = axMap1.AddLayer(sf, true);
                sf = axMap1.get_Shapefile(layer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
                sf = new Shapefile();
                if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                {
                    MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("InitPointImageLayer()");
                    return;
                }
                layer = axMap1.AddLayer(sf, true);
                machinePointLayers.Add(layer);
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

        private static void InitLineLayer()
        {
            foreach (string color in Constants.colors)
            {
                var sf = new Shapefile();
                sf.CreateNew("", ShpfileType.SHP_POLYLINE);
                //Shape shp = new Shape();
                //shp.Create(ShpfileType.SHP_POLYLINE);
                //int index = sf.NumShapes;
                //sf.EditInsertShape(shp, ref index);
                //var sf = CreateLines();
                int layer = axMap1.AddLayer(sf, true);
                machineLineLayers.Add(layer);

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

        private static void InitPointImageModelLayer()
        {

            foreach (string color in Constants.colors)
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
                machinePointModelLayers.Add(layer);
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
            foreach (string color in Constants.colors)
            {
                var sf = new Shapefile();
                sf.CreateNew("", ShpfileType.SHP_POLYLINE);
                Shape shp = new Shape();
                shp.Create(ShpfileType.SHP_POLYLINE);
                int index = sf.NumShapes;
                sf.EditInsertShape(shp, ref index);
                //var sf = CreateLines();
                int layer = axMap1.AddLayer(sf, true);
                machineLineModelLayers.Add(layer);

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

            foreach (string color in Constants.colors)
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
            foreach (string color in Constants.colors)
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
            foreach (string color in Constants.colors)
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
            foreach (string color in Constants.colors)
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
            foreach (string color in Constants.colors)
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
            foreach (string color in Constants.colors)
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

        private static void initPolygonLayer()
        {
            //Shapefile sf = new Shapefile();
            ////sf.Open(filename, null);
            //polygonLayer = axMap1.AddLayer(sf, true);
            //sf = axMap1.get_Shapefile(polygonLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            Shapefile sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("initPolygonLayer()");
                return;
            }
            polygonLayer = axMap1.AddLayer(sf, true);
            bool success = axMap1.MoveLayerBottom(polygonLayer);
        }

        private static void InitSuspectPointLayer()
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
            suspectPointLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(suspectPointLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitSuspectPointLayer()");
                return;
            }
            suspectPointLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //MessageBox.Show("axMap1.MoveLayerTop(suspectPointLayer): " + axMap1.MoveLayerTop(suspectPointLayer));
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        private static void InitSuspectPointMineLayer()
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
            suspectPointLayerMine = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(suspectPointLayerMine);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("InitSuspectPointMineLayer()");
                return;
            }
            suspectPointLayerMine = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker min.png");
            options.Picture = AppUtils.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //MessageBox.Show("axMap1.MoveLayerTop(suspectPointLayer): " + axMap1.MoveLayerTop(suspectPointLayer));
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
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

        public static void LoadKhuVuc(long idKV, long idDA)
        {
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new DataTable();
            string sql = "SELECT " +
                "position_lat, position_long, " +
                "photo_file, " +
                "left_long, right_long, bottom_lat, top_lat " +
                "FROM cecm_program_area_map where id = " + idKV;
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            string photoFileName = "unknown.png";
            double xmax = 0, xmin = 0, ymax = 0, ymin = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                axMap1.SetLatitudeLongitude(double.Parse(dr["position_lat"].ToString()), double.Parse(dr["position_long"].ToString()));
                photoFileName = dr["photo_file"].ToString();
                xmin = double.Parse(dr["left_long"].ToString());
                xmax = double.Parse(dr["right_long"].ToString());
                ymin = double.Parse(dr["bottom_lat"].ToString());
                ymax = double.Parse(dr["top_lat"].ToString());
            }
            SetBound(xmin, ymin, xmax, ymax);
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
            SqlCommandBuilder sqlCommand3 = null;
            SqlDataAdapter sqlAdapter3 = null;
            System.Data.DataTable datatable3 = new System.Data.DataTable();
            string sql_oluoi =
                "SELECT " +
                "gid, o_id, " +
                "long_corner1, lat_corner1, " +
                "long_corner2, lat_corner2, " +
                "long_corner3, lat_corner3, " +
                "long_corner4, lat_corner4 " +
                "FROM OLuoi where cecm_program_areamap_id = " + idKV;
            sqlAdapter3 = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
            sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
            sqlAdapter3.Fill(datatable3);
            //axMap1.ClearDrawing(oluoiLayer);
            oluoiLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //Console.WriteLine("axMap1.MoveLayerBottom(oluoiLayer): " + axMap1.MoveLayerBottom(oluoiLayer));
            foreach (DataRow dr in datatable3.Rows)
            {
                double long_corner1 = double.Parse(dr["long_corner1"].ToString());
                double long_corner2 = double.Parse(dr["long_corner2"].ToString());
                double long_corner3 = double.Parse(dr["long_corner3"].ToString());
                double long_corner4 = double.Parse(dr["long_corner4"].ToString());
                double lat_corner1 = double.Parse(dr["lat_corner1"].ToString());
                double lat_corner2 = double.Parse(dr["lat_corner2"].ToString());
                double lat_corner3 = double.Parse(dr["lat_corner3"].ToString());
                double lat_corner4 = double.Parse(dr["lat_corner4"].ToString());
                drawOluoi(lat_corner1, long_corner1, lat_corner2, long_corner2, lat_corner3, long_corner3, lat_corner4, long_corner4);
                List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "cecm_program_area_line", "cecmprogramareasub_id", dr["gid"].ToString());
                //List<CecmProgramAreaLineDTO> lstRanhDo = new List<CecmProgramAreaLineDTO>();
                int index = 0;
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
                        line.start_x = lattStart;
                        line.start_y = longtStart;
                        line.end_x = lattEnd;
                        line.end_y = longtEnd;
                        //lstRanhDo.Add(line);
                        index++;
                        axMap1.DrawLineEx(lineLayer, line.start_y, line.start_x, line.end_y, line.end_x, 1, AppUtils.ColorToUint(Color.White));
                        if (index % 2 == 1)
                        {
                            axMap1.DrawLabelEx(lineLayer, index.ToString(), line.start_y, line.start_x, 0);
                        }

                    }

                }
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
            axMap1.ClearDrawing(oluoiLayer);
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new DataTable();
            string sql = "SELECT " +
                "id " +
                "FROM cecm_program_area_map where cecm_program_id = " + idDA;
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                long.TryParse(dr["id"].ToString(), out long idKV);
                LoadKhuVuc(idKV, idDA);
            }
        }

        private static void drawOluoi(
            double lat_corner1, double long_corner1,
            double lat_corner2, double long_corner2,
            double lat_corner3, double long_corner3,
            double lat_corner4, double long_corner4)
        {
            axMap1.DrawLineEx(oluoiLayer, long_corner1, lat_corner1, long_corner2, lat_corner2, 5, AppUtils.ColorToUint(Color.White));
            axMap1.DrawLineEx(oluoiLayer, long_corner2, lat_corner2, long_corner3, lat_corner3, 5, AppUtils.ColorToUint(Color.White));
            axMap1.DrawLineEx(oluoiLayer, long_corner3, lat_corner3, long_corner4, lat_corner4, 5, AppUtils.ColorToUint(Color.White));
            axMap1.DrawLineEx(oluoiLayer, long_corner4, lat_corner4, long_corner1, lat_corner1, 5, AppUtils.ColorToUint(Color.White));
        }

        public static void LoadPoints(Dictionary<long, Dictionary<string, List<InfoConnect>>> idKV__Points)
        {

        }
    }
}
