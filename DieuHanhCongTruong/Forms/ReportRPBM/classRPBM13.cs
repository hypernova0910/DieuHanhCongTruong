using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM13
	{
		public long gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public long cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public String cecm_program_code;

		public long category;      //Hạng mục
		public String categoryST;
		public String address_cecm;        //Địa điểm
		public String base_on_tech;        //Căn cứ quy trình kỹ thuật
		public String technical_regulations;       //Quy chuẩn kỹ thuật quốc gia
		public String num_economic_contracts;      //Số hợp đồng kinh tế
		public DateTime date_economic_contracts;       //Ngày hợp đồng kinh tế
		public String date_economic_contractsST;
		public String organization_signed;     //Hợp đồng ký kết giữa
		public DateTime time_signed;       //Thời gian tại hiện trường
		public String time_signedST;
		public Double area_rapha;      //Diện tích đã rà phá bom mìn, vật nổ
		public String files_1;     //File bản vẽ hoàn công rà phá bom mìn
		public Double deep_rpbm;       //Độ sâu rà phá BMVN
		public Double area_cecm;       //Diện tích dự án
		public Double deep;        //Độ sâu rà phá
		public String request_other;       //Các yêu cầu khác của chủ dự án
		public String files_2;     //File tọa độ và bản vẽ thiết kế mặt bằng khu vực dự án
		public Double amount;      //Số lượng biên bản
		public long survey_id;     //Đại diện đơn vị giám sát
		public String survey_idST;
		public String survey_id_other;     //Khác
		public long local_id;      //Cơ quan địa phương
		public String local_idST;
		public String local_id_other;      //Khác
		public long boss_id;       //Đại diện chủ đầu tư
		public String boss_idST;
		public String boss_id_other;       //Khác
		public long construct_id;      //Đại diện đơn vị thi công
		public String construct_idST;
		public String construct_id_other;      //Khác
		public String files;       //File đính kèm
		public int deptid_load;       //Dự án
		public string deptid_loadST;
		public List<groundDeliveryRecords_Member> reportHandoverResult_CDTMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportHandoverResult_ConsMem_lstSubTable;
		public List<groundDeliveryRecords_Member> reportHandoverResult_SurveyMem_lstSubTable;
		public List<groundDeliveryRecords_Member> reportHandoverResult_LocalMem_lstSubTable;

	}
}

