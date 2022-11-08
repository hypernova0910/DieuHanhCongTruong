using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM8
	{
		public int gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public int cecm_program_id;       //Dự án
		public String cecm_program_code;
		public String cecm_program_idST;
		public int category;
		public string categoryST; 
		public String address_cecm;        //Địa điểm
		public String base_on_tech;        //Căn cứ quy trình kỹ thuật
		public String files_1;     //File nhật ký thi công hàng ngày
		public string result_rpbm;     //Kết quả thi công rà phá bom mìn vật nổ
		public String num_point_test;      //Số điểm được kiểm tra
		public DateTime dates_now;     //Ngày hiện tại
		public String dates_nowST;
		public Double num_machine_mine;        //Số lượng máy dò mìn
		public Double num_machine_bomb;        //Số lượng máy dò bom
		public Double area_test;       //Phạm vi diện tích kiểm tra
		public Double num_o_test;      //Số ô được kiểm tra
		public Double area_o_test;     //Diện tích kiểm tra
		public Double deep_o_test;     //Độ sâu dò mìn
		public String result_test;     //Kết quả kiểm tra
		public String files_2;     //File PAKTTC
		public String qtkt;        //Quy trình kỹ thuật
		public String qckt;        //Quy chuẩn kỹ thuật
		public int boss_id;       //Đại diện chủ đầu tư
		public String boss_idST;
		public String boss_id_other;       //Khác
		public int monitor_id;        //Đại diện đơn vị giám sát
		public String monitor_idST;
		public String monitor_id_other;        //Khác
		public int constuct_id;       //Đại diện đơn vị thi công
		public String constuct_idST;
		public String constuct_id_other;       //Khác
		public String files;       //File đính kèm
		public int deptid_load;       //Dự án
		public string deptid_loadST;
		public List<groundDeliveryRecords_Member> reportProbScene_CDTMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportProbScene_MonitoMem_lstSubTable;
		public List<groundDeliveryRecords_Member> reportProbScene_ConsMem_lstSubTable;



	}
}

