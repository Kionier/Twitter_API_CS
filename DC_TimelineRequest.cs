using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class DC_TimelineRequest
    {
        public int count { get; set; }
        public int include_rts { get; set; }
        public long? since_id { get; set; }
        public long? max_id { get; set; }
        public Boolean? trim_user { get; set; }
        public Boolean? contributor_details { get; set; }
        public Boolean? include_entities { get; set; }

        public DC_TimelineRequest()
        {
            count = 200;
            //include_rts = 1;
        }
    }
}
