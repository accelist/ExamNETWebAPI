using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.BookedTicket
{
    public class CreateBookedTicketResponse
    {
        public string Message { get; set; } = string.Empty;
        public Guid BookId { get; set; }
       
    }
}
