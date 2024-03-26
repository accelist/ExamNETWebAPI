using Contracts.ResponseModels.Ticket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.Ticket
{
    public class CreateTicketRequest : IRequest<CreateTicketResponse>
    {
        public DateTime EventTime { get; set; }
        public int Quota { get; set; }
        public string TicketName { get; set; } = string.Empty;
        public Guid CategoryId {  get; set; }
        public decimal Price { get; set; }


    }
}
