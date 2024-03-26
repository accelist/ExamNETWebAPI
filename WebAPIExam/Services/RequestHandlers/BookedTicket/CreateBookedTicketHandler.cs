using System;
using Contracts.RequestModels;
using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels;
using Entity;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers
{
    public class CreateBookedTicketHandler : IRequestHandler<CreateBookedTicketRequest, CreateBookedTicketResponse>
    {
        private readonly DBContext _db;

        public CreateBookedTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateBookedTicketResponse> Handle(CreateBookedTicketRequest request, CancellationToken cancellationToken)
        {


            var existingCategory = await (from t in _db.Tickets
                                          join c in _db.Categories on t.CategoryID equals c.CategoryID
                                          select c.CategoryID).FirstOrDefaultAsync();

            var updateTicket = await _db.Tickets.Where(Q => Q.TicketCode == request.TicketCode).FirstOrDefaultAsync();

            var BookedData = new BookedTicket
            {
                BookedTicketID = Guid.NewGuid(),
            };

            _db.BookedTickets.Add(BookedData);
            await _db.SaveChangesAsync(cancellationToken); // better ada cancellation token

            var response = new CreateBookedTicketResponse
            {
                BookedTicketID = BookedData.BookedTicketID
            };

            return response;

            //var datas1 = await _db.Tickets.Where(Q => Q.TicketCode == request.TicketCode)
            //    .Select(Q => new TicketDescription
            //    {
            //        TicketCode = Q.TicketCode,
            //        TicketName = Q.TicketName,
            //        Price = Q.Price
            //    }).AsNoTracking()
            //.ToListAsync(cancellationToken);

            //var response = new CategorySummary
            //{
            //    Tickets = datas1
            //};

            //var datas2 = await _db.Tickets.Where(Q => Q.TicketCode == request.TicketCode)
            //    .Select(Q => new CategorySummary
            //    {
            //        CategoryName = Q.CategoryName,
            //        SummaryPrice = Q.Price + Q.Price
            //    }).AsNoTracking()
            //.ToListAsync(cancellationToken);

            //var response = new CategorySummary
            //{
            //    Tickets = datas2
            //}; 
        }
    }
}
