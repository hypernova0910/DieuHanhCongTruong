using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin
{
    public class ClassBCTHKQ
    {
        public List<ClassBCTHKQDTO> ClassBCTHKQDTO;
    }

    public class ClassBCTHKQDTO
    {
        public string Mabaocao;
        public string DVKScode;
        public long commune_id;//id xã
        public long district_id;//id huyện
        public long province_id;//id tỉnh
        public string commune_code;//id xã
        public string district_code;//id huyện
        public string province_code;//id tỉnh
        public string communeidST;//id xã
        public string districtidST;//id huyện
        public string provinceidST;//id tỉnh
        public string Thon;
        public string cecm_program_code; // code dự án
        public string cecm_program_idST; // tên dự án
        public long component_id; //id hợp phần

        public string topoST; //Địa hình
        public string soilST; // Loại đất 
        public string weatherST; // Thời tiết
        public string pollutionST;// Hiện trạng ô nhiễm bom mìn
        public string accidentST; // Lịch sử tai nạn bom mìn
        public string historyST; // Lịch sử hoạt động khắc phục bom mìn 

        public double totalArea1; //Tổng diện tích đã điều tra 
        public double totalArea2; //Tổng diện tích nghi ngờ ô nhiễm sau điều tra
        public lstHientrang lstSD;
        public lstHientrang lstNN;
        public lstHientrang lstKON;

        public List<lstBMVN> lstMine;// {Trên mặt, Đến 0,3m, Đến 3 m, Đến 5 m, Tổng cộng}
        public lstBMVN_note lstMineNote; // Ghi chú {Trên mặt, Đến 0,3m, Đến 3 m, Đến 5 m, Tổng cộng}

        public double DTKS; // DTKS
        public double TH_tong; // Tổng tín hiệu
        public double TH_1; // tín hiệu bom 
        public double TH_2; // tín hiệu
        public double MD_tong; // Tổng mật độ
        public double MD_1; // mật độ bom 
        public double MD_2; // mật độ

        public double TH_chuaXL; // tín hiệu chưa xl
        public string Mucdo; // Mức độ
        public string Phanloai; // Phân loại
        public string Capdat; // Cấp đất

        public double DT_tong; //Diện tích cần rà phá bom mìn
        public lstDT DTRP; // Diện tích rà phá

        public long Chihuy_id; // Chỉ huy 
        public string Chihuy; // Chỉ huy 
        public string Chihuy_other; // Chỉ huy 
        public long Doitruong_id; // Đội trường
        public string Doitruong; // Đội trường
        public string Doitruong_other; // Đội trường

        public long Giamsat_id; // Giám sát id
        public string Giamsat;
        public string Giamsat_other; // Giám sát khác

    }
    public class lstHientrang
    {
        public double area1; //Đất thổ cư 
        public double area2; //Đất trồng trọt
        public double area3; //Đất trồng cây lâu năm
        public double area4; //Mặt nước
        public double area5; //Đất rừng tự nhiên 
        public double area6; //Đất xây dựng
        public double area7; //Đất giao thông
        public double area8; //Đất thuỷ lợi 
        public double area9; //Đất nghĩa địa
        public double area10; //Các loại đất khác
    }
    public class lstBMVN 
    {
        public double mine1; //Bom phá các loại
        public double mine2; // Đạn pháo, đạn cối
        public double mine3; //Tên lửa, rốc két
        public double mine4; //Lựu đạn các loại
        public double mine5; //Bom bi, đạn M79
        public double mine6; //Mìn chống tăng
        public double mine7; //Mìn chống người
        public double mine8; //Các loại vật nổ khác
        public double mine9; //Sắt thép các loại
    }
    public class lstBMVN_note
    {
        public string mine1_note; //Bom phá các loại
        public string mine2_note; // Đạn pháo, đạn cối
        public string mine3_note; //Tên lửa, rốc két
        public string mine4_note; //Lựu đạn các loại
        public string mine5_note; //Bom bi, đạn M79
        public string mine6_note; //Mìn chống tăng
        public string mine7_note; //Mìn chống người
        public string mine8_note; //Các loại vật nổ khác
        public string mine9_note; //Sắt thép các loại
    }
    public class lstDT
    {
        public double DT1; //Đến độ sâu 0,3m
        public double DT2; //Đến độ sâu 5m
        public double DT3; //Đến độ sâu 3m
        public double DT4; //Đến độ sâu 1,5m
        public double DT5; //Diện tích mặt nước
        public double DT6; //Diện tích biển
        public double DT7; //≤ 15m
        public double DT8; //từ 15-30m
        public double DT9; //trên 30m
        public double DT10; //Diện tích phát quang phục vụ RPBM   
    }
}