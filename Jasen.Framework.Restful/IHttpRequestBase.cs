using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Jasen.Framework.Restful
{
    public interface IHttpRequestBase
    {
        string HostUrl { get;}
        HttpVerbs HttpVerbs { get; }
        CookieCollection Cookies { get; }
        NameValueCollection QueryStringParams { get; } 

        string ContentType { get; set; }
        Encoding Encoding { get; set; }
        int Timeout { get; set; }
         
        string GetRequestUrl();
        byte[] GetRequestBytes();
    }
}
