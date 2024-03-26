using Contracts.RequestModels.Booking;
using Contracts.ResponseModels.Booking;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageBooking
{
    internal class DeleteBookingDataHandler : IRequestHandler<DeleteBookingDataRequest, DeleteBookingDataResponse>
    {
        private readonly DBContext _db;

        public DeleteBookingDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteBookingDataResponse> Handle(DeleteBookingDataRequest request, CancellationToken ct)
        {
            var data = await _db.BookedTicket.Where(x => x.TicketID == request.BookedTicketID)
                .Select(x => x).FirstOrDefaultAsync(ct);

            if()
        }
    }
}
