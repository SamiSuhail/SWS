using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Http
{
    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            headers = new Dictionary<string, HttpHeader>();
        }

        public int Count => headers.Count;

        public void Add(HttpHeader header)
            => this.headers.Add(header.Name, header);

        public static HttpHeaderCollection Parse(IEnumerable<string> headerAndBodyLines)
        {

            var collection = new HttpHeaderCollection();

            foreach (var line in headerAndBodyLines)
            {
                if (line == String.Empty)
                {
                    break;
                }

                var header = HttpHeader.Parse(line);
                collection.Add(header);
            }

            return collection;

        }
    }
}
