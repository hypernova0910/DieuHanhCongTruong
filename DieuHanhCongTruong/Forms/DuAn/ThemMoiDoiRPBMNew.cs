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
    public partial class ThemMoiDoiRPBMNew : Form
    {
        private int _idDoi = -1;
        private int _idDuAn = -1;
        private ConnectionWithExtraInfo _ExtraInfoConnettion = null;

        public ThemMoiDoiRPBMNew(int idDoi, int idDuAn)
        {
            InitializeComponent();

            _idDoi = idDoi;
            _idDuAn = idDuAn;
            _ExtraInfoConnettion = UtilsDatabase._ExtraInfoConnettion;
            if (_ExtraInfoConnettion == null)
                return;
        }

        private void ThemMoiDoiRPBM_Load(object sender, EventArgs e)
        {
            if(_idDoi > 0)
            {
                this.Text = "CHỈNH SỬA ĐỘI RPBM";
            }
            else
            {
                this.Text = "THÊM MỚI ĐỘI RPBM";
            }
            LoadDataForm(null);
        }

        public void LoadDataForm(CecmProgramTeamDTO jsonInfo)
        {
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cbtype, "034");
            UtilsDatabase.LoadCBItemDatabaseGroup(_ExtraInfoConnettion, cblevels, "043");

            if (_idDoi >= 0)
            {
                var lstDatarow = UtilsDatabase.GetAllDataInTableWithId(_ExtraInfoConnettion, "Cecm_ProgramTeam", "id", _idDoi.ToString());
                foreach (var item in lstDatarow)
                {
                    UtilsDatabase.SetTextboxValue(tbname, item, "name", false);
                    UtilsDatabase.SetComboboxValue(cbtype, item, "type");
                    UtilsDatabase.SetComboboxValue(cblevels, item, "levels");
                    UtilsDatabase.SetTextboxValue(tbtotal_member, item, "total_member", true);
                    UtilsDatabase.SetTextboxValue(tbaddress, item, "address", false);
                    UtilsDatabase.SetTextboxValue(tbphone, item, "phone", false);
                    UtilsDatabase.SetTextboxValue(tbemail, item, "email", false);
                    UtilsDatabase.SetTextboxValue(tbnotes, item, "notes", false);
                }
            }

            if (jsonInfo != null)
            {
                UtilsDatabase.PopularStringTextboxJson(tbname, jsonInfo.name);
                UtilsDatabase.PopularComboboxJson(cbtype, jsonInfo.type);
                UtilsDatabase.PopularComboboxJson(cblevels, jsonInfo.levels);
                UtilsDatabase.PopularNumberTextboxJson(tbtotal_member, jsonInfo.total_member);
                UtilsDatabase.PopularStringTextboxJson(tbaddress, jsonInfo.address);
                UtilsDatabase.PopularStringTextboxJson(tbphone, jsonInfo.phone);
                UtilsDatabase.PopularStringTextboxJson(tbemail, jsonInfo.email);
                UtilsDatabase.PopularStringTextboxJson(tbnotes, jsonInfo.notes);
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            UpdateToDatabase(true);
        }

        public bool UpdateToDatabase(bool isShowMess)
        {
            try
            {
                SqlCommand cmd = null;

                if (_idDoi < 0)
                {
                    // Chua co tao moi
                    cmd = new SqlCommand(string.Format("INSERT INTO Cecm_ProgramTeam ("
                        + "name,"
                        + "type,"
                        + "levels,"
                        + "total_member,"
                        + "address,"
                        + "phone,"
                        + "email,"
                        + "notes,"
                        + "cecmProgramId)"

                        + "VALUES("
                        + "@name,"
                        + "@type,"
                        + "@levels,"
                        + "@total_member,"
                        + "@address,"
                        + "@phone,"
                        + "@email,"
                        + "@notes,"
                        + "@cecmProgramId)"), _ExtraInfoConnettion.Connection as SqlConnection);
                }
                else
                {
                    cmd = new SqlCommand(string.Format("UPDATE Cecm_ProgramTeam SET "
                        + "name = @name,"
                        + "type = @type,"
                        + "levels = @levels,"
                        + "total_member = @total_member,"
                        + "address = @address,"
                        + "phone = @phone,"
                        + "email = @email,"
                        + "notes = @notes,"
                        + "cecmProgramId = @cecmProgramId"
                        + " WHERE Cecm_ProgramTeam.id = {0}", _idDoi), _ExtraInfoConnettion.Connection as SqlConnection);
                }

                UtilsDatabase.UpdateDataSqlParameter(cmd, "name", tbname, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "type", cbtype, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "levels", cblevels, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "total_member", tbtotal_member, true);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "address", tbaddress, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "phone", tbphone, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "email", tbemail, false);
                UtilsDatabase.UpdateDataSqlParameter(cmd, "notes", tbnotes, false);
                var texboxTemp = new TextBox();
                texboxTemp.Text = _idDuAn.ToString();
                UtilsDatabase.UpdateDataSqlParameter(cmd, "cecmProgramId", texboxTemp, true);

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
    }
}