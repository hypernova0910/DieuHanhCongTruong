using Aglomera;
using Aglomera.Linkage;
using DieuHanhCongTruong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Command.Clustering
{
    class HierarchicalClustering
    {
        private const double Dissimilarity_CompleteLinkage = 4;
        private const double Dissimilarity_All = 0.5;

        private InfoConnect GetCentroid(List<InfoConnect> poly)
        {
            double tongX = 0;
            double tongY = 0;
            double z = 0;
            foreach (var item in poly)
            {
                tongX += item.lat_value;
                tongY += item.long_value;
                z += item.the_value;
            }
            return new InfoConnect(tongX / poly.Count, tongY / poly.Count, z / poly.Count);
        }


        public List<InfoConnect> Cluster(List<InfoConnect> vertices, int minClusterSize = 1)
        {
            List<InfoConnect> result = new List<InfoConnect>();

            var metric = new DissimilarityMetric();
            var linkage = new CompleteLinkage<InfoConnect>(metric);
            var algorithm = new AgglomerativeClusteringAlgorithm<InfoConnect>(linkage);
            ClusteringResult<InfoConnect> clusteringResult = algorithm.GetClustering(vertices.ToHashSet());

            if (clusteringResult.Count > 0)
            {
                ClusterSet<InfoConnect> clusterSet = clusteringResult[0];
                for (int i = 0; i < clusteringResult.Count; i++)
                //foreach (ClusterSet<InfoConnect> clusterSet in clusteringResult)
                {
                    //int count = 0;
                    if (clusteringResult[i].Dissimilarity > Dissimilarity_CompleteLinkage)
                    {
                        break;
                    }
                    else
                    {
                        clusterSet = clusteringResult[i];
                    }
                    //Console.WriteLine("clusterSet.Dissimilarity: " + clusterSet.Dissimilarity);
                    //Console.WriteLine("");
                }
                foreach (Cluster<InfoConnect> cluster in clusterSet)
                {
                    if (cluster.Count >= minClusterSize)
                    {
                        InfoConnect InfoConnect = GetCentroid(cluster.ToList());
                        InfoConnect.code = cluster.ToList()[0].code;
                        result.Add(InfoConnect);
                    }
                }
            }
            return result;
        }
    }
}
