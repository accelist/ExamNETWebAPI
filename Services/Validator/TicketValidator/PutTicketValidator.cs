using Contracts.Request;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators.Cart
{
    public class PutTicketValidator : AbstractValidator<TicketModel2>
    {
        private readonly DBContext _db;

        public PutTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.TicketCode).NotEmpty().WithMessage("Ticket Code cannot be empty.")
                .MaximumLength(50).WithMessage("Maximum 50 characters.");

            RuleFor(Q => Q.Quantity).NotEmpty().WithMessage("Quantity cannot be empty.")
                .GreaterThan(1).WithMessage("Quantity at least 1");
        }
    }
}
