namespace Contracts.ResponseModels
{
    public class UpdateBookingResponse
    {
        public List<UpdateBookingResponseDataModel> Tickets { get; set; } = new List<UpdateBookingResponseDataModel>();
    }

    public class UpdateBookingResponseDataModel
    {
        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        public string CategoryName { get; set; } = string.Empty;
    }
}