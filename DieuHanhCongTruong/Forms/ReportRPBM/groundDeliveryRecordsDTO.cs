using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong
{
    public class groundDeliveryRecordsDTO
    {
        public int gid;
        public string symbol;
        public int category;
        public string categoryST;
        public string address;
        public string dates;
        public string datesST;
        public string cecm_program_code;
        public int cecm_program_id;
        public string cecm_program_idST;
        public string address_cecm;
        public string base_on_tech;
        public string technical_regulations;
        public string technical_regulationsST;
        public string num_economic_contracts;
        public string date_economic_contracts;
        public string date_economic_contractsST;
        public string organization_signed;
        public string time_signed;
        public string time_signedST;
        public string detail_giaonhan;
        public string files_1;
        public float area_ground;
        public string files_2;
        public float area_construction;
        public string files_3;
        public float deep;
        public string request_other;
        public string files_4;
        public int deptid_handover;
        public string deptid_handoverST;
        public string conclusion;
        public float amount;
        public int boss_id;
        public string boss_idST;
        public string boss_id_other;
        public int survey_id;
        public string survey_idST;
        public string survey_id_other;
        public string files;
        public string deptId;
        public string deptIdST;
        public List<groundDeliveryRecords_Member> groundDeliveryRecords_CDTMember_lstSubTable;
        public List<groundDeliveryRecords_Member> groundDeliveryRecords_SurveyMem_lstSubTable;
    }

    public class groundDeliveryRecords_Member
    {
        public int gid;
        public string table_name;
        public string field_name;
        public int main_id;
        public string string1;
        public string string2;
        public string string3;
        public string string4;
        public string string5;
        public string string6;
        public string long1ST;
        public string long2ST;
        public long long1;
        public long long2;
        public string long5;
        public double double1;
        public double double2;
        public double double3;
        public double double4;
        public double double5;
        public double double6;
    }
}