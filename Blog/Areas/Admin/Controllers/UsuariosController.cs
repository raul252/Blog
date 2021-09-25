﻿using Blog.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity) this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var usuarios = _contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value);

            return View(usuarios);
        }

        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.BloquearUsuario(id);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.DesbloquearUsuario(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
