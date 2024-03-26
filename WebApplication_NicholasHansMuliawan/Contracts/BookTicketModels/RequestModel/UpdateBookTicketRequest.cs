using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModels.ResponseModel;
using MediatR;

namespace Contracts.BookTicketModels.RequestModel
{
    public class UpdateBookTicketRequest : IRequest<DeleteUpdateBookTicketResponse>
    {
        public Guid BookedTicketId { get; set; }
        public int NewQuantity { get; set; }
    }
}
