using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MIConvexHull;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Command
{
    class DHDACommand
    {
        private List<string> lstMayBom = new List<string>();
        private List<string> lstMayMin = new List<string>();

        public void Execute(long idDA)
        {
            Dictionary<long, Dictionary<string, List<InfoConnect>>> idKV__Points = GetPointDatabase(idDA);
            MapMenuCommand.LoadPoints(idKV__Points);
            foreach (var pair_idKV__Points in idKV__Points)
            {
                Dictionary<string, List<InfoConnect>> code__Points = pair_idKV__Points.Value;
                List<InfoConnect> lstPointBomb = new List<InfoConnect>();
                foreach(var pair_code__Points in code__Points)
                {
                    if (lstMayBom.Contains(pair_code__Points.Key))
                    {
                        lstPointBomb.AddRange(pair_code__Points.Value);
                    }
                }
                IList<DefaultVertex2D> hullPoints = TINCommand.Build(lstPointBomb);
                List<double> xPoints = new List<double>();
                List<double> yPoints = new List<double>();
                if (hullPoints != null)
                {
                    foreach(DefaultVertex2D point in hullPoints)
                    {
                        double latt = 0, longt = 0;
                        AppUtils.ToLatLon(point.X, point.Y, ref latt, ref longt, "48N");
                        xPoints.Add(longt);
                        yPoints.Add(latt);
                    }
                    MapMenuCommand.drawPolygon(xPoints.ToArray(), yPoints.ToArray());
                }
            }
        }

        private List<long> GetAllIDKV(long idDA)
        {
            List<long> idKVs = new List<long>();
            SqlDataAdapter sqlAdapter = null;
            DataTable datatable = new DataTable();
            string sql = "SELECT " +
                "id " +
                "FROM cecm_program_area_map where cecm_program_id = " + idDA;
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                if(long.TryParse(dr["id"].ToString(), out long idKV))
                {
                    idKVs.Add(idKV);
                }
            }
            return idKVs;
        }

        private Dictionary<long, Dictionary<string, List<InfoConnect>>> GetPointDatabase(long idDuAn)
        {
            //Dictionary<long, Dictionary<string, List<InfoConnect>>> retValTemp = new Dictionary<long, Dictionary<string, List<InfoConnect>>>();
            Dictionary<long, Dictionary<string, List<InfoConnect>>> retVal = new Dictionary<long, Dictionary<string, List<InfoConnect>>>();
            try
            {
                MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
                AppUtils.LoadRecentInput(frm.tbHeSoMayDoBom, AppUtils.DefaultNanoTesla.ToString());
                AppUtils.LoadRecentInput(frm.tbHeSoMayDoMin, AppUtils.DefaultNanoTeslaMin.ToString());
                double heSoMayDoBom = double.Parse(frm.tbHeSoMayDoBom.Text);
                double heSoMayDoMin = double.Parse(frm.tbHeSoMayDoMin.Text);

                // Get all duong bao
                //var oidDuongBaos = DBUtils.SelectAllDuongBao();
                //if (oidDuongBaos != null && oidDuongBaos.Count != 0)
                //{
                //    using (Transaction tr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
                //    {
                //        foreach (ObjectId oid in oidDuongBaos)
                //        {
                //            MgdAcDbVNTerrainBaseRegion mBaseRegion = oid.GetObject(OpenMode.ForRead) as MgdAcDbVNTerrainBaseRegion;
                //            if (mBaseRegion == null)
                //                continue;

                //            if (retValTemp.ContainsKey(mBaseRegion) == false)
                //                retValTemp.Add(mBaseRegion, new Dictionary<string, List<DataDatabase>>());
                //        }

                //        tr.Commit();
                //    }
                //}

                List<long> idKVs = GetAllIDKV(idDuAn);
                //foreach(int idKV in idKVs)
                //{
                //    retValTemp.Add(idKV, new Dictionary<string, List<InfoConnect>>());
                //}

                // Get all machine boom in du an
                List<string> lstCodeMachine = new List<string>();
                var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "Cecm_ProgramMachineBomb", "cecm_program_id", idDuAn.ToString());
                foreach (var item in lstDatarow)
                {
                    lstCodeMachine.Add(item["mac_id"].ToString());
                    if (item["IsMachineBoom"].ToString() == "1" || item["code"].ToString().StartsWith("B"))
                    {
                        lstMayBom.Add(item["mac_id"].ToString());
                    }
                    else
                    {
                        lstMayMin.Add(item["mac_id"].ToString());
                    }
                }

                foreach (var idKV in idKVs)
                {
                    //var boundingBox = vungDuAn.Key.Bounds;
                    //if (boundingBox == null)
                    //    continue;

                    //List<Point3d> lstP = new List<Point3d>();
                    //for (int i = 0; i < vungDuAn.Key.NumberOfVertices; i++)
                    //    lstP.Add(vungDuAn.Key.GetPoint3dAt(i));
                    //lstP.Add(vungDuAn.Key.GetPoint3dAt(0));

                    //Point3d minP = boundingBox.Value.MinPoint;
                    //Point3d maxP = boundingBox.Value.MaxPoint;

                    Dictionary<string, List<InfoConnect>> dicMayDoDiemDo = new Dictionary<string, List<InfoConnect>>();

                    foreach (var mayDo in lstCodeMachine)
                    {
                        //var lstDataBomb = ReadDataMongo(idKV, true);
                        //var lstDataMine = ReadDataMongo(idKV, false);
                        //foreach (var infoConnect in lstDataBomb)
                        //{
                        //    infoConnect.the_value *= heSoMayDoBom;
                        //}
                        //foreach (var infoConnect in lstDataMine)
                        //{
                        //    infoConnect.the_value *= heSoMayDoMin;
                        //}
                        //dicMayDoDiemDo.Add(true, lstDataBomb);
                        //dicMayDoDiemDo.Add(false, lstDataMine);
                        var lstData = ReadDataMongo(idKV);

                        double x, y, z;
                        //byte bitSensOut = byte.MinValue;
                        //DateTime dateTimeMeasure = DateTime.MinValue;
                        //DateTime dateTimeRecv = DateTime.MinValue;

                        List<InfoConnect> lDataValDatabase = new List<InfoConnect>();
                        foreach (var infoConnect in lstData)
                        {
                            //if (!AppUtils.IsNumber(dr.lat_value) || !AppUtils.IsNumber(dr.long_value) || !AppUtils.IsNumber(dr.long_value))
                            //    continue;

                            x = infoConnect.lat_value;
                            y = infoConnect.long_value;
                            z = infoConnect.the_value;

                            if (x == 0 && y == 0)
                                continue;

                            if (infoConnect.isMachineBom)
                                infoConnect.the_value *= heSoMayDoBom;
                            else
                                infoConnect.the_value *= heSoMayDoMin;


                            //try
                            //{

                            //    if (_LastUpdateDWG < dr.time_action)
                            //        _LastUpdateDWG = dr.time_action;
                            //}
                            //catch
                            //{
                            //}


                            Point3d pt = new Point3d(x, y, z);

                            //CheckPointInsidePolygon pointInside = new CheckPointInsidePolygon(lstP, pt);
                            //if (pointInside.GetPointPosition() != Positions.Outside)
                            //{
                            //    DataDatabase data = new DataDatabase();
                            //    data.point3dLocation = pt;
                            //    data.bitSent = bitSensOut;
                            //    data.DateTimeMeasureOut = dateTimeMeasure;
                            //    data.DateTimeRecvOut = dateTimeRecv;
                            //    data.isMachineBom = dr.isMachineBom;
                            //    data.code = dr.code;

                            //    lDataValDatabase.Add(data);
                            //}
                            lDataValDatabase.Add(infoConnect);
                        }

                        if (dicMayDoDiemDo.ContainsKey(mayDo) == false)
                            dicMayDoDiemDo.Add(mayDo, lDataValDatabase);
                    }

                    retVal.Add(idKV, dicMayDoDiemDo);
                    //retValTemp[vungDuAn.Key] = dicMayDoDiemDo;
                }
            }
            catch (System.Exception ex)
            {

            }

            return retVal;
        }

        private List<InfoConnect> ReadDataMongo(string maMayDo, double minLat, double maxLat, double minLong, double maxLong)
        {
            try
            {
                MongoClient dbClient = frmLoggin.mgCon;
                var database = dbClient.GetDatabase("db_cecm");
                var collection = database.GetCollection<InfoConnect>("cecm_data");

                var builder = Builders<InfoConnect>.Filter;
                var filter = builder.And(
                builder.Eq(z => z.code, maMayDo),
                builder.Gt(z => z.lat_value, minLat),   //min lat_value
                builder.Lt(z => z.lat_value, maxLat),   //max lat_value
                builder.Gt(z => z.long_value, minLong),  //min long_value
                builder.Lt(z => z.long_value, maxLong)   //max long_value
                );

                var zooWithAnimalFilter = Builders<InfoConnect>.Filter.Eq(z => z.code, maMayDo);

                return collection.Find(zooWithAnimalFilter).ToList();
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return new List<InfoConnect>();
            }
        }

        private List<InfoConnect> ReadDataMongo(long idKV)
        {
            try
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name, polygongeomst FROM cecm_program_area_map where id = " + idKV), frmLoggin.sqlCon);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                MongoClient dbClient = frmLoggin.mgCon;
                var database = dbClient.GetDatabase("db_cecm");
                var collection = database.GetCollection<InfoConnect>("cecm_data");

                List<FilterDefinition<InfoConnect>> predicates = new List<FilterDefinition<InfoConnect>>();
                var builder = Builders<InfoConnect>.Filter;

                if (datatable.Rows.Count > 0)
                {     
                    DataRow dr_KV = datatable.Rows[0];
                    string wkt = dr_KV["polygongeomst"].ToString();
                    List<FilterDefinition<InfoConnect>> predicatesPolygon = new List<FilterDefinition<InfoConnect>>();
                    List<GeoJsonPolygon<GeoJson2DCoordinates>> polygons = AppUtils.GetPolygon(wkt);
                    //foreach(GeoJsonPolygon<GeoJson2DCoordinates> polygon in multipolygon)
                    //{
                    //    predicatesPolygon.Add(builder.GeoWithin(item => new double[] { item.long_value, item.lat_value }, polygon));
                    //}
                    if (polygons.Count > 0)
                    {
                        var filter = builder.GeoWithin(item => item.coordinate, polygons[0]);
                        return collection.Find(filter).ToList();
                    }
                }
                return new List<InfoConnect>();
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return new List<InfoConnect>();
            }
        }

        private List<InfoConnect> ReadDataMongo(long idKV, bool isMachineBomb)
        {
            try
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name, polygongeomst FROM cecm_program_area_map where id = " + idKV), frmLoggin.sqlCon);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);

                MongoClient dbClient = frmLoggin.mgCon;
                var database = dbClient.GetDatabase("db_cecm");
                var collection = database.GetCollection<InfoConnect>("cecm_data");

                List<FilterDefinition<InfoConnect>> predicates = new List<FilterDefinition<InfoConnect>>();
                var builder = Builders<InfoConnect>.Filter;

                if (datatable.Rows.Count > 0)
                {
                    DataRow dr_KV = datatable.Rows[0];
                    string wkt = dr_KV["polygongeomst"].ToString();
                    List<FilterDefinition<InfoConnect>> predicatesPolygon = new List<FilterDefinition<InfoConnect>>();
                    List<GeoJsonPolygon<GeoJson2DCoordinates>> polygons = AppUtils.GetPolygon(wkt);
                    //foreach(GeoJsonPolygon<GeoJson2DCoordinates> polygon in multipolygon)
                    //{
                    //    predicatesPolygon.Add(builder.GeoWithin(item => new double[] { item.long_value, item.lat_value }, polygon));
                    //}
                    if (polygons.Count > 0)
                    {
                        predicates.Add(builder.GeoWithin(item => item.coordinate, polygons[0]));
                        predicates.Add(builder.Where(item => item.isMachineBom == isMachineBomb));
                        var filter = builder.And(predicates);
                        return collection.Find(filter).ToList();
                    }
                }
                return new List<InfoConnect>();
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return new List<InfoConnect>();
            }
        }
    }
}
