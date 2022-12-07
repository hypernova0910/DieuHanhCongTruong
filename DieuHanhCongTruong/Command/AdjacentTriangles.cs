using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNRaPaBomMin.Models;

namespace DieuHanhCongTruong.Command
{
    class AdjacentTriangles
    {
        private const double delta = 1e-4;
        private List<CustomFace> cellRead;
        public List<List<InfoConnect>> lstContour;
        private List<CustomFace> triangles;

        public AdjacentTriangles(List<CustomFace> triangles)
        {
            this.triangles = triangles;
            lstContour = new List<List<InfoConnect>>();
            cellRead = new List<CustomFace>();
        }

        private InfoConnect GetGiaoTamGiacMatPhang(CustomFace cell, double z0, out bool existPoint, InfoConnect initPoint)
        {
            //z0 không cắt 3 cạnh tam giác
            if ((cell.Vertices[0].the_value <= z0 && cell.Vertices[1].the_value <= z0 && cell.Vertices[2].the_value <= z0) ||
               (cell.Vertices[0].the_value >= z0 && cell.Vertices[1].the_value >= z0 && cell.Vertices[2].the_value >= z0))
            {
                existPoint = false;
                return null;
            }
            else
            {
                List<InfoConnect> polygon_temp = new List<InfoConnect>();
                //z0
                InfoConnect A = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[0], cell.Vertices[1], z0);
                if (A != null)
                {
                    polygon_temp.Add(A);
                    //Check điểm ở rìa lưới tam giác
                    //if (cell.Adjacency.Length < 3)
                    //{
                    //    bool isInEdgeTriangles = true;
                    //    foreach (CustomFace adjacent in cell.Adjacency)
                    //    {
                    //        if (adjacent.Vertices[0] == cell.Vertices[0] && adjacent.Vertices[1] == cell.Vertices[1] &&
                    //           adjacent.Vertices[1] == cell.Vertices[0] && adjacent.Vertices[0] == cell.Vertices[1])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //        if (adjacent.Vertices[1] == cell.Vertices[0] && adjacent.Vertices[2] == cell.Vertices[1] &&
                    //           adjacent.Vertices[2] == cell.Vertices[0] && adjacent.Vertices[1] == cell.Vertices[1])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //        if (adjacent.Vertices[2] == cell.Vertices[0] && adjacent.Vertices[0] == cell.Vertices[1] &&
                    //           adjacent.Vertices[0] == cell.Vertices[0] && adjacent.Vertices[2] == cell.Vertices[1])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //    }
                    //    A.isInEdgeTriangles = isInEdgeTriangles;
                    //}
                }
                InfoConnect B = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[1], cell.Vertices[2], z0);
                if (B != null)
                {
                    polygon_temp.Add(B);
                    //if (cell.Adjacency.Length < 3)
                    //{
                    //    bool isInEdgeTriangles = true;
                    //    foreach (CustomFace adjacent in cell.Adjacency)
                    //    {
                    //        if (adjacent.Vertices[0] == cell.Vertices[1] && adjacent.Vertices[1] == cell.Vertices[2] &&
                    //           adjacent.Vertices[1] == cell.Vertices[1] && adjacent.Vertices[0] == cell.Vertices[2])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //        if (adjacent.Vertices[1] == cell.Vertices[1] && adjacent.Vertices[2] == cell.Vertices[2] &&
                    //           adjacent.Vertices[2] == cell.Vertices[1] && adjacent.Vertices[1] == cell.Vertices[2])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //        if (adjacent.Vertices[2] == cell.Vertices[1] && adjacent.Vertices[0] == cell.Vertices[2] &&
                    //           adjacent.Vertices[0] == cell.Vertices[1] && adjacent.Vertices[2] == cell.Vertices[2])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //    }
                    //    A.isInEdgeTriangles = isInEdgeTriangles;
                    //}
                }
                InfoConnect C = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[2], cell.Vertices[0], z0);
                if (C != null)
                {
                    polygon_temp.Add(C);
                    //if (cell.Adjacency.Length < 3)
                    //{
                    //    bool isInEdgeTriangles = true;
                    //    foreach (CustomFace adjacent in cell.Adjacency)
                    //    {
                    //        if (adjacent.Vertices[0] == cell.Vertices[2] && adjacent.Vertices[1] == cell.Vertices[0] &&
                    //           adjacent.Vertices[1] == cell.Vertices[2] && adjacent.Vertices[0] == cell.Vertices[0])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //        if (adjacent.Vertices[1] == cell.Vertices[2] && adjacent.Vertices[2] == cell.Vertices[0] &&
                    //           adjacent.Vertices[2] == cell.Vertices[2] && adjacent.Vertices[1] == cell.Vertices[0])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //        if (adjacent.Vertices[2] == cell.Vertices[2] && adjacent.Vertices[0] == cell.Vertices[0] &&
                    //           adjacent.Vertices[0] == cell.Vertices[2] && adjacent.Vertices[2] == cell.Vertices[0])
                    //        {
                    //            isInEdgeTriangles = false;
                    //            break;
                    //        }
                    //    }
                    //    A.isInEdgeTriangles = isInEdgeTriangles;
                    //}
                }
                //sắp xếp các điểm theo thứ tự
                if (polygon_temp.Count == 0)
                {
                    existPoint = false;
                    return null;
                }
                
