using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels
{
    public class BookTicketResponse
    {
        public Guid TicketCode { get; set; }
        public Guid BookId { get; set; }
        public string TicketName { get; set; } = string.Empty;
        public int BuyQuantity { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

        //ticketcode, bookId, tickername, quota, buy, price, catname, eventdate
    }
}