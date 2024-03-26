using Contracts.ResponseModels.BookedTicket;
using MediatR;

namespace Contracts.RequestModels.BookedTicket
{
    public class BookTicketRequest : IRequest<BookTicketResponse>
    {
        public List<BookTicketModel> BookingList { get; set; } = [];
    }
    public class BookTicketModel
    {
        public Guid TicketId { get; set; }
        public int Quantity {  get; set; }
    }
}
