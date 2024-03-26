using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.Category
{
    public class GetCategoryDataResponse 
    {
        public List<GetCategoryDataModel> CategoriDatas { get; set; } = new List<GetCategoryDataModel>();
    }
    public class GetCategoryDataModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
