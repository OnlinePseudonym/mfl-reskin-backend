using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFWaiverSnipe.Models
{
    public class Players
    {
        public Players(string json)
        {
            var jObject = JObject.Parse(json);
            var players = jObject["players"]["player"].ToArray();
            var playerList = new Dictionary<string, Player>();

            if (players.Any())
            {
                foreach (var player in players)
                {
                    playerList.Add(player["id"].ToString(), new Player(player.ToString()));
                }
            }

            PlayerList = playerList;
        }
        public Dictionary<string, Player> PlayerList { get; set; }
    }
}
