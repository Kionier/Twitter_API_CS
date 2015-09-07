using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class DC_FriendshipsLookupRequest
    {
        public List<string> screen_name { get; set; }
        public List<long> user_id { get; set; }

        public DC_FriendshipsLookupRequest()
        {
            screen_name = new List<string>();
            user_id = new List<long>();
        }
    }
}
