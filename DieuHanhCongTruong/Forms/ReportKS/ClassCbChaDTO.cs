using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin
{
    public class ClassCbChaDTO
    {
        public List<CbChaDTO> CbChaDTO;
    }

    public class CbChaDTO
    {
        public string code;
        public long commune_id;//id xã
        public long district_id;//id huyện
        public long province_id;//id tỉnh
        public string commune_code;//mã xã
        public string district_code;//mã huyện
        public string province_code;//mã tỉnh
        public string commune_idST;//id xã
        public string district_idST;//id huyện
        public string province_idST;//id tỉnh
        public string village;
        public string cecm_program_code; // code dự án
        public string cecm_program_idST; // tên dự án
        public string date_createST;

        public long component_id; // hợp phần
        public long cecm_program_packageid;//goi thau
        public long cecm_plan_program_id;//ke hoach

        //public long cecm_program_area_id;//
        public string cecm_program_area_code; // code vùng dự án

        public string cecm_program_area_idST; // tên vùng dự án

        public string codeCHA; //mã cha
        public long deptid; // dơn vị id
        public string headerName; //đội trường
        public string mission_code; //mã nhiệm vụ
        public double length;//dài
        public double width; //rômg
        public double surface;// tích

        public long status;// id trạng thái
        public long priority;// id độ ưu tiên
        public string date_startST; //ngày bđ
        public string date_endST;//ngày kt
        public long purpose;//id mục đihcs sd đất
        public string purposeOther; //"purposeOther": // mục đích khác
        public long person_benefit; //"": //id doois tượng hưởng lợi
        public string lst_plantType;    //"1,2,5" //loại thực vật, cho các giá trị vào text cho dễ
        public string lst_plantLevel;   //"": "1,2,5" // độ phủ thực vật
        public string lst_plantDevice;  //"": "1,2,5" //Phương tiện phát quang thảm thực vật
        public string lst_soilType; //"lst_soilType": "1,2,5" //loại đất
        public string lst_geologicalType;   //"": "1,2,5" //loại hình địa chất
        public string lst_areaType;    //"": "1,2,5" //loại hình khu vực
        public string lst_areaDevice;   //"": "1,2,5" //loại xe tiếp cận đc khu vực
        public string lst_topographic;//"": "1,2,5" //địa hình
        public string lst_month;    //"": "1,2,5" //tháng ko thể tiế cận

        public double latValue;//"": //vĩ độ
        public double longValue;  //"": //kinh độ
        public string polygonGeomST;    //"": "text" //Tọa độ vùng bao Lat Long
        public long meridian;   //"": //kinh tuyen truc
        public long pZone;	//"": //id tỷ lệ bản đồ

        public string note;
        public string reason;

        public string staff_idST;
        public string staff_other;

        public string group_idST;
        public string group_other;

        public string captain_idST;
        public string captain_other;

        public string supervisor_idST;
        public string supervisor_other;
        
        public string head_idST;
        public string head_other;

        public string dept_code;
        public long dept_id;

        public long percentage;

        public List<lstChaBombDTO> lstChaBombDTO;  // 3. Bom mìn, vật nổ tìm thấy
    }

    public class lstChaBombDTO
    {
        public String codeCha; //mã cha
        public long bombType; //id loại bom
        public String codeO; //Ô
        public long weight;//So luongư
        public long statusBomb;//id tình trạng
        public string theNote;//ghi chú
        public double Kinhdo;
        public double Vido;
        public string Kyhieu;
        public double Deep;
        public double Kichthuoc;
        public long PPXuLy;
        public string PPXuLyST;
    }
}