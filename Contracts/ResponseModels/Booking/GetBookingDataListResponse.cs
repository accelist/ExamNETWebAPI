using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.Booking
{
    public class GetBookingDataListResponse 
    {
        public List<BookingData> BookingDatas = new List<BookingData>();
    }

    public class BookingData
    {
        public DateTime EventDate { get; set; }
        public int Quota { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
