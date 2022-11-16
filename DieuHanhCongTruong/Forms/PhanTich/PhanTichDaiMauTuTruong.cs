using System;
using System.Windows.Forms;
using System.Drawing;
using DieuHanhCongTruong.Common;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using DieuHanhCongTruong.Command;
using System.Threading;
using DieuHanhCongTruong.Forms;

namespace VNRaPaBomMin
{
    public partial class PhanTichDaiMauTuTruong : Form
    {
        private ConnectionWithExtraInfo _connectionWithExtraInfo;
        private bool loaded = false;

        public PhanTichDaiMauTuTruong()
        {
            InitializeComponent();
            _connectionWithExtraInfo = UtilsDatabase._ExtraInfoConnettion;
        }

        private void PhanTichDaiMauTuTruong_Load(object sender, EventArgs e)
        {
            //DGVThongTin.Columns[0].HeaderText = "Min";
            //DGVThongTin.Columns[1].HeaderText = "Max";
            //DGVThongTin.Columns[2].HeaderText = "Màu";

            //for (int i = 0; i < DGVThongTin.ColumnCount; i++)
            //{
            //    DGVThongTin.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    DGVThongTin.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //DGVThongTin.AllowUserToAddRows = false;
            //DGVThongTin.BackgroundColor = System.Drawing.Color.White;
            //DGVThongTin.RowHeadersVisible = false;
            //DGVThongTin.AllowUserToResizeRows = false;

            //Autodesk.Civil.DatabaseServices.SurfaceAnalysisElevationData[] data = cmd.ListAllSurfaceAnalysis();

            //if (data == null)
            //{
            //    this.Close();
            //    return;
            //}

            List<DataRow> lst = UtilsDatabase.GetAllDataInTable(_connectionWithExtraInfo, "DaiMauTuTruong");

            if(lst.Count > 0)
            {
                numDaiMau.Value = lst.Count;
                foreach (DataRow dr in lst)
                {
                    DGVThongTin.Rows.Add(dr["min"].ToString(), dr["max"].ToString());

                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTin.Rows[DGVThongTin.Rows.Count - 1].Cells[cotColor.Index];
                    int r = int.Parse(dr["red"].ToString());
                    int g = int.Parse(dr["green"].ToString());
                    int b = int.Parse(dr["blue"].ToString());
                    Color color = Color.FromArgb(r, g, b);
                    buttonCell.Style.BackColor = color;
                    buttonCell.Style.SelectionBackColor = color;
                }
            }
            else
            {
                SetDefault();
            }
            loaded = true;

            //numDaiMau.Value = data.Length;
            //Microsoft.VisualBasic.Interaction.SaveSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau.Name, numDaiMau.Value.ToString());
        }

        private int selectedRow = -1;

        private void DGVThongTin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //selectedRow = e.RowIndex;
            if (e.ColumnIndex != cotColor.Index || e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTin.Rows[e.RowIndex].Cells[cotColor.Index];
            //this.Hide();
            colorDialog1.Color = buttonCell.Style.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonCell.Style.BackColor = colorDialog1.Color;

                DGVThongTin.ClearSelection();
            }
            //Autodesk.AutoCAD.Colors.Color mColor = cmd.getColor();
            //this.Show();

            

            //if (mColor != null)
            //    buttonCell.Style.BackColor = mColor.ColorValue;
            
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            //using (Autodesk.AutoCAD.DatabaseServices.Transaction ts =
            //    Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
            //{
            //    _SAnalysisData = new SurfaceAnalysisElevationData[DGVThongTin.Rows.Count];

            //    int i = 0;
            //    foreach (DataGridViewRow row in DGVThongTin.Rows)
            //    {
            //        if (row.Cells[0].Value == null || row.Cells[1].Value == null ||
            //            !AppUtils.IsNumber(row.Cells[0].Value.ToString()) || !AppUtils.IsNumber(row.Cells[1].Value.ToString()))
            //        {
            //            MessageBox.Show("Dữ liệu nhập không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return;
            //        }
            //        _SAnalysisData[i] = new SurfaceAnalysisElevationData();
            //        _SAnalysisData[i].MinimumElevation = double.Parse(row.Cells[0].Value.ToString());
            //        _SAnalysisData[i].MaximumElevation = double.Parse(row.Cells[1].Value.ToString());

