using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasen.Framework.Restful
{
    public class HttpDeleteRequest : HttpFormRequestBase
    {
        public HttpDeleteRequest(string hostUrl)
            : base(hostUrl)
        {

        }

        public HttpDeleteRequest(string hostUrl, Encoding encoding)
             :base(hostUrl,encoding)
        {
        }

        public override HttpVerbs HttpVerbs
        {
            get
            {
                return HttpVerbs.Delete;
            }
        }
    }
}
