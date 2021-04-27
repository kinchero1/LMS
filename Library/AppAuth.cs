using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AppAuth
    {
        public bool authorized;


        protected static HttpResponseMessage GET(string Url)
        {

            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync(Url);
                result.Wait();
                return result.Result;
            }
        }

        public static bool  getAuth()
        {
            if (Program.CheckForInternetConnection())
            {
                var response = GET("https://dbjsonmicroservice.kinchero1.repl.co/appAuth");
                string content = response.Content.ReadAsStringAsync().Result;


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AppAuth appAuthorisation= JsonConvert.DeserializeObject<AppAuth>(content);
                    return appAuthorisation.authorized;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }
    }


   
}
