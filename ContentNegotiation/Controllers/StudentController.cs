using ContentNegotiation.Data;
using ContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiation.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {

            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 60, VaryByHeader = "Accept")]
        public IActionResult GetStudentsList()
        {
            var students = _db.Students.ToList();
            return Ok(students);
        }

        [HttpGet]
        [ResponseCache(Duration = 60, VaryByHeader = "Accept")]
        public IActionResult GetStudentsData()
        {
            var students = _db.Students.ToList();
            var isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxRequest)
            {
                return Ok(students);
                //return PartialView("_GetStudents", students);
            }
            else
            {
                return View(students);
            }
        }

        [HttpGet]
        public IActionResult GetStudents()
        {

            var students = _db.Students.ToList();
            var isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxRequest)
            {
                
                return PartialView("_GetStudents", students);
            }
            else
            {
                return View(students);
            }
        }


    }
}
