using Friedrich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Friedrich.Controllers
{
    public class CarPageController : Controller
    {
        // GET: CarPage
        public ActionResult Rent(String id)
        {
            //var carId = Request.Url.Query.Substring(6);
            DataHelper data = new DataHelper();
            var car = data.GetCar(id);
            ViewBag.Car = car;
            return View();
        }
    }
}