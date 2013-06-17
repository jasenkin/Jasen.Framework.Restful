using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Jasen.Framework.Restful
{
    internal class AsyncRequestState
    {
        public IHttpRequestBase Request { get; set; }

        public HttpWebRequest HttpWebRequest { get; set; }

        public Action<string> AysncCallBack { get; set; }
    }
}
