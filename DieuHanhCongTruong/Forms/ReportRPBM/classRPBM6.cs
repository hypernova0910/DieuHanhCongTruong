using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
	class classRPBM6
	{
		public double gid;       //Khóa tự sinh
		public String symbol;      //Ký hiệu số
		public String address;     //Địa chỉ
		public DateTime dates;     //Ngày
		public String datesST;
		public int cecm_program_id;       //Dự án
		public String cecm_program_idST;
		public string cecm_program_code;
		public int category;
		public string categoryST;
		public String address_cecm;        //Địa điểm
		public DateTime date_now;      //Ngày hiện tại
		public String date_nowST;
		public Double num_team_bctc;       //Số đội thi công trong biên chế
		public Double num_min;     //Số lượng máy dò min
		public Double num_bomb;        //Số lượng máy dò bom
		public Double num_elect;       //Số lượng máy phát điện
		public Double num_talkie;      //Số lượng bộ đàm
		public Double num_gps;     //Số lượng thiết bị định vị GPS
		public Double num_car;       //Số lượng ô tô
		public String num_carST;
		public String conclusion;      //Kết luận
		public Double amount;      //Số lượng biên bản
		public double boss_id;       //Đại diện chủ đầu tư
		public String boss_idST;
		public String boss_id_other;       //Khác
		public double monitor_id;        //Đại diện đơn vị giám sát
		public String monitor_idST;
		public String monitor_id_other;        //Khác
		public double constuct_id;       //Đại diện đơn vị thi công
		public String constuct_idST;
		public String constuct_id_other;       //Khác
		public String files;       //File đính kèm
		public int deptid_load;       //Dự án
		public string deptid_loadST;
		public List<groundDeliveryRecords_Member> reportTestEquipment_CDTMember_lstSubTable;
		public List<groundDeliveryRecords_Member> reportTestEquipment_ConsMem_lstSubTable;
		public List<groundDeliveryRecords_Member> reportTestEquipment_MonitoMem_lstSubTable;

	}
}
