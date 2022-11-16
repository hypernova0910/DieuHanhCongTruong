using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapWinGIS;

namespace DieuHanhCongTruong.Command
{
    class VeMatCatTuTruongCommand
    {
        public static void Execute()
        {
            //MapMenuCommand.axMap1.SendMouseDown = true;
            ////MapMenuCommand.axMap1.Identifier.IdentifierMode = tkIdentifierMode.imAllLayers;
            //MapMenuCommand.axMap1.CursorMode = tkCursorMode.cmIdentify;
            //MapMenuCommand.axMap1.ShapeIdentified += AxMap1_ShapeIdentified;
        }

        private static void AxMap1_ShapeIdentified(object sender, AxMapWinGIS._DMapEvents_ShapeIdentifiedEvent e)
        {
            
        }
    }
}
