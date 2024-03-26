using System;
using Contracts.RequestModels;
using Contracts.ResponseModels.Category;
using Entity;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.Category
{
    public class CreateCategoryDataHandler : IRequestHandler<CreateCategoryDataRequest, CreateCategoryDataResponse>
    {
        private readonly DBContext _db;

        public CreateCategoryDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateCategoryDataResponse> Handle(CreateCategoryDataRequest request, CancellationToken cancellationToken)
        {
            var category = new Entity.Entity.Category
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = request.Name,
                Quota = request.Quota
            };

            _db.Categories.Add(category);
            await _db.SaveChangesAsync(cancellationToken);

            var response = new CreateCategoryDataResponse
            {
                CategoryID = category.CategoryID
            };

            return response;
        }
    }
}


