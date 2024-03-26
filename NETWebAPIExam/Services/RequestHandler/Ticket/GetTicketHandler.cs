using Contracts.Request.Ticket;
using Contracts.Response.Ticket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandler.Ticket
{
    public class GetTicketHandler : IRequestHandler<GetTicketRequest,GetTicketResponse>
    {
        private readonly DBContext _db;

        public GetTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetTicketResponse> Handle(GetTicketRequest request, CancellationToken cancellationToken)
        {
            var datas = await _db.Tickets.Where(Q=>
            DateTime.Compare(Q.EventDate,request.MinDate) >=0
            && DateTime.Compare(Q.EventDate, request.MaxDate) <= 0
            || Q.TicketName.Contains(request.SearchQuery) 
            || Q.TicketCode.Contains(request.SearchQuery)
            || Q.CategoryName.Contains(request.SearchQuery)
            /*|| Q.Price.Equals(Int32.Parse(request.SearchQuery))*/
            )
                .Select(Q => new DataTicket
            {
                EventDate = Q.EventDate,
                Quota = Q.Quota,
                TicketCode = Q.TicketCode,
                TicketName = Q.TicketName,
                CategoryName = Q.CategoryName,
                Price = Q.Price,
            }).OrderBy(Q => Q.TicketCode)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            List<DataTicket> data = datas.OrderBy(Q => Q.TicketCode).ToList();

            if (request.OrderState == "asc"|| request.OrderState=="ascending")
            {
                if (request.OrderBy == "date")
                {
                    data = datas.OrderBy(Q => Q.EventDate).ToList();
                }
                else if (request.OrderBy == "quta")
                {
                    data = datas.OrderBy(Q => Q.Quota).ToList();
                }
                else if (request.OrderBy == "code")
                {
                    data = datas.OrderBy(Q => Q.TicketCode).ToList();
                }
                else if (request.OrderBy == "name")
                {
                    data = datas.OrderBy(Q => Q.TicketName).ToList();
                }
                else if (request.OrderBy == "category")
                {
                    data = datas.OrderBy(Q => Q.CategoryName).ToList();
                }
                else if (request.OrderBy == "price")
                {
                    data = datas.OrderBy(Q => Q.Price).ToList();
                }
            }
            else if (request.OrderState == "desc" || request.OrderState == "descending")
            {
                data = datas.OrderByDescending(Q => Q.TicketCode).ToList();
                if (request.OrderBy == "date")
                {
                    data = datas.OrderByDescending(Q => Q.EventDate).ToList();
                }
                else if (request.OrderBy == "quta")
                {
                    data = datas.OrderByDescending(Q => Q.Quota).ToList();
                }
                else if (request.OrderBy == "code")
                {
                    data = datas.OrderByDescending(Q => Q.TicketCode).ToList();
                }
                else if (request.OrderBy == "name")
                {
                    data = datas.OrderByDescending(Q => Q.TicketName).ToList();
                }
                else if (request.OrderBy == "category")
                {
                    data = datas.OrderByDescending(Q => Q.CategoryName).ToList();
                }
                else if (request.OrderBy == "price")
                {
                    data = datas.OrderByDescending(Q => Q.Price).ToList();
                }
            }

                var response = new GetTicketResponse
            {
                Tickets = data,
                TotalTickets = data.Count
            };

            return response;
        }
    }
}
