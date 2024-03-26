
namespace Contracts.ResponseModels.BookedTicket
{
    public class BookTicketResponse
    {
        public decimal TotalPrice {  get; set; }
        public List<TicketPerCategoryModel> TicketPerCategory { get; set; } = [];

    }
    public class TicketPerCategoryModel
    {
        public string CategoryName {  get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public List<TicketModel> Tickets { get; set; } = [];
    }
    public class TicketModel
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
