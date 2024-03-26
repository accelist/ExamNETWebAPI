using Contracts.ResponseModels;
using MediatR;


namespace Contracts.RequestModels
{
    public class BookTicketRequest : IRequest<BookTicketResponse>
    {
        public Guid TicketCode { get; set; }
        public int BuyQuantity { get; set; }
    }
    
}
