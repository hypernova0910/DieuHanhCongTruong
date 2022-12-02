using MIConvexHull;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Models
{
    public class InfoConnect : IVertex, IVertex2D
    {
        public InfoConnect()
        {

        }

        public InfoConnect(double lat, double longt)
        {
            lat_value = lat;
            long_value = longt;
        }

        public InfoConnect(double lat, double longt, double z)
        {
            lat_value = lat;
            long_value = longt;
            the_value = z;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string code { get; set; }
        public long project_id { get; set; }
        public string machineBomCode { get; set; }
        public double lat_value { get; set; }
        public double long_value { get; set; }

        public GeoJsonPoint<GeoJson2DCoordinates> coordinate { get; set; }
        public double the_value { get; set; }
        public DateTime update_time { get; set; }
        public DateTime time_action { get; set; }
        public int bit_sens { get; set; }
        public bool isMachineBom { get; set; }

        public double dilution { get; set; }

        public double satelliteCount { get; set; }

        //Điểm có thuộc rìa lưới tam giác không
        public bool isInEdgeTriangles { get; set; }

        public double[] Position
        {
            get
            {
                return new double[] { lat_value, long_value };
            }
        }

        public double X
        {
            get
            {
                return lat_value;
            }
        }

        public double Y
        {
            get
            {
                return long_value;
            }
        }
    }
}
