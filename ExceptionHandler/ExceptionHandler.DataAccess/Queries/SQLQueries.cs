using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionHandler.DataAccess.Queries
{
    /// <summary>
    /// This class contains the list of SQL Queries used by the Data Access Layer
    /// </summary>
    public class SQLQueries
    {
        public static readonly string FetchUser = "Select * FROM [dbo].[User] WHERE [UserID]=@UserID";
        public static readonly string FetchExceptionDetails = "Select * FROM shipment.logger";
        public static readonly string StoreException = "INSERT INTO shipment.logger (ExceptionGUID,ExceptionDetails,StackTrace,UserID,Time,Service) VALUES (@ExceptionGUID" +
            ",@ExceptionDetails,@StackTrace,@UserID,@Time,@Service)";
    }
}
