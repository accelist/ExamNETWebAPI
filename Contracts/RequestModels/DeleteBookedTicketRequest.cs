using Contracts.ResponseModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels
{
    public class DeleteBookedTicketRequest :IRequest<DeleteBookedTicketResponse>
    {
        public Guid BookId {get; set;}
        public Guid TicketCode { get; set;}
        public int BuyQuantity { get; set;}
    }
}
