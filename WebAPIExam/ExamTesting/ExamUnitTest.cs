using System.Net;
using System.Text;
using Contracts.RequestModels;
using Contracts.RequestModels.BookedTicket;
using Newtonsoft.Json;

namespace ExamTesting;
public class ExamUnitTest : BaseTest
{
    private readonly HttpClient _client;

    public ExamUnitTest()
    {
        _client = GetClient;
    }


    [Test]
    public async Task CreateTicket()
    {
        //arrange
        var client = _factory.CreateClient();
        var fromBody = new CreateBookedTicketRequest()
        {
            TicketCode = "BA101",
            Quantity = 50
        };

        var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("api/v1/get-available-ticket", temp);

        // Assert /Validator
        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.Pass();
    }
}

