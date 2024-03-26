namespace Contracts.ResponseModels
{
    public class GetAvailableTicketsResponse
    {
        public List<AvailableTicketData> AvailableTicketDatas { get; set; } = new List<AvailableTicketData>();
    }

    public class AvailableTicketData
    {
        public DateTimeOffset EventDate { get; set; } = DateTimeOffset.UtcNow;

        public int Quota { get; set; } = 1;

        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;
    }
}
