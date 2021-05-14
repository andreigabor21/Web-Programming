using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace lab9.Models
{ //author, name, type, the actual recipe
    public class Recipe
    {
        public int id { get; set; }

        [Required]
        public String author { get; set; }

        [Required]
        public String name { get; set; }

        [Required]
        public String type { get; set; }

        [Required]
        public String description { get; set; }
    }
}
