using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
    public class Ticket
    {
        [Key]
        public Guid TicketID { get; set; }

        [Required]
        [StringLength(20)]
        public string TicketCode { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string TicketName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public int Quota { get; set; }
    }
}
