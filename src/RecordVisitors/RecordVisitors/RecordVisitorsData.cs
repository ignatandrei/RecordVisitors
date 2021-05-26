using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecordVisitors
{
    public class RecordVisitorsMiddleware : IMiddleware
    {
        public RecordVisitorsMiddleware()
        {
            GetUser = cnt =>
            {
                string name = cnt.User?.Identity?.Name;
                if (name != null)
                {
                    return new Claim("user", name);
                }
                return cnt.User?.Claims.FirstOrDefault();
            };
            RegisterInScope = (cnt, claim) => cnt.Items.Add("userClaimMiddleware", claim);
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claim = GetUser(context);
            UserComing?.Invoke(this, claim);
            RegisterInScope?.Invoke(context, claim);
            Database?.Invoke(claim);
            await next(context);
        }

        private Func<HttpContext, Claim> GetUser;
        private Action<HttpContext, Claim> RegisterInScope;
        public event EventHandler<Claim> UserComing;
        private Action<Claim> Database;

    }

    public static class EndpointRoutingRecordVisitors
    {
        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder builder, Action<IEndpointRouteBuilder> configure)
        {
            return builder;
        }
    }
}
