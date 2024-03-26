using Contracts.ResponseModels.Ticket;
using Contracts.RequestModels.Ticket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageTicket
{
    public class DeleteTicketDataHandler : IRequestHandler<DeleteTicketDataRequest, DeleteTicketDataResponse>
    {
        private readonly DBContext _db;

        public DeleteTicketDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteTicketDataResponse> Handle(DeleteTicketDataRequest request, CancellationToken ct)
        {
            var data = await _db.Tickets.Where(x => x.TicketCode == request.TicketCode)
                .Select(x => x).FirstOrDefaultAsync(ct);

            if(data == null)
            {
                return new DeleteTicketDataResponse
                {
                    IsSuccess = false,
                    Message = "Ticket does not exist!"
                };
            }

            _db.Remove(data);
            await _db.SaveChangesAsync(ct);

            return new DeleteTicketDataResponse
            {
                IsSuccess = true,
                Message = "Ticket has been deleted!"
            };
        }
    }
}
