using DieuHanhCongTruong.Command;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.Account;
using MapWinGIS;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class ChiaManhBanDoOLuoiFrm : Form
    {
        public double xx = double.NaN;
        public double yy = double.NaN;
        public double offsetLe = double.NaN;
        //private bool _IsFirstRun = false;
        public static List<string> valueForm = new List<string>();
        public string m_MediaAvailable = null;
        public static double m_DisPaperX = double.NaN;
        public static double m_DisPaperY = double.NaN;
        public static double m_letren = double.NaN;
        public static double m_leduoi = double.NaN;
        public static double m_letrai = double.NaN;
        public static double m_lephai = double.NaN;
        public int m_cbtileSelected = -1;
        public bool m_rbcheck = true;
        public int m_comboBoxScale = 0;
        public string m_NameLayout = "";
        //private bool _isKhaoSat;
        public double x_ol = 0;
        public double y_ol = 0;
        public string TenVungDA = "";
        public string MaO = "";
        private double MaxLong = 0;
        private double MinLong = 0;
        private double MaxLat = 0;
        private double MinLat = 0;

        public ChiaManhBanDoOLuoiFrm()
        {
            //cmd = new ClassChiaManh();
            //_IsFirstRun = isFirstRun;
            //_isKhaoSat = isKhaoSat;
            InitializeComponent();
        }

        public static string m_DeviceName = null;

        private static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void frmChiaManhBanDo_Load(object sender, EventArgs e)
        {
            tbDuAn.Text = MyMainMenu2.tenDADH;
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name, polygongeomst FROM cecm_program_area_map where cecm_program_id = " + MyMainMenu2.idDADH), frmLoggin.sqlCon);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            DataRow dr2 = datatable.NewRow();
            dr2["id"] = -1;
            dr2["name"] = "Chưa chọn vùng dự án";
            dr2["polygongeomst"] = "";
            datatable.Rows.InsertAt(dr2, 0);
            cbKhuVuc.DataSource = datatable;
            cbKhuVuc.DisplayMember = "name";
            cbKhuVuc.ValueMember = "id";

            //comboBoxScale.Items.Add("1/500");
            //comboBoxScale.Items.Add("1/1000");
            //comboBoxScale.Items.Add("1/2000");
            //comboBoxScale.Items.Add("1/5000");
            //m_cbtileSelected = comboBoxScale.SelectedIndex;
            //comboBoxScale.SelectedIndex = 0;
            //StringCollection devicesPrinter = Extensions.GetDevicesAvailable();
            //if (devicesPrinter == null)
            //    return;
            //foreach (string s in devicesPrinter)
            //    comboBoxDevice.Items.Add(s);

            //// set default value
            //tbletren.Text = "25";
            //tbleduoi.Text = "25";
            //tbletrai.Text = "35";
            //tblephai.Text = "15";
            //tbX.Text = "0";
            //tbY.Text = "0";
            //tbTenKhungIn.Text = "";
            //rbnamdoc.Checked = true;
            //tbOffsetLe.Text = "10";


            //AppUtils.LoadRecentInput(comboBoxDevice, string.Empty);
            //AppUtils.LoadRecentInput(comboBoxMedia, string.Empty);
            //AppUtils.LoadRecentInput(comboBoxScale, string.Empty);
        }

        private void comboBoxDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string deviceName = null;
            //deviceName = comboBoxDevice.SelectedItem.ToString();
            ////gan cho extensisions
            //m_DeviceName = deviceName;

            //StringCollection mediaAvailable = new StringCollection();
            //mediaAvailable = Extensions.GetMediaAvailable(deviceName);
            //comboBoxMedia.Items.Clear();
            //foreach (string s in mediaAvailable)
            //    comboBoxMedia.Items.Add(s);
        }

        private void comboBoxMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxMedia.SelectedItem == null)
            //    return;

            //m_MediaAvailable = comboBoxMedia.SelectedItem.ToString();
            //List<string> ListPrinterName = new List<string>();
            //List<string> listValue = new List<string>();

            //listValue.Add(m_DeviceName + ";" + m_MediaAvailable);
            //ListPrinterName = cmd.CreateDictionary(listValue);

            //if (!ListPrinterName.Contains(m_DeviceName + ";" + m_MediaAvailable))
            //{
            //    tbletren.Text = "0";
            //    tbleduoi.Text = "0";
            //    tbletrai.Text = "0";
            //    tblephai.Text = "0";
            //    tbX.Text = "0";
            //    tbY.Text = "0";

            //    Extents2d pt2dWH = new Extents2d();
            //    PlotRotation plotrotation = new PlotRotation();

            //    Extents2d margins = new Extents2d();
            //    if (m_DeviceName != null && m_MediaAvailable != null)
            //        Extensions.GetPlotPaperSize(m_DeviceName, m_MediaAvailable, ref margins, ref plotrotation);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamngang.Checked == true && plotrotation == PlotRotation.Degrees090)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees000, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamngang.Checked == true && plotrotation == PlotRotation.Degrees000)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees090, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamdoc.Checked == true && plotrotation == PlotRotation.Degrees090)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees090, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamdoc.Checked == true && plotrotation == PlotRotation.Degrees000)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees000, ref margins);
            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);

            //    tbletren.Text = Math.Round(margins.MaxPoint.Y, 2).ToString();
            //    m_letren = Math.Round(margins.MaxPoint.Y, 2);
            //    tbleduoi.Text = Math.Round(margins.MinPoint.Y, 2).ToString();
            //    m_leduoi = Math.Round(margins.MinPoint.Y, 2);
            //    tbletrai.Text = Math.Round(margins.MinPoint.X, 2).ToString();
            //    m_letrai = Math.Round(margins.MinPoint.X, 2);
            //    tblephai.Text = Math.Round(margins.MaxPoint.X, 2).ToString();
            //    m_lephai = Math.Round(margins.MaxPoint.X, 2);

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);
            //    quaybandau = plotrotation;

            //    tbletren.Text = "25";
            //    tbleduoi.Text = "25";
            //    tbletrai.Text = "35";
            //    tblephai.Text = "15";

            //    listValue.Add(m_DeviceName + ";" + m_MediaAvailable + ";" +
            //                                    plotrotation.ToString() + ";" +
            //                                    pt2dWH.MinPoint.X.ToString() + ";" +
            //                                    pt2dWH.MinPoint.Y.ToString() + ";" +
            //                                    pt2dWH.MaxPoint.X.ToString() + ";" +
            //                                    pt2dWH.MaxPoint.Y.ToString() + ";" +
            //                                    Math.Round(margins.MinPoint.X, 2).ToString() + ";" +
            //                                    Math.Round(margins.MinPoint.Y, 2).ToString() + ";" +
            //                                    Math.Round(margins.MaxPoint.X, 2).ToString() + ";" +
            //                                    Math.Round(margins.MaxPoint.Y, 2).ToString());
            //    cmd.CreateDictionary(listValue, true);
            //}
            //else
            //{
            //    int index = ListPrinterName.LastIndexOf(m_DeviceName + ";" + m_MediaAvailable);
            //    string[] arrValue = new string[11];
            //    arrValue = ListPrinterName[index + 1].Split(';');

            //    Extents2d pt2dWH = new Extents2d(double.Parse(arrValue[3]), double.Parse(arrValue[4]), double.Parse(arrValue[5]), double.Parse(arrValue[6]));
            //    Extents2d margins = new Extents2d(double.Parse(arrValue[7]), double.Parse(arrValue[8]), double.Parse(arrValue[9]), double.Parse(arrValue[10]));
            //    PlotRotation plotrotation = new PlotRotation();
            //    switch (arrValue[2])
            //    {
            //        case "Degrees090":
            //            plotrotation = PlotRotation.Degrees090;
            //            break;

            //        case "Degrees000":
            //            plotrotation = PlotRotation.Degrees000;
            //            break;

            //        default:
            //            plotrotation = PlotRotation.Degrees000;
            //            break;
            //    }

            //    // set default value
            //    tbletren.Text = "0";
            //    tbleduoi.Text = "0";
            //    tbletrai.Text = "0";
            //    tblephai.Text = "0";
            //    tbX.Text = "0";
            //    tbY.Text = "0";

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);

            //    tbletren.Text = Math.Round(margins.MaxPoint.Y, 2).ToString();
            //    m_letren = Math.Round(margins.MaxPoint.Y, 2);
            //    tbleduoi.Text = Math.Round(margins.MinPoint.Y, 2).ToString();
            //    m_leduoi = Math.Round(margins.MinPoint.Y, 2);
            //    tbletrai.Text = Math.Round(margins.MinPoint.X, 2).ToString();
            //    m_letrai = Math.Round(margins.MinPoint.X, 2);
            //    tblephai.Text = Math.Round(margins.MaxPoint.X, 2).ToString();
            //    m_lephai = Math.Round(margins.MaxPoint.X, 2);

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);
            //    quaybandau = plotrotation;

            //    tbletrai.Text = "25";
            //    tblephai.Text = "25";
            //    tbleduoi.Text = "35";
            //    tbletren.Text = "15";
            //}
        }

        private void tbletren_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tblephai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbleduoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbletrai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void rbnamngang_CheckedChanged(object sender, EventArgs e)
        {
            //if (comboBoxMedia.SelectedItem == null)
            //    return;

            //m_MediaAvailable = comboBoxMedia.SelectedItem.ToString();
            //List<string> ListPrinterName = new List<string>();
            //List<string> listValue = new List<string>();

            //listValue.Add(m_DeviceName + ";" + m_MediaAvailable);
            //ListPrinterName = cmd.CreateDictionary(listValue);

            //if (!ListPrinterName.Contains(m_DeviceName + ";" + m_MediaAvailable))
            //{
            //    tbletren.Text = "0";
            //    tbleduoi.Text = "0";
            //    tbletrai.Text = "0";
            //    tblephai.Text = "0";
            //    tbX.Text = "0";
            //    tbY.Text = "0";

            //    Extents2d pt2dWH = new Extents2d();
            //    PlotRotation plotrotation = new PlotRotation();

            //    Extents2d margins = new Extents2d();
            //    if (m_DeviceName != null && m_MediaAvailable != null)
            //        Extensions.GetPlotPaperSize(m_DeviceName, m_MediaAvailable, ref margins, ref plotrotation);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamngang.Checked == true && plotrotation == PlotRotation.Degrees090)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees000, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamngang.Checked == true && plotrotation == PlotRotation.Degrees000)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees090, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamdoc.Checked == true && plotrotation == PlotRotation.Degrees090)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees090, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamdoc.Checked == true && plotrotation == PlotRotation.Degrees000)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees000, ref margins);
            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);

            //    tbletren.Text = Math.Round(margins.MaxPoint.Y, 2).ToString();
            //    m_letren = Math.Round(margins.MaxPoint.Y, 2);
            //    tbleduoi.Text = Math.Round(margins.MinPoint.Y, 2).ToString();
            //    m_leduoi = Math.Round(margins.MinPoint.Y, 2);
            //    tbletrai.Text = Math.Round(margins.MinPoint.X, 2).ToString();
            //    m_letrai = Math.Round(margins.MinPoint.X, 2);
            //    tblephai.Text = Math.Round(margins.MaxPoint.X, 2).ToString();
            //    m_lephai = Math.Round(margins.MaxPoint.X, 2);

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);
            //    quaybandau = plotrotation;

            //    listValue.Add(m_DeviceName + ";" + m_MediaAvailable + ";" +
            //                                    plotrotation.ToString() + ";" +
            //                                    pt2dWH.MinPoint.X.ToString() + ";" +
            //                                    pt2dWH.MinPoint.Y.ToString() + ";" +
            //                                    pt2dWH.MaxPoint.X.ToString() + ";" +
            //                                    pt2dWH.MaxPoint.Y.ToString() + ";" +
            //                                    Math.Round(margins.MinPoint.X, 2).ToString() + ";" +
            //                                    Math.Round(margins.MinPoint.Y, 2).ToString() + ";" +
            //                                    Math.Round(margins.MaxPoint.X, 2).ToString() + ";" +
            //                                    Math.Round(margins.MaxPoint.Y, 2).ToString());
            //    cmd.CreateDictionary(listValue, true);
            //}
            //else
            //{
            //    int index = ListPrinterName.LastIndexOf(m_DeviceName + ";" + m_MediaAvailable);
            //    string[] arrValue = new string[11];
            //    arrValue = ListPrinterName[index + 1].Split(';');

            //    Extents2d pt2dWH = new Extents2d(double.Parse(arrValue[3]), double.Parse(arrValue[4]), double.Parse(arrValue[5]), double.Parse(arrValue[6]));
            //    Extents2d margins = new Extents2d(double.Parse(arrValue[7]), double.Parse(arrValue[8]), double.Parse(arrValue[9]), double.Parse(arrValue[10]));
            //    PlotRotation plotrotation = new PlotRotation();
            //    switch (arrValue[2])
            //    {
            //        case "Degrees090":
            //            plotrotation = PlotRotation.Degrees090;
            //            break;

            //        case "Degrees000":
            //            plotrotation = PlotRotation.Degrees000;
            //            break;

            //        default:
            //            plotrotation = PlotRotation.Degrees000;
            //            break;
            //    }

            //    // set default value
            //    tbletren.Text = "0";
            //    tbleduoi.Text = "0";
            //    tbletrai.Text = "0";
            //    tblephai.Text = "0";
            //    tbX.Text = "0";
            //    tbY.Text = "0";

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);

            //    tbletren.Text = Math.Round(margins.MaxPoint.Y, 2).ToString();
            //    m_letren = Math.Round(margins.MaxPoint.Y, 2);
            //    tbleduoi.Text = Math.Round(margins.MinPoint.Y, 2).ToString();
            //    m_leduoi = Math.Round(margins.MinPoint.Y, 2);
            //    tbletrai.Text = Math.Round(margins.MinPoint.X, 2).ToString();
            //    m_letrai = Math.Round(margins.MinPoint.X, 2);
            //    tblephai.Text = Math.Round(margins.MaxPoint.X, 2).ToString();
            //    m_lephai = Math.Round(margins.MaxPoint.X, 2);

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);
            //    quaybandau = plotrotation;
            //}
        }

        private void rbnamdoc_CheckedChanged(object sender, EventArgs e)
        {
            //if (comboBoxMedia.SelectedItem == null)
            //    return;

            //m_MediaAvailable = comboBoxMedia.SelectedItem.ToString();
            //List<string> ListPrinterName = new List<string>();
            //List<string> listValue = new List<string>();

            //listValue.Add(m_DeviceName + ";" + m_MediaAvailable);
            //ListPrinterName = cmd.CreateDictionary(listValue);

            //if (!ListPrinterName.Contains(m_DeviceName + ";" + m_MediaAvailable))
            //{
            //    tbletren.Text = "0";
            //    tbleduoi.Text = "0";
            //    tbletrai.Text = "0";
            //    tblephai.Text = "0";
            //    tbX.Text = "0";
            //    tbY.Text = "0";

            //    Extents2d pt2dWH = new Extents2d();
            //    PlotRotation plotrotation = new PlotRotation();

            //    Extents2d margins = new Extents2d();
            //    if (m_DeviceName != null && m_MediaAvailable != null)
            //        Extensions.GetPlotPaperSize(m_DeviceName, m_MediaAvailable, ref margins, ref plotrotation);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamngang.Checked == true && plotrotation == PlotRotation.Degrees090)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees000, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamngang.Checked == true && plotrotation == PlotRotation.Degrees000)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees090, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamdoc.Checked == true && plotrotation == PlotRotation.Degrees090)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees090, ref margins);
            //    if (m_DeviceName != null && m_MediaAvailable != null && rbnamdoc.Checked == true && plotrotation == PlotRotation.Degrees000)
            //        pt2dWH = Extensions.GetPlotRotatePaperSize(m_DeviceName, m_MediaAvailable, PlotRotation.Degrees000, ref margins);
            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);

            //    tbletren.Text = Math.Round(margins.MaxPoint.Y, 2).ToString();
            //    m_letren = Math.Round(margins.MaxPoint.Y, 2);
            //    tbleduoi.Text = Math.Round(margins.MinPoint.Y, 2).ToString();
            //    m_leduoi = Math.Round(margins.MinPoint.Y, 2);
            //    tbletrai.Text = Math.Round(margins.MinPoint.X, 2).ToString();
            //    m_letrai = Math.Round(margins.MinPoint.X, 2);
            //    tblephai.Text = Math.Round(margins.MaxPoint.X, 2).ToString();
            //    m_lephai = Math.Round(margins.MaxPoint.X, 2);

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);
            //    quaybandau = plotrotation;

            //    listValue.Add(m_DeviceName + ";" + m_MediaAvailable + ";" +
            //                                    plotrotation.ToString() + ";" +
            //                                    pt2dWH.MinPoint.X.ToString() + ";" +
            //                                    pt2dWH.MinPoint.Y.ToString() + ";" +
            //                                    pt2dWH.MaxPoint.X.ToString() + ";" +
            //                                    pt2dWH.MaxPoint.Y.ToString() + ";" +
            //                                    Math.Round(margins.MinPoint.X, 2).ToString() + ";" +
            //                                    Math.Round(margins.MinPoint.Y, 2).ToString() + ";" +
            //                                    Math.Round(margins.MaxPoint.X, 2).ToString() + ";" +
            //                                    Math.Round(margins.MaxPoint.Y, 2).ToString());
            //    cmd.CreateDictionary(listValue, true);
            //}
            //else
            //{
            //    int index = ListPrinterName.LastIndexOf(m_DeviceName + ";" + m_MediaAvailable);
            //    string[] arrValue = new string[11];
            //    arrValue = ListPrinterName[index + 1].Split(';');

            //    Extents2d pt2dWH = new Extents2d(double.Parse(arrValue[3]), double.Parse(arrValue[4]), double.Parse(arrValue[5]), double.Parse(arrValue[6]));
            //    Extents2d margins = new Extents2d(double.Parse(arrValue[7]), double.Parse(arrValue[8]), double.Parse(arrValue[9]), double.Parse(arrValue[10]));
            //    PlotRotation plotrotation = new PlotRotation();
            //    switch (arrValue[2])
            //    {
            //        case "Degrees090":
            //            plotrotation = PlotRotation.Degrees090;
            //            break;

            //        case "Degrees000":
            //            plotrotation = PlotRotation.Degrees000;
            //            break;

            //        default:
            //            plotrotation = PlotRotation.Degrees000;
            //            break;
            //    }

            //    // set default value
            //    tbletren.Text = "0";
            //    tbleduoi.Text = "0";
            //    tbletrai.Text = "0";
            //    tblephai.Text = "0";
            //    tbX.Text = "0";
            //    tbY.Text = "0";

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);

            //    tbletren.Text = Math.Round(margins.MaxPoint.Y, 2).ToString();
            //    m_letren = Math.Round(margins.MaxPoint.Y, 2);
            //    tbleduoi.Text = Math.Round(margins.MinPoint.Y, 2).ToString();
            //    m_leduoi = Math.Round(margins.MinPoint.Y, 2);
            //    tbletrai.Text = Math.Round(margins.MinPoint.X, 2).ToString();
            //    m_letrai = Math.Round(margins.MinPoint.X, 2);
            //    tblephai.Text = Math.Round(margins.MaxPoint.X, 2).ToString();
            //    m_lephai = Math.Round(margins.MaxPoint.X, 2);

            //    // set W H to textbox
            //    tbX.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MaxPoint.X, pt2dWH.MinPoint.Y, 0)).ToString();
            //    tbX.Text = Math.Round(double.Parse(tbX.Text), 2).ToString();
            //    m_DisPaperX = double.Parse(tbX.Text);
            //    tbY.Text = cmd.distancePoint(new Point3d(pt2dWH.MinPoint.X, pt2dWH.MinPoint.Y, 0), new Point3d(pt2dWH.MinPoint.X, pt2dWH.MaxPoint.Y, 0)).ToString();
            //    tbY.Text = Math.Round(double.Parse(tbY.Text), 2).ToString();
            //    m_DisPaperY = double.Parse(tbY.Text);
            //    quaybandau = plotrotation;
            //}
        }

        private void comboBoxScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            //m_cbtileSelected = comboBoxScale.SelectedIndex;
            //Point3d ptBottonLeftFix = new Point3d();
            ////Fix lai point3d botton left with scale
            //if (ptBottonLeft != null && ptBottonLeftFix != null && m_cbtileSelected != -1)
            //{
            //    cmd.fixFist(ptBottonLeft, ref ptBottonLeftFix, m_cbtileSelected);
            //}

            //if (comboBoxScale.SelectedIndex == 0)
            //    tbOffsetLe.Text = "5";
            //else if (comboBoxScale.SelectedIndex == 1)
            //    tbOffsetLe.Text = "10";
            //else if (comboBoxScale.SelectedIndex == 2)
            //    tbOffsetLe.Text = "20";
            //else if (comboBoxScale.SelectedIndex == 3)
            //    tbOffsetLe.Text = "50";
        }

        private void btchiamanh_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            try
            {
                string pathFile = "";
                saveFileDialog1.FileName = Guid.NewGuid().ToString();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pathFile = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                //string sourceFile = Path.Combine(AppUtils.GetAppDataPath() + "\\TempExcel\\BB_KS1KV.xls");
                if (pathFile != null)
                {
                    TenVungDA = cbKhuVuc.SelectedIndex == 0 ? "" : cbKhuVuc.Text;
                    MaO = cbOLuoi.SelectedIndex == 0 ? "" : cbOLuoi.Text;
                    //Extents extents = new Extents();
                    //extents.SetBounds(x_ol - 0.0002, y_ol - 0.0005, 0, x_ol + 0.0002, y_ol + 0.0005, 0);
                    //Image img = MapMenuCommand.axMap1.SnapShot(extents);
                    MapWinGIS.Image img;
                    double margin_x = 0.0001;
                    
                    //if (rbnamdoc.Checked)
                    //{
                    double lat_center = (MaxLat + MinLat) / 2;
                    double left = MinLong - margin_x;
                    double right = MaxLong + margin_x;
                    double margin_y = (right - left) / 2 / Constants.PAPER_RATIO;
                    double top = lat_center + margin_y;
                    double bottom = lat_center - margin_y;

                    Labels labels = new Labels();
                    //LabelCategory labelCategory = labels.AddCategory("Transparent");
                    //labelCategory.FrameVisible = false;
                    //labelCategory.FontColor = AppUtils.ColorToUint(Color.Red);
                    //labelCategory.FontSize = 14;
                    //labelCategory.Alignment = tkLabelAlignment.laCenterRight;
                    //labels.ApplyCategories();

                    labels.Alignment = tkLabelAlignment.laCenterRight;
                    labels.InboxAlignment = tkLabelAlignment.laCenterLeft;
                    labels.FontSize = 14;

                    var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "cecm_programData", "id", MyMainMenu2.idDADH.ToString());
                    string tenDuAn = "";
                    foreach (DataRow dr in lstDatarow)
                    {
                        tenDuAn = dr["name"].ToString();
                        //labels.AddLabel("Dự án: " + dr["name"].ToString(), left + margin_x / 2, bottom + margin_y * 0.3, 0, 0);
                    }
                    //labels.AddLabel("Khu vực: " + TenVungDA, left + margin_x / 2, bottom + margin_y * 0.2, 0, 0);
                    string info =
                        "Dự án: " + tenDuAn + "\n" +
                        "Khu vực: " + TenVungDA;
                    if (MaO != "")
                    {
                        //labels.AddLabel("Mã ô: " + MaO, left + margin_x / 2, bottom + margin_y * 0.1, 0, 0);
                        info += "\nMã ô: " + MaO;
                    }

                    labels.AddLabel(info, left + margin_x / 2, bottom + margin_y * 0.3);
                    MapMenuCommand.axMap1.set_DrawingLabels(MapMenuCommand.labelLayer, labels);
                    img = MapMenuCommand.axMap1.SnapShot3(left, right, top, bottom, 1080);
                    MapMenuCommand.axMap1.ClearDrawingLabels(MapMenuCommand.labelLayer);
                    //}
                    //else
                    //{
                    //    double long_center = (MaxLat + MinLat) / 2;
                    //    double top = MaxLat + margin_x;
                    //    double bottom = MinLat - margin_x;
                    //    double left = long_center - (top - bottom) / 2 * Constants.PAPER_RATIO;
                    //    double right = long_center + (top - bottom) / 2 * Constants.PAPER_RATIO;
                    //    img = MapMenuCommand.axMap1.SnapShot3(left, right, top, bottom, 1080);
                    //}
                    img.Save(pathFile);
                    FileInfo fi = new FileInfo(pathFile);
                    if (fi.Exists)
                    {
                        System.Diagnostics.Process.Start(pathFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
            
            //valueForm.Clear();


            //double letren = double.NaN;
            //double leduoi = double.NaN;
            //double letrai = double.NaN;
            //double lephai = double.NaN;
            //offsetLe = double.Parse(tbOffsetLe.Text);

            //switch (m_cbtileSelected)
            //{
            //    case 0:
            //        letren = double.Parse(tbletren.Text) / 2;
            //        leduoi = double.Parse(tbleduoi.Text) / 2;
            //        letrai = double.Parse(tbletrai.Text) / 2;
            //        lephai = double.Parse(tblephai.Text) / 2;
            //        xx = double.Parse(tbX.Text) / 2;
            //        yy = double.Parse(tbY.Text) / 2;
            //        break;

            //    case 1:
            //        letren = double.Parse(tbletren.Text);
            //        leduoi = double.Parse(tbleduoi.Text);
            //        letrai = double.Parse(tbletrai.Text);
            //        lephai = double.Parse(tblephai.Text);
            //        xx = double.Parse(tbX.Text);
            //        yy = double.Parse(tbY.Text);
            //        break;

            //    case 2:
            //        letren = double.Parse(tbletren.Text) * 2;
            //        leduoi = double.Parse(tbleduoi.Text) * 2;
            //        letrai = double.Parse(tbletrai.Text) * 2;
            //        lephai = double.Parse(tblephai.Text) * 2;
            //        xx = double.Parse(tbX.Text) * 2;
            //        yy = double.Parse(tbY.Text) * 2;
            //        break;

            //    case 3:
            //        letren = double.Parse(tbletren.Text) * 5;
            //        leduoi = double.Parse(tbleduoi.Text) * 5;
            //        letrai = double.Parse(tbletrai.Text) * 5;
            //        lephai = double.Parse(tblephai.Text) * 5;
            //        xx = double.Parse(tbX.Text) * 5;
            //        yy = double.Parse(tbY.Text) * 5;
            //        break;
            //}

            ////lay du lieu form de gan xdata
            //valueForm.Add("0");
            //valueForm.Add("0");
            //valueForm.Add("0");
            //valueForm.Add("0");
            //valueForm.Add(tbTenKhungIn.Text);
            //valueForm.Add(comboBoxDevice.SelectedItem.ToString());
            //valueForm.Add(comboBoxMedia.SelectedItem.ToString());
            //valueForm.Add(comboBoxScale.SelectedIndex.ToString());
            //valueForm.Add(letren.ToString());
            //valueForm.Add(leduoi.ToString());
            //valueForm.Add(letrai.ToString());
            //valueForm.Add(lephai.ToString());
            //valueForm.Add(xx.ToString());
            //valueForm.Add(yy.ToString());
            //valueForm.Add(rbnamngang.Checked.ToString());
            //valueForm.Add(ckbkhungbando.Checked.ToString());
            //valueForm.Add(quaybandau.ToString());

            //AppUtils.SaveRecentInput(tbTenKhungIn);
            //AppUtils.SaveRecentInput(comboBoxDevice);
            //AppUtils.SaveRecentInput(comboBoxMedia);
            //AppUtils.SaveRecentInput(comboBoxScale);
            //AppUtils.SaveRecentInput(tbletren);
            //AppUtils.SaveRecentInput(tbleduoi);
            //AppUtils.SaveRecentInput(tbletrai);
            //AppUtils.SaveRecentInput(tblephai);
            //AppUtils.SaveRecentInput(ckbkhungbando);
            //AppUtils.SaveRecentInput(tbX);
            //AppUtils.SaveRecentInput(tbY);
            //AppUtils.SaveRecentInput(rbnamngang);
            //AppUtils.SaveRecentInput(tbOffsetLe);

            this.DialogResult = DialogResult.OK;
        }

        private void tbgocx_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbgocx_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbgocy_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbngonx_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbngony_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbX_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbY_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Text
            string text = ((Control)sender).Text;

            // Is Float Number?
            if (e.KeyChar == '.' && text.Length > 0 && !text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Is Digit?
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        //private void cbDA_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SqlCommandBuilder sqlCommand = null;
        //        SqlDataAdapter sqlAdapter = null;
        //        System.Data.DataTable datatable = new System.Data.DataTable();
        //        sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, CONCAT(code, ' - ', address) as name, polygongeomst FROM cecm_program_area_map where cecm_program_id = " + cbDA.SelectedValue), frmLoggin.sqlCon);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapter);
        //        sqlAdapter.Fill(datatable);
        //        DataRow dr2 = datatable.NewRow();
        //        dr2["id"] = -1;
        //        dr2["name"] = "Chưa chọn vùng dự án";
        //        datatable.Rows.InsertAt(dr2, 0);
        //        cbKhuVuc.DataSource = datatable;
        //        cbKhuVuc.DisplayMember = "name";
        //        cbKhuVuc.ValueMember = "id";
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //}

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
                    "lat_center, long_center, " +
                    "(SELECT Max(long) FROM (VALUES (long_corner1), (long_corner2), (long_corner3), (long_corner4)) AS value(long)) as [MaxLong], " +
                    "(SELECT Min(long) FROM(VALUES(long_corner1), (long_corner2), (long_corner3), (long_corner4)) AS value(long)) as [MinLong], " +
                    "(SELECT Max(lat) FROM(VALUES(lat_corner1), (lat_corner2), (lat_corner3), (lat_corner4)) AS value(lat)) as [MaxLat], " +
                    "(SELECT Min(lat) FROM(VALUES(lat_corner1), (lat_corner2), (lat_corner3), (lat_corner4)) AS value(lat)) as [MinLat] " +
                    "FROM OLuoi where cecm_program_areamap_id = " + cbKhuVuc.SelectedValue;
                sqlAdapter = new SqlDataAdapter(sql_oluoi, frmLoggin.sqlCon);
                sqlCommand3 = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable3);

                DataRowView drw = (DataRowView)cbKhuVuc.SelectedItem;
                DataRow dr = drw.Row;
                //if (
                //    double.TryParse(dr["long_center"].ToString(), out double long_center) &&
                //    double.TryParse(dr["lat_center"].ToString(), out double lat_center))
                //{
                //    double[] position = AppUtils.ConverLatLongToUTM(lat_center, long_center);
                //    x_ol = position[0];
                //    y_ol = position[1];
                //}
                DataRow dr2 = datatable3.NewRow();
                dr2["gid"] = -1;
                dr2["o_id"] = "Chưa chọn ô lưới";

                string polygongeomst = dr["polygongeomst"].ToString();
                if (polygongeomst.Trim() != "")
                {
                    List<GeoJsonPolygon<GeoJson2DCoordinates>> polygons = AppUtils.GetPolygon(polygongeomst);

                    double long_min = double.MaxValue;
                    double lat_min = double.MaxValue;
                    double long_max = 0;
                    double lat_max = 0;
                    foreach (GeoJsonPolygon<GeoJson2DCoordinates> polygon in polygons)
                    {
                        foreach (GeoJson2DCoordinates position in polygon.Coordinates.Exterior.Positions)
                        {
                            if (position.X < long_min)
                            {
                                long_min = position.X;
                            }
                            if (position.Y < lat_min)
                            {
                                lat_min = position.Y;
                            }
                            if (position.X > long_max)
                            {
                                long_max = position.X;
                            }
                            if (position.Y > lat_max)
                            {
                                lat_max = position.Y;
                            }
                        }
                    }

                    //double long_center = (polygon.BoundingBox.Max.X + polygon.BoundingBox.Min.X) / 2;
                    //double lat_center = (polygon.BoundingBox.Max.Y + polygon.BoundingBox.Min.Y) / 2;
                    //double[] position = AppUtils.ConverLatLongToUTM(lat_center, long_center);
                    dr2["long_center"] = (long_min + long_max) / 2;
                    dr2["lat_center"] = (lat_min + lat_max) / 2;
                    dr2["MaxLong"] = long_max;
                    dr2["MinLong"] = long_min;
                    dr2["MaxLat"] = lat_max;
                    dr2["MinLat"] = lat_min;
                }
                else
                {
                    dr2["long_center"] = 0;
                    dr2["lat_center"] = 0;
                    dr2["MaxLong"] = 0;
                    dr2["MinLong"] = 0;
                    dr2["MaxLat"] = 0;
                    dr2["MinLat"] = 0;
                }

                datatable3.Rows.InsertAt(dr2, 0);
                cbOLuoi.DataSource = datatable3;
                cbOLuoi.DisplayMember = "o_id";
                cbOLuoi.ValueMember = "gid";
            }
            catch (Exception ex)
            {

            }

        }

        private void cbOLuoi_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView drw = (DataRowView)cbOLuoi.SelectedItem;
                DataRow dr = drw.Row;
                if (
                    double.TryParse(dr["long_center"].ToString(), out double long_center) &&
                    double.TryParse(dr["lat_center"].ToString(), out double lat_center) &&
                    double.TryParse(dr["MaxLat"].ToString(), out double max_lat) &&
                    double.TryParse(dr["MinLat"].ToString(), out double min_lat) &&
                    double.TryParse(dr["MaxLong"].ToString(), out double max_long) &&
                    double.TryParse(dr["MinLong"].ToString(), out double min_long))
                {
                    //double[] position = AppUtils.ConverLatLongToUTM(lat_center, long_center);
                    //x_ol = position[0];
                    //y_ol = position[1];
                    x_ol = long_center;
                    y_ol = lat_center;
                    MaxLat = max_lat;
                    MinLat = min_lat;
                    MaxLong = max_long;
                    MinLong = min_long;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void tbTenKhungIn_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (tbTenKhungIn.Text.Trim() != "")
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tbTenKhungIn, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tbTenKhungIn.Focus();
            //    errorProvider.SetError(tbTenKhungIn, "Chưa nhập tên khung in");
            //}
        }

        //private void cbDA_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if ((long)cbDA.SelectedValue > 0)
        //    {
        //        e.Cancel = false;
        //        errorProvider.SetError(cbDA, "");
        //    }
        //    else
        //    {
        //        e.Cancel = true;
        //        cbDA.Focus();
        //        errorProvider.SetError(cbDA, "Chưa chọn dự án");
        //    }
        //}

        private void cbKhuVuc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((long)cbKhuVuc.SelectedValue > 0)
            {
                e.Cancel = false;
                errorProvider.SetError(cbKhuVuc, "");
            }
            else
            {
                e.Cancel = true;
                cbKhuVuc.Focus();
                errorProvider.SetError(cbKhuVuc, "Chưa chọn khu vực dự án");
            }
        }

        private void tbletren_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (AppUtils.IsNumber(tbletren.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tbletren, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tbletren.Focus();
            //    errorProvider.SetError(tbletren, "Lề trên phải là số");
            //}
        }

        private void tbletrai_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (AppUtils.IsNumber(tbletrai.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tbletrai, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tbletrai.Focus();
            //    errorProvider.SetError(tbletrai, "Lề trái phải là số");
            //}
        }

        private void tblephai_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (AppUtils.IsNumber(tblephai.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tblephai, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tblephai.Focus();
            //    errorProvider.SetError(tblephai, "Lề phải phải là số");
            //}
        }

        private void tbleduoi_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (AppUtils.IsNumber(tbleduoi.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tbleduoi, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tbleduoi.Focus();
            //    errorProvider.SetError(tbleduoi, "Lề dưới phải là số");
            //}
        }

        private void tbOffsetLe_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (AppUtils.IsNumber(tbOffsetLe.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tbOffsetLe, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tbOffsetLe.Focus();
            //    errorProvider.SetError(tbOffsetLe, "Lui lề phải là số");
            //}
        }

        private void tbX_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (AppUtils.IsNumber(tbX.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tbX, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tbX.Focus();
            //    errorProvider.SetError(tbX, "X phải là số");
            //}
        }

        private void tbY_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (AppUtils.IsNumber(tbY.Text))
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(tbY, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    tbY.Focus();
            //    errorProvider.SetError(tbY, "Y phải là số");
            //}
        }

        private void comboBoxDevice_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (comboBoxDevice.SelectedIndex >= 0)
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(comboBoxDevice, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    comboBoxDevice.Focus();
            //    errorProvider.SetError(comboBoxDevice, "Chưa chọn máy vẽ");
            //}
        }

        private void comboBoxMedia_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if ((long)comboBoxMedia.SelectedIndex >= 0)
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(comboBoxMedia, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    comboBoxMedia.Focus();
            //    errorProvider.SetError(comboBoxMedia, "Chưa chọn khổ giấy");
            //}
        }

        private void comboBoxScale_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if ((long)comboBoxScale.SelectedIndex >= 0)
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(comboBoxScale, "");
            //}
            //else
            //{
            //    e.Cancel = true;
            //    comboBoxScale.Focus();
            //    errorProvider.SetError(comboBoxScale, "Chưa chọn tỷ lệ");
            //}
        }
    }
}