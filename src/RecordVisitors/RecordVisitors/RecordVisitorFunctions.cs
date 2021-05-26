using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace RecordVisitors
{
    public class RecordVisitorFunctions
    {
        public RecordVisitorFunctions()
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
        public Func<HttpContext, Claim> GetUser;
        public Action<HttpContext, Claim> RegisterInScope;

    
    }
}
