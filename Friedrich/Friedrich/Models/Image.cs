using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friedrich.Models
{
    public class Image
    {
        public Image()
        {
            Guid g = Guid.NewGuid();
            this.id = g.ToString();
        }
        public String id { get; set; }
        public String model_id { get; set; }
        public String path { get; set; }
        public static List<Image> Images(String img, String car_id)
        {
            List<Image> imgs = new List<Image>();
            var pathes = img.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var path in pathes)
            {
                Image image = new Image();
                image.path = path;
                image.model_id = car_id;
                imgs.Add(image);
            }
            return imgs;
        }
    }
}