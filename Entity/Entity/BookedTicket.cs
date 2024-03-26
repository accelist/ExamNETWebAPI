using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
    public class BookedTicket
    {
        [Key]
        public Guid BookedTicketId { get; set; } = Guid.NewGuid();

        //public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        //public ICollection<TicketBookedTicketMapping>? TicketBookedTicketMappings { get; set; }

        public List<TicketBookedTicketMapping> TicketBookedTicketMappings { get; set; } = new List<TicketBookedTicketMapping>();
    }
}
