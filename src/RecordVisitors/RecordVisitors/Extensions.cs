using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RecordVisitors
{
    public static class Extensions
    {
        //private static void MapJSONVisitors(IApplicationBuilder app,UsersRepository repository)
        //{
        //    app.Run(async context =>
        //    {
        //        var data = await repository.GetClaims();
        //        var json = System.Text.Json.JsonSerializer.Serialize(data);
        //        await context.Response.WriteAsync(json);
        //    });

        //}
        public static IApplicationBuilder UseRecordVisitors(this IApplicationBuilder app)
        {
            app.UseMiddleware<RecordVisitorsMiddleware>();
            return app;
        }
        public static IEndpointRouteBuilder UseVisitors(this IEndpointRouteBuilder endpoints)
        {
            var repo = endpoints.ServiceProvider.GetService<UsersRepository>();
            endpoints.Map("/recordVisitors/AllVisitors", async app => {
                var data = await repo.GetClaims();
                await app.Response.WriteAsJsonAsync(data);
            });
            return endpoints;
        }
        //public static IApplicationBuilder UseEndpointsVisitors(this IApplicationBuilder builder, Action<IEndpointRouteBuilder> configure)
        //{
        //    var repo = builder.ApplicationServices.GetService<UsersRepository>();
        //    builder.Map("/recordVisitors/AllVisitors",app=> MapJSONVisitors(app,repo));
        //    return builder;
        //}
        public static IServiceCollection AddRecordVisitorsDefault(this IServiceCollection services)
        {
            services.AddSingleton<RecordVisitorsMiddleware>();
            services.AddSingleton<RecordVisitorFunctions>();
            services.AddTransient<UsersRepository>();
            return services;
        } 
    }
}
