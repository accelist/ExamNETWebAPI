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
    public class GetBookedValidator: AbstractValidator<GetBookedRequest>
    {
        private readonly DBContext _db;
        public GetBookedValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.BookedId)
                .NotEmpty().WithMessage("Booked id cannot be empty.")
                .MustAsync(CheckBookedId).WithMessage("Booked id cannot be found!");
        }

        public async Task<bool> CheckBookedId(Guid id, CancellationToken cancellationToken)
        {
            var idExist = await _db.BookedTickets.Where(Q => Q.BookedTicketId == id)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return idExist;
        }
    }
}
