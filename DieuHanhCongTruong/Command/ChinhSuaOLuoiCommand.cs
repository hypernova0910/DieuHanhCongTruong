using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
using DieuHanhCongTruong.Forms.InAn;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieuHanhCongTruong.Command
{
    class ChinhSuaOLuoiCommand
    {
        public static void Execute()
        {
            MapMenuCommand.axMap1.ChooseLayer += AxMap1_ChooseLayer;
            MapMenuCommand.axMap1.Identifier.IdentifierMode = tkIdentifierMode.imSingleLayer;
            MapMenuCommand.axMap1.Identifier.HotTracking = true;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmIdentify;
            MapMenuCommand.axMap1.ShowToolTip("Chọn ô lưới", Constants.TOOLTIP_MAP_TIME);

            MapMenuCommand.axMap1.ShapeIdentified += AxMap1_ShapeIdentified;
            MyMainMenu2.Instance.KeyDown += Instance_KeyDown;
            Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(MapMenuCommand.oluoiLayer);
            sf.Identifiable = true;
            MyMainMenu2.Instance.menuStrip1.Enabled = false;
        }

        private static void Exit()
        {
            MapMenuCommand.axMap1.ChooseLayer -= AxMap1_ChooseLayer;
            MapMenuCommand.axMap1.ShapeIdentified -= AxMap1_ShapeIdentified;
            MyMainMenu2.Instance.KeyDown -= Instance_KeyDown;
            MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmPan;
            Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(MapMenuCommand.oluoiLayer);
            sf.Identifiable = false;
            MyMainMenu2.Instance.menuStrip1.Enabled = true;
            //MapMenuCommand.axMap1.IdentifiedShapes.Clear();
        }

        private static void AxMap1_ChooseLayer(object sender, AxMapWinGIS._DMapEvents_ChooseLayerEvent e)
        {
            e.layerHandle = MapMenuCommand.oluoiLayer;
        }

        private static void Instance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        private static void AxMap1_ShapeIdentified(object sender, AxMapWinGIS._DMapEvents_ShapeIdentifiedEvent e)
        {
            if (e.layerHandle == MapMenuCommand.oluoiLayer)
            {
                Shapefile sf = MapMenuCommand.axMap1.get_Shapefile(e.layerHandle);
                Shape shp = sf.Shape[e.shapeIndex];
                OLuoi oLuoi = JsonConvert.DeserializeObject<OLuoi>(shp.Key);
                ChinhSuaOLuoi form = new ChinhSuaOLuoi(oLuoi.Copy());
                DialogResult result = form.ShowDialog();
                if(result == DialogResult.OK)
                {
                    //Remove old
                    Shapefile sfLine = MapMenuCommand.axMap1.get_Shapefile(MapMenuCommand.ranhDoLayer);
                    //int indexShapeDelete = -1;
                    //int indexLabelDelete = -1;
                    //if (oLuoi.lstRanhDo.Count > 0)
                    //{
                    //    indexShapeDelete = oLuoi.lstRanhDo[0].indexShape;
                    //    indexLabelDelete = oLuoi.lstRanhDo[0].indexLabel;
                    //}
                    List<CecmProgramAreaLineDTO> lstRanhDoReverse = oLuoi.lstRanhDo.Reverse<CecmProgramAreaLineDTO>().ToList();
                    foreach (CecmProgramAreaLineDTO line in lstRanhDoReverse)
                    {
                        sfLine.EditDeleteShape(line.indexShape);
                        if (line.indexLabel >= 0)
                        {
                            sfLine.Labels.RemoveLabel(line.indexLabel);
                        }
                    }
                    sf.EditDeleteShape(e.shapeIndex);
                    MapMenuCommand.idKV__shapeOLuoi[oLuoi.cecm_program_areamap_ID].Remove(shp);
                    //Add new
                    for (int i = 0; i < form.oLuoi.lstRanhDo.Count; i++)
                    {
                        CecmProgramAreaLineDTO line = form.oLuoi.lstRanhDo[i];
                        string json = JsonConvert.SerializeObject(line);
                        MapMenuCommand.addRanhDo(line.start_y, line.start_x, line.end_y, line.end_x, i % 2 == 1 ? i.ToString() : "", json, out int indexShape, out int indexLabel);
                        line.indexShape = indexShape;
                        line.indexLabel = indexLabel;
                    }
                    MapMenuCommand.drawOluoi(form.oLuoi);
                    MapMenuCommand.axMap1.Redraw();
                    //MapMenuCommand.LoadDuAn(MyMainMenu2.idDADH);
                }
                Exit();
            }
        }
    }
}
