using Contracts.RequestModels.BookedTicket;
using Newtonsoft.Json;
using System.Text;

namespace AcceTickTest
{
    public class Tests : BaseTest
    {
        private readonly HttpClient _Client;
        public Tests()
        {
            _Client = GetClient;
        }
        [SetUp]
        public void Setup()
        {
        }

        private string Url = "api/v1/booked";

        [Test, Order(0)]
        public async Task GetAvailableTickets()
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync("api/v1/ticket/get-available-ticket");

            //assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }
        [Test, Order(1)]
        public async Task BookTicket()
        {
            //arrange
            var client = _factory.CreateClient();

            BookTicketRequest bookTickets = new BookTicketRequest
            {
                BookingList = []
            };
            bookTickets.BookingList.Add(new BookTicketModel
            {
                TicketId = Guid.Parse("D842F4AD-6703-474D-AF68-77006A055613"),
                Quantity = 5
            });
            bookTickets.BookingList.Add(new BookTicketModel
            {
                TicketId = Guid.Parse("56EDF6D8-AF0E-4504-B635-46C133CB492E"),
                Quantity = 15
            });
            bookTickets.BookingList.Add(new BookTicketModel
            {
                TicketId = Guid.Parse("267E3768-1174-4723-8C15-295839010788"),
                Quantity = 27
            });


            //act
            var stringContent = new StringContent(JsonConvert.SerializeObject(bookTickets), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{Url}/book-ticket", stringContent);

            //assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }
        [Test, Order(2)]
        public async Task DeleteBookedTickets()
        {
            //arrange
            var client = _factory.CreateClient();

            DeleteBookedTicketsRequest delete = new DeleteBookedTicketsRequest
            {
                BookedId = Guid.Parse("729FD330-EAC2-47D4-8CFD-49CB19C53923"),
                TicketCode = "TJ012",
                Quantity = 10,
            };

            //act
            var response = await client.DeleteAsync($"{Url}/revoke-ticket/{delete.BookedId}/{delete.TicketCode}/{delete.Quantity}");

            //assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }
        [Test, Order(3)]
        public async Task EditBookedTickets()
        {
            //arrange
            var client = _factory.CreateClient();

            var testId = Guid.Parse("729FD330-EAC2-47D4-8CFD-49CB19C53923");
            EditBookedTicketModel edit = new EditBookedTicketModel
            {
                Quantity = 10,
            };

            //act
            var stringContent = new StringContent(JsonConvert.SerializeObject(edit), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{Url}/edit-booked-ticket/{testId}", stringContent);

            //assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }
        [Test, Order(3)]
        public async Task GetBookedDetailTest()
        {
            //arrange
            var client = _factory.CreateClient();

            var testId = Guid.Parse("729FD330-EAC2-47D4-8CFD-49CB19C53923");

            //act

            var response = await client.GetAsync($"{Url}/get-booked-ticket/{testId}");

            //assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }

    }
}