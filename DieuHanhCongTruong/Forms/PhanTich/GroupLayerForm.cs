using DieuHanhCongTruong.Command;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public partial class GroupLayerForm : Form
    {
        public GroupLayerForm()
        {
            InitializeComponent();
        }

        public void LoadDGV(List<string> listValueOut)
        {
            //try
            //{
            //    bool hasCheckedItem = true;
            //    DGVData.Rows.Clear();
            //    int index = 0;
            //    if (listValueOut != null)
            //    {
            //        string[] arrData = new string[listValueOut.Count];
            //        arrData = listValueOut.ToArray();
            //        for (int i = 0; i < listValueOut.Count; i++)
            //        {
            //            string[] ss = new string[3];
            //            ss = arrData[i].Split(';');
            //            if (ss[0] != null && ss[1] != null && ss[2] != null && ss[3] != null)
            //                if (ss[1] != "" || ss[2] != "" || ss[3] != "")
            //                {
            //                    DGVData.Rows.Add(ss[1], "");
            //                    if (ss[2] != null && ss[2].Length > 0)
            //                    {
            //                        string[] ss2 = ss[2].Split(',');
            //                        for (int k = 0; k < ss2.Length; k++)
            //                        {
            //                            if (ClassGroupLayer.findLayerOnOff(ss2[k]))
            //                            {
            //                                DGVData.Rows[index].Cells["Visible"].Value = true;
            //                                index++;
            //                                break;
            //                            }
            //                            else
            //                            {
            //                                DGVData.Rows[index].Cells["Visible"].Value = false;
            //                                hasCheckedItem = false;
            //                                index++;
            //                                break;
            //                            }
            //                        }
            //                    }
            //                }
            //        }
            //    }

            //    DGVData.Rows.Add("Các lớp", "");
            //    DGVData.Rows[index].Cells["Visible"].Value = hasCheckedItem;
            //}
            //catch (Exception ex)
            //{
            //    MyLogger.Log(string.Format("Có lỗi xảy ra trong quá trình hiển thị lớp {0}", ex.Message));
            //}
        }

        public void findLayerNameOnOff(List<string> listData, ref List<string> layerOn, ref List<string> layerOff)
        {
            if (listData != null)
            {
                string[] arrData = new string[listData.Count];
                arrData = listData.ToArray();
                for (int i = 0; i < listData.Count; i++)
                {
                    string[] ss = new string[4];
                    ss = arrData[i].Split(';');
                    if (ss[0] != null && ss[1] != null && ss[2] != null && ss[3] != null)
                    {
                        if (ss[2] != "")
                        {
                            if ((bool)DGVData.Rows[i].Cells[1].Value == true)
                            {
                                string[] desc = ss[2].Split(',');
                                if (desc != null && desc.Length > 0)
                                {
                                    for (int j = 0; j < desc.Length; j++)
                                    {
                                        string[] layer = ss[2].Split(',');
                                        for (int k = 0; k < layer.Length; k++)
                                            if (layer != null && layer.Length > 0 && !layerOn.Contains(layer[k]))
                                                layerOn.Add(layer[k]);
                                    }
                                }
                            }
                            if ((bool)DGVData.Rows[i].Cells[1].Value == false)
                            {
                                string[] desc = ss[2].Split(',');
                                if (desc != null && desc.Length > 0)
                                {
                                    for (int j = 0; j < desc.Length; j++)
                                    {
                                        string[] layer = ss[2].Split(',');
                                        for (int k = 0; k < layer.Length; k++)
                                            if (layer != null && layer.Length > 0 && !layerOff.Contains(layer[k]))
                                                layerOff.Add(layer[k]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bool itemcheked = true;
            //    foreach (DataGridViewRow row in DGVData.Rows)
            //    {
            //        if ((bool)row.Cells[1].Value == false && row.Cells[0].Value.ToString() != "Các lớp")
            //        {
            //            itemcheked = false;
            //        }
            //    }

            //    if ((bool)DGVData.Rows[DGVData.Rows.Count - 1].Cells[1].Value == true && itemcheked == true)
            //    {
            //        List<string> listValueOut = new List<string>();
            //        List<string> layerOn = new List<string>();
            //        List<string> layerOff = new List<string>();
            //        List<string> alllayerOn = new List<string>();
            //        listValueOut = ClassGroupLayer.CreateDictionary(ClassGroupLayer.CreateListData());
            //        findLayerNameOnOff(listValueOut, ref layerOn, ref layerOff);
            //        int number = ClassGroupLayer.onLayer(layerOn);
            //        ClassGroupLayer.onLayer(layerOff);
            //        Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.UpdateScreen();

            //        DGVData.Rows.Clear();
            //        LoadDGV(listValueOut);
            //    }
            //    else
            //    {
            //        List<string> listValueOut = new List<string>();
            //        List<string> layerOn = new List<string>();
            //        List<string> layerOff = new List<string>();
            //        List<string> alllayerOn = new List<string>();
            //        listValueOut = ClassGroupLayer.CreateDictionary(ClassGroupLayer.CreateListData());
            //        findLayerNameOnOff(listValueOut, ref layerOn, ref layerOff);
            //        int number = ClassGroupLayer.onLayer(layerOn);
            //        ClassGroupLayer.offLayer(layerOff);
            //        Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.UpdateScreen();

            //        DGVData.Rows.Clear();
            //        LoadDGV(listValueOut);
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MyLogger.Log("Đã có lỗi xảy ra khi tắt bật lớp! {0}", ex.Message);
            //}
            ApplyChange();
            this.Close();
        }

        private void GroupLayerForm_Load(object sender, EventArgs e)
        {
            //List<string> listValueOut = new List<string>();
            //listValueOut = ClassGroupLayer.CreateDictionary(ClassGroupLayer.CreateListData());
            //LoadDGV(listValueOut);
            bool hasCheckedItem = true;
            if (MapMenuCommand.machinePointBombLayers.Count > 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.imageLayers[0]);
                int index = DGVData.Rows.Add("Quỹ đạo đo", visible);
                List<int> layers = new List<int>();
                layers.AddRange(MapMenuCommand.machinePointBombLayers);
                layers.AddRange(MapMenuCommand.machineLineBombLayers);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            if(MapMenuCommand.polygonLayers.Count > 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayers[0]);
                int index = DGVData.Rows.Add("Bề mặt địa hình (bom)", visible);
                DGVData.Rows[index].Tag = MapMenuCommand.polygonLayers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            if (MapMenuCommand.polygonLayersMine.Count > 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.polygonLayersMine[0]);
                int index = DGVData.Rows.Add("Bề mặt địa hình (mìn)", visible);
                DGVData.Rows[index].Tag = MapMenuCommand.polygonLayersMine;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
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
            if (MapMenuCommand.suspectPointLayerBomb >= 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.suspectPointLayerBomb);
                int index = DGVData.Rows.Add("Điểm nghi ngờ (bom)", visible);
                List<int> layers = new List<int>();
                layers.Add(MapMenuCommand.suspectPointLayerBomb);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            if (MapMenuCommand.suspectPointLayerMine >= 0)
            {
                bool visible = MapMenuCommand.axMap1.get_LayerVisible(MapMenuCommand.suspectPointLayerMine);
                int index = DGVData.Rows.Add("Điểm nghi ngờ (mìn)", visible);
                List<int> layers = new List<int>();
                layers.Add(MapMenuCommand.suspectPointLayerMine);
                DGVData.Rows[index].Tag = layers;
                if (!visible)
                {
                    hasCheckedItem = false;
                }
            }
            int lastIndex = DGVData.Rows.Add("Tất cả", hasCheckedItem);
            DGVData.Rows[lastIndex].Tag = new List<int>();
        }

        private void btApDung_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bool itemcheked = true;
            //    foreach (DataGridViewRow row in DGVData.Rows)
            //    {
            //        if ((bool)row.Cells[1].Value == false && row.Cells[0].Value.ToString() != "Các lớp")
            //        {
            //            itemcheked = false;
            //        }
            //    }

            //    if ((bool)DGVData.Rows[DGVData.Rows.Count - 1].Cells[1].Value == true && itemcheked == true)
            //    {
            //        List<string> listValueOut = new List<string>();
            //        List<string> layerOn = new List<string>();
            //        List<string> layerOff = new List<string>();
            //        List<string> alllayerOn = new List<string>();
            //        listValueOut = ClassGroupLayer.CreateDictionary(ClassGroupLayer.CreateListData());
            //        findLayerNameOnOff(listValueOut, ref layerOn, ref layerOff);
            //        int number = ClassGroupLayer.onLayer(layerOn);
            //        ClassGroupLayer.onLayer(layerOff);
            //        Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.UpdateScreen();

            //        DGVData.Rows.Clear();
            //        LoadDGV(listValueOut);
            //    }
            //    else
            //    {
            //        List<string> listValueOut = new List<string>();
            //        List<string> layerOn = new List<string>();
            //        List<string> layerOff = new List<string>();
            //        List<string> alllayerOn = new List<string>();
            //        listValueOut = ClassGroupLayer.CreateDictionary(ClassGroupLayer.CreateListData());
            //        findLayerNameOnOff(listValueOut, ref layerOn, ref layerOff);
            //        int number = ClassGroupLayer.onLayer(layerOn);
            //        ClassGroupLayer.offLayer(layerOff);
            //        Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.UpdateScreen();

            //        DGVData.Rows.Clear();
            //        LoadDGV(listValueOut);
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MyLogger.Log("Đã có lỗi xảy ra khi tắt bật lớp! {0}", ex.Message);
            //}
            ApplyChange();
        }

        private void ApplyChange()
        {
            foreach (DataGridViewRow row in DGVData.Rows)
            {
                List<int> layers = row.Tag as List<int>;
                foreach (int layer in layers)
                {
                    MapMenuCommand.axMap1.set_LayerVisible(layer, (bool)row.Cells[1].Value);
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
            if (e.ColumnIndex != 1)
                return;

            if (DGVData.Rows[e.RowIndex].Cells[0].Value.ToString() == "Tất cả" && (bool)DGVData.Rows[e.RowIndex].Cells[1].Value == true)
            {
                foreach (DataGridViewRow item in DGVData.Rows)
                {
                    if (item != null)
                    {
                        item.Cells[1].Value = false;
                    }
                }
            }
            else if (DGVData.Rows[e.RowIndex].Cells[0].Value.ToString() == "Tất cả" && (bool)DGVData.Rows[e.RowIndex].Cells[1].Value == false)
            {
                foreach (DataGridViewRow item in DGVData.Rows)
                {
                    if (item != null)
                    {
                        item.Cells[1].Value = true;
                    }
                }
            }

            DGVData.Update();
        }
    }
}