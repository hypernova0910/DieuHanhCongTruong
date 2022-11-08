using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM11
	{
		public int gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ category
		public int category;
		public string categoryST; 
		public DateTime dates;     //Ngày
		public String datesST;
		public int cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public String cecm_program_code;

		public String address_cecm;        //Địa điểm
		public String base_on_tech;        //Căn cứ quy trình kỹ thuật
		public String technical_regulations;       //Quy chuẩn kỹ thuật quốc gia
		public DateTime date_economic_contracts;       //Ngày diễn ra
		public String date_economic_contractsST;
		public String address_ground;      //Hiện trường
		public String conclusion;      //Kết luận
		public String regulations;     //Quy định
		public Double amount;      //Số lượng biên bản
		public Double amount_each;      //Số lượng biên bản
		public int boss_id;       //Đại diện chủ đầu tư
		public String boss_idST;
		public String boss_id_other;       //Khác
		public int survey_id;     //Đại diện đơn vị khảo sát
		public String survey_idST;
		public String survey_id_other;     //Khác
		public int construction_id;       //Đại diện đơn vị thi công
		public String construction_idST;
		public String construction_id_other;       //Khác
		public String files;       //File đính kèm
		public int deptid_load;       //Dự án
		public string deptid_loadST;
		public List<groundDeliveryRecords_Member> confirmBomb_CDTMem_lstSubTable;
		public List<groundDeliveryRecords_Member> confirmBomb_SurMem_lstSubTable;
		public List<groundDeliveryRecords_Member> confirmBomb_ConsMem_lstSubTable;
		public List<groundDeliveryRecords_Member> confirmBMVN_Type_lstSubTable;

	}
}

