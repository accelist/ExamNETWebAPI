using System;
using System.ComponentModel.DataAnnotations;
using Entity.Entity;

namespace Entity
{
	public class Category
	{
        [Key]
        public Guid CategoryId { get; set; } 

      
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        public List<Ticket> tickets { get; set; } = new List<Ticket>();
    }
}

