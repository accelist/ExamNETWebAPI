using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Response.BookedTicket
{
    public class CreateBookedTicketResponse
    {
        public int PriceSummary { get; set; }
        public List<TicketPerCategory> TicketPerCategories { get; set; } = new List<TicketPerCategory>();
    }

    public class TicketPerCategory
    {
        public string CategoryName { get; set; } = string.Empty;
        public int SummaryPrice { get; set; }
        public List<Tickets> Ticketz { get; set; } = new List<Tickets>();
    }

    public class Tickets
    {
        public Guid Id { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
