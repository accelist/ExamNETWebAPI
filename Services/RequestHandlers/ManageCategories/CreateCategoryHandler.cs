using Contracts.RequestModels.Category;
using Contracts.ResponseModels.Category;
using Entity.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageCategories
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryDataRequest, CreateCategoryDataResponse>
    {
        private readonly DBContext _db;
        public CreateCategoryHandler(DBContext db)
        {
            _db = db;
        }

        public async Task <CreateCategoryDataResponse> Handle (CreateCategoryDataRequest request, CancellationToken ct)
        {
            var data = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = request.CategoryName,
                Quantity = request.Quantity
            };
            _db.Categories.Add(data);
            await _db.SaveChangesAsync(ct);
            var response = new CreateCategoryDataResponse
            {
                CategoryId = data.CategoryId,
            };
            return response;
        }
    }
}
