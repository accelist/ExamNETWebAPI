using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers
{
    public class GetBookingDetailHandler : IRequestHandler<GetBookingDetailRequest, GetBookingDetailResponse>
    {
        private readonly DBContext _db;
        public GetBookingDetailHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookingDetailResponse> Handle(GetBookingDetailRequest request, CancellationToken cancellationToken)
        {
            var datas = await (from m in _db.TicketBookedTicketMappings
                               join t in _db.Tickets on m.TicketCode equals t.TicketCode
                               join c in _db.Categories on t.CategoryId equals c.CategoryId
                               where m.BookedTicketId == request.BookedTicketId
                               select new GetBookingDetailHandlerHelperModel
                               {
                                   TicketCode = t.TicketCode,
                                   TicketName = t.TicketName,
                                   EventDate = t.EventDate,
                                   Quantity = m.TicketQuantity,
                                   CategoryName = c.CategoryName
                               }).AsNoTracking().ToListAsync();

            var categories = await _db.Categories.Select(Q => Q.CategoryName).AsNoTracking().ToListAsync();

            var response = new GetBookingDetailResponse();

            foreach (var category in categories)
            {
                var ticketListPerCategory = new GetBookingDetailDataListModel
                {
                    CategoryName = category,
                };

                foreach (var data in datas)
                {
                    if (data.CategoryName == category)
                    {
                        var ticketData = new GetBookingDetailDataModel
                        {
                            TicketCode = data.TicketCode,
                            TicketName = data.TicketName,
                            EventDate = data.EventDate,
                        };
                        ticketListPerCategory.QtyPerCategory += data.Quantity;
                        ticketListPerCategory.Tickets.Add(ticketData);
                    }
                }

                if (ticketListPerCategory.Tickets.Count > 0)
                {
                    response.DataList.Add(ticketListPerCategory);
                }
            }

            return response;
        }

        public class GetBookingDetailHandlerHelperModel //TODO Pindahin, ato bikin anonymus kalo aman
        {
            public string TicketCode { get; set; } = string.Empty;

            public string TicketName { get; set; } = string.Empty;

            public DateTimeOffset? EventDate { get; set; }

            public int Quantity { get; set; } = 0;

            public string CategoryName { get; set; } = string.Empty;
        }
    }
}
