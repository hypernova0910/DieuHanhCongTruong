using DieuHanhCongTruong.Command;
using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.Account;
using MapWinGIS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VNRaPaBomMin.Models;

namespace VNRaPaBomMin
{
    public partial class KhoangNghiNgoForm : Form
    {
        //private List<ObjectId> _oidCollMine = new List<ObjectId>();
        //private KhoangNghiNgoCmd _cls = new KhoangNghiNgoCmd();
        //private ObjectId _OidDuongBao = ObjectId.Null;
        private List<Shape> lstBomb;
        private List<Shape> lstMine;

        public KhoangNghiNgoForm(List<Shape> lstBomb, List<Shape> lstMine)
        {
            //_oidCollMine = oidCollMine;
            //_OidDuongBao = oidDuongBao;
            this.lstBomb = lstBomb;
            this.lstMine = lstMine;
            InitializeComponent();
        }

        private void KhoangNghiNgoForm_Load(object sender, EventArgs e)
        {
            //int stt = 1;
            //foreach (ObjectId oid in _oidCollMine)
            //{
            //    if (oid.IsValid == false)
            //        continue;

            //    MgdAcDbVNTerrainMinePoint mMine = oid.GetObject(OpenMode.ForRead) as MgdAcDbVNTerrainMinePoint;
            //    if (mMine == null)
            //        continue;

            //    var latLong = AppUtils.ConverUTMToLatLong(mMine.Position.X, mMine.Position.Y);
            //    DGVData.Rows.Add(stt, latLong.X, latLong.Y, mMine.Position.Z, mMine.Deep, mMine.Area, mMine.MineType == 4 ? true : false, mMine.MineType == 2 ? true : false, "");
            //    DGVData.Rows[stt - 1].Tag = mMine.ObjectId;
            //    stt++;
            //}
            int stt = 1;
            foreach(Shape shp in lstBomb)
            {
                if (string.IsNullOrEmpty(shp.Key))
                {
                    continue;
                }
                CecmReportPollutionAreaBMVN bmvn = JsonConvert.DeserializeObject<CecmReportPollutionAreaBMVN>(shp.Key);
                int index = DGVData.Rows.Add(stt, bmvn.Vido, bmvn.Kinhdo, bmvn.ZPoint, bmvn.Deep, bmvn.Area, "Bom", !bmvn.isSaved, bmvn.isSaved);
                if (bmvn.UserAdd)
                {
                    DGVData.Rows[index].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                }
                bmvn.IsBomb = TypeBMVN.BOMB;
                DGVData.Rows[stt - 1].Tag = shp;
                //DGVData.Rows[stt - 1].Tag = mMine.ObjectId;
                stt++;
            }
            foreach (Shape shp in lstMine)
            {
                if (string.IsNullOrEmpty(shp.Key))
                {
                    continue;
                }
                CecmReportPollutionAreaBMVN bmvn = JsonConvert.DeserializeObject<CecmReportPollutionAreaBMVN>(shp.Key);
                int index = DGVData.Rows.Add(stt, bmvn.Vido, bmvn.Kinhdo, bmvn.ZPoint, bmvn.Deep, bmvn.Area, "Mìn", !bmvn.isSaved, bmvn.isSaved);
                if (bmvn.UserAdd)
                {
                    DGVData.Rows[index].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                }
                bmvn.IsBomb = TypeBMVN.MINE;
                DGVData.Rows[stt - 1].Tag = shp;
                //DGVData.Rows[stt - 1].Tag = mMine.ObjectId;
                stt++;
            }
        }

