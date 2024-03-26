using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.TicketData.ResponseModels;
using MediatR;

namespace Contracts.TicketData.RequestModels
{
    public class GetTicketDataListRequest : IRequest<GetTicketDataListResponse>
    {
        public string CategoryName { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public string OrderBy { get; set; } = string.Empty;
        public string OrderState { get; set; } = string.Empty;
    }
}
