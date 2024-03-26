using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public List<Ticket> Tickets { get; set; } = [];
    }
}
