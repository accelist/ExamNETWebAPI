using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.BookedTicket
{
    public class GetBookedDetailDataResponse
    {
        public decimal TotalAll {  get; set; }
        public List<TicketPerCategory> TicketPerCategories { get; set; } = new List<TicketPerCategory>();   

    }
    public class TicketPerCategory
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<TicketModels> TicketModels {  get; set; } = new List<TicketModels>();
    }
    public class TicketModels
    {
        public string TicketCode { get; set; } = string.Empty;
        public String TicketName { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
