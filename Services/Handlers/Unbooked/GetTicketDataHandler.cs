using Contracts.RequestModels.Unbooked;
using Contracts.ResponseModels.Unbooked;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.Unbooked
{
    public class GetTicketDataHandler : IRequestHandler<GetTicketRequest, GetTicketResponse>
    {
        private readonly DBContext _db;

        public GetTicketDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetTicketResponse> Handle(GetTicketRequest request, CancellationToken cancellationToken)
        {
            var datas = await _db.Tickets.Select(Q => new TicketData
            {
                TicketCode = Q.TicketCode,
                TicketName = Q.TicketName,
                CategoryName = Q.CategoryName,
                Quota = Q.Quota,
                Price = Q.Price,
                EventDate = Q.EventDate,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            var response = new GetTicketResponse
            {
                TicketDatas = datas
            };

            return response;
        }
    }

}
