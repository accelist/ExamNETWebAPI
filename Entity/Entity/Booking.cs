using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class Booking
    {
        [Key]
        public Guid BookingID { get; set; }

        [ForeignKey("TicketID")]
        public Guid TicketID { get; set; }
        public Ticket? Ticket { get; set; }
        [Required]
        public int Quantity { get; set; }  
    }
}
