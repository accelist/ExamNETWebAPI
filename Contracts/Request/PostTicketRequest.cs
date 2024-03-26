using Contracts.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public class PostTicketRequest : IRequest<PostTicketResponse>
    {
        public List<TicketModel> TicketList { get; set; } = new List<TicketModel>();
    }

    public class TicketModel
    {
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
