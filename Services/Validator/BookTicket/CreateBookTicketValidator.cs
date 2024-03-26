using Contracts.RequestModels.BookedTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validator.BookTicket
{
    public class CreateBookTicketValidator : AbstractValidator<CreateBookedTicketRequest>
    {
        private readonly DBContext _db;

        public CreateBookTicketValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.TicketCode).MustAsync(isExistTicketId).WithMessage("The follow ticket code you just inserted doesn't exist!")
                .NotEmpty().WithMessage("You must fill this !");
            RuleFor(Q => Q.Quantity).NotEmpty().WithMessage("You must fill this!")
                .GreaterThanOrEqualTo(0).WithMessage("The Quantity must more than 0!");
        }

        private async Task <bool> isExistTicketId (string id, CancellationToken ct)
        {
            var isExistId = await _db.Tickets.Where(Q => Q.TicketCode == id).AsNoTracking().AnyAsync(ct);
            return isExistId;   
        }
    }
}
