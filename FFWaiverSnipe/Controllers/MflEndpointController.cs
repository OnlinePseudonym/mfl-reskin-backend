using FFWaiverSnipe.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FFWaiverSnipe.Controllers
{
    public static class MflEndpointController
    {
        public static async Task<string> GetResponseString(Uri uri, MflEndpoint endpoint, string userId, Dictionary<string, string> cookies = null)
        {
            if (cookies == null)
            {
                cookies = new Dictionary<string, string>()
                {
                    { "MFL_USER_ID", userId }
                };
            }
            else
            {
                cookies.Add("MFL_USER_ID", userId);
            }

            try
            {
                var cookieContainer = new CookieContainer();

                using (var httpClientHandler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer
                })
                {
                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        AddCookies(cookieContainer, new Uri(endpoint.Host), cookies);

                        var response = await httpClient.GetAsync(uri);

                        if (response != null)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            return jsonString;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetResponseString: ", ex);
            }

            return null;
        }

        private static void AddCookies(CookieContainer cookieContainer, Uri domain, Dictionary<string, string> cookies)
        {
            foreach (var cookieValues in cookies)
            {
                var cookie = new Cookie(cookieValues.Key, cookieValues.Value);
                cookieContainer.Add(domain, cookie);
            }
        }
    }
}
