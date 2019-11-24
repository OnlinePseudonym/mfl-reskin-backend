using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FFWaiverSnipe.Models;

namespace FFWaiverSnipe.Controllers
{
    public static class PlayerController
    {
        [FunctionName("GetAllPlayers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            string userId = req.Query["userid"].ToString();

            var endpoint = new MflEndpoint();
            var playerEndpoint = new Uri($"{endpoint.Host}/{endpoint.Year}/export?TYPE=players&DETAILS=1&JSON=1");

            string playerResponse = await MflEndpointController.GetResponseString(playerEndpoint, endpoint, userId);

            var players = new Players(playerResponse);

            return userId != null
                ? (ActionResult)new OkObjectResult(players.PlayerList)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
