using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
	public class Category
	{
        [Key]
        public Guid CategoryID { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public int Quota { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}

