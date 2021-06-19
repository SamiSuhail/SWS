using SWS.Server.Http;
using SWS.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index()
        {
            return new HtmlResponse("<h1>Welcome to the home page</h1>");
        }
    }
}
