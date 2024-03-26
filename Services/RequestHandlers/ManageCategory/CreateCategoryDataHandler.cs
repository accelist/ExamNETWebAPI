using Contracts.RequestModels.Category;
using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Category;
using Contracts.ResponseModels.Ticket;
using Entity.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageCategory
{
    public class CreateCategoryDataHandler : IRequestHandler<CreateCategoryDataRequest, CreateCategoryDataResponse>
    {
        private readonly DBContext _db;

        public CreateCategoryDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateCategoryDataResponse> Handle(CreateCategoryDataRequest request, CancellationToken ct)
        {
            Category category = new Category
            {
                CategoryID = Guid.NewGuid(),
                Name = request.Name
            };

            _db.Categories.Add(category);
            await _db.SaveChangesAsync(ct);

            return new CreateCategoryDataResponse
            {
                CategoryID = category.CategoryID
            };
        }
    }
}
