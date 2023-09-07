using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace MVC_Demo.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeBusinessLayer Method { get; set; }
        public EmployeeController()
        {
            Method = new EmployeeBusinessLayer();
        }
        // GET: Employee
        public ActionResult Index()
        {           
            List<Employee> employees = Method.GetAllEmployess();
            return View(employees);
        }
        [HttpGet]
        public ActionResult Create()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee value)
        {
            //Employee employee = new Employee()
            //{
            //    Name = value["Name"],
            //    Gender = value["Gender"],
            //    City  = value["City"],
            //    Salary = Convert.ToDecimal(value["Salary"]),
            //    DateOfBirth = Convert.ToDateTime(value["DateOfBirth"])               
            //};
            Method.AddEmployee(value);
            return RedirectToAction("Index");
        }
    }
}