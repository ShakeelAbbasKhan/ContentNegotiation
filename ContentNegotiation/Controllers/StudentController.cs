using ContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiation.Controllers
{
    public class StudentController : Controller
    {
        private List<Student> students;

        public StudentController()
        {
            // Initialize the list of students
            students = new List<Student>
            {
                new Student { Id = 1, FirstName = "Shakeel", LastName = "Abbas" },
                new Student { Id = 2, FirstName = "Bilal", LastName = "Asghar" },
                // Add more students as needed
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(students);
        }
    }
}
