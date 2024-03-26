using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using Entity.Entity;
using MediatR;

namespace Services.Handlers.HandleTicket
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketRequest, CreateTicketResponse>
    {
        private readonly DBContext _db;
        public CreateTicketHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<CreateTicketResponse> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var data = new Ticket
            {
                TicketId = Guid.NewGuid(),
                CategoryId = request.CategoryId,
                TicketCode = request.TicketCode,
                TicketName = request.TicketName,
                Price = request.Price,
                EventDate = request.EventDate,
                Quota = request.Quota,
            };
            _db.Tickets.Add(data);
            await _db.SaveChangesAsync();

            return new CreateTicketResponse { TicketId = data.TicketId, TicketName = data.TicketName };
        }
    }
}
