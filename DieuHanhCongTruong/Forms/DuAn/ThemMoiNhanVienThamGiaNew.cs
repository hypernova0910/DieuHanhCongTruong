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
    public partial class ThemMoiNhanVienThamGiaNew : Form
    {
        private int _idNhanVien = -1;
        private int _idDuAn = -1;
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;

        public ThemMoiNhanVienThamGiaNew(int idNhanVien, int idDuAn)
        {
            InitializeComponent();

            _idNhanVien = idNhanVien;
            _idDuAn = idDuAn;
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            if (_ExtraInfoConnettion == null)
                return;
        }

        private void ThemMoiNhanVienThamGiaNew_Load(object sender, EventArgs e)
        {
            if(_idNhanVien > 0)
            {
                this.Text = "CHỈNH SỬA NHÂN VIÊN THAM GIA";
            }
            else
            {
                this.Text = "THÊM MỚI NHÂN VIÊN THAM GIA";
            }
            LoadDataForm(null);
        }

        public void LoadDataForm(CecmProgramStaffDTO jsonInfo)
        {
            LoadCbDoiRPBM(_idDuAn);
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cblevel, "046");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbtype_standart, "047");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbtheMaster, "048");
            UtilsDatabase.LoadCBDonVi(_ExtraInfoConnettion, cbdepartmentId);
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbrank, "049");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbthesex, "050");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbedu, "052");

            if (_idNhanVien >= 0)
            {
                var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "Cecm_ProgramStaff", "id", _idNhanVien.ToString());
                foreach (var item in lstDatarow)
                {
                    UtilsDatabase.SetTextboxValue(tbnameId, item, "nameId", false);
                    UtilsDatabase.SetTextboxValue(tbcode, item, "code", false);
                    UtilsDatabase.SetComboboxValue(cbcecmProgramTeamId, item, "cecmProgramTeamId");
                    UtilsDatabase.SetComboboxValue(cblevel, item, "level");
                    UtilsDatabase.SetComboboxValue(cbtype_standart, item, "type_standart");
                    UtilsDatabase.SetComboboxValue(cbtheMaster, item, "theMaster");
                    UtilsDatabase.SetTextboxValue(tbStaff_lstSub, item, "Staff_lstSub", false);
                    UtilsDatabase.SetTextboxValue(tbdescription, item, "description", false);
                    UtilsDatabase.SetComboboxValue(cbdepartmentId, item, "departmentId");
                    UtilsDatabase.SetComboboxValue(cbrank, item, "fullName");
                    UtilsDatabase.SetDateTimeValue(timebirthday, item, "birthday");
                    UtilsDatabase.SetComboboxValue(cbthesex, item, "thesex");
                    UtilsDatabase.SetTextboxValue(tbphone, item, "phone", true);
                    UtilsDatabase.SetTextboxValue(tbemail, item, "email", false);
                    UtilsDatabase.SetTextboxValue(tbtype_standart_detail, item, "type_standart_detail", false);
                    UtilsDatabase.SetComboboxValue(cbedu, item, "edu");
                }
            }

            if (jsonInfo != null)
            {
                UtilsDatabase.PopularStringTextboxJson(tbnameId, jsonInfo.nameIdST);
                UtilsDatabase.PopularStringTextboxJson(tbcode, jsonInfo.code);
                UtilsDatabase.PopularComboboxJson(cbcecmProgramTeamId, jsonInfo.cecmProgramTeamIdST);
                UtilsDatabase.PopularComboboxJson(cblevel, jsonInfo.level);
                UtilsDatabase.PopularComboboxJson(cbtype_standart, jsonInfo.type_standart);
                UtilsDatabase.PopularComboboxJson(cbtheMaster, jsonInfo.theMaster);
                //UtilsDatabase.PopularStringTextboxJson(tbStaff_lstSub, jsonInfo.Staff_lstSub);
                UtilsDatabase.PopularComboboxJson(cbdepartmentId, jsonInfo.departmentId);
                //UtilsDatabase.PopularStringTextboxJson(tbdepartmentId, jsonInfo.departmentId);
                UtilsDatabase.PopularComboboxJson(cbrank, jsonInfo.rank);
                UtilsDatabase.PopularDatetimeTextboxJson(timebirthday, jsonInfo.birthday);
                UtilsDatabase.PopularComboboxJson(cbthesex, jsonInfo.thesex);
                UtilsDatabase.PopularNumberTextboxJson(tbphone, jsonInfo.phone);
                UtilsDatabase.PopularStringTextboxJson(tbemail, jsonInfo.email);
                UtilsDatabase.PopularStringTextboxJson(tbtype_standart_detail, jsonInfo.type_standartST);
                UtilsDatabase.PopularComboboxJson(cbedu, jsonInfo.edu);
                UtilsDatabase.PopularStringTextboxJson(tbdescription, jsonInfo.description);
            }
        }

        public bool UpdateToDatabase(bool isShowMess)
        {
            try
            {
                SqlCommand cmd = null;

                if (_idNhanVien < 0)
                {
                    // Chua co tao moi
                    cmd = new SqlCommand(string.Format("INSERT INTO Cecm_ProgramStaff ("
                        + "nameId,"
                        + "cecmProgramId,"
                        + "cecmProgramTeamId,"
                        + "level,"
                        + "theMaster,"
                        + "Staff_lstSub,"
                        + "description,"
                        + "departmentId,"
                        + "rank,"
                        + "birthday,"
                        + "thesex,"
                        + "phone,"
                        + "email,"
                        + "type_standart_detail,"
                        + "edu,"
                        + "code)"

                        + "VALUES("
                        + "@nameId,"
                        + "@cecmProgramId,"
                        + "@cecmProgramTeamId,"
                        + "@level,"
                        + "@theMaster,"
                        + "@Staff_lstSub,"
                        + "@description,"
                        + "@departmentId,"
                        + "@rank,"
                        + "@birthday,"
                        + "@thesex,"
                        + "@phone,"
                        + "@email,"
                        + "@type_standart_detail,"
                        + "@edu,"
                        + "@code)"), _ExtraInfoConnettion.Connection as SqlConnection);
                }
                else
                {
                    cmd = new SqlCommand(string.Format("UPDATE Cecm_ProgramStaff SET "
                        + "tbnameId = @tbnameId,"
                        + " WHERE Cecm_ProgramStaff.id = {0}", _idNhanVien), _ExtraInfoConnettion.Connection as SqlConnection);
                }

                var texboxTemp = new TextBox();
                texboxTemp.Text = _idDuAn.ToString();
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecmProgramId", texboxTemp, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "nameId", tbnameId, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "code", tbcode, false);
                UtilsDatabase.UpdateDataSqlParameterTag(cmd, "cecmProgramTeamId", cbcecmProgramTeamId);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "level", cblevel, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type_standart", cbtype_standart, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "theMaster", cbtheMaster, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "Staff_lstSub", tbStaff_lstSub, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "description", tbdescription, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "departmentId", cbdepartmentId, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "rank", cbrank, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "birthday", timebirthday);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "thesex", cbthesex, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", tbphone, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", tbemail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type_standart_detail", tbtype_standart_detail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "edu", cbedu, true);

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

        private void LoadCbDoiRPBM(int idDuAn)
        {
            List<TinhObject> retVal = new List<TinhObject>();

            SqlConnection cn = _ExtraInfoConnettion.Connection as SqlConnection;
            SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("Select * FROM Cecm_ProgramTeam WHERE Cecm_ProgramTeam.cecmProgramId = {0}", idDuAn), cn);
            sqlAdapterProvince.SelectCommand.Transaction = _ExtraInfoConnettion.Transaction as SqlTransaction;
            var sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
            System.Data.DataTable datatableProvince = new System.Data.DataTable();
            sqlAdapterProvince.Fill(datatableProvince);

            cbcecmProgramTeamId.Items.Add("Chọn");
            cbcecmProgramTeamId.SelectedIndex = 0;

            foreach (DataRow dr in datatableProvince.Rows)
            {
                string id = dr["id"].ToString();
                string tenDoi = dr["name"].ToString();

                TinhObject obj = new TinhObject();
                obj.id = id;
                obj.name = tenDoi;

                cbcecmProgramTeamId.Items.Add(obj);
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            UpdateToDatabase(true);
        }

        private void cbdepartmentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbdepartmentId.SelectedIndex == -1 || cbdepartmentId.SelectedIndex == 0)
                tbdepartmentId.Enabled = true;
            else
                tbdepartmentId.Enabled = false;
        }
    }
}