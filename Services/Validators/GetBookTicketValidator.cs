using Contracts.RequestModels.BookedTickets;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class GetBookTicketValidator : AbstractValidator<GetBookedTicketDataRequest>
    {
        private readonly DBContext _db;

        public GetBookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.TicketId)
                .NotEmpty().WithMessage("Ticket Id can't be empty")
                .MustAsync(IsAvailable).WithMessage("Ticket Code Doesn't Exist");

            
        }

        private async Task<bool> IsAvailable(Guid ticketId, CancellationToken cancellationToken)
        {
            var isExist = await _db.BookedTickets.Where(Q => Q.TicketId ==  ticketId)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return isExist;
        }
    }
}



