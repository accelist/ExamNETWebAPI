using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers
{
    public class BookTicketsHandler : IRequestHandler<BookTicketsRequest, BookTicketsResponse>
    {
        private readonly DBContext _db;

        public BookTicketsHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<BookTicketsResponse> Handle(BookTicketsRequest request, CancellationToken cancellationToken)
        {
            var bookedTicket = new BookedTicket
            {
                BookedTicketId = Guid.NewGuid(),
            };

            var list = new List<TicketBookedTicketMapping>();

            foreach (var data in request.BookTicketRequestDatas)
            {
                var temp = new TicketBookedTicketMapping()
                {
                    MappingId = Guid.NewGuid(),
                    BookedTicketId = bookedTicket.BookedTicketId,
                    TicketCode = data.TicketCode,
                    TicketQuantity = data.Quantity,
                };

                list.Add(temp);
            }

            _db.TicketBookedTicketMappings.AddRange(list);
            _db.BookedTickets.Add(bookedTicket);
            await _db.SaveChangesAsync(cancellationToken);

            //TODO update quota

            var datas = await (from m in _db.TicketBookedTicketMappings
                               join t in _db.Tickets on m.TicketCode equals t.TicketCode
                               join c in _db.Categories on t.CategoryId equals c.CategoryId
                               where m.BookedTicketId == bookedTicket.BookedTicketId
                               select new BookTicketsHandlerHelperModel
                               {
                                   TicketCode = t.TicketCode,
                                   TicketName = t.TicketName,
                                   Price = t.Price,
                                   Quantity = m.TicketQuantity,
                                   CategoryName = c.CategoryName
                               }).AsNoTracking().ToListAsync();

            var categories = await _db.Categories.Select(Q => Q.CategoryName).AsNoTracking().ToListAsync();

            var response = new BookTicketsResponse();

            foreach (var category in categories)
            {
                var ticketListPerCategory = new BookCategoryResponseDataModel
                {
                    CategoryName = category,
                };

                foreach (var data in datas)
                {
                    if (data.CategoryName == category)
                    {
                        var ticketData = new BookTicketResponseDataModel
                        {
                            TicketCode = data.TicketCode,
                            TicketName = data.TicketName,
                            Price = data.Price,
                        };
                        ticketListPerCategory.SummaryPrice += data.Price * data.Quantity;
                        ticketListPerCategory.Tickets.Add(ticketData);
                    }
                }

                response.PriceSummary += ticketListPerCategory.SummaryPrice;

                if (ticketListPerCategory.Tickets.Count > 0) 
                {
                    response.TicketPerCategories.Add(ticketListPerCategory);
                }
            }

            return response;
        }

        public class BookTicketsHandlerHelperModel //TODO Pindahin, ato bikin anonymus kalo aman
        {
            public string TicketCode { get; set; } = string.Empty;

            public string TicketName { get; set; } = string.Empty;

            public decimal Price { get; set; } = decimal.Zero;

            public int Quantity { get; set; } = 0;

            public string CategoryName { get; set; } = string.Empty;
        }
    }
}
