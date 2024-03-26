namespace Contracts.ResponseModels.BookedTickets
{
    public class CreateBookedTicketResponse
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set;} = string.Empty;
        public int Price { get; set; }
    }
}
