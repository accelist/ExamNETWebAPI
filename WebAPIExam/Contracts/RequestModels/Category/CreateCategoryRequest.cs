using Contracts.ResponseModels.Category;
using MediatR;

namespace Contracts.RequestModels.Category
{
    public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; set; } = string.Empty;
    }
}
