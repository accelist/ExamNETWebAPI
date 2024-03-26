using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class Ticket
    {
        [Key]
        public string TicketCode { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public int Quota { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
    }
}

