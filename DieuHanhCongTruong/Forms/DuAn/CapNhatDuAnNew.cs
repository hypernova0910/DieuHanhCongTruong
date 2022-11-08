using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Models;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using DieuHanhCongTruong.Properties;
using DieuHanhCongTruong;
using DieuHanhCongTruong.Forms.Account;

namespace VNRaPaBomMin
{
    public partial class CapNhatDuAnNew : Form
    {
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;
        public int _IdDuAn = -1;
        public bool _IsUpdate = false;
        public string _DWGFile = string.Empty;
        public string _TenDuAn = string.Empty;
        public static long idDA_DH = -1;

        private string databaseProgram = "cecm_programData";
        private List<CheckBox> cbkDoc_lstAction_type = new List<CheckBox>();
        private List<CheckBox> cbkLst_step_type = new List<CheckBox>();
        private List<CheckBox> cbkDoc_source_money = new List<CheckBox>();
        private List<CheckBox> cbklst_person_assign = new List<CheckBox>();

        public List<SqlCommand> sqlCommands = new List<SqlCommand>();

        public CapNhatDuAnNew(int idDuAn)
        {
            InitializeComponent();

            _IdDuAn = idDuAn;
            cbkDoc_lstAction_type = new List<CheckBox> { cbkDoc_lstAction_type1, cbkDoc_lstAction_type2, cbkDoc_lstAction_type3 };
            cbkLst_step_type = new List<CheckBox> { cbkLst_step_type1, cbkLst_step_type2, cbkLst_step_type3 };
            cbkDoc_source_money = new List<CheckBox> { cbkDoc_source_money1, cbkDoc_source_money2, cbkDoc_source_money3, cbkDoc_source_money4 };
            cbklst_person_assign = new List<CheckBox> { cbkLst_person_assign1, cbkLst_person_assign2, cbkLst_person_assign3, cbkLst_person_assign4 };
        }

        public void LoadData()
        {
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            IDbTransaction trans = _ExtraInfoConnettion.BeginTransaction();

            
            

            LoadDataThongTinChung(_IdDuAn);

            LoadDataDonViThamGia(_IdDuAn);

            LoadDataKhuVucDuAn(_IdDuAn);

            LoadDataDoiRPBM(_IdDuAn);

            LoadDataMayDo(_IdDuAn);

            LoadDataNhanVienThamGia(_IdDuAn);

            LoadDataTrangThietBiThamGia(_IdDuAn);

            LoadDataDVDT(_IdDuAn);

            LoadDataDVKS(_IdDuAn);

            LoadDataDVRP(_IdDuAn);

            LoadDataDVTTGD(_IdDuAn);

            LoadDataCQHTNN(_IdDuAn);

            UpdateStatusGiaiDoan(cbkDoc_lstAction_type1.Checked);

            if (_IdDuAn <= 0)
            {
                //_IdDuAn = UtilsDatabase.GetIdDuAnByMa(_ExtraInfoConnettion, tbCode.Text.Trim());
                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO {0} (name) VALUES ('')", databaseProgram));
                cmd.Connection = _ExtraInfoConnettion.Connection as SqlConnection;
                cmd.Transaction = trans as SqlTransaction;
                cmd.ExecuteNonQuery();
                _IdDuAn = UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);
            }

            //trans.Commit();
        }

        private void CapNhatDuAn_Load(object sender, EventArgs e)
        {
            
            if(_IdDuAn <= 0)
            {
                btnImportLog.Enabled = false;
            }
            LoadData();
        }

        private void LoadDataDVDT(int idDA)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            string sql = @"select gid, CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web
            from dept_tham_gia dtg left join cert_department d on 
            CASE 
            WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1
            WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1
            ELSE 0
            END = 1
            WHERE dtg.cecm_program_id = {0} and dtg.table_name = '{1}'";
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format(sql, idDA, TableName.DIEU_TRA), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            dgvDVDT.Rows.Clear();
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string gid = dr["gid"].ToString();
                string dept_idST = dr["dept_idST"].ToString();
                string phone = dr["phone"].ToString();
                string email = dr["email"].ToString();
                string address = dr["address"].ToString();
                string fax = dr["fax"].ToString();

                //indexRow++;

