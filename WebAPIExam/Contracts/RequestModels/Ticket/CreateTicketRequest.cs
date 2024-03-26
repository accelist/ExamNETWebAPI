using Contracts.ResponseModels.Ticket;
using MediatR;

namespace Contracts.RequestModels.Ticket
{
    public class CreateTicketRequest : IRequest<CreateTicketResponse>
    {
        public Guid CategoryId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly EventDate { get; set; }
        public int Quota { get; set; }

    }
}
