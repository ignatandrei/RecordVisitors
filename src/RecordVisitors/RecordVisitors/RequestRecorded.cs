using System;

namespace RecordVisitors
{
    class RequestRecorded : IRequestRecorded
    {
        public RequestRecorded()
        {
            ID = Guid.NewGuid().ToString();
            DateRecorded = DateTime.UtcNow;
        }
        public string ID { get; set; }
        public string URL { get; set; }
        public string AdditionalData { get; set; }
        public string UserRecordedId { get; set; }
        public DateTime DateRecorded { get ; set ; }
    }
}
