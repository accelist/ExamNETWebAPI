
namespace Contracts.ResponseModels.Ticket
{
    public class CreateTicketResponse
    {
        public Guid TicketId { get; set; }
        public string TicketName { get; set;} = string.Empty;
    }
}
