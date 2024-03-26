using Contracts.ResponseModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestModels
{
    public class DetailBookedRequest : IRequest<DetailBookedResponse>
    {
        public Guid BookId { get; set; }
    }
}
