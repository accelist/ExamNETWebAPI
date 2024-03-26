using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BookTicketModel.ResponseModel
{
    public class GetBookedTicketByIdResponse
    {
        public List<CategoryDetail> Categories { get; set; } = new List<CategoryDetail>();
    }

    public class CategoryDetail
    {
        public int QtyPerCategory { get; set; }
        public string CategoryName { get; set; }
        public List<GetTicketDetail> Tickets { get; set; } = new List<GetTicketDetail>();
    }

    public class GetTicketDetail
    {
        public string TicketCode { get; set; }
        public string TicketName { get; set; }
        public DateTime Date { get; set; }
    }
}
