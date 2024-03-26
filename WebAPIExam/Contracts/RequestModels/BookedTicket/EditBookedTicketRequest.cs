
using Contracts.ResponseModels.BookedTicket;
using MediatR;
using System.Diagnostics.Contracts;

namespace Contracts.RequestModels.BookedTicket
{
    public class EditBookedTicketRequest : EditBookedTicketModel, IRequest<EditBookedTicketResponse>
    {
        public Guid BookedId { get; set; }
    }
    public class EditBookedTicketModel
    {
        public int Quantity { get; set; }

    }
}
