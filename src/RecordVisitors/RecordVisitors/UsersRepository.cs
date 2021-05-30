using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RecordVisitors
{
    class UsersRepository : IUsersRepository
    {
        static SemaphoreSlim ss = new SemaphoreSlim(1, 1);
        public bool RecordJustLatest { get; set; }
        DbContextOptions<UserRecordVisitorsContext> options;
        public UsersRepository(DbContextOptions<UserRecordVisitorsContext> options = null)
        {
            RecordJustLatest = true;
            this.options = options;
            if (this.options == null)
                this.options = new DbContextOptionsBuilder<UserRecordVisitorsContext>()
                .UseInMemoryDatabase(databaseName: "AndreiIgnatRecord")
                .Options;

        }
        public async Task<IUserRecorded[]> GetUsers(uint minutesBeforeNow)
        {
            using (var cnt = new UserRecordVisitorsContext(options))
            {
                var date = DateTime.UtcNow.AddMinutes(-minutesBeforeNow);

                var data = await cnt.UserRecorded
                    .Where(it => it.dateRecorded >= date)
                    .ToArrayAsync();

                return data;
            }
        }

        public async Task<int> SaveUser(Claim c)
        {
            try
            {
                if (RecordJustLatest)
                    await ss.WaitAsync();
                var ur = new UserRecorded();
                ur.UserName = c.Value;
                if (RecordJustLatest)
                {

                    using (var cnt = new UserRecordVisitorsContext(options))
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
                using (var cnt = new UserRecordVisitorsContext(options))
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

        public async Task<string> GetUserId(string userName)
        {
            using (var cnt = new UserRecordVisitorsContext(options))
            {
                Console.WriteLine(cnt.UserRecorded.FirstOrDefault().UserName);
                var user = await cnt.UserRecorded.FirstOrDefaultAsync(it => it.UserName == userName);
                return user?.ID;
            }
        }
    }
}