using DieuHanhCongTruong.Properties;
using MongoDB.Driver;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DieuHanhCongTruong.Common;
//using VNRaPaBomMin.Command;
//using VNRaPaBomMin.Properties;
//using VNRaPaBomMin.Utlils;

//using VNRaPaBomMin.Command;

namespace DieuHanhCongTruong.Forms.Account
{
    public partial class frmLoggin : Form
    {
        public static string ipAddr = "", databaseName = "DB_CECM", userName = "sa", userPasswords = "";
        public static SqlConnection sqlCon = null;
        public static MongoClient mgCon = null;

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public frmLoggin()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (tbDatabaseName.Text.Length == 0)
            {
                MessageBox.Show("Nhập tên cơ sở dữ liệu", "Lỗi khi lưu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbDatabaseName.Focus();
                return;
            }
            Settings.Default["IpAddr"] = tbIpAddr.Text;
            Settings.Default["DatabaseName"] = tbDatabaseName.Text;
            Settings.Default.Save();

            ExecuteOKBtn();
        }

        private void frmLoggin_Load(object sender, EventArgs e)
        {
            try
            {
                tbIpAddr.Text = Settings.Default["IpAddr"].ToString();
                tbDatabaseName.Text = Settings.Default["DatabaseName"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            // load config
            if (Settings.Default["IpAddr"] != null && Settings.Default["DatabaseName"] != null && Settings.Default["UserName"] != null)
            {
                //Test
                tbPassword.Text = "Vuthelam1608";
                //tbDatabaseName.Text = "DB_CECM"; tbUserName.Text = "sa";

                tbDatabaseName.Text = Settings.Default["DatabaseName"].ToString();
                tbUserName.Text = Settings.Default["UserName"].ToString();
            }
        }

        private void ExecuteOKBtn()
        {
            if (tbUserName.Text.Length == 0)
            {
                MessageBox.Show("Nhập tên đăng nhập", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbUserName.Focus();
                return;
            }
            if (tbPassword.Text.Length == 0)
            {
                MessageBox.Show("Nhập mật khẩu đăng nhập", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbPassword.Focus();
                return;
            }

            try
            {
                ipAddr = tbIpAddr.Text;
                userName = tbUserName.Text;
                userPasswords = tbPassword.Text;

                sqlCon = Logincmd.connectSever(ipAddr, databaseName, userName, userPasswords);
                mgCon = new MongoClient("mongodb://localhost:27017");
                if (sqlCon != null)
                {
                    if (userName != "sa")
                    {
                        // check time login
                        SqlCommandBuilder sqlCommand = null;
                        SqlDataAdapter sqlAdapter = null;
                        DataTable datatable = new DataTable();
                        sqlAdapter = new SqlDataAdapter(String.Format("SELECT start_day, end_day, day_use, start_time, end_time FROM cecm_user where user_name = '{0}'", userName), sqlCon);
                        sqlCommand = new SqlCommandBuilder(sqlAdapter);
                        sqlAdapter.Fill(datatable);
                        if (datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr in datatable.Rows)
                            {
                                DateTime timeNgayBatDau = (DateTime)dr["start_day"];
                                DateTime timeNgayketThuc = (DateTime)dr["end_day"];
                                int tbNgaySuDung = (int)dr["day_use"];
                                TimeSpan timeThoiGianSuDngTu;
                                TimeSpan.TryParseExact(dr["start_time"].ToString(), "g", System.Globalization.CultureInfo.CurrentCulture, out timeThoiGianSuDngTu);
                                TimeSpan timeThoiGianSuDungToi;
                                TimeSpan.TryParseExact(dr["end_time"].ToString(), "g", System.Globalization.CultureInfo.CurrentCulture, out timeThoiGianSuDungToi);

                                if (DateTime.Compare(timeNgayBatDau, DateTime.Now) > 0)
                                {
                                    MessageBox.Show(String.Format("Chưa đến thời gian sử dụng, thời gian sử dụng của User {0} bắt đầu từ {1}", userName, timeNgayBatDau.ToString()));
                                    sqlCon.Close();
                                    return;
                                }
                                if (DateTime.Compare(timeNgayketThuc, DateTime.Now) < 0)
                                {
                                    MessageBox.Show(String.Format("Đã hết thời gian sử dụng, thời gian sử dụng của User {0} kết thúc lúc {1}", userName, timeNgayketThuc.ToString()));
                                    sqlCon.Close();
                                    return;
                                }
                                if (TimeSpan.Compare(timeThoiGianSuDngTu, DateTime.Now.TimeOfDay) > 0)
                                {
                                    MessageBox.Show(String.Format("Chưa đến thời gian sử dụng, thời gian sử dụng trong ngày của User {0} bắt đầu từ {1}", userName, timeThoiGianSuDngTu.ToString()));
                                    sqlCon.Close();
                                    return;
                                }
                                if (TimeSpan.Compare(timeThoiGianSuDungToi, DateTime.Now.TimeOfDay) < 0)
                                {
                                    MessageBox.Show(String.Format("Đã hết thời gian sử dụng, thời gian sử dụng trong ngày của User {0} kết thúc lúc {1}", userName, timeThoiGianSuDungToi.ToString()));
                                    sqlCon.Close();
                                    return;
                                }
                                int date = 0;
                                switch (DateTime.Now.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                        date = 2;
                                        break;

                                    case DayOfWeek.Tuesday:
                                        date = 3;
                                        break;

                                    case DayOfWeek.Wednesday:
                                        date = 4;
                                        break;

                                    case DayOfWeek.Thursday:
                                        date = 5;
                                        break;

                                    case DayOfWeek.Friday:
                                        date = 6;
                                        break;

                                    case DayOfWeek.Saturday:
                                        date = 7;
                                        break;

                                    case DayOfWeek.Sunday:
                                        date = 8;
                                        break;

                                    default:
                                        date = 0;
                                        break;
                                }
                                if (!tbNgaySuDung.ToString().Contains(date.ToString()))
                                {
                                    MessageBox.Show(string.Format("Thời gian sử dụng trong tuần của User {0} là {1}", userName, tbNgaySuDung));
                                    sqlCon.Close();
                                    return;
                                }
                            }
                        }
                    }

                    UtilsDatabase._ExtraInfoConnettion = new ConnectionWithExtraInfo(frmLoggin.sqlCon);

                    // Update Resource
                    Settings.Default["UserName"] = tbUserName.Text;
                    Settings.Default.Save();
                    MessageBox.Show(string.Format("Xin chào {0} bạn đã đăng nhập thành công", tbUserName.Text));

                    // Tim user
                    //QuyenNguoiSuDungCMD cmd = new QuyenNguoiSuDungCMD();
                    //cmd.GetRibbonTab(userName);

                    

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Không thể đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}