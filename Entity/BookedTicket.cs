using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BookedTicket
    {
        [Key]
        public Guid BookedID { get; set; }

        [ForeignKey("TicketID")]
        public Guid TicketID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime BookDate { get; set; }

        [Required]
        public int Quota { get; set; }
    }
}
