using Contracts.Response.Ticket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request.Ticket
{
    public class GetTicketRequest: IRequest<GetTicketResponse>
    {
        public string SearchQuery { get; set; } = string.Empty;
        public DateTime MaxDate { get; set; }
        public DateTime MinDate { get; set; }
        public string OrderBy { get; set; } = string.Empty;
        public string OrderState { get; set; } = string.Empty;
    }
}
