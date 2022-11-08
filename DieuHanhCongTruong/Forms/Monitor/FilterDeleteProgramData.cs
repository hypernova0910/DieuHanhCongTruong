using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
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

namespace VNRaPaBomMin
{
    public partial class FilterDeleteProgramData : Form
    {
        private long idDA;

        public FilterDeleteProgramData(long idDA)
        {
            InitializeComponent();
            this.idDA = idDA;
        }

        private void FilterDeleteProgramData_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name, polygongeomst FROM cecm_program_area_map where cecm_program_id = " + idDA), frmLoggin.sqlCon);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                DataRow dr2 = datatable.NewRow();
                dr2["id"] = -1;
                dr2["name"] = "Chưa chọn vùng dự án";
                datatable.Rows.InsertAt(dr2, 0);
                cbKhuVuc.DataSource = datatable;
                cbKhuVuc.DisplayMember = "name";
                cbKhuVuc.ValueMember = "id";
                
            }
            catch (Exception ex)
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
                    "gid, o_id, " +
                    "long_corner1, lat_corner1, " +
                    "long_corner2, lat_corner2, " +
                    "long_corner3, lat_corner3, " +
                    "long_corner4, lat_corner4 " +
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataRowView drv_KV = (DataRowView)cbKhuVuc.SelectedItem;
            DataRow dr_KV = drv_KV.Row;

            DataRowView drv_OLuoi = (DataRowView)cbOLuoi.SelectedItem;
            DataRow dr_OLuoi = drv_OLuoi.Row;

            var database = frmLoggin.mgCon.GetDatabase("db_cecm");
            if (database != null)
            {
                var collection = database.GetCollection<InfoConnect>("cecm_data");
                var collection_model = database.GetCollection<InfoConnect>("cecm_data_model");
                var last_model_time = database.GetCollection<LastModelTime>("last_model_time");

                var builder = Builders<InfoConnect>.Filter;
                var builder_last_model_time = Builders<LastModelTime>.Filter;

                List<FilterDefinition<InfoConnect>> predicates = new List<FilterDefinition<InfoConnect>>();
                List<FilterDefinition<LastModelTime>> predicates_last_model_time = new List<FilterDefinition<LastModelTime>>();

                predicates.Add(builder.Where(item => item.project_id == idDA));
                predicates_last_model_time.Add(builder_last_model_time.Where(item => item.cecm_program_id == idDA));

                if ((long)cbKhuVuc.SelectedValue > 0)
                {
                    predicates_last_model_time.Add(builder_last_model_time.Where(item => item.area_id == (long)cbKhuVuc.SelectedValue));
                    if ((long)cbOLuoi.SelectedValue > 0)
                    {
                        predicates_last_model_time.Add(builder_last_model_time.Where(item => item.o_id == (long)cbOLuoi.SelectedValue));
                        if (
                            double.TryParse(dr_OLuoi["long_corner2"].ToString(), out double max_long) &&
                            double.TryParse(dr_OLuoi["lat_corner2"].ToString(), out double max_lat) &&
                            double.TryParse(dr_OLuoi["long_corner4"].ToString(), out double min_long) &&
                            double.TryParse(dr_OLuoi["lat_corner4"].ToString(), out double min_lat)
                        )
                        {
                            double[] minLatLong = AppUtils.ConverLatLongToUTM(min_lat, min_long);
                            double[] maxLatLong = AppUtils.ConverLatLongToUTM(max_lat, max_long);
                            predicates.Add(builder.Where(item => item.long_value <= maxLatLong[1]));
                            predicates.Add(builder.Where(item => item.lat_value <= maxLatLong[0]));
                            predicates.Add(builder.Where(item => item.long_value >= minLatLong[1]));
                            predicates.Add(builder.Where(item => item.lat_value >= minLatLong[0]));
                            //predicates.Add(builder.Where(item => item.coordinate.Coordinates.X <= max_long));
                            //predicates.Add(builder.Where(item => item.coordinate.Coordinates.Y <= max_lat));
                            //predicates.Add(builder.Where(item => item.coordinate.Coordinates.X >= min_long));
                            //predicates.Add(builder.Where(item => item.coordinate.Coordinates.Y >= min_lat));
                        }
                    }
                    else
                    {
                        string wkt = dr_KV["polygongeomst"].ToString();
                        List<FilterDefinition<InfoConnect>> predicatesPolygon = new List<FilterDefinition<InfoConnect>>();
                        List<GeoJsonPolygon<GeoJson2DCoordinates>> polygons = AppUtils.GetPolygon(wkt);
                        //foreach(GeoJsonPolygon<GeoJson2DCoordinates> polygon in multipolygon)
                        //{
                        //    predicatesPolygon.Add(builder.GeoWithin(item => new double[] { item.long_value, item.lat_value }, polygon));
                        //}
                        if(polygons.Count > 0)
                        {
                            predicates.Add(builder.GeoWithin(item => item.coordinate, polygons[0]));
                        }
                        
                    }
                }
                
                if (dtpTimeStart.Checked)
                {
                    predicates.Add(builder.Where(item => item.time_action >= dtpTimeStart.Value));
                }
                if (dtpTimeEnd.Checked)
                {
                    predicates.Add(builder.Where(item => item.time_action <= dtpTimeEnd.Value));
                }

                var filter = builder.And(predicates);
                var filter_last_model_time = builder_last_model_time.And(predicates_last_model_time);

                //var filter = builder.And(
                //    builder.Eq("code", "1050524391326a20"),
                //    builder.Gt("lat_value", 2329395),   //min lat_value
                //    builder.Lt("lat_value", 2329496),   //max lat_value
                //    builder.Gt("long_value", 582551),   //min long_value
                //    builder.Lt("long_value", 582652)    //max long_value
                //);
                //collection.DeleteMany(
                //    item => 
                //    item.project_id == idDA &&
                //    ((long)cbOLuoi.SelectedValue > 0 ? 
                //    (item.long_value > )
                //    : true)
                //);
                long count = collection.Find(filter).CountDocuments();
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa " + count + " bản ghi không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                DeleteResult result = collection.DeleteMany(filter);
                collection_model.DeleteMany(filter);
                last_model_time.DeleteMany(filter_last_model_time);
                MessageBox.Show("Đã xóa " + result.DeletedCount + " bản ghi");
            }
        }

        private void dtpTimeStart_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpTimeEnd.Value < dtpTimeStart.Value)
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
    }
}
