using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModel.RequestModel;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validator
{
    public class GetBookedTicketsByIdValidator : AbstractValidator<GetBookedTicketByIdRequest>
    {
        private readonly DBContext _db;

        public GetBookedTicketsByIdValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.BookedId)
                .NotEmpty()
                .MustAsync(ExistingCode)
                .WithMessage("Booked Ticket Id does not exist.");
        }

        private async Task<bool> ExistingCode(Guid bookedId, CancellationToken cancellationToken)
        {
            var exist = await _db.BookTickets
                .AnyAsync(bt => bt.BookedTicketID == bookedId, cancellationToken);

            return exist;
        }
    }
}
