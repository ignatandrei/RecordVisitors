using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("AutomatedTestRecord")]

namespace RecordVisitors
{

    internal class RecordVisitorsMiddleware : IMiddleware
    {
        static RecordVisitorsMiddleware()
        {
            try
            {
                Console.WriteLine($"{ThisAssembly.Project.AssemblyName} version {ThisAssembly.Info.Version}");
            }
            catch
            {
                //do nothing - if console is not available...
            }
        }
        private readonly IRecordVisitorFunctions recordVisitorFunctions;
        private readonly IUsersRepository retrieveUsersRepository;

        public RecordVisitorsMiddleware(IRecordVisitorFunctions recordVisitorFunctions,IUsersRepository retrieveUsersRepository)
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
            await retrieveUsersRepository.SaveUser(claim);
            await next(context);
        }

        public event EventHandler<Claim> UserComing;

    }
}
