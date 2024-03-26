using Contracts.RequestModels;
using Entity.Entity;
using FluentValidation;

namespace Services.Validators
{
    public class UpdateBookedDataValidator : AbstractValidator<UpdateBookedTicketRequest>
    {
        private readonly DBContext _db;

        public UpdateBookedDataValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.BuyQuantity).NotEmpty().WithMessage("Quantity can't be empty!").GreaterThan(1).WithMessage("Has to be at least 1qty");
        }
    }
}
