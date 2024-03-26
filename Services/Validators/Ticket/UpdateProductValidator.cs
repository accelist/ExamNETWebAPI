using System;
using Contracts.RequestModels.Product;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators.Product
{
	public class UpdateProductValidator : AbstractValidator<UpdateProductDataRequest>
	{
        private readonly DBContext _db;

        public UpdateProductValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.Name).NotEmpty().WithMessage("Name Can't be Empty")
                .MaximumLength(50).WithMessage("Max 50 Characters");

            RuleFor(Q => Q.Price).NotEmpty().WithMessage("Price Can't be Empty")
                .GreaterThanOrEqualTo(10000).WithMessage("Price at Least 10000");
        }

       
        
    }
}

