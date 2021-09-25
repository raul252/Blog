using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo,
            IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;
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
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string ruta = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                    if (archivos.Count() > 0)
                    {
                        //Nuevo articulo
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(ruta, @"imagenes\sliders");
                        var extension = Path.GetExtension(archivos[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStreams);
                        }
                        slider.UrlImagen = @"imagenes\sliders\" + nombreArchivo + extension;
                    }

                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Slider slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                if (slider != null)
                {
                    return View(slider);
                }
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string ruta = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeDB = _contenedorTrabajo.Slider.Get(slider.Id);

                if (archivos.Count() > 0)
                {
                    //Editar imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(ruta, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    if (sliderDesdeDB.UrlImagen != null)
                    {
                        var rutaImagen = Path.Combine(ruta, sliderDesdeDB.UrlImagen.TrimStart('\\'));

                        if (System.IO.File.Exists(rutaImagen))
                        {
                            System.IO.File.Delete(rutaImagen);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    slider.UrlImagen = @"imagenes\sliders\" + nombreArchivo + extension;
                } else
                {
                    if (slider.UrlImagen != null)
                    {
                        slider.UrlImagen = sliderDesdeDB.UrlImagen;
                    }
                }
                _contenedorTrabajo.Slider.update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        #region APICalls

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderDesdeDB = _contenedorTrabajo.Slider.Get(id);

            string rutaDirectorioPrincipal = _hostingEnviroment.WebRootPath;

            string rutaImagen = Path.Combine(rutaDirectorioPrincipal, sliderDesdeDB.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (sliderDesdeDB == null)
            {
                return Json(new { success = false, message = "No existe el slider" });
            }

            _contenedorTrabajo.Slider.Remove(sliderDesdeDB);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Slider borrado con éxito" });
        }

        [HttpGet]
        public IActionResult getAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        #endregion
    }
}
