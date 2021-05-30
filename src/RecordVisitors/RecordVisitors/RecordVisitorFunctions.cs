using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace RecordVisitors
{
    class RecordVisitorFunctions : IRecordVisitorFunctions
    {
        public Func<HttpContext, Claim> GetUser { get; set; }

        Claim IRecordVisitorFunctions.GetUser(HttpContext cnt)
        {


            string name = cnt.User?.Identity?.Name;
            if (name != null)
            {
                return new Claim("user", name);
            }
            return cnt.User?.Claims.FirstOrDefault();

        }
    }
}
