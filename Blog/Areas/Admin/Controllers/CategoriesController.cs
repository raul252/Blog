using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriesController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Category.Add(category);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _contenedorTrabajo.Category.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Category.update(category);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region APICalls

        [HttpGet]
        public IActionResult getAll()
        {
            return Json(new { data = _contenedorTrabajo.Category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoryDB = _contenedorTrabajo.Category.Get(id);

            if (categoryDB == null)
            {
                return Json(new { success = false, message = "Error borrando categoría" });
            }

            _contenedorTrabajo.Category.Remove(categoryDB);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Categoría borrada con éxito" });
        }
        #endregion
    }
}
