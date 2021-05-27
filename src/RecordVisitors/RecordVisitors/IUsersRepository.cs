using System.Security.Claims;
using System.Threading.Tasks;

namespace RecordVisitors
{
    public interface IUsersRepository
    {
        bool RecordJustLatest { get; set; }

        Task<UserRecorded[]> GetClaims(uint minutesBeforeNow);
        Task<int> SaveClaim(Claim c);
    }
}