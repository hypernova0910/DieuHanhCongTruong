//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.Geometry;
using CoordinateSharp;
using DieuHanhCongTruong.Forms.Account;
using Microsoft.Win32;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using DieuHanhCongTruong;
using DieuHanhCongTruong.Models;

namespace DieuHanhCongTruong.Common
{
    //public class DataDatabase
    //{
    //    public Point3d point3dLocation;
    //    public byte bitSent;
    //    public DateTime DateTimeMeasureOut;
    //    public DateTime DateTimeRecvOut;
    //    public bool isMachineBom;
    //    public string code;
    //}

    //public class DataMine
    //{
    //    public Point3d point3dLocation;
    //    public DateTime DateTimePick;
    //    public int CodeProgram;
    //}

    public class DataStartAndEnd
    {
        public DateTime DateTimeStart;
        public DateTime DateTimeEnd;
        public int CodeProgram;
    }

    public class planMachinebomb
    {
        public DateTime StartTime;
        public DateTime EndTime;
    }

    internal class AppUtils
    {
        public static int _SizeX = 650;
        public static int _SizeY = 130;

        public static string DateTimeSql = "yyyy-MM-dd HH:mm:ss";
        public static string DateTimeTostring = "yyyy-MM-dd";

        public static string DateTimeShow = "hh:mm:ss dd/MM/yyyy";

        public static string[] DateTimeSqlMachine = {"h:mm:ss tt d/M/yyyy", "h:mm tt d/M/yyyy",
                         "hh:mm:ss dd/MM/yyyy", "h:mm:ss d/M/yyyy", "h:m:ss d/M/yyyy", "h:m:s d/M/yyyy",
                         "hh:mm tt d/M/yyyy", "hh tt d/M/yyyy",
                         "h:mm d/M/yyyy", "h:mm d/M/yyyy",
                         "hh:mm dd/MM/yyyy", "hh:mm dd/M/yyyy",
                           "hh:mm:ss dd/mm/yyyy",
        "H:mm:ss tt d/M/yyyy", "H:mm tt d/M/yyyy",
                         "HH:mm:ss dd/MM/yyyy", "H:mm:ss d/M/yyyy", "H:m:ss d/M/yyyy", "H:m:s d/M/yyyy",
                         "HH:mm tt d/M/yyyy", "HH tt d/M/yyyy",
                         "H:mm d/M/yyyy", "H:mm d/M/yyyy",
                         "HH:mm dd/MM/yyyy", "HH:mm dd/M/yyyy",
                           "HH:mm:ss dd/mm/yyyy"};

        public static string appXdataDatetimeMinePick = "appDateMine";
        public static bool isKhaoSat = true;
        public static int TypeMenu = 0;
        public static List<string> _ListCommandName = new List<string>();

        public static string SubMenuName = "SubMenuItem";
        public static string SubMenuNameTemp = "SubMenuItemTemp";
        public static double DefaultNanoTesla = 3278.688524590164;
        public static double DefaultNanoTeslaMin = 3278.688524590164;
        public static string ReportFolder = "ReportFolder";

        public static string GetMenuName()
        {
            if (TypeMenu == 1)
                return "RBBMMenu1.xml";
            else if (TypeMenu == 2)
                return "RBBMMenu2.xml";
            else if (TypeMenu == 3)
                return "RBBMMenu3.xml";
            else
                return "RBBMMenu.xml";
        }

        public static string GetAppPath()
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return System.IO.Path.GetDirectoryName(appPath).Trim("\\/".ToCharArray());
        }

        public static string GetAppDataPath()
        {
            return string.Format("{0}\\Data", GetAppPath());
        }

        public static MapWinGIS.Image OpenImage(string path)
        {
            //string path = @"../../data/marker.png";
            if (!File.Exists(path))
            {
                MessageBox.Show("Can't find the file: " + path);
            }
            else
            {
                MapWinGIS.Image img = new MapWinGIS.Image();
                if (!img.Open(path, MapWinGIS.ImageType.USE_FILE_EXTENSION))
                {
                    MessageBox.Show(img.ErrorMsg[img.LastErrorCode]);
                    img.Close();
                }
                else
                    return img;
            }
            return null;
        }

        //public static string GetCopyFolderDWG(int maDuAn)
        //{
        //    var strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
        //    if (string.IsNullOrEmpty(strPath))
        //    {
        //        System.Windows.Forms.MessageBox.Show("Không tìm thấy đường dẫn lưu tệp");
        //        ConfigForm frm = new ConfigForm();
        //        frm.ShowDialog();
        //        if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
        //            strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
        //        else
        //            return string.Empty;
        //    }

        //    if (System.IO.Directory.Exists(strPath + "\\" + maDuAn.ToString() + "\\Data\\") == false)
        //    {
        //        System.IO.Directory.CreateDirectory(strPath + "\\" + maDuAn.ToString() + "\\Data\\");
        //    }

        //    return strPath + "\\" + maDuAn.ToString() + "\\Data\\";
        //}

        public static string GetFolderTemp(int maDuAn)
        {
            var strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
            if (string.IsNullOrEmpty(strPath))
            {
                System.Windows.Forms.MessageBox.Show("Không tìm thấy đường dẫn lưu tệp");
                MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
                else
                    return string.Empty;
            }

            if (System.IO.Directory.Exists(strPath + "\\" + maDuAn + "\\Temp\\") == false)
            {
                System.IO.Directory.CreateDirectory(strPath + "\\" + maDuAn + "\\Temp\\");
            }

            return strPath + "\\" + maDuAn + "\\Temp\\";
        }

        //Mở rộng hàm trên chỉ dùng được khi có mã dự án
        public static string GetFolderTemp(string folderName)
        {
            var strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
            if (string.IsNullOrEmpty(strPath))
            {
                System.Windows.Forms.MessageBox.Show("Không tìm thấy đường dẫn lưu tệp");
                MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
                else
                    return string.Empty;
            }

            if (System.IO.Directory.Exists(strPath + "\\" + folderName + "\\Temp\\") == false)
            {
                System.IO.Directory.CreateDirectory(strPath + "\\" + folderName + "\\Temp\\");
            }

            return strPath + "\\" + folderName + "\\Temp\\";
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                try
                {
                    fi.Delete();
                }
                catch (Exception) { } // Ignore all exceptions
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                try
                {
                    di.Delete();
                }
                catch (Exception) { } // Ignore all exceptions
            }

