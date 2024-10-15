using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPICRUD.Data;
using RestAPICRUD.Models;
using RestAPICRUD.Models.Entity;
using System;
using System.Xml;

namespace RestAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;

        public EmployeesController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            /*var allEmployees = await dBContext.Employees.ToListAsync();*/

            return Ok(await dBContext.Employees.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAllEmployee(Guid id)
        {
            var Employee = await dBContext.Employees.FindAsync(id);

            if (Employee is null)
            {
                return NotFound("Employee not found");
            }
                
            return Ok(Employee);

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync(AddEmployeeDto employeeDto)
        {
            var empEntity = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Salary = employeeDto.Salary
            };

            dBContext.Employees.Add(empEntity);
            await dBContext.SaveChangesAsync();

            return Ok($"{empEntity.Name} Record Added");
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, AddEmployeeDto addEmployee) 
        {
            var emp = await dBContext.Employees.FindAsync(id);
            if(emp is null)
            {
                return NotFound("Invalid id employee not ound");
            }

            emp.Name = addEmployee.Name;
            emp.Email = addEmployee.Email;
            emp.Phone = addEmployee.Phone;
            emp.Salary = addEmployee.Salary;
            await dBContext.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var emp = await dBContext.Employees.FindAsync(id);
            if (emp is null)
            {
                return NotFound("Invalid id, employee not found");
            }
            dBContext.Employees.Remove(emp);
            await dBContext.SaveChangesAsync();
            return Ok("Record Deleted");
        }



    }
}
