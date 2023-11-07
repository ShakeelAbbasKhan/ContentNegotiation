using ContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiation.Controllers
{
    public class StudentController : Controller
    {
        private List<Student> students;

        public StudentController()
        {
            students = new List<Student>
            {
                new Student { Id = 1, FirstName = "Shakeel", LastName = "Abbas" },
                new Student { Id = 2, FirstName = "Bilal", LastName = "Asghar" },
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 60, VaryByHeader = "Accept")]
        public IActionResult GetStudentsData()
        {
            return Ok(students);
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxRequest)
            {
                return PartialView("_GetStudents", students);
                //_GetStudents   _PartialView
            }
            else
            {
                return View(students);
            }

            return Ok(students);
        }

    }
}
