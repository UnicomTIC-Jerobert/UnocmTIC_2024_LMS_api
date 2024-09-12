using LMS.rest_api.DTOs;
using LMS.rest_api.Repositories;
using LMS.rest_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("{empId}")]
        public IActionResult GetById(int empId)
        {
            var employee = _employeeRepository.GetById(empId);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Dob = employeeDto.Dob
            };
            _employeeRepository.Add(employee);
            return CreatedAtAction(nameof(GetById), new { empId = employee.EmpId }, employee);
        }

        [HttpPut("{empId}")]
        public IActionResult Update(int empId, EmployeeDto employeeDto)
        {
            var employee = _employeeRepository.GetById(empId);
            if (employee == null)
                return NotFound();

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Dob = employeeDto.Dob;
            _employeeRepository.Update(employee);

            return NoContent();
        }

        [HttpDelete("{empId}")]
        public IActionResult Delete(int empId)
        {
            var employee = _employeeRepository.GetById(empId);
            if (employee == null)
                return NotFound();

            _employeeRepository.Delete(empId);
            return NoContent();
        }
    }
}
