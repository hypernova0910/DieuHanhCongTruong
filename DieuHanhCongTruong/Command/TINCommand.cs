using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIConvexHull;
using DieuHanhCongTruong.Common;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
//using Autodesk.Civil.DatabaseServices;
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.Civil.ApplicationServices;

namespace DieuHanhCongTruong.Command
{
    class TINCommand
    {
        private const double MAX_TRIANGLE_LENGTH = 10;

        public static Dictionary<long, List<CustomFace>> triangulations_bomb = new Dictionary<long, List<CustomFace>>();
        public static Dictionary<long, List<CustomFace>> triangulations_mine = new Dictionary<long, List<CustomFace>>();

        public static IList<DefaultVertex2D> BuildConvexHull(List<InfoConnect> lst)
        {
            try
            {
                if (lst.Count == 0)
                {
                    return null;
                }
                List<double[]> vertices = new List<double[]>();
                foreach (InfoConnect infoConnect in lst)
                {
                    vertices.Add(new double[] { infoConnect.lat_value, infoConnect.long_value });
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

        

        public static void BuildDelaunayTriangulation(List<InfoConnect> lst, bool isBomb, long idKV)
        {
            try
            {
                if (lst.Count == 0)
                {
                    if (isBomb)
                    {
                        triangulations_bomb.Add(idKV, new List<CustomFace>());
                    }
                    else
                    {
                        triangulations_mine.Add(idKV, new List<CustomFace>());
                    }
                    
                    return;
                }
                List<double[]> vertices = new List<double[]>();
                //foreach (InfoConnect infoConnect in lst)
                //{
                //    vertices.Add(new double[] { infoConnect.lat_value, infoConnect.long_value });
                //}
                var triangle = Triangulation.CreateDelaunay<InfoConnect, CustomFace>(lst);
                if (triangle != null)
                {
                    //double minZ = double.MaxValue;
                    //double maxZ = double.MinValue;
                    List<CustomFace> lstCell = new List<CustomFace>();
                    foreach (var cell in triangle.Cells)
                    {
                        if(IsWithinMaxLength(cell))
                        {
                            //cell.id = Guid.NewGuid().ToString();
                            List<CustomFace> faces = cell.Adjacency.ToList();
                            for(int i = 0; i < faces.Count; i++)
                            {
                                if (!IsWithinMaxLength(cell))
                                {
                                    faces.RemoveAt(i);
                                    i--;
                                }
                            }
                            cell.Adjacency = faces.ToArray();
                            lstCell.Add(cell);
                            //foreach (var vertice in cell.Vertices)
                            //{
                            //    if (vertice.the_value > maxZ)
                            //    {
                            //        maxZ = vertice.the_value;
                            //    }
                            //    if (vertice.the_value < minZ)
                            //    {
                            //        minZ = vertice.the_value;
                            //    }
                            //}
                        }
                    }
                    if (isBomb)
                    {
                        triangulations_bomb.Add(idKV, lstCell);
                    }
                    else
                    {
                        triangulations_mine.Add(idKV, lstCell);
                    }
                    //double elevation = (maxZ - minZ) / MapMenuCommand.magnetic_colors.Length;
                    List<DataRow> lstColor = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "DaiMauTuTruong", "IsBomb", isBomb ? "1" : "2");

                    if (lstColor.Count > 0)
                    {
                        //double minZ = double.Parse(lstColor[0]["min"].ToString());
                        //double maxZ = double.Parse(lstColor[lstColor.Count - 1]["max"].ToString());
                        //BuildSurface(lstCell, minZ, maxZ, lstColor.Count);
                        for(int i = 0; i < lstColor.Count; i++)
                        {
                            DataRow dr = lstColor[i];
                            double min = double.Parse(dr["min"].ToString());
                            double max = double.Parse(dr["max"].ToString());
                            BuildOneColorSurface(lstCell, min, max, i, isBomb);
                        }
                        MapMenuCommand.Redraw();
                    }
                    else
                    {
                        double minZ;
                        double maxZ;
                        if (isBomb)
                        {
                            minZ = Constants.MIN_Z_BOMB;
                            maxZ = Constants.MAX_Z_BOMB;
                        }
                        else
                        {
                            minZ = Constants.MIN_Z_MINE;
                            maxZ = Constants.MAX_Z_MINE;
                        }
                        BuildSurface(lstCell, minZ, maxZ, Constants.magnetic_colors.Length, isBomb);
                    }



                }

            }
            catch (System.Exception ex)
            {
                
            }
        }

        private static bool IsWithinMaxLength(CustomFace cell)
        {
            double a = Distance(cell.Vertices[0], cell.Vertices[1]);
            double b = Distance(cell.Vertices[1], cell.Vertices[2]);
            double c = Distance(cell.Vertices[2], cell.Vertices[0]);
            if (
                a > MAX_TRIANGLE_LENGTH ||
                b > MAX_TRIANGLE_LENGTH ||
                c > MAX_TRIANGLE_LENGTH
            )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static double Distance(InfoConnect a, InfoConnect b)
        {
            return Math.Sqrt(Math.Pow(a.Position[0] - b.Position[0], 2) + Math.Pow(a.Position[1] - b.Position[1], 2));
        }

        private static List<InfoConnect> GetGiaoTamGiacMatPhang(CustomFace cell, double zMin, double zMax)
        {
            //3 đỉnh tam giác nằm giữa zMin và zMax => Lấy cả 3 điểm
            if ((cell.Vertices[0].the_value >= zMin && cell.Vertices[1].the_value >= zMin && cell.Vertices[2].the_value >= zMin) &&
               (cell.Vertices[0].the_value <= zMax && cell.Vertices[1].the_value <= zMax && cell.Vertices[2].the_value <= zMax))
            {
                return cell.Vertices.ToList();
            }
            //3 đỉnh tam giác nằm ngoài zMin hoặc zMax => Không lấy điểm nào
            else if ((cell.Vertices[0].the_value <= zMin && cell.Vertices[1].the_value <= zMin && cell.Vertices[2].the_value <= zMin) ||
               (cell.Vertices[0].the_value >= zMax && cell.Vertices[1].the_value >= zMax && cell.Vertices[2].the_value >= zMax))
            {
                return null;
            }
            else
            {
                List<InfoConnect> polygon_temp = new List<InfoConnect>();
                if(cell.Vertices[0].the_value >= zMin && cell.Vertices[0].the_value <= zMax)
                {
                    polygon_temp.Add(cell.Vertices[0]);
                }
                if (cell.Vertices[1].the_value >= zMin && cell.Vertices[1].the_value <= zMax)
                {
                    polygon_temp.Add(cell.Vertices[1]);
                }
                if (cell.Vertices[2].the_value >= zMin && cell.Vertices[2].the_value <= zMax)
                {
                    polygon_temp.Add(cell.Vertices[2]);
                }
                //zMin
                InfoConnect A_min = GiaoDoanThangMatPhang(cell.Vertices[0], cell.Vertices[1], zMin);
                if(A_min != null)
                {
                    polygon_temp.Add(A_min);
                }
                InfoConnect B_min = GiaoDoanThangMatPhang(cell.Vertices[1], cell.Vertices[2], zMin);
                if (B_min != null)
                {
                    polygon_temp.Add(B_min);
                }
                InfoConnect C_min = GiaoDoanThangMatPhang(cell.Vertices[2], cell.Vertices[0], zMin);
                if (C_min != null)
                {
                    polygon_temp.Add(C_min);
                }
                //zMax
                InfoConnect A_max = GiaoDoanThangMatPhang(cell.Vertices[0], cell.Vertices[1], zMax);
                if (A_max != null)
                {
                    polygon_temp.Add(A_max);
                }
                InfoConnect B_max = GiaoDoanThangMatPhang(cell.Vertices[1], cell.Vertices[2], zMax);
                if (B_max != null)
                {
                    polygon_temp.Add(B_max);
                }
                InfoConnect C_max = GiaoDoanThangMatPhang(cell.Vertices[2], cell.Vertices[0], zMax);
                if (C_max != null)
                {
                    polygon_temp.Add(C_max);
                }
                //sắp xếp các điểm theo thứ tự
                if(polygon_temp.Count == 0)
                {
                    return null;
                }
                //List<InfoConnect> polygon = new List<InfoConnect>();
                //polygon.Add(polygon_temp[0]);
                //polygon_temp.RemoveAt(0);
                //InfoConnect pointTemp = polygon_temp[0];
                //while (polygon_temp.Count > 0)
                //{
                //    //Mục đích tìm điểm gần nhất thay vì so  sánh điểm trùng để tránh sai số
                //    //delta = (x - x0) + (y - y0) + (z - z0) để tìm điểm gần nhất
                //    double delta = double.MaxValue;
                //    //chỉ số điểm gần nhất

                //    for(int i = 0; i < polygon_temp.Count; i++)
                //    {

                //    }
                //}
                //return polygon_temp;
                var convexHull = ConvexHull.Create2D(polygon_temp);
                if(convexHull.Outcome == ConvexHullCreationResultOutcome.Success)
                {
                    return convexHull.Result.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        //private List<DefaultVertex> GetThietDien(DefaultVertex[] tuDien, double z)
        //{
        //    //3 đỉnh tam giác nằm khác phía với mặt phẳng z
        //    if(!(tuDien[0].Position[2] > z && tuDien[1].Position[2] > z && tuDien[2].Position[2] > z) &&
        //       !(tuDien[0].Position[2] < z && tuDien[1].Position[2] < z && tuDien[2].Position[2] < z))
        //    {

        //    }
        //}

        public static InfoConnect GiaoDoanThangMatPhang(InfoConnect M0, InfoConnect N0, double Z)
        {
            //Check đoạn thẳng có cắt mặt phẳng không
            if ((M0.the_value > Z && N0.the_value > Z) ||
                (M0.the_value < Z && N0.the_value < Z))
            {
                return null;
            }
            //Phương trình đường thẳng đi qua điểm M(x0, y0, z0) và nhận vectơ u(a,b,c) làm vector chỉ phương
            //x = x0 + at
            //y = y0 + bt
            //z = z0 + ct
            //Với x0 = M0.Position[0], y0 = M0.Position[1], z0 = M0.Position[2]
            double x0 = M0.Position[0];
            double y0 = M0.Position[1];
            double z0 = M0.the_value;
            double a = N0.Position[0] - M0.Position[0];
            double b = N0.Position[1] - M0.Position[1];
            double c = N0.the_value - M0.the_value;
            if(c == 0)
            {
                return null;
            }
            //Mặt phẳng có phương trình z = Z
            //=> Tìm t từ phương trình z0 + ct = Z => t = (Z - z0) / c
            double t = (Z - z0) / c;
            double x = x0 + a * t;
            double y = y0 + b * t;
            double z = Z;
            InfoConnect infoConnect = new InfoConnect();
            infoConnect.lat_value = x;
            infoConnect.long_value = y;
            infoConnect.the_value = z;
            return infoConnect;
        }

        public static void BuildSurface(List<CustomFace> lstCell, double minZ, double maxZ, int colorCount, bool isBomb)
        {
            double elevation = (maxZ - minZ) / colorCount;
            if (elevation != 0)
            {
                for (int i = 0; i < Constants.magnetic_colors.Length; i++)
                {
                    double minElevation = minZ + i * elevation;
                    double maxElevation = minZ + (i + 1) * elevation;

                    BuildOneColorSurface(lstCell, minElevation, maxElevation, i, isBomb);
                }
                MapMenuCommand.Redraw();
            }
        }

        public static void BuildOneColorSurface(List<CustomFace> lstCell, double minElevation, double maxElevation, int colorIndex, bool isBomb)
        {
            foreach (var cell in lstCell)
            {
                List<double> xPoints_triangle = new List<double>();
                List<double> yPoints_triangle = new List<double>();
                List<InfoConnect> infoConnects = GetGiaoTamGiacMatPhang(cell, minElevation, maxElevation);
                if (infoConnects != null)
                {
                    foreach (var vertice in infoConnects)
                    {
                        double latt = 0, longt = 0;
                        AppUtils.ToLatLon(vertice.Position[0], vertice.Position[1], ref latt, ref longt, "48N");
                        xPoints_triangle.Add(longt);
                        yPoints_triangle.Add(latt);
                    }
                    MapMenuCommand.drawPolygon(colorIndex, xPoints_triangle.ToArray(), yPoints_triangle.ToArray(), isBomb);
                }
            }
        }
    }
}
