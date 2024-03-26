using System;
using Contracts.RequestModels;
using Contracts.RequestModels.Category;
using Contracts.ResponseModels.Category;
using Entity;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.Category
{
    public class GetCategoryDataListHandler : IRequestHandler<CategoryDataListRequest, CategoryDataListResponse>
    {
        private readonly DBContext _db;

        public GetCategoryDataListHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CategoryDataListResponse> Handle(CategoryDataListRequest request, CancellationToken cancellationToken)
        {
            var datas = await _db.Categories.Select(Q => new CategoryData
            {
                CategoryID = Q.CategoryID,
                CategoryName = Q.CategoryName,
                Quota = Q.Quota
            }).AsNoTracking()
            .ToListAsync(cancellationToken); 

            var response = new CategoryDataListResponse
            {
                CategoryDatas = datas
            };

            return response;
        }
    }
}
