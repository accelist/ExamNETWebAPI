using Contracts.RequestModels.Ticket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validator.Ticket
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
    {
        private readonly DBContext _db;
        public CreateTicketValidator(DBContext db)
        {
            _db = db;
            RuleFor (Q => Q.CategoryId).MustAsync(isCategoryExist).WithMessage("The Following Category ID doesnt Exist!)");
        }
        private async Task<bool> isCategoryExist(Guid id, CancellationToken ct)
        {
            var isExist = await _db.Categories.Where(Q => Q.CategoryId == id).AsNoTracking().AnyAsync(ct);
            return isExist;

        }
    }    
}

