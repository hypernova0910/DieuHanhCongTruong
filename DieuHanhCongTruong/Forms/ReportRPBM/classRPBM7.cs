using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM7
	{
		public int gid;       //Khóa tự sinh
		public int deptid_survey;     //Đơn vị rà phá bom mìn
		public String deptid_surveyST;
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public String address_cecm;     //Địa chỉ
		public String cecm_program_code;
		public DateTime dates;     //Ngày
		public String datesST;
		public int cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public int captain_id;        //Đội trưởng
		public String captain_idST;
		public String captain_id_other;        //Khác
		public int cadres_id;     //Cán bộ GSKT/QLCL
		public String cadres_idST;
		public String cadres_id_other;     //Khác
		public String address_tc;      //Khu vực rà phá
		public DateTime dates_tc;      //Ngày thi công
		public String dates_tcST;
		public String weather;     //Thời tiết
		public String comment;     //Nhận xét
		public int boss_id;       //Cán bộ QLCL
		public String boss_idST;
		public String boss_id_other;       //Khác
		public int captain_sign_id;       //Đội trưởng
		public String captain_sign_idST;
		public String captain_sign_id_other;       //Khác
		public String files;       //File đính kèm
		public int category;
		public string categoryST;
		public List<groundDeliveryRecords_Member> constructionDiaryGeneralInfor_lstSubTable;
		public List<groundDeliveryRecords_Member> reportConfDestroy_Bomb_lstSubTable;
	}
}

