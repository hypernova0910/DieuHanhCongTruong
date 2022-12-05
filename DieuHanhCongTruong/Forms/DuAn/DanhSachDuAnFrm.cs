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
using System.Web.Script.Serialization;
using System.Net;
using DieuHanhCongTruong.Forms.Account;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Properties;
using DieuHanhCongTruong.Forms;

namespace VNRaPaBomMin
{
    public partial class DanhSachDuAnFrm : Form
    {
        public SqlConnection _Cn = null;

        public DanhSachDuAnFrm()
        {
            InitializeComponent();

            _Cn = frmLoggin.sqlCon;
        }

        private void DanhSachDuAnFrm_Load(object sender, EventArgs e)
        {
            LoadDanhSachDuAn();
        }

        private void LoadDanhSachDuAn()
        {
            try
            {
                dgvDuAn.Rows.Clear();

                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM cecm_programData"), _Cn);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    int indexRow = 1;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idDuAn = dr["id"].ToString();
                        var maDuAn = dr["code"].ToString();
                        var tenDuAn = dr["name"].ToString();
                        var donViThucHien = dr["departmentName2"].ToString();
                        var startTime = (DateTime)dr["startTime"];
                        var endTime = (DateTime)dr["endTime"];
                        var diaDiem = dr["address"].ToString();

                        dgvDuAn.Rows.Add(indexRow, maDuAn, tenDuAn, donViThucHien, startTime.ToString(AppUtils.DateTimeTostring) + " Đến " + endTime.ToString(AppUtils.DateTimeTostring), diaDiem, Resources.Modify, Resources.DeleteRed);
                        dgvDuAn.Rows[indexRow - 1].Tag = idDuAn;

                        indexRow++;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        private void btThemMoi_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand("insert into cecm_programData(code) values ('')", _Cn);
            //cmd.Transaction = UtilsDatabase._ExtraInfoConnettion.Transaction as SqlTransaction;
            //cmd.ExecuteNonQuery();
            //int id = UtilsDatabase.GetLastIdIndentifyTable(UtilsDatabase._ExtraInfoConnettion, "cecm_programData");
            CapNhatDuAnNew frm = new CapNhatDuAnNew(-1);
            frm.Text = "THÊM MỚI DỰ ÁN";
            frm.ShowDialog();

            LoadDanhSachDuAn();
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            CapNhatDuAnNew frm = new CapNhatDuAnNew(-1);
            //frm.LoadData();
            if (frm.NhapDuAn() == false)
            {
                try
                {
                    if(UtilsDatabase._ExtraInfoConnettion.Transaction != null)
                    {
                        UtilsDatabase._ExtraInfoConnettion.Transaction.Rollback();
                    }
                    
                }
                catch (Exception ex)
                {
                    var mess = ex.Message;
                }

                return;
            }
            frm.Close();

            MessageBox.Show(string.Format("Cập nhật dữ liệu thành công"));
            MyMainMenu2.Instance.managerCECMUserControl1.LoadTreeView(false);

            LoadDanhSachDuAn();
        }

        private void dgvDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgvRow = dgvDuAn.Rows[e.RowIndex];
            if (dgvRow.Tag == null)
                return;
            string str = dgvRow.Tag as string;
            int id = int.Parse(str);

            if (e.ColumnIndex == DoiRPBMSua.Index && e.RowIndex >= 0)
            {
                CapNhatDuAnNew frm = new CapNhatDuAnNew(id);
                frm.Text = "CẬP NHẬT DỰ ÁN";
                //frm.btDieuHanh.Enabled = false;
                frm.ShowDialog();

                LoadDanhSachDuAn();
            }
            else if (e.ColumnIndex == DoiRPBMXoa.Index && e.RowIndex >= 0)
            {
                int susscec2 = 0;
                //ManagerCECMUserControl cecm = new ManagerCECMUserControl();
                var lDuanCon = AppUtils.GetAllIdDuAnCon(id, _Cn);
                foreach (var item in lDuanCon)
                    susscec2 = susscec2 + XoaDuAn(item, _Cn);

                susscec2 = susscec2 + XoaDuAn(id, _Cn);

                susscec2 = susscec2 + XoaDuAn(id, _Cn);

                if (susscec2 > 0)
                {
                    MessageBox.Show(string.Format("Đã xóa thành công"));
                }
                else
                {
                    MessageBox.Show(string.Format("Không thể xóa"));
                }

                LoadDanhSachDuAn();
            }
        }

        public int XoaDuAn(int idDuAn, SqlConnection cn)
        {
            try
            {
                SqlCommand cmd2;
                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_work_program WHERE  cecm_work_program.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_program_department WHERE  cecm_program_department.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM dept_tham_gia where cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_programData WHERE  cecm_programData.id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_machinebomb WHERE  cecm_machinebomb.program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM Cecm_ProgramMachineBomb WHERE  Cecm_ProgramMachineBomb.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_plan_machinebomb WHERE  cecm_plan_machinebomb.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_plan_program WHERE  cecm_plan_program.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_plan_program_area WHERE  cecm_plan_program_area.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_plan_program_device_use WHERE  cecm_plan_program_device_use.cecm_program = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_plan_program_method WHERE  cecm_plan_program_method.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_plan_program_process WHERE  cecm_plan_program_process.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_plan_program_process WHERE  cecm_plan_program_process.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM OLuoi WHERE cecm_program_areamap_id in (SELECT id FROM cecm_program_area_map WHERE cecm_program_id = {1})", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_program_area_line WHERE cecmprogram_id = {1}", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_program_area_map WHERE  cecm_program_area_map.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_work_program WHERE  cecm_work_program.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE cert_command_person FROM cert_command INNER JOIN cert_command_person ON cert_command.id = cert_command_person.cert_command_id WHERE  cert_command.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cert_command WHERE  cert_command.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM plan_time WHERE  plan_time.cecm_program_id = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM Cecm_VNTerrainMinePoint_CHA WHERE  Cecm_VNTerrainMinePoint_CHA.programId = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM Cecm_VNTerrainMinePoint WHERE  Cecm_VNTerrainMinePoint.programId = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM Cecm_TerrainRectangle WHERE  Cecm_TerrainRectangle.programId = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd2 = new SqlCommand(string.Format("USE [{0}] DELETE FROM cecm_ReportPollutionArea WHERE  cecm_ReportPollutionArea.programId = {1};", frmLoggin.databaseName, idDuAn), cn);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
                if (CapNhatDuAnNew.idDA_DH == idDuAn)
                {
                    CapNhatDuAnNew.idDA_DH = -1;
                }
                return 1;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImportLog_Click(object sender, EventArgs e)
        {
            
        }
    }
}