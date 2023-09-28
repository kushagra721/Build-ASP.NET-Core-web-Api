using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.data;

namespace NZwalks.API.Controllers
{
    [Route("data/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        

       
      
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            String[] students = new String[] { "kushagra", "rahul", "atul", "tanuj", "tushaR", "VARUN", "tatta patel" };

          

            return Ok(students);
        }
    }
}
