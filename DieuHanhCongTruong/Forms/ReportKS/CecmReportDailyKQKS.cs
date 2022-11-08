using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin.Models
{
    public class CecmReportDailyKQKS
    {
        public long id { get; set; }
        public long area_id { get; set; }
        public long ol_id { get; set; }
        public string code { get; set; }
        public double Xpoint { get; set; }
        public double YPoint { get; set; }
        public double Distance { get; set; }
        public string ColorIndex { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double EstimateTime { get; set; }
        public string Infor { get; set; }
        public DateTime TimeExecute { get; set; }
        public long Ketqua { get; set; }
        public double DientichchuaKS { get; set; }
        public double Dientichcaybui { get; set; }
        public double Dientichcayto { get; set; }
        public double MatdoTB { get; set; }
        public int Sotinhieu { get; set; }
        public string Ghichu { get; set; }
        public double Dientichtretruc { get; set; }
        public double Matdothua { get; set; }
        public double Matdoday { get; set; }
        public int Daxuly { get; set; }
        public string Matdo { get; set; }
        public string Kyhieu { get; set; }
        public string Loai { get; set; }
        public int SL { get; set; }
        public int Kichthuoc { get; set; }
        public int Kinhdo { get; set; }
        public int Vido { get; set; }
        public int Dosau { get; set; }
        public string Tinhtrang { get; set; }
        public double DaxulyM2 { get; set; }

        public long cecm_reportdaily_id { get; set; }
    }
}
