using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public class HttpHelper
    {
        #region Private Field
        private static readonly HttpHelper instance = new HttpHelper();
        private static HttpClient httpClient;
        private static HttpClientHandler handler;
        #endregion


        #region Public Filed

        public event EventHandler TokenExpired;

        #endregion

        public static Dictionary<string, string> Headers = new Dictionary<string, string>();

        private HttpHelper()
        {
            Initialize();
        }


        #region Public Function

        public static HttpHelper GetInstance()
        {
            //if (NetworkUtils.HaveNetworkAccess)
            //{
            //    return instance;
            //}
            //else
            //{
            //    //throw new Exception("Seems unable to connect to the network.");
            //}
            return instance;
        }

        public static void CancelPendingRequests()
        {
            httpClient.CancelPendingRequests();
        }




        /// <summary>
        /// Initialize
        /// Initial the IMEI and HttpClient
        /// Disable Redirect
        /// </summary>
        public void Initialize()
        {
            handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;
            handler.UseProxy = true;
            httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);
        }


        public void EnableProxy(bool userProxy)
        {
            handler.UseProxy = userProxy;
        }


        public void SetProxy(string server, int port)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get Http Request
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>String</returns>
        public async Task<string> HttpGetRequest(string url)
        {
            string result = String.Empty;
            try
            {
                var res = await httpClient.GetAsync(url);
                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TokenExpired?.Invoke(this, null);
                }
                if (res.Content.Headers.ContentEncoding.Any((c) => c.ToLower().Contains("gzip")))
                {
                    using (var stream = new GZipStream(await res.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = await reader.ReadToEndAsync();
                        }
                    }
                }
                else
                    result = await res.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return result;
        }


        /// <summary>
        /// Get Http Request
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>String</returns>
        public async Task<string> HttpGetRequest(string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);
            string request = url;
            string response = await HttpGetRequest(request);
            return response;
        }


        /// <summary>
        /// Post Http Request
        /// </summary>
        /// <param name="url">Request Url</param>
        /// <param name="contentStr">Post Content</param>
        /// <returns></returns>
        public async Task<string> HttpPostRequest(string url, string content)
        {
            string result = string.Empty;
            try
            {
                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(url, httpContent);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TokenExpired?.Invoke(this, null);
                }
                if (httpResponseMessage.Content.Headers.ContentEncoding.Any((c) => c.ToLower().Contains("gzip")))
                {
                    using (var stream = new GZipStream(await httpResponseMessage.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = await reader.ReadToEndAsync();
                        }
                    }
                }
                else
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// Post Http Request
        /// </summary>
        /// <param name="url">Request Url</param>
        /// <param name="contentStr">Post Content</param>
        /// <returns></returns>
        public async Task<string> HttpPostRequest(string url, Stream content)
        {
            string result = string.Empty;
            try
            {
                var multi = new MultipartFormDataContent();
                //var dic = new Dictionary<string, string>();
                //var key = $"Content-Length:{content.Length}\r\nContent-Type:image/*\r\ncontent-disposition:form-data;name:\"picture\";filename:\"avatar.img\"";
                //dic.Add("Content-Length", content.Length.ToString());
                //dic.Add("Content-Type", "image/*");
                //dic.Add("content-disposition", "form-data");
                //dic.Add("name", "picture");
                //dic.Add("filename", "avatar.img");
                //multi.Add(new StringContent("content-disposition=form-data&name=picture&filename=avatar.img"));
                multi.Add(new StreamContent(content), Uri.EscapeUriString("content-disposition=form-data&name=\"picture\"&filename=\"avatar.img\""));

                HttpContent httpContent = multi;
                //httpClient.Headers.ContentType = new MediaTypeHeaderValue("image/*");
                //httpContent.Headers.ContentLength = content.Length;
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(url, httpContent);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TokenExpired?.Invoke(this, null);
                }
                if (httpResponseMessage.Content.Headers.ContentEncoding.Any((c) => c.ToLower().Contains("gzip")))
                {
                    using (var stream = new GZipStream(await httpResponseMessage.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = await reader.ReadToEndAsync();
                        }
                    }
                }
                else
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// Delete Http Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> HttpDeleteRequest(string url, Dictionary<string, string> headers = null)
        {
            string result = string.Empty;
            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(url);
                if (httpResponseMessage != null && httpResponseMessage.Content != null)
                {
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TokenExpired?.Invoke(this, null);
                }
                if (httpResponseMessage.Content.Headers.ContentEncoding.Any((c) => c.ToLower().Contains("gzip")))
                {
                    using (var stream = new GZipStream(await httpResponseMessage.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = await reader.ReadToEndAsync();
                        }
                    }
                }
                else
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return result;
        }

        public async Task<string> HttpPostRequest(string url, string content, Dictionary<string, string> headers = null)
        {
            string result = string.Empty;
            try
            {
                AddHeader(headers);
                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(url, httpContent);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TokenExpired?.Invoke(this, null);
                }
                if (httpResponseMessage.Content.Headers.ContentEncoding.Any((c) => c.ToLower().Contains("gzip")))
                {
                    using (var stream = new GZipStream(await httpResponseMessage.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = await reader.ReadToEndAsync();
                        }
                    }
                }
                else
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return result;
        }


        public async Task<string> HttpPutRequest(string url, string content, Dictionary<string, string> headers = null)
        {
            string result = string.Empty;
            try
            {
                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(url, httpContent);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TokenExpired?.Invoke(this, null);
                }
                if (httpResponseMessage.Content.Headers.ContentEncoding.Any((c) => c.ToLower().Contains("gzip")))
                {
                    using (var stream = new GZipStream(await httpResponseMessage.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = await reader.ReadToEndAsync();
                        }
                    }
                }
                else
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return result;
        }


        /// <summary>
        /// Get Http Request and Converter to T
        /// </summary>
        /// <typeparam name="T">Type you want to converter</typeparam>
        /// <param name="url">Request url</param>
        /// <param name="headers">Request headers</param>
        /// <returns>Type you want</returns>
        public async Task<T> HttpGetRequest<T>(string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);
            string request = url;
            string response = await HttpGetRequest(request);

            T result;
            if (!string.IsNullOrEmpty(response))
            {
                T t = await Task.Factory.StartNew<T>(() => JsonHelper.JsonDeserilize<T>(response));
                result = t;
            }
            else
            {
                result = default(T);
            }
            return result;
        }



        /// <summary>
        /// Post Http Request
        /// </summary>
        /// <typeparam name="T">Type you want to converter</typeparam>
        /// <param name="url">Post Url</param>
        /// <param name="content">Post Content</param>
        /// <param name="headers">Post Headers</param>
        /// <returns>Type you want</returns>
        public async Task<T> HttpPostRequest<T>(string url, string content, Dictionary<string, string> headers = null) where T : class
        {
            AddHeader(headers);
            string contentStr = content;
            string response = await HttpPostRequest(url, contentStr);
            T result;
            if (!string.IsNullOrEmpty(response))
            {
                result = await Task.Factory.StartNew<T>(() => JsonHelper.JsonDeserilize<T>(response));
            }
            else
            {
                result = default(T);
            }
            return result;
        }


        /// <summary>
        /// Delete Http Rquest
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<T> HttpDeleteRequest<T>(string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);
            var response = await HttpDeleteRequest(url);

            T result;
            if (!string.IsNullOrEmpty(response))
            {
                result = await Task.Factory.StartNew<T>(() => JsonHelper.JsonDeserilize<T>(response));
            }
            else
            {
                result = default(T);
            }
            return result;

        }

        public async Task<T> HttpPutRequest<T>(string url, string content, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);
            string contentStr = content;
            string response = await HttpPutRequest(url, contentStr);
            T result;
            if (!string.IsNullOrEmpty(response))
            {
                result = await Task.Factory.StartNew<T>(() => JsonHelper.JsonDeserilize<T>(response));
            }
            else
            {
                result = default(T);
            }
            return result;
        }


        #endregion


        private void AddHeader(Dictionary<string, string> headers)
        {
            if (httpClient != null && httpClient.DefaultRequestHeaders != null)
            {
                httpClient.DefaultRequestHeaders.Clear();
                AddDefultHeaders();
            }
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    if (httpClient.DefaultRequestHeaders.Contains(item.Key))
                        httpClient.DefaultRequestHeaders.Remove(item.Key);
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
        }

        private void AddDefultHeaders()
        {
           
            foreach (var item in Headers)
            {
                httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }
    }
}
