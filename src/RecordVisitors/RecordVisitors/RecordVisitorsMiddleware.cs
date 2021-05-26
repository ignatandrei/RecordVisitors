using Microsoft.AspNetCore.Http;
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
            if (claim == null)
                return;
            UserComing?.Invoke(this, claim);
            recordVisitorFunctions.RegisterInScope?.Invoke(context, claim);
            await retrieveUsersRepository.SaveClaim(claim);
            await next(context);
        }

        public event EventHandler<Claim> UserComing;

    }
}
