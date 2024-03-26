using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.BookedTicket
{
    public class GetBookedDetailDataResponse
    {
        public int QtyPerCategory {  get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<TicketModels> TicketPerCategories { get; set; } = new List<TicketModels>();   

    }
  
    public class TicketModels
    {
        public string TicketCode { get; set; } = string.Empty;
        public String TicketName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

    }
}
