using Contracts.RequestModels.Category;
using Contracts.ResponseModels.Category;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageCategories
{
    public class GetCategoryHandler : IRequestHandler <GetCategoryDataRequest, GetCategoryDataResponse>
    {
        private readonly DBContext _db;
        public GetCategoryHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<GetCategoryDataResponse> Handle (GetCategoryDataRequest request, CancellationToken ct)
        {
            var datas = await _db.Categories.Select(Q => new GetCategoryDataModel
            {
                CategoryId = Q.CategoryId,
                CategoryName = Q.CategoryName,
                Quantity = Q.Quantity
            }).ToListAsync(ct);
            var response = new GetCategoryDataResponse
            {
                CategoriDatas = datas
            };
            return response;

        }
    }
}
