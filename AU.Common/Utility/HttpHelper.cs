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
        public static bool SendPostRequest<T>(string resourceUrl, T objectToPost)
        {
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(new Uri(resourceUrl));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            string parsedContent = JsonConvert.SerializeObject(objectToPost);
            ASCIIEncoding encoding = new ASCIIEncoding();
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
        public static T SendPostRequest<T, J>(string resourceUrl, J objectToPost)
        {
            var http = (HttpWebRequest)WebRequest.Create(new Uri(resourceUrl));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            string parsedContent = JsonConvert.SerializeObject(objectToPost);
            ASCIIEncoding encoding = new ASCIIEncoding();
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
    }
}
