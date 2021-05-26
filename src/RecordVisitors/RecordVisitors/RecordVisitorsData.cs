using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecordVisitors
{


    public class RecordVisitorsMiddleware : IMiddleware
    {
        private readonly RecordVisitorFunctions recordVisitorFunctions;
        private readonly UsersRepository retrieveUsersRepository;

        public RecordVisitorsMiddleware(RecordVisitorFunctions  recordVisitorFunctions,UsersRepository retrieveUsersRepository)
        {
            this.recordVisitorFunctions = recordVisitorFunctions;
            this.retrieveUsersRepository = retrieveUsersRepository;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claim = recordVisitorFunctions.GetUser(context);
            UserComing?.Invoke(this, claim);
            recordVisitorFunctions.RegisterInScope?.Invoke(context, claim);
            await retrieveUsersRepository.SaveClaim(claim);
            await next(context);
        }

        public event EventHandler<Claim> UserComing;

    }

    public static class EndpointRoutingRecordVisitors
    {
        private static void MapJSONVisitors(IApplicationBuilder app,UsersRepository repository)
        {
            app.Run(async context =>
            {
                var data = await repository.GetClaims();
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                await context.Response.WriteAsync(json);
            });

        }
        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder builder, Action<IEndpointRouteBuilder> configure)
        {
            var repo = builder.ApplicationServices.GetService<UsersRepository>();
            builder.Map("/recordVisitors/AllVisitors",app=> MapJSONVisitors(app,repo));
            return builder;
        }
        public static IServiceCollection AddRecordVisitorsDefault(IServiceCollection services)
        {
            services.AddSingleton<RecordVisitorFunctions>();
            services.AddTransient<UsersRepository>();
            return services;

        } 
    }
}
