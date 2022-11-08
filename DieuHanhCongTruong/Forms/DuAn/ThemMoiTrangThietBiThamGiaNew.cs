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
    public partial class ThemMoiTrangThietBiThamGiaNew : Form
    {
        private int _idTrangThietBi = -1;
        private int _idDuAn = -1;
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;

        public ThemMoiTrangThietBiThamGiaNew(int idTrangThietBi, int idDuAn)
        {
            InitializeComponent();

            _idTrangThietBi = idTrangThietBi;
            _idDuAn = idDuAn;
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            if (_ExtraInfoConnettion == null)
                return;
        }

        private void ThemMoiTrangThietBiThamGiaNew_Load(object sender, EventArgs e)
        {
            LoadDataForm(null);
        }

        public bool UpdateToDatabase(bool isShowMess)
        {
            try
            {
                SqlCommand cmd = null;

                if (_idTrangThietBi < 0)
                {
                    // Chua co tao moi
                    cmd = new SqlCommand(string.Format("INSERT INTO Cecm_ProgramDevice ("
                        + "cecm_program_id,"
                        + "nameId,"
                        + "dept_id,"
                        + "nameOther,"
                        + "action_type,"
                        + "name,"
                        + "status,"
                        + "code,"
                        + "time_test,"
                        + "type_standart,"
                        + "type_standart_detail,"
                        + "notes)"

                        + "VALUES("
                        + "@cecm_program_id,"
                        + "@nameId,"
                        + "@dept_id,"
                        + "@nameOther,"
                        + "@action_type,"
                        + "@name,"
                        + "@status,"
                        + "@code,"
                        + "@time_test,"
                        + "@type_standart,"
                        + "@type_standart_detail,"
                        + "@notes)"), _ExtraInfoConnettion.Connection as SqlConnection);
                }
                else
                {
                    cmd = new SqlCommand(string.Format("UPDATE Cecm_ProgramDevice SET "
                        + "cecm_program_id = @cecm_program_id,"
                        + "nameId = @nameId,"
                        + "dept_id = @dept_id,"
                        + "nameOther = @nameOther,"
                        + "action_type = @action_type,"
                        + "name = @name,"
                        + "status = @status,"
                        + "code = @code,"
                        + "time_test = @time_test,"
                        + "type_standart = @type_standart,"
                        + "type_standart_detail = @type_standart_detail,"
                        + "notes = @notes"
                        + " WHERE Cecm_ProgramDevice.id = {0}", _idTrangThietBi), _ExtraInfoConnettion.Connection as SqlConnection);
                }

                var texboxTemp = new TextBox();
                texboxTemp.Text = _idDuAn.ToString();
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecm_program_id", texboxTemp, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "nameId", tbnameId, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "dept_id", cbdept_id, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "nameOther", tbnameOther, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "action_type", cbaction_type, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "status", cbstatus, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type_standart", cbtype_standart, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "name", tbname, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "code", tbcode, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "time_test", timetime_test);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type_standart_detail", tbtype_standart_detail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "notes", tbnotes, false);

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

        public void LoadDataForm(CecmProgramDeviceDTO jsonInfo)
        {
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdept_id);
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbaction_type, "053");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbstatus, "053");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbstatus, "054");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbtype_standart, "055");

            if (_idTrangThietBi >= 0)
            {
                var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "Cecm_ProgramDevice", "id", _idTrangThietBi.ToString());
                foreach (var item in lstDatarow)
                {
                    UtilsDatabase.SetTextboxValue(tbnameId, item, "nameId", false);
                    UtilsDatabase.SetComboboxValue(cbaction_type, item, "action_type");
                    UtilsDatabase.SetComboboxValue(cbstatus, item, "status");
                    UtilsDatabase.SetComboboxValue(cbtype_standart, item, "type_standart");
                    UtilsDatabase.SetTextboxValue(tbnotes, item, "notes", false);
                    UtilsDatabase.SetComboboxValue(cbdept_id, item, "dept_id");
                    UtilsDatabase.SetTextboxValue(tbnameOther, item, "nameOther", false);
                    UtilsDatabase.SetTextboxValue(tbname, item, "name", false);
                    UtilsDatabase.SetTextboxValue(tbcode, item, "code", false);
                    UtilsDatabase.SetDateTimeValue(timetime_test, item, "time_test");
                    UtilsDatabase.SetTextboxValue(tbtype_standart_detail, item, "type_standart_detail", false);
                }
            }

            if (jsonInfo != null)
            {
                UtilsDatabase.PopularStringTextboxJson(tbnameId, jsonInfo.nameIdST);
                UtilsDatabase.PopularComboboxJson(cbaction_type, jsonInfo.action_type);
                UtilsDatabase.PopularComboboxJson(cbstatus, jsonInfo.status);
                UtilsDatabase.PopularComboboxJson(cbtype_standart, jsonInfo.type_standart);
                UtilsDatabase.PopularStringTextboxJson(tbnotes, jsonInfo.notes);
                UtilsDatabase.PopularStringTextboxJson(tbnameOther, jsonInfo.deptIdST);
                UtilsDatabase.PopularComboboxJson(cbdept_id, jsonInfo.deptIdST, true);
                UtilsDatabase.PopularStringTextboxJson(tbname, jsonInfo.name);
                UtilsDatabase.PopularStringTextboxJson(tbcode, jsonInfo.code);
                UtilsDatabase.PopularDatetimeTextboxJson(timetime_test, jsonInfo.time_test);
                UtilsDatabase.PopularStringTextboxJson(tbtype_standart_detail, jsonInfo.type_standart_detail);
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            UpdateToDatabase(true);
        }

        private void cbdept_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbdept_id.SelectedIndex == -1 || cbdept_id.SelectedIndex == 0)
                tbnameOther.Enabled = true;
            else
                tbnameOther.Enabled = false;
        }
    }
}