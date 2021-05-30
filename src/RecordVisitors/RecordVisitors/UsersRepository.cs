using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RecordVisitors
{
    public class UsersRepository : IUsersRepository
    {
        static SemaphoreSlim ss = new SemaphoreSlim(1, 1);
        public bool RecordJustLatest { get; set; }
        DbContextOptions<UserRecordVisitors> options;
        public UsersRepository(DbContextOptions<UserRecordVisitors> options= null)
        {
            RecordJustLatest = true;
            this.options = options;
            if(this.options == null)
                this.options = new DbContextOptionsBuilder<UserRecordVisitors>()
                .UseInMemoryDatabase(databaseName: "AndreiIgnatRecord")
                .Options;

        }
        public async Task<UserRecorded[]> GetClaims(uint minutesBeforeNow)
        {
            using (var cnt = new UserRecordVisitors(options))
            {
                var date = DateTime.UtcNow.AddMinutes(-minutesBeforeNow);

                var data = await cnt.UserRecorded
                    .Where(it => it.dateRecorded >= date)
                    .ToArrayAsync();

                return data;
            }
        }

        public async Task<int> SaveClaim(Claim c)
        {
            try
            {
                if (RecordJustLatest)
                    await ss.WaitAsync();
                var ur = new UserRecorded();
                ur.UserName = c.Value;
                if (RecordJustLatest)
                {

                    using (var cnt = new UserRecordVisitors(options))
                    {
                        var existingRecord = await cnt
                            .UserRecorded
                            .Where(it => it.IdentifierApp == ur.IdentifierApp && it.UserName == ur.UserName)
                            .FirstOrDefaultAsync();

                        if (existingRecord != null)
                        {
                            existingRecord.dateRecorded = DateTime.UtcNow;
                            return await cnt.SaveChangesAsync();

                        }
                    }
                }
                using (var cnt = new UserRecordVisitors(options))
                {
                    cnt.UserRecorded.Add(ur);
                    return await cnt.SaveChangesAsync();
                }
            }
            finally
            {
                if (RecordJustLatest)
                    ss.Release();

            }

        }

    }
}
