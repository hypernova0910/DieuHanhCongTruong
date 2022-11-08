using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Models
{
    class LastModelTime
    {
        public ObjectId _id { get; set; }

        public long cecm_program_id { get; set; }
        public long area_id { get; set; }
        public long o_id { get; set; }
        public DateTime last_model_time { get; set; }
    }
}
