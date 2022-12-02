using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Forms;
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
        private long idKV;
        private bool isBomb;

        public PhanTichCommand(List<CustomFace> triangles, long idKV, bool isBomb)
        {
            this.triangles = triangles;
            this.idKV = idKV;
            lstContour = new List<List<InfoConnect>>();
            cellRead = new List<CustomFace>();
            this.isBomb = isBomb;
        }

        public void Execute()
        {
            if (triangles == null)
            {
                return;
            }
            double elevation = 0, contourMinElevation = double.MaxValue, contourMaxElevation = double.MinValue;

            List<DataRow> lst = UtilsDatabase.GetAllDataInTableWithId(UtilsDatabase._ExtraInfoConnettion, "DaiMauTuTruong", "IsBomb", isBomb ? "1" : "2");

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
            cmd.GetSuspectPoints(contourMaxElevation, true);
            lstContour.AddRange(cmd.lstContour);
            List<List<InfoConnect>> lstContourMax = cmd.lstContour;
            List<List<InfoConnect>> lstContourMin = new List<List<InfoConnect>>();
            if (isBomb)
            {
                cmd = new AdjacentTriangles(triangles);
                cmd.GetSuspectPoints(contourMinElevation, false);
                lstContour.AddRange(cmd.lstContour);
                lstContourMin = cmd.lstContour;
            }

            foreach (List<InfoConnect> contour in lstContourMax)
            {
                AddPoint(contour, true);
            }
            foreach (List<InfoConnect> contour in lstContourMin)
            {
                AddPoint(contour, false);
            }
        }

        private void AddPoint(List<InfoConnect> contour, bool ascend)
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
            bmvn.idArea = idKV;
            bmvn.programId = MyMainMenu2.idDADH;
            bmvn.XPoint = x;
            bmvn.YPoint = y;
            var latLong = AppUtils.ConverUTMToLatLong(bmvn.XPoint, bmvn.YPoint);
            bmvn.Vido = latLong.Y;
            bmvn.Kinhdo = latLong.X;
            bmvn.TimeExecute = DateTime.Now;
            //bmvn.ZPoint = z;
            bmvn.ZPoint = GeometryUtils.GetMagneticAtPoint(x, y, triangles);
            List<InfoConnect> contourGiamNghiNgo = new List<InfoConnect>();
            double area = PhanTichKhoangGiamNghiNgoCommand.FindArea(bmvn, 50, triangles, ref contourGiamNghiNgo, ascend, false);
            bmvn.Area = area;
            bmvn.contour = contourGiamNghiNgo;
            if (contourGiamNghiNgo.Count > 2)
            {
                PhanTichKhoangGiamNghiNgoCommand.FindDeep(bmvn);
            }
            else
            {
                bmvn.Deep = 0;
            }
            bmvn.UserAdd = false;
            MapMenuCommand.addSuspectPoint(longt, latt, bmvn, isBomb, false);
            //double area = PhanTichKhoangGiamNghiNgoCommand.FindArea(x, y, z, 50, triangles);
            //Point centroid = shp.Centroid;
            //MapMenuCommand.addSuspectPoint(centroid.x, centroid.y);
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




    }
}
