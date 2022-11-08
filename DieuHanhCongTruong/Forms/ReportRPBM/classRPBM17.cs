using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM17
	{
		public long gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public String organ_receive;       //Cơ quan nhận
		public long cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public string cecm_program_code;
		public long deptid_hrpbm;      //Đơn vị rà phá bom mìn
		public String deptid_hrpbmST;
		public long category;      //Hạng mục
		public String categoryST;
		public String address_cecm;        //Địa điểm
		public String base_on_thongtu;     //Thông tư số
		public String technical_regulations;       //Quy chuẩn kỹ thuật
		public String num_economic_contracts;      //Số hợp đồng kinh tế
		public DateTime date_economic_contracts;       //Ngày hợp đồng kinh tế
		public String date_economic_contractsST;
		public String organization_signed;     //Hợp đồng ký kết giữa
		public String mission;     //Nhiệm vụ
		public Double deep_rpbm;       //Độ sâu RPBM
		public DateTime date_start;        //Ngày bắt đầu thi công
		public String date_startST;
		public DateTime dates_end;     //Ngày kết thúc thi công
		public String dates_endST;
		public long command_id;        //Chỉ huy chung
		public String command_idST;
		public String command_id_other;        //Khác
		public Double num_team;        //Số đội RPBM
		public String files_1;     //File danh sách và quyết định điều động lực lượng
		public Double number_driver;       //Số xe ô tô
		public Double number_machine_min;      //Số máy dò mìn
		public Double number_machine_bomb;     //Số máy dò bom
		public Double area_rapha;      //Diện tích thi công RPBM
		public String files_2;     //File bản vẽ thiết kế mặt bằng, tọa độ
		public Double deep_rapha;      //Độ sâu RPBM
		public String result;      //Kết quả thu được và xử lý
		public String comment_tc;      //Nhận xét quá trình thi công
		public String conclusion;      //Kết luận
		public long construct_id;      //Đại diện đơn vị rà phá bom mìn
		public String construct_idST;
		public String construct_id_other;      //Khác
		public String files;       //File đính kèm

		public List<groundDeliveryRecords_Member> reportResConsRPBM_Work_lstSubTable;

	}
}