                //return polygon_temp;
                //List<InfoConnect> polygon = new List<InfoConnect>();
                InfoConnect pointEqualInit = null;
                //if(initPoint != null)
                //{
                foreach (InfoConnect point in polygon_temp)
                {
                    if (initPoint.lat_value == point.lat_value && initPoint.long_value == point.long_value)
                    {
                        pointEqualInit = point;
                    }
                }
                if (pointEqualInit != null)
                {
                    foreach (InfoConnect point in polygon_temp)
                    {
                        if (pointEqualInit != point)
                        {
                            existPoint = false;
                            return point;
                        }
                    }
                }
                existPoint = true;
                return null;
                //}
                //else
                //{
                //    existPoint = false;
                //    return polygon_temp[0];
                //}
                
            }
        }

        private bool ReadAdjacentFace(CustomFace cell, double z, InfoConnect pointInit, ref List<InfoConnect> lstRef)
        {
            if (cell == null)
            {
                return false;
            }
            //Kiểm tra cell đã duyệt chưa
            if (cellRead.Exists(face => face == cell))
            {
                return false;
            }
            
            //Giao tam giác cell với mặt phẳng
            InfoConnect infoConnect = GetGiaoTamGiacMatPhang(cell, z, out bool existPoint, pointInit);

            //Duyệt cell
            //existPoint = false thì không tồn tại điểm cắt hoặc đã lấy được điểm cắt
            //existPoint = true thì tồn tại điểm cắt nhưng chưa lấy ngay do chưa phải là điểm tiếp theo trong polygon
            if (!existPoint)
            {
                cellRead.Add(cell);
            }
            if (infoConnect == null)
            {
                return false;
            }
            lstRef.Add(infoConnect);
            //Giao các tam giác liền kề cell với mặt phẳng
            foreach (CustomFace adjacent in cell.Adjacency)
            {
                if(ReadAdjacentFace(adjacent, z, infoConnect, ref lstRef))
                {
                    break;
                }
                //List<InfoConnect> lst = new List<InfoConnect>();
                //foreach (InfoConnect infoConnect in lst_temp)
                //{
                //    if (!infoConnects.Exists(point => point.lat_value == infoConnect.lat_value && point.long_value == infoConnect.long_value))
                //    {
                //        lst.Add(infoConnect);
                //    }
                //}
                //infoConnects.AddRange(lst);
                //infoConnects.AddRange(ReadAdjacentFace(adjacent, z));
            }
            return true;
        }

        private List<InfoConnect> GetGiaoTamGiacMatPhangInit(CustomFace cell, double z0)
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
                //z0
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
                //List<InfoConnect> polygon = new List<InfoConnect>();
                //polygon.Add(polygon_temp[0]);
                //return polygon;
            }
        }

        public void GetSuspectPoints(double z, bool ascend)
        {
            cellRead.Clear();
            foreach (var cell in triangles)
            {
                if (cellRead.Exists(face => face == cell))
                {
                    continue;
                }
                List<InfoConnect> lstPointIntersect = GetGiaoTamGiacMatPhangInit(cell, z);
                if (lstPointIntersect != null)
                {
                    //List<InfoConnect> lstPointIntersect = new List<InfoConnect>();
                    ReadAdjacentFace(cell, z, lstPointIntersect[lstPointIntersect.Count - 1], ref lstPointIntersect);
                    lstContour.Add(lstPointIntersect);
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