        private void CapNhatDatabaseBoMin(/*ObjectId oidDuongBao*/)
        {
            //var editor = AutoCADApp.DocumentManager.MdiActiveDocument.Editor;
            //var acCurDb = editor.Document.Database;
            //int idDuAn = int.MinValue;

            //using (Transaction acTrans = AutoCADApp.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
            //{
            //    // get all name
            //    ObjectLinkManager.Instance.Load(acTrans, acCurDb);
            //    if (ObjectLinkManager.Instance.IsDictionaryNameExist(AppUtils.m_Dictionary_IdDuAn, acCurDb.NamedObjectsDictionaryId))
            //    {
            //        var mapLink = ObjectLinkManager.Instance.GetLink(AppUtils.m_Dictionary_IdDuAn, acCurDb.NamedObjectsDictionaryId);
            //        idDuAn = int.Parse(mapLink.ChildString);
            //    }
            //    else
            //    {
            //        System.Windows.Forms.MessageBox.Show("Không tìm thấy dự án", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            //        acTrans.Abort();
            //        return;
            //    }
            //    ObjectLinkManager.Instance.Save(acTrans, acCurDb);

            //    MgdAcDbVNTerrainBaseRegion duongBao = oidDuongBao.GetObject(OpenMode.ForRead) as MgdAcDbVNTerrainBaseRegion;
            //    if (duongBao == null)
            //    {
            //        acTrans.Abort();
            //        return;
            //    }
            //    var objectDatabase = GetSelectDuAn(idDuAn, duongBao.Code);
            //    if (objectDatabase == null)
            //    {
            //        acTrans.Abort();
            //        return;
            //    }

            //    var nameBaselineRegion = objectDatabase.ToString();
            //    if (string.IsNullOrEmpty(nameBaselineRegion))
            //    {
            //        acTrans.Abort();
            //        return;
            //    }

            //    var lstSelectedItems = DBUtils.SelectAll(null);
            //    if (lstSelectedItems == null || lstSelectedItems.Count == 0)
            //    {
            //        acTrans.Abort();
            //        return;
            //    }

            //    CapNhatKQRPDB cmd = new CapNhatKQRPDB();
            //    if(long.TryParse(objectDatabase.Id, out long idVungDA))
            //    {
            //        cmd.CapNhatDatabase2(lstSelectedItems, idVungDA, idDuAn);
            //    }
                

            //    acTrans.Commit();
            //}
        }

        //private void FillChart(Dictionary<double, double> mDataVal)
        //{
        //    // clear the chart
        //    charDisplay.Series.Clear();

        //    // fill the chart
        //    var series = charDisplay.Series.Add("My Series");
        //    series.ChartType = SeriesChartType.Line;
        //    series.XValueType = ChartValueType.Int32;

        //    foreach (var item in mDataVal)
        //    {
        //        series.Points.AddXY(item.Key, item.Value);
        //    }
        //    // Visible legend
        //    series.IsVisibleInLegend = false;

        //    var chartArea = charDisplay.ChartAreas[series.ChartArea];

        //    //chartArea.AxisX.Minimum = mDataVal.Min(X => X.Key);
        //    //chartArea.AxisX.Maximum = mDataVal.Max(X => X.Key);

        //    //chartArea.AxisY.Minimum = mDataVal.Min(X => X.Value);
        //    //chartArea.AxisY.Maximum = mDataVal.Max(X => X.Value);

        //    double minValX = mDataVal.Min(X => X.Key);
        //    double maxValX = mDataVal.Max(X => X.Key);
        //    double minValY = mDataVal.Min(X => X.Value);
        //    double maxValY = mDataVal.Max(X => X.Value);

        //    // Interval axisX and axisY
        //    //chartArea.AxisX.Interval = Math.Round(Math.Abs((minValX + maxValX) / 5), 0);

        //    chartArea.AxisX.Interval = 0.25;
        //    chartArea.AxisY.Interval = Math.Round(Math.Abs((minValY + maxValY) / 10), 0);
        //}

        private void DGVData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            var senderGrid = (DataGridView)sender;
            senderGrid.Rows[e.RowIndex].Selected = true;
            //var column = senderGrid.Columns[e.ColumnIndex];
            //if (e.ColumnIndex == Zoom.Index)
            //{

