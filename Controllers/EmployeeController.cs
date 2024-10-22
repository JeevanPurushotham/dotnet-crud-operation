using AutoMapper;
using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;


        public EmployeeController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
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
            // Check if the employee with the same email or phone number already exists
            var existingEmployee = dbContext.Employeess
                .FirstOrDefault(e => e.Email == addEmployeedto.Email || e.Phone == addEmployeedto.Phone);

            if (existingEmployee != null)
            {
                return BadRequest("An employee with the same email or phone number already exists.");
            }
            //var employeeEntity = _mapper.Map<Employee>(addEmployeedto);

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
        public IActionResult UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            // Find the employee by the provided id
            var employee = dbContext.Employeess.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with id {id} not found"); // Better logging for debugging
            }
            _mapper.Map(updateEmployeeDto, employee);


            // Save the changes in the database
            dbContext.SaveChanges();

            return Ok(employee); // Return the updated employee

        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            // Find the employee by ID
            var employee = dbContext.Employeess.Find(id);

            // If the employee is not found, return a 404 NotFound response
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            // Remove the employee from the database
            dbContext.Employeess.Remove(employee);

            // Save the changes to the database
            dbContext.SaveChanges();

            // Return a success message or the deleted employee object (if needed)
            return Ok($"Employee with ID {id} has been deleted successfully.");
        }
    }

}
