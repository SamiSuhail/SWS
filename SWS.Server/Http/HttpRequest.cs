using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }
        public string Url { get; private set; }
        public Dictionary<string, HttpHeader> Headers { get; } = new Dictionary<string, HttpHeader>();
        public string Body { get; private set; }
    }
}
