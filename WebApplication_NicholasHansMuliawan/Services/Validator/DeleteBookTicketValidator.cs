using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BookTicketModels.RequestModel;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validator
{
    public class DeleteBookTicketValidator : AbstractValidator<DeleteBookTicketRequest>
    {
        private readonly DBContext _db;

        public DeleteBookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.TicketID)
                .NotEmpty()
                .MustAsync(ExistingCode)
                .WithMessage("Ticket Code does not exist.");

            RuleFor(Q => Q.BookedId)
                .NotEmpty()
                .MustAsync(ExistingBookId)
                .WithMessage("Ticket Code does not exist.");
        }
        private async Task<bool> ExistingCode(string code, CancellationToken cancellationToken)
        {
            var exist = await _db.Tickets
                .Where(Q => Q.TicketCode == code)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return exist;
        }

        private async Task<bool> ExistingBookId(Guid id, CancellationToken cancellationToken)
        {
            var exist = await _db.BookTickets
                .Where(Q => Q.BookedTicketID == id)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return exist;
        }
    }
}
