using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]
        public string Name { get; set; }

        public string Direction { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string City { get; set; }

        public string Country { get; set; }
    }
}
