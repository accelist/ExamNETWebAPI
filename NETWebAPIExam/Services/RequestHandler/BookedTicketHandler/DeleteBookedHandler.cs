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
    public class DeleteBookedHandler:IRequestHandler<DeleteBookRequesr,DeleteBookedResponse>
    {
        private readonly DBContext _db;
        public DeleteBookedHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteBookedResponse> Handle(DeleteBookRequesr request, CancellationToken cancellationToken)
        {
            var existingData = await _db.BookedTickets.Where(Q => Q.BookedTicketId == request.BookedId)
                .Select(Q => Q).FirstOrDefaultAsync();

            if (existingData == null)
            {
                return new DeleteBookedResponse();
            }

            var data = await (from t in _db.Tickets
                              join bt in _db.BookedTickets on t.TicketId equals bt.TicketId
                              where bt.BookedTicketId == request.BookedId
                              select new DeleteBookedResponse
                              {
                                  Quantity = bt.Quantity-request.Quantity,
                                  CategoryName = t.CategoryName,
                                  TicketCode = t.TicketCode,
                                  TicketName = t.TicketName,
                              }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            
            existingData.Quantity -= request.Quantity;
            if (data == null)
            {
                return new DeleteBookedResponse();
            }

            if (data.Quantity == 0)
            {
                _db.BookedTickets.Remove(existingData);
            }

            await _db.SaveChangesAsync(cancellationToken);
            var result = data;
            return result;
        }
    }
}
