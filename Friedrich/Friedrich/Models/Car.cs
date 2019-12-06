using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friedrich.Models
{
    public class Car
    {
        public Car()
        {
            Guid g = Guid.NewGuid();
            this.id = g.ToString();
            ImagesPath = new List<Image>();
        }
        public String id { get; set; }
        public String brand_id { get; set; }
        public Brand brand { set; get; }
        public int year { get; set; }
        public int price { get; set; }
        public bool available { get; set; }
        public int mileAge { get; set; }
        public String Fuel {get; set;}
        public List<Image> ImagesPath { get; set; }
    }
}