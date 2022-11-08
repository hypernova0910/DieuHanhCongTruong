using CoordinateSharp;
using MongoDB.Bson;
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
using System.Windows.Forms.DataVisualization.Charting;
using uPLibrary.Networking.M2Mqtt;

// autocad stuff
//using Autodesk.AutoCAD.ApplicationServices;

//using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

//using Autodesk.AutoCAD.GraphicsInterface;
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.Windows;
//using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.Runtime;
//using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.GraphicsSystem;

//using GsRenderMode = Autodesk.AutoCAD.GraphicsSystem.RenderMode;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using MapWinGIS;
using AxMapWinGIS;
using Shape = MapWinGIS.Shape;
using Point = MapWinGIS.Point;
using Image = MapWinGIS.Image;
using Newtonsoft.Json;
using Exception = System.Exception;
using VNRaPaBomMin.Models;
using Vertex = DieuHanhCongTruong.Models.Vertex;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Driver;
using uPLibrary.Networking.M2Mqtt.Messages;
using DieuHanhCongTruong.CustomControl;
using DieuHanhCongTruong.Models;
using DieuHanhCongTruong.UDP;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Command;
using DieuHanhCongTruong.Properties;

namespace VNRaPaBomMin
{
    public partial class MonitorFormNew : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        private string _currentId = "-1";

        //private List<Label> lstLabel = new List<Label>();
        private Dictionary<string, MachineControl> machine__label = new Dictionary<string, MachineControl>();

        private Dictionary<string, EventHandler> machine__event = new Dictionary<string, EventHandler>();

        private Dictionary<string, CustomTimeout> machine__timeout = new Dictionary<string, CustomTimeout>();

        public static List<CecmProgramAreaMapDTO> lstArea = new List<CecmProgramAreaMapDTO>();
        public static string currMapId = "";
        public static Dictionary<string, List<Control>> map_id__point = new Dictionary<string, List<Control>>();
        private Dictionary<string, Series> machine_series_bomb = new Dictionary<string, Series>();
        private Dictionary<string, Series> machine_series_mine = new Dictionary<string, Series>();
        private string prevProgramId = "";
        private int rowCount = 0;

        private string IotEndpoint = "localhost";
        private const int BrokerPort = 1883; // no SSL 1883 or SSL 8883;
        private const string user = "";
        private const string pw = "";
        private const int maxLine = 24;
        private MqttClient client = null;
        private MqttClient clientAddDb = null;

        private double Epsilone = 5;

        private DateTime _LastTimeDrawMapMayDoMin;
        private DateTime _LastTimeDrawMapMayDoBom;
        private double _HeSoMayDoBom = 1;
        private double _HeSoMayDoMin = 1;
        //private List<OluoiDataView> _lstOLuoi = new List<OluoiDataView>();
        private List<int> lstP = new List<int>();

        //public OluoiDataView itemSelectedOLuoi = null;

        private static double longitude = 0;
        private static double latitude = 0;
        private List<InfoConnect> lst = new List<InfoConnect>();
        private Dictionary<string, Color> machine__color = new Dictionary<string, Color>();
        private Dictionary<string, Color> machineActive__color = new Dictionary<string, Color>();

        private Dictionary<string, int> machineActive__pointLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__pointModelLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__pointModelHistoryLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__pointRealTimeLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__pointRealTimeModelLayer = new Dictionary<string, int>();

        private Dictionary<string, Point> machineActive__lastPoint = new Dictionary<string, Point>();
        private Dictionary<string, Point> machineActive__lastModelPoint = new Dictionary<string, Point>();
        private Dictionary<string, Point> machineActive__lastModelPointHistory = new Dictionary<string, Point>();
        private Dictionary<string, Point> machineActive__lastPointRealTime = new Dictionary<string, Point>();
        private Dictionary<string, Point> machineActive__lastModelPointRealTime = new Dictionary<string, Point>();

        private Dictionary<string, Point> machineActive__highlightPoint = new Dictionary<string, Point>();
        private Dictionary<string, Point> machineActive__highlightPointModel = new Dictionary<string, Point>();

        private Dictionary<string, Shape> machineActive__lastShape = new Dictionary<string, Shape>();

        private Dictionary<string, int> machineActive__lineLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__lineModelLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__lineModelHistoryLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__lineRealTimeLayer = new Dictionary<string, int>();
        private Dictionary<string, int> machineActive__lineRealTimeModelLayer = new Dictionary<string, int>();

        private Dictionary<string, DateTime> machineActive__lastTime = new Dictionary<string, DateTime>();
        private Dictionary<string, DateTime> machineActive__lastTimeModel = new Dictionary<string, DateTime>();
        private Dictionary<string, DateTime> machineActive__lastTimeModelHistory = new Dictionary<string, DateTime>();
        private Dictionary<string, DateTime> machineActive__lastRealTime = new Dictionary<string, DateTime>();
        private Dictionary<string, DateTime> machineActive__lastRealTimeModel = new Dictionary<string, DateTime>();
        private Dictionary<string, string> macID__code = new Dictionary<string, string>();

        //private Dictionary<string, Dictionary<int, DateTime>> machine__shapeIndex_time = new Dictionary<string, Dictionary<int, DateTime>>();
        //private Dictionary<string, int> machineActive__currentLineShapeIndex = new Dictionary<string, int>();
        //private Dictionary<string, int> machineActive__currentLineShapeIndexModel = new Dictionary<string, int>();

        private bool choosingLatLongPoint = false;
        private bool choosingFirstPoint = false;
        private bool choosingSecondPoint = false;  //Đã chọn điểm đầu tiên để đo khoảng cách chưa để phân biệt với khi chọn điểm 2

        private System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer timerAddDb = new System.Timers.Timer();
        private bool userClickDisconnect = false;

        //Số bản ghi mỗi lần load lịch sử
        //private const double DELTA = 3.2;

        //private string[] colors = {
        //    "#ff4500",
        //    "#00008b",
        //    "#b03060",
        //    "#228b22",
        //    "#ffd700",
        //    "#00ff00",
        //    "#00ffff",
        //    "#ff00ff",
        //    "#6495ed",
        //    "#ffdab9",
        //};

        private string[] colors = {
            "#A2231D",
            "#2271B3",
            "#D0D0D0",
            "#2C5545",
            "#A03472",
            "#A18594",
            "#898176",
            "#F3DA0B",
            "#955F20",
            "#A98307",
            "#49678D",
            "#FFA420",
            "#F5D033",
            "#89AC76",
            "#212121",
            "#4C9141",
            "#8A6642",
            "#6C7059",
        };

        private Color[] activeMachineColor;

        //private Color[] activeMachineColor =
        //{
        //    ColorTranslator.FromHtml("#ff4500"),
        //    ColorTranslator.FromHtml("#00008b"),
        //    ColorTranslator.FromHtml("#b03060"),
        //    ColorTranslator.FromHtml("#228b22"),
        //    ColorTranslator.FromHtml("#ffd700"),
        //    ColorTranslator.FromHtml("#00ff00"),
        //    ColorTranslator.FromHtml("#00ffff"),
        //    ColorTranslator.FromHtml("#ff00ff"),
        //    ColorTranslator.FromHtml("#6495ed"),
        //    ColorTranslator.FromHtml("#ffdab9"),
        //};

        private List<int> machinePointLayers = new List<int>();
        private List<int> machinePointModelLayers = new List<int>();
        private List<int> machinePointModelHistoryLayers = new List<int>();
        private List<int> machinePointRealTimeLayers = new List<int>();
        private List<int> machinePointRealTimeModelLayers = new List<int>();

        private List<int> machineLineLayers = new List<int>();
        private List<int> machineLineModelLayers = new List<int>();
        private List<int> machineLineModelHistoryLayers = new List<int>();
        private List<int> machineLineRealTimeLayers = new List<int>();
        private List<int> machineLineRealTimeModelLayers = new List<int>();

        private int polygonLayer = -1;
        private int oluoiLayer = -1;
        private int suspectPointLayer = -1;
        private int suspectPointLayerMine = -1;
        private int flagLayer = -1;
        private int flagRealTimeLayer = -1;
        private int imageLayer = -1;
        private int deepLayer = -1;
        private int pointLayer = -1;
        private int greenFlagLayer = -1;
        private int machineLayer = -1;
        private int bgLayer = -1;
        private int lineLayer = -1;
        private int labelLayer = -1;
        private int distanceLayer = -1;
        private int markerDistanceLayer = -1;
        private int highlightLayer = -1;
        private int highlightCurrentPointLayer = -1;
        private int highlightCurrentPointModelLayer = -1;
        private int tooltipLayer = -1;

        private UdpUser socketclient = UdpUser.ConnectTo("127.0.0.1", 32123);
        private UdpUser socketNanDiem = UdpUser.ConnectTo("127.0.0.1", 32124);
        private DateTime start;

        private const int PTCOUNT = 100;
        private const int PTTIMEOUT = 300;
        //private const double min_distance = 0.5;
        private const int MIN_TIME_NEW_LINE = 90;
        private const double MIN_DISTANCE_NEW_LINE = 7;
        private const int MACHINE_ACTIVE_TIMEOUT = 30;
        private const string ICON_FOLDER = "machine icons extended";

        private int ptCount = 0;
        private CustomTimeout ptTimeout;
        //WebSocket ws = new WebSocket("ws://127.0.0.1:9000");

        private List<Vertex> lstCenterGlobal = new List<Vertex>();
        private List<Vertex> lstInput = new List<Vertex>();
        //List<Vertex> lst_vertex = new List<Vertex>();

        private Boundary oluoi_boundary = new Boundary();
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;
        private List<long> lstOluoiPointLoaded = new List<long>();
        private List<long> lstOluoiPointModelLoaded = new List<long>();

        private Thread threadLoadHistory = null;

        public MonitorFormNew()
        {
            InitializeComponent();
            //axMap1.ZoomBarVerbosity = tkZoomBarVerbosity.zbvFull;
            axMap1.ZoomBarMaxZoom = 25;
            axMap1.ZoomBehavior = tkZoomBehavior.zbDefault;
            //GeoProjection proj = new GeoProjection();
            axMap1.Projection = tkMapProjection.PROJECTION_NONE;
            //axMap1.GeoProjection = proj;
            axMap1.SendMouseMove = true;
            axMap1.SendMouseDown = true;
            axMap1.SendMouseUp = true;

            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;

            //LoadMap();
            //readMarkers();
            //phanTichTest();
            ptCount = 0;

            //_currentId = idDuAn.ToString();

            activeMachineColor = colors.Select(color => ColorTranslator.FromHtml(color)).ToArray();
        }

        public void initMap()
        {
            try
            {
                //InitWhiteLayer();

                InitImageLayer();
                //initOluoiLayer();
                initPolygonLayer();
                //drawPolygon(
                //     new double[4] { 106.840871, 106.840207, 106.830766, 106.833126 },
                //     new double[4] { 17.268326, 17.264206, 17.264576, 17.272517 }
                // );
                //InitPointLayer();
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

                //foreach (int layer in machinePointLayers)
                //{
                //    axMap1.set_LayerVisible(layer, false);
                //    //axMap1.SetDrawingLayerVisible(layer, false);
                //}
                //foreach (int layer in machinePointModelLayers)
                //{
                //    axMap1.set_LayerVisible(layer, false);
                //    //axMap1.SetDrawingLayerVisible(layer, false);
                //}
                //foreach (int layer in machineLineLayers)
                //{
                //    axMap1.set_LayerVisible(layer, false);
                //}
                //foreach (int layer in machineLineModelLayers)
                //{
                //    axMap1.set_LayerVisible(layer, false);
                //}
                //axMap1.set_LayerVisible(highlightCurrentPointLayer, false);
                hideShowModel(false);
                hideShowUnmodel(false);
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }
        }

        //private void readMarkers(double minLat = 0, double minLong = 0, double maxLat = double.PositiveInfinity, double maxLong = double.PositiveInfinity)
        //{
        //    lstInput.Clear();
        //    using (StreamReader r = new StreamReader(@"C:\temp\dataRPBM.json"))
        //    {
        //        string json2 = r.ReadToEnd();
        //        DataRPBM data = JsonConvert.DeserializeObject<DataRPBM>(json2);
        //        foreach (Models.MachineBomCodePoint machineBomPointDatas in data.MachineBomPointDatas)
        //        {
        //            foreach (var item in machineBomPointDatas.DatabasePointInfo)
        //            {
        //                if (item.isMachineBom == "True" && double.Parse(item.lat_value) > minLat && double.Parse(item.lat_value) < maxLat && double.Parse(item.long_value) > minLong && double.Parse(item.long_value) < maxLong)
        //                {
        //                    lstInput.Add(new Vertex(double.Parse(item.lat_value), double.Parse(item.long_value), double.Parse(item.the_value)));
        //                    //Console.WriteLine("true");
        //                }
        //            }
        //        }
        //    }
        //}

        public void LoadMap()
        {
            try
            {
                //LoadMachineBomCodePoint();
                //LoadMatCat();
                //LoadDoSau();
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }
        }

