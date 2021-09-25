using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AccesoDatos.Data
{
    class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);

            usuarioDesdeDB.LockoutEnd = DateTime.Now.AddYears(100);
            _db.SaveChanges();

        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);

            usuarioDesdeDB.LockoutEnd = DateTime.Now;
            _db.SaveChanges();

        }
    }
}
