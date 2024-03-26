using Contracts.ResponseModels.BookedTickets;
using MediatR;

namespace Contracts.RequestModels.BookedTickets
{
    public class GetBookedTicketDataRequest : IRequest <GetBookedTicketDataResponse>
    {
        public Guid TicketId { get; set; }
    }
}
