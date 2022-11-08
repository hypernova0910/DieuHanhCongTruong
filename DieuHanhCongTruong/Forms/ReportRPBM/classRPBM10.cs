using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM10
	{
		public int gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số 
		public int category;
		public string categoryST;
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public int deptid_rpbm;       //Đơn vị rà phá bom mìn
		public String deptid_rpbmST;
		public String organ_receive;       //Cơ quan nhận
		public int cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public String cecm_program_code;

		public String address_cecm;        //Địa điểm dự án
		public String base_hdkt_number;        //Căn cứ hợp đồng kinh tế số
		public DateTime dates_hdkt;        //Ngày hợp đồng kinh tế
		public String dates_hdktST;
		public String organization_signed;     //Hợp đồng ký kết giữa
		public String organ_ngthu;     //Tổ chức nghiệm thu
		public DateTime dates_request;     //Ngày đề nghị hoàn thành
		public String dates_requestST;
		public String address_request;     //Địa điểm hoàn thành
		public int construct_id;      //Đại diện chủ đầu tư
		public String construct_idST;
		public String construct_id_other;      //Khác
		public String files;       //File đính kèm
	}
}

