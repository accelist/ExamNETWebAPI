
using Contracts.RequestModels.Category;
using Contracts.ResponseModels.Category;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Handlers.HandleCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, GetCategoryResponse>
    {
        private readonly DBContext _db;
        public GetCategoryHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var dataList = await _db.Categories.Select(Q=>new GetCategoryModel
            {
                CategoryId = Q.CategoryId,
                CategoryName = Q.CategoryName,
            }).AsNoTracking().ToListAsync(cancellationToken);
            var response = new GetCategoryResponse()
            {
                categoryList = dataList
            };
            return response;
        }
    }
}