            //}
            if (e.ColumnIndex == cotXoa.Index)
            {
                if (MessageBox.Show("Xác nhận xóa điểm", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Shape shapeBomb = senderGrid.Rows[e.RowIndex].Tag as Shape;
                    CecmReportPollutionAreaBMVN bmvn = JsonConvert.DeserializeObject<CecmReportPollutionAreaBMVN>(shapeBomb.Key);
                    Shapefile sf;
                    if (bmvn.IsBomb == TypeBMVN.BOMB)
                    {
                        sf = MyMainMenu2.Instance.axMap1.get_Shapefile(MapMenuCommand.suspectPointLayerBomb);
                    }
                    else
                    {
                        sf = MyMainMenu2.Instance.axMap1.get_Shapefile(MapMenuCommand.suspectPointLayerMine);
                    }
                    sf.Labels.RemoveLabel(bmvn.indexLabel);
                    shapeBomb.Clear();
                    shapeBomb.Key = null;
                    MapMenuCommand.RemoveHighlight();

                    DGVData.Rows.RemoveAt(e.RowIndex);
                    MyMainMenu2.Instance.axMap1.Redraw();
                }
            }
            else
            {


                Shape shapeBomb = senderGrid.Rows[e.RowIndex].Tag as Shape;
                CecmReportPollutionAreaBMVN bmvn = JsonConvert.DeserializeObject<CecmReportPollutionAreaBMVN>(shapeBomb.Key);
                Extents extents = new Extents();
                extents.SetBounds(bmvn.Kinhdo - 0.00001, bmvn.Vido - 0.00001, 0, bmvn.Kinhdo + 0.00001, bmvn.Vido + 0.00001, 0);
                MyMainMenu2.Instance.axMap1.Extents = extents;
                MapMenuCommand.RemoveHighlight();
                MapMenuCommand.addHighlight(bmvn.Kinhdo, bmvn.Vido);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //using (AutoCADApp.DocumentManager.MdiActiveDocument.LockDocument())
            //{
            //    using (Transaction tr = AutoCADApp.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
            //    {
            //        foreach (DataGridViewRow row in DGVData.Rows)
            //        {
            //            ObjectId oidMine = (ObjectId)row.Tag;
            //            if (oidMine.IsValid == false)
            //                continue;

            //            MgdAcDbVNTerrainMinePoint m_Mine = oidMine.GetObject(OpenMode.ForWrite) as MgdAcDbVNTerrainMinePoint;
            //            if (m_Mine == null)
            //            {
            //                tr.Abort();
            //                return;
            //            }

            //            bool typeQuyetDinh = bool.Parse(row.Cells["QuyetDinh"].Value.ToString());

            //            if (typeQuyetDinh)
            //                m_Mine.MineType = 2;
            //            else
            //                m_Mine.MineType = m_Mine.OriginMineType;
            //        }

            //        // Save database
            //        CapNhatDatabaseBoMin(_OidDuongBao);

            //        tr.Commit();
            //    }
            //}
            int count = 0;
            int countUpdate = 0;
            UtilsDatabase._ExtraInfoConnettion.BeginTransaction();
            foreach (DataGridViewRow row in DGVData.Rows)
            {
                //ObjectId oidMine = (ObjectId)row.Tag;
                //if (oidMine.IsValid == false)
                //    continue;

                //MgdAcDbVNTerrainMinePoint m_Mine = oidMine.GetObject(OpenMode.ForWrite) as MgdAcDbVNTerrainMinePoint;
                //if (m_Mine == null)
                //{
                //    tr.Abort();
                //    return;
                //}

                bool typeQuyetDinh = bool.Parse(row.Cells["QuyetDinh"].Value.ToString());
                bool phanTich = bool.Parse(row.Cells["PhanTich"].Value.ToString());

                //if (typeQuyetDinh)
                //    m_Mine.MineType = 2;
                //else
                //    m_Mine.MineType = m_Mine.OriginMineType;
                if (typeQuyetDinh && phanTich)
                {
                    count++;
                    try
                    {
                        Shape shapeBomb = row.Tag as Shape;
                        CecmReportPollutionAreaBMVN bmvn = JsonConvert.DeserializeObject<CecmReportPollutionAreaBMVN>(shapeBomb.Key);
                        SqlCommand cmd1 = new SqlCommand(
                            "INSERT INTO Cecm_VNTerrainMinePoint (programId, idArea, idRectangle, XPoint, YPoint, ZPoint, Deep, Area, MineType, TimeExecute, Kinhdo, Vido, IsBomb) " +
                            "VALUES(@programId, @idArea, @idRectangle, @XPoint, @YPoint, @ZPoint, @Deep, @Area, @MineType, @TimeExecute, @Kinhdo, @Vido, @IsBomb)", 
                            UtilsDatabase._ExtraInfoConnettion.Connection as SqlConnection, UtilsDatabase._ExtraInfoConnettion.Transaction as SqlTransaction);

                        //idArea
                        SqlParameter idArea = new SqlParameter("@idArea", SqlDbType.BigInt, 255);
                        idArea.Value = bmvn.idArea;
                        cmd1.Parameters.Add(idArea);
                        //ToLatLon(mMine.Position.X, mMine.Position.Y, "48N");

                        //XPoint
                        SqlParameter XPoint = new SqlParameter("@XPoint", SqlDbType.Float);
                        XPoint.Value = bmvn.XPoint;
                        cmd1.Parameters.Add(XPoint);

                        //YPoint
                        SqlParameter YPoint = new SqlParameter("@YPoint", SqlDbType.Float);
                        YPoint.Value = bmvn.YPoint;
                        cmd1.Parameters.Add(YPoint);

                        //ZPoint
                        SqlParameter ZPoint = new SqlParameter("@ZPoint", SqlDbType.Float);
                        ZPoint.Value = bmvn.ZPoint;
                        cmd1.Parameters.Add(ZPoint);

                        //Deep
                        SqlParameter Deep = new SqlParameter("@Deep", SqlDbType.Float);
                        Deep.Value = bmvn.Deep;
                        cmd1.Parameters.Add(Deep);

                        //Area
                        SqlParameter Area = new SqlParameter("@Area", SqlDbType.Float);
                        Area.Value = bmvn.Area;
                        cmd1.Parameters.Add(Area);

                        //Area
                        SqlParameter MineType = new SqlParameter("@MineType", SqlDbType.NVarChar, 255);
                        MineType.Value = bmvn.MineType != null ? bmvn.MineType : "";
                        cmd1.Parameters.Add(MineType);

                        //TimeExecute
                        SqlParameter TimeExecute = new SqlParameter("@TimeExecute", SqlDbType.DateTime, 255);
                        TimeExecute.Value = bmvn.TimeExecute;
                        cmd1.Parameters.Add(TimeExecute);
                        //XPoint
                        SqlParameter Kinhdo = new SqlParameter("@Kinhdo", SqlDbType.Float);
                        Kinhdo.Value = bmvn.Kinhdo;
                        cmd1.Parameters.Add(Kinhdo);

                        //YPoint
                        SqlParameter Vido = new SqlParameter("@Vido", SqlDbType.Float);
                        Vido.Value = bmvn.Vido;
                        cmd1.Parameters.Add(Vido);

                        //idRectangle
                        SqlParameter idRectangle = new SqlParameter("@idRectangle", SqlDbType.BigInt);
                        idRectangle.Value = bmvn.idRectangle;
                        cmd1.Parameters.Add(idRectangle);

                        //programId
                        SqlParameter programId = new SqlParameter("@programId", SqlDbType.BigInt);
                        programId.Value = bmvn.programId;
                        cmd1.Parameters.Add(programId);

                        //IsBomb
                        SqlParameter IsBomb = new SqlParameter("@IsBomb", SqlDbType.Int);
                        IsBomb.Value = bmvn.IsBomb;
                        cmd1.Parameters.Add(IsBomb);

                        countUpdate += cmd1.ExecuteNonQuery();
                        bmvn.isSaved = true;
                        shapeBomb.Key = JsonConvert.SerializeObject(bmvn);
                    }
                    catch(Exception ex)
                    {

                    }
                    

                }
            }

            // Save database
            //CapNhatDatabaseBoMin(_OidDuongBao);
            if(count == countUpdate)
            {
                UtilsDatabase._ExtraInfoConnettion.Transaction.Commit();
                MessageBox.Show("Cập nhật BMVN thành công");
            }
            else
            {
                UtilsDatabase._ExtraInfoConnettion.Transaction.Rollback();
                MessageBox.Show("Cập nhật BMVN không thành công");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KhoangNghiNgoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MapMenuCommand.RemoveHighlight();
            MapMenuCommand.axMap1.Redraw();
            if (e.CloseReason == CloseReason.UserClosing && MyMainMenu2.Instance.tabCtrlLineChart.Visible == false)
            {
                MyMainMenu2.Instance.tabControlBottom.Visible = false;
            }
        }
    }
}