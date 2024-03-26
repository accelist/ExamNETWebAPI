using System;
namespace Contracts.Response.Ticket
{
	public class TicketDataListResponse
	{
		List<TicketData> TicketDatas { get; set; } = new List<TicketData>();
	}

	public class TicketData
	{
		public int Quota { get; set; }
		public string TicketCode { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string CategoryName { get; set; } = string.Empty;
		public decimal Price { get; set; }
	}
}

