using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM9
	{
		public int gid;       //Khóa tự sinh
		public string symbol;      //Ký hiệu số
		public string address;     //Địa chỉ
		public int category;
		public string categoryST; 
		public DateTime dates;     //Ngày
		public string datesST;
		public string construction;        //Công trình
		public int cecm_program_id;       //Dự án
		public string cecm_program_idST;
		public string cecm_program_code;

		public string address_cecm;        //Địa điểm dự án
		public string base_qtkt;       //Căn cứ quy trình kỹ thuật
		public DateTime dates_qtkt;        //Ngày quy trình kỹ thuật
		public string dates_qtktST;
		public string base_qcqg;       //Quy chuẩn kỹ thuật quốc gia
		public string files_1;     //File PAKTTC
		public DateTime time_nt_from;      //Thời gian nghiệm thu từ
		public string time_nt_fromST;
		public DateTime time_nt_to;        //Thời gian nghiệm thu đến
		public string time_nt_toST;
		public string files_2;     //File hồ sơ, tài liệu nghiệm thu
		public string result;      //Kết quả thu được
		public string comment;     //Các ý kiến khác tham gia
		public string conclusion;      //Kết luận
		public int construct_id;      //Đại diện đơn vị thi công
		public string construct_idST;
		public string construct_id_other;      //Khác
		public int survey_id;     //Đại diện công trường
		public string survey_idST;
		public string survey_id_other;     //Khác
		public string files;       //File đính kèm
		public int amount;
		public int deptid_load;       //Dự án
		public string deptid_loadST;
		public List<groundDeliveryRecords_Member> internalAcceptMinutesWork_lstSubTable;
		public List<groundDeliveryRecords_Member> internalAcceptMinutes_ConMember_lstSubTable;
		public List<groundDeliveryRecords_Member> internalAcceptMinutes_SurMember_lstSubTable;
	}
}

