using Contracts.ResponseModels.Category;
using MediatR;

namespace Contracts.RequestModels
{
    public class CreateCategoryDataRequest : IRequest<CreateCategoryDataResponse>
    {
        public string Name { get; set; } = string.Empty;

        public int Quota { get; set; }
    }
}
