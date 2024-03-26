namespace Contracts.ResponseModels
{
    public class GetBookingDetailResponse
    {
        public List<GetBookingDetailDataListModel> DataList { get; set; } = new List<GetBookingDetailDataListModel>();
    }

    public class GetBookingDetailDataListModel
    {
        public int QtyPerCategory { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public List<GetBookingDetailDataModel> Tickets { get; set; } = new List<GetBookingDetailDataModel>();
    }

    public class GetBookingDetailDataModel
    {
        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set; } = string.Empty;

        public DateTimeOffset? EventDate { get; set; }
    }
}
