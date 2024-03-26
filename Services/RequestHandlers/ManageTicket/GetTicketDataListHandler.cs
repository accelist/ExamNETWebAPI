using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageTicket
{
    public class GetTicketDataListHandler : IRequestHandler<GetTicketDataListRequest, GetTicketDataListResponse>
    {
        private readonly DBContext _db;

        public GetTicketDataListHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetTicketDataListResponse> Handle(GetTicketDataListRequest request, CancellationToken ct)
        {
            var datas = await (from x in _db.Tickets
                               join y in _db.Categories on x.CategoryID equals y.CategoryID
                               select new TicketData
                               {
                                   TicketID = x.TicketID,
                                   TicketName = x.TicketName,
                                   TicketCode = x.TicketCode,
                                   CategoryName = y.Name,
                                   Price = x.Price,
                                   Quota = x.Quota,
                                   EventDate = x.EventDate
                               }).AsNoTracking().ToListAsync(ct);

            return new GetTicketDataListResponse
            {
                TicketDatas = datas
            };
        }
    }
}
