using Contracts.ResponseModels.Booking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.Booking
{
    public class GetBookingDataListRequest : IRequest<GetBookingDataListResponse>
    {
        public string CategoryName { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName {  get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
