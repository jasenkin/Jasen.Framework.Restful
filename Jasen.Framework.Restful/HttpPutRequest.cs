using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasen.Framework.Restful
{
    public class HttpPutRequest : HttpFormRequestBase
    {
        public HttpPutRequest(string hostUrl)
            : base(hostUrl)
        {

        }

        public HttpPutRequest(string hostUrl, Encoding encoding)
             :base(hostUrl,encoding)
        {
        }

        public override HttpVerbs HttpVerbs
        {
            get
            {
                return HttpVerbs.Put;
            }
        }
    }
}
