using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public class DeleteTicketRequest
    {
        public Guid BookedID { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
