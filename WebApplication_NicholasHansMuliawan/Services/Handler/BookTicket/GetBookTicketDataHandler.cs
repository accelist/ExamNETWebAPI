using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModels.RequestModel;
using Contracts.BookTicketModels.ResponseModel;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handler.BookTicket
{
    public class GetBookTicketDataHandler : IRequestHandler<GetBookTicketDataRequest, GetBookTicketDataResponse>
    {
        private readonly DBContext _db;

        public GetBookTicketDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookTicketDataResponse> Handle(GetBookTicketDataRequest request, CancellationToken cancellationToken)
        {
            var response = new GetBookTicketDataResponse();

            // Retrieve all booked ticket data IDs from the database
            var bookedTicketDataIds = await _db.BookTickets
                .Select(b => b.BookedTicketID)
                .ToListAsync(cancellationToken);

            // Set the response IDs to the retrieved IDs
            response.Ids = bookedTicketDataIds;

            return response;
        }
    }
}