            //        DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)row.Cells[2];
            //        buttonCell.FlatStyle = FlatStyle.Popup;

            //        _SAnalysisData[i].Scheme = (Autodesk.AutoCAD.Colors.Color.FromColor(buttonCell.Style.BackColor));
            //        i++;
            //    }

            //    ts.Abort();
            //}
            //Microsoft.VisualBasic.Interaction.SaveSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau.Name, numDaiMau.Value.ToString());

            //ObjectItem objItem = cbVungDuAn.SelectedItem as ObjectItem;
            //if (objItem == null)
            //    return;

            //oidSurface = objItem.ObjectId;

            //this.DialogResult = DialogResult.OK;
            SqlTransaction tran = _connectionWithExtraInfo.BeginTransaction() as SqlTransaction;
            try
            {
                string sqlDelete = "DELETE FROM DaiMauTuTruong";
                SqlCommand cmdDelete = new SqlCommand(sqlDelete, _connectionWithExtraInfo.Connection as SqlConnection, tran);
                cmdDelete.ExecuteNonQuery();

                int temp = 0;
                foreach (DataGridViewRow row in DGVThongTin.Rows)
                {
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null ||
                        !AppUtils.IsNumber(row.Cells[0].Value.ToString()) || !AppUtils.IsNumber(row.Cells[1].Value.ToString()))
                    {
                        MessageBox.Show("Dữ liệu nhập không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    double min = double.Parse(row.Cells[cotMin.Index].Value.ToString());
                    double max = double.Parse(row.Cells[cotMax.Index].Value.ToString());
                    Color color = row.Cells[cotColor.Index].Style.BackColor;
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    string sqlInsert = "INSERT INTO DaiMauTuTruong(min, max, red, green, blue) " +
                        "VALUES(@min, @max, @red, @green, @blue)";
                    SqlCommand cmdInsert = new SqlCommand(sqlInsert, _connectionWithExtraInfo.Connection as SqlConnection, tran);
                    cmdInsert.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@min",
                        SqlDbType = SqlDbType.Float,
                        Value = min
                    });
                    cmdInsert.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@max",
                        SqlDbType = SqlDbType.Float,
                        Value = max
                    });
                    cmdInsert.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@red",
                        SqlDbType = SqlDbType.Int,
                        Value = r
                    });
                    cmdInsert.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@green",
                        SqlDbType = SqlDbType.Int,
                        Value = g
                    });
                    cmdInsert.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@blue",
                        SqlDbType = SqlDbType.Int,
                        Value = b
                    });
                    temp += cmdInsert.ExecuteNonQuery();
                }
                if (temp == DGVThongTin.Rows.Count)
                {
                    tran.Commit();
                    MessageBox.Show("Cập nhật dải màu thành công");
                    DialogResult = DialogResult.OK;
                    MapMenuCommand.initPolygonLayer();
                    MyMainMenu2.Instance.TogglePhanTichMenu(false);
                    Thread thread = new Thread(() =>
                    {
                        foreach (var triangle in TINCommand.triangulations)
                        {
                            for (int i = 0; i < DGVThongTin.Rows.Count; i++)
                            {
                                DataGridViewRow row = DGVThongTin.Rows[i];
                                double min = double.Parse(row.Cells[cotMin.Index].Value.ToString());
                                double max = double.Parse(row.Cells[cotMax.Index].Value.ToString());
                                TINCommand.BuildOneColorSurface(triangle, min, max, i);
                            }
                            MapMenuCommand.Redraw();
                        }
                        MagneticCommand.threadMagneticStopped = true;
                        if (MyMainMenu2.Instance.InvokeRequired)
                        {
                            MyMainMenu2.Instance.Invoke(new MethodInvoker(() => {

                                MyMainMenu2.Instance.ToggleMagneticMenu(true);
                            }));
                        }
                        //else
                        //{
                        //    MyMainMenu2.Instance.ToggleMagneticMenu(true);
                        //}
                        if (MagneticCommand.threadMagneticStopped && MagneticCommand.threadPointStopped)
                        {
                            if (MyMainMenu2.Instance.InvokeRequired)
                            {
                                MyMainMenu2.Instance.Invoke(new MethodInvoker(() => {

                                    MyMainMenu2.Instance.TogglePhanTichMenu(true);
                                }));
                            }
                            //else
                            //{
                            //    MyMainMenu2.Instance.TogglePhanTichMenu(true);
                            //}
                        }
                    });
                    thread.IsBackground = true;
                    MagneticCommand.threadMagneticStopped = false;
                    thread.Start();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cập nhật dải màu không thành công");
                DialogResult = DialogResult.Cancel;
            }
            
        }

        private void numDaiMau_ValueChanged(object sender, EventArgs e)
        {
            if (!loaded)
            {
                return;
            }
            if (numDaiMau.Value == DGVThongTin.Rows.Count || numDaiMau.Value < 3)
                return;
            double minElevation = double.Parse(DGVThongTin.Rows[0].Cells[0].Value.ToString());
            double maxElevation = double.Parse(DGVThongTin.Rows[DGVThongTin.Rows.Count - 1].Cells[1].Value.ToString());

            //string prevValueStr = Microsoft.VisualBasic.Interaction.GetSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau.Name, "3");

            //decimal prevValue = 3;
            //decimal.TryParse(prevValueStr, out prevValue);

            if (numDaiMau.Value < DGVThongTin.Rows.Count)
            {
                while(DGVThongTin.Rows.Count > numDaiMau.Value)
                {
                    DGVThongTin.Rows.RemoveAt(DGVThongTin.Rows.Count - 1);
                }
            }
            else if (numDaiMau.Value > DGVThongTin.Rows.Count)
            {
                while (DGVThongTin.Rows.Count < numDaiMau.Value)
                {
                    DGVThongTin.Rows.Add("0", "0", "");
                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTin.Rows[DGVThongTin.Rows.Count - 1].Cells[2];
                    buttonCell.Style.BackColor = Color.White;
                }
            }

            double increment = (maxElevation - minElevation) / (double)numDaiMau.Value;
            for (int i = 0; i < numDaiMau.Value; i++)
            {
                DGVThongTin.Rows[i].Cells[0].Value = minElevation + (increment * i);
                DGVThongTin.Rows[i].Cells[1].Value = minElevation + (increment * (i + 1));
            }

            //prevValue = numDaiMau.Value;
            //Microsoft.VisualBasic.Interaction.SaveSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau.Name, prevValue.ToString());
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void cbVungDuAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ObjectItem objItem = cbVungDuAn.SelectedItem as ObjectItem;
            //if (objItem == null)
            //    return;

            //Autodesk.Civil.DatabaseServices.SurfaceAnalysisElevationData[] data = cmd.ListAllSurfaceAnalysis(objItem.ObjectId);

            //if (data != null)
            //{
            //    DGVThongTin.Rows.Clear();
            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        DGVThongTin.Rows.Add(data[i].MinimumElevation, data[i].MaximumElevation, "");

            //        DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTin.Rows[i].Cells[2];
            //        buttonCell.FlatStyle = FlatStyle.Popup;
            //        buttonCell.Style.BackColor = data[i].Scheme.ColorValue;
            //    }

            //    numDaiMau.Value = data.Length;
            //    Microsoft.VisualBasic.Interaction.SaveSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //            this.Name, numDaiMau.Name, numDaiMau.Value.ToString());
            //}
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            SetDefault();
        }

        private void SetDefault()
        {
            loaded = false;
            DGVThongTin.Rows.Clear();
            numDaiMau.Value = Constants.magnetic_colors.Length;
            double minZ = Constants.MIN_Z_BOMB;
            double maxZ = Constants.MAX_Z_BOMB;
            double elevation = (maxZ - minZ) / Constants.magnetic_colors.Length;
            for (int i = 0; i < Constants.magnetic_colors.Length; i++)
            {
                double minElevation = minZ + i * elevation;
                double maxElevation = minZ + (i + 1) * elevation;
                DGVThongTin.Rows.Add(minElevation, maxElevation);

                DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTin.Rows[DGVThongTin.Rows.Count - 1].Cells[cotColor.Index];
                Color color = Constants.magnetic_colors[i];
                buttonCell.Style.BackColor = color;
                buttonCell.Style.SelectionBackColor = color;
            }
            loaded = true;
        }
    }
}