using Contracts.ResponseModels.BookedTicket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.BookedTicket
{
    public class GetBookedDetailDataRequest : IRequest<GetBookedDetailDataResponse>
    {
        public Guid BookId { get; set; }
    }
}
