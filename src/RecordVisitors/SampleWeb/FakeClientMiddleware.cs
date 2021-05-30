using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SampleWeb
{
    //credits to https://visualstudiomagazine.com/Blogs/Tool-Tracker/2019/11/mocking-authenticated-users.aspx

    public class MockAuthenticatedUser : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public bool ReturnFalse;
        const string userId = "phv";
        const string userName = "JeanIrvine";
        const string userRole = "ProductManager";

        public MockAuthenticatedUser(
          IOptionsMonitor<AuthenticationSchemeOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          ISystemClock clock)
          : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (ReturnFalse)
                return AuthenticateResult.NoResult();
            var claims = new[]
              {
          new Claim(ClaimTypes.NameIdentifier, userId),
          new Claim(ClaimTypes.Name, userName),
          new Claim(ClaimTypes.Role, userRole),
          new Claim(ClaimTypes.Email, "peter.vogel@phvis.com"),
        };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}

