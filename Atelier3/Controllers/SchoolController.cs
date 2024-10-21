using Atelier3.Models;
using Atelier3.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Atelier3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class SchoolController : Controller
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }
        [AllowAnonymous]

        // GET: SchoolController/Index
        public IActionResult Index()
        {
            var schools = _schoolRepository.GetAll();
            return View(schools);
        }

        // GET: SchoolController/Details/5
        public IActionResult Details(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // GET: SchoolController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(School school)
        {
            try
            {
                _schoolRepository.Add(school);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(school);
            }
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int id)
        {
            var school = _schoolRepository.GetById(id);
            return View(school);
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, School school)
        {
            try
            {
                _schoolRepository.Edit(school);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Delete/5
        public IActionResult Delete(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(School school)
        {
            _schoolRepository.Delete(school);
            return RedirectToAction(nameof(Index));
        }
    }
}