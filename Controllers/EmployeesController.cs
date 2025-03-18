using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DefaultDbContext db;
        public EmployeesController(DefaultDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employeecs employeecs)
        {
            var existingEmployee = db.Employeecs.FirstOrDefault(e => e.EmployeeCode == employeecs.EmployeeCode);
            if (existingEmployee != null)
            {
                return BadRequest("Employee Code Already Exists");
            }
            if(employeecs.Age<18 || employeecs.Age > 50)
            {
                return BadRequest("Age must be between 18 and 50");
            }
            db.Employeecs.Add(employeecs);
            db.SaveChanges();
            return Ok(employeecs);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employe = db.Employeecs.
                Where(e => e.Id == id).FirstOrDefault();

            if (employe == null)
            {
                return NotFound("Employee Not found");
            }
            db.Employeecs.Remove(employe);
            db.SaveChanges();
            return Ok("Employee deleted");
        }
        [HttpGet("Getbyjoiningmonth")]
        public IActionResult GetEmployeesByJoiningMonth()
        {
            var currntmonth = DateTime.Now.Month;
            var employee = db.Employeecs.Where(e => e.JoiningDate.Month == currntmonth).ToList();
            return Ok(employee);
        }
        [HttpGet("GetAllWithTenure")]
        public IActionResult GetAllEmployeByTenure()
        {
            var employe = db.Employeecs.Select(e => new
            {
                e.Id,
                e.Name,
                e.Emailid,
                e.JoiningDate,
                e.Age,
                e.Salary,
                e.EmployeeCode,
                e.EmployeeType,
                TotalMonthsCompany = ((DateTime.Now.Year - e.JoiningDate.Year) * 12 + (DateTime.Now.Month - e.JoiningDate.Month))
               })
            .ToList();
            return Ok(employe);
        }
    }
}
