
using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.HandleBookedTicket
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
            foreach(BookTicketModel ticket in request.BookingList)
            {
                var selectedTicket = await _db.Tickets.Where(Q=>Q.TicketId == ticket.TicketId).Select(Q => Q).FirstOrDefaultAsync(cancellationToken);
                if(selectedTicket == null)
                {
                    continue;
                }
                if(selectedTicket.Quota < ticket.Quantity)
                {
                    ticket.Quantity = ticket.Quantity - selectedTicket.Quota;
                    selectedTicket.Quota = 0;
                }
                else
                {
                    selectedTicket.Quota -= ticket.Quantity;

                }
                var data = new BookedTicket
                {
                    BookedId = Guid.NewGuid(),
                    TicketId = ticket.TicketId,
                    Quantity = ticket.Quantity,
                };
                _db.Add(data);
            }
            await _db.SaveChangesAsync(cancellationToken);

            var databaseData = await (from b in _db.BookedTickets
                                      join t in _db.Tickets on b.TicketId equals t.TicketId
                                      join c in _db.Categories on t.CategoryId equals c.CategoryId
                                      select new
                                      {
                                          c.CategoryName,
                                          t.TicketCode,
                                          t.TicketName,
                                          t.Price,
                                          b.Quantity,
                                          b.BookedId,
                                          b.TicketId,
                                          c.CategoryId,
                                      }
                ).AsNoTracking().ToListAsync(cancellationToken);
            var categories = await _db.Categories.AsNoTracking().ToListAsync(cancellationToken);

            var response = new BookTicketResponse
            {
                TotalPrice = 0,
            };
            foreach(var category in categories)
            {
                var categoryModel = new TicketPerCategoryModel
                {
                    CategoryName = category.CategoryName,
                    SubTotal = 0,
                    Tickets = [],
                };
                
                foreach(var data in databaseData)
                {
                    if(data.CategoryId == category.CategoryId)
                    {
                        categoryModel.SubTotal += data.Price * data.Quantity;
                        var ticket = new TicketModel
                        {
                            TicketCode = data.TicketCode,
                            TicketName = data.TicketName,
                            Price = data.Price * data.Quantity,
                        };
                        categoryModel.Tickets.Add(ticket);
                    }
                }
                response.TotalPrice += categoryModel.SubTotal;
                response.TicketPerCategory.Add(categoryModel);
            }

            return response;
        }
    }
}
