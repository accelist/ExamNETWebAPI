using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModel.ResponseModel;
using MediatR;

namespace Contracts.BookTicketModel.RequestModel
{
    public class GetBookedTicketByIdRequest : GetTicketModel, IRequest<GetBookedTicketByIdResponse>
    {
        public Guid BookedId { get; set; }
    }

    public class GetTicketModel
    {

    }
}
