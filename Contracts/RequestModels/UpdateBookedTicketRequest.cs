using Contracts.ResponseModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels
{
    public class UpdateBookedTicketRequest : UpdateTicketModel, IRequest<UpdateBookedTicketResponse>
    {
        public Guid BookId { get; set; }
        public List<UpdateTicketModel> UpdateTickets { get; set; } = new List<UpdateTicketModel>();
    }
    public class UpdateTicketModel
    {
        public Guid TicketCode { get; set; }
        public int BuyQuantity { get; set; }
    }
}
