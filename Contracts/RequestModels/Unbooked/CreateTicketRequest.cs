using Contracts.ResponseModels.Unbooked;
using MediatR;

namespace Contracts.RequestModels.Unbooked
{
    public class CreateTicketRequest : IRequest<CreateTicketResponse>
    {
        public string TicketName { get; set; } = string.Empty;
        public int Quota { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

    }
}
