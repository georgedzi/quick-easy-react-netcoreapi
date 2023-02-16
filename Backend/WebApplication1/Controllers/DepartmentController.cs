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
    }
}
