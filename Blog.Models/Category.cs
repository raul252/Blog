using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre de la categoria es requerido")]
        [Display(Name="Nombre Categoría")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Orden de visualización")]
        public string Orden { get; set; }
    }
}
