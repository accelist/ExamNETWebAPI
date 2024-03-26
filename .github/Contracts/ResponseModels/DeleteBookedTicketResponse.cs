using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels
{
    public class DeleteBookedTicketResponse
    {
        public Guid TicketCode { get; set; }
        public string TicketName { get; set; } = string.Empty;  
        public string CategoryName {  get; set; } = string.Empty;
        public int BuyQuantity { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success {  get; set; }
    }
}
