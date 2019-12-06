using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friedrich.Models
{
    public class User
    {
        public User()
        {
            Guid uid = Guid.NewGuid();
            this.Id = uid.ToString();
        }
        public String Card { get; set; }
        public String Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String RentedCar_id { get; set; }
        public String licenseNumber { get; set; }
    }
}