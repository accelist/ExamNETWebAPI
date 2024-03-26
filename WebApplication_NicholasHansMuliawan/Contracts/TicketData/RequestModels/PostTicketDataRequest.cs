using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.TicketData.ResponseModels;
using MediatR;

namespace Contracts.TicketData.RequestModels
{
    public class PostTicketDataRequest : IRequest<PostTicketDataResponse>
    {
        public string TicketCode{  get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public DateTime Date {  get; set; }
        public decimal Price { get; set; }
        public int Quota { get; set; }
    }
}
