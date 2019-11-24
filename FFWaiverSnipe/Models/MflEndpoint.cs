using System;
using System.Collections.Generic;
using System.Text;

namespace FFWaiverSnipe.Models
{
    public class MflEndpoint
    {
        public string Host { get; set; } = "https://api.myfantasyleague.com";
        public bool IsJson { get; set; } = true;
        public string Year { get; set; } = DateTime.Now.Year.ToString();
    }
}
