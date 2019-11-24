using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using FFWaiverSnipe.Models;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FFWaiverSnipe.Controllers
{
    public static class LoginController
    {

        [FunctionName("Login")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            /* Set up http client to retrieve cookies */
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            var authClient = new HttpClient(handler);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<User>(requestBody);

            if (data == null || data.Username == null || data.Password == null)
            {
                var responseObject = new Dictionary<string, object>();

                responseObject.Add("errorMessage", "Please pass a valid object to endpoint with username and password");

                return new BadRequestObjectResult(responseObject) ;
            }

            var endpoint = new MflEndpoint();
            var user = new User()
            {
                Username = data.Username
            };

            var loginUrl = new Uri($"{endpoint.Host}/{endpoint.Year}/login?USERNAME={user.Username}&PASSWORD={data.Password}&XML=1");

            var loginResponse = await authClient.GetAsync(loginUrl);
            var authCookie = cookies.GetCookies(loginUrl).Cast<Cookie>().FirstOrDefault(x => x.Name == "MFL_USER_ID");

            user.UserId = authCookie?.Value;

            if (user.UserId == null)
            {
                log.LogError($"Authentication failed for: {user.Username}");

                var responseObject = new Dictionary<string, object>();

                responseObject.Add("errorMessage", "Authentication failed please check credentials and try again");

                return new ObjectResult(user) { StatusCode = StatusCodes.Status403Forbidden };
            }

            return new OkObjectResult(user);
        }
    }
}
