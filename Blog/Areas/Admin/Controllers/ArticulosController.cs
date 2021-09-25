using Blog.AccesoDatos.Data.Repository;
using Blog.Models.ViewModels;
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
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
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
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategories = _contenedorTrabajo.Category.GetListCategories()
            };

            return View(artivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artivm)
        {
            if (ModelState.IsValid)
            {
                string ruta = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (artivm.Articulo.Id == 0)
                {
                    if (archivos.Count() > 0)
                    {
                        //Nuevo articulo
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(ruta, @"imagenes\articulos");
                        var extension = Path.GetExtension(archivos[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStreams);
                        }
                        artivm.Articulo.UrlImage = @"imagenes\articulos\" + nombreArchivo + extension;
                        artivm.Articulo.CreatedAt = DateTime.Now.ToString();
                    }
                    
                    _contenedorTrabajo.Articulo.Add(artivm.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }

            artivm.ListaCategories = _contenedorTrabajo.Category.GetListCategories();

            return View(artivm);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategories = _contenedorTrabajo.Category.GetListCategories()
            };
            if (id != null)
            {
                artivm.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }

            return View(artivm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artivm)
        {
            if (ModelState.IsValid)
            {
                string ruta = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeDB = _contenedorTrabajo.Articulo.Get(artivm.Articulo.Id);

                if (archivos.Count() > 0)
                {
                    //Editar imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(ruta, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    if (articuloDesdeDB.UrlImage != null)
                    {
                        var rutaImagen = Path.Combine(ruta, articuloDesdeDB.UrlImage.TrimStart('\\'));

                        if (System.IO.File.Exists(rutaImagen))
                        {
                            System.IO.File.Delete(rutaImagen);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    artivm.Articulo.CreatedAt = DateTime.Now.ToString();
                    artivm.Articulo.UrlImage = @"imagenes\articulos\" + nombreArchivo + extension;
                } else
                {
                    if (artivm.Articulo.UrlImage != null)
                    {
                        artivm.Articulo.UrlImage = articuloDesdeDB.UrlImage;
                    }
                    artivm.Articulo.CreatedAt = articuloDesdeDB.CreatedAt;
                }
                _contenedorTrabajo.Articulo.update(artivm.Articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(artivm);
        }


        #region APICalls

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeDB = _contenedorTrabajo.Articulo.Get(id);

            string rutaDirectorioPrincipal = _hostingEnviroment.WebRootPath;

            string rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDB.UrlImage.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articuloDesdeDB == null)
            {
                return Json(new { success = false, message = "No existe el artículo" });
            }

            _contenedorTrabajo.Articulo.Remove(articuloDesdeDB);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Artículo borrado con éxito" });
        }

        [HttpGet]
        public IActionResult getAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(
                includeProperties: "Category"
            ) });
        }

        #endregion
    }
}
