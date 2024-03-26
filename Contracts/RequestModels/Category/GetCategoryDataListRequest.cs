using Contracts.ResponseModels.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.Category
{
    public class GetCategoryDataListRequest : IRequest<GetCategoryDataListResponse>
    {
    }
}
