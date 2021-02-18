using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AppIsphereTravelers.Classes
{
    public class clsReadUrl
    {

        public HttpClient client = new HttpClient();

        public void ReadUrl()
        {
            //Task.Run(() => ReadUrlAsync());

        }

        public async Task ReadUrlAsync()
        {


            client.BaseAddress = new Uri("https://app.webfinan.com.br/erp/webservices/api.php");
            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("select * From tblcontas where nome = '123'", "SQL")
                });
            var result = await client.PostAsync("https://app.webfinan.com.br/erp/webservices/api.php", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultContent);

        }
        public async Task<string> ServicePostRequest(string url, string parameters)
        {
            string result = String.Empty;

            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(parameters);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                client.Timeout = new TimeSpan(0, 0, 15);
                using (var response = await client.PostAsync(url, content))
                {
                    using (var responseContent = response.Content)
                    {
                        result = await responseContent.ReadAsStringAsync();
                        Console.WriteLine(result);
                        return result;
                    }
                }
            }
        }
        /*public string DoSQl(string sql)
        {
            var Html = new clsReadUrl();

            Task<string> result = Html.ServicePostRequest("https://app.webfinan.com.br/erp/webservices/api.php", "sql=select  * from tblcontas where Id=1");
            string myResult = result.Result;
            return myResult;
        }
        
        public async void PostRequest(string URL)
        {
            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("somekey", "1"),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            var json = await response.Content.ReadAsStringAsync();
            Events result = JsonConvert.DeserializeObject<Events>(json);
        }
        
        public clsReadUrl(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public async Task<T> MakeGetRequest<T>(string resource)
        {
            try
            {

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_client.BaseAddress, resource),
                    Method = HttpMethod.Post,
                };
                var response = await _client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var jss = new JavaScriptSerializer();
                    string json = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Dictionary<string, string> sData = jss.Deserialize<Dictionary<string, string>>(json);


                    var responseString = await response.Content.ReadAsStringAsync();
                    var model = await  new JavaScriptSerializer().DeserializeObject<T>(responseString);
                    return model;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // you need to maybe re-authenticate here
                    return default(T);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/
    }
}
