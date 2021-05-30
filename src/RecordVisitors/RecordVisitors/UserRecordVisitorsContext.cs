using Microsoft.EntityFrameworkCore;

namespace RecordVisitors
{
    class UserRecordVisitorsContext : DbContext
    {
        public UserRecordVisitorsContext(DbContextOptions<UserRecordVisitorsContext> options)
            : base(options)
        { }
        public DbSet<UserRecorded> UserRecorded { get; set; }
    }
}
