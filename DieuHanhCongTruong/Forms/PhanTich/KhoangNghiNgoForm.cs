using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private List<CecmReportPollutionAreaBMVN> lstBMVN;

        public KhoangNghiNgoForm(List<CecmReportPollutionAreaBMVN> lstBMVN)
        {
            //_oidCollMine = oidCollMine;
            //_OidDuongBao = oidDuongBao;
            this.lstBMVN = lstBMVN;
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
            foreach(CecmReportPollutionAreaBMVN bmvn in lstBMVN)
            {
                var latLong = AppUtils.ConverUTMToLatLong(bmvn.XPoint, bmvn.YPoint);
                bmvn.Vido = latLong.Y;
                bmvn.Kinhdo = latLong.X;
                DGVData.Rows.Add(stt, latLong.Y, latLong.X, bmvn.ZPoint, bmvn.Deep, bmvn.Area, true, false);
                DGVData.Rows[stt - 1].Tag = bmvn;
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

        private void FillChart(Dictionary<double, double> mDataVal)
        {
            // clear the chart
            charDisplay.Series.Clear();

            // fill the chart
            var series = charDisplay.Series.Add("My Series");
            series.ChartType = SeriesChartType.Line;
            series.XValueType = ChartValueType.Int32;

            foreach (var item in mDataVal)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
            // Visible legend
            series.IsVisibleInLegend = false;

            var chartArea = charDisplay.ChartAreas[series.ChartArea];

            //chartArea.AxisX.Minimum = mDataVal.Min(X => X.Key);
            //chartArea.AxisX.Maximum = mDataVal.Max(X => X.Key);

            //chartArea.AxisY.Minimum = mDataVal.Min(X => X.Value);
            //chartArea.AxisY.Maximum = mDataVal.Max(X => X.Value);

            double minValX = mDataVal.Min(X => X.Key);
            double maxValX = mDataVal.Max(X => X.Key);
            double minValY = mDataVal.Min(X => X.Value);
            double maxValY = mDataVal.Max(X => X.Value);

            // Interval axisX and axisY
            //chartArea.AxisX.Interval = Math.Round(Math.Abs((minValX + maxValX) / 5), 0);

            chartArea.AxisX.Interval = 0.25;
            chartArea.AxisY.Interval = Math.Round(Math.Abs((minValY + maxValY) / 10), 0);
        }

        private void DGVData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            var senderGrid = (DataGridView)sender;
            var column = senderGrid.Columns[e.ColumnIndex];
            if (column.Name == "Zoom")
            {
                if (e.RowIndex >= 0)
                {
                    //using (AutoCADApp.DocumentManager.MdiActiveDocument.LockDocument())
                    //{
                    //    using (Transaction tr = AutoCADApp.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
                    //    {
                    //        ObjectId oidMine = (ObjectId)DGVData.Rows[e.RowIndex].Tag;
                    //        if (oidMine.IsValid == false)
                    //            return;

                    //        MgdAcDbVNTerrainMinePoint mMine = oidMine.GetObject(OpenMode.ForRead) as MgdAcDbVNTerrainMinePoint;
                    //        if (mMine == null)
                    //            return;

                    //        DBUtils.ZoomCenterPoint(mMine.Position);

                    //        tr.Commit();
                    //    }
                    //}
                    CecmReportPollutionAreaBMVN bmvn = senderGrid.Rows[e.RowIndex].Tag as CecmReportPollutionAreaBMVN;
                    Extents extents = new Extents();
                    extents.SetBounds(bmvn.Kinhdo - 0.00001, bmvn.Vido - 0.00001, 0, bmvn.Kinhdo + 0.00001, bmvn.Vido + 0.00001, 0);
                    MyMainMenu2.Instance.axMap1.Extents = extents;
                }
            }
            else
            {
                //using (AutoCADApp.DocumentManager.MdiActiveDocument.LockDocument())
                //{
                //    using (Transaction tr = AutoCADApp.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
                //    {
                //        ObjectId oidMine = (ObjectId)DGVData.Rows[e.RowIndex].Tag;
                //        if (oidMine == null)
                //        {
                //            tr.Abort();
                //            return;
                //        }

                //        var oidDuAn = DBUtils.GetObjectIdDuAn();

                //        MgdAcDbVNTerrainMinePoint m_Mine = oidMine.GetObject(OpenMode.ForRead) as MgdAcDbVNTerrainMinePoint;
                //        if (m_Mine == null)
                //        {
                //            tr.Abort();
                //            return;
                //        }

                //        List<MgdAcDbVNTerrainMinePoint> lMine = new List<MgdAcDbVNTerrainMinePoint> { m_Mine };

                //        var mLine = KhoangGiamNghiNgoCmd.FindDeepMine(lMine, true);

                //        var mElevation = _cls.GetTuTruong(oidDuAn, mLine.FirstOrDefault(), lMine.FirstOrDefault(), _OidDuongBao);

                //        if (mElevation.Count == 0)
                //        {
                //            charDisplay.Series.Clear();
                //            return;
                //        }
                //        FillChart(mElevation);

                //        tr.Commit();
                //    }
                //}
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
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}