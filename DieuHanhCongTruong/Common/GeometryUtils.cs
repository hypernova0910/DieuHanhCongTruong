using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Common
{
    class GeometryUtils
    {
        //public static double minDiff = double.MaxValue;

        public static bool isInside(double x1, double y1, double x2,
                         double y2, double x3, double y3,
                         double x, double y)
        {
            /* Calculate area of triangle ABC */
            double A = area(x1, y1, x2, y2, x3, y3);

            /* Calculate area of triangle PBC */
            double A1 = area(x, y, x2, y2, x3, y3);

            /* Calculate area of triangle PAC */
            double A2 = area(x1, y1, x, y, x3, y3);

            /* Calculate area of triangle PAB */
            double A3 = area(x1, y1, x2, y2, x, y);

            /* Check if sum of A1, A2 and A3 is same as A */
            double diff = Math.Abs(A - (A1 + A2 + A3));
            //if (diff < minDiff)
            //{
            //    minDiff = diff;
            //}
            return diff < 1E-6;
        }

        private static double area(double x1, double y1, double x2,
                       double y2, double x3, double y3)
        {
            return Math.Abs((x1 * (y2 - y3) +
                             x2 * (y3 - y1) +
                             x3 * (y1 - y2)) / 2.0);
        }

        public static InfoConnect GiaoDuongThangMatPhang(InfoConnect M0, InfoConnect N0, double vtpt_x, double vtpt_y, double vtpt_z, double heSoTuDo, bool isLineSegment)
        {
            if (isLineSegment)
            {
                //Check đoạn thẳng có cắt mặt phẳng không
                if ((vtpt_x * M0.lat_value + vtpt_y * M0.long_value + vtpt_z * M0.the_value - heSoTuDo) * (vtpt_x * N0.lat_value + vtpt_y * N0.long_value + vtpt_z * N0.the_value - heSoTuDo) > 0)
                {
                    return null;
                }
            }
            //Phương trình đường thẳng đi qua điểm M(x0, y0, z0) và nhận vectơ u(a,b,c) làm vector chỉ phương
            //x = x0 + at
            //y = y0 + bt
            //z = z0 + ct
            //Với x0 = M0.Position[0], y0 = M0.Position[1], z0 = M0.Position[2]
            double x0 = M0.lat_value;
            double y0 = M0.long_value;
            double z0 = M0.the_value;
            double a = N0.lat_value - M0.lat_value;
            double b = N0.long_value - M0.long_value;
            double c = N0.the_value - M0.the_value;
            //Mặt phẳng có phương trình vtpt_x * x + vtpt_y * y + vtpt_z * z = heSoTuDo
            //=> Tìm t từ phương trình vtpt_x * (x0 + at) + vtpt_y * (y0 + bt) + vtpt_z * (z0 + ct) = heSoTuDo
            //<=> vtpt_x * at + vtpt_y * bt + vtpt_z * ct = heSoTuDo - vtpt_x * x0 - vtpt_y * y0 - vtpt_z * z0
            //<=> t = (heSoTuDo - vtpt_x * x0 - vtpt_y * y0 - vtpt_z * z0) / (vtpt_x * a + vtpt_y * b + vtpt_z * c)
            if (vtpt_x * a + vtpt_y * b + vtpt_z * c == 0)
            {
                return null;
            }
            double t = (heSoTuDo - vtpt_x * x0 - vtpt_y * y0 - vtpt_z * z0) / (vtpt_x * a + vtpt_y * b + vtpt_z * c);
            double x = x0 + a * t;
            double y = y0 + b * t;
            double z = z0 + c * t;
            InfoConnect infoConnect = new InfoConnect();
            infoConnect.lat_value = x;
            infoConnect.long_value = y;
            infoConnect.the_value = z;
            return infoConnect;
        }

        public static InfoConnect Giao2DuongThang2D(double A1, double B1, double C1, double A2, double B2, double C2)
        {
            double delta = A1 * B2 - A2 * B1;

            if (delta == 0)
                throw new ArgumentException("Lines are parallel");

            double x = (B2 * C1 - B1 * C2) / delta;
            double y = (A1 * C2 - A2 * C1) / delta;
            return new InfoConnect(x, y);
        }

        public static double GetMagneticAtPoint(double x, double y, List<CustomFace> triangles)
        {
            InfoConnect start = new InfoConnect(x, y, 0);
            InfoConnect end = new InfoConnect(x, y, 100);
            foreach (CustomFace cell in triangles)
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
