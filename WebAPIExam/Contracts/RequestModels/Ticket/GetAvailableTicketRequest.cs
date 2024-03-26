using Contracts.ResponseModels.Ticket;
using MediatR;

namespace Contracts.RequestModels.Ticket
{
    public class GetAvailableTicketRequest : IRequest<GetAvailableTicketResponse>
    {
        public string CategoryName { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly EventDate {  get; set; }
        public DateOnly MinimumDate {  get; set; }
        public DateOnly MaximumDate {  get; set; }
        //OrderBy accepts property name as argument. Not case sensitive but spaces matter.
        public string OrderBy { get; set; } = string.Empty;
        public bool SortAscending { get; set; } = true;
    }
}
