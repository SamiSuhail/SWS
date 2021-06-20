using SWS.Server.Http;
using SWS.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Controllers
{
    public abstract class Controller
    {
        protected readonly HttpRequest request;
        protected Controller(HttpRequest request)
            => this.request = request;

        protected HttpResponse Text(string text) => new TextResponse(text);
        protected HttpResponse Html(string html) => new HtmlResponse(html);
        protected HttpResponse Redirect(string location) => new RedirectResponse(location);

    }
}
