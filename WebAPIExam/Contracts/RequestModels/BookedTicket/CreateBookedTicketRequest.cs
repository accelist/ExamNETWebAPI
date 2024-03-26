using System;
using Contracts.ResponseModels;
using MediatR;

namespace Contracts.RequestModels.BookedTicket
{
    public class CreateBookedTicketRequest : IRequest<CreateBookedTicketResponse>
    {
        public string TicketCode { get; set; } = string.Empty;

        public int Quantity { get; set; } 
    }
}

