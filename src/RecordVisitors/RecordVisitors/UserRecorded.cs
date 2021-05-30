using System;
using System.Reflection;

namespace RecordVisitors
{
    class UserRecorded : IUserRecorded
    {
        static string nameApp;
        static UserRecorded()
        {
            nameApp = Assembly.GetEntryAssembly().FullName;
        }
        public UserRecorded()
        {
            this.dateRecorded = DateTime.UtcNow;

            this.IdentifierApp = $"{Environment.MachineName}_{Environment.CurrentDirectory}_{nameApp}";
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string IdentifierApp { get; set; }


        public DateTime dateRecorded { get; set; }
    }
}
