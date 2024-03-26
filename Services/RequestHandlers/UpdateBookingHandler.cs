using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers
{
    public class UpdateBookingHandler : IRequestHandler<UpdateBookingRequest, UpdateBookingResponse>
    {
        private readonly DBContext _db;

        public UpdateBookingHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<UpdateBookingResponse> Handle(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            var tempTicketcodeList = new List<string>();

            foreach (var item in request.Tickets)
            {
                tempTicketcodeList.Add(item.TicketCode);
            }

            var datas = await (from m in _db.TicketBookedTicketMappings
                               join t in _db.Tickets on m.TicketCode equals t.TicketCode
                               join c in _db.Categories on t.CategoryId equals c.CategoryId
                               where m.BookedTicketId == request.BookedTicketId
                               && tempTicketcodeList.Contains(m.TicketCode)
                               select m).ToListAsync();

            foreach (var data in datas)
            {
                foreach (var item in request.Tickets)
                {
                    if (data.TicketCode == item.TicketCode)
                    {
                        data.TicketQuantity = item.Quantity;
                        break;
                    }
                }
            }

            await _db.SaveChangesAsync(cancellationToken);

            var datas2 = await (from m in _db.TicketBookedTicketMappings
                               join t in _db.Tickets on m.TicketCode equals t.TicketCode
                               join c in _db.Categories on t.CategoryId equals c.CategoryId
                               where m.BookedTicketId == request.BookedTicketId
                               && tempTicketcodeList.Contains(m.TicketCode)
                               select new UpdateBookingResponseDataModel
                               {
                                   TicketCode = t.TicketCode,
                                   TicketName = t.TicketName,
                                   Quantity = m.TicketQuantity,
                                   CategoryName = c.CategoryName
                               }).ToListAsync(cancellationToken);

            var response = new UpdateBookingResponse
            {
                Tickets = datas2
            };

            return response;
        }
    }
}
