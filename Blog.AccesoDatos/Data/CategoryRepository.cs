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
    class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListCategories()
        {
            return _db.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void update(Category category)
        {
            var objDB = _db.Category.FirstOrDefault(s => s.Id == category.Id);
            objDB.Name = category.Name;
            objDB.Orden = category.Orden;
            _db.SaveChanges();
        }
    }
}
