
using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.HandleTicket
{
    public class GetAllTicketHandler : IRequestHandler<GetAllTicketRequest, GetAllTicketResponse>
    {
        private readonly DBContext _db;
        public GetAllTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetAllTicketResponse> Handle(GetAllTicketRequest request, CancellationToken cancellationToken)
        {
            var dataList = await _db.Tickets.Select(Q=> new GetAllTicketModel
            {
                TicketId = Q.TicketId,
                CategoryId = Q.CategoryId,
                TicketName = Q.TicketName,
                TicketCode = Q.TicketCode,
                Price = Q.Price,
                EventDate = Q.EventDate,
                Quota = Q.Quota,
            }).AsNoTracking().ToListAsync(cancellationToken);
            var response = new GetAllTicketResponse
            {
                TicketList = dataList,
            };
            return response;
        }
    }
}
