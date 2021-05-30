using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace RecordVisitors
{
    /// <summary>
    /// how to indentify the user
    /// </summary>
    public interface IRecordVisitorFunctions
    {
        /// <summary>
        /// obtain the user
        /// used to store - <see cref="IUserRecorded.UserName"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns>claim for the user</returns>
        Claim GetUser(HttpContext context) ;

        /// <summary>
        /// Gets the url to be saved into the database
        /// </summary>
        /// <param name="context"></param>
        /// <returns>null or IRequestRecorded - without user id</returns>
        IRequestRecorded GetUrl(HttpContext context);
        
    }
}