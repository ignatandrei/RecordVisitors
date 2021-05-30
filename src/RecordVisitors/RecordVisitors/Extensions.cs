using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RecordVisitors
{
    /// <summary>
    /// ASP.NET Core extensions to add easy  to your project
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// use as extension
        /// </summary>
        /// <example>
        /// <code>
        /// in Configure(IApplicationBuilder app, IWebHostEnvironment env)
        /// //code omitted 
        /// app.UseAuthentication();
        /// //put AFTER authentication
        ///  app.UseRecordVisitors();
        /// </code>
        /// </example>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRecordVisitors(this IApplicationBuilder app)
        {
            app.UseMiddleware<RecordVisitorsMiddleware>();
            return app;
        }
        /// <summary>
        /// If you want to see the endpoints
        /// </summary>
        /// <example>
        /// <code>
        /// app.UseEndpoints(endpoints =>
        ///{
        ///    endpoints.MapControllers();
        ///    endpoints.UseVisitors();
        ///});
        ///</code>
        ///</example>
        /// <param name="endpoints"></param>
        /// <returns></returns>
        public static IEndpointRouteBuilder UseVisitors(this IEndpointRouteBuilder endpoints)
        {
            var repo = endpoints.ServiceProvider.GetService<IUsersRepository>();
            if(repo == null)
            {
                throw new ArgumentException("please add IUsersRepository DI : did you add services.AddRecordVisitorsDefault(); ? ");
            }
            endpoints.MapGet("/recordVisitors/AllVisitors5Min", async app => {
                
                var data = await repo.GetUsers(5);
                await app.Response.WriteAsJsonAsync(data);
            });
            endpoints.MapGet("/recordVisitors/AllVisitors/{time:int}", async app => {

                var time = app.Request.RouteValues["time"]?.ToString();
                var val = uint.Parse(time);
                var data = await repo.GetUsers(val);
                await app.Response.WriteAsJsonAsync(data);
            });
            endpoints.MapGet("/recordVisitors/GetUserId/{userName:alpha}", async app => {

                var val = app.Request.RouteValues["userName"]?.ToString();
                var data = await repo.GetUserId(val);
                await app.Response.WriteAsync(data);
            });
            endpoints.MapGet("/recordVisitors/UserHistory/{userId}/{dateFrom:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}/{dateTo?}", async app => {

                var userID = app.Request.RouteValues["userId"]?.ToString();
                var dateFromStr = app.Request.RouteValues["dateFrom"]?.ToString();
                var dateToStr = app.Request.RouteValues["dateTo"]?.ToString();
                var dateTo = DateTime.UtcNow.AddDays(2);
                if(dateToStr != null)
                {
                    dateTo = DateTime.ParseExact(dateToStr,"yyyy-MM-dd",null);
                }

                var dateFrom= DateTime.ParseExact(dateFromStr,"yyyy-MM-dd", null);
                
                var data = await repo.UserRecordedUrls(userID,dateFrom,dateTo);
                await app.Response.WriteAsJsonAsync(data);
            });

            return endpoints;
        }
        /// <summary>
        /// Used for extension ASP.NET Core
        /// </summary>
        /// <example>
        /// <code>
        /// public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        ///     //code omitted 
        /// app.UseAuthentication();
        /// //put AFTER authentication
        /// app.UseRecordVisitors();
        /// </code>
        /// </example>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRecordVisitorsDefault(this IServiceCollection services)
        {
            services.AddSingleton<RecordVisitorsMiddleware>();
            services.AddSingleton<IRecordVisitorFunctions>(new RecordVisitorFunctions());
            services.AddTransient<IUsersRepository>(sc=>new UsersRepository());
            return services;
        } 
    }
}
