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
        public Guid CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public int Quantity { get; set; }
        
    }
}
