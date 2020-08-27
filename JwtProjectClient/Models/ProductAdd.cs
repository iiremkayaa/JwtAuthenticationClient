using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjectClient.Models
{
    public class ProductAdd
    {
        [Required(ErrorMessage=("Name must not be empty."))]
        public string Name { get; set; }
    }
}
