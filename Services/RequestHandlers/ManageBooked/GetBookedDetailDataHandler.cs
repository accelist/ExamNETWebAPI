using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Contracts.ResponseModels.Category;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageBooked
{
    public class GetBookedDetailDataHandler : IRequestHandler <GetBookedDetailDataRequest, GetBookedDetailDataResponse>
    {
        private readonly DBContext _db;
        public GetBookedDetailDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookedDetailDataResponse> Handle (GetBookedDetailDataRequest request, CancellationToken ct)
        {
            var isDataExist = await _db.BookedTickets.FindAsync(request.BookId);
            var model = await (from t in _db.Tickets
                               join cat in _db.Categories on t.CategoryId equals cat.CategoryId
                               join btc in _db.BookedTickets on cat.CategoryId equals btc.CategoryId
                               where  btc.BookId == request.BookId
                               select new TicketModels
                               {
                                   TicketCode = t.TicketCode,
                                   TicketName = t.TicketName,
                                   EventDate = t.EventDate

                               }).AsNoTracking().ToListAsync(ct);

            var models = new GetBookedDetailDataResponse
            {

                TicketPerCategories = model
            };
            return models;
        }
    }
}
