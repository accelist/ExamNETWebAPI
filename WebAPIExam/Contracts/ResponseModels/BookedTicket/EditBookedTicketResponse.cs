namespace Contracts.ResponseModels.BookedTicket
{
    public class EditBookedTicketResponse
    {
        public string TicketName { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
