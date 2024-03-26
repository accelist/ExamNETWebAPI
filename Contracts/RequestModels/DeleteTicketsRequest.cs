using Contracts.ResponseModels;
using MediatR;

namespace Contracts.RequestModels
{
    public class DeleteTicketsRequest : IRequest<DeleteTicketsResponse>
    {
        public Guid BookedTickedId { get; set; } = Guid.NewGuid();

        public string TicketCode { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;
    }
}
