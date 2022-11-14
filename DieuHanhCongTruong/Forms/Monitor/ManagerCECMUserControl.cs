using DieuHanhCongTruong.Command;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.Account;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class ManagerCECMUserControl : UserControl
    {
        //public SqlConnection _Cn = null;
        public int _IDDuAn = int.MinValue;
        public static bool isManaging = false;
        private bool isClick = false;

        public ManagerCECMUserControl()
        {
            InitializeComponent();
        }

        public void LoadTreeView()
        {
            try
            {
                TVDanhSach.Nodes.Clear();
                SqlCommandBuilder sqlCommand = null;
                SqlDataAdapter sqlAdapter = null;
                System.Data.DataTable datatable = new System.Data.DataTable();
                sqlAdapter = new SqlDataAdapter(string.Format("SELECT id,name FROM cecm_programData"), frmLoggin.sqlCon);
                sqlCommand = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.Fill(datatable);
                if (datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in datatable.Rows)
                    {
                        var idDuAn = dr["id"].ToString();
                        var tenDuAn = dr["name"].ToString();

                        var nodeAdded = TVDanhSach.Nodes.Add(tenDuAn);
                        nodeAdded.Tag = idDuAn;

                        SqlDataAdapter sqlAdapterProvince = new SqlDataAdapter(string.Format("SELECT * FROM cecm_program_area_map WHERE cecm_program_area_map.cecm_program_id = '{0}'", idDuAn), frmLoggin.sqlCon);
                        sqlCommand = new SqlCommandBuilder(sqlAdapterProvince);
                        System.Data.DataTable datatableProvince = new System.Data.DataTable();
                        sqlAdapterProvince.Fill(datatableProvince);
                        foreach (DataRow dr1 in datatableProvince.Rows)
                        {
                            var nodeVung = nodeAdded.Nodes.Add(dr1["code"].ToString());
                            var idVungDuAn = dr1["id"].ToString();
                            nodeVung.Tag = idVungDuAn;
                        }
                    }
                }
                TVDanhSach.ExpandAll();
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private void ManagerCECMUserControl_Load(object sender, EventArgs e)
        {
            //_Cn = frmLoggin.sqlCon;
            TVDanhSach.ContextMenuStrip = menuKeHoachTrienKhai;
            LoadTreeView();
        }

        private void ClearSelectedNode(TreeNode treeNode)
        {
            treeNode.ForeColor = Color.Black;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.ForeColor = Color.Black;
                ClearSelectedNode(tn);
            }
        }

        private void TVDanhSach_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        //public List<DataMayDoDuAn> GetAllDateTimePlane(int idDuAn)
        //{
        //    List<DataMayDoDuAn> retVal = new List<DataMayDoDuAn>();
        //    try
        //    {
        //        List<int> listIdDuAn = AppUtils.GetAllIdDuAnCon(idDuAn, _Cn);

        //        foreach (var item in listIdDuAn)
        //        {
        //            SqlCommandBuilder sqlCommand = null;
        //            SqlDataAdapter sqlAdapter = null;
        //            System.Data.DataTable datatable = new System.Data.DataTable();
        //            sqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM plan_time WHERE cecm_program_id = {0}", item), _Cn);
        //            sqlCommand = new SqlCommandBuilder(sqlAdapter);
        //            sqlAdapter.Fill(datatable);
        //            if (datatable.Rows.Count != 0)
        //            {
        //                foreach (DataRow dr in datatable.Rows)
        //                {
        //                    DataMayDoDuAn dataVal = new DataMayDoDuAn();
        //                    dataVal.idTable = int.Parse(dr["id"].ToString());
        //                    dataVal.CecmProgramId = int.Parse(dr["cecm_program_id"].ToString());
        //                    dataVal.StartTime = DateTime.Parse(dr["start_time"].ToString());
        //                    dataVal.EndTime = DateTime.Parse(dr["end_time"].ToString());
        //                    retVal.Add(dataVal);
        //                }
        //            }
        //        }

        //        // Sort
        //        retVal = retVal.OrderBy(x => x.StartTime).ToList();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MyLogger.Log("Đã có lỗi xảy ra khi load dữ liệu! {0}", ex.Message);
        //    }

        //    return retVal;
        //}

        private void TVDanhSach_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void TVDanhSach_DoubleClick(object sender, EventArgs e)
        {
        }

        private void quảnLýDựÁnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get fist node
            TreeNode node = TVDanhSach.SelectedNode;
            if (node == null)
                return;
            while (node.Parent != null)
            {
                node = node.Parent;
            }

            if (TVDanhSach.SelectedNode == null || TVDanhSach.SelectedNode.Tag == null)
                return;
            int.TryParse(TVDanhSach.SelectedNode.Tag.ToString(), out _IDDuAn);

            int programId = int.Parse(TVDanhSach.SelectedNode.Tag.ToString());

            CapNhatDuAnNew frm = new CapNhatDuAnNew(_IDDuAn);
            frm.ShowDialog();

            DieuHanhDuAn(frm);
        }

        private void DieuHanhDuAn(CapNhatDuAnNew frm)
        {
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //TaoDiemDoCMD cmdDiemDo = new TaoDiemDoCMD();
                //cmdDiemDo.ExecuteCMD(frm._TenDuAn, frm._IdDuAn, true);
                MyMainMenu2.idDADH = frm._IdDuAn;
                MapMenuCommand.LoadDuAn(frm._IdDuAn);
                MagneticCommand.Execute(frm._IdDuAn);
            }
            //else if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //{
            //    if (DBUtils.OpenDWFFile(frm._DWGFile))
            //        MyLogger.Log("Mở tệp thành công");

            //    if (frm._IsUpdate)
            //    {
            //        TaoDiemDoCMD cmdDiemDo = new TaoDiemDoCMD();
            //        cmdDiemDo.ExecuteCMD(frm._TenDuAn, frm._IdDuAn, false);
            //    }
            //}
            //else if (frm.DialogResult == System.Windows.Forms.DialogResult.No)
            //{
            //    ChuanBiBanVeCmd cmdChuanBiBanVeCmd = new ChuanBiBanVeCmd();
            //    cmdChuanBiBanVeCmd.ExecuteCMD(frm._TenDuAn, frm._IdDuAn);
            //}

            LoadTreeView();

            //CommandEventData cmdEventData = new CommandEventData();
            //var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            //cmdEventData.SubscribeToDoc(doc);
        }

        private void btThemTabDo_Click(object sender, EventArgs e)
        {
        }

        private void btXoaTabMayDo_Click(object sender, EventArgs e)
        {
        }

        private void thêmMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapNhatDuAnNew frm = new CapNhatDuAnNew(-1);
            frm.ShowDialog();

            //if (frm.DialogResult == System.Windows.Forms.DialogResult.No)
            //{
            //    ChuanBiBanVeCmd cmdChuanBiBanVeCmd = new ChuanBiBanVeCmd();
            //    cmdChuanBiBanVeCmd.ExecuteCMD(frm._TenDuAn, frm._IdDuAn);
            //}

            LoadTreeView();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa dự án", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;

            int susscec2 = 0;

            var lDuanCon = AppUtils.GetAllIdDuAnCon(_IDDuAn, frmLoggin.sqlCon);
            foreach (var item in lDuanCon)
                susscec2 = susscec2 + XoaDuAn(item, frmLoggin.sqlCon);

            susscec2 = susscec2 + XoaDuAn(_IDDuAn, frmLoggin.sqlCon);

            susscec2 = susscec2 + XoaDuAn(_IDDuAn, frmLoggin.sqlCon);

            if (susscec2 > 0)
            {
                MessageBox.Show(string.Format("Đã xóa thành công"));
            }
            else
            {
                MessageBox.Show(string.Format("Không thể xóa"));
            }

            LoadTreeView();
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

        private void xóaToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void menuKeHoachTrienKhai_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var nodeSelected = TVDanhSach.SelectedNode;
            if (nodeSelected == null)
            {
                quảnLýDựÁnToolStripMenuItem.Enabled = false;
                xóaToolStripMenuItem.Enabled = false;
                return;
            }

            if (nodeSelected.Parent != null)
            {
                quảnLýDựÁnToolStripMenuItem.Enabled = false;
                xóaToolStripMenuItem.Enabled = false;
            }
            else
            {
                quảnLýDựÁnToolStripMenuItem.Enabled = true;
                xóaToolStripMenuItem.Enabled = true;
            }
        }

        private void TVDanhSach_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                foreach (TreeNode mnode in TVDanhSach.Nodes)
                    ClearSelectedNode(mnode);

                // Get Id Du an
                TreeNode nodeDuAn = e.Node;
                if (nodeDuAn == null || nodeDuAn.Tag == null)
                    return;
                nodeDuAn.ForeColor = Color.Blue;

                _IDDuAn = GetIdDuAnChaByNodeSelected(e.Node);

                if (e.Node == null || e.Node.Tag == null)
                    return;
                int.TryParse(e.Node.Tag.ToString(), out int idFuAnVungDuAnSelected);
                string tenDuongBao = e.Node.Text.ToString();

                if (idFuAnVungDuAnSelected == _IDDuAn)
                {
                }
                else
                {
                    // Zoom to duong bao
                    //MaganerCecmUsercontrolCmd.ZoomToDuongBao(tenDuongBao);
                    MapMenuCommand.SetBoundKVDA(idFuAnVungDuAnSelected);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private int GetIdDuAnChaByNodeSelected(TreeNode currentNode)
        {
            if (currentNode == null)
                return -1;

            // Get fist node
            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
            }

            if (currentNode.Tag == null)
                return -1;

            return int.Parse(currentNode.Tag.ToString());
        }

        private void TVDanhSach_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            isClick = true;
            _IDDuAn = GetIdDuAnChaByNodeSelected(e.Node);

            CapNhatDuAnNew frm = new CapNhatDuAnNew(_IDDuAn);
            frm.LoadData();
            frm.DieuHanhDuAn();
            try
            {
                UtilsDatabase._ExtraInfoConnettion.Transaction.Rollback();
            }
            catch (Exception)
            {
            }

            if (frm.DialogResult == DialogResult.OK)
            {
                //string strTemplatePath = "acad.dwg";

                //var appLocation = AppUtils.GetAppDataPath();
                //string fileLocation = System.IO.Path.Combine(appLocation, strTemplatePath);
                //if (System.IO.File.Exists(fileLocation) == false)
                //{
                //    MessageBox.Show("Không tìm thấy file template .Dwg");
                //    return;
                //}
                //DocumentCollection acDocMgr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                //acDocMgr.CloseAll();
                //Document acDoc = acDocMgr.Add(fileLocation);
                //acDocMgr.MdiActiveDocument = acDoc;

            }
            //using (DocumentLock acLdoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            //{
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        TaoDiemDoCMD cmd = new TaoDiemDoCMD();
            //        cmd.Import(true);

            //        ChuanBiBanVeCmd cmdChuanBiBanVeCmd = new ChuanBiBanVeCmd();
            //        cmdChuanBiBanVeCmd.ExecuteCMD(frm._TenDuAn, frm._IdDuAn);
            //    }
            //}

            DieuHanhDuAn(frm);
            
            isManaging = true;
        }

        private void deleteProgramDataItem_Click(object sender, EventArgs e)
        {
            FilterDeleteProgramData frm = new FilterDeleteProgramData(_IDDuAn);
            frm.ShowDialog();
        }

        private void TVDanhSach_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            //    if (isClick)
            //    {
            //        e.Cancel = true;
            //        isClick = false;
            //    }
            Point p = this.TVDanhSach.PointToClient(Control.MousePosition);
            TreeNode node = this.TVDanhSach.GetNodeAt(p);
            if (node != null)
            {
                if (node.Bounds.Contains(p))
                {
                    e.Cancel = true;
                }
            }
        }

        private void TVDanhSach_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //if (isClick)
            //{
            //    e.Cancel = true;
            //    isClick = false;
            //}
            Point p = this.TVDanhSach.PointToClient(Control.MousePosition);
            TreeNode node = this.TVDanhSach.GetNodeAt(p);
            if (node != null)
            {
                if (node.Bounds.Contains(p))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}