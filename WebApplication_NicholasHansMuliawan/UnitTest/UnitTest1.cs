using System.Text;
using Contracts.BookTicketModels.RequestModel;
using Contracts.TicketData.RequestModels;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using TrainingUnitTesting;

namespace CustomerTest
{
    public class Tests : BaseTest
    {
        private readonly HttpClient _client;
        private Guid _TicketId;

        public Tests()
        {
            _client = GetClient;
        }


        [Test, Order(1)]
        public async Task TestPost()
        {
            //arrange
            var client = _factory.CreateClient();
            var fromBody = new PostTicketDataRequest()
            {
                TicketCode = "T001",
                TicketName = "Test",
                CategoryName = "Test",
                Date = DateTime.Now,
                Price = 10000,
                Quota = 100,
            };

            //Act
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/v1/book-ticket", temp);

            var content = await response.Content.ReadAsStringAsync();
            var createdTicket = JsonConvert.DeserializeObject<Tickets>(content);
            _TicketId = createdTicket.TicketID;


            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(2)]
        public async Task TestGet()
        {
            //arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("api/v1/get-available");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(3)]
        public async Task TestGetByID()
        {
            //arrange
            var client = _factory.CreateClient();
            //Guid id = await GetExistingCustomerId();

            //Act
            var response = await client.GetAsync($"api/v1/get-booked-id/{_TicketId}");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(4)]
        public async Task TestPut()
        {
            //arrange
            var client = _factory.CreateClient();

            var fromBody = new UpdateBookTicketRequest ()
            {
                BookedTicketId = _TicketId,
                NewQuantity = 1
            };

            //Act
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/v1/edit-booked-ticket/{_TicketId}", temp);

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(5)]
        public async Task TestDelete()
        {
            //arrange
            var client = _factory.CreateClient();

            var fromBody = new DeleteBookTicketRequest()
            {
                BookedId = _TicketId,
                TicketID = "T001",
                Quantity = 1
            };
            //Act
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await client.DeleteAsync($"api/v1/revoke-ticket/{_TicketId}");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }
    }
}