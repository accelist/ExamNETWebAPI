using Contracts.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class DeleteBookedValidator : AbstractValidator<DeleteBookedTicketRequest>
    {
        public DeleteBookedValidator() 
        {
            RuleFor(Q => Q.BookId).NotEmpty().WithMessage("Book Id can't be empty");
            RuleFor(Q => Q.TicketCode).NotEmpty().WithMessage("Ticket code can't be empty");
            RuleFor(Q => Q.BuyQuantity).NotEmpty().WithMessage("Buy Quantity can't be empty")
                .GreaterThanOrEqualTo(0).WithMessage("It has to be more than 0");
        }  
    }
}
