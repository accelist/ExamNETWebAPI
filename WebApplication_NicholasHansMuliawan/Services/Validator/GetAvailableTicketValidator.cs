using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.TicketData.RequestModels;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validator
{
    public class GetAvailableTicketValidator : AbstractValidator<GetTicketDataListRequest>
    {
        private readonly DBContext _db;

        public GetAvailableTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.CategoryName)
                .NotEmpty().When(Q => !string.IsNullOrEmpty(Q.CategoryName))
                .MustAsync(ExistingCategory).When(Q => !string.IsNullOrEmpty(Q.CategoryName))
                .WithMessage("Category does not exist.");

            RuleFor(Q => Q.TicketCode)
                .NotEmpty().When(Q => !string.IsNullOrEmpty(Q.TicketCode))
                .MustAsync(ExistingCode).When(Q => !string.IsNullOrEmpty(Q.TicketCode))
                .WithMessage("Ticket Code does not exist.");

            RuleFor(Q => Q.OrderState)
                .NotEmpty().When(Q => !string.IsNullOrEmpty(Q.OrderState))
                .Must(Q => Q.ToLower() == "ascending" || Q.ToLower() == "descending").When(Q => !string.IsNullOrEmpty(Q.OrderState))
                .WithMessage("OrderBy must be 'ascending' or 'descending'.");
        }

        private async Task<bool> ExistingCategory(string category, CancellationToken cancellationToken)
        {
            var exist = await _db.Tickets
                .Where(Q => Q.CategoryName == category)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return exist;
        }
        private async Task<bool> ExistingCode(string code, CancellationToken cancellationToken)
        {
            var exist = await _db.Tickets
                .Where(Q => Q.TicketCode == code)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return exist;
        }
    }
}
