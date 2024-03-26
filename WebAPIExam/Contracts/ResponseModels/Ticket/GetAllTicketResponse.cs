namespace Contracts.ResponseModels.Ticket
{
    public class GetAllTicketResponse
    {
        public List<GetAllTicketModel> TicketList { get; set; } = [];
    }
    public class GetAllTicketModel
    {
        public Guid TicketId { get; set; }
        public Guid CategoryId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly EventDate { get; set; }
        public int Quota { get; set; }
    }
}
