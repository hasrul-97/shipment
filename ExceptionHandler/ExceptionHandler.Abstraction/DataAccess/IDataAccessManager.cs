using ExceptionHandler.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler.Abstraction.DataAccess
{
    public interface IDataAccessManager
    {
        public Task<UserDetail> FetchUserData(string userID);
        public Task<List<ExceptionDetail>> FetchAllException();
        public Task<string> StoreException(ExceptionDetail exception);
    }
}
