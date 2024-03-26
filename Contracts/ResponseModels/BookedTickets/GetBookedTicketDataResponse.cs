using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.BookedTickets
{
    public class GetBookedTicketDataResponse
    {
        public string TicketName {  get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string CategoryName { get; set; } = string.Empty;   
    }
}
