using MIConvexHull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Models
{
    public class CustomFace : TriangulationCell<InfoConnect, CustomFace>
    {
        public string id { get; set; }
    }
}
