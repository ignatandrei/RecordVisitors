using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecordVisitors
{
    /// <summary>
    /// the connection to the storage( database, csv , others)
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// if record just latest user interaction
        /// or all ( the database will be inflated ...)
        /// </summary>
        bool RecordJustLatest { get; set; }

        /// <summary>
        /// obtain latest users
        /// </summary>
        /// <param name="minutesBeforeNow"> how many minutes before now</param>
        /// <returns>users </returns>
        Task<IUserRecorded[]> GetUsers(uint minutesBeforeNow);
        /// <summary>
        /// save the user
        /// </summary>
        /// <param name="claim">claim of the user</param>
        /// <returns>the user id </returns>
        Task<string> SaveUser(Claim claim);
        /// <summary>
        /// Get user id after the user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>user id or null </returns>
        Task<string> GetUserId(string userName);
        /// <summary>
        /// obtain visitors from date to date
        /// </summary>
        /// <param name="userId">user id to retrieve history</param>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date </param>
        /// <returns></returns>
        Task<IRequestRecorded[]> UserRecordedUrls(string userId, DateTime fromDate, DateTime toDate);
        /// <summary>
        /// save history for the user
        /// </summary>
        /// <param name="rr">the  request that can be recorded </param>
        /// <returns>nothing</returns>
        Task SaveHistory(IRequestRecorded rr);
    }
}