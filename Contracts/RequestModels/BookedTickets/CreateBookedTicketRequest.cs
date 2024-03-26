using Contracts.ResponseModels.BookedTickets;
using MediatR;

namespace Contracts.RequestModels.BookedTickets
{
    public class CreateBookedTicketRequest : IRequest<CreateBookedTicketResponse>
    {
        public string TicketCode { get; set; } = string.Empty;

        public int Quantity { get; set; } 
    }
}
