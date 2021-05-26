using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecordVisitors
{
    public class UsersRepository
    {
        DbContextOptions<UserRecordVisitors> options;
        public UsersRepository()
        {
            options = new DbContextOptionsBuilder<UserRecordVisitors>()
                .UseInMemoryDatabase(databaseName: "AndreiIgnatRecord")
                .Options;
            
        }
        public async Task<UserRecorded[]> GetClaims()
        {
            using (var cnt = new UserRecordVisitors(options))
            {
                var data = await cnt.UserRecorded.ToArrayAsync();
                return data;               
            }
        }
        public async Task<int> SaveClaim(Claim c)
        {
            var ur = new UserRecorded();
            ur.TypeClaim = c.Type;
            ur.Value = c.Value;
            ur.ValueType = c.ValueType;
            using(var cnt=new UserRecordVisitors(options))
            {
                cnt.UserRecorded.Add(ur);
                return await cnt.SaveChangesAsync();
            }
            
        }

    }
}
