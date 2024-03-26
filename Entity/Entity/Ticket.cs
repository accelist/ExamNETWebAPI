using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class Ticket
    {
        public DateTime EventDate { get; set; }
        [Required]
        [MaxLength(100)]
        public int Quota { get; set; }
        [Key]
        public string TicketCode { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string TicketName { get; set;} = string.Empty;
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
    }
}
