using Friedrich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Friedrich.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public bool Admin;
        [HttpGet]
        public ActionResult Index()
        {
            Admin = false;
            return View();
        }
        [HttpPost]
        public ActionResult Panel(Admin admin)
        {
            DataHelper data = new DataHelper();
            if(data.ContainsAdmin(admin))
            {
                var cars = data.GetCars();
                ViewBag.CarsAdmin = cars;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(String id)
        {
            DataHelper data = new DataHelper();
            data.Delete(id);
            return View();
        }
        public ActionResult Edit(String id)
        {
            DataHelper data = new DataHelper();
            var car = data.GetCar(id);
            ViewBag.EditCar = car;
            return View();
        }
        public ActionResult Change(Car car)
        {
            DataHelper data = new DataHelper();
            data.UPDATE(car);
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Insert(Car car, String path)
        {
            DataHelper data = new DataHelper();
            car.ImagesPath = Image.Images(path,car.id);
            if(car.price==0)
            {
                car.available = false;
            }
            data.INSERT(car);
            return View();
        }
    }
}