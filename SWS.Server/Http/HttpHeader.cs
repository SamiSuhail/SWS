using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Http
{
    public class HttpHeader
    {
        public HttpHeader(string name, string value)
        {
            Guard.AgainstNull(name, "Header Name");
            Guard.AgainstNull(value, "Header Value");

            this.Name = name;
            this.Value = value;
        }
        public string Name { get; init; }
        public string Value { get; init; }

        public static HttpHeader Parse(string header)
        {
            var headerMembers = header.Split(":", 2);
            var name = headerMembers[0].Trim();
            var value = headerMembers[1].Trim();
            Guard.AgainstNull(name, "Header Name");
            Guard.AgainstNull(value, "Header Value");

            return new HttpHeader(name, value);
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}
