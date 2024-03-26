using Contracts.RequestModels.BookedTickets;
using Contracts.ResponseModels.BookedTickets;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandler.ManageBookedTickets
{
    public class CreateBookTicketHandler : IRequestHandler<CreateBookedTicketRequest, CreateBookedTicketResponse>
    {
        private readonly DBContext _db;
        public CreateBookTicketHandler(DBContext db) 
        {
            _db = db;
        }

        public async Task<CreateBookedTicketResponse>Handle(CreateBookedTicketRequest request, CancellationToken cancellationToken) 
        {
            var bookedTicket = await _db.Tickets.Where(Q => Q.TicketCode == request.TicketCode)
                .Select(Q => new BookedTicket
                {
                    TicketId = new Guid(),
                    TicketCode = Q.TicketCode,
                    TicketName = Q.TicketName,
                    CategoryName = Q.CategoryName,
                    Price = Q.Price,
                }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if(bookedTicket == null)
            {
                return new CreateBookedTicketResponse();
            }

            var response = new CreateBookedTicketResponse
            {
                TicketCode = bookedTicket.TicketCode,
                TicketName = bookedTicket.TicketName,
                Price = bookedTicket.Price
            };
            _db.BookedTickets.Add(bookedTicket);
            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
