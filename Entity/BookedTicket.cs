using System;
using System.ComponentModel.DataAnnotations;
using Entity.Entity;

namespace Entity
{
	public class BookedTicket
	{
		[Key]
		public Guid BookedTicketId { get; set; }

		public List<Ticket> tickets { get; set; } = new List<Ticket>();
	}
}