            try
            {
                if (Directory.Exists(FolderName))
                    Directory.Delete(FolderName);
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static void WriteTrustPath()
        {
            //TEST
            string key = string.Format("SOFTWARE\\Autodesk\\AutoCAD\\{0}", "R21.0");

            var reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(key, true);
            if (reg != null)
            {
                var acad_cur_name = reg.GetValue("CurVer");

                if (acad_cur_name != null)
                {
                    var acad_cur = reg.OpenSubKey(string.Format("{0}", acad_cur_name));

                    //Profiles
                    var profiles = acad_cur.OpenSubKey(string.Format("{0}", "Profiles"));

                    var profile_name = profiles.GetValue(null).ToString();

                    //Get profile
                    var profile_cur = profiles.OpenSubKey(string.Format("{0}", profile_name));

                    //Dll path
                    var path = GetAppPath();
                    var folder = Path.GetDirectoryName(path);

                    //SEARCH PATH
                    var general = profile_cur.OpenSubKey(string.Format("{0}", "General"), true);
                    //Get all values
                    var values = general.GetValueNames();

                    string ACAD = string.Empty;
                    if (values.ToList().Contains("ACAD"))
                    {
                        ACAD = general.GetValue("ACAD").ToString();
                    }

                    if (ACAD.ToUpper().Contains(folder.ToUpper()) == false)
                    {
                        if (ACAD != string.Empty)
                        {
                            ACAD += ";";
                        }

                        ACAD += folder;

                        general.SetValue("ACAD", ACAD);
                    }

                    //TRUST PATH

                    //Variables
                    var variables = profile_cur.OpenSubKey(string.Format("{0}", "Variables"), true);

                    //Get all values
                    values = variables.GetValueNames();

                    string trust = string.Empty;
                    if (values.ToList().Contains("TRUSTEDPATHS"))
                    {
                        trust = variables.GetValue("TRUSTEDPATHS").ToString();
                    }

                    if (trust.ToUpper().Contains(folder.ToUpper()) == false)
                    {
                        if (trust != string.Empty)
                        {
                            trust += ";";
                        }

                        trust += folder;

                        variables.SetValue("TRUSTEDPATHS", trust);
                    }
                }
            }
        }

        public static string OpenFileDlg()
        {
            string locationstring = string.Empty;
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            locationstring = openFileDialog1.FileName;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể đọc file từ ổ đĩa: " + ex.Message);
                }
            }

