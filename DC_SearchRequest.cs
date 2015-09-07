using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class DC_SearchRequest
    {
        public DC_SearchRequest()
        {
            count = 100;
        }

        public string q { get; set; }
        public Geocode geocode { get; set; }
        public string lang { get; set; }
        public string locale { get; set; }
        public Result_type result_type { get; set; }
        public int? count { get; set; }
        public DateTime? until { get; set; } //YYYY-MM-DD
        public long? since_id { get; set; }
        public long? max_id { get; set; }
        public Boolean? include_entities { get; set; }

        public enum Result_type
        {
            recent,
            mixed,
            popular
        }

        public class Geocode
        {
            public Geocode(double la, double lo, int r, RadiusM rm)
            {
                latitude = la;
                longitude = lo;
                radius = r;
                radiusm = rm;
            }

            public enum RadiusM
            {
                mi,
                km
            }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public int radius { get; set; }
            public RadiusM radiusm { get; set; }

            public override string ToString()
            {
                return string.Format("{0},{1},{2}{3}", latitude, longitude, radius, radiusm);
            }

        }
        
    }
}
