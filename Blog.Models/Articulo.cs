using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre del artículo es obligatorio")]
        [Display(Name="Nombre del artículo")]
        public string Name { get; set; }


        [Required(ErrorMessage = "La descripción del artículo es obligatoria")]
        [Display(Name = "Descripción del artículo")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de creación")]
        public string CreatedAt { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Nombre del imagen")]
        public string UrlImage { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
