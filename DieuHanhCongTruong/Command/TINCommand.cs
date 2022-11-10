using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIConvexHull;
//using Autodesk.Civil.DatabaseServices;
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.Civil.ApplicationServices;

namespace DieuHanhCongTruong.Command
{
    class TINCommand
    {
        public static IList<DefaultVertex2D> Build(List<InfoConnect> lst)
        {
            try
            {
                if(lst.Count == 0)
                {
                    return null;
                }
                List<double[]> vertices = new List<double[]>();
                foreach(InfoConnect infoConnect in lst)
                {
                    vertices.Add(new double[] { infoConnect.lat_value, infoConnect.long_value});
                }
                var convexHull = ConvexHull.Create2D(vertices);

                IList<DefaultVertex2D> hullPoints = convexHull.Result;
                return hullPoints;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }


    }
}
