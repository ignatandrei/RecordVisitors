using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace RecordVisitors
{
    public interface IRecordVisitorFunctions
    {
        Claim GetUser(HttpContext context) ;
    }
}