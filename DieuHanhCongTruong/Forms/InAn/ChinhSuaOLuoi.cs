using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Models;
using DieuHanhCongTruong.Properties;
using DieuHanhCongTruong.UDP;
using MapWinGIS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.Forms.InAn
{
    public partial class ChinhSuaOLuoi : Form
    {
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;
        private bool btnLuuClicked = false;
        private bool loaded = false;

        private List<CecmProgramAreaLineDTO> lstRanhDo = new List<CecmProgramAreaLineDTO>();

        private int imageLayer = -1;
        private int lineLayer = -1;
        private int oLuoiLayer = -1;

        //private UdpUser socketclient = UdpUser.ConnectTo("127.0.0.1", 32124);

        private double NuaDoDaiCanh = 25;
        //private long id;
        public OLuoi oLuoi;

        public ChinhSuaOLuoi(OLuoi oLuoi)
        {
            InitializeComponent();
            this.oLuoi = oLuoi;
            rb_4goc.Checked = true;
            rb_toanO_truc2.Checked = true;
            pnlToanO.Visible = false;
            pnlToanO.Size = new System.Drawing.Size(1764, 0);
            this.pnlToanO.Location = new System.Drawing.Point(3, 1080);
            initLayers();
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            //if (_ExtraInfoConnettion == null)
            //    return;
        }

        private void initLayers()
        {
            //initOLuoiLayer();
            //initLineLayer();
            oLuoiLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            lineLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        }

        private void initOLuoiLayer()
        {
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            oLuoiLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(oLuoiLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                return;
            }
            //sf.SelectionAppearance = tkSelectionAppearance.saSelectionColor;
            oLuoiLayer = axMap1.AddLayer(sf, true);
        }

        private void initLineLayer()
        {
            Shapefile sf = new Shapefile();
            //sf.Open(filename, null);
            lineLayer = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(lineLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            sf = new Shapefile();
            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
            {
                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                return;
            }
            //sf.SelectionAppearance = tkSelectionAppearance.saSelectionColor;
            lineLayer = axMap1.AddLayer(sf, true);
        }

        private void getCurrentDataOLuoi()
        {
            //oLuoi.cecm_program_id = oLuoi.cecm_program_id;
            //oLuoi.cecm_program_areamap_ID = oLuoi.cecm_program_areamap_ID;
            //oLuoi.gid = oLuoi.gid;
            oLuoi.o_id = tb_o_id.Text;
            oLuoi.autoDivide = cbTuDongChia.SelectedIndex;
            oLuoi.khaosat_deptId = (long)cbDVKhaoSat.SelectedValue;
            oLuoi.raPha_deptId = (long)cbDVRaPha.SelectedValue;
            oLuoi.distanceAllGrid = (double)nud_distanceAll.Value;
            if (rb_4goc.Checked)
            {
                oLuoi.dividerAllGrid = 1;
            }
            else if (rb_toanOLuoi.Checked)
            {
                oLuoi.dividerAllGrid = 2;
            }
            if (rb_toanO_truc1.Checked)
            {
                oLuoi.isCustomAllGrid = 1;
            }
            else if (rb_toanO_truc2.Checked)
            {
                oLuoi.isCustomAllGrid = 2;
            }
            else if (rb_toanO_truc3.Checked)
            {
                oLuoi.isCustomAllGrid = 3;
            }
            if (nud_angleAll.ReadOnly)
            {
                oLuoi.acutangeAllGrid = 0;
            }
            else
            {
                oLuoi.acutangeAllGrid = (double)nud_angleAll.Value;
            }
            oLuoi.long_center = (double)nud_long_center.Value;
            oLuoi.long_corner1 = (double)nud_long_corner1.Value;
            oLuoi.long_corner2 = (double)nud_long_corner2.Value;
            oLuoi.long_corner3 = (double)nud_long_corner3.Value;
            oLuoi.long_corner4 = (double)nud_long_corner4.Value;
            oLuoi.lat_center = (double)nud_lat_center.Value;
            oLuoi.lat_corner1 = (double)nud_lat_corner1.Value;
            oLuoi.lat_corner2 = (double)nud_lat_corner2.Value;
            oLuoi.lat_corner3 = (double)nud_lat_corner3.Value;
            oLuoi.lat_corner4 = (double)nud_lat_corner4.Value;
            //Góc 1
            oLuoi.distance1 = (double)nud_distance1.Value;
            //Chia theo góc lệch
            if (rb_goc1_truc1.Checked)
            {
                oLuoi.isCustom1 = 1;
            }
            //Chia theo Đông - Tây
            else if (rb_goc1_truc2.Checked)
            {
                oLuoi.isCustom1 = 2;
            }
            //Chia theo Bắc - Nam
            else if (rb_goc1_truc3.Checked)
            {
                oLuoi.isCustom1 = 3;
            }
            else
            {
                oLuoi.isCustom1 = 0;
            }
            if (nud_angle1.ReadOnly)
            {
                oLuoi.acuteAngle1 = 0;
            }
            else
            {
                oLuoi.acuteAngle1 = (double)nud_angle1.Value;
            }
            //Góc 2
            oLuoi.distance2 = (double)nud_distance2.Value;
            if (rb_goc2_truc1.Checked)
            {
                oLuoi.isCustom2 = 1;
            }
            else if (rb_goc2_truc2.Checked)
            {
                oLuoi.isCustom2 = 2;
            }
            else if (rb_goc2_truc3.Checked)
            {
                oLuoi.isCustom2 = 3;
            }
            else
            {
                oLuoi.isCustom2 = 0;
            }
            if (nud_angle2.ReadOnly)
            {
                oLuoi.acuteAngle2 = 0;
            }
            else
            {
                oLuoi.acuteAngle2 = (double)nud_angle2.Value;
            }
            //Góc 3
            oLuoi.distance3 = (double)nud_distance3.Value;
            if (rb_goc3_truc1.Checked)
            {
                oLuoi.isCustom3 = 1;
            }
            else if (rb_goc3_truc2.Checked)
            {
                oLuoi.isCustom3 = 2;
            }
            else if (rb_goc3_truc3.Checked)
            {
                oLuoi.isCustom3 = 3;
            }
            else
            {
                oLuoi.isCustom3 = 0;
            }
            if (nud_angle3.ReadOnly)
            {
                oLuoi.acuteAngle3 = 0;
            }
            else
            {
                oLuoi.acuteAngle3 = (double)nud_angle3.Value;
            }
            //Góc 4
            oLuoi.distance4 = (double)nud_distance4.Value;
            if (rb_goc4_truc1.Checked)
            {
                oLuoi.isCustom4 = 1;
            }
            else if (rb_goc4_truc2.Checked)
            {
                oLuoi.isCustom4 = 2;
            }
            else if (rb_goc4_truc3.Checked)
            {
                oLuoi.isCustom4 = 3;
            }
            else
            {
                oLuoi.isCustom4 = 0;
            }
            if (nud_angle4.ReadOnly)
            {
                oLuoi.acuteAngle4 = 0;
            }
            else
            {
                oLuoi.acuteAngle4 = (double)nud_angle4.Value;
            }
            //Rãnh dò
            oLuoi.lstRanhDo = lstRanhDo;
        }

        private void redrawLines(bool redrawBorder = false)
        {
            if (!loaded || NuaDoDaiCanh == 0)
            {
                return;
            }
            //btLuu.Enabled = false;
            //MessageBox.Show("Redraw");
            //DateTime start2 = DateTime.Now;
            getCurrentDataOLuoi();
            //DateTime end2 = DateTime.Now;
            //MessageBox.Show("Lấy dữ liệu: " + (end2 - start2).TotalMilliseconds.ToString());

            //DateTime start = DateTime.Now;

            //string json = JsonConvert.SerializeObject(ol);
            //socketclient.Send(json);
            //Task.Factory.StartNew(async () =>
            //{
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            //    try
            //    {
            //        var received = await socketclient.Receive();
            //        axMap1.ClearDrawing(lineLayer);
            //        lineLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //        lstRanhDo = JsonConvert.DeserializeObject<List<CecmProgramAreaLineDTO>>(received.Message);
            //        foreach (CecmProgramAreaLineDTO line in lstRanhDo)
            //        {
            //            axMap1.DrawLineEx(lineLayer, line.start_y, line.start_x, line.end_y, line.end_x, 1, AppUtils.ColorToUint(Color.White));
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //});

            axMap1.ClearDrawing(lineLayer);
            lineLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            lstRanhDo = AutoDivide.AutoDividerRanhDoView(oLuoi, lineLayer, axMap1);
            foreach (CecmProgramAreaLineDTO lineDTO in lstRanhDo)
            {
                axMap1.DrawLineEx(lineLayer, lineDTO.start_x, lineDTO.start_y, lineDTO.end_x, lineDTO.end_y, 1, AppUtils.ColorToUint(Color.White));
            }
            if (redrawBorder)
            {
                //ol = getCurrentDataOLuoi();
                axMap1.ClearDrawing(oLuoiLayer);
                oLuoiLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                axMap1.DrawLineEx(oLuoiLayer, (double)nud_long_corner1.Value, (double)nud_lat_corner1.Value, (double)nud_long_corner2.Value, (double)nud_lat_corner2.Value, 5, AppUtils.ColorToUint(Color.White));
                axMap1.DrawLineEx(oLuoiLayer, (double)nud_long_corner2.Value, (double)nud_lat_corner2.Value, (double)nud_long_corner3.Value, (double)nud_lat_corner3.Value, 5, AppUtils.ColorToUint(Color.White));
                axMap1.DrawLineEx(oLuoiLayer, (double)nud_long_corner3.Value, (double)nud_lat_corner3.Value, (double)nud_long_corner4.Value, (double)nud_lat_corner4.Value, 5, AppUtils.ColorToUint(Color.White));
                axMap1.DrawLineEx(oLuoiLayer, (double)nud_long_corner4.Value, (double)nud_lat_corner4.Value, (double)nud_long_corner1.Value, (double)nud_lat_corner1.Value, 5, AppUtils.ColorToUint(Color.White));
                Extents extents = new Extents();
                extents.SetBounds((double)nud_long_corner4.Value, (double)nud_lat_corner4.Value - 0.0001, 0, (double)nud_long_corner2.Value, (double)nud_lat_corner2.Value + 0.0001, 0);
                axMap1.Extents = extents;
            }
            //foreach (CecmProgramAreaLineDTO line in lstRanhDo)
            //{
            //    axMap1.DrawLineEx(lineLayer, line.start_y, line.start_x, line.end_y, line.end_x, 1, AppUtils.ColorToUint(Color.White));
            //}
            //DateTime end = DateTime.Now;
            //MessageBox.Show("Vẽ: " + (end - start).TotalMilliseconds.ToString());
            //btLuu.Enabled = true;
        }

        private void LoadCombobox()
        {
            cbTuDongChia.SelectedIndex = 0;
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            DataTable datatable = new DataTable();
            string sql_oLuoi =
                "select id, name from cert_department";
            //frmLoggin.sqlCon.Open();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql_oLuoi, cn);
            sqlAdapter.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            //frmLoggin.sqlCon.BeginTransaction();
            sqlAdapter.Fill(datatable);
            DataRow dr = datatable.NewRow();
            dr["id"] = -1;
            dr["name"] = "- Chưa chọn đơn vị -";
            datatable.Rows.InsertAt(dr, 0);
            cbDVKhaoSat.DataSource = datatable;
            cbDVKhaoSat.DisplayMember = "name";
            cbDVKhaoSat.ValueMember = "id";
            cbDVRaPha.DataSource = datatable.Copy();
            cbDVRaPha.DisplayMember = "name";
            cbDVRaPha.ValueMember = "id";
        }

        //private OLuoi GetOLuoi()
        //{
        //    List<DataRow> drOL = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "OLuoi", "gid", id.ToString());
        //    if (drOL.Count == 0)
        //    {
        //        return null;
        //    }
        //    DataRow dr = drOL[0];
        //    OLuoi oLuoi = new OLuoi();
        //    //gid
        //    string gid = dr["gid"].ToString();
        //    oLuoi.gid = long.Parse(gid);
        //    //cecm_program_areamap_id
        //    string cecm_program_areamap_ID = dr["cecm_program_areamap_id"].ToString();
        //    oLuoi.cecm_program_areamap_ID = long.Parse(cecm_program_areamap_ID);
        //    //o_id
        //    string o_id = dr["o_id"].ToString();
        //    oLuoi.o_id = o_id;
        //    //autoDivide
        //    oLuoi.autoDivide = int.TryParse(dr["autoDivide"].ToString(), out int autoDivide) ? autoDivide : 0;
        //    //khaosat_deptid
        //    long khaosat_deptid = -1;
        //    long.TryParse(dr["khaosat_deptid"].ToString(), out khaosat_deptid);
        //    oLuoi.khaosat_deptId = khaosat_deptid;
        //    //rapha_deptid
        //    long rapha_deptid = -1;
        //    long.TryParse(dr["rapha_deptid"].ToString(), out rapha_deptid);
        //    oLuoi.raPha_deptId = rapha_deptid;
        //    //lat_center
        //    double lat_center = -1;
        //    double.TryParse(dr["lat_center"].ToString(), out lat_center);
        //    oLuoi.lat_center = lat_center;
        //    //lat_corner1
        //    double lat_corner1 = -1;
        //    double.TryParse(dr["lat_corner1"].ToString(), out lat_corner1);
        //    oLuoi.lat_corner1 = lat_corner1;
        //    //lat_corner2
        //    double lat_corner2 = -1;
        //    double.TryParse(dr["lat_corner2"].ToString(), out lat_corner2);
        //    oLuoi.lat_corner2 = lat_corner2;
        //    //lat_corner3
        //    double lat_corner3 = -1;
        //    double.TryParse(dr["lat_corner3"].ToString(), out lat_corner3);
        //    oLuoi.lat_corner3 = lat_corner3;
        //    //lat_corner4
        //    double lat_corner4 = -1;
        //    double.TryParse(dr["lat_corner4"].ToString(), out lat_corner4);
        //    oLuoi.lat_corner4 = lat_corner4;
        //    //long_center
        //    double long_center = -1;
        //    double.TryParse(dr["long_center"].ToString(), out long_center);
        //    oLuoi.long_center = long_center;
        //    //long_corner1
        //    double long_corner1 = -1;
        //    double.TryParse(dr["long_corner1"].ToString(), out long_corner1);
        //    oLuoi.long_corner1 = long_corner1;
        //    //long_corner2
        //    double long_corner2 = -1;
        //    double.TryParse(dr["long_corner2"].ToString(), out long_corner2);
        //    oLuoi.long_corner2 = long_corner2;
        //    //long_corner3
        //    double long_corner3 = -1;
        //    double.TryParse(dr["long_corner3"].ToString(), out long_corner3);
        //    oLuoi.long_corner3 = long_corner3;
        //    //long_corner4
        //    double long_corner4 = -1;
        //    double.TryParse(dr["long_corner4"].ToString(), out long_corner4);
        //    oLuoi.long_corner4 = long_corner4;
        //    //dividerAllGrid
        //    long dividerAllGrid = 0;
        //    long.TryParse(dr["dividerAllGrid"].ToString(), out dividerAllGrid);
        //    oLuoi.dividerAllGrid = dividerAllGrid;
        //    //isCustomAllGrid
        //    long isCustomAllGrid = 0;
        //    long.TryParse(dr["isCustomAllGrid"].ToString(), out isCustomAllGrid);
        //    oLuoi.isCustomAllGrid = isCustomAllGrid;
        //    //distanceAllGrid
        //    double distanceAllGrid = 0;
        //    double.TryParse(dr["distanceAllGrid"].ToString(), out distanceAllGrid);
        //    oLuoi.distanceAllGrid = distanceAllGrid;
        //    //acutangeAllGrid
        //    double acutangeAllGrid = 0;
        //    double.TryParse(dr["acutangeAllGrid"].ToString(), out acutangeAllGrid);
        //    oLuoi.acutangeAllGrid = acutangeAllGrid;
        //    //isCustom1
        //    long isCustom1 = 0;
        //    long.TryParse(dr["isCustom1"].ToString(), out isCustom1);
        //    oLuoi.isCustom1 = isCustom1;
        //    //isCustom2
        //    long isCustom2 = 0;
        //    long.TryParse(dr["isCustom2"].ToString(), out isCustom2);
        //    oLuoi.isCustom2 = isCustom2;
        //    //isCustom3
        //    long isCustom3 = 0;
        //    long.TryParse(dr["isCustom3"].ToString(), out isCustom3);
        //    oLuoi.isCustom3 = isCustom3;
        //    //isCustom4
        //    long isCustom4 = 0;
        //    long.TryParse(dr["isCustom4"].ToString(), out isCustom4);
        //    oLuoi.isCustom4 = isCustom4;
        //    //acuteAngle1
        //    double acuteAngle1 = 0;
        //    double.TryParse(dr["acuteAngle1"].ToString(), out acuteAngle1);
        //    oLuoi.acuteAngle1 = acuteAngle1;
        //    //acuteAngle2
        //    double acuteAngle2 = 0;
        //    double.TryParse(dr["acuteAngle2"].ToString(), out acuteAngle2);
        //    oLuoi.acuteAngle2 = acuteAngle2;
        //    //acuteAngle3
        //    double acuteAngle3 = 0;
        //    double.TryParse(dr["acuteAngle3"].ToString(), out acuteAngle3);
        //    oLuoi.acuteAngle3 = acuteAngle3;
        //    //acuteAngle4
        //    double acuteAngle4 = 0;
        //    double.TryParse(dr["acuteAngle4"].ToString(), out acuteAngle4);
        //    oLuoi.acuteAngle4 = acuteAngle4;
        //    //distance1
        //    double distance1 = 0;
        //    double.TryParse(dr["distance1"].ToString(), out distance1);
        //    oLuoi.distance1 = distance1;
        //    //distance2
        //    double distance2 = 0;
        //    double.TryParse(dr["distance2"].ToString(), out distance2);
        //    oLuoi.distance2 = distance2;
        //    //distance3
        //    double distance3 = 0;
        //    double.TryParse(dr["distance3"].ToString(), out distance3);
        //    oLuoi.distance3 = distance3;
        //    //distance4
        //    double distance4 = 0;
        //    double.TryParse(dr["distance4"].ToString(), out distance4);
        //    oLuoi.distance4 = distance4;

        //    //indexRow++;
        //    List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "cecm_program_area_line", "cecmprogramareasub_id", oLuoi.gid.ToString());
        //    List<CecmProgramAreaLineDTO> lstRanhDo = new List<CecmProgramAreaLineDTO>();
        //    foreach (DataRow dataRow in lst)
        //    {
        //        bool parseSuccess =
        //        double.TryParse(dataRow["start_x"].ToString(), out double lattStart) &
        //        double.TryParse(dataRow["start_y"].ToString(), out double longtStart) &
        //        double.TryParse(dataRow["end_x"].ToString(), out double lattEnd) &
        //        double.TryParse(dataRow["end_y"].ToString(), out double longtEnd);
        //        long.TryParse(dataRow["cecmprogramareasub_id"].ToString(), out long cecmprogramareasub_id);
        //        long.TryParse(dataRow["cecmprogramareamap_id"].ToString(), out long cecmprogramareamap_id);
        //        long.TryParse(dataRow["cecmprogram_id"].ToString(), out long cecmprogram_id);
        //        if (parseSuccess)
        //        {
        //            CecmProgramAreaLineDTO line = new CecmProgramAreaLineDTO();
        //            line.cecmprogram_id = cecmprogram_id;
        //            line.cecmprogramareamap_id = cecmprogramareamap_id;
        //            line.cecmprogramareasub_id = cecmprogramareasub_id;
        //            line.start_x = lattStart;
        //            line.start_y = longtStart;
        //            line.end_x = lattEnd;
        //            line.end_y = longtEnd;
        //            lstRanhDo.Add(line);
        //        }

        //    }
        //    oLuoi.lstRanhDo = lstRanhDo;
        //    return oLuoi;
        //}

        private void LoadKhuVuc(long idKV)
        {
            try
            {
                List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "cecm_program_area_map", "id", idKV.ToString());
                if (lst.Count == 0)
                {
                    return;
                }
                DataRow dr = lst[0];
                long.TryParse(dr["cecm_program_id"].ToString(), out long idDA);
                string pathTemp = AppUtils.GetFolderTemp((int)idDA);
                string fullPath = System.IO.Path.Combine(pathTemp, dr["photo_file"].ToString());
                MapWinGIS.Image img = AppUtils.OpenImage(fullPath);
                double.TryParse(dr["right_long"].ToString(), out double right_long);
                double.TryParse(dr["left_long"].ToString(), out double left_long);
                double.TryParse(dr["top_lat"].ToString(), out double top_lat);
                double.TryParse(dr["bottom_lat"].ToString(), out double bottom_lat);
                img.OriginalDX = (right_long - left_long) / img.OriginalWidth;
                img.OriginalDY = (top_lat - bottom_lat) / img.OriginalHeight;
                //MessageBox.Show("width = " + img.OriginalDX + " / height = " + img.OriginalDY);
                //img.ProjectionToImage(xmin, ymin, out int x, out int y);
                //img.SetVisibleExtents(xmin, ymin, xmax, ymax, NuaDoDaiCanh * 20, 10);
                img.OriginalXllCenter = left_long;
                img.OriginalYllCenter = bottom_lat;

                imageLayer = axMap1.AddLayer(img, true);

                axMap1.Redraw();
            }
            catch (Exception ex)
            {

            }
        }

        private void ThemMoiOLuoi_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            //UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbDVKhaoSat);
            //UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbDVRaPha);
            //OLuoi oLuoi = GetOLuoi();
            if(oLuoi == null)
            {
                return;
            }
            LoadKhuVuc(oLuoi.cecm_program_areamap_ID);


            cbDVKhaoSat.SelectedValue = oLuoi.khaosat_deptId;
            cbDVRaPha.SelectedValue = oLuoi.raPha_deptId;
            cbTuDongChia.SelectedIndex = oLuoi.autoDivide;
            tb_o_id.Text = oLuoi.o_id;
            nud_long_center.Value = (decimal)oLuoi.long_center;
            nud_long_corner1.Value = (decimal)oLuoi.long_corner1;
            nud_long_corner2.Value = (decimal)oLuoi.long_corner2;
            nud_long_corner3.Value = (decimal)oLuoi.long_corner3;
            nud_long_corner4.Value = (decimal)oLuoi.long_corner4;
            nud_lat_center.Value = (decimal)oLuoi.lat_center;
            nud_lat_corner1.Value = (decimal)oLuoi.lat_corner1;
            nud_lat_corner2.Value = (decimal)oLuoi.lat_corner2;
            nud_lat_corner3.Value = (decimal)oLuoi.lat_corner3;
            nud_lat_corner4.Value = (decimal)oLuoi.lat_corner4;
            //Kiểu chia
            if (oLuoi.dividerAllGrid == 1)
            {
                rb_4goc.Checked = true;
            }
            else if (oLuoi.dividerAllGrid == 2)
            {
                rb_toanOLuoi.Checked = true;
            }
            //Toàn ô lưới
            nud_distanceAll.Value = (decimal)(oLuoi.distanceAllGrid);
            if (oLuoi.isCustomAllGrid != 1)
            {
                nud_angleAll.Value = 0;
                nud_angleAll.ReadOnly = true;
            }
            else
            {
                nud_angleAll.Value = (decimal)oLuoi.acutangeAllGrid;
                nud_angleAll.ReadOnly = false;
            }
            if (oLuoi.isCustomAllGrid == 1)
            {
                rb_toanO_truc1.Checked = true;
            }
            else if (oLuoi.isCustomAllGrid == 2)
            {
                rb_toanO_truc2.Checked = true;
            }
            else if (oLuoi.isCustomAllGrid == 3)
            {
                rb_toanO_truc3.Checked = true;
            }
            //Góc 1
            nud_distance1.Value = (decimal)(oLuoi.distance1);
            if (oLuoi.isCustom1 != 1)
            {
                nud_angle1.Value = 0;
                nud_angle1.ReadOnly = true;
            }
            else
            {
                nud_angle1.Value = (decimal)oLuoi.acuteAngle1;
                nud_angle1.ReadOnly = false;
            }
            if (oLuoi.isCustom1 == 1)
            {
                rb_goc1_truc1.Checked = true;
            }
            else if (oLuoi.isCustom1 == 2)
            {
                rb_goc1_truc2.Checked = true;
            }
            else if (oLuoi.isCustom1 == 3)
            {
                rb_goc1_truc3.Checked = true;
            }
            //Góc 2
            nud_distance2.Value = (decimal)(oLuoi.distance2);
            if (oLuoi.isCustom2 != 1)
            {
                nud_angle2.Value = 0;
                nud_angle2.ReadOnly = true;
            }
            else
            {
                nud_angle2.Value = (decimal)oLuoi.acuteAngle2;
                nud_angle2.ReadOnly = false;
            }
            if (oLuoi.isCustom2 == 1)
            {
                rb_goc2_truc1.Checked = true;
            }
            else if (oLuoi.isCustom2 == 2)
            {
                rb_goc2_truc2.Checked = true;
            }
            else if (oLuoi.isCustom2 == 3)
            {
                rb_goc2_truc3.Checked = true;
            }
            //Góc 3
            nud_distance3.Value = (decimal)(oLuoi.distance3);
            if (oLuoi.isCustom3 != 1)
            {
                nud_angle3.Value = 0;
                nud_angle3.ReadOnly = true;
            }
            else
            {
                nud_angle3.Value = (decimal)oLuoi.acuteAngle3;
                nud_angle3.ReadOnly = false;
            }
            if (oLuoi.isCustom3 == 1)
            {
                rb_goc3_truc1.Checked = true;
            }
            else if (oLuoi.isCustom3 == 2)
            {
                rb_goc3_truc2.Checked = true;
            }
            else if (oLuoi.isCustom3 == 3)
            {
                rb_goc3_truc3.Checked = true;
            }
            //Góc 4
            nud_distance4.Value = (decimal)(oLuoi.distance4);
            if (oLuoi.isCustom4 != 1)
            {
                nud_angle4.Value = 0;
                nud_angle4.ReadOnly = true;
            }
            else
            {
                nud_angle4.Value = (decimal)oLuoi.acuteAngle4;
                nud_angle4.ReadOnly = false;
            }
            if (oLuoi.isCustom4 == 1)
            {
                rb_goc4_truc1.Checked = true;
            }
            else if (oLuoi.isCustom4 == 2)
            {
                rb_goc4_truc2.Checked = true;
            }
            else if (oLuoi.isCustom4 == 3)
            {
                rb_goc4_truc3.Checked = true;
            }
            axMap1.ZoomBarMaxZoom = 20;
            axMap1.ZoomToTileLevel(20);
            axMap1.SetLatitudeLongitude(oLuoi.lat_center, oLuoi.long_center);
            //oLuoiLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            axMap1.DrawLineEx(oLuoiLayer, oLuoi.long_corner1, oLuoi.lat_corner1, oLuoi.long_corner2, oLuoi.lat_corner2, 5, AppUtils.ColorToUint(Color.White));
            axMap1.DrawLineEx(oLuoiLayer, oLuoi.long_corner2, oLuoi.lat_corner2, oLuoi.long_corner3, oLuoi.lat_corner3, 5, AppUtils.ColorToUint(Color.White));
            axMap1.DrawLineEx(oLuoiLayer, oLuoi.long_corner3, oLuoi.lat_corner3, oLuoi.long_corner4, oLuoi.lat_corner4, 5, AppUtils.ColorToUint(Color.White));
            axMap1.DrawLineEx(oLuoiLayer, oLuoi.long_corner4, oLuoi.lat_corner4, oLuoi.long_corner1, oLuoi.lat_corner1, 5, AppUtils.ColorToUint(Color.White));
            Extents extents = new Extents();
            extents.SetBounds(oLuoi.long_corner4, oLuoi.lat_corner4 - 0.0001, 0, oLuoi.long_corner2, oLuoi.lat_corner2 + 0.0001, 0);
            axMap1.Extents = extents;
            //if (oLuoi.gid > 0)
            //{

            //    //List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "cecm_program_area_line", "cecmprogramareasub_id", oLuoi.gid.ToString());

            //    //foreach (DataRow dataRow in lst)
            //    //{
            //    //    bool parseSuccess =
            //    //    double.TryParse(dataRow["start_x"].ToString(), out double lattStart) &
            //    //    double.TryParse(dataRow["start_y"].ToString(), out double longtStart) &
            //    //    double.TryParse(dataRow["end_x"].ToString(), out double lattEnd) &
            //    //    double.TryParse(dataRow["end_y"].ToString(), out double longtEnd);
            //    //    if (parseSuccess)
            //    //    {
            //    //        axMap1.DrawLine(longtStart, lattStart, longtEnd, lattEnd, 1, AppUtils.ColorToUint(Color.Red));
            //    //    }

            //    //}

            //}
            if (oLuoi.lstRanhDo != null)
            {
                lstRanhDo = oLuoi.lstRanhDo;
            }
            foreach (CecmProgramAreaLineDTO line in lstRanhDo)
            {
                axMap1.DrawLineEx(lineLayer, line.start_x, line.start_y, line.end_x, line.end_y, 1, AppUtils.ColorToUint(Color.White));
            }
            loaded = true;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            btnLuuClicked = true;
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                btnLuuClicked = false;
                return;
            }
            redrawLines();
            getCurrentDataOLuoi();
            if (oLuoi.gid > 0)
            {
                SqlCommand cmd_sub = new SqlCommand(
                        "UPDATE OLuoi SET " +
                        //"cecm_program_areamap_id = @cecm_program_areamap_id, " +
                        "khaosat_deptid = @khaosat_deptid, " +
                        "rapha_deptid = @rapha_deptid," +
                        "autoDivide = @autoDivide, " +
                        "lat_center = @lat_center," +
                        "lat_corner1 = @lat_corner1," +
                        "lat_corner2 = @lat_corner2," +
                        "lat_corner3 = @lat_corner3," +
                        "lat_corner4 = @lat_corner4," +
                        "long_center = @long_center," +
                        "long_corner1 = @long_corner1," +
                        "long_corner2 = @long_corner2," +
                        "long_corner3 = @long_corner3," +
                        "long_corner4 = @long_corner4," +
                        "dividerAllGrid = @dividerAllGrid," +
                        "isCustomAllGrid = @isCustomAllGrid," +
                        "distanceAllGrid = @distanceAllGrid," +
                        "acutangeAllGrid = @acutangeAllGrid," +
                        "distance1 = @distance1," +
                        "distance2 = @distance2," +
                        "distance3 = @distance3," +
                        "distance4 = @distance4," +
                        "isCustom1= @isCustom1," +
                        "isCustom2 = @isCustom2," +
                        "isCustom3 = @isCustom3," +
                        "isCustom4 = @isCustom4," +
                        "acuteAngle1 = @acuteAngle1," +
                        "acuteAngle2 = @acuteAngle2," +
                        "acuteAngle3 = @acuteAngle3," +
                        "acuteAngle4 = @acuteAngle4," +
                        "o_id = @o_id " +
                        "WHERE gid = " + oLuoi.gid, _ExtraInfoConnettion.Connection as SqlConnection);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "cecm_program_areamap_id", oLuoi.khaosat_deptId.ToString(), true);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "khaosat_deptid", oLuoi.khaosat_deptId.ToString(), true);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "rapha_deptid", oLuoi.raPha_deptId.ToString(), true);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "autoDivide", oLuoi.autoDivide.ToString(), true, SqlDbType.Int);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "o_id", oLuoi.o_id.ToString(), false);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "lat_center", oLuoi.lat_center.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "lat_corner1", oLuoi.lat_corner1.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "lat_corner2", oLuoi.lat_corner2.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "lat_corner3", oLuoi.lat_corner3.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "lat_corner4", oLuoi.lat_corner4.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "long_center", oLuoi.long_center.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "long_corner1", oLuoi.long_corner1.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "long_corner2", oLuoi.long_corner2.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "long_corner3", oLuoi.long_corner3.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "long_corner4", oLuoi.long_corner4.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "dividerAllGrid", oLuoi.dividerAllGrid.ToString(), true, SqlDbType.Int);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "isCustomAllGrid", oLuoi.isCustomAllGrid.ToString(), true, SqlDbType.Int);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "distanceAllGrid", oLuoi.distanceAllGrid.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "acutangeAllGrid", oLuoi.acutangeAllGrid.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "isCustom1", oLuoi.isCustom1.ToString(), true, SqlDbType.Int);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "isCustom2", oLuoi.isCustom2.ToString(), true, SqlDbType.Int);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "isCustom3", oLuoi.isCustom3.ToString(), true, SqlDbType.Int);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "isCustom4", oLuoi.isCustom4.ToString(), true, SqlDbType.Int);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "acuteAngle1", oLuoi.acuteAngle1.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "acuteAngle2", oLuoi.acuteAngle2.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "acuteAngle3", oLuoi.acuteAngle3.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "acuteAngle4", oLuoi.acuteAngle4.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "distance1", oLuoi.distance1.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "distance2", oLuoi.distance2.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "distance3", oLuoi.distance3.ToString(), true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd_sub, "distance4", oLuoi.distance4.ToString(), true, SqlDbType.Float);
                //cmd_sub.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                int temp = cmd_sub.ExecuteNonQuery();

                SqlCommand cmd_delete_lines = new SqlCommand("DELETE FROM cecm_program_area_line WHERE cecmprogramareasub_id = @cecmprogramareasub_id", _ExtraInfoConnettion.Connection as SqlConnection);
                UtilsDatabase.UpdateDataSqlParameter(cmd_delete_lines, "cecmprogramareasub_id", oLuoi.gid.ToString(), true, SqlDbType.BigInt);
                cmd_delete_lines.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                cmd_delete_lines.ExecuteNonQuery();

                foreach (CecmProgramAreaLineDTO line in lstRanhDo)
                {
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO cecm_program_area_line(start_x, start_y, end_x, end_y, cecmprogramareasub_id, cecmprogramareamap_id, cecmprogram_id) " +
                        "VALUES(@start_x, @start_y, @end_x, @end_y, @cecmprogramareasub_id, @cecmprogramareamap_id, @cecmprogram_id)", _ExtraInfoConnettion.Connection as SqlConnection);
                    cmd2.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                    UtilsDatabase.UpdateDataSqlParameter(cmd2, "start_x", line.start_x.ToString(), true, SqlDbType.Float);
                    UtilsDatabase.UpdateDataSqlParameter(cmd2, "start_y", line.start_y.ToString(), true, SqlDbType.Float);
                    UtilsDatabase.UpdateDataSqlParameter(cmd2, "end_x", line.end_x.ToString(), true, SqlDbType.Float);
                    UtilsDatabase.UpdateDataSqlParameter(cmd2, "end_y", line.end_y.ToString(), true, SqlDbType.Float);
                    UtilsDatabase.UpdateDataSqlParameter(cmd2, "cecmprogramareasub_id", line.cecmprogramareasub_id.ToString(), true, SqlDbType.BigInt);
                    UtilsDatabase.UpdateDataSqlParameter(cmd2, "cecmprogramareamap_id", line.cecmprogramareamap_id.ToString(), true, SqlDbType.BigInt);
                    UtilsDatabase.UpdateDataSqlParameter(cmd2, "cecmprogram_id", line.cecmprogram_id.ToString(), true, SqlDbType.BigInt);
                    cmd2.ExecuteNonQuery();
                }

                if (temp > 0)
                {
                    MessageBox.Show("Cập nhật ô lưới thành công");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Cập nhật ô lưới thất bại");
                    DialogResult = DialogResult.None;
                }
            }

            this.Close();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_o_id_Validating(object sender, CancelEventArgs e)
        {
            if (!btnLuuClicked)
            {
                return;
            }
            if (tb_o_id.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider.SetError(tb_o_id, "Chưa nhập mã ô");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(tb_o_id, "");
            }
        }

        private void rb_goc1_truc1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc1_truc1.Checked)
            {
                nud_angle1.ReadOnly = false;
                redrawLines();
            }
            else
            {
                nud_angle1.ReadOnly = true;
            }

        }

        private void rb_goc2_truc1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc2_truc1.Checked)
            {
                nud_angle2.ReadOnly = false;
                redrawLines();
            }
            else
            {
                nud_angle2.ReadOnly = true;
            }

        }

        private void rb_goc3_truc1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc3_truc1.Checked)
            {
                nud_angle3.ReadOnly = false;
                redrawLines();
            }
            else
            {
                nud_angle3.ReadOnly = true;
            }

        }

        private void rb_goc4_truc1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc4_truc1.Checked)
            {
                nud_angle4.ReadOnly = false;
                redrawLines();
            }
            else
            {
                nud_angle4.ReadOnly = true;
            }
        }

        private void nud_distance1_ValueChanged(object sender, EventArgs e)
        {
            redrawLines();
        }

        private void nud_distance2_ValueChanged(object sender, EventArgs e)
        {
            redrawLines();
        }

        private void nud_distance3_ValueChanged(object sender, EventArgs e)
        {
            redrawLines();
        }

        private void nud_distance4_ValueChanged(object sender, EventArgs e)
        {
            redrawLines();
        }

        private void rb_goc1_truc2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc1_truc2.Checked)
            {
                redrawLines();
            }
        }

        private void rb_goc1_truc3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc1_truc3.Checked)
            {
                redrawLines();
            }
        }

        private void nud_angle1_ValueChanged(object sender, EventArgs e)
        {
            if (!nud_angle1.ReadOnly)
            {
                redrawLines();
            }

        }

        private void rb_goc2_truc2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc2_truc2.Checked)
            {
                redrawLines();
            }
        }

        private void rb_goc2_truc3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc2_truc3.Checked)
            {
                redrawLines();
            }
        }

        private void nud_angle2_ValueChanged(object sender, EventArgs e)
        {
            if (!nud_angle2.ReadOnly)
            {
                redrawLines();
            }
        }

        private void rb_goc3_truc2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc3_truc2.Checked)
            {
                redrawLines();
            }
        }

        private void rb_goc3_truc3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc3_truc3.Checked)
            {
                redrawLines();
            }
        }

        private void nud_angle3_ValueChanged(object sender, EventArgs e)
        {
            if (!nud_angle3.ReadOnly)
            {
                redrawLines();
            }
        }

        private void rb_goc4_truc2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc4_truc2.Checked)
            {
                redrawLines();
            }
        }

        private void rb_goc4_truc3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_goc4_truc3.Checked)
            {
                redrawLines();
            }
        }

        private void nud_angle4_ValueChanged(object sender, EventArgs e)
        {
            if (!nud_angle4.ReadOnly)
            {
                redrawLines();
            }
        }

        private void nud_long_center_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCenter();
        }

        private void nud_lat_center_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCenter();
        }

        private void AutoDivideBasedOnCenter()
        {
            if (!loaded || NuaDoDaiCanh == 0)
            {
                return;
            }
            loaded = false;
            double[] latlongCenter = AppUtils.ConverLatLongToUTM((double)nud_lat_center.Value, (double)nud_long_center.Value);
            double latt1 = 0;
            double longt1 = 0;
            AppUtils.ToLatLon(latlongCenter[0] + NuaDoDaiCanh, latlongCenter[1] - NuaDoDaiCanh, ref latt1, ref longt1, "48N");
            nud_lat_corner1.Value = (decimal)latt1;
            nud_long_corner1.Value = (decimal)longt1;
            double latt2 = 0;
            double longt2 = 0;
            AppUtils.ToLatLon(latlongCenter[0] + NuaDoDaiCanh, latlongCenter[1] + NuaDoDaiCanh, ref latt2, ref longt2, "48N");
            nud_lat_corner2.Value = (decimal)latt2;
            nud_long_corner2.Value = (decimal)longt2;
            double latt3 = 0;
            double longt3 = 0;
            AppUtils.ToLatLon(latlongCenter[0] - NuaDoDaiCanh, latlongCenter[1] + NuaDoDaiCanh, ref latt3, ref longt3, "48N");
            nud_lat_corner3.Value = (decimal)latt3;
            nud_long_corner3.Value = (decimal)longt3;
            double latt4 = 0;
            double longt4 = 0;
            AppUtils.ToLatLon(latlongCenter[0] - NuaDoDaiCanh, latlongCenter[1] - NuaDoDaiCanh, ref latt4, ref longt4, "48N");
            nud_lat_corner4.Value = (decimal)latt4;
            nud_long_corner4.Value = (decimal)longt4;
            loaded = true;
            redrawLines(true);
            Extents extents = new Extents();
            extents.SetBounds(longt4, latt4 - 0.0001, 0, longt2, latt2 + 0.0001, 0);
            axMap1.Extents = extents;
        }

        private void nud_long_corner1_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner1();
        }

        private void nud_lat_corner1_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner1();
        }

        private void AutoDivideBasedOnCorner1()
        {
            if (!loaded || NuaDoDaiCanh == 0)
            {
                return;
            }
            loaded = false;
            double[] latlongCorner1 = AppUtils.ConverLatLongToUTM((double)nud_lat_corner1.Value, (double)nud_long_corner1.Value);
            double lattCenter = 0;
            double longtCenter = 0;
            AppUtils.ToLatLon(latlongCorner1[0] - NuaDoDaiCanh, latlongCorner1[1] + NuaDoDaiCanh, ref lattCenter, ref longtCenter, "48N");
            nud_lat_center.Value = (decimal)lattCenter;
            nud_long_center.Value = (decimal)longtCenter;
            double latt2 = 0;
            double longt2 = 0;
            AppUtils.ToLatLon(latlongCorner1[0], latlongCorner1[1] + NuaDoDaiCanh * 2, ref latt2, ref longt2, "48N");
            nud_lat_corner2.Value = (decimal)latt2;
            nud_long_corner2.Value = (decimal)longt2;
            double latt3 = 0;
            double longt3 = 0;
            AppUtils.ToLatLon(latlongCorner1[0] - NuaDoDaiCanh * 2, latlongCorner1[1] + NuaDoDaiCanh * 2, ref latt3, ref longt3, "48N");
            nud_lat_corner3.Value = (decimal)latt3;
            nud_long_corner3.Value = (decimal)longt3;
            double latt4 = 0;
            double longt4 = 0;
            AppUtils.ToLatLon(latlongCorner1[0] - NuaDoDaiCanh * 2, latlongCorner1[1], ref latt4, ref longt4, "48N");
            nud_lat_corner4.Value = (decimal)latt4;
            nud_long_corner4.Value = (decimal)longt4;
            loaded = true;
            redrawLines(true);
            Extents extents = new Extents();
            extents.SetBounds(longt4, latt4 - 0.0001, 0, longt2, latt2 + 0.0001, 0);
            axMap1.Extents = extents;
        }

        private void nud_long_corner2_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner2();
        }

        private void nud_lat_corner2_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner2();
        }

        private void AutoDivideBasedOnCorner2()
        {
            if (!loaded || NuaDoDaiCanh == 0)
            {
                return;
            }
            loaded = false;
            double[] latlongCorner2 = AppUtils.ConverLatLongToUTM((double)nud_lat_corner2.Value, (double)nud_long_corner2.Value);
            double lattCenter = 0;
            double longtCenter = 0;
            AppUtils.ToLatLon(latlongCorner2[0] - NuaDoDaiCanh, latlongCorner2[1] - NuaDoDaiCanh, ref lattCenter, ref longtCenter, "48N");
            nud_lat_center.Value = (decimal)lattCenter;
            nud_long_center.Value = (decimal)longtCenter;
            double latt1 = 0;
            double longt1 = 0;
            AppUtils.ToLatLon(latlongCorner2[0], latlongCorner2[1] - NuaDoDaiCanh * 2, ref latt1, ref longt1, "48N");
            nud_lat_corner1.Value = (decimal)latt1;
            nud_long_corner1.Value = (decimal)longt1;
            double latt3 = 0;
            double longt3 = 0;
            AppUtils.ToLatLon(latlongCorner2[0] - NuaDoDaiCanh * 2, latlongCorner2[1], ref latt3, ref longt3, "48N");
            nud_lat_corner3.Value = (decimal)latt3;
            nud_long_corner3.Value = (decimal)longt3;
            double latt4 = 0;
            double longt4 = 0;
            AppUtils.ToLatLon(latlongCorner2[0] - NuaDoDaiCanh * 2, latlongCorner2[1] - NuaDoDaiCanh * 2, ref latt4, ref longt4, "48N");
            nud_lat_corner4.Value = (decimal)latt4;
            nud_long_corner4.Value = (decimal)longt4;
            loaded = true;
            redrawLines(true);
            Extents extents = new Extents();
            extents.SetBounds(longt4, latt4 - 0.0001, 0, (double)nud_long_corner2.Value, (double)nud_lat_corner2.Value + 0.0001, 0);
            axMap1.Extents = extents;
        }

        private void nud_long_corner3_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner3();
        }

        private void nud_lat_corner3_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner3();
        }

        private void AutoDivideBasedOnCorner3()
        {
            if (!loaded || NuaDoDaiCanh == 0)
            {
                return;
            }
            loaded = false;
            double[] latlongCorner3 = AppUtils.ConverLatLongToUTM((double)nud_lat_corner3.Value, (double)nud_long_corner3.Value);
            double lattCenter = 0;
            double longtCenter = 0;
            AppUtils.ToLatLon(latlongCorner3[0] + NuaDoDaiCanh, latlongCorner3[1] - NuaDoDaiCanh, ref lattCenter, ref longtCenter, "48N");
            nud_lat_center.Value = (decimal)lattCenter;
            nud_long_center.Value = (decimal)longtCenter;
            double latt1 = 0;
            double longt1 = 0;
            AppUtils.ToLatLon(latlongCorner3[0] + NuaDoDaiCanh * 2, latlongCorner3[1] - NuaDoDaiCanh * 2, ref latt1, ref longt1, "48N");
            nud_lat_corner1.Value = (decimal)latt1;
            nud_long_corner1.Value = (decimal)longt1;
            double latt2 = 0;
            double longt2 = 0;
            AppUtils.ToLatLon(latlongCorner3[0] + NuaDoDaiCanh * 2, latlongCorner3[1], ref latt2, ref longt2, "48N");
            nud_lat_corner2.Value = (decimal)latt2;
            nud_long_corner2.Value = (decimal)longt2;
            double latt4 = 0;
            double longt4 = 0;
            AppUtils.ToLatLon(latlongCorner3[0], latlongCorner3[1] - NuaDoDaiCanh * 2, ref latt4, ref longt4, "48N");
            nud_lat_corner4.Value = (decimal)latt4;
            nud_long_corner4.Value = (decimal)longt4;
            loaded = true;
            redrawLines(true);
            Extents extents = new Extents();
            extents.SetBounds(longt4, latt4 - 0.0001, 0, longt2, latt2 + 0.0001, 0);
            axMap1.Extents = extents;
        }

        private void nud_long_corner4_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner4();
        }

        private void nud_lat_corner4_ValueChanged(object sender, EventArgs e)
        {
            AutoDivideBasedOnCorner4();
        }

        private void AutoDivideBasedOnCorner4()
        {
            if (!loaded || NuaDoDaiCanh == 0)
            {
                return;
            }
            loaded = false;
            double[] latlongCorner4 = AppUtils.ConverLatLongToUTM((double)nud_lat_corner4.Value, (double)nud_long_corner4.Value);
            double lattCenter = 0;
            double longtCenter = 0;
            AppUtils.ToLatLon(latlongCorner4[0] + NuaDoDaiCanh, latlongCorner4[1] + NuaDoDaiCanh, ref lattCenter, ref longtCenter, "48N");
            nud_lat_center.Value = (decimal)lattCenter;
            nud_long_center.Value = (decimal)longtCenter;
            double latt1 = 0;
            double longt1 = 0;
            AppUtils.ToLatLon(latlongCorner4[0] + NuaDoDaiCanh * 2, latlongCorner4[1], ref latt1, ref longt1, "48N");
            nud_lat_corner1.Value = (decimal)latt1;
            nud_long_corner1.Value = (decimal)longt1;
            double latt2 = 0;
            double longt2 = 0;
            AppUtils.ToLatLon(latlongCorner4[0] + NuaDoDaiCanh * 2, latlongCorner4[1] + NuaDoDaiCanh * 2, ref latt2, ref longt2, "48N");
            nud_lat_corner2.Value = (decimal)latt2;
            nud_long_corner2.Value = (decimal)longt2;
            double latt3 = 0;
            double longt3 = 0;
            AppUtils.ToLatLon(latlongCorner4[0], latlongCorner4[1] + NuaDoDaiCanh * 2, ref latt3, ref longt3, "48N");
            nud_lat_corner3.Value = (decimal)latt3;
            nud_long_corner3.Value = (decimal)longt3;
            loaded = true;
            redrawLines(true);
            Extents extents = new Extents();
            extents.SetBounds((double)nud_long_corner4.Value, (double)nud_lat_corner4.Value - 0.0001, 0, longt2, latt2 + 0.0001, 0);
            axMap1.Extents = extents;
        }

        private void rb_4goc_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_4goc.Checked)
            {
                pnl4Goc.Visible = true;
                pnl4Goc.Size = new System.Drawing.Size(1764, 583);
                redrawLines();
            }
            else
            {
                pnl4Goc.Visible = false;
                pnl4Goc.Size = new System.Drawing.Size(1764, 0);
            }
        }

        private void rb_toanOLuoi_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_toanOLuoi.Checked)
            {
                pnlToanO.Visible = true;
                pnlToanO.Size = new System.Drawing.Size(1764, 338);
                this.pnlToanO.Location = new System.Drawing.Point(3, 499);
                redrawLines();

            }
            else
            {
                pnlToanO.Visible = false;
                pnlToanO.Size = new System.Drawing.Size(1764, 0);
                this.pnlToanO.Location = new System.Drawing.Point(3, 1080);
            }
        }

        private void nud_distanceAll_ValueChanged(object sender, EventArgs e)
        {
            redrawLines();
        }

        private void rb_toanO_truc2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_toanO_truc2.Checked)
            {
                redrawLines();
            }
        }

        private void rb_toanO_truc3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_toanO_truc3.Checked)
            {
                redrawLines();
            }
        }

        private void rb_toanO_truc1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_toanO_truc1.Checked)
            {
                nud_angleAll.ReadOnly = false;
                redrawLines();
            }
            else
            {
                nud_angleAll.ReadOnly = true;
            }
        }

        private void nud_angleAll_ValueChanged(object sender, EventArgs e)
        {
            if (!nud_angleAll.ReadOnly)
            {
                redrawLines();
            }
        }

        private void cbTuDongChia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTuDongChia.SelectedIndex == 0)
            {
                NuaDoDaiCanh = 25;
            }
            else if (cbTuDongChia.SelectedIndex == 1)
            {
                NuaDoDaiCanh = 12.5;
            }
            else
            {
                NuaDoDaiCanh = 0;
            }
            AutoDivideBasedOnCenter();
        }
    }
}
