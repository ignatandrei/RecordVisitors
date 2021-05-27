using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace RecordVisitors
{
    public class RecordVisitorFunctions : IRecordVisitorFunctions
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

        }
        public Func<HttpContext, Claim> GetUser { get; set; }


    }
}
