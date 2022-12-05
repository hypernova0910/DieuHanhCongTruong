using CoordinateSharp;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;
using Image = MapWinGIS.Image;
using Point = MapWinGIS.Point;
using Shape = MapWinGIS.Shape;

namespace VNRaPaBomMin
{
    public partial class DanhSachBMVN : Form
    {
        //private SqlConnection _Cn = null;

        private AxMapWinGIS._DMapEvents_MouseUpEventHandler mouseUpHandler = null;
        private List<AxMapWinGIS._DMapEvents_MouseUpEventHandler> pointHandlers;

        private AxMapWinGIS._DMapEvents_MouseDownEvent mouseDownGlobal = null;
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;

        private int suspectPointLayer = -1;
        private int highlightLayer = -1;
        private int labelLayer = -1;
        private int oluoiLayer = -1;
        private int lineLayer = -1;

        //private int DACount = 0;
        //private int KVCount = 0;
        //private int OLCount = 0;

        public DanhSachBMVN()
        {
            InitializeComponent();
            axMap1.MapCursor = tkCursor.crsrHand;
            axMap1.SendMouseDown = true;
            //axMap1.MouseDownEvent += new AxMapWinGIS._DMapEvents_MouseDownEventHandler((sender3, e3) =>
            //{
            //    MessageBox.Show("mouse down");
            //});
            pointHandlers = new List<AxMapWinGIS._DMapEvents_MouseUpEventHandler>();
            //_Cn = _Cn = frmLoggin.sqlCon;
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
        }

        private Image OpenImage(string path)
        {
            //string path = @"../../data/marker.png";
            if (!File.Exists(path))
            {
                MessageBox.Show("Can't find the file: " + path);
            }
            else
            {
                Image img = new Image();
                if (!img.Open(path, ImageType.USE_FILE_EXTENSION))
                {
                    MessageBox.Show(img.ErrorMsg[img.LastErrorCode]);
                    img.Close();
                }
                else
                    return img;
            }
            return null;
        }

        private void loadImage(string path, double xmax, double xmin, double ymax, double ymin)
        {
            Image img = OpenImage(path);
            axMap1.AddLayer(img, true);
            //Image img = axMap1.get_Image(imageLayer);
            //img.Open(path, ImageType.USE_FILE_EXTENSION);
            //Image img = OpenImage(path);
            img.OriginalDX = (xmax - xmin) / img.OriginalWidth;
            img.OriginalDY = (ymax - ymin) / img.OriginalHeight;
            //MessageBox.Show("width = " + img.OriginalDX + " / height = " + img.OriginalDY);
            //img.ProjectionToImage(xmin, ymin, out int x, out int y);
            //img.SetVisibleExtents(xmin, ymin, xmax, ymax, 500, 10);
            img.OriginalXllCenter = xmin;
            img.OriginalYllCenter = ymin;
            //axMap1.set_Image(imageLayer, img);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btThem_Click(object sender, EventArgs e)
        {

        }

        private void DanhSachBMVN_Load(object sender, EventArgs e)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, name FROM cecm_programData"), frmLoggin.sqlCon);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            //DACount = datatable.Rows.Count;
            DataRow dr2 = datatable.NewRow();
            dr2["id"] = -1;
            dr2["name"] = "Chọn dự án";
            datatable.Rows.InsertAt(dr2, 0);
            cbDA.DataSource = datatable;
            cbDA.DisplayMember = "name";
            cbDA.ValueMember = "id";
            LoadImages();
            lineLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            InitOLuoiRanhDo();
            InitSuspectPointLayer();
            LoadData();
            InitHighlightLayer();
            labelLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        }


        private void InitOLuoiRanhDo()
        {
            SqlDataAdapter sqlAdapter3 = null;
            System.Data.DataTable datatable3 = new System.Data.DataTable();
            string sql_oluoi =
                "SELECT " +
                "gid, o_id, " +
                "long_corner1, lat_corner1, " +
                "long_corner2, lat_corner2, " +
                "long_corner3, lat_corner3, " +
                "long_corner4, lat_corner4 " +
                "FROM OLuoi";
            sqlAdapter3 = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
            sqlAdapter3.Fill(datatable3);
            //axMap1.ClearDrawing(oluoiLayer);
            oluoiLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
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
                List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "cecm_program_area_line", "cecmprogramareasub_id", dr["gid"].ToString());
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
            
        }

