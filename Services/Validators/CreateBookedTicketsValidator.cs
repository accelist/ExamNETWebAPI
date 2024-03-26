using Contracts.RequestModels.BookedTickets;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class CreateBookedTicketsValidator : AbstractValidator<CreateBookedTicketRequest>
    {
        private readonly DBContext _db;

        public CreateBookedTicketsValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.TicketCode)
                .NotEmpty().WithMessage("Ticket Code can't be empty")
                .MustAsync(IsAvailable).WithMessage("Ticket Code Doesn't Exist");

            RuleFor(Q => Q.Quantity)
                .NotEmpty().WithMessage("Quantity can't be 0");
                //.MustAsync(IsOverQuantity).WithMessage("Ticket quota can't provide your request");
        }

        private async Task<bool> IsAvailable(string ticketCode, CancellationToken cancellationToken)
        {
            var isTicketCodeExist = await _db.Tickets.Where(Q => Q.TicketCode == ticketCode)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return isTicketCodeExist;
        }

        private async Task<bool> IsOverQuantity(int quantity, CancellationToken cancellationToken)
        {
            var isQuantityOver = await _db.Tickets.Where(Q => Q.Quota >= quantity)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return isQuantityOver;
        }
    }
}
