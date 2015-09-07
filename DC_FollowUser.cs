using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class DC_FollowUser
    {
        public long? user_id { get; set; }
        public string screen_name { get; set; }
        public Boolean? follow { get; set; }
    }
}
