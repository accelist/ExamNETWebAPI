using Contracts.RequestModels.Category;
using Contracts.ResponseModels.Category;
using Entity.Entity;
using MediatR;

namespace Services.Handlers.HandleCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        private readonly DBContext _db;
        public CreateCategoryHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var data = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = request.CategoryName
            };
            _db.Add(data);
            await _db.SaveChangesAsync(cancellationToken);

            return new CreateCategoryResponse { CategoryId = data.CategoryId };
        }
    }
}
