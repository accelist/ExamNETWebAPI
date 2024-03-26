using Contracts.ResponseModels;
using MediatR;

namespace Contracts.RequestModels
{
    public class GetAvailableTicketsRequest : IRequest<GetAvailableTicketsResponse>
    {
        public string CategoryName { get; set; } = string.Empty;

        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set;} = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public DateTimeOffset? MinEventDate { get; set; }

        public DateTimeOffset? MaxEventDate {  get; set; }

        // order by "semua kolom dari yang ditampilkan di result."
        public string OrderBy {  get; set; } = string.Empty;

        // "kolom penentu untuk data akan diurutkan secara ascending atau descending."
        public string OrderState {  get; set; } = string.Empty;
    }
}
