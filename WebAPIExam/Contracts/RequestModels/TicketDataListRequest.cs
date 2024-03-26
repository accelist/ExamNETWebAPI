using Contracts.ResponseModels;
using MediatR;

namespace Contracts.RequestModels
{
    public class TicketDataListRequest : IRequest<TicketDataListResponse>
    {
        public string CategoryName { get; set; } = string.Empty;

        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime MinEvent { get; set; }

        public DateTime MaxEvent { get; set; }

        public string OrderBy { get; set; } = string.Empty;

        public string OrderState { get; set; } = string.Empty;

    }
}

