using Contracts.ResponseModels.Unbooked;
using MediatR;


namespace Contracts.RequestModels.Unbooked
{
    public class GetTicketRequest : IRequest<GetTicketResponse>
    {
        public Guid TicketCode { get; set; }
    }
    public class TicketDataModel
    {
        public string TicketName { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int Quota { get; set; }

        public double Price { get; set; }
    }
}
