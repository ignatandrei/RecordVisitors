using System;

namespace RecordVisitors
{
    public interface IUserRecorded
    {
        DateTime dateRecorded { get; set; }
        string ID { get; set; }
        string IdentifierApp { get; set; }
        string UserName { get; set; }
    }
}