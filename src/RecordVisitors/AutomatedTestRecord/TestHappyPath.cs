using Microsoft.AspNetCore.Mvc.Testing;
using SampleWeb;
using System;
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
            var str = "Jean Irvine";
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
            var str = "Jean Irvine";
            Assert.True(response.Contains(str), $"{response} must contain {str}");

        }
    }
}
