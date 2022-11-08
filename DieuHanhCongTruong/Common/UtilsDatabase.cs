using CoordinateSharp;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.Common
{
    public class TableName
    {
        public static string DIEU_TRA = "DIEU_TRA";
        public static string KHAO_SAT = "KHAO_SAT";
        public static string RA_PHA = "RA_PHA";
        public static string TUYEN_TRUYEN_GIAO_DUC = "TUYEN_TRUYEN_GIAO_DUC";
        public static string HO_TRO_NAN_NHAN = "HO_TRO_NAN_NHAN";
    }

    internal class UtilsDatabase
    {
        public static ConnectionWithExtraInfo _ExtraInfoConnettion = null;

        public static void LoadTinh(ConnectionWithExtraInfo connectionInfo, System.Windows.Forms.ComboBox cbBox)
        {
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT Ten, id FROM cecm_provinces where level = 1", cn);
            sqlAdapterProvince.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);
            cbBox.Items.Add("Chọn");
            foreach (DataRow dr in datatableProvince.Rows)
            {
                if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                    continue;

                TinhObject tinh = new TinhObject();
                tinh.name = dr["Ten"].ToString();
                tinh.id = dr["id"].ToString();

                cbBox.Items.Add(tinh);
            }

            if (cbBox.Items.Count != 0)
                cbBox.SelectedIndex = 0;
        }

        public static void LoadTinhComboboxCheckbox(ConnectionWithExtraInfo connectionInfo, System.Windows.Forms.ComboBox cbBox)
        {
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT Ten, id FROM cecm_provinces where level = 1", cn);
            sqlAdapterProvince.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);
            foreach (DataRow dr in datatableProvince.Rows)
            {
                if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                    continue;

                TinhObject tinh = new TinhObject();
                tinh.name = dr["Ten"].ToString();
                tinh.id = dr["id"].ToString();

                cbBox.Items.Add(tinh);
            }

            if (cbBox.Items.Count != 0)
                cbBox.SelectedIndex = 0;
        }

        public static void LoadCBDonVi(ConnectionWithExtraInfo connectionInfo, System.Windows.Forms.ComboBox cbTenDonVi)
        {
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            SqlConnection cn = connectionInfo.Connection as SqlConnection;

            try
            {
                // Set value Xa
                sqlAdapter = new SqlDataAdapter("SELECT name,id FROM cert_department", cn);
                sqlAdapter.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                cbTenDonVi.Items.Clear();
                cbTenDonVi.Items.Add("Chọn");

                foreach (DataRow dr in datatable.Rows)
                {
                    TinhObject tinh = new TinhObject();
                    tinh.name = dr["name"].ToString();
                    tinh.id = dr["id"].ToString();

                    cbTenDonVi.Items.Add(tinh);
                }
                cbTenDonVi.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        public static void LoadHuyen(ConnectionWithExtraInfo connectionInfo, System.Windows.Forms.ComboBox cbBoxTinh, System.Windows.Forms.ComboBox cbBoxHuyen)
        {
            if (cbBoxTinh == null || cbBoxHuyen == null)
                return;

            if (cbBoxTinh.SelectedIndex < 0)
                return;

            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return;

            int tinhId = 0;
            string tinhItem = cbBoxTinh.SelectedItem.ToString();

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", tinhItem), cn);
            sqlAdapterWard.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            System.Data.DataTable datatableWard = new System.Data.DataTable();
            sqlAdapterWard.Fill(datatableWard);

            foreach (DataRow dr in datatableWard.Rows)
                tinhId = int.Parse(dr["id"].ToString());

            SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id, Ten FROM cecm_provinces where level = 2 and parent_id = {0}", tinhId), cn);
            sqlAdapterCounty.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
            System.Data.DataTable datatableCounty = new System.Data.DataTable();
            sqlAdapterCounty.Fill(datatableCounty);
            cbBoxHuyen.Items.Clear();
            cbBoxHuyen.Items.Add("Chọn");
            foreach (DataRow dr in datatableCounty.Rows)
            {
                if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                    continue;

                TinhObject tinh = new TinhObject();
                tinh.name = dr["Ten"].ToString();
                tinh.id = dr["id"].ToString();

                cbBoxHuyen.Items.Add(tinh);
            }
        }

        public static void LoadCBDA(ComboBox cb)
        {
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter("SELECT id, name FROM cecm_programData", _ExtraInfoConnettion.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["name"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            cb.DataSource = datatableProgram;
            cb.ValueMember = "id";
            cb.DisplayMember = "name";
        }

        public static void buildComboboxMST(ComboBox cb, string cd)
        {
            string sql = "SELECT dvs_name, dvs_value FROM mst_division WHERE dvs_group_cd = '" + cd + "'";
            buildCombobox(cb, sql, "dvs_value", "dvs_name");
        }

        public static void buildCombobox(ComboBox cb, string sql, string valueMember, string displayMember)
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
            DataTable datatable = new DataTable();
            sqlAdapter.Fill(datatable);
            DataRow dr = datatable.NewRow();
            dr[valueMember] = -1;
            dr[displayMember] = "Chưa chọn";
            datatable.Rows.InsertAt(dr, 0);
            cb.DataSource = datatable;
            cb.ValueMember = valueMember;
            cb.DisplayMember = displayMember;
        }
        public static void comboboxRPBM(ComboBox cb, string sql, string valueMember, string displayMember)
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, _ExtraInfoConnettion.Connection as SqlConnection);
            DataTable datatable = new DataTable();
            sqlAdapter.Fill(datatable);
            DataRow dr = datatable.NewRow();
            dr[valueMember] = -1;
            dr[displayMember] = "Chọn";
            datatable.Rows.InsertAt(dr, 0);
            cb.DataSource = datatable;
            cb.ValueMember = valueMember;
            cb.DisplayMember = displayMember;
        }
        public static void LoadCBStaff(ComboBox cb, int DuanId)
        {
            cb.DataSource = null;
            //if (comboBox_TenDA.SelectedValue is long)
            //{
            comboboxRPBM(cb, "SELECT id, nameId FROM Cecm_ProgramStaff where cecmProgramId = " + DuanId.ToString(), "id", "nameId");
            //}

        }
        public static void LoadCBDept(ComboBox cb, int DuanId)
        {
            cb.DataSource = null;
            //if (comboBox_TenDA.SelectedValue is long)
            //{
            comboboxRPBM(cb, 
                "select " +
                "CONCAT(d.name, dept_other) as dept_idST, " +
                "dtg.phone, " +
                "dtg.email, " +
                "dtg.address, " +
                "dtg.fax , " +
                "dept_id_web as 'gid'," +
                "dtg.cecm_program_id," +
                "dtg.table_name " +
                "from dept_tham_gia dtg " +
                "left join cert_department d on CASE WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1 WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1 ELSE 0 END = 1 " +
                "WHERE dtg.cecm_program_id = " + DuanId.ToString()+" " +
                "and dtg.dept_id_web IS NOT NULL", "gid", "dept_idST");
            //}

        }
        public static void DeleteMemberByTablename(string table_name)
        {
            SqlConnection cn = frmLoggin.sqlCon;
            SqlCommand cmd2 = new SqlCommand("DELETE FROM groundDeliveryRecordsMember WHERE main_id = 0 and table_name = N'" + table_name + "'", cn);
            int temp = 0;
            temp = cmd2.ExecuteNonQuery();
        }
        public static void checkNumberForm(ComboBox cb, ErrorProvider errorProvider1, CancelEventArgs e)
        {
            if (cb.Text == "" || !Double.TryParse(cb.Text, out double A))
            {
                e.Cancel = true;
                //comboBox_Tinh.Focus();
                errorProvider1.SetError(cb, "Chưa chọn tỉnh");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cb, "");
            }
        }
        public static void DeleteMemberByMainId(string table_name,int mainid)
        {
            SqlConnection cn = frmLoggin.sqlCon;
            SqlCommand cmd2 = new SqlCommand("DELETE FROM groundDeliveryRecordsMember WHERE main_id = "+mainid+" and table_name = N'" + table_name + "'", cn);
            int temp = 0;
            temp = cmd2.ExecuteNonQuery();
        }
        public static void LoadCBGroup(ComboBox cb, string group)
        {
            cb.DataSource = null;
            //if (comboBox_TenDA.SelectedValue is long)
            //{
            comboboxRPBM(cb, "SELECT * FROM cecm_department WHERE dvs_group_cd = "+ group, "dvs_value", "dvs_name");
            //}

        }
        public static void LoadDVKS(ComboBox cb, long idDA)
        {
            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter(
                string.Format(@"SELECT id, name 
                FROM cert_department 
                WHERE id_web in (SELECT dept_id_web FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0}) 
                or id in (SELECT dept_id FROM dept_tham_gia WHERE table_name = '{1}' and cecm_program_id = {0})", idDA, TableName.KHAO_SAT), _ExtraInfoConnettion.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableStaff = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableStaff);
            DataRow drStaff = datatableStaff.NewRow();
            drStaff["id"] = -1;
            drStaff["name"] = "Chưa chọn";
            datatableStaff.Rows.InsertAt(drStaff, 0);
            cb.DataSource = datatableStaff;
            cb.ValueMember = "id";
            cb.DisplayMember = "name";
        }

        public static void LoadXa(ConnectionWithExtraInfo connectionInfo, System.Windows.Forms.ComboBox cbBoxHuyen, System.Windows.Forms.ComboBox cbBoxXa)
        {
            if (cbBoxHuyen == null || cbBoxXa == null)
                return;

            if (cbBoxHuyen.SelectedIndex < 0)
                return;

            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return;

            int huyenId = 0;
            string huyenItem = cbBoxHuyen.SelectedItem.ToString();

            SqlDataAdapter sqlAdapterCounty = new SqlDataAdapter(string.Format("SELECT id FROM cecm_provinces where Ten = N'{0}'", huyenItem), cn);
            sqlAdapterCounty.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterCounty);
            System.Data.DataTable datatableCounty = new System.Data.DataTable();
            sqlAdapterCounty.Fill(datatableCounty);

            foreach (DataRow dr in datatableCounty.Rows)
                huyenId = int.Parse(dr["id"].ToString());

            SqlDataAdapter sqlAdapterWard = new SqlDataAdapter(string.Format("SELECT id, Ten FROM cecm_provinces where level = 3 and parentiddistrict = {0}", huyenId), cn);
            sqlAdapterWard.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapterWard);
            System.Data.DataTable datatableWard = new System.Data.DataTable();
            sqlAdapterWard.Fill(datatableWard);
            cbBoxXa.Items.Clear();
            cbBoxXa.Items.Add("Chọn");
            foreach (DataRow dr in datatableWard.Rows)
            {
                if (string.IsNullOrEmpty(dr["Ten"].ToString()))
                    continue;

                TinhObject tinh = new TinhObject();
                tinh.name = dr["Ten"].ToString();
                tinh.id = dr["id"].ToString();

                cbBoxXa.Items.Add(tinh);
            }
        }

        public static void LoadCBTinh(ComboBox cb)
        {
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 1", _ExtraInfoConnettion.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            cb.DataSource = datatableProgram;
            cb.ValueMember = "id";
            cb.DisplayMember = "Ten";
        }

        public static void LoadCBHuyen(ComboBox cb, long TinhID)
        {
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 2 AND parent_id = " + TinhID, _ExtraInfoConnettion.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            cb.DataSource = datatableProgram;
            cb.ValueMember = "id";
            cb.DisplayMember = "Ten";
        }

        public static void LoadCBXa(ComboBox cb, long HuyenID)
        {
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter("SELECT id, Ten FROM cecm_provinces WHERE level = 3 AND parentiddistrict = " + HuyenID, _ExtraInfoConnettion.Connection as SqlConnection);
            //sqlAdapterProgram.SelectCommand.Transaction = _cn.Transaction as SqlTransaction;
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProgram);
            DataRow drProgram = datatableProgram.NewRow();
            drProgram["id"] = -1;
            drProgram["Ten"] = "Chưa chọn";
            datatableProgram.Rows.InsertAt(drProgram, 0);
            cb.DataSource = datatableProgram;
            cb.ValueMember = "id";
            cb.DisplayMember = "Ten";
        }

        public static SqlConnection connectSever(string ipAddr, string databaseName, string userName, string userPasswords)
        {
            SqlConnection cn = null;

            try
            {
                if (string.IsNullOrEmpty(ipAddr) || ipAddr == "...")
                    ipAddr = Environment.MachineName;

                cn = new SqlConnection(String.Format("server={0};", ipAddr) + String.Format("Initial Catalog={0};", databaseName) + String.Format("User id={0};", userName) + String.Format("Password={0};", userPasswords));
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Open();
                return cn;
            }
            catch (Exception ex)
            {
                var mess = ex.Message;

                var lstSeverName = GetDataSources2();
                foreach (var severName in lstSeverName)
                {
                    try
                    {
                        cn = new SqlConnection(String.Format("server={0};", severName) + String.Format("Initial Catalog={0};", databaseName) + String.Format("User id={0};", userName) + String.Format("Password={0};", userPasswords));
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                        cn.Open();

                        return cn;
                    }
                    catch (Exception ex1)
                    {
                        var mess1 = ex1.Message;
                    }
                }

                return null;
            }
        }

        private static List<string> GetDataSources2()
        {
            List<string> retVal = new List<string>();

            string ServerName = Environment.MachineName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        if (instanceName == "MSSQLSERVER")
                            continue;

                        retVal.Add(ServerName + "\\" + instanceName);
                    }
                }
            }

            return retVal;
        }

        public static void LoadCBItemDatabaseGroup(ConnectionWithExtraInfo connectionInfo, System.Windows.Forms.ComboBox cb, string name)
        {
            if (connectionInfo == null || cb == null || string.IsNullOrEmpty(name))
                return;

            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return;
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT dvs_name FROM cecm_department WHERE dvs_group_cd = '{0}'", name), cn);
                sqlAdapter.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);
                cb.Items.Clear();
                cb.Items.Add("Chọn");
                foreach (System.Data.DataRow dr in datatable.Rows)
                    cb.Items.Add(dr["dvs_name"].ToString());

                cb.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
            }
        }

        public static string GetNameDatabaseGroup(ConnectionWithExtraInfo connectionInfo, int indexSelect, string code)
        {
            if (connectionInfo == null || indexSelect < 0 || string.IsNullOrEmpty(code))
                return string.Empty;

            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return string.Empty;
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = null;
            try
            {
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM cecm_department WHERE dvs_group_cd = '{0}'", code), cn);
                sqlAdapter.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                datatable = new System.Data.DataTable();
                sqlAdapter.Fill(datatable);

                foreach (System.Data.DataRow dr in datatable.Rows)
                {
                    if (int.TryParse(dr["dvs_value"].ToString(), out int index) == false)
                        continue;

                    if (indexSelect == index)
                        return dr["dvs_name"].ToString();
                }

                return string.Empty;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return string.Empty;
            }
        }

        public static List<DataRow> GetAllDataInTable(ConnectionWithExtraInfo connectionInfo, string tableName)
        {
            List<DataRow> lstDatarow = new List<DataRow>();
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return lstDatarow;

            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter(string.Format("SELECT * FROM {0}", tableName), cn);
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);

            foreach (DataRow dr in datatableProgram.Rows)
                lstDatarow.Add(dr);

            return lstDatarow;
        }

        public static List<DataRow> GetAllDataInTableWithId(ConnectionWithExtraInfo connectionInfo, string tableName, string idColumnName, string idVal)
        {
            List<DataRow> lstDatarow = new List<DataRow>();
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return lstDatarow;

            SqlDataAdapter sqlAdapterProgram = new SqlDataAdapter(string.Format("SELECT * FROM {0} where {1} = {2}", tableName, idColumnName, idVal), cn);
            sqlAdapterProgram.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProgram);
            System.Data.DataTable datatableProgram = new System.Data.DataTable();
            sqlAdapterProgram.Fill(datatableProgram);

            foreach (DataRow dr in datatableProgram.Rows)
                lstDatarow.Add(dr);

            return lstDatarow;
        }

        public static void PopularComboboxJson(System.Windows.Forms.ComboBox cbBox, string jsonStr, bool isString = false)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return;
            if (isString == false)
            {
                if (int.TryParse(jsonStr, out int index) == false)
                    return;

                if (cbBox.Items.Count <= index)
                    return;

                cbBox.SelectedIndex = index;
            }
            else
            {
                int indexFind = cbBox.FindStringExact(jsonStr);
                if (indexFind >= 0)
                    cbBox.SelectedIndex = indexFind;
            }
        }

        public static void PopularComboboxTinhJson(System.Windows.Forms.ComboBox cbBox, string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return;

            if (int.TryParse(jsonStr, out int index) == false)
                return;

            for (int i = 0; i < cbBox.Items.Count; i++)
            {
                TinhObject tinh = cbBox.Items[i] as TinhObject;
                if (tinh == null)
                    continue;

                if (tinh.id == jsonStr)
                {
                    cbBox.SelectedIndex = i;
                    return;
                }
            }
        }

        public static void PopularComboboxTextboxTinhJson(PresentationControls.CheckBoxComboBox cbBox, List<string> lstJsonStr)
        {
            if (lstJsonStr.Count == 0)
                return;

            foreach (var jsonStr in lstJsonStr)
            {
                if (string.IsNullOrEmpty(jsonStr))
                    continue;

                if (int.TryParse(jsonStr, out int index) == false)
                    continue;

                for (int i = 0; i < cbBox.Items.Count; i++)
                {
                    TinhObject tinh = cbBox.Items[i] as TinhObject;
                    if (tinh == null)
                        continue;

                    if (tinh.id == jsonStr)
                        cbBox.CheckBoxItems[i].Checked = true;
                }
            }
        }

        public static void PopularStringTextboxJson(System.Windows.Forms.TextBox textboxStr, string jsonStr)
        {
            textboxStr.Text = string.Empty;

            if (string.IsNullOrEmpty(jsonStr))
                return;

            textboxStr.Text = jsonStr;
        }

        public static void PopularStringTextboxJson(System.Windows.Forms.RichTextBox textboxStr, string jsonStr)
        {
            textboxStr.Text = string.Empty;

            if (string.IsNullOrEmpty(jsonStr))
                return;

            textboxStr.Text = jsonStr;
        }

        public static void PopularStringPolygonTextboxJson(System.Windows.Forms.RichTextBox textboxStr, string jsonStr)
        {
            textboxStr.Text = string.Empty;

            if (string.IsNullOrEmpty(jsonStr))
                return;

            var lstPoint = GetPointOfPolygonDatabase(jsonStr, false);

            foreach (var item in lstPoint)
            {
                textboxStr.Text = textboxStr.Text + string.Format("{0},{1}\n", item.X, item.Y);
            }
        }

        public static List<Point2d> GetPointOfPolygonDatabase(string dbValue, bool isConvert)
        {
            List<Point2d> retVal = new List<Point2d>();

            try
            {
                dbValue = dbValue.Trim();
                if (dbValue.Length <= 18)
                    return retVal;
                dbValue = dbValue.Substring(15, dbValue.Length - 15 - 3);

                var splitVals = dbValue.Split(',');
                foreach (var str in splitVals)
                {
                    var splitArr = str.Split(' ');
                    if (splitArr.Length != 2)
                        continue;

                    var str1 = splitArr[0].Trim();
                    var str2 = splitArr[1].Trim();

                    if (AppUtils.IsNumber(str1) && AppUtils.IsNumber(str2))
                    {
                        var dx = double.Parse(str1);
                        var dy = double.Parse(str2);

                        try
                        {
                            if (isConvert)
                            {
                                Coordinate cWGS84 = new Coordinate(dy, dx);
                                Point2d pt2d = new Point2d(cWGS84.UTM.Easting, cWGS84.UTM.Northing);

                                retVal.Add(pt2d);
                            }
                            else
                            {
                                Point2d pt2d = new Point2d(dx, dy);

                                retVal.Add(pt2d);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            var mess = ex.Message;
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return retVal;
        }

        public static void PopularStringTextboxJson(System.Windows.Forms.Label textboxStr, string jsonStr)
        {
            textboxStr.Text = string.Empty;

            if (string.IsNullOrEmpty(jsonStr))
                return;

            textboxStr.Text = jsonStr;
        }

        public static void PopularNumberTextboxJson(System.Windows.Forms.TextBox textboxStr, string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return;

            if (double.TryParse(jsonStr, out double dVal) == false)
                return;

            textboxStr.Text = dVal.ToString();
        }

        public static void PopularDatetimeTextboxJson(System.Windows.Forms.DateTimePicker datetimeTextbox, string jsonStr)
        {
            datetimeTextbox.Text = string.Empty;

            if (string.IsNullOrEmpty(jsonStr))
                return;

            DateTime datetime = DateTime.ParseExact(jsonStr, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);

            datetimeTextbox.Value = datetime;
        }

        public static void PopularComboboxToCheckboxJson(List<System.Windows.Forms.CheckBox> lstcbk, List<string> lstJsonStr, System.Windows.Forms.TextBox tbxDefault = null)
        {
            if (lstJsonStr == null || lstJsonStr.Count == 0)
                return;

            for (int i = 0; i < lstJsonStr.Count; i++)
            {
                string str = lstJsonStr[i];

                if (int.TryParse(str, out int intVal) == false)
                {
                    if (tbxDefault != null)
                        tbxDefault.Text = str;

                    continue;
                }

                if (lstcbk.Count - 1 >= intVal)
                    lstcbk[intVal].Checked = true;
            }
        }

        public static void UpdateDataSqlParameter(SqlCommand cmd, string paraName, System.Windows.Forms.TextBox tbx, bool isNumber, SqlDbType SqlDbTypeNumber = SqlDbType.BigInt)
        {
            SqlParameter sqlPara = null;

            if (isNumber)
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbTypeNumber);
            else
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.NVarChar, 255);

            if (tbx == null || string.IsNullOrEmpty(tbx.Text))
            {
                if (isNumber)
                    sqlPara.Value = 0;
                else
                    sqlPara.Value = string.Empty;
            }
            else
                sqlPara.Value = tbx.Text;
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameter(SqlCommand cmd, string paraName, string text, bool isNumber, SqlDbType SqlDbTypeNumber = SqlDbType.BigInt)
        {
            SqlParameter sqlPara = null;

            if (isNumber)
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbTypeNumber);
            else
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.NVarChar, 255);

            if (string.IsNullOrEmpty(text))
            {
                if (isNumber)
                    sqlPara.Value = 0;
                else
                    sqlPara.Value = string.Empty;
            }
            else
                sqlPara.Value = text;
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameter(SqlCommand cmd, string paraName, System.Windows.Forms.Label tbx, bool isNumber, SqlDbType SqlDbTypeNumber = SqlDbType.BigInt)
        {
            SqlParameter sqlPara = null;

            if (isNumber)
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbTypeNumber);
            else
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.NVarChar, 255);

            if (tbx == null || string.IsNullOrEmpty(tbx.Text))
            {
                if (isNumber)
                    sqlPara.Value = 0;
                else
                    sqlPara.Value = string.Empty;
            }
            else
                sqlPara.Value = tbx.Text;
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameter(SqlCommand cmd, string paraName, System.Windows.Forms.RichTextBox tbx, bool isNumber, SqlDbType SqlDbTypeNumber = SqlDbType.BigInt)
        {
            SqlParameter sqlPara = null;

            if (isNumber)
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbTypeNumber);
            else
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.NVarChar, 1000);

            if (tbx == null || string.IsNullOrEmpty(tbx.Text))
            {
                if (isNumber)
                    sqlPara.Value = 0;
                else
                    sqlPara.Value = string.Empty;
            }
            else
                sqlPara.Value = tbx.Text;
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataPolygonSqlParameter(SqlCommand cmd, string paraName, System.Windows.Forms.RichTextBox tbx)
        {
            var sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.NVarChar, 8000);

            if (tbx == null || string.IsNullOrEmpty(tbx.Text))
                sqlPara.Value = string.Empty;
            else
            {
                var text = tbx.Text;

                string[] stringSeparators = new string[] { "\n" };
                string[] lines = text.Split(stringSeparators, StringSplitOptions.None);

                string newLineData = string.Empty;

                foreach (var lineData in lines)
                {
                    var split = lineData.Split(',');
                    if (split.Length != 2)
                        continue;

                    newLineData = newLineData + string.Format("{0} {1},", split[0], split[1]);
                }

                newLineData = newLineData.Remove(newLineData.Length - 1, 1);

                newLineData = string.Format("MULTIPOLYGON((({0})))", newLineData);
                sqlPara.Value = newLineData;
            }
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameter(SqlCommand cmd, string paraName, System.Windows.Forms.ComboBox cbx, bool isNumber, SqlDbType SqlDbTypeNumber = SqlDbType.BigInt)
        {
            SqlParameter sqlPara = null;

            if (isNumber)
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbTypeNumber);
            else
                sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.NVarChar, 255);

            if (cbx == null || cbx.SelectedIndex == -1)
            {
                if (isNumber)
                    sqlPara.Value = 0;
                else
                    sqlPara.Value = string.Empty;
            }
            else
                sqlPara.Value = cbx.SelectedIndex;
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameterComboboxCheckbox(SqlCommand cmd, string paraName, PresentationControls.CheckBoxComboBox cbx)
        {
            SqlParameter sqlPara = null;

            sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.NVarChar, 255);

            string strVal = string.Empty;
            for (int i = 0; i < cbx.Items.Count; i++)
            {
                if (cbx.CheckBoxItems[i].Checked)
                {
                    strVal = strVal + "," + i.ToString() + ",";
                }
            }

            sqlPara.Value = strVal;
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameterTag(SqlCommand cmd, string paraName, System.Windows.Forms.ComboBox cbx, SqlDbType SqlDbTypeNumber = SqlDbType.BigInt)
        {
            SqlParameter sqlPara = null;

            sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbTypeNumber);

            if (cbx == null || cbx.SelectedIndex == -1)
                sqlPara.Value = 0;
            else
            {
                var obj = cbx.SelectedItem as TinhObject;
                if (obj == null)
                    sqlPara.Value = cbx.SelectedIndex;
                else
                {
                    if (int.TryParse(obj.id, out int index))
                        sqlPara.Value = index;
                    else
                        sqlPara.Value = cbx.SelectedIndex;
                }
            }
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameter(SqlCommand cmd, string paraName, System.Windows.Forms.DateTimePicker tbx)
        {
            SqlParameter sqlPara = null;

            sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.DateTime);

            if (tbx == null)
                sqlPara.Value = string.Empty;

            sqlPara.Value = tbx.Value;
            cmd.Parameters.Add(sqlPara);
        }

        public static void UpdateDataSqlParameter(SqlCommand cmd, string paraName, List<System.Windows.Forms.CheckBox> lstCheckbox)
        {
            SqlParameter sqlPara = null;

            sqlPara = new SqlParameter(string.Format("@{0}", paraName), SqlDbType.BigInt);

            if (lstCheckbox.Count == 0)
                sqlPara.Value = 0;

            string str = string.Empty;
            for (int i = 1; i <= lstCheckbox.Count; i++)
            {
                if (lstCheckbox[i - 1].Checked)
                    str = str + i.ToString();
            }
            if (string.IsNullOrEmpty(str))
                str = "0";
            sqlPara.Value = str;
            cmd.Parameters.Add(sqlPara);
        }

        public static string GetTenDuAnById(ConnectionWithExtraInfo connectionInfo, int idDuAn)
        {
            string retVal = string.Empty;
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return retVal;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT name FROM cecm_programData WHERE cecm_programData.id = {0}", idDuAn), cn);
            sqlAdapter.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                retVal = (dr["name"].ToString());
            }

            return retVal;
        }

        public static string GetMaDuAnById(ConnectionWithExtraInfo connectionInfo, int idDuAn)
        {
            string retVal = string.Empty;
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return retVal;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT parentName FROM cecm_programData WHERE cecm_programData.id = {0}", idDuAn), cn);
            sqlAdapter.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                retVal = (dr["parentName"].ToString());
            }

            return retVal;
        }

        public static string GetTenDoiRPBMById(ConnectionWithExtraInfo connectionInfo, int idDoi)
        {
            string retVal = string.Empty;
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return retVal;

            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;

            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT name FROM Cecm_ProgramTeam WHERE Cecm_ProgramTeam.id = {0}", idDoi), cn);
            sqlAdapter.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                retVal = (dr["name"].ToString());
            }

            return retVal;
        }

        public static int GetIdDuAnByMa(ConnectionWithExtraInfo connectionInfo, string maDuAn)
        {
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return -1;

            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("SELECT id FROM cecm_programData WHERE cecm_programData.code = '{0}'", maDuAn), cn);
            sqlAdapterProvince.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);
            foreach (DataRow dr in datatableProvince.Rows)
            {
                var retVal = (dr["id"].ToString());
                if (int.TryParse(retVal, out int idDuAn))
                    return idDuAn;
            }

            return -1;
        }

        public static bool DeleteRowDatabaseById(ConnectionWithExtraInfo connectionInfo, string tableName, string colName, string colVal)
        {
            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            if (cn == null)
                return false;

            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("DELETE FROM {0} WHERE {1}.{2} = {3};", tableName, tableName, colName, colVal), cn);
                cmd.Transaction = connectionInfo.Transaction as SqlTransaction;
                int temp = 0;
                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }
        }

        public static bool SetComboboxValue(System.Windows.Forms.ComboBox cbBox, DataRow dr, string rowName)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();
                if (int.TryParse(strVal, out int indexSelected) == false)
                    return false;

                if (cbBox.Items.Count <= indexSelected)
                    return false;

                cbBox.SelectedIndex = indexSelected;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }

            return false;
        }

        public static bool SetComboboxCheckboxValue(PresentationControls.CheckBoxComboBox cbBox, DataRow dr, string rowName)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();
                var splitVal = strVal.Split(',');
                foreach (var item in splitVal)
                {
                    if (int.TryParse(item, out int indexSelected) == false)
                        continue;

                    if (cbBox.Items.Count <= indexSelected)
                        continue;

                    cbBox.CheckBoxItems[indexSelected].Checked = true;
                }
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }

            return false;
        }

        public static bool SetTextboxValue(System.Windows.Forms.TextBox tbx, DataRow dr, string rowName, bool isNumber)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();
                if (isNumber)
                {
                    if (double.TryParse(strVal, out double dVal) == false)
                        return false;

                    tbx.Text = dVal.ToString();
                }
                else
                    tbx.Text = strVal;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }

            return false;
        }

        public static bool SetTextboxValue(System.Windows.Forms.Label lbl, DataRow dr, string rowName, bool isNumber)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();
                if (isNumber)
                {
                    if (double.TryParse(strVal, out double dVal) == false)
                        return false;

                    lbl.Text = dVal.ToString();
                }
                else
                    lbl.Text = strVal;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }

            return false;
        }

        public static bool SetTextboxValue(System.Windows.Forms.RichTextBox tbx, DataRow dr, string rowName, bool isNumber)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();
                if (isNumber)
                {
                    if (double.TryParse(strVal, out double dVal) == false)
                        return false;

                    tbx.Text = dVal.ToString();
                }
                else
                    tbx.Text = strVal;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }

            return false;
        }

        public static bool SetTextboxPolygonValue(System.Windows.Forms.RichTextBox tbx, DataRow dr, string rowName)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();

                var lstPoint = GetPointOfPolygonDatabase(strVal, false);
                foreach (var item in lstPoint)
                    tbx.Text = tbx.Text + string.Format("{0},{1}\n", item.X, item.Y);
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }

            return false;
        }

        public static bool SetDateTimeValue(System.Windows.Forms.DateTimePicker datetime, DataRow dr, string rowName)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();
                if (DateTime.TryParse(strVal, out DateTime dateTimeMeasure) == false)
                    return false;

                datetime.Value = dateTimeMeasure;
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }

            return false;
        }

        public static bool SetListCheckboxValue(List<System.Windows.Forms.CheckBox> lstcbk, DataRow dr, string rowName)
        {
            if (dr == null || string.IsNullOrEmpty(rowName))
                return false;

            try
            {
                var strVal = dr[rowName].ToString();

                if (string.IsNullOrEmpty(strVal))
                    return false;

                foreach (var item in strVal)
                {
                    if (int.TryParse(item.ToString(), out int val) == false)
                        continue;

                    if (val <= 0)
                        continue;

                    if (lstcbk.Count >= val)
                        lstcbk[val - 1].Checked = true;
                }
            }
            catch (System.Exception ex)
            {
                var mess = ex.Message;
                return false;
            }
            return false;
        }

        public static int GetLastIdIndentifyTable(ConnectionWithExtraInfo connectionInfo, string nameTable)
        {
            int retVal = 0;

            SqlConnection cn = connectionInfo.Connection as SqlConnection;
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM {0}", nameTable), cn);
            sqlAdapter.SelectCommand.Transaction = connectionInfo.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);

            List<int> lVal = new List<int>();
            foreach (DataRow dr in datatable.Rows)
            {
                int mId = int.Parse(dr[0].ToString());
                lVal.Add(mId);
            }
            retVal = lVal.Max();

            return retVal;
        }

        public static bool SetPhotoFileToPicturebox(string photoFileName, int idDuAn, System.Windows.Forms.PictureBox pBox)
        {
            string pathTemp = AppUtils.GetFolderTemp(idDuAn);
            string fullPath = System.IO.Path.Combine(pathTemp, photoFileName);
            if (System.IO.File.Exists(fullPath) == false)
                return false;

            //var imageFile = AppUtils.GetImage(fullPath);
            //if (imageFile == null)
            //    return false;

            //pBox.BackgroundImage = imageFile;
            pBox.ImageLocation = fullPath;
            return true;
        }

        public static List<CecmProgramAreaMapDTO> GetAllCecmProgramAreaMapByProgramId(ConnectionWithExtraInfo extraInfoConnettion, long idDuAn)
        {
            if (extraInfoConnettion == null)
                return new List<CecmProgramAreaMapDTO>();

            SqlConnection cn = extraInfoConnettion.Connection as SqlConnection;
            string sql = "SELECT ";
            sql += "id as id, ";
            sql += "cecm_program_area_id as cecm_program_area_id, ";
            sql += "cecm_program_id as cecm_program_id, ";
            sql += "parentid as parentid, ";
            sql += "parentiddistrict as parentiddistrict, ";
            sql += "parentidcommune as parentidcommune, ";
            sql += "cecm_program_station_id as cecm_program_station_id, ";
            sql += "villageid as villageid, ";
            sql += "code as code, ";
            sql += "address as address, ";
            sql += "CONCAT(code, ' - ', address) as cb_name, ";
            sql += "position_lat as position_lat, ";
            sql += "position_long as position_long, ";
            sql += "areamap as areamap, ";
            sql += "polygongeomst as polygongeomst, ";
            sql += "positionlongvn2000 as positionlongvn2000, ";
            sql += "positionlatvn2000 as positionlatvn2000, ";
            sql += "meridian as meridian, ";
            sql += "pzone as pzone, ";
            sql += "lx as lx, ";
            sql += "ly as ly, ";
            sql += "vn2000 as vn2000, ";
            sql += "photo_file as photo_file, ";
            sql += "bottom_lat as bottom_lat, ";
            sql += "left_long as left_long, ";
            sql += "right_long as right_long, ";
            sql += "top_lat as top_lat ";
            sql += "FROM cecm_program_area_map cpm ";
            sql += "WHERE cpm.cecm_program_id = " + idDuAn;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(sql, cn);
            sqlAdapterProvince.SelectCommand.Transaction = extraInfoConnettion.Transaction as SqlTransaction;

            SqlCommandBuilder sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            DataTable datatableProvince = new DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            List<CecmProgramAreaMapDTO> lst = new List<CecmProgramAreaMapDTO>();
            lst =
            (
                from DataRow dr in datatableProvince.Rows
                select new CecmProgramAreaMapDTO()
                {
                    cecmProgramAreaMapId = dr["id"].ToString(),
                    cecmProgramId = dr["cecm_program_id"].ToString(),
                    cecmProgramAreaId = dr["cecm_program_area_id"].ToString(),
                    cecmProgramStationId = dr["cecm_program_station_id"].ToString(),
                    positionLat = dr["position_lat"].ToString(),
                    positionLong = dr["position_long"].ToString(),
                    parentId = dr["parentid"].ToString(),
                    parentIdDistrict = dr["parentiddistrict"].ToString(),
                    parentIdCommune = dr["parentidcommune"].ToString(),
                    villageId = dr["villageid"].ToString(),
                    code = dr["code"].ToString(),
                    address = dr["address"].ToString(),
                    comboboxName = dr["cb_name"].ToString(),
                    polygonGeomST = dr["polygongeomst"].ToString(),
                    photo_file = dr["photo_file"].ToString(),
                    vn2000 = dr["vn2000"].ToString(),
                    positionLongVN2000 = dr["positionlongvn2000"].ToString(),
                    positionLatVN2000 = dr["positionlatvn2000"].ToString(),
                    meridian = dr["meridian"].ToString(),
                    pzone = dr["pzone"].ToString(),
                    lx = dr["lx"].ToString(),
                    ly = dr["ly"].ToString(),
                    areamap = dr["areamap"].ToString(),
                    bottom_lat = dr["bottom_lat"].ToString(),
                    top_lat = dr["top_lat"].ToString(),
                    left_long = dr["left_long"].ToString(),
                    right_long = dr["right_long"].ToString()
                }
            ).ToList();
            return lst;
        }
    }
}