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
    }
}
