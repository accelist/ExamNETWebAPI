using Contracts.ResponseModels.Tickets;
using MediatR;

namespace Contracts.RequestModels.Tickets
{
    public class GetTicketDataListRequest : IRequest<GetTicketDataListResponse>
    {
        /// <summary>
        /// Using Query param
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set;} = string.Empty;

        public int Price { get; set; }

        public DateTime MinDate { get; set;}

        public DateTime MaxDate { get; set;}
        
        
        public string OrderBy { get; set; } = string.Empty;

        
    }
}
