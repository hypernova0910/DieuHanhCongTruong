using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin
{
    public class ReportSurveyDaily
    {
        public List<ReportSurveyDailyDTO> ReportSurveyDailyDTO;
    }

    public class ReportSurveyDailyDTO
    {
        public string commune_code;//code xã
        public string district_code;//code huyện
        public string province_code;//code tỉnh
        public long communeid;//id xã
        public long districtid;//id huyện
        public long provinceid;//id tỉnh
        public string communeidST;//id xã
        public string districtidST;//id huyện
        public string provinceidST;//id tỉnh
        public string cecm_program_code; // code dự án
        public string cecm_program_idST; // tên dự án
        public String datesST;
        public String taskcode; //mã nhiệm vụ
        public long component_id; //id hợp phần
        public string dept_idST;  //tên Tổ điều tra, Đội điều tra.
        public String team_number;  //đội ngoài
        public String dailycode; //mã báo cáo gàng ngày
        public String date_startST;
        public String date_endST;
        public String date_endtmpST;
        public string master_idST; //tên đội trưởng
        public String master_name; //doi truong ngoai
        public long total_team; //quân số
        public String reason; // ;lý do dừng việc
        public string DVKScode; //Mã DVKS
        public List<lst_resultSurvey> lst_resultSurvey;  // 2. Kết quả khảo sát
        public List<lst_bomb> lst_bomb;  // 3. Bom mìn, vật nổ tìm thấy
        public double DTNNON;
        public double DTON;
        public int TinHieuTong;
        public int TinHieuBMVN;
        public int TinHieuKhac;
        public double MatDoTHTong;
        public double MatDoTHBMVN;
        public double MatDoTHKhac;
        public int Oxanhla;
        public int Odo;
        public int Ovang;
        public int Onau;
        public int Oxam;
        public int TongO;
        public int TongoDs;
        public int TongoKs;
        public double DientichKS;
        public double DientichKTLai;
        public string Thon;
        public long GiamSat_id;
        public string GiamSat_idST;
        public string GiamSat_other;
        public long ChiHuyCT_id;
        public string ChiHuyCT_idST;
        public string ChiHuyCT_other;
    }

    public class lst_resultSurvey
    {
        public String st_ref1; //Mã ô
        public long id_ref1; //id kết quả
        public long id_ref2;//dộ mật

        //public string id_ref1; //id kết quả
        //public string id_ref2;//dộ mật
        public Double double_column1; //đã xử lý

        public Double double_column2; //chưa khảo sat
        public String st_ref2; //Ghi chú về diện tích chưa khảo sát
        public Double double_column3;//dt cay bụi
        public Double double_column4; //dt tre trúc
        public Double double_column5;//dt cây to
        public Double double_column6;//mật độ thưa
        public Double double_column7; //mat do trung binh
        public Double double_column8; //md dày
        public Double double_column9; //so hiuej
        public int int_column1; //số lượng
        public string st_ref3;  //mã vùng dự án
    }

    public class lst_bomb
    {
        public String st_ref1; //Ký hiệu
        public long id_ref1; //id loại bom

        //public string id_ref1;
        public String st_ref2; //Ô

        public long id_ref2;//So luong
        public Double double_column1; //kihcs thước dài

        public Double double_column2;//kinh do
        public Double double_column3;//vi do
        public Double double_column4; //dộ sâu
        public Double double_column5; //kích thước rộng

        public String st_ref3; //tinh trang
        public string st_ref4; //pp xử lý

        public long id_ref8; //tình trạng
        public long id_ref9; //PP xử lý
    }
}