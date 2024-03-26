using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class BookedTicket
    {
        [Key]
        public Guid BookId {  get; set; }
        public int Quantity { get; set; }
        [ForeignKey("TicketCode")]
        public string TicketCode { get; set; } = string.Empty;
        public Category? Category { get; set; }
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }

        public Ticket? Ticket { get; set;}
    }
}
