
using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.HandleBookedTicket
{
    public class BookedTicketDetailHandler : IRequestHandler<BookedTicketDetailRequest, BookedTicketDetailResponse>
    {
        private readonly DBContext _db;
        public BookedTicketDetailHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<BookedTicketDetailResponse> Handle(BookedTicketDetailRequest request, CancellationToken cancellationToken)
        {
            var bookedTickets = await (from b in _db.BookedTickets
                                       join t in _db.Tickets on b.TicketId equals t.TicketId
                                       join c in _db.Categories on t.CategoryId equals c.CategoryId
                                       select new
                                       {
                                           c.CategoryName,
                                           t.TicketCode,
                                           t.TicketName,
                                           t.EventDate,
                                           b.Quantity,
                                           b.BookedId,
                                           b.TicketId,
                                           c.CategoryId,
                                       }).AsNoTracking().ToListAsync(cancellationToken);
            

            var selectedBookedTicket = bookedTickets.Where(Q=>Q.BookedId == request.BookedId).FirstOrDefault();
            if (selectedBookedTicket == null)
            {
                return new BookedTicketDetailResponse();
            }
            var response = new BookedTicketDetailResponse
            {
                CategoryName = selectedBookedTicket.CategoryName,
                QuantityPerCategory = 0,
                Tickets = [],
            };
            
            foreach(var bookedTicket in bookedTickets)
            {
                if(selectedBookedTicket.CategoryId == selectedBookedTicket.CategoryId)
                {
                    var ticket = new TicketDetailModel
                    {
                        TicketCode = bookedTicket.TicketCode,
                        TicketName = bookedTicket.TicketName,
                        EventDate = bookedTicket.EventDate
                    };
                    response.QuantityPerCategory += bookedTicket.Quantity;
                    response.Tickets.Add(ticket);
                }
            }

            return response;
        }
    }
}
