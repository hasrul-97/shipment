using ExceptionHandler.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler.Abstraction.BusinessLogic
{
    public interface IBusinessManager
    {
        public Task<string> StoreException(ExceptionDetail exception);
        public Task<List<ExceptionDetail>> GetAllExceptions(string userID);
    }
}
