using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	public class classRPBM4
	{
		public int gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public int cecm_program_id;       //Dự án
		public String cecm_program_code;
		public String cecm_program_idST;
		public String address_cecm;        //Địa điểm dự án
		public int category;
		public string categoryST;
		public String files_1;     //File nghị định
		public String thong_tu;        //Số thông tư
		public String base_qckt;       //Quy chuẩn kỹ thuật
		public String base_hdkt_number;        //Căn cứ hợp đồng kinh tế số
		public DateTime dates_hdkt;        //Ngày hợp đồng kinh tế
		public String dates_hdktST;
		public String organization_signed;     //Hợp đồng ký kết giữa
		public Double deep_signal;     //Mật độ tín hiệu độ sâu
		public String files_2;     //File yêu cầu thực hiện
		public String num_team_construct;      //Số lượng đội thi công khảo sát
		public String num_mem_all;     //Tổng quân số
		public String num_mem_1;       //Số lượng phụ trách phân đội
		public String num_mem_2;       //Kỹ thuật viên
		public String num_mem_3;       //Nhân viên y tế
		public int captain_id;        //Chỉ huy
		public String captain_idST;
		public String captain_id_other;        //Khác
		public DateTime time_nt_from;      //Thời gian thi công từ
		public String time_nt_fromST;
		public DateTime time_nt_to;        //Thời gian thi công đến
		public String time_nt_toST;
		public Double area_rpbm;       //Diện tích RPBM
		public String area_tcks;       //Diện tích thi công khảo sát
		public String num_tcks;        //Số điểm thi công
		public String area_ks;     //Diện tích khảo sát
		public String type_forest_1;       //Loại rừng được phát dọn
		public String area_phatdon;        //Diện tích được phát dọn
		public String area_ks_1;       //Diện tích khảo sát đến độ sâu 0,3 m
		public String signal_process;      //Xử lý tín hiệu đến độ sâu 0,3 m
		public String area_ks_2;       //Diện tích khảo sát rà phá bom mìn vật nổ đến độ sâu 5 m
		public String dig_lane_signal_1;       //Đào, xử lý tín hiệu độ sâu 0,3 ¸ 3 m
		public String dig_lane_signal_2;       //Đào, xử lý tín hiệu độ sâu 5 m
		public String result;      //Kết quả thu được
		public String ratio_clean_ground;      //Tỷ lệ phát dọn mặt bằng thi công tương đương rừng
		public String type_forest_2;       //Loại rừng được phát dọn
		public String type_signal_density_1;       //Loại mật độ tín hiệu
		public String avg_signal_density_1;        //Trung bình mật độ tín hiệu trên cạn đến độ sâu 0,3 m (hoặc 0,5 m, 1 m)
		public String avg_signal_density_2;        //Mật độ tín hiệu trên cạn đến độ sâu 3 m trung bình
		public String avg_signal_density_3;        //Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình
		public String type_land_1;     //Đất cấp tại khu vực thi công ở độ sâu 0,3 m
		public String type_land_2;     //Đất cấp tại khu vực thi công ở độ sâu lớn hơn 0,3m đến 5 m
		public String topo;        //Địa hình
		public String type_land_3;     //Đất cấp tại khu vực thi công ở độ sâu 0,3 m
		public String type_land_4;     //Đất cấp tại khu vực thi công ở độ sâu lớn hơn 0,3m đến 5 m
		public String climate;     //Khí hậu, thủy văn
		public String situation_bomb;      //Tình hình bom mìn vật nổ
		public String infor_other;     //Thông tin liên quan
		public String area_affect;     //Khu vực có bị ảnh hưởng BMVN
		public int deptid_tcks;       //Đơn vị thi công khảo sát
		public String deptid_tcksST;
		public String conclusion;      //Kết luận
		public int survey_id;     //Đại diện đơn vị khảo sát
		public String survey_idST;
		public String survey_id_other;     //Khác
		public String files;       //File đính kèm
		public String situation_dancu;       //File đính kèm

	}
}