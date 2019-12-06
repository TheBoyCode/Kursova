using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friedrich.Models
{
    public class Brand
    {
        public Brand()
        {
            Guid g = Guid.NewGuid();
            this.Id = g.ToString();
        }
        public String Id { get; set; }
        public String Model { get; set; } 
        public String Company { get; set; }
        public bool Premium { get; set; }
    }
}