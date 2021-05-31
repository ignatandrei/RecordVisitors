using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SampleWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using RecordVisitors;
using LightBDD.Framework;
using System.Net.Http;
using LightBDD.XUnit2;
using LightBDD.Framework.Scenarios;

namespace AutomatedTestRecord
{
    public class CustomWebApplicationFactory
    : WebApplicationFactory<Startup> 
    {
        public bool RemoveServices =false;
        public bool RemoveFakeUser = false;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                if (RemoveServices)
                {
                    Remove<RecordVisitorsMiddleware>(services);
                    Remove<IRecordVisitorFunctions>(services);
                    Remove<IUsersRepository>(services);
                }
                if (RemoveFakeUser)
                {
                //    var user= services.SingleOrDefault(
                //d => d.ServiceType ==
                //    typeof(MockAuthenticatedUser));
                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var user = scopedServices.GetRequiredService<MockAuthenticatedUser>();
                        user.ReturnFalse = true;
                    }
                    //Remove<IAuthenticationService>(services);
                    //Remove<IClaimsTransformation>(services);
                    //Remove<IAuthenticationHandlerProvider>(services);
                    //Remove<IAuthenticationSchemeProvider>(services);

                }
            });
        }
        protected void Remove<T>(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(T));

            services.Remove(descriptor);

        }
    }
    [FeatureDescription(@"test errors")]
    [Label(nameof(TestErrors))]
    public class TestErrors :  FeatureFixture,IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        HttpClient client;
        string response;
        private void Then_The_Application_Will_Have_Error()
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                client = _factory.CreateClient();
                await Task.Delay(3000);
            }
                );
            //var ex = Record.ExceptionAsync( async () => 
            //{
            //    client = _factory.CreateClient();
            //    await Task.Delay(3000);
            //}
            //    );
            
            //StepExecution.Current.Comment($"the exception is {ex}");
            //Assert.IsType<ArgumentException>(ex);

        }
        private void Then_The_Response_Should_Be_Empty()
        {
            StepExecution.Current.Comment($"the response is {response}");
            var length = response?.Length ?? 0;
            Assert.True(length == 0, " the response should be empty");
        }
        private void Given_The_Application_Starts()
        {
            StepExecution.Current.Comment("!!!Start application!!!!");
            client = _factory.CreateClient();
        }
        private void Given_Factory(bool RemoveServices ,bool RemoveFakeUser)
        {
            _factory.RemoveServices = RemoveServices;
            _factory.RemoveFakeUser = RemoveFakeUser;

        }

        private async Task When_The_User_Access_The_Url(string url)
        {
            response = await client.GetStringAsync(url);
        }

        public TestErrors(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Scenario]
        [ScenarioCategory("VisitorRecord")]

        public async void TestNoServicesAdded()
        {
            await Runner
                .AddSteps(_=> Given_Factory(true,false))
                .AddSteps(Then_The_Application_Will_Have_Error)
                .RunAsync();

            
        }
        //[Scenario]
        //[ScenarioCategory("VisitorRecord")]
        //public async void TestNoUser()
        //{
        //    await Runner
        //        .AddSteps(_ => Given_Factory(false, true))
        //        .AddSteps(Given_The_Application_Starts)
        //        .AddAsyncSteps(_=>When_The_User_Access_The_Url("/recordVisitors/AllVisitors5Min"))
        //        .AddSteps(Then_The_Response_Should_Be_Empty)
        //        .RunAsync();
             

        //    //var str = "JeanIrvine";
        //    //Assert.True(response.Contains(str), $"{response} must contain {str}");

        //}
    }
}
