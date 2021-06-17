﻿using System;
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
        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();
        public string Body { get; private set; }
    }
}
