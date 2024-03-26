using FluentValidation;

using Contracts.RequestModels;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class BookTicketValidator : AbstractValidator<BookTicketRequest>
    {
        private readonly DBContext _db;

        public BookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.BuyQuantity).NotEmpty().WithMessage("Buy quantity can't be empty");
            RuleFor(Q => Q.TicketCode)
                .MustAsync(TicketExists).WithMessage("Ticket doesn't exist.")
                .DependentRules(() =>
                {
                    RuleFor(Q => Q.TicketCode)
                        .MustAsync(TicketHasQuota).WithMessage("Ticket quota is exhausted")
                        .MustAsync((request, ticketCode, cancellationToken) => QuantityIsValid(ticketCode, request.BuyQuantity, cancellationToken)).WithMessage("Quantity exceeds available quota.");
                });
            //test pull request kedua

        }

        private async Task<bool> TicketExists(Guid TicketCode, CancellationToken cancellationToken)
        {
            return await _db.Tickets.AnyAsync(t => t.TicketCode == TicketCode, cancellationToken: cancellationToken);
        }

        private async Task<bool> TicketHasQuota(Guid TicketCode, CancellationToken cancellationToken)
        {
            var ticket = await _db.Tickets.FirstOrDefaultAsync(t => t.TicketCode.Equals(TicketCode), cancellationToken: cancellationToken);
            if (ticket != null)
            {
                if (ticket.Quota <= 0)
                {
                    _db.Tickets.Remove(ticket);
                    await _db.SaveChangesAsync(cancellationToken);
                    return false;
                }
                return true;
            }
            return false;
        }


        private async Task<bool> QuantityIsValid(Guid ticketCode, decimal quantity, CancellationToken cancellationToken)
        {
            var ticket = await _db.Tickets.FirstOrDefaultAsync(t => t.TicketCode == ticketCode, cancellationToken: cancellationToken);
            return ticket != null && quantity <= ticket.Quota;
        }

    }
}

