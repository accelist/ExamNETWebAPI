using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class Ticket
    {
        [Key]
        public Guid TicketID { get; set; }

        [Required]
        public string TicketCode { get; set; } = string.Empty;
        [Required]

        [StringLength(255)]
        public string TicketName { get; set;} = string.Empty;

        [Required]
        public Category? Category { get; set;}

        [Required]
        [ForeignKey("CatergoryID")]
        public Guid CategoryID { get; set;}

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quota {  get; set; }

        [Required]
        public DateTime EventDate {  get; set; }
    }
}
