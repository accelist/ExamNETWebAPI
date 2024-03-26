using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class Ticket
    {
        [Key]
        [StringLength(255)]
        public string TicketCode { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string TicketName { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; } = decimal.Zero;

        [Required]
        public int Quota { get; set; } = 0;

        [Required]
        public DateTimeOffset EventDate { get; set; } = DateTimeOffset.UtcNow;

        public string CategoryId { get; set; } = string.Empty;

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        //public List<BookedTicket> BookedTickets { get; set; } = new List<BookedTicket>();

        //public ICollection<TicketBookedTicketMapping>? TicketBookedTicketMappings { get; set; }

        public List<TicketBookedTicketMapping> TicketBookedTicketMappings { get; set; } = new List<TicketBookedTicketMapping>();
    }
}
