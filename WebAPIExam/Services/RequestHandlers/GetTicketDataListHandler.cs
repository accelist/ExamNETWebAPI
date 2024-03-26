﻿using Contracts.RequestModels;
using Contracts.ResponseModels;
using Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers
{
    public class GetTicketDataListHandler : IRequestHandler<TicketDataListRequest, TicketDataListResponse>
    {
        private readonly DBContext _db;

        public GetTicketDataListHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<TicketDataListResponse> Handle(TicketDataListRequest request, CancellationToken cancellationToken)
        {
            var datas = await _db.Tickets.Select(Q => new TicketData
            {
                Quota = Q.Quota,
                TicketCode = Q.TicketCode,
                TicketName = Q.TicketName,
                CategoryName = Q.CategoryName,
                Price = Q.Price,
            }).AsNoTracking()
            .ToListAsync(cancellationToken);

            var response = new TicketDataListResponse
            {
                TicketDatas = datas
            };

            return response;
        }
    }
}
