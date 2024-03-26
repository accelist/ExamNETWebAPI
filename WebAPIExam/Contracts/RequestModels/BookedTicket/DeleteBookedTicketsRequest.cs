using Contracts.ResponseModels.BookedTicket;
using MediatR;

namespace Contracts.RequestModels.BookedTicket
{
    public class DeleteBookedTicketsRequest : DeleteBookedTicketsModel, IRequest<DeleteBookedTicketsResponse>
    {

    }
    public class DeleteBookedTicketsModel
    {
        public Guid BookedId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
