using Contracts.RequestModels.BookedTicket;
using Contracts.ResponseModels.BookedTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RequestHandlers.ManageBooked
{
    public class CreatedBookedHandler : IRequestHandler<CreateBookedTicketRequest, CreateBookedTicketResponse>
    {
        private readonly DBContext _db;

        public CreatedBookedHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<CreateBookedTicketResponse> Handle(CreateBookedTicketRequest request, CancellationToken ct)
        {
            
            var model = new CreateBookedTicketRequest
            {
                TicketCode = request.TicketCode,
                Quantity = request.Quantity,
            };
            var data = await (from tck in _db.Tickets
                                      join ctg in _db.Categories on tck.CategoryId equals ctg.CategoryId
                                      select ctg.CategoryId).FirstOrDefaultAsync(ct);
            var bookedTicketDat = new BookedTicket
            {
                BookId = Guid.NewGuid(),
                TicketCode = model.TicketCode,
                Quantity = model.Quantity,
                CategoryId = data
            };
            var updateCounter = await _db.Tickets.Where(Q => Q.TicketCode == request.TicketCode).FirstOrDefaultAsync();
           
            if(updateCounter != null)
            {
                if (updateCounter.Quota > model.Quantity)
                {
                    updateCounter.Quota -= model.Quantity;
                }
                else
                {
                    return new CreateBookedTicketResponse
                    {
                        Message = "You are out of quota!"
                    };
                }
            }
            else
            {
                return new CreateBookedTicketResponse
                {
                    Message = "TicketCode is Empty!"
                };
            }
            
            _db.BookedTickets.Add(bookedTicketDat);
            await _db.SaveChangesAsync(ct);
            return new CreateBookedTicketResponse
            {
                Message = "Successfully Created!",
                BookId = bookedTicketDat.BookId
            };
        }
    }
}
