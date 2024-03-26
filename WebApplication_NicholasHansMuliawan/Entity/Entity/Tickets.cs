using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
    public class Tickets
    {
        [Key]
        public Guid TicketID { get; set; }
        [Required]
        public string TicketCode { get; set; } = string.Empty;
        [Required]
        public string TicketName { get; set; } = string.Empty;
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quota { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
