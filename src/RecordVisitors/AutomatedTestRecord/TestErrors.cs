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
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                Remove<RecordVisitorsMiddleware>(services);
                Remove<IRecordVisitorFunctions>(services);
                Remove<IUsersRepository>(services);
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
        private readonly WebApplicationFactory<Startup> _factory;

        public TestErrors(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }
        [Fact]
        public async void TestFakeUser()
        {
            var ex = Record.Exception(()=>_factory.CreateClient());

            Assert.IsType<ArgumentException>(ex);
            
        }

    }
}
