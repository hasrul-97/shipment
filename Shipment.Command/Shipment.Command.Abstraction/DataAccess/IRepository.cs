using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Command.Abstraction.DataAccess
{
    public interface IRepository
    {
        Task<int> AddToDatabase(string sqlQuery);
        Task<List<T>> FetchList<T>(string sqlQuery);
        Task<T> FetchData<T>(string sqlQuery);
        Task<int> Update(string sqlQuery);
        Task<int> Delete(string sqlQuery);
        Task<int> AddToDatabaseWithParameter(string sqlQuery, object parameter);
        Task<int> AddToDatabaseBulkWithParameter<T>(string sqlQuery, List<T> parameter);
        Task<List<T>> FetchListWithParameter<T>(string sqlQuery, object parameter);
        Task<T> FetchDataWithParameter<T>(string sqlQuery, object parameter);
        Task<int> UpdateWithParameter(string sqlQuery, object parameter);
        Task<int> DeleteWithParameter(string sqlQuery, object parameter);
    }
}
