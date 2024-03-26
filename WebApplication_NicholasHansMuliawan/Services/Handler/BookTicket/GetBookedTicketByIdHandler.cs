using Contracts.BookTicketModel.RequestModel;
using Contracts.BookTicketModel.ResponseModel;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handler.BookTicket
{
    public class GetBookedTicketByIdHandler : IRequestHandler<GetBookedTicketByIdRequest, GetBookedTicketByIdResponse>
    {
        private readonly DBContext _db;

        public GetBookedTicketByIdHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookedTicketByIdResponse> Handle(GetBookedTicketByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new GetBookedTicketByIdResponse();

            var bookedTicket = await _db.BookTickets
                .Include(bt => bt.Ticket)
                .FirstOrDefaultAsync(bt => bt.BookedTicketID == request.BookedId, cancellationToken);

            var bookedTicketsByCategory = _db.BookTickets
                .Include(Q => Q.Ticket)
                .Where(Q => Q.BookedTicketID == request.BookedId)
                .GroupBy(Q => Q.Ticket.CategoryName);

            foreach (var categoryGroup in bookedTicketsByCategory)
            {
                var categoryDetail = new CategoryDetail
                {
                    CategoryName = categoryGroup.Key,
                    QtyPerCategory = categoryGroup.Sum(Q => Q.Quantity),
                    Tickets = categoryGroup.Select(Q => new GetTicketDetail
                    {
                        TicketCode = Q.Ticket.TicketCode,
                        TicketName = Q.Ticket.TicketName,
                        Date = Q.Ticket.Date
                    }).ToList()
                };

                response.Categories.Add(categoryDetail);
            }

            return response;
        }
    }
}
