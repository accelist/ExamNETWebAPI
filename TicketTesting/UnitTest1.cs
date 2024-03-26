using Contracts.Request;
using Entity.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CustomerTesting
{
    [TestFixture]
    public class Tests : BaseTest
    {
        private readonly HttpClient _client;
        public Tests()
        {
            _client = GetClient;
        }

        [Test]
        public async Task TestCreate()
        {
            // arrange
            var client = _factory.CreateClient();
            var fromBody = new TicketModel()
            {
                TicketCode = "T1000",
                Quantity = 5
            };
            // act
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/v1/book-ticket", temp);

            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }

        [Test]
        public async Task TestGet()
        {
            // arrange
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/v1/get-available-ticket");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public async Task TestGetByID()
        {
            // arrange
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/v1/get-booked-ticket/id");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public async Task TestUpdate()
        {
            // arrange
            var client = _factory.CreateClient();
            var fromBody = new TicketModel2()
            {
                TicketCode = "T1U00",
                Quantity = 10
            };

            // act
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/v1/edit-booked-ticket/id", temp);

            // assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public async Task TestDelete()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await _client.DeleteAsync($"api/v1/revoke-ticket/bookedid/ticketcode/qty");

            // assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }
    }
}