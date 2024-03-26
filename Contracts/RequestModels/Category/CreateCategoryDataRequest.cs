﻿using Contracts.ResponseModels.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels.Category
{
    public class CreateCategoryDataRequest : IRequest<CreateCategoryDataResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}
