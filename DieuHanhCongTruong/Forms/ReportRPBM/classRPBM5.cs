using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
    class classRPBM5
    {
		public int gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public int cecm_program_id;       //Dự án
		public String cecm_program_code;
		public String cecm_program_idST;
		public int category;      //Hạng mục
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
		public Double area_ground;     //Diện tích dự án
		public Double deep;        //Độ sâu rà phá
		public String request_other;       //Các yêu cầu khác
		public String files_1;     //File thiết kế mặt bằng khu vực dự án
		public String conclusion;      //Kết luận
		public Double amount;      //Số lượng biên bản
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
		public List<groundDeliveryRecords_Member> groundConstrucRecords_CDTMember_lstSubTable;
		public List<groundDeliveryRecords_Member> groundConstrucRecords_ConsMem_lstSubTable;
		public List<groundDeliveryRecords_Member> groundConstrucRecords_MonitoMem_lstSubTable;

	}
}