        private void drawOluoi(
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

        private void InitSuspectPointLayer()
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
                return;
            }
            suspectPointLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "marker.png");
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitHighlightLayer()
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
                return;
            }
            highlightLayer = axMap1.AddLayer(sf, true);
            ShapeDrawingOptions options = sf.DefaultDrawingOptions;
            options.PointType = tkPointSymbolType.ptSymbolPicture;
            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "highlight.png");
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public Shape addSuspectPoint(Vertex vertex)
        {
            Shapefile sf = axMap1.get_Shapefile(suspectPointLayer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POINT);
            Point pnt = new Point();
            //axMap1.PixelToProj(x, y, ref x, ref y);
            pnt.x = vertex.X;
            pnt.y = vertex.Y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);

            //index = sf.NumShapes;
            //if (!sf.EditInsertShape(shp, ref index))
            //{
            //    MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
            //    return -1;
            //}
            int indexShp = sf.EditAddShape(shp);
            
            axMap1.Redraw();
            //return indexShp;
            return shp;
        }

        public void removeSuspectPoint(Shape shape)
        {
            Shapefile sf = axMap1.get_Shapefile(suspectPointLayer);
            //Shape shp = new Shape();
            //shp.Create(ShpfileType.SHP_POINT);
            //Point pnt = new Point();
            ////axMap1.PixelToProj(x, y, ref x, ref y);
            //pnt.x = vertex.X;
            //pnt.y = vertex.Y;
            //int index = shp.numPoints;
            //shp.InsertPoint(pnt, ref index);

            //int index = sf.NumShapes;
            //if (!sf.EditDeleteShape(shapeIndex))
            //{
            //    MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
            //    return;
            //}
            shape.Clear();
            axMap1.Redraw();
        }

        public void addHighlight(double x, double y)
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
                return;
            }
            axMap1.Redraw();
        }

        private void resetPoints()
        {
            axMap1.RemoveLayer(suspectPointLayer);
            InitSuspectPointLayer();
            axMap1.RemoveLayer(highlightLayer);
            InitHighlightLayer();
            axMap1.ClearDrawingLabels(labelLayer);
        }

        private void LoadDACount()
        {
            if ((long)cbDA.SelectedValue < 0)
            {
                DataTable datatable = new DataTable();
                datatable.Clear();
                string sql =
                @"SELECT COUNT(tbl.id) as DACount FROM cecm_programData tbl WHERE 1 = 1 ";
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
                sqlAdapter.Fill(datatable);
                foreach (DataRow dr in datatable.Rows)
                {
                    lblDACount.Text = dr["DACount"].ToString();
                }
            }
            else
            {
                lblDACount.Text = "1";
            }
        }

        private void LoadKVCount()
        {
            if ((long)cbKhuVuc.SelectedValue < 0)
            {
                DataTable datatable = new DataTable();
                datatable.Clear();
                string sql =
                @"SELECT COUNT(tbl.id) as KVCount FROM cecm_program_area_map tbl WHERE 1 = 1 ";
                if ((long)cbDA.SelectedValue > 0)
                {
                    sql += "AND tbl.cecm_program_id = @programId";
                }
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
                if ((long)cbDA.SelectedValue > 0)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "programId",
                        Value = cbDA.SelectedValue,
                        SqlDbType = SqlDbType.BigInt
                    });
                }
                
                sqlAdapter.Fill(datatable);
                foreach (DataRow dr in datatable.Rows)
                {
                    lblKVCount.Text = dr["KVCount"].ToString();
                }
            }
            else
            {
                lblKVCount.Text = "1";
            }
        }

        private void LoadOLCount()
        {
            if ((long)cbOLuoi.SelectedValue < 0)
            {
                DataTable datatable = new DataTable();
                datatable.Clear();
                string sql =
                @"SELECT COUNT(tbl.gid) as OLCount FROM OLuoi tbl WHERE 1 = 1 ";
                if ((long)cbDA.SelectedValue > 0)
                {
                    sql += "AND tbl.cecm_program_id = @programId ";
                    if ((long)cbKhuVuc.SelectedValue > 0)
                    {
                        sql += "AND tbl.cecm_program_areamap_id = @cecm_program_areamap_id ";
                    }
                }
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
                if ((long)cbDA.SelectedValue > 0)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "programId",
                        Value = cbDA.SelectedValue,
                        SqlDbType = SqlDbType.BigInt
                    });
                    if ((long)cbKhuVuc.SelectedValue > 0)
                    {
                        sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "cecm_program_areamap_id",
                            Value = cbKhuVuc.SelectedValue,
                            SqlDbType = SqlDbType.BigInt
                        });
                    }
                }

                sqlAdapter.Fill(datatable);
                foreach (DataRow dr in datatable.Rows)
                {
                    lblOLCount.Text = dr["OLCount"].ToString();
                }
            }
            else
            {
                lblOLCount.Text = "1";
            }
        }

        private void LoadStatistic()
        {
            LoadDACount();
            LoadKVCount();
            LoadOLCount();
        }

        private void LoadData()
        {
            try
            {
                resetPoints();
                LoadStatistic();
                foreach (AxMapWinGIS._DMapEvents_MouseUpEventHandler handler in pointHandlers)
                {
                    axMap1.MouseUpEvent -= handler;
                }
                pointHandlers.Clear();
                SqlDataAdapter sqlAdapter = null;

                if (dgvBMVN.Rows.Count != 0)
                    dgvBMVN.Rows.Clear();

                System.Data.DataTable datatable = new System.Data.DataTable();
                datatable.Clear();
                string sql =
                @"SELECT 
                tbl.id, 
                pro.name as programIdST, 
                CONCAT(area_map.code, ' - ', area_map.address) as idAreaST,
                ol.o_id as idRectangleST,
                Kinhdo, Vido, ZPoint, Deep,
                CONCAT(tbl.XPoint, ', ', tbl.YPoint) as position,
                FORMAT(tbl.TimeExecute, 'HH:mm:ss dd/MM/yyyy') as timeST
                FROM Cecm_VNTerrainMinePoint tbl
                left join cecm_programData pro on (pro.id = tbl.programId)
                left join cecm_program_area_map area_map on (area_map.id = tbl.idArea)
                left join OLuoi ol on (ol.gid = tbl.idRectangle) 
                WHERE 1 = 1 ";
                if((long)cbDA.SelectedValue > 0)
                {
                    sql += "AND tbl.programId = @programId ";
                    if((long)cbKhuVuc.SelectedValue > 0)
                    {
                        sql += "AND tbl.idArea = @idArea ";
                        if ((long)cbOLuoi.SelectedValue > 0)
                        {
                            sql += "AND tbl.idRectangle = @idRectangle ";
                        }
                    }
                }
                if (dtpTimeStart.Checked)
                {
                    sql += "AND tbl.TimeExecute >= @timeStart ";
                }
                if (dtpTimeEnd.Checked)
                {
                    sql += "AND tbl.TimeExecute <= @timeEnd ";
                }
                sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
                if ((long)cbDA.SelectedValue > 0)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "programId",
                        Value = cbDA.SelectedValue,
                        SqlDbType = SqlDbType.BigInt
                    });
                    if ((long)cbKhuVuc.SelectedValue > 0)
                    {
                        sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "idArea",
                            Value = cbKhuVuc.SelectedValue,
                            SqlDbType = SqlDbType.BigInt
                        });
                        if ((long)cbOLuoi.SelectedValue > 0)
                        {
                            sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "idRectangle",
                                Value = cbOLuoi.SelectedValue,
                                SqlDbType = SqlDbType.BigInt
                            });
                        }
                    }
                }
                if (dtpTimeStart.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "timeStart",
                        Value = dtpTimeStart.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }
                if (dtpTimeEnd.Checked)
                {
                    sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "timeEnd",
                        Value = dtpTimeEnd.Value,
                        SqlDbType = SqlDbType.DateTime
                    });
                }

                sqlAdapter.Fill(datatable);
                lblBMVNCount.Text = datatable.Rows.Count.ToString();
                if (datatable.Rows.Count != 0)
                {
                    int count = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        string linhVucHoatDong = string.Empty;
                        //if (AppUtils.IsNumber(dr["action_type"].ToString()))
                        //{
                        //    int action_type = int.Parse(dr["action_type"].ToString());
                        //    linhVucHoatDong = FindLinhVuc(action_type);
                        //}
                        //Coordinate coordinate = new Coordinate(double.Parse(dr["YPoint"].ToString()), double.Parse(dr["XPoint"].ToString()));
                        Vertex vertex = new Vertex(double.Parse(dr["Kinhdo"].ToString()), double.Parse(dr["Vido"].ToString()), double.Parse(dr["ZPoint"].ToString()));
                        vertex.depth = double.Parse(dr["Deep"].ToString());
                        //dgvBMVN.Rows[i].Tag = dr["id"].ToString();
                        
                        Shape shape = addSuspectPoint(vertex);
                        vertex.shape = shape;
                        int i = dgvBMVN.Rows.Add(
                            count,
                            dr["id"].ToString(),
                            dr["programIdST"].ToString(),
                            dr["idAreaST"].ToString(),
                            dr["idRectangleST"].ToString(),
                            dr["position"].ToString(),
                            dr["timeST"].ToString()
                            );
                        dgvBMVN.Rows[i].Tag = vertex;

                        count++;
                    }
                }

                //zoom đến vùng dự án
                zoomToVungDA();

                //zoom đến ô lưới
                zoomToOLuoi();
            }
            catch(Exception ex)
            {

            }
            
        }

        private void LoadImages()
        {
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new System.Data.DataTable();
            string sql = "SELECT " +
                "position_lat, position_long, " +
                "photo_file, " +
                "cecm_program_id, " +
                "left_long, right_long, bottom_lat, top_lat " +
                "FROM cecm_program_area_map";
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            string photoFileName = "unknown.png";
            double xmax = 0, xmin = 0, ymax = 0, ymin = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                //axMap1.SetLatitudeLongitude(double.Parse(dr["position_lat"].ToString()), double.Parse(dr["position_long"].ToString()));
                photoFileName = dr["photo_file"].ToString();
                xmin = double.Parse(dr["left_long"].ToString());
                xmax = double.Parse(dr["right_long"].ToString());
                ymin = double.Parse(dr["bottom_lat"].ToString());
                ymax = double.Parse(dr["top_lat"].ToString());
                if (int.TryParse(dr["cecm_program_id"].ToString(), out int programId))
                {
                    string pathTemp = AppUtils.GetFolderTemp(programId);
                    string fullPath = Path.Combine(pathTemp, photoFileName);
                    if (File.Exists(fullPath))
                    {
                        loadImage(fullPath, xmax, xmin, ymax, ymin);
                    }
                }
                Extents extents = new Extents();
                extents.SetBounds(xmin, ymin, 0, xmax, ymax, 0);
                axMap1.Extents = extents;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbDA_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name, polygongeomst FROM cecm_program_area_map where cecm_program_id = " + cbDA.SelectedValue), frmLoggin.sqlCon);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                //if ((long)cbDA.SelectedValue > 0)
                //{
                //    lblDACount.Text = "1";
                //}
                //else
                //{
                //    lblDACount.Text = DACount.ToString();
                //}
                DataRow dr2 = datatable.NewRow();
                dr2["id"] = -1;
                dr2["name"] = "Chưa chọn vùng dự án";
                datatable.Rows.InsertAt(dr2, 0);
                cbKhuVuc.DataSource = datatable;
                cbKhuVuc.DisplayMember = "name";
                cbKhuVuc.ValueMember = "id";
            }catch(Exception ex)
            {

            }
            
        }

        private void cbKhuVuc_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder sqlCommand3 = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable3 = new System.Data.DataTable();
                string sql_oluoi =
                    "SELECT " +
                    "gid, o_id " +
                    "FROM OLuoi where cecm_program_areamap_id = " + cbKhuVuc.SelectedValue;
                sqlAdapter = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
                sqlCommand3 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable3);
                DataRow dr2 = datatable3.NewRow();
                dr2["gid"] = -1;
                dr2["o_id"] = "Chưa chọn ô lưới";
                datatable3.Rows.InsertAt(dr2, 0);
                cbOLuoi.DataSource = datatable3;
                cbOLuoi.DisplayMember = "o_id";
                cbOLuoi.ValueMember = "gid";
            }
            catch (Exception ex)
            {
            }
        }

        private void zoomToVungDA()
        {
            try
            {
                if ((long)cbKhuVuc.SelectedValue < 0)
                {
                    return;
                }
                SqlDataAdapter sqlAdapter = null;
                DataTable datatable = new System.Data.DataTable();
                string sql = "SELECT " +
                    "position_lat, position_long, " +
                    //"photo_file, " +
                    "cecm_program_id, " +
                    "left_long, right_long, bottom_lat, top_lat " +
                    "FROM cecm_program_area_map WHERE id = " + cbKhuVuc.SelectedValue;
                sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
                sqlAdapter.Fill(datatable);
                //string photoFileName = "unknown.png";
                double xmax = 0, xmin = 0, ymax = 0, ymin = 0;
                foreach (DataRow dr in datatable.Rows)
                {
                    //axMap1.SetLatitudeLongitude(double.Parse(dr["position_lat"].ToString()), double.Parse(dr["position_long"].ToString()));
                    //photoFileName = dr["photo_file"].ToString();
                    xmin = double.Parse(dr["left_long"].ToString());
                    xmax = double.Parse(dr["right_long"].ToString());
                    ymin = double.Parse(dr["bottom_lat"].ToString());
                    ymax = double.Parse(dr["top_lat"].ToString());
                    //if (int.TryParse(dr["cecm_program_id"].ToString(), out int programId))
                    //{
                    //    string pathTemp = AppUtils.GetFolderTemp(programId);
                    //    string fullPath = Path.Combine(pathTemp, photoFileName);
                    //    if (File.Exists(fullPath))
                    //    {
                    //        loadImage(fullPath, xmax, xmin, ymax, ymin);
                    //    }
                    //}
                    Extents extents = new Extents();
                    extents.SetBounds(xmin, ymin, 0, xmax, ymax, 0);
                    axMap1.Extents = extents;
                }
            }
            catch (Exception)
            {

            }
        }

        private void zoomToOLuoi()
        {
            try
            {
                if((long)cbOLuoi.SelectedValue < 0)
                {
                    return;
                }
                DataTable datatable = new DataTable();
                string sql =
                    "SELECT " +
                    "lat_corner1, lat_corner2, lat_corner3, lat_corner4, " +
                    "long_corner1, long_corner2, long_corner3, long_corner4, " +
                    "long_center, lat_center, " +
                    "distanceAllGrid " +
                    "FROM OLuoi where gid = " + cbOLuoi.SelectedValue;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    DataRow dr = datatable.Rows[0];
                    List<double> lstLat = new List<double>();
                    lstLat.Add(double.Parse(dr["lat_corner1"].ToString()));
                    lstLat.Add(double.Parse(dr["lat_corner2"].ToString()));
                    lstLat.Add(double.Parse(dr["lat_corner3"].ToString()));
                    lstLat.Add(double.Parse(dr["lat_corner4"].ToString()));
                    List<double> lstLong = new List<double>();
                    lstLong.Add(double.Parse(dr["long_corner1"].ToString()));
                    lstLong.Add(double.Parse(dr["long_corner2"].ToString()));
                    lstLong.Add(double.Parse(dr["long_corner3"].ToString()));
                    lstLong.Add(double.Parse(dr["long_corner4"].ToString()));
                    double minLat = lstLat.Min();
                    double maxLat = lstLat.Max();
                    double minLong = lstLong.Min();
                    double maxLong = lstLong.Max();
                    Extents extents = new Extents();
                    extents.SetBounds(minLong, minLat - 0.0001, 0, maxLong, maxLat + 0.0001, 0);
                    axMap1.Extents = extents;
                }
            }
            catch (Exception)
            {

            }
        }

        private void cbOLuoi_SelectedValueChanged(object sender, EventArgs e)
        {
            //zoomToOLuoi();
        }

        private void dgvBMVN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            var dgvRow = dgvBMVN.Rows[e.RowIndex];
            //if (dgvRow.Tag == null)
            //    return;
            string str = dgvRow.Cells[cotIDHidden.Index].Value as string;
            int id = int.Parse(str);
            //if(e.RowIndex == 0)
            //{
            //    return;
            //}
            if (e.ColumnIndex == cotXoa.Index)
            {
                if (MessageBox.Show("Xác nhận xóa dữ liệu", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
                        SqlTransaction transaction = _ExtraInfoConnettion.BeginTransaction() as SqlTransaction;
                        try
                        {
                            SqlCommand cmd = new SqlCommand(string.Format("USE [{0}] DELETE FROM Cecm_VNTerrainMinePoint WHERE Cecm_VNTerrainMinePoint.id = {1};", frmLoggin.databaseName, id), cn, transaction);
                            int susscec1 = cmd.ExecuteNonQuery();
                            if (susscec1 < 0)
                            {
                                MessageBox.Show(string.Format("Không thể xóa"));
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show(ex.Message);
                            return;
                        }

                        transaction.Commit();

                        dgvBMVN.Rows.RemoveAt(e.RowIndex);
                        Vertex vertex = dgvRow.Tag as Vertex;
                        removeSuspectPoint(vertex.shape);
                    }
                    catch (System.Exception ex)
                    {
                        
                    }
                }
            }
            else
            {
                //foreach(DataGridCell cell in dgvBMVN.Rows[e.RowIndex].Cells)
                //{
                //    cell
                //}
                dgvBMVN.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dtpTimeStart_ValueChanged(object sender, EventArgs e)
        {
            //if(dtpTimeEnd.Value < dtpTimeStart.Value)
            //{
            //    dtpTimeEnd.Value = dtpTimeStart.Value;
            //}
        }

        private void dtpTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpTimeEnd.Value < dtpTimeStart.Value)
            //{
            //    dtpTimeStart.Value = dtpTimeEnd.Value;
            //}
        }

        private void dgvBMVN_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected)
            {
                return;
            }
            if(dgvBMVN.SelectedRows.Count > 0)
            {
                //dgvBMVN.SelectedRows[0]
                Vertex vertex = dgvBMVN.SelectedRows[0].Tag as Vertex;
                axMap1.ClearDrawingLabels(labelLayer);
                axMap1.RemoveLayer(highlightLayer);
                InitHighlightLayer();
                //axMap1.SetLatitudeLongitude(latt, longt);
                //axMap1.ZoomToTileLevel(25);
                Extents extents = new Extents();
                extents.SetBounds(vertex.X - 0.00001, vertex.Y - 0.00001, 0, vertex.X + 0.00001, vertex.Y + 0.00001, 0);
                axMap1.Extents = extents;
                addHighlight(vertex.X, vertex.Y);
                //labels.Visible = true;
                //Labels labels = axMap1.get_DrawingLabels(labelLayer);
                Labels labels = new Labels();
                labels.FontSize = 12;
                string labelText = string.Format(
                    "Kinh độ: {0}\n" +
                    "Vĩ độ: {1}\n" +
                    "Độ sâu: {2}m\n" +
                    "Cường độ từ trường: {3}",
                    Math.Round(vertex.X, 6), Math.Round(vertex.Y, 6), Math.Round(vertex.depth, 6), Math.Round(vertex.Z, 6)
                );
                double pxX = 0, pxY = 0;
                //axMap1.ProjToPixel(longt, latt, ref pxX, ref pxY);
                //axMap1.PixelToProj(pxX + 10, pxY + 10, ref longt, ref latt);
                labels.AddLabel(labelText, vertex.X, vertex.Y);
                labels.Visible = true;
                labels.Alignment = tkLabelAlignment.laBottomRight;
                labels.OffsetX = 20;
                labels.OffsetY = 20;
                labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
                labels.AvoidCollisions = true;

                axMap1.set_DrawingLabels(labelLayer, labels);
                if (mouseUpHandler != null)
                {
                    axMap1.MouseUpEvent -= mouseUpHandler;
                }

                mouseUpHandler = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((sender3, e3) =>
                {
                    //if(mouseDownGlobal == null)
                    //{
                    //    return;
                    //}
                    //if(mouseDownGlobal.x != e3.x || mouseDownGlobal.y != e3.y)
                    //{
                    //    return;
                    //}
                    if (labels.get_Label(0, 0) != null)
                    {
                        Extents ext = labels.get_Label(0, 0).ScreenExtents;
                        if (ext != null)
                        {
                            if (e3.x < ext.xMin || e3.x > ext.xMax || e3.y < ext.yMin || e3.y > ext.yMax)
                            {
                                labels.Visible = false;
                                axMap1.RemoveLayer(highlightLayer);
                                InitHighlightLayer();
                            }
                        }
                    }
                });
                axMap1.MouseUpEvent += mouseUpHandler;
                //axMap1.Redraw();
            }
        }

        private void axMap1_MouseDownEvent(object sender, AxMapWinGIS._DMapEvents_MouseDownEvent e)
        {
            //MessageBox.Show("mouse down");
            //mouseDownGlobal = e;
        }
    }
}
