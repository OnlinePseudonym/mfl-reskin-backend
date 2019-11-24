using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FFWaiverSnipe.Models;
using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

namespace FFWaiverSnipe.Controllers
{
    public static class LeagueController
    {
        [FunctionName("GetLeagues")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string userId = req.Query["userid"].ToString();
            var endpoint = new MflEndpoint();
            var leaguesEndpoint = new Uri($"{endpoint.Host}/{endpoint.Year}/export?TYPE=myleagues&FRANCHISE_NAMES=1&JSON=1");

            string leaguesResponse = MflEndpointController.GetResponseString(leaguesEndpoint, endpoint, userId).Result; ;

            var leagues = new Leagues(leaguesResponse);

            return userId != null
                ? (ActionResult)new OkObjectResult(leagues.LeagueList)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
