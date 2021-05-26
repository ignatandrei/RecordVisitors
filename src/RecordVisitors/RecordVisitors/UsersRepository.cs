using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecordVisitors
{
    public class UsersRepository
    {
        public async Task<Claim[]> GetClaims()
        {
            return null;
        }
        public async Task<bool> SaveClaim(Claim c)
        {
            return  true;
        }

    }
}
