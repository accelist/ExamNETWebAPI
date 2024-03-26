using Contracts.RequestModels.BookedTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class DeleteBookedTicketValidator : AbstractValidator<DeleteBookedTicketsRequest>
    {
        private readonly DBContext _db;
        public DeleteBookedTicketValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.BookedId).NotEmpty().MustAsync(CheckBookedTicketIdExists).WithMessage("Booked Ticked ID does not exist");
            RuleFor(Q => Q.Quantity).NotEmpty().GreaterThan(0);
            RuleFor(Q => Q.TicketCode).NotEmpty().MustAsync(CheckTicketCodeExists).WithMessage("Ticket Code not found");
        }
        public async Task<bool> CheckBookedTicketIdExists(Guid id, CancellationToken cancellationToken)
        {
            var response = await _db.BookedTickets.Where(Q => Q.BookedId == id).AsNoTracking().AnyAsync(cancellationToken);
            return response;
        }
        public async Task<bool> CheckTicketCodeExists(string id, CancellationToken cancellationToken)
        {
            var response = await _db.Tickets.Where(Q => Q.TicketCode == id).AsNoTracking().AnyAsync(cancellationToken);
            return response;
        }
    }
}
