
using Contracts.ResponseModels.Category;
using MediatR;

namespace Contracts.RequestModels.Category
{
    public class CategoryDataListRequest : IRequest<CategoryDataListResponse>
    {
        public Guid CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int Quota { get; set; }
    }
}


