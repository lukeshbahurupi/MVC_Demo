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
        [ActionName("Create")]
        public ActionResult Create_Get()
        {           
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(Employee employee)
        {

            #region 
            //Employee employee = new Employee()
            //{
            //    Name = value["Name"],
            //    Gender = value["Gender"],
            //    City = value["City"],
            //    Salary = Convert.ToDecimal(value["Salary"]),
            //    DateOfBirth = Convert.ToDateTime(value["DateOfBirth"])
            //};
            #endregion
            //Employee employee = new Employee();

            //TryUpdateModel<Employee>(employee);
            if (ModelState.IsValid)
            {
                Method.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            Employee employee = Method.GetAllEmployess().FirstOrDefault(el => el.ID == id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee value)
        {
            if (ModelState.IsValid)
            {
                //Employee value = new Employee();

                //UpdateModel<Employee>(value,new string[] {"Name"});//include
                //UpdateModel<Employee>(value,null,null,new string[] {"Name"});//exclude

                Method.UpdateEmployee(value);
                return RedirectToAction("Index");
            }
            return View();
            }

        public ActionResult Details(int id)
        {
            return View(Method.GetEmployeeDetails(id));
            //return View(Method.GetAllEmployess().FirstOrDefault(el => el.ID == id));
        }

        public  ActionResult Delete(int id)
        {           
            return View(Method.GetEmployeeDetails(id));          
        }
        
        public  ActionResult GetDelete(int id)
        {
            Method.DeleteEmployee(id);
            return RedirectToAction("Index");          
        }
    }
}