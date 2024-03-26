using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels
{
    public class DetailBookedResponse
    {
        //Kode Tiket, Nama Tiket, Tanggal Event, Quantity tiket
        public Guid BookId { get; set; }
        public Guid TicketCode { get; set; }
        public DateTime EventDate { get; set; }
        public int BuyQuantity { get; set; }
        public string TicketName { get; set; } = string.Empty;
    }
}
