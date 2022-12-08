using DieuHanhCongTruong.Command;
using DieuHanhCongTruong.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class GroupLayerForm : Form
    {
        private const string POINT_LAYER_NAME = "Điểm đã thu thập";
        private const string SURFACE_LAYER_NAME = "Bề mặt từ trường";
        private const string FLAG_LAYER_NAME = "Cờ";
        private const string SUSPECT_POINT_LAYER_NAME = "Điểm nghi ngờ";

        public GroupLayerForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ApplyChange();
            this.Close();
        }

        private void GroupLayerForm_Load(object sender, EventArgs e)
        {
            //List<string> listValueOut = new List<string>();
            //listValueOut = ClassGroupLayer.CreateDictionary(ClassGroupLayer.CreateListData());
            //LoadDGV(listValueOut);
            bool hasCheckedItem = true;
            if (MapMenuCommand.machinePointBombLayers.Count > 0 &&
                MapMenuCommand.machinePointMineLayers.Count > 0 &&
                MapMenuCommand.machinePointBombModelLayers.Count > 0 &&
                MapMenuCommand.machinePointMineModelLayers.Count > 0)
            {
                bool visible;
                List<int> layers = new List<int>();
                bool showBomb = MyMainMenu2.Instance.rbBomb.Checked;
                bool showModel = MyMainMenu2.Instance.rbModel.Checked;
                if (showBomb && !showModel)
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.machinePointBombLayers[0]);
                    layers.AddRange(MapMenuCommand.machinePointBombLayers);
                    layers.AddRange(MapMenuCommand.machineLineBombLayers);
                }
                else if(!showBomb && !showModel)
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.machinePointMineLayers[0]);
                    layers.AddRange(MapMenuCommand.machinePointMineLayers);
                    layers.AddRange(MapMenuCommand.machineLineMineLayers);
                }
                else if(showBomb && showModel)
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.machinePointBombModelLayers[0]);
                    layers.AddRange(MapMenuCommand.machinePointBombModelLayers);
                    layers.AddRange(MapMenuCommand.machineLineBombModelLayers);
                }
                else
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.machinePointMineModelLayers[0]);
                    layers.AddRange(MapMenuCommand.machinePointMineModelLayers);
                    layers.AddRange(MapMenuCommand.machineLineMineModelLayers);
                }
                int index = DGVData.Rows.Add(POINT_LAYER_NAME, visible);
                
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            //if (MapMenuCommand.machinePointMineLayers.Count > 0)
            //{
            //    bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.machinePointMineLayers[0]);
            //    int index = DGVData.Rows.Add("Quỹ đạo đo (mìn chưa nắn)", visible);
            //    List<int> layers = new List<int>();
            //    layers.AddRange(MapMenuCommand.machinePointMineLayers);
            //    layers.AddRange(MapMenuCommand.machineLineMineLayers);
            //    DGVData.Rows[index].Tag = layers;
            //    if (!visible)
            //    {
            //        hasCheckedItem = false;
            //    }
            //}
            //if (MapMenuCommand.machinePointBombModelLayers.Count > 0)
            //{
            //    bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.machinePointBombModelLayers[0]);
            //    int index = DGVData.Rows.Add("Quỹ đạo đo (bom đã nắn)", visible);
            //    List<int> layers = new List<int>();
            //    layers.AddRange(MapMenuCommand.machinePointBombModelLayers);
            //    layers.AddRange(MapMenuCommand.machineLineBombModelLayers);
            //    DGVData.Rows[index].Tag = layers;
            //    if (!visible)
            //    {
            //        hasCheckedItem = false;
            //    }
            //}
            //if (MapMenuCommand.machinePointMineModelLayers.Count > 0)
            //{
            //    bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.machinePointMineModelLayers[0]);
            //    int index = DGVData.Rows.Add("Quỹ đạo đo (mìn đã nắn)", visible);
            //    List<int> layers = new List<int>();
            //    layers.AddRange(MapMenuCommand.machinePointMineModelLayers);
            //    layers.AddRange(MapMenuCommand.machineLineMineModelLayers);
            //    DGVData.Rows[index].Tag = layers;
            //    if (!visible)
            //    {
            //        hasCheckedItem = false;
            //    }
            //}
            if (MapMenuCommand.polygonLayers.Count > 0 &&
                MapMenuCommand.polygonLayersMine.Count > 0)
            {
                bool showBomb = MyMainMenu2.Instance.rbBomb.Checked;
                bool visible;
                List<int> layer;
                if (showBomb)
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayers[0]);
                    layer = MapMenuCommand.polygonLayers;
                }
                else
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayersMine[0]);
                    layer = MapMenuCommand.polygonLayersMine;
                }
                int index = DGVData.Rows.Add(SURFACE_LAYER_NAME, visible);
                DGVData.Rows[index].Tag = layer;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            //if (MapMenuCommand.polygonLayersMine.Count > 0)
            //{
            //    bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayersMine[0]);
            //    int index = DGVData.Rows.Add("Bề mặt địa hình (mìn)", visible);
            //    DGVData.Rows[index].Tag = MapMenuCommand.polygonLayersMine;
            //    if (!visible)
            //    {
            //        hasCheckedItem = false;
            //    }
            //}
            if (MapMenuCommand.flagBombLayer > 0 &&
                MapMenuCommand.flagMineLayer > 0)
            {
                bool showBomb = MyMainMenu2.Instance.rbBomb.Checked;
                List<int> layers = new List<int>();
                bool visible;
                if (showBomb)
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.flagBombLayer);
                    layers.Add(MapMenuCommand.flagBombLayer);
                }
                else
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.flagMineLayer);
                    layers.Add(MapMenuCommand.flagMineLayer);
                }
                int index = DGVData.Rows.Add(FLAG_LAYER_NAME, visible);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            //if (MapMenuCommand.flagMineLayer > 0)
            //{
            //    bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.flagMineLayer);
            //    int index = DGVData.Rows.Add("Cờ (mìn)", visible);
            //    List<int> layers = new List<int>();
            //    layers.Add(MapMenuCommand.flagMineLayer);
            //    DGVData.Rows[index].Tag = layers;
            //    if (!visible)
            //    {
            //        hasCheckedItem = false;
            //    }
            //}

            if (MapMenuCommand.imageLayers.Count > 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.imageLayers[0]);
                int index = DGVData.Rows.Add("Ảnh vệ tinh", visible);
                DGVData.Rows[index].Tag = MapMenuCommand.imageLayers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            if (MapMenuCommand.polygonAreaLayer >= 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonAreaLayer);
                int index = DGVData.Rows.Add("Đường bao dự án", visible);
                List<int> layers = new List<int>();
                layers.Add(MapMenuCommand.polygonAreaLayer);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            if (MapMenuCommand.oluoiLayer >= 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.oluoiLayer);
                int index = DGVData.Rows.Add("Ô lưới", visible);
                List<int> layers = new List<int>();
                layers.Add(MapMenuCommand.oluoiLayer);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            if (MapMenuCommand.ranhDoLayer >= 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.ranhDoLayer);
                int index = DGVData.Rows.Add("Rãnh dò", visible);
                List<int> layers = new List<int>();
                layers.Add(MapMenuCommand.ranhDoLayer);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            if (MapMenuCommand.suspectPointLayerBomb >= 0 &&
                MapMenuCommand.suspectPointLayerMine >= 0 &&
                MapMenuCommand.userSuspectPointLayerBomb >= 0 &&
                MapMenuCommand.userSuspectPointLayerMine >= 0)
            {
                bool showBomb = MyMainMenu2.Instance.rbBomb.Checked;
                bool visible;
                List<int> layers = new List<int>();
                if (showBomb)
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.suspectPointLayerBomb);
                    layers.Add(MapMenuCommand.suspectPointLayerBomb);
                    layers.Add(MapMenuCommand.userSuspectPointLayerBomb);
                }
                else
                {
                    visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.suspectPointLayerMine);
                    layers.Add(MapMenuCommand.suspectPointLayerMine);
                    layers.Add(MapMenuCommand.userSuspectPointLayerMine);
                }
                int index = DGVData.Rows.Add(SUSPECT_POINT_LAYER_NAME, visible);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            //if (MapMenuCommand.suspectPointLayerMine >= 0)
            //{
            //    bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.suspectPointLayerMine);
            //    int index = DGVData.Rows.Add("Điểm nghi ngờ (mìn)", visible);
            //    List<int> layers = new List<int>();
            //    layers.Add(MapMenuCommand.suspectPointLayerMine);
            //    layers.Add(MapMenuCommand.userSuspectPointLayerMine);
            //    DGVData.Rows[index].Tag = layers;
            //    if (!visible)
            //    {
            //        hasCheckedItem = false;
            //    }
            //}
            int lastIndex = DGVData.Rows.Add("Tất cả", hasCheckedItem);
            DGVData.Rows[lastIndex].Tag = new List<int>();
        }

        private void btApDung_Click(object sender, EventArgs e)
        {
            ApplyChange();
        }

        private void ApplyChange()
        {
            foreach (DataGridViewRow row in DGVData.Rows)
            {
                string layerName = row.Cells[cotLayer.Index].Value.ToString();
                if(layerName == POINT_LAYER_NAME)
                {
                    MapMenuCommand.pointVisible = (bool)row.Cells[cotVisible.Index].Value;
                }
                else if(layerName == SURFACE_LAYER_NAME)
                {
                    MapMenuCommand.surfaceVisible = (bool)row.Cells[cotVisible.Index].Value;
                }
                else if(layerName == FLAG_LAYER_NAME)
                {
                    MapMenuCommand.flagVisible = (bool)row.Cells[cotVisible.Index].Value;
                }
                else if(layerName == SUSPECT_POINT_LAYER_NAME)
                {
                    MapMenuCommand.suspectPointVisible = (bool)row.Cells[cotVisible.Index].Value;
                }
                List<int> layers = row.Tag as List<int>;
                foreach (int layer in layers)
                {
                    MapMenuCommand.axMap1.set_LayerVisible(layer, (bool)row.Cells[cotVisible.Index].Value);
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ClassGroupLayer.LoadFormSetting();
            //List<string> listValueOut = new List<string>();
            //listValueOut = ClassGroupLayer.CreateDictionary(ClassGroupLayer.CreateListData());
            //LoadDGV(listValueOut);
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.UpdateScreen();
        }

        private void DGVData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex != 1)
                return;

            if (DGVData.Rows[e.RowIndex].Cells[cotLayer.Index].Value.ToString() == "Tất cả" && (bool)DGVData.Rows[e.RowIndex].Cells[cotVisible.Index].Value == true)
            {
                foreach (DataGridViewRow item in DGVData.Rows)
                {
                    if (item != null)
                    {
                        item.Cells[1].Value = false;
                    }
                }
            }
            else if (DGVData.Rows[e.RowIndex].Cells[cotLayer.Index].Value.ToString() == "Tất cả" && (bool)DGVData.Rows[e.RowIndex].Cells[cotVisible.Index].Value == false)
            {
                foreach (DataGridViewRow item in DGVData.Rows)
                {
                    if (item != null)
                    {
                        item.Cells[cotVisible.Index].Value = true;
                    }
                }
            }

            DGVData.Update();
        }
    }
}