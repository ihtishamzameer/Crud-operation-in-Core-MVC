using Crud_Operations.Data;
using Crud_Operations.Models;
using Crud_Operations.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operations.Controllers
{
    public class EmployeesController : Controller
    {
        public EmployeesController(MVCDemoDbContext mvcdemocontext )
        {
            Mvcdemocontext = mvcdemocontext;
        }

        public MVCDemoDbContext Mvcdemocontext;


        [HttpGet]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel AddEmployeeRequest)
        {
            if (AddEmployeeRequest != null)
            {
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = AddEmployeeRequest.Name,
                    Email = AddEmployeeRequest.Email,
                    Salary = AddEmployeeRequest.Salary,
                    DateOfBirth = AddEmployeeRequest.DateOfBirth,
                    department = AddEmployeeRequest.department,
                    Address = AddEmployeeRequest.Address
                };

               Mvcdemocontext.Employees.Add(employee);
                Mvcdemocontext.SaveChanges();
            }

            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            var employees = Mvcdemocontext.Employees.ToList();
            return View(employees);
        }       
        [HttpGet]
        public IActionResult View(Guid id)
        {
            var employee = Mvcdemocontext.Employees.FirstOrDefault(x => x.Id == id);
            if(employee!=null)
            {

                var viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    department = employee.department,
                    DateOfBirth = employee.DateOfBirth
                };
                return View("view");

            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult View(UpdateEmployeeViewModel model)
        {
            var employee = Mvcdemocontext.Employees.Find(model.Id);
            if(employee!=null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Address = model.Address;
                employee.DateOfBirth = model.DateOfBirth;
                employee.department = model.department;
                employee.Salary = model.Salary;

               Mvcdemocontext.SaveChanges();

                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(UpdateEmployeeViewModel model)
        {
            var employee = Mvcdemocontext.Employees.Find(model.Id);
            if(employee!=null)
            {
                Mvcdemocontext.Employees.Remove(employee);
                Mvcdemocontext.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}
