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
    public class GetBoolDetailDataValidator : AbstractValidator <GetBookedDetailDataRequest>
    {
        private readonly DBContext _db;

        public GetBoolDetailDataValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.BookId).MustAsync(isBookIdExist).WithMessage("The BookedTicketId Doesnt Exist!").NotEmpty();
        }
        private async Task <bool> isBookIdExist (Guid id, CancellationToken ct)
        {
            var isBookIdExist = await _db.BookedTickets.Where(Q => Q.BookId == id).AsNoTracking().AnyAsync(ct);
            return isBookIdExist;
        }
    }
}
