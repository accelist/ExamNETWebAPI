using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.Ticket
{
    public class DeleteTicketDataResponse
    {
        public bool IsSuccess {  get; set; }
        public string Message {  get; set; } = string.Empty;
    }
}
