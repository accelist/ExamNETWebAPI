using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.BookTicketModel.RequestModel;
using Contracts.BookTicketModel.ResponseModel;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handler
{
    public class PostBookTicketHandler : IRequestHandler<PostBookTicketRequest, PostBookTicketResponse>
    {
        private readonly DBContext _db;

        public PostBookTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<PostBookTicketResponse> Handle(PostBookTicketRequest request, CancellationToken cancellationToken)
        {
            var response = new PostBookTicketResponse();

            var allTickets = await _db.Tickets.ToListAsync();
            var allBookedTickets = await _db.BookTickets.ToListAsync();

            var bookedTicketsByCategory = allBookedTickets
                .Join(allTickets, bookedTicket => bookedTicket.TicketID, ticket => ticket.TicketID,
                      (bookedTicket, ticket) => new { BookedTicket = bookedTicket, Ticket = ticket })
                .GroupBy(x => x.Ticket.CategoryName);

            foreach (var categoryGroup in bookedTicketsByCategory)
            {
                var categorySummary = new CategorySummary
                {
                    CategoryName = categoryGroup.Key,
                    SummaryPrice = 0,
                    Tickets = new List<TicketDetail>()
                };

                foreach (var bookedTicket in categoryGroup)
                {
                    var ticket = bookedTicket.Ticket;
                    var quantity = bookedTicket.BookedTicket.Quantity;

                    categorySummary.Tickets.Add(new TicketDetail
                    {
                        TicketCode = ticket.TicketCode,
                        TicketName = ticket.TicketName,
                        Price = ticket.Price * quantity
                    });

                    categorySummary.SummaryPrice += ticket.Price * quantity;
                }
                response.TicketsPerCategories.Add(categorySummary);
            }

            foreach (var booking in request.TicketBookings)
            {
                var ticket = allTickets.FirstOrDefault(t => t.TicketCode == booking.TicketCode);
                if (ticket != null)
                {
                    var newBookedTicket = new Entity.Entity.BookTicket
                    {
                        BookedTicketID = Guid.NewGuid(),
                        Quantity = booking.Quantity,
                        TicketID = ticket.TicketID
                    };
                    _db.BookTickets.Add(newBookedTicket);

                    var categorySummary = response.TicketsPerCategories.FirstOrDefault(c => c.CategoryName == ticket.CategoryName);
                    if (categorySummary == null)
                    {
                        categorySummary = new CategorySummary
                        {
                            CategoryName = ticket.CategoryName,
                            SummaryPrice = 0,
                            Tickets = new List<TicketDetail>()
                        };
                        response.TicketsPerCategories.Add(categorySummary);
                    }

                    categorySummary.Tickets.Add(new TicketDetail
                    {
                        TicketCode = ticket.TicketCode,
                        TicketName = ticket.TicketName,
                        Price = ticket.Price * booking.Quantity
                    });

                    categorySummary.SummaryPrice += ticket.Price * booking.Quantity;
                }
            }

            response.PriceSummary = response.TicketsPerCategories.Sum(category => category.SummaryPrice);

            await _db.SaveChangesAsync(cancellationToken);

            return response;
        }


    }
}
