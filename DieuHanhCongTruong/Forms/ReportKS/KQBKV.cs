using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin
{
    public class ResultReportArea
    {
        public List<ResultReportAreaDTO> ResultReportAreaDTO;
    }

    public class ResultReportAreaDTO
    {
        public long commune_id;//id xã
        public long district_id;//id huyện
        public long province_id;//id tỉ
        public string commune_code;//code xã
        public string district_code;//code huyện
        public string province_code;//code tỉnh
        public string communeidST;//id xã
        public string districtidST;//id huyện
        public string provinceidST;//id tỉnh
        public string Thon;
        public string cecm_program_code; // code dự án
        public string cecm_program_idST; // tên dự án
        public int package_id;
        public string misson_code; //mã nhiệm vụ
        public string start_date;
        public string end_date;
        public long area_square; //Diện tích kv
        public int evidence_source;  //Nguồn bằng chứng khảo sát
        public int proof_count;  //so bang chung
        public int mission_status; //ttrang
        public long result_doubt_survey;//so nghi ngo
        public long result_survey_square;//Diện tích đã thực hiện khảo sát
        public long result_survey_diff;//Diện tích khảo sát khác với yêu cầu
        public long result_square_tested; //Diện tích đã kiểm tra chất lượng
        public long result_square_remake; //DT làm lại
        public int result_red; //So o do
        public int result_signal_survey;//tin hieu
        public int result_yellow; //So o vang
        //public int result_blue;//xanh troi
        //public int result_white; //So o trang
        public int result_gray;//xam
        //public int result_greeen; //xanh la
        public int result_color;//tong o
        public int result_count_color_cbma; //KD o nhiem
        public int result_count_cbma;//so o trong kv nhiem
        public long result_square_cha;//dt nhiem
        public int polluted_level; // mucdo
        public int progress;//tien do
        public string dept_idST;//"Tên đội tổ số",
        public string captain_idST;//"Tên doi trưởng",
        public string GiamSat_idST;
        public string GiamSat;
        public string ChiHuyCT_idST;
        public string site_commander;//"Chỉ huy công trường",
        public int countDate;
        public int area_map_id;
        public string MaBaocaongay;
        public string Ngaytao;
        public string DVKScode;
        public int result_brown;//nâu
        public double DTKhongONhiem; //DT không ô nhiễm
        public string NguoiVe; //Người vẽ
        public string TGVe; //TG vẽ
        public string cecm_program_area_code;
        public long result_not_polluted;    //Số ô không ô nhiễm

        public List<result_report_lstSubTable> result_report_lstSubTable;  // 2. Bom mìn, vật nổ tìm thấy
    }

    public class result_report_lstSubTable
    {
        public long long1; //id loại bom
        public string string1; //Ký hiệu
        public string string2; //Ô
        public Double double1; //kihcs thước
        public Double double2; //dộ sâu
        public Double double3;//kinh do
        public Double double4;//vi do
        public int long3; //tinh trang
        public long long4;//ppxl
    }
}