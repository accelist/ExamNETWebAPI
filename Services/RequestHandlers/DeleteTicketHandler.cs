using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers
{
    public class DeleteTicketHandler : IRequestHandler<DeleteTicketsRequest, DeleteTicketsResponse>
    {
        private readonly DBContext _db;

        public DeleteTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteTicketsResponse> Handle(DeleteTicketsRequest request, CancellationToken cancellationToken)
        {
            var targetData = await (from m in _db.TicketBookedTicketMappings
                                  join t in _db.Tickets on m.TicketCode equals t.TicketCode
                                  join c in _db.Categories on t.CategoryId equals c.CategoryId
                                  where m.BookedTicketId == request.BookedTickedId
                                  && m.TicketCode == request.TicketCode
                                  select m).FirstOrDefaultAsync(cancellationToken);

            targetData.TicketQuantity -= request.Quantity;

            if (targetData.TicketQuantity <= 0)
            {
                _db.TicketBookedTicketMappings.Remove(targetData);
            }

            await _db.SaveChangesAsync(cancellationToken);

            var datas = await (from m in _db.TicketBookedTicketMappings
                               join t in _db.Tickets on m.TicketCode equals t.TicketCode
                               join c in _db.Categories on t.CategoryId equals c.CategoryId
                               where m.BookedTicketId == request.BookedTickedId
                               select new DeleteTicketResponseDataModel
                               {
                                   TicketCode = t.TicketCode,
                                   TicketName = t.TicketName,
                                   Quantity = m.TicketQuantity,
                                   CategoryName = c.CategoryName
                               }).ToListAsync(cancellationToken); //TODO no tracking?

            var response = new DeleteTicketsResponse();

            if (datas == null)
            {
                var bookedTicketRow = await _db.BookedTickets.Where(Q => Q.BookedTicketId == request.BookedTickedId).Select(Q => Q).FirstOrDefaultAsync(cancellationToken);

                _db.BookedTickets.Remove(bookedTicketRow); //TODO BookedTicket belum ke remove

                await _db.SaveChangesAsync(cancellationToken);
            }
            else
            {
                response.Tickets = datas;
            }

            return response;
        }
    }
}
