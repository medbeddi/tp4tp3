using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Atelier3.Models;
using Atelier3.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Atelier3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ISchoolRepository schoolRepository;

        public StudentController(IStudentRepository studentRepository, ISchoolRepository schoolRepository)
        {
            this.studentRepository = studentRepository;
            this.schoolRepository = schoolRepository;
        }
        [AllowAnonymous]
        // GET: StudentController
        public IActionResult Index()
        {
            var students = studentRepository.GetAll();
            if (students == null)
            {
                return View(new List<Student>());
            }

            return View(students);
        }
        // GET: SchoolController/Details/5
        public IActionResult Details(int id)
        {
            var students = studentRepository.GetById(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            // Remplir la liste déroulante avec les écoles
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                studentRepository.Add(student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
                return View(student);
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = studentRepository.GetById(id);
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Student newStudent)
        {
            try
            {
                studentRepository.Edit(newStudent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student); // Affichage de la vue de confirmation de suppression
        }

        // POST: StudentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var student = studentRepository.GetById(id);
                if (student != null)
                {
                    studentRepository.Delete(student);
                    return RedirectToAction(nameof(Index));
                }

                return NotFound();
            }
            catch (Exception)
            {
                return View(); // En cas d'erreur
            }
        }

        // Méthode pour la recherche d'étudiants
        public ActionResult Search(string name, int? schoolid)
        {
            var result = studentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = studentRepository.FindByName(name);
            else if (schoolid != null)
                result = studentRepository.GetStudentsBySchoolID(schoolid);

            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }
    }
}
