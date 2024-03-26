using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
    public class Ticket
    {
        [Key]
        public Guid TicketId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string TicketCode { get; set; } = string.Empty;

        [Required]
        public string TicketName { get; set;} = string.Empty;

        [Required]
        public string CategoryName {  get; set; } = string.Empty;

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quota { get; set; }
    }
}
