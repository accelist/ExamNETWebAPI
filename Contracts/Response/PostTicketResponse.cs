using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Response
{
    public class PostTicketResponse
    {
        public decimal PriceSummary { get; set; }
        public List<CategorySummary> Categories { get; set; } = new List<CategorySummary>();
    }

    public class CategorySummary
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal SummaryPrice { get; set; }
        public List<TicketSummary> TicketSummaries { get; set; } = new List<TicketSummary>();
    }

    public class TicketSummary
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
