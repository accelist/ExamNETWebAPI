using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Contracts.TicketData.RequestModels;
using Contracts.TicketData.ResponseModels;
using Entity.Entity;
using MediatR;

namespace Services.Handler.TicketData
{
    public class PostTicketDataHandler : IRequestHandler<PostTicketDataRequest, PostTicketDataResponse>

    {
        private readonly DBContext _db;

        public PostTicketDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<PostTicketDataResponse> Handle(PostTicketDataRequest request, CancellationToken cancellationToken)
        {
            var ticket = new Tickets
            {
                TicketID = Guid.NewGuid(),
                TicketCode = request.TicketCode,
                TicketName = request.TicketName,
                Price = request.Price,
                CategoryName = request.CategoryName,
                Quota = request.Quota,
                Date = request.Date,

            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync(cancellationToken);

            var response = new PostTicketDataResponse
            {
                Message = "Ticket Added."
            };

            return response;
        }
    }
}
