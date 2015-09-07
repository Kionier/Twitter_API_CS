using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class DC_OAuthSettings
    {
        public string Version { get; private set; }
        public string SignMethod { get; private set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public DC_OAuthSettings()
        {
            Version = "1.0";
            SignMethod = "HMAC-SHA1";
        }
    }
}
