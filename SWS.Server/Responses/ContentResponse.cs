using Common;
using SWS.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType) : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text, "Text Http Response Content");

            var textLength = Encoding.UTF8.GetByteCount(text);
            this.Headers.Add("Content-Length", $"{textLength}");
            this.Headers.Add("Content-Type", $"{contentType}; charset=UTF-8");
            this.Content = text;
        }
    }
}
