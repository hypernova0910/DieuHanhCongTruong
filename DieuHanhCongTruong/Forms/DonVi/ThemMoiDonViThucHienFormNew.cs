using DieuHanhCongTruong.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class ThemMoiDonViThucHienFormNew : Form
    {
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;

        private long id;
        public long ParentID = -1;

        private string tempFolder = "Dept" + Guid.NewGuid().ToString();

        public static string MONEY_SOURCE = "CERT_DEPARTMENT__MONEY_SOURCE";
        public static string FIELD = "CERT_DEPARTMENT__FIELD";

        public ThemMoiDonViThucHienFormNew(long id = -1)
        {
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            this.id = id;
            InitializeComponent();
        }

        private void ThemMoiDonViThucHienFormNew_Load(object sender, EventArgs e)
        {
            DataTable datatable2 = new DataTable();
            datatable2.Columns.Add("id");
            datatable2.Columns.Add("name");
            DataRow dr_ = datatable2.NewRow();
            dr_["id"] = 1;
            dr_["name"] = "Đơn vị RPBM";
            datatable2.Rows.Add(dr_);
            dr_ = datatable2.NewRow();
            dr_["id"] = 2;
            dr_["name"] = "Tổ";
            datatable2.Rows.Add(dr_);
            dr_ = datatable2.NewRow();
            dr_["id"] = 3;
            dr_["name"] = "Đội";
            datatable2.Rows.Add(dr_);
            cbType.DataSource = datatable2;
            cbType.DisplayMember = "name";
            cbType.ValueMember = "id";
            if (id < 0)
            {
                this.Text = "THÊM MỚI ĐƠN VỊ THỰC HIỆN";
            }
            else
            {
                this.Text = "CHỈNH SỬA ĐƠN VỊ THỰC HIỆN";
                LoadDataDonVi(id);
            }
        }

        private void LoadDataDonVi(long idVal)
        {
            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            SqlDataAdapter sqlAdapter = null;

            DataTable datatable = new DataTable();
            datatable.Clear();
            string sql =
                @"SELECT 
                d.id,
                d.name,
                d.address,
                d.phone,
                d.fax,
                d.email,
                d.master,
                d.pos_master,
                d.id_web,
                d.parent_id,
                d.code,
                d.name_english,
                d.type,
                d.money,
                d.money_average,
                d.description,
                d.license,
                d.license_file,
                d.license_start,
                d.license_end,
                d.grant_agency,
                d.grant_sign,
                d.grant_pos,
                d.operation_area,
                d.head_email,
                d.head_phone,
                d_parent.name as parent_idST
                FROM cert_department d
                left join cert_department d_parent on d.parent_id = d_parent.id
                WHERE d.id = {0}";
            sqlAdapter = new SqlDataAdapter(string.Format(sql, idVal), cn);
            sqlAdapter.Fill(datatable);

            if (datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in datatable.Rows)
                {
                    if (long.TryParse(dr["parent_id"].ToString(), out long parent))
                    {
                        ParentID = parent;
                    }
                    tbDeptParent.Text = dr["parent_idST"].ToString();
                    tbCode.Text = dr["code"].ToString();
                    tbName.Text = dr["name"].ToString();
                    tbNameEnglish.Text = dr["name_english"].ToString();
                    if(long.TryParse(dr["type"].ToString(), out long type))
                    {
                        cbType.SelectedValue = type;
                    }
                    tbAddress.Text = dr["address"].ToString();
                    tbPhone.Text = dr["phone"].ToString();
                    tbFax.Text = dr["fax"].ToString();
                    tbEmail.Text = dr["email"].ToString();
                    if(float.TryParse(dr["money"].ToString(), out float money))
                    {
                        nud_money.Value = (decimal)money;
                    }
                    if (float.TryParse(dr["money_average"].ToString(), out float money_average))
                    {
                        nud_money_average.Value = (decimal)money_average;
                    }
                    if (decimal.TryParse(dr["id_web"].ToString(), out decimal id_web))
                    {
                        nudIDWeb.Value = id_web;
                    }
                    else
                    {
                        nudIDWeb.Value = 0;
                    }
                    rtbDescription.Text = dr["description"].ToString();
                    tbLicense.Text = dr["license"].ToString();
                    tbDoc_file.Text = dr["license_file"].ToString();
                    if (DateTime.TryParse(dr["license_start"].ToString(), out DateTime license_start))
                    {
                        dtpLicenseStart.Value = license_start;
                        dtpLicenseStart.Checked = true;
                    }
                    if (DateTime.TryParse(dr["license_end"].ToString(), out DateTime license_end))
                    {
                        dtpLicenseEnd.Value = license_end;
                        dtpLicenseEnd.Checked = true;
                    }
                    tbGrantAgency.Text = dr["grant_agency"].ToString();
                    tbGrantSign.Text = dr["grant_sign"].ToString();
                    tbGrantPos.Text = dr["grant_pos"].ToString();
                    rtbOperationArea.Text = dr["operation_area"].ToString();
                    tbHeadName.Text = dr["master"].ToString();
                    tbHeadPos.Text = dr["pos_master"].ToString();
                    tbHeadEmail.Text = dr["head_email"].ToString();
                    tbHeadPhone.Text = dr["head_phone"].ToString();
                }
            }

            DataTable datatable3 = new System.Data.DataTable();
            datatable3.Clear();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM object_relation WHERE id_first = {0} AND code_relation = '{1}'", idVal, MONEY_SOURCE), cn);
            sqlAdapter.Fill(datatable3);
            foreach (DataRow dr in datatable3.Rows)
            {
                if (int.Parse(dr["id_second"].ToString()) == 1)
                {
                    cb_money_source_1.Checked = true;
                }
                else if(int.Parse(dr["id_second"].ToString()) == 2)
                {
                    cb_money_source_2.Checked = true;
                }
                else if (int.Parse(dr["id_second"].ToString()) == 3)
                {
                    cb_money_source_3.Checked = true;
                }
                else if (int.Parse(dr["id_second"].ToString()) == 4)
                {
                    cb_money_source_4.Checked = true;
                }
            }

            DataTable datatable4 = new DataTable();
            datatable4.Clear();
            sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM object_relation WHERE id_first = {0} AND code_relation = '{1}'", idVal, FIELD), cn);
            sqlAdapter.Fill(datatable4);
            foreach (DataRow dr in datatable4.Rows)
            {
                if (int.Parse(dr["id_second"].ToString()) == 1)
                {
                    cb_field_1.Checked = true;
                }
                else if (int.Parse(dr["id_second"].ToString()) == 2)
                {
                    cb_field_2.Checked = true;
                }
                else if (int.Parse(dr["id_second"].ToString()) == 3)
                {
                    cb_field_3.Checked = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                return;
            }
            if (id < 0)
                AddNewData(true);
            else
                UpdateData(id);
        }

        private void AddNewData(bool _isShowMess)
        {
            try
            {
                SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
                _ExtraInfoConnettion.BeginTransaction();
                SqlCommand cmd1 = new SqlCommand(
                    "INSERT INTO cert_department (" +

                    "parent_id, " +
                    "code, " +
                    "name, " +
                    "name_english, " +
                    "type, " +
                    "address," +
                    "phone," +
                    "fax," +
                    "email," +
                    "money," +
                    "money_average, " +
                    "id_web," +
                    "description, " +
                    "license, " +
                    "license_file, " +
                    "license_start," +
                    "license_end," +
                    "grant_agency," +
                    "grant_sign," +
                    "grant_pos," +
                    "operation_area," +
                    "master," +
                    "pos_master, " +
                    "head_email, " +
                    "head_phone" +

                    " )VALUES(" +

                    "@parent_id, " +
                    "@code, " +
                    "@name, " +
                    "@name_english, " +
                    "@type, " +
                    "@address," +
                    "@phone," +
                    "@fax," +
                    "@email," +
                    "@money," +
                    "@money_average, " +
                    "@id_web," +
                    "@description, " +
                    "@license, " +
                    "@license_file, " +
                    "@license_start," +
                    "@license_end," +
                    "@grant_agency," +
                    "@grant_sign," +
                    "@grant_pos," +
                    "@operation_area," +
                    "@master," +
                    "@pos_master, " +
                    "@head_email, " +
                    "@head_phone" +
                    ")", cn);
                cmd1.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;

                //parent_id
                SqlParameter parent_id = new SqlParameter("@parent_id", SqlDbType.BigInt);
                parent_id.Value = ParentID;
                cmd1.Parameters.Add(parent_id);

                //code
                SqlParameter code = new SqlParameter("@code", SqlDbType.NVarChar, 255);
                code.Value = tbCode.Text;
                cmd1.Parameters.Add(code);

                //name
                SqlParameter name = new SqlParameter("@name", SqlDbType.NVarChar, 255);
                name.Value = tbName.Text;
                cmd1.Parameters.Add(name);

                //name_english
                SqlParameter name_english = new SqlParameter("@name_english", SqlDbType.NVarChar, 255);
                name_english.Value = tbNameEnglish.Text;
                cmd1.Parameters.Add(name_english);

                //type
                SqlParameter type = new SqlParameter("@type", SqlDbType.Int);
                type.Value = cbType.SelectedValue;
                cmd1.Parameters.Add(type);

                //address
                SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 255);
                address.Value = tbAddress.Text;
                cmd1.Parameters.Add(address);

                //phone
                SqlParameter phone = new SqlParameter("@phone", SqlDbType.NVarChar, 255);
                phone.Value = tbPhone.Text;
                cmd1.Parameters.Add(phone);

                //fax
                SqlParameter fax = new SqlParameter("@fax", SqlDbType.NVarChar, 255);
                fax.Value = tbFax.Text;
                cmd1.Parameters.Add(fax);

                //email
                SqlParameter email = new SqlParameter("@email", SqlDbType.NVarChar, 255);
                email.Value = tbEmail.Text;
                cmd1.Parameters.Add(email);

                //money
                SqlParameter money = new SqlParameter("@money", SqlDbType.Float, 255);
                money.Value = (float)nud_money.Value;
                cmd1.Parameters.Add(money);

                //money_average
                SqlParameter money_average = new SqlParameter("@money_average", SqlDbType.Float, 255);
                money_average.Value = nud_money_average.Value;
                cmd1.Parameters.Add(money_average);

                //id_web
                SqlParameter id_web = new SqlParameter("@id_web", SqlDbType.BigInt);
                id_web.Value = nudIDWeb.Value.ToString();
                cmd1.Parameters.Add(id_web);

                //description
                SqlParameter description = new SqlParameter("@description", SqlDbType.NVarChar, 4000);
                description.Value = rtbDescription.Text;
                cmd1.Parameters.Add(description);

                //license
                SqlParameter license = new SqlParameter("@license", SqlDbType.NVarChar, 255);
                license.Value = tbLicense.Text;
                cmd1.Parameters.Add(license);

                //license_file
                SqlParameter license_file = new SqlParameter("@license_file", SqlDbType.NVarChar, 255);
                license_file.Value = tbDoc_file.Text;
                cmd1.Parameters.Add(license_file);

                //license_start
                SqlParameter license_start = new SqlParameter("@license_start", SqlDbType.Date);
                if (dtpLicenseStart.Checked)
                {
                    license_start.Value = dtpLicenseStart.Value;
                }
                else
                {
                    license_start.Value = DBNull.Value;
                }
                cmd1.Parameters.Add(license_start);

                //license_end
                SqlParameter license_end = new SqlParameter("@license_end", SqlDbType.Date);
                if (dtpLicenseEnd.Checked)
                {
                    license_end.Value = dtpLicenseEnd.Value;
                }
                else
                {
                    license_end.Value = DBNull.Value;
                }
                cmd1.Parameters.Add(license_end);

                //grant_agency
                SqlParameter grant_agency = new SqlParameter("@grant_agency", SqlDbType.NVarChar, 255);
                grant_agency.Value = tbGrantAgency.Text;
                cmd1.Parameters.Add(grant_agency);

                //grant_sign
                SqlParameter grant_sign = new SqlParameter("@grant_sign", SqlDbType.NVarChar, 255);
                grant_sign.Value = tbGrantSign.Text;
                cmd1.Parameters.Add(grant_sign);

                //grant_pos
                SqlParameter grant_pos = new SqlParameter("@grant_pos", SqlDbType.NVarChar, 255);
                grant_pos.Value = tbGrantPos.Text;
                cmd1.Parameters.Add(grant_pos);

                //operation_area
                SqlParameter operation_area = new SqlParameter("@operation_area", SqlDbType.NVarChar, 1000);
                operation_area.Value = rtbOperationArea.Text;
                cmd1.Parameters.Add(operation_area);

                //master
                SqlParameter master = new SqlParameter("@master", SqlDbType.NVarChar, 255);
                master.Value = tbHeadName.Text;
                cmd1.Parameters.Add(master);

                //pos_master
                SqlParameter pos_master = new SqlParameter("@pos_master", SqlDbType.NVarChar, 255);
                pos_master.Value = tbHeadPos.Text;
                cmd1.Parameters.Add(pos_master);

                //head_email
                SqlParameter head_email = new SqlParameter("@head_email", SqlDbType.NVarChar, 255);
                head_email.Value = tbHeadEmail.Text;
                cmd1.Parameters.Add(head_email);

                //head_phone
                SqlParameter head_phone = new SqlParameter("@head_phone", SqlDbType.NVarChar, 255);
                head_phone.Value = tbHeadPhone.Text;
                cmd1.Parameters.Add(head_phone);

                int temp1 = 0;
                temp1 = cmd1.ExecuteNonQuery();

                long idVal = UtilsDatabase.GetLastIdIndentifyTable(_ExtraInfoConnettion, "cert_department");
                CopyFolder(idVal);

                bool isFirst1 = true;
                string insert_money_source = "INSERT INTO object_relation(code_relation, id_first, id_second) VALUES";
                if (cb_money_source_1.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 1)";
                    isFirst1 = false;
                }
                if (cb_money_source_2.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 2)";
                    isFirst1 = false;
                }
                if (cb_money_source_3.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 3)";
                    isFirst1 = false;
                }
                if (cb_money_source_4.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 4)";
                    isFirst1 = false;
                }
                if (!isFirst1)
                {
                    SqlCommand cmd3 = new SqlCommand(insert_money_source, cn, _ExtraInfoConnettion.Transaction as SqlTransaction);
                    cmd3.ExecuteNonQuery();
                }
                

                bool isFirst2 = true;
                string insert_field = "INSERT INTO object_relation(code_relation, id_first, id_second) VALUES";
                if (cb_field_1.Checked)
                {
                    if (!isFirst2)
                    {
                        insert_field += ",";
                    }
                    insert_field += "('" + FIELD + "', " + idVal + ", 1)";
                    isFirst2 = false;
                }
                if (cb_field_2.Checked)
                {
                    if (!isFirst2)
                    {
                        insert_field += ",";
                    }
                    insert_field += "('" + FIELD + "', " + idVal + ", 2)";
                    isFirst2 = false;
                }
                if (cb_field_3.Checked)
                {
                    if (!isFirst2)
                    {
                        insert_field += ",";
                    }
                    insert_field += "('" + FIELD + "', " + idVal + ", 3)";
                    isFirst2 = false;
                }
                if (!isFirst2)
                {
                    SqlCommand cmd4 = new SqlCommand(insert_field, cn, _ExtraInfoConnettion.Transaction as SqlTransaction);
                    cmd4.ExecuteNonQuery();
                }
                

                _ExtraInfoConnettion.Transaction.Commit();

                if (temp1 > 0)
                {
                    if (_isShowMess)
                        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    if (_isShowMess)
                        MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                    this.DialogResult = DialogResult.None;
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void UpdateData(long idVal)
        {
            try
            {
                SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
                _ExtraInfoConnettion.BeginTransaction();
                SqlCommand cmd1 = new SqlCommand(string.Format(
                    "UPDATE cert_department SET " +
                    "parent_id=@parent_id, " +
                    "code=@code, " +
                    "name=@name, " +
                    "name_english=@name_english, " +
                    "type=@type, " +
                    "address=@address," +
                    "phone=@phone," +
                    "fax=@fax," +
                    "email=@email," +
                    "money=@money," +
                    "money_average=@money_average, " +
                    "id_web=@id_web," +
                    "description=@description, " +
                    "license=@license, " +
                    "license_file=@license_file, " +
                    "license_start=@license_start," +
                    "license_end=@license_end," +
                    "grant_agency=@grant_agency," +
                    "grant_sign=@grant_sign," +
                    "grant_pos=@grant_pos," +
                    "operation_area=@operation_area," +
                    "master=@master," +
                    "pos_master=@pos_master, " +
                    "head_email=@head_email, " +
                    "head_phone=@head_phone" +

                        " WHERE cert_department.id = {0}", idVal), cn);

                cmd1.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;

                //parent_id
                SqlParameter parent_id = new SqlParameter("@parent_id", SqlDbType.BigInt);
                parent_id.Value = ParentID;
                cmd1.Parameters.Add(parent_id);

                //code
                SqlParameter code = new SqlParameter("@code", SqlDbType.NVarChar, 255);
                code.Value = tbCode.Text;
                cmd1.Parameters.Add(code);

                //name
                SqlParameter name = new SqlParameter("@name", SqlDbType.NVarChar, 255);
                name.Value = tbName.Text;
                cmd1.Parameters.Add(name);

                //name_english
                SqlParameter name_english = new SqlParameter("@name_english", SqlDbType.NVarChar, 255);
                name_english.Value = tbNameEnglish.Text;
                cmd1.Parameters.Add(name_english);

                //type
                SqlParameter type = new SqlParameter("@type", SqlDbType.Int);
                type.Value = cbType.SelectedValue;
                cmd1.Parameters.Add(type);

                //address
                SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar, 255);
                address.Value = tbAddress.Text;
                cmd1.Parameters.Add(address);

                //phone
                SqlParameter phone = new SqlParameter("@phone", SqlDbType.NVarChar, 255);
                phone.Value = tbPhone.Text;
                cmd1.Parameters.Add(phone);

                //fax
                SqlParameter fax = new SqlParameter("@fax", SqlDbType.NVarChar, 255);
                fax.Value = tbFax.Text;
                cmd1.Parameters.Add(fax);

                //email
                SqlParameter email = new SqlParameter("@email", SqlDbType.NVarChar, 255);
                email.Value = tbEmail.Text;
                cmd1.Parameters.Add(email);

                //money
                SqlParameter money = new SqlParameter("@money", SqlDbType.Float, 255);
                money.Value = (float)nud_money.Value;
                cmd1.Parameters.Add(money);

                //money_average
                SqlParameter money_average = new SqlParameter("@money_average", SqlDbType.Float, 255);
                money_average.Value = nud_money_average.Value;
                cmd1.Parameters.Add(money_average);

                //id_web
                SqlParameter id_web = new SqlParameter("@id_web", SqlDbType.BigInt);
                id_web.Value = nudIDWeb.Value.ToString();
                cmd1.Parameters.Add(id_web);

                //description
                SqlParameter description = new SqlParameter("@description", SqlDbType.NVarChar, 4000);
                description.Value = rtbDescription.Text;
                cmd1.Parameters.Add(description);

                //license
                SqlParameter license = new SqlParameter("@license", SqlDbType.NVarChar, 255);
                license.Value = tbLicense.Text;
                cmd1.Parameters.Add(license);

                //license_file
                SqlParameter license_file = new SqlParameter("@license_file", SqlDbType.NVarChar, 255);
                license_file.Value = tbDoc_file.Text;
                cmd1.Parameters.Add(license_file);

                //license_start
                SqlParameter license_start = new SqlParameter("@license_start", SqlDbType.Date);
                if (dtpLicenseStart.Checked)
                {
                    license_start.Value = dtpLicenseStart.Value;
                }
                else
                {
                    license_start.Value = DBNull.Value;
                }
                cmd1.Parameters.Add(license_start);

                //license_end
                SqlParameter license_end = new SqlParameter("@license_end", SqlDbType.Date);
                if (dtpLicenseEnd.Checked)
                {
                    license_end.Value = dtpLicenseEnd.Value;
                }
                else
                {
                    license_end.Value = DBNull.Value;
                }
                cmd1.Parameters.Add(license_end);

                //grant_agency
                SqlParameter grant_agency = new SqlParameter("@grant_agency", SqlDbType.NVarChar, 255);
                grant_agency.Value = tbGrantAgency.Text;
                cmd1.Parameters.Add(grant_agency);

                //grant_sign
                SqlParameter grant_sign = new SqlParameter("@grant_sign", SqlDbType.NVarChar, 255);
                grant_sign.Value = tbGrantSign.Text;
                cmd1.Parameters.Add(grant_sign);

                //grant_pos
                SqlParameter grant_pos = new SqlParameter("@grant_pos", SqlDbType.NVarChar, 255);
                grant_pos.Value = tbGrantPos.Text;
                cmd1.Parameters.Add(grant_pos);

                //operation_area
                SqlParameter operation_area = new SqlParameter("@operation_area", SqlDbType.NVarChar, 1000);
                operation_area.Value = rtbOperationArea.Text;
                cmd1.Parameters.Add(operation_area);

                //master
                SqlParameter master = new SqlParameter("@master", SqlDbType.NVarChar, 255);
                master.Value = tbHeadName.Text;
                cmd1.Parameters.Add(master);

                //pos_master
                SqlParameter pos_master = new SqlParameter("@pos_master", SqlDbType.NVarChar, 255);
                pos_master.Value = tbHeadPos.Text;
                cmd1.Parameters.Add(pos_master);

                //head_email
                SqlParameter head_email = new SqlParameter("@head_email", SqlDbType.NVarChar, 255);
                head_email.Value = tbHeadEmail.Text;
                cmd1.Parameters.Add(head_email);

                //head_phone
                SqlParameter head_phone = new SqlParameter("@head_phone", SqlDbType.NVarChar, 255);
                head_phone.Value = tbHeadPhone.Text;
                cmd1.Parameters.Add(head_phone);

                int temp1 = 0;
                temp1 = cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(string.Format("DELETE FROM object_relation WHERE id_first = {0}", idVal), cn, _ExtraInfoConnettion.Transaction as SqlTransaction);
                cmd2.ExecuteNonQuery();

                bool isFirst1 = true;
                string insert_money_source = "INSERT INTO object_relation(code_relation, id_first, id_second) VALUES";
                if (cb_money_source_1.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 1)";
                    isFirst1 = false;
                }
                if (cb_money_source_2.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 2)";
                    isFirst1 = false;
                }
                if (cb_money_source_3.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 3)";
                    isFirst1 = false;
                }
                if (cb_money_source_4.Checked)
                {
                    if (!isFirst1)
                    {
                        insert_money_source += ",";
                    }
                    insert_money_source += "('" + MONEY_SOURCE + "', " + idVal + ", 4)";
                    isFirst1 = false;
                }
                if (!isFirst1)
                {
                    SqlCommand cmd3 = new SqlCommand(insert_money_source, cn, _ExtraInfoConnettion.Transaction as SqlTransaction);
                    cmd3.ExecuteNonQuery();
                }
                    

                bool isFirst2 = true;
                string insert_field = "INSERT INTO object_relation(code_relation, id_first, id_second) VALUES";
                if (cb_field_1.Checked)
                {
                    if (!isFirst2)
                    {
                        insert_field += ",";
                    }
                    insert_field += "('" + FIELD + "', " + idVal + ", 1)";
                    isFirst2 = false;
                }
                if (cb_field_2.Checked)
                {
                    if (!isFirst2)
                    {
                        insert_field += ",";
                    }
                    insert_field += "('" + FIELD + "', " + idVal + ", 2)";
                    isFirst2 = false;
                }
                if (cb_field_3.Checked)
                {
                    if (!isFirst2)
                    {
                        insert_field += ",";
                    }
                    insert_field += "('" + FIELD + "', " + idVal + ", 3)";
                    isFirst2 = false;
                }
                if (!isFirst2)
                {
                    SqlCommand cmd4 = new SqlCommand(insert_field, cn, _ExtraInfoConnettion.Transaction as SqlTransaction);
                    cmd4.ExecuteNonQuery();
                }
                    

                _ExtraInfoConnettion.Transaction.Commit();
                if (temp1 > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu không thành công ", "Lỗi");
                    _ExtraInfoConnettion.Transaction.Rollback();
                    this.DialogResult = DialogResult.None;
                }
            }
            catch (System.Exception ex)
            {
                _ExtraInfoConnettion.Transaction.Rollback();
                return;
            }
        }

        private void btnChangeDeptParent_Click(object sender, EventArgs e)
        {
            DeptTree deptTree = new DeptTree(this);
            deptTree.ShowDialog();
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (tbName.Text.Trim() == "")
            {
                e.Cancel = true;
                tbName.Focus();
                errorProvider.SetError(tbName, "Chưa nhập tên đơn vị");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(tbName, "");
            }
        }

        private void btOpentbDoc_file_Click(object sender, EventArgs e)
        {
            if(id > 0)
            {
                string filePath = AppUtils.OpenFileDialogCopy("Dept" + id);
                if (string.IsNullOrEmpty(filePath) == false)
                    tbDoc_file.Text = filePath;
            }
            else
            {
                //AppUtils.GetFolderTemp("DeptTemp");
                string filePath = AppUtils.OpenFileDialogCopy(tempFolder);
                if (string.IsNullOrEmpty(filePath) == false)
                    tbDoc_file.Text = filePath;
            }
        }

        private void tbDoc_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile;
            if (id > 0)
            {
                pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp("Dept" + id), tbDoc_file.Text);
            }
            else
            {
                pathFile = System.IO.Path.Combine(AppUtils.GetFolderTemp(tempFolder), tbDoc_file.Text);
            }
            if (System.IO.File.Exists(pathFile))
            {
                var savePath = AppUtils.SaveFileDlg(pathFile);
                AppUtils.CopyFile(pathFile, savePath);
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            tbDoc_file.Text = "";
        }

        private void CopyFolder(long id)
        {
            string destPath = AppUtils.GetFolderTemp("Dept" + id);
            //string strPath = AppUtils.GetRecentInput("$MenuLoaderManagerFrm$lbTepDuongDan");
            string sourcePath = AppUtils.GetFolderTemp(tempFolder);
            AppUtils.Copy(sourcePath, destPath);
            sourcePath = Directory.GetParent(sourcePath).FullName;
            sourcePath = Directory.GetParent(sourcePath).FullName;
            AppUtils.ClearFolder(sourcePath);
        }

        private void tbCode_Validating(object sender, CancelEventArgs e)
        {
            if (tbCode.Text.Trim() == "")
            {
                e.Cancel = true;
                tbCode.Focus();
                errorProvider.SetError(tbCode, "Chưa nhập mã đơn vị");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(tbCode, "");
            }
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.PHONE_REGEX.IsMatch(tbPhone.Text) || tbPhone.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbPhone, "");
            }
            else
            {
                e.Cancel = true;
                tbPhone.Focus();
                errorProvider.SetError(tbPhone, "SĐT chưa đúng định dạng");
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.EMAIL_REGEX.IsMatch(tbEmail.Text) || tbEmail.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbEmail, "");
            }
            else
            {
                e.Cancel = true;
                tbEmail.Focus();
                errorProvider.SetError(tbEmail, "Email chưa đúng định dạng");
            }
        }

        private void tbHeadEmail_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.EMAIL_REGEX.IsMatch(tbHeadEmail.Text) || tbHeadEmail.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbHeadEmail, "");
            }
            else
            {
                e.Cancel = true;
                tbHeadEmail.Focus();
                errorProvider.SetError(tbHeadEmail, "Email chưa đúng định dạng");
            }
        }

        private void tbHeadPhone_Validating(object sender, CancelEventArgs e)
        {
            if (Regexes.PHONE_REGEX.IsMatch(tbHeadPhone.Text) || tbHeadPhone.Text == "")
            {
                e.Cancel = false;
                errorProvider.SetError(tbHeadPhone, "");
            }
            else
            {
                e.Cancel = true;
                tbHeadPhone.Focus();
                errorProvider.SetError(tbHeadPhone, "SĐT chưa đúng định dạng");
            }
        }

        private void dtpLicenseStart_Validating(object sender, CancelEventArgs e)
        {
            if(dtpLicenseStart.Checked && dtpLicenseEnd.Checked)
            {
                if(dtpLicenseStart.Value > dtpLicenseEnd.Value)
                {
                    e.Cancel = true;
                    dtpLicenseStart.Focus();
                    errorProvider.SetError(dtpLicenseStart, "Ngày bắt đầu cần trước ngày kết thúc");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(dtpLicenseStart, "");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(dtpLicenseStart, "");
            }
        }

        private void dtpLicenseEnd_Validating(object sender, CancelEventArgs e)
        {
            if (dtpLicenseStart.Checked && dtpLicenseEnd.Checked)
            {
                if (dtpLicenseStart.Value > dtpLicenseEnd.Value)
                {
                    e.Cancel = true;
                    dtpLicenseEnd.Focus();
                    errorProvider.SetError(dtpLicenseEnd, "Ngày kết thúc cần sau ngày bắt đầu");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(dtpLicenseEnd, "");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(dtpLicenseEnd, "");
            }
        }

        private void tbNameEnglish_Validating(object sender, CancelEventArgs e)
        {
            if (tbNameEnglish.Text.Trim() == "")
            {
                e.Cancel = true;
                tbNameEnglish.Focus();
                errorProvider.SetError(tbNameEnglish, "Chưa nhập tên tiếng Anh");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(tbNameEnglish, "");
            }
        }

        private void tbAddress_Validating(object sender, CancelEventArgs e)
        {
            if (tbAddress.Text.Trim() == "")
            {
                e.Cancel = true;
                tbAddress.Focus();
                errorProvider.SetError(tbAddress, "Chưa nhập địa chỉ đơn vị");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(tbAddress, "");
            }
        }
    }
}