        private void MonitorForm2_Load(object sender, EventArgs e)
        {
            initMap();
            GetAllDuAn();

            //LoadMongoData();

            _LastTimeDrawMapMayDoMin = DateTime.Now;
            _LastTimeDrawMapMayDoBom = DateTime.Now;

            MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
            AppUtils.LoadRecentInput(frm.tbHeSoMayDoBom, AppUtils.DefaultNanoTesla.ToString());
            AppUtils.LoadRecentInput(frm.tbHeSoMayDoMin, AppUtils.DefaultNanoTeslaMin.ToString());
            AppUtils.LoadRecentInput(tbIP, "localhost");

            _HeSoMayDoBom = double.Parse(frm.tbHeSoMayDoBom.Text);
            _HeSoMayDoMin = double.Parse(frm.tbHeSoMayDoMin.Text);

            //foreach (var item in _lstOLuoi)
            //    cbOLuoi.Items.Add(item);
            //if (_lstOLuoi.Count != 0)
            //    cbOLuoi.SelectedIndex = 0;

            //convert to pfx using openssl
            //you'll need to add these two files to the project and copy them to the output
            //var clientCert = new X509Certificate2("YOURPFXFILE.pfx", "YOURPFXFILEPASSWORD");
            //this is the AWS caroot.pem file that you get as part of the download
            //var caCert = X509Certificate.CreateFromSignedFile("root.pem"); // this doesn't have to be a new X509 type...
            //var client = new MqttClient(IotEndpoint, BrokerPort, true, caCert, clientCert, MqttSslProtocols.None);

            IotEndpoint = tbIP.Text;

            client = new MqttClient(IotEndpoint, BrokerPort, false, null, null, MqttSslProtocols.None);
            clientAddDb = new MqttClient(IotEndpoint, BrokerPort, false, null, null, MqttSslProtocols.None);

            //event handler for inbound messages

            client.MqttMsgPublishReceived += ClientMqttMsgPublishReceived;
            clientAddDb.MqttMsgPublishReceived += EventAddDatabase;

            if (AppUtils.PingHost(tbIP.Text))
                ButtonConnectClick(false);

            timer.Elapsed += new System.Timers.ElapsedEventHandler((sender2, e2) =>
            {
                if (!client.IsConnected && !userClickDisconnect && btConnect.Enabled)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            ButtonConnectClick(false);
                        }));
                    }
                    else
                    {
                        ButtonConnectClick(false);
                    }
                }
            });
            timer.AutoReset = true;
            timer.Interval = 30000;
            timer.Start();

            //OluoiDataView itemSelected = cbOLuoi.SelectedItem as OluoiDataView;
            //if (itemSelected == null)
            //    return;

            //var dataFolder = AppUtils.GetAppDataPath();
            //dataFolder = Path.Combine(dataFolder, "LastMQTTIP.txt");
            //if (File.Exists(dataFolder) == false)
            //    tbIP.Text = "localhost";
            //else
            //{
            //    using (StreamReader r = new StreamReader(dataFolder))
            //    {
            //        string IP = r.ReadToEnd();
            //        tbIP.Text = IP;
            //    }
            //}
            ClientAddDbConnect();
            timerAddDb.Elapsed += new System.Timers.ElapsedEventHandler((sender2, e2) =>
            {
                ClientAddDbConnect();
            });
            timerAddDb.AutoReset = true;
            timerAddDb.Interval = 30000;
            timerAddDb.Start();
            
        }

        private void ClientAddDbConnect()
        {
            string id = Guid.NewGuid().ToString();
            //MessageBox.Show("Connect");
            if (!clientAddDb.IsConnected)
            {
                Thread threadConnect = new Thread(() =>
                {
                    try
                    {
                        clientAddDb.Connect(id);
                    }
                    catch (System.Exception ex)
                    {
                        return;
                    }
                    clientAddDb.Subscribe(new[] { "bridge/message" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                });
                threadConnect.IsBackground = true;
                threadConnect.Start();
            }
        }

        private void LoadMongoData()
        {
            var database = frmLoggin.mgCon.GetDatabase("db_cecm");
            if (database != null)
            {
                var collection = database.GetCollection<InfoConnect>("cecm_data");
                //var builder = Builders<InfoConnect>.Filter;
                //var filter = builder.And();
                //var filter1 = Builders<InfoConnect>.Filter
                //.Gt(x => double.Parse(x.lat_value), boundary.minLat);
                //var filter2 = Builders<InfoConnect>.Filter
                //.Lt(x => double.Parse(x.lat_value), boundary.maxLat);
                //var filter3 = Builders<InfoConnect>.Filter
                //.Gt(x => double.Parse(x.long_value), boundary.minLong);
                //var filter4 = Builders<InfoConnect>.Filter
                //.Lt(x => double.Parse(x.long_value), boundary.maxLong);
                //var filter5 = Builders<InfoConnect>.Filter
                //.Eq(x => x.isMachineBom, "True");
                //var filter = filter1 & filter2 & filter3 & filter4 & filter5;
                //var filter = Builders<InfoConnect>.Filter.Eq(x => double.Parse(x.lat_value), boundary.minLat);
                //collection.Find()
                var docs = collection.Find(doc => true).ToList();
                //MessageBox.Show("docs.Count: " + docs.Count);
                //int count = 0;
                foreach (InfoConnect doc in docs)
                {
                    double longt = 0, latt = 0;
                    AppUtils.ToLatLon(doc.lat_value, doc.long_value, ref latt, ref longt, "48N");
                    addMachinePoint(longt, latt, doc.code, doc.time_action.ToLocalTime());
                }
                //collection.InsertOne(document);
            }
        }

        private void MonitorForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
            //if (client != null)
            //{
            //    if (client.IsConnected)
            //    {
            //        client.Disconnect();
            //    }
            //}
            if (ptTimeout != null)
            {
                ptTimeout.Stop();
            }
            if (threadLoadHistory != null)
            {
                if (threadLoadHistory.IsAlive)
                {
                    threadLoadHistory.Abort();
                }
            }
            timer.Stop();
            timerAddDb.Stop();
        }

        private void GetAllDuAn()
        {
            try
            {
                //SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT id,name FROM cecm_programData"), frmLoggin.sqlCon);
                SqlCommandBuilder sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                
                cbTenDuAn.DisplayMember = "name";
                cbTenDuAn.ValueMember = "id";
                cbTenDuAn.DataSource = datatable;

                //SqlCommandBuilder sqlCommand2 = null;
                //SqlDataAdapter sqlAdapter2 = null;
                //System.Data.DataTable datatable2 = new System.Data.DataTable();
                //sqlAdapter2 = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name FROM cecm_program_area_map where cecm_program_id = " + cbTenDuAn.SelectedValue), frmLoggin.sqlCon);
                //sqlCommand2 = new SqlCommandBuilder(sqlAdapter2);
                //sqlAdapter2.Fill(datatable2);
                //cbKhuVuc.DataSource = datatable2;
                //cbKhuVuc.DisplayMember = "name";
                //cbKhuVuc.ValueMember = "id";

                //SqlCommandBuilder sqlCommand3 = null;
                //SqlDataAdapter sqlAdapter3 = null;
                //System.Data.DataTable datatable3 = new System.Data.DataTable();
                //sqlAdapter3 = new SqlDataAdapter(string.Format("SELECT gid, o_id FROM OLuoi where cecm_program_areamap_id = " + cbKhuVuc.SelectedValue), frmLoggin.sqlCon);
                //sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                //sqlAdapter3.Fill(datatable3);
                //cb50x50.DataSource = datatable3;
                //cb50x50.DisplayMember = "o_id";
                //cb50x50.ValueMember = "gid";
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private string filterMachineCode = "";

        private void LoadDanhSachMayDo()
        {
            _currentId = ((long)cbTenDuAn.SelectedValue).ToString();
            machine__label.Clear();
            //pnlMachineMine.Controls.Clear();
            pnlMachineBomb.Controls.Clear();

            // Load Danh Sach May do plan
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            //sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, code,staff_id, description, mac_id, IsMachineBoom FROM Cecm_ProgramMachineBomb WHERE cecm_program_id = {0}", _currentId), frmLoggin.sqlCon);
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT b.id, b.code,staff_id, s.nameId, b.description, b.mac_id, b.IsMachineBoom FROM Cecm_ProgramMachineBomb b left join Cecm_ProgramStaff s on (b.staff_id = s.id) WHERE cecm_program_id = {0} order by IsMachineBoom", _currentId), frmLoggin.sqlCon);
            //MessageBox.Show(_currentId);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);

            List<MachineBombLabel> lstMayBom = new List<MachineBombLabel>();
            List<MachineBombLabel> lstMayMin = new List<MachineBombLabel>();
            foreach (DataRow dr in datatable.Rows)
            {
                string code = dr["code"].ToString();
                string codeMachine = dr["mac_id"].ToString();
                string idStaff = dr["staff_id"].ToString();
                string staff_name = dr["nameId"].ToString();

                MachineControl lbl;
                if (dr["IsMachineBoom"].ToString() == "1")
                {
                    //MachineBombLabel lbl = new MachineBombLabel();
                    ////lbl.Text = string.Format("{0} - {1}", codeMachine, staff_name);
                    //lbl.machineId = codeMachine;
                    //lbl.staff_name = staff_name;
                    //lbl._Code = codeMachine;
                    //lbl.Image = Properties.Resources.bom;
                    //lstMayBom.Add(lbl);
                    lbl = new MachineControl(
                        MachineControl.BOM,
                        code + " - " + codeMachine,
                        staff_name
                    );
                    
                }
                else
                {
                    //MachineBombLabel lbl = new MachineBombLabel();
                    ////lbl.Text = string.Format("{0} - {1}", codeMachine, staff_name);
                    //lbl.machineId = codeMachine;
                    //lbl.staff_name = staff_name;
                    //lbl._Code = codeMachine;
                    //lbl.Image = Properties.Resources.min;
                    //lstMayMin.Add(lbl);
                    lbl = new MachineControl(
                        MachineControl.MIN,
                        code + " - " + codeMachine,
                        staff_name
                    );
                }
                if (machineActive__color.ContainsKey(codeMachine))
                {
                    lbl.setForeColor(machineActive__color[codeMachine]);
                }
                if (!macID__code.ContainsKey(codeMachine))
                {
                    macID__code.Add(codeMachine, code);
                }
                filterMachineCode = "";
                lbl.Click += (sender, e) =>
                {
                    if (filterMachineCode == codeMachine)
                    {
                        //lbl.BackColor = SystemColors.Control;
                        filterMachineCode = "";
                    }
                    else
                    {
                        //lbl.BackColor = Color.AliceBlue;
                        filterMachineCode = codeMachine;
                    }
                };
                pnlMachineBomb.Controls.Add(lbl);
                if (!machine__label.ContainsKey(codeMachine))
                    machine__label.Add(codeMachine, lbl);
            }

            //var lstColorMayBom = AppUtils.GetColorMayBomMinWindows(lstMayBom.Count, true);
            //var lstColorMayMin = AppUtils.GetColorMayBomMinWindows(lstMayMin.Count, false);

            //int index = 0;
            //foreach (var author in lstMayBom.OrderBy(x => x._Code))
            //{
            //    author._ColorRun = lstColorMayBom[index];
            //    index++;
            //}
            //index = 0;
            //foreach (var author in lstMayMin.OrderBy(x => x._Code))
            //{
            //    author._ColorRun = lstColorMayMin[index];
            //    index++;
            //}
        }

        private double DepthSchedule(double firstUTMNorthing, double firstUTMEasting, double M1, double lastUTMNorthing, double lastUTMEasting, double M2)
        {
            double dDepth = 0.0;
            double dx = firstUTMNorthing - lastUTMNorthing;
            double dy = firstUTMEasting - lastUTMEasting;
            double distace = Math.Sqrt(dx * dx + dy * dy);
            if (distace <= 3)
            {
                double dValue = M1 / M2;
                if (dValue >= 10)
                    return 0.5;
                else if (dValue < 10 && dValue >= 4.1)
                    return 1.0;
                else if (dValue < 4.1 && dValue >= 2.8)
                    return 1.5;
                else if (dValue < 2.8 && dValue >= 2.3)
                    return 2.0;
                else if (dValue < 2.3 && dValue >= 2.0)
                    return 2.5;
                else if (dValue < 2.0 && dValue >= 1.8)
                    return 3.0;
                else if (dValue < 1.8 && dValue >= 1.65)
                    return 3.5;
                else if (dValue < 1.65 && dValue >= 1.55)
                    return 4.0;
                else if (dValue < 1.55 && dValue >= 1.5)
                    return 4.5;
                else if (dValue < 1.5 && dValue >= 1.44)
                    return 5.0;
                else if (dValue < 1.44 && dValue >= 1.4)
                    return 5.5;
                else
                    return 6.0;
            }
            return dDepth;
        }

        private void cbTenDuAn_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTenDuAn.SelectedValue is long)
            {
                LoadDanhSachMayDo();
                try
                {
                    SqlCommandBuilder sqlCommand = null;
                    SqlDataAdapter sqlAdapter = null;
                    System.Data.DataTable datatable = new System.Data.DataTable();
                    sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name FROM cecm_program_area_map where cecm_program_id = " + cbTenDuAn.SelectedValue), frmLoggin.sqlCon);
                    sqlCommand = new SqlCommandBuilder(sqlAdapter);
                    sqlAdapter.Fill(datatable);
                    cbKhuVuc.DataSource = datatable;
                    cbKhuVuc.DisplayMember = "name";
                    cbKhuVuc.ValueMember = "id";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private List<DiemDoData> lstDiemDo1 = new List<DiemDoData>();

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (!client.IsConnected)
            {
                IotEndpoint = tbIP.Text;

                client = new MqttClient(IotEndpoint, BrokerPort, false, null, null, MqttSslProtocols.None);
                clientAddDb = new MqttClient(IotEndpoint, BrokerPort, false, null, null, MqttSslProtocols.None);

                //event handler for inbound messages
                client.MqttMsgPublishReceived += ClientMqttMsgPublishReceived;
                clientAddDb.MqttMsgPublishReceived += EventAddDatabase;
            }

            ButtonConnectClick(true);
            ClientAddDbConnect();
        }

        private void ButtonConnectClick(bool isClicked)
        {
            try
            {


                _currentId = ((long)cbTenDuAn.SelectedValue).ToString();
                _LastTimeDrawMapMayDoMin = DateTime.Now;
                _LastTimeDrawMapMayDoBom = DateTime.Now;

                if (!client.IsConnected)
                {
                    //Thread threadConnect = new Thread(() =>
                    //{
                    //    if (this.InvokeRequired)
                    //    {
                    //        this.Invoke(new MethodInvoker(delegate
                    //        {
                    //            Subscribe();


                    //        }));
                    //    }
                    //    else
                    //    {
                    //        Subscribe();
                    //    }
                    //});
                    //threadConnect.IsBackground = true;
                    //threadConnect.Start();
                    Subscribe();

                    if (_currentId != prevProgramId)
                    {
                        lstArea = UtilsDatabase.GetAllCecmProgramAreaMapByProgramId(UtilsDatabase._ExtraInfoConnettion, long.Parse(_currentId));
                        CecmProgramAreaMapDTO init = new CecmProgramAreaMapDTO();
                        init.cecmProgramAreaMapId = "-" + _currentId;
                        init.comboboxName = "Tất cả";
                        init.left_long = lstArea.Count > 0 ? lstArea[0].left_long : "0";
                        init.right_long = "0";
                        init.top_lat = "0";
                        init.bottom_lat = lstArea.Count > 0 ? lstArea[0].bottom_lat : "0";
                        init.polygonGeom = "";
                        //Tìm các giá trị góc lớn nhất trong tất cả các vùng dự án
                        foreach (CecmProgramAreaMapDTO dto in lstArea)
                        {
                            init.polygonGeomST += dto.polygonGeomST != null ? dto.polygonGeomST : "";
                            if (dto.left_long != null)
                            {
                                if (float.Parse(dto.left_long) < float.Parse(init.left_long))
                                {
                                    init.left_long = dto.left_long;
                                }
                            }
                            if (dto.right_long != null)
                            {
                                if (float.Parse(dto.right_long) > float.Parse(init.right_long))
                                {
                                    init.right_long = dto.right_long;
                                }
                            }
                            if (dto.top_lat != null)
                            {
                                if (float.Parse(dto.top_lat) > float.Parse(init.top_lat))
                                {
                                    init.top_lat = dto.top_lat;
                                }
                            }
                            if (dto.bottom_lat != null)
                            {
                                if (float.Parse(dto.bottom_lat) < float.Parse(init.bottom_lat))
                                {
                                    init.bottom_lat = dto.bottom_lat;
                                }
                            }
                        }
                        List<CecmProgramAreaMapDTO> lst = new List<CecmProgramAreaMapDTO>(lstArea);
                        lst.Insert(0, init);
                    }
                    prevProgramId = _currentId;
                }
                else
                {
                    client.Disconnect();
                    ptCount = 0;
                    //client = null;
                    btConnect.Text = "Kết nối";
                    btConnect.BackColor = Color.ForestGreen;
                    cbTenDuAn.Enabled = true;
                    tbIP.Enabled = true;
                    if (isClicked)
                    {
                        userClickDisconnect = true;
                    }
                    //reset();
                }
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private void reset()
        {
            foreach (CustomTimeout timeout in machine__timeout.Values)
            {
                timeout.Stop();
            }
            //foreach (MachineControl lbl in machine__label.Values)
            //{
            //    lbl.setForeColor(SystemColors.ControlText);
            //}

            //reset
            chart_bomb.Series.Clear();
            chart_mine.Series.Clear();
            machine_series_bomb.Clear();
            machine_series_mine.Clear();
        }

        public void Subscribe()
        {
            btConnect.Text = "Đang kết nối";
            //btConnect.BackColor = Color.FromArgb(23, 162, 184);
            btConnect.BackColor = Color.Yellow;
            string id = Guid.NewGuid().ToString();
            userClickDisconnect = false;
            btConnect.Enabled = false;

            //client id here is totally arbitary, but I'm pretty sure you can't have more than one client named the same.
            //client.Connect("listener", user, pw);
            Thread threadConnect = new Thread(() =>
            {
                try
                {
                    client.Connect(id);
                }
                catch (System.Exception ex)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            btConnect.BackColor = Color.ForestGreen;
                            btConnect.Text = "Kết nối";
                            btConnect.Enabled = true;
                            cbTenDuAn.Enabled = true;
                            tbIP.Enabled = true;
                        }));
                    }
                    else
                    {
                        btConnect.BackColor = Color.ForestGreen;
                        btConnect.Text = "Kết nối";
                        btConnect.Enabled = true;
                        cbTenDuAn.Enabled = true;
                        tbIP.Enabled = true;
                    }
                    return;
                }
                // '#' is the wildcard to subscribe to anything under the 'root' topic
                // the QOS level here - I only partially understand why it has to be this level - it didn't seem to work at anything else.
                client.Subscribe(new[] { "bridge/message" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                AppUtils.SaveRecentInput(tbIP);
                //return true;
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        btConnect.Text = "Ngắt kết nối";
                        btConnect.BackColor = Color.Red;
                        cbTenDuAn.Enabled = false;
                        tbIP.Enabled = false;
                        btConnect.Enabled = true;
                    }));
                }
                else
                {
                    btConnect.Text = "Ngắt kết nối";
                    btConnect.BackColor = Color.Red;
                    cbTenDuAn.Enabled = false;
                    tbIP.Enabled = false;
                    btConnect.Enabled = true;
                }
                
            });
            threadConnect.IsBackground = true;
            threadConnect.Start();

        }

        private void PlaySound(string pathFile)
        {
            try
            {
                if (System.IO.File.Exists(pathFile) == false)
                    return;

                using (var soundPlayer = new System.Media.SoundPlayer(pathFile))
                {
                    soundPlayer.Play(); // can also use soundPlayer.PlaySync()
                }
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
            }
        }

        private AxMapWinGIS._DMapEvents_MouseUpEventHandler mouseUpHandler = null;

        private void EventAddDatabase(object sender, MqttMsgPublishEventArgs e)
        {
            if (!client.IsConnected)
            {
                return;
            }
            char[] strMess = Encoding.UTF8.GetChars(e.Message);
            string strBuff = new string(strMess);
            MQTTObject obj = HandleMQTTMessage(strBuff);
            if (!machine__label.ContainsKey(obj.codeMachine))
                return;
            bool isMachineBomb = obj.type == "Bom";
            foreach (GPS gps in obj.lstGPS)
            {
                Coordinate cWGS84 = new Coordinate(gps.dLat, gps.dLong);

                //AddDatabase(_currentId, codeMachine, cWGS84.UTM.Northing, cWGS84.UTM.Easting, dValData, updateTimeData, timeActionData, bitSent, true, dilution, satelliteCount);
                AddDatabase(_currentId, obj.codeMachine, cWGS84.UTM.Northing, cWGS84.UTM.Easting, obj.dValData, gps.updateTimeData, gps.timeActionData, obj.bitSent, isMachineBomb, obj.dilution, obj.satelliteCount);
            }
            
        }

        private void ClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {


            MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
            AppUtils.LoadRecentInput(frm.tbKhoangThoiGian, "10");
            AppUtils.LoadRecentInput(frm.tbPhanTichOnline, "1000");
            double heSoThoiGiand = double.Parse(frm.tbKhoangThoiGian.Text);
            double heSoPhanTichOnline = double.Parse(frm.tbPhanTichOnline.Text);

            string pathSound = AppUtils.GetAppDataPath();
            pathSound = System.IO.Path.Combine(pathSound, "SoundRing.wav");

            if (lstP.Count >= heSoPhanTichOnline)
            {
                UpdateModelData();
                lstP.Clear();
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            try
            {
                //Autodesk.AutoCAD.EditorInput.Editor ed = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;
                //using (ed.Document.LockDocument())
                //{
                ////Console.WriteLine("We received a message...");
                ////Console.WriteLine(Encoding.UTF8.GetChars(e.Message));
                char[] strMess = Encoding.UTF8.GetChars(e.Message);
                if ((strMess.Length > 5) &&
                    (strMess[0] == '$') &&
                    (strMess[1] == 'M') &&
                    (strMess[2] == 'D') &&
                    (strMess[3] == 'N') &&
                    (strMess[4] == ','))
                {
                    string strBuff = new string(strMess);
                    string[] elements = strBuff.Split(',');
                    if (elements.Length > 10)
                    {
                        short numValue = short.Parse(elements[8]);
                        short numGPS = short.Parse(elements[9]);

                        var codeMachine = elements[1];
                        double dilution = double.Parse(elements[11]);
                        int satelliteCount = int.Parse(elements[12]);
                        //lọc máy dò
                        if (!machine__label.ContainsKey(codeMachine))
                            return;

                        if (numGPS <= 0)
                            machine__label[codeMachine].BackColor = Color.Yellow;
                        else
                            machine__label[codeMachine].BackColor = Color.White;

                        /////////////////////////// first version not process numGPS==0
                        if (numGPS > 0 || numValue > 0)
                        {
                            double northingData = 0, eastingData = 0, dValData = 0, dLat = 0, dLong = 0;
                            DateTime updateTimeData = new DateTime();
                            DateTime timeActionData = new DateTime();
                            int bitSent = 0;

                            int.TryParse(elements[10], out bitSent);
                            string dateTimeVal = elements[2] + ":" + elements[3] + ":" + elements[4] + " " + elements[5] + "/" + elements[6] + "/" + elements[7];

                            for (short i = 0; i < numValue; i++)
                                dValData += double.Parse(elements[13 + i]);

                            if (numValue > 0)
                                dValData /= numValue;

                            short offset = numValue;
                            offset += 13;
                            bool isFirstFlag = true;
                            for (short k = 0; k < numGPS; k++)
                            {

                                try
                                {
                                    string timeGPS = elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++];

                                    string latVal = elements[offset++];
                                    string longVal = elements[offset++];

                                    if (ValidateValue(dateTimeVal, timeGPS, latVal, longVal, dValData.ToString()) == false)
                                        continue;

                                    double.TryParse(latVal, out dLat);
                                    double.TryParse(longVal, out dLong);

                                    CultureInfo enUS = new CultureInfo("en-US");
                                    DateTime.TryParseExact(dateTimeVal, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out updateTimeData);
                                    updateTimeData = updateTimeData.ToLocalTime();
                                    DateTime.TryParseExact(timeGPS, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out timeActionData);
                                    timeActionData = timeActionData.ToLocalTime();

                                    Coordinate cWGS84 = new Coordinate(dLat, dLong);

                                    //AddDatabase(_currentId, codeMachine, cWGS84.UTM.Northing, cWGS84.UTM.Easting, dValData, updateTimeData, timeActionData, bitSent, true, dilution, satelliteCount);
                                    if (this.InvokeRequired)
                                    {
                                        this.Invoke(new MethodInvoker(delegate
                                        {
                                            
                                            bool isCamCo = CheckCamCo(bitSent, out bool isButton1Press);
                                            AddDataToTable(codeMachine, updateTimeData, dValData, dLong, dLat, timeActionData, bitSent, satelliteCount, dilution, isButton1Press, "Bom");
                                            machine__label[codeMachine].setGPS(dLat, dLong);
                                            EventHandler handler = new EventHandler((sender2, e2) =>
                                            {
                                                //axMap1.SetLatitudeLongitude(dLat, dLong);
                                                //axMap1.Latitude = (float)dLat;
                                                //axMap1.Longitude = (float)dLong;
                                                Extents extents = new Extents();
                                                extents.SetBounds(dLong - 0.00001, dLat - 0.00001, 0, dLong + 0.00001, dLat + 0.00001, 0);
                                                axMap1.Extents = extents;
                                                addMachineNote(dLong, dLat, codeMachine);
                                            });
                                            if (machine__event.ContainsKey(codeMachine))
                                            {
                                                machine__label[codeMachine].Click -= machine__event[codeMachine];
                                                machine__event[codeMachine] = handler;
                                            }
                                            else
                                            {
                                                machine__event.Add(codeMachine, handler);
                                            }
                                            machine__label[codeMachine].Click += machine__event[codeMachine];
                                            if (isCamCo)
                                            {
                                                if (isFirstFlag)
                                                {
                                                    isFirstFlag = false;
                                                    PlaySound(pathSound);
                                                    Vertex item = new Vertex(cWGS84.UTM.Northing, cWGS84.UTM.Easting, dValData);
                                                    item.BitSent = bitSent;
                                                    item.Type = Vertex.CAMCO;
                                                    item.X = cWGS84.UTM.Easting;
                                                    item.Y = cWGS84.UTM.Northing;

                                                    Labels labels = new Labels();
                                                    labels.FontSize = 12;
                                                    labels.Visible = true;
                                                    labels.Alignment = tkLabelAlignment.laBottomRight;
                                                    labels.OffsetX = 20;
                                                    labels.OffsetY = 20;
                                                    labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
                                                    labels.AvoidCollisions = true;
                                                    string labelText = "";

                                                    //labels_info.AvoidCollisions = true;

                                                    //axMap1.set_DrawingLabels(handleLayer_info, labels_info);
                                                    //axMap1.Redraw();
                                                    KQPhanTichControl phanTichControl = new KQPhanTichControl(
                                                        KQPhanTichControl.CAMCO,
                                                        machine__label[codeMachine].getLblMachineCode().Text,
                                                        machine__label[codeMachine].getLblStaff().Text,
                                                        Math.Round(dLat, 6), Math.Round(dLong, 6), Math.Round(dValData * _HeSoMayDoBom, 6),
                                                        0, false, false
                                                    );
                                                    //phanTichControl.label_info = labels_info;

                                                    //dgvData.Rows.Add(++count, longt, latt, item.Z);
                                                    //bool existNearby = false;
                                                    //foreach (Vertex vertex in lstCenterGlobal)
                                                    //{
                                                    //    if (Math.Sqrt(Math.Pow(item.X - vertex.X, 2) + Math.Pow(item.Y - vertex.Y, 2)) < min_distance)
                                                    //    {
                                                    //        existNearby = true;
                                                    //        if (vertex.Type == Vertex.BOM)
                                                    //        {
                                                    //            if (vertex.phanTichControl.InvokeRequired)
                                                    //            {
                                                    //                vertex.phanTichControl.Invoke(new MethodInvoker(delegate
                                                    //                {
                                                    //                    if(vertex.TypeBombMine == Vertex.TYPE_BOMB)
                                                    //                    {
                                                    //                        vertex.phanTichControl.setExistBomb(true);
                                                    //                    }
                                                    //                    else if(vertex.TypeBombMine == Vertex.TYPE_MINE)
                                                    //                    {
                                                    //                        vertex.phanTichControl.setExistMine(true);
                                                    //                    }
                                                    //                }));
                                                    //            }
                                                    //            else
                                                    //            {
                                                    //                if (vertex.TypeBombMine == Vertex.TYPE_BOMB)
                                                    //                {
                                                    //                    vertex.phanTichControl.setExistBomb(true);
                                                    //                }
                                                    //                else if (vertex.TypeBombMine == Vertex.TYPE_MINE)
                                                    //                {
                                                    //                    vertex.phanTichControl.setExistMine(true);
                                                    //                }
                                                    //            }
                                                    //        }
                                                    //        else if (vertex.Type == Vertex.BOM && item.Type == Vertex.CAMCO)
                                                    //        {
                                                    //            vertex.Type = Vertex.CAMCO;
                                                    //            if (vertex.phanTichControl.InvokeRequired)
                                                    //            {
                                                    //                vertex.phanTichControl.Invoke(new MethodInvoker(delegate
                                                    //                {
                                                    //                    vertex.phanTichControl.setType(Vertex.CAMCO);
                                                    //                }));
                                                    //            }
                                                    //            else
                                                    //            {
                                                    //                vertex.phanTichControl.setType(Vertex.CAMCO);
                                                    //            }
                                                    //        }
                                                    //    }
                                                    //}
                                                    //if (!existNearby)
                                                    //{
                                                    //    pnlKQPTContainer.Controls.Add(phanTichControl);
                                                    //}

                                                    //int handleLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                                                    //Labels labels = axMap1.get_DrawingLabels(handleLayer);
                                                    //labels.FontSize = 12;
                                                    if (isButton1Press)
                                                    {
                                                        //row.DefaultCellStyle.BackColor = Color.Red;

                                                        DiemDoData diemDoData = new DiemDoData();
                                                        diemDoData.XUtm = cWGS84.UTM.Northing;
                                                        diemDoData.YUtm = cWGS84.UTM.Easting;
                                                        diemDoData.Z = dValData;
                                                        diemDoData.CodeMachine = codeMachine;
                                                        diemDoData.DepthBoom = double.NaN;
                                                        lstDiemDo1.Add(diemDoData);
                                                        //labels.AddLabel("0", dLong, dLat);
                                                        //labels.OffsetX = 20;
                                                        //labels.OffsetY = 20;
                                                        //axMap1.set_DrawingLabels(handleLayer, labels);
                                                        addFlagRealTime(dLong, dLat, 0);
                                                        labelText = string.Format(
                                                            "Mã máy: {0}\n" +
                                                            "Kinh độ: {1}\n" +
                                                            "Vĩ độ: {2}\n" +
                                                            "Cường độ từ trường: {3}\n" +
                                                            "Độ sâu: {4}m",
                                                            codeMachine, Math.Round(dLong, 6), Math.Round(dLat, 6), Math.Round(dValData * _HeSoMayDoBom), 0
                                                        );
                                                        labels.AddLabel(labelText, dLong, dLat);
                                                        //axMap1.set_DrawingLabels(labelLayer, labels);
                                                        //axMap1.Redraw();
                                                        phanTichControl.setDepth(0);
                                                        item.depth = 0;
                                                        //lstCenterGlobal.Add(item);
                                                    }
                                                    else
                                                    {
                                                        var diemDoGanNhat = lstDiemDo1.Where(x => x.CodeMachine == codeMachine &&
                                                                                                Math.Abs(x.XUtm - cWGS84.UTM.Northing) < Epsilone &&
                                                                                                Math.Abs(x.YUtm - cWGS84.UTM.Easting) < Epsilone).LastOrDefault();

                                                        if (diemDoGanNhat != null)
                                                        {
                                                            diemDoGanNhat.DepthBoom = DepthSchedule(diemDoGanNhat.XUtm, diemDoGanNhat.YUtm, diemDoGanNhat.Z,
                                                                                            cWGS84.UTM.Northing, cWGS84.UTM.Easting, dValData);
                                                            //labels.AddLabel(diemDoGanNhat.DepthBoom.ToString(), dLong, dLat);
                                                            //labels.OffsetX = 20;
                                                            //labels.OffsetY = 20;
                                                            //axMap1.set_DrawingLabels(handleLayer, labels);
                                                            addFlagRealTime(dLong, dLat, diemDoGanNhat.DepthBoom);
                                                            labelText = string.Format(
                                                                "Mã máy: {0}\n" +
                                                                "Kinh độ: {1}\n" +
                                                                "Vĩ độ: {2}\n" +
                                                                "Cường độ từ trường: {3}\n" +
                                                                "Độ sâu: {4}m",
                                                                codeMachine, Math.Round(dLong, 6), Math.Round(dLat, 6), Math.Round(dValData * _HeSoMayDoBom), diemDoGanNhat.DepthBoom
                                                            );
                                                            labels.AddLabel(labelText, dLong, dLat);
                                                            //axMap1.set_DrawingLabels(labelLayer, labels);
                                                            //axMap1.Redraw();
                                                            phanTichControl.setDepth(diemDoGanNhat.DepthBoom);
                                                            item.depth = diemDoGanNhat.DepthBoom;
                                                            //lstCenterGlobal.Add(item);
                                                        }
                                                        else
                                                        {
                                                            //labels.AddLabel("0", dLong, dLat);
                                                            //labels.OffsetX = 20;
                                                            //labels.OffsetY = 20;
                                                            //axMap1.set_DrawingLabels(handleLayer, labels);
                                                            addFlagRealTime(dLong, dLat, 0);
                                                            labelText = string.Format(
                                                                "Mã máy: {0}\n" +
                                                                "Kinh độ: {1}\n" +
                                                                "Vĩ độ: {2}\n" +
                                                                "Cường độ từ trường: {3}\n" +
                                                                "Độ sâu: {4}m",
                                                                codeMachine, Math.Round(dLong, 6), Math.Round(dLat, 6), Math.Round(dValData * _HeSoMayDoBom), 0
                                                            );
                                                            labels.AddLabel(labelText, dLong, dLat);
                                                            //axMap1.set_DrawingLabels(labelLayer, labels);
                                                            //axMap1.Redraw();
                                                            phanTichControl.setDepth(0);
                                                            item.depth = 0;
                                                            //lstCenterGlobal.Add(item);
                                                        }
                                                        //axMap1.Redraw();
                                                        UpdateDiemCamCo();
                                                    }
                                                    phanTichControl.Click += (sender2, e2) =>
                                                    {
                                                        foreach (Control control in pnlKQPTContainer.Controls)
                                                        {
                                                            control.BackColor = Control.DefaultBackColor;
                                                            //if (control is KQPhanTichControl)
                                                            //{
                                                            //    KQPhanTichControl ptControl = (KQPhanTichControl)control;
                                                            //    ptControl.label_info.Visible = false;
                                                            //    //axMap1.Redraw();
                                                            //}
                                                        }
                                                        axMap1.RemoveLayer(highlightLayer);
                                                        InitHighlightLayer();
                                                        //double minLat = 0, maxLat = 0, minLong = 0, maxLong = 0;
                                                        //axMap1.PixelToProj(ext.xMin, )
                                                        if (mouseUpHandler != null)
                                                        {
                                                            axMap1.MouseUpEvent -= mouseUpHandler;
                                                        }

                                                        mouseUpHandler = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((sender3, e3) =>
                                                        {
                                                            //if (labels.get_Label(0, 0) != null)
                                                            //{
                                                            //    Extents ext = labels.get_Label(0, 0).ScreenExtents;
                                                            //    if (ext != null)
                                                            //    {
                                                            //        if (e3.x < ext.xMin || e3.x > ext.xMax || e3.y < ext.yMin || e3.y > ext.yMax)
                                                            //        {
                                                            //            labels.Visible = false;
                                                            //            axMap1.RemoveLayer(highlightLayer);
                                                            //            InitHighlightLayer();
                                                            //        }
                                                            //    }
                                                            //}
                                                            labels.Visible = false;
                                                            axMap1.RemoveLayer(highlightLayer);
                                                            InitHighlightLayer();

                                                        });
                                                        axMap1.MouseUpEvent += mouseUpHandler;
                                                        phanTichControl.BackColor = Color.Green;
                                                        //axMap1.SetLatitudeLongitude(dLat, dLong);
                                                        //axMap1.ZoomToTileLevel(25);
                                                        Extents extents = new Extents();
                                                        extents.SetBounds(dLong - 0.00001, dLat - 0.00001, 0, dLong + 0.00001, dLat + 0.00001, 0);
                                                        axMap1.Extents = extents;
                                                        axMap1.set_DrawingLabels(labelLayer, labels);
                                                        //labels.Visible = true;
                                                        addHighlight(dLong, dLat);
                                                        //axMap1.Redraw();
                                                    };
                                                    pnlKQPTContainer.Controls.Add(phanTichControl);
                                                }

                                            }
                                            
                                            lstP.Add(0);

                                            //draw chart
                                            //DateTime now = DateTime.Now;
                                            DateTime now = new DateTime(int.Parse(elements[7]), int.Parse(elements[6]), int.Parse(elements[5]), int.Parse(elements[2]), int.Parse(elements[3]), int.Parse(elements[4]));
                                            now = now.ToLocalTime();
                                            if (chart_bomb.ChartAreas[0].AxisX.Title != "Thời gian (" + now.Hour + "h" + now.Minute + "p...s)")
                                            {
                                                chart_bomb.ChartAreas[0].AxisX.Title = "Thời gian (" + now.Hour + "h" + now.Minute + "p...s)";
                                                chart_bomb.Series.Clear();
                                                machine_series_bomb.Clear();
                                            }
                                            if (!machine_series_bomb.ContainsKey(codeMachine))
                                            {
                                                Series series = new Series(codeMachine);
                                                machine_series_bomb.Add(codeMachine, series);
                                                chart_bomb.Series.Add(series);
                                                chart_bomb.Series[codeMachine].ChartType = SeriesChartType.Line;
                                                chart_bomb.Series[codeMachine].BorderWidth = 7;
                                                //chart_bomb.Series[machineId].Points.AddXY(0, 0);
                                            }
                                            chart_bomb.Series[codeMachine].Points.AddXY(now.Second, dValData * _HeSoMayDoBom);
                                            

                                            //machine__label[codeMachine].ForeColor = machine__label[codeMachine]._ColorRun;
                                            //machine__label[codeMachine].setForeColor(machineActive__color[codeMachine]);
                                            
                                            
                                            machine__label[codeMachine].setMagnetic(dValData * _HeSoMayDoBom);
                                            if (machine__timeout.ContainsKey(codeMachine))
                                            {
                                                machine__timeout[codeMachine].Stop();
                                            }
                                            else
                                            {
                                                machine__timeout.Add(codeMachine, null);
                                            }
                                            machine__timeout[codeMachine] = new CustomTimeout(() =>
                                            {
                                                machine__label[codeMachine].setActive(false);
                                            }, MACHINE_ACTIVE_TIMEOUT * 1000);
                                            //draw map
                                            addMachinePointRealTime(dLong, dLat, codeMachine, timeActionData);
                                            addMachinePointRealTimeModel(dLong, dLat, codeMachine, timeActionData);
                                        }));
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    var mess = ex.Message;
                                    //ed.WriteMessage(mess);
                                }
                                ptCount++;
                                //if (ptCount >= PTCOUNT)
                                //{
                                    //if (ptTimeout != null)
                                    //{
                                    //    ptTimeout.Stop();
                                    //}
                                    //ptCount = 0;
                                    //phanTichTest();
                                //}
                            }
                        }
                    }
                }
                else if ((strMess.Length > 5) &&
                    (strMess[0] == '$') &&
                    (strMess[1] == 'M') &&
                    (strMess[2] == 'D') &&
                    (strMess[3] == 'M') &&
                    (strMess[4] == ','))
                {
                    string strBuff = new string(strMess);
                    string[] elements = strBuff.Split(',');
                    if (elements.Length > 10)
                    {
                        short numValue = short.Parse(elements[8]);
                        short numGPS = short.Parse(elements[9]);

                        var codeMachine = elements[1];
                        double dilution = double.Parse(elements[11]);
                        int satelliteCount = int.Parse(elements[12]);
                        //lọc máy dò
                        if (!machine__label.ContainsKey(codeMachine))
                            return;

                        if (numGPS <= 0)
                            machine__label[codeMachine].BackColor = Color.Yellow;
                        else
                            machine__label[codeMachine].BackColor = Color.White;

                        /////////////////////////// first version not process numGPS==0
                        if (numGPS > 0 || numValue > 0)
                        {
                            double northingData = 0, eastingData = 0, dValData = 0, dLat = 0, dLong = 0;
                            DateTime updateTimeData = new DateTime();
                            DateTime timeActionData = new DateTime();
                            int bitSent = 0;
                            string dateTimeVal = elements[2] + ":" + elements[3] + ":" + elements[4] + " " + elements[5] + "/" + elements[6] + "/" + elements[7];

                            int.TryParse(elements[10], out bitSent);

                            byte value = 0;
                            uint led14, mask = 80;
                            for (short i = 0; i < numValue; i++)
                            {
                                value = byte.Parse(elements[13 + i]);

                                led14 = value;
                                led14 &= mask;
                                if (led14 > 0)
                                    led14 = 1;
                                mask = value;
                                mask &= 127;
                                //if (i == numValue - 1)
                                //    AddLine("Value = " + mask + "-" + led14); // gia tri thang do - trang thai led 14
                                //else
                                //    AddLine("Value = " + mask + "-" + led14 + ","); // gia tri thang do - trang thai led 14

                                if (led14 == 1)
                                    dValData = value;

                                if (i == numValue - 1 && dValData == 0)
                                    dValData = value;
                            }
                            short offset = numValue;
                            offset += 13;
                            bool isFirstFlag = true;
                            for (short k = 0; k < numGPS; k++)
                            {

                                try
                                {
                                    string timeGPS = elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++];
                                    string latVal = elements[offset++];
                                    string longVal = elements[offset++];

                                    if (ValidateValue(dateTimeVal, timeGPS, latVal, longVal, value.ToString()) == false)
                                        continue;

                                    double.TryParse(latVal, out dLat);
                                    double.TryParse(longVal, out dLong);

                                    CultureInfo enUS = new CultureInfo("en-US");
                                    DateTime.TryParseExact(dateTimeVal, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out updateTimeData);
                                    updateTimeData = updateTimeData.ToLocalTime();
                                    DateTime.TryParseExact(timeGPS, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out timeActionData);
                                    timeActionData = timeActionData.ToLocalTime();

                                    Coordinate cWGS84 = new Coordinate(dLat, dLong);

                                    //AddDatabase(_currentId, codeMachine, cWGS84.UTM.Northing, cWGS84.UTM.Easting, dValData, updateTimeData, timeActionData, bitSent, false, dilution, satelliteCount);
                                    if (this.InvokeRequired)
                                    {
                                        this.Invoke(new MethodInvoker(delegate
                                        {
                                            
                                            //add data to table
                                            bool isCamCo = CheckCamCo(bitSent, out bool isButton1Press);
                                            AddDataToTable(codeMachine, updateTimeData, dValData, dLong, dLat, timeActionData, bitSent, satelliteCount, dilution, isButton1Press, "Mìn");
                                            machine__label[codeMachine].setGPS(dLat, dLong);
                                            EventHandler handler = new EventHandler((sender2, e2) =>
                                            {
                                                //axMap1.SetLatitudeLongitude(dLat, dLong);
                                                Extents extents = new Extents();
                                                extents.SetBounds(dLong - 0.00001, dLat - 0.00001, 0, dLong + 0.00001, dLat + 0.00001, 0);
                                                axMap1.Extents = extents;
                                                addMachineNote(dLong, dLat, codeMachine);
                                            });
                                            if (machine__event.ContainsKey(codeMachine))
                                            {
                                                machine__label[codeMachine].Click -= machine__event[codeMachine];
                                                machine__event[codeMachine] = handler;
                                            }
                                            else
                                            {
                                                machine__event.Add(codeMachine, handler);
                                            }
                                            machine__label[codeMachine].Click += machine__event[codeMachine];
                                            
                                            //int handleLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                                            //Labels labels = axMap1.get_DrawingLabels(handleLayer);
                                            //labels.FontSize = 12;
                                            if (isCamCo)
                                            {
                                                if (isFirstFlag)
                                                {
                                                    isFirstFlag = false;
                                                    PlaySound(pathSound);
                                                    Vertex item = new Vertex(cWGS84.UTM.Northing, cWGS84.UTM.Easting, dValData, Vertex.TYPE_MINE);
                                                    item.BitSent = bitSent;
                                                    item.Type = Vertex.CAMCO;
                                                    item.X = cWGS84.UTM.Easting;
                                                    item.Y = cWGS84.UTM.Northing;

                                                    Labels labels = new Labels();
                                                    labels.FontSize = 12;
                                                    labels.Visible = false;
                                                    labels.Alignment = tkLabelAlignment.laBottomRight;
                                                    labels.OffsetX = 20;
                                                    labels.OffsetY = 20;
                                                    labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
                                                    labels.AvoidCollisions = true;
                                                    //labels_info.AvoidCollisions = true;

                                                    //axMap1.set_DrawingLabels(handleLayer_info, labels_info);
                                                    //axMap1.Redraw();
                                                    string labelText = "";

                                                    //labels_info.AvoidCollisions = true;

                                                    //axMap1.set_DrawingLabels(handleLayer_info, labels_info);
                                                    //axMap1.Redraw();
                                                    KQPhanTichControl phanTichControl = new KQPhanTichControl(
                                                        KQPhanTichControl.CAMCO,
                                                        machine__label[codeMachine].getLblMachineCode().Text,
                                                        machine__label[codeMachine].getLblStaff().Text,
                                                        Math.Round(dLat, 6), Math.Round(dLong, 6), Math.Round(dValData, 6),
                                                        0, false, false
                                                    );
                                                    //phanTichControl.label_info = labels_info;

                                                    //dgvData.Rows.Add(++count, longt, latt, item.Z);
                                                    //bool existNearby = false;
                                                    //foreach (Vertex vertex in lstCenterGlobal)
                                                    //{
                                                    //    if (Math.Sqrt(Math.Pow(item.X - vertex.X, 2) + Math.Pow(item.Y - vertex.Y, 2)) < min_distance)
                                                    //    {
                                                    //        existNearby = true;
                                                    //        if (vertex.Type == Vertex.BOM)
                                                    //        {
                                                    //            if (vertex.phanTichControl.InvokeRequired)
                                                    //            {
                                                    //                vertex.phanTichControl.Invoke(new MethodInvoker(delegate
                                                    //                {
                                                    //                    if (vertex.TypeBombMine == Vertex.TYPE_BOMB)
                                                    //                    {
                                                    //                        vertex.phanTichControl.setExistBomb(true);
                                                    //                    }
                                                    //                    else if (vertex.TypeBombMine == Vertex.TYPE_MINE)
                                                    //                    {
                                                    //                        vertex.phanTichControl.setExistMine(true);
                                                    //                    }
                                                    //                }));
                                                    //            }
                                                    //            else
                                                    //            {
                                                    //                if (vertex.TypeBombMine == Vertex.TYPE_BOMB)
                                                    //                {
                                                    //                    vertex.phanTichControl.setExistBomb(true);
                                                    //                }
                                                    //                else if (vertex.TypeBombMine == Vertex.TYPE_MINE)
                                                    //                {
                                                    //                    vertex.phanTichControl.setExistMine(true);
                                                    //                }
                                                    //            }
                                                    //        }
                                                    //        else if (vertex.Type == Vertex.BOM && item.Type == Vertex.CAMCO)
                                                    //        {
                                                    //            vertex.Type = Vertex.CAMCO;
                                                    //            if (vertex.phanTichControl.InvokeRequired)
                                                    //            {
                                                    //                vertex.phanTichControl.Invoke(new MethodInvoker(delegate
                                                    //                {
                                                    //                    vertex.phanTichControl.setType(Vertex.CAMCO);
                                                    //                }));
                                                    //            }
                                                    //            else
                                                    //            {
                                                    //                vertex.phanTichControl.setType(Vertex.CAMCO);
                                                    //            }
                                                    //        }
                                                    //    }
                                                    //}
                                                    //if (!existNearby)
                                                    //{
                                                    //    pnlKQPTContainer.Controls.Add(phanTichControl);
                                                    //}

                                                    //int handleLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                                                    //Labels labels = axMap1.get_DrawingLabels(handleLayer);
                                                    //labels.FontSize = 12;
                                                    if (isButton1Press)
                                                    {
                                                        //row.DefaultCellStyle.BackColor = Color.Red;

                                                        DiemDoData diemDoData = new DiemDoData();
                                                        diemDoData.XUtm = cWGS84.UTM.Northing;
                                                        diemDoData.YUtm = cWGS84.UTM.Easting;
                                                        diemDoData.Z = dValData;
                                                        diemDoData.CodeMachine = codeMachine;
                                                        diemDoData.DepthBoom = double.NaN;
                                                        lstDiemDo1.Add(diemDoData);
                                                        //labels.AddLabel("0", dLong, dLat);
                                                        //labels.OffsetX = 20;
                                                        //labels.OffsetY = 20;
                                                        //axMap1.set_DrawingLabels(handleLayer, labels);
                                                        addFlagRealTime(dLong, dLat, 0);
                                                        labelText = string.Format(
                                                            "Mã máy: {0}\n" +
                                                            "Kinh độ: {1}\n" +
                                                            "Vĩ độ: {2}\n" +
                                                            "Cường độ từ trường: {3}\n" +
                                                            "Độ sâu: {4}m",
                                                            codeMachine, Math.Round(dLong, 6), Math.Round(dLat, 6), Math.Round(dValData * _HeSoMayDoBom), 0
                                                        );
                                                        labels.AddLabel(labelText, dLong, dLat);
                                                        //axMap1.set_DrawingLabels(labelLayer, labels);
                                                        //axMap1.Redraw();
                                                        phanTichControl.setDepth(0);
                                                        item.depth = 0;
                                                        //lstCenterGlobal.Add(item);
                                                    }
                                                    else
                                                    {
                                                        var diemDoGanNhat = lstDiemDo1.Where(x => x.CodeMachine == codeMachine &&
                                                                                                Math.Abs(x.XUtm - cWGS84.UTM.Northing) < Epsilone &&
                                                                                                Math.Abs(x.YUtm - cWGS84.UTM.Easting) < Epsilone).LastOrDefault();

                                                        if (diemDoGanNhat != null)
                                                        {
                                                            diemDoGanNhat.DepthBoom = DepthSchedule(diemDoGanNhat.XUtm, diemDoGanNhat.YUtm, diemDoGanNhat.Z,
                                                                                            cWGS84.UTM.Northing, cWGS84.UTM.Easting, dValData);
                                                            //labels.AddLabel(diemDoGanNhat.DepthBoom.ToString(), dLong, dLat);
                                                            //labels.OffsetX = 20;
                                                            //labels.OffsetY = 20;
                                                            //axMap1.set_DrawingLabels(handleLayer, labels);
                                                            addFlagRealTime(dLong, dLat, diemDoGanNhat.DepthBoom);
                                                            labelText = string.Format(
                                                                "Mã máy: {0}\n" +
                                                                "Kinh độ: {1}\n" +
                                                                "Vĩ độ: {2}\n" +
                                                                "Cường độ từ trường: {3}\n" +
                                                                "Độ sâu: {4}m",
                                                                codeMachine, Math.Round(dLong, 6), Math.Round(dLat, 6), Math.Round(dValData), diemDoGanNhat.DepthBoom
                                                            );
                                                            labels.AddLabel(labelText, dLong, dLat);
                                                            //axMap1.set_DrawingLabels(labelLayer, labels);
                                                            //axMap1.Redraw();
                                                            phanTichControl.setDepth(diemDoGanNhat.DepthBoom);
                                                            item.depth = diemDoGanNhat.DepthBoom;
                                                            //lstCenterGlobal.Add(item);
                                                        }
                                                        else
                                                        {
                                                            //labels.AddLabel("0", dLong, dLat);
                                                            //labels.OffsetX = 20;
                                                            //labels.OffsetY = 20;
                                                            //axMap1.set_DrawingLabels(handleLayer, labels);
                                                            addFlagRealTime(dLong, dLat, 0);
                                                            labelText = string.Format(
                                                                "Mã máy: {0}\n" +
                                                                "Kinh độ: {1}\n" +
                                                                "Vĩ độ: {2}\n" +
                                                                "Cường độ từ trường: {3}\n" +
                                                                "Độ sâu: {4}m",
                                                                codeMachine, Math.Round(dLong, 6), Math.Round(dLat, 6), Math.Round(dValData), 0
                                                            );
                                                            labels.AddLabel(labelText, dLong, dLat);
                                                            //axMap1.set_DrawingLabels(labelLayer, labels);
                                                            //axMap1.Redraw();
                                                            phanTichControl.setDepth(0);
                                                            item.depth = 0;
                                                            //lstCenterGlobal.Add(item);
                                                        }
                                                        //axMap1.Redraw();
                                                        UpdateDiemCamCo();
                                                    }
                                                    phanTichControl.Click += (sender2, e2) =>
                                                    {
                                                        foreach (Control control in pnlKQPTContainer.Controls)
                                                        {
                                                            control.BackColor = Control.DefaultBackColor;
                                                            //if (control is KQPhanTichControl)
                                                            //{
                                                            //    KQPhanTichControl ptControl = (KQPhanTichControl)control;
                                                            //    ptControl.label_info.Visible = false;
                                                            //    //axMap1.Redraw();
                                                            //}
                                                        }
                                                        axMap1.RemoveLayer(highlightLayer);
                                                        InitHighlightLayer();
                                                        //double minLat = 0, maxLat = 0, minLong = 0, maxLong = 0;
                                                        //axMap1.PixelToProj(ext.xMin, )
                                                        if (mouseUpHandler != null)
                                                        {
                                                            axMap1.MouseUpEvent -= mouseUpHandler;
                                                        }

                                                        mouseUpHandler = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((sender3, e3) =>
                                                        {
                                                            //if (labels.get_Label(0, 0) != null)
                                                            //{
                                                            //    Extents ext = labels.get_Label(0, 0).ScreenExtents;
                                                            //    if (ext != null)
                                                            //    {
                                                            //        if (e3.x < ext.xMin || e3.x > ext.xMax || e3.y < ext.yMin || e3.y > ext.yMax)
                                                            //        {
                                                            //            labels.Visible = false;
                                                            //            axMap1.RemoveLayer(highlightLayer);
                                                            //            InitHighlightLayer();
                                                            //        }
                                                            //    }
                                                            //}
                                                            labels.Visible = false;
                                                            axMap1.RemoveLayer(highlightLayer);
                                                            InitHighlightLayer();

                                                        });
                                                        axMap1.MouseUpEvent += mouseUpHandler;
                                                        phanTichControl.BackColor = Color.Green;
                                                        //axMap1.SetLatitudeLongitude(dLat, dLong);
                                                        //axMap1.ZoomToTileLevel(25);
                                                        Extents extents = new Extents();
                                                        extents.SetBounds(dLong - 0.00001, dLat - 0.00001, 0, dLong + 0.00001, dLat + 0.00001, 0);
                                                        axMap1.Extents = extents;
                                                        axMap1.set_DrawingLabels(labelLayer, labels);
                                                        //labels.Visible = true;
                                                        addHighlight(dLong, dLat);
                                                        //axMap1.Redraw();
                                                    };
                                                    pnlKQPTContainer.Controls.Add(phanTichControl);
                                                }
                                            }

                                            lstP.Add(0);
                                            //draw chart
                                            //DateTime now = DateTime.Now;
                                            DateTime now = new DateTime(int.Parse(elements[7]), int.Parse(elements[6]), int.Parse(elements[5]), int.Parse(elements[2]), int.Parse(elements[3]), int.Parse(elements[4]));
                                            now = now.ToLocalTime();
                                            if (chart_mine.ChartAreas[0].AxisX.Title != "Thời gian (" + now.Hour + "h" + now.Minute + "p...s)")
                                            {
                                                chart_mine.ChartAreas[0].AxisX.Title = "Thời gian (" + now.Hour + "h" + now.Minute + "p...s)";
                                                chart_mine.Series.Clear();
                                                machine_series_mine.Clear();
                                            }
                                            if (!machine_series_mine.ContainsKey(codeMachine))
                                            {
                                                Series series = new Series(codeMachine);
                                                machine_series_mine.Add(codeMachine, series);
                                                chart_mine.Series.Add(series);
                                                chart_mine.Series[codeMachine].ChartType = SeriesChartType.Line;
                                                chart_mine.Series[codeMachine].BorderWidth = 7;
                                                //chart_bomb.Series[machineId].Points.AddXY(0, 0);
                                            }
                                            chart_mine.Series[codeMachine].Points.AddXY(now.Second, dValData);
                                            
                                            machine__label[codeMachine].setMagnetic(dValData);
                                            if (machine__timeout.ContainsKey(codeMachine))
                                            {
                                                machine__timeout[codeMachine].Stop();
                                            }
                                            else
                                            {
                                                machine__timeout.Add(codeMachine, null);
                                            }
                                            machine__timeout[codeMachine] = new CustomTimeout(() =>
                                            {
                                                machine__label[codeMachine].setActive(false);
                                            }, MACHINE_ACTIVE_TIMEOUT * 1000);
                                            //draw map
                                            //axMap1.DrawPointEx(pointLayer, dLong, dLat, 10, ColorToUint(machineActive__color[codeMachine]));
                                            addMachinePointRealTime(dLong, dLat, codeMachine, timeActionData);
                                            addMachinePointRealTimeModel(dLong, dLat, codeMachine, timeActionData);
                                        }));
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    var mess = ex.Message;
                                    //ed.WriteMessage(mess);
                                }
                                ptCount++;
                                //if (ptCount >= PTCOUNT)
                                //{
                                    //if (ptTimeout != null)
                                    //{
                                    //    ptTimeout.Stop();
                                    //}
                                    //ptCount = 0;
                                    //phanTichTest();
                                //}
                            }
                        }
                    }
                }
                //}
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
            }
        }

        private void UpdateDiemCamCo()
        {
            //dgvDiemCamCo.Rows.Clear();

            int diemCamCo = 1;
            for (int i = 0; i < lstDiemDo1.Count; i++)
            {
                //if (double.IsNaN(lstDiemDo1[i].DepthBoom))
                //    continue;

                Point2d latLong = AppUtils.ConverUTMToLatLong(lstDiemDo1[i].XUtm, lstDiemDo1[i].YUtm);
                //dgvDiemCamCo.Rows.Add(diemCamCo, lstDiemDo1[i].CodeMachine, string.Format("{0},{1}", latLong.X, latLong.Y), lstDiemDo1[i].DepthBoom);

                diemCamCo++;
            }
        }

        public static bool CheckCamCo(int aValue, out bool isButton1Press)
        {
            // Button 1 la diem cam co, button 2 la dung de do do sau (A An)
            isButton1Press = false;
            bool isButton1 = ((aValue & 8) > 0);   // khong thay doi
            bool isButton2 = ((aValue & 2) > 0);  // sua lai
            bool isButton3 = ((aValue & 4) > 0); // sua lai
            bool isButton4 = ((aValue & 1) > 0); // khong sua

            if (isButton1)
                isButton1Press = true;

            if (isButton1 || isButton2)
                return true;
            else
                return false;
        }

        private void AddDatabase(string projectId, string machinebomCode, double longVal, double latVal, double valueZ, DateTime update_timeVal, DateTime timeActionVal, int bitSent, bool isMachineBomData, double dilution, int satelliteCount)
        {
            if (projectId == "-1")
                return;
            try
            {
                //add document into mongodb
                if (frmLoggin.mgCon != null)
                {
                    var database = frmLoggin.mgCon.GetDatabase("db_cecm");
                    if (database != null)
                    {
                        var collection = database.GetCollection<InfoConnect>("cecm_data");
                        var document = new InfoConnect();
                        document.code = machinebomCode;
                        document.project_id = long.Parse(projectId);
                        document.machineBomCode = machinebomCode;
                        document.lat_value = latVal;
                        document.long_value = longVal;
                        double longt = 0, latt = 0;
                        ToLatLon(latVal, longVal, ref latt, ref longt, "48N");
                        document.coordinate = new GeoJsonPoint<GeoJson2DCoordinates>(GeoJson.Position(longt, latt));
                        document.the_value = valueZ;
                        document.update_time = update_timeVal;
                        //document.time_action = timeActionVal;
                        document.time_action = DateTime.Now.ToUniversalTime();
                        document.bit_sens = bitSent;
                        document.isMachineBom = isMachineBomData;
                        document.dilution = dilution;
                        document.satelliteCount = satelliteCount;
                        //{
                        //    { "code", machinebomCode },
                        //    { "project_id", long.Parse(projectId) },
                        //    { "machineBomCode", machinebomCode },
                        //    { "lat_value", longVal },
                        //    { "long_value", latVal},
                        //    { "coordinate", new GeoJsonPoint<GeoJson2DCoordinates>(GeoJson.Position(longVal, latVal))},
                        //    { "the_value", valueZ},
                        //    { "update_time", update_timeVal},
                        //    { "time_action", timeActionVal},
                        //    { "bit_sens", bitSent},
                        //    { "isMachineBom", isMachineBomData}
                        //};
                        collection.InsertOne(document);
                    }
                }
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
            }
        }

        private bool ValidateValue(string dateTimeVal, string timeGPSVal, string latVal, string longVal, string valueZ)
        {
            CultureInfo enUS = new CultureInfo("en-US");

            if (DateTime.TryParseExact(dateTimeVal, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out DateTime updateTime) == false)
                return false;

            if (DateTime.TryParseExact(timeGPSVal, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out DateTime timeGPS) == false)
                return false;

            if (double.TryParse(latVal, out double dLat) == false)
                return false;

            if (double.TryParse(longVal, out double dLong) == false)
                return false;

            if (double.TryParse(valueZ, out double dZ) == false)
                return false;

            return true;
        }

        //public void UpdateSelectedOluoi()
        //{
        //    //OluoiDataView itemSelected = cbOLuoi.SelectedItem as OluoiDataView;
        //    //if (itemSelected == null)
        //    //    return;

        //    //itemSelectedOLuoi = itemSelected;

        //    MonitorForm2Cmd cmd = new MonitorForm2Cmd();
        //    //cmd.UpdateDGVDoSau(dgvData, itemSelected);

        //    TuDongPhanTichNewCmd cmdTuDong = new TuDongPhanTichNewCmd();
        //    TuDongPhanTichNewCmd.syncCtrl = this;
        //    cmdTuDong.SetImage(MgdAcApplication.DocumentManager.MdiActiveDocument, this);
        //}

        private void btnCapNhatDuLieu_Click(object sender, EventArgs e)
        {
            UpdateModelData();
        }

        private void UpdateModelData()
        {
            //try
            //{
            //    TuDongPhanTichNewCmd cmd = new TuDongPhanTichNewCmd();
            //    TuDongPhanTichNewCmd.syncCtrl = this;
            //    TuDongPhanTichNewCmd._idDuAn = int.Parse(_currentId);
            //    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(cmd.BackgroundProcess));
            //    thread.Start();
            //}
            //catch (System.Exception ex)
            //{
            //    var mess = ex.Message;
            //}
        }

        private void mPreviewCtrl_Click(object sender, EventArgs e)
        {
        }

        public static uint ColorToUint(Color c)
        {
            //uint u = (uint)c.A << 24;
            //u += (uint)c.R << 16;
            //u += (uint)c.G << 8;
            //u += c.B;
            return Convert.ToUInt32(ColorTranslator.ToOle(c));

            // (UInt32)((UInt32)c.A << 24 + (UInt32)c.R << 16 + (UInt32)c.G << 8 + (UInt32)c.B);
        }

        private void initPolygonLayer()
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

        //private void initOluoiLayer()
        //{
        //    //Shapefile sf = new Shapefile();
        //    ////sf.Open(filename, null);
        //    //polygonLayer = axMap1.AddLayer(sf, true);
        //    //sf = axMap1.get_Shapefile(polygonLayer);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
        //    Shapefile sf = new Shapefile();
        //    if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYGON))
        //    {
        //        MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
        //        return;
        //    }
        //    oluoiLayer = axMap1.AddLayer(sf, true);
        //}

        private void drawOluoi(
            double lat_corner1, double long_corner1,
            double lat_corner2, double long_corner2,
            double lat_corner3, double long_corner3,
            double lat_corner4, double long_corner4)
        {
            axMap1.DrawLineEx(oluoiLayer, long_corner1, lat_corner1, long_corner2, lat_corner2, 5, ColorToUint(Color.White));
            axMap1.DrawLineEx(oluoiLayer, long_corner2, lat_corner2, long_corner3, lat_corner3, 5, ColorToUint(Color.White));
            axMap1.DrawLineEx(oluoiLayer, long_corner3, lat_corner3, long_corner4, lat_corner4, 5, ColorToUint(Color.White));
            axMap1.DrawLineEx(oluoiLayer, long_corner4, lat_corner4, long_corner1, lat_corner1, 5, ColorToUint(Color.White));
        }

        private void InitPointLayer()
        {
            pointLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        }

        private void InitPointImageLayer()
        {
            foreach (string color in colors)
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
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), ICON_FOLDER, color + ".png");
                options.Picture = this.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
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

        private void InitLineLayer()
        {
            foreach (string color in colors)
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
                options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private void InitPointImageModelLayer()
        {

            foreach (string color in colors)
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
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), ICON_FOLDER, color + ".png");
                options.Picture = this.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
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

        private void InitLineModelLayer()
        {
            foreach (string color in colors)
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
                options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private void InitPointImageModelHistoryLayer()
        {

            foreach (string color in colors)
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
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), ICON_FOLDER, color + ".png");
                options.Picture = this.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
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

        private void InitLineModelHistoryLayer()
        {
            foreach (string color in colors)
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
                options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private void InitPointImageRealTimeLayer()
        {
            foreach (string color in colors)
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
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), ICON_FOLDER, color + ".png");
                options.Picture = this.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
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

        private void InitLineRealTimeLayer()
        {
            foreach (string color in colors)
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
                options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private void InitPointImageRealTimeModelLayer()
        {
            foreach (string color in colors)
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
                string pathpng = System.IO.Path.Combine(AppUtils.GetAppDataPath(), ICON_FOLDER, color + ".png");
                options.Picture = this.OpenImage(pathpng);
                options.AlignPictureByBottom = false;

                //options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
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

        private void InitLineRealTimeModelLayer()
        {
            foreach (string color in colors)
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
                options.LineColor = ColorToUint(ColorTranslator.FromHtml(color));
                options.LineWidth = 2;

                MapWinGIS.LinePattern pattern = new MapWinGIS.LinePattern();
                pattern.AddLine(ColorToUint(ColorTranslator.FromHtml(color)), 2, tkDashStyle.dsSolid);
                LineSegment segm = pattern.AddMarker(tkDefaultPointSymbol.dpsArrowDown);
                segm.Color = ColorToUint(Color.Black);
                segm.MarkerSize = 15;
                segm.MarkerInterval = 32;
                segm.MarkerIntervalIsRelative = false;
                segm.MarkerOrientation = tkLineLabelOrientation.lorParallel;
                options.LinePattern = pattern;
                options.UseLinePattern = true;
            }
        }

        private void drawPolygon(double[] xPoints, double[] yPoints)
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
            axMap1.DrawPolygon(ref X, ref Y, size, ColorToUint(Color.FromArgb(173, 38, 169)), true, 45);
            axMap1.Redraw();
        }

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader(@"C:\temp\dataRPBM.json"))
            {
                string json = r.ReadToEnd();
                lst = JsonConvert.DeserializeObject<List<InfoConnect>>(json);
            }
        }

        //private void LoadMatCat()
        //{
        //    using (StreamReader r = new StreamReader(@"C:\temp\dataRPBM.json"))
        //    {
        //        string json = r.ReadToEnd();
        //        DataRPBM data = JsonConvert.DeserializeObject<DataRPBM>(json);
        //        //double longCorner1 = 0;
        //        //double longCorner2 = 0;
        //        //double longCorner3 = 0;
        //        //double longCorner4 = 0;
        //        //double lattCorner1 = 0;
        //        //double lattCorner2 = 0;
        //        //double lattCorner3 = 0;
        //        //double lattCorner4 = 0;

        //        //double firstLattStart = 0;
        //        //double firstLattEnd = 0;
        //        //double firstLongStart = 0;
        //        //double firstLongEnd = 0;

        //        double longtMin = 0;
        //        double lattMin = 0;
        //        double longtMax = 0;
        //        double lattMax = 0;
        //        double count = 0;
        //        foreach (Models.MatCatTuTruong matCatData in data.MatCatTuTruongDatas)
        //        {
        //            double lattStart = 0;
        //            double longtStart = 0;
        //            double lattEnd = 0;
        //            double longtEnd = 0;
        //            ToLatLon(matCatData.xPointStart, matCatData.yPointStart, ref lattStart, ref longtStart, "48N");
        //            ToLatLon(matCatData.xPointEnd, matCatData.yPointEnd, ref lattEnd, ref longtEnd, "48N");
        //            if (count == 0)
        //            {
        //                longtMin = Math.Min(longtStart, longtEnd);
        //                lattMin = Math.Min(lattStart, lattEnd);
        //            }
        //            count++;
        //            if (Math.Min(longtStart, longtEnd) < longtMin)
        //            {
        //                longtMin = Math.Min(longtStart, longtEnd);
        //            }
        //            if (Math.Min(lattStart, lattEnd) < lattMin)
        //            {
        //                lattMin = Math.Min(lattStart, lattEnd);
        //            }
        //            if (Math.Max(longtStart, longtEnd) > longtMax)
        //            {
        //                longtMax = Math.Max(longtStart, longtEnd);
        //            }
        //            if (Math.Max(lattStart, lattEnd) > lattMax)
        //            {
        //                lattMax = Math.Max(lattStart, lattEnd);
        //            }
        //            axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        //            //longtStart = Math.Round(longtStart, 6);
        //            //lattStart = Math.Round(lattStart, 6);
        //            //longtEnd = Math.Round(longtEnd, 6);
        //            //lattEnd = Math.Round(lattEnd, 6);
        //            //if(matCatData.xPointStart == matCatData.xPointEnd)
        //            //{
        //            //    longtStart = Math.Round(longtStart, 4);
        //            //    longtEnd = Math.Round(longtEnd, 4);
        //            //}
        //            //if(matCatData.yPointStart == matCatData.yPointEnd)
        //            //{
        //            //    lattStart = Math.Round(lattStart, 4);
        //            //    lattEnd = Math.Round(lattEnd, 4);
        //            //}
        //            axMap1.DrawLine(longtStart, lattStart, longtEnd, lattEnd, 1, ColorToUint(Color.White));
        //        }
        //        int handleLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        //        Labels labels = axMap1.get_DrawingLabels(handleLayer);
        //        labels.FontSize = 12;
        //        //labels.FrameBackColor = ColorToUint(Color.Transparent);
        //        //labels.FontColor = ColorToUint(Color.White);
        //        //LabelCategory cat = labels.AddCategory("White");
        //        //cat.FontColor = ColorToUint(Color.White);
        //        labels.AddLabel("Góc 1", longtMin + (longtMax - longtMin) / 4 * 3, lattMin + (lattMax - lattMin) / 4 * 3);
        //        labels.AddLabel("Góc 2", longtMin + (longtMax - longtMin) / 4, lattMin + (lattMax - lattMin) / 4 * 3);
        //        labels.AddLabel("Góc 3", longtMin + (longtMax - longtMin) / 4, lattMin + (lattMax - lattMin) / 4);
        //        labels.AddLabel("Góc 4", longtMin + (longtMax - longtMin) / 4 * 3, lattMin + (lattMax - lattMin) / 4);
        //        axMap1.set_DrawingLabels(handleLayer, labels);
        //    }
        //}

        //private void LoadMachineBomCodePoint()
        //{
        //    using (StreamReader r = new StreamReader(@"C:\temp\dataRPBM.json"))
        //    {
        //        string json = r.ReadToEnd();
        //        DataRPBM data = JsonConvert.DeserializeObject<DataRPBM>(json);
        //        Random rand = new Random(DateTime.Now.Millisecond);
        //        axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
        //        foreach (Models.MachineBomCodePoint machineBomCodePoint in data.MachineBomPointDatas)
        //        {
        //            if (!machine__color.ContainsKey(machineBomCodePoint.CodeMachineBom))
        //            {
        //                machine__color.Add(machineBomCodePoint.CodeMachineBom, Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256)));
        //            }
        //            foreach (Models.DatabaseInfo info in machineBomCodePoint.DatabasePointInfo)
        //            {
        //                try
        //                {
        //                    double longt = 0;
        //                    double latt = 0;
        //                    ToLatLon(info.lat_value, info.long_value, ref latt, ref longt, "48N");
        //                    if (CheckCamCo(info.bit_sens, out bool isButton1Press))
        //                    {
        //                        addFlag(longt, latt);
        //                    }
        //                    else
        //                    {
        //                        axMap1.DrawPoint(longt, latt, 2, ColorToUint(machine__color[machineBomCodePoint.CodeMachineBom]));
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    continue;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void LoadDoSau()
        //{
        //    using (StreamReader r = new StreamReader(@"C:\temp\dataRPBM.json"))
        //    {
        //        string json = r.ReadToEnd();
        //        DataRPBM data = JsonConvert.DeserializeObject<DataRPBM>(json);
        //        Random rand = new Random(DateTime.Now.Millisecond);
        //        foreach (Models.MineDoSau mineDoSau in data.MineDoSau)
        //        {
        //            try
        //            {
        //                double longt = 0;
        //                double latt = 0;
        //                ToLatLon(mineDoSau.xPoint, mineDoSau.yPoint, ref latt, ref longt, "48N");
        //                addDeep(longt, latt);
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //    }
        //}

        public static void ToLatLon(double utmX, double utmY, ref double latt, ref double longt, string utmZone)
        {
            bool isNorthHemisphere = utmZone.Last() >= 'N';

            var zone = int.Parse(utmZone.Remove(utmZone.Length - 1));
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (6366197.724 * 0.9996); // Change c_sa for 6366197.724
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = (5 * j4 + a2 * Math.Pow((Math.Cos(lat)), 2)) / 3.0; // saque a2 de multiplicar por el coseno de lat y elevar al cuadrado
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longt = ((delt / Math.PI) * 180 + s);
            latt = ((((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat))) / Math.PI) * 180); // era incorrecto el calculo
        }

        // <summary>
        // Loads the layers and registers event handlers
        // </summary>
        public void InitSuspectPointLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //MessageBox.Show("axMap1.MoveLayerTop(suspectPointLayer): " + axMap1.MoveLayerTop(suspectPointLayer));
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitSuspectPointMineLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //MessageBox.Show("axMap1.MoveLayerTop(suspectPointLayer): " + axMap1.MoveLayerTop(suspectPointLayer));
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitDistancePointLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //MessageBox.Show("axMap1.MoveLayerTop(markerDistanceLayer): " + axMap1.MoveLayerTop(markerDistanceLayer));
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
                MessageBox.Show("InitHighlightLayer()");
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

        public void InitHighlightCurrentPointLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitHighlightCurrentPointModelLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitGreenFlagLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitMachineLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitFlagLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitFlagRealTimeLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        public void InitDeepLayer()
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
            options.Picture = this.OpenImage(pathpng);
            options.AlignPictureByBottom = false;
            sf.CollisionMode = tkCollisionMode.AllowCollisions;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent;   // change MapEvents to axMap1
        }

        // <summary>
        // Opens a marker from the file
        // </summary>
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

        // <summary>
        // Handles mouse down event and adds the marker
        // </summary>
        public void addSuspectPoint(double x, double y)
        {
            Shapefile sf = axMap1.get_Shapefile(suspectPointLayer);
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
                MessageBox.Show("addSuspectPoint()");
                return;
            }
            axMap1.Redraw();
        }

        public void addSuspectPointMine(double x, double y)
        {
            Shapefile sf = axMap1.get_Shapefile(suspectPointLayerMine);
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
                MessageBox.Show("addSuspectPointMine()");
                return;
            }
            axMap1.Redraw();
        }

        public void addMachinePoint(double x, double y, string codeMachine, DateTime timeAction, bool isRedraw = true)
        {
            try
            {
                if (!machine__label.ContainsKey(codeMachine))
                    return;

                getColorForMachine(codeMachine);
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
                //if (!machineActive__highlightPoint.ContainsKey(codeMachine))
                //{
                //    addHighlightCurrentPoint(x, y, codeMachine, isRedraw);
                //}
                //else
                //{
                //    Point highlightPoint = machineActive__highlightPoint[codeMachine];
                //    highlightPoint.Set(x, y);
                //}
                //if (!machineActive__highlightLayer.ContainsKey(codeMachine))
                //{
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer.Add(codeMachine, layer);
                //}
                //else
                //{
                //    axMap1.ClearDrawing(machineActive__highlightLayer[codeMachine]);
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer[codeMachine] = layer;
                //}

                if (isRedraw)
                {
                    axMap1.Redraw();
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        private void addMachineLine(double x, double y, string codeMachine, bool isNewLine)
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

            Point pnt = new Point();
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            //axMap1.Redraw();
        }

        public void addMachinePointModel(double x, double y, string codeMachine, DateTime timeAction, bool isRedraw = true)
        {
            try
            {
                if (!machine__label.ContainsKey(codeMachine))
                    return;
                //máy online hay offline
                //if (!machineActive__color.ContainsKey(codeMachine))
                //{
                //    machineActive__color.Add(codeMachine, activeMachineColor[machineActive__color.Count % activeMachineColor.Length]);
                //    machine__label[codeMachine].setForeColor(machineActive__color[codeMachine]);
                //}
                //getLayerPointForMachines(codeMachine);
                getColorForMachine(codeMachine);
                Shapefile sf = axMap1.get_Shapefile(machineActive__pointModelLayer[codeMachine]);
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
                    MessageBox.Show("addMachinePointModel()");
                    return;
                }
                //if (!machine__shapeIndex_time.ContainsKey(codeMachine))
                //{
                //    machine__shapeIndex_time.Add(codeMachine, new Dictionary<int, DateTime>());
                //}
                //machine__shapeIndex_time[codeMachine].Add(index, timeAction);
                if (!machineActive__lastTimeModel.ContainsKey(codeMachine))
                {
                    machineActive__lastTimeModel.Add(codeMachine, DateTime.MinValue);
                }

                double recentDistance = 0;
                if (!machineActive__lastModelPoint.ContainsKey(codeMachine))
                {
                    machineActive__lastModelPoint.Add(codeMachine, pnt);
                }
                else
                {
                    Point recentPoint = machineActive__lastModelPoint[codeMachine];
                    machineActive__lastModelPoint[codeMachine] = pnt;
                    recentDistance = AppUtils.DistanceLatLong(recentPoint.y, recentPoint.x, y, x);
                    //if (!machineActive__lineModelLayer.ContainsKey(codeMachine))
                    //{
                    //    int lineLayerNew = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                    //    if (btnShowPoint.Text == "Hiện điểm dò được" || btnModel.Text == "Bật nắn điểm")
                    //    {
                    //        axMap1.SetDrawingLayerVisible(lineLayerNew, false);
                    //    }
                    //    machineActive__lineModelLayer.Add(codeMachine, lineLayerNew);
                    //}
                    //int lineLayer = machineActive__lineModelLayer[codeMachine];
                    //axMap1.DrawLineEx(lineLayer, recentPoint.x, recentPoint.y, x, y, 2, ColorToUint(machineActive__color[codeMachine]));
                }
                if ((timeAction - machineActive__lastTimeModel[codeMachine]).TotalSeconds <= MIN_TIME_NEW_LINE && recentDistance < MIN_DISTANCE_NEW_LINE)
                {
                    addMachineLineModel(x, y, codeMachine, false);
                }
                else
                {
                    addMachineLineModel(x, y, codeMachine, true);
                }
                machineActive__lastTimeModel[codeMachine] = timeAction;
                //if (!machineActive__highlightPoint.ContainsKey(codeMachine))
                //{
                //    addHighlightCurrentPoint(x, y, codeMachine);
                //}
                //else
                //{
                //    Point highlightPoint = machineActive__highlightPoint[codeMachine];
                //    highlightPoint.Set(x, y);
                //}
                //if (!machineActive__highlightLayer.ContainsKey(codeMachine))
                //{
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer.Add(codeMachine, layer);
                //}
                //else
                //{
                //    axMap1.ClearDrawing(machineActive__highlightLayer[codeMachine]);
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer[codeMachine] = layer;
                //}

                if (isRedraw)
                {
                    axMap1.Redraw();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void addMachineLineModel(double x, double y, string codeMachine, bool isNewLine)
        {
            //getLayerLineForMachines(codeMachine);
            Shapefile sf = axMap1.get_Shapefile(machineActive__lineModelLayer[codeMachine]);
            //Shape shp = sf.Shape[0];
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

            Point pnt = new Point();
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            //axMap1.Redraw();
        }

        public void addMachinePointModelHistory(double x, double y, string codeMachine, DateTime timeAction, bool isRedraw = true)
        {
            try
            {
                if (!machine__label.ContainsKey(codeMachine))
                    return;
                //máy online hay offline
                //if (!machineActive__color.ContainsKey(codeMachine))
                //{
                //    machineActive__color.Add(codeMachine, activeMachineColor[machineActive__color.Count % activeMachineColor.Length]);
                //    machine__label[codeMachine].setForeColor(machineActive__color[codeMachine]);
                //}
                //getLayerPointForMachines(codeMachine);
                getColorForMachine(codeMachine);
                Shapefile sf = axMap1.get_Shapefile(machineActive__pointModelHistoryLayer[codeMachine]);
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
                    MessageBox.Show("addMachinePointModelHistory()");
                    return;
                }
                //if (!machine__shapeIndex_time.ContainsKey(codeMachine))
                //{
                //    machine__shapeIndex_time.Add(codeMachine, new Dictionary<int, DateTime>());
                //}
                //machine__shapeIndex_time[codeMachine].Add(index, timeAction);
                if (!machineActive__lastTimeModelHistory.ContainsKey(codeMachine))
                {
                    machineActive__lastTimeModelHistory.Add(codeMachine, DateTime.MinValue);
                }

                double recentDistance = 0;
                if (!machineActive__lastModelPointHistory.ContainsKey(codeMachine))
                {
                    machineActive__lastModelPointHistory.Add(codeMachine, pnt);
                }
                else
                {
                    Point recentPoint = machineActive__lastModelPointHistory[codeMachine];
                    machineActive__lastModelPointHistory[codeMachine] = pnt;
                    recentDistance = AppUtils.DistanceLatLong(recentPoint.y, recentPoint.x, y, x);
                    //if (!machineActive__lineModelLayer.ContainsKey(codeMachine))
                    //{
                    //    int lineLayerNew = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                    //    if (btnShowPoint.Text == "Hiện điểm dò được" || btnModel.Text == "Bật nắn điểm")
                    //    {
                    //        axMap1.SetDrawingLayerVisible(lineLayerNew, false);
                    //    }
                    //    machineActive__lineModelLayer.Add(codeMachine, lineLayerNew);
                    //}
                    //int lineLayer = machineActive__lineModelLayer[codeMachine];
                    //axMap1.DrawLineEx(lineLayer, recentPoint.x, recentPoint.y, x, y, 2, ColorToUint(machineActive__color[codeMachine]));
                }
                if ((timeAction - machineActive__lastTimeModelHistory[codeMachine]).TotalSeconds <= MIN_TIME_NEW_LINE && recentDistance < MIN_DISTANCE_NEW_LINE)
                {
                    addMachineLineModelHistory(x, y, codeMachine, false);
                }
                else
                {
                    addMachineLineModelHistory(x, y, codeMachine, true);
                }
                machineActive__lastTimeModelHistory[codeMachine] = timeAction;
                //if (!machineActive__highlightPoint.ContainsKey(codeMachine))
                //{
                //    addHighlightCurrentPoint(x, y, codeMachine);
                //}
                //else
                //{
                //    Point highlightPoint = machineActive__highlightPoint[codeMachine];
                //    highlightPoint.Set(x, y);
                //}
                //if (!machineActive__highlightLayer.ContainsKey(codeMachine))
                //{
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer.Add(codeMachine, layer);
                //}
                //else
                //{
                //    axMap1.ClearDrawing(machineActive__highlightLayer[codeMachine]);
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer[codeMachine] = layer;
                //}

                if (isRedraw)
                {
                    axMap1.Redraw();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void addMachineLineModelHistory(double x, double y, string codeMachine, bool isNewLine)
        {
            //getLayerLineForMachines(codeMachine);
            Shapefile sf = axMap1.get_Shapefile(machineActive__lineModelHistoryLayer[codeMachine]);
            //Shape shp = sf.Shape[0];
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

            Point pnt = new Point();
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            //axMap1.Redraw();
        }

        public void addMachinePointRealTime(double x, double y, string codeMachine, DateTime timeAction, bool isRedraw = true)
        {
            try
            {
                if (!machine__label.ContainsKey(codeMachine))
                    return;
                //máy online hay offline
                //if (!machineActive__color.ContainsKey(codeMachine))
                //{
                //    machineActive__color.Add(codeMachine, activeMachineColor[machineActive__color.Count % activeMachineColor.Length]);
                //    machine__label[codeMachine].setForeColor(machineActive__color[codeMachine]);
                //}
                
                //getLayerPointForMachines(codeMachine);
                getColorForMachine(codeMachine);
                machine__label[codeMachine].setActive(true);
                Shapefile sf = axMap1.get_Shapefile(machineActive__pointRealTimeLayer[codeMachine]);
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
                    MessageBox.Show("addMachinePointRealTime()");
                    return;
                }
                //if (!machine__shapeIndex_time.ContainsKey(codeMachine))
                //{
                //    machine__shapeIndex_time.Add(codeMachine, new Dictionary<int, DateTime>());
                //}
                //machine__shapeIndex_time[codeMachine].Add(index, time);
                if (!machineActive__lastRealTime.ContainsKey(codeMachine))
                {
                    machineActive__lastRealTime.Add(codeMachine, DateTime.MinValue);
                }

                double recentDistance = 0;
                if (!machineActive__lastPointRealTime.ContainsKey(codeMachine))
                {
                    machineActive__lastPointRealTime.Add(codeMachine, pnt);
                    //machineActive__lastTime[codeMachine] = timeAction;
                }
                else
                {
                    Point recentPoint = machineActive__lastPointRealTime[codeMachine];
                    machineActive__lastPointRealTime[codeMachine] = pnt;
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
                if ((timeAction - machineActive__lastRealTime[codeMachine]).TotalSeconds <= MIN_TIME_NEW_LINE && recentDistance < MIN_DISTANCE_NEW_LINE)
                {
                    addMachineLineRealTime(x, y, codeMachine, false);
                }
                else
                {
                    addMachineLineRealTime(x, y, codeMachine, true);
                }
                machineActive__lastRealTime[codeMachine] = timeAction;
                if (!machineActive__highlightPoint.ContainsKey(codeMachine))
                {
                    addHighlightCurrentPoint(x, y, codeMachine, isRedraw);
                }
                else
                {
                    Point highlightPoint = machineActive__highlightPoint[codeMachine];
                    highlightPoint.Set(x, y);
                }
                //if (!machineActive__highlightLayer.ContainsKey(codeMachine))
                //{
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer.Add(codeMachine, layer);
                //}
                //else
                //{
                //    axMap1.ClearDrawing(machineActive__highlightLayer[codeMachine]);
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer[codeMachine] = layer;
                //}

                if (isRedraw)
                {
                    axMap1.Redraw();
                }
            }
            catch (Exception ex)
            {
                
                //MessageBox.Show(ex.StackTrace);
            }

        }

        private void addMachineLineRealTime(double x, double y, string codeMachine, bool isNewLine)
        {
            //getLayerLineForMachines(codeMachine);

            Shapefile sf = axMap1.get_Shapefile(machineActive__lineRealTimeLayer[codeMachine]);
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

            Point pnt = new Point();
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            //axMap1.Redraw();
        }

        public void addMachinePointRealTimeModel(double x, double y, string codeMachine, DateTime timeAction, bool isRedraw = true)
        {
            try
            {
                if (!machine__label.ContainsKey(codeMachine))
                    return;
                //máy online hay offline
                //if (!machineActive__color.ContainsKey(codeMachine))
                //{
                //    machineActive__color.Add(codeMachine, activeMachineColor[machineActive__color.Count % activeMachineColor.Length]);
                //    machine__label[codeMachine].setForeColor(machineActive__color[codeMachine]);
                //}
                //getLayerPointForMachines(codeMachine);
                getColorForMachine(codeMachine);
                Shapefile sf = axMap1.get_Shapefile(machineActive__pointRealTimeModelLayer[codeMachine]);
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
                    MessageBox.Show("addMachinePointRealTimeModel()");
                    return;
                }
                //if (!machine__shapeIndex_time.ContainsKey(codeMachine))
                //{
                //    machine__shapeIndex_time.Add(codeMachine, new Dictionary<int, DateTime>());
                //}
                //machine__shapeIndex_time[codeMachine].Add(index, time);
                if (!machineActive__lastRealTimeModel.ContainsKey(codeMachine))
                {
                    machineActive__lastRealTimeModel.Add(codeMachine, DateTime.MinValue);
                }

                double recentDistance = 0;
                if (!machineActive__lastModelPointRealTime.ContainsKey(codeMachine))
                {
                    machineActive__lastModelPointRealTime.Add(codeMachine, pnt);
                    //machineActive__lastTime[codeMachine] = timeAction;
                }
                else
                {
                    Point recentPoint = machineActive__lastModelPointRealTime[codeMachine];
                    machineActive__lastModelPointRealTime[codeMachine] = pnt;
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

                if ((timeAction - machineActive__lastRealTimeModel[codeMachine]).TotalSeconds <= MIN_TIME_NEW_LINE && recentDistance < MIN_DISTANCE_NEW_LINE)
                {
                    addMachineLineRealTimeModel(x, y, codeMachine, false);
                }
                else
                {
                    addMachineLineRealTimeModel(x, y, codeMachine, true);
                }
                machineActive__lastRealTimeModel[codeMachine] = timeAction;
                if (!machineActive__highlightPointModel.ContainsKey(codeMachine))
                {
                    addHighlightCurrentPointModel(x, y, codeMachine, isRedraw);
                }
                else
                {
                    Point highlightPoint = machineActive__highlightPointModel[codeMachine];
                    highlightPoint.Set(x, y);
                }
                //if (!machineActive__highlightLayer.ContainsKey(codeMachine))
                //{
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer.Add(codeMachine, layer);
                //}
                //else
                //{
                //    axMap1.ClearDrawing(machineActive__highlightLayer[codeMachine]);
                //    int layer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //    axMap1.DrawCircleEx(layer, x, y, 12, ColorToUint(Color.Black), false);
                //    machineActive__highlightLayer[codeMachine] = layer;
                //}

                if (isRedraw)
                {
                    axMap1.Redraw();
                }
            }
            catch (Exception ex)
            {
                
                //MessageBox.Show(ex.StackTrace);
            }

        }

        private void addMachineLineRealTimeModel(double x, double y, string codeMachine, bool isNewLine)
        {
            //getLayerLineForMachines(codeMachine);

            Shapefile sf = axMap1.get_Shapefile(machineActive__lineRealTimeModelLayer[codeMachine]);
            Shape shp;
            if (isNewLine || sf.NumShapes == 0)
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

            Point pnt = new Point();
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            //axMap1.Redraw();
        }

        private void getLayerPointForMachines(string codeMachine)
        {
            //Nhằm đồng bộ màu điểm thì gán layer vào cả 2 collection điểm bình thường và điểm nắn
            if (!machineActive__pointLayer.ContainsKey(codeMachine))
            {
                machineActive__pointLayer.Add(codeMachine, machinePointLayers[machineActive__pointLayer.Count % machinePointLayers.Count]);
            }
            if (!machineActive__pointModelLayer.ContainsKey(codeMachine))
            {
                machineActive__pointModelLayer.Add(codeMachine, machinePointModelLayers[machineActive__pointModelLayer.Count % machinePointModelLayers.Count]);
            }
            if (!machineActive__pointModelHistoryLayer.ContainsKey(codeMachine))
            {
                machineActive__pointModelHistoryLayer.Add(codeMachine, machinePointModelHistoryLayers[machineActive__pointModelHistoryLayer.Count % machinePointModelHistoryLayers.Count]);
            }
            if (!machineActive__pointRealTimeLayer.ContainsKey(codeMachine))
            {
                machineActive__pointRealTimeLayer.Add(codeMachine, machinePointRealTimeLayers[machineActive__pointRealTimeLayer.Count % machinePointRealTimeLayers.Count]);
            }
            if (!machineActive__pointRealTimeModelLayer.ContainsKey(codeMachine))
            {
                machineActive__pointRealTimeModelLayer.Add(codeMachine, machinePointRealTimeModelLayers[machineActive__pointRealTimeModelLayer.Count % machinePointRealTimeModelLayers.Count]);
            }
        }

        private void getLayerLineForMachines(string codeMachine)
        {
            if (!machineActive__lineLayer.ContainsKey(codeMachine))
            {
                int lineLayer = machineLineLayers[machineActive__lineLayer.Count % machineLineLayers.Count];
                machineActive__lineLayer.Add(codeMachine, lineLayer);
            }
            if (!machineActive__lineModelLayer.ContainsKey(codeMachine))
            {
                int lineLayer = machineLineModelLayers[machineActive__lineModelLayer.Count % machineLineModelLayers.Count];
                machineActive__lineModelLayer.Add(codeMachine, lineLayer);
            }
            if (!machineActive__lineModelHistoryLayer.ContainsKey(codeMachine))
            {
                int lineLayer = machineLineModelHistoryLayers[machineActive__lineModelHistoryLayer.Count % machineLineModelHistoryLayers.Count];
                machineActive__lineModelHistoryLayer.Add(codeMachine, lineLayer);
            }
            if (!machineActive__lineRealTimeLayer.ContainsKey(codeMachine))
            {
                int lineLayer = machineLineRealTimeLayers[machineActive__lineRealTimeLayer.Count % machineLineRealTimeLayers.Count];
                machineActive__lineRealTimeLayer.Add(codeMachine, lineLayer);
            }
            if (!machineActive__lineRealTimeModelLayer.ContainsKey(codeMachine))
            {
                int lineLayer = machineLineRealTimeModelLayers[machineActive__lineRealTimeModelLayer.Count % machineLineRealTimeModelLayers.Count];
                machineActive__lineRealTimeModelLayer.Add(codeMachine, lineLayer);
            }
        }

        private void getColorForMachine(string codeMachine)
        {
            //máy online hay offline
            if (!machineActive__color.ContainsKey(codeMachine))
            {
                machineActive__color.Add(codeMachine, activeMachineColor[machineActive__color.Count % activeMachineColor.Length]);
                machine__label[codeMachine].setForeColor(machineActive__color[codeMachine]);
            }
            getLayerPointForMachines(codeMachine);
            getLayerLineForMachines(codeMachine);
        }

        public void addMarkerDistancePoint(double x, double y)
        {
            Shapefile sf = axMap1.get_Shapefile(markerDistanceLayer);
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
                MessageBox.Show("addMarkerDistancePoint()");
                return;
            }
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
                MessageBox.Show("addHighlight()");
                return;
            }
            axMap1.Redraw();
        }

        public void addHighlightCurrentPoint(double x, double y, string machineCode, bool isRedraw)
        {
            Shapefile sf = axMap1.get_Shapefile(highlightCurrentPointLayer);
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
                MessageBox.Show("addHighlightCurrentPoint()");
                return;
            }
            machineActive__highlightPoint.Add(machineCode, pnt);
            if (isRedraw)
            {
                axMap1.Redraw();
            }
        }

        public void addHighlightCurrentPointModel(double x, double y, string machineCode, bool isRedraw)
        {
            Shapefile sf = axMap1.get_Shapefile(highlightCurrentPointModelLayer);
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
                MessageBox.Show("addHighlightCurrentPointModel()");
                return;
            }
            machineActive__highlightPointModel.Add(machineCode, pnt);
            if (isRedraw)
            {
                axMap1.Redraw();
            }
        }

        public void addFlag(double x, double y, double depth = -1)
        {
            Shapefile sf = axMap1.get_Shapefile(flagLayer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POINT);
            Point pnt = new Point();
            //axMap1.PixelToProj(x, y, ref x, ref y);
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            index = sf.NumShapes;
            if (depth >= 0)
            {
                sf.Labels.AddLabel(depth.ToString(), x, y);
                sf.Labels.OffsetX = 20;
                sf.Labels.OffsetY = 20;
                sf.Labels.Alignment = tkLabelAlignment.laBottomRight;
            }
            //if (!sf.EditInsertShape(shp, ref index))
            if (sf.EditAddShape(shp) < 0)
            {
                MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("addFlag()");
                return;
            }
            axMap1.Redraw();
        }

        public void addFlagRealTime(double x, double y, double depth = -1)
        {
            try
            {
                //axMap1.set_LayerVisible(flagRealTimeLayer, true);
                Shapefile sf = axMap1.get_Shapefile(flagRealTimeLayer);
                Shape shp = new Shape();
                shp.Create(ShpfileType.SHP_POINT);
                Point pnt = new Point();
                //axMap1.PixelToProj(x, y, ref x, ref y);
                pnt.x = x;
                pnt.y = y;
                int index = shp.numPoints;
                shp.InsertPoint(pnt, ref index);
                index = sf.NumShapes;
                if (depth >= 0)
                {
                    sf.Labels.AddLabel(depth.ToString(), x, y);
                    sf.Labels.OffsetX = 20;
                    sf.Labels.OffsetY = 20;
                    sf.Labels.Alignment = tkLabelAlignment.laBottomRight;
                }
                //if (!sf.EditInsertShape(shp, ref index))
                if (sf.EditAddShape(shp) < 0)
                {
                    MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                    MessageBox.Show("addFlagRealTime()");
                    return;
                }
                axMap1.Redraw();
            }
            catch(Exception ex)
            {
                
            }
        }

        //private int tooltipLayer = -1;

        private AxMapWinGIS._DMapEvents_MouseMoveEventHandler mouseMoveHandler;

        public void addMachineNote(double x, double y, string codeMachine)
        {
            axMap1.ClearDrawingLabels(labelLayer);
            Labels labels = new Labels();
            labels.FontSize = 12;
            //labels.FrameBackColor = ColorToUint(Color.Transparent);
            //labels.FontColor = ColorToUint(Color.White);
            //LabelCategory cat = labels.AddCategory("White");
            //cat.FontColor = ColorToUint(Color.White);
            string labelText = string.Format(
                "Mã máy: {0}\n" +
                "Kinh độ: {1}\n" +
                "Vĩ độ: {2}",
                codeMachine, x, y
            );
            labels.AddLabel(labelText, x, y);
            labels.Visible = true;

            axMap1.MouseMoveEvent -= mouseMoveHandler;
            mouseMoveHandler = new AxMapWinGIS._DMapEvents_MouseMoveEventHandler((sender, e) =>
            {
                double pxX = 0;
                double pxY = 0;
                axMap1.ProjToPixel(x, y, ref pxX, ref pxY);
                if (Math.Abs(e.x - pxX) < 50 && Math.Abs(e.y - pxY) < 50)
                {
                    //axMap1.ShowToolTip("12345", 5000);
                    //labels.Visible = true;
                    //axMap1.Redraw();
                }
                else
                {
                    labels.Visible = false;
                    //axMap1.Redraw();
                    //axMap1.ClearDrawingLabels(labelLayer);
                }
            });
            axMap1.MouseMoveEvent += mouseMoveHandler;
            axMap1.set_DrawingLabels(labelLayer, labels);
            //axMap1.MouseMoveEvent += (sender, e) =>
            //{
            //};
            //axMap1.set_DrawingLabels(tooltipLayer, labels);
            //axMap1.Redraw();
        }

        public void addGreenFlag(double x, double y)
        {
            Shapefile sf = axMap1.get_Shapefile(greenFlagLayer);
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
                MessageBox.Show("addGreenFlag()");
                return;
            }
            axMap1.Redraw();
        }

        public void addDeep(double x, double y)
        {
            Shapefile sf = axMap1.get_Shapefile(deepLayer);
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POINT);
            Point pnt = new Point();
            //axMap1.PixelToProj(x, y, ref x, ref y);
            pnt.x = x;
            pnt.y = y;
            pnt.Z = double.PositiveInfinity;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);
            index = sf.NumShapes;
            if (!sf.EditInsertShape(shp, ref index))
            {
                MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                MessageBox.Show("addDeep()");
                return;
            }
            axMap1.Redraw();
        }

        public void addImage(double x, double y)
        {
            Shapefile sf = axMap1.get_Shapefile(imageLayer);
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
                MessageBox.Show("addImage()");
                return;
            }
            axMap1.Redraw();
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            //CreatePointShapefile();
        }

        private void InitWhiteLayer()
        {
            double xmin = -180;
            double xmax = 180;
            double ymin = -90;
            double ymax = 90;

            string pathpng = @"../../data/white bg.png";

            MapWinGIS.Image img = OpenImage(pathpng);
            img.OriginalDX = (xmax - xmin) / img.OriginalWidth;
            img.OriginalDY = (ymax - ymin) / img.OriginalHeight;
            img.OriginalXllCenter = xmin;
            img.OriginalYllCenter = ymin;

            bgLayer = axMap1.AddLayer(img, true);

            axMap1.Redraw();

            //axMap1.SetLatitudeLongitude(17.261828, 106.828998);
        }

        private void InitImageLayer()
        {
            double xmin = 106.828998;
            double xmax = 106.844626;
            double ymin = 17.261828;
            double ymax = 17.27483;

            //double xminUTM = 1909441.791;
            //double xmaxUTM = 1910895.457;
            //double yminUTM = 696088.270;
            //double ymaxUTM = 694439.028;

            var pathpng = AppUtils.GetAppDataPath();
            pathpng = System.IO.Path.Combine(pathpng, "11111111.png");

            MapWinGIS.Image img = OpenImage(pathpng);
            img.OriginalDX = (xmax - xmin) / img.OriginalWidth;
            img.OriginalDY = (ymax - ymin) / img.OriginalHeight;
            //MessageBox.Show("width = " + img.OriginalDX + " / height = " + img.OriginalDY);
            //img.ProjectionToImage(xmin, ymin, out int x, out int y);
            //img.SetVisibleExtents(xmin, ymin, xmax, ymax, 500, 10);
            img.OriginalXllCenter = xmin;
            img.OriginalYllCenter = ymin;

            imageLayer = axMap1.AddLayer(img, true);

            axMap1.Redraw();

            //axMap1.SetLatitudeLongitude(17.261828, 106.828998);
        }

        private void loadImage(string path, double xmax, double xmin, double ymax, double ymin)
        {
            Image img = axMap1.get_Image(imageLayer);
            img.Open(path, ImageType.USE_FILE_EXTENSION);
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

        private int count_mess = 0;

        private void phanTichTest()
        {
            bool valid = true;
            if (cb50x50.InvokeRequired)
            {
                cb50x50.Invoke(new MethodInvoker(delegate
                {
                    if (cb50x50.SelectedValue != null && (long)cb50x50.SelectedValue <= 0)
                    {
                        valid = false;
                    }
                }));
            }
            else
            {
                if (cb50x50.SelectedValue != null && (long)cb50x50.SelectedValue <= 0)
                {
                    valid = false;
                }
            }
            if (!valid)
            {
                return;
            }
            callSocketPhanTich();
            callSocketNanDiem();
            ptTimeout = new CustomTimeout(() =>
            {
                ptCount = 0;
                phanTichTest();
            }, PTTIMEOUT * 1000);
        }

        //private delegate void dlgAddItem(KQPhanTichControl phanTichControl);
        //private void AddItem(KQPhanTichControl phanTichControl)
        //{
        //    if (pnlKQPTContainer.InvokeRequired)
        //    {
        //        pnlKQPTContainer.Invoke(new dlgAddItem(AddItem));
        //    }
        //    else
        //    {
        //        pnlKQPTContainer.Controls.Add(phanTichControl);
        //    }
        //}

        private void cbKhuVuc_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbKhuVuc.SelectedValue is long)
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql = "SELECT " +
                    "position_lat, position_long, " +
                    "photo_file, " +
                    "left_long, right_long, bottom_lat, top_lat " +
                    "FROM cecm_program_area_map where id = " + cbKhuVuc.SelectedValue;
                sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
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
                Extents extents = new Extents();
                extents.SetBounds(xmin, ymin, 0, xmax, ymax, 0);
                axMap1.Extents = extents;
                string pathTemp = AppUtils.GetFolderTemp(int.Parse(_currentId));
                string fullPath = System.IO.Path.Combine(pathTemp, photoFileName);
                if (File.Exists(fullPath))
                {
                    loadImage(fullPath, xmax, xmin, ymax, ymin);
                }
                else
                {
                    //axMap1.set_Image(imageLayer, null);
                    Image img = axMap1.get_Image(imageLayer);
                    img.Clear();
                }
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
                    "FROM OLuoi where cecm_program_areamap_id = " + cbKhuVuc.SelectedValue;
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
                            axMap1.DrawLineEx(lineLayer, line.start_y, line.start_x, line.end_y, line.end_x, 1, ColorToUint(Color.White));
                            if (index % 2 == 1)
                            {
                                axMap1.DrawLabelEx(lineLayer, index.ToString(), line.start_y, line.start_x, 0);
                            }

                        }

                    }
                }
                DataRow dr2 = datatable3.NewRow();
                dr2["gid"] = -1;
                dr2["o_id"] = "Chưa chọn";
                datatable3.Rows.InsertAt(dr2, 0);
                cb50x50.DataSource = datatable3;
                cb50x50.DisplayMember = "o_id";
                cb50x50.ValueMember = "gid";

            }
        }

        private void cb50x50_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cb50x50.SelectedValue is long)
            {
                lstCenterGlobal.Clear();
                pnlKQPTContainer.Controls.Clear();
                lstDiemDo1.Clear();
                //Shapefile sf = axMap1.get_Shapefile(suspectPointLayer);
                //sf.EditClear();
                //Shapefile sf2 = axMap1.get_Shapefile(flagLayer);
                //sf2.EditClear();

                axMap1.RemoveLayer(suspectPointLayer);
                InitSuspectPointLayer();
                axMap1.RemoveLayer(suspectPointLayerMine);
                InitSuspectPointMineLayer();
                axMap1.RemoveLayer(flagLayer);
                //axMap1.set_LayerVisible(flagRealTimeLayer, false);
                InitFlagLayer();
                axMap1.RemoveLayer(flagRealTimeLayer);
                InitFlagRealTimeLayer();
                axMap1.RemoveLayer(highlightLayer);
                InitHighlightLayer();
                //Shapefile sf = axMap1.get_Shapefile(suspectPointLayer);
                //sf.EditClear();
                //sf = axMap1.get_Shapefile(suspectPointLayerMine);
                //sf.EditClear();
                //sf = axMap1.get_Shapefile(flagLayer);
                //sf.EditClear();
                //axMap1.ClearDrawingLabels(flagLayer);
                //sf = axMap1.get_Shapefile(highlightLayer);
                //sf.EditClear();
                //axMap1.ClearDrawingLabels(labelLayer);

                axMap1.MouseUpEvent -= mouseUpHandler;
                mouseUpHandler = null;

                if ((long)cb50x50.SelectedValue <= 0)
                {
                    return;
                }

                LoadHistory();

                SqlCommandBuilder sqlCommand3 = null;
                SqlDataAdapter sqlAdapter3 = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                string sql =
                    "SELECT " +
                    "lat_corner1, lat_corner2, lat_corner3, lat_corner4, " +
                    "long_corner1, long_corner2, long_corner3, long_corner4, " +
                    "long_center, lat_center, " +
                    "distanceAllGrid " +
                    "FROM OLuoi where gid = " + cb50x50.SelectedValue;
                sqlAdapter3 = new SqlDataAdapter(sql, frmLoggin.sqlCon);
                sqlCommand3 = new SqlCommandBuilder(sqlAdapter3);
                sqlAdapter3.Fill(datatable);
                //cb50x50.DataSource = datatable3;
                //cb50x50.DisplayMember = "gid";
                //cb50x50.ValueMember = "o_id";
                oluoi_boundary.lstRanhDo.Clear();

                

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
                    Coordinate cWGS84min = new Coordinate(minLat, minLong);
                    Coordinate cWGS84max = new Coordinate(maxLat, maxLong);
                    pnlKQPTContainer.Controls.Clear();
                    lstCenterGlobal.Clear();
                    oluoi_boundary.minLat = cWGS84min.UTM.Easting;
                    oluoi_boundary.minLong = cWGS84min.UTM.Northing;
                    oluoi_boundary.maxLat = cWGS84max.UTM.Easting;
                    oluoi_boundary.maxLong = cWGS84max.UTM.Northing;

                    OLuoi oluoi = new OLuoi();
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

                    //Thêm rãnh dò
                    double ranhDoPT = 0.2;
                    if (!string.IsNullOrEmpty(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudRanhDoPT")))
                    {
                        if (double.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudRanhDoPT"), out double ranhDoPT_))
                        {
                            ranhDoPT = ranhDoPT_;
                        }
                    }
                    ChiaMatCatOLuoiData data = new ChiaMatCatOLuoiData();
                    data.gocTuyChon1 = 90;
                    data.isBacNamGoc1 = 3;
                    data.khoangCachChia1 = ranhDoPT;
                    //double[] latlongCenter = AppUtils.ConverLatLongToUTM(double.Parse(dr["lat_center"].ToString()), double.Parse(dr["long_center"].ToString()));
                    //double lat_center = latlongCenter[0];
                    //double long_center = latlongCenter[1];
                    //MgdAcDbVNTerrainRectangle mgdAcDbVNTerrainRectangle = new MgdAcDbVNTerrainRectangle();
                    //mgdAcDbVNTerrainRectangle.Create("OLTemp" + cb50x50.SelectedValue, lat_center, long_center, 0, 25, 3, "OLInfo",
                    //                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second,
                    //                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 60);
                    ChiaMatCatCmd cmd = new ChiaMatCatCmd();
                    List<CecmProgramAreaLineDTO> lines = cmd.DrawLineJigAll(oluoi, data);
                    //foreach (CecmProgramAreaLineDTO line in lines)
                    //{
                    //    CecmProgramAreaLineDTO lineDTO = new CecmProgramAreaLineDTO();
                    //    lineDTO.start_x = line.StartPoint.X;
                    //    lineDTO.start_y = line.StartPoint.Y;
                    //    lineDTO.end_x = line.EndPoint.X;
                    //    lineDTO.end_y = line.EndPoint.Y;
                    //    oluoi_boundary.lstRanhDo.Add(lineDTO);
                    //}
                    oluoi_boundary.lstRanhDo = lines;
                    //Hết thêm rãnh dò

                    if (ptTimeout != null)
                    {
                        ptTimeout.Stop();
                    }
                    //axMap1.SetLatitudeLongitude(minLat, minLong);
                    Extents extents = new Extents();
                    extents.SetBounds(minLong, minLat - 0.0001, 0, maxLong, maxLat + 0.0001, 0);
                    axMap1.Extents = extents;
                    //readMarkers(minLat, minLong, maxLat, maxLong);

                    phanTichTest();

                }
                else
                {
                    oluoi_boundary.minLat = -1;
                    oluoi_boundary.minLong = -1;
                    oluoi_boundary.maxLat = -1;
                    oluoi_boundary.maxLong = -1;

                }
                //if (btnModel.Text == "Bật nắn điểm")
                //{
                //    LoadPointsHistory();
                //}
                //else
                //{
                //    LoadPointsModelHistory();
                //}
                
            }
        }

        private void LoadHistory()
        {
            threadLoadHistory = new Thread(() =>
            {
                Stopwatch swObj = new Stopwatch();
                //swObj.Start();
                long oluoi_id = -1;
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        oluoi_id = (long)cb50x50.SelectedValue;
                        cbTenDuAn.Enabled = false;
                        cbKhuVuc.Enabled = false;
                        cb50x50.Enabled = false;
                    }));
                }
                else
                {
                    oluoi_id = (long)cb50x50.SelectedValue;
                    cbTenDuAn.Enabled = false;
                    cbKhuVuc.Enabled = false;
                    cb50x50.Enabled = false;
                }
                bool modeled = getLastModelTime(oluoi_id) != null;
                LoadPointsHistory();
                if (modeled)
                {
                    LoadPointsModelHistory();
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        cbTenDuAn.Enabled = true;
                        cbKhuVuc.Enabled = true;
                        cb50x50.Enabled = true;
                    }));
                }
                else
                {
                    cbTenDuAn.Enabled = true;
                    cbKhuVuc.Enabled = true;
                    cb50x50.Enabled = true;
                }
                //swObj.Stop();
                //MessageBox.Show("Load lịch sử: " + swObj.Elapsed.TotalSeconds + "s");
            });
            threadLoadHistory.IsBackground = true;
            threadLoadHistory.Start();
        }

        private void btnDistance_Click(object sender, EventArgs e)
        {
            //AxMapWinGIS._DMapEvents_MouseMoveEventHandler toolTipEvent = null;
            //toolTipEvent = new AxMapWinGIS._DMapEvents_MouseMoveEventHandler((sender3, e3) =>
            //{

            //});
            //AxMapWinGIS._DMapEvents_MouseUpEventHandler handlerSelectFirstPoint = null;
            //handlerSelectFirstPoint = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((sender3, e3) =>
            //{
            //    double projX = 0;
            //    double projY = 0;
            //    axMap1.PixelToProj(e3.x, e3.y, ref projX, ref projY);
            //    //MessageBox.Show(
            //    //    "e.button: " + e3.button.ToString() + "\n" +
            //    //    "projX: " + projX + "\n" +
            //    //    "projY: " + projY
            //    //);
            //    AxMapWinGIS._DMapEvents_MouseUpEventHandler handleSelectSecondPoint = null;
            //    handleSelectSecondPoint = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((sender2, e2) =>
            //    {
            //        double projX2 = 0;
            //        double projY2 = 0;
            //        axMap1.PixelToProj(e2.x, e2.y, ref projX2, ref projY2);
            //        double[] utmStart = AppUtils.ConverLatLongToUTM(projY, projX);
            //        double[] utmEnd = AppUtils.ConverLatLongToUTM(projY2, projX2);
            //        double distance = Math.Sqrt(Math.Pow(utmStart[0] - utmEnd[0], 2) + Math.Pow(utmStart[1] - utmEnd[1], 2));
            //        //axMap1.PixelToDegrees(e2.x, e2.y, ref projX2, ref projY2);
            //        MessageBox.Show(
            //            //"e.button: " + e2.button.ToString() + "\n" +
            //            //"projX: " + projX2 + "\n" +
            //            //"projY: " + projY2
            //            "distance: " + distance
            //        );
            //        axMap1.MouseUpEvent -= handleSelectSecondPoint;
            //    });
            //    axMap1.MouseUpEvent -= handlerSelectFirstPoint;
            //    axMap1.MouseUpEvent += handleSelectSecondPoint;

            //});
            //axMap1.MouseUpEvent += handlerSelectFirstPoint;
            //axMap1.UDCursorHandle = 3;
            //axMap1.MapCursor = tkCursor.crsrUserDefined;
            //AxMapWinGIS._DMapEvents_MouseMoveEventHandler firstPointMouseMove = new AxMapWinGIS._DMapEvents_MouseMoveEventHandler((sender3, e3) =>
            //{
            //    double projX_move = 0;
            //    double projY_move = 0;
            //    //axMap1.ClearDrawing(tooltipLayer);
            //    //tooltipLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
            //    axMap1.PixelToProj(e3.x, e3.y, ref projX_move, ref projY_move);
            //    axMap1.ClearDrawingLabels(tooltipLayer);
            //    Labels labels_info = axMap1.get_DrawingLabels(tooltipLayer);
            //    labels_info.FontSize = 12;

            //    labels_info.Visible = true;
            //    labels_info.Alignment = tkLabelAlignment.laTopRight;
            //    labels_info.OffsetX = 20;
            //    labels_info.OffsetY = 20;
            //    labels_info.AutoOffset = true;
            //    //labels_info.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
            //    if (choosingFirstPoint)
            //    {
            //        //axMap1.DrawLabelEx(tooltipLayer, "Chọn điểm 1", projX_move, projX_move, 0);
            //        labels_info.AddLabel("Chọn điểm 1", projX_move, projX_move);
            //    }
            //    else if (choosingSecondPoint)
            //    {
            //        //axMap1.DrawLabelEx(tooltipLayer, "Chọn điểm 2", projX_move, projX_move, 0);
            //        labels_info.AddLabel("Chọn điểm 2", projX_move, projX_move);
            //    }
            //    axMap1.Redraw();

            //});
            toolStrip1.Enabled = false;
            choosingFirstPoint = true;
            AxMapWinGIS._DMapEvents_MouseUpEventHandler handleSelectFirstPoint = null;
            handleSelectFirstPoint = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((senderFirstPoint, eFirstPoint) =>
            {
                if (eFirstPoint.button != 1)
                {
                    return;
                }
                choosingSecondPoint = true;
                choosingFirstPoint = false;
                axMap1.MouseUpEvent -= handleSelectFirstPoint;
                double projX = 0;
                double projY = 0;
                axMap1.PixelToProj(eFirstPoint.x, eFirstPoint.y, ref projX, ref projY);
                //axMap1.ClearDrawing(markerDistanceLayer);
                //markerDistanceLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                //axMap1.DrawPointEx(markerDistanceLayer, projX, projY, 20, ColorToUint(Color.Red));
                addMarkerDistancePoint(projX, projY);
                AxMapWinGIS._DMapEvents_MouseMoveEventHandler secondPointMouseMove = null;
                secondPointMouseMove = new AxMapWinGIS._DMapEvents_MouseMoveEventHandler((sender3, e3) =>
                {
                    axMap1.ClearDrawing(distanceLayer);
                    distanceLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                    double projX_move = 0;
                    double projY_move = 0;
                    axMap1.PixelToProj(e3.x, e3.y, ref projX_move, ref projY_move);
                    axMap1.DrawLineEx(distanceLayer, projX, projY, projX_move, projY_move, 1, ColorToUint(Color.White));
                    //axMap1.DrawLabelEx(distanceLayer, "Nhấn chuột phải để đo khoảng cách", projX_move, projX_move, 0);
                });
                AxMapWinGIS._DMapEvents_MouseUpEventHandler handleSelectSecondPoint = null;
                handleSelectSecondPoint = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((sender2, e2) =>
                {
                    choosingSecondPoint = false;

                    double projX2 = 0;
                    double projY2 = 0;
                    axMap1.PixelToProj(e2.x, e2.y, ref projX2, ref projY2);
                    double[] utmStart = AppUtils.ConverLatLongToUTM(projY, projX);
                    double[] utmEnd = AppUtils.ConverLatLongToUTM(projY2, projX2);
                    //axMap1.DrawPointEx(markerDistanceLayer, projX2, projY2, 20, ColorToUint(Color.Red));
                    addMarkerDistancePoint(projX2, projY2);
                    double horizontal = Math.Abs(utmStart[0] - utmEnd[0]);
                    double vertical = Math.Abs(utmStart[1] - utmEnd[1]);
                    double distance = Math.Sqrt(Math.Pow(horizontal, 2) + Math.Pow(vertical, 2));
                    //axMap1.PixelToDegrees(e2.x, e2.y, ref projX2, ref projY2);
                    MessageBox.Show(
                        "Tọa độ điểm 1: (" + Math.Round(projY, 6) + ", " + Math.Round(projX, 6) + ")\n" +
                        "Tọa độ điểm 2: (" + Math.Round(projY2, 6) + ", " + Math.Round(projX2, 6) + ")\n" +
                        "Khoảng cách: " + Math.Round(distance, 3) + " m\n" +
                        "Chiều ngang: " + Math.Round(horizontal, 3) + " m\n" +
                        "Chiều dọc: " + Math.Round(vertical, 3) + "m",
                        "Thông tin khoảng cách"
                    );
                    axMap1.ClearDrawing(distanceLayer);
                    axMap1.RemoveLayer(markerDistanceLayer);
                    InitDistancePointLayer();
                    //axMap1.ClearDrawingLabels(tooltipLayer);
                    //axMap1.ClearDrawing(tooltipLayer);
                    //tooltipLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                    //markerDistanceLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                    axMap1.MouseUpEvent -= handleSelectSecondPoint;
                    axMap1.MouseMoveEvent -= secondPointMouseMove;
                    //axMap1.MouseMoveEvent -= firstPointMouseMove;
                    toolStrip1.Enabled = true;
                });
                axMap1.MouseUpEvent += handleSelectSecondPoint;
                axMap1.MouseMoveEvent += secondPointMouseMove;
            });
            axMap1.MouseUpEvent += handleSelectFirstPoint;
            //axMap1.MouseMoveEvent += firstPointMouseMove;
            //if (choosingSecondPoint)
            //{
            //    return;
            //}

        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            toolStrip1.Enabled = false;
            choosingFirstPoint = true;
            AxMapWinGIS._DMapEvents_MouseUpEventHandler handleSelectPoint = null;
            handleSelectPoint = new AxMapWinGIS._DMapEvents_MouseUpEventHandler((senderFirstPoint, ePoint) =>
            {
                if (ePoint.button != 1)
                {
                    return;
                }
                double projX = 0;
                double projY = 0;
                axMap1.PixelToProj(ePoint.x, ePoint.y, ref projX, ref projY);
                addMarkerDistancePoint(projX, projY);
                MessageBox.Show(
                    "Kinh độ: " + Math.Round(projX, 6) + "\n" +
                    "Vĩ độ: " + Math.Round(projY, 6),
                    "Thông tin tọa độ"
                );
                //axMap1.ClearDrawing(markerDistanceLayer);
                axMap1.RemoveLayer(markerDistanceLayer);
                InitDistancePointLayer();
                axMap1.MouseUpEvent -= handleSelectPoint;
                toolStrip1.Enabled = true;
            });
            axMap1.MouseUpEvent += handleSelectPoint;
        }

        private void btnShowPoint_Click(object sender, EventArgs e)
        {
            if (btnShowPoint.Text == "Hiện điểm dò được")
            {
                btnShowPoint.Text = "Ẩn điểm dò được";
                btnShowPoint.Image = Resources.hide;
                //axMap1.set_LayerVisible(suspectPointLayer, true);
                //axMap1.set_LayerVisible(flagLayer, true);
                if (btnModel.Text == "Bật nắn điểm")
                {
                    hideShowUnmodel(true);
                }
                else
                {
                    hideShowModel(true);
                }
                btnModel.Enabled = true;
            }
            else
            {
                btnShowPoint.Text = "Hiện điểm dò được";
                btnShowPoint.Image = Resources.show;
                //axMap1.set_LayerVisible(suspectPointLayer, false);
                //axMap1.set_LayerVisible(flagLayer, false);

                //Ẩn hết các layer lịch sử
                //if (btnModel.Text == "Bật nắn điểm")
                //{
                //    foreach (int layer in machinePointLayers)
                //    {
                //        axMap1.set_LayerVisible(layer, false);
                //        //axMap1.SetDrawingLayerVisible(layer, false);
                //    }
                //    //foreach (int layer in machineActive__lineLayer.Values)
                //    //{
                //    //    axMap1.SetDrawingLayerVisible(layer, false);
                //    //}
                //    axMap1.set_LayerVisible(highlightCurrentPointLayer, false);
                //    foreach (int layer in machineLineLayers)
                //    {
                //        axMap1.set_LayerVisible(layer, false);
                //    }
                //}
                //else
                //{
                //    foreach (int layer in machinePointModelLayers)
                //    {
                //        axMap1.set_LayerVisible(layer, false);
                //        //axMap1.SetDrawingLayerVisible(layer, true);
                //    }
                //    //foreach (int layer in machineActive__lineModelLayer.Values)
                //    //{
                //    //    axMap1.SetDrawingLayerVisible(layer, false);
                //    //}
                //    foreach (int layer in machineLineModelLayers)
                //    {
                //        axMap1.set_LayerVisible(layer, false);
                //    }
                //}
                hideShowUnmodel(false);
                hideShowModel(false);
                btnModel.Enabled = false;
            }
        }

        private void btnModel_Click(object sender, EventArgs e)
        {

            //deletePointsWithinTime(DateTime.Parse("2022-08-03T08:44:23.000+00:00"), DateTime.Parse("2022-08-03T10:34:07.000+00:00"));
            //Bật nắn điểm
            if (btnModel.Text == "Bật nắn điểm")
            {
                btnModel.Text = "Tắt nắn điểm";
                btnModel.Image = Resources.model_off;
                //LoadPointsModelHistory();
                hideShowUnmodel(false);
                hideShowModel(true);
            }
            //Tắt nắn điểm
            else
            {
                btnModel.Text = "Bật nắn điểm";
                btnModel.Image = Resources.model_on;
                //LoadPointsHistory();
                hideShowUnmodel(true);
                hideShowModel(false);

            }
        }

        private void hideShowUnmodel(bool show)
        {
            foreach (int layer in machinePointLayers)
            {
                axMap1.set_LayerVisible(layer, show);
                //axMap1.SetDrawingLayerVisible(layer, true);
            }
            //foreach (int layer in machineActive__lineLayer.Values)
            //{
            //    axMap1.SetDrawingLayerVisible(layer, false);
            //}
            foreach (int layer in machineLineLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
            axMap1.set_LayerVisible(highlightCurrentPointLayer, show);
            foreach (int layer in machinePointRealTimeLayers)
            {
                axMap1.set_LayerVisible(layer, show);
                //axMap1.SetDrawingLayerVisible(layer, true);
            }
            foreach (int layer in machineLineRealTimeLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
        }

        private void hideShowModel(bool show)
        {
            foreach (int layer in machinePointModelHistoryLayers)
            {
                axMap1.set_LayerVisible(layer, show);
                //axMap1.SetDrawingLayerVisible(layer, true);
            }
            foreach (int layer in machineLineModelHistoryLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
            foreach (int layer in machinePointModelLayers)
            {
                axMap1.set_LayerVisible(layer, show);
                //axMap1.SetDrawingLayerVisible(layer, true);
            }
            //foreach (int layer in machineActive__lineModelLayer.Values)
            //{
            //    axMap1.SetDrawingLayerVisible(layer, true);
            //}
            foreach (int layer in machineLineModelLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
            axMap1.set_LayerVisible(highlightCurrentPointModelLayer, show);
            foreach (int layer in machinePointRealTimeModelLayers)
            {
                axMap1.set_LayerVisible(layer, show);
                //axMap1.SetDrawingLayerVisible(layer, true);
            }
            foreach (int layer in machineLineRealTimeModelLayers)
            {
                axMap1.set_LayerVisible(layer, show);
            }
        }

        private void deletePointsWithinTime(DateTime start, DateTime end)
        {
            //string codeMachine = "1850525692375d20";
            //foreach (string codeMachine in machine__shapeIndex_time.Keys)
            //{
            //    Dictionary<int, DateTime> shapeIndex_time = machine__shapeIndex_time[codeMachine];
            //    List<KeyValuePair<int, DateTime>> pairsWithinTime = shapeIndex_time.Where(pair => pair.Value > start && pair.Value < end).OrderByDescending(pair => pair.Key).ToList();
            //    int countSuccess = 0;
            //    int countFail = 0;
            //    int countSuccess15 = 0;
            //    int countFail15 = 0;
            //    int countSuccess2 = 0;
            //    int countFail2 = 0;
            //    Shapefile sf = axMap1.get_Shapefile(machineActive__pointModelLayer[codeMachine]);
            //    Shapefile sfLine = axMap1.get_Shapefile(machineActive__lineModelLayer[codeMachine]);
            //    foreach (KeyValuePair<int, DateTime> pair in pairsWithinTime)
            //    {
            //        bool success = sf.Shape[pair.Key].DeletePoint(0);
            //        bool success15 = sf.EditDeleteShape(pair.Key);
            //        int count = sfLine.Shape[0].numPoints;
            //        bool success2 = sfLine.Shape[0].DeletePoint(pair.Key);
            //        //bool success = sf.EditDeleteShape(pair.Key);
            //        if (success)
            //        {
            //            countSuccess++;
            //        }
            //        else
            //        {
            //            countFail++;
            //        }
            //        if (success15)
            //        {
            //            countSuccess15++;
            //        }
            //        else
            //        {
            //            countFail15++;
            //        }
            //        if (success2)
            //        {
            //            countSuccess2++;
            //        }
            //        else
            //        {
            //            countFail2++;
            //        }
            //        shapeIndex_time.Remove(pair.Key);
            //    }
            //}


            ////axMap1.set_Shapefile(machineActive__pointLayer[codeMachine], sf);
            //axMap1.Redraw();
        }

        private void LoadPointsHistory()
        {
            long oluoi_id = -1;
            if (cb50x50.InvokeRequired)
            {
                cb50x50.Invoke(new MethodInvoker(() =>
                {
                    oluoi_id = (long)cb50x50.SelectedValue;
                }));
            }
            else
            {
                oluoi_id = (long)cb50x50.SelectedValue;
            }
            if (!lstOluoiPointLoaded.Contains(oluoi_id))
            {
                lstOluoiPointLoaded.Add(oluoi_id);
                var database = frmLoggin.mgCon.GetDatabase("db_cecm");
                if (database != null)
                {
                    var collection = database.GetCollection<InfoConnect>("cecm_data");
                    long count = collection.Find(
                        doc => doc.lat_value > oluoi_boundary.minLat &&
                        doc.lat_value < oluoi_boundary.maxLat &&
                        doc.long_value > oluoi_boundary.minLong &&
                        doc.long_value < oluoi_boundary.maxLong).CountDocuments();
                    if (count > 0)
                    {
                        //Stopwatch swObj = new Stopwatch();
                        //swObj.Start();
                        //for (int i = 0; i < count; i += CHUNK_SIZE)
                        //{
                        //    lstSkipPoint.Add(i);
                        //    Task.Factory.StartNew(() =>
                        //    {
                        //        var docs = collection.Find(
                        //        doc => doc.lat_value > oluoi_boundary.minLat &&
                        //        doc.lat_value < oluoi_boundary.maxLat &&
                        //        doc.long_value > oluoi_boundary.minLong &&
                        //        doc.long_value > oluoi_boundary.minLong).Skip(lstSkipPoint[indexLstSkip++]).Limit(CHUNK_SIZE).ToList();
                        //        foreach (InfoConnect doc in docs)
                        //        {
                        //            addMachinePoint(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code);
                        //        }
                        //    });
                        //}
                        //Thread thread = new Thread(() =>
                        //{
                        var docs = collection.Find(
                            doc => doc.lat_value > oluoi_boundary.minLat &&
                            doc.lat_value < oluoi_boundary.maxLat &&
                            doc.long_value > oluoi_boundary.minLong &&
                            doc.long_value < oluoi_boundary.maxLong).ToList().OrderBy(doc => doc.time_action);
                        foreach (InfoConnect doc in docs)
                        {
                            addMachinePoint(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code, doc.time_action.ToLocalTime(), false);
                        }
                        axMap1.Redraw();
                        //});
                        //thread.IsBackground = true;
                        //thread.Start();
                        //Task.Factory.StartNew(async () =>
                        //{
                        //    var docs = await collection.Find(
                        //            doc => doc.lat_value > oluoi_boundary.minLat &&
                        //            doc.lat_value < oluoi_boundary.maxLat &&
                        //            doc.long_value > oluoi_boundary.minLong &&
                        //            doc.long_value > oluoi_boundary.minLong).ToListAsync();
                        //    foreach (InfoConnect doc in docs)
                        //    {
                        //        addMachinePoint(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code);
                        //    }
                        //});
                        //swObj.Stop();
                        //MessageBox.Show("Load: " + swObj.Elapsed.TotalSeconds + "s");
                    }


                    //Load điểm nắn
                    //var collection_model = database.GetCollection<InfoConnect>("cecm_data_model");
                    //long count_model = collection_model.Find(
                    //    doc => doc.lat_value > oluoi_boundary.minLat &&
                    //        doc.lat_value < oluoi_boundary.maxLat &&
                    //        doc.long_value > oluoi_boundary.minLong &&
                    //        doc.long_value > oluoi_boundary.minLong).CountDocuments();
                    //if (count_model > 0)
                    //{
                    //    Thread thread = new Thread(() =>
                    //    {
                    //        var docs = collection_model.Find(
                    //                doc => doc.lat_value > oluoi_boundary.minLat &&
                    //                doc.lat_value < oluoi_boundary.maxLat &&
                    //                doc.long_value > oluoi_boundary.minLong &&
                    //                doc.long_value > oluoi_boundary.minLong).ToList();
                    //        foreach (InfoConnect doc in docs)
                    //        {
                    //            addMachinePointModel(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code, doc.time_action.ToLocalTime());
                    //        }
                    //    });
                    //    thread.IsBackground = true;
                    //    thread.Start();
                    //    //Task.Factory.StartNew(async () =>
                    //    //{
                    //    //    var docs = await collection.Find(
                    //    //            doc => doc.lat_value > oluoi_boundary.minLat &&
                    //    //            doc.lat_value < oluoi_boundary.maxLat &&
                    //    //            doc.long_value > oluoi_boundary.minLong &&
                    //    //            doc.long_value > oluoi_boundary.minLong).ToListAsync();
                    //    //    foreach (InfoConnect doc in docs)
                    //    //    {
                    //    //        addMachinePoint(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code);
                    //    //    }
                    //    //});
                    //    //swObj.Stop();
                    //    //MessageBox.Show("Load: " + swObj.Elapsed.TotalSeconds + "s");
                    //}
                }

            }
        }

        private void LoadPointsModelHistory()
        {
            long oluoi_id = -1;
            if (cb50x50.InvokeRequired)
            {
                cb50x50.Invoke(new MethodInvoker(() =>
                {
                    oluoi_id = (long)cb50x50.SelectedValue;
                }));
            }
            else
            {
                oluoi_id = (long)cb50x50.SelectedValue;
            }
            if (!lstOluoiPointModelLoaded.Contains(oluoi_id))
            {
                lstOluoiPointModelLoaded.Add(oluoi_id);
                var database = frmLoggin.mgCon.GetDatabase("db_cecm");
                if (database != null)
                {
                    //Load điểm nắn
                    var collection_model = database.GetCollection<InfoConnect>("cecm_data_model");
                    long count_model = collection_model.Find(
                        doc => doc.lat_value > oluoi_boundary.minLat &&
                            doc.lat_value < oluoi_boundary.maxLat &&
                            doc.long_value > oluoi_boundary.minLong &&
                            doc.long_value < oluoi_boundary.maxLong).CountDocuments();
                    if (count_model > 0)
                    {
                        //Thread thread = new Thread(() =>
                        //{
                        var docs = collection_model.Find(
                            doc => doc.lat_value > oluoi_boundary.minLat &&
                            doc.lat_value < oluoi_boundary.maxLat &&
                            doc.long_value > oluoi_boundary.minLong &&
                            doc.long_value < oluoi_boundary.maxLong).ToList().OrderBy(doc => doc.time_action);
                        foreach (InfoConnect doc in docs)
                        {
                            addMachinePointModelHistory(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code, doc.time_action.ToLocalTime(), false);
                        }
                        axMap1.Redraw();
                        //});
                        //thread.IsBackground = true;
                        //thread.Start();
                        //Task.Factory.StartNew(async () =>
                        //{
                        //    var docs = await collection.Find(
                        //            doc => doc.lat_value > oluoi_boundary.minLat &&
                        //            doc.lat_value < oluoi_boundary.maxLat &&
                        //            doc.long_value > oluoi_boundary.minLong &&
                        //            doc.long_value > oluoi_boundary.minLong).ToListAsync();
                        //    foreach (InfoConnect doc in docs)
                        //    {
                        //        addMachinePoint(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code);
                        //    }
                        //});
                        //swObj.Stop();
                        //MessageBox.Show("Load: " + swObj.Elapsed.TotalSeconds + "s");
                    }
                }

            }
        }

        private void LoadPointsModelHistory(DateTime start, DateTime end)
        {
            var database = frmLoggin.mgCon.GetDatabase("db_cecm");
            if (database != null)
            {
                //Load điểm nắn
                var collection_model = database.GetCollection<InfoConnect>("cecm_data_model");
                long count_model = collection_model.Find(
                    doc => doc.lat_value > oluoi_boundary.minLat &&
                        doc.lat_value < oluoi_boundary.maxLat &&
                        doc.long_value > oluoi_boundary.minLong &&
                        doc.long_value < oluoi_boundary.maxLong &&
                        doc.time_action >= start &&
                        doc.time_action <= end).CountDocuments();
                if (count_model > 0)
                {
                    //Thread thread = new Thread(() =>
                    //{
                    var docs = collection_model.Find(
                            doc => doc.lat_value > oluoi_boundary.minLat &&
                            doc.lat_value < oluoi_boundary.maxLat &&
                            doc.long_value > oluoi_boundary.minLong &&
                            doc.long_value < oluoi_boundary.maxLong &&
                            doc.time_action >= start &&
                            doc.time_action <= end).ToList().OrderBy(doc => doc.time_action);
                    foreach (InfoConnect doc in docs)
                    {
                        addMachinePointModel(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code, doc.time_action.ToLocalTime(), false);
                    }
                    axMap1.Redraw();
                    //});
                    //thread.IsBackground = true;
                    //thread.Start();
                    //Task.Factory.StartNew(async () =>
                    //{
                    //    var docs = await collection.Find(
                    //            doc => doc.lat_value > oluoi_boundary.minLat &&
                    //            doc.lat_value < oluoi_boundary.maxLat &&
                    //            doc.long_value > oluoi_boundary.minLong &&
                    //            doc.long_value > oluoi_boundary.minLong).ToListAsync();
                    //    foreach (InfoConnect doc in docs)
                    //    {
                    //        addMachinePoint(doc.coordinate.Coordinates.X, doc.coordinate.Coordinates.Y, doc.code);
                    //    }
                    //});
                    //swObj.Stop();
                    //MessageBox.Show("Load: " + swObj.Elapsed.TotalSeconds + "s");
                }
            }
        }

        private void callSocketPhanTich()
        {
            

            oluoi_boundary.timeSent = DateTime.Now;
            //if (string.IsNullOrEmpty(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudKhoangPT")))
            //{
            //    oluoi_boundary.khoangPT = 3;
            //}
            //else
            //{
            //    if (int.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudKhoangPT"), out int khoangPT))
            //    {
            //        oluoi_boundary.khoangPT = khoangPT;
            //    }
            //    else
            //    {
            //        oluoi_boundary.khoangPT = 3;
            //    }
            //}
            if (int.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudKhoangPT"), out int khoangPT))
            {
                oluoi_boundary.khoangPT = khoangPT;
            }
            //if (!string.IsNullOrEmpty(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudNguongBom")))
            //{
            //    if (double.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudNguongBom"), out double nguongBom))
            //    {
            //        oluoi_boundary.nguongBom = nguongBom;
            //    }
            //}
            if (double.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudNguongBom"), out double nguongBom))
            {
                oluoi_boundary.nguongBom = nguongBom;
            }

            if (double.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudNguongMin"), out double nguongMin))
            {
                oluoi_boundary.nguongMin = nguongMin;
            }
            if (int.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudMinClusterSize"), out int minClusterSize))
            {
                oluoi_boundary.minClusterSize = minClusterSize;
            }
            if (double.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudMinBomb"), out double minBomb))
            {
                oluoi_boundary.minBomb = minBomb;
            }
            if (double.TryParse(AppUtils.GetRecentInput("$MenuLoaderManagerFrm$nudMinMine"), out double minMine))
            {
                oluoi_boundary.minMine = minMine;
            }
            string json = JsonConvert.SerializeObject(oluoi_boundary);
            //string mess = minLat.ToString() + ";;" + minLong.ToString() + ";;" + maxLat.ToString() + maxLong.ToString();
            socketclient.Send(json);
            Task.Factory.StartNew(ResponseSocketPhanTich);
        }

        private void callSocketNanDiem()
        {
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            long oluoi_id = -1;
            if (cb50x50.InvokeRequired)
            {
                cb50x50.Invoke(new MethodInvoker(() =>
                {
                    oluoi_id = (long)cb50x50.SelectedValue;
                }));
            }
            else
            {
                oluoi_id = (long)cb50x50.SelectedValue;
            }
            string sql =
                "select " +
                "gid, " +
                "o_id, " +
                "khaosat_deptid, " +
                "dept_ks.name as khaosat_deptidST, " +
                "rapha_deptid, " +
                "dept_rp.name as rapha_deptidST, " +
                "cecm_program_areamap_id, " +
                "cecm_program_id, " +
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
                "where gid = " + oluoi_id;
            sqlAdapter = new SqlDataAdapter(sql, frmLoggin.sqlCon);
            sqlAdapter.Fill(datatable);
            if (datatable.Rows.Count > 0)
            {
                DataRow dr = datatable.Rows[0];
                OLuoi oluoi = new OLuoi();
                //gid
                string gid = dr["gid"].ToString();
                oluoi.gid = long.Parse(gid);
                //cecm_program_areamap_id
                string cecm_program_areamap_id = dr["cecm_program_areamap_id"].ToString();
                oluoi.cecm_program_areamap_ID = long.Parse(cecm_program_areamap_id);
                //cecm_program_id
                string cecm_program_id = dr["cecm_program_id"].ToString();
                oluoi.cecm_program_id = long.Parse(cecm_program_id);
                //o_id
                string o_id = dr["o_id"].ToString();
                oluoi.o_id = o_id;
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
                ObjectModel model = new ObjectModel();
                model.oluoi = oluoi;
                if (string.IsNullOrEmpty(AppUtils.GetRecentInput("$ParametersFrm$nudDilution")))
                {
                    model.dilution = ParametersFrm.DILUTION_DEFAULT;
                }
                else
                {
                    if (double.TryParse(AppUtils.GetRecentInput("$ParametersFrm$nudDilution"), out double dilution))
                    {
                        model.dilution = dilution;
                    }
                    else
                    {
                        model.dilution = ParametersFrm.DILUTION_DEFAULT;
                    }
                }
                if (string.IsNullOrEmpty(AppUtils.GetRecentInput("$ParametersFrm$nudMinPoint")))
                {
                    model.minPoint = ParametersFrm.MIN_POINT_DEFAULT;
                }
                else
                {
                    if (int.TryParse(AppUtils.GetRecentInput("$ParametersFrm$nudMinPoint"), out int minPoint))
                    {
                        model.minPoint = minPoint;
                    }
                    else
                    {
                        model.minPoint = ParametersFrm.MIN_POINT_DEFAULT;
                    }
                }
                if (string.IsNullOrEmpty(AppUtils.GetRecentInput("$ParametersFrm$nudMinTime")))
                {
                    model.minTime = ParametersFrm.MIN_TIME_DEFAULT;
                }
                else
                {
                    if (int.TryParse(AppUtils.GetRecentInput("$ParametersFrm$nudMinTime"), out int minTime))
                    {
                        model.minTime = minTime;
                    }
                    else
                    {
                        model.minTime = ParametersFrm.MIN_TIME_DEFAULT;
                    }
                }
                string json = JsonConvert.SerializeObject(model);
                socketNanDiem.Send(json);
                Task.Factory.StartNew(ResponseSocketNanDien);
            }
        }

        private LastModelTime getLastModelTime(long o_id)
        {
            var database = frmLoggin.mgCon.GetDatabase("db_cecm");
            if (database != null)
            {
                var collection = database.GetCollection<LastModelTime>("last_model_time");
                LastModelTime time = collection.Find(item => item.o_id == o_id).FirstOrDefault();
                return time;
            }
            else
            {
                return null;
            }
        }

        private void ClearRealTimeModelLayer()
        {
            foreach (int layer in machinePointRealTimeModelLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                sf.EditClear();
            }
            foreach (int layer in machineLineRealTimeModelLayers)
            {
                Shapefile sf = axMap1.get_Shapefile(layer);
                //sf.EditClear();
                int numShape = sf.NumShapes;
                for(int i = 0; i < numShape; i++)
                {
                    sf.EditDeleteShape(i);
                }
            }
            //foreach(string code in machineActive__lastRealTimeModel.Keys.ToList())
            //{
            //    machineActive__lastRealTimeModel[code] = DateTime.MinValue;
            //}
        }

        private void btnPara_Click(object sender, EventArgs e)
        {
            ParametersFrm frm = new ParametersFrm();
            frm.ShowDialog();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            hideShowUnmodel(false);
            hideShowModel(false);
            foreach (int layer in machinePointLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
            foreach (int layer in machineLineLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
        }

        private void btnRealTime_Click(object sender, EventArgs e)
        {
            hideShowUnmodel(false);
            hideShowModel(false);
            foreach (int layer in machinePointRealTimeLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
            foreach (int layer in machineLineRealTimeLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
        }

        private void btnModelHistory_Click(object sender, EventArgs e)
        {
            hideShowUnmodel(false);
            hideShowModel(false);
            foreach (int layer in machinePointModelHistoryLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
            foreach (int layer in machineLineModelHistoryLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
        }

        private void btnModel__Click(object sender, EventArgs e)
        {
            hideShowUnmodel(false);
            hideShowModel(false);
            foreach (int layer in machinePointModelLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
            foreach (int layer in machineLineModelLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
        }

        private void btnModelRealTime_Click(object sender, EventArgs e)
        {
            hideShowUnmodel(false);
            hideShowModel(false);
            foreach (int layer in machinePointRealTimeModelLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
            foreach (int layer in machineLineRealTimeModelLayers)
            {
                axMap1.set_LayerVisible(layer, true);
            }
        }

        private async void ResponseSocketPhanTich()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            try
            {
                count_mess++;
                var received = await socketclient.Receive();
                lstCenterGlobal.Clear();

                //if (count_mess >= 2) {
                //    if (pnlKQPTContainer.InvokeRequired)
                //    {
                //        pnlKQPTContainer.Invoke(new MethodInvoker(delegate
                //        {
                //            pnlKQPTContainer.Controls.Clear();
                //        }));
                //    }
                //    else
                //    {
                //        pnlKQPTContainer.Controls.Clear();
                //    }
                //    //pnlKQPTContainer.Controls.Clear();
                //}
                if (pnlKQPTContainer.InvokeRequired)
                {
                    pnlKQPTContainer.Invoke(new MethodInvoker(delegate
                    {
                        pnlKQPTContainer.Controls.Clear();

                    }));
                }
                else
                {
                    pnlKQPTContainer.Controls.Clear();
                }
                if (axMap1.InvokeRequired)
                {
                    axMap1.Invoke(new MethodInvoker(delegate
                    {
                        axMap1.RemoveLayer(suspectPointLayer);
                        InitSuspectPointLayer();
                        axMap1.RemoveLayer(suspectPointLayerMine);
                        InitSuspectPointMineLayer();
                        axMap1.RemoveLayer(flagLayer);
                        //axMap1.set_LayerVisible(flagRealTimeLayer, false);
                        InitFlagLayer();
                        axMap1.RemoveLayer(flagRealTimeLayer);
                        InitFlagRealTimeLayer();
                        axMap1.RemoveLayer(highlightLayer);
                        InitHighlightLayer();
                        axMap1.ClearDrawingLabels(labelLayer);
                        //Shapefile sf = axMap1.get_Shapefile(suspectPointLayer);
                        //sf.EditClear();
                        //sf = axMap1.get_Shapefile(suspectPointLayerMine);
                        //sf.EditClear();
                        //sf = axMap1.get_Shapefile(flagLayer);
                        //sf.EditClear();
                        //axMap1.ClearDrawingLabels(flagLayer);
                        //sf = axMap1.get_Shapefile(highlightLayer);
                        //sf.EditClear();
                        //axMap1.ClearDrawingLabels(labelLayer);

                        axMap1.MouseUpEvent -= mouseUpHandler;
                        mouseUpHandler = null;
                    }));
                }
                else
                {
                    axMap1.RemoveLayer(suspectPointLayer);
                    InitSuspectPointLayer();
                    axMap1.RemoveLayer(suspectPointLayerMine);
                    InitSuspectPointMineLayer();
                    axMap1.RemoveLayer(flagLayer);
                    //axMap1.set_LayerVisible(flagRealTimeLayer, false);
                    InitFlagLayer();
                    axMap1.RemoveLayer(flagRealTimeLayer);
                    InitFlagRealTimeLayer();
                    axMap1.RemoveLayer(highlightLayer);
                    InitHighlightLayer();
                    axMap1.ClearDrawingLabels(labelLayer);
                    //Shapefile sf = axMap1.get_Shapefile(suspectPointLayer);
                    //sf.EditClear();
                    //sf = axMap1.get_Shapefile(suspectPointLayerMine);
                    //sf.EditClear();
                    //sf = axMap1.get_Shapefile(flagLayer);
                    //sf.EditClear();
                    //axMap1.ClearDrawingLabels(flagLayer);
                    //sf = axMap1.get_Shapefile(highlightLayer);
                    //sf.EditClear();
                    //axMap1.ClearDrawingLabels(labelLayer);

                    axMap1.MouseUpEvent -= mouseUpHandler;
                    mouseUpHandler = null;
                }

                //MessageBox.Show("Clear");

                //MessageBox.Show("count_mess: " + count_mess + " " + DateTime.Now.ToString("HH:mm:ss"));
                //MessageBox.Show("Đã nhận: " + received.Message);
                List<Vertex> lstCenter = JsonConvert.DeserializeObject<List<Vertex>>(received.Message);
                //int count = 0;
                var st = "";

                List<KQPhanTichControl> controls = new List<KQPhanTichControl>();
                foreach (var item in lstCenter)
                {
                    if (item.Type == 1)
                    {
                        st += "X: " + item.X + " Y: " + item.Y + " Type " + item.Type + " \n";
                    }

                    //if (lstCenterGlobal.Exists(vertex =>
                    //    //Math.Sqrt(Math.Pow(item.X - vertex.X, 2) + Math.Pow(item.Y - vertex.Y, 2)) < 1 ||
                    //    (
                    //    vertex.X < oluoi_boundary.minLat ||
                    //    vertex.X > oluoi_boundary.maxLat ||
                    //    vertex.Y < oluoi_boundary.minLong ||
                    //    vertex.Y > oluoi_boundary.maxLong
                    //    )
                    //))
                    //{
                    //    continue;
                    //}
                    if (
                        item.X < oluoi_boundary.minLat ||
                        item.X > oluoi_boundary.maxLat ||
                        item.Y < oluoi_boundary.minLong ||
                        item.Y > oluoi_boundary.maxLong
                    )
                    {
                        continue;
                    }
                    //bool existNearby = false;

                    //foreach (Vertex vertex in lstCenterOver)
                    //{
                    //    if (vertex.phanTichControl == null)
                    //    {
                    //        continue;
                    //    }
                    //    if (Math.Sqrt(Math.Pow(item.X - vertex.X, 2) + Math.Pow(item.Y - vertex.Y, 2)) < min_distance)
                    //    {
                    //        existNearby = true;

                    //        if (vertex.Type == Vertex.CAMCO && item.Type == Vertex.BOM)
                    //        {
                    //            count++;

                    //            if (vertex.phanTichControl.InvokeRequired)
                    //            {
                    //                vertex.phanTichControl.Invoke(new MethodInvoker(delegate
                    //                {
                    //                    if (vertex.TypeBombMine == Vertex.TYPE_BOMB)
                    //                    {
                    //                        vertex.phanTichControl.setExistBomb(true);
                    //                    }
                    //                    else if (vertex.TypeBombMine == Vertex.TYPE_MINE)
                    //                    {
                    //                        vertex.phanTichControl.setExistMine(true);
                    //                    }
                    //                }));
                    //            }
                    //            else
                    //            {
                    //                if (vertex.TypeBombMine == Vertex.TYPE_BOMB)
                    //                {
                    //                    vertex.phanTichControl.setExistBomb(true);
                    //                }
                    //                else if (vertex.TypeBombMine == Vertex.TYPE_MINE)
                    //                {
                    //                    vertex.phanTichControl.setExistMine(true);
                    //                }
                    //            }
                    //        }
                    //        else if (vertex.Type == Vertex.BOM && item.Type == Vertex.CAMCO)
                    //        {
                    //            vertex.Type = Vertex.CAMCO;
                    //            if (vertex.phanTichControl.InvokeRequired)
                    //            {
                    //                vertex.phanTichControl.Invoke(new MethodInvoker(delegate
                    //                {
                    //                    vertex.phanTichControl.setType(Vertex.CAMCO);
                    //                }));
                    //            }
                    //            else
                    //            {
                    //                vertex.phanTichControl.setType(Vertex.CAMCO);
                    //            }
                    //        }
                    //    }
                    //}
                    //if (existNearby)
                    //{
                    //    continue;
                    //}
                    //if (item.Type == Vertex.BOM)
                    //{
                    //    count++;
                    //}
                    double latt = 0;
                    double longt = 0;
                    ////Console.WriteLine("CENTER  = " + item.ToString());
                    ToLatLon(item.X, item.Y, ref latt, ref longt, "48N");

                    //int handleLayer = axMap1.NewDrawing(tkDrawReferenceList.dlSpatiallyReferencedList);
                    //Labels labels = axMap1.get_DrawingLabels(labelLayer);
                    //labels.FontSize = 12;
                    //string labelText = string.Format(
                    //    "Kinh độ: {0}\n" +
                    //    "Vĩ độ: {1}\n" +
                    //    "Độ sâu: {2}m\n" +
                    //    "Cường độ từ trường: {3}",
                    //    Math.Round(longt, 6), Math.Round(latt, 6), Math.Round(item.depth, 6), Math.Round(item.Z, 6)
                    //);
                    //double pxX = 0, pxY = 0;
                    ////axMap1.ProjToPixel(longt, latt, ref pxX, ref pxY);
                    ////axMap1.PixelToProj(pxX + 10, pxY + 10, ref longt, ref latt);
                    //labels.AddLabel(labelText, longt, latt);
                    //labels.Visible = false;
                    //labels.Alignment = tkLabelAlignment.laBottomRight;
                    //labels.OffsetX = 20;
                    //labels.OffsetY = 20;
                    //labels.VerticalPosition = tkVerticalPosition.vpAboveAllLayers;
                    //labels.AvoidCollisions = true;

                    //axMap1.set_DrawingLabels(labelLayer, labels);
                    KQPhanTichControl phanTichControl;
                    if (item.Type == Vertex.BOM_CAMCO || item.Type == Vertex.CAMCO)
                    {
                        bool existBomb = false;
                        bool existMine = false;
                        if (item.Type == Vertex.BOM_CAMCO)
                        {
                            if (item.TypeBombMine == Vertex.TYPE_BOMB_MINE)
                            {
                                existBomb = true;
                                existMine = true;
                            }
                            else if (item.TypeBombMine == Vertex.TYPE_BOMB)
                            {
                                existBomb = true;
                            }
                            else if (item.TypeBombMine == Vertex.TYPE_MINE)
                            {
                                existMine = true;
                            }
                        }
                        if (machine__label.ContainsKey(item.MachineCode))
                        {
                            phanTichControl = new KQPhanTichControl(
                                KQPhanTichControl.CAMCO,
                                machine__label[item.MachineCode].getLblMachineCode().Text,
                                machine__label[item.MachineCode].getLblStaff().Text,
                                Math.Round(latt, 6), Math.Round(longt, 6), Math.Round(item.Z, 6),
                                Math.Round(item.depth, 6),
                                existBomb, existMine
                            );
                        }
                        else
                        {
                            phanTichControl = new KQPhanTichControl(
                                KQPhanTichControl.CAMCO,
                                "",
                                "",
                                Math.Round(latt, 6), Math.Round(longt, 6), Math.Round(item.Z, 6),
                                Math.Round(item.depth, 6),
                                existBomb, existMine
                            );
                        }
                        CheckCamCo(item.BitSent, out bool isButton1Press);
                        if (isButton1Press)
                        {
                            //row.DefaultCellStyle.BackColor = Color.Red;

                            DiemDoData diemDoData = new DiemDoData();
                            diemDoData.XUtm = item.X;
                            diemDoData.YUtm = item.Y;
                            diemDoData.Z = item.Z;
                            diemDoData.CodeMachine = item.MachineCode;
                            diemDoData.DepthBoom = double.NaN;
                            lstDiemDo1.Add(diemDoData);
                            //labels.AddLabel("0", dLong, dLat);
                            //labels.OffsetX = 20;
                            //labels.OffsetY = 20;
                            //axMap1.set_DrawingLabels(handleLayer, labels);
                            addFlag(longt, latt, 0);
                            string labelText2 = string.Format(
                                "Mã máy: {0}\n" +
                                "Kinh độ: {1}\n" +
                                "Vĩ độ: {2}\n" +
                                "Cường độ từ trường: {3}\n" +
                                "Độ sâu: {4}m",
                                item.MachineCode, Math.Round(longt, 6), Math.Round(latt, 6), Math.Round(item.Z * _HeSoMayDoBom), 0
                            );
                            //labels_info.AddLabel(labelText2, longt, latt);
                            //axMap1.Redraw();
                            phanTichControl.setDepth(0);
                        }
                        else
                        {
                            var diemDoGanNhat = lstDiemDo1.Where(x => x.CodeMachine == item.MachineCode &&
                                                                    Math.Abs(x.XUtm - item.X) < Epsilone &&
                                                                    Math.Abs(x.YUtm - item.Y) < Epsilone).LastOrDefault();

                            if (diemDoGanNhat != null)
                            {
                                diemDoGanNhat.DepthBoom = DepthSchedule(diemDoGanNhat.XUtm, diemDoGanNhat.YUtm, diemDoGanNhat.Z,
                                                                item.X, item.Y, item.Z);
                                //labels.AddLabel(diemDoGanNhat.DepthBoom.ToString(), dLong, dLat);
                                //labels.OffsetX = 20;
                                //labels.OffsetY = 20;
                                //axMap1.set_DrawingLabels(handleLayer, labels);
                                addFlag(longt, latt, diemDoGanNhat.DepthBoom);
                                string labelText2 = string.Format(
                                    "Mã máy: {0}\n" +
                                    "Kinh độ: {1}\n" +
                                    "Vĩ độ: {2}\n" +
                                    "Cường độ từ trường: {3}\n" +
                                    "Độ sâu: {4}m",
                                    item.MachineCode, Math.Round(longt, 6), Math.Round(latt, 6), Math.Round(item.Z * _HeSoMayDoBom), diemDoGanNhat.DepthBoom
                                );
                                //labels_info.AddLabel(labelText2, longt, latt);
                                //axMap1.Redraw();
                                phanTichControl.setDepth(diemDoGanNhat.DepthBoom);
                            }
                            else
                            {
                                //labels.AddLabel("0", dLong, dLat);
                                //labels.OffsetX = 20;
                                //labels.OffsetY = 20;
                                //axMap1.set_DrawingLabels(handleLayer, labels);
                                addFlag(longt, latt, 0);
                                string labelText2 = string.Format(
                                    "Mã máy: {0}\n" +
                                    "Kinh độ: {1}\n" +
                                    "Vĩ độ: {2}\n" +
                                    "Cường độ từ trường: {3}\n" +
                                    "Độ sâu: {4}m",
                                    item.MachineCode, Math.Round(longt, 6), Math.Round(latt, 6), Math.Round(item.Z * _HeSoMayDoBom), 0
                                );
                                //labels_info.AddLabel(labelText2, longt, latt);
                                //axMap1.Redraw();
                                phanTichControl.setDepth(0);
                            }
                            //axMap1.Redraw();
                            UpdateDiemCamCo();
                        }
                        //if(item.Type == Vertex.BOM_CAMCO)
                        //{
                        //    if (item.TypeBombMine == Vertex.TYPE_BOMB_MINE)
                        //    {
                        //        phanTichControl.setExistBomb(true);
                        //        phanTichControl.setExistMine(true);
                        //    }
                        //    else if (item.TypeBombMine == Vertex.TYPE_BOMB)
                        //    {
                        //        phanTichControl.setExistBomb(true);
                        //    }
                        //    else if (item.TypeBombMine == Vertex.TYPE_MINE)
                        //    {
                        //        phanTichControl.setExistMine(true);
                        //    }
                        //}
                    }
                    else
                    {
                        if (item.TypeBombMine == Vertex.TYPE_BOMB)
                        {
                            addSuspectPoint(longt, latt);
                        }
                        else
                        {
                            addSuspectPointMine(longt, latt);
                        }
                        phanTichControl = new KQPhanTichControl(
                            KQPhanTichControl.BOM,
                            "Điểm nghi ngờ ",
                            "",
                            Math.Round(latt, 6), Math.Round(longt, 6), Math.Round(item.Z, 6),
                            Math.Round(item.depth, 6), item.TypeBombMine == Vertex.TYPE_BOMB, item.TypeBombMine == Vertex.TYPE_MINE
                        );
                    }
                    //phanTichControl.vertex = item;
                    phanTichControl.program_id = long.Parse(_currentId);
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            phanTichControl.area_id = (long)cbKhuVuc.SelectedValue;
                            phanTichControl.o_id = (long)cb50x50.SelectedValue;
                        }));
                    }
                    else
                    {
                        phanTichControl.area_id = (long)cbKhuVuc.SelectedValue;
                        phanTichControl.o_id = (long)cb50x50.SelectedValue;
                    }
                    //phanTichControl.label_info = labels;
                    phanTichControl.Click += (sender, e) =>
                    {
                        foreach (Control control in pnlKQPTContainer.Controls)
                        {
                            control.BackColor = Control.DefaultBackColor;
                            //if (control is KQPhanTichControl)
                            //{
                            //    KQPhanTichControl ptControl = (KQPhanTichControl)control;
                            //    ptControl.label_info.Visible = false;
                            //    axMap1.Redraw();
                            //}
                        }
                        axMap1.RemoveLayer(highlightLayer);
                        InitHighlightLayer();
                        phanTichControl.BackColor = Color.Green;
                        //axMap1.SetLatitudeLongitude(latt, longt);
                        //axMap1.ZoomToTileLevel(25);
                        Extents extents = new Extents();
                        extents.SetBounds(longt - 0.00001, latt - 0.00001, 0, longt + 0.00001, latt + 0.00001, 0);
                        axMap1.Extents = extents;
                        addHighlight(longt, latt);
                        //labels.Visible = true;
                        //Labels labels = axMap1.get_DrawingLabels(labelLayer);
                        Labels labels = new Labels();
                        labels.FontSize = 12;
                        string labelText = string.Format(
                            "Kinh độ: {0}\n" +
                            "Vĩ độ: {1}\n" +
                            "Độ sâu: {2}m\n" +
                            "Cường độ từ trường: {3}",
                            Math.Round(longt, 6), Math.Round(latt, 6), Math.Round(item.depth, 6), Math.Round(item.Z, 6)
                        );
                        double pxX = 0, pxY = 0;
                        //axMap1.ProjToPixel(longt, latt, ref pxX, ref pxY);
                        //axMap1.PixelToProj(pxX + 10, pxY + 10, ref longt, ref latt);
                        labels.AddLabel(labelText, longt, latt);
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
                            //if(labels.get_Label(0, 0) != null)
                            //{
                            //    Extents ext = labels.get_Label(0, 0).ScreenExtents;
                            //    if (ext != null)
                            //    {
                            //        if (e3.x < ext.xMin || e3.x > ext.xMax || e3.y < ext.yMin || e3.y > ext.yMax)
                            //        {
                            //            labels.Visible = false;
                            //            axMap1.RemoveLayer(highlightLayer);
                            //            InitHighlightLayer();
                            //        }
                            //    }
                            //}
                            labels.Visible = false;
                            axMap1.RemoveLayer(highlightLayer);
                            phanTichControl.BackColor = SystemColors.Control;
                            InitHighlightLayer();

                        });
                        axMap1.MouseUpEvent += mouseUpHandler;
                        axMap1.Redraw();
                    };
                    //if (phanTichControl.InvokeRequired)
                    //{
                    //    phanTichControl.Invoke(new MethodInvoker(delegate
                    //    {
                    //        if (pnlKQPTContainer.InvokeRequired)
                    //        {
                    //            pnlKQPTContainer.Invoke(new MethodInvoker(delegate
                    //            {
                    //                pnlKQPTContainer.Controls.Add(phanTichControl);
                    //            }
                    //            ));
                    //        }
                    //        else
                    //        {
                    //            pnlKQPTContainer.Controls.Add(phanTichControl);//vào đây
                    //        }
                    //        //pnlKQPTContainer.Controls.Add(phanTichControl);
                    //    }));
                    //}

                    //else
                    //{
                    //    if (pnlKQPTContainer.InvokeRequired)
                    //    {
                    //        pnlKQPTContainer.Invoke(new MethodInvoker(delegate
                    //        {
                    //            pnlKQPTContainer.Controls.Add(phanTichControl);//vào đây
                    //        }
                    //        ));
                    //    }
                    //    else
                    //    {
                    //        pnlKQPTContainer.Controls.Add(phanTichControl);
                    //    }
                    //}
                    //if (pnlKQPTContainer.InvokeRequired)
                    //{
                    //    pnlKQPTContainer.Invoke(new dlgAddItem(AddItem), phanTichControl);
                    //}
                    //AddItem(phanTichControl);
                    item.phanTichControl = phanTichControl;
                    phanTichControl.Name = "phanTichControl" + controls.Count;
                    controls.Add(phanTichControl);
                    //pnlKQPTContainer.Controls.Add(phanTichControl);
                    lstCenterGlobal.Add(item);
                }
                if (pnlKQPTContainer.InvokeRequired)
                {
                    pnlKQPTContainer.Invoke(new MethodInvoker(delegate
                    {
                        IEnumerable<KQPhanTichControl> controlsInvoke = controls.Where(control => control.InvokeRequired);
                        IEnumerable<KQPhanTichControl> controlsNotInvoke = controls.Where(control => !control.InvokeRequired);
                        pnlKQPTContainer.Controls.AddRange(controlsNotInvoke.ToArray());
                    }
                    ));
                }
                else
                {
                    IEnumerable<KQPhanTichControl> controlsInvoke = controls.Where(control => control.InvokeRequired);
                    IEnumerable<KQPhanTichControl> controlsNotInvoke = controls.Where(control => !control.InvokeRequired);
                    pnlKQPTContainer.Controls.AddRange(controlsNotInvoke.ToArray());
                }
                //pnlKQPTContainer.Controls.AddRange(controls.ToArray());
                //BackgroundWorker backgroundWorker1 = new BackgroundWorker();
                //backgroundWorker1.DoWork += new DoWorkEventHandler((sender, e) =>
                //{
                //    e.Result = controls;
                //});
                //backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, e) =>
                //{
                //    List<KQPhanTichControl> lstControls = e.Result as List<KQPhanTichControl>;
                //    pnlKQPTContainer.Controls.AddRange(lstControls.ToArray());
                //});
                //backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                //MessageBox.Show(ex.Message);
                //MessageBox.Show(ex.StackTrace);
                
            }
        }

        private async void ResponseSocketNanDien()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            try
            {
                var received = await socketNanDiem.Receive();
                TimeQuery timeQuery = JsonConvert.DeserializeObject<TimeQuery>(received.Message);
                //deletePointsWithinTime(timeQuery.timeStart, timeQuery.timeEnd);
                ClearRealTimeModelLayer();
                LoadPointsModelHistory(timeQuery.timeStart, timeQuery.timeEnd);
            }
            catch (Exception ex)
            {

            }
        }

        private void AddDataToTable(string codeMachine, DateTime updateTimeData, double dValData, double dLong, double dLat, DateTime timeActionData, int bitSent, int satelliteCount, double dilution, bool isButton1Press, string type)
        {
            //add data to table
            DataGridViewRow row = (DataGridViewRow)dgvMessage.Rows[0].Clone();
            //row.Cells[0].Value = dgvMessage.Rows.Count;
            row.Cells[0].Value = ++rowCount;
            row.Cells[1].Value = type;
            if (macID__code.ContainsKey(codeMachine))
            {
                row.Cells[2].Value = macID__code[codeMachine] + " - " + codeMachine;
            }
            else
            {
                row.Cells[2].Value = codeMachine;
            }
            row.Cells[3].Value = updateTimeData.ToString(AppUtils.DateTimeShow);
            row.Cells[4].Value = dValData * _HeSoMayDoBom;
            row.Cells[5].Value = Math.Round(dLong, 5) + " - " + Math.Round(dLat, 5);
            row.Cells[6].Value = "00";
            row.Cells[7].Value = timeActionData.ToString(AppUtils.DateTimeShow);
            row.Cells[8].Value = bitSent;
            //Số vệ tinh
            row.Cells[9].Value = satelliteCount;
            //Sai số
            row.Cells[10].Value = dilution;
            if (isButton1Press)
            {
                row.DefaultCellStyle.BackColor = Color.Red;
            }
            else
            {
                if(type == "Bom")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSteelBlue;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
                

            if (filterMachineCode == codeMachine)
            {
                row.DefaultCellStyle.ForeColor = Color.Green;
            }
            else
            {
                row.DefaultCellStyle.ForeColor = SystemColors.ControlText;
            }
            dgvMessage.Rows.Insert(0, row);
            if (dgvMessage.Rows.Count > 100)
            {
                //dgvMessage.Rows[100].Visible = false;
                dgvMessage.Rows.RemoveAt(99);
            }
        }

        private MQTTObject HandleMQTTMessage(string strBuff)
        {
            MQTTObject obj = new MQTTObject();
            if (strBuff.StartsWith("$MDN,"))
            {
                obj.type = "Bom";
                string[] elements = strBuff.Split(',');
                if (elements.Length > 10)
                {
                    obj.numValue = short.Parse(elements[8]);
                    obj.numGPS = short.Parse(elements[9]);

                    obj.codeMachine = elements[1];
                    obj.dilution = double.Parse(elements[11]);
                    obj.satelliteCount = int.Parse(elements[12]);
                    //lọc máy dò
                    //if (!machine__label.ContainsKey(codeMachine))
                    //    return;
                    List<GPS> lstGPS = new List<GPS>();
                    if (obj.numGPS > 0 || obj.numValue > 0)
                    {
                        double northingData = 0, eastingData = 0, dValData = 0, dLat = 0, dLong = 0;
                        DateTime updateTimeData = new DateTime();
                        DateTime timeActionData = new DateTime();
                        int bitSent = 0;

                        int.TryParse(elements[10], out bitSent);
                        obj.bitSent = bitSent;
                        string dateTimeVal = elements[2] + ":" + elements[3] + ":" + elements[4] + " " + elements[5] + "/" + elements[6] + "/" + elements[7];

                        for (short i = 0; i < obj.numValue; i++)
                            dValData += double.Parse(elements[13 + i]);

                        if (obj.numValue > 0)
                            dValData /= obj.numValue;

                        obj.dValData = dValData;

                        short offset = obj.numValue;
                        offset += 13;
                        
                        for (short k = 0; k < obj.numGPS; k++)
                        {

                            try
                            {
                                GPS gps = new GPS();
                                string timeGPS = elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++];

                                string latVal = elements[offset++];
                                string longVal = elements[offset++];

                                if (ValidateValue(dateTimeVal, timeGPS, latVal, longVal, dValData.ToString()) == false)
                                    continue;

                                double.TryParse(latVal, out dLat);
                                gps.dLat = dLat;

                                double.TryParse(longVal, out dLong);
                                gps.dLong = dLong;

                                CultureInfo enUS = new CultureInfo("en-US");
                                DateTime.TryParseExact(dateTimeVal, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out updateTimeData);
                                updateTimeData = updateTimeData.ToLocalTime();
                                gps.updateTimeData = updateTimeData;

                                DateTime.TryParseExact(timeGPS, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out timeActionData);
                                timeActionData = timeActionData.ToLocalTime();
                                gps.timeActionData = timeActionData;

                                lstGPS.Add(gps);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        
                    }
                    obj.lstGPS = lstGPS;
                }
            }
            else if (strBuff.StartsWith("$MDM,"))
            {
                obj.type = "Mìn";
                string[] elements = strBuff.Split(',');
                if (elements.Length > 10)
                {
                    obj.numValue = short.Parse(elements[8]);
                    obj.numGPS = short.Parse(elements[9]);

                    obj.codeMachine = elements[1];
                    obj.dilution = double.Parse(elements[11]);
                    obj.satelliteCount = int.Parse(elements[12]);
                    //lọc máy dò
                    //if (!machine__label.ContainsKey(codeMachine))
                    //    return;
                    List<GPS> lstGPS = new List<GPS>();
                    if (obj.numGPS > 0 || obj.numValue > 0)
                    {
                        double northingData = 0, eastingData = 0, dValData = 0, dLat = 0, dLong = 0;
                        DateTime updateTimeData = new DateTime();
                        DateTime timeActionData = new DateTime();
                        int bitSent = 0;
                        string dateTimeVal = elements[2] + ":" + elements[3] + ":" + elements[4] + " " + elements[5] + "/" + elements[6] + "/" + elements[7];

                        int.TryParse(elements[10], out bitSent);
                        obj.bitSent = bitSent;

                        byte value = 0;
                        uint led14, mask = 80;
                        for (short i = 0; i < obj.numValue; i++)
                        {
                            value = byte.Parse(elements[13 + i]);

                            led14 = value;
                            led14 &= mask;
                            if (led14 > 0)
                                led14 = 1;
                            mask = value;
                            mask &= 127;
                            //if (i == numValue - 1)
                            //    AddLine("Value = " + mask + "-" + led14); // gia tri thang do - trang thai led 14
                            //else
                            //    AddLine("Value = " + mask + "-" + led14 + ","); // gia tri thang do - trang thai led 14

                            if (led14 == 1)
                                dValData = value;

                            if (i == obj.numValue - 1 && dValData == 0)
                                dValData = value;
                        }
                        obj.dValData = dValData;
                        short offset = obj.numValue;
                        offset += 13;
                        
                        for (short k = 0; k < obj.numGPS; k++)
                        {

                            try
                            {
                                GPS gps = new GPS();
                                string timeGPS = elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++];
                                string latVal = elements[offset++];
                                string longVal = elements[offset++];

                                if (ValidateValue(dateTimeVal, timeGPS, latVal, longVal, value.ToString()) == false)
                                    continue;

                                double.TryParse(latVal, out dLat);
                                gps.dLat = dLat;

                                double.TryParse(longVal, out dLong);
                                gps.dLong = dLong;

                                CultureInfo enUS = new CultureInfo("en-US");
                                DateTime.TryParseExact(dateTimeVal, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out updateTimeData);
                                updateTimeData = updateTimeData.ToLocalTime();
                                gps.updateTimeData = updateTimeData;

                                DateTime.TryParseExact(timeGPS, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out timeActionData);
                                timeActionData = timeActionData.ToLocalTime();
                                gps.timeActionData = timeActionData;

                                lstGPS.Add(gps);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        
                    }
                    obj.lstGPS = lstGPS;
                }
            }
            return obj;
        }
    }

    internal class DiemDoData
    {
        public double XUtm;
        public double YUtm;
        public double Z;
        public string CodeMachine;
        public bool IsButton1;
        public double DepthBoom;
    }
}