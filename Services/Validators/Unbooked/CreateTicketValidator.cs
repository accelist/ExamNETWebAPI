using Contracts.RequestModels.Unbooked;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators.Unbooked
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
    {
        private readonly DBContext _db;

        public CreateTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.TicketName).NotEmpty().WithMessage("Ticket Name can't be empty")
                .MustAsync(BeAvailableName).WithMessage("Name already exists!");
            RuleFor(Q => Q.CategoryName).NotEmpty().WithMessage("Category Name can't be empty");
            RuleFor(Q => Q.Price).NotEmpty().WithMessage("Price  can't be empty").GreaterThanOrEqualTo(0).WithMessage("Price Can't be negative");
            RuleFor(Q => Q.Quota).NotEmpty().WithMessage("Ticket Quota can't be empty");
            RuleFor(Q => Q.EventDate).NotEmpty().WithMessage("Event Date can't be empty");
        }
        private async Task<bool> BeAvailableName(string name, CancellationToken cancellationToken)
        {
            var isNameExists = await _db.Tickets.Where(Q => Q.TicketName == name).AsNoTracking().AnyAsync(cancellationToken);

            return !isNameExists;
        }

    }


}
