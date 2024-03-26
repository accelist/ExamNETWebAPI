using Contracts.RequestModels.Category;
using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Category;
using Contracts.ResponseModels.Ticket;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageCategory
{
    public class GetCategoryDataListHandler
    {
        private readonly DBContext _db;

        public GetCategoryDataListHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetCategoryDataListResponse> Handle(GetCategoryDataListRequest request, CancellationToken ct)
        {
            var datas = await _db.Categories.Select(x => new CategoryData
            {
                CategoryID = x.CategoryID,
                Name = x.Name
            }).AsNoTracking().ToListAsync();

            return new GetCategoryDataListResponse
            {
                CategoryDatas = datas
            };
        }
    }
}
