using PetAPI.Controllers;
using NSubstitute;
using System.Net;

namespace PetTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetPet_Test()
        {
            //Use Nsubstite to mock the external 3rd party api that is used
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);


            var controller = new PetController(httpClientFactoryMock);

            var actionResult = await controller.GetPets();

            Assert.IsNotNull(actionResult);
        }
    }
}