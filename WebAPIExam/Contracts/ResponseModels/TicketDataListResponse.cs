
namespace Contracts.ResponseModels
{
    public class TicketDataListResponse
    {
        public List<TicketData> TicketDatas { get; set; } = new List<TicketData>();

    }

    public class TicketData
    {
        public DateTime EventDate { get; set; }

        public int Quota { get; set; }

        public string TicketCode { get; set; } = string.Empty;

        public string TicketName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}

