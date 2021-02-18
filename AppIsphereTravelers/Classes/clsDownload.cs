using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using AppIsphereTravelers.Models;

namespace AppIsphereTravelers.Classes
{
    public class clsDownload
    {
        public HttpClient _client = new HttpClient();
        private Label _label;
        public byte[] GetFile { get; private set; }
        public string MensagemErro { get; set; }

        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public clsDownload(Label label)
        {
            _label = label;
        }

        public string RemoveDiacritics(string text)
        {
            //if (text == null)
            //    return string.Empty;

            //byte[] tempBytes;
            //tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            //string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes, 0, tempBytes.Length);

            //return asciiStr;

            // Convert iso-8859-1 to utf-8.

            // C# strings are Unicode, so we must first get a byte
            // array containing iso-8859-1 bytes.
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
            System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;

            // This is our Unicode string:
            string s_unicode = text;

            // Convert it to iso-8859-1 bytes
            byte[] isoBytes = iso_8859_1.GetBytes(s_unicode);

            // Convert the bytes to utf-8:
            byte[] utf8Bytes = System.Text.Encoding.Convert(iso_8859_1, utf_8, isoBytes);

            return System.Text.Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);

            // Let's examine both byte arrays as hex strings.
            // We're using Chilkat.Crypt2 for simplicity...

            // The result is this:
            // isoBytes = 616263<strong>E9</strong>616263
            // utf8Bytes = 616263<strong>C3A9</strong>616263

            // The é character is 0xE9 in iso-8859-1, but is represented
            // as two bytes (0xC3 0xA9) in utf-8.

        }

        public async Task<UserLogin> Login(string login, string senha)
        {
            UserLogin Retorno = await DownloadPage<UserLogin>($"http://www.ispheretravelers.com/my-account/ws/login.php?login={login}&senha={senha}","", "Logando no sistema");

            return Retorno;
        }


        public async Task<T> DoSql<T>(string sql, string informacao = "")
        {
          //  CancellationToken c;


            return await DownloadPage<T>($"http://www.ispheretravelers.com/my-account/ws/ws_bd.php?sql={sql}", "", informacao);

            //return jSonText;
        }


