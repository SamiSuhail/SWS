using Common;
using SWS.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string textHtml) : base(textHtml, "text/html")
        {
            
        }
    }
}
