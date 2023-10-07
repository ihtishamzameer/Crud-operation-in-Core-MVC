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
        public async Task<IActionResult> Add(AddEmployeeViewModel AddEmployeeRequest)
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

                await Mvcdemocontext.Employees.AddAsync(employee);
                await Mvcdemocontext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var employees = await Mvcdemocontext.Employees.ToListAsync();
            return View(employees);
        }       
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await Mvcdemocontext.Employees.FirstOrDefaultAsync(x => x.Id == id);
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
                return await Task.Run(()=>View("view",viewmodel)); 

            }
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await Mvcdemocontext.Employees.FindAsync(model.Id);
            if(employee!=null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Address = model.Address;
                employee.DateOfBirth = model.DateOfBirth;
                employee.department = model.department;
                employee.Salary = model.Salary;

                await Mvcdemocontext.SaveChangesAsync();

                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await Mvcdemocontext.Employees.FindAsync(model.Id);
            if(employee!=null)
            {
                Mvcdemocontext.Employees.Remove(employee);
                await Mvcdemocontext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

    }
}
