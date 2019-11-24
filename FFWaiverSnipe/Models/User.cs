using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FFWaiverSnipe.Models
{
    public class User
    {
        public string LeagueId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Year { get; set; }
    }
}
