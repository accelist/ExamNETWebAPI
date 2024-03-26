using Contracts.Response.BookedTicket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request.BookedTicket
{
    public class GetBookedRequest: IRequest<GetBookedResponse>
    {
        public Guid BookedId { get; set; }
    }
}
