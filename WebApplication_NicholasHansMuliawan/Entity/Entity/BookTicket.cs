using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entity.Entity
{
    public class BookTicket
    {
        [Key]   
        public Guid BookedTicketID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [ForeignKey("TicketID")]
        public Guid TicketID { get; set; }
        public Tickets? Ticket { get; set; }
    }
}
