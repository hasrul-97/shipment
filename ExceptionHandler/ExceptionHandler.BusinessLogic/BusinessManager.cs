using ExceptionHandler.Abstraction.BusinessLogic;
using ExceptionHandler.Abstraction.DataAccess;
using ExceptionHandler.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler.BusinessLogic
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IDataAccessManager _dataAccessManager;
        public BusinessManager(IDataAccessManager dataAccessManager)
        {
            _dataAccessManager = dataAccessManager;
        }
        /// <summary>
        /// A business manager to fetch all the exceptions from the database.
        /// Since we dont maintain any user table, the user is allowed to fetch the list of exceptions irrespective of him role.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of exceptions</returns>
        public async Task<List<ExceptionDetail>> GetAllExceptions(string userID)
        {
            try
            {
                if (true)
                {
                    return await _dataAccessManager.FetchAllException();
                }
                else
                {
                    throw new Exception("The user does not have the required scope for this operation (OR) The user is not registered with us.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to store exception. It acts as a mediator between the controller and the Data Access layer
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>A string which states the status of the operation</returns>
        public async Task<string> StoreException(ExceptionDetail exception)
        {
            try
            {
                return await _dataAccessManager.StoreException(exception);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This is a helper method which is used to check if the user is an Admin and authorized to view the details.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>A boolean value</returns>
        public async Task<bool> CheckIfUserIsAdmin(string userID)
        {
            try
            {
                var userData = await _dataAccessManager.FetchUserData(userID);
                if (userData != null)
                {
                    if (userData.UserType == "Admin")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
