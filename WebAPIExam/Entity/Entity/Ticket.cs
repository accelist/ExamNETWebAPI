using System;
using System;
using System.ComponentModel.DataAnnotations;
using Entity.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Ticket
    {
        [Key]
        [Required]
        [StringLength(255)]
        public string TicketCode { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string TicketName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public int Quota { get; set; }

        [Required]
        public decimal Price { get; set; }

        //[Required]
        //public DateTime EventDate { get; set; }

        [ForeignKey("CategoryID")]
        public Guid CategoryID { get; set; }

        public Category? Category { get; set; }

    }
}

