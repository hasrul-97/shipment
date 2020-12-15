using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Query.Entities
{
    public class ExceptionDetail
    {
        public ExceptionDetail(string exceptionDetails, string stackTrace, string userID, string service)
        {
            ExceptionDetails = exceptionDetails;
            StackTrace = stackTrace;
            UserID = userID;
            Service = service;
            Time = DateTime.UtcNow;
            ExceptionGUID = Guid.NewGuid().ToString();
        }
        public string ExceptionGUID { get; set; }
        public string ExceptionDetails { get; set; }
        public string StackTrace { get; set; }
        public string UserID { get; set; }
        public DateTime Time { get; set; }
        public string Service { get; set; }
    }
}
