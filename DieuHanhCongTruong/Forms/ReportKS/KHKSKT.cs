using DieuHanhCongTruong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin.Models
{
    class KHKSKT
    {
        public long id { get; set; }
        public string code { get; set; }
        public long province_id { get; set; }
        public long district_id { get; set; }
        public long commune_id { get; set; }
        public string date_createST { get; set; }
        public string  province_code {get;set;}
        public string  district_code{get;set;}
        public string commune_code{get;set;}
        public string dept_code {get;set;}
        public string cecm_program_code {get;set;}
        public string address{get;set;}
        public int ksdh_kl{get;set;}
        public string ksdh_date_startST{get;set;}
        public string ksdh_date_endST{get;set;}
        public string ksdh_note {get;set;}
        public int thbs_kl {get;set;}
        public string thbs_date_startST{get;set;}
        public string thbs_date_endST {get;set;}
        public string thbs_note{get;set;}
        public int iatl_kl{get;set;}
        public string iatl_date_startST{get;set;}
        public string iatl_date_endST{get;set;}
        public string iatl_note{get;set;}
        public int xdpd_kl{get;set;}
        public string xdpd_date_startST{get;set;}
        public string xdpd_date_endST {get;set;}
        public string xdpd_note{get;set;}
        public int ktcl_kl {get;set;}
        public string ktcl_date_startST {get;set;}
        public string ktcl_date_endST {get;set;}
        public string ktcl_note{get;set;}
        public int dkct_kl{get;set;}
        public string dkct_date_startST{get;set;}
        public string dkct_date_endST {get;set;}
        public string dkct_note{get;set;}
        public int thdl_kl{get;set;}
        public string thdl_date_startST {get;set;}
        public string thdl_date_endST {get;set;}
        public string thdl_note{get;set;}
        public int ntkq_kl{get;set;}
        public string ntkq_date_startST {get;set;}
        public string ntkq_date_endST {get;set;}
        public string ntkq_note{get;set;}
        public int lbb_kl{get;set;}
        public string lbb_date_startST {get;set;}
        public string lbb_date_endST {get;set;}
        public string lbb_note{get;set;}
        public string tgth_date_startST {get;set;}
        public string tgth_date_endST {get;set;}
        public string address_receive{get;set;}
        public double nv1_kl{get;set;}
        public string nv1_date_startST {get;set;}
        public string nv1_date_endST {get;set;}
        public string nv1_note{get;set;}
        public string mission_01_code{get;set;}
        public string mission_01_address {get;set;}
        public string mission_02_code {get;set;}
        public string mission_02_address {get;set;}

        public string geo_common { get; set; }
        public string social_common { get; set; }
        public string top_file { get; set; }
        public string info_provided { get; set; }

        public string mission_target { get; set; }
        public string technical_requirement { get; set; }
        public string document_collect { get; set; }
        public string map_draw { get; set; }

        public string survey_geo { get; set; }
        public string medical_handle { get; set; }
        public string quality_guarantee { get; set; }
        public string equipment { get; set; }

        public string method { get; set; }
        public string deptMasterST { get; set; }
        public string deptMaster_other { get; set; }

        public List<groundDeliveryRecords_Member> files { get; set; }
    }
}
