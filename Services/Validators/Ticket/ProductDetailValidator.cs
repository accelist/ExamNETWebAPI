using System;
using Contracts.RequestModels.Customer;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators
{
    public class ProductDetailValidator : AbstractValidator<ProductDetailRequest>
    {
        private readonly DBContext _db;

        public ProductDetailValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.ProductId)
                .NotEmpty().WithMessage("ID Can't be Empty")
                .MustAsync(BeAvailableId).WithMessage("ID not Exist");




        }


        public async Task<bool> BeAvailableId(Guid? id, CancellationToken cancellationToken)
        {
            var existingId = await _db.Products.Where(Q => Q.ProductId == id)
                .AsNoTracking().AnyAsync();

            return existingId;
        }

    }
}

