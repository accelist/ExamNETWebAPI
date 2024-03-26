using Contracts.Request.BookedTicket;
using Contracts.Response.BookedTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandler.BookedTicketHandler
{
    public class CreateBookedHandler : IRequestHandler<CreateBookedTicketRequest,CreateBookedTicketResponse>
    {
        private readonly DBContext _db;

        public CreateBookedHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateBookedTicketResponse> Handle(CreateBookedTicketRequest request, CancellationToken cancellationToken)
        {
            var booked = new BookedTicket
            {
                BookedTicketId = Guid.NewGuid(),
                Quantity = request.Quantity,
                TicketId = await _db.Tickets.Where(Q=> Q.TicketCode==request.TicketCode).Select(Q => Q.TicketId).FirstOrDefaultAsync()
            };
            var existingData = await _db.Tickets.Where(Q => Q.TicketCode == request.TicketCode).Select(Q => Q).FirstOrDefaultAsync();

            _db.BookedTickets.Add(booked);
            var update = existingData.Quota - request.Quantity;
            existingData.Quota = update;
            await _db.SaveChangesAsync(cancellationToken);

            var tdaw = await (from t in _db.Tickets
                               join bt in _db.BookedTickets on t.TicketId equals bt.TicketId
                              select new Tickets
                               {
                                  Id = bt.BookedTicketId,
                                   TicketCode = t.TicketCode,
                                   TicketName = t.TicketName,
                                   Price = t.Price,
                               }).AsNoTracking().ToListAsync(cancellationToken);

            var datas = await (from t in _db.Tickets
                               join bt in _db.BookedTickets on t.TicketId equals bt.TicketId
                               where t.TicketId == bt.TicketId
                               select new TicketPerCategory
                               {
                                   CategoryName = t.CategoryName,
                                   SummaryPrice = request.Quantity*t.Price,
                                   Ticketz = tdaw
                               }).AsNoTracking().ToListAsync(cancellationToken);

            var response = new CreateBookedTicketResponse
            {
                PriceSummary = datas.Sum(x => x.SummaryPrice),
                TicketPerCategories = datas
            };

            return response;
        }
    }
}
