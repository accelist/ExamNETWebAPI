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
    public class GetBookedHandler: IRequestHandler<GetBookedRequest,GetBookedResponse>
    {
        private readonly DBContext _db;
        public GetBookedHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookedResponse> Handle(GetBookedRequest request, CancellationToken cancellationToken)
        {
            var data = await (from t in _db.Tickets
                              join bt in _db.BookedTickets on t.TicketId equals bt.TicketId
                              where bt.BookedTicketId == request.BookedId
                              select new GetBookedResponse
                              {
                                  Quantity = bt.Quantity,
                                  CategoryName = t.CategoryName,
                                  TicketCode = t.TicketCode,
                                  TicketName = t.TicketName,
                                  EventDate = t.EventDate,
                                  TotalPrice = bt.Quantity*t.Price
                              }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (data == null)
            {
                return new GetBookedResponse();
            }
            var result = data;
            return result;
        }
    }
}
