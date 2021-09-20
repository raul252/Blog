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
    class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Articulo articulo)
        {
            var objDB = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDB.Name = articulo.Name;
            objDB.Descripcion = articulo.Descripcion;
            objDB.CreatedAt = articulo.CreatedAt;
            objDB.UrlImage = articulo.UrlImage;
            objDB.CategoryId = articulo.CategoryId;
            _db.SaveChanges();
        }
    }
}
