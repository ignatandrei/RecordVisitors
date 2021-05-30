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
    
    public class TestErrors : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public TestErrors(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }
        [Fact]
        public void TestFakeUser()
        {
            _factory.RemoveServices = true;
            _factory.RemoveFakeUser = false;
            var ex = Record.Exception(()=>_factory.CreateClient());

            Assert.IsType<ArgumentException>(ex);
            
        }
        [Fact]
        public async void TestNoUser()
        {
            _factory.RemoveServices = false;

            _factory.RemoveFakeUser= true;
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetStringAsync("/recordVisitors/AllVisitors5Min");

            // Assert
            var str = "Jean Irvine";
            Assert.True(response.Contains(str), $"{response} must contain {str}");

        }
    }
}
