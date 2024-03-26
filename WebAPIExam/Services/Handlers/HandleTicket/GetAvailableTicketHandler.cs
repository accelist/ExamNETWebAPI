
using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.HandleTicket
{
    public class GetAvailableTicketHandler : IRequestHandler<GetAvailableTicketRequest,  GetAvailableTicketResponse>
    {
        private readonly DBContext _db;
        public GetAvailableTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetAvailableTicketResponse> Handle(GetAvailableTicketRequest request, CancellationToken cancellationToken)
        {
            var dataList = await _db.Tickets.Include(Q => Q.Category).Where(Q => Q.Quota > 0)
                .Select(Q=> new GetAvailableTicketModel
                {
                    EventDate = Q.EventDate,
                    Quota = Q.Quota,
                    TicketCode = Q.TicketCode,
                    TicketName  = Q.TicketName,
                    CategoryName = (Q.Category != null)? Q.Category.CategoryName : string.Empty,
                    Price = Q.Price,
                }).AsNoTracking().OrderBy(Q=>Q.TicketCode).ToListAsync(cancellationToken);
            //Query Logic
            var response = new GetAvailableTicketResponse();
            if (request == null || (request.CategoryName == string.Empty &&
                                    request.TicketCode == string.Empty &&
                                    request.TicketName == string.Empty &&
                                    request.Price == default &&
                                    request.EventDate == default &&
                                    request.MinimumDate == default &&
                                    request.MaximumDate == default &&
                                    request.OrderBy == string.Empty &&
                                    request.SortAscending == true))
            {
                response.AvailableTickets = dataList;
            }
            else
            {
                if(request.CategoryName != string.Empty)
                {
                    dataList = dataList.Where(Q => Q.CategoryName.Contains(request.CategoryName)).ToList();
                }
                if (request.TicketCode != string.Empty)
                {
                    dataList = dataList.Where(Q => Q.TicketCode.Contains(request.TicketCode)).ToList();
                }
                if (request.TicketName != string.Empty)
                {
                    dataList = dataList.Where(Q => Q.TicketName.Contains(request.TicketName)).ToList();
                }
                if (request.Price != default)
                {
                    dataList = dataList.Where(Q => Q.Price == request.Price).ToList();
                }
                if (request.EventDate != default)
                {
                    dataList = dataList.Where(Q => Q.EventDate == request.EventDate).ToList();
                }
                else
                {
                    if (request.CategoryName != default)
                    {
                        dataList = dataList.Where(Q => Q.EventDate > request.MinimumDate).ToList();
                    }
                    if (request.MaximumDate != default)
                    {
                        dataList = dataList.Where(Q => Q.EventDate < request.MaximumDate).ToList();
                    }
                }
                if (request.OrderBy != string.Empty)
                {
                    var orderBy = request.OrderBy.ToLower();
                    if (request.SortAscending == true)
                    {
                        switch (orderBy)
                        {
                            case "categoryname":
                            case "category":
                                dataList = dataList.OrderBy(Q => Q.CategoryName).ToList();
                                break;
                            case "ticketcode":
                            case "ticket":
                                dataList = dataList.OrderBy(Q => Q.TicketCode).ToList();
                                break;
                            case "ticketname":
                                dataList = dataList.OrderBy(Q => Q.TicketName).ToList();
                                break;
                            case "price":
                                dataList = dataList.OrderBy(Q => Q.Price).ToList();
                                break;
                            case "eventdate":
                            case "date":
                                dataList = dataList.OrderBy(Q => Q.EventDate).ToList();
                                break;
                            default:
                                dataList = dataList.OrderBy(Q => Q.TicketCode).ToList();
                                break;
                        }
                    }
                    else
                    {
                        switch (orderBy)
                        {
                            case "categoryname":
                            case "category":
                                dataList = dataList.OrderByDescending(Q => Q.CategoryName).ToList();
                                break;
                            case "ticketcode":
                            case "ticket":
                                dataList = dataList.OrderByDescending(Q => Q.TicketCode).ToList();
                                break;
                            case "ticketname":
                                dataList = dataList.OrderByDescending(Q => Q.TicketName).ToList();
                                break;
                            case "price":
                                dataList = dataList.OrderByDescending(Q => Q.Price).ToList();
                                break;
                            case "eventdate":
                            case "date":
                                dataList = dataList.OrderByDescending(Q => Q.EventDate).ToList();
                                break;
                            default:
                                dataList = dataList.OrderByDescending(Q => Q.TicketCode).ToList();
                                break;
                        }
                    }
                }
                response.AvailableTickets = dataList;
            }

            return response;
        }
    }
}
