
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class Ticket
    {
        [Key]
        public Guid TicketId { get; set; }
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly EventDate { get; set; }
        public int Quota { get; set; }

        public Category? Category { get; set; }
        public List<BookedTicket> BookedTickets { get; set; } = [];
    }
}
