using Contracts.ResponseModels.Ticket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.Ticket
{
    public class CreateTicketDataRequest : IRequest<CreateTicketDataResponse>
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set;} = string.Empty;
        public Guid CategoryID {  get; set; }
        public int Quota {  get; set; }
        public decimal Price { get; set; }
        public DateTime EventDate {  get; set; }

    }
}
