using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> listaSliders { get; set; }

        public IEnumerable<Articulo> listaArticulos { get; set; }
    }
}
