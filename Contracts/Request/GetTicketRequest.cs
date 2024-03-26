using Contracts.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public class GetTicketRequest : IRequest<GetTicketResponse>
    {
        public string CategoryName { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set;} = string.Empty;
        public decimal Price { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public string OrderBy { get; set; } = string.Empty;
        public string OrderState { get; set; } = string.Empty;
    }
}
