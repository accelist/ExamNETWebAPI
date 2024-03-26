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
    public class GetBookingDataListHandler : IRequestHandler<GetBookingDataListRequest, GetBookingDataListResponse>
    {
        private readonly DBContext _db;
        public GetBookingDataListHandler(DBContext db)
        {
            _db = db;
        }
        
        public async Task<GetBookingDataListResponse> Handle(GetBookingDataListRequest request, CancellationToken ct)
        {
            if(string.IsNullOrEmpty(request.CategoryName) != true)
            {
                var filterSearch = await (from x in _db.Categories
                                    where x.Name.Contains(request.CategoryName)
                                    select x).ToListAsync(ct);
            }
            if (string.IsNullOrEmpty(request.TicketCode) != true)
            {
                var filterSearch = await (from x in _db.Tickets
                                    where x.TicketName.Contains(request.TicketCode)
                                    select x).ToListAsync(ct);
            }
            if (string.IsNullOrEmpty(request.TicketName) != true)
            {
                var filterSearch = await (from x in _db.Tickets
                                    where x.TicketName.Contains(request.TicketName)
                                    select x).ToListAsync(ct);
            }

            var datas = await (from x in _db.BookedTicket
                              join y in _db.Tickets on x.TicketID equals y.TicketID
                              join z in _db.Categories on y.CategoryID equals z.CategoryID
                              select new BookingData
                              {
                                  EventDate = y.EventDate,
                                  Quota= y.Quota,
                                  TicketCode = y.TicketCode,
                                  TicketName = y.TicketName,
                                  CategoryName = z.Name,
                                  Price = y.Price
                              }).ToList();

            return new GetBookingDataListResponse
            {
                BookingDatas = datas;
            };

        }
    }
}
