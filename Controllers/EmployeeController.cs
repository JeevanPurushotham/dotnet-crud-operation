using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var allEmployee = dbContext.Employeess.ToList();
            return Ok(allEmployee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeedto addEmployeedto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeedto.Name,
                Email = addEmployeedto.Email,
                Phone = addEmployeedto.Phone,
                Salary = addEmployeedto.Salary
            };

            dbContext.Employeess.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEMployeeById(Guid id)
        {
          var employee = dbContext.Employeess.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult updateEMployess(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
           
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employeess.Find(id);
            if (employee == null)
            {
                return NotFound();
            }        var employee = dbContext.Employeess.Find(id);
        if (employee == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrEmpty(updateEmployeeDto.Name))
        {
            employee.Name = updateEmployeeDto.Name;
        }

        if (!string.IsNullOrEmpty(updateEmployeeDto.Email))
        {
            employee.Email = updateEmployeeDto.Email;
        }

        if (!string.IsNullOrEmpty(updateEmployeeDto.Phone))
        {
            employee.Phone = updateEmployeeDto.Phone;
        }

        dbContext.SaveChanges();
        return Ok(employee);
            dbContext.Employeess.Remove(employee);
            dbContext.SaveChanges();
            return Ok("Delete is Done");
        }
    }
}
