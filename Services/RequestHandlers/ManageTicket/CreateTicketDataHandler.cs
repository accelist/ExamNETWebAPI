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
    public class CreateTicketDataHandler : IRequestHandler<CreateTicketDataRequest, CreateTicketDataResponse>
    {
        private readonly DBContext _db;

        public CreateTicketDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateTicketDataResponse> Handle(CreateTicketDataRequest request, CancellationToken ct)
        {
            Category? category = await _db.Categories.Where(x => x.CategoryID == request.CategoryID)
                .Select(x => x).FirstOrDefaultAsync(ct);

            Ticket ticket = new Ticket
            {
                TicketID = Guid.NewGuid(),
                TicketCode = request.TicketCode,
                TicketName = request.TicketName,
                Category = category,
                CategoryID = category.CategoryID,
                EventDate = DateTime.Now,
                Price = request.Price,
                Quota = request.Quota,
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync(ct);

            return new CreateTicketDataResponse
            {
                TicketCode = ticket.TicketCode
            };
        }
    }
}
