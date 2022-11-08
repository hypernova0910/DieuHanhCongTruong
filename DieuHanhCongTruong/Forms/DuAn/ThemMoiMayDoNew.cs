using DieuHanhCongTruong.Common;
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
    public partial class ThemMoiMayDoNew : Form
    {
        private int _idMayDo = -1;
        private int _idDuAn = -1;
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;

        public ThemMoiMayDoNew(int idMayDo, int idDuAn)
        {
            InitializeComponent();

            _idMayDo = idMayDo;
            _idDuAn = idDuAn;
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            if (_ExtraInfoConnettion == null)
                return;
        }

        private void ThemMoiMayDo_Load(object sender, EventArgs e)
        {
            if(_idMayDo > 0)
            {
                this.Text = "CHỈNH SỬA MÁY DÒ";
            }
            else
            {
                this.Text = "THÊM MỚI MÁY DÒ";
            }
            LoadDataForm(null);
        }

        public void LoadDataForm(CecmProgramMachineBombDTO jsonInfo)
        {
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdept_id);
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbstatus, "044");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbtype_standart, "045");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbIsMachineBoom, "056");
            SqlCommandBuilder sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            System.Data.DataTable datatable = new System.Data.DataTable();
            sqlAdapter = new SqlDataAdapter(string.Format("select id, nameId FROM Cecm_ProgramStaff where cecmProgramId = {0} ", _idDuAn), _ExtraInfoConnettion.Connection as SqlConnection);
            sqlAdapter.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            sqlCommand = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(datatable);
            DataRow dr2 = datatable.NewRow();
            dr2["id"] = -1;
            dr2["nameId"] = "Chọn nhận viên";
            datatable.Rows.InsertAt(dr2, 0);
            cbStaff.DataSource = datatable;
            cbStaff.DisplayMember = "nameId";
            cbStaff.ValueMember = "id";

            if (_idMayDo >= 0)
            {
                var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "Cecm_ProgramMachineBomb", "id", _idMayDo.ToString());
                foreach (var item in lstDatarow)
                {
                    UtilsDatabase.SetTextboxValue(tbcode, item, "code", false);
                    UtilsDatabase.SetComboboxValue(cbdept_id, item, "dept_id");
                    UtilsDatabase.SetTextboxValue(tbmac_id, item, "mac_id", false);
                    UtilsDatabase.SetTextboxValue(tbtype_machine, item, "type_machine", false);
                    UtilsDatabase.SetComboboxValue(cbstatus, item, "status");
                    UtilsDatabase.SetDateTimeValue(timetime_test, item, "time_test");
                    //UtilsDatabase.SetComboboxValue(cbStaff, item, "staff_id");
                    if(long.TryParse(item["staff_id"].ToString(), out long staff_id))
                    {
                        cbStaff.SelectedValue = staff_id;
                    }
                    UtilsDatabase.SetComboboxValue(cbtype_standart, item, "type_standart");
                    UtilsDatabase.SetTextboxValue(tbtype_standart_detail, item, "type_standart_detail", false);
                    UtilsDatabase.SetTextboxValue(tbdescription, item, "description", false);
                    UtilsDatabase.SetComboboxValue(cbIsMachineBoom, item, "IsMachineBoom");
                }
            }

            if (jsonInfo != null)
            {
                UtilsDatabase.PopularStringTextboxJson(tbcode, jsonInfo.code);
                UtilsDatabase.PopularComboboxJson(cbdept_id, jsonInfo.dept_id);
                UtilsDatabase.PopularStringTextboxJson(tbdept_idST, jsonInfo.dept_idST);
                UtilsDatabase.PopularStringTextboxJson(tbmac_id, jsonInfo.mac_id);
                UtilsDatabase.PopularStringTextboxJson(tbtype_machine, jsonInfo.type_machineST);
                UtilsDatabase.PopularComboboxJson(cbstatus, jsonInfo.status);
                UtilsDatabase.PopularDatetimeTextboxJson(timetime_test, jsonInfo.time_test);
                UtilsDatabase.PopularComboboxJson(cbStaff, jsonInfo.staffId);
                UtilsDatabase.PopularComboboxJson(cbtype_standart, jsonInfo.type_standart);
                UtilsDatabase.PopularStringTextboxJson(tbtype_standart_detail, jsonInfo.tbtype_standart_detail);
                UtilsDatabase.PopularStringTextboxJson(tbdescription, jsonInfo.description);
                UtilsDatabase.PopularComboboxJson(cbIsMachineBoom, jsonInfo.cecm_ProgramMachineBomb);
            }
        }

        public bool UpdateToDatabase(bool isShowMess)
        {
            try
            {
                SqlCommand cmd = null;

                if (_idMayDo < 0)
                {
                    // Chua co tao moi
                    cmd = new SqlCommand(string.Format("INSERT INTO Cecm_ProgramMachineBomb ("
                        + "cecm_program_id,"
                        + "staff_id,"
                        + "dept_id,"
                        + "dept_idST,"
                        + "code,"
                        + "mac_id,"
                        + "type_machine,"
                        + "status,"
                        + "time_test,"
                        + "type_standart,"
                        + "type_standart_detail,"
                        + "IsMachineBoom,"
                        + "description)"

                        + "VALUES("
                        + "@cecm_program_id,"
                        + "@staff_id,"
                        + "@dept_id,"
                        + "@dept_idST,"
                        + "@code,"
                        + "@mac_id,"
                        + "@type_machine,"
                        + "@status,"
                        + "@time_test,"
                        + "@type_standart,"
                        + "@type_standart_detail,"
                        + "@IsMachineBoom,"
                        + "@description)"), _ExtraInfoConnettion.Connection as SqlConnection);
                }
                else
                {
                    cmd = new SqlCommand(string.Format("UPDATE Cecm_ProgramMachineBomb SET "
                        + "cecm_program_id = @cecm_program_id,"
                        + "staff_id = @staff_id,"
                        + "dept_id = @dept_id,"
                        + "dept_idST = @dept_idST,"
                        + "code = @code,"
                        + "mac_id = @mac_id,"
                        + "type_machine = @type_machine,"
                        + "status = @status,"
                        + "time_test = @time_test,"
                        + "type_standart = @type_standart,"
                        + "type_standart_detail = @type_standart_detail,"
                        + "IsMachineBoom = @IsMachineBoom,"
                        + "description = @description"
                        + " WHERE Cecm_ProgramMachineBomb.id = {0}", _idMayDo), _ExtraInfoConnettion.Connection as SqlConnection);
                }

                var texboxTemp = new TextBox();
                texboxTemp.Text = _idDuAn.ToString();
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", texboxTemp, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "code", tbcode, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", cbdept_id, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_idST", tbdept_idST, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "mac_id", tbmac_id, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type_machine", tbtype_machine, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "status", cbstatus, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "time_test", timetime_test);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "staff_id", cbStaff.SelectedValue.ToString(), false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type_standart", cbtype_standart, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type_standart_detail", tbtype_standart_detail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "description", tbdescription, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "IsMachineBoom", cbIsMachineBoom, true);

                int temp = 0;
                cmd.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
                temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    if (isShowMess)
                        MessageBox.Show("Cập nhật dữ liệu thành công... ", "Thành công");
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

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateToDatabase(true);
        }

        private void cbdept_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbdept_id.SelectedIndex == -1 || cbdept_id.SelectedIndex == 0)
                tbdept_idST.Enabled = true;
            else
                tbdept_idST.Enabled = false;
        }
    }
}