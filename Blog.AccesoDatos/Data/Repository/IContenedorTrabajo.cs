using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoryRepository Category { get; }

        void Save();
    }
}
