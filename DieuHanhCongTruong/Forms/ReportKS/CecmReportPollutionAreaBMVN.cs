using DieuHanhCongTruong.Common;
using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin.Models
{
    public class CecmReportPollutionAreaBMVN
    {
        public long id { get; set; }
        public long idRectangle { get; set; }
        public long idArea { get; set; }
        public long programId { get; set; }
        public double XPoint { get; set; }
        public double YPoint { get; set; }
        public double ZPoint { get; set; }
        public double Deep { get; set; }
        public double Area { get; set; }
        public string MineType { get; set; }
        public DateTime TimeExecute { get; set; }
        public string Loai { get; set; }
        public string Kyhieu { get; set; }
        public int SL { get; set; }
        public string Tinhtrang { get; set; }
        public double Kinhdo { get; set; }
        public double Vido { get; set; }
        public string PPXuLy { get; set; }
        public long cecm_ReportPollutionArea_id { get; set; }

        public double Kichthuoc { get; set; }

        public string Ghichu { get; set; }

        public List<InfoConnect> contour { get; set; }

        public int indexLabel { get; set; }

        public bool isSaved { get; set; }

        public TypeBMVN IsBomb { get; set; }

        public bool UserAdd { get; set; }
    }
}
