using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly mytestdbContext _dbContext;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(mytestdbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var data = _dbContext.Employees.ToList();
            return new JsonResult(data);
        }

        [HttpPost]
        public JsonResult Post(Employee Employee)
        {
            _dbContext.Employees.Add(Employee);
            _dbContext.SaveChanges();
            return new JsonResult($"Added {Employee} successfully");
        }


        [HttpPut]
        public JsonResult Put(Employee Employee)
        {
            var existingEmployee = _dbContext.Employees.FirstOrDefault(d => d.EmployeeId == Employee.EmployeeId);

            if (existingEmployee == null)
            {
                 return new JsonResult($"No such Employee with Id: {Employee.EmployeeId} was found");
            }

            _dbContext.Entry(existingEmployee).CurrentValues.SetValues(Employee);

            _dbContext.SaveChanges();

            return new JsonResult($"Updated {Employee}");
        }

        [HttpDelete]
        public JsonResult Delete(int EmployeeId)
        {
            var existingEmployee = _dbContext.Employees.FirstOrDefault(d => d.EmployeeId == EmployeeId);

            if (existingEmployee == null)
            {
                return new JsonResult($"Employee with Id {EmployeeId} was not found");
            }

            _dbContext.Remove(existingEmployee);
            _dbContext.SaveChanges();
            return new JsonResult($"Deleted Employee with id: {EmployeeId}");
        }


        [Route("UploadFile")]
        [HttpPost]
        public JsonResult UploadFile (IFormFile input)
        {
            var physicalPath = _env.ContentRootPath + "/Photos/" + input.FileName;

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                input.CopyTo(stream);
            }

            return new JsonResult($"Added {input.FileName}");
        }

    }
}
