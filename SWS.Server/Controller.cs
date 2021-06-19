using SWS.Server.Http;
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



    }
}
