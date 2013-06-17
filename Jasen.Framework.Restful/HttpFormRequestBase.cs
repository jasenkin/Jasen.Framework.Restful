using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace Jasen.Framework.Restful
{
    public abstract class HttpFormRequestBase : RequestBase, IHttpFormParams
    { 
         public HttpFormRequestBase(string hostUrl)
            : this(hostUrl, Encoding.UTF8)
        {
        }

         public HttpFormRequestBase(string hostUrl, Encoding encoding)
             :base(hostUrl,encoding)
        {
        }

        public NameValueCollection FormParams
        {
            get;
            private set;
        }
         

        public override byte[] GetRequestBytes()
        {
            string requestBytes = GetRequestParams(this.FormParams);

            if (string.IsNullOrWhiteSpace(requestBytes))
            {
                return null;
            }

            return this.Encoding.GetBytes(requestBytes);
        }

        private string GetRequestParams(NameValueCollection collection)
        {
            if (collection != null && collection.Count > 0)
            {
                var builder = new StringBuilder();

                foreach (var key in collection.AllKeys)
                {
                    builder.Append(HttpUtility.UrlEncode(key, this.Encoding));
                    builder.Append("=");
                    builder.Append(HttpUtility.UrlEncode(collection[key], this.Encoding));
                    builder.Append("&");
                }

                return builder.Remove(builder.Length - 1, 1).ToString();
            }

            return string.Empty;
        }
    }
}
