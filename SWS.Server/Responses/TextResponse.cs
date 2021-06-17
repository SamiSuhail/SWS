using SWS.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Responses
{
    public class TextResponse : HttpResponse
    {
        public TextResponse(string text) : base(HttpStatusCode.OK)
        {
            var textLength = Encoding.UTF8.GetByteCount(text);
            this.Headers.Add("Content-Length", $"{textLength}");
            this.Headers.Add("Content-Type", "text/html; charset=UTF-8");
        }
    }
}
