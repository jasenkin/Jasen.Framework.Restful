using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Jasen.Framework.Restful
{
    public interface IHttpFormParams
    {
        NameValueCollection FormParams { get; } 
    }
}
