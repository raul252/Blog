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
    class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListSliders()
        {
            return _db.Slider.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void update(Slider slider)
        {
            var objDB = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDB.Name = slider.Name;
            objDB.Estado = slider.Estado;
            objDB.UrlImagen = slider.UrlImagen;
            _db.SaveChanges();
        }

    }
}
