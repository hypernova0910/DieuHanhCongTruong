using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Models
{
    class MQTTObject
    {
        public string type { get; set; }
        public string codeMachine { get; set; }

        public double dilution { get; set; }

        public short numValue { get; set; }

        public short numGPS { get; set; }

        public int satelliteCount { get; set; }

        public int bitSent { get; set; }

        public List<GPS> lstGPS { get; set; }

        public double dValData { get; set; }
    }

    class GPS
    {
        public DateTime updateTimeData { get; set; }

        public DateTime timeActionData { get; set; }

        public double dLat { get; set; }

        public double dLong { get; set; }
    }
}
