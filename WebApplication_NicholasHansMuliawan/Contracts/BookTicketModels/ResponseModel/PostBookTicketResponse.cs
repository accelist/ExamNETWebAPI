namespace Contracts.BookTicketModel.ResponseModel
{
    public class PostBookTicketResponse
    {
        public decimal PriceSummary { get; set; }
        public List<CategorySummary> TicketsPerCategories { get; set; } = new List<CategorySummary>();
    }

    public class CategorySummary
    {
        public string CategoryName { get; set; }
        public decimal SummaryPrice { get; set; }
        public List<TicketDetail> Tickets { get; set; } = new List<TicketDetail>();     
    }

    public class TicketDetail
    {
        public string TicketCode { get; set; }
        public string TicketName { get; set; }
        public decimal Price { get; set; }
    }
}
