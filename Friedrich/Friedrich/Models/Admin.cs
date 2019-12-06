using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friedrich.Models
{
    public class Admin
    {
        public Admin()
        {
            Guid g = Guid.NewGuid();
            this.Id = g.ToString();
        }
        public String Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
    }
}