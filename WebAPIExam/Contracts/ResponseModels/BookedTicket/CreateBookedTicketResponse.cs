using System;
namespace Contracts.ResponseModels
{
    public class CreateBookedTicketResponse
    {
        public Guid BookedTicketID { get; set; }
    }
    
    //public class CreateBookedTicketResponse
    //{
    //       public decimal PriceSummary { get; set; }

    //       public List<CategorySummary> TicketPerCategories { get; set; } = new List<CategorySummary>();
    //   }

    //   public class CategorySummary
    //   {
    //       public string CategoryName { get; set; } = string.Empty;

    //       public decimal SummaryPrice { get; set; }

    //       public List<TicketDescription> Tickets { get; set; } = new List<TicketDescription>();

    //   }

    //   public class TicketDescription
    //   {
    //       public string TicketCode { get; set; } = string.Empty;

    //       public string TicketName { get; set; } = string.Empty;

    //       public decimal Price { get; set; }
    //   }
}