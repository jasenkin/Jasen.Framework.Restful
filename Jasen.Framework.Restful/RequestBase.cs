using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Net;

namespace Jasen.Framework.Restful
{
    public abstract class RequestBase : IHttpRequestBase
    {
        private string _contentType = null;
        private int _timeOut = 10000;

        public RequestBase(string hostUrl)
            : this(hostUrl, Encoding.UTF8)
        {
        }

        public RequestBase(string hostUrl, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(hostUrl))
            {
                throw new ArgumentNullException("hostUrl");
            }

            this.Encoding = encoding ?? Encoding.UTF8;
            this.HostUrl = hostUrl.Trim(); 
            this.QueryStringParams = new NameValueCollection();
            this.Cookies = new CookieCollection();
        }

        public abstract HttpVerbs HttpVerbs 
        { 
            get; 
        }

        public string HostUrl 
        {
            get; 
            private set; 
        } 
         
        public string ContentType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._contentType))
                {
                    if (this.HttpVerbs == HttpVerbs.Post || this.HttpVerbs == HttpVerbs.Put
                        || this.HttpVerbs == HttpVerbs.Delete)
                    {
                        this._contentType = "application/x-www-form-urlencoded";
                    }
                }

                return this._contentType;
            }
            set
            {
                this._contentType = value;
            }
        }
         
        public Encoding Encoding
        { 
            get; 
            set;
        }
         
        public int Timeout
        {
            get
            {
                return this._timeOut;
            }
            set
            {
                if(value>=0)
                {
                    this._timeOut = value;
                }       
            }
       }

        public CookieCollection Cookies 
        {
            get; 
            private set;
        }

        public NameValueCollection QueryStringParams 
        { 
            get; 
            private set; 
        } 

        public string GetRequestUrl()
        {
            if (this.QueryStringParams != null && this.QueryStringParams.Count > 0)
            {
                if (!this.HostUrl.EndsWith("?"))
                {
                    this.HostUrl += "?";
                }

                return this.HostUrl + this.GetRequestParams(this.QueryStringParams);
            }

            return this.HostUrl;
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
                    builder.Append(HttpUtility.UrlEncode(collection[key],this.Encoding));
                    builder.Append("&");
                } 

                return builder.Remove(builder.Length - 1, 1).ToString(); 
            }

            return string.Empty;
        }

        public abstract byte[] GetRequestBytes();
    }
}
