using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AU.Common.Utility
{
    public static class HttpHelper
    {
        ///// <summary>
        ///// Send a GET request to the specified resource URL and return and object of type T
        ///// </summary>
        public static T SendGetRequest<T>(string url)
        {
            WebClient wc = new WebClient();
            wc.UseDefaultCredentials = true;
            string response = wc.DownloadString(url);
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(response, typeof(T));

            return JsonConvert.DeserializeObject<T>(response);
        }

        /// <summary>
        /// Download a binary file at the specified URL and return a byte array
        /// </summary>
        public static byte[] DownloadBinaryFile(string resourceUrl)
        {
            var filePath = string.Format("{0}", DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ff"));

            DownloadBinaryFile(resourceUrl, filePath);

            byte[] file = File.ReadAllBytes(filePath);
            File.Delete(filePath);

            return file;
        }

        /// <summary>
        /// Download a binary file at the specified URL and save it to the destination path
        /// </summary>
        public static void DownloadBinaryFile(string resourceUrl, string destinationFilePath)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(resourceUrl, destinationFilePath);
            }
        }

        /// <summary>
        /// Send a POST request with object of type T in the body and return true if response status code = 200
        /// </summary>
        public static bool SendPostRequest<T>(string resourceUrl, T objectToPost, string encodingName = "utf-8")
        {
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(new Uri(resourceUrl));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            string parsedContent = JsonConvert.SerializeObject(objectToPost);

            Encoding encoding = Encoding.GetEncoding(encodingName);
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)http.GetResponse();
            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Send a POST request with object of type J in the body and return and object of type T
        /// </summary>
        public static T SendPostRequest<T, J>(string resourceUrl, J objectToPost, string encodingName = "utf-8")
        {
            var http = (HttpWebRequest)WebRequest.Create(new Uri(resourceUrl));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            string parsedContent = JsonConvert.SerializeObject(objectToPost);
            Encoding encoding = Encoding.GetEncoding(encodingName);
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(content);
        }


        public static T SendGetRequest<T>(string resourceUrl, out HttpStatusCode code, int timeOut = 5000)
        {
            var http = (HttpWebRequest)WebRequest.Create(new Uri(resourceUrl));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "GET";
            http.Timeout = timeOut;
            string content = string.Empty;
            try
            {
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)http.GetResponse();
                code = response.StatusCode;

                //response.StatuCode
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                content = sr.ReadToEnd();
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.Timeout)
                    code = HttpStatusCode.GatewayTimeout;
                else
                    code = ((System.Net.HttpWebResponse)we.Response).StatusCode;
                content = we.Message;
            }
            catch (Exception e)
            {
                code = HttpStatusCode.GatewayTimeout;
                content = e.Message;
            }

            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(content, typeof(T));

            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Get a valid url for the provided base url and query string. Prevents issues when the trailing slash is missing.
        /// </summary>
        public static string GetUrl(string baseUrl, string queryString)
        {
            if (string.IsNullOrEmpty(baseUrl))
                return string.Empty;

            var uriBuilder = new UriBuilder(baseUrl);
            uriBuilder.Query = queryString;
            return uriBuilder.ToString();
        }

        /// <summary>
        /// Get a valid url for the provided base url, path, and query string. Prevents issues when the trailing slash is missing.
        /// </summary>
        public static string GetUrl(string baseUrl, string path, string queryString)
        {
            if (string.IsNullOrEmpty(baseUrl))
                return string.Empty;

            var uriBuilder = new UriBuilder(baseUrl);
            uriBuilder.Path = path;
            uriBuilder.Query = queryString;
            return uriBuilder.ToString();
        }





        ///// <summary>
        ///// post请求
        ///// </summary>
        ///// <param name="url">服务端api地址</param>
        ///// <param name="verb">控制器地址</param>
        ///// <param name="requestJson">请求参数</param>
        ///// /// <returns></returns>
        //public static string HttpPost(string url, string verb, string requestJson)
        //{
        //    HttpClient client = null;
        //    bool isUrl = string.IsNullOrEmpty(url);
        //    if (KeepAlive && isUrl)
        //    {
        //        client = _client;
        //    }
        //    else
        //    {
        //        client = new HttpClient();
        //        client.Timeout = TimeSpan.FromMilliseconds(TimeOut);
        //        client.BaseAddress = new Uri(new Uri(isUrl ? ApiUrl : url), "Api/");
        //    }
        //    if (IsAuthentication)
        //    {
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetAccessToken(url));
        //    }
        //    HttpContent content = new StringContent(requestJson);
        //    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //    string result = "";
        //    try
        //    {
        //        HttpResponseMessage hrm = client.PostAsync(verb, content).Result;
        //        result = hrm.Content.ReadAsStringAsync().Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (log.IsErrorEnabled)
        //            log.Error("Post请求 [" + verb + "] 参数 [" + requestJson + "] 失败", ex);
        //    }

        //    return result;
        //}
        ///// <summary>
        ///// Get 请求
        ///// </summary>
        ///// <param name="url">服务器地址</param>
        ///// <param name="verb">请求地址</param>
        ///// <returns>请求结果</returns>
        //public static string HttpGet(string url, string verb)
        //{
        //    HttpClient client = null;
        //    bool isUrl = string.IsNullOrEmpty(url);
        //    if (KeepAlive && isUrl)
        //    {
        //        client = _client;
        //    }
        //    else
        //    {
        //        client = new HttpClient();
        //        client.Timeout = TimeSpan.FromMilliseconds(TimeOut);
        //        client.BaseAddress = new Uri(new Uri(isUrl ? ApiUrl : url), "Api/");
        //    }
        //    if (IsAuthentication)
        //    {
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetAccessToken(url));
        //    }
        //    string result = "";
        //    try
        //    {
        //        HttpResponseMessage hrm = client.GetAsync(verb).Result;
        //        result = hrm.Content.ReadAsStringAsync().Result;
        //    }
        //    catch (Exception err)
        //    {
        //        if (log.IsErrorEnabled)
        //            log.Error("Get请求 [" + verb + "] ", err);
        //    }

        //    return result;
        //}
    }
}
