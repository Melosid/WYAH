using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WYAH
{
    public class Response
    {
        public Dictionary<string, string> headers = new Dictionary<string, string>();
        public string body;
        public string status;

        public Response(string message)
        {
            var endOfHeader = message.IndexOf("\r\n\r\n") + 4;
            Regex newlineRegex = new Regex(@"(.*\r\n)");
            var m = newlineRegex.Match(message.Substring(0, endOfHeader));
            var responseLine = "";
            if (m.Success)
            {
                responseLine = m.Value;
                m = m.NextMatch();
            }
            Regex statusRegex = new Regex(@"HTTP\/\d\.\d (.*) ");
            var sm = statusRegex.Match(responseLine);
            if (sm.Success)
            {
                status = sm.Groups[1].Value;
            }
            Regex headerRegex = new Regex(@"(.*): (.*)");
            while (m.Success)
            {
                var headerMatch = headerRegex.Match(m.Value);
                if (headerMatch.Success)
                {
                    var key = headerMatch.Groups[1].Value;
                    var value = headerMatch.Groups[2].Value;
                    headers[key] = value;
                }
                m = m.NextMatch();
            }
            body = message.Substring(endOfHeader);
        }
    }
}
