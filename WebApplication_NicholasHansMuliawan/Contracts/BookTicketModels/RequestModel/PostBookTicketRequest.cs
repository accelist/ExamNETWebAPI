using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModel.ResponseModel;
using MediatR;

namespace Contracts.BookTicketModel.RequestModel
{
    public class PostBookTicketRequest : IRequest<PostBookTicketResponse>
    {
        public List<TicketBookingModel> TicketBookings { get; set; } = new List<TicketBookingModel>();
    }

    public class TicketBookingModel
    {
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}

