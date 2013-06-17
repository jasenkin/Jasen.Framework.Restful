using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Jasen.Framework.Restful
{
    public class HttpGetRequest : RequestBase
    {
        public HttpGetRequest(string hostUrl)
            : base(hostUrl)
        {
        
        }

        public HttpGetRequest(string hostUrl, Encoding encoding)
             :base(hostUrl,encoding)
        {
        }

        public override HttpVerbs HttpVerbs
        {
            get
            {
                return HttpVerbs.Get;
            }
        }

        public override byte[] GetRequestBytes()
        {
            return null;
        }
    }
}
