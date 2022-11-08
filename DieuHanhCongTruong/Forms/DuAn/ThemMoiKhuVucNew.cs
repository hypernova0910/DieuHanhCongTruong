using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Models;
using DieuHanhCongTruong.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class ThemMoiKhuVucNew : Form
    {
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;
        public int _IdVungDuAn = 0;
        public int _IdDuAn = 0;
        private List<string> pzone = new List<string>();
        public double top_lat = 0;
        public double bottom_lat = 0;
        public double left_long = 0;
        public double right_long = 0;
        //public List<OLuoi> lst_oluoi = new List<OLuoi>();

        public ThemMoiKhuVucNew(int idVungDuAn, int idDuAn)
        {
            InitializeComponent();

            _IdVungDuAn = idVungDuAn;
            _IdDuAn = idDuAn;

            pzone = new List<string> { @"3- 1 / 10.000 - múi 3,", " 6- 1 / 25.000 - múi 6" };

            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            if (_ExtraInfoConnettion == null)
                return;
        }

        public void LoadDataForm(CecmProgramAreaMapDTO jsonInfo)
        {
            UtilsDatabase.LoadTinh(_ExtraInfoConnettion, cbparentId);
            foreach (var item in pzone)
                cbpzone.Items.Add(item);

            if (_IdVungDuAn >= 0)
            {
                this.Text = "Chỉnh sửa khu vực";
                var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "cecm_program_area_map", "id", _IdVungDuAn.ToString());
                foreach (var item in lstDatarow)
                {
                    UtilsDatabase.SetComboboxValue(cbparentId, item, "parentid");
                    UtilsDatabase.SetComboboxValue(cbparentIdDistrict, item, "parentiddistrict");
                    UtilsDatabase.SetComboboxValue(cbparentIdCommune, item, "parentidcommune");
                    UtilsDatabase.SetComboboxValue(cbvillageId, item, "villageid");
                    UtilsDatabase.SetTextboxValue(tbcode, item, "code", false);
                    UtilsDatabase.SetTextboxValue(tbaddress, item, "address", false);
                    UtilsDatabase.SetTextboxValue(tbposition_lat, item, "position_lat", true);
                    UtilsDatabase.SetTextboxValue(tbposition_long, item, "position_long", true);
                    UtilsDatabase.SetTextboxValue(tbareamap, item, "areamap", true);
                    UtilsDatabase.SetTextboxValue(tbpolygonGeomST, item, "polygongeomst", false);
                    //UtilsDatabase.SetTextboxPolygonValue(tbpolygonGeomST, item, "polygongeomst");
                    UtilsDatabase.SetTextboxValue(tbpositionlongvn2000, item, "positionlongvn2000", true);
                    UtilsDatabase.SetTextboxValue(tbpositionlatvn2000, item, "positionlatvn2000", true);
                    UtilsDatabase.SetTextboxValue(tbmeridian, item, "meridian", true);
                    UtilsDatabase.SetComboboxValue(cbpzone, item, "pzone");
                    UtilsDatabase.SetTextboxValue(tblx, item, "lx", true);
                    UtilsDatabase.SetTextboxValue(tbly, item, "ly", true);
                    UtilsDatabase.SetTextboxPolygonValue(tbvn2000, item, "vn2000");
                    UtilsDatabase.SetTextboxValue(tbphoto_file, item, "photo_file", false);
                    UtilsDatabase.SetTextboxValue(tbbottom_lat, item, "bottom_lat", true);
                    double.TryParse(item["bottom_lat"].ToString(), out bottom_lat);
                    UtilsDatabase.SetTextboxValue(tbleft_long, item, "left_long", true);
                    double.TryParse(item["left_long"].ToString(), out left_long);
                    UtilsDatabase.SetTextboxValue(tbright_long, item, "right_long", true);
                    double.TryParse(item["right_long"].ToString(), out right_long);
                    UtilsDatabase.SetTextboxValue(tbtop_lat, item, "top_lat", true);
                    double.TryParse(item["top_lat"].ToString(), out top_lat);
                }

                //UtilsDatabase.SetPhotoFileToPicturebox(tbphoto_file.Text, _IdDuAn, areaImg);

                SetImage();
                LoadOLuoi();
            }

            else
            {
                this.Text = "Thêm mới khu vực";
            }

            if (jsonInfo != null)
            {
                UtilsDatabase.PopularComboboxTinhJson(cbparentId, jsonInfo.parentId);
                UtilsDatabase.PopularComboboxTinhJson(cbparentIdDistrict, jsonInfo.parentIdDistrict);
                UtilsDatabase.PopularComboboxTinhJson(cbparentIdCommune, jsonInfo.parentIdCommune);
                UtilsDatabase.PopularComboboxJson(cbvillageId, jsonInfo.villageId);
                UtilsDatabase.PopularStringTextboxJson(tbcode, jsonInfo.code);
                UtilsDatabase.PopularStringTextboxJson(tbaddress, jsonInfo.address);
                UtilsDatabase.PopularNumberTextboxJson(tbposition_long, jsonInfo.positionLong);
                UtilsDatabase.PopularNumberTextboxJson(tbposition_lat, jsonInfo.positionLat);
                UtilsDatabase.PopularNumberTextboxJson(tbareamap, jsonInfo.areamap);
                UtilsDatabase.PopularStringTextboxJson(tbpolygonGeomST, jsonInfo.polygonGeom);
                //UtilsDatabase.PopularStringPolygonTextboxJson(tbpolygonGeomST, jsonInfo.polygonGeom);
                UtilsDatabase.PopularNumberTextboxJson(tbpositionlongvn2000, jsonInfo.positionLongVN2000);
                UtilsDatabase.PopularNumberTextboxJson(tbpositionlatvn2000, jsonInfo.positionLatVN2000);
                UtilsDatabase.PopularNumberTextboxJson(tbmeridian, jsonInfo.meridian);
                UtilsDatabase.PopularComboboxJson(cbpzone, jsonInfo.pzone);
                UtilsDatabase.PopularNumberTextboxJson(tblx, jsonInfo.lx);
                UtilsDatabase.PopularNumberTextboxJson(tbly, jsonInfo.ly);
                UtilsDatabase.PopularStringPolygonTextboxJson(tbvn2000, jsonInfo.polygonGeomST);
                UtilsDatabase.PopularNumberTextboxJson(tbleft_long, jsonInfo.left_long);
                UtilsDatabase.PopularNumberTextboxJson(tbtop_lat, jsonInfo.top_lat);
                UtilsDatabase.PopularNumberTextboxJson(tbbottom_lat, jsonInfo.bottom_lat);
                UtilsDatabase.PopularNumberTextboxJson(tbright_long, jsonInfo.right_long);
                UtilsDatabase.PopularStringTextboxJson(tbphoto_file, jsonInfo.photo_file);
                LoadOLuoi(jsonInfo.cecmProgramAreaMapSub_lstSubTable);
            }
        }

        private void ThemMoiKhuVuc_Load(object sender, EventArgs e)
        {
            LoadDataForm(null);
            //LoadOLuoi();
        }

        private void LoadOLuoi(List<OLuoi> lst_oLuoi = null)
        {
            if(lst_oLuoi != null)
            {
                foreach(OLuoi oLuoi in lst_oLuoi)
                {
                    oLuoi.gid = -1;
                    int index = DgvOluoi.Rows.Add(DgvOluoi.Rows.Count + 1, oLuoi.o_id, oLuoi.khaosat_deptIdST, oLuoi.raPha_deptIdST, Resources.Modify, Resources.DeleteRed);
                    DgvOluoi.Rows[index].Tag = oLuoi;
                }
                return;
            }
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new DataTable();
            string sql =
                "select " +
                "gid, " +
                "o_id, " +
                "autoDivide, " +
                "khaosat_deptid, " +
                "dept_ks.name as khaosat_deptidST, " +
                "rapha_deptid, " +
                "dept_rp.name as rapha_deptidST, " +
                "dividerAllGrid, " +
                "isCustomAllGrid, " +
                "distanceAllGrid, " +
                "acutangeAllGrid, " +
                "lat_center, " +
                "lat_corner1, " +
                "lat_corner2, " +
                "lat_corner3, " +
                "lat_corner4, " +
                "long_center, " +
                "long_corner1, " +
                "long_corner2, " +
                "long_corner3, " +
                "long_corner4, " +
                "isCustom1, " +
                "isCustom2, " +
                "isCustom3, " +
                "isCustom4, " +
                "acuteAngle1, " +
                "acuteAngle2, " +
                "acuteAngle3, " +
                "acuteAngle4, " +
                "distance1, " +
                "distance2, " +
                "distance3, " +
                "distance4 " +
                "from OLuoi ol " +
                "left join cert_department dept_ks on dept_ks.id = ol.khaosat_deptid " +
                "left join cert_department dept_rp on dept_rp.id = ol.rapha_deptid " +
                "where cecm_program_areamap_id = " + _IdVungDuAn;
            sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
            sqlAdapter.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            DgvOluoi.Rows.Clear();
            //int indexRow = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                OLuoi oluoi = new OLuoi();
                //gid
                string gid = dr["gid"].ToString();
                oluoi.gid = long.Parse(gid);
                //o_id
                string o_id = dr["o_id"].ToString();
                oluoi.o_id = o_id;
                //autoDivide
                oluoi.autoDivide = int.TryParse(dr["autoDivide"].ToString(), out int autoDivide) ? autoDivide : 0;
                //khaosat_deptid
                long khaosat_deptid = -1;
                long.TryParse(dr["khaosat_deptid"].ToString(), out khaosat_deptid);
                oluoi.khaosat_deptId = khaosat_deptid;
                //khaosat_deptidST
                string khaosat_deptidST = dr["khaosat_deptidST"].ToString();
                //rapha_deptid
                long rapha_deptid = -1;
                long.TryParse(dr["rapha_deptid"].ToString(), out rapha_deptid);
                oluoi.raPha_deptId = rapha_deptid;
                //rapha_deptidST
                string rapha_deptidST = dr["rapha_deptidST"].ToString();
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

                //indexRow++;
                List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "cecm_program_area_line", "cecmprogramareasub_id", oluoi.gid.ToString());
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
                        line.start_x = lattStart;
                        line.start_y = longtStart;
                        line.end_x = lattEnd;
                        line.end_y = longtEnd;
                        lstRanhDo.Add(line);
                    }

                }
                oluoi.lstRanhDo = lstRanhDo;

                int index = DgvOluoi.Rows.Add(DgvOluoi.Rows.Count + 1, o_id, khaosat_deptidST, rapha_deptidST, Resources.Modify, Resources.DeleteRed);
                DgvOluoi.Rows[index].Tag = oluoi;

                //lst_oluoi.Add(oluoi);

            }
        }

        private void cbparentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            UtilsDatabase.LoadHuyen(_ExtraInfoConnettion, cbparentId, cbparentIdDistrict);
            cbparentIdCommune.Items.Clear();
        }

        private void cbparentIdDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            UtilsDatabase.LoadXa(_ExtraInfoConnettion, cbparentIdDistrict, cbparentIdCommune);
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            if (UpdateToDatabase(true))
            {
                this.Close();
            }
        }

        public bool UpdateToDatabase(bool isShowMess)
        {
            try
            {
                SqlCommand cmd = null;
                //string sql_sub_table = string.Format("DELETE FROM OLuoi WHERE cecm_program_areamap_id = {0} ", _IdVungDuAn);
                if (_IdVungDuAn < 0)
                {
                    // Chua co tao moi
                    cmd = new SqlCommand(string.Format("INSERT INTO cecm_program_area_map "
                        + "(parentid,"
                        + "cecm_program_id,"
                        + "parentidcommune,"
                        + "villageid,"
                        + "code,"
                        + "address,"
                        + "position_lat,"
                        + "position_long,"
                        + "areamap,"
                        + "polygongeomst,"
                        + "positionlongvn2000,"
                        + "positionlatvn2000,"
                        + "meridian,"
                        + "pzone,"
                        + "lx,"
                        + "ly,"
                        + "vn2000,"
                        + "photo_file,"
                        + "bottom_lat,"
                        + "left_long,"
                        + "right_long,"
                        + "top_lat,"
                        + "parentiddistrict)"

                        + "VALUES("
                        + "@parentid,"
                        + "@cecm_program_id,"
                        + "@parentidcommune,"
                        + "@villageid,"
                        + "@code,"
                        + "@address,"
                        + "@position_lat,"
                        + "@position_long,"
                        + "@areamap,"
                        + "@polygongeomst,"
                        + "@positionlongvn2000,"
                        + "@positionlatvn2000,"
                        + "@meridian,"
                        + "@pzone,"
                        + "@lx,"
                        + "@ly,"
                        + "@vn2000,"
                        + "@photo_file,"
                        + "@bottom_lat,"
                        + "@left_long,"
                        + "@right_long,"
                        + "@top_lat,"
                        + "@parentiddistrict)"), _ExtraInfoConnettion.Connection as SqlConnection);
                }
                else
                {
                    cmd = new SqlCommand(
                        string.Format("UPDATE cecm_program_area_map SET "
                        + "parentid = @parentid,"
                        + "cecm_program_id = @cecm_program_id,"
                        + "parentidcommune = @parentidcommune,"
                        + "villageid = @villageid,"
                        + "code = @code,"
                        + "address = @address,"
                        + "position_lat = @position_lat,"
                        + "position_long = @position_long,"
                        + "areamap = @areamap,"
                        + "polygongeomst = @polygongeomst,"
                        + "positionlongvn2000 = @positionlongvn2000,"
                        + "positionlatvn2000 = @positionlatvn2000,"
                        + "meridian = @meridian,"
                        + "pzone = @pzone,"
                        + "lx = @lx,"
                        + "ly = @ly,"
                        + "vn2000 = @vn2000,"
                        + "photo_file = @photo_file,"
                        + "bottom_lat = @bottom_lat,"
                        + "left_long = @left_long,"
                        + "right_long = @right_long,"
                        + "top_lat = @top_lat"
                        + " WHERE cecm_program_area_map.id = {0} ", _IdVungDuAn)

                        
                        
                        , 
                        _ExtraInfoConnettion.Connection as SqlConnection);
                }

                var texboxTemp = new TextBox();
                texboxTemp.Text = _IdDuAn.ToString();
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", texboxTemp, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_station_id", texboxTemp, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "parentid", cbparentId, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "parentiddistrict", cbparentIdDistrict, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "parentidcommune", cbparentIdCommune, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "villageid", cbvillageId, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "code", tbcode, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", tbaddress, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "position_lat", tbposition_lat, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "position_long", tbposition_long, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "areamap", tbareamap, true, SqlDbType.Float);
                //UtilsDatabase.UpdateDataPolygonSqlParameter(cmd, "polygongeomst", tbpolygonGeomST);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "polygongeomst", tbpolygonGeomST, false, SqlDbType.NVarChar);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "positionlongvn2000", tbpositionlongvn2000, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "positionlatvn2000", tbpositionlatvn2000, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "meridian", tbmeridian, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "pzone", cbpzone, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "lx", tblx, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "ly", tbly, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataPolygonSqlParameter(cmd, "vn2000", tbvn2000);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "photo_file", tbphoto_file, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "bottom_lat", tbbottom_lat, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "left_long", tbleft_long, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "right_long", tbright_long, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "top_lat", tbtop_lat, true, SqlDbType.Float);
                int temp = 0;
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                temp = cmd.ExecuteNonQuery();
                for (int i = 0; i < DgvOluoi.Rows.Count; i++)
                {
                    OLuoi oLuoi_temp = (OLuoi)DgvOluoi.Rows[i].Tag;
                    //Bỏ qua trường hợp edit
                    if (oLuoi_temp.gid > 0)
                    {
                        continue;
                    }
                    if (_IdVungDuAn <= 0)
                    {
                        _IdVungDuAn = UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, "cecm_program_area_map");
                    }
                    SqlCommand cmd_sub = new SqlCommand(
                        "INSERT INTO OLuoi(" +
                        "cecm_program_id, " +
                        "cecm_program_areamap_id, " +
                        "autoDivide, " +
                        "khaosat_deptid, " +
                        "rapha_deptid," +
                        "lat_center," +
                        "lat_corner1," +
                        "lat_corner2," +
                        "lat_corner3," +
                        "lat_corner4," +
                        "long_center," +
                        "long_corner1," +
                        "long_corner2," +
                        "long_corner3," +
                        "long_corner4," +
                        "dividerAllGrid," +
                        "isCustomAllGrid," +
                        "distanceAllGrid," +
                        "acutangeAllGrid," +
                        "distance1," +
                        "distance2," +
                        "distance3," +
                        "distance4," +
                        "isCustom1," +
                        "isCustom2," +
                        "isCustom3," +
                        "isCustom4," +
                        "acuteAngle1," +
                        "acuteAngle2," +
                        "acuteAngle3," +
                        "acuteAngle4," +
                        "o_id) VALUES " +
                        "(" +
                        _IdDuAn + "," +
                        _IdVungDuAn + "," +
                        "@autoDivide, " +
                        "@khaosat_deptid," +
                        "@rapha_deptid," +
                        "@lat_center," +
                        "@lat_corner1," +
                        "@lat_corner2," +
                        "@lat_corner3," +
                        "@lat_corner4," +
                        "@long_center," +
                        "@long_corner1," +
                        "@long_corner2," +
                        "@long_corner3," +
                        "@long_corner4," +
                        "@dividerAllGrid," +
                        "@isCustomAllGrid," +
                        "@distanceAllGrid," +
                        "@acutangeAllGrid," +
                        "@distance1," +
                        "@distance2," +
                        "@distance3," +
                        "@distance4," +
                        "@isCustom1," +
                        "@isCustom2," +
                        "@isCustom3," +
                        "@isCustom4," +
                        "@acuteAngle1," +
                        "@acuteAngle2," +
                        "@acuteAngle3," +
                        "@acuteAngle4," +
                        "@o_id)", _ExtraInfoConnettion.Connection as SqlConnection);
                    OLuoi oLuoi = DgvOluoi.Rows[i].Tag as OLuoi;
                    
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
                    cmd_sub.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                    cmd_sub.ExecuteNonQuery();
                    int id_oluoi = UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, "OLuoi");
                    if(oLuoi.lstRanhDo != null)
                    {
                        foreach(CecmProgramAreaLineDTO dto in oLuoi.lstRanhDo)
                        {
                            SqlCommand cmd_line = new SqlCommand(
                                "INSERT INTO cecm_program_area_line(" +
                                "code," +
                                "corner_num," +
                                "start_x," +
                                "start_y," +
                                "end_x," +
                                "end_y," +
                                "cecmprogramareasub_id," +
                                "cecmprogramareamap_id," +
                                "cecmprogram_id) " +
                                "VALUES(" +
                                "@code," +
                                "@corner_num," +
                                "@start_x," +
                                "@start_y," +
                                "@end_x," +
                                "@end_y," +
                                "@cecmprogramareasub_id," +
                                "@cecmprogramareamap_id," +
                                "@cecmprogram_id)", _ExtraInfoConnettion.Connection as SqlConnection);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "code", dto.code, false);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "corner_num", dto.corner_num.ToString(), true, SqlDbType.Int);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "start_x", dto.start_x.ToString(), true, SqlDbType.Float);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "start_y", dto.start_y.ToString(), true, SqlDbType.Float);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "end_x", dto.end_x.ToString(), true, SqlDbType.Float);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "end_y", dto.end_y.ToString(), true, SqlDbType.Float);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "cecmprogramareasub_id", id_oluoi.ToString(), true, SqlDbType.BigInt);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "cecmprogramareamap_id", _IdVungDuAn.ToString(), true, SqlDbType.BigInt);
                            UtilsDatabase.UpdateDataSqlParameter(cmd_line, "cecmprogram_id", _IdDuAn.ToString(), true, SqlDbType.BigInt);
                            cmd_line.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                            cmd_line.ExecuteNonQuery();
                        }
                    }
                    

                }
                

                if (temp > 0)
                {
                    if (isShowMess)
                        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                    return true;
                }
                else
                {
                    if (isShowMess)
                        MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
            {
                tbphoto_file.Text = filePath;
                UtilsDatabase.SetPhotoFileToPicturebox(tbphoto_file.Text, _IdDuAn, areaImg);
            }
        }

        private void tbphoto_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbphoto_file.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void SetImage()
        {
            var lstAllAreaMap = UtilsDatabase.GetAllCecmProgramAreaMapByProgramId(_ExtraInfoConnettion, _IdDuAn);
            var folderTemp = AppUtils.GetFolderTemp(_IdDuAn);

            foreach (var dto in lstAllAreaMap)
            {
                if (dto.cecmProgramAreaMapId != _IdVungDuAn.ToString())
                    continue;

                if (dto.right_long == null || dto.left_long == null
                    || dto.top_lat == null || dto.bottom_lat == null
                    || float.Parse(dto.right_long) - float.Parse(dto.left_long) <= 0
                    || float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat) <= 0)
                    continue;

                //PictureBox pictureBox = new PictureBox();

                //pictureBox.ImageLocation = System.IO.Path.Combine(folderTemp, dto.photo_file);
                //pictureBox.Width = (int)(areaImg.Width * (float.Parse(dto.right_long) - float.Parse(dto.left_long)) / (float.Parse(dto.right_long) - float.Parse(dto.left_long)));
                //pictureBox.Height = (int)(areaImg.Height * (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)) / (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)));
                //int x = (int)(areaImg.Width * (float.Parse(dto.left_long) - float.Parse(dto.left_long)) / (float.Parse(dto.right_long) - float.Parse(dto.left_long)));
                //int y = (int)(areaImg.Height * (1 - ((float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)) / (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)))));
                //pictureBox.Location = new Point(x, y);
                //pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                //areaImg.Controls.Add(pictureBox);
                //pictureBox.Paint += new PaintEventHandler(delegate (object sender2, PaintEventArgs e2)
                //{
                //    Graphics g = e2.Graphics;
                //    //g.SmoothingMode = SmoothingMode.AntiAlias;
                //    using (Pen pen = new Pen(Color.Red, 1))
                //    {
                //        //CecmProgramAreaMapDTO dto2 = (CecmProgramAreaMapDTO)cbAreaMap.SelectedItem;
                //        //string polygon = dto.polygonGeom;
                //        //MessageBox.Show(polygon);
                //        List<PointF[]> lst = Utils.AppUtils.convertMultiPolygon(dto, pictureBox.Size);
                //        foreach (PointF[] points in lst)
                //        {
                //            g.DrawPolygon(pen, points);
                //            g.FillPolygon(new SolidBrush(Color.FromArgb(85, 168, 50, 147)), points);
                //        }
                //    }
                //});
                //pictureBox.BorderStyle = BorderStyle.FixedSingle;

                areaImg.ImageLocation = System.IO.Path.Combine(folderTemp, dto.photo_file);
                //pictureBox.Width = (int)(areaImg.Width * (float.Parse(dto.right_long) - float.Parse(dto.left_long)) / (float.Parse(dto.right_long) - float.Parse(dto.left_long)));
                //pictureBox.Height = (int)(areaImg.Height * (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)) / (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)));
                //int x = (int)(areaImg.Width * (float.Parse(dto.left_long) - float.Parse(dto.left_long)) / (float.Parse(dto.right_long) - float.Parse(dto.left_long)));
                //int y = (int)(areaImg.Height * (1 - ((float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)) / (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat)))));
                //pictureBox.Location = new Point(x, y);
                //pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                //areaImg.Controls.Add(pictureBox);
                areaImg.Paint += new PaintEventHandler(delegate (object sender2, PaintEventArgs e2)
                {
                    Graphics g = e2.Graphics;
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        //CecmProgramAreaMapDTO dto2 = (CecmProgramAreaMapDTO)cbAreaMap.SelectedItem;
                        //string polygon = dto.polygonGeom;
                        //MessageBox.Show(polygon);
                        try
                        {
                            List<PointF[]> lstPolygon = AppUtils.convertMultiPolygonNew(dto, areaImg.Size);
                            foreach (PointF[] Polygon in lstPolygon)
                            {
                                g.DrawPolygon(pen, Polygon);
                                g.FillPolygon(new SolidBrush(Color.FromArgb(85, 168, 50, 147)), Polygon);
                            }
                        }
                        catch (Exception)
                        {

                        }
                        
                    }
                });
                //pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        public DataGridView getDgvOluoi()
        {
            return DgvOluoi;
        }

        private void btnThemOLuoi_Click(object sender, EventArgs e)
        {
            ThemMoiOLuoi frm = new ThemMoiOLuoi(this);
            double.TryParse(tbleft_long.Text, out left_long);
            double.TryParse(tbright_long.Text, out right_long);
            double.TryParse(tbtop_lat.Text, out top_lat);
            double.TryParse(tbbottom_lat.Text, out bottom_lat);
            frm.ShowDialog();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvOluoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            var dgvRow = DgvOluoi.Rows[e.RowIndex];
            //string str = dgvRow.Tag as string;
            //int id = int.Parse(str);
            OLuoi oluoi = dgvRow.Tag as OLuoi;
            if (e.ColumnIndex == CotSua.Index && e.RowIndex >= 0)
            {
                ThemMoiOLuoi frm = new ThemMoiOLuoi(this, dgvRow);
                double.TryParse(tbleft_long.Text, out left_long);
                double.TryParse(tbright_long.Text, out right_long);
                double.TryParse(tbtop_lat.Text, out top_lat);
                double.TryParse(tbbottom_lat.Text, out bottom_lat);
                frm.ShowDialog();
            }
            else if (e.ColumnIndex == CotXoa.Index && e.RowIndex >= 0)
            {
                //if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "cecm_program_area_map", "id", id.ToString()))
                //    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                //else
                //    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                //LoadDataKhuVucDuAn(_IdDuAn);
                if (MessageBox.Show("Xác nhận xóa dữ liệu", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if(oluoi.gid > 0)
                    {
                        UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "OLuoi", "gid", oluoi.gid.ToString());
                    }
                    
                    DgvOluoi.Rows.Remove(dgvRow);
                    for (int i = e.RowIndex; i < DgvOluoi.Rows.Count; i++)
                    {
                        DgvOluoi.Rows[i].Cells[CotStt.Index].Value = (int)DgvOluoi.Rows[i].Cells[CotStt.Index].Value - 1;
                    }
                }
            }
        }

        private void tbcode_Validating(object sender, CancelEventArgs e)
        {
            if(tbcode.Text.Trim() == "")
            {
                e.Cancel = true;
                tbcode.Focus();
                errorProvider.SetError(tbcode, "Chưa nhập mã");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(tbcode, "");
            }
        }
    }
}