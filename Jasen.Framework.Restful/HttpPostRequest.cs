using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jasen.Framework.Restful
{
    public class HttpPostRequest : HttpFormRequestBase
    {
        public HttpPostRequest(string hostUrl)
            : base(hostUrl)
        {

        }

        public HttpPostRequest(string hostUrl, Encoding encoding)
             :base(hostUrl,encoding)
        {
        }

        public override HttpVerbs HttpVerbs
        {
            get
            {
                return HttpVerbs.Post;
            }
        }
    }
}
