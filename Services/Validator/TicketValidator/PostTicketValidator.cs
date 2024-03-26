using Contracts.Request;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validator.TicketValidator
{
    public class PostTicketValidator : AbstractValidator<PostTicketRequest>
    {
        private readonly DBContext _db;

        public PostTicketValidator(DBContext db)
        {
            _db = db;

            RuleForEach(Q => Q.TicketList).ChildRules(ticket =>
            {
                ticket.RuleFor(T => T.TicketCode)
                    .MustAsync(async (ticketCode, cancellationToken) =>
                    {
                        return await _db.Tickets.AnyAsync(T => T.TicketCode == ticketCode, cancellationToken);
                    })
                    .WithMessage("Ticket code does not exist in the database.");

                /*ticket.RuleFor(T => T.Quantity)
                    .MustAsync(async (quantity, ticketContext, cancellationToken) =>
                    {
                        var ticketCode = ticketContext.TicketCode;
                        var remainingQuota = await _db.Tickets
                            .Where(T => T.TicketCode == ticketCode)
                            .Select(T => T.Quota - T.BookedQuantity)
                            .FirstOrDefaultAsync(cancellationToken);

                        return remainingQuota >= quantity;
                    })
                    .WithMessage("Quantity exceeds the remaining quota for the ticket code.");*/

                ticket.RuleFor(T => T.TicketCode)
                    .MustAsync(async (ticketCode, cancellationToken) =>
                    {
                        var ticketEntity = await _db.Tickets.FirstOrDefaultAsync(T => T.TicketCode == ticketCode, cancellationToken);
                        return ticketEntity?.EventDate > DateTime.Now;
                    })
                    .WithMessage("Event date for the ticket code must be after the ticket booking date.");
            });
        }
    }
}