        public async Task<T> DownloadPage<T>(string url, string parameters, string infor = "")
        {
            using (var client = new HttpClient())
            {
                var CharSetName = "UTF-8";
                client.DefaultRequestHeaders.Add("Accept-Charset", CharSetName);
                client.DefaultRequestHeaders.Add("Charset", CharSetName);



                if (_label != null)
                {
                    _label.Text = infor;
                }
                //System.Text.EncodingProvider provider = System.Text.i .CodePagesEncodingProvider.Instance;
                //Encoding.RegisterProvider(provider);
                //EncodingProvider provider = CodePagesEncodingProvider.Instance;

                HttpContent content = new StringContent(parameters);
                //content.Headers.Add("Accept-Charset", CharSetName);
                //content.Headers.Add("Charset", CharSetName);

                client.Timeout =TimeSpan.Parse("00.00:30:0");
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                //client.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
                //client.DefaultRequestHeaders.Add("Charset", "UTF-8");

                ////client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("UTF-8"));
                //using (var response = await client.PostAsync(url, content))

                using (var r = await client.PostAsync(new Uri(url), content))
                {
                    //r.Headers.Add("Accept-Charset", CharSetName);
                    //r.Headers.Add("Charset", CharSetName);

                    string result = "";
                    try
                    {
                        var Response = r.Content;
                        var byteArray = r.Content.ReadAsByteArrayAsync().Result;
                        result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);


                        /*result = await Response.ReadAsStringAsync();
                                ReadAsByteArrayAsync
var buffer = await r.Content.re.ReadAsBufferAsync();
                        var byteArray = buffer.ToArray();
                        var responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                        */
                    }
                    catch(Exception e)
                    {
                        result = e.Message;
                    }
                    
                    var jSonText = JsonConvert.DeserializeObject<T>(result);
                    if (_label != null)
                    {
                        _label.Text = "";
                    }
                    return jSonText;
                }



            }
        }
        public string ConvertToUTF8(string texto)
        {
            var utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(texto);
            texto = utf8.GetString(utfBytes, 0, utfBytes.Length);
            return texto;
        }
        public async Task<string> Upload(string url, string parameters, string infor = "")
        {
            using (var client = new HttpClient())
            {
                //request.Headers.Add("Accept-Charset", "UTF-8");
                //request.Headers.Add("Charset", "UTF-8");
                if (_label != null)
                {
                    _label.Text = infor;
                }
                HttpContent content = new StringContent(ConvertToUTF8(parameters));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                //client.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
                //client.DefaultRequestHeaders.Add("Charset", "UTF-8");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("UTF-8"));
                //using (var response = await client.PostAsync(url, content))

                using (var r = await client.PostAsync(new Uri(url), content))
                {
                    // r.Headers.Add("Accept-Charset", "UTF-8");
                    // r.Headers.Add("Charset", "UTF-8");

                    string result = await r.Content.ReadAsStringAsync();

                    return result;
                }
            }
        }

        public async Task<T> GetResponse<T>(string ApiUrl) where T : class
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(ApiUrl);
                var content = await response.Content.ReadAsStringAsync();
                var atividades = JsonConvert.DeserializeObject<T>(content);

                return atividades;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<string> DownloadFileGet(string url)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();

                response.Dispose();

                //string responseString = await client.GetStringAsync(url);

                return responseString;

                //var client = new System.Net.Http.HttpClient();
                //client.DefaultRequestHeaders.Add("Accept", "application/json");
                //var content = new StringContent(url);

                //var response = await client.PostAsync(url, content);

                //var result = (response.Content.ReadAsStringAsync().Result);
                ////ObservableCollection<Foundation> list = new ObservableCollection<Foundation>();
                ////list.Add(foundationobject);
                //return result;


                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = null;
                //response = await client.PostAsync(url, content);



            }
            catch (Exception e)
            {
                MensagemErro = e.Message;
                //Debug.LogWarning("File download got canceled.", "FileDownloader");
                return "";
            }
        }
        private string UTF8_to_ISO(string value)
        {

            Encoding Origem = Encoding.GetEncoding("ISO-8859-1");
            Encoding Destino = Encoding.GetEncoding("UTF-8");

            // Converte os bytes 
            byte[] bytesIso = Origem.GetBytes(value);

            //  Obtém os bytes da string UTF 
            byte[] bytesUtf = Encoding.Convert(Origem, Destino, bytesIso);

            // Obtém a string ISO a partir do array de bytes convertido
            string textoISO = Destino.GetString(bytesUtf, 0, bytesUtf.Length);

            return textoISO;

        }
        public async Task<string> DownloadFile(string url)
        {



            try
            {

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var response9 = await client.GetAsync(url);
                //var contentX = await response9.Content.ReadAsStringAsync();
                //return contentX;



                //var httpClient = new System.Net.Http.HttpClient(); // Error CS0246: The type or namespace name `HttpClient' could not be found. Are you missing an assembly reference? (CS0246)
                //this.GetFile = await httpClient.GetByteArrayAsync(uri);
                // var response = await client.GetByteArrayAsync(url);//.GetStringAsync(uri);
                //var retorno = Encoding.UTF8.GetString(response, 0,response.Length);
                //Encoding.Unicode.GetString(response, 0, response.Length - 1);
                //response = Encoding.UTF8.GetString(response.ToCharArray());

                //return retorno;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                //request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                //request.Headers.Add("Accept", "application/json");
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                //request.Headers.Add("Accept-Language", "pt-BR,pt;q=0.8,en-US;q=0.5,en;q=0.3");
                //request.Headers.Add("Connection", "keep-alive");
                //request.Headers.Add("DNT", "1");
                //request.Headers.Add("Host", "fidemcards.com");
                //request.Headers.Add("Upgrade-Insecure-Requests", "1");
                //request.Headers.Add("User-Agent", "	Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:56.0) Gecko/20100101 Firefox/56.0");
                //request.Headers.Add("Accept-Charset", "UTF-8");
                //request.Headers.Add("Charset", "UTF-8");

                /*
                //request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
                request.Headers.Add("Accept-Language", "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
                request.Headers.Add("Accept-Charset", "UTF8");
                (*/
                //HttpResponseMessage response = await client.get(request);

                //request.use .UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.116 Safari/537.36";
                //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                ////////request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
                ////////request.Headers.Add("Accept-Language", "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
                ////////request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");

                //request.Headers.Add("Accept-Ranges", "bytes");
                //request.Headers.Add("Age", 0);
                //request.Headers.Add("Cache-Control", "No-Cache");
                //request.Headers.Add("Content-Type", "application/json");
                //request.Headers.Add("Date", "Tue, 12 Dec 2017 00:49:07 GMT");
                //request.Headers.Add("Pragma", no - cache
                //request.Headers.Add("Server Apache
                //request.Headers.Add("Via 1.1 varnish - v4
                //request.Headers.Add("X - Varnish   23935491

                //request.Content.Headers.Add("Accept-Charset", "ISO-8859-1");
                //request.Content.Headers.Add("Charset", "ISO-8859-1");

                //var charset = "ISO-8859-1";

                //request.Headers.Add("Accept-Charset", charset);
                //request.Headers.Add("Charset", charset);

                //request.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue(charset));
                //client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue(charset));

                //request.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
                //client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));

                //request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                //client.DefaultRequestHeaders.Add ("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                ////request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"));
                ////client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"));


                ////client.DefaultRequestHeaders.
                ////.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //client.DefaultRequestHeaders.Add("Accept-Charset", charset);
                //client.DefaultRequestHeaders.Add("Charset", charset);



                client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0");
                client.DefaultRequestHeaders.Add("Referer", "http://urmia.divar.ir/browse/");
                client.DefaultRequestHeaders.Add("Pragma", "no-cache");
                //client.DefaultRequestHeaders.Add("Host", "urmia.divar.ir");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("charset", "ISO-8859-1");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.DefaultRequestHeaders.Add("Accept-Language", "pt-br,en;q=0.5");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");


                HttpResponseMessage response = await client.SendAsync(request);

                /*var contenttype = response.Content.Headers.First(h => h.Key.Equals("Content-Type"));
                var rawencoding = contenttype.Value.First();

               // if (rawencoding.Contains("utf8") || rawencoding.Contains("UTF-8"))
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                   // return Encoding.UTF8.GetString(bytes);
                }
                */

                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();

                response.Dispose();

                //string responseString = await client.GetStringAsync(url);
                //responseString = UTF8_to_ISO(responseString);

                return responseString;

                //var client = new System.Net.Http.HttpClient();
                //client.DefaultRequestHeaders.Add("Accept", "application/json");
                //var content = new StringContent(url);

                //var response = await client.PostAsync(url, content);

                //var result = (response.Content.ReadAsStringAsync().Result);
                ////ObservableCollection<Foundation> list = new ObservableCollection<Foundation>();
                ////list.Add(foundationobject);
                //return result;


                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = null;
                //response = await client.PostAsync(url, content);



            }
            catch (Exception e)
            {
                MensagemErro = e.Message;
                //Debug.LogWarning("File download got canceled.", "FileDownloader");
                return "";
            }
        }



    }
}
