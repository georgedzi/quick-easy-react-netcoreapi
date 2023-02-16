using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {

        private readonly mytestdbContext _dbContext;

        public DepartmentController(mytestdbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _dbContext.Departments.ToList();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post(Department Department)
        {
            _dbContext.Departments.Add(Department);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = Department.DepartmentId }, Department);
        }


        [HttpPut]
        public IActionResult Put(Department Department)
        {
            var existingDepartment = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == Department.DepartmentId);

            if (existingDepartment == null)
            {
                return NotFound();
            }

            _dbContext.Entry(existingDepartment).CurrentValues.SetValues(Department);

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int DepartmentId)
        {
            var existingDepartment = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == DepartmentId);

            if (existingDepartment == null)
            {
                return NotFound($"DepartmentId of {DepartmentId} was not found");
            }

            _dbContext.Remove(existingDepartment);
            _dbContext.SaveChanges();
            return NoContent();
        }

    }
}
