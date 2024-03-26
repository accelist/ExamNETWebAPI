using Contracts.ResponseModels.Tickets;
using MediatR;

namespace Contracts.RequestModels.Tickets
{
    public class CreateTicketRequest : IRequest<CreateTicketResponse>
    {
        public string TicketCode { get; set; } = string.Empty;

        public int Quota { get; set; }
    }
}
