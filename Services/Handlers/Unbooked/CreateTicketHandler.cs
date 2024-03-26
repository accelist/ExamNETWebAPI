using Contracts.RequestModels.Unbooked;
using Contracts.ResponseModels.Unbooked;
using Entity.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handlers.Unbooked
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
            var ticket = new Ticket
            {
                TicketCode = Guid.NewGuid(),
                TicketName = request.TicketName,
                Quota = request.Quota,
                Price = request.Price,
                CategoryName = request.CategoryName,
                EventDate = request.EventDate
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();

            var response = new CreateTicketResponse
            {
                TicketCode = ticket.TicketCode,
            };
            return response;

        }
    }
}
