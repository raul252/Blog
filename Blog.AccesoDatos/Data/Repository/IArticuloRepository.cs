using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {

        void update(Articulo articulo);
    }
}
