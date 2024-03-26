using Contracts.Request.BookedTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class DeleteBookedValidator: AbstractValidator<DeleteBookRequesr>
    {
        private readonly DBContext _db;
        public DeleteBookedValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.BookedId)
                .NotEmpty().WithMessage("Booked id cannot be empty.")
                .MustAsync(CheckBookedId).WithMessage("Booked id cannot be found!");
            RuleFor(Q => Q.TicketCode)
               .NotEmpty().WithMessage("Ticket code cannot be empty.")
               .MustAsync(CheckTicketCode).WithMessage("Ticket code cannot be found!");
            RuleFor(Q => Q.Quantity).NotEmpty().WithMessage("Quantity cannot be empty.")
                .MustAsync(CheckQuotaDelete).WithMessage("Quantity cannot exceed quantity of booked tickets!")
                .GreaterThan(0).WithMessage("Quantity cannot be minus!");
        }

        public async Task<bool> CheckBookedId(Guid id, CancellationToken cancellationToken)
        {
            var idExist = await _db.BookedTickets.Where(Q => Q.BookedTicketId == id)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return idExist;
        }

        public async Task<bool> CheckTicketCode(string code, CancellationToken cancellationToken)
        {
            var idExist = await _db.Tickets.Where(Q => Q.TicketCode == code)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return idExist;
        }

        public async Task<bool> CheckQuotaDelete(int quota, CancellationToken cancellationToken)
        {
            var idExist = await _db.BookedTickets.Where(Q => Q.Quantity < quota)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return !idExist;
        }
    }
}
