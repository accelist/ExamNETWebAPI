using MediatR;
using Entity.Entity;
using Contracts.RequestModels.Tickets;
using Contracts.ResponseModels.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandler.ManageTickets
{
    public class GetTicketDataHandler : IRequestHandler<GetTicketDataListRequest, GetTicketDataListResponse> 
    {
        private readonly DBContext _db;

        public GetTicketDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetTicketDataListResponse> Handle(GetTicketDataListRequest request, CancellationToken cancellationToken)
        {
            var data = await _db.Tickets.Where(Q =>
                Q.CategoryName.ToLower().Contains(request.CategoryName.ToLower()) ||
                Q.TicketCode.ToLower().Contains(request.TicketCode.ToLower()) ||
                Q.TicketName.ToLower().Contains(request.TicketName.ToLower()) ||
                Q.Price >= request.Price ||
                (Q.EventDate <= request.MaxDate && Q.EventDate >= request.MinDate)
                ).Select(Q => new TicketData
                {
                    EventDate = Q.EventDate,
                    TicketCode = Q.TicketCode,
                    TicketName = Q.TicketName,
                    CategoryName = Q.CategoryName,
                    Price = Q.Price
                }).OrderBy(Q => request.OrderBy).ToListAsync(cancellationToken);

            var result = new GetTicketDataListResponse
            {
                TicketDatas = data
            };

            return result;
        }
    }
}
