using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly mytestdbContext _dbContext;

        public EmployeeController(mytestdbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _dbContext.Employees.ToList();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post(Employee Employee)
        {
            _dbContext.Employees.Add(Employee);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = Employee.EmployeeId }, Employee);
        }


        [HttpPut]
        public IActionResult Put(Employee Employee)
        {
            var existingEmployee = _dbContext.Employees.FirstOrDefault(d => d.EmployeeId == Employee.EmployeeId);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            _dbContext.Entry(existingEmployee).CurrentValues.SetValues(Employee);

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int EmployeeId)
        {
            var existingEmployee = _dbContext.Employees.FirstOrDefault(d => d.EmployeeId == EmployeeId);

            if (existingEmployee == null)
            {
                return NotFound($"EmployeeId of {EmployeeId} was not found");
            }

            _dbContext.Remove(existingEmployee);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
