using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BookTicketModels.ResponseModel
{
    public class GetBookTicketDataResponse
    {
        public List<Guid> Ids { get; set; } = new List<Guid>();
    }
}
