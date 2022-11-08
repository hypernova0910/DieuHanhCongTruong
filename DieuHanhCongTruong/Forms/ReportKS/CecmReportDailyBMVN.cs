using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin.Models
{
    class CecmReportDailyBMVN
    {
        public long id { get; set; }
        public long idRectangle  { get; set; }
        public long idArea { get; set; }
        public long programId { get; set; }
        public double XPoint { get; set; }
        public double YPoint  { get; set; }
        public double ZPoint  { get; set; }
        public double Deep  { get; set; }
        public string Area { get; set; }
        public string MineType  { get; set; }
        public DateTime TimeExecute { get; set; }
        public string Loai   { get; set; }
        public string Kyhieu   { get; set; }
        public int SL  { get; set; }
        public long Tinhtrang   { get; set; }
        public double Kinhdo   { get; set; }
        public double Vido  { get; set; }
        public long PPXuLy  { get; set; }
        public long cecm_reportdaily_id  { get; set; }

        public double Kichthuoc { get; set; }

        public double Dai { get; set; }

        public double Rong { get; set; }
    }
}
