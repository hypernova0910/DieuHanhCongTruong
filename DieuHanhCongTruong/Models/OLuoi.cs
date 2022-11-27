using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Models
{
    public class OLuoi
    {
        public long gid { get; set; }
        public long cecm_program_areamap_ID { get; set; }

        public long cecm_program_id { get; set; }
        public long khaosat_deptId { get; set; }
        public double lat_center { get; set; }
        public double lat_corner1 { get; set; }
        public double lat_corner2 { get; set; }
        public double lat_corner3 { get; set; }
        public double lat_corner4 { get; set; }
        public double long_center { get; set; }
        public double long_corner1 { get; set; }
        public double long_corner2 { get; set; }
        public double long_corner3 { get; set; }
        public double long_corner4 { get; set; }
        public string o_id { get; set; }
        public long raPha_deptId { get; set; }

        public string khaosat_deptIdST { get; set; }
        public string raPha_deptIdST { get; set; }

        public long isCustom1 { get; set; }
        public long isCustom2 { get; set; }
        public long isCustom3 { get; set; }
        public long isCustom4 { get; set; }
        public double acuteAngle1 { get; set; }
        public double acuteAngle2 { get; set; }
        public double acuteAngle3 { get; set; }
        public double acuteAngle4 { get; set; }

        public double distance1 { get; set; }
        public double distance2 { get; set; }
        public double distance3 { get; set; }
        public double distance4 { get; set; }

        public long dividerAllGrid { get; set; }//1~Chia theo góc phần tư, 2~ chia cả ô lưới
        public long isCustomAllGrid { get; set; }//Chọn chia theo bắc-nam, đông tây, góc lệch
        public double distanceAllGrid { get; set; }//Khoảng cách rãnh dò khi chia toàn ô lưới
        public double acutangeAllGrid { get; set; }//Góc lệch khi chia toàn ô lưới

        public int autoDivide { get; set; }//Kiểu tự động chia

        public List<CecmProgramAreaLineDTO> lstRanhDo { get; set; }

        public OLuoi Copy()
        {
            OLuoi oLuoiNew = new OLuoi();
            oLuoiNew.gid = gid;
            oLuoiNew.cecm_program_areamap_ID = cecm_program_areamap_ID;
            oLuoiNew.cecm_program_id = cecm_program_id;
            oLuoiNew.khaosat_deptId = khaosat_deptId;
            oLuoiNew.lat_center = lat_center;
            oLuoiNew.lat_corner1 = lat_corner1;
            oLuoiNew.lat_corner2 = lat_corner2;
            oLuoiNew.lat_corner3 = lat_corner3;
            oLuoiNew.lat_corner4 = lat_corner4;
            oLuoiNew.long_center = long_center;
            oLuoiNew.long_corner1 = long_corner1;
            oLuoiNew.long_corner2 = long_corner2;
            oLuoiNew.long_corner3 = long_corner3;
            oLuoiNew.long_corner4 = long_corner4;
            oLuoiNew.o_id = o_id;
            oLuoiNew.raPha_deptId = raPha_deptId;
            oLuoiNew.khaosat_deptIdST = khaosat_deptIdST;
            oLuoiNew.raPha_deptIdST = raPha_deptIdST;
            oLuoiNew.isCustom1 = isCustom1;
            oLuoiNew.isCustom2 = isCustom2;
            oLuoiNew.isCustom3 = isCustom3;
            oLuoiNew.isCustom4 = isCustom4;
            oLuoiNew.acuteAngle1 = acuteAngle1;
            oLuoiNew.acuteAngle2 = acuteAngle2;
            oLuoiNew.acuteAngle3 = acuteAngle3;
            oLuoiNew.acuteAngle4 = acuteAngle4;

            oLuoiNew.distance1 = distance1;
            oLuoiNew.distance2 = distance2;
            oLuoiNew.distance3 = distance3;
            oLuoiNew.distance4 = distance4;

            oLuoiNew.dividerAllGrid = dividerAllGrid;//1~Chia theo góc phần tư, 2~ chia cả ô lưới
            oLuoiNew.isCustomAllGrid = isCustomAllGrid;//Chọn chia theo bắc-nam, đông tây, góc lệch
            oLuoiNew.distanceAllGrid = distanceAllGrid;//Khoảng cách rãnh dò khi chia toàn ô lưới
            oLuoiNew.acutangeAllGrid = acutangeAllGrid;//Góc lệch khi chia toàn ô lưới

            oLuoiNew.autoDivide = autoDivide;//Kiểu tự động chia
            List<CecmProgramAreaLineDTO> lstRanhDoNew = new List<CecmProgramAreaLineDTO>();
            foreach(CecmProgramAreaLineDTO line in lstRanhDo)
            {
                lstRanhDoNew.Add(line);
            }
            oLuoiNew.lstRanhDo = lstRanhDoNew;
            return oLuoiNew;
        }
    }
}
