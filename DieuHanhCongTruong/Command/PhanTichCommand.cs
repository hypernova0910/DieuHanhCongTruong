using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using MIConvexHull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Command
{
    class PhanTichCommand
    {
        private const double delta = 1e-4;
        private List<CustomFace> cellRead = new List<CustomFace>();
        private List<List<InfoConnect>> lstContour = new List<List<InfoConnect>>();
        private List<CustomFace> triangles;

        public PhanTichCommand(List<CustomFace> triangles)
        {
            this.triangles = triangles;
        }

        public void Execute()
        {
            //MapMenuCommand.ClearSuspectPoints();
            if (triangles == null)
            {
                return;
            }
            double elevation = (Constants.MAX_Z_BOMB - Constants.MIN_Z_BOMB) / Constants.magnetic_colors.Length;
            double contourMinElevation = Constants.MIN_Z_BOMB + elevation / 2;
            double contourMaxElevation = Constants.MAX_Z_BOMB - elevation / 2;
            //list các đường đồng mức
            GetSuspectPoints(contourMaxElevation);
            GetSuspectPoints(contourMinElevation);

            foreach (List<InfoConnect> infoConnects in lstContour)
            {
                //Shape shp = new Shape();
                //shp.Create(ShpfileType.SHP_POLYGON);
                double sumX = 0, sumY = 0;
                foreach (InfoConnect point in infoConnects)
                {
                    double latt = 0, longt = 0;
                    AppUtils.ToLatLon(point.Position[0], point.Position[1], ref latt, ref longt, "48N");
                    //Point pnt = new Point();
                    //pnt.x = longt;
                    //pnt.y = latt;
                    //shp.AddPoint(longt, latt);
                    //MapMenuCommand.addSuspectPoint(longt, latt);
                    sumX += longt;
                    sumY += latt;
                }
                double x = sumX / infoConnects.Count;
                double y = sumY / infoConnects.Count;
                MapMenuCommand.addSuspectPoint(x, y);
                //Point centroid = shp.Centroid;
                //MapMenuCommand.addSuspectPoint(centroid.x, centroid.y);
            }
            
        }

        private List<InfoConnect> GetGiaoTamGiacMatPhang(CustomFace cell, double z0)
        {
            //z0 không cắt 3 cạnh tam giác
            if ((cell.Vertices[0].the_value <= z0 && cell.Vertices[1].the_value <= z0 && cell.Vertices[2].the_value <= z0) ||
               (cell.Vertices[0].the_value >= z0 && cell.Vertices[1].the_value >= z0 && cell.Vertices[2].the_value >= z0))
            {
                return null;
            }
            else
            {
                List<InfoConnect> polygon_temp = new List<InfoConnect>();
                //zMin
                InfoConnect A = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[0], cell.Vertices[1], z0);
                if (A != null)
                {
                    polygon_temp.Add(A);
                }
                InfoConnect B = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[1], cell.Vertices[2], z0);
                if (B != null)
                {
                    polygon_temp.Add(B);
                }
                InfoConnect C = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[2], cell.Vertices[0], z0);
                if (C != null)
                {
                    polygon_temp.Add(C);
                }
                //sắp xếp các điểm theo thứ tự
                if (polygon_temp.Count == 0)
                {
                    return null;
                }
                return polygon_temp;
            }
        }

        private List<InfoConnect> ReadAdjacentFace(CustomFace cell, double z)
        {
            //Kiểm tra cell đã duyệt chưa
            if(cellRead.Exists(face => face.id == cell.id))
            {
                return new List<InfoConnect>();
            }
            //Duyệt cell
            cellRead.Add(cell);
            //Giao tam giác cell với mặt phẳng
            List<InfoConnect> infoConnects = GetGiaoTamGiacMatPhang(cell, z);
            if (infoConnects == null)
            {
                return new List<InfoConnect>();
            }
            //Giao các tam giác liền kề cell với mặt phẳng
            foreach (CustomFace adjacent in cell.Adjacency)
            {
                infoConnects.AddRange(ReadAdjacentFace(adjacent, z));
            }
            return infoConnects;
        }

        private void GetSuspectPoints(double z)
        {
            cellRead.Clear();
            foreach (var cell in triangles)
            {
                if (cellRead.Exists(face => face.id == cell.id))
                {
                    continue;
                }
                List<InfoConnect> infoConnectsMin = GetGiaoTamGiacMatPhang(cell, z);
                if (infoConnectsMin != null)
                {
                    infoConnectsMin = ReadAdjacentFace(cell, z);
                    lstContour.Add(infoConnectsMin);
                }
                else
                {
                    cellRead.Add(cell);
                }

                //if(lstContour.Count == 0)
                //{
                //    List<InfoConnect> contour = new List<InfoConnect>();
                //    contour.AddRange(infoConnectsMin);
                //    continue;
                //}
                //for(int i = 0; i < lstContour.Count; i++)
                //{

                //}
            }
        }
    }
}
