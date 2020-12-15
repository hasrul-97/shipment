using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionHandler.Entities
{
    public class ExceptionDetail
    {
        public string ExceptionGUID { get; set; }
        public string ExceptionDetails { get; set; }
        public string StackTrace { get; set; }
        public string UserID { get; set; }
        public DateTime Time { get; set; }
        public string Service { get; set; }
    }
}
