using Contracts.ResponseModels;
using MediatR;

namespace Contracts.RequestModels
{
    public class GetBookingDetailRequest : IRequest<GetBookingDetailResponse>
    {
        public Guid BookedTicketId { get; set; } = Guid.NewGuid();
    }
}
