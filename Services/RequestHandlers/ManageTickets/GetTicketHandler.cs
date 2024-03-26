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

namespace Services.RequestHandlers.ManageTickets
{
    public class GetTicketHandler : IRequestHandler <GetTicketDataRequest, GetTicketDataResponse>
    {
        private readonly DBContext _db;

        public GetTicketHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<GetTicketDataResponse> Handle (GetTicketDataRequest request, CancellationToken ct)
        {
            var datas = await (from tck in _db.Tickets
                               join cat in _db.Categories on tck.CategoryId equals cat.CategoryId select new GetTicketDataModel
                               {
                                   EventTime = DateTime.Now,
                                   CategoryName = cat.CategoryName,
                                   Quota = tck.Quota,
                                   TicketCode = tck.TicketCode,
                                   TicketName = tck.TicketName,
                                   Price = tck.Price,
                               }).AsNoTracking().ToListAsync();
            var response = new GetTicketDataResponse
            {
                TicketDatas = datas
            };
            return response;
        }
    }
}
