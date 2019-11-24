
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFWaiverSnipe.Models
{
    public class Roster
    {
        public Roster(string json)
        {
            var jObject = JObject.Parse(json);
            var franchise = jObject["rosters"]["franchise"];

            var playerList = new List<Player>();

            if (franchise["player"] != null)
            {
                var players = franchise["player"].ToArray();

                if (players.Any())
                {
                    foreach (var player in players)
                    {
                        playerList.Add(new Player(player.ToString()));
                    }
                }
            }

            PlayerList = playerList;
        }
        public List<Player> PlayerList { get; set; }
    }
}
