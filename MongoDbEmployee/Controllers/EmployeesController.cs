using Microsoft.AspNetCore.Mvc;
using MongoDbEmployee.Models;
using MongoDbEmployee.Services;
using System.Collections.Generic;

namespace MongoDbEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get() =>
            _employeeService.Get();

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public ActionResult<Employee> Get(string id)
        {
            var emp = _employeeService.Get(id);

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }

        [HttpPost]
        public IActionResult Post(Employee newEmployee)
        {
             _employeeService.Create(newEmployee);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Employee updatedEmployee)
        {
            var employee =  _employeeService.Get(id);

            if (employee is null)
            {
                return NotFound();
            }

            updatedEmployee.Id = employee.Id;

             _employeeService.Update(id, updatedEmployee);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            var employee =  _employeeService.Get(id);

            if (employee is null)
            {
                return NotFound();
            }

             _employeeService.Remove(id);

            return NoContent();
        }

    }
}
