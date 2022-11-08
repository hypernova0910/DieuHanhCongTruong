using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Models
{
    internal class PointDTO
    {
        public Double xLatLong { get; set; }
        public Double yLatLong { get; set; }
        public Double zLatLong { get; set; }

        public Double xUTM { get; set; }
        public Double yUTM { get; set; }
        public Double zUTM { get; set; }
    }
}
