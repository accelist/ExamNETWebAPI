using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
	public class BookedTicket
    {
        [Key]
        public Guid BookedTicketID { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}

