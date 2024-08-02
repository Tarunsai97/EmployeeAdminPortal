using EmployeeAdminPortal.Data;
using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase {

        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();

            return Ok(allEmployees);
        }

        [HttpPost]

        public IActionResult AddEmployee([FromBody] EmployeeDTO employee)
        {
            var newEmployee = new Employee()
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                salary = employee.salary
            };
            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();
            return Ok(newEmployee);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, [FromBody] EmployeeDTO employee)
        {
            var existingEmployee = dbContext.Employees.Find(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.salary = employee.salary;
            dbContext.SaveChanges();
            return Ok(existingEmployee);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}
