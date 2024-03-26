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
    public class PostTicketDataHandler : AbstractValidator<PostBookTicketRequest>
    {
        private readonly DBContext _db;

        public PostTicketDataHandler(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.TicketBookings)
                .NotEmpty()
                .WithMessage("At least one ticket booking must be provided.");

            RuleFor(Q => Q.TicketBookings)
                .NotEmpty()
                .MustAsync(ExistingCode)
                .WithMessage("Ticket Code does not exist.");

            RuleForEach(Q => Q.TicketBookings)
                .MustAsync((request, booking, cancellationToken) => QuantityGreaterThanZero(booking.Quantity))
                .WithMessage("Quantity must be greater than 0.");

        }

        private async Task<bool> ExistingCode(List<TicketBookingModel> ticketBookings, CancellationToken cancellationToken)
        {
            foreach (var booking in ticketBookings)
            {
                var exists = await _db.Tickets
                    .AnyAsync(t => t.TicketCode == booking.TicketCode, cancellationToken);

                if (!exists)
                {
                    return false;
                }
            }

            return true;
        }

        private Task<bool> QuantityGreaterThanZero(int quantity)
        {
            return Task.FromResult(quantity > 0);
        }
    }
}
