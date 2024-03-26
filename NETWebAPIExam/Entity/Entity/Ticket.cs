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
        public Guid TicketId { get; set; }
        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string TicketCode { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string TicketName { get; set; } = string.Empty;
        [Required]
        public int Quota { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public List<BookedTicket> bookedTickets { get; set; } = new List<BookedTicket>();
    }
}
