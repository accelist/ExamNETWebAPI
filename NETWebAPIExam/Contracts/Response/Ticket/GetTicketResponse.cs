using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Response.Ticket
{
    public class GetTicketResponse
    {
        public List<DataTicket> Tickets { get; set; } = new List<DataTicket>();
        public int TotalTickets { get; set; }
    }

    public class DataTicket
    {
        public DateTime EventDate { get; set; }
        public int Quota { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