                int index = dgvDVDT.Rows.Add(dgvDVDT.Rows.Count + 1, dept_idST, address, phone, email, fax, Resources.Modify, Resources.DeleteRed);
                dgvDVDT.Rows[index].Tag = gid;
            }
        }

        private void LoadDataDVKS(int idDA)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            string sql = @"select gid, CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web
            from dept_tham_gia dtg left join cert_department d on 
            CASE 
            WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1
            WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1
            ELSE 0
            END = 1
            WHERE dtg.cecm_program_id = {0} and dtg.table_name = '{1}'";
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format(sql, idDA, TableName.KHAO_SAT), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            dgvDVKS.Rows.Clear();
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string gid = dr["gid"].ToString();
                string dept_idST = dr["dept_idST"].ToString();
                string phone = dr["phone"].ToString();
                string email = dr["email"].ToString();
                string address = dr["address"].ToString();
                string fax = dr["fax"].ToString();

                //indexRow++;

                int index = dgvDVKS.Rows.Add(dgvDVKS.Rows.Count + 1, dept_idST, address, phone, email, fax, Resources.Modify, Resources.DeleteRed);
                dgvDVKS.Rows[index].Tag = gid;
            }
        }

        private void LoadDataDVRP(int idDA)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            string sql = @"select gid, CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web
            from dept_tham_gia dtg left join cert_department d on 
            CASE 
            WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1
            WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1
            ELSE 0
            END = 1
            WHERE dtg.cecm_program_id = {0} and dtg.table_name = '{1}'";
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format(sql, idDA, TableName.RA_PHA), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            dgvDVRP.Rows.Clear();
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string gid = dr["gid"].ToString();
                string dept_idST = dr["dept_idST"].ToString();
                string phone = dr["phone"].ToString();
                string email = dr["email"].ToString();
                string address = dr["address"].ToString();
                string fax = dr["fax"].ToString();

                //indexRow++;

                int index = dgvDVRP.Rows.Add(dgvDVRP.Rows.Count + 1, dept_idST, address, phone, email, fax, Resources.Modify, Resources.DeleteRed);
                dgvDVRP.Rows[index].Tag = gid;
            }
        }

        private void LoadDataDVTTGD(int idDA)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            string sql = @"select gid, CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web
            from dept_tham_gia dtg left join cert_department d on 
            CASE 
            WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1
            WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1
            ELSE 0
            END = 1
            WHERE dtg.cecm_program_id = {0} and dtg.table_name = '{1}'";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(string.Format(sql, idDA, TableName.TUYEN_TRUYEN_GIAO_DUC), cn);
            sqlAdapter.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapter);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapter.Fill(datatableProvince);

            dgvDVTTGD.Rows.Clear();
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string gid = dr["gid"].ToString();
                string dept_idST = dr["dept_idST"].ToString();
                string phone = dr["phone"].ToString();
                string email = dr["email"].ToString();
                string address = dr["address"].ToString();
                string fax = dr["fax"].ToString();

                //indexRow++;

                int index = dgvDVTTGD.Rows.Add(dgvDVTTGD.Rows.Count + 1, dept_idST, address, phone, email, fax, Resources.Modify, Resources.DeleteRed);
                dgvDVTTGD.Rows[index].Tag = gid;
            }
        }

        private void LoadDataCQHTNN(int idDA)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            string sql = @"select gid, CONCAT(d.name, dept_other) as dept_idST, dtg.phone, dtg.email, dtg.address, dtg.fax , dept_id_web
            from dept_tham_gia dtg left join cert_department d on 
            CASE 
            WHEN dtg.dept_id IS NULL AND d.id_web = dtg.dept_id_web THEN 1
            WHEN dtg.dept_id IS NOT NULL  AND d.id = dtg.dept_id THEN 1
            ELSE 0
            END = 1
            WHERE dtg.cecm_program_id = {0} and dtg.table_name = '{1}'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Format(sql, idDA, TableName.HO_TRO_NAN_NHAN), cn);
            sqlDataAdapter.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlDataAdapter);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlDataAdapter.Fill(datatableProvince);

            dgvCQHTNN.Rows.Clear();
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string gid = dr["gid"].ToString();
                string dept_idST = dr["dept_idST"].ToString();
                string phone = dr["phone"].ToString();
                string email = dr["email"].ToString();
                string address = dr["address"].ToString();
                string fax = dr["fax"].ToString();

                //indexRow++;

                int index = dgvCQHTNN.Rows.Add(dgvCQHTNN.Rows.Count + 1, dept_idST, address, phone, email, fax, Resources.Modify, Resources.DeleteRed);
                dgvCQHTNN.Rows[index].Tag = gid;
            }
        }

        private void LoadDataDonViThamGia(int idDuAn)
        {
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdepartmentMaster2);
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdepartmentMaster3);
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdepartmentMaster);
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdepartmentMaster6);
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdepartmentMaster7);
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdeptCDT);
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdeptQL);

            var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, databaseProgram, "id", idDuAn.ToString());
            foreach (var item in lstDatarow)
            {
                UtilsDatabase.SetComboboxValue(cbdepartmentMaster2, item, "departmentMaster2");
                UtilsDatabase.SetTextboxValue(tbdepartmentName2, item, "departmentName2", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentphone2, item, "departmentphone2", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentemail2, item, "departmentemail2", false);
                UtilsDatabase.SetTextboxValue(tbcontact2, item, "contact2", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentfax2, item, "departmentfax2", false);
                UtilsDatabase.SetTextboxValue(tbdeptHead2, item, "deptHead2", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPost2, item, "deptHeadPost2", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPhone2, item, "deptHeadPhone2", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadEmail2, item, "deptHeadEmail2", false);
                UtilsDatabase.SetTextboxValue(tbdoc_number2, item, "doc_number2", false);
                UtilsDatabase.SetDateTimeValue(timedoc_date2, item, "doc_date2");
                UtilsDatabase.SetTextboxValue(tbdoc_person_type2, item, "doc_person_type2", false);
                UtilsDatabase.SetTextboxValue(tbdoc_files2, item, "doc_files2", false);

                UtilsDatabase.SetComboboxValue(cbdepartmentMaster3, item, "departmentMaster3");
                UtilsDatabase.SetTextboxValue(tbdepartmentName3, item, "departmentName3", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentphone3, item, "departmentphone3", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentemail3, item, "departmentemail3", false);
                UtilsDatabase.SetTextboxValue(tbcontact3, item, "contact3", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentfax3, item, "departmentfax3", false);
                UtilsDatabase.SetTextboxValue(tbdeptHead3, item, "deptHead3", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPost3, item, "deptHeadPost3", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPhone3, item, "deptHeadPhone3", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadEmail3, item, "deptHeadEmail3", false);
                UtilsDatabase.SetTextboxValue(tbdoc_number3, item, "doc_number3", false);
                UtilsDatabase.SetDateTimeValue(timedoc_date3, item, "doc_date3");
                UtilsDatabase.SetTextboxValue(tbdoc_person_type3, item, "doc_person_type3", false);
                UtilsDatabase.SetTextboxValue(tbdoc_files3, item, "doc_files3", false);

                UtilsDatabase.SetComboboxValue(cbdepartmentMaster, item, "departmentMaster");
                UtilsDatabase.SetTextboxValue(tbdepartmentName, item, "departmentName", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentphone, item, "departmentphone", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentemail, item, "departmentemail", false);
                UtilsDatabase.SetTextboxValue(tbcontact, item, "contact", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentfax, item, "departmentfax", false);
                UtilsDatabase.SetTextboxValue(tbdeptHead, item, "deptHead", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPost, item, "deptHeadPost", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPhone, item, "deptHeadPhone", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadEmail, item, "deptHeadEmail", false);
                UtilsDatabase.SetTextboxValue(tbbdoc_number1, item, "doc_number1", false);
                UtilsDatabase.SetDateTimeValue(timeedoc_date1, item, "doc_date1");
                UtilsDatabase.SetTextboxValue(tbbdoc_person_type1, item, "doc_person_type1", false);
                UtilsDatabase.SetTextboxValue(tbdoc_files, item, "doc_files", false);

                UtilsDatabase.SetComboboxValue(cbdepartmentMaster6, item, "departmentMaster6");
                UtilsDatabase.SetTextboxValue(tbdepartmentName6, item, "departmentName6", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentphone6, item, "departmentphone6", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentemail6, item, "departmentemail6", false);
                UtilsDatabase.SetTextboxValue(tbcontact6, item, "contact6", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentfax6, item, "departmentfax6", false);
                UtilsDatabase.SetTextboxValue(tbdeptHead6, item, "deptHead6", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPost6, item, "deptHeadPost6", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPhone6, item, "deptHeadPhone6", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadEmail6, item, "deptHeadEmail6", false);
                UtilsDatabase.SetTextboxValue(tbdoc_number6, item, "doc_number6", false);
                UtilsDatabase.SetDateTimeValue(timedoc_date6, item, "doc_date6");
                UtilsDatabase.SetTextboxValue(tbdoc_person_type6, item, "doc_person_type6", false);
                UtilsDatabase.SetTextboxValue(tbdoc_files6, item, "doc_files6", false);

                UtilsDatabase.SetComboboxValue(cbdepartmentMaster7, item, "departmentMaster7");
                UtilsDatabase.SetTextboxValue(tbdepartmentName7, item, "departmentName7", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentphone7, item, "departmentphone7", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentemail7, item, "departmentemail7", false);
                UtilsDatabase.SetTextboxValue(tbcontact7, item, "contact7", false);
                UtilsDatabase.SetTextboxValue(tbdepartmentfax7, item, "departmentfax7", false);
                UtilsDatabase.SetTextboxValue(tbdeptHead7, item, "deptHead7", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPost7, item, "deptHeadPost7", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadPhone7, item, "deptHeadPhone7", false);
                UtilsDatabase.SetTextboxValue(tbdeptHeadEmail7, item, "deptHeadEmail7", false);
                UtilsDatabase.SetTextboxValue(tbdoc_number7, item, "doc_number7", false);
                UtilsDatabase.SetDateTimeValue(timedoc_date7, item, "doc_date7");
                UtilsDatabase.SetTextboxValue(tbdoc_person_type7, item, "doc_person_type7", false);
                UtilsDatabase.SetTextboxValue(tbdoc_files7, item, "doc_files7", false);

                UtilsDatabase.SetComboboxValue(cbdeptCDT, item, "deptCDT");
                UtilsDatabase.SetTextboxValue(tbdeptCDTName, item, "deptCDTName", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTphone, item, "deptCDTphone", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTemail, item, "deptCDTemail", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTcontact, item, "deptCDTcontact", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTfax, item, "deptCDTfax", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTHead, item, "deptCDTHead", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTPost, item, "deptCDTPost", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTTel, item, "deptCDTTel", false);
                UtilsDatabase.SetTextboxValue(tbdeptCDTHeadEmail, item, "deptCDTHeadEmail", false);

                UtilsDatabase.SetComboboxValue(cbdeptQL, item, "deptQL");
                UtilsDatabase.SetTextboxValue(tbdeptQLName, item, "deptQLName", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLphone, item, "deptQLphone", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLemail, item, "deptQLemail", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLcontact, item, "deptQLcontact", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLfax, item, "deptQLfax", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLHead, item, "deptQLHead", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLPost, item, "deptQLPost", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLTel, item, "deptQLTel", false);
                UtilsDatabase.SetTextboxValue(tbdeptQLHeadEmail, item, "deptQLHeadEmail", false);
            }
        }

        private void LoadDataThongTinChung(int idDuAn)
        {
            UtilsDatabase.LoadTinhComboboxCheckbox(_ExtraInfoConnettion, cbProviceIdOther);
            UtilsDatabase.LoadTinh(_ExtraInfoConnettion, cbProviceId);
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbIsActive, "040");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbProgramType, "041");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbMoneySource, "042");

            var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, databaseProgram, "id", idDuAn.ToString());
            foreach (var item in lstDatarow)
            {
                UtilsDatabase.SetComboboxValue(cbProgramType, item, "programType");
                UtilsDatabase.SetTextboxValue(tbParentName, item, "parentName", false);
                UtilsDatabase.SetTextboxValue(tbCode, item, "code", false);
                UtilsDatabase.SetTextboxValue(tbName, item, "name", false);
                UtilsDatabase.SetComboboxValue(cbIsActive, item, "isActive");
                UtilsDatabase.SetTextboxValue(tbMoney_pre, item, "money_pre", true);
                UtilsDatabase.SetTextboxValue(tbMoneyTotal, item, "moneyTotal", true);
                UtilsDatabase.SetTextboxValue(tb_Acreage, item, "acreage", true);
                UtilsDatabase.SetTextboxValue(tb_Acreagewater, item, "acreagewater", true);
                UtilsDatabase.SetTextboxValue(tbAcreagesea, item, "acreagesea", true);
                UtilsDatabase.SetDateTimeValue(timeStartTime, item, "startTime");
                UtilsDatabase.SetDateTimeValue(timeEndTime, item, "endTime");
                UtilsDatabase.SetTextboxValue(tbtimeTotal, item, "timeTotal", true);
                UtilsDatabase.SetComboboxValue(cbProviceId, item, "proviceId");
                UtilsDatabase.SetComboboxCheckboxValue(cbProviceIdOther, item, "proviceIdOther");
                UtilsDatabase.SetTextboxValue(tbAddress, item, "address", false);
                UtilsDatabase.SetComboboxValue(cbMoneySource, item, "moneySource");
                UtilsDatabase.SetListCheckboxValue(cbkDoc_lstAction_type, item, "doc_lstAction_type");
                UtilsDatabase.SetListCheckboxValue(cbkLst_step_type, item, "lst_step_type");
                UtilsDatabase.SetListCheckboxValue(cbkDoc_source_money, item, "doc_source_money");
                UtilsDatabase.SetTextboxValue(cbkDoc_source_money5, item, "source_mouney_other", false);
                UtilsDatabase.SetListCheckboxValue(cbklst_person_assign, item, "lst_person_assign");
                UtilsDatabase.SetTextboxValue(cbkLst_person_assign5, item, "person_assign_other", false);
                UtilsDatabase.SetTextboxValue(tbDoc_number, item, "doc_number", false);
                UtilsDatabase.SetDateTimeValue(timeDoc_date, item, "doc_date");
                UtilsDatabase.SetTextboxValue(tbDoc_person_type, item, "doc_person_type", false);
                UtilsDatabase.SetTextboxValue(tbDoc_number4, item, "doc_number4", false);
                UtilsDatabase.SetDateTimeValue(timeDoc_date4, item, "doc_date4");
                UtilsDatabase.SetTextboxValue(tbDoc_person_type4, item, "doc_person_type4", false);
                UtilsDatabase.SetTextboxValue(tbDoc_number5, item, "doc_number5", false);
                UtilsDatabase.SetDateTimeValue(timeDoc_date5, item, "doc_date5");
                UtilsDatabase.SetTextboxValue(tbDoc_person_type5, item, "doc_person_type5", false);
                UtilsDatabase.SetTextboxValue(tbDoc_file, item, "doc_files", false);
                UtilsDatabase.SetTextboxValue(tbDoc_file4, item, "doc_files4", false);
                UtilsDatabase.SetTextboxValue(tbDoc_file5, item, "doc_files5", false);
                UtilsDatabase.SetTextboxValue(lblDwg, item, "dwg", false);
            }

            _TenDuAn = tbParentName.Text;
        }

        private void LoadDataKhuVucDuAn(int idDuAn)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("Select id, code, address, areamap, position_long, position_lat FROM cecm_program_area_map WHERE cecm_program_area_map.cecm_program_id = {0}", idDuAn), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            DvgKhuVucDuAn.Rows.Clear();
            int indexRow = 0;
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string id = dr["id"].ToString();
                string ma = dr["code"].ToString();
                string diaChi = dr["address"].ToString();
                string dienTich = dr["areamap"].ToString();
                string posLong = dr["position_long"].ToString();
                string posLat = dr["position_lat"].ToString();

                indexRow++;

                int index = DvgKhuVucDuAn.Rows.Add(indexRow, ma, diaChi, dienTich, posLong + " " + posLat, Resources.Modify, Resources.DeleteRed);
                DvgKhuVucDuAn.Rows[index].Tag = id;
            }
        }

        private void LoadDataNhanVienThamGia(int idDuAn)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("Select * FROM Cecm_ProgramStaff WHERE Cecm_ProgramStaff.cecmProgramId = {0}", idDuAn), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            DgvNhanVien.Rows.Clear();
            int indexRow = 0;
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string id = dr["id"].ToString();
                string tenNhanVien = dr["nameId"].ToString();
                string doiRPBM = dr["cecmProgramTeamId"].ToString();
                string chucVu = dr["rank"].ToString();
                string chungChi = dr["Staff_lstSub"].ToString();

                if (int.TryParse(doiRPBM, out int indexDoiRPBM) == false)
                    continue;
                if (int.TryParse(chucVu, out int indexChucVu) == false)
                    continue;

                var strDoiRPBM = UtilsDatabase.GetTenDoiRPBMById(_ExtraInfoConnettion, indexDoiRPBM);
                var strChucVu = UtilsDatabase.GetNameDatabaseGroup(_ExtraInfoConnettion, indexChucVu, "049");

                indexRow++;

                int index = DgvNhanVien.Rows.Add(indexRow, tenNhanVien, strDoiRPBM, strChucVu, chungChi, Resources.Modify, Resources.DeleteRed);
                DgvNhanVien.Rows[index].Tag = id;
            }
        }

        private void LoadDataMayDo(int idDuAn)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("Select * FROM Cecm_ProgramMachineBomb WHERE Cecm_ProgramMachineBomb.cecm_program_id = {0}", idDuAn), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            dgvMayDo.Rows.Clear();
            int indexRow = 0;
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string id = dr["id"].ToString();
                string mac_id = dr["mac_id"].ToString();
                string soDangKy = dr["code"].ToString();
                string loaiRPBM = dr["type_standart"].ToString();
                string trangThai = dr["status"].ToString();
                DateTime thoiGianKiemDinh = (DateTime)dr["time_test"];

                if (int.TryParse(loaiRPBM, out int indexloaiRPBM) == false)
                    continue;
                if (int.TryParse(trangThai, out int indextrangThaii) == false)
                    continue;

                var strLoai = UtilsDatabase.GetNameDatabaseGroup(_ExtraInfoConnettion, indexloaiRPBM, "045");
                var strTrangThai = UtilsDatabase.GetNameDatabaseGroup(_ExtraInfoConnettion, indextrangThaii, "044");

                indexRow++;

                int index = dgvMayDo.Rows.Add(indexRow, mac_id, soDangKy, strLoai, strTrangThai, thoiGianKiemDinh.ToString(AppUtils.DateTimeTostring), Resources.Modify, Resources.DeleteRed);
                dgvMayDo.Rows[index].Tag = id;
            }
        }

        private void LoadDataTrangThietBiThamGia(int idDuAn)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("Select * FROM Cecm_ProgramDevice WHERE Cecm_ProgramDevice.cecm_program_id = {0}", idDuAn), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            dgvTrangThietBi.Rows.Clear();
            int indexRow = 0;
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string id = dr["id"].ToString();
                string soDangKy = dr["code"].ToString();
                string loaiThietBi = dr["type_standart"].ToString();
                string trangThai = dr["status"].ToString();
                DateTime thoiGianKiemDinh = (DateTime)dr["time_test"];

                if (int.TryParse(loaiThietBi, out int indexLoaiThietBi) == false)
                    continue;
                if (int.TryParse(trangThai, out int indexTrangThai) == false)
                    continue;

                var strLoaiThietBi = UtilsDatabase.GetNameDatabaseGroup(_ExtraInfoConnettion, indexLoaiThietBi, "055");
                var strTrangThai = UtilsDatabase.GetNameDatabaseGroup(_ExtraInfoConnettion, indexTrangThai, "054");

                indexRow++;

                int index = dgvTrangThietBi.Rows.Add(indexRow, soDangKy, strLoaiThietBi, strTrangThai, thoiGianKiemDinh.ToString(AppUtils.DateTimeTostring), Resources.Modify, Resources.DeleteRed);
                dgvTrangThietBi.Rows[index].Tag = id;
            }
        }

        private void LoadDataDoiRPBM(int idDuAn)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("Select * FROM Cecm_ProgramTeam WHERE Cecm_ProgramTeam.cecmProgramId = {0}", idDuAn), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            DgvDoiRPBM.Rows.Clear();
            int indexRow = 0;
            foreach (DataRow dr in datatableProvince.Rows)
            {
                string id = dr["id"].ToString();
                string tenDoi = dr["name"].ToString();
                string levelDoiRP = dr["levels"].ToString();
                string loaiDoi = dr["type"].ToString();
                string tongThanhVien = dr["total_member"].ToString();

                if (int.TryParse(levelDoiRP, out int indexCap) == false)
                    continue;
                if (int.TryParse(loaiDoi, out int indexLoai) == false)
                    continue;

                var strCap = UtilsDatabase.GetNameDatabaseGroup(_ExtraInfoConnettion, indexCap, "043");
                var strLoai = UtilsDatabase.GetNameDatabaseGroup(_ExtraInfoConnettion, indexLoai, "034");

                indexRow++;

                int index = DgvDoiRPBM.Rows.Add(indexRow, tenDoi, strCap, strLoai, tongThanhVien, Resources.Modify, Resources.DeleteRed);
                DgvDoiRPBM.Rows[index].Tag = id;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ThemMoiKhuVucNew frm = new ThemMoiKhuVucNew(-1, _IdDuAn);
            frm.ShowDialog();

            LoadDataKhuVucDuAn(_IdDuAn);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ThemMoiNhanVienThamGiaNew frm = new ThemMoiNhanVienThamGiaNew(-1, _IdDuAn);
            frm.ShowDialog();

            LoadDataNhanVienThamGia(_IdDuAn);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ThemMoiMayDoNew frm = new ThemMoiMayDoNew(-1, _IdDuAn);
            frm.ShowDialog();

            LoadDataMayDo(_IdDuAn);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ThemMoiDoiRPBMNew frm = new ThemMoiDoiRPBMNew(-1, _IdDuAn);
            frm.ShowDialog();

            LoadDataDoiRPBM(_IdDuAn);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ThemMoiTrangThietBiThamGiaNew frm = new ThemMoiTrangThietBiThamGiaNew(-1, _IdDuAn);
            frm.ShowDialog();

            LoadDataTrangThietBiThamGia(_IdDuAn);
        }

        public bool NhapDuAn()
        {
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            ImportDuAnCmdNew cmd = new ImportDuAnCmdNew();
            var dataJson = cmd.ReadDataZipFile(_ExtraInfoConnettion.Connection as SqlConnection, out string folderUnzip);
            if (dataJson == null)
                return false;

            //_ExtraInfoConnettion.Transaction.Rollback();
            _ExtraInfoConnettion.BeginTransaction();
            _IdDuAn = -1;

            try
            {
                PopularThongTinChung(dataJson);

                PopularDonViThamGia(dataJson);

                PopularKhuVucDuAn(dataJson);

                PopularNhanVienThamGia(dataJson);

                PopularDoiRPBM(dataJson);

                PopularMayDoDuAn(dataJson);

                PopularTrangThietBi(dataJson);

                // Copy file
                CopyFolder(_IdDuAn, folderUnzip);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import dự án không thành công");
                return false;
            }

            

            _ExtraInfoConnettion.Transaction.Commit();

            this.DialogResult = DialogResult.No;

            return true;
        }

        private void btNhap_Click(object sender, EventArgs e)
        {
            NhapDuAn();
        }

        private void CopyFolder(int maDuAn, string sourceFolder)
        {
            var strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
            if (string.IsNullOrEmpty(strPath))
            {
                MessageBox.Show("Không tìm thấy đường dẫn lưu tệp");
                MenuLoaderManagerFrm frm = new MenuLoaderManagerFrm();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
                else
                    return;
            }

            if (System.IO.Directory.Exists(strPath + "\\" + maDuAn + "\\Temp\\") == false)
            {
                System.IO.Directory.CreateDirectory(strPath + "\\" + maDuAn + "\\Temp\\");
            }

            var pathDest = strPath + "\\" + maDuAn + "\\Temp\\";
            AppUtils.Copy(sourceFolder, pathDest);
            // remove temp
            sourceFolder = Directory.GetParent(sourceFolder).FullName;
            sourceFolder = Directory.GetParent(sourceFolder).FullName;
            AppUtils.ClearFolder(sourceFolder);
        }

        private void PopularTrangThietBi(CecmProgramComplexDTO dataJson)
        {
            // Delete data
            UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramDevice", "cecm_program_id", _IdDuAn.ToString());

            foreach (var cecmProgramMachineBomb in dataJson.lstCecmProgramDeviceDTO)
            {
                ThemMoiTrangThietBiThamGiaNew frm = new ThemMoiTrangThietBiThamGiaNew(-1, _IdDuAn);
                frm.LoadDataForm(cecmProgramMachineBomb);
                frm.UpdateToDatabase(false);
            }

            LoadDataTrangThietBiThamGia(_IdDuAn);
        }

        private void PopularMayDoDuAn(CecmProgramComplexDTO dataJson)
        {
            // Delete data
            UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramMachineBomb", "cecm_program_id", _IdDuAn.ToString());

            foreach (var cecmProgramMachineBomb in dataJson.lstCecmProgramMachineBombDTO)
            {
                ThemMoiMayDoNew frm = new ThemMoiMayDoNew(-1, _IdDuAn);
                frm.LoadDataForm(cecmProgramMachineBomb);
                frm.UpdateToDatabase(false);
            }

            LoadDataMayDo(_IdDuAn);
        }

        private void PopularDoiRPBM(CecmProgramComplexDTO dataJson)
        {
            // Delete data
            UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramTeam", "cecmProgramId", _IdDuAn.ToString());

            foreach (var cecmProgramTeam in dataJson.lstCecmProgramTeamDTO)
            {
                ThemMoiDoiRPBMNew frm = new ThemMoiDoiRPBMNew(-1, _IdDuAn);
                frm.LoadDataForm(cecmProgramTeam);
                frm.UpdateToDatabase(false);
            }

            LoadDataDoiRPBM(_IdDuAn);
        }

        private void PopularNhanVienThamGia(CecmProgramComplexDTO dataJson)
        {
            // Delete data
            UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramStaff", "cecmProgramId", _IdDuAn.ToString());

            foreach (var cecmStaffDTO in dataJson.lstCecmProgramStaffDTO)
            {
                ThemMoiNhanVienThamGiaNew frm = new ThemMoiNhanVienThamGiaNew(-1, _IdDuAn);
                frm.LoadDataForm(cecmStaffDTO);
                frm.UpdateToDatabase(false);
            }

            LoadDataNhanVienThamGia(_IdDuAn);
        }

        private void PopularKhuVucDuAn(CecmProgramComplexDTO dataJson)
        {
            // Delete data
            UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "cecm_program_area_map", "cecm_program_id", _IdDuAn.ToString());

            foreach (var cecmAreaMapDTO in dataJson.lstArea)
            {
                ThemMoiKhuVucNew frm = new ThemMoiKhuVucNew(-1, _IdDuAn);
                frm.LoadDataForm(cecmAreaMapDTO);
                frm.UpdateToDatabase(false);
            }

            LoadDataKhuVucDuAn(_IdDuAn);
        }

        private void PopularDonViThamGia(CecmProgramComplexDTO dataJson)
        {
            var cemcemDTO = dataJson.cecmProgramDTO;

            UtilsDatabase.PopularComboboxJson(cbdepartmentMaster2, cemcemDTO.departmentMaster2);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentName2, cemcemDTO.departmentName2);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentphone2, cemcemDTO.departmentphone2);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentemail2, cemcemDTO.departmentemail2);
            UtilsDatabase.PopularStringTextboxJson(tbcontact2, cemcemDTO.contact2);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentfax2, cemcemDTO.departmentfax2);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHead2, cemcemDTO.deptHead2);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPost2, cemcemDTO.deptHeadPost2);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPhone2, cemcemDTO.deptHeadPhone2);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadEmail2, cemcemDTO.deptHeadEmail2);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_number2, cemcemDTO.doc_number2);
            UtilsDatabase.PopularDatetimeTextboxJson(timedoc_date2, cemcemDTO.doc_date2);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_person_type2, cemcemDTO.doc_person_type2);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_files2, cemcemDTO.doc_file2);

            UtilsDatabase.PopularComboboxJson(cbdepartmentMaster3, cemcemDTO.departmentMaster3);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentName3, cemcemDTO.departmentName3);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentphone3, cemcemDTO.departmentphone3);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentemail3, cemcemDTO.departmentemail3);
            UtilsDatabase.PopularStringTextboxJson(tbcontact3, cemcemDTO.contact3);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentfax3, cemcemDTO.departmentfax3);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHead3, cemcemDTO.deptHead3);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPost3, cemcemDTO.deptHeadPost3);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPhone3, cemcemDTO.deptHeadPhone3);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadEmail3, cemcemDTO.deptHeadEmail3);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_number3, cemcemDTO.doc_number3);
            UtilsDatabase.PopularDatetimeTextboxJson(timedoc_date3, cemcemDTO.doc_date3);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_person_type3, cemcemDTO.doc_person_type3);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_files3, cemcemDTO.doc_file3);

            UtilsDatabase.PopularComboboxJson(cbdepartmentMaster, cemcemDTO.departmentMaster);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentName, cemcemDTO.departmentName);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentphone, cemcemDTO.departmentphone);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentemail, cemcemDTO.departmentemail);
            UtilsDatabase.PopularStringTextboxJson(tbcontact, cemcemDTO.contact);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentfax, cemcemDTO.departmentfax);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHead, cemcemDTO.deptHead);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPost, cemcemDTO.deptHeadPost);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPost, cemcemDTO.deptHeadPost);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPhone, cemcemDTO.deptHeadPhone);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadEmail, cemcemDTO.deptHeadEmail);
            UtilsDatabase.PopularStringTextboxJson(tbbdoc_number1, cemcemDTO.doc_number1);
            UtilsDatabase.PopularDatetimeTextboxJson(timeedoc_date1, cemcemDTO.doc_date1);
            UtilsDatabase.PopularStringTextboxJson(tbbdoc_person_type1, cemcemDTO.doc_person_type1);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_files, cemcemDTO.doc_file);

            UtilsDatabase.PopularComboboxJson(cbdepartmentMaster6, cemcemDTO.departmentMaster6);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentName6, cemcemDTO.departmentName6);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentphone6, cemcemDTO.departmentphone6);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentemail6, cemcemDTO.departmentemail6);
            UtilsDatabase.PopularStringTextboxJson(tbcontact6, cemcemDTO.contact6);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentfax6, cemcemDTO.departmentfax6);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHead6, cemcemDTO.deptHead6);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPost6, cemcemDTO.deptHeadPost6);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPhone6, cemcemDTO.deptHeadPhone6);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadEmail6, cemcemDTO.deptHeadEmail6);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_number6, cemcemDTO.doc_number6);
            UtilsDatabase.PopularDatetimeTextboxJson(timedoc_date6, cemcemDTO.doc_date6);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_person_type6, cemcemDTO.doc_person_type6);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_files6, cemcemDTO.doc_file6);

            UtilsDatabase.PopularComboboxJson(cbdepartmentMaster7, cemcemDTO.departmentMaster7);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentName7, cemcemDTO.departmentName7);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentphone7, cemcemDTO.departmentphone7);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentemail7, cemcemDTO.departmentemail7);
            UtilsDatabase.PopularStringTextboxJson(tbcontact7, cemcemDTO.contact7);
            UtilsDatabase.PopularStringTextboxJson(tbdepartmentfax7, cemcemDTO.departmentfax7);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHead7, cemcemDTO.deptHead7);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPost7, cemcemDTO.deptHeadPost7);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadPhone7, cemcemDTO.deptHeadPhone7);
            UtilsDatabase.PopularStringTextboxJson(tbdeptHeadEmail7, cemcemDTO.deptHeadEmail7);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_number7, cemcemDTO.doc_number7);
            UtilsDatabase.PopularDatetimeTextboxJson(timedoc_date7, cemcemDTO.doc_date7);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_person_type7, cemcemDTO.doc_person_type7);
            UtilsDatabase.PopularStringTextboxJson(tbdoc_files7, cemcemDTO.doc_file7);

            UtilsDatabase.PopularComboboxJson(cbdeptCDT, cemcemDTO.deptCDT);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTName, cemcemDTO.deptCDTName);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTphone, cemcemDTO.deptCDTphone);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTemail, cemcemDTO.deptCDTemail);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTcontact, cemcemDTO.deptCDTcontact);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTfax, cemcemDTO.deptCDTfax);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTHead, cemcemDTO.deptCDTHead);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTPost, cemcemDTO.deptCDTPost);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTTel, cemcemDTO.deptCDTTel);
            UtilsDatabase.PopularStringTextboxJson(tbdeptCDTHeadEmail, cemcemDTO.deptCDTHeadEmail);

            UtilsDatabase.PopularComboboxJson(cbdeptQL, cemcemDTO.deptQL);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLName, cemcemDTO.deptQLName);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLphone, cemcemDTO.deptQLPhone);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLemail, cemcemDTO.deptQLEmail);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLcontact, cemcemDTO.deptQLContact);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLfax, cemcemDTO.deptQLfax);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLHead, cemcemDTO.deptQLHead);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLPost, cemcemDTO.deptQLPost);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLTel, cemcemDTO.deptQLHeadTel);
            UtilsDatabase.PopularStringTextboxJson(tbdeptQLHeadEmail, cemcemDTO.deptQLHeadEmail);

            //Update DVDT
            foreach (CecmProgramSubDeptDTO subDTO in cemcemDTO.lst_dept_investigate)
            {
                string sql =
                    "INSERT INTO dept_tham_gia(" +
                    "dept_id_web, dept_other, phone, email, address, fax, head, head_pos, head_phone, head_email, doc_number, " + (subDTO.doc_dateST != null ? "doc_date, " : "") + "doc_person, doc_file, cecm_program_id, table_name) " +
                    "VALUES(" +
                    "@dept_id, @dept_other, @phone, @email, @address, @fax, @head, @head_pos, @head_phone, @head_email, @doc_number, " + (subDTO.doc_dateST != null ? "@doc_date, " : "") + "@doc_person, @doc_file, @cecm_program_id, @table_name) ";
                SqlCommand cmd = new SqlCommand(sql, frmLoggin.sqlCon);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", subDTO.dept_id.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_other", subDTO.dept_other, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", subDTO.dept_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", subDTO.dept_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", subDTO.dept_contact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "fax", subDTO.dept_fax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head", subDTO.depthead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_pos", subDTO.depthead_pos, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_phone", subDTO.depthead_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_email", subDTO.depthead_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", subDTO.doc_number, false);
                if(subDTO.doc_dateST != null)
                {
                    DateTimePicker dtp_temp = new DateTimePicker();
                    dtp_temp.Value = DateTime.TryParse(subDTO.doc_dateST, out DateTime doc_date) ? doc_date : DateTime.Now;
                    UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", dtp_temp);
                }
                
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person", subDTO.doc_person_type, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_file", subDTO.doc_file, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", _IdDuAn + "", false);
                //UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "table_name", TableName.DIEU_TRA, false);
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                cmd.ExecuteNonQuery();
            }

            //Update DVKS
            foreach (CecmProgramSubDeptDTO subDTO in cemcemDTO.lst_dept_survey)
            {
                string sql =
                    "INSERT INTO dept_tham_gia(" +
                    "dept_id_web, dept_other, phone, email, address, fax, head, head_pos, head_phone, head_email, doc_number, " + (subDTO.doc_dateST != null ? "doc_date, " : "") + "doc_person, doc_file, cecm_program_id, table_name) " +
                    "VALUES(" +
                    "@dept_id, @dept_other, @phone, @email, @address, @fax, @head, @head_pos, @head_phone, @head_email, @doc_number, " + (subDTO.doc_dateST != null ? "@doc_date, " : "") + "@doc_person, @doc_file, @cecm_program_id, @table_name) ";
                SqlCommand cmd = new SqlCommand(sql, frmLoggin.sqlCon);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", subDTO.dept_id.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_other", subDTO.dept_other, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", subDTO.dept_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", subDTO.dept_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", subDTO.dept_contact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "fax", subDTO.dept_fax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head", subDTO.depthead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_pos", subDTO.depthead_pos, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_phone", subDTO.depthead_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_email", subDTO.depthead_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", subDTO.doc_number, false);
                if (subDTO.doc_dateST != null)
                {
                    DateTimePicker dtp_temp = new DateTimePicker();
                    dtp_temp.Value = DateTime.TryParse(subDTO.doc_dateST, out DateTime doc_date) ? doc_date : DateTime.Now;
                    UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", dtp_temp);
                }

                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person", subDTO.doc_person_type, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_file", subDTO.doc_file, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", _IdDuAn + "", false);
                //UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "table_name", TableName.KHAO_SAT, false);
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                cmd.ExecuteNonQuery();
            }

            //Update DVRP
            foreach (CecmProgramSubDeptDTO subDTO in cemcemDTO.lst_dept_destroy)
            {
                string sql =
                    "INSERT INTO dept_tham_gia(" +
                    "dept_id_web, dept_other, phone, email, address, fax, head, head_pos, head_phone, head_email, doc_number, " + (subDTO.doc_dateST != null ? "doc_date, " : "") + "doc_person, doc_file, cecm_program_id, table_name) " +
                    "VALUES(" +
                    "@dept_id, @dept_other, @phone, @email, @address, @fax, @head, @head_pos, @head_phone, @head_email, @doc_number, " + (subDTO.doc_dateST != null ? "@doc_date, " : "") + "@doc_person, @doc_file, @cecm_program_id, @table_name) ";
                SqlCommand cmd = new SqlCommand(sql, frmLoggin.sqlCon);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", subDTO.dept_id.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_other", subDTO.dept_other, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", subDTO.dept_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", subDTO.dept_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", subDTO.dept_contact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "fax", subDTO.dept_fax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head", subDTO.depthead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_pos", subDTO.depthead_pos, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_phone", subDTO.depthead_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_email", subDTO.depthead_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", subDTO.doc_number, false);
                if (subDTO.doc_dateST != null)
                {
                    DateTimePicker dtp_temp = new DateTimePicker();
                    dtp_temp.Value = DateTime.TryParse(subDTO.doc_dateST, out DateTime doc_date) ? doc_date : DateTime.Now;
                    UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", dtp_temp);
                }

                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person", subDTO.doc_person_type, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_file", subDTO.doc_file, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", _IdDuAn + "", false);
                //UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "table_name", TableName.RA_PHA, false);
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                cmd.ExecuteNonQuery();
            }

            //Update DVTTGD
            foreach (CecmProgramSubDeptDTO subDTO in cemcemDTO.lst_dept_edu)
            {
                string sql =
                    "INSERT INTO dept_tham_gia(" +
                    "dept_id_web, dept_other, phone, email, address, fax, head, head_pos, head_phone, head_email, doc_number, " + (subDTO.doc_dateST != null ? "doc_date, " : "") + "doc_person, doc_file, cecm_program_id, table_name) " +
                    "VALUES(" +
                    "@dept_id, @dept_other, @phone, @email, @address, @fax, @head, @head_pos, @head_phone, @head_email, @doc_number, " + (subDTO.doc_dateST != null ? "@doc_date, " : "") + "@doc_person, @doc_file, @cecm_program_id, @table_name) ";
                SqlCommand cmd = new SqlCommand(sql, frmLoggin.sqlCon);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", subDTO.dept_id.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_other", subDTO.dept_other, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", subDTO.dept_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", subDTO.dept_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", subDTO.dept_contact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "fax", subDTO.dept_fax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head", subDTO.depthead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_pos", subDTO.depthead_pos, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_phone", subDTO.depthead_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_email", subDTO.depthead_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", subDTO.doc_number, false);
                if (subDTO.doc_dateST != null)
                {
                    DateTimePicker dtp_temp = new DateTimePicker();
                    dtp_temp.Value = DateTime.TryParse(subDTO.doc_dateST, out DateTime doc_date) ? doc_date : DateTime.Now;
                    UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", dtp_temp);
                }

                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person", subDTO.doc_person_type, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_file", subDTO.doc_file, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", _IdDuAn + "", false);
                //UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "table_name", TableName.TUYEN_TRUYEN_GIAO_DUC, false);
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                cmd.ExecuteNonQuery();
            }

            //Update CQHTNN
            foreach (CecmProgramSubDeptDTO subDTO in cemcemDTO.lst_dept_support)
            {
                string sql =
                    "INSERT INTO dept_tham_gia(" +
                    "dept_id_web, dept_other, phone, email, address, fax, head, head_pos, head_phone, head_email, doc_number, " + (subDTO.doc_dateST != null ? "doc_date, " : "") + "doc_person, doc_file, cecm_program_id, table_name) " +
                    "VALUES(" +
                    "@dept_id, @dept_other, @phone, @email, @address, @fax, @head, @head_pos, @head_phone, @head_email, @doc_number, " + (subDTO.doc_dateST != null ? "@doc_date, " : "") + "@doc_person, @doc_file, @cecm_program_id, @table_name) ";
                SqlCommand cmd = new SqlCommand(sql, frmLoggin.sqlCon);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", subDTO.dept_id.ToString(), true, SqlDbType.BigInt);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_other", subDTO.dept_other, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", subDTO.dept_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", subDTO.dept_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", subDTO.dept_contact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "fax", subDTO.dept_fax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head", subDTO.depthead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_pos", subDTO.depthead_pos, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_phone", subDTO.depthead_phone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "head_email", subDTO.depthead_email, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", subDTO.doc_number, false);
                if (subDTO.doc_dateST != null)
                {
                    DateTimePicker dtp_temp = new DateTimePicker();
                    dtp_temp.Value = DateTime.TryParse(subDTO.doc_dateST, out DateTime doc_date) ? doc_date : DateTime.Now;
                    UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", dtp_temp);
                }

                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person", subDTO.doc_person_type, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_file", subDTO.doc_file, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", _IdDuAn + "", false);
                //UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "table_name", TableName.HO_TRO_NAN_NHAN, false);
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                cmd.ExecuteNonQuery();
            }

            UpdateDonViThamGia(false);
        }

        private void PopularThongTinChung(CecmProgramComplexDTO dataJson)
        {
            var cemcemDTO = dataJson.cecmProgramDTO;

            UtilsDatabase.PopularComboboxJson(cbProgramType, cemcemDTO.programType);
            UtilsDatabase.PopularStringTextboxJson(tbParentName, cemcemDTO.parentName);
            UtilsDatabase.PopularStringTextboxJson(tbParentName, cemcemDTO.parentName);
            UtilsDatabase.PopularStringTextboxJson(tbCode, cemcemDTO.code);
            UtilsDatabase.PopularStringTextboxJson(tbName, cemcemDTO.name);
            UtilsDatabase.PopularComboboxJson(cbIsActive, cemcemDTO.isActive);
            UtilsDatabase.PopularNumberTextboxJson(tbMoney_pre, cemcemDTO.money_pre);
            UtilsDatabase.PopularNumberTextboxJson(tbMoneyTotal, cemcemDTO.moneyTotal);
            UtilsDatabase.PopularNumberTextboxJson(tb_Acreage, cemcemDTO.acreage);
            UtilsDatabase.PopularNumberTextboxJson(tbAcreagesea, cemcemDTO.acreagesea);
            UtilsDatabase.PopularNumberTextboxJson(tb_Acreage, cemcemDTO.acreagewater);
            UtilsDatabase.PopularDatetimeTextboxJson(timeStartTime, cemcemDTO.startTime);
            UtilsDatabase.PopularDatetimeTextboxJson(timeEndTime, cemcemDTO.endTime);
            UtilsDatabase.PopularNumberTextboxJson(tbtimeTotal, cemcemDTO.timeTotal);
            UtilsDatabase.PopularComboboxJson(cbMoneySource, cemcemDTO.moneySource);
            UtilsDatabase.PopularComboboxTinhJson(cbProviceId, cemcemDTO.proviceId);
            UtilsDatabase.PopularComboboxTextboxTinhJson(cbProviceIdOther, cemcemDTO.lst_proviceOther);
            UtilsDatabase.PopularStringTextboxJson(tbAddress, cemcemDTO.address);

            UtilsDatabase.PopularComboboxToCheckboxJson(cbkDoc_lstAction_type, cemcemDTO.doc_lstAction_type);
            UtilsDatabase.PopularComboboxToCheckboxJson(cbkLst_step_type, cemcemDTO.lst_step_type);
            UtilsDatabase.PopularComboboxToCheckboxJson(cbkDoc_source_money, cemcemDTO.doc_source_money, cbkDoc_source_money5);
            UtilsDatabase.PopularComboboxToCheckboxJson(cbklst_person_assign, cemcemDTO.lst_person_assign, cbkLst_person_assign5);

            UtilsDatabase.PopularStringTextboxJson(tbDoc_number, cemcemDTO.doc_number);
            UtilsDatabase.PopularDatetimeTextboxJson(timeDoc_date, cemcemDTO.doc_date);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_person_type, cemcemDTO.doc_person_type);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_file, cemcemDTO.doc_file);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_number4, cemcemDTO.doc_number4);
            UtilsDatabase.PopularDatetimeTextboxJson(timeDoc_date4, cemcemDTO.doc_date4);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_person_type4, cemcemDTO.doc_person_type4);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_file4, cemcemDTO.doc_file4);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_number5, cemcemDTO.doc_number5);
            UtilsDatabase.PopularDatetimeTextboxJson(timeDoc_date5, cemcemDTO.doc_date5);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_person_type5, cemcemDTO.doc_person_type5);
            UtilsDatabase.PopularStringTextboxJson(tbDoc_file5, cemcemDTO.doc_file5);

            UpdateThongTinChung(false);

            _TenDuAn = tbParentName.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateThongTinChung(false);

            UpdateDonViThamGia(true);

            _ExtraInfoConnettion.Transaction.Commit();
            this.Close();
        }

        private bool UpdateThongTinChung(bool isShowMess)
        {
            try
            {
                int idDuAn = UtilsDatabase.GetIdDuAnByMa(_ExtraInfoConnettion, tbCode.Text.Trim());
                SqlCommand cmd = null;
                if(idDuAn < 0)
                {
                    idDuAn = _IdDuAn;
                }

                if (idDuAn >= 0)
                {
                    cmd = new SqlCommand(string.Format("UPDATE "
                        + "{0} SET "
                        + "programType = @programType,"
                        + "parentName = @parentName,"
                        + "code = @code,"
                        + "isActive = @isActive,"
                        + "money_pre = @money_pre,"
                        + "moneyTotal = @moneyTotal,"
                        + "acreage = @acreage,"
                        + "acreagewater = @acreagewater,"
                        + "acreagesea = @acreagesea,"
                        + "startTime = @startTime,"
                        + "endTime = @endTime,"
                        + "timeTotal = @timeTotal,"
                        + "proviceId = @proviceId,"
                        + "proviceIdOther = @proviceIdOther,"
                        + "address = @address,"
                        + "moneySource = @moneySource,"
                        + "doc_lstAction_type = @doc_lstAction_type,"
                        + "lst_step_type = @lst_step_type,"
                        + "doc_source_money = @doc_source_money,"
                        + "source_mouney_other = @source_mouney_other,"
                        + "lst_person_assign = @lst_person_assign,"
                        + "person_assign_other = @person_assign_other,"
                        + "doc_number = @doc_number,"
                        + "doc_date = @doc_date,"
                        + "doc_person_type = @doc_person_type,"
                        + "doc_number4 = @doc_number4,"
                        + "doc_date4 = @doc_date4,"
                        + "doc_person_type4 = @doc_person_type4,"
                        + "doc_number5 = @doc_number5,"
                        + "doc_date5 = @doc_date5,"
                        + "doc_person_type5 = @doc_person_type5,"
                        + "doc_files = @doc_files,"
                        + "doc_files4 = @doc_files4,"
                        + "doc_files5 = @doc_files5,"
                        + "dwg = @dwg,"
                        + "name = @name WHERE id = {1}", databaseProgram, idDuAn), _ExtraInfoConnettion.Connection as SqlConnection);
                }
                else
                {
                    // Chua co tao moi
                    cmd = new SqlCommand(string.Format("INSERT INTO "
                        + "{0} "
                        + "(programType,"
                        + "parentName,"
                        + "code,"
                        + "isActive,"
                        + "money_pre,"
                        + "moneyTotal,"
                        + "acreage,"
                        + "acreagewater,"
                        + "acreagesea,"
                        + "startTime,"
                        + "endTime,"
                        + "timeTotal,"
                        + "proviceId,"
                        + "proviceIdOther,"
                        + "address,"
                        + "moneySource,"
                        + "doc_lstAction_type,"
                        + "lst_step_type,"
                        + "doc_source_money,"
                        + "source_mouney_other,"
                        + "lst_person_assign,"
                        + "person_assign_other,"
                        + "doc_number,"
                        + "doc_date,"
                        + "doc_person_type,"
                        + "doc_number4,"
                        + "doc_date4,"
                        + "doc_person_type4,"
                        + "doc_number5,"
                        + "doc_date5,"
                        + "doc_person_type5,"
                        + "doc_files,"
                        + "doc_files4,"
                        + "doc_files5,"
                        + "dwg,"
                        + "name)"
                        + "VALUES("
                        + "@programType,"
                        + "@parentName,"
                        + "@code,"
                        + "@isActive,"
                        + "@money_pre,"
                        + "@moneyTotal,"
                        + "@acreage,"
                        + "@acreagewater,"
                        + "@acreagesea,"
                        + "@startTime,"
                        + "@endTime,"
                        + "@timeTotal,"
                        + "@proviceId,"
                        + "@proviceIdOther,"
                        + "@address,"
                        + "@moneySource,"
                        + "@doc_lstAction_type,"
                        + "@lst_step_type,"
                        + "@doc_source_money,"
                        + "@source_mouney_other,"
                        + "@lst_person_assign,"
                        + "@person_assign_other,"
                        + "@doc_number,"
                        + "@doc_date,"
                        + "@doc_person_type,"
                        + "@doc_number4,"
                        + "@doc_date4,"
                        + "@doc_person_type4,"
                        + "@doc_number5,"
                        + "@doc_date5,"
                        + "@doc_person_type5,"
                        + "@doc_files,"
                        + "@doc_files4,"
                        + "@doc_files5,"
                        + "@dwg,"
                        + "@name)", databaseProgram), _ExtraInfoConnettion.Connection as SqlConnection);
                }

                UtilsDatabase.UpdateDataSqlParameter(cmd, "programType", cbProgramType, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "parentName", tbParentName, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "code", tbCode, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "isActive", cbIsActive, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "money_pre", tbMoney_pre, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "moneyTotal", tbMoneyTotal, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "acreage", tb_Acreage, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "acreagewater", tb_Acreagewater, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "acreagesea", tbAcreagesea, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "startTime", timeStartTime);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "endTime", timeEndTime);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "timeTotal", tbtimeTotal, true, SqlDbType.Float);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "proviceId", cbProviceId, true);
                UtilsDatabase.UpdateDataSqlParameterComboboxCheckbox(cmd, "proviceIdOther", cbProviceIdOther);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", tbAddress, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "moneySource", cbMoneySource, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_lstAction_type", cbkDoc_lstAction_type);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "lst_step_type", cbkLst_step_type);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_source_money", cbkDoc_source_money);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "source_mouney_other", cbkDoc_source_money5, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "lst_person_assign", cbklst_person_assign);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "person_assign_other", cbkLst_person_assign5, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number", tbDoc_number, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date", timeDoc_date);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type", tbDoc_person_type, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number4", tbDoc_number4, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date4", timeDoc_date4);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type4", tbDoc_person_type4, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number5", tbDoc_number5, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date5", timeDoc_date5);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type5", tbDoc_person_type5, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files", tbDoc_file, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files4", tbDoc_file4, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files5", tbDoc_file5, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "name", tbName, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dwg", lblDwg, false);

                int temp = 0;
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    if (isShowMess)
                        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");

                    if (idDuAn > 0)
                        _IdDuAn = idDuAn;
                    else
                        _IdDuAn = UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);

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
                return false;
            }
        }

        private bool UpdateDonViThamGia(bool isShowMess)
        {
            try
            {
                int idDuAn = UtilsDatabase.GetIdDuAnByMa(_ExtraInfoConnettion, tbCode.Text.Trim());

                if (idDuAn < 0)
                    return false;

                var cmd = new SqlCommand(string.Format("UPDATE "
                    + "{0} SET "

                    + "departmentMaster2 = @departmentMaster2,"
                    + "departmentName2 = @departmentName2,"
                    + "departmentphone2 = @departmentphone2,"
                    + "departmentemail2 = @departmentemail2,"
                    + "contact2 = @contact2,"
                    + "departmentfax2 = @departmentfax2,"
                    + "deptHead2 = @deptHead2,"
                    + "deptHeadPost2 = @deptHeadPost2,"
                    + "deptHeadPhone2 = @deptHeadPhone2,"
                    + "deptHeadEmail2 = @deptHeadEmail2,"
                    + "doc_number2 = @doc_number2,"
                    + "doc_date2 = @doc_date2,"
                    + "doc_person_type2 = @doc_person_type2,"
                    + "doc_files2 = @doc_files2,"

                    + "departmentMaster3 = @departmentMaster3,"
                    + "departmentName3 = @departmentName3,"
                    + "departmentphone3 = @departmentphone3,"
                    + "departmentemail3 = @departmentemail3,"
                    + "contact3 = @contact3,"
                    + "departmentfax3 = @departmentfax3,"
                    + "deptHead3 = @deptHead3,"
                    + "deptHeadPost3 = @deptHeadPost3,"
                    + "deptHeadPhone3 = @deptHeadPhone3,"
                    + "deptHeadEmail3 = @deptHeadEmail3,"
                    + "doc_number3 = @doc_number3,"
                    + "doc_date3 = @doc_date3,"
                    + "doc_person_type3 = @doc_person_type3,"
                    + "doc_files3 = @doc_files3,"

                    + "departmentMaster = @departmentMaster,"
                    + "departmentName = @departmentName,"
                    + "departmentphone = @departmentphone,"
                    + "departmentemail = @departmentemail,"
                    + "contact = @contact,"
                    + "departmentfax = @departmentfax,"
                    + "deptHead = @deptHead,"
                    + "deptHeadPost = @deptHeadPost,"
                    + "deptHeadPhone = @deptHeadPhone,"
                    + "deptHeadEmail = @deptHeadEmail,"
                    + "doc_number1 = @doc_number1,"
                    + "doc_date1 = @doc_date1,"
                    + "doc_person_type1 = @doc_person_type1,"
                    + "doc_files = @doc_files,"

                    + "departmentMaster6 = @departmentMaster6,"
                    + "departmentName6 = @departmentName6,"
                    + "departmentphone6 = @departmentphone6,"
                    + "departmentemail6 = @departmentemail6,"
                    + "contact6 = @contact6,"
                    + "departmentfax6 = @departmentfax6,"
                    + "deptHead6 = @deptHead6,"
                    + "deptHeadPost6 = @deptHeadPost6,"
                    + "deptHeadPhone6 = @deptHeadPhone6,"
                    + "deptHeadEmail6 = @deptHeadEmail6,"
                    + "doc_number6 = @doc_number6,"
                    + "doc_date6 = @doc_date6,"
                    + "doc_person_type6 = @doc_person_type6,"
                    + "doc_files6 = @doc_files6,"

                    + "departmentMaster7 = @departmentMaster7,"
                    + "departmentName7 = @departmentName7,"
                    + "departmentphone7 = @departmentphone7,"
                    + "departmentemail7 = @departmentemail7,"
                    + "contact7 = @contact7,"
                    + "departmentfax7 = @departmentfax7,"
                    + "deptHead7 = @deptHead7,"
                    + "deptHeadPost7 = @deptHeadPost7,"
                    + "deptHeadPhone7 = @deptHeadPhone7,"
                    + "deptHeadEmail7 = @deptHeadEmail7,"
                    + "doc_number7 = @doc_number7,"
                    + "doc_date7 = @doc_date7,"
                    + "doc_person_type7 = @doc_person_type7,"
                    + "doc_files7 = @doc_files7,"

                    + "deptCDT = @deptCDT,"
                    + "deptCDTName = @deptCDTName,"
                    + "deptCDTphone = @deptCDTphone,"
                    + "deptCDTemail = @deptCDTemail,"
                    + "deptCDTcontact = @deptCDTcontact,"
                    + "deptCDTfax = @deptCDTfax,"
                    + "deptCDTHead = @deptCDTHead,"
                    + "deptCDTPost = @deptCDTPost,"
                    + "deptCDTTel = @deptCDTTel,"
                    + "deptCDTHeadEmail = @deptCDTHeadEmail,"

                    + "deptQL = @deptQL,"
                    + "deptQLName = @deptQLName,"
                    + "deptQLphone = @deptQLphone,"
                    + "deptQLemail = @deptQLemail,"
                    + "deptQLcontact = @deptQLcontact,"
                    + "deptQLfax = @deptQLfax,"
                    + "deptQLHead = @deptQLHead,"
                    + "deptQLPost = @deptQLPost,"
                    + "deptQLTel = @deptQLTel,"
                    + "deptQLHeadEmail = @deptQLHeadEmail"

                    + " WHERE id = {1}", databaseProgram, idDuAn), _ExtraInfoConnettion.Connection as SqlConnection);

                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentMaster2", cbdepartmentMaster2, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentName2", tbdepartmentName2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentphone2", tbdepartmentphone2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentemail2", tbdepartmentemail2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "contact2", tbcontact2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentfax2", tbdepartmentfax2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHead2", tbdeptHead2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPost2", tbdeptHeadPost2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPhone2", tbdeptHeadPhone2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadEmail2", tbdeptHeadEmail2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number2", tbdoc_number2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date2", timedoc_date2);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type2", tbdoc_person_type2, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files2", tbdoc_files2, false);

                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentMaster3", cbdepartmentMaster3, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentName3", tbdepartmentName3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentphone3", tbdepartmentphone3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentemail3", tbdepartmentemail3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "contact3", tbcontact3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentfax3", tbdepartmentfax3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHead3", tbdeptHead3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPost3", tbdeptHeadPost3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPhone3", tbdeptHeadPhone3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadEmail3", tbdeptHeadEmail3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number3", tbdoc_number3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date3", timedoc_date3);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type3", tbdoc_person_type3, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files3", tbdoc_files3, false);

                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentMaster", cbdepartmentMaster, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentName", tbdepartmentName, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentphone", tbdepartmentphone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentemail", tbdepartmentemail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "contact", tbcontact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentfax", tbdepartmentfax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHead", tbdeptHead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPost", tbdeptHeadPost, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPhone", tbdeptHeadPhone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadEmail", tbdeptHeadEmail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number1", tbbdoc_number1, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date1", timeedoc_date1);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type1", tbbdoc_person_type1, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files", tbdoc_files, false);

                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentMaster6", cbdepartmentMaster6, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentName6", tbdepartmentName6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentphone6", tbdepartmentphone6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentemail6", tbdepartmentemail6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "contact6", tbcontact6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentfax6", tbdepartmentfax6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHead6", tbdeptHead6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPost6", tbdeptHeadPost6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPhone6", tbdeptHeadPhone6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadEmail6", tbdeptHeadEmail6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number6", tbdoc_number6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date6", timedoc_date6);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type6", tbdoc_person_type6, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files6", tbdoc_files6, false);

                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentMaster7", cbdepartmentMaster7, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentName7", tbdepartmentName7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentphone7", tbdepartmentphone7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentemail7", tbdepartmentemail7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "contact7", tbcontact7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentfax7", tbdepartmentfax7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHead7", tbdeptHead7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPost7", tbdeptHeadPost7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadPhone7", tbdeptHeadPhone7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptHeadEmail7", tbdeptHeadEmail7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_number7", tbdoc_number7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_date7", timedoc_date7);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_person_type7", tbdoc_person_type7, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "doc_files7", tbdoc_files7, false);

                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDT", cbdeptCDT, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTName", tbdeptCDTName, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTphone", tbdeptCDTphone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTemail", tbdeptCDTemail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTcontact", tbdeptCDTcontact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTfax", tbdeptCDTfax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTHead", tbdeptCDTHead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTPost", tbdeptCDTPost, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTTel", tbdeptCDTTel, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptCDTHeadEmail", tbdeptCDTHeadEmail, false);

                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQL", cbdeptQL, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLName", tbdeptQLName, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLphone", tbdeptQLphone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLemail", tbdeptQLemail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLcontact", tbdeptQLcontact, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLfax", tbdeptQLfax, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLHead", tbdeptQLHead, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLPost", tbdeptQLPost, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLTel", tbdeptQLTel, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "deptQLHeadEmail", tbdeptQLHeadEmail, false);

                int temp = 0;
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    if (isShowMess)
                        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");

                    _IdDuAn = UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, databaseProgram);

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
                return false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _ExtraInfoConnettion.Transaction.Rollback();
            this.Close();
        }

        private void btOpentbDoc_file_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbDoc_file.Text = filePath;
        }

        private void btOpentbDoc_file4_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbDoc_file4.Text = filePath;
        }

        private void tbOpentbDoc_file5_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbDoc_file5.Text = filePath;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
        }

        private void DvgKhuVucDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = DvgKhuVucDuAn.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == DvgKhuVucDuAnSua.Index && e.RowIndex >= 0)
            {
                ThemMoiKhuVucNew frm = new ThemMoiKhuVucNew(id, _IdDuAn);
                frm.ShowDialog();

                LoadDataKhuVucDuAn(_IdDuAn);
            }
            else if (e.ColumnIndex == DvgKhuVucDuAnXoa.Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Xác nhận xóa dữ liệu", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "cecm_program_area_map", "id", id.ToString()))
                        MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                    else
                        MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                    LoadDataKhuVucDuAn(_IdDuAn);
                }
            }
        }

        private void DgvDoiRPBM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = DgvDoiRPBM.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == DoiRPBMSua.Index && e.RowIndex >= 0)
            {
                ThemMoiDoiRPBMNew frm = new ThemMoiDoiRPBMNew(id, _IdDuAn);
                frm.ShowDialog();

                LoadDataDoiRPBM(_IdDuAn);
            }
            else if (e.ColumnIndex == DoiRPBMXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramTeam", "id", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataDoiRPBM(_IdDuAn);
            }
        }

        private void dgvMayDo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvMayDo.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == dgvMayDoSua.Index && e.RowIndex >= 0)
            {
                ThemMoiMayDoNew frm = new ThemMoiMayDoNew(id, _IdDuAn);
                frm.ShowDialog();

                LoadDataMayDo(_IdDuAn);
            }
            else if (e.ColumnIndex == dgvMayDoXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramMachineBomb", "id", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataMayDo(_IdDuAn);
            }
        }

        public void DieuHanhDuAn()
        {
            _TenDuAn = tbParentName.Text;
            SelectDieuHanhRPBMForm frm = new SelectDieuHanhRPBMForm(_IdDuAn, lblDwg.Text);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                if (frm._IsNew)
                {
                    _DWGFile = frm._DWGCopy;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    _DWGFile = frm._DWGCopy;
                    _IsUpdate = frm._IsUpdate;
                    this.DialogResult = DialogResult.Yes;
                }
                idDA_DH = _IdDuAn;
            }
            else if (frm.DialogResult == DialogResult.Cancel)
                this.DialogResult = DialogResult.None;
        }

        //private void btDieuHanh_Click(object sender, EventArgs e)
        //{
        //    DieuHanhDuAn();
        //}

        private void CapNhatDuAnNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _ExtraInfoConnettion.Transaction.Rollback();
            }
            catch (Exception)
            {
            }
        }

        private void btTaoDuLieu_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void DgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = DgvNhanVien.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == DgvNhanVienSua.Index && e.RowIndex >= 0)
            {
                ThemMoiNhanVienThamGiaNew frm = new ThemMoiNhanVienThamGiaNew(id, _IdDuAn);
                frm.ShowDialog();

                LoadDataNhanVienThamGia(_IdDuAn);
            }
            else if (e.ColumnIndex == DgvNhanVienXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramStaff", "id", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataNhanVienThamGia(_IdDuAn);
            }
        }

        private void dgvTrangThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvTrangThietBi.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == dgvTrangThietBiSua.Index && e.RowIndex >= 0)
            {
                ThemMoiTrangThietBiThamGiaNew frm = new ThemMoiTrangThietBiThamGiaNew(id, _IdDuAn);
                frm.ShowDialog();

                LoadDataTrangThietBiThamGia(_IdDuAn);
            }
            else if (e.ColumnIndex == dgvTrangThietBiXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "Cecm_ProgramDevice", "id", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataTrangThietBiThamGia(_IdDuAn);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    var strOpnFile = AppUtils.OpenFileDialogCopyData(_IdDuAn);
        //    if (string.IsNullOrEmpty(strOpnFile) == false)
        //        lblDwg.Text = strOpnFile;
        //}

        private void tbDoc_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbDoc_file.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void tbDoc_file4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbDoc_file4.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void tbDoc_file5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbDoc_file5.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void cbkDoc_lstAction_type1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatusGiaiDoan(cbkDoc_lstAction_type1.Checked);
        }

        private void UpdateStatusGiaiDoan(bool isEnable)
        {
            cbkLst_step_type1.Enabled = isEnable;
            cbkLst_step_type2.Enabled = isEnable;
            cbkLst_step_type3.Enabled = isEnable;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbdoc_files2.Text = filePath;
        }

        private void tbdoc_files3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbdoc_files3.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void tbdoc_files_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbdoc_files.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbdoc_files3.Text = filePath;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbdoc_files.Text = filePath;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbdoc_files6.Text = filePath;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string filePath = AppUtils.OpenFileDialogCopy(_IdDuAn);
            if (string.IsNullOrEmpty(filePath) == false)
                tbdoc_files7.Text = filePath;
        }

        private void tbdoc_files6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbdoc_files6.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void tbdoc_files7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), tbdoc_files7.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void lblDwg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(_IdDuAn), lblDwg.Text);
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void btnThemDVDT_Click(object sender, EventArgs e)
        {
            ThemDVDT frm = new ThemDVDT(_IdDuAn);
            frm.ShowDialog();
            LoadDataDVDT(_IdDuAn);
        }

        private void dgvDVDT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvDVDT.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == dgvDVDT_cotSua.Index && e.RowIndex >= 0)
            {
                ThemDVDT frm = new ThemDVDT(_IdDuAn, id);
                frm.ShowDialog();

                LoadDataDVDT(_IdDuAn);
            }
            else if (e.ColumnIndex == dgvDVDT_cotXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "dept_tham_gia", "gid", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataDVDT(_IdDuAn);
            }
        }

        private void btnThemDVKS_Click(object sender, EventArgs e)
        {
            ThemDVKS frm = new ThemDVKS(_IdDuAn);
            frm.ShowDialog();
            LoadDataDVKS(_IdDuAn);
        }

        private void dgvDVKS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvDVKS.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == dgvDVKS_cotSua.Index && e.RowIndex >= 0)
            {
                ThemDVKS frm = new ThemDVKS(_IdDuAn, id);
                frm.ShowDialog();

                LoadDataDVKS(_IdDuAn);
            }
            else if (e.ColumnIndex == dgvDVKS_cotXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "dept_tham_gia", "gid", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataDVKS(_IdDuAn);
            }
        }

        private void btnThemDVRP_Click(object sender, EventArgs e)
        {
            ThemDVRP frm = new ThemDVRP(_IdDuAn);
            frm.ShowDialog();
            LoadDataDVRP(_IdDuAn);
        }

        private void dgvDVRP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvDVRP.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == dgvDVRP_cotSua.Index && e.RowIndex >= 0)
            {
                ThemDVRP frm = new ThemDVRP(_IdDuAn, id);
                frm.ShowDialog();

                LoadDataDVRP(_IdDuAn);
            }
            else if (e.ColumnIndex == dgvDVRP_cotXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "dept_tham_gia", "gid", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataDVRP(_IdDuAn);
            }
        }

        private void btnThemDVTTGD_Click(object sender, EventArgs e)
        {
            ThemDVTTGD frm = new ThemDVTTGD(_IdDuAn);
            frm.ShowDialog();
            LoadDataDVTTGD(_IdDuAn);
        }

        private void dgvDVTTGD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvDVTTGD.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == dgvDVTTGD_cotSua.Index && e.RowIndex >= 0)
            {
                ThemDVTTGD frm = new ThemDVTTGD(_IdDuAn, id);
                frm.ShowDialog();

                LoadDataDVTTGD(_IdDuAn);
            }
            else if (e.ColumnIndex == dgvDVTTGD_cotXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "dept_tham_gia", "gid", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataDVTTGD(_IdDuAn);
            }
        }

        private void btnThemCQHTNN_Click(object sender, EventArgs e)
        {
            ThemCQHTNN frm = new ThemCQHTNN(_IdDuAn);
            frm.ShowDialog();
            LoadDataCQHTNN(_IdDuAn);
        }

        private void dgvCQHTNN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvCQHTNN.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == dgvCQHTNN_cotSua.Index && e.RowIndex >= 0)
            {
                ThemCQHTNN frm = new ThemCQHTNN(_IdDuAn, id);
                frm.ShowDialog();

                LoadDataCQHTNN(_IdDuAn);
            }
            else if (e.ColumnIndex == dgvCQHTNN_cotXoa.Index && e.RowIndex >= 0)
            {
                if (UtilsDatabase.DeleteRowDatabaseById(_ExtraInfoConnettion, "dept_tham_gia", "gid", id.ToString()))
                    MessageBox.Show("Xóa dữ liệu thành công... ", "Thành công");
                else
                    MessageBox.Show("Xóa dữ liệu không thành công ", "Lỗi");

                LoadDataCQHTNN(_IdDuAn);
            }
        }

        private void btnImportLog_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                List<InfoConnect> infoConnects = new List<InfoConnect>();
                foreach (string line in lines)
                {
                    InfoConnect infoConnect = readMQTTLine(line);
                    if(infoConnect == null)
                    {
                        continue;
                    }
                    if(infoConnects.Count > 0)
                    {
                        int lastIndex = infoConnects.Count - 1;
                        if (infoConnect.time_action == infoConnects[lastIndex].time_action)
                        {
                            infoConnects.RemoveAt(lastIndex);
                        }
                    }
                    infoConnects.Add(infoConnect);
                }
                int count = importLog(infoConnects);
                MessageBox.Show("Import thành công " + count + " bản ghi");
            }
        }

        private InfoConnect readMQTTLine(string strMess)
        {
            try
            {
                if (strMess.StartsWith("$MDN"))
                {
                    string[] elements = strMess.Split(',');
                    if (elements.Length <= 10)
                    {
                        return null;
                    }
                    List<string> lstJson = new List<string>();
                    string machineId = elements[1];
                    string time = elements[2] + ":" + elements[3] + ":" + elements[4] + " " + elements[5] + "/" + elements[6] + "/" + elements[7];
                    short numValue = short.Parse(elements[8]);
                    short numGPS = short.Parse(elements[9]);
                    string status = elements[10];
                    int.TryParse(elements[10], out int bitSent);
                    bool isFlag = ((bitSent & 8) > 0);
                    bool isDeep = ((bitSent & 2) > 0);

                    double magnetic = 0.0;
                    if (numGPS <= 0 && numValue <= 0)
                    {
                        return null;
                    }
                    for (short i = 0; i < numValue; i++)
                    {
                        magnetic += double.Parse(elements[11 + i]);
                    }
                    if (numValue != 0)
                    {
                        magnetic /= numValue;
                    }
                    short offset = numValue;
                    offset += 11;
                    double dLat = 0;
                    double dLon = 0;
                    string timeGPS = "";
                    for (short k = 0; k < numGPS; k++)
                    {
                        timeGPS = elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++];
                        //Console.WriteLine("TimeGPS= " + elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++]);
                        try
                        {
                            dLat += double.Parse(elements[offset++]);
                            dLon += double.Parse(elements[offset++]);
                            //Console.WriteLine("Lat,Lon= " + dLat + " " + dLon);
                            //Coordinate cWGS84 = new Coordinate(dLat, dLon);
                            //Console.WriteLine("UTM N,E= " + cWGS84.UTM.Northing + " " + cWGS84.UTM.Easting);
                            // write ObjectID,button,TimeGPS,cWGS84.UTM.Northing,cWGS84.UTM.Easting,dTotal to database for CAD app
                            // create packet send ObjectID,button,TimeGPS,dLon,dLat,dTotal to server
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        //lstJson.Add(json);
                    }
                    if (numGPS != 0)
                    {
                        dLon = dLon / numGPS;
                        dLat = dLat / numGPS;
                    }
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        command = "MDN",
                        machineId = machineId,
                        time = time,
                        status = status,
                        magnetic = magnetic,
                        corner = "00",
                        timeGPS = timeGPS,
                        dLon = dLon,
                        dLat = dLat,
                        isFlag = isFlag,
                        isDeep = isDeep
                        //northing = cWGS84.UTM.Northing,
                        //easting = cWGS84.UTM.Easting,
                        //longZone = cWGS84.UTM.LongZone,
                        //latZone = cWGS84.UTM.LatZone
                    });
                    InfoConnect infoConnect = new InfoConnect();
                    infoConnect.code = machineId;
                    infoConnect.project_id = _IdDuAn;
                    infoConnect.machineBomCode = machineId;
                    double[] utm = AppUtils.ConverLatLongToUTM(dLat, dLon);
                    infoConnect.lat_value = utm[0];
                    infoConnect.long_value = utm[1];
                    infoConnect.the_value = magnetic;
                    CultureInfo enUS = new CultureInfo("en-US");
                    DateTime.TryParseExact(timeGPS, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out DateTime updateTimeData);
                    infoConnect.update_time = DateTime.Now;
                    infoConnect.time_action = updateTimeData;
                    infoConnect.bit_sens = bitSent;
                    infoConnect.isMachineBom = true;
                    infoConnect.coordinate = new GeoJsonPoint<GeoJson2DCoordinates>(GeoJson.Position(dLon, dLat));
                    return infoConnect;
                    //addInfoConnectDTO(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString(), "MDN", magnetic.ToString(), dLon + "; " + dLat, "00");
                }
                // $MDN,fe807857c8c8be98,7,53,14,15,5,2021,4,1,1,1.1234568357,-1.1234568357,2.1234567165,-2.1234567165,7,53,15,15,5,2021,105.7946321670,21.0636426670
                else if (strMess.StartsWith("$MDM"))
                {
                    string[] elements = strMess.Split(',');
                    if (elements.Length <= 10)
                    {
                        return null;
                    }
                    short numValue = short.Parse(elements[8]);
                    short numGPS = short.Parse(elements[9]);
                    double dLat = 0;
                    double dLon = 0;
                    string timeGPS = "";
                    /////////////////////////// first version not process numGPS==0
                    if (numGPS > 0 || numValue > 0)
                    {
                        Console.WriteLine("ObjectID= " + elements[1]);
                        Console.WriteLine("Time= " + elements[2] + ":" + elements[3] + ":" + elements[4] + " " + elements[5] + "/" + elements[6] + "/" + elements[7]);
                        Console.WriteLine("Number value= " + elements[8]);
                        Console.WriteLine("Number GPS= " + elements[9]);
                        Console.WriteLine("Byte status button= " + elements[10]);
                        string machineId = elements[1];
                        string time = elements[2] + ":" + elements[3] + ":" + elements[4] + " " + elements[5] + "/" + elements[6] + "/" + elements[7];
                        string status = elements[10];
                        int.TryParse(elements[10], out int bitSent);
                        bool isFlag = ((bitSent & 8) > 0);
                        bool isDeep = ((bitSent & 2) > 0);
                        byte value;
                        uint led14 = 0, mask = 80;
                        double magnetic = 0;
                        for (short i = 0; i < numValue; i++)
                        {
                            value = byte.Parse(elements[11 + i]);
                            /*
                            if(i== numValue-1)
                                Console.WriteLine(value);
                            else
                                Console.Write(value+",");
                            */
                            led14 = value;
                            led14 &= mask;
                            if (led14 > 0)
                                led14 = 1;
                            mask = value;
                            mask &= 127;
                            if (led14 == 1 && mask > magnetic)
                            {
                                magnetic = mask;
                            }
                            if (i == numValue - 1)
                                Console.WriteLine(mask + "-" + led14); // gia tri thang do - trang thai led 14
                            else
                                Console.Write(mask + "-" + led14 + ","); // gia tri thang do - trang thai led 14
                        }
                        short offset = numValue;
                        offset += 11;
                        for (short k = 0; k < numGPS; k++)
                        {
                            timeGPS = elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++];
                            //Console.WriteLine("TimeGPS= " + elements[offset++] + ":" + elements[offset++] + ":" + elements[offset++] + " " + elements[offset++] + "/" + elements[offset++] + "/" + elements[offset++]);
                            try
                            {
                                dLat += double.Parse(elements[offset++]);
                                dLon += double.Parse(elements[offset++]);
                                //Console.WriteLine("Lat,Lon= " + dLat + " " + dLon);
                                //Coordinate cWGS84 = new Coordinate(dLat, dLon);
                                //Console.WriteLine("UTM N,E= " + cWGS84.UTM.Northing + " " + cWGS84.UTM.Easting);
                                // write ObjectID,button,TimeGPS,cWGS84.UTM.Northing,cWGS84.UTM.Easting,dTotal to database for CAD app
                                // create packet send ObjectID,button,TimeGPS,dLon,dLat,dTotal to server
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            //lstJson.Add(json);
                        }
                        if (numGPS != 0)
                        {
                            dLon = dLon / numGPS;
                            dLat = dLat / numGPS;
                        }
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            command = "MDM",
                            machineId = machineId,
                            time = time,
                            status = status,
                            magnetic = magnetic,
                            corner = "00",
                            timeGPS = timeGPS,
                            dLon = dLon,
                            dLat = dLat,
                            isFlag = isFlag,
                            isDeep = isDeep
                            //northing = cWGS84.UTM.Northing,
                            //easting = cWGS84.UTM.Easting,
                            //longZone = cWGS84.UTM.LongZone,
                            //latZone = cWGS84.UTM.LatZone
                        });
                        InfoConnect infoConnect = new InfoConnect();
                        infoConnect.code = machineId;
                        infoConnect.project_id = _IdDuAn;
                        infoConnect.machineBomCode = machineId;
                        infoConnect.lat_value = dLat;
                        infoConnect.long_value = dLon;
                        infoConnect.the_value = magnetic;
                        CultureInfo enUS = new CultureInfo("en-US");
                        DateTime.TryParseExact(timeGPS, AppUtils.DateTimeSqlMachine, enUS, DateTimeStyles.None, out DateTime updateTimeData);
                        infoConnect.update_time = DateTime.Now;
                        infoConnect.time_action = updateTimeData;
                        infoConnect.bit_sens = bitSent;
                        infoConnect.isMachineBom = false;
                        infoConnect.coordinate = new GeoJsonPoint<GeoJson2DCoordinates>(GeoJson.Position(dLon, dLat));
                        return infoConnect;
                        //addInfoConnectDTO(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString(), "MDM", magnetic.ToString(), dLon + "; " + dLat, "00");
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        //bool addInfoConnectDTO(string IP, string COMMAND, string Magnetic, string GPS, string Corner, DateTime dateTime)
        //{
        //    // call api add history table
        //    DateTime now = DateTime.Now;
        //    string datenow = now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        //    Console.WriteLine("DateTime.Now.ToString: " + datenow);

        //    try
        //    {
        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigURL.LocalMongoUrl + "/addObj");
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            string json = new JavaScriptSerializer().Serialize(new
        //            {
        //                cecmProgramId = _IdDuAn,
        //                //deptId = long.Parse(lblDeptId.Text),
        //                cecmProgramName = lbl_ProgramName.Text,
        //                //username = usernameI,
        //                ip = IP,
        //                command = COMMAND,
        //                magnetic = Magnetic,
        //                gps = GPS,
        //                corner = Corner,
        //                timeStart = datenow,
        //                timeStartST = now.ToString("HH':'mm':'ss' 'dd'/'MM'/'yyyy")
        //            }); ; ;
        //            streamWriter.Write(json);
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            var result = streamReader.ReadToEnd();
        //            Console.WriteLine("result add: " + result);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Không thể gửi dữ liệu lên MongoDB", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    return true;
        //}

        private int importLog(List<InfoConnect> lst)
        {
            var database = frmLoggin.mgCon.GetDatabase("db_cecm");
            if (database != null)
            {
                var collection = database.GetCollection<InfoConnect>("cecm_data");
                collection.InsertMany(lst);
                return lst.Count;
            }
            return 0;
        }
    }
}