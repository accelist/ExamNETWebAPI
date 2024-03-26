using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
