using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM15
	{
		public long gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public long deptid;        //Đơn vị
		public String deptidST;
		public long cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public String cecm_program_code;
		public String address_cecm;        //Địa điểm
		public String technical_regulations;       //Quy chuẩn kỹ thuật quốc gia
		public String base_on_tech;        //Căn cứ
		public String files_1;     //File yêu cầu
		public DateTime time_signed;       //Thời gian tại hiện trường
		public String time_signedST;
		public Double num_all_bomb;        //Tổng số lượng bom
		public String force_used;      //Lực lượng sử dụng
		public long command_destroy_id;        //Chỉ huy hủy nổ
		public String command_destroy_idST;
		public String command_destroy_id_other;        //Khác
		public DateTime dates_notified;        //Ngày hiệp đồng và ra thông báo hủy
		public String dates_notifiedST;
		public DateTime time_deli_from;        //Thời gian vận chuyển, hủy nổ từ
		public String time_deli_fromST;
		public DateTime time_deli_to;      //Thời gian vận chuyển, hủy nổ đến
		public String time_deli_toST;
		public long time_destroy_from;     //Thời gian hủy trong ngày từ
		public long time_destroy_to;       //Thời gian hủy trong ngày đến
		public String address_destroy;     //Bãi hủy
		public String num_ship;        //Số chuyến vận chuyển
		public long command_ship_id;       //Chỉ huy hủy nổ
		public String command_ship_idST;
		public String command_ship_id_other;       //Khác
		public String organ_approve;       //Cơ quan phê duyệt
		public String plan_approve;        //Kế hoạch được phê duyệt
		public long boss_id;       //Chỉ huy đơn vị hủy nổ
		public String boss_idST;
		public String boss_id_other;       //Khác
		public String files;       //File đính kèm
		public int category;
		public string categoryST;

		public List<groundDeliveryRecords_Member> planDestroyBombType_lstSubTable;
		public List<groundDeliveryRecords_Member> planDestroyBombVehicle_lstSubTable;

	}
}

