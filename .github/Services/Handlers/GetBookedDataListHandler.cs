using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handlers
{
    public class GetBookedDataListHandler : IRequestHandler<GetBookedTicketRequest, GetBookedTicketResponse>
    {
        private readonly DBContext _db;

        public GetBookedDataListHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookedTicketResponse> Handle(GetBookedTicketRequest request, CancellationToken cancellationToken)
        {
            var datas = await _db.BookedTickets.Include(c => c.Ticket).Select(c => new BookData
            {
                BookId = c.BookId,
                TicketCode = c.TicketCode,
                EventDate = c.EventDate,
                BuyQuantity =  c.BuyQuantity,
                TicketName = c.TicketName
            }).AsNoTracking().ToListAsync(cancellationToken);

            var response = new GetBookedTicketResponse
            {
                BookDatas = datas,
            };

            return response;
        }
    }
}
