using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ECCHackaton24.Foundation.ServiceManager
{
    public static class HttpConnectionManager
    {
        public static Uri CreateEndpoint(string url)
        {
            Uri baseApiUri = null;

            if (Uri.TryCreate(url, UriKind.Absolute, out baseApiUri))
            {
                var uriBuilder = new UriBuilder(baseApiUri);

                return uriBuilder.Uri;
            }

            return baseApiUri;
        }
        public static HttpResponseMessage PerformGetSync(Uri endpoint, string keyName = null, string keyValue = null)
        {
            using (var httpClient = GetHttpClient(endpoint, keyName, keyValue))
            {                
                var response = httpClient.GetAsync(endpoint).ConfigureAwait(true).GetAwaiter().GetResult();
                return response;
            }
        }
        private static HttpClient GetHttpClient(Uri endpoint, string keyName = null, string keyValue = null)
        {
            var requestHandler = new WebRequestHandler();
            var httpClient = new HttpClient(requestHandler, true)
            {
                BaseAddress = endpoint
            };
            if (!String.IsNullOrEmpty(keyValue))
            {
                httpClient.DefaultRequestHeaders.Add(keyName, keyValue);
            }

            return httpClient;
        }
    }
}