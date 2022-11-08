using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
    public class classRPBM3
    {
		public int gid;       //Khóa tự sinh
		public string symbol;      //Ký hiệu số
		public string address;     //Địa chỉ
		public string dates;     //Ngày
		public string datesST;
		public int cecm_program_id;       //Dự án
		public string cecm_program_code;
		public string cecm_program_idST;
		public string address_cecm;        //Địa điểm dự án
		public string base_qtkt;       //Căn cứ quy trình kỹ thuật
		public string dates_qtkt;        //Ngày quy trình kỹ thuật
		public string dates_qtktST;
		public string base_qcqg;       //Quy chuẩn kỹ thuật quốc gia
		public string base_hdkt_number;        //Căn cứ hợp đồng kinh tế số
		public string dates_hdkt;        //Ngày hợp đồng kinh tế
		public string dates_hdktST;
		public string organization_signed;     //Hợp đồng ký kết giữa
		public string dates_now;     //Ngày hiện tại
		public string dates_nowST;
		public string time_nt_from;      //Thời gian nghiệm thu từ
		public string time_nt_fromST;
		public string time_nt_to;        //Thời gian nghiệm thu đến
		public string time_nt_toST;

		public string quality_ks;      //Chất lượng công tác khảo sát
		public string files_1;     //File chất lượng công tác khảo sát
		public string quymo_ks;        //Quy mô khảo sát
		public string files_2;     //File quy mô khảo sát
		public string phamvi_dtich_ks;     //Phạm vi, diện tích thi công khảo sát
		public string files_3;     //File phạm vi, diện tích thi công khảo sát
		public Double area_rpbm;       //Diện tích RPBM
		public string area_tcks;       //Diện tích thi công khảo sát
		public string area_phatdon;        //Diện tích phát dọn
		public string matdo_phatdon;       //Mật độ loại đất sau khảo sát đến độ sâu 0,3 m (hoặc 0,5 m)
		public string area_ks_1;       //Diện tích khảo sát đến độ sâu 0,3 m (hoặc 0,5 m)
		public string signal_process;      //Xử lý tín hiệu đến độ sâu 0,3 m (hoặc 0,5 m, 1 m)
		public string area_ks_2;       //Diện tích khảo sát rà phá bom mìn vật nổ đến độ sâu 5 m
		public string dig_lane_signal_1;       //Đào, xử lý tín hiệu độ sâu 0,3 ¸ 3 m
		public string dig_lane_signal_2;       //Đào, xử lý tín hiệu độ sâu 5 m
		public string result;      //Kết quả thu được
		public string ratio_clean_ground;      //Tỷ lệ phát dọn mặt bằng thi công tương đương rừng
		public string type_forest;     //Loại rừng được phát dọn
		public string type_signal_density_1;       //Loại mật độ tín hiệu
		public string avg_signal_density_1;        //Trung bình mật độ tín hiệu trên cạn đến độ sâu 0,3 m (hoặc 0,5 m, 1 m)
		public string avg_signal_density_2;        //Mật độ tín hiệu trên cạn đến độ sâu 3 m trung bình
		public string avg_signal_density_3;        //Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình
		public string type_land_1;     //Đất cấp tại khu vực thi công ở độ sâu 0,3 m
		public string type_land_2;     //Đất cấp tại khu vực thi công ở độ sâu lớn hơn 0,3m đến 5 m
		public string ratio_clean_water;       //Tỷ lệ phát dọn mặt bằng thi công tương đương rừng
		public string type_forest_water;       //Loại rừng được phát dọn
		public string type_signal_density_2;       //Loại mật độ tín hiệu
		public string avg_signal_density_4;        //Trung bình mật độ tín hiệu trên cạn đến độ sâu 0,3 m (hoặc 0,5 m, 1 m)
		public string avg_signal_density_5;        //Mật độ tín hiệu trên cạn đến độ sâu 3 m trung bình
		public string avg_signal_density_6;        //Mật độ tín hiệu trên cạn đến độ sâu 5 m trung bình
		public string eval_water_flow;     //Đánh giá về lưu tốc nước chảy
		public string deep_water;      //Độ sâu nước
		public string limit_area_rpbm;     //Phạm vi khu vực cần RPBM
		public string signal_sea_1;        //Mật độ tín hiệu trên bề mặt đáy biển
		public string signal_sea_2;        //Mật độ tín hiệu từ bề mặt đáy biển đến độ sâu 1 m
		public string deep_sea;      //Độ sâu nước biển
		public string deep_seaST;
		public string files_4;     //File kết quả khảo sát
		public string conclusion;      //Kết luận
		public Double num_all_report;      //Số biên bản được lập
		public Double num_cdt_report;      //Số biên bản chủ đầu tư giữ
		public Double num_ks_report;       //Số biên bản đơn vị khảo sát giữ
		public int boss_id;       //Đại diện chủ đầu tư
		public string boss_idST;
		public string boss_id_other;       //Khác
		public int survey_id;     //Đại diện đơn vị khảo sát
		public string survey_idST;
		public string survey_id_other;     //Khác
		public string files;       //File đính kèm

		public int deptid_load;       //Dự án
		public string deptid_loadST;

		public int category;
		public string categoryST;
		public List<groundDeliveryRecords_Member> testRecordResult_CdtMember_lstSubTable;
		public List<groundDeliveryRecords_Member> testRecordResult_SurMember_lstSubTable;
	}
}