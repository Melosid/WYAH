using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYAH
{
    public class Request
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        Dictionary<string, string> queryParams = new Dictionary<string, string>();

        string host;
        string url;
        const string method = "GET";
        const string version = "1.0";

        public Request(string url, string host)
        {
            this.url = url;
            this.host = host;
        }

        public void addHeader(string key, string value)
        {
            headers[key] = value;
        }

        public void addQueryParam(string key, string value)
        {
            queryParams[key] = value;
        }

        public string serialize()
        {
            String headersStr = "";
            foreach (KeyValuePair<string, string> entry in headers)
            {
                headersStr += entry.Key + ": " + entry.Value + "\r\n";
            }
            string path = url;
            if (queryParams.Count > 0)
            {
                url += "?";
                foreach (KeyValuePair<string, string> entry in queryParams)
                {
                    url += entry.Key + "=" + entry.Value;
                }
            }
            var requestLine = method + " " + path + " " + "HTTP/" + version + "\r\n";
            var message = requestLine + headersStr + "\r\n";

            return message;
        }
    }
}
