using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFWaiverSnipe.Models
{
    public class Player
    {
        public Player()
        {
        }
        public Player(string json)
        {
            var jObject = JObject.Parse(json);

            Name = (string)jObject["name"];
            Id = (int)jObject["id"];
            Team = (string)jObject["team"];
            Position = (string)jObject["position"];
            StatsGlobalId = (string)jObject["stats_global_id"];
            StatsId = (string)jObject["stats_id"];
            SportsDataId = (string)jObject["sportsdata_id"];
            Status = (string)jObject["status"];
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public string StatsGlobalId { get; set; }
        public string StatsId { get; set; }
        public string SportsDataId { get; set; }
        public string Status { get; set; }
    }
}
