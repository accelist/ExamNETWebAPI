

using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.HandleBookedTicket
{
    public class DeleteBookedTicketsHandler : IRequestHandler<DeleteBookedTicketsRequest, DeleteBookedTicketsResponse>
    {

        private readonly DBContext _db;
        public DeleteBookedTicketsHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<DeleteBookedTicketsResponse> Handle(DeleteBookedTicketsRequest request, CancellationToken cancellationToken)
        {
            var selectedData = await _db.BookedTickets.Where(Q=>Q.BookedId == request.BookedId).Select(Q=>Q).FirstOrDefaultAsync(cancellationToken);
            if(selectedData == null)
            {
                return new DeleteBookedTicketsResponse();
            }
            if(selectedData.Quantity == 0)
            {
                _db.Remove(selectedData);
                await _db.SaveChangesAsync(cancellationToken);
            }
            else
            {
                if(selectedData.Quantity <= request.Quantity)
                {
                    selectedData.Quantity = request.Quantity;
                    await _db.SaveChangesAsync(cancellationToken);
                }
            }
            var response = await (from b in _db.BookedTickets
                                  join t in _db.Tickets on b.TicketId equals t.TicketId
                                  join c in _db.Categories on t.CategoryId equals c.CategoryId
                                  select new DeleteBookedTicketsResponse
                                  {
                                      CategoryName = c.CategoryName,
                                      TicketCode = request.TicketCode,
                                      Quantity = request.Quantity,
                                  }
                ).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (response == null)
            {
                return new DeleteBookedTicketsResponse();
            }
            return response;
        }
    }
}
