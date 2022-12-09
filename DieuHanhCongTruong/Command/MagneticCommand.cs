using DieuHanhCongTruong.Command.Clustering;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using MIConvexHull;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.Command
{
    class MagneticCommand
    {
        private List<string> lstMayBom = new List<string>();
        private List<string> lstMayMin = new List<string>();
        //public static bool threadMagneticStopped = true;
        //public static bool threadPointStopped = true;
        private static bool threadMagneticBombStopped = true;
        private static bool threadMagneticMineStopped = true;

        private static bool threadPointBombStopped = true;
        private static bool threadPointMineStopped = true;

        public static void Execute(long idDA)
        {
            MagneticCommand command = new MagneticCommand();
            Thread thread = new Thread(() =>
            {
                if (MyMainMenu2.Instance.InvokeRequired)
                {
                    MyMainMenu2.Instance.Invoke(new MethodInvoker(() => {
                        MyMainMenu2.Instance.tsProgressHistory.Visible = true;
                        MyMainMenu2.Instance.tsProgressSurface.Visible = true;
                        MyMainMenu2.Instance.TogglePhanTichMenu(false);
                    }));
                }
                command.ExecutePrivate(idDA);
            });
            thread.IsBackground = true;
            thread.Start();
        }

        public void ExecutePrivate(long idDA)
        {
            MapMenuCommand.ClearPolygon();
            MapMenuCommand.ClearHistoryPoints();
            MapMenuCommand.ClearPointLayers();
            MapMenuCommand.ClearSuspectPoints();
            MapMenuCommand.ClearUserSuspectPoints();
            //Dictionary<long, Dictionary<string, List<InfoConnect>>> idKV__Points = GetPointDatabase(idDA);
            Dictionary<long, Dictionary<bool, List<InfoConnect>>> idKV__PointsUnmodel = GetPointDatabase(idDA, false);
            Dictionary<long, Dictionary<bool, List<InfoConnect>>> idKV__PointsModel = GetPointDatabase(idDA, true);

            TINCommand.triangulations_bomb.Clear();
            TINCommand.triangulations_mine.Clear();
            Thread threadMagneticBomb = new Thread(() =>
            {
                threadMagneticBombStopped = false;
                foreach (var pair_idKV__Points in idKV__PointsUnmodel)
                {
                    List<InfoConnect> lstPointBomb = pair_idKV__Points.Value[true];
                    addFlagFromPoints(lstPointBomb, true);
                    TINCommand.BuildDelaunayTriangulation(lstPointBomb, true, pair_idKV__Points.Key);
                }
                threadMagneticBombStopped = true;
                MyMainMenu2.Instance.Invoke(new MethodInvoker(() => {
                    if (threadMagneticMineStopped)
                    {
                        MyMainMenu2.Instance.tsProgressSurface.Visible = false;
                        MyMainMenu2.Instance.ToggleMagneticMenu(true);
                    }
                }));
                

            });
            Thread threadMagneticMine = new Thread(() =>
            {
                threadMagneticMineStopped = false;
                foreach (var pair_idKV__Points in idKV__PointsUnmodel)
                {
                    List<InfoConnect> lstPointMine = pair_idKV__Points.Value[false];
                    addFlagFromPoints(lstPointMine, false);
                    TINCommand.BuildDelaunayTriangulation(lstPointMine, false, pair_idKV__Points.Key);
                }
                threadMagneticMineStopped = true;
                if (MyMainMenu2.Instance.InvokeRequired)
                {
                    MyMainMenu2.Instance.Invoke(new MethodInvoker(() =>
                    {
                        if (threadMagneticBombStopped)
                        {
                            MyMainMenu2.Instance.tsProgressSurface.Visible = false;
                            MyMainMenu2.Instance.ToggleMagneticMenu(true);
                        }
                    }));
                }
                

            });
            Thread threadPointBomb = new Thread(() =>
            {
                threadPointBombStopped = false;
                foreach (var pair_idKV__Points in idKV__PointsUnmodel)
                {

                    List<InfoConnect> lstPointBomb = pair_idKV__Points.Value[true];

                    MapMenuCommand.LoadPointsHistory(lstPointBomb);
                }
                foreach (var pair_idKV__Points in idKV__PointsModel)
                {

                    List<InfoConnect> lstPointBomb = pair_idKV__Points.Value[true];

                    MapMenuCommand.LoadPointsModelHistory(lstPointBomb);
                }
                threadPointBombStopped = true;
                if (MyMainMenu2.Instance.InvokeRequired)
                {
                    MyMainMenu2.Instance.Invoke(new MethodInvoker(() => {
                        if (threadPointMineStopped)
                        {
                            MyMainMenu2.Instance.tsProgressHistory.Visible = false;
                            MyMainMenu2.Instance.TogglePointMenu(true);
                        }
                        
                    }));
                }
            });
            Thread threadPointMine = new Thread(() =>
            {
                threadPointMineStopped = false;
                Thread.Sleep(500);
                foreach (var pair_idKV__Points in idKV__PointsUnmodel)
                {

                    List<InfoConnect> lstPointMine = pair_idKV__Points.Value[false];

                    MapMenuCommand.LoadPointsHistory(lstPointMine);
                }
                foreach (var pair_idKV__Points in idKV__PointsModel)
                {

                    List<InfoConnect> lstPointMine = pair_idKV__Points.Value[false];

                    MapMenuCommand.LoadPointsModelHistory(lstPointMine);
                }
                threadPointMineStopped = true;
                if (MyMainMenu2.Instance.InvokeRequired)
                {
                    MyMainMenu2.Instance.Invoke(new MethodInvoker(() =>
                    {
                        if (threadPointBombStopped)
                        {
                            MyMainMenu2.Instance.tsProgressHistory.Visible = false;
                            MyMainMenu2.Instance.TogglePointMenu(true);
                        }
                    }));
                }
            });
            threadMagneticBomb.IsBackground = true;
            threadMagneticMine.IsBackground = true;
            threadPointBomb.IsBackground = true;
            threadPointMine.IsBackground = true;
            threadMagneticBomb.Start();
            threadMagneticMine.Start();
            threadPointBomb.Start();
            threadPointMine.Start();
        }

        private static void addFlagFromPoints(List<InfoConnect> lst, bool isBomb)
        {
            //Lấy điểm cám cờ
            List<InfoConnect> lstFlag = new List<InfoConnect>();
            foreach (InfoConnect point in lst)
            {
                if (AppUtils.CheckCamCo(point.bit_sens, out bool isButton1Press))
                {
                    lstFlag.Add(point);
                }
            }
            HierarchicalClustering clustering = new HierarchicalClustering();
            List<InfoConnect> lstFlagClustered = clustering.Cluster(lstFlag);
            foreach (InfoConnect flag in lstFlagClustered)
            {
                Point2d flagLatLong = AppUtils.ConverUTMToLatLong(flag.lat_value, flag.long_value);
                if (isBomb)
                {
                    MapMenuCommand.addFlagBomb(flagLatLong.X, flagLatLong.Y);
                }
                else
                {
                    MapMenuCommand.addFlagMine(flagLatLong.X, flagLatLong.Y);
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

        //private Dictionary<long, Dictionary<string, List<InfoConnect>>> GetPointDatabase(long idDuAn)
        //{
        //    //Dictionary<long, Dictionary<string, List<InfoConnect>>> retValTemp = new Dictionary<long, Dictionary<string, List<InfoConnect>>>();
        //    Dictionary<long, Dictionary<string, List<InfoConnect>>> retVal = new Dictionary<long, Dictionary<string, List<InfoConnect>>>();
        //    try
        //    {
        //        MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
        //        AppUtils.LoadRecentInput(frm.tbHeSoMayDoBom, AppUtils.DefaultNanoTesla.ToString());
        //        AppUtils.LoadRecentInput(frm.tbHeSoMayDoMin, AppUtils.DefaultNanoTeslaMin.ToString());
        //        double heSoMayDoBom = double.Parse(frm.tbHeSoMayDoBom.Text);
        //        double heSoMayDoMin = double.Parse(frm.tbHeSoMayDoMin.Text);

        //        // Get all duong bao
        //        //var oidDuongBaos = DBUtils.SelectAllDuongBao();
        //        //if (oidDuongBaos != null && oidDuongBaos.Count != 0)
        //        //{
        //        //    using (Transaction tr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
        //        //    {
        //        //        foreach (ObjectId oid in oidDuongBaos)
        //        //        {
        //        //            MgdAcDbVNTerrainBaseRegion mBaseRegion = oid.GetObject(OpenMode.ForRead) as MgdAcDbVNTerrainBaseRegion;
        //        //            if (mBaseRegion == null)
        //        //                continue;

        //        //            if (retValTemp.ContainsKey(mBaseRegion) == false)
        //        //                retValTemp.Add(mBaseRegion, new Dictionary<string, List<DataDatabase>>());
        //        //        }

        //        //        tr.Commit();
        //        //    }
        //        //}

        //        List<long> idKVs = GetAllIDKV(idDuAn);
        //        //foreach(int idKV in idKVs)
        //        //{
        //        //    retValTemp.Add(idKV, new Dictionary<string, List<InfoConnect>>());
        //        //}

        //        // Get all machine boom in du an
        //        List<string> lstCodeMachine = new List<string>();
        //        var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "Cecm_ProgramMachineBomb", "cecm_program_id", idDuAn.ToString());
        //        foreach (var item in lstDatarow)
        //        {
        //            lstCodeMachine.Add(item["mac_id"].ToString());
        //            if (item["IsMachineBoom"].ToString() == "1" || item["code"].ToString().StartsWith("B"))
        //            {
        //                lstMayBom.Add(item["mac_id"].ToString());
        //            }
        //            else
        //            {
        //                lstMayMin.Add(item["mac_id"].ToString());
        //            }
        //        }

        //        foreach (var idKV in idKVs)
        //        {
        //            //var boundingBox = vungDuAn.Key.Bounds;
        //            //if (boundingBox == null)
        //            //    continue;

        //            //List<Point3d> lstP = new List<Point3d>();
        //            //for (int i = 0; i < vungDuAn.Key.NumberOfVertices; i++)
        //            //    lstP.Add(vungDuAn.Key.GetPoint3dAt(i));
        //            //lstP.Add(vungDuAn.Key.GetPoint3dAt(0));

        //            //Point3d minP = boundingBox.Value.MinPoint;
        //            //Point3d maxP = boundingBox.Value.MaxPoint;

        //            Dictionary<string, List<InfoConnect>> dicMayDoDiemDo = new Dictionary<string, List<InfoConnect>>();

        //            foreach (var mayDo in lstCodeMachine)
        //            {
        //                //var lstDataBomb = ReadDataMongo(idKV, true);
        //                //var lstDataMine = ReadDataMongo(idKV, false);
        //                //foreach (var infoConnect in lstDataBomb)
        //                //{
        //                //    infoConnect.the_value *= heSoMayDoBom;
        //                //}
        //                //foreach (var infoConnect in lstDataMine)
        //                //{
        //                //    infoConnect.the_value *= heSoMayDoMin;
        //                //}
        //                //dicMayDoDiemDo.Add(true, lstDataBomb);
        //                //dicMayDoDiemDo.Add(false, lstDataMine);
        //                var lstData = ReadDataMongo(idKV, mayDo);

        //                double x, y, z;
        //                //byte bitSensOut = byte.MinValue;
        //                //DateTime dateTimeMeasure = DateTime.MinValue;
        //                //DateTime dateTimeRecv = DateTime.MinValue;

        //                List<InfoConnect> lDataValDatabase = new List<InfoConnect>();
        //                foreach (var infoConnect in lstData)
        //                {
        //                    //if (!AppUtils.IsNumber(dr.lat_value) || !AppUtils.IsNumber(dr.long_value) || !AppUtils.IsNumber(dr.long_value))
        //                    //    continue;

        //                    x = infoConnect.lat_value;
        //                    y = infoConnect.long_value;
        //                    z = infoConnect.the_value;

        //                    if (x == 0 && y == 0)
        //                        continue;

        //                    if (infoConnect.isMachineBom)
        //                        infoConnect.the_value *= heSoMayDoBom;
        //                    else
        //                        infoConnect.the_value *= heSoMayDoMin;


        //                    lDataValDatabase.Add(infoConnect);
        //                }

        //                if (dicMayDoDiemDo.ContainsKey(mayDo) == false)
        //                {
        //                    dicMayDoDiemDo.Add(mayDo, lDataValDatabase);
        //                }
        //                else
        //                {
        //                    dicMayDoDiemDo[mayDo].AddRange(lDataValDatabase);
        //                }
                            
        //            }

        //            retVal.Add(idKV, dicMayDoDiemDo);
        //            //retValTemp[vungDuAn.Key] = dicMayDoDiemDo;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {

        //    }

        //    return retVal;
        //}

        private Dictionary<long, Dictionary<bool, List<InfoConnect>>> GetPointDatabase(long idDuAn, bool model)
        {
            //Dictionary<long, Dictionary<string, List<InfoConnect>>> retValTemp = new Dictionary<long, Dictionary<string, List<InfoConnect>>>();
            Dictionary<long, Dictionary<bool, List<InfoConnect>>> retVal = new Dictionary<long, Dictionary<bool, List<InfoConnect>>>();
            try
            {
                MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
                AppUtils.LoadRecentInput(frm.tbHeSoMayDoBom, AppUtils.DefaultNanoTesla.ToString());
                AppUtils.LoadRecentInput(frm.tbHeSoMayDoMin, AppUtils.DefaultNanoTeslaMin.ToString());
                double heSoMayDoBom = double.Parse(frm.tbHeSoMayDoBom.Text);
                double heSoMayDoMin = double.Parse(frm.tbHeSoMayDoMin.Text);

                List<long> idKVs = GetAllIDKV(idDuAn);

                // Get all machine boom in du an
                //List<string> lstCodeMachine = new List<string>();
                //var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "Cecm_ProgramMachineBomb", "cecm_program_id", idDuAn.ToString());
                //foreach (var item in lstDatarow)
                //{
                //    lstCodeMachine.Add(item["mac_id"].ToString());
                //    if (item["IsMachineBoom"].ToString() == "1" || item["code"].ToString().StartsWith("B"))
                //    {
                //        lstMayBom.Add(item["mac_id"].ToString());
                //    }
                //    else
                //    {
                //        lstMayMin.Add(item["mac_id"].ToString());
                //    }
                //}

                foreach (var idKV in idKVs)
                {

                    Dictionary<bool, List<InfoConnect>> dicMayDoDiemDo = new Dictionary<bool, List<InfoConnect>>();
                    var lstDataBomb = ReadDataMongo(idKV, true, model);
                    var lstDataMine = ReadDataMongo(idKV, false, model);
                    //double maxMine = double.MinValue;
                    //double minMine = double.MaxValue;
                    List<InfoConnect> lstDataBombFinal = new List<InfoConnect>();
                    List<InfoConnect> lstDataMineFinal = new List<InfoConnect>();
                    foreach (var infoConnect in lstDataBomb)
                    {
                        //if(Math.Abs(infoConnect.the_value) >= 6.1)
                        //{
                        //    continue;
                        //}
                        infoConnect.the_value *= heSoMayDoBom;
                        lstDataBombFinal.Add(infoConnect);
                    }
                    
                    foreach (var infoConnect in lstDataMine)
                    {
                        if (infoConnect.the_value > 13)
                        {
                            continue;
                        }
                        //infoConnect.the_value *= heSoMayDoMin;
                        lstDataMineFinal.Add(infoConnect);
                    }
                    //foreach (var infoConnect in lstDataMineFinal)
                    //{
                    //    if (infoConnect.the_value < minMine)
                    //    {
                    //        minMine = infoConnect.the_value;
                    //    }
                    //    if (infoConnect.the_value > maxMine)
                    //    {
                    //        maxMine = infoConnect.the_value;
                    //    }
                    //}
                    //maxMine = lstDataMineFinal.Max(x => x.the_value);
                    //minMine = lstDataMineFinal.Min(x => x.the_value);
                    dicMayDoDiemDo.Add(true, lstDataBombFinal);
                    dicMayDoDiemDo.Add(false, lstDataMineFinal);
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

        private List<InfoConnect> ReadDataMongo(long idKV, string code)
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
                        predicates.Add(builder.Where(item => item.code == code));
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

        private List<InfoConnect> ReadDataMongo(long idKV, bool isMachineBomb, bool model)
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
                IMongoCollection<InfoConnect> collection;
                if (model)
                {
                    collection = database.GetCollection<InfoConnect>("cecm_data_model");
                }
                else
                {
                    collection = database.GetCollection<InfoConnect>("cecm_data");
                }

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
