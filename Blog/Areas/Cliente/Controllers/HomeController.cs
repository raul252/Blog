using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IContenedorTrabajo contenedorTrabajo)
        {
            _logger = logger;
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            HomeVM homevm = new HomeVM()
            {
                listaSliders = _contenedorTrabajo.Slider.GetAll(),
                listaArticulos = _contenedorTrabajo.Articulo.GetAll()
            };
            return View(homevm);
        }

        public IActionResult Details(int id)
        {
            var articuloDesdeDB = _contenedorTrabajo.Articulo.GetFirstOrDefault(a=> a.Id == id);

            return View(articuloDesdeDB);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
