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
    [Label(nameof(TestHappyPath))]
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
        private void Then_The_Response_Should_Contain(string str)
        {            
            Assert.True(response.Contains(str), $"{response} must contain {str}");
        }
        private void Then_The_Response_Should_Be_Guid()
        {
            var b = Guid.TryParse(response, out var _);
            Assert.True(b, $"{response} must be guid");

        }
        private void Then_The_Response_Should_Be_Something()
        {
            StepExecution.Current.Comment($"the response is:{response}");
            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.True(response.Length > 5, $"the {response} should have a decent length");


        }
        [Scenario]
        [ScenarioCategory("VisitorRecord")]
        public async void TestFakeUser()
        {
            await Runner
                .AddSteps(Given_The_Application_Starts)
                .AddAsyncSteps(
                    _ => When_The_User_Access_The_Url("/recordVisitors/AllVisitors5Min")
                )
                .AddSteps(
                    _ => Then_The_Response_Should_Contain("JeanIrvine")

                )
                .RunAsync();
                
        }
        [Scenario]
        [ScenarioCategory("VisitorRecord")]
        public async void TestUserEndpoint()
        {

            await Runner
                .AddSteps(_ => It_should_have_UserId("JeanIrvine"))
                .RunAsync();

        }
        //[Fact]
        //public async void TestFakeUser()
        //{

        //    // Arrange
        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await client.GetStringAsync("/recordVisitors/AllVisitors5Min");

        //    // Assert
        //    var str = "JeanIrvine";
        //    Assert.True(response.Contains(str),$"{response} must contain {str}");

        //}
         
        
        private CompositeStep It_should_have_UserId(string userName)
        {
            var step = CompositeStep.DefineNew()
                .AddSteps(Given_The_Application_Starts)
                .AddAsyncSteps(_ => When_The_User_Access_The_Url($"/recordVisitors/GetUserId/{userName}"))
                .AddSteps(Then_The_Response_Should_Be_Guid)
                .Build();
            return step;
        }

        [Scenario]
        [ScenarioCategory("VisitorHsitory")]
        public async void TestEndpointGetHistoryUser()
        {
            await Runner
                .AddSteps(_ => It_should_have_UserId("JeanIrvine"))
                .AddAsyncSteps(_=>
                When_The_User_Access_The_Url(
                    $"/recordVisitors/UserHistory/{this.response}/1970-04-16/" + DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")
                    )
                )
                .AddSteps(Then_The_Response_Should_Be_Something)
                .RunAsync();

            

        }
    }
}
