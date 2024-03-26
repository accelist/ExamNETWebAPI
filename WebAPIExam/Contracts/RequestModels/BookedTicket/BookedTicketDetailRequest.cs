
using Contracts.ResponseModels.BookedTicket;
using MediatR;

namespace Contracts.RequestModels.BookedTicket
{
    public class BookedTicketDetailRequest : BookedTicketDetailRequestModel, IRequest<BookedTicketDetailResponse>
    {

    }
    public class BookedTicketDetailRequestModel
    {
        public Guid BookedId { get; set; }
    }
}
