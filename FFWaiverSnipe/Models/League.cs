using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace FFWaiverSnipe.Models
{
    public class League
    {
        public League(string json)
        {
            var jObject = JObject.Parse(json);

            var url = (string)jObject["url"];

            FranchiseId = (string)jObject["franchise_id"];
            Url = url;
            Name = (string)jObject["name"];
            FranchiseName = (string)jObject["franchise_name"];

            if (int.TryParse(url.Split(new char[] { '/' }).LastOrDefault(), out int leagueId)) {
                LeagueId = leagueId;
            };
        }

        public string FranchiseId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string FranchiseName { get; set; }
        public int LeagueId { get; set; }
    }
}
