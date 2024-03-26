using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Services.Handlers
{
    public class UpdateBookedDataHandler : IRequestHandler<UpdateBookedTicketRequest, UpdateBookedTicketResponse>
    {
        private readonly DBContext _db;
        public UpdateBookedDataHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<UpdateBookedTicketResponse> Handle(UpdateBookedTicketRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.BookedTickets
                .Include(b => b.Ticket)
                .FirstOrDefaultAsync(b => b.BookId == request.BookId);

            if (existingData == null)
            {
                return new UpdateBookedTicketResponse()
                {
                    Success = false,
                    Message = "Data not found"
                };
            }

            // Update the BuyQuantity
            existingData.BuyQuantity = request.BuyQuantity;

            await _db.SaveChangesAsync(cancellationToken);

            return new UpdateBookedTicketResponse()
            {
                Success = true,
                Message = "Quantity successfully updated",
                TicketCode = existingData.TicketCode,
                TicketName = existingData.Ticket.TicketName,
                CategoryName = existingData.Ticket.CategoryName,
                BuyQuantity = existingData.BuyQuantity
            };
        }



    }
}
