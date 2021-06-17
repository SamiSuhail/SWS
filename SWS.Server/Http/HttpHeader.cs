using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Http
{
    public class HttpHeader
    {
        public string Name { get; init; }
        public string Value { get; init; }

        public static HttpHeader Parse(string header)
        {
            var headerMembers = header.Split(":", 2);
            var name = headerMembers[0].Trim();
            var value = headerMembers[1].Trim();

            return new HttpHeader() { Name = name, Value = value };
        }
    }
}
