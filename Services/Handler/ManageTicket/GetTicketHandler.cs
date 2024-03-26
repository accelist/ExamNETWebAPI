using Contracts.Request;
using Contracts.Response;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handler.ManageTicket
{
    public class GetTicketHandler : IRequestHandler<GetTicketRequest, GetTicketResponse>
    {
        private readonly DBContext _db;

        public GetTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetTicketResponse> Handle(GetTicketRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                query = query.Where(Q => Q.CategoryName.Contains(request.CategoryName));
            }

            if (!string.IsNullOrEmpty(request.TicketCode))
            {
                query = query.Where(Q => Q.TicketCode.Contains(request.TicketCode));
            }

            if (!string.IsNullOrEmpty(request.TicketName))
            {
                query = query.Where(Q => Q.TicketName.Contains(request.TicketName));
            }

            if (request.Price > 0)
            {
                query = query.Where(Q => Q.Price <= request.Price);
            }

            if (request.MinDate != default && request.MaxDate != default)
            {
                query = query.Where(Q => Q.EventDate >= request.MinDate && Q.EventDate <= request.MaxDate);
            }

            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                query = Sort(query, request.OrderBy, request.OrderState);
            }

            var datas = await query.Select(Q => new TicketData
            {
                TicketID = Q.TicketID,
                CategoryName = Q.CategoryName,
                TicketCode = Q.TicketCode,
                TicketName = Q.TicketName,
                EventDate = Q.EventDate,
                Price = Q.Price,
                Quota = Q.Quota
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            var response = new GetTicketResponse
            {
                TicketDatas = datas
            };

            return response;
        }

        private IQueryable<Ticket> Sort(IQueryable<Ticket> query, string orderBy, string orderState)
        {
            switch (orderBy)
            {
                case "CategoryName":
                    query = orderState == "asc" ? query.OrderBy(Q => Q.CategoryName) : query.OrderByDescending(Q => Q.CategoryName);
                    break;
                case "TicketCode":
                    query = orderState == "asc" ? query.OrderBy(Q => Q.TicketCode) : query.OrderByDescending(Q => Q.TicketCode);
                    break;
                default:
                    query = query.OrderBy(Q => Q.TicketID);
                    break;
            }

            return query;
        }
    }
}
