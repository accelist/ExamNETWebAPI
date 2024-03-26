using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ResponseModels.Category
{
    public class CategoryDataListResponse
    {
        public List<CategoryData> CategoryDatas { get; set; } = new List<CategoryData>();
    }

    public class CategoryData
    {
        public Guid CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int Quota { get; set; }
    }
}

