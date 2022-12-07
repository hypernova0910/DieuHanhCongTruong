using Aglomera;
using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Command.Clustering
{
    class DissimilarityMetric : IDissimilarityMetric<InfoConnect>
    {
        public double Calculate(InfoConnect instance1, InfoConnect instance2)
        {
            return Math.Sqrt(Math.Pow(instance1.lat_value - instance2.lat_value, 2) + Math.Pow(instance1.long_value - instance2.long_value, 2));
        }
    }
}
