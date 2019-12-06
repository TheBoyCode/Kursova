using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Friedrich.Models;

namespace Friedrich.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            DataHelper data = new DataHelper();
            var cars = data.GetCars();
            ViewBag.Cars = cars;
            return View();
        }
    }
}