using ExceptionHandler.Abstraction.DataAccess;
using ExceptionHandler.DataAccess.Queries;
using ExceptionHandler.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler.DataAccess
{
    public class DataAccessManager : IDataAccessManager
    {
        private IRepository _repository;
        public DataAccessManager(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This method is used to fetch the list of exceptions from the database.
        /// </summary>
        /// <returns>List of exceptions</returns>
        public async Task<List<ExceptionDetail>> FetchAllException()
        {
            try
            {
                List<ExceptionDetail> exceptionDetails = await _repository.FetchList<ExceptionDetail>(SQLQueries.FetchExceptionDetails);
                return exceptionDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to fetch the specific user data from the database
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>The user profile data</returns>
        public async Task<UserDetail> FetchUserData(string userID)
        {
            try
            {
                var user = await _repository.FetchDataWithParameter<UserDetail>(SQLQueries.FetchUser, new { UserID = userID });
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to log an exception to the database.
        /// </summary>
        /// <param name="exception">Exception Details</param>
        /// <returns>A string which states the status of the operation</returns>
        public async Task<string> StoreException(ExceptionDetail exception)
        {
            try
            {
                exception.ExceptionGUID = Guid.NewGuid().ToString();
                int numberOfRowsAffected = await _repository.AddToDatabaseWithParameter(SQLQueries.StoreException, exception);
                if (numberOfRowsAffected > 0)
                {
                    return "The exception is logged successfully in the database";
                }
                else
                {
                    return "An error has occured while storing the details";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
