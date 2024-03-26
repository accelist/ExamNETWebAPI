using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class BookedTicket
    {
        [Key]
        public Guid BookedId { get; set; }
        [ForeignKey("TicketId")]
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }

        public Ticket? Tickets { get; set; }
    }
}
