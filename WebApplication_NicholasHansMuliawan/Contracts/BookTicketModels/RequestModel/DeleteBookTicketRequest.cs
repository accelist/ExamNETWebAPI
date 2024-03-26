using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModels.ResponseModel;
using MediatR;

namespace Contracts.BookTicketModels.RequestModel
{
    public class DeleteBookTicketRequest : IRequest<DeleteUpdateBookTicketResponse>
    {
        public Guid BookedId { get; set; }
        public string TicketID { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }


}

