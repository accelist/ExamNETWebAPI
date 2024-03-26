
namespace Contracts.ResponseModels.Ticket
{
    public class GetAvailableTicketResponse
    {
        public List<GetAvailableTicketModel> AvailableTickets { get; set; } = [];
    }
    public class GetAvailableTicketModel
    {
        public DateOnly EventDate { get; set; }
        public int Quota { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
