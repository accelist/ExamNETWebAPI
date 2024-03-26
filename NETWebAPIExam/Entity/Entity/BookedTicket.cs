using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class BookedTicket
    {
        [Key]
        public Guid BookedTicketId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [ForeignKey("TicketId")]
        public Guid TicketId { get; set; }
        public Ticket? ticket { get; set; }
    }
}
