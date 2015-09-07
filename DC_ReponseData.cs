using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Twitter
{
    public class DC_ReponseData
    {
        public DC_ReponseData(string s)
        {
            this.JsonString = s;
            this.JsonObject = (Dictionary<string,Object>)new JavaScriptSerializer().Deserialize<object>(s);
        }

        public DC_ReponseData(Dictionary<string,Object> o)
        {
            this.JsonObject = o;
        }

        public object Get(string query)
        {
            string[] qs = query.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            object o = JsonObject;
            foreach (string q in qs)
            {
                o = ((Dictionary<string, object>)o)[q];
            }

            if (o is Dictionary<string, object>)
            {
                o = new DC_ReponseData((Dictionary<string, object>)o);
            }
            return o;

        }

        public string JsonString { get; set; }
        public Dictionary<string,Object> JsonObject { get; set; }
    }
}
