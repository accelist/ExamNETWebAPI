
namespace Contracts.ResponseModels.BookedTicket
{
    public class DeleteBookedTicketsResponse
    {
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
