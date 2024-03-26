using Contracts.ResponseModels.Booking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.Booking
{
    public class DeleteBookingDataRequest : IRequest<DeleteBookingDataResponse>
    {
        public Guid BookedTicketID { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
