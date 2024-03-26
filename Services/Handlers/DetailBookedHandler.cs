using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handlers
{
    public class DetailBookedHandler :IRequestHandler<DetailBookedRequest, DetailBookedResponse>
    {
        private readonly DBContext _db;

        public DetailBookedHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DetailBookedResponse> Handle(DetailBookedRequest request, CancellationToken cancellationToken)
        {
            var data = await (from c in _db.BookedTickets
                              join t in _db.Tickets on c.TicketCode equals t.TicketCode
                              where c.BookId == request.BookId
                              select new DetailBookedResponse()
                              {
                                  BookId = c.BookId,
                                  TicketCode = c.TicketCode,
                                  TicketName = t.TicketName,
                                  EventDate = c.EventDate,
                                  BuyQuantity = c.BuyQuantity
                              }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            return data;
        }
    }
}
