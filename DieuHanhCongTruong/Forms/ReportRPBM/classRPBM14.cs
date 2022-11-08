using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM14
	{
		public long gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public long deptid_rpbm;       //Đơn vị RPBM
		public String deptid_rpbmST;
		public long cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public string cecm_program_code;
		public String address_cecm;        //Địa điểm
		public String cdt;     //Chủ đầu tư
		public String ground_done;     //Mặt bằng khu vực đã thi công
		public Double area_all_rpbm;       //Tổng diện tích rà phá bom mìn, vật nổ
		public String address_rpbm;        //Địa điểm
		public String detail;      //Cụ thể
		public Double deep;        //Độ sâu rà phá BMVN
		public DateTime dates_rpbm;        //Ngày bắt đầu RPBM
		public String dates_rpbmST;
		public long captain_sign_id;       //Đại diện chủ đầu tư
		public String captain_sign_idST;
		public String captain_sign_id_other;       //Khác
		public String files;       //File đính kèm
		public long category;
		public string categoryST;
	}
}

