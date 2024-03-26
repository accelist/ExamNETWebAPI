using Contracts.ResponseModels.BookedTickets;
using MediatR;

namespace Contracts.RequestModels.BookedTickets
{
    public class DeleteBookedTicketRequest : IRequest<DeleteBookedTicketResponse>
    {
        public Guid TicketId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity;


    }
}
