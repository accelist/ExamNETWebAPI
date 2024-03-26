using Contracts.ResponseModels;
using MediatR;

namespace Contracts.RequestModels
{
    public class BookTicketsRequest : IRequest<BookTicketsResponse>
    {
        public List<BookTicketRequestDataModel> BookTicketRequestDatas { get; set; } = new List<BookTicketRequestDataModel>();
    }

    public class BookTicketRequestDataModel
    {
        public string TicketCode { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;
    }
}
