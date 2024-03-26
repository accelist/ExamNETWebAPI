using Contracts.BookTicketModels.RequestModel;
using Contracts.BookTicketModels.ResponseModel;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handler.BookTicket
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookTicketRequest, DeleteUpdateBookTicketResponse>
    {
        private readonly DBContext _db;

        public UpdateBookHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteUpdateBookTicketResponse> Handle(UpdateBookTicketRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteUpdateBookTicketResponse();

            var bookedTicket = await _db.BookTickets
                .FirstOrDefaultAsync(bt => bt.BookedTicketID == request.BookedTicketId, cancellationToken);

            if (bookedTicket == null)
            {
                return null;
            }

            int difference = request.NewQuantity - bookedTicket.Quantity;

            bookedTicket.Quantity = request.NewQuantity;

            if (request.NewQuantity < 0 || request.NewQuantity > bookedTicket.Quantity)
            {
                return null;
            }

            if (difference > 0)
            {
                bookedTicket.Ticket.Quota -= difference;
            }

            // Save changes to the database
            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
