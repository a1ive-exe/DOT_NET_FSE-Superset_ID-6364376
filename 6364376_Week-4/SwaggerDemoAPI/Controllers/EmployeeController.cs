using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerDemoAPI.Models;

namespace SwaggerDemoAPI.Controllers
{
    [Authorize(Roles = "Admin,POC")] // ✅ Enforces JWT role check
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employeeList = new();

        public EmployeeController()
        {
            if (!employeeList.Any())
            {
                employeeList = GetStandardEmployeeList();
            }
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John",
                    Salary = 50000,
                    Permanent = true,
                    Department = new Department { DepartmentId = 1, DepartmentName = "HR" },
                    Skills = new List<Skill>
                    {
                        new Skill { SkillId = 1, SkillName = "Communication" },
                        new Skill { SkillId = 2, SkillName = "Leadership" }
                    },
                    DateOfBirth = new DateTime(1990, 01, 01)
                }
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        public ActionResult<List<Employee>> GetStandard()
        {
            throw new Exception("Testing custom exception handling");
            // return Ok(employeeList); // Unreachable after throw
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            employeeList.Add(employee);
            return Ok("Employee added.");
        }

        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updated)
        {
            if (id <= 0)
                return BadRequest("Invalid employee id");

            var emp = employeeList.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return BadRequest("Invalid employee id");

            emp.Name = updated.Name;
            emp.Salary = updated.Salary;
            emp.Permanent = updated.Permanent;
            emp.Department = updated.Department;
            emp.Skills = updated.Skills;
            emp.DateOfBirth = updated.DateOfBirth;

            return Ok(emp);
        }
    }
}
