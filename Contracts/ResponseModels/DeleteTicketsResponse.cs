namespace Contracts.ResponseModels
{
    public class DeleteTicketsResponse
    {
        public List<DeleteTicketResponseDataModel> Tickets { get; set; } = new List<DeleteTicketResponseDataModel>();
    }

    public class DeleteTicketResponseDataModel
    {
        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        public string CategoryName { get; set; } = string.Empty;
    }
}
