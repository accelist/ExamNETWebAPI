using Contracts.RequestModels.BookedTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class BookedTicketDetailValidator : AbstractValidator<BookedTicketDetailRequest>
    {
        private readonly DBContext _db;
        public BookedTicketDetailValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.BookedId).NotEmpty().MustAsync(CheckBookedTicketIdExists).WithMessage("Booked Ticked ID does not exist");
        }
        public async Task<bool> CheckBookedTicketIdExists(Guid id, CancellationToken cancellationToken)
        {
            var response = await _db.BookedTickets.Where(Q => Q.BookedId == id).AsNoTracking().AnyAsync(cancellationToken);
            return response;
        }
    }
}
