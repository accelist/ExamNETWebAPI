using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.Ticket
{
    public class GetTicketDataResponse
    {
        public List<GetTicketDataModel> TicketDatas { get; set; } = new List<GetTicketDataModel>();
    }
    public class GetTicketDataModel
    {
        public DateTime EventTime { get; set; }
        public int Quota { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

}
