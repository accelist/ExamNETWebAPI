using Contracts.RequestModels.Booking;
using System.Net;

namespace ProjectTesting
{
    public class Tests : BaseTest
    {

        private readonly HttpClient _client;
        public Tests()
        {
            _client = GetClient;
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPost()
        {
            var client = _factory.CreateClient();
            var fromBody = new CreateBookingDataRequest
            {

            }
            Assert.Pass();
        }
    }
}