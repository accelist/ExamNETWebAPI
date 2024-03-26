using Contracts.RequestModels.Booking;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators.Booking
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDataRequest>
    {
        private readonly DBContext _db;

        public CreateBookingValidator(DBContext db)
        {
            _db = db;

            RuleFor(x => x.TicketCode).NotEmpty().MustAsync(TicketCodeExists).WithMessage("Ticket not found!");
        }

        public async Task<bool> TicketCodeExists(string code, CancellationToken ct)
        {
            var result = await _db.Tickets.Where(x => x.TicketCode == code).AsNoTracking().AnyAsync(ct);
            return result;
        }
        //public async Task<bool> OrderMoreThanExist(string code, CancellationToken ct)
        //{
        //    var result = await _db.Tickets.Where(x => x.TicketCode == code).Select(x => x.Quota).FirstOrDefaultAsync(ct);
        //}
    }
}
