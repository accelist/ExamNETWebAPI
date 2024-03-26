using Contracts.Response.BookedTicket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request.BookedTicket
{
    public class EditBookedRequest : UpdateBooked,IRequest<EditBookedResponse>
    {
        public Guid BookedId { get; set; }
    }

    public class UpdateBooked
    {
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
