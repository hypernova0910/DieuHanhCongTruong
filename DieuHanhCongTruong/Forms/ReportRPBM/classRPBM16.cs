using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM16
	{
		public long gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public long cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public string cecm_program_code;
		public int category;      //Hạng mục
		public String categoryST;
		public String address_cecm;        //Địa điểm
		public String base_on_tech;        //Tiêu chuẩn
		public String technical_regulations;       //Quy chuẩn
		public String base_on;     //Căn cứ
		public DateTime dates_now;     //Ngày hiện tại
		public String dates_nowST;
		public String ground_destroy;      //Bãi hủy thuộc
		public long deptid;        //Đơn vị thi công
		public String deptidST;
		public DateTime date_destroy_from;     //Ngày hủy nổ từ
		public String date_destroy_fromST;
		public DateTime date_destroy_to;       //Ngày hủy nổ đến
		public String date_destroy_toST;
		public String method_handl;        //Phương pháp xử lý
		public String address_handl;       //Địa điểm xử lý
		public String result_handl;        //Kết quả xử lý
		public Double amount;      //Số lượng biên bản
		public long survey_id;     //Đại diện đơn vị giám sát
		public String survey_idST;
		public String survey_id_other;     //Khác
		public long local_id;      //Cơ quan quân sự địa phương
		public String local_idST;
		public String local_id_other;      //Khác
		public long boss_id;       //Đại diện chủ đầu tư
		public String boss_idST;
		public String boss_id_other;       //Khác
		public long construct_id;      //Đại diện đơn vị thi công
		public String construct_idST;
		public String construct_id_other;      //Khác
		public String files;       //File đính kèm

		public List<groundDeliveryRecords_Member> reportConfDestroy_CDTMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConfDestroy_SurMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConfDestroy_LocalMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConfDestroy_ConsMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConfDestroy_Bomb_lstSubTable;

	}
}

