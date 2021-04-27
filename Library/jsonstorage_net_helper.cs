using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Library
{
    class jsonstorage_net_helper
    {

        

        public HttpResponseMessage Create()
        {
            string Url = "https://jsonstorage.net/api/items/Perle1";
            using (HttpClient client = new HttpClient())
            {


                HttpRequestMessage message = new HttpRequestMessage
                {
                    RequestUri = new Uri(Url),
                    Method = HttpMethod.Post,
                    Headers =
                    {
                        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                        
                       

                    },
                    Content = new StringContent("{\"name\":\"John Doe\",\"age\":353}", Encoding.UTF8, "application/json")
                };




                var result = client.SendAsync(message);
                result.Wait();
                return result.Result;
            }
        }


        public HttpResponseMessage Update(string id,string json)
        {
          //  string Url = "https://jsonstorage.net/api/items/" + id;
              string Url = "https://DBJsonmicroService.kinchero1.repl.co/dbUpdate?ste=" + id;
            using (HttpClient client = new HttpClient())
            {


                HttpRequestMessage message = new HttpRequestMessage
                {
                    RequestUri = new Uri(Url),
                    Method = HttpMethod.Put,
                    Headers =
                    {
                        { HttpRequestHeader.ContentType.ToString(), "application/json" },



                    },
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };



                try
                {
                    var result = client.SendAsync(message);
                    result.Wait();
                    return result.Result;

                }
                catch (Exception)
                {

                    return null;
                }
              
            }
        }

        public HttpResponseMessage Read(string id)
        {
            //string Url = "https://jsonstorage.net/api/items/" + id;
            string Url = "https://DBJsonmicroService.kinchero1.repl.co/dbGet?ste=" + id;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(Url);
                    result.Wait();
                    return result.Result;
                }
                catch (Exception)
                {

                    return null;
                }
              
            }
        }

        public HttpResponseMessage GetAuth()
        {
     
            string Url = "https://DBJsonmicroService.kinchero1.repl.co/appAuth";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(Url);
                    result.Wait();
                    return result.Result;

                }
                catch (Exception)
                {

                    return null;
                }
              
            }
        }




    }
}
