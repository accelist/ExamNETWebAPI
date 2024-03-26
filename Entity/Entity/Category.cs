using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class Category
    {
        [Key]
        public Guid CategoryID {  get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
