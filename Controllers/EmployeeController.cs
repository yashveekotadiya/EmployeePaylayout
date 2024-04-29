using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrud.Models;
using EmployeeCrud.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeCrud.Controllers
{
    // [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDesignationRepository _designationRepository;


        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository, IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var employee = _employeeRepository.GetAllEmployee();
            return View(employee);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            // if (ModelState.IsValid)
            // {
                _employeeRepository.InsertEmployee(employee);
                return RedirectToAction("Index");
            // }else{
            //     return View();
            // }
            // return RedirectToAction("AddEmployee");
        }

        // public IActionResult AllDesignation()
        // {
        //     var designations = _employeeRepository.GetAllDesignation();
        //     ViewBag.Designations = designations;
        //     return View();
        // }



        // Inside your controller

        // public IActionResult PayRoll(int id)
        // {
        //     // Update payroll for the specified employee
        //         // Update payroll for the specified employee
        //     _employeeRepository.UpdateEmployeePayroll(id);

        //     // Retrieve the updated employee details
        //     Employee updatedEmployee = _employeeRepository.GetOneEmployee(id);

        //     return View(updatedEmployee);
        // }


        public IActionResult PayRoll(int id)
        {
            // Update payroll for the specified employee
            _employeeRepository.UpdateEmployeePayroll(id);

            // Retrieve the updated employee details
            Employee updatedEmployee = _employeeRepository.GetOneEmployee(id);

            return View(updatedEmployee);
        }





        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetOneEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetOneEmployee(id);
            _employeeRepository.DeleteEmployee(employee);
            return RedirectToAction("Index");
        }


        public IActionResult GetDesignations()
        {
            return Json(_designationRepository.GetDesignations());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}