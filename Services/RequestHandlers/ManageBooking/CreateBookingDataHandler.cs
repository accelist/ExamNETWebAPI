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
    public class CreateBookingDataHandler : IRequestHandler<CreateBookingDataRequest, CreateBookingDataResponse>
    {
        public readonly DBContext _db;

        public CreateBookingDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateBookingDataResponse> Handle(CreateBookingDataRequest request, CancellationToken ct)
        {
            Ticket? ticket = await _db.Tickets.Where(x => x.TicketCode == request.TicketCode)
                .Select(x => x).FirstOrDefaultAsync();

            var booking = new Booking
            {
                BookingID = Guid.NewGuid(),
                Ticket = ticket,
                TicketID = ticket.TicketID
            };

            _db.BookedTicket.Add(booking);
            await _db.SaveChangesAsync(ct);

            return new CreateBookingDataResponse
            {
               TicketCode = ticket.TicketCode,
               TicketName = ticket.TicketName,
               Price = ticket.Price
            };
        }
    }
}
