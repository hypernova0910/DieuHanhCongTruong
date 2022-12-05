using AxMapWinGIS;
using DieuHanhCongTruong.Command;
using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DieuHanhCongTruong.Common
{
    class AutoDivide
    {
        private static List<CecmProgramAreaLineDTO> CreateLineByAcuteAngle(long cecmprogram_id, long cecmprogramareamap_id, long cecmprogramareasub_id, long corner_num, PointDTO goc1, PointDTO goc2, PointDTO goc3, PointDTO goc4, Double alpha, Double distanceTmp)
        {
            List<CecmProgramAreaLineDTO> lst = new List<CecmProgramAreaLineDTO>();
            Double alplaRadian = alpha * Math.PI / 180;
            PointF point1 = new PointF();
            point1.X = (float)goc1.xUTM;
            point1.Y = (float)goc1.yUTM;
            PointF point2 = new PointF();
            point2.X = (float)goc2.xUTM;
            point2.Y = (float)goc2.yUTM;
            PointF point3 = new PointF();
            point3.X = (float)goc3.xUTM;
            point3.Y = (float)goc3.yUTM;
            PointF point4 = new PointF();
            point4.X = (float)goc4.xUTM;
            point4.Y = (float)goc4.yUTM;

            if (alpha < 90)
            {
                int count = 1;
                while (true)
                {
                    Double hightCount = distanceTmp * count;
                    Double dX = hightCount / Math.Cos(alplaRadian);
                    Double dY = hightCount / Math.Sin(alplaRadian);
                    Double pointX = goc4.xUTM + dX;
                    Double pointY = goc4.yUTM + dY;

                    PointF pointTempStart = new PointF();
                    pointTempStart.X = (float)pointX;
                    pointTempStart.Y = (float)goc1.yUTM;
                    PointF pointTempEnd = new PointF();
                    pointTempEnd.X = (float)goc1.xUTM;
                    pointTempEnd.Y = (float)pointY;

                    List<PointF> lstLine = new List<PointF>();

                    //Check đường 12 giao đường tìm được
                    PointF resutl = FindLineIntersection(point1, point2, pointTempStart, pointTempEnd);
                    if (resutl.IsEmpty == false)
                    {
                        if (PointOnLineSegment(point1, point2, resutl, 0.001))
                        {
                            lstLine.Add(resutl);
                        }
                    }

                    //Check đường 23 giao đường tìm được
                    resutl = FindLineIntersection(point3, point2, pointTempStart, pointTempEnd);
                    if (resutl.IsEmpty == false)
                    {
                        if (PointOnLineSegment(point3, point2, resutl, 0.001))
                        {
                            lstLine.Add(resutl);
                        }
                    }

                    //Check đường 34 giao đường tìm được
                    resutl = FindLineIntersection(point3, point4, pointTempStart, pointTempEnd);
                    if (resutl.IsEmpty == false)
                    {
                        if (PointOnLineSegment(point3, point4, resutl, 0.001))
                        {
                            lstLine.Add(resutl);
                        }
                    }

                    //Check đường 41 giao đường tìm được
                    resutl = FindLineIntersection(point1, point4, pointTempStart, pointTempEnd);
                    if (resutl.IsEmpty == false)
                    {
                        if (PointOnLineSegment(point1, point4, resutl, 0.001))
                        {
                            lstLine.Add(resutl);
                        }
                    }

                    if (lstLine.Count < 2)
                    {
                        break;
                    }
                    else if (lstLine.Count >= 2)
                    {
                        CecmProgramAreaLineDTO cecmProgramAreaLineDTO = new CecmProgramAreaLineDTO();
                        cecmProgramAreaLineDTO.cecmprogram_id = cecmprogram_id;
                        cecmProgramAreaLineDTO.cecmprogramareamap_id = cecmprogramareamap_id;
                        cecmProgramAreaLineDTO.cecmprogramareasub_id = cecmprogramareasub_id;
                        cecmProgramAreaLineDTO.start_x = lstLine[0].X;
                        cecmProgramAreaLineDTO.start_y = lstLine[0].Y;
                        cecmProgramAreaLineDTO.end_x = lstLine[1].X;
                        cecmProgramAreaLineDTO.end_y = lstLine[1].Y;
                        lst.Add(cecmProgramAreaLineDTO);
                    }
                    Console.WriteLine(count);
                    count++;
                }

            }
            return lst;
        }

        private static List<CecmProgramAreaLineDTO> calculateLines(OLuoi item)
        {
            //convert to utm
            double[] latlongCenter = AppUtils.ConverLatLongToUTM(item.lat_center, item.long_center);
            item.lat_center = latlongCenter[0];
            item.long_center = latlongCenter[1];
            double[] latlong1 = AppUtils.ConverLatLongToUTM(item.lat_corner1, item.long_corner1);
            item.lat_corner1 = latlong1[0];
            item.long_corner1 = latlong1[1];
            double[] latlong2 = AppUtils.ConverLatLongToUTM(item.lat_corner2, item.long_corner2);
            item.lat_corner2 = latlong2[0];
            item.long_corner2 = latlong2[1];
            double[] latlong3 = AppUtils.ConverLatLongToUTM(item.lat_corner3, item.long_corner3);
            item.lat_corner3 = latlong3[0];
            item.long_corner3 = latlong3[1];
            double[] latlong4 = AppUtils.ConverLatLongToUTM(item.lat_corner4, item.long_corner4);
            item.lat_corner4 = latlong4[0];
            item.long_corner4 = latlong4[1];

            double NuaDoDaiCanh = item.lat_center - item.lat_corner4;

            if (item.dividerAllGrid == 1)
            {
                ChiaMatCatOLuoiData data = new ChiaMatCatOLuoiData();
                data.gocTuyChon1 = item.acuteAngle1;
                data.gocTuyChon2 = item.acuteAngle2;
                data.gocTuyChon3 = item.acuteAngle3;
                data.gocTuyChon4 = item.acuteAngle4;
                data.isBacNamGoc1 = (int)item.isCustom1;
                data.isBacNamGoc2 = (int)item.isCustom2;
                data.isBacNamGoc3 = (int)item.isCustom3;
                data.isBacNamGoc4 = (int)item.isCustom4;
                data.khoangCachChia1 = item.distance1;
                data.khoangCachChia2 = item.distance2;
                data.khoangCachChia3 = item.distance3;
                data.khoangCachChia4 = item.distance4;
                //MgdAcDbVNTerrainRectangle mgdAcDbVNTerrainRectangle = new MgdAcDbVNTerrainRectangle();
                //mgdAcDbVNTerrainRectangle.Create("OLTemp" + item.gid.ToString(), item.lat_center, item.long_center, 0, NuaDoDaiCanh, 3, "OLInfo",
                //                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second,
                //                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 60);
                ChiaMatCatCmd cmd = new ChiaMatCatCmd();
                List<CecmProgramAreaLineDTO> lines = cmd.DrawLineJigGocPhanTu2(item, data);
                return lines;
            }
            else if (item.dividerAllGrid == 2)
            {
                ChiaMatCatOLuoiData data = new ChiaMatCatOLuoiData();
                data.gocTuyChon1 = item.acutangeAllGrid;
                data.isBacNamGoc1 = (int)item.isCustomAllGrid;
                data.khoangCachChia1 = item.distanceAllGrid;
                //MgdAcDbVNTerrainRectangle mgdAcDbVNTerrainRectangle = new MgdAcDbVNTerrainRectangle();
                //mgdAcDbVNTerrainRectangle.Create("OLTemp" + item.gid.ToString(), item.lat_center, item.long_center, 0, NuaDoDaiCanh, 3, "OLInfo",
                //                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second,
                //                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 60);
                ChiaMatCatCmd cmd = new ChiaMatCatCmd();
                List<CecmProgramAreaLineDTO> lines = cmd.DrawLineJigAll(item, data);
                return lines;
            }
            else
            {
                return new List<CecmProgramAreaLineDTO>();
            }
        }

        public static List<CecmProgramAreaLineDTO> AutoDividerRanhDoView(OLuoi item, int lineLayer, AxMap axMap1)
        {
            List<CecmProgramAreaLineDTO> lst = new List<CecmProgramAreaLineDTO>();


            List<CecmProgramAreaLineDTO> lines = calculateLines(item);
            DateTime start2 = DateTime.Now;
            int count = 1;
            foreach (CecmProgramAreaLineDTO line in lines)
            {
                CecmProgramAreaLineDTO lineDTO = new CecmProgramAreaLineDTO();
                double start_latt = 0;
                double start_longt = 0;
                AppUtils.ToLatLon(line.start_y, line.start_x, ref start_latt, ref start_longt, "48N");
                lineDTO.start_x = start_latt;
                lineDTO.start_y = start_longt;
                double end_latt = 0;
                double end_longt = 0;
                AppUtils.ToLatLon(line.end_y, line.end_x, ref end_latt, ref end_longt, "48N");
                lineDTO.end_x = end_latt;
                lineDTO.end_y = end_longt;
                lineDTO.cecmprogramareasub_id = item.gid;
                lineDTO.cecmprogramareamap_id = item.cecm_program_areamap_ID;
                lineDTO.cecmprogram_id = item.cecm_program_id;
                lineDTO.code = count.ToString();
                count++;
                axMap1.DrawLineEx(lineLayer, lineDTO.start_y, lineDTO.start_x, lineDTO.end_y, lineDTO.end_x, 1, AppUtils.ColorToUint(Color.White));
                lst.Add(lineDTO);
            }

            //if (item.isCustom1 == 1 || item.isCustom2 == 1 || item.isCustom3 == 1 || item.isCustom4 == 1)
            //{
            //    PointDTO pointDTO1 = new PointDTO();
            //    pointDTO1.xUTM = item.long_corner1;
            //    pointDTO1.yUTM = item.lat_corner1;

            //    PointDTO pointDTO2 = new PointDTO();
            //    pointDTO2.xUTM = item.long_corner2;
            //    pointDTO2.yUTM = item.lat_corner2;

            //    PointDTO pointDTO3 = new PointDTO();
            //    pointDTO3.xUTM = item.long_corner3;
            //    pointDTO3.yUTM = item.lat_corner3;

            //    PointDTO pointDTO4 = new PointDTO();
            //    pointDTO4.xUTM = item.long_corner4;
            //    pointDTO4.yUTM = item.lat_corner4;

            //    PointDTO pointCenterDTO = new PointDTO();
            //    pointCenterDTO.yUTM = ((pointDTO1.yUTM + pointDTO3.yUTM) / 2);
            //    pointCenterDTO.xUTM = ((pointDTO1.xUTM + pointDTO3.xUTM) / 2);

            //    //Điểm giữa góc 1 và 2
            //    PointDTO pointDTO12 = new PointDTO();
            //    pointDTO12.xUTM = (pointDTO1.xUTM + pointDTO2.xUTM) / 2;
            //    pointDTO12.yUTM = (pointDTO1.yUTM + pointDTO2.yUTM) / 2;

            //    //Điểm giữa góc 2 và 3
            //    PointDTO pointDTO23 = new PointDTO();
            //    pointDTO23.xUTM = (pointDTO2.xUTM + pointDTO3.xUTM) / 2;
            //    pointDTO23.yUTM = (pointDTO2.yUTM + pointDTO3.yUTM) / 2;

            //    //Điểm giữa góc 3 và 4
            //    PointDTO pointDTO34 = new PointDTO();
            //    pointDTO34.xUTM = (pointDTO3.xUTM + pointDTO4.xUTM) / 2;
            //    pointDTO34.yUTM = (pointDTO3.yUTM + pointDTO4.yUTM) / 2;

            //    //Điểm giữa góc 4 và 1
            //    PointDTO pointDTO41 = new PointDTO();
            //    pointDTO41.xUTM = (pointDTO4.xUTM + pointDTO1.xUTM) / 2;
            //    pointDTO41.yUTM = (pointDTO4.yUTM + pointDTO1.yUTM) / 2;

            //    if (item.isCustom1 == 1 && item.distance1 != 0d)
            //    {
            //        List<CecmProgramAreaLineDTO> lstLine = CreateLineByAcuteAngle(-1L, -1L, -1L, 1L, pointCenterDTO, pointDTO23, pointDTO3, pointDTO34, item.acuteAngle1, item.distance1);
            //        if (lstLine != null && lstLine.Count > 0)
            //        {
            //            foreach (CecmProgramAreaLineDTO itemLine in lstLine)
            //            {
            //                double start_x = 0;
            //                double start_y = 0;
            //                AppUtils.ToLatLon(itemLine.start_y, itemLine.start_x, ref start_x, ref start_y, "48N");
            //                itemLine.start_x = start_x;
            //                itemLine.start_y = start_y;
            //                double end_x = 0;
            //                double end_y = 0;
            //                AppUtils.ToLatLon(itemLine.end_y, itemLine.end_x, ref end_x, ref end_y, "48N");
            //                itemLine.end_x = end_x;
            //                itemLine.end_y = end_y;
            //                lst.Add(itemLine);
            //            }
            //        }
            //    }
            //}
            return lst;
        }

        private static PointF FindLineIntersection(PointF start1, PointF end1, PointF start2, PointF end2)
        {
            float denom = ((end1.X - start1.X) * (end2.Y - start2.Y)) - ((end1.Y - start1.Y) * (end2.X - start2.X));

            //  AB & CD are parallel 
            if (denom == 0)
                return PointF.Empty;

            float numer = ((start1.Y - start2.Y) * (end2.X - start2.X)) - ((start1.X - start2.X) * (end2.Y - start2.Y));

            float r = numer / denom;

            float numer2 = ((start1.Y - start2.Y) * (end1.X - start1.X)) - ((start1.X - start2.X) * (end1.Y - start1.Y));

            float s = numer2 / denom;

            if ((r < 0 || r > 1) || (s < 0 || s > 1))
            {
                return PointF.Empty;
            }

            // Find intersection point
            PointF result = new PointF();
            result.X = start1.X + (r * (end1.X - start1.X));
            result.Y = start1.Y + (r * (end1.Y - start1.Y));

            return result;
        }

        private static bool PointOnLineSegment(PointF pt1, PointF pt2, PointF pt, double epsilon = 0.001)
        {
            if (pt.X - Math.Max(pt1.X, pt2.X) > epsilon ||
                Math.Min(pt1.X, pt2.X) - pt.X > epsilon ||
                pt.Y - Math.Max(pt1.Y, pt2.Y) > epsilon ||
                Math.Min(pt1.Y, pt2.Y) - pt.Y > epsilon)
                return false;

            if (Math.Abs(pt2.X - pt1.X) < epsilon)
                return Math.Abs(pt1.X - pt.X) < epsilon || Math.Abs(pt2.X - pt.X) < epsilon;
            if (Math.Abs(pt2.Y - pt1.Y) < epsilon)
                return Math.Abs(pt1.Y - pt.Y) < epsilon || Math.Abs(pt2.Y - pt.Y) < epsilon;

            double x = pt1.X + (pt.Y - pt1.Y) * (pt2.X - pt1.X) / (pt2.Y - pt1.Y);
            double y = pt1.Y + (pt.X - pt1.X) * (pt2.Y - pt1.Y) / (pt2.X - pt1.X);

            return Math.Abs(pt.X - x) < epsilon || Math.Abs(pt.Y - y) < epsilon;
        }

        public static List<CecmProgramAreaLineDTO> AutoDividerRanhDoView(OLuoi item)
        {
            List<CecmProgramAreaLineDTO> lst = new List<CecmProgramAreaLineDTO>();

            if (item.isCustom1 == 1 || item.isCustom2 == 1 || item.isCustom3 == 1 || item.isCustom4 == 1)
            {
                PointDTO pointDTO1 = new PointDTO();
                pointDTO1.yLatLong = item.long_corner1;
                pointDTO1.xLatLong = item.lat_corner1;
                double[] p1 = AppUtils.ConverLatLongToUTM(pointDTO1.xLatLong, pointDTO1.yLatLong);
                pointDTO1.xUTM = p1[0];
                pointDTO1.yUTM = p1[1];

                PointDTO pointDTO2 = new PointDTO();
                pointDTO2.yLatLong = item.long_corner2;
                pointDTO2.xLatLong = item.lat_corner2;
                double[] p2 = AppUtils.ConverLatLongToUTM(pointDTO2.xLatLong, pointDTO2.yLatLong);
                pointDTO2.xUTM = p2[0];
                pointDTO2.yUTM = p2[1];

                PointDTO pointDTO3 = new PointDTO();
                pointDTO3.yLatLong = item.long_corner3;
                pointDTO3.xLatLong = item.lat_corner3;
                double[] p3 = AppUtils.ConverLatLongToUTM(pointDTO3.xLatLong, pointDTO3.yLatLong);
                pointDTO3.xUTM = p3[0];
                pointDTO3.yUTM = p3[1];

                PointDTO pointDTO4 = new PointDTO();
                pointDTO4.yLatLong = item.long_corner4;
                pointDTO4.xLatLong = item.lat_corner4;
                double[] p4 = AppUtils.ConverLatLongToUTM(pointDTO4.xLatLong, pointDTO4.yLatLong);
                pointDTO4.xUTM = p4[0];
                pointDTO4.yUTM = p4[1];

                PointDTO pointCenterDTO = new PointDTO();
                pointCenterDTO.yLatLong = item.long_center;
                pointCenterDTO.xLatLong = item.lat_center;
                double[] pCenter = AppUtils.ConverLatLongToUTM(pointCenterDTO.xLatLong, pointCenterDTO.yLatLong);
                pointCenterDTO.xUTM = pCenter[0];
                pointCenterDTO.yUTM = pCenter[1];

                //Điểm giữa góc 1 và 2
                PointDTO pointDTO12 = new PointDTO();
                pointDTO12.xLatLong = pointDTO1.xLatLong;
                pointDTO12.yLatLong = pointCenterDTO.yLatLong;
                pointDTO12.xUTM = pointDTO1.xUTM;
                pointDTO12.yUTM = pointCenterDTO.yUTM;

                //Điểm giữa góc 2 và 3
                PointDTO pointDTO23 = new PointDTO();
                pointDTO23.xLatLong = pointCenterDTO.xLatLong;
                pointDTO23.yLatLong = pointDTO3.yLatLong;
                pointDTO23.xUTM = pointCenterDTO.xUTM;
                pointDTO23.yUTM = pointDTO3.yUTM;

                //Điểm giữa góc 3 và 4
                PointDTO pointDTO34 = new PointDTO();
                pointDTO34.xLatLong = pointDTO4.xLatLong;
                pointDTO34.yLatLong = pointCenterDTO.yLatLong;
                pointDTO34.xUTM = pointDTO4.xUTM;
                pointDTO34.yUTM = pointCenterDTO.yUTM;

                //Điểm giữa góc 4 và 1
                PointDTO pointDTO41 = new PointDTO();
                pointDTO41.xLatLong = pointCenterDTO.xLatLong;
                pointDTO41.yLatLong = pointDTO4.yLatLong;
                pointDTO41.xUTM = pointCenterDTO.xUTM;
                pointDTO41.yUTM = pointDTO4.yUTM;

                if (item.dividerAllGrid == 1)
                {
                    if (item.isCustom1 == 1 && item.distance1 != 0)
                    {
                        List<CecmProgramAreaLineDTO> lstLine = CreateLineByAcuteAngle(-1L, -1L, -1L, 1L, pointCenterDTO, pointDTO23, pointDTO3, pointDTO34, item.acuteAngle1, item.distance1);
                        if (lstLine != null && lstLine.Count > 0)
                        {
                            foreach (CecmProgramAreaLineDTO itemLine in lstLine)
                            {
                                double latt_start = 0, longt_start = 0;
                                AppUtils.ToLatLon(itemLine.start_x, itemLine.start_y, ref latt_start, ref longt_start, "48N");
                                itemLine.start_x = longt_start;
                                itemLine.start_y = latt_start;
                                double latt_end = 0, longt_end = 0;
                                AppUtils.ToLatLon(itemLine.end_x, itemLine.end_y, ref latt_end, ref longt_end, "48N");
                                itemLine.end_x = longt_end;
                                itemLine.end_y = latt_end;
                                lst.Add(itemLine);
                            }
                        }
                    }
                    if (item.isCustom2 == 1 && item.distance2 != 0)
                    {
                        List<CecmProgramAreaLineDTO> lstLine = CreateLineByAcuteAngle(-2L, -2L, -2L, 2L, pointCenterDTO, pointDTO23, pointDTO3, pointDTO34, item.acuteAngle2, item.distance2);
                        if (lstLine != null && lstLine.Count > 0)
                        {
                            foreach (CecmProgramAreaLineDTO itemLine in lstLine)
                            {
                                double latt_start = 0, longt_start = 0;
                                AppUtils.ToLatLon(itemLine.start_x, itemLine.start_y, ref latt_start, ref longt_start, "48N");
                                itemLine.start_x = longt_start;
                                itemLine.start_y = latt_start;
                                double latt_end = 0, longt_end = 0;
                                AppUtils.ToLatLon(itemLine.end_x, itemLine.end_y, ref latt_end, ref longt_end, "48N");
                                itemLine.end_x = longt_end;
                                itemLine.end_y = latt_end;
                                lst.Add(itemLine);
                            }
                        }
                    }
                    if (item.isCustom3 == 1 && item.distance3 != 0)
                    {
                        List<CecmProgramAreaLineDTO> lstLine = CreateLineByAcuteAngle(-3L, -3L, -3L, 3L, pointCenterDTO, pointDTO23, pointDTO3, pointDTO34, item.acuteAngle3, item.distance3);
                        if (lstLine != null && lstLine.Count > 0)
                        {
                            foreach (CecmProgramAreaLineDTO itemLine in lstLine)
                            {
                                double latt_start = 0, longt_start = 0;
                                AppUtils.ToLatLon(itemLine.start_x, itemLine.start_y, ref latt_start, ref longt_start, "48N");
                                itemLine.start_x = longt_start;
                                itemLine.start_y = latt_start;
                                double latt_end = 0, longt_end = 0;
                                AppUtils.ToLatLon(itemLine.end_x, itemLine.end_y, ref latt_end, ref longt_end, "48N");
                                itemLine.end_x = longt_end;
                                itemLine.end_y = latt_end;
                                lst.Add(itemLine);
                            }
                        }
                    }
                    if (item.isCustom4 == 1 && item.distance4 != 0)
                    {
                        List<CecmProgramAreaLineDTO> lstLine = CreateLineByAcuteAngle(-4L, -4L, -4L, 4L, pointCenterDTO, pointDTO23, pointDTO3, pointDTO34, item.acuteAngle4, item.distance4);
                        if (lstLine != null && lstLine.Count > 0)
                        {
                            foreach (CecmProgramAreaLineDTO itemLine in lstLine)
                            {
                                double latt_start = 0, longt_start = 0;
                                AppUtils.ToLatLon(itemLine.start_x, itemLine.start_y, ref latt_start, ref longt_start, "48N");
                                itemLine.start_x = longt_start;
                                itemLine.start_y = latt_start;
                                double latt_end = 0, longt_end = 0;
                                AppUtils.ToLatLon(itemLine.end_x, itemLine.end_y, ref latt_end, ref longt_end, "48N");
                                itemLine.end_x = longt_end;
                                itemLine.end_y = latt_end;
                                lst.Add(itemLine);
                            }
                        }
                    }
                }
                else if (item.dividerAllGrid == 2L)
                {
                    if (item.isCustomAllGrid == 1 && item.distanceAllGrid != 0)
                    {
                        List<CecmProgramAreaLineDTO> lstLine = CreateLineByAcuteAngle(-1L, -1L, -1L, 4L, pointDTO1, pointDTO2, pointDTO3, pointDTO4, item.acutangeAllGrid, item.distanceAllGrid);
                        if (lstLine != null && lstLine.Count > 0)
                        {
                            foreach (CecmProgramAreaLineDTO itemLine in lstLine)
                            {
                                double latt_start = 0, longt_start = 0;
                                AppUtils.ToLatLon(itemLine.start_x, itemLine.start_y, ref latt_start, ref longt_start, "48N");
                                itemLine.start_x = longt_start;
                                itemLine.start_y = latt_start;
                                double latt_end = 0, longt_end = 0;
                                AppUtils.ToLatLon(itemLine.end_x, itemLine.end_y, ref latt_end, ref longt_end, "48N");
                                itemLine.end_x = longt_end;
                                itemLine.end_y = latt_end;
                                lst.Add(itemLine);
                            }
                        }
                    }
                }
            }

            return lst;
        }
    }
}
