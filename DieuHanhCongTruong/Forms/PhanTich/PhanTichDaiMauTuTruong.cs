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

        private bool threadMagneticBombStopped = true;
        private bool threadMagneticMineStopped = true;

        public PhanTichDaiMauTuTruong()
        {
            InitializeComponent();
            _connectionWithExtraInfo = UtilsDatabase._ExtraInfoConnettion;
        }

        private void PhanTichDaiMauTuTruong_Load(object sender, EventArgs e)
        {
            //DGVThongTinBomb.Columns[0].HeaderText = "Min";
            //DGVThongTinBomb.Columns[1].HeaderText = "Max";
            //DGVThongTinBomb.Columns[2].HeaderText = "Màu";

            //for (int i = 0; i < DGVThongTinBomb.ColumnCount; i++)
            //{
            //    DGVThongTinBomb.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    DGVThongTinBomb.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //DGVThongTinBomb.AllowUserToAddRows = false;
            //DGVThongTinBomb.BackgroundColor = System.Drawing.Color.White;
            //DGVThongTinBomb.RowHeadersVisible = false;
            //DGVThongTinBomb.AllowUserToResizeRows = false;

            //Autodesk.Civil.DatabaseServices.SurfaceAnalysisElevationData[] data = cmd.ListAllSurfaceAnalysis();

            //if (data == null)
            //{
            //    this.Close();
            //    return;
            //}

            List<DataRow> lstColorBomb = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "DaiMauTuTruong", "IsBomb", "1");

            if (lstColorBomb.Count > 0)
            {
                numDaiMau_Bomb.Value = lstColorBomb.Count;
                foreach (DataRow dr in lstColorBomb)
                {
                    DataGridViewButtonCell buttonCell;
                    DGVThongTinBomb.Rows.Add(dr["min"].ToString(), dr["max"].ToString());

                    buttonCell = (DataGridViewButtonCell)DGVThongTinBomb.Rows[DGVThongTinBomb.Rows.Count - 1].Cells[cotColor_Bomb.Index];
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
                SetDefaultBomb();
            }

            List<DataRow> lstColorMine = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "DaiMauTuTruong", "IsBomb", "2");

            if (lstColorMine.Count > 0)
            {
                numDaiMau_Mine.Value = lstColorMine.Count;
                foreach (DataRow dr in lstColorMine)
                {
                    DataGridViewButtonCell buttonCell;
                    DGVThongTinMine.Rows.Add(dr["min"].ToString(), dr["max"].ToString());

                    buttonCell = (DataGridViewButtonCell)DGVThongTinMine.Rows[DGVThongTinMine.Rows.Count - 1].Cells[cotColor_Mine.Index];
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
                SetDefaultMine();
            }
            loaded = true;

            //numDaiMau_Bomb.Value = data.Length;
            //Microsoft.VisualBasic.Interaction.SaveSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau_Bomb.Name, numDaiMau_Bomb.Value.ToString());
        }

        private int selectedRow = -1;

        private void DGVThongTin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //selectedRow = e.RowIndex;
            if (e.ColumnIndex != cotColor_Bomb.Index || e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTinBomb.Rows[e.RowIndex].Cells[cotColor_Bomb.Index];
            //this.Hide();
            colorDialog1.Color = buttonCell.Style.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonCell.Style.BackColor = colorDialog1.Color;

                DGVThongTinBomb.ClearSelection();
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
            //    _SAnalysisData = new SurfaceAnalysisElevationData[DGVThongTinBomb.Rows.Count];

            //    int i = 0;
            //    foreach (DataGridViewRow row in DGVThongTinBomb.Rows)
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
            //        this.Name, numDaiMau_Bomb.Name, numDaiMau_Bomb.Value.ToString());

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
                bool bombVisible;
                bool mineVisible;
                if (MapMenuCommand.polygonLayers.Count > 0)
                {
                    bombVisible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayers[0]);
                }
                else
                {
                    bombVisible = true;
                }
                if (MapMenuCommand.polygonLayersMine.Count > 0)
                {
                    mineVisible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayersMine[0]);
                }
                else
                {
                    mineVisible = false;
                }

                int temp = 0;
                foreach (DataGridViewRow row in DGVThongTinBomb.Rows)
                {
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null ||
                        !AppUtils.IsNumber(row.Cells[0].Value.ToString()) || !AppUtils.IsNumber(row.Cells[1].Value.ToString()))
                    {
                        MessageBox.Show("Dữ liệu nhập không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    double min = double.Parse(row.Cells[cotMin_Bomb.Index].Value.ToString());
                    double max = double.Parse(row.Cells[cotMax_Bomb.Index].Value.ToString());
                    Color color = row.Cells[cotColor_Bomb.Index].Style.BackColor;
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    string sqlInsert = "INSERT INTO DaiMauTuTruong(min, max, red, green, blue, IsBomb) " +
                        "VALUES(@min, @max, @red, @green, @blue, @IsBomb)";
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
                    cmdInsert.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@IsBomb",
                        SqlDbType = SqlDbType.Int,
                        Value = 1
                    });
                    temp += cmdInsert.ExecuteNonQuery();
                }
                foreach (DataGridViewRow row in DGVThongTinMine.Rows)
                {
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null ||
                        !AppUtils.IsNumber(row.Cells[0].Value.ToString()) || !AppUtils.IsNumber(row.Cells[1].Value.ToString()))
                    {
                        MessageBox.Show("Dữ liệu nhập không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    double min = double.Parse(row.Cells[cotMin_Mine.Index].Value.ToString());
                    double max = double.Parse(row.Cells[cotMax_Mine.Index].Value.ToString());
                    Color color = row.Cells[cotColor_Mine.Index].Style.BackColor;
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    string sqlInsert = "INSERT INTO DaiMauTuTruong(min, max, red, green, blue, IsBomb) " +
                        "VALUES(@min, @max, @red, @green, @blue, @IsBomb)";
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
                    cmdInsert.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@IsBomb",
                        SqlDbType = SqlDbType.Int,
                        Value = 2
                    });
                    temp += cmdInsert.ExecuteNonQuery();
                }
                if (temp == DGVThongTinBomb.Rows.Count + DGVThongTinMine.Rows.Count)
                {
                    tran.Commit();
                    MessageBox.Show("Cập nhật dải màu thành công");
                    DialogResult = DialogResult.OK;
                    MapMenuCommand.initPolygonLayer(bombVisible, mineVisible);
                    //MapMenuCommand.ClearPolygon();
                    MyMainMenu2.Instance.TogglePhanTichMenu(false);
                    MyMainMenu2.Instance.tsProgressSurface.Visible = true;
                    Thread threadMagneticBomb = new Thread(() =>
                    {
                        threadMagneticBombStopped = false;
                        foreach (var triangle in TINCommand.triangulations_bomb.Values)
                        {
                            for (int i = 0; i < DGVThongTinBomb.Rows.Count; i++)
                            {
                                DataGridViewRow row = DGVThongTinBomb.Rows[i];
                                double min = double.Parse(row.Cells[cotMin_Bomb.Index].Value.ToString());
                                double max = double.Parse(row.Cells[cotMax_Bomb.Index].Value.ToString());
                                //TINCommand.BuildOneColorSurface(triangle, min, max, i, true);
                                if (i == 0)
                                {
                                    TINCommand.BuildOneColorSurface(triangle, double.MinValue, max, i, true);
                                }
                                else if (i == DGVThongTinBomb.Rows.Count - 1)
                                {
                                    TINCommand.BuildOneColorSurface(triangle, min, double.MaxValue, i, true);
                                }
                                else
                                {
                                    TINCommand.BuildOneColorSurface(triangle, min, max, i, true);
                                }
                            }
                            MapMenuCommand.Redraw();
                        }
                        threadMagneticBombStopped = true;
                        if (threadMagneticMineStopped)
                        {
                            if (MyMainMenu2.Instance.InvokeRequired)
                            {
                                MyMainMenu2.Instance.Invoke(new MethodInvoker(() => {

                                    MyMainMenu2.Instance.TogglePhanTichMenu(true);
                                    MyMainMenu2.Instance.tsProgressSurface.Visible = false;
                                }));
                            }
                        }
                    });
                    Thread threadMagneticMine = new Thread(() =>
                    {
                        threadMagneticMineStopped = false;
                        foreach (var triangle in TINCommand.triangulations_mine.Values)
                        {
                            for (int i = 0; i < DGVThongTinMine.Rows.Count; i++)
                            {
                                DataGridViewRow row = DGVThongTinMine.Rows[i];
                                double min = double.Parse(row.Cells[cotMin_Mine.Index].Value.ToString());
                                double max = double.Parse(row.Cells[cotMax_Mine.Index].Value.ToString());
                                //TINCommand.BuildOneColorSurface(triangle, min, max, i, false);
                                if (i == 0)
                                {
                                    TINCommand.BuildOneColorSurface(triangle, double.MinValue, max, i, false);
                                }
                                else if (i == DGVThongTinMine.Rows.Count - 1)
                                {
                                    TINCommand.BuildOneColorSurface(triangle, min, double.MaxValue, i, false);
                                }
                                else
                                {
                                    TINCommand.BuildOneColorSurface(triangle, min, max, i, false);
                                }
                            }
                            MapMenuCommand.Redraw();
                        }
                        threadMagneticMineStopped = true;
                        if (threadMagneticBombStopped)
                        {
                            if (MyMainMenu2.Instance.InvokeRequired)
                            {
                                MyMainMenu2.Instance.Invoke(new MethodInvoker(() => {

                                    MyMainMenu2.Instance.TogglePhanTichMenu(true);
                                    MyMainMenu2.Instance.tsProgressSurface.Visible = false;
                                }));
                            }
                        }
                    });

                    threadMagneticBomb.IsBackground = true;
                    threadMagneticMine.IsBackground = true;

                    threadMagneticBomb.Start();
                    threadMagneticMine.Start();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cập nhật dải màu không thành công");
                DialogResult = DialogResult.Cancel;
            }
            
        }

        private void numDaiMau_Bomb_ValueChanged(object sender, EventArgs e)
        {
            if (!loaded)
            {
                return;
            }
            if (numDaiMau_Bomb.Value == DGVThongTinBomb.Rows.Count || numDaiMau_Bomb.Value < 3)
                return;
            double minElevation = double.Parse(DGVThongTinBomb.Rows[0].Cells[0].Value.ToString());
            double maxElevation = double.Parse(DGVThongTinBomb.Rows[DGVThongTinBomb.Rows.Count - 1].Cells[1].Value.ToString());

            //string prevValueStr = Microsoft.VisualBasic.Interaction.GetSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau_Bomb.Name, "3");

            //decimal prevValue = 3;
            //decimal.TryParse(prevValueStr, out prevValue);

            if (numDaiMau_Bomb.Value < DGVThongTinBomb.Rows.Count)
            {
                while(DGVThongTinBomb.Rows.Count > numDaiMau_Bomb.Value)
                {
                    DGVThongTinBomb.Rows.RemoveAt(DGVThongTinBomb.Rows.Count - 1);
                }
            }
            else if (numDaiMau_Bomb.Value > DGVThongTinBomb.Rows.Count)
            {
                while (DGVThongTinBomb.Rows.Count < numDaiMau_Bomb.Value)
                {
                    DGVThongTinBomb.Rows.Add("0", "0", "");
                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTinBomb.Rows[DGVThongTinBomb.Rows.Count - 1].Cells[2];
                    buttonCell.Style.BackColor = Color.White;
                }
            }

            double increment = (maxElevation - minElevation) / (double)numDaiMau_Bomb.Value;
            for (int i = 0; i < numDaiMau_Bomb.Value; i++)
            {
                DGVThongTinBomb.Rows[i].Cells[0].Value = minElevation + (increment * i);
                DGVThongTinBomb.Rows[i].Cells[1].Value = minElevation + (increment * (i + 1));
            }

            //prevValue = numDaiMau_Bomb.Value;
            //Microsoft.VisualBasic.Interaction.SaveSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau_Bomb.Name, prevValue.ToString());
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
            //    DGVThongTinBomb.Rows.Clear();
            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        DGVThongTinBomb.Rows.Add(data[i].MinimumElevation, data[i].MaximumElevation, "");

            //        DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTinBomb.Rows[i].Cells[2];
            //        buttonCell.FlatStyle = FlatStyle.Popup;
            //        buttonCell.Style.BackColor = data[i].Scheme.ColorValue;
            //    }

            //    numDaiMau_Bomb.Value = data.Length;
            //    Microsoft.VisualBasic.Interaction.SaveSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //            this.Name, numDaiMau_Bomb.Name, numDaiMau_Bomb.Value.ToString());
            //}
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            SetDefaultBomb();
        }

        private void SetDefaultBomb()
        {
            loaded = false;
            DGVThongTinBomb.Rows.Clear();
            numDaiMau_Bomb.Value = Constants.magnetic_colors.Length;
            double minZ = Constants.MIN_Z_BOMB;
            double maxZ = Constants.MAX_Z_BOMB;
            double elevation = (maxZ - minZ) / Constants.magnetic_colors.Length;
            for (int i = 0; i < Constants.magnetic_colors.Length; i++)
            {
                double minElevation = minZ + i * elevation;
                double maxElevation = minZ + (i + 1) * elevation;
                DGVThongTinBomb.Rows.Add(minElevation, maxElevation);

                DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTinBomb.Rows[DGVThongTinBomb.Rows.Count - 1].Cells[cotColor_Bomb.Index];
                Color color = Constants.magnetic_colors[i];
                buttonCell.Style.BackColor = color;
                buttonCell.Style.SelectionBackColor = color;
            }
            loaded = true;
        }

        private void SetDefaultMine()
        {
            loaded = false;
            DGVThongTinMine.Rows.Clear();
            numDaiMau_Mine.Value = Constants.magnetic_colors.Length;
            double minZ = Constants.MIN_Z_MINE;
            double maxZ = Constants.MAX_Z_MINE;
            double elevation = (maxZ - minZ) / Constants.magnetic_colors.Length;
            for (int i = 0; i < Constants.magnetic_colors.Length; i++)
            {
                double minElevation = minZ + i * elevation;
                double maxElevation = minZ + (i + 1) * elevation;
                DGVThongTinMine.Rows.Add(minElevation, maxElevation);

                DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTinMine.Rows[DGVThongTinMine.Rows.Count - 1].Cells[cotColor_Mine.Index];
                Color color = Constants.magnetic_colors[i];
                buttonCell.Style.BackColor = color;
                buttonCell.Style.SelectionBackColor = color;
            }
            loaded = true;
        }

        private void numDaiMau_Mine_ValueChanged(object sender, EventArgs e)
        {
            if (!loaded)
            {
                return;
            }
            if (numDaiMau_Mine.Value == DGVThongTinMine.Rows.Count || numDaiMau_Mine.Value < 3)
                return;
            double minElevation = double.Parse(DGVThongTinMine.Rows[0].Cells[0].Value.ToString());
            double maxElevation = double.Parse(DGVThongTinMine.Rows[DGVThongTinMine.Rows.Count - 1].Cells[1].Value.ToString());

            //string prevValueStr = Microsoft.VisualBasic.Interaction.GetSetting(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
            //        this.Name, numDaiMau_Mine.Name, "3");

            //decimal prevValue = 3;
            //decimal.TryParse(prevValueStr, out prevValue);

            if (numDaiMau_Mine.Value < DGVThongTinMine.Rows.Count)
            {
                while (DGVThongTinMine.Rows.Count > numDaiMau_Mine.Value)
                {
                    DGVThongTinMine.Rows.RemoveAt(DGVThongTinMine.Rows.Count - 1);
                }
            }
            else if (numDaiMau_Mine.Value > DGVThongTinMine.Rows.Count)
            {
                while (DGVThongTinMine.Rows.Count < numDaiMau_Mine.Value)
                {
                    DGVThongTinMine.Rows.Add("0", "0", "");
                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTinMine.Rows[DGVThongTinMine.Rows.Count - 1].Cells[2];
                    buttonCell.Style.BackColor = Color.White;
                }
            }

            double increment = (maxElevation - minElevation) / (double)numDaiMau_Mine.Value;
            for (int i = 0; i < numDaiMau_Mine.Value; i++)
            {
                DGVThongTinMine.Rows[i].Cells[0].Value = minElevation + (increment * i);
                DGVThongTinMine.Rows[i].Cells[1].Value = minElevation + (increment * (i + 1));
            }
        }

        private void btnDefault_Mine_Click(object sender, EventArgs e)
        {
            SetDefaultMine();
        }

        private void DGVThongTinMine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != cotColor_Mine.Index || e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTinMine.Rows[e.RowIndex].Cells[cotColor_Mine.Index];
            //this.Hide();
            colorDialog1.Color = buttonCell.Style.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonCell.Style.BackColor = colorDialog1.Color;

                DGVThongTinMine.ClearSelection();
            }
        }
    }
}