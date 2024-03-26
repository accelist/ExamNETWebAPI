using Contracts.ResponseModels.Ticket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.Ticket
{
    public class DeleteTicketDataRequest : IRequest<DeleteTicketDataResponse>
    {
        public string TicketCode { get; set; } = string.Empty;
    }
}
