using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Contracts.TicketData.RequestModels;
using Contracts.TicketData.ResponseModels;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Services.Handler.TicketData
{
    public class GetAvailableTicketHandler : IRequestHandler<GetTicketDataListRequest, GetTicketDataListResponse>
    {
        private readonly DBContext _db;

        public GetAvailableTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetTicketDataListResponse> Handle(GetTicketDataListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Tickets> query = _db.Tickets;

            if (!string.IsNullOrEmpty(request.CategoryName))
                query = query.Where(Q => Q.CategoryName == request.CategoryName);
            if (!string.IsNullOrEmpty(request.TicketCode))
                query = query.Where(Q => Q.TicketCode == request.TicketCode);
            if (request.MinDate != default)
                query = query.Where(Q => Q.Date >= request.MinDate);
            if (request.MaxDate != default)
                query = query.Where(Q => Q.Date <= request.MaxDate);

            if (string.IsNullOrEmpty(request.OrderBy))
                request.OrderBy = "MinDate";

            switch (request.OrderState?.ToLower())
            {
                case "descending":
                    query = request.OrderBy.ToLower() switch
                    {
                        "categoryname" => query.OrderByDescending(t => t.CategoryName),
                        "ticketcode" => query.OrderByDescending(t => t.TicketCode),
                        "ticketname" => query.OrderByDescending(t => t.TicketName),
                        "eventdate" => query.OrderByDescending(t => t.Date),
                        "price" => query.OrderByDescending(t => t.Price),
                        "quota" => query.OrderByDescending(t => t.Quota),
                        _ => query.OrderByDescending(t => t.Date)
                    };
                    break;
                case "ascending":
                default:
                    query = request.OrderBy.ToLower() switch
                    {
                        "categoryname" => query.OrderBy(t => t.CategoryName),
                        "ticketcode" => query.OrderBy(t => t.TicketCode),
                        "ticketname" => query.OrderBy(t => t.TicketName),
                        "eventdate" => query.OrderBy(t => t.Date),
                        "price" => query.OrderBy(t => t.Price),
                        "quota" => query.OrderBy(t => t.Quota),
                        _ => query.OrderBy(t => t.Date)
                    };
                    break;
            }

            var ticketDatas = await query
                .Select(t => new Contracts.TicketData.ResponseModels.TicketData
                {
                    CategoryName = t.CategoryName,
                    TicketCode = t.TicketCode,
                    TicketName = t.TicketName,
                    Date = t.Date,
                    Price = t.Price,
                    Quota = t.Quota
                })
                .ToListAsync(cancellationToken);

            var response = new GetTicketDataListResponse
            {
                TicketDatas = ticketDatas,
            };

            return response;
        }
    }
}
