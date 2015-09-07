using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class DC_SearchResult
    {
        public Statuses[] statuses {get; set;}
        public Search_metadata search_metedata { get; set; }

        public class Search_metadata
        {
            public string max_id { get; set; }
            public string since_id { get; set; }
            public string refresh_url { get; set; }
            public string nest_result { get; set; }
            public int count { get; set; }
            public float completed_in { get; set; }
            public string sence_id_str { get; set; }
            public string query { get; set; }
            public string max_id_str { get; set; }
        }

        public class Statuses
        {
            public object coordinates { get; set; }
            public Boolean favorited { get; set; }
            public Boolean truncated { get; set; }
            public string created_at { get; set; }
            public string id_str { get; set; }
            public Entities entities { get; set; }
            public string in_reply_to_user_str { get; set; }
            public object contributors { get; set; }
            public string text { get; set; }
            public Metadata metadata { get; set; }
            public int retweet_count { get; set; }
            public string in_reply_to_status_id_str { get; set; }
            public string id { get; set; }
            public object geo { get; set; }
            public Boolean retweeted { get; set; }
            public string in_reply_to_user_id { get; set; }
            public object place { get; set; }
            public User user { get; set; }
            public object in_reply_to_screen_name { get; set; }
            public string source { get; set; }
            public string in_reply_to_status_id { get; set; }


            public class User
            {
                public string profile_sidebar_fill_color { get; set; }
                public string profile_sidebar_border_color { get; set; }
                public Boolean profile_background_tile { get; set; }
                public string name { get; set; }
                public string prifile_image_url { get; set; }
                public string created_at { get; set; }
                public string location { get; set; }
                public object follow_request_sent { get; set; }
                public string profile_link_color { get; set; }
                public Boolean is_translator { get; set; }
                public string is_str { get; set; }
                public Entities entities { get; set; }
                public Boolean default_profile { get; set; }
                public Boolean contributors_enabled { get; set; }
                public int favourites_count { get; set; }
                public string url { get; set; }
                public string profile_image_url_https { get; set; }
                public int utc_offset { get; set; }
                public string id { get; set; }
                public Boolean profile_use_background_image { get; set; }
                public int listed_count { get; set; }
                public string profile_text_color { get; set; }
                public string lang { get; set; }
                public int followers_count { get; set; }
                //public Boolean protected { get; set; }
                public object notifications { get; set; }
                public string profile_background_image_url_https { get; set; }
                public string profile_background_color { get; set; }
                public Boolean verified { get; set; }
                public Boolean geo_enabled { get; set; }
                public string time_zone { get; set; }
                public string description { get; set; }
                public Boolean default_profile_image { get; set; }
                public string profile_bankground_image_url { get; set; }
                public int statuses_count { get; set; }
                public int friends_count { get; set; }
                public object followings { get; set; }
                public Boolean show_all_inline_media { get; set; }
                public string screen_name { get; set; }


                public class Entities
                {
                    public Url url { get; set; }
                    public Description description { get; set; }

                    public class Description
                    {
                        public object[] urls { get; set; }
                    }

                    public class Url
                    {
                        public Urls[] MyProperty { get; set; }

                        public class Urls
                        {
                            public object expanded_url { get; set; }
                            public string url { get; set; }
                            public int[] indices { get; set; }
                        }
                    }
                }
            }

            public class Metadata
            {
                public string iso_language_code { get; set; }
                public string result_type { get; set; }
            }

            public class Entities
            {
                public object[] urls { get; set; }
                public Hashtags[] hashtags { get; set; }
                public object[] user_mentions { get; set; }

                public class Hashtags
                {
                    public string text { get; set; }
                    public int[] indices { get; set; }
                }

            }
        }

     

        

      

       
       
    }


}
