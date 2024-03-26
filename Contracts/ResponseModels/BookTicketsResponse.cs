namespace Contracts.ResponseModels
{
    public class BookTicketsResponse
    {
        public decimal PriceSummary { get; set; } = decimal.Zero;

        public List<BookCategoryResponseDataModel> TicketPerCategories { get; set; } = new List<BookCategoryResponseDataModel>();
    }

    public class BookCategoryResponseDataModel
    {
        public string CategoryName { get; set; } = string.Empty;

        public decimal SummaryPrice { get; set; } = decimal.Zero;

        public List<BookTicketResponseDataModel> Tickets { get; set; } = new List<BookTicketResponseDataModel>();
    }

    public class BookTicketResponseDataModel
    {
        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;
    }
}
