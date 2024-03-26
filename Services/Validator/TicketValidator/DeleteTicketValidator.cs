using Contracts.Request;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Validator.TicketValidator
{
    public class DeleteTicketValidator : AbstractValidator<DeleteTicketRequest>
    {
        private readonly DBContext _db;

        public DeleteTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(request => request.BookedID)
                .MustAsync(BookedTicketExists)
                .WithMessage("Booked ticket ID is not registered.");

            RuleFor(request => request.TicketCode)
                .MustAsync(TicketCodeExists)
                .WithMessage("Ticket code is not registered.");

            RuleFor(request => request.Quantity)
                .MustAsync((request, quantity, cancellationToken) => QuantityIsValid(request.BookedID, quantity, cancellationToken))
                .WithMessage("Quantity exceeds the number of previously booked tickets.");
        }

        private async Task<bool> BookedTicketExists(Guid BookedID, CancellationToken cancellationToken)
        {
            return await _db.BookedTickets.AnyAsync(Q => Q.BookedID == BookedID, cancellationToken);
        }

        private async Task<bool> TicketCodeExists(string TicketCode, CancellationToken cancellationToken)
        {
            return await _db.Tickets.AnyAsync(T => T.TicketCode == TicketCode, cancellationToken);
        }

        private async Task<bool> QuantityIsValid(Guid BookedID, int quantity, CancellationToken cancellationToken)
        {
            var bookedQuantity = await _db.BookedTickets
                .Where(bt => bt.BookedID == BookedID)
                .Select(bt => bt.Quantity)
                .FirstOrDefaultAsync(cancellationToken);

            return quantity <= bookedQuantity;
        }
    }
}
