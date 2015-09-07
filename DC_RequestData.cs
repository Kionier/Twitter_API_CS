using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.IO;


namespace Twitter
{
    public class DC_RequestData
    {
        public enum EnumMethod
        {
            GET,
            POST
        }
        private static Random R = new Random();

        public EnumMethod Method { get; set; }
        public Uri URL { get; set; }
        public DC_OAuthSettings OAuth { get; set; }
        public Dictionary<string,string> Para { get; set; }
        public string TimeStamp { get; set; }
        public string Nonce { get; set; }

        public string Payload()
        {
            string p = "";
            foreach (string k in Para.Keys.OrderBy(kk => kk))
            {
                p = string.Format("{0}&{1}={2}",p,k, UrlEncode(Para[k]));
            }
            if(p.Length > 0)
            {
                p=p.Remove(0,1);
            }
            return p;
        }

        public DC_RequestData()
        {
            Para = new Dictionary<string, string>();
            TimeStamp = ((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(); //"1391283437";
            Nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()+R.Next(100000).ToString())).Replace("=","");
        }

        private string BaseString()
        {
            Dictionary<string, string> para = Para.ToDictionary(t => t.Key, t => t.Value);// ctionary<string, string>();
            para.Add("oauth_consumer_key", OAuth.ConsumerKey);
            para.Add("oauth_nonce", Nonce);
            para.Add("oauth_signature_method", OAuth.SignMethod);
            para.Add("oauth_timestamp", TimeStamp);
            para.Add("oauth_token", OAuth.AccessToken);
            para.Add("oauth_version", OAuth.Version);
            
            string s = "";
            foreach (string k in para.Keys.OrderBy(kk => kk))
            {
                s = string.Format("{0}&{1}={2}",s,k,UrlEncode(para[k]));
            }
            s = s.Remove(0, 1);

            s = string.Format("{0}&{1}&{2}", Method.ToString(), UrlEncode(URL.ToString()), UrlEncode(s));

            return s;
            
        }

        public string UrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp).Replace("+", "%20").Replace("(", "%28").Replace(")", "%29").Replace("!", "%21").Replace("%7E", "~");
        }

        public string OAuthSignature()
        {

            string baseString = BaseString();
            //return bs;
            string signingKey = OAuth.ConsumerSecret + "&" + OAuth.AccessTokenSecret;
            HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(signingKey));

            string signatureString = Convert.ToBase64String(
                hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString)));
            return signatureString;
        }

        public string OAuthHeader()
        {
            
            string h = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", oauth_timestamp=\"{4}\", oauth_token=\"{5}\", oauth_version=\"{6}\"";
            h = string.Format(h, OAuth.ConsumerKey, Nonce, UrlEncode(OAuthSignature()), OAuth.SignMethod, TimeStamp, OAuth.AccessToken, OAuth.Version);
            return h;
        }
    }
}
