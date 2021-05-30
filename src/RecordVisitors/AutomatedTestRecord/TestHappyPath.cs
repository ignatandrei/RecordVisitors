using Microsoft.AspNetCore.Mvc.Testing;
using SampleWeb;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AutomatedTestRecord
{
    
    public class TestHappyPath : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TestHappyPath(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async void TestFakeUser()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetStringAsync("/recordVisitors/AllVisitors5Min");

            // Assert
            var str = "JeanIrvine";
            Assert.True(response.Contains(str),$"{response} must contain {str}");
                
        }
        [Fact]
        public async void TestEndpointMoreThan5Minutes()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetStringAsync("/recordVisitors/AllVisitors/5");

            // Assert
            var str = "JeanIrvine";
            Assert.True(response.Contains(str), $"{response} must contain {str}");

        }
        [Fact]
        public async void TestEndpointGetUser()
        {
            // Arrange
            // Act
            var response = await GetUserId("JeanIrvine");

            Assert.NotNull(response);
            Assert.NotEmpty(response);
            // Assert
            if (!Guid.TryParse(response, out var g))
            {
                Assert.True(false, $"the {response} should be a guid");
            };

        }
        private Task<string> GetUserId(string userName)
        {
            var client = _factory.CreateClient();            
            var response = client.GetStringAsync($"/recordVisitors/GetUserId/{userName}");
            return response;
        }
        [Fact]
        public async void TestEndpointGetHistoryUser()
        {
            var user = await GetUserId("JeanIrvine");
            // Arrange
            var client = _factory.CreateClient();
            string arguments = $"{user}/1970-04-16/" + DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd"); 
            // Act
            var response = await client.GetStringAsync($"/recordVisitors/UserHistory/{arguments}");

            Assert.NotNull(response);
            Assert.NotEmpty(response);
            

        }
    }
}
