using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;  

namespace Twitter
{
    public class RestService
    {
        private DC_OAuthSettings OAuth { get; set; }

        public RestService(DC_OAuthSettings oAuth)
        {
            OAuth = oAuth;
        }

        public Dictionary<string, object> Standard(DC_RequestData requestData)
        {

            requestData.OAuth = OAuth;
            HttpWebRequest request = null;
                
            if(requestData.Method == DC_RequestData.EnumMethod.POST){
                request = (HttpWebRequest)HttpWebRequest.Create(requestData.URL);
            } else {
                request = (HttpWebRequest)HttpWebRequest.Create(new Uri(requestData.URL,"?" + requestData.Payload()));

            }


            request.Method = requestData.Method.ToString();
            request.ContentType = "application/x-www-form-urlencoded";
            //request.Headers.Add("Accept", @"*/*");
            request.Headers.Add("Authorization", requestData.OAuthHeader());

            if (requestData.Method == DC_RequestData.EnumMethod.POST)
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] bytes = encoding.GetBytes(requestData.Payload());

                request.ContentLength = bytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    // Send the data.
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }
                      

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Console application output  
                string json = reader.ReadToEnd();
                if (json == "[]") { return new Dictionary<string, object>(); }
                if(json.StartsWith("[")) json = "{ \"Array\":" + json + "}";
                return (Dictionary<string,object>)new JavaScriptSerializer().Deserialize<object>(json);
                // return new DC_ReponseData(reader.ReadToEnd());
            }
        }

        public Dictionary<string, object> Tweet(string msg)
        {
            Twitter.DC_RequestData r = new Twitter.DC_RequestData();
            r.Method = Twitter.DC_RequestData.EnumMethod.POST;
            r.URL = new Uri("https://api.twitter.com/1.1/statuses/update.json");
            //r.Nonce = "18b5b7b4967f3bc7656fb17a885e8e35";
            r.Para.Add("status",msg);

            return Standard(r);
        }

        //private void GetResponse(Uri uri, Action<Response> callback)
        //{
        //    WebClient wc = new WebClient();
        //    wc.OpenReadCompleted += (o, a) =>
        //    {
        //        if (callback != null)
        //        {
        //            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
        //            callback(ser.ReadObject(a.Result) as Response);
        //        }
        //    };
        //    wc.OpenReadAsync(uri);
        //}

    

        //public void Tweet()
        //{
        //    //curl --request 'POST' 'https://api.twitter.com/1.1/statuses/update.json' 
        //    //--data 'status=Maybe+he%27ll+finally+find+his+keys.+%23peterfalk' --header 'Authorization: OAuth oauth_consumer_key="yNopUCE5Ujeiu6GS1nhjtg", oauth_nonce="5622d20438f222ef686a0e94af8c52ce", oauth_signature="tB710XmQCYbDUXW2j2HwBn1CYA4%3D", oauth_signature_method="HMAC-SHA1", oauth_timestamp="1391264390", oauth_token="2321549028-b12glidDOoAdvhlWSCHfkUjF3uqj9hxe8YpWU92", oauth_version="1.0"' --verbose 

        //    string url = "https://api.twitter.com/1.1/statuses/update.json";
        //    string oauth_consumer_key = "yNopUCE5Ujeiu6GS1nhjtg";
        //    string oauth_nonce = "4477ed662ac620ccd449d47b81a8c21e";
        //    string oauth_signature = "vhR7AIGmb5nrszHuwEUW9zAt36Y=";
        //    string oauth_timestamp = "1391276185";//((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(); // "1391265067";
        //    string oauth_token = "2321549028-b12glidDOoAdvhlWSCHfkUjF3uqj9hxe8YpWU92";

        //    string header = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", oauth_signature=\"{2}\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"{3}\", oauth_token=\"{4}\", oauth_version=\"1.0\"";
        //    header = string.Format(header, oauth_consumer_key, oauth_nonce, oauth_signature, oauth_timestamp, oauth_token);
        //    header = "OAuth oauth_consumer_key=\"yNopUCE5Ujeiu6GS1nhjtg\", oauth_nonce=\"18b5b7b4967f3bc7656fb17a885e8e33\", oauth_signature=\"hFUbM4kwMTbPjV%2F%2B%2FkTKVWOg0F0%3D\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"1391283437\", oauth_token=\"2321549028-b12glidDOoAdvhlWSCHfkUjF3uqj9hxe8YpWU92\", oauth_version=\"1.0\"";
        //    string data = "status=Maybe+he%27ll+finally+find+his+keys.+%23peterfalk4";


        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    //request.Headers.Add("Accept", @"*/*");
        //    request.Headers.Add("Authorization", header);

        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    byte[] bytes = encoding.GetBytes(data);

        //    request.ContentLength = bytes.Length;

        //    using (Stream requestStream = request.GetRequestStream())
        //    {
        //        // Send the data.
        //        requestStream.Write(bytes, 0, bytes.Length);
        //    }

        //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        // Get the response stream  
        //        StreamReader reader = new StreamReader(response.GetResponseStream());

        //        // Console application output  
        //        Console.WriteLine(reader.ReadToEnd());
        //    }
        //}

        //private void yahoo()
        //{
        //    // We use the HttpUtility class from the System.Web namespace  


        //    Uri address = new Uri("http://api.search.yahoo.com/ContentAnalysisService/V1/termExtraction");

        //    // Create the web request  
        //    HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

        //    // Set type to POST  
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    // Create the data we want to send  
        //    string appId = "YahooDemo";
        //    string context = "Italian sculptors and painters of the renaissance"
        //                        + "favored the Virgin Mary for inspiration";
        //    string query = "madonna";

        //    StringBuilder data = new StringBuilder();
        //    data.Append("appid=" + HttpUtility.UrlEncode(appId));
        //    data.Append("&context=" + HttpUtility.UrlEncode(context));
        //    data.Append("&query=" + HttpUtility.UrlEncode(query));

        //    // Create a byte array of the data we want to send  
        //    byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

        //    // Set the content length in the request headers  
        //    request.ContentLength = byteData.Length;

        //    // Write data  
        //    using (Stream postStream = request.GetRequestStream())
        //    {
        //        postStream.Write(byteData, 0, byteData.Length);
        //    }

        //    // Get response  
        //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        // Get the response stream  
        //        StreamReader reader = new StreamReader(response.GetResponseStream());

        //        // Console application output  
        //        Console.WriteLine(reader.ReadToEnd());
        //    }
        //}
    

        //public void GetPOSTResponse(Uri uri, DC_RequestData data)
        //{
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

        //    request.Method = "POST";
        //    request.ContentType = "text/plain;charset=utf-8";
        //    foreach(string key in data.Headers.Keys)
        //    {
        //        request.Headers.Add(key, data.Headers[key]);
        //    }

        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    byte[] bytes = encoding.GetBytes(data.GetData());

        //    request.ContentLength = bytes.Length;

        //    using (Stream requestStream = request.GetRequestStream())
        //    {
        //        // Send the data.
        //        requestStream.Write(bytes, 0, bytes.Length);
        //    }

        //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        // Get the response stream  
        //        StreamReader reader = new StreamReader(response.GetResponseStream());

        //        // Console application output  
        //        Console.WriteLine(reader.ReadToEnd());
        //    }

        //    ////request.BeginGetResponse((x) =>
        //    ////{
        //    ////    using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(x))
        //    ////    {
        //    ////        if (callback != null)
        //    ////        {
        //    ////            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
        //    ////            callback(ser.ReadObject(response.GetResponseStream()) as Response);
        //    ////        }
        //    ////    }
        //    ////}, null);
        //}

        public Dictionary<string, object> Search(DC_SearchRequest searchRequest)
        {
           //https://api.twitter.com/1.1/search/tweets.json?q=%23freebandnames&since_id=24012619984051000&max_id=250126199840518145&result_type=mixed&count=4 
            //string q = "q=%23freebandnames";
            Twitter.DC_RequestData r = new Twitter.DC_RequestData();
            r.Method = Twitter.DC_RequestData.EnumMethod.GET;
            r.URL = new Uri("https://api.twitter.com/1.1/search/tweets.json");
            //r.Nonce = "18b5b7b4967f3bc7656fb17a885e8e35";
            r.Para.Add("q", searchRequest.q);   // = q;// string.Format("status={0}", r.UrlEncode(g));
            if (searchRequest.count != null) { r.Para.Add("count", searchRequest.count.ToString()); }
            if (searchRequest.geocode != null) { r.Para.Add("geocode", searchRequest.geocode.ToString()); }
            if (searchRequest.include_entities != null) { r.Para.Add("include_entities", searchRequest.include_entities.ToString()); }
            if (searchRequest.lang != null) { r.Para.Add("lang", searchRequest.lang); }
            if (searchRequest.locale != null) { r.Para.Add("locale", searchRequest.locale); }
            if (searchRequest.max_id != null) { r.Para.Add("max_id", searchRequest.max_id.ToString()); }
            if (searchRequest.result_type != null) { r.Para.Add("result_type", searchRequest.result_type.ToString()); }
            if (searchRequest.since_id != null) { r.Para.Add("since_id", searchRequest.since_id.ToString()); }
            if (searchRequest.until != null) { r.Para.Add("until", ((DateTime)searchRequest.until).ToString("yyyy-MM-dd")); }

            return Standard(r);
        }

        public Dictionary<string, object> Retweet(DC_RetweetRequest retweetRequest)
        {
            Twitter.DC_RequestData r = new Twitter.DC_RequestData();
            r.Method = Twitter.DC_RequestData.EnumMethod.POST;
            r.URL = new Uri(string.Format("https://api.twitter.com/1.1/statuses/retweet/{0}.json", retweetRequest.id));
            if (retweetRequest.trim_user != null) { r.Para.Add("trim_user", retweetRequest.trim_user.ToString()); }
            return Standard(r);
        }

        public Dictionary<string, object> FollowUser(DC_FollowUser followRequest)
        {
            Twitter.DC_RequestData r = new Twitter.DC_RequestData();
            r.Method = Twitter.DC_RequestData.EnumMethod.POST;
            r.URL = new Uri("https://api.twitter.com/1.1/friendships/create.json");
            if (followRequest.user_id != null) { r.Para.Add("user_id", followRequest.user_id.ToString()); }
            if (followRequest.screen_name != null) { r.Para.Add("screen_name", followRequest.screen_name); }
            if (followRequest.follow != null) { r.Para.Add("follow", followRequest.follow.ToString().ToLower()); }
            return Standard(r);
        }

        public Dictionary<string, object> UnfollowUser(DC_UnfollowUser unfollowRequest)
        {
            Twitter.DC_RequestData r = new Twitter.DC_RequestData();
            r.Method = Twitter.DC_RequestData.EnumMethod.POST;
            r.URL = new Uri("https://api.twitter.com/1.1/friendships/destroy.json");
            if (unfollowRequest.user_id != null) { r.Para.Add("user_id", unfollowRequest.user_id.ToString()); }
            if (unfollowRequest.screen_name != null) { r.Para.Add("screen_name", unfollowRequest.screen_name); }
            return Standard(r);
        }

        public Dictionary<string, object> Mentions(DC_TimelineRequest timelineRequest)
        {
            Twitter.DC_RequestData r = new Twitter.DC_RequestData();
            r.Method = Twitter.DC_RequestData.EnumMethod.GET;
            r.URL = new Uri("https://api.twitter.com/1.1/statuses/mentions_timeline.json");
            if (timelineRequest.count != null) { r.Para.Add("count", timelineRequest.count.ToString()); }
            if (timelineRequest.max_id != null) { r.Para.Add("max_id", timelineRequest.max_id.ToString()); }
            if (timelineRequest.since_id != null) { r.Para.Add("since_id", timelineRequest.since_id.ToString()); }
            if (timelineRequest.contributor_details != null) { r.Para.Add("contributor_details", timelineRequest.contributor_details.ToString().ToLower()); }
            if (timelineRequest.include_entities != null) { r.Para.Add("include_entities", timelineRequest.include_entities.ToString().ToLower()); }
            if (timelineRequest.include_rts != null) { r.Para.Add("include_rts", timelineRequest.include_rts.ToString()); }
            if (timelineRequest.trim_user != null) { r.Para.Add("trim_user", timelineRequest.trim_user.ToString().ToLower()); }
            return Standard(r);
        }

        public Dictionary<string, object> FriendshipsLookup(DC_FriendshipsLookupRequest friendshipsLookupRequest)
        {
            Twitter.DC_RequestData r = new Twitter.DC_RequestData();
            r.Method = Twitter.DC_RequestData.EnumMethod.GET;
            r.URL = new Uri("https://api.twitter.com/1.1/friendships/lookup.json");
            if (friendshipsLookupRequest.screen_name.Count > 0 & friendshipsLookupRequest.screen_name.Count <= 100)
            {
                r.Para.Add("screen_name", string.Join(",", friendshipsLookupRequest.screen_name));
            }
            else if (friendshipsLookupRequest.user_id.Count > 0 & friendshipsLookupRequest.user_id.Count <= 100)
            {
                r.Para.Add("user_id", string.Join(",", friendshipsLookupRequest.user_id));
            }
            else
            {
                throw new Exception("Screen_name/user_id should be between 1-100");
            }
            
            return Standard(r);
        }
    }
}
