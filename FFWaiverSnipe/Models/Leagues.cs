
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFWaiverSnipe.Models
{
    public class Leagues
    {
        public Leagues(string json)
        {
            var jObject = JObject.Parse(json);
            var leagues = jObject["leagues"]["league"].ToArray();
            var leagueList = new List<League>();

            if (leagues.Any())
            {
                foreach (var league in leagues)
                {
                    leagueList.Add(new League(league.ToString()));
                }
            }

            LeagueList = leagueList;
        }
        public List<League> LeagueList { get; set; }
    }
}
