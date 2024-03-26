namespace Contracts.ResponseModels.BookedTicket
{
    public class BookedTicketDetailResponse
    {
        public int QuantityPerCategory {  get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<TicketDetailModel> Tickets { get; set; } = [];

    }
    public class TicketDetailModel
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public DateOnly EventDate {  get; set; }

    }
}
