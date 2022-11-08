using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM12
	{
		public int gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public int category;
		public string categoryST; 
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public int cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public String cecm_program_code;

		public String address_cecm;        //Địa điểm
		public String base_on_tech;        //Căn cứ quy trình kỹ thuật
		public String technical_regulations;       //Quy chuẩn kỹ thuật quốc gia
		public String num_economic_contracts;      //Số hợp đồng kinh tế
		public DateTime date_economic_contracts;       //Ngày hợp đồng kinh tế
		public String date_economic_contractsST;
		public String organization_signed;     //Hợp đồng ký kết giữa
		public String report_scene;        //Biên bản hiện trường
		public DateTime time_acceptance_start;     //Thời gian bắt đầu nghiệm thu
		public String time_acceptance_startST;
		public DateTime time_acceptance_end;       //Thời gian kết thúc nghiệm thu
		public String time_acceptance_endST;
		public String field_site;      //Thực địa công trường
		public String resutl_acceptance;       //Kết quả thu được
		public String evaluate_opinion;        //Ý kiến đánh giá chất lượng thi công dự án
		public String evaluate_opinion_other;      //Ý kiến đánh giá khác
		public String conclude_acceptance;     //Kết luận
		public Double amount;      //Số lượng biên bản
		public int survey_id;     //Đại diện đơn vị giám sát
		public String survey_idST;
		public String survey_id_other;     //Khác
		public int command_id;        //Chỉ huy công trường
		public String command_idST;
		public String command_id_other;        //Khác
		public int boss_id;       //Đại diện chủ đầu tư
		public String boss_idST;
		public String boss_id_other;       //Khác
		public int construct_id;      //Đại diện đơn vị thi công
		public String construct_idST;
		public String construct_id_other;      //Khác
		public String files;       //File đính kèm
		public int deptid_load;       //Dự án
		public string deptid_loadST;
		public List<groundDeliveryRecords_Member> reportConsDone_CDTMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConsDone_ConsMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConsDone_SurveyMem_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConsDone_Work_lstSubTable;

	}
}

