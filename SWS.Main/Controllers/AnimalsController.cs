using SWS.Server.Http;
using SWS.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Controllers
{
    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            var catName = "all of our cats";


            if (request.Query != null && request.Query.ContainsKey("Name"))
            {
                catName = request.Query["Name"];
            }

            return new HtmlResponse($"<h1>Welcome to the Cats page, you are currently looking at {catName}!</h1>");
        }

        public HttpResponse Dogs()
        {
            var dogName = "all of our dogs";


            if (request.Query != null && request.Query.ContainsKey("Name"))
            {
                dogName = request.Query["Name"];
            }

            return new HtmlResponse($"<h1>Welcome to the Cats page, you are currently looking at {dogName}!</h1>");
        }
    }
}
