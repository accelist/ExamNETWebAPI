using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.Unbooked
{
    public class GetTicketResponse
    {
        public List<TicketData> TicketDatas { get; set; } = new List<TicketData>();
    }
    public class TicketData
    {
        public Guid TicketCode { get; set; }
        public string TicketName { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int Quota { get; set; }

        public double Price { get; set; }
    }
}
