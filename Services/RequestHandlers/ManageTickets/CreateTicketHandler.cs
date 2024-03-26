using Contracts.RequestModels.Ticket;
using Contracts.ResponseModels.Ticket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageTickets
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketRequest, CreateTicketResponse>
    {
        private readonly DBContext _db;
        public CreateTicketHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<CreateTicketResponse> Handle(CreateTicketRequest request, CancellationToken ct)
        {
            var isDataExist = await _db.Categories.Where(Q => Q.CategoryId == request.CategoryId).FirstOrDefaultAsync();
            if(isDataExist == null)
            {
                return new CreateTicketResponse
                {
                    Message = "There is no CategoryId"
                };
            }
            var word = isDataExist.CategoryName.Split(" ");
            var ticketedCode = string.Join(" ", word.Select(Q => Q[0].ToString().ToUpper()));

            int counter = await _db.Tickets.CountAsync(q => q.TicketCode.StartsWith(ticketedCode));
            string ticketNumber = (counter + 1).ToString("D3");
            string ticketCode = $"{ticketedCode}{ticketNumber}";

            var data = new Ticket
            {
                CategoryId = isDataExist.CategoryId,
                EventDate = DateTime.Now,
                Quota = request.Quota,
                TicketName = request.TicketName,
                TicketCode = ticketCode,
                Price = request.Price,
            };
            _db.Tickets.Add(data);
            await _db.SaveChangesAsync(ct);

            return new CreateTicketResponse
            {
                Message = $"Sucessfully Created WithId {ticketCode}" 

            };


        }
    }

}
