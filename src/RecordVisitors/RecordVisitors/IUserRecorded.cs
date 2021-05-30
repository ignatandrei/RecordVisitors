using System;

namespace RecordVisitors
{
    /// <summary>
    /// the user recorded interface to store data
    /// </summary>
    public interface IUserRecorded
    {
        /// <summary>
        /// when the visit to the site was done
        /// </summary>
        DateTime dateRecorded { get; set; }
        /// <summary>
        /// the PK
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// what app stored this
        /// </summary>
        string IdentifierApp { get; set; }
        /// <summary>
        ///  user name obtained from
        ///  <see cref="IRecordVisitorFunctions.GetUser(Microsoft.AspNetCore.Http.HttpContext)"/>
        /// </summary>
        string UserName { get; set; }
    }
}