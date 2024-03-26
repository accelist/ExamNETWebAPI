using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class Ticket
    {
        [Key]
        public Guid TicketCode { get; set; }
        [Required]
        public string TicketName { get; set; } = string.Empty;
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public int Quota { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public decimal BuyQuantity { get; set; }
        
    }
}
