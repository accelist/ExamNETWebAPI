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
    public class CreateBookedValidator: AbstractValidator<CreateBookedTicketRequest>
    {
        private readonly DBContext _db;

        public CreateBookedValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.Quantity).NotEmpty().WithMessage("Quantity cannot be empty.")
                .MustAsync(CheckQuota).WithMessage("Quantity cannot exceed quota!")
                .GreaterThan(0).WithMessage("Quantity cannot be minus!");

            RuleFor(Q => Q.TicketCode)
                .NotEmpty().WithMessage("Ticket code cannot be empty.")
                .MustAsync(CheckTicketCode).WithMessage("Ticket code cannot be found!");
        }
        public async Task<bool> CheckTicketCode(string code, CancellationToken cancellationToken)
        {
            var idExist = await _db.Tickets.Where(Q => Q.TicketCode == code)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return idExist;
        }

        public async Task<bool> CheckQuota(int quota, CancellationToken cancellationToken)
        {
            var idExist = await _db.Tickets.Where(Q => Q.Quota < quota )
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return !idExist;
        }
    }
}
