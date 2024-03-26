using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.TicketData.ResponseModels
{
    public class GetTicketDataListResponse
    {
        public List<TicketData> TicketDatas { get; set; } = new List<TicketData>();
    }

    public class TicketData
    {
        public string CategoryName { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Quota { get; set; }
    }
}
