using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BookTicketModels.ResponseModel
{
    public class DeleteUpdateBookTicketResponse
    {
        public List<TicketDetail> RevokedTickets { get; set; } = new List<TicketDetail>();
    }

    public class TicketDetail
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

}
