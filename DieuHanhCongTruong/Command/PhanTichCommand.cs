using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Models;
using MapWinGIS;
using MIConvexHull;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNRaPaBomMin.Models;

namespace DieuHanhCongTruong.Command
{
    class PhanTichCommand
    {
        private const double delta = 1e-4;
        private List<CustomFace> cellRead;
        public List<List<InfoConnect>> lstContour;
        private List<CustomFace> triangles;

        public PhanTichCommand(List<CustomFace> triangles)
        {
            this.triangles = triangles;
            lstContour = new List<List<InfoConnect>>();
            cellRead = new List<CustomFace>();
        }

        public void Execute()
        {
            if (triangles == null)
            {
                return;
            }
            double elevation = 0, contourMinElevation = double.MaxValue, contourMaxElevation = double.MinValue;
            
            List<DataRow> lst = UtilsDatabase.GetAllDataInTable(UtilsDatabase._ExtraInfoConnettion, "DaiMauTuTruong");

            if (lst.Count > 0)
            {
                foreach (DataRow dr in lst)
                {
                    //DGVThongTin.Rows.Add(dr["min"].ToString(), dr["max"].ToString());

                    //DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)DGVThongTin.Rows[DGVThongTin.Rows.Count - 1].Cells[cotColor.Index];
                    //int r = int.Parse(dr["red"].ToString());
                    //int g = int.Parse(dr["green"].ToString());
                    //int b = int.Parse(dr["blue"].ToString());
                    //Color color = Color.FromArgb(r, g, b);
                    //buttonCell.Style.BackColor = color;
                    //buttonCell.Style.SelectionBackColor = color;
                    double min = double.Parse(dr["min"].ToString());
                    double max = double.Parse(dr["max"].ToString());
                    if(contourMinElevation > min)
                    {
                        contourMinElevation = min;
                    }
                    if(contourMaxElevation < max)
                    {
                        contourMaxElevation = max;
                    }
                    
                }
                elevation = (contourMaxElevation - contourMinElevation) / lst.Count;
                contourMinElevation = contourMinElevation + elevation / 2;
                contourMaxElevation = contourMaxElevation - elevation / 2;
            }
            else
            {
                elevation = (Constants.MAX_Z_BOMB - Constants.MIN_Z_BOMB) / Constants.magnetic_colors.Length;
                contourMinElevation = Constants.MIN_Z_BOMB + elevation / 2;
                contourMaxElevation = Constants.MAX_Z_BOMB - elevation / 2;
            }
            //list các đường đồng mức
            //GetSuspectPoints(contourMaxElevation);
            //GetSuspectPoints(contourMinElevation);
            AdjacentTriangles cmd = new AdjacentTriangles(triangles);
            cmd.GetSuspectPoints(contourMaxElevation);
            lstContour.AddRange(cmd.lstContour);
            cmd = new AdjacentTriangles(triangles);
            cmd.GetSuspectPoints(contourMinElevation);
            lstContour.AddRange(cmd.lstContour);

            foreach (List<InfoConnect> contour in lstContour)
            {
                //Shape shp = new Shape();
                //shp.Create(ShpfileType.SHP_POLYGON);
                double sumX = 0, sumY = 0, sumZ = 0; ;
                foreach (InfoConnect point in contour)
                {
                    
                    
                    //Point pnt = new Point();
                    //pnt.x = longt;
                    //pnt.y = latt;
                    //shp.AddPoint(longt, latt);
                    //MapMenuCommand.addSuspectPoint(longt, latt);
                    sumX += point.lat_value;
                    sumY += point.long_value;
                    sumZ += point.the_value;
                }
                double x = sumX / contour.Count;
                double y = sumY / contour.Count;
                double z = sumZ / contour.Count;
                double latt = 0, longt = 0;
                AppUtils.ToLatLon(x, y, ref latt, ref longt, "48N");
                CecmReportPollutionAreaBMVN bmvn = new CecmReportPollutionAreaBMVN();
                bmvn.XPoint = x;
                bmvn.YPoint = y;
                //bmvn.ZPoint = z;
                bmvn.ZPoint = GetMagneticAtPoint(x, y);
                MapMenuCommand.addSuspectPoint(longt, latt, bmvn);
                //double area = PhanTichKhoangGiamNghiNgoCommand.FindArea(x, y, z, 50, triangles);
                //Point centroid = shp.Centroid;
                //MapMenuCommand.addSuspectPoint(centroid.x, centroid.y);
            }
            
        }

