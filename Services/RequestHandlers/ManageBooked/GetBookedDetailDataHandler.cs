using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Entity.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageBooked
{
    public class GetBookedDetailDataHandler : IRequestHandler <GetBookedDetailDataRequest, GetBookedDetailDataResponse>
    {
        private readonly DBContext _db;
        public GetBookedDetailDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookedDetailDataResponse> Handle (GetBookedDetailDataRequest request, CancellationToken ct)
        {

        }
    }
}
