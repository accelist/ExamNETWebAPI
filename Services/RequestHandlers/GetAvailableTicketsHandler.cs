using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers
{
    public class GetAvailableTicketsHandler : IRequestHandler<GetAvailableTicketsRequest, GetAvailableTicketsResponse>
    {
        private readonly DBContext _db;

        public GetAvailableTicketsHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetAvailableTicketsResponse> Handle(GetAvailableTicketsRequest request, CancellationToken cancellationToken)
        {
            var query = (from t in _db.Tickets
                         join c in _db.Categories on t.CategoryId equals c.CategoryId
                         where c.CategoryName.ToLower().Contains(request.CategoryName.ToLower())
                         && t.TicketCode.ToLower().Contains(request.TicketCode.ToLower())
                         && t.TicketName.ToLower().Contains(request.TicketName.ToLower())
                         && (request.Price == 0 || t.Price <= request.Price)
                         && (request.MinEventDate == null || t.EventDate >= request.MinEventDate)
                         && (request.MaxEventDate == null || t.EventDate <= request.MaxEventDate)
                         select new AvailableTicketData
                         {
                             EventDate = t.EventDate,
                             Quota = t.Quota,
                             TicketCode = t.TicketCode,
                             TicketName = t.TicketName,
                             CategoryName = c.CategoryName,
                             Price = t.Price,
                         });

            switch (request.OrderBy) //TODO at least orderby price sama quota masih ngaco
            {
                case "CategoryName":
                    if (request.OrderState != "Descending")
                    {
                        query.OrderBy(t => t.CategoryName);
                    } 
                    else
                    {
                        query.OrderByDescending(t => t.CategoryName);
                    }
                    break;

                case "TicketCode":
                    if (request.OrderState != "Descending")
                    {
                        query.OrderBy(t => t.TicketCode);
                    }
                    else
                    {
                        query.OrderByDescending(t => t.TicketCode);
                    }
                    break;

                case "TicketName":
                    if (request.OrderState != "Descending")
                    {
                        query.OrderBy(t => t.TicketName);
                    }
                    else
                    {
                        query.OrderByDescending(t => t.TicketName);
                    }
                    break;

                case "Price":
                    if (request.OrderState != "Descending")
                    {
                        query.OrderBy(t => Convert.ToDecimal(t.Price));
                    }
                    else
                    {
                        query.OrderByDescending(t => Convert.ToDecimal(t.Price));
                    }
                    break;

                case "EventDate":
                    if (request.OrderState != "Descending")
                    {
                        query.OrderBy(t => t.EventDate);
                    }
                    else
                    {
                        query.OrderByDescending(t => t.EventDate);
                    }
                    break;

                case "Quota":
                    if (request.OrderState != "Descending")
                    {
                        query.OrderBy(t => t.Quota);
                    }
                    else
                    {
                        query.OrderByDescending(t => t.Quota);
                    }
                    break;

                default:
                    query = query.OrderBy(t => t.TicketCode);
                    break;
            }

            var datas = await query.AsNoTracking().ToListAsync(cancellationToken);

            var response = new GetAvailableTicketsResponse
            {
                AvailableTicketDatas = datas
            };

            return response;
        }
    }
}
