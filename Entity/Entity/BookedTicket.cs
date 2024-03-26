
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entity.Entity
{
    public class BookedTicket
    {
        [Key]
        public Guid BookId { get; set; }
        [ForeignKey("TicketCode")]
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
        public int BuyQuantity { get; set; }
        [Required]
        public double Price { get; set; }

        public Ticket? Ticket { get; set; }
    }
}
