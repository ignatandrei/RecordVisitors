using System;

namespace RecordVisitors
{
    /// <summary>
    /// request of the user 
    /// </summary>
    public interface IRequestRecorded
    {
        /// <summary>
        /// data that you may want to persist
        /// </summary>
        string AdditionalData { get; set; }
        /// <summary>
        /// id of the request
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// the url 
        /// </summary>
        string URL { get; set; }
        /// <summary>
        /// the user id 
        /// </summary>
        string UserRecordedId { get; set; }
        /// <summary>
        /// the data when it was recorded
        /// </summary>
        DateTime DateRecorded { get; set; }
    }
}