using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.db.DbOperations;

namespace MyAppWithDB.Controllers
{
    public class HomeController : Controller
    {

        EmployeeRepository repository = null;

        public HomeController()
        {
            repository = new EmployeeRepository();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                int id=repository.AddEmployee(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = "Data Added";
                }
                
            }

            return View();
        }

        public ActionResult GetAllRecords()
        {
            var result = repository.GetAllEmployees();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var employee = repository.GetEmployee(id);
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            var employee = repository.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var emp = repository.UpdateEmployee(model.Id, model);
                return RedirectToAction("GetAllRecords");
            }
            return View();

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
           bool res= repository.DeleteEmployee(id);
            if (res)
            {
                return RedirectToAction("GetAllRecords");
            }
            return RedirectToAction("Create");
           
        }
    }

}