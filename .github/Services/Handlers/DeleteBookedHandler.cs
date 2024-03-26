using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Services.Handlers
{
    public class DeleteBookedHandler : IRequestHandler<DeleteBookedTicketRequest, DeleteBookedTicketResponse>
    {
        private readonly DBContext _db;
        public DeleteBookedHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteBookedTicketResponse> Handle(DeleteBookedTicketRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.BookedTickets
                .Include(b => b.Ticket)
                .Where(b => b.BookId == request.BookId && b.TicketCode == request.TicketCode)
                .FirstOrDefaultAsync();

            if (existingData == null)
            {
                return new DeleteBookedTicketResponse()
                {
                    Success = false,
                    Message = "Data not found"
                };
            }

            existingData.BuyQuantity -= request.BuyQuantity;

            // If the BuyQuantity is now 0, remove the booked ticket
            if (existingData.BuyQuantity == 0)
            {
                _db.BookedTickets.Remove(existingData);
            }
            else if (existingData.BuyQuantity < 0)
            {
                return new DeleteBookedTicketResponse()
                {
                    Success = false,
                    Message = "BuyQuantity cannot be negative"
                };
            }

            await _db.SaveChangesAsync(cancellationToken);


            return existingData != null ? new DeleteBookedTicketResponse()
            {
                Success = true,
                Message = "Quantity or ticket successfully deleted",
                TicketCode = existingData.TicketCode,
                TicketName = existingData.Ticket.TicketName,
                CategoryName = existingData.Ticket.CategoryName,
                BuyQuantity = existingData.BuyQuantity
            } :
            new DeleteBookedTicketResponse()
            {
                Success = true
            };
        }



    }
}
