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
        public JsonResult Get()
        {
            var data = _dbContext.Departments.ToList();
            return new JsonResult(data);
        }

        [HttpPost]
        public JsonResult Post(Department Department)
        {
            _dbContext.Departments.Add(Department);
            _dbContext.SaveChanges();
            return new JsonResult($"Added {Department} successfully");
        }


        [HttpPut]
        public JsonResult Put(Department Department)
        {
            var existingDepartment = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == Department.DepartmentId);

            if (existingDepartment == null)
            {
                return new JsonResult($"No such department with Id: {Department.DepartmentId} was found");
            }

            _dbContext.Entry(existingDepartment).CurrentValues.SetValues(Department);

            _dbContext.SaveChanges();

            return new JsonResult($"Updated {Department}");
        }

        [HttpDelete]
        public JsonResult Delete(int DepartmentId)
        {
            var existingDepartment = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == DepartmentId);

            if (existingDepartment == null)
            {
                return new JsonResult($"Department with Id {DepartmentId} was not found");
            }

            _dbContext.Remove(existingDepartment);
            _dbContext.SaveChanges();
            return new JsonResult($"Deleted Department with id: {DepartmentId}");
        }

    }
}
