using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.HandleBookedTicket
{
    public class EditBookedTicketHandler : IRequestHandler<EditBookedTicketRequest, EditBookedTicketResponse>
    {
        private readonly DBContext _db;
        public EditBookedTicketHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<EditBookedTicketResponse> Handle(EditBookedTicketRequest request, CancellationToken cancellationToken)
        {
            var selectedData = await _db.BookedTickets.Where(Q=>Q.BookedId == request.BookedId).FirstOrDefaultAsync(cancellationToken);
            if (selectedData == null)
            {
                return new EditBookedTicketResponse();
            }
            selectedData.Quantity = request.Quantity;
            await _db.SaveChangesAsync(cancellationToken);
            
            var response = await (from b in _db.BookedTickets
                                  join t in _db.Tickets on b.TicketId equals t.TicketId
                                  join c in _db.Categories on t.CategoryId equals c.CategoryId
                                  select new EditBookedTicketResponse
                                  {
                                      CategoryName = c.CategoryName,
                                      TicketName = t.TicketName,
                                      TicketCode = t.TicketCode,
                                      Quantity = request.Quantity,
                                  }
                ).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (response == null)
            {
                return new EditBookedTicketResponse();
            }
            return response;
        }
    }
}
