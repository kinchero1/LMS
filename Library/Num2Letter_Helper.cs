using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library
{
    class Num2Letter_Helper
    {
        public string value;
        public string Decimal;
        public string lang;

        protected static HttpResponseMessage GET(string Url)
        {

            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync(Url);
                result.Wait();
                return result.Result;
            }
        }

        public static Num2Letter_Helper getValue(string url,string number,string lang ="fr")
        {
            if (Program.CheckForInternetConnection())
            {
                var response = GET(url + "/api?number=" + number + "&lang=" + lang);
                string content = response.Content.ReadAsStringAsync().Result;


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Num2Letter_Helper>(content);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
          

        }


    }
}
