using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Entity.Entity;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class EditBookedTicketValidator : AbstractValidator<EditBookedTicketRequest>
    {
        private readonly DBContext _db;
        public EditBookedTicketValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.BookedId).NotEmpty().MustAsync(CheckBookedTicketIdExists).WithMessage("Booked Ticked ID does not exist");
            RuleFor(Q => Q.Quantity).NotEmpty().GreaterThan(0);
        }
        public async Task<bool> CheckBookedTicketIdExists(Guid id, CancellationToken cancellationToken)
        {
            var response = await _db.BookedTickets.Where(Q => Q.BookedId == id).AsNoTracking().AnyAsync(cancellationToken);
            return response;
        }
        public async Task<bool> CheckTicketCodeExists(string id, CancellationToken cancellationToken)
        {
            var response = await (from b in _db.BookedTickets
                                  join t in _db.Tickets on b.TicketId equals t.TicketId
                                  where t.TicketCode == id
                                  select b
                                  ).AsNoTracking().AnyAsync(cancellationToken);
            return response;
        }
    }
}
