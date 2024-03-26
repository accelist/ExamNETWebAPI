
using System;
using Contracts.RequestModels;
using Contracts.RequestModels.BookedTicket;
using Entity;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace Services.Validators
{
    public class CreateBookTicketValidator : AbstractValidator<CreateBookedTicketRequest>
    {
        private readonly DBContext _db;

        public CreateBookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.TicketCode).NotEmpty().WithMessage("Ticket Code cannot be empty")
                .MustAsync(AvailablesTicket).WithMessage("Email cannot be the same");
            RuleFor(Q => Q.Quantity).NotEmpty().WithMessage("Email cannot be empty");

        }

        public async Task<bool> AvailablesTicket(string TicketCode, CancellationToken cancellationToken)
        {
            var existingTicket = await _db.Tickets.Where(Q => Q.TicketCode == TicketCode).AsNoTracking().AnyAsync();
            return existingTicket;
        }
    }
}
