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
        /// <returns>how many records were affected when save ( usual 1) </returns>
        Task<int> SaveUser(Claim claim);
    }
}