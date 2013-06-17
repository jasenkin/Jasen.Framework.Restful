using System; 
using System.Net;
using System.IO; 

namespace Jasen.Framework.Restful
{
    public class WebInvoker
    { 
        public static bool Get(HttpGetRequest request, out string result)
        {
            return Invoke(request, out result);
        }

        public static bool Delete(HttpDeleteRequest request, out string result)
        {
            return Invoke(request, out result);
        }

        public static bool Put(HttpPutRequest request, out string result)
        {
            return Invoke(request, out result);
        }

        public static bool Post(HttpPostRequest request, out string result)
        {
            return Invoke(request, out result);
        } 

        public static bool Invoke(IHttpRequestBase request, out string result)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            result = string.Empty;

            byte[] contents;
            bool requestState = Invoke(request, out contents);

            if (contents != null && contents.Length > 0)
            {
                result = request.Encoding.GetString(contents);
            }

            return requestState;
        }

        public static bool Get(HttpGetRequest request, out byte[] result)
        {
            return Invoke(request, out result);
        }

        public static bool Delete(HttpDeleteRequest request, out byte[] result)
        {
            return Invoke(request, out result);
        }

        public static bool Put(HttpPutRequest request, out byte[] result)
        {
            return Invoke(request, out result);
        }

        public static bool Post(HttpPostRequest request, out byte[] result)
        {
            return Invoke(request, out result);
        } 

        public static bool Invoke(IHttpRequestBase request, out byte[] result)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            result = null;

            try
            {
                var webRequest = WebRequest.Create(request.GetRequestUrl()) as HttpWebRequest;

                if (webRequest == null)
                {
                    return false;
                }

                SetHttpWebRequest(request, webRequest);
                SetRequestStream(request, webRequest);

                return GetResponse(webRequest, out result);
            }
            catch (Exception ex)
            {
                // log the exception...
                return false; 
            }
        }

        public static bool AsyncDelete(HttpDeleteRequest request, Action<string> callBack)
        {
            return AsyncInvoke(request, callBack);
        }

        public static bool AsyncPut(HttpPutRequest request, Action<string> callBack)
        {
            return AsyncInvoke(request, callBack);
        }

        public static bool AsyncPost(HttpPostRequest request, Action<string> callBack)
        {
            return AsyncInvoke(request, callBack);
        } 

        public static bool AsyncInvoke(IHttpRequestBase request, Action<string> callBack)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.HttpVerbs == HttpVerbs.Get)
            {
                throw new ArgumentException("Async Invoke don't provide HttpVerbs.Get Method.");
            }

            try
            {
                var webRequest = WebRequest.Create(request.GetRequestUrl()) as HttpWebRequest;

                if (webRequest == null)
                {
                    return false;
                }

                SetHttpWebRequest(request, webRequest);

                var requestState = new AsyncRequestState();
                requestState.Request = request;
                requestState.HttpWebRequest = webRequest;
                requestState.AysncCallBack = callBack;
                webRequest.BeginGetRequestStream(GetRequestStreamCallback, requestState);

                return true;
            }
            catch (Exception ex)
            {
                // log the exception...
                return false; 
            }
        }

        private static bool GetResponse(HttpWebRequest webRequest, out byte[] content)
        {
            content = null;

            using (var webResponse = webRequest.GetResponse() as HttpWebResponse)
            {
                if (webResponse == null || webResponse.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                using (var responseStream = webResponse.GetResponseStream())
                {
                    if (responseStream == null)
                    {
                        return false;
                    }

                    var memoryStream = new MemoryStream();
                    responseStream.CopyTo(memoryStream);
                    content = memoryStream.ToArray();

                    return true;
                }
            }
        }

        private static void SetRequestStream(IHttpRequestBase request, HttpWebRequest webRequest)
        {
            if (request.HttpVerbs != HttpVerbs.Get)
            {
                var requestBytes = request.GetRequestBytes();

                if (requestBytes != null && requestBytes.Length > 0)
                {
                    using (var requestStream = webRequest.GetRequestStream())
                    {
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                    }
                }
            }
        }

        private static void SetHttpWebRequest(IHttpRequestBase request, HttpWebRequest webRequest)
        {
            webRequest.Timeout = request.Timeout;
            webRequest.AllowAutoRedirect = false;
            webRequest.Method = request.HttpVerbs.ToString().ToUpper();
            webRequest.ContentType = request.ContentType;

            if (request.Cookies.Count > 0)
            {
                webRequest.CookieContainer = new CookieContainer();
                webRequest.CookieContainer.Add(request.Cookies);
            }
        }

        private static void GetRequestStreamCallback(IAsyncResult asyncResult)
        {
            var requestState = asyncResult.AsyncState as AsyncRequestState;

            if (requestState.Request.HttpVerbs != HttpVerbs.Get)
            {
                var requestBytes = requestState.Request.GetRequestBytes();

                if (requestBytes != null && requestBytes.Length > 0)
                {
                    using (var requestStream = requestState.HttpWebRequest.EndGetRequestStream(asyncResult))
                    {
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                    }
                }
            }

            requestState.HttpWebRequest.BeginGetResponse(GetResponseCallback, requestState);
        }

        private static void GetResponseCallback(IAsyncResult asyncResult)
        {
            var requestState = asyncResult.AsyncState as AsyncRequestState;

            using (var response = requestState.HttpWebRequest.EndGetResponse(asyncResult) as HttpWebResponse)
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    if (requestState.AysncCallBack != null)
                    { 
                        requestState.AysncCallBack.Invoke(streamReader.ReadToEnd());
                    }
                }
            }
        }
    }
}