        //private List<InfoConnect> GetGiaoTamGiacMatPhang(CustomFace cell, double z0)
        //{
        //    //z0 không cắt 3 cạnh tam giác
        //    if ((cell.Vertices[0].the_value <= z0 && cell.Vertices[1].the_value <= z0 && cell.Vertices[2].the_value <= z0) ||
        //       (cell.Vertices[0].the_value >= z0 && cell.Vertices[1].the_value >= z0 && cell.Vertices[2].the_value >= z0))
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        List<InfoConnect> polygon_temp = new List<InfoConnect>();
        //        //z0
        //        InfoConnect A = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[0], cell.Vertices[1], z0);
        //        if (A != null)
        //        {
        //            polygon_temp.Add(A);
        //        }
        //        InfoConnect B = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[1], cell.Vertices[2], z0);
        //        if (B != null)
        //        {
        //            polygon_temp.Add(B);
        //        }
        //        InfoConnect C = TINCommand.GiaoDoanThangMatPhang(cell.Vertices[2], cell.Vertices[0], z0);
        //        if (C != null)
        //        {
        //            polygon_temp.Add(C);
        //        }
        //        //sắp xếp các điểm theo thứ tự
        //        if (polygon_temp.Count == 0)
        //        {
        //            return null;
        //        }
        //        return polygon_temp;
        //        //List<InfoConnect> polygon = new List<InfoConnect>();
        //        //polygon.Add(polygon_temp[0]);
        //        //return polygon;
        //    }
        //}

        //private List<InfoConnect> ReadAdjacentFace(CustomFace cell, double z)
        //{
        //    if(cell == null)
        //    {
        //        return new List<InfoConnect>();
        //    }
        //    //Kiểm tra cell đã duyệt chưa
        //    if(cellRead.Exists(face => face == cell))
        //    {
        //        return new List<InfoConnect>();
        //    }
        //    //Duyệt cell
        //    cellRead.Add(cell);
        //    //Giao tam giác cell với mặt phẳng
        //    List<InfoConnect> infoConnects = GetGiaoTamGiacMatPhang(cell, z);
        //    if (infoConnects == null)
        //    {
        //        return new List<InfoConnect>();
        //    }
        //    //Giao các tam giác liền kề cell với mặt phẳng
        //    foreach (CustomFace adjacent in cell.Adjacency)
        //    {
        //        List<InfoConnect> lst_temp = ReadAdjacentFace(adjacent, z);
        //        List<InfoConnect> lst = new List<InfoConnect>();
        //        foreach (InfoConnect infoConnect in lst_temp)
        //        {
        //            if(!infoConnects.Exists(point => point.lat_value == infoConnect.lat_value && point.long_value == infoConnect.long_value))
        //            {
        //                lst.Add(infoConnect);
        //            }
        //        }
        //        infoConnects.AddRange(lst);
        //        //infoConnects.AddRange(ReadAdjacentFace(adjacent, z));
        //    }
        //    return infoConnects;
        //}

        //public void GetSuspectPoints(double z)
        //{
        //    cellRead.Clear();
        //    foreach (var cell in triangles)
        //    {
        //        if (cellRead.Exists(face => face == cell))
        //        {
        //            continue;
        //        }
        //        List<InfoConnect> infoConnectsMin = GetGiaoTamGiacMatPhang(cell, z);
        //        if (infoConnectsMin != null)
        //        {
        //            infoConnectsMin = ReadAdjacentFace(cell, z);
        //            lstContour.Add(infoConnectsMin);
        //        }
        //        else
        //        {
        //            cellRead.Add(cell);
        //        }

