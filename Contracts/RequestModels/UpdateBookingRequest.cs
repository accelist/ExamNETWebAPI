using Contracts.ResponseModels;
using MediatR;

namespace Contracts.RequestModels
{
    public class UpdateBookingRequest : UpdateBookingRequestDataListModel, IRequest<UpdateBookingResponse>
    {
        public Guid BookedTicketId { get; set; } = Guid.NewGuid();
    }

    public class UpdateBookingRequestDataListModel
    {
        public List<UpdateBookingRequestDataModel> Tickets { get; set; } = new List<UpdateBookingRequestDataModel>();
    }

    public class UpdateBookingRequestDataModel
    {
        public string TicketCode { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;
    }
}
