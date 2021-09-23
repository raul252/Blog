using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Ingrese un nombre para el slider")]
        [Display(Name="Nombre Slider")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Estado")]
        public bool Estado { get; set; }

        [Display(Name="Imagen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }
    }
}
