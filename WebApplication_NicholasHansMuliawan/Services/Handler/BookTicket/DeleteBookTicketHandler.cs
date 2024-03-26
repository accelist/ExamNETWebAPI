using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModels.RequestModel;
using Contracts.BookTicketModels.ResponseModel;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handler.BookTicket
{
    public class DeleteBookTicketHandler : IRequestHandler<DeleteBookTicketRequest, DeleteUpdateBookTicketResponse>
    {
        private readonly DBContext _db;

        public DeleteBookTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteUpdateBookTicketResponse> Handle(DeleteBookTicketRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteUpdateBookTicketResponse();

            var bookedTicket = await _db.BookTickets
                .Include(Q => Q.Ticket)
                .FirstOrDefaultAsync(Q => Q.BookedTicketID == request.BookedId, cancellationToken);

            if (bookedTicket == null)
            {
                return null;
            }

            if (request.Quantity > bookedTicket.Quantity)
            {
                return null;
            }

            bookedTicket.Quantity -= request.Quantity;

            if (bookedTicket.Quantity == 0)
            {
                _db.BookTickets.Remove(bookedTicket);
            }

            await _db.SaveChangesAsync(cancellationToken);

            response.RevokedTickets.Add(new TicketDetail
            {
                TicketCode = bookedTicket.Ticket.TicketCode,
                TicketName = bookedTicket.Ticket.TicketName,
                CategoryName = bookedTicket.Ticket.CategoryName,
                Quantity = request.Quantity
            });


            // Return the response
            return response;
        }
    }
}
