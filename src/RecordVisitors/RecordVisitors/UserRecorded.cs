using Microsoft.EntityFrameworkCore;
using System;

namespace RecordVisitors
{
    public class UserRecorded
    {
        public UserRecorded()
        {
            this.dateRecorded = DateTime.UtcNow;
        }
        public int ID { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string TypeClaim { get; set; }

        public DateTime dateRecorded { get; set; }
    }
    public class UserRecordVisitors : DbContext
    {
        public UserRecordVisitors(DbContextOptions<UserRecordVisitors> options)
            : base(options)
        { }
        public DbSet<UserRecorded> UserRecorded { get; set; }
    }
}
