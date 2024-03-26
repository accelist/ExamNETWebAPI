using Contracts.RequestModels.BookedTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class BookTicketValidator : AbstractValidator<BookTicketRequest>
    {
        private readonly DBContext _db;
        public BookTicketValidator(DBContext db)
        {
            _db = db;
            RuleForEach(Q=>Q.BookingList)
                .ChildRules(child => 
                child.RuleFor(X=>X.TicketId).MustAsync(CheckTicketIdExists).WithMessage("Ticket does not exist")
                                            .MustAsync(CheckQuota).WithMessage("Quota insufficient"));
        }

        public async Task<bool> CheckTicketIdExists(Guid id, CancellationToken cancellationToken)
        {
            var response = await _db.Tickets.Where(Q => Q.TicketId == id).AsNoTracking().AnyAsync(cancellationToken);
            return response;
        }
        public async Task<bool> CheckQuota(Guid id, CancellationToken cancellationToken)
        {
            var response = await _db.Tickets.Where(Q => Q.Quota > 0).AsNoTracking().AnyAsync(cancellationToken);
            return response;
        }
    }
}
