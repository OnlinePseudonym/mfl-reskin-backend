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
using System.Collections.Generic;

namespace FFWaiverSnipe.Controllers
{
    public static class RosterController
    {
        [FunctionName("GetRoster")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string userId = req.Query["userid"].ToString();
            string leagueId = req.Query["leagueid"].ToString();
            string franchiseId = req.Query["franchiseid"].ToString();

            var endpoint = new MflEndpoint();
            var rosterEndpoint = new Uri($"{endpoint.Host}/{endpoint.Year}/export?TYPE=rosters&L={leagueId}&FRANCHISE={franchiseId}&JSON=1");

            string rosterResponse = await MflEndpointController.GetResponseString(rosterEndpoint, endpoint, userId);
            var roster = new Roster(rosterResponse);

            return userId != null
                ? (ActionResult)new OkObjectResult(roster.PlayerList)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
