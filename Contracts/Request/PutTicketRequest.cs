using Contracts.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public class PutTicketRequest : PutTicketModel, IRequest<PutTicketResponse>
    {
        public Guid? BookedID { get; set; }
    }
    public class PutTicketModel
    {
        public List<TicketModel2> PutTicketList { get; set; }
    }
    public class TicketModel2
    {
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
