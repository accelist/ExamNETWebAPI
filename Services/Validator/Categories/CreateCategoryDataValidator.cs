using Contracts.RequestModels.Category;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validator.Categories
{
    public class CreateCategoryDataValidator : AbstractValidator <CreateCategoryDataRequest>
    {
        private readonly DBContext _db;
        public CreateCategoryDataValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.CategoryName).MustAsync(IsDataExist).WithMessage("The Category you give is already exist!")
                .NotEmpty().WithMessage("The Category Name Cannot Be Empty!");
            RuleFor(Q => Q.Quantity).NotEmpty().WithMessage("The Quantity must more than 0!")
                .GreaterThanOrEqualTo(0).WithMessage("The Quantity must more than 0!");
        }

        private async Task<bool> IsDataExist (string name , CancellationToken ct)
        {
            var isExistData = await _db.Categories.Where(Q => Q.CategoryName == name).AsNoTracking().AnyAsync(ct);
            return !isExistData;
        }
    }
}
