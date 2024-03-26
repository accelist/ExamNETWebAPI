using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Services.Handlers
{
    public class BookTicketHandler : IRequestHandler<BookTicketRequest, BookTicketResponse>
    {
        private readonly DBContext _db;

        public BookTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<BookTicketResponse> Handle(BookTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = await _db.Tickets.FirstOrDefaultAsync(t => t.TicketCode == request.TicketCode);

           
            var book = new BookedTicket
            {
                BookId = Guid.NewGuid(),
                BuyQuantity = request.BuyQuantity,
                TicketCode = request.TicketCode,
                Ticket = ticket,
                Quota = ticket.Quota,
                TicketName = ticket.TicketName,
                EventDate = ticket.EventDate
            };

            _db.BookedTickets.Add(book);
            await _db.SaveChangesAsync(cancellationToken);

            book = await _db.BookedTickets.Include(c => c.Ticket).SingleAsync(c => c.BookId == book.BookId);

            ticket.Quota -= request.BuyQuantity;

            if (ticket == null)
            {
                throw new Exception("Ticket code is not registered in the database.");
            }

            if (ticket.Quota <= 0)
            {
                _db.Tickets.Remove(ticket);
                await _db.SaveChangesAsync(cancellationToken);
                throw new Exception("Ticket quota is exhausted.");
            }

            if (request.BuyQuantity > ticket.Quota)
            {
                throw new Exception("The quantity of tickets booked exceeds the remaining quota.");
            }
            var response = new BookTicketResponse
            {
                BookId = book.BookId,
                BuyQuantity = book.BuyQuantity,
                TicketCode = book.TicketCode,
                TicketName = book.Ticket.TicketName,
                CategoryName = book.Ticket.CategoryName,
                EventDate = book.Ticket.EventDate,
                Price = book.Ticket.Price
            };

            return response;
        }
    }
}

