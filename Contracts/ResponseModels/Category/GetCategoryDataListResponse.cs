﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ResponseModels.Category
{
    public class GetCategoryDataListResponse
    {
        public List<CategoryData> CategoryDatas = new List<CategoryData>();
    }

    public class CategoryData
    { 
        public Guid CategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
    }

}

