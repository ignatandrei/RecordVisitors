using LightBDD.Framework;
using LightBDD.XUnit2;
using Microsoft.AspNetCore.Mvc.Testing;
using SampleWeb;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using LightBDD.Framework.Scenarios;

namespace AutomatedTestRecord
{
    [FeatureDescription(
@"In order to access personal data
As an user
I want to login into system")]
    [Label("Story-1")]
    public class TestHappyPath : FeatureFixture, IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TestHappyPath(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        HttpClient client;
        string response;
        private void Given_The_Application_Starts()
        {
            StepExecution.Current.Comment("!!!Start application!!!!");
            client = _factory.CreateClient();
        }
        private async Task When_The_User_Access_The_Url(string url)
        {
            response = await client.GetStringAsync(url);
        }
        private async Task Then_The_Response_Should_Contain(string str)
        {
            await Task.Delay(1);
            Assert.True(response.Contains(str), $"{response} must contain {str}");
        }
        [Scenario]
        [Label("TestEndPoint")]
        [ScenarioCategory("HappyPath")]
        public async void TestFakeUser1()
        {
            await Runner
                .AddSteps(Given_The_Application_Starts)
                .AddAsyncSteps(
                    _ => When_The_User_Access_The_Url("/recordVisitors/AllVisitors5Min"),
                    _ => Then_The_Response_Should_Contain("JeanIrvine")
                )
                .RunAsync();
                
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
