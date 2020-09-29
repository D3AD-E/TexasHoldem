using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemServer.DAL.Models
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public double Money { get; set; }

        public User()
        {
            Money = 1000;
        }
    }
}