        //        //if(lstContour.Count == 0)
        //        //{
        //        //    List<InfoConnect> contour = new List<InfoConnect>();
        //        //    contour.AddRange(infoConnectsMin);
        //        //    continue;
        //        //}
        //        //for(int i = 0; i < lstContour.Count; i++)
        //        //{

        //        //}
        //    }
        //}

        private double GetMagneticAtPoint(double x, double y)
        {
            InfoConnect start = new InfoConnect(x, y, 0);
            InfoConnect end = new InfoConnect(x, y, 100);
            foreach(CustomFace cell in triangles)
            {
                //Tìm vector chỉ phương AB, AC
                double vtcp1_x = cell.Vertices[0].lat_value - cell.Vertices[1].lat_value;
                double vtcp1_y = cell.Vertices[0].long_value - cell.Vertices[1].long_value;
                double vtcp1_z = cell.Vertices[0].the_value - cell.Vertices[1].the_value;
                double vtcp2_x = cell.Vertices[1].lat_value - cell.Vertices[2].lat_value;
                double vtcp2_y = cell.Vertices[1].long_value - cell.Vertices[2].long_value;
                double vtcp2_z = cell.Vertices[1].the_value - cell.Vertices[2].the_value;
                //Tìm vector pháp tuyến
                double vtpt_x = vtcp1_y * vtcp2_z - vtcp2_y * vtcp1_z;  //a
                double vtpt_y = vtcp1_z * vtcp2_x - vtcp2_z * vtcp1_x;  //b
                double vtpt_z = vtcp1_x * vtcp2_y - vtcp2_x * vtcp1_y;  //c
                //Phương trình mặt phẳng a(x - x0) + b(y - y0) + c(z - z0) = 0
                //Chuyển về ax + by + cz = ax0 + by0 + cz0 = heSoTuDo
                double heSoTuDo = vtpt_x * cell.Vertices[0].lat_value + vtpt_y * cell.Vertices[0].long_value + vtpt_z * cell.Vertices[0].the_value;

                //Shape shpPolygon = new Shape();
                //shpPolygon.Create(ShpfileType.SHP_POLYGON);
                //MapMenuCommand.drawPolygonTest(cell.Vertices.ToList());
                //for (int i = 0; i < cell.Vertices.Length; i++)
                //{
                //    InfoConnect infoConnect = cell.Vertices[i];
                //    Point vertexPolygon = new Point();
                //    Point2d point2D = AppUtils.ConverUTMToLatLong(infoConnect.lat_value, infoConnect.long_value);
                //    vertexPolygon.x = point2D.X;
                //    vertexPolygon.y = point2D.Y;
                //    //pnt.x = infoConnect.lat_value;
                //    //pnt.y = infoConnect.long_value;
                //    shpPolygon.InsertPoint(vertexPolygon, ref i);
                //}
                //Point2d point_temp = AppUtils.ConverUTMToLatLong(x, y);
                //Point point = new Point();
                //point.x = point_temp.X;
                //point.y = point_temp.Y;
                //if (shpPolygon.PointInThisPoly(point))
                if (GeometryUtils.isInside(
                    cell.Vertices[0].lat_value, cell.Vertices[0].long_value,
                    cell.Vertices[1].lat_value, cell.Vertices[1].long_value,
                    cell.Vertices[2].lat_value, cell.Vertices[2].long_value,
                    x, y))
                {
                    InfoConnect pointIntersect = GeometryUtils.GiaoDuongThangMatPhang(start, end, vtpt_x, vtpt_y, vtpt_z, heSoTuDo, false);
                    if (pointIntersect != null)
                    {
                        return pointIntersect.the_value;
                    }
                }
            }
            //double diff = GeometryUtils.minDiff;
            //GeometryUtils.minDiff = double.MaxValue;
            return double.NaN;
        }

        
    }
}
