using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.Ticket
{
    public class GetTicketDataListResponse
    {
        public List<TicketData> TicketDatas { get; set; } = new List<TicketData>();
    }

    public class TicketData
    {
        public Guid TicketID { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int Quota { get; set; }
        public decimal Price { get; set; }
        public DateTime EventDate { get; set; }
    }
}
