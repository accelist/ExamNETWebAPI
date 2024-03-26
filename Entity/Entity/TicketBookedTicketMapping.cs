using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class TicketBookedTicketMapping
    {
        [Key]
        public Guid MappingId { get; set; } = Guid.NewGuid();

        public Guid BookedTicketId { get; set; } = Guid.NewGuid();

        public string TicketCode { get; set; } = string.Empty;

        public int TicketQuantity { get; set; } = 0;

        [ForeignKey("BookedTicketId")]
        public BookedTicket? BookedTicket { get; set; }

        [ForeignKey("TicketCode")]
        public Ticket? Ticket { get; set; }
    }
}
