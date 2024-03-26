using Contracts.RequestModels.BookedTickets;
using Contracts.ResponseModels.BookedTickets;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandler.ManageBookedTickets
{
    public class GetBookTicketHandler : IRequestHandler<GetBookedTicketDataRequest, GetBookedTicketDataResponse>
    {

        private readonly DBContext _db;

        public GetBookTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookedTicketDataResponse>Handle(GetBookedTicketDataRequest request, CancellationToken cancellationToken)
        {
            var data = await _db.BookedTickets.Where(Q => Q.TicketId == request.TicketId).FirstOrDefaultAsync(cancellationToken);
            if(data == null)
            {
                return new GetBookedTicketDataResponse();
            }

            var response = new GetBookedTicketDataResponse
            {
                TicketCode = data.TicketCode,
                TicketName = data.TicketName,
                CategoryName = data.CategoryName,
                EventDate = data.EventDate
            };

            return response;
        }
    }
}
