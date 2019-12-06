using Friedrich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Friedrich.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public  Car car;
        [HttpGet]
        public ActionResult Pay(String id)
        {
            DataHelper data = new DataHelper();
            this.car = data.GetCar(id);
            ViewBag.Car = this.car;
            return View();
        }
        [HttpPost]
        public ActionResult PayLog(User user)
        {
            DataHelper data = new DataHelper();

            if(data.Contains(user))
            {
                data.UserPay(user);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User user)
        {
            DataHelper data = new DataHelper();
            if(data.AddUser(user))
            {
                data.UserPay(user);
            }
            return View();
        }
    }
}