            return locationstring;
        }

        //public static bool CheckLoggin()
        //{
        //    try
        //    {
        //        if (frmLoggin.sqlCon == null || frmLoggin.userPasswords == string.Empty)
        //        {
        //            MessageBox.Show("Chưa kết nối vào hệ thống, vui lòng đăng nhập vào hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        //            frmLoggin frm = new frmLoggin();
        //            frm.ShowDialog();
        //            if (frm.DialogResult == DialogResult.OK)
        //            {
        //                var cmdDangNhap = new MenuCommand();
        //                cmdDangNhap.Execute();

        //                return true;
        //            }
        //            else
        //                return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //MyLogger.Log(ex.Message);
        //    }

        //    return true;
        //}

        //public static bool CheckMenuAndCommand()
        //{
        //    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //    Editor ed = doc.Editor;
        //    object names = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CMDNAMES");
        //    string strText = names as string;
        //    if (strText.Length != 0)
        //    {
        //        if (_ListCommandName.Any(x => x.ToUpper() == strText.ToUpper()))
        //            return true;
        //        else
        //            return false;
        //    }

        //    return false;
        //}

        public static bool CheckMenuAndCommand(string strText)
        {
            if (string.IsNullOrEmpty(strText))
                return false;

            if (_ListCommandName.Any(x => x.ToUpper() == strText.ToUpper()))
                return true;
            else
                return false;
        }

        //public static bool CheckStatusFormType()
        //{
        //    return true;

        //    MenuFormFirst frm = new MenuFormFirst();
        //    frm.ShowDialog();

        //    if (frm.DialogResult == DialogResult.OK)
        //        return true;
        //    else
        //        return false;
        //}

        //public static bool CheckPermistion(string strCommand)
        //{
        //    try
        //    {
        //        Autodesk.Windows.RibbonControl ribbonControl = Autodesk.Windows.ComponentManager.Ribbon;
        //        if (ribbonControl == null)
        //            return false;

        //        var tabId = "RPBM" + "_ID";

        //        var existing = ribbonControl.Tabs.Where(x => x.Id == tabId).FirstOrDefault();
        //        if (existing == null)
        //            return false;

        //        var dataPath = AppUtils.GetAppDataPath();
        //        var fileMenuPath = string.Format("{0}\\{1}", dataPath, "RBBMMenu.xml");
        //        if (System.IO.File.Exists(fileMenuPath) == false)
        //        {
        //            string message = string.Format("Không tìm thấy menu file: {0}", fileMenuPath);
        //            //MyLogger.Log(message);
        //            return false;
        //        }

        //        XmlDocument xDoc = new XmlDocument();
        //        xDoc.Load(fileMenuPath);
        //        if (xDoc == null)
        //            return false;

        //        var elements = xDoc.GetElementsByTagName("Button");

        //        if (elements == null)
        //            return false;

        //        SqlConnection cn = frmLoggin.sqlCon;
        //        SqlCommandBuilder sqlCommand = null;
        //        SqlDataAdapter sqlAdapter = null;
        //        System.Data.DataTable datatable = null;
        //        sqlAdapter = new SqlDataAdapter(string.Format("SELECT cecm_user.mode FROM cecm_user WHERE cecm_user.user_name like '%{0}%'", frmLoggin.userName), cn);
        //        sqlCommand = new SqlCommandBuilder(sqlAdapter);
        //        datatable = new System.Data.DataTable();
        //        sqlAdapter.Fill(datatable);

        //        string quyenSD = string.Empty;
        //        foreach (System.Data.DataRow dr in datatable.Rows)
        //        {
        //            quyenSD = dr["mode"].ToString();
        //        }

        //        string[] spl = quyenSD.Split(',');
        //        if (spl.Count() == 0)
        //            return false;
        //        string valueLbx = string.Empty;

        //        List<string> listDisable = new List<string>();
        //        foreach (var val in spl)
        //        {
        //            listDisable.Add(val);
        //        }

        //        int count = -1;
        //        foreach (var xml in elements)
        //        {
        //            count++;
        //            var ele = xml as XmlElement;
        //            if (ele == null)
        //                continue;

        //            if (listDisable.Contains(count.ToString()) == false)
        //            {
        //                continue;
        //            }
        //            Autodesk.Windows.RibbonItem rbItem = existing.FindItem(ele.Attributes["name"].Value, true);
        //            if (rbItem == null)
        //                return false;

        //            Autodesk.Windows.RibbonButton button = (Autodesk.Windows.RibbonButton)rbItem;

        //            if (button == null)
        //                return false;

        //            if (button.CommandParameter.ToString() == strCommand)
        //                return false;
        //        }
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //MyLogger.Log(ex.Message);
        //        return false;
        //    }
        //}

        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        public static List<string> ListLayer(string handleSur1 = "")
        {
            List<string> listValue = new List<string>();
            if (isKhaoSat)
            {
                listValue.Add("0" + ";" + "Điểm đo" + ";" + "RB_DiemDo" + ";" + "" + ";" + "");
                listValue.Add("1" + ";" + "Quỹ đạo đo" + ";" + "RB_QuyDaoDo" + ";" + "" + ";" + "");
                listValue.Add("2" + ";" + "Bề mặt địa hình" + ";" + "RB_BeMatDiaHinh,C-TOPO-MAJR,C-TOPO-MAJR-N,C-TOPO-MINR,C-TOPO-MINR" + ";" + "" + ";" + handleSur1);
                listValue.Add("3" + ";" + "Nền bản đồ" + ";" + "RB_NenBanDo" + ";" + "" + ";" + "");
                listValue.Add("4" + ";" + "Ảnh vệ tinh" + ";" + "RB_AnhVeTinh" + ";" + "" + ";" + "");
                listValue.Add("5" + ";" + "Nhãn đường đồng mức" + ";" + "RB_NhanDuongDongMuc" + ";" + "" + ";" + "");
                listValue.Add("6" + ";" + "Đường bao dự án" + ";" + "RB_DuongBaoDuAn" + ";" + "" + ";" + "");
                listValue.Add("7" + ";" + "Đường đồng mức" + ";" + "RB_DuongDongMuc" + ";" + "" + ";" + "");
                listValue.Add("8" + ";" + "Ô lưới" + ";" + "RB_OLuoiKhaoSat" + ";" + "" + ";" + "");
                listValue.Add("9" + ";" + "Ô cùng màu" + ";" + "RB_DuongBaoCungMauKhaoSat" + ";" + "" + ";" + "");
                listValue.Add("10" + ";" + "In mảnh bản đồ" + ";" + "RB_BlockPhanManh_500KhaoSat,RB_BlockPhanManh_1000KhaoSat,RB_BlockPhanManh_2000KhaoSat,RB_BlockPhanManh_5000KhaoSat" + ";" + "" + ";" + "");
                listValue.Add("11" + ";" + "Mặt cắt từ trường" + ";" + MatCatTuTruong + ";" + "" + ";" + "");
                listValue.Add("12" + ";" + "0" + ";" + PointCloudLayer + ";" + "" + ";" + "");
            }
            else
            {
                listValue.Add("0" + ";" + "Điểm đo" + ";" + "RB_DiemDo" + ";" + "" + ";" + "");
                listValue.Add("1" + ";" + "Quỹ đạo đo" + ";" + "RB_QuyDaoDo" + ";" + "" + ";" + "");
                listValue.Add("2" + ";" + "Bề mặt địa hình" + ";" + "RB_BeMatDiaHinh,C-TOPO-MAJR,C-TOPO-MAJR-N,C-TOPO-MINR,C-TOPO-MINR" + ";" + "" + ";" + handleSur1);
                listValue.Add("3" + ";" + "Nền bản đồ" + ";" + "RB_NenBanDo" + ";" + "" + ";" + "");
                listValue.Add("4" + ";" + "Ảnh vệ tinh" + ";" + "RB_AnhVeTinh" + ";" + "" + ";" + "");
                listValue.Add("5" + ";" + "Nhãn đường đồng mức" + ";" + "RB_NhanDuongDongMuc" + ";" + "" + ";" + "");
                listValue.Add("6" + ";" + "Đường bao dự án" + ";" + "RB_DuongBaoDuAn" + ";" + "" + ";" + "");
                listValue.Add("7" + ";" + "Đường đồng mức" + ";" + "RB_DuongDongMuc" + ";" + "" + ";" + "");
                listValue.Add("8" + ";" + "Ô lưới" + ";" + "RB_OLuoiThiCong" + ";" + "" + ";" + "");
                listValue.Add("9" + ";" + "Ô cùng màu" + ";" + "RB_DuongBaoCungMauThiCong" + ";" + "" + ";" + "");
                listValue.Add("10" + ";" + "In mảnh bản đồ" + ";" + "RB_BlockPhanManh_500ThiCong,RB_BlockPhanManh_1000ThiCong,RB_BlockPhanManh_2000ThiCong,RB_BlockPhanManh_5000ThiCong" + ";" + "" + ";" + "");
                listValue.Add("11" + ";" + "Mặt cắt từ trường" + ";" + MatCatTuTruong + ";" + "" + ";" + "");
                listValue.Add("12" + ";" + "0" + ";" + PointCloudLayer + ";" + "" + ";" + "");
            }
            return listValue;
        }

        public static List<string> ListBanDoNen = new List<string>(new string[] { "Nền bản đồ", "Ảnh vệ tinh" });
        public static List<string> ListLayerBanDoNen = new List<string>(new string[] { "RB_NenBanDo", "RB_AnhVeTinh" });
        public static string DUONG_DONG_MUC = "RB_DuongDongMuc";
        public static string NHAN_DUONG_DONG_MUC = "RB_NhanDuongDongMuc";

        public static string OLuoi
        {
            get
            {
                return (isKhaoSat == true) ? "RB_OLuoiKhaoSat" : "RB_OLuoiThiCong";
            }
        }

        public static string CungMauKhaoSat = "RB_DuongBaoCungMauKhaoSat";
        public static string CungMauThiCong = "RB_DuongBaoCungMauThiCong";

        public static string DuongBaoCungMau
        {
            get
            {
                return isKhaoSat == true ? "RB_DuongBaoCungMauKhaoSat" : "RB_DuongBaoCungMauThiCong";
            }
        }

        public static string BeMatDiaHinh = "RB_BeMatDiaHinh";
        public static string QuyDaoDo = "RB_QuyDaoDo";
        public static string DiemDo = "RB_DiemDo";
        public static string MatCatTuTruong = "RB_MatCatTuTruong";
        public static string PointCloudLayer = "RB_PointCloud";

        public static string DuongBaoDuAn = "RB_DuongBaoDuAn";

        public static List<string> LopThucVat = new List<string>(new string[] { "1.Mỏng", "2.Vừa", "3.Dày" });
        public static List<string> ThoiGianQuyDinh = new List<string>(new string[] { "30", "40", "50", "60", "70", "80", "90" });
        public static List<string> LoaiVatNo = new List<string>(new string[] { "Bom đạn", "Vật nổ", "Mảnh vỡ" });
        public static string APPXDataHandleGrid = "XdataGrid";
        public static string APPXDataNumGrid = "XdataNumGrid";

        public static string m_Dictionary_SURFACE_PROFILE = "RB_SurfaceProfile";
        public static string m_Dictionary_PROFILE_SIMPLIFY = "RB_ProfileSimply";
        public static string m_Dictionary_PROFILE_POLYLINE = "RB_ProfilePolyline";
        public static string m_Dictionary_SURFACE_POINT = "RB_SurfacePoint";
        public static string m_Dictionary_TinDuAn = "RB_Tinsurface_DuAn";
        public static string m_Dictionary_LineMatCat = "RB_LineMatCat";
        public static string m_Dictionary_MatCatTimLine = "RB_MatCatTimLine";
        public static string m_Dictionary_KhoangChia = "RB_LineMatCatKhoangChia";
        public static string m_Dictionary_Point_User_Mine = "RB_Point_Point_User_Mine";
        public static string m_Dictionary_DuongBaoDuAn = "RB_Dic_DuongBaoDuAn";
        public static string m_Dictionary_KeyLastUpdate = "RB_KeyLastUpdate";
        public static string m_Dictionary_IdDuAn = "RB_Id_DuAn";
        public static string m_Dictionary_MatCatTuTruongTrongOLuoi = "RB_MatCatTuTruongTrongOLuoi";

        public static void LoadCB(System.Data.SqlClient.SqlConnection cn, System.Windows.Forms.ComboBox cb, string name, string drName)
        {
            System.Data.SqlClient.SqlCommandBuilder sqlCommand = null;
            System.Data.SqlClient.SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                // Set value Xa
                sqlAdapter = new System.Data.SqlClient.SqlDataAdapter(string.Format("SELECT dvs_name FROM cecm_department WHERE dvs_group_cd = '{0}'", name), cn);
                sqlCommand = new System.Data.SqlClient.SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                cb.Items.Clear();
                cb.Items.Add("Chọn");
                foreach (System.Data.DataRow dr in datatable.Rows)
                {
                    cb.Items.Add(dr[drName].ToString());
                }

                cb.SelectedIndex = 0;
            }
            catch
            {
            }
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

        public static void LoadCB(System.Data.SqlClient.SqlConnection cn, System.Windows.Forms.DataGridViewComboBoxColumn cb, string name, string drName)
        {
            System.Data.SqlClient.SqlCommandBuilder sqlCommand = null;
            System.Data.SqlClient.SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                // Set value Xa
                sqlAdapter = new System.Data.SqlClient.SqlDataAdapter(string.Format("SELECT dvs_name FROM cecm_department WHERE dvs_group_cd = '{0}'", name), cn);
                sqlCommand = new System.Data.SqlClient.SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                cb.Items.Clear();
                cb.Items.Add("Chọn");
                foreach (System.Data.DataRow dr in datatable.Rows)
                {
                    cb.Items.Add(dr[drName].ToString());
                }
            }
            catch
            {
            }
        }

        //public static void GetDistanceWH(Extents3d extendSource, ref double H, ref double V)
        //{
        //    Point3d newPoint1 = new Point3d(extendSource.MinPoint.X, extendSource.MaxPoint.Y, 0);
        //    Point3d newPoint2 = new Point3d(extendSource.MaxPoint.X, extendSource.MinPoint.Y, 0);
        //    H = extendSource.MinPoint.DistanceTo(newPoint1);
        //    V = extendSource.MinPoint.DistanceTo(newPoint2);
        //}

        public static string GetRecentInput(string name)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\RAPABOMMIN\\RecentInput");
                if (key == null)
                {
                    return null;
                }
                else
                {
                    var obj = key.GetValue(name);
                    if (obj != null && key.GetValueKind(name) == RegistryValueKind.String)
                    {
                        return obj.ToString();
                    }
                }
                return null;
            }
            catch (System.Exception ex)
            {
                ////MyLogger.Log("Không đọc được registry {0}", ex.Message);
                return null;
            }
        }

        public static bool SaveRecentInput(string name, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\RAPABOMMIN\\RecentInput");
                if (key == null)
                {
                    return false;
                }
                else
                {
                    key.SetValue(name, value);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                ////MyLogger.Log("Không ghi được registry {0}", ex.Message);
                return false;
            }
        }

        public static void SaveRecentInput(Control ctrl)
        {
            string key = string.Format("${0}${1}", ctrl./*Parent*/TopLevelControl.Name, ctrl.Name);

            string value = ctrl.Text;
            if (ctrl is RadioButton)
            {
                value = (ctrl as RadioButton).Checked == true ? "true" : "false";
            }
            else if (ctrl is CheckBox)
            {
                value = (ctrl as CheckBox).Checked == true ? "true" : "false";
            }
            SaveRecentInput(key, value);
        }

        public static void LoadRecentInput(Control ctrl, string def)
        {
            string key = string.Format("${0}${1}", ctrl./*Parent*/TopLevelControl.Name, ctrl.Name);
            string value = GetRecentInput(key);
            if (value != null)
            {
                if (ctrl is ComboBox)
                {
                    var combobox = ctrl as ComboBox;
                    if (combobox.Items.Count == 0)
                        return;
                    int index = combobox.FindStringExact(value);
                    if (index != -1)
                        combobox.SelectedIndex = index;
                }
                else if (ctrl is RadioButton)
                {
                    if (value == "true" || value == "false")
                    {
                        var rad = ctrl as RadioButton;
                        rad.Checked = value == "true" ? true : false;
                    }
                }
                else if (ctrl is CheckBox)
                {
                    if (value == "true" || value == "false")
                    {
                        var rad = ctrl as CheckBox;
                        rad.Checked = value == "true" ? true : false;
                    }
                }
                else
                    ctrl.Text = value;
            }
            else
            {
                if (ctrl is ComboBox || ctrl is RadioButton || ctrl is CheckBox)
                {
                    return;
                }
                if (string.IsNullOrEmpty(def) == false)
                    ctrl.Text = def;
            }
        }

        public static bool ValidateNumber(TextBox textBox)
        {
            try
            {
                if (textBox == null)
                    return false;

                double distance;
                if (double.TryParse(textBox.Text, out distance))
                {
                    if (distance < 0)
                        return false;
                    else
                        return true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox.Focus();
            }

            textBox.Focus();
            return false;
        }

        public static bool ValidateText(TextBox textBox)
        {
            try
            {
                if (textBox == null)
                    return false;

                if (string.IsNullOrEmpty(textBox.Text) == false)
                    return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox.Focus();
            }

            textBox.Focus();
            return false;
        }

        public static bool ValidateText(RichTextBox textBox)
        {
            try
            {
                if (textBox == null)
                    return false;

                if (string.IsNullOrEmpty(textBox.Text) == false)
                    return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox.Focus();
            }

            textBox.Focus();
            return false;
        }

        public static bool ValidateCombobox(ComboBox combobox)
        {
            try
            {
                if (combobox == null)
                    return false;

                if (combobox.SelectedIndex > 0)
                    return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                combobox.Focus();
            }

            combobox.DroppedDown = true;
            return false;
        }

        public static bool ValidateComboboxForm(ComboBox combobox)
        {
            try
            {
                if (combobox == null)
                    return false;

                if (combobox.SelectedIndex >= 0)
                    return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                combobox.Focus();
            }

            combobox.DroppedDown = true;
            return false;
        }

        public static void LoadCBBox(SqlConnection cn, ComboBox cb, string name)
        {
            if (cn == null || cb == null || string.IsNullOrEmpty(name))
                return;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT dvs_name FROM cecm_department WHERE dvs_group_cd = '{0}'", name), cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                cb.Items.Clear();
                cb.Items.Add("Chọn");
                foreach (System.Data.DataRow dr in datatable.Rows)
                {
                    cb.Items.Add(dr["dvs_name"].ToString());
                }
                cb.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        public static string FindValueDeparment(SqlConnection cn, string group, string value)
        {
            string retVal = string.Empty;
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                // Set value Xa
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT dvs_name FROM cecm_department WHERE dvs_group_cd = '{0}' AND dvs_value = {1}", group, value), cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                foreach (System.Data.DataRow dr in datatable.Rows)
                {
                    retVal = dr["dvs_name"].ToString();
                }
                return retVal;
            }
            catch
            {
                return string.Empty;
            }
        }

        private void LoadLaiThoiGian(SqlConnection cn)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM cecm_data"), cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            if (datatable.Rows.Count != 0)
            {
                DateTime nowdate = DateTime.Now;
                foreach (DataRow dr in datatable.Rows)
                {
                    nowdate = nowdate.AddSeconds(1);
                    int idTemp = int.Parse(dr["id"].ToString());
                    SqlCommand cmd1 = new SqlCommand(string.Format("UPDATE cecm_data SET time_action=@time_action WHERE cecm_data.id = {0}", idTemp), cn);

                    // Ngay Bat dau
                    SqlParameter NgayBatDau = new SqlParameter("@time_action", SqlDbType.DateTime);
                    NgayBatDau.Value = (nowdate.ToString());
                    cmd1.Parameters.Add(NgayBatDau);

                    var temp1 = cmd1.ExecuteNonQuery();
                }
            }
        }

        public static List<int> GetDanhSachMayDoHoatDong(SqlConnection cn, int idDuAnCha, int idDuAn)
        {
            List<int> retVal = new List<int>();
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            DataSet dataset = new DataSet();
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT id, code, description FROM cecm_machinebomb WHERE program_id = {0}", idDuAnCha), cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);

            if (datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in datatable.Rows)
                {
                    int idMachinebombPlane = int.Parse(dr["id"].ToString());
                    string nameBomb = dr["code"].ToString();
                    string descriptionBomb = dr["description"].ToString();

                    // Load chi tiet Danh Sach May do plan
                    SqlCommandBuilder sqlCommand1 = null;
                    SqlDataAdapter sqlAdapter1 = null;
                    DataSet dataset1 = new DataSet();
                    System.Data.DataTable datatable1 = new System.Data.DataTable();
                    sqlAdapter1 = new SqlDataAdapter(string.Format("SELECT plan_machineBomb.machineBomb_id from plan_machineBomb, plan_time WHERE cecm_program_id = {0} and plan_time.id = plan_machineBomb.plan_id", idDuAn), cn);
                    sqlCommand1 = new SqlCommandBuilder(sqlAdapter1);
                    sqlAdapter1.Fill(datatable1);
                    if (datatable1.Rows.Count != 0)
                    {
                        foreach (DataRow dr1 in datatable1.Rows)
                            retVal.Add((int.Parse(dr1["machineBomb_id"].ToString())));
                    }
                }
            }

            // Remove duplicate
            retVal = retVal.GroupBy(x => x).Select(y => y.First()).ToList();
            return retVal;
        }

        public static List<int> GetAllIdDuAnCon(int idDuAn, SqlConnection cn)
        {
            List<int> retVal = new List<int>();

            try
            {
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;

                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM cecm_program_area_map WHERE cecm_program_area_map.cecm_program_id = {0}", idDuAn), cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                foreach (DataRow dr in datatable.Rows)
                {
                    retVal.Add(int.Parse(dr["id"].ToString()));
                }
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }

            return retVal;
        }

        public static int GetLastIdIndentifyTable(SqlConnection cn, string nameTable)
        {
            int retVal = 0;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM {0}", nameTable), cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);

            List<int> lVal = new List<int>();
            foreach (DataRow dr in datatable.Rows)
            {
                int mId = int.Parse(dr["id"].ToString());
                lVal.Add(mId);
            }
            retVal = lVal.Max();

            return retVal;
        }

        public static string GetTenDuAnById(SqlConnection cn, int idDuAn)
        {
            string retVal = string.Empty;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT name FROM cecm_programData WHERE cecm_programData.id = {0}", idDuAn), cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                retVal = (dr["name"].ToString());
            }

            return retVal;
        }

        public static string GetParentNameDuAnById(SqlConnection cn, int idDuAn)
        {
            string retVal = string.Empty;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT parentName FROM cecm_programData WHERE cecm_programData.id = {0}", idDuAn), cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                retVal = (dr["parentName"].ToString());
            }

            return retVal;
        }

        public static string GetMaDuAnById(SqlConnection cn, int idDuAn)
        {
            string retVal = string.Empty;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT code FROM cecm_programData WHERE cecm_programData.id = {0}", idDuAn), cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                retVal = (dr["code"].ToString());
            }

            return retVal;
        }

        public static string GetMaVungDuAnById(SqlConnection cn, int idDuAn)
        {
            string retVal = string.Empty;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT code FROM cecm_program_area_map WHERE cecm_program_area_map.id = {0}", idDuAn), cn);
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                retVal = (dr["code"].ToString());
            }

            return retVal;
        }

        public static string FormatDouble(double d, int i)
        {
            var format = "F" + i;
            return d.ToString(format.ToString(), CultureInfo.InvariantCulture);
        }

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        //public static double GetDistancePoint(Point3d pt1, Point3d pt2)
        //{
        //    return Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y) + (pt1.Z - pt2.Z) * (pt1.Z - pt2.Z));
        //}

        public static bool CopyFile(string sourcePath, string desPath, bool overWite = true)
        {
            try
            {
                System.IO.File.Copy(sourcePath, desPath, overWite);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string SaveFileRPBM(string text)
        {
            string locationstring = string.Empty;
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = text;
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            locationstring = openFileDialog1.FileName;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể đọc file từ ổ đĩa: " + ex.Message);
                }
            }

            if (string.IsNullOrEmpty(locationstring) == false)
            {
                return openFileDialog1.SafeFileName;
            }
                
            else
            {
                MessageBox.Show("Mở file thất bại");
                var textError = "...";
                return textError;
            }
        }

        public static string SaveFileTxt()
        {
            //Show dialog save file
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "Lưu tệp";
            saveFileDialog.Filter = "Json Files | *.json|Text Files | *.txt";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return string.Empty;
            }
            string path = saveFileDialog.FileName;

            //Check exist
            if (System.IO.File.Exists(path) == true)
            {
                //Remove
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (System.Exception ex)
                {
                    string mess = ex.Message;
                    MessageBox.Show("Không thể lưu tệp", "Lỗi");
                    return string.Empty;
                }
            }
            return path;
        }
        public static string SaveFileJson(string text)
        {
            //Show dialog save file
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "Lưu tệp";
            saveFileDialog.Filter = "Json Files | *.json|Text Files | *.txt";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = text + "_" + Guid.NewGuid().ToString();

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return string.Empty;
            }
            string path = saveFileDialog.FileName;

            //Check exist
            if (System.IO.File.Exists(path) == true)
            {
                //Remove
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (System.Exception ex)
                {
                    string mess = ex.Message;
                    MessageBox.Show("Không thể lưu tệp", "Lỗi");
                    return string.Empty;
                }
            }
            return path;
        }
        public static string SaveExcel(string pathName)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "Đường dẫn lưu";
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "xls file (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            var dialogRe = saveFileDialog.ShowDialog();
            if (dialogRe != DialogResult.OK)
                return string.Empty;

            string pathExcel = saveFileDialog.FileName;
            string sourceFile = AppUtils.GetAppDataPath() + "\\TempExcel\\"+ pathName +".xls";
            if (System.IO.File.Exists(sourceFile) == false)
            {
                MessageBox.Show("Không tìm thấy tệp", "Lỗi");
                return string.Empty;
            }
            if (AppUtils.CopyFile(sourceFile, pathExcel, true) == false)
            {
                MessageBox.Show("Không thể copy file ", "Lỗi");
                return string.Empty;
            }
            return pathExcel;
        }
        public static string SaveFileDlg(string pathDownload)
        {
            if (System.IO.File.Exists(pathDownload) == false)
                return string.Empty;

            //Show dialog save file
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "Lưu tệp";

            string extendson = System.IO.Path.GetExtension(pathDownload);

            saveFileDialog.Filter = string.Format("{0} | *.{1}", extendson, extendson);
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = Path.GetFileName(pathDownload);

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return string.Empty;

            string path = saveFileDialog.FileName;

            //Check exist
            if (System.IO.File.Exists(path) == true)
            {
                //Remove
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (System.Exception ex)
                {
                    string mess = ex.Message;
                    MessageBox.Show("Không thể lưu tệp", "Lỗi");
                    return string.Empty;
                }
            }
            return path;
        }

        public static void SelectedComboboxItem(ComboBox cb, string strValue)
        {
            if (string.IsNullOrEmpty(strValue) || cb == null)
                return;

            if (int.TryParse(strValue, out int index))
            {
                if (index < 0)
                    return;

                if (cb.Items.Count > index)
                    cb.SelectedIndex = index;
            }
        }

        public static void SetTime(DateTimePicker dateTimeItem, string strValue)
        {
            if (string.IsNullOrEmpty(strValue) || dateTimeItem == null)
                return;

            if (DateTime.TryParse(strValue, out DateTime dateTimeValue))
                dateTimeItem.Value = dateTimeValue;
        }

        public static void ClearStartusErrorAllControl(Form form, ErrorProvider er)
        {
            var lstControl = FindControls<TextBox>(form);

            foreach (var control in lstControl)
            {
                er.SetError(control, "");
            }
        }

        public static IEnumerable<T> FindControls<T>(Control control) where T : Control
        {
            // we can't cast here because some controls in here will most likely not be <T>
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => FindControls<T>(ctrl))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == typeof(T)).Cast<T>();
        }

        public static List<PointF[]> convertMultiPolygon(CecmProgramAreaMapDTO dto, Size size)
        {
            List<PointF[]> lst = new List<PointF[]>();
            if (dto == null || size == null)
            {
                return lst;
            }
            string multiPolygon = dto.polygonGeomST;
            if (string.IsNullOrEmpty(multiPolygon))
            {
                return lst;
            }
            multiPolygon = multiPolygon.Replace("MULTI", "");
            multiPolygon = multiPolygon.Replace("POLYGON", "");
            multiPolygon = multiPolygon.Replace(")))(((", "_");
            multiPolygon = multiPolygon.Replace("(((", "");
            multiPolygon = multiPolygon.Replace(")))", "");
            //multiPolygon.Replace("((", "(");
            //multiPolygon.Replace("))", ")");
            multiPolygon = multiPolygon.Replace(")),((", "_");
            string[] polygons = multiPolygon.Split("_".ToCharArray());
            foreach (string polygon in polygons)
            {
                if (dto.left_long != null && dto.right_long != null
                    && dto.top_lat != null && dto.bottom_lat != null
                    && float.Parse(dto.left_long) < float.Parse(dto.right_long) && float.Parse(dto.bottom_lat) < float.Parse(dto.top_lat))
                {
                    List<PointF> points = new List<PointF>();
                    string[] coordinates = polygon.Split(",".ToCharArray());
                    foreach (string coordinate in coordinates)
                    {
                        string[] latLong = coordinate.Split(" ".ToCharArray());
                        double lat = double.Parse(latLong[1]);
                        double lon = double.Parse(latLong[0]);
                        //if (lat > dto.top_lat || lat < dto.bottom_lat ||
                        //   lon > dto.right_long || lon < dto.left_long)
                        //{
                        //    continue;
                        //}
                        float x = (float)(size.Width * (lon - float.Parse(dto.left_long)) / (float.Parse(dto.right_long) - float.Parse(dto.left_long)));
                        float y = (float)(size.Height * (1 - (lat - float.Parse(dto.bottom_lat)) / (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat))));
                        PointF point = new PointF(x, y);
                        points.Add(point);
                    }
                    if (points.Count > 0)
                    {
                        lst.Add(points.ToArray());
                    }
                }
            }
            return lst;
        }

        public static List<PointF[]> convertMultiPolygonNew(CecmProgramAreaMapDTO dto, Size size)
        {
            List<PointF[]> lstPolygon = new List<PointF[]>();
            if (dto == null || size == null)
            {
                return lstPolygon;
            }
            string multiPolygon = dto.polygonGeomST;
            if (string.IsNullOrEmpty(multiPolygon))
            {
                return lstPolygon;
            }
            multiPolygon = multiPolygon.Replace("POLYGON((", "");
            multiPolygon = multiPolygon.Replace("MULTI(", "");
            multiPolygon = multiPolygon.Replace("POLYGON ((", "");
            var i = multiPolygon.Length - 1;
            while (multiPolygon[i] == ')')
            {
                i--;
            }
            multiPolygon = multiPolygon.Substring(0, i);
            multiPolygon = multiPolygon.Replace(")),((", "_");
            string[] polygons = multiPolygon.Split("_".ToCharArray());
            foreach (string polygon in polygons)
            {
                //List<PointF[]> lst = new List<PointF[]>();
                if (dto.left_long != null && dto.right_long != null
                    && dto.top_lat != null && dto.bottom_lat != null
                    && float.Parse(dto.left_long) < float.Parse(dto.right_long) && float.Parse(dto.bottom_lat) < float.Parse(dto.top_lat))
                {
                    List<PointF> points = new List<PointF>();
                    string[] coordinates = polygon.Split(",".ToCharArray());
                    foreach (string coordinate in coordinates)
                    {
                        string[] latLong = coordinate.Split(" ".ToCharArray());
                        double lat = double.Parse(latLong[1]);
                        double lon = double.Parse(latLong[0]);
                        //if (lat > dto.top_lat || lat < dto.bottom_lat ||
                        //   lon > dto.right_long || lon < dto.left_long)
                        //{
                        //    continue;
                        //}
                        float x = (float)(size.Width * (lon - float.Parse(dto.left_long)) / (float.Parse(dto.right_long) - float.Parse(dto.left_long)));
                        float y = (float)(size.Height * (1 - (lat - float.Parse(dto.bottom_lat)) / (float.Parse(dto.top_lat) - float.Parse(dto.bottom_lat))));
                        PointF point = new PointF(x, y);
                        points.Add(point);
                    }
                    //if (points.Count > 0)
                    //{
                    //    lst.Add(points.ToArray());
                    //}
                    lstPolygon.Add(points.ToArray());
                }
            }
            return lstPolygon;
        }

        public static List<PointF[]> convertMultiPolygon(string multiPolygon)
        {
            List<PointF[]> lstPolygon = new List<PointF[]>();
            if (string.IsNullOrEmpty(multiPolygon))
            {
                return lstPolygon;
            }
            multiPolygon = multiPolygon.Replace("POLYGON((", "");
            multiPolygon = multiPolygon.Replace("MULTI(", "");
            multiPolygon = multiPolygon.Replace("POLYGON ((", "");
            var i = multiPolygon.Length - 1;
            while (multiPolygon[i] == ')')
            {
                i--;
            }
            multiPolygon = multiPolygon.Substring(0, i + 1);
            multiPolygon = multiPolygon.Replace(")),((", "_");
            string[] polygons = multiPolygon.Split("_".ToCharArray());
            foreach (string polygon in polygons)
            {
                //List<PointF[]> lst = new List<PointF[]>();
                List<PointF> points = new List<PointF>();
                string[] coordinates = polygon.Split(",".ToCharArray());
                foreach (string coordinate in coordinates)
                {
                    string[] latLong = coordinate.Split(" ".ToCharArray());
                    float lat = float.Parse(latLong[1]);
                    float lon = float.Parse(latLong[0]);
                    //if (lat > dto.top_lat || lat < dto.bottom_lat ||
                    //   lon > dto.right_long || lon < dto.left_long)
                    //{
                    //    continue;
                    //}
                    PointF point = new PointF(lon, lat);
                    points.Add(point);
                }
                //if (points.Count > 0)
                //{
                //    lst.Add(points.ToArray());
                //}
                lstPolygon.Add(points.ToArray());
            }
            return lstPolygon;
        }

        //public static bool CheckPointInsidePolygon(Position p, string wkt)
        //{
        //    CheckInPolygonCmd cmd = new CheckInPolygonCmd();
        //    return cmd.IsPointInPolygon(p, wkt);
        //}

        public static List<GeoJsonPolygon<GeoJson2DCoordinates>> GetPolygon(string multiPolygon)
        {
            List<GeoJsonPolygon<GeoJson2DCoordinates>> lstPolygon = new List<GeoJsonPolygon<GeoJson2DCoordinates>>();
            if (string.IsNullOrEmpty(multiPolygon))
            {
                return lstPolygon;
            }
            multiPolygon = multiPolygon.Replace("POLYGON((", "");
            multiPolygon = multiPolygon.Replace("MULTI(", "");
            multiPolygon = multiPolygon.Replace("POLYGON ((", "");
            var i = multiPolygon.Length - 1;
            while (multiPolygon[i] == ')')
            {
                i--;
            }
            multiPolygon = multiPolygon.Substring(0, i + 1);
            multiPolygon = multiPolygon.Replace(")),((", "_");
            string[] polygons = multiPolygon.Split("_".ToCharArray());
            foreach (string polygon in polygons)
            {
                //List<PointF[]> lst = new List<PointF[]>();
                //List<PointF> points = new List<PointF>();
                List<GeoJson2DCoordinates> points = new List<GeoJson2DCoordinates>();
                string[] coordinates = polygon.Split(",".ToCharArray());
                foreach (string coordinate in coordinates)
                {
                    string[] latLong = coordinate.Split(" ".ToCharArray());
                    float lat = float.Parse(latLong[1]);
                    float lon = float.Parse(latLong[0]);
                    //if (lat > dto.top_lat || lat < dto.bottom_lat ||
                    //   lon > dto.right_long || lon < dto.left_long)
                    //{
                    //    continue;
                    //}
                    //PointF point = new PointF(lon, lat);
                    points.Add(GeoJson.Position(lon, lat));
                }
                //if (points.Count > 0)
                //{
                //    lst.Add(points.ToArray());
                //}
                lstPolygon.Add(GeoJson.Polygon(points.ToArray()));
            }
            return lstPolygon;
        }

        //public static GeoJsonPolygon<GeoJson2DCoordinates> GetPolygonOld(string wkt)
        //{
        //    CheckInPolygonCmd cmd = new CheckInPolygonCmd();
        //    return cmd.convertMultiArrPolygon(wkt);
        //}

        public static string OpenFileDialogCopy(int maDuAn)
        {
            string retVal = string.Empty;

            retVal = OpenFileDlg();
            if (string.IsNullOrEmpty(retVal))
                return retVal;

            string path = GetFolderTemp(maDuAn);
            if (string.IsNullOrEmpty(path))
                return retVal;

            string fileName = System.IO.Path.GetFileName(retVal);
            var pathTemp = System.IO.Path.Combine(path, fileName);

            if (System.IO.File.Exists(pathTemp))
            {
                var extendsion = System.IO.Path.GetExtension(fileName);
                fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                pathTemp = System.IO.Path.Combine(path, fileName + DateTime.Now.Ticks.ToString() + extendsion);
            }
            path = pathTemp;
            if (CopyFile(retVal, path))
                return System.IO.Path.GetFileName(path);
            else
                return string.Empty;
        }

        public static string OpenFileDialogCopy(string folderName)
        {
            string retVal = string.Empty;

            retVal = OpenFileDlg();
            if (string.IsNullOrEmpty(retVal))
                return retVal;

            string path = GetFolderTemp(folderName);
            if (string.IsNullOrEmpty(path))
                return retVal;

            string fileName = System.IO.Path.GetFileName(retVal);
            var pathTemp = System.IO.Path.Combine(path, fileName);

            if (System.IO.File.Exists(pathTemp))
            {
                var extendsion = System.IO.Path.GetExtension(fileName);
                fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                pathTemp = System.IO.Path.Combine(path, fileName + DateTime.Now.Ticks.ToString() + extendsion);
            }
            path = pathTemp;
            if (CopyFile(retVal, path))
                return System.IO.Path.GetFileName(path);
            else
                return string.Empty;
        }

        //public static string OpenFileDialogCopyData(int maDuAn)
        //{
        //    string retVal = string.Empty;

        //    retVal = OpenFileDlg();
        //    if (string.IsNullOrEmpty(retVal))
        //        return retVal;

        //    string path = GetCopyFolderDWG(maDuAn);
        //    if (string.IsNullOrEmpty(path))
        //        return retVal;

        //    if (CopyFile(retVal, path))
        //        return System.IO.Path.GetFileName(retVal);
        //    else
        //        return string.Empty;
        //}

        public static System.Drawing.Image GetImage(string pathImage)
        {
            try
            {
                if (System.IO.File.Exists(pathImage) == false)
                    return null;

                System.Drawing.Image img = System.Drawing.Image.FromFile(pathImage);
                //img = Resize(img, _SizeX, _SizeY, false);

                return img;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return null;
            }
        }

        public static System.Drawing.Image Resize(System.Drawing.Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new System.Drawing.Bitmap(newWidth, newHeight);

            using (var graphic = System.Drawing.Graphics.FromImage(res))
            {
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }

        public static double[] ConverLatLongToUTM(double latt, double longt)
        {
            //DateTime start = DateTime.Now;
            Coordinate coordinate = new Coordinate(latt, longt);
            double xUTM = coordinate.UTM.Easting;
            double yUTM = coordinate.UTM.Northing;
            //DateTime end = DateTime.Now;
            //MessageBox.Show((end - start).TotalMilliseconds.ToString());
            return new double[] { xUTM, yUTM };
        }

        public static double DistanceLatLong(double latt1, double longt1, double latt2, double longt2)
        {
            double[] utm1 = ConverLatLongToUTM(latt1, longt1);
            double[] utm2 = ConverLatLongToUTM(latt2, longt2);
            double distance = Math.Sqrt(Math.Pow(utm1[0] - utm2[0], 2) + Math.Pow(utm1[1] - utm2[1], 2));
            return distance;
        }

        public static double DistanceUTM(double latt1, double longt1, double latt2, double longt2)
        {
            double distance = Math.Sqrt(Math.Pow(latt1 - latt2, 2) + Math.Pow(longt1 - longt2, 2));
            return distance;
        }

        public static double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double deltaX = x2 - x1;
            double deltaY = y2 - y1;
            double d = deltaX / deltaY;
            double angleRad = Math.Atan(d);
            double angle = angleRad * 180 / Math.PI;
            if(angle < 0)
            {
                angle += 360;
            }
            return angle;
        }

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

        public static Point2d ConverUTMToLatLong(double utmX, double utmY)
        {
            try
            {
                string utmZone = "48N";

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

                var longitude = ((delt / Math.PI) * 180 + s).ToString();
                var latitude = ((((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat))) / Math.PI) * 180).ToString(); // era incorrecto el calculo

                var latVal = double.Parse(latitude);
                var longVal = double.Parse(longitude);

                return new Point2d(latVal, longVal);
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
                return new Point2d(0, 0);
            }
        }

        public static bool PingHost(string nameOrAddress)
        {
            if (nameOrAddress == "localhost")
                return true;

            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        //public static List<Autodesk.AutoCAD.Colors.Color> GetColorMayBomMin(int steps, bool isMayBom)
        //{
        //    Color start = Color.Red;
        //    Color end = Color.Yellow;

        //    if (isMayBom == false)
        //    {
        //        start = Color.Green;
        //        end = Color.Blue;
        //    }

        //    //int stepA = ((end.A - start.A) / (steps - 1));
        //    if (steps == 1)
        //        steps = 10;

        //    int stepR = ((end.R - start.R) / (steps - 1));
        //    int stepG = ((end.G - start.G) / (steps - 1));
        //    int stepB = ((end.B - start.B) / (steps - 1));

        //    List<Autodesk.AutoCAD.Colors.Color> retVal = new List<Autodesk.AutoCAD.Colors.Color>();

        //    for (int i = 0; i < steps; i++)
        //    {
        //        var color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.FromArgb(90,
        //                                    start.R + (stepR * i),
        //                                    start.G + (stepG * i),
        //                                    start.B + (stepB * i)));
        //        retVal.Add(color);
        //    }

        //    return retVal;
        //}

        public static List<Color> GetColorMayBomMinWindows(int steps, bool isMayBom)
        {
            Color start = Color.Red;
            Color end = Color.Yellow;

            if (isMayBom == false)
            {
                start = Color.Green;
                end = Color.Blue;
            }

            steps = steps * 2;

            int stepA = ((end.A - start.A) / (steps - 1));
            int stepR = ((end.R - start.R) / (steps - 1));
            int stepG = ((end.G - start.G) / (steps - 1));
            int stepB = ((end.B - start.B) / (steps - 1));

            List<Color> retVal = new List<Color>();

            for (int i = 0; i < steps; i++)
            {
                var color = Color.FromArgb(start.A + (stepA * i),
                                            start.R + (stepR * i),
                                            start.G + (stepG * i),
                                            start.B + (stepB * i));
                if (i % 2 == 0)
                    retVal.Add(color);
            }

            return retVal;
        }

        //public static Point3d GetPointOnVector(Point3d pointInsert, Vector3d vectorDir, double dDistance)
        //{
        //    return (pointInsert + (vectorDir.GetNormal()) * dDistance);
        //}

        //public static Line ExtendLine(Line line, double dLen)
        //{
        //    if (line == null)
        //        return null;

        //    if (dLen <= 0)
        //        return line;

        //    var startP = GetPointOnVector(line.StartPoint, line.StartPoint - line.EndPoint, dLen);
        //    var endP = GetPointOnVector(line.EndPoint, line.EndPoint - line.StartPoint, dLen);
        //    return new Line(startP, endP);
        //}
    }
}