using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace RecordVisitors
{
    public interface IRecordVisitorFunctions
    {
        Func<HttpContext, Claim> GetUser { get; set; }
    }
}