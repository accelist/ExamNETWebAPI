﻿using Contracts.Response.BookedTicket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request.BookedTicket
{
    public class CreateBookedTicketRequest: IRequest<CreateBookedTicketResponse>
    {
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